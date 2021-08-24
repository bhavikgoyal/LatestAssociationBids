SELECT COLUMN_NAME,* 
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'YourTableName'

SELECT TABLE_NAME, COLUMN_NAME
FROM INFORMATION_SCHEMA.COLUMNS
WHERE COLUMN_NAME = 'Status'
ORDER BY TABLE_NAME


SELECT DISTINCT TABLE_NAME
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME NOT IN (
	SELECT TABLE_NAME
	FROM INFORMATION_SCHEMA.COLUMNS
	WHERE COLUMN_NAME = 'PortalKey'
)
ORDER BY TABLE_NAME


SELECT TABLE_NAME, COLUMN_NAME, TABLE_NAME + ' = ' + CAST(ROW_NUMBER() OVER ( ORDER BY TABLE_NAME ) AS VARCHAR(5)) + ','
FROM INFORMATION_SCHEMA.COLUMNS
WHERE COLUMN_NAME = 'ModuleKey'
ORDER BY TABLE_NAME



select '<table name="' + T.TABLE_NAME + '" HasApprovedState="' + ISNULL(A.HasApprovedState, 'N') + '" HasPaging="N" Migrate="N">' + ISNULL(A.FilterFields, '') + '</table>'
FROM INFORMATION_SCHEMA.TABLES T
LEFT JOIN (
	SELECT TABLE_NAME, 'Y' AS HasApprovedState, '<filterfields><field name="Status" bitwise="Y"></field></filterfields>' AS FilterFields
	FROM INFORMATION_SCHEMA.COLUMNS
	WHERE COLUMN_NAME = 'Status'
) A ON T.TABLE_NAME = A.TABLE_NAME
ORDER BY T.TABLE_NAME




SELECT  
    fk.name,
    OBJECT_NAME(fk.parent_object_id) 'Parent table',
    c1.name 'Parent column',
    OBJECT_NAME(fk.referenced_object_id) 'Referenced table',
    c2.name 'Referenced column'
FROM 
    sys.foreign_keys fk
INNER JOIN 
    sys.foreign_key_columns fkc ON fkc.constraint_object_id = fk.object_id
INNER JOIN
    sys.columns c1 ON fkc.parent_column_id = c1.column_id AND fkc.parent_object_id = c1.object_id
INNER JOIN
    sys.columns c2 ON fkc.referenced_column_id = c2.column_id AND fkc.referenced_object_id = c2.object_id
where c2.name = 'LookUpKey'
order by OBJECT_NAME(fk.parent_object_id)



SELECT Controller + ' = ' + cast(ModuleKey AS VARCHAR(5)) + ',' 
FROM Module


//------------------- change database owner to be able to create database diagrams

ALTER AUTHORIZATION ON DATABASE::[AssociationBids] TO sa
go