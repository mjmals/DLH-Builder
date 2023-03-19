SELECT
	FORMATMESSAGE('%s.%s', col.TABLE_SCHEMA, col.TABLE_NAME) AS TableFullname,
	col.TABLE_SCHEMA AS SchemaName,
	col.TABLE_NAME AS [TableName],
	col.COLUMN_NAME AS [ColumnName],
	REPLACE(FORMATMESSAGE('StructField("%s", %s, %s, {%s})',
		col.COLUMN_NAME,
		CASE
			WHEN RIGHT(col.DATA_TYPE, 3) IN ('int')
				THEN CASE col.DATA_TYPE
					WHEN 'bigint' THEN 'BigIntegerDataType()'
					WHEN 'smallint' THEN 'SmallIntegerDataType()'
					WHEN 'tinyint' THEN 'TinyIntegerDataType()'
					ELSE FORMATMESSAGE('IntegerDataType()')
				END
			WHEN RIGHT(col.DATA_TYPE, 4) IN ('char', 'text')
				THEN FORMATMESSAGE(
					'%sStringDataType(%i)', 
					CASE LEFT(col.DATA_TYPE, 1) WHEN 'n' THEN 'Unicode' ELSE '' END, 
					col.CHARACTER_MAXIMUM_LENGTH
				)
			WHEN col.DATA_TYPE = 'decimal'
				THEN FORMATMESSAGE('DecimalDataType(%i,%i)', col.NUMERIC_PRECISION, col.NUMERIC_SCALE)
			WHEN col.DATA_TYPE = 'numeric'
				THEN FORMATMESSAGE('NumericDataType(%i,%i)', col.NUMERIC_PRECISION, col.NUMERIC_SCALE)
			WHEN col.DATA_TYPE IN ('datetime', 'smalldatetime')
				THEN 'TimestampDataType()'
			WHEN col.DATA_TYPE = 'DateTime2' 
				THEN 'TimestampDataType()'
			WHEN col.DATA_TYPE = 'date'
				THEN 'DateDataType()'
			WHEN col.DATA_TYPE = 'time'
				THEN 'TimeDataType()'
			WHEN col.DATA_TYPE = 'bit'
				THEN 'BooleanDataType()'
			WHEN col.DATA_TYPE = 'money'
				THEN 'MoneyDataType()'
			WHEN col.DATA_TYPE = 'smallmoney'
				THEN 'SmallMoneyDataType()'
			WHEN col.DATA_TYPE = 'float'
				THEN FORMATMESSAGE('FloatDataType(%i)', col.NUMERIC_PRECISION)
			WHEN col.DATA_TYPE LIKE '%binary'
				THEN FORMATMESSAGE('BinaryDataType(%i)', col.CHARACTER_MAXIMUM_LENGTH)
			WHEN col.DATA_TYPE IN ('rowversion', 'timestamp')
				THEN FORMATMESSAGE('BinaryDataType(%i)', 8)
			WHEN col.DATA_TYPE IN ('uniqueidentifier')
				THEN 'UniqueIdentifier()'
			WHEN col.DATA_TYPE IN ('geography')
				THEN 'StringDataType()'
			WHEN col.DATA_TYPE IN ('hierarchyid')
				THEN 'HierarchyType()'
			WHEN col.DATA_TYPE IN ('xml')
				THEN 'XMLType()'

		END,
		CASE col.IS_NULLABLE WHEN 'YES' THEN 'True' ELSE 'False' END,
		CASE
			WHEN keymap.IsPrimaryKey = 1 THEN 'keytype:Primary'
			WHEN keymap.IsForeignKey = 1 THEN 'keytype:Foreign'
			WHEN keymap.IsUniqueKey = 1 THEN 'keytype:Unique'
			WHEN col.DATA_TYPE IN ('rowversion', 'timestamp') THEN 'keytype:Version'
			ELSE ''
		END
		+ CASE WHEN col.COLLATION_NAME LIKE '%_CS%' THEN ',casesensitive:true' ELSE '' END
	)
	, '{,', '{'
	) AS StructField
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