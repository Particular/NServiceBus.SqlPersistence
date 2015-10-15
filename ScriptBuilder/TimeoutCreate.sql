﻿
declare @tableName nvarchar(max) = '[' + @schema + '].[' + @endpointName + '.TimeoutData]';

IF NOT EXISTS (
    SELECT * FROM sys.objects 
    WHERE 
        object_id = OBJECT_ID(@tableName) 
        AND type in (N'U')
)
BEGIN
DECLARE @createTable nvarchar(max);
SET @createTable = N'
    CREATE TABLE ' + @tableName + '(
	    [Id] [uniqueidentifier] NOT NULL PRIMARY KEY,
	    [Destination] [nvarchar](1024) NULL,
	    [SagaId] [uniqueidentifier] NULL,
	    [State] [varbinary](max) NULL,
	    [Time] [datetime] NULL,
	    [Headers] [xml] NULL,
	    [PersistenceVersion] [nvarchar](23) NOT NULL
    )
';
exec(@createTable);
END
