﻿SELECT
	TABLE_NAME AS [Name],
	CONCAT(TABLE_CATALOG, ',', TABLE_SCHEMA) AS [ArtifactNamespace],
	COLUMN_NAME AS [Schema.Name],
	TABLE_SCHEMA AS [DataSource.Schema]
FROM
	INFORMATION_SCHEMA.COLUMNS
ORDER BY
	TABLE_SCHEMA,
	TABLE_NAME,
	ORDINAL_POSITION