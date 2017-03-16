﻿using System;

namespace NServiceBus.Persistence.Sql.ScriptBuilder
{
    /// <summary>
    /// Not for public use.
    /// </summary>
    public static class OracleCorrelationPropertyTypeConverter
    {
        public static string GetColumnType(CorrelationPropertyType propertyType)
        {
            switch (propertyType)
            {
                case CorrelationPropertyType.DateTime:
                    return "DATE";
                case CorrelationPropertyType.DateTimeOffset:
                    return "DATE";
                case CorrelationPropertyType.String:
                    return "NVARCHAR2(200)";
                case CorrelationPropertyType.Int:
                    return "NUMBER(19)";
                case CorrelationPropertyType.Guid:
                    return "VARCHAR2(38)";
            }
            throw new Exception($"Could not convert {propertyType}.");
        }
    }
}