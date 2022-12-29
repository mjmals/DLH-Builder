SELECT
	'{0}' AS [Name],
	CONCAT_WS(',', DB_NAME(), 'Stored Procedures', OBJECT_SCHEMA_NAME(OBJECT_ID('{0}'))) AS ArtifactNamespace,
	name AS [Schema.Name],
	system_type_name AS [Schema.DataType],
	OBJECT_SCHEMA_NAME(OBJECT_ID('{0}')) AS [DataSource.Schema],
	'StoredProcedure' AS [DataSource.ObjectType],
	'{0}' AS [DataSource.ObjectName],
	CONCAT_WS('.', DB_NAME(), OBJECT_SCHEMA_NAME(OBJECT_ID('{0}')), '{0}') AS [DataSource.Name],
	'None' AS [Schema.KeyType],
	'true' AS [Schema.IsNullable],
	CASE WHEN collation_name LIKE '%_CS%' THEN 'true' ELSE 'false' END AS [DataType.IsCaseSensitive],
	CASE WHEN collation_name LIKE '%_AS%' THEN 'true' ELSE 'false' END AS [DataType.IsAccentSensitive]
FROM 
	sys.dm_exec_describe_first_result_set(N'{0}', null, 0)