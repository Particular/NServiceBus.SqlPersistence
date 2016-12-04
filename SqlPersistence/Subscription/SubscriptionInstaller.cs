﻿using System.IO;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Installation;
using NServiceBus.Logging;
using NServiceBus.Persistence;
using NServiceBus.Settings;

class SubscriptionInstaller : INeedToInstallSomething
{
    static ILog log = LogManager.GetLogger<SubscriptionInstaller>();
    ReadOnlySettings settings;

    public SubscriptionInstaller(ReadOnlySettings settings)
    {
        this.settings = settings;
    }

    public Task Install(string identity)
    {
        if (!settings.ShouldInstall<StorageType.Subscriptions>())
        {
            return Task.FromResult(0);
        }
        var connectionBuilder = settings.GetConnectionBuilder<StorageType.Subscriptions>();
        var sqlVarient = settings.GetSqlVarient();

        var endpointName = settings.GetTablePrefixForEndpoint<StorageType.Subscriptions>();
        var createScript = Path.Combine(ScriptLocation.FindScriptDirectory(sqlVarient), "Subscription_Create.sql");
        log.Info($"Executing '{createScript}'");
        return SqlHelpers.Execute(connectionBuilder, File.ReadAllText(createScript),
            manipulateCommand: command =>
            {
                command.AddParameter("schema", settings.GetSchema<StorageType.Subscriptions>());
                command.AddParameter("tablePrefix", endpointName);
            });
    }

}