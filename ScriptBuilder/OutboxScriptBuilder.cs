﻿using System.IO;
using Resourcer;

namespace NServiceBus.SqlPersistence
{
    public static class OutboxScriptBuilder
    {

        public static void BuildCreateScript(TextWriter writer)
        {
            writer.Write(Resource.AsString("OutboxCreate.sql"));
        }

        public static void BuildDropScript(TextWriter writer)
        {
            writer.Write(Resource.AsString("OutboxDrop.sql"));
        }
    }
}