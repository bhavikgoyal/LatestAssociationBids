USE [AssociationBids]
GO

----------------------------------------------------------------------
/*

select object_name(parent_object_id) ParentTableName,
  object_name(referenced_object_id) RefTableName,
  object_name(object_id) ForeignKeyName
from sys.foreign_keys
where object_name(referenced_object_id) = 'LookUp'

*/
----------------------------------------------------------------------

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[config_LookUp_InsertData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[config_LookUp_InsertData]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[config_LookUp_InsertData]
AS
SET NOCOUNT ON

CREATE TABLE #LT ( LookUpTypeTitle VARCHAR(150) )

INSERT INTO #LT ( LookUpTypeTitle ) VALUES  ( 'Status' )
INSERT INTO #LT ( LookUpTypeTitle ) VALUES  ( 'Access' )
INSERT INTO #LT ( LookUpTypeTitle ) VALUES  ( 'Task Status' )
INSERT INTO #LT ( LookUpTypeTitle ) VALUES  ( 'Task Priority' )
INSERT INTO #LT ( LookUpTypeTitle ) VALUES  ( 'Email Status' )
INSERT INTO #LT ( LookUpTypeTitle ) VALUES  ( 'Bid Request Status' )
INSERT INTO #LT ( LookUpTypeTitle ) VALUES  ( 'Bid Vendor Status' )
INSERT INTO #LT ( LookUpTypeTitle ) VALUES  ( 'Bid Status' )
INSERT INTO #LT ( LookUpTypeTitle ) VALUES  ( 'Message Status' )

INSERT INTO #LT ( LookUpTypeTitle ) VALUES  ( 'Company Type' )
INSERT INTO #LT ( LookUpTypeTitle ) VALUES  ( 'Resource Type' )
INSERT INTO #LT ( LookUpTypeTitle ) VALUES  ( 'Pricing Type' )
INSERT INTO #LT ( LookUpTypeTitle ) VALUES  ( 'Payment Type' )
INSERT INTO #LT ( LookUpTypeTitle ) VALUES  ( 'Frequency Type' )

SET NOCOUNT OFF


DECLARE @lookUpTypeTitle VARCHAR(150),
		@lookUpTypeKey INT,
		@errorCode INT

DECLARE object_cursor CURSOR FOR 
SELECT LookUpTypeTitle
FROM #LT

OPEN object_cursor

FETCH NEXT FROM object_cursor 
INTO @lookUpTypeTitle

WHILE @@FETCH_STATUS = 0
BEGIN

	EXEC dbo.config_LookUpType_InsertOne 
		@lookUpTypeTitle,
	    @lookUpTypeKey OUTPUT,
	    @errorCode OUTPUT

	FETCH NEXT FROM object_cursor 
	INTO @lookUpTypeTitle

END

CLOSE object_cursor
DEALLOCATE object_cursor

DROP TABLE #LT

----------------------------------------------------------

SET NOCOUNT ON 

CREATE TABLE #L (LookUpTypeTitle VARCHAR(150), LookUpTitle VARCHAR(150), SortOrder FLOAT, Value INT)

INSERT INTO #L ( LookUpTypeTitle, LookUpTitle, SortOrder, Value ) VALUES  ( 'Status', 'Pending', 1, 1 )
INSERT INTO #L ( LookUpTypeTitle, LookUpTitle, SortOrder, Value ) VALUES  ( 'Status', 'Approved', 2, 2 )
INSERT INTO #L ( LookUpTypeTitle, LookUpTitle, SortOrder, Value ) VALUES  ( 'Status', 'Unapproved', 3, 4 )

INSERT INTO #L ( LookUpTypeTitle, LookUpTitle, SortOrder, Value ) VALUES  ( 'Access', 'Create', 1, 1 )
INSERT INTO #L ( LookUpTypeTitle, LookUpTitle, SortOrder, Value ) VALUES  ( 'Access', 'Read', 2, 2 )
INSERT INTO #L ( LookUpTypeTitle, LookUpTitle, SortOrder, Value ) VALUES  ( 'Access', 'Update', 3, 4 )
INSERT INTO #L ( LookUpTypeTitle, LookUpTitle, SortOrder, Value ) VALUES  ( 'Access', 'Delete', 4, 8 )

INSERT INTO #L ( LookUpTypeTitle, LookUpTitle, SortOrder, Value ) VALUES  ( 'Task Status', 'Not Started', 1, 1 )
INSERT INTO #L ( LookUpTypeTitle, LookUpTitle, SortOrder, Value ) VALUES  ( 'Task Status', 'In Progress', 2, 2 )
INSERT INTO #L ( LookUpTypeTitle, LookUpTitle, SortOrder, Value ) VALUES  ( 'Task Status', 'Waiting', 3, 16 )
INSERT INTO #L ( LookUpTypeTitle, LookUpTitle, SortOrder, Value ) VALUES  ( 'Task Status', 'Deferred', 4, 8 )
INSERT INTO #L ( LookUpTypeTitle, LookUpTitle, SortOrder, Value ) VALUES  ( 'Task Status', 'Complete', 5, 4 )
INSERT INTO #L ( LookUpTypeTitle, LookUpTitle, SortOrder, Value ) VALUES  ( 'Task Status', 'Closed', 6, 32 )

INSERT INTO #L ( LookUpTypeTitle, LookUpTitle, SortOrder, Value ) VALUES  ( 'Task Priority', 'Low', 1, 1 )
INSERT INTO #L ( LookUpTypeTitle, LookUpTitle, SortOrder, Value ) VALUES  ( 'Task Priority', 'Normal', 2, 2 )
INSERT INTO #L ( LookUpTypeTitle, LookUpTitle, SortOrder, Value ) VALUES  ( 'Task Priority', 'High', 3, 4 )

INSERT INTO #L ( LookUpTypeTitle, LookUpTitle, SortOrder, Value ) VALUES  ( 'Email Status', 'New', 1, 1 )
INSERT INTO #L ( LookUpTypeTitle, LookUpTitle, SortOrder, Value ) VALUES  ( 'Email Status', 'Pending', 2, 2 )
INSERT INTO #L ( LookUpTypeTitle, LookUpTitle, SortOrder, Value ) VALUES  ( 'Email Status', 'Sent', 3, 4 )

INSERT INTO #L ( LookUpTypeTitle, LookUpTitle, SortOrder, Value ) VALUES  ( 'Bid Request Status', 'In Progress', 1, 1 )
INSERT INTO #L ( LookUpTypeTitle, LookUpTitle, SortOrder, Value ) VALUES  ( 'Bid Request Status', 'Submitted', 2, 2 )
INSERT INTO #L ( LookUpTypeTitle, LookUpTitle, SortOrder, Value ) VALUES  ( 'Bid Request Status', 'Completed', 3, 4 )
INSERT INTO #L ( LookUpTypeTitle, LookUpTitle, SortOrder, Value ) VALUES  ( 'Bid Request Status', 'Closed', 4, 8 )

INSERT INTO #L ( LookUpTypeTitle, LookUpTitle, SortOrder, Value ) VALUES  ( 'Bid Vendor Status', 'In Progress', 1, 1 )
INSERT INTO #L ( LookUpTypeTitle, LookUpTitle, SortOrder, Value ) VALUES  ( 'Bid Vendor Status', 'Submitted', 2, 2 )
INSERT INTO #L ( LookUpTypeTitle, LookUpTitle, SortOrder, Value ) VALUES  ( 'Bid Vendor Status', 'Interested', 3, 4 )
INSERT INTO #L ( LookUpTypeTitle, LookUpTitle, SortOrder, Value ) VALUES  ( 'Bid Vendor Status', 'Not Interested', 4, 8 )

INSERT INTO #L ( LookUpTypeTitle, LookUpTitle, SortOrder, Value ) VALUES  ( 'Bid Status', 'In Progress', 1, 1 )
INSERT INTO #L ( LookUpTypeTitle, LookUpTitle, SortOrder, Value ) VALUES  ( 'Bid Status', 'Submitted', 2, 2 )
INSERT INTO #L ( LookUpTypeTitle, LookUpTitle, SortOrder, Value ) VALUES  ( 'Bid Status', 'Accepted', 3, 4 )
INSERT INTO #L ( LookUpTypeTitle, LookUpTitle, SortOrder, Value ) VALUES  ( 'Bid Status', 'Rejected', 4, 8 )

INSERT INTO #L ( LookUpTypeTitle, LookUpTitle, SortOrder, Value ) VALUES  ( 'Message Status', 'New', 1, 1 )
INSERT INTO #L ( LookUpTypeTitle, LookUpTitle, SortOrder, Value ) VALUES  ( 'Message Status', 'Read', 2, 2 )
INSERT INTO #L ( LookUpTypeTitle, LookUpTitle, SortOrder, Value ) VALUES  ( 'Message Status', 'Deleted', 3, 4 )

----------------------------------------------------------

INSERT INTO #L ( LookUpTypeTitle, LookUpTitle ) VALUES  ( 'Company Type', 'Administration' )
INSERT INTO #L ( LookUpTypeTitle, LookUpTitle ) VALUES  ( 'Company Type', 'Management Company' )
INSERT INTO #L ( LookUpTypeTitle, LookUpTitle ) VALUES  ( 'Company Type', 'Vendor' )
INSERT INTO #L ( LookUpTypeTitle, LookUpTitle ) VALUES  ( 'Company Type', 'Company Vendor' )

INSERT INTO #L ( LookUpTypeTitle, LookUpTitle ) VALUES  ( 'Resource Type', 'Staff' )
INSERT INTO #L ( LookUpTypeTitle, LookUpTitle ) VALUES  ( 'Resource Type', 'Contact' )
INSERT INTO #L ( LookUpTypeTitle, LookUpTitle ) VALUES  ( 'Resource Type', 'Mailing Address' )
INSERT INTO #L ( LookUpTypeTitle, LookUpTitle ) VALUES  ( 'Resource Type', 'Other' )

INSERT INTO #L ( LookUpTypeTitle, LookUpTitle ) VALUES  ( 'Pricing Type', 'Membership Fee' )
INSERT INTO #L ( LookUpTypeTitle, LookUpTitle ) VALUES  ( 'Pricing Type', 'No Bid Fee' )
INSERT INTO #L ( LookUpTypeTitle, LookUpTitle ) VALUES  ( 'Pricing Type', 'Bid Fee' )

INSERT INTO #L ( LookUpTypeTitle, LookUpTitle, SortOrder ) VALUES  ( 'Payment Type', 'Check', 1 )
INSERT INTO #L ( LookUpTypeTitle, LookUpTitle, SortOrder ) VALUES  ( 'Payment Type', 'ACH', 2 )
INSERT INTO #L ( LookUpTypeTitle, LookUpTitle, SortOrder ) VALUES  ( 'Payment Type', 'Debit Card', 3 )
INSERT INTO #L ( LookUpTypeTitle, LookUpTitle, SortOrder ) VALUES  ( 'Payment Type', 'Credit Card', 4 )
INSERT INTO #L ( LookUpTypeTitle, LookUpTitle, SortOrder ) VALUES  ( 'Payment Type', 'Other', 5 )

INSERT INTO #L ( LookUpTypeTitle, LookUpTitle, SortOrder ) VALUES  ( 'Frequency Type', 'One-Time', 1 )
INSERT INTO #L ( LookUpTypeTitle, LookUpTitle, SortOrder ) VALUES  ( 'Frequency Type', 'Daily', 2 )
INSERT INTO #L ( LookUpTypeTitle, LookUpTitle, SortOrder ) VALUES  ( 'Frequency Type', 'Weekly', 3 )
INSERT INTO #L ( LookUpTypeTitle, LookUpTitle, SortOrder ) VALUES  ( 'Frequency Type', 'Every two weeks', 4 )
INSERT INTO #L ( LookUpTypeTitle, LookUpTitle, SortOrder ) VALUES  ( 'Frequency Type', 'Twice a month', 5 )
INSERT INTO #L ( LookUpTypeTitle, LookUpTitle, SortOrder ) VALUES  ( 'Frequency Type', 'Monthly', 6 )
INSERT INTO #L ( LookUpTypeTitle, LookUpTitle, SortOrder ) VALUES  ( 'Frequency Type', 'Quarterly', 7 )
INSERT INTO #L ( LookUpTypeTitle, LookUpTitle, SortOrder ) VALUES  ( 'Frequency Type', 'Twice a year', 8 )
INSERT INTO #L ( LookUpTypeTitle, LookUpTitle, SortOrder ) VALUES  ( 'Frequency Type', 'Annually', 9 )


SET NOCOUNT OFF

DECLARE @lookUpTitle VARCHAR(150),
		@lookUpValue INT,
		@sortOrder FLOAT,
		@lookUpKey INT

DECLARE object_cursor CURSOR FOR 
SELECT LookUpTypeTitle, LookUpTitle, SortOrder, Value
FROM #L t

OPEN object_cursor

FETCH NEXT FROM object_cursor 
INTO @lookUpTypeTitle, @lookUpTitle, @sortOrder, @lookUpValue

WHILE @@FETCH_STATUS = 0
BEGIN

	EXEC dbo.config_LookUp_InsertOne 
		@lookUpTypeTitle,
	    @lookUpTitle,
	    @lookUpValue,
	    @sortOrder,
	    @lookUpKey OUTPUT,
	    @errorCode OUTPUT

	FETCH NEXT FROM object_cursor 
	INTO @lookUpTypeTitle, @lookUpTitle, @sortOrder, @lookUpValue

END

CLOSE object_cursor
DEALLOCATE object_cursor

DROP TABLE #L

GO

exec [dbo].[config_LookUp_InsertData]