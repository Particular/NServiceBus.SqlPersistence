﻿using System;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.AcceptanceTesting;
using NServiceBus.AcceptanceTests;
using NServiceBus.AcceptanceTests.EndpointTemplates;
using NServiceBus.Persistence.Sql;
using NUnit.Framework;

[TestFixture]
public class When_different_persistence_is_used_for_outbox : NServiceBusAcceptanceTest
{
    [Test]
    public void Should_throw_exception_at_startup()
    {
        var ex = Assert.ThrowsAsync<System.Exception>(async () =>
        {
            await Scenario.Define<Context>()
                .WithEndpoint<EndpointWithDummySaga>()
                .Done(c => c.Done)
                .Run()
                .ConfigureAwait(false);
        });

        StringAssert.StartsWith("Sql Persistence must be enable for either both Sagas and Outbox, or neither.", ex.Message);
    }

    public class Context : ScenarioContext
    {
        public bool SessionCreated { get; set; }
        public bool Done { get; set; }
        public string ExceptionMessage { get; set; }
    }

    public class EndpointWithDummySaga : EndpointConfigurationBuilder
    {
        public EndpointWithDummySaga()
        {
            EndpointSetup<DefaultServer>(c =>
            {
                c.EnableOutbox();
                c.UsePersistence<InMemoryPersistence, StorageType.Outbox>();
            });
        }
        
        //This saga is not used but required to activate the saga feature
        public class DummySaga : SqlSaga<DummySagaData>, IAmStartedByMessages<DummyMessage>
        {
            protected override string CorrelationPropertyName => "Dummy";
            protected override void ConfigureMapping(IMessagePropertyMapper mapper)
            {
                mapper.ConfigureMapping<DummyMessage>(m => null);
            }

            public Task Handle(DummyMessage message, IMessageHandlerContext context)
            {
                throw new System.NotImplementedException();
            }
        }

        public class DummySagaData : ContainSagaData
        {
            public string Dummy { get; set; }
        }
    }

    public class DummyMessage : IMessage
    {
    }
}