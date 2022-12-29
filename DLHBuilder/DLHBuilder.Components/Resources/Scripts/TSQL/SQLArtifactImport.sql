SELECT
	col.TABLE_NAME AS [Name],
	CONCAT_WS(',', col.TABLE_CATALOG, 'Tables', col.TABLE_SCHEMA) AS [ArtifactNamespace],
	col.COLUMN_NAME AS [Schema.Name],
	CASE
		WHEN RIGHT(col.DATA_TYPE, 4) IN ('char', 'text') THEN FORMATMESSAGE('%s(%s)', col.DATA_TYPE,CASE WHEN col.CHARACTER_MAXIMUM_LENGTH = -1 THEN 'MAX' ELSE CONVERT(VARCHAR(10), col.CHARACTER_MAXIMUM_LENGTH) END)
		WHEN col.DATA_TYPE IN ('decimal', 'numeric') THEN FORMATMESSAGE('%s(%i,%i)', col.DATA_TYPE, col.NUMERIC_PRECISION, col.NUMERIC_SCALE)
		WHEN col.DATA_TYPE LIKE '%binary' THEN FORMATMESSAGE('%s(%s)', col.DATA_TYPE, CASE WHEN col.CHARACTER_MAXIMUM_LENGTH = -1 THEN 'MAX' ELSE CONVERT(VARCHAR(10), col.CHARACTER_MAXIMUM_LENGTH) END)
		WHEN col.DATA_TYPE IN ('rowversion', 'timestamp') THEN 'varbinary(8)'
		ELSE col.DATA_TYPE
	END AS [Schema.DataType],
	col.TABLE_SCHEMA AS [DataSource.Schema],
	'Table' AS [DataSource.ObjectType],
	col.TABLE_NAME AS [DataSource.ObjectName],
	FORMATMESSAGE('%s.%s.%s', col.TABLE_CATALOG, col.TABLE_SCHEMA, col.TABLE_NAME) AS [DataSource.Name],
	CASE
		WHEN keymap.IsPrimaryKey = 1 THEN 'Primary'
		WHEN keymap.IsForeignKey = 1 THEN 'Foreign'
		WHEN keymap.IsUniqueKey = 1 THEN 'Unique'
		WHEN col.DATA_TYPE IN ('rowversion', 'timestamp') THEN 'Version'
		ELSE 'None'
	END AS [Schema.KeyType],
	CASE col.IS_NULLABLE WHEN 'Yes' THEN 'true' ELSE 'false' END AS [Schema.IsNullable],
	CASE WHEN col.COLLATION_NAME LIKE '%_CS%' THEN 'true' ELSE 'false' END AS [DataType.IsCaseSensitive],
	CASE WHEN col.COLLATION_NAME LIKE '%_AS%' THEN 'true' ELSE 'false' END AS [DataType.IsAccentSensitive]
FROM
	INFORMATION_SCHEMA.COLUMNS AS col
	LEFT JOIN
		(
			SELECT
				keycol.TABLE_CATALOG,
				keycol.TABLE_SCHEMA,
				keycol.TABLE_NAME,
				keycol.COLUMN_NAME,
				MAX(CASE WHEN keys.CONSTRAINT_TYPE = 'PRIMARY KEY' THEN 1 ELSE 0 END) AS IsPrimaryKey,
				MAX(CASE WHEN keys.CONSTRAINT_TYPE = 'FOREIGN KEY' THEN 1 ELSE 0 END) AS IsForeignKey,
				MAX(CASE WHEN keys.CONSTRAINT_TYPE = 'UNIQUE' THEN 1 ELSE 0 END) AS IsUniqueKey
			FROM
				INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS keycol
				INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS keys ON
					keycol.CONSTRAINT_NAME = keys.CONSTRAINT_NAME
					AND keycol.CONSTRAINT_SCHEMA = keys.CONSTRAINT_SCHEMA
					AND keycol.CONSTRAINT_CATALOG = keys.CONSTRAINT_CATALOG
			GROUP BY
				keycol.TABLE_CATALOG,
				keycol.TABLE_SCHEMA,
				keycol.TABLE_NAME,
				keycol.COLUMN_NAME
		) AS keymap
		ON col.TABLE_CATALOG = keymap.TABLE_CATALOG
		AND col.TABLE_SCHEMA = keymap.TABLE_SCHEMA
		AND col.TABLE_NAME = keymap.TABLE_NAME
		AND col.COLUMN_NAME = keymap.COLUMN_NAME
ORDER BY
	col.TABLE_SCHEMA,
	col.TABLE_NAME,
	col.ORDINAL_POSITION