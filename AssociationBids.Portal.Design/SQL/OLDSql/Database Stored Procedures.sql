
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[GetSortOrder]') AND xtype IN (N'FN', N'IF', N'TF'))
DROP FUNCTION [dbo].[GetSortOrder]
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

CREATE FUNCTION GetSortOrder(@sortOrder VARCHAR(1000))
RETURNS VARCHAR(1000)
AS
BEGIN
	DECLARE @delimiter VARCHAR(1)
	DECLARE @index INT
	DECLARE @delimiterLength INT
	DECLARE @orderField VARCHAR(100)
	DECLARE @newSortOrder VARCHAR(1000)

	SET @newSortOrder = ''
	SET @delimiter = ','
	
	SELECT @delimiterLength = LEN(@delimiter)
	SELECT @sortOrder = @sortOrder + @delimiter
	
	SELECT @index = CHARINDEX(@delimiter, @sortOrder)
	WHILE (@index > 0)
	BEGIN
		SET @orderField = LTRIM(RTRIM(SUBSTRING(@sortOrder,1,@index-1)))
		IF (@orderField <> '')
		BEGIN
			IF (LEN(@newSortOrder) > 0)
				SET @newSortOrder = @newSortOrder + ', '

			IF (UPPER(RIGHT(@orderField, 5)) = ' DESC')
				SET @newSortOrder = @newSortOrder + Replace(@orderField, ' DESC', '')
			ELSE
				SET @newSortOrder = @newSortOrder + @orderField + ' DESC'
		END	
		SET @sortOrder = SUBSTRING(@sortOrder,@index+@delimiterLength,LEN(@sortOrder)-LEN(@orderField)-@delimiterLength)
		SELECT @index = CHARINDEX(@delimiter, @sortOrder)
	END
	
	RETURN @newSortOrder
END

GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[fn_Split]') AND xtype IN (N'FN', N'IF', N'TF'))
DROP FUNCTION [dbo].[fn_Split]
GO


SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO


CREATE FUNCTION fn_Split(@ValueList VARCHAR(8000), @Delimiter VARCHAR(50) = ',')
RETURNS @ValueTable TABLE (Value INT)
AS
BEGIN
	DECLARE @Index INT
	DECLARE @DelimiterLength INT
	DECLARE @Value VARCHAR(100)
	
	SELECT @DelimiterLength = LEN(@Delimiter)
	SELECT @ValueList = @ValueList + @Delimiter
	
	SELECT @Index = CHARINDEX(@Delimiter, @ValueList)
	WHILE (@Index > 0)
	BEGIN
		SET @Value = LTRIM(RTRIM(SUBSTRING(@ValueList,1,@Index-1)))
		IF (@Value <> '')
		BEGIN
			INSERT INTO @ValueTable (Value) VALUES (CAST(@Value AS INT))
		END	
		SET @ValueList = SUBSTRING(@ValueList,@Index+@DelimiterLength,LEN(@ValueList)-LEN(@Value)-@DelimiterLength)
		SELECT @Index = CHARINDEX(@Delimiter, @ValueList)
	END
	
	RETURN
END


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[Split_Int]') AND xtype IN (N'FN', N'IF', N'TF'))
DROP FUNCTION [dbo].[Split_Int]
GO


SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO


CREATE FUNCTION Split_Int(@ValueList VARCHAR(8000), @Delimiter VARCHAR(50) = ',')
RETURNS @ValueTable TABLE (Value INT)
AS
BEGIN
	DECLARE @Index INT
	DECLARE @DelimiterLength INT
	DECLARE @Value VARCHAR(100)
	
	SELECT @DelimiterLength = LEN(@Delimiter)
	SELECT @ValueList = @ValueList + @Delimiter
	
	SELECT @Index = CHARINDEX(@Delimiter, @ValueList)
	WHILE (@Index > 0)
	BEGIN
		SET @Value = LTRIM(RTRIM(SUBSTRING(@ValueList,1,@Index-1)))
		IF (@Value <> '')
		BEGIN
			INSERT INTO @ValueTable (Value) VALUES (CAST(@Value AS INT))
		END	
		SET @ValueList = SUBSTRING(@ValueList,@Index+@DelimiterLength,LEN(@ValueList)-LEN(@Value)-@DelimiterLength)
		SELECT @Index = CHARINDEX(@Delimiter, @ValueList)
	END
	
	RETURN
END


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[Split_VarChar]') AND xtype IN (N'FN', N'IF', N'TF'))
DROP FUNCTION [dbo].[Split_VarChar]
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO


CREATE FUNCTION Split_VarChar(@ValueList VARCHAR(8000), @Delimiter VARCHAR(50) = ',')
RETURNS @ValueTable TABLE (Value VARCHAR(100))
AS
BEGIN
	DECLARE @Index INT
	DECLARE @DelimiterLength INT
	DECLARE @Value VARCHAR(100)
	
	SELECT @DelimiterLength = LEN(@Delimiter)
	SELECT @ValueList = @ValueList + @Delimiter
	
	SELECT @Index = CHARINDEX(@Delimiter, @ValueList)
	WHILE (@Index > 0)
	BEGIN
		SET @Value = LTRIM(RTRIM(SUBSTRING(@ValueList,1,@Index-1)))
		IF (@Value <> '')
		BEGIN
			INSERT INTO @ValueTable (Value) VALUES (@Value)
		END	
		SET @ValueList = SUBSTRING(@ValueList,@Index+@DelimiterLength,LEN(@ValueList)-LEN(@Value)-@DelimiterLength)
		SELECT @Index = CHARINDEX(@Delimiter, @ValueList)
	END
	
	RETURN
END


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

-- *****************************************************
-- * Table          : Agreement
-- * Procedure Name : gensp_Agreement_SelectOneByAgreementKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Agreement_SelectOneByAgreementKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Agreement_SelectOneByAgreementKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Agreement_SelectOneByAgreementKey]
	@agreementKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON
SELECT
	[Agreement].[AgreementKey], 
	[Agreement].[PortalKey], 
	[Agreement].[Title], 
	[Agreement].[AgreementDate], 
	[Agreement].[Description], 
	[Agreement].[LastModificationTime], 
	[Agreement].[Status] 
FROM
	[Agreement] 
WHERE [Agreement].[AgreementKey] = @agreementKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Agreement
-- * Procedure Name : gensp_Agreement_SelectSomeBySearch
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Agreement_SelectSomeBySearch]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Agreement_SelectSomeBySearch]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Agreement_SelectSomeBySearch]
	@portalKey INT = 0,
	@propertyKeyList VARCHAR(2000) = NULL,
	@status INT = 0,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

-- Select PortalKey & UnitKey
SELECT PortalKey, UnitKey
INTO #T
FROM [dbo].[Split_PropertyKeyList](@propertyKeyList, ',')

SELECT
	[Agreement].[AgreementKey], 
	[Agreement].[PortalKey], 
	[Agreement].[Title], 
	[Agreement].[AgreementDate], 
	[Agreement].[Description], 
	[Agreement].[LastModificationTime], 
	[Agreement].[Status] 
FROM
	[Agreement] 
	LEFT JOIN #T T ON [Agreement].PortalKey = T.PortalKey 
WHERE ([Agreement].[PortalKey] = @portalKey OR @portalKey IS NULL OR @portalKey = 0)
AND ([Agreement].[Status] & ISNULL(@status, 7) = [Agreement].[Status])
AND (T.PortalKey IS NOT NULL OR ISNULL(@propertyKeyList, '') = '') 

-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Agreement
-- * Procedure Name : gensp_Agreement_SelectSomeBySearchAndPaging
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Agreement_SelectSomeBySearchAndPaging]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Agreement_SelectSomeBySearchAndPaging]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Agreement_SelectSomeBySearchAndPaging]
	@portalKey INT = 0,
	@propertyKeyList VARCHAR(2000) = NULL,
	@status INT = 0,
	@sortOrder varchar(250) = null,
	@pageSize int = 0,
	@pageNumber int = 0,
	@totalRecordCount INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DECLARE @sqlString nvarchar(4000)
DECLARE @parmDefinition nvarchar(1000)
DECLARE @startingRow int

IF (@pageNumber = 0)
	SET @pageNumber = 1

IF (@pageSize = 0)
	SET @pageSize = 10

IF (@pageNumber = 1)
	SET @startingRow = 1
ELSE
	SET @startingRow = ((@pageNumber - 1) * @pageSize) + 1

-- Add the primary key for better sorting results
IF (ISNULL(@sortOrder, '') = '')
	SET @sortOrder = '[Agreement].[AgreementKey]'
ELSE
	SET @sortOrder = @sortOrder + ', [Agreement].[AgreementKey]'

SET @parmDefinition = N' @startingRow INT, @pageSize INT, @portalKey INT, @status INT'

-- Select PortalKey & UnitKey
SELECT PortalKey, UnitKey
INTO #T
FROM [dbo].[Split_PropertyKeyList](@propertyKeyList, ',')

-- Get total records count
SELECT @totalRecordCount = COUNT(*)
FROM
	[Agreement] 
	LEFT JOIN #T T ON [Agreement].PortalKey = T.PortalKey 
WHERE ([Agreement].[PortalKey] = @portalKey OR @portalKey IS NULL OR @portalKey = 0)
AND ([Agreement].[Status] & ISNULL(@status, 7) = [Agreement].[Status])
AND (T.PortalKey IS NOT NULL OR ISNULL(@propertyKeyList, '') = '') 

-- Determine records exist to retrieve
IF (@totalRecordCount > 0)
BEGIN

	SET @sqlString = N'SELECT * ' + 
		'FROM ( ' + 
		'	SELECT ROW_NUMBER() OVER ( ORDER BY ' + @sortOrder + ' ) AS RowNumber, ' + 
		'		[Agreement].[AgreementKey], ' +
		'		[Agreement].[PortalKey], ' +
		'		[Agreement].[Title], ' +
		'		[Agreement].[AgreementDate], ' +
		'		[Agreement].[Description], ' +
		'		[Agreement].[LastModificationTime], ' +
		'		[Agreement].[Status] ' +
		'	FROM [Agreement]  ' + 
		'	LEFT JOIN #T T ON [Agreement].PortalKey = T.PortalKey ' + 
		'	WHERE ([Agreement].[PortalKey] = @portalKey OR @portalKey IS NULL OR @portalKey = 0) ' +
		'	AND ([Agreement].[Status] & ISNULL(@status, 7) = [Agreement].[Status]) ' +
		'' 

	IF (ISNULL(@propertyKeyList, '') <> '')
		SET @sqlString = @sqlString + 'AND (T.PortalKey IS NOT NULL) ' 

	SET @sqlString = @sqlString + 
		') AS FilteredResult ' + 
		'WHERE RowNumber >= @startingRow ' + 
		'AND RowNumber < (@startingRow + @pageSize) ' + 
		'ORDER BY RowNumber ' 

	EXECUTE sp_executesql @sqlString, @parmDefinition, @startingRow, @pageSize, @portalKey, @status

	-- Get the Error Code for the statement just executed.
	SELECT @errorCode = @@ERROR
END

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Agreement
-- * Procedure Name : gensp_Agreement_InsertOne
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Agreement_InsertOne]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Agreement_InsertOne]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Agreement_InsertOne]
	@portalKey INT,
	@title VARCHAR(150),
	@agreementDate DATETIME,
	@description VARCHAR(MAX),
	@lastModificationTime DATETIME,
	@status INT,
	@agreementKey INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON


INSERT INTO [Agreement]
(
	[PortalKey],
	[Title],
	[AgreementDate],
	[Description],
	[LastModificationTime],
	[Status]
)
VALUES
(
	@portalKey,
	@title,
	@agreementDate,
	@description,
	@lastModificationTime,
	@status
)
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR
-- Get the IDENTITY value for the row just inserted.
SELECT @agreementKey = SCOPE_IDENTITY()

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Agreement
-- * Procedure Name : gensp_Agreement_UpdateOneByAgreementKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Agreement_UpdateOneByAgreementKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Agreement_UpdateOneByAgreementKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Agreement_UpdateOneByAgreementKey]
	@agreementKey INT,
	@portalKey INT,
	@title VARCHAR(150),
	@agreementDate DATETIME,
	@description VARCHAR(MAX),
	@lastModificationTime DATETIME,
	@status INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

UPDATE [Agreement]
SET
	[PortalKey] = @portalKey,
	[Title] = @title,
	[AgreementDate] = @agreementDate,
	[Description] = @description,
	[LastModificationTime] = @lastModificationTime,
	[Status] = @status
WHERE [Agreement].[AgreementKey] = @agreementKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Agreement
-- * Procedure Name : gensp_Agreement_DeleteOneByAgreementKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Agreement_DeleteOneByAgreementKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Agreement_DeleteOneByAgreementKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Agreement_DeleteOneByAgreementKey]
	@agreementKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DELETE FROM [Agreement]
WHERE [Agreement].[AgreementKey] = @agreementKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Bid
-- * Procedure Name : gensp_Bid_SelectOneByBidKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Bid_SelectOneByBidKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Bid_SelectOneByBidKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Bid_SelectOneByBidKey]
	@bidKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON
SELECT
	[Bid].[BidKey], 
	[Bid].[BidVendorKey], 
	[Bid].[ResourceKey], 
	[Bid].[Title], 
	[Bid].[Total], 
	[Bid].[Description], 
	[Bid].[LastModificationTime], 
	[Bid].[BidStatus] 
FROM
	[Bid] 
WHERE [Bid].[BidKey] = @bidKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Bid
-- * Procedure Name : gensp_Bid_SelectSomeBySearch
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Bid_SelectSomeBySearch]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Bid_SelectSomeBySearch]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Bid_SelectSomeBySearch]
	@bidVendorKey INT = 0,
	@resourceKey INT = 0,
	@bidStatus INT = 0,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

SELECT
	[Bid].[BidKey], 
	[Bid].[BidVendorKey], 
	[Bid].[ResourceKey], 
	[Bid].[Title], 
	[Bid].[Total], 
	[Bid].[Description], 
	[Bid].[LastModificationTime], 
	[Bid].[BidStatus] 
FROM
	[Bid] 
WHERE ([Bid].[BidVendorKey] = @bidVendorKey OR @bidVendorKey IS NULL OR @bidVendorKey = 0)
AND ([Bid].[ResourceKey] = @resourceKey OR @resourceKey IS NULL OR @resourceKey = 0)
AND ([Bid].[BidStatus] & @bidStatus = [Bid].[BidStatus] OR @bidStatus IS NULL OR @bidStatus = 0)

-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Bid
-- * Procedure Name : gensp_Bid_SelectSomeBySearchAndPaging
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Bid_SelectSomeBySearchAndPaging]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Bid_SelectSomeBySearchAndPaging]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Bid_SelectSomeBySearchAndPaging]
	@bidVendorKey INT = 0,
	@resourceKey INT = 0,
	@bidStatus INT = 0,
	@sortOrder varchar(250) = null,
	@pageSize int = 0,
	@pageNumber int = 0,
	@totalRecordCount INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DECLARE @sqlString nvarchar(4000)
DECLARE @parmDefinition nvarchar(1000)
DECLARE @startingRow int

IF (@pageNumber = 0)
	SET @pageNumber = 1

IF (@pageSize = 0)
	SET @pageSize = 10

IF (@pageNumber = 1)
	SET @startingRow = 1
ELSE
	SET @startingRow = ((@pageNumber - 1) * @pageSize) + 1

-- Add the primary key for better sorting results
IF (ISNULL(@sortOrder, '') = '')
	SET @sortOrder = '[Bid].[BidKey]'
ELSE
	SET @sortOrder = @sortOrder + ', [Bid].[BidKey]'

SET @parmDefinition = N' @startingRow INT, @pageSize INT, @bidVendorKey INT, @resourceKey INT, @bidStatus INT'

-- Get total records count
SELECT @totalRecordCount = COUNT(*)
FROM
	[Bid] 
WHERE ([Bid].[BidVendorKey] = @bidVendorKey OR @bidVendorKey IS NULL OR @bidVendorKey = 0)
AND ([Bid].[ResourceKey] = @resourceKey OR @resourceKey IS NULL OR @resourceKey = 0)
AND ([Bid].[BidStatus] & @bidStatus = [Bid].[BidStatus] OR @bidStatus IS NULL OR @bidStatus = 0)

-- Determine records exist to retrieve
IF (@totalRecordCount > 0)
BEGIN

	SET @sqlString = N'SELECT * ' + 
		'FROM ( ' + 
		'	SELECT ROW_NUMBER() OVER ( ORDER BY ' + @sortOrder + ' ) AS RowNumber, ' + 
		'		[Bid].[BidKey], ' +
		'		[Bid].[BidVendorKey], ' +
		'		[Bid].[ResourceKey], ' +
		'		[Bid].[Title], ' +
		'		[Bid].[Total], ' +
		'		[Bid].[Description], ' +
		'		[Bid].[LastModificationTime], ' +
		'		[Bid].[BidStatus] ' +
		'	FROM [Bid]  ' + 
		'	WHERE ([Bid].[BidVendorKey] = @bidVendorKey OR @bidVendorKey IS NULL OR @bidVendorKey = 0) ' +
		'	AND ([Bid].[ResourceKey] = @resourceKey OR @resourceKey IS NULL OR @resourceKey = 0) ' +
		'	AND ([Bid].[BidStatus] & @bidStatus = [Bid].[BidStatus] OR @bidStatus IS NULL OR @bidStatus = 0) ' +
		') AS FilteredResult ' + 
		'WHERE RowNumber >= @startingRow ' + 
		'AND RowNumber < (@startingRow + @pageSize) ' + 
		'ORDER BY RowNumber ' 

	EXECUTE sp_executesql @sqlString, @parmDefinition, @startingRow, @pageSize, @bidVendorKey, @resourceKey, @bidStatus

	-- Get the Error Code for the statement just executed.
	SELECT @errorCode = @@ERROR
END

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Bid
-- * Procedure Name : gensp_Bid_InsertOne
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Bid_InsertOne]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Bid_InsertOne]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Bid_InsertOne]
	@bidVendorKey INT,
	@resourceKey INT,
	@title VARCHAR(150),
	@total MONEY,
	@description VARCHAR(MAX),
	@lastModificationTime DATETIME,
	@bidStatus INT,
	@bidKey INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON


INSERT INTO [Bid]
(
	[BidVendorKey],
	[ResourceKey],
	[Title],
	[Total],
	[Description],
	[LastModificationTime],
	[BidStatus]
)
VALUES
(
	@bidVendorKey,
	@resourceKey,
	@title,
	@total,
	@description,
	@lastModificationTime,
	@bidStatus
)
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR
-- Get the IDENTITY value for the row just inserted.
SELECT @bidKey = SCOPE_IDENTITY()

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Bid
-- * Procedure Name : gensp_Bid_UpdateOneByBidKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Bid_UpdateOneByBidKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Bid_UpdateOneByBidKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Bid_UpdateOneByBidKey]
	@bidKey INT,
	@bidVendorKey INT,
	@resourceKey INT,
	@title VARCHAR(150),
	@total MONEY,
	@description VARCHAR(MAX),
	@lastModificationTime DATETIME,
	@bidStatus INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

UPDATE [Bid]
SET
	[BidVendorKey] = @bidVendorKey,
	[ResourceKey] = @resourceKey,
	[Title] = @title,
	[Total] = @total,
	[Description] = @description,
	[LastModificationTime] = @lastModificationTime,
	[BidStatus] = @bidStatus
WHERE [Bid].[BidKey] = @bidKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Bid
-- * Procedure Name : gensp_Bid_DeleteOneByBidKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Bid_DeleteOneByBidKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Bid_DeleteOneByBidKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Bid_DeleteOneByBidKey]
	@bidKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DELETE FROM [Bid]
WHERE [Bid].[BidKey] = @bidKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : BidRequest
-- * Procedure Name : gensp_BidRequest_SelectOneByBidRequestKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_BidRequest_SelectOneByBidRequestKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_BidRequest_SelectOneByBidRequestKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_BidRequest_SelectOneByBidRequestKey]
	@bidRequestKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON
SELECT
	[BidRequest].[BidRequestKey], 
	[BidRequest].[PropertyKey], 
	[BidRequest].[ResourceKey], 
	[BidRequest].[ServiceKey], 
	[BidRequest].[Title], 
	[BidRequest].[BidDueDate], 
	[BidRequest].[StartDate], 
	[BidRequest].[EndDate], 
	[BidRequest].[Description], 
	[BidRequest].[DateAdded], 
	[BidRequest].[LastModificationTime], 
	[BidRequest].[BidRequestStatus] 
FROM
	[BidRequest] 
WHERE [BidRequest].[BidRequestKey] = @bidRequestKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : BidRequest
-- * Procedure Name : gensp_BidRequest_SelectSomeBySearch
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_BidRequest_SelectSomeBySearch]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_BidRequest_SelectSomeBySearch]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_BidRequest_SelectSomeBySearch]
	@propertyKey INT = 0,
	@resourceKey INT = 0,
	@serviceKey INT = 0,
	@bidRequestStatus INT = 0,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

SELECT
	[BidRequest].[BidRequestKey], 
	[BidRequest].[PropertyKey], 
	[BidRequest].[ResourceKey], 
	[BidRequest].[ServiceKey], 
	[BidRequest].[Title], 
	[BidRequest].[BidDueDate], 
	[BidRequest].[StartDate], 
	[BidRequest].[EndDate], 
	[BidRequest].[Description], 
	[BidRequest].[DateAdded], 
	[BidRequest].[LastModificationTime], 
	[BidRequest].[BidRequestStatus] 
FROM
	[BidRequest] 
WHERE ([BidRequest].[PropertyKey] = @propertyKey OR @propertyKey IS NULL OR @propertyKey = 0)
AND ([BidRequest].[ResourceKey] = @resourceKey OR @resourceKey IS NULL OR @resourceKey = 0)
AND ([BidRequest].[ServiceKey] = @serviceKey OR @serviceKey IS NULL OR @serviceKey = 0)
AND ([BidRequest].[BidRequestStatus] & @bidRequestStatus = [BidRequest].[BidRequestStatus] OR @bidRequestStatus IS NULL OR @bidRequestStatus = 0)

-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : BidRequest
-- * Procedure Name : gensp_BidRequest_SelectSomeBySearchAndPaging
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_BidRequest_SelectSomeBySearchAndPaging]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_BidRequest_SelectSomeBySearchAndPaging]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_BidRequest_SelectSomeBySearchAndPaging]
	@propertyKey INT = 0,
	@resourceKey INT = 0,
	@serviceKey INT = 0,
	@bidRequestStatus INT = 0,
	@sortOrder varchar(250) = null,
	@pageSize int = 0,
	@pageNumber int = 0,
	@totalRecordCount INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DECLARE @sqlString nvarchar(4000)
DECLARE @parmDefinition nvarchar(1000)
DECLARE @startingRow int

IF (@pageNumber = 0)
	SET @pageNumber = 1

IF (@pageSize = 0)
	SET @pageSize = 10

IF (@pageNumber = 1)
	SET @startingRow = 1
ELSE
	SET @startingRow = ((@pageNumber - 1) * @pageSize) + 1

-- Add the primary key for better sorting results
IF (ISNULL(@sortOrder, '') = '')
	SET @sortOrder = '[BidRequest].[BidRequestKey]'
ELSE
	SET @sortOrder = @sortOrder + ', [BidRequest].[BidRequestKey]'

SET @parmDefinition = N' @startingRow INT, @pageSize INT, @propertyKey INT, @resourceKey INT, @serviceKey INT, @bidRequestStatus INT'

-- Get total records count
SELECT @totalRecordCount = COUNT(*)
FROM
	[BidRequest] 
WHERE ([BidRequest].[PropertyKey] = @propertyKey OR @propertyKey IS NULL OR @propertyKey = 0)
AND ([BidRequest].[ResourceKey] = @resourceKey OR @resourceKey IS NULL OR @resourceKey = 0)
AND ([BidRequest].[ServiceKey] = @serviceKey OR @serviceKey IS NULL OR @serviceKey = 0)
AND ([BidRequest].[BidRequestStatus] & @bidRequestStatus = [BidRequest].[BidRequestStatus] OR @bidRequestStatus IS NULL OR @bidRequestStatus = 0)

-- Determine records exist to retrieve
IF (@totalRecordCount > 0)
BEGIN

	SET @sqlString = N'SELECT * ' + 
		'FROM ( ' + 
		'	SELECT ROW_NUMBER() OVER ( ORDER BY ' + @sortOrder + ' ) AS RowNumber, ' + 
		'		[BidRequest].[BidRequestKey], ' +
		'		[BidRequest].[PropertyKey], ' +
		'		[BidRequest].[ResourceKey], ' +
		'		[BidRequest].[ServiceKey], ' +
		'		[BidRequest].[Title], ' +
		'		[BidRequest].[BidDueDate], ' +
		'		[BidRequest].[StartDate], ' +
		'		[BidRequest].[EndDate], ' +
		'		[BidRequest].[Description], ' +
		'		[BidRequest].[DateAdded], ' +
		'		[BidRequest].[LastModificationTime], ' +
		'		[BidRequest].[BidRequestStatus] ' +
		'	FROM [BidRequest]  ' + 
		'	WHERE ([BidRequest].[PropertyKey] = @propertyKey OR @propertyKey IS NULL OR @propertyKey = 0) ' +
		'	AND ([BidRequest].[ResourceKey] = @resourceKey OR @resourceKey IS NULL OR @resourceKey = 0) ' +
		'	AND ([BidRequest].[ServiceKey] = @serviceKey OR @serviceKey IS NULL OR @serviceKey = 0) ' +
		'	AND ([BidRequest].[BidRequestStatus] & @bidRequestStatus = [BidRequest].[BidRequestStatus] OR @bidRequestStatus IS NULL OR @bidRequestStatus = 0) ' +
		') AS FilteredResult ' + 
		'WHERE RowNumber >= @startingRow ' + 
		'AND RowNumber < (@startingRow + @pageSize) ' + 
		'ORDER BY RowNumber ' 

	EXECUTE sp_executesql @sqlString, @parmDefinition, @startingRow, @pageSize, @propertyKey, @resourceKey, @serviceKey, @bidRequestStatus

	-- Get the Error Code for the statement just executed.
	SELECT @errorCode = @@ERROR
END

GO

-- *****************************************************

-- *****************************************************
-- * Table          : BidRequest
-- * Procedure Name : gensp_BidRequest_InsertOne
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_BidRequest_InsertOne]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_BidRequest_InsertOne]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_BidRequest_InsertOne]
	@propertyKey INT,
	@resourceKey INT,
	@serviceKey INT,
	@title VARCHAR(150),
	@bidDueDate SMALLDATETIME,
	@startDate SMALLDATETIME,
	@endDate SMALLDATETIME,
	@description VARCHAR(MAX),
	@dateAdded DATETIME,
	@lastModificationTime DATETIME,
	@bidRequestStatus INT,
	@bidRequestKey INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON


INSERT INTO [BidRequest]
(
	[PropertyKey],
	[ResourceKey],
	[ServiceKey],
	[Title],
	[BidDueDate],
	[StartDate],
	[EndDate],
	[Description],
	[DateAdded],
	[LastModificationTime],
	[BidRequestStatus]
)
VALUES
(
	@propertyKey,
	@resourceKey,
	@serviceKey,
	@title,
	@bidDueDate,
	@startDate,
	@endDate,
	@description,
	@dateAdded,
	@lastModificationTime,
	@bidRequestStatus
)
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR
-- Get the IDENTITY value for the row just inserted.
SELECT @bidRequestKey = SCOPE_IDENTITY()

GO

-- *****************************************************

-- *****************************************************
-- * Table          : BidRequest
-- * Procedure Name : gensp_BidRequest_UpdateOneByBidRequestKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_BidRequest_UpdateOneByBidRequestKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_BidRequest_UpdateOneByBidRequestKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_BidRequest_UpdateOneByBidRequestKey]
	@bidRequestKey INT,
	@propertyKey INT,
	@resourceKey INT,
	@serviceKey INT,
	@title VARCHAR(150),
	@bidDueDate SMALLDATETIME,
	@startDate SMALLDATETIME,
	@endDate SMALLDATETIME,
	@description VARCHAR(MAX),
	@dateAdded DATETIME,
	@lastModificationTime DATETIME,
	@bidRequestStatus INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

UPDATE [BidRequest]
SET
	[PropertyKey] = @propertyKey,
	[ResourceKey] = @resourceKey,
	[ServiceKey] = @serviceKey,
	[Title] = @title,
	[BidDueDate] = @bidDueDate,
	[StartDate] = @startDate,
	[EndDate] = @endDate,
	[Description] = @description,
	[DateAdded] = @dateAdded,
	[LastModificationTime] = @lastModificationTime,
	[BidRequestStatus] = @bidRequestStatus
WHERE [BidRequest].[BidRequestKey] = @bidRequestKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : BidRequest
-- * Procedure Name : gensp_BidRequest_DeleteOneByBidRequestKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_BidRequest_DeleteOneByBidRequestKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_BidRequest_DeleteOneByBidRequestKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_BidRequest_DeleteOneByBidRequestKey]
	@bidRequestKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DELETE FROM [BidRequest]
WHERE [BidRequest].[BidRequestKey] = @bidRequestKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : BidVendor
-- * Procedure Name : gensp_BidVendor_SelectOneByBidVendorKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_BidVendor_SelectOneByBidVendorKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_BidVendor_SelectOneByBidVendorKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_BidVendor_SelectOneByBidVendorKey]
	@bidVendorKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON
SELECT
	[BidVendor].[BidVendorKey], 
	[BidVendor].[BidRequestKey], 
	[BidVendor].[VendorKey], 
	[BidVendor].[ResourceKey], 
	[BidVendor].[BidVendorID], 
	[BidVendor].[IsAssigned], 
	[BidVendor].[RespondByDate], 
	[BidVendor].[DateAdded], 
	[BidVendor].[LastModificationTime], 
	[BidVendor].[BidVendorStatus] 
FROM
	[BidVendor] 
WHERE [BidVendor].[BidVendorKey] = @bidVendorKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : BidVendor
-- * Procedure Name : gensp_BidVendor_SelectSomeBySearch
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_BidVendor_SelectSomeBySearch]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_BidVendor_SelectSomeBySearch]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_BidVendor_SelectSomeBySearch]
	@bidRequestKey INT = 0,
	@vendorKey INT = 0,
	@resourceKey INT = 0,
	@bidVendorStatus INT = 0,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

SELECT
	[BidVendor].[BidVendorKey], 
	[BidVendor].[BidRequestKey], 
	[BidVendor].[VendorKey], 
	[BidVendor].[ResourceKey], 
	[BidVendor].[BidVendorID], 
	[BidVendor].[IsAssigned], 
	[BidVendor].[RespondByDate], 
	[BidVendor].[DateAdded], 
	[BidVendor].[LastModificationTime], 
	[BidVendor].[BidVendorStatus] 
FROM
	[BidVendor] 
WHERE ([BidVendor].[BidRequestKey] = @bidRequestKey OR @bidRequestKey IS NULL OR @bidRequestKey = 0)
AND ([BidVendor].[VendorKey] = @vendorKey OR @vendorKey IS NULL OR @vendorKey = 0)
AND ([BidVendor].[ResourceKey] = @resourceKey OR @resourceKey IS NULL OR @resourceKey = 0)
AND ([BidVendor].[BidVendorStatus] & @bidVendorStatus = [BidVendor].[BidVendorStatus] OR @bidVendorStatus IS NULL OR @bidVendorStatus = 0)

-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : BidVendor
-- * Procedure Name : gensp_BidVendor_SelectSomeBySearchAndPaging
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_BidVendor_SelectSomeBySearchAndPaging]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_BidVendor_SelectSomeBySearchAndPaging]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_BidVendor_SelectSomeBySearchAndPaging]
	@bidRequestKey INT = 0,
	@vendorKey INT = 0,
	@resourceKey INT = 0,
	@bidVendorStatus INT = 0,
	@sortOrder varchar(250) = null,
	@pageSize int = 0,
	@pageNumber int = 0,
	@totalRecordCount INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DECLARE @sqlString nvarchar(4000)
DECLARE @parmDefinition nvarchar(1000)
DECLARE @startingRow int

IF (@pageNumber = 0)
	SET @pageNumber = 1

IF (@pageSize = 0)
	SET @pageSize = 10

IF (@pageNumber = 1)
	SET @startingRow = 1
ELSE
	SET @startingRow = ((@pageNumber - 1) * @pageSize) + 1

-- Add the primary key for better sorting results
IF (ISNULL(@sortOrder, '') = '')
	SET @sortOrder = '[BidVendor].[BidVendorKey]'
ELSE
	SET @sortOrder = @sortOrder + ', [BidVendor].[BidVendorKey]'

SET @parmDefinition = N' @startingRow INT, @pageSize INT, @bidRequestKey INT, @vendorKey INT, @resourceKey INT, @bidVendorStatus INT'

-- Get total records count
SELECT @totalRecordCount = COUNT(*)
FROM
	[BidVendor] 
WHERE ([BidVendor].[BidRequestKey] = @bidRequestKey OR @bidRequestKey IS NULL OR @bidRequestKey = 0)
AND ([BidVendor].[VendorKey] = @vendorKey OR @vendorKey IS NULL OR @vendorKey = 0)
AND ([BidVendor].[ResourceKey] = @resourceKey OR @resourceKey IS NULL OR @resourceKey = 0)
AND ([BidVendor].[BidVendorStatus] & @bidVendorStatus = [BidVendor].[BidVendorStatus] OR @bidVendorStatus IS NULL OR @bidVendorStatus = 0)

-- Determine records exist to retrieve
IF (@totalRecordCount > 0)
BEGIN

	SET @sqlString = N'SELECT * ' + 
		'FROM ( ' + 
		'	SELECT ROW_NUMBER() OVER ( ORDER BY ' + @sortOrder + ' ) AS RowNumber, ' + 
		'		[BidVendor].[BidVendorKey], ' +
		'		[BidVendor].[BidRequestKey], ' +
		'		[BidVendor].[VendorKey], ' +
		'		[BidVendor].[ResourceKey], ' +
		'		[BidVendor].[BidVendorID], ' +
		'		[BidVendor].[IsAssigned], ' +
		'		[BidVendor].[RespondByDate], ' +
		'		[BidVendor].[DateAdded], ' +
		'		[BidVendor].[LastModificationTime], ' +
		'		[BidVendor].[BidVendorStatus] ' +
		'	FROM [BidVendor]  ' + 
		'	WHERE ([BidVendor].[BidRequestKey] = @bidRequestKey OR @bidRequestKey IS NULL OR @bidRequestKey = 0) ' +
		'	AND ([BidVendor].[VendorKey] = @vendorKey OR @vendorKey IS NULL OR @vendorKey = 0) ' +
		'	AND ([BidVendor].[ResourceKey] = @resourceKey OR @resourceKey IS NULL OR @resourceKey = 0) ' +
		'	AND ([BidVendor].[BidVendorStatus] & @bidVendorStatus = [BidVendor].[BidVendorStatus] OR @bidVendorStatus IS NULL OR @bidVendorStatus = 0) ' +
		') AS FilteredResult ' + 
		'WHERE RowNumber >= @startingRow ' + 
		'AND RowNumber < (@startingRow + @pageSize) ' + 
		'ORDER BY RowNumber ' 

	EXECUTE sp_executesql @sqlString, @parmDefinition, @startingRow, @pageSize, @bidRequestKey, @vendorKey, @resourceKey, @bidVendorStatus

	-- Get the Error Code for the statement just executed.
	SELECT @errorCode = @@ERROR
END

GO

-- *****************************************************

-- *****************************************************
-- * Table          : BidVendor
-- * Procedure Name : gensp_BidVendor_InsertOne
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_BidVendor_InsertOne]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_BidVendor_InsertOne]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_BidVendor_InsertOne]
	@bidRequestKey INT,
	@vendorKey INT,
	@resourceKey INT,
	@bidVendorID VARCHAR(255),
	@isAssigned BIT,
	@respondByDate DATETIME,
	@dateAdded DATETIME,
	@lastModificationTime DATETIME,
	@bidVendorStatus INT,
	@bidVendorKey INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON


INSERT INTO [BidVendor]
(
	[BidRequestKey],
	[VendorKey],
	[ResourceKey],
	[BidVendorID],
	[IsAssigned],
	[RespondByDate],
	[DateAdded],
	[LastModificationTime],
	[BidVendorStatus]
)
VALUES
(
	@bidRequestKey,
	@vendorKey,
	@resourceKey,
	@bidVendorID,
	@isAssigned,
	@respondByDate,
	@dateAdded,
	@lastModificationTime,
	@bidVendorStatus
)
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR
-- Get the IDENTITY value for the row just inserted.
SELECT @bidVendorKey = SCOPE_IDENTITY()

GO

-- *****************************************************

-- *****************************************************
-- * Table          : BidVendor
-- * Procedure Name : gensp_BidVendor_UpdateOneByBidVendorKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_BidVendor_UpdateOneByBidVendorKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_BidVendor_UpdateOneByBidVendorKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_BidVendor_UpdateOneByBidVendorKey]
	@bidVendorKey INT,
	@bidRequestKey INT,
	@vendorKey INT,
	@resourceKey INT,
	@bidVendorID VARCHAR(255),
	@isAssigned BIT,
	@respondByDate DATETIME,
	@dateAdded DATETIME,
	@lastModificationTime DATETIME,
	@bidVendorStatus INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

UPDATE [BidVendor]
SET
	[BidRequestKey] = @bidRequestKey,
	[VendorKey] = @vendorKey,
	[ResourceKey] = @resourceKey,
	[BidVendorID] = @bidVendorID,
	[IsAssigned] = @isAssigned,
	[RespondByDate] = @respondByDate,
	[DateAdded] = @dateAdded,
	[LastModificationTime] = @lastModificationTime,
	[BidVendorStatus] = @bidVendorStatus
WHERE [BidVendor].[BidVendorKey] = @bidVendorKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : BidVendor
-- * Procedure Name : gensp_BidVendor_DeleteOneByBidVendorKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_BidVendor_DeleteOneByBidVendorKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_BidVendor_DeleteOneByBidVendorKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_BidVendor_DeleteOneByBidVendorKey]
	@bidVendorKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DELETE FROM [BidVendor]
WHERE [BidVendor].[BidVendorKey] = @bidVendorKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Calendar
-- * Procedure Name : gensp_Calendar_SelectOneByCalendarKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Calendar_SelectOneByCalendarKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Calendar_SelectOneByCalendarKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Calendar_SelectOneByCalendarKey]
	@calendarKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON
SELECT
	[Calendar].[CalendarKey], 
	[Calendar].[ModuleKey], 
	[Calendar].[ResourceKey], 
	[Calendar].[ObjectKey], 
	[Calendar].[Subject], 
	[Calendar].[StartDate], 
	[Calendar].[EndDate], 
	[Calendar].[AllDayEvent], 
	[Calendar].[Location], 
	[Calendar].[Description], 
	[Calendar].[LastModificationTime], 
	[Calendar].[Status] 
FROM
	[Calendar] 
WHERE [Calendar].[CalendarKey] = @calendarKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Calendar
-- * Procedure Name : gensp_Calendar_SelectSomeBySearch
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Calendar_SelectSomeBySearch]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Calendar_SelectSomeBySearch]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Calendar_SelectSomeBySearch]
	@moduleKey INT = 0,
	@resourceKey INT = 0,
	@objectKey INT = 0,
	@status INT = 0,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

SELECT
	[Calendar].[CalendarKey], 
	[Calendar].[ModuleKey], 
	[Calendar].[ResourceKey], 
	[Calendar].[ObjectKey], 
	[Calendar].[Subject], 
	[Calendar].[StartDate], 
	[Calendar].[EndDate], 
	[Calendar].[AllDayEvent], 
	[Calendar].[Location], 
	[Calendar].[Description], 
	[Calendar].[LastModificationTime], 
	[Calendar].[Status] 
FROM
	[Calendar] 
WHERE ([Calendar].[ModuleKey] = @moduleKey OR @moduleKey IS NULL OR @moduleKey = 0)
AND ([Calendar].[ResourceKey] = @resourceKey OR @resourceKey IS NULL OR @resourceKey = 0)
AND ([Calendar].[ObjectKey] = @objectKey OR @objectKey IS NULL OR @objectKey = 0)
AND ([Calendar].[Status] & ISNULL(@status, 7) = [Calendar].[Status])

-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Calendar
-- * Procedure Name : gensp_Calendar_SelectSomeBySearchAndPaging
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Calendar_SelectSomeBySearchAndPaging]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Calendar_SelectSomeBySearchAndPaging]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Calendar_SelectSomeBySearchAndPaging]
	@moduleKey INT = 0,
	@resourceKey INT = 0,
	@objectKey INT = 0,
	@status INT = 0,
	@sortOrder varchar(250) = null,
	@pageSize int = 0,
	@pageNumber int = 0,
	@totalRecordCount INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DECLARE @sqlString nvarchar(4000)
DECLARE @parmDefinition nvarchar(1000)
DECLARE @startingRow int

IF (@pageNumber = 0)
	SET @pageNumber = 1

IF (@pageSize = 0)
	SET @pageSize = 10

IF (@pageNumber = 1)
	SET @startingRow = 1
ELSE
	SET @startingRow = ((@pageNumber - 1) * @pageSize) + 1

-- Add the primary key for better sorting results
IF (ISNULL(@sortOrder, '') = '')
	SET @sortOrder = '[Calendar].[CalendarKey]'
ELSE
	SET @sortOrder = @sortOrder + ', [Calendar].[CalendarKey]'

SET @parmDefinition = N' @startingRow INT, @pageSize INT, @moduleKey INT, @resourceKey INT, @objectKey INT, @status INT'

-- Get total records count
SELECT @totalRecordCount = COUNT(*)
FROM
	[Calendar] 
WHERE ([Calendar].[ModuleKey] = @moduleKey OR @moduleKey IS NULL OR @moduleKey = 0)
AND ([Calendar].[ResourceKey] = @resourceKey OR @resourceKey IS NULL OR @resourceKey = 0)
AND ([Calendar].[ObjectKey] = @objectKey OR @objectKey IS NULL OR @objectKey = 0)
AND ([Calendar].[Status] & ISNULL(@status, 7) = [Calendar].[Status])

-- Determine records exist to retrieve
IF (@totalRecordCount > 0)
BEGIN

	SET @sqlString = N'SELECT * ' + 
		'FROM ( ' + 
		'	SELECT ROW_NUMBER() OVER ( ORDER BY ' + @sortOrder + ' ) AS RowNumber, ' + 
		'		[Calendar].[CalendarKey], ' +
		'		[Calendar].[ModuleKey], ' +
		'		[Calendar].[ResourceKey], ' +
		'		[Calendar].[ObjectKey], ' +
		'		[Calendar].[Subject], ' +
		'		[Calendar].[StartDate], ' +
		'		[Calendar].[EndDate], ' +
		'		[Calendar].[AllDayEvent], ' +
		'		[Calendar].[Location], ' +
		'		[Calendar].[Description], ' +
		'		[Calendar].[LastModificationTime], ' +
		'		[Calendar].[Status] ' +
		'	FROM [Calendar]  ' + 
		'	WHERE ([Calendar].[ModuleKey] = @moduleKey OR @moduleKey IS NULL OR @moduleKey = 0) ' +
		'	AND ([Calendar].[ResourceKey] = @resourceKey OR @resourceKey IS NULL OR @resourceKey = 0) ' +
		'	AND ([Calendar].[ObjectKey] = @objectKey OR @objectKey IS NULL OR @objectKey = 0) ' +
		'	AND ([Calendar].[Status] & ISNULL(@status, 7) = [Calendar].[Status]) ' +
		') AS FilteredResult ' + 
		'WHERE RowNumber >= @startingRow ' + 
		'AND RowNumber < (@startingRow + @pageSize) ' + 
		'ORDER BY RowNumber ' 

	EXECUTE sp_executesql @sqlString, @parmDefinition, @startingRow, @pageSize, @moduleKey, @resourceKey, @objectKey, @status

	-- Get the Error Code for the statement just executed.
	SELECT @errorCode = @@ERROR
END

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Calendar
-- * Procedure Name : gensp_Calendar_InsertOne
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Calendar_InsertOne]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Calendar_InsertOne]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Calendar_InsertOne]
	@moduleKey INT,
	@resourceKey INT,
	@objectKey INT,
	@subject VARCHAR(150),
	@startDate SMALLDATETIME,
	@endDate SMALLDATETIME,
	@allDayEvent BIT,
	@location VARCHAR(150),
	@description VARCHAR(MAX),
	@lastModificationTime DATETIME,
	@status INT,
	@calendarKey INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON


INSERT INTO [Calendar]
(
	[ModuleKey],
	[ResourceKey],
	[ObjectKey],
	[Subject],
	[StartDate],
	[EndDate],
	[AllDayEvent],
	[Location],
	[Description],
	[LastModificationTime],
	[Status]
)
VALUES
(
	@moduleKey,
	@resourceKey,
	@objectKey,
	@subject,
	@startDate,
	@endDate,
	@allDayEvent,
	@location,
	@description,
	@lastModificationTime,
	@status
)
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR
-- Get the IDENTITY value for the row just inserted.
SELECT @calendarKey = SCOPE_IDENTITY()

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Calendar
-- * Procedure Name : gensp_Calendar_UpdateOneByCalendarKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Calendar_UpdateOneByCalendarKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Calendar_UpdateOneByCalendarKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Calendar_UpdateOneByCalendarKey]
	@calendarKey INT,
	@moduleKey INT,
	@resourceKey INT,
	@objectKey INT,
	@subject VARCHAR(150),
	@startDate SMALLDATETIME,
	@endDate SMALLDATETIME,
	@allDayEvent BIT,
	@location VARCHAR(150),
	@description VARCHAR(MAX),
	@lastModificationTime DATETIME,
	@status INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

UPDATE [Calendar]
SET
	[ModuleKey] = @moduleKey,
	[ResourceKey] = @resourceKey,
	[ObjectKey] = @objectKey,
	[Subject] = @subject,
	[StartDate] = @startDate,
	[EndDate] = @endDate,
	[AllDayEvent] = @allDayEvent,
	[Location] = @location,
	[Description] = @description,
	[LastModificationTime] = @lastModificationTime,
	[Status] = @status
WHERE [Calendar].[CalendarKey] = @calendarKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Calendar
-- * Procedure Name : gensp_Calendar_DeleteOneByCalendarKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Calendar_DeleteOneByCalendarKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Calendar_DeleteOneByCalendarKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Calendar_DeleteOneByCalendarKey]
	@calendarKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DELETE FROM [Calendar]
WHERE [Calendar].[CalendarKey] = @calendarKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Company
-- * Procedure Name : gensp_Company_SelectOneByCompanyKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Company_SelectOneByCompanyKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Company_SelectOneByCompanyKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Company_SelectOneByCompanyKey]
	@companyKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON
SELECT
	[Company].[CompanyKey], 
	[Company].[ParentCompanyKey], 
	[Company].[RelatedCompanyKey], 
	[Company].[CompanyTypeKey], 
	[Company].[PortalKey], 
	[Company].[CompanyID], 
	[Company].[Name], 
	[Company].[LegalName], 
	[Company].[TaxID], 
	[Company].[Work], 
	[Company].[Work2], 
	[Company].[Fax], 
	[Company].[Address], 
	[Company].[Address2], 
	[Company].[City], 
	[Company].[State], 
	[Company].[Zip], 
	[Company].[Website], 
	[Company].[Description], 
	[Company].[BidRequestResponseDays], 
	[Company].[BidSubmitDays], 
	[Company].[BidRequestAmount], 
	[Company].[NotificationPreference], 
	[Company].[DateAdded], 
	[Company].[LastModificationTime], 
	[Company].[Status] 
FROM
	[Company] 
WHERE [Company].[CompanyKey] = @companyKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Company
-- * Procedure Name : gensp_Company_SelectSomeBySearch
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Company_SelectSomeBySearch]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Company_SelectSomeBySearch]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Company_SelectSomeBySearch]
	@parentCompanyKey INT = 0,
	@relatedCompanyKey INT = 0,
	@companyTypeKey INT = 0,
	@portalKey INT = 0,
	@propertyKeyList VARCHAR(2000) = NULL,
	@state VARCHAR(2) = '',
	@name VARCHAR(150) = '',
	@status INT = 0,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

-- Select PortalKey & UnitKey
SELECT PortalKey, UnitKey
INTO #T
FROM [dbo].[Split_PropertyKeyList](@propertyKeyList, ',')

SELECT
	[Company].[CompanyKey], 
	[Company].[ParentCompanyKey], 
	[Company].[RelatedCompanyKey], 
	[Company].[CompanyTypeKey], 
	[Company].[PortalKey], 
	[Company].[CompanyID], 
	[Company].[Name], 
	[Company].[LegalName], 
	[Company].[TaxID], 
	[Company].[Work], 
	[Company].[Work2], 
	[Company].[Fax], 
	[Company].[Address], 
	[Company].[Address2], 
	[Company].[City], 
	[Company].[State], 
	[Company].[Zip], 
	[Company].[Website], 
	[Company].[Description], 
	[Company].[BidRequestResponseDays], 
	[Company].[BidSubmitDays], 
	[Company].[BidRequestAmount], 
	[Company].[NotificationPreference], 
	[Company].[DateAdded], 
	[Company].[LastModificationTime], 
	[Company].[Status] 
FROM
	[Company] 
	LEFT JOIN #T T ON [Company].PortalKey = T.PortalKey 
WHERE ([Company].[ParentCompanyKey] = @parentCompanyKey OR @parentCompanyKey IS NULL OR @parentCompanyKey = 0)
AND ([Company].[RelatedCompanyKey] = @relatedCompanyKey OR @relatedCompanyKey IS NULL OR @relatedCompanyKey = 0)
AND ([Company].[CompanyTypeKey] = @companyTypeKey OR @companyTypeKey IS NULL OR @companyTypeKey = 0)
AND ([Company].[PortalKey] = @portalKey OR @portalKey IS NULL OR @portalKey = 0)
AND ([Company].[State] LIKE @state OR @state IS NULL OR @state = '')
AND ([Company].[Name] LIKE @name OR @name IS NULL OR @name = '')
AND ([Company].[Status] & ISNULL(@status, 7) = [Company].[Status])
AND (T.PortalKey IS NOT NULL OR ISNULL(@propertyKeyList, '') = '') 

-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Company
-- * Procedure Name : gensp_Company_SelectSomeBySearchAndPaging
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Company_SelectSomeBySearchAndPaging]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Company_SelectSomeBySearchAndPaging]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Company_SelectSomeBySearchAndPaging]
	@parentCompanyKey INT = 0,
	@relatedCompanyKey INT = 0,
	@companyTypeKey INT = 0,
	@portalKey INT = 0,
	@propertyKeyList VARCHAR(2000) = NULL,
	@state VARCHAR(2) = '',
	@name VARCHAR(150) = '',
	@status INT = 0,
	@sortOrder varchar(250) = null,
	@pageSize int = 0,
	@pageNumber int = 0,
	@totalRecordCount INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DECLARE @sqlString nvarchar(4000)
DECLARE @parmDefinition nvarchar(1000)
DECLARE @startingRow int

IF (@pageNumber = 0)
	SET @pageNumber = 1

IF (@pageSize = 0)
	SET @pageSize = 10

IF (@pageNumber = 1)
	SET @startingRow = 1
ELSE
	SET @startingRow = ((@pageNumber - 1) * @pageSize) + 1

-- Add the primary key for better sorting results
IF (ISNULL(@sortOrder, '') = '')
	SET @sortOrder = '[Company].[CompanyKey]'
ELSE
	SET @sortOrder = @sortOrder + ', [Company].[CompanyKey]'

SET @parmDefinition = N' @startingRow INT, @pageSize INT, @parentCompanyKey INT, @relatedCompanyKey INT, @companyTypeKey INT, @portalKey INT, @state VARCHAR(2), @name VARCHAR(150), @status INT'

-- Select PortalKey & UnitKey
SELECT PortalKey, UnitKey
INTO #T
FROM [dbo].[Split_PropertyKeyList](@propertyKeyList, ',')

-- Get total records count
SELECT @totalRecordCount = COUNT(*)
FROM
	[Company] 
	LEFT JOIN #T T ON [Company].PortalKey = T.PortalKey 
WHERE ([Company].[ParentCompanyKey] = @parentCompanyKey OR @parentCompanyKey IS NULL OR @parentCompanyKey = 0)
AND ([Company].[RelatedCompanyKey] = @relatedCompanyKey OR @relatedCompanyKey IS NULL OR @relatedCompanyKey = 0)
AND ([Company].[CompanyTypeKey] = @companyTypeKey OR @companyTypeKey IS NULL OR @companyTypeKey = 0)
AND ([Company].[PortalKey] = @portalKey OR @portalKey IS NULL OR @portalKey = 0)
AND ([Company].[State] LIKE @state OR @state IS NULL OR @state = '')
AND ([Company].[Name] LIKE @name OR @name IS NULL OR @name = '')
AND ([Company].[Status] & ISNULL(@status, 7) = [Company].[Status])
AND (T.PortalKey IS NOT NULL OR ISNULL(@propertyKeyList, '') = '') 

-- Determine records exist to retrieve
IF (@totalRecordCount > 0)
BEGIN

	SET @sqlString = N'SELECT * ' + 
		'FROM ( ' + 
		'	SELECT ROW_NUMBER() OVER ( ORDER BY ' + @sortOrder + ' ) AS RowNumber, ' + 
		'		[Company].[CompanyKey], ' +
		'		[Company].[ParentCompanyKey], ' +
		'		[Company].[RelatedCompanyKey], ' +
		'		[Company].[CompanyTypeKey], ' +
		'		[Company].[PortalKey], ' +
		'		[Company].[CompanyID], ' +
		'		[Company].[Name], ' +
		'		[Company].[LegalName], ' +
		'		[Company].[TaxID], ' +
		'		[Company].[Work], ' +
		'		[Company].[Work2], ' +
		'		[Company].[Fax], ' +
		'		[Company].[Address], ' +
		'		[Company].[Address2], ' +
		'		[Company].[City], ' +
		'		[Company].[State], ' +
		'		[Company].[Zip], ' +
		'		[Company].[Website], ' +
		'		[Company].[Description], ' +
		'		[Company].[BidRequestResponseDays], ' +
		'		[Company].[BidSubmitDays], ' +
		'		[Company].[BidRequestAmount], ' +
		'		[Company].[NotificationPreference], ' +
		'		[Company].[DateAdded], ' +
		'		[Company].[LastModificationTime], ' +
		'		[Company].[Status] ' +
		'	FROM [Company]  ' + 
		'	LEFT JOIN #T T ON [Company].PortalKey = T.PortalKey ' + 
		'	WHERE ([Company].[ParentCompanyKey] = @parentCompanyKey OR @parentCompanyKey IS NULL OR @parentCompanyKey = 0) ' +
		'	AND ([Company].[RelatedCompanyKey] = @relatedCompanyKey OR @relatedCompanyKey IS NULL OR @relatedCompanyKey = 0) ' +
		'	AND ([Company].[CompanyTypeKey] = @companyTypeKey OR @companyTypeKey IS NULL OR @companyTypeKey = 0) ' +
		'	AND ([Company].[PortalKey] = @portalKey OR @portalKey IS NULL OR @portalKey = 0) ' +
		'	AND ([Company].[State] LIKE @state OR @state IS NULL OR @state = '''') ' +
		'	AND ([Company].[Name] LIKE @name OR @name IS NULL OR @name = '''') ' +
		'	AND ([Company].[Status] & ISNULL(@status, 7) = [Company].[Status]) ' +
		'' 

	IF (ISNULL(@propertyKeyList, '') <> '')
		SET @sqlString = @sqlString + 'AND (T.PortalKey IS NOT NULL) ' 

	SET @sqlString = @sqlString + 
		') AS FilteredResult ' + 
		'WHERE RowNumber >= @startingRow ' + 
		'AND RowNumber < (@startingRow + @pageSize) ' + 
		'ORDER BY RowNumber ' 

	EXECUTE sp_executesql @sqlString, @parmDefinition, @startingRow, @pageSize, @parentCompanyKey, @relatedCompanyKey, @companyTypeKey, @portalKey, @state, @name, @status

	-- Get the Error Code for the statement just executed.
	SELECT @errorCode = @@ERROR
END

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Company
-- * Procedure Name : gensp_Company_InsertOne
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Company_InsertOne]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Company_InsertOne]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Company_InsertOne]
	@parentCompanyKey INT,
	@relatedCompanyKey INT,
	@companyTypeKey INT,
	@portalKey INT,
	@companyID VARCHAR(255),
	@name VARCHAR(150),
	@legalName VARCHAR(150),
	@taxID VARCHAR(50),
	@work VARCHAR(50),
	@work2 VARCHAR(50),
	@fax VARCHAR(50),
	@address VARCHAR(100),
	@address2 VARCHAR(100),
	@city VARCHAR(50),
	@state VARCHAR(2),
	@zip VARCHAR(11),
	@website VARCHAR(255),
	@description VARCHAR(MAX),
	@bidRequestResponseDays INT,
	@bidSubmitDays INT,
	@bidRequestAmount MONEY,
	@notificationPreference INT,
	@dateAdded DATETIME,
	@lastModificationTime DATETIME,
	@status INT,
	@companyKey INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON


INSERT INTO [Company]
(
	[ParentCompanyKey],
	[RelatedCompanyKey],
	[CompanyTypeKey],
	[PortalKey],
	[CompanyID],
	[Name],
	[LegalName],
	[TaxID],
	[Work],
	[Work2],
	[Fax],
	[Address],
	[Address2],
	[City],
	[State],
	[Zip],
	[Website],
	[Description],
	[BidRequestResponseDays],
	[BidSubmitDays],
	[BidRequestAmount],
	[NotificationPreference],
	[DateAdded],
	[LastModificationTime],
	[Status]
)
VALUES
(
	@parentCompanyKey,
	@relatedCompanyKey,
	@companyTypeKey,
	@portalKey,
	@companyID,
	@name,
	@legalName,
	@taxID,
	@work,
	@work2,
	@fax,
	@address,
	@address2,
	@city,
	@state,
	@zip,
	@website,
	@description,
	@bidRequestResponseDays,
	@bidSubmitDays,
	@bidRequestAmount,
	@notificationPreference,
	@dateAdded,
	@lastModificationTime,
	@status
)
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR
-- Get the IDENTITY value for the row just inserted.
SELECT @companyKey = SCOPE_IDENTITY()

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Company
-- * Procedure Name : gensp_Company_UpdateOneByCompanyKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Company_UpdateOneByCompanyKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Company_UpdateOneByCompanyKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Company_UpdateOneByCompanyKey]
	@companyKey INT,
	@parentCompanyKey INT,
	@relatedCompanyKey INT,
	@companyTypeKey INT,
	@portalKey INT,
	@companyID VARCHAR(255),
	@name VARCHAR(150),
	@legalName VARCHAR(150),
	@taxID VARCHAR(50),
	@work VARCHAR(50),
	@work2 VARCHAR(50),
	@fax VARCHAR(50),
	@address VARCHAR(100),
	@address2 VARCHAR(100),
	@city VARCHAR(50),
	@state VARCHAR(2),
	@zip VARCHAR(11),
	@website VARCHAR(255),
	@description VARCHAR(MAX),
	@bidRequestResponseDays INT,
	@bidSubmitDays INT,
	@bidRequestAmount MONEY,
	@notificationPreference INT,
	@dateAdded DATETIME,
	@lastModificationTime DATETIME,
	@status INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

UPDATE [Company]
SET
	[ParentCompanyKey] = @parentCompanyKey,
	[RelatedCompanyKey] = @relatedCompanyKey,
	[CompanyTypeKey] = @companyTypeKey,
	[PortalKey] = @portalKey,
	[CompanyID] = @companyID,
	[Name] = @name,
	[LegalName] = @legalName,
	[TaxID] = @taxID,
	[Work] = @work,
	[Work2] = @work2,
	[Fax] = @fax,
	[Address] = @address,
	[Address2] = @address2,
	[City] = @city,
	[State] = @state,
	[Zip] = @zip,
	[Website] = @website,
	[Description] = @description,
	[BidRequestResponseDays] = @bidRequestResponseDays,
	[BidSubmitDays] = @bidSubmitDays,
	[BidRequestAmount] = @bidRequestAmount,
	[NotificationPreference] = @notificationPreference,
	[DateAdded] = @dateAdded,
	[LastModificationTime] = @lastModificationTime,
	[Status] = @status
WHERE [Company].[CompanyKey] = @companyKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Company
-- * Procedure Name : gensp_Company_DeleteOneByCompanyKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Company_DeleteOneByCompanyKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Company_DeleteOneByCompanyKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Company_DeleteOneByCompanyKey]
	@companyKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DELETE FROM [Company]
WHERE [Company].[CompanyKey] = @companyKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : CompanyVendor
-- * Procedure Name : gensp_CompanyVendor_SelectOneByCompanyVendorKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_CompanyVendor_SelectOneByCompanyVendorKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_CompanyVendor_SelectOneByCompanyVendorKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_CompanyVendor_SelectOneByCompanyVendorKey]
	@companyVendorKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON
SELECT
	[CompanyVendor].[CompanyVendorKey], 
	[CompanyVendor].[CompanyKey], 
	[CompanyVendor].[VendorKey], 
	[CompanyVendor].[LastModificationTime], 
	[CompanyVendor].[Status] 
FROM
	[CompanyVendor] 
WHERE [CompanyVendor].[CompanyVendorKey] = @companyVendorKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : CompanyVendor
-- * Procedure Name : gensp_CompanyVendor_SelectSomeBySearch
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_CompanyVendor_SelectSomeBySearch]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_CompanyVendor_SelectSomeBySearch]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_CompanyVendor_SelectSomeBySearch]
	@companyKey INT = 0,
	@vendorKey INT = 0,
	@status INT = 0,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

SELECT
	[CompanyVendor].[CompanyVendorKey], 
	[CompanyVendor].[CompanyKey], 
	[CompanyVendor].[VendorKey], 
	[CompanyVendor].[LastModificationTime], 
	[CompanyVendor].[Status] 
FROM
	[CompanyVendor] 
WHERE ([CompanyVendor].[CompanyKey] = @companyKey OR @companyKey IS NULL OR @companyKey = 0)
AND ([CompanyVendor].[VendorKey] = @vendorKey OR @vendorKey IS NULL OR @vendorKey = 0)
AND ([CompanyVendor].[Status] & ISNULL(@status, 7) = [CompanyVendor].[Status])

-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : CompanyVendor
-- * Procedure Name : gensp_CompanyVendor_SelectSomeBySearchAndPaging
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_CompanyVendor_SelectSomeBySearchAndPaging]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_CompanyVendor_SelectSomeBySearchAndPaging]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_CompanyVendor_SelectSomeBySearchAndPaging]
	@companyKey INT = 0,
	@vendorKey INT = 0,
	@status INT = 0,
	@sortOrder varchar(250) = null,
	@pageSize int = 0,
	@pageNumber int = 0,
	@totalRecordCount INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DECLARE @sqlString nvarchar(4000)
DECLARE @parmDefinition nvarchar(1000)
DECLARE @startingRow int

IF (@pageNumber = 0)
	SET @pageNumber = 1

IF (@pageSize = 0)
	SET @pageSize = 10

IF (@pageNumber = 1)
	SET @startingRow = 1
ELSE
	SET @startingRow = ((@pageNumber - 1) * @pageSize) + 1

-- Add the primary key for better sorting results
IF (ISNULL(@sortOrder, '') = '')
	SET @sortOrder = '[CompanyVendor].[CompanyVendorKey]'
ELSE
	SET @sortOrder = @sortOrder + ', [CompanyVendor].[CompanyVendorKey]'

SET @parmDefinition = N' @startingRow INT, @pageSize INT, @companyKey INT, @vendorKey INT, @status INT'

-- Get total records count
SELECT @totalRecordCount = COUNT(*)
FROM
	[CompanyVendor] 
WHERE ([CompanyVendor].[CompanyKey] = @companyKey OR @companyKey IS NULL OR @companyKey = 0)
AND ([CompanyVendor].[VendorKey] = @vendorKey OR @vendorKey IS NULL OR @vendorKey = 0)
AND ([CompanyVendor].[Status] & ISNULL(@status, 7) = [CompanyVendor].[Status])

-- Determine records exist to retrieve
IF (@totalRecordCount > 0)
BEGIN

	SET @sqlString = N'SELECT * ' + 
		'FROM ( ' + 
		'	SELECT ROW_NUMBER() OVER ( ORDER BY ' + @sortOrder + ' ) AS RowNumber, ' + 
		'		[CompanyVendor].[CompanyVendorKey], ' +
		'		[CompanyVendor].[CompanyKey], ' +
		'		[CompanyVendor].[VendorKey], ' +
		'		[CompanyVendor].[LastModificationTime], ' +
		'		[CompanyVendor].[Status] ' +
		'	FROM [CompanyVendor]  ' + 
		'	WHERE ([CompanyVendor].[CompanyKey] = @companyKey OR @companyKey IS NULL OR @companyKey = 0) ' +
		'	AND ([CompanyVendor].[VendorKey] = @vendorKey OR @vendorKey IS NULL OR @vendorKey = 0) ' +
		'	AND ([CompanyVendor].[Status] & ISNULL(@status, 7) = [CompanyVendor].[Status]) ' +
		') AS FilteredResult ' + 
		'WHERE RowNumber >= @startingRow ' + 
		'AND RowNumber < (@startingRow + @pageSize) ' + 
		'ORDER BY RowNumber ' 

	EXECUTE sp_executesql @sqlString, @parmDefinition, @startingRow, @pageSize, @companyKey, @vendorKey, @status

	-- Get the Error Code for the statement just executed.
	SELECT @errorCode = @@ERROR
END

GO

-- *****************************************************

-- *****************************************************
-- * Table          : CompanyVendor
-- * Procedure Name : gensp_CompanyVendor_InsertOne
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_CompanyVendor_InsertOne]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_CompanyVendor_InsertOne]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_CompanyVendor_InsertOne]
	@companyKey INT,
	@vendorKey INT,
	@lastModificationTime DATETIME,
	@status INT,
	@companyVendorKey INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON


INSERT INTO [CompanyVendor]
(
	[CompanyKey],
	[VendorKey],
	[LastModificationTime],
	[Status]
)
VALUES
(
	@companyKey,
	@vendorKey,
	@lastModificationTime,
	@status
)
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR
-- Get the IDENTITY value for the row just inserted.
SELECT @companyVendorKey = SCOPE_IDENTITY()

GO

-- *****************************************************

-- *****************************************************
-- * Table          : CompanyVendor
-- * Procedure Name : gensp_CompanyVendor_UpdateOneByCompanyVendorKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_CompanyVendor_UpdateOneByCompanyVendorKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_CompanyVendor_UpdateOneByCompanyVendorKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_CompanyVendor_UpdateOneByCompanyVendorKey]
	@companyVendorKey INT,
	@companyKey INT,
	@vendorKey INT,
	@lastModificationTime DATETIME,
	@status INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

UPDATE [CompanyVendor]
SET
	[CompanyKey] = @companyKey,
	[VendorKey] = @vendorKey,
	[LastModificationTime] = @lastModificationTime,
	[Status] = @status
WHERE [CompanyVendor].[CompanyVendorKey] = @companyVendorKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : CompanyVendor
-- * Procedure Name : gensp_CompanyVendor_DeleteOneByCompanyVendorKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_CompanyVendor_DeleteOneByCompanyVendorKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_CompanyVendor_DeleteOneByCompanyVendorKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_CompanyVendor_DeleteOneByCompanyVendorKey]
	@companyVendorKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DELETE FROM [CompanyVendor]
WHERE [CompanyVendor].[CompanyVendorKey] = @companyVendorKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Document
-- * Procedure Name : gensp_Document_SelectOneByDocumentKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Document_SelectOneByDocumentKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Document_SelectOneByDocumentKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Document_SelectOneByDocumentKey]
	@documentKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON
SELECT
	[Document].[DocumentKey], 
	[Document].[ModuleKey], 
	[Document].[ObjectKey], 
	[Document].[FileName], 
	[Document].[FileSize], 
	[Document].[LastModificationTime] 
FROM
	[Document] 
WHERE [Document].[DocumentKey] = @documentKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Document
-- * Procedure Name : gensp_Document_SelectSomeBySearch
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Document_SelectSomeBySearch]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Document_SelectSomeBySearch]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Document_SelectSomeBySearch]
	@moduleKey INT = 0,
	@objectKey INT = 0,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

SELECT
	[Document].[DocumentKey], 
	[Document].[ModuleKey], 
	[Document].[ObjectKey], 
	[Document].[FileName], 
	[Document].[FileSize], 
	[Document].[LastModificationTime] 
FROM
	[Document] 
WHERE ([Document].[ModuleKey] = @moduleKey OR @moduleKey IS NULL OR @moduleKey = 0)
AND ([Document].[ObjectKey] = @objectKey OR @objectKey IS NULL OR @objectKey = 0)

-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Document
-- * Procedure Name : gensp_Document_SelectSomeBySearchAndPaging
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Document_SelectSomeBySearchAndPaging]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Document_SelectSomeBySearchAndPaging]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Document_SelectSomeBySearchAndPaging]
	@moduleKey INT = 0,
	@objectKey INT = 0,
	@sortOrder varchar(250) = null,
	@pageSize int = 0,
	@pageNumber int = 0,
	@totalRecordCount INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DECLARE @sqlString nvarchar(4000)
DECLARE @parmDefinition nvarchar(1000)
DECLARE @startingRow int

IF (@pageNumber = 0)
	SET @pageNumber = 1

IF (@pageSize = 0)
	SET @pageSize = 10

IF (@pageNumber = 1)
	SET @startingRow = 1
ELSE
	SET @startingRow = ((@pageNumber - 1) * @pageSize) + 1

-- Add the primary key for better sorting results
IF (ISNULL(@sortOrder, '') = '')
	SET @sortOrder = '[Document].[DocumentKey]'
ELSE
	SET @sortOrder = @sortOrder + ', [Document].[DocumentKey]'

SET @parmDefinition = N' @startingRow INT, @pageSize INT, @moduleKey INT, @objectKey INT'

-- Get total records count
SELECT @totalRecordCount = COUNT(*)
FROM
	[Document] 
WHERE ([Document].[ModuleKey] = @moduleKey OR @moduleKey IS NULL OR @moduleKey = 0)
AND ([Document].[ObjectKey] = @objectKey OR @objectKey IS NULL OR @objectKey = 0)

-- Determine records exist to retrieve
IF (@totalRecordCount > 0)
BEGIN

	SET @sqlString = N'SELECT * ' + 
		'FROM ( ' + 
		'	SELECT ROW_NUMBER() OVER ( ORDER BY ' + @sortOrder + ' ) AS RowNumber, ' + 
		'		[Document].[DocumentKey], ' +
		'		[Document].[ModuleKey], ' +
		'		[Document].[ObjectKey], ' +
		'		[Document].[FileName], ' +
		'		[Document].[FileSize], ' +
		'		[Document].[LastModificationTime] ' +
		'	FROM [Document]  ' + 
		'	WHERE ([Document].[ModuleKey] = @moduleKey OR @moduleKey IS NULL OR @moduleKey = 0) ' +
		'	AND ([Document].[ObjectKey] = @objectKey OR @objectKey IS NULL OR @objectKey = 0) ' +
		') AS FilteredResult ' + 
		'WHERE RowNumber >= @startingRow ' + 
		'AND RowNumber < (@startingRow + @pageSize) ' + 
		'ORDER BY RowNumber ' 

	EXECUTE sp_executesql @sqlString, @parmDefinition, @startingRow, @pageSize, @moduleKey, @objectKey

	-- Get the Error Code for the statement just executed.
	SELECT @errorCode = @@ERROR
END

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Document
-- * Procedure Name : gensp_Document_InsertOne
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Document_InsertOne]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Document_InsertOne]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Document_InsertOne]
	@moduleKey INT,
	@objectKey INT,
	@fileName VARCHAR(150),
	@fileSize FLOAT(53),
	@lastModificationTime DATETIME,
	@documentKey INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON


INSERT INTO [Document]
(
	[ModuleKey],
	[ObjectKey],
	[FileName],
	[FileSize],
	[LastModificationTime]
)
VALUES
(
	@moduleKey,
	@objectKey,
	@fileName,
	@fileSize,
	@lastModificationTime
)
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR
-- Get the IDENTITY value for the row just inserted.
SELECT @documentKey = SCOPE_IDENTITY()

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Document
-- * Procedure Name : gensp_Document_UpdateOneByDocumentKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Document_UpdateOneByDocumentKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Document_UpdateOneByDocumentKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Document_UpdateOneByDocumentKey]
	@documentKey INT,
	@moduleKey INT,
	@objectKey INT,
	@fileName VARCHAR(150),
	@fileSize FLOAT(53),
	@lastModificationTime DATETIME,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

UPDATE [Document]
SET
	[ModuleKey] = @moduleKey,
	[ObjectKey] = @objectKey,
	[FileName] = @fileName,
	[FileSize] = @fileSize,
	[LastModificationTime] = @lastModificationTime
WHERE [Document].[DocumentKey] = @documentKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Document
-- * Procedure Name : gensp_Document_DeleteOneByDocumentKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Document_DeleteOneByDocumentKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Document_DeleteOneByDocumentKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Document_DeleteOneByDocumentKey]
	@documentKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DELETE FROM [Document]
WHERE [Document].[DocumentKey] = @documentKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Email
-- * Procedure Name : gensp_Email_SelectOneByEmailKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Email_SelectOneByEmailKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Email_SelectOneByEmailKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Email_SelectOneByEmailKey]
	@emailKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON
SELECT
	[Email].[EmailKey], 
	[Email].[ModuleKey], 
	[Email].[ResourceKey], 
	[Email].[ObjectKey], 
	[Email].[From], 
	[Email].[To], 
	[Email].[Cc], 
	[Email].[Bcc], 
	[Email].[Subject], 
	[Email].[Body], 
	[Email].[DateAdded], 
	[Email].[DateSent], 
	[Email].[EmailStatus] 
FROM
	[Email] 
WHERE [Email].[EmailKey] = @emailKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Email
-- * Procedure Name : gensp_Email_SelectSomeBySearch
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Email_SelectSomeBySearch]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Email_SelectSomeBySearch]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Email_SelectSomeBySearch]
	@moduleKey INT = 0,
	@resourceKey INT = 0,
	@objectKey INT = 0,
	@emailStatus INT = 0,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

SELECT
	[Email].[EmailKey], 
	[Email].[ModuleKey], 
	[Email].[ResourceKey], 
	[Email].[ObjectKey], 
	[Email].[From], 
	[Email].[To], 
	[Email].[Cc], 
	[Email].[Bcc], 
	[Email].[Subject], 
	[Email].[Body], 
	[Email].[DateAdded], 
	[Email].[DateSent], 
	[Email].[EmailStatus] 
FROM
	[Email] 
WHERE ([Email].[ModuleKey] = @moduleKey OR @moduleKey IS NULL OR @moduleKey = 0)
AND ([Email].[ResourceKey] = @resourceKey OR @resourceKey IS NULL OR @resourceKey = 0)
AND ([Email].[ObjectKey] = @objectKey OR @objectKey IS NULL OR @objectKey = 0)
AND ([Email].[EmailStatus] & @emailStatus = [Email].[EmailStatus] OR @emailStatus IS NULL OR @emailStatus = 0)

-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Email
-- * Procedure Name : gensp_Email_SelectSomeBySearchAndPaging
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Email_SelectSomeBySearchAndPaging]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Email_SelectSomeBySearchAndPaging]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Email_SelectSomeBySearchAndPaging]
	@moduleKey INT = 0,
	@resourceKey INT = 0,
	@objectKey INT = 0,
	@emailStatus INT = 0,
	@sortOrder varchar(250) = null,
	@pageSize int = 0,
	@pageNumber int = 0,
	@totalRecordCount INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DECLARE @sqlString nvarchar(4000)
DECLARE @parmDefinition nvarchar(1000)
DECLARE @startingRow int

IF (@pageNumber = 0)
	SET @pageNumber = 1

IF (@pageSize = 0)
	SET @pageSize = 10

IF (@pageNumber = 1)
	SET @startingRow = 1
ELSE
	SET @startingRow = ((@pageNumber - 1) * @pageSize) + 1

-- Add the primary key for better sorting results
IF (ISNULL(@sortOrder, '') = '')
	SET @sortOrder = '[Email].[EmailKey]'
ELSE
	SET @sortOrder = @sortOrder + ', [Email].[EmailKey]'

SET @parmDefinition = N' @startingRow INT, @pageSize INT, @moduleKey INT, @resourceKey INT, @objectKey INT, @emailStatus INT'

-- Get total records count
SELECT @totalRecordCount = COUNT(*)
FROM
	[Email] 
WHERE ([Email].[ModuleKey] = @moduleKey OR @moduleKey IS NULL OR @moduleKey = 0)
AND ([Email].[ResourceKey] = @resourceKey OR @resourceKey IS NULL OR @resourceKey = 0)
AND ([Email].[ObjectKey] = @objectKey OR @objectKey IS NULL OR @objectKey = 0)
AND ([Email].[EmailStatus] & @emailStatus = [Email].[EmailStatus] OR @emailStatus IS NULL OR @emailStatus = 0)

-- Determine records exist to retrieve
IF (@totalRecordCount > 0)
BEGIN

	SET @sqlString = N'SELECT * ' + 
		'FROM ( ' + 
		'	SELECT ROW_NUMBER() OVER ( ORDER BY ' + @sortOrder + ' ) AS RowNumber, ' + 
		'		[Email].[EmailKey], ' +
		'		[Email].[ModuleKey], ' +
		'		[Email].[ResourceKey], ' +
		'		[Email].[ObjectKey], ' +
		'		[Email].[From], ' +
		'		[Email].[To], ' +
		'		[Email].[Cc], ' +
		'		[Email].[Bcc], ' +
		'		[Email].[Subject], ' +
		'		[Email].[Body], ' +
		'		[Email].[DateAdded], ' +
		'		[Email].[DateSent], ' +
		'		[Email].[EmailStatus] ' +
		'	FROM [Email]  ' + 
		'	WHERE ([Email].[ModuleKey] = @moduleKey OR @moduleKey IS NULL OR @moduleKey = 0) ' +
		'	AND ([Email].[ResourceKey] = @resourceKey OR @resourceKey IS NULL OR @resourceKey = 0) ' +
		'	AND ([Email].[ObjectKey] = @objectKey OR @objectKey IS NULL OR @objectKey = 0) ' +
		'	AND ([Email].[EmailStatus] & @emailStatus = [Email].[EmailStatus] OR @emailStatus IS NULL OR @emailStatus = 0) ' +
		') AS FilteredResult ' + 
		'WHERE RowNumber >= @startingRow ' + 
		'AND RowNumber < (@startingRow + @pageSize) ' + 
		'ORDER BY RowNumber ' 

	EXECUTE sp_executesql @sqlString, @parmDefinition, @startingRow, @pageSize, @moduleKey, @resourceKey, @objectKey, @emailStatus

	-- Get the Error Code for the statement just executed.
	SELECT @errorCode = @@ERROR
END

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Email
-- * Procedure Name : gensp_Email_InsertOne
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Email_InsertOne]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Email_InsertOne]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Email_InsertOne]
	@moduleKey INT,
	@resourceKey INT,
	@objectKey INT,
	@from VARCHAR(MAX),
	@to VARCHAR(MAX),
	@cc VARCHAR(MAX),
	@bcc VARCHAR(MAX),
	@subject VARCHAR(MAX),
	@body VARCHAR(MAX),
	@dateAdded DATETIME,
	@dateSent SMALLDATETIME,
	@emailStatus INT,
	@emailKey INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON


INSERT INTO [Email]
(
	[ModuleKey],
	[ResourceKey],
	[ObjectKey],
	[From],
	[To],
	[Cc],
	[Bcc],
	[Subject],
	[Body],
	[DateAdded],
	[DateSent],
	[EmailStatus]
)
VALUES
(
	@moduleKey,
	@resourceKey,
	@objectKey,
	@from,
	@to,
	@cc,
	@bcc,
	@subject,
	@body,
	@dateAdded,
	@dateSent,
	@emailStatus
)
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR
-- Get the IDENTITY value for the row just inserted.
SELECT @emailKey = SCOPE_IDENTITY()

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Email
-- * Procedure Name : gensp_Email_UpdateOneByEmailKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Email_UpdateOneByEmailKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Email_UpdateOneByEmailKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Email_UpdateOneByEmailKey]
	@emailKey INT,
	@moduleKey INT,
	@resourceKey INT,
	@objectKey INT,
	@from VARCHAR(MAX),
	@to VARCHAR(MAX),
	@cc VARCHAR(MAX),
	@bcc VARCHAR(MAX),
	@subject VARCHAR(MAX),
	@body VARCHAR(MAX),
	@dateAdded DATETIME,
	@dateSent SMALLDATETIME,
	@emailStatus INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

UPDATE [Email]
SET
	[ModuleKey] = @moduleKey,
	[ResourceKey] = @resourceKey,
	[ObjectKey] = @objectKey,
	[From] = @from,
	[To] = @to,
	[Cc] = @cc,
	[Bcc] = @bcc,
	[Subject] = @subject,
	[Body] = @body,
	[DateAdded] = @dateAdded,
	[DateSent] = @dateSent,
	[EmailStatus] = @emailStatus
WHERE [Email].[EmailKey] = @emailKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Email
-- * Procedure Name : gensp_Email_DeleteOneByEmailKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Email_DeleteOneByEmailKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Email_DeleteOneByEmailKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Email_DeleteOneByEmailKey]
	@emailKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DELETE FROM [Email]
WHERE [Email].[EmailKey] = @emailKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : ErrorLog
-- * Procedure Name : gensp_ErrorLog_SelectOneByErrorLogKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_ErrorLog_SelectOneByErrorLogKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_ErrorLog_SelectOneByErrorLogKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_ErrorLog_SelectOneByErrorLogKey]
	@errorLogKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON
SELECT
	[ErrorLog].[ErrorLogKey], 
	[ErrorLog].[Details], 
	[ErrorLog].[Session], 
	[ErrorLog].[DateAdded] 
FROM
	[ErrorLog] 
WHERE [ErrorLog].[ErrorLogKey] = @errorLogKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : ErrorLog
-- * Procedure Name : gensp_ErrorLog_SelectSomeBySearch
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_ErrorLog_SelectSomeBySearch]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_ErrorLog_SelectSomeBySearch]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_ErrorLog_SelectSomeBySearch]
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

SELECT
	[ErrorLog].[ErrorLogKey], 
	[ErrorLog].[Details], 
	[ErrorLog].[Session], 
	[ErrorLog].[DateAdded] 
FROM
	[ErrorLog] 

-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : ErrorLog
-- * Procedure Name : gensp_ErrorLog_SelectSomeBySearchAndPaging
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_ErrorLog_SelectSomeBySearchAndPaging]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_ErrorLog_SelectSomeBySearchAndPaging]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_ErrorLog_SelectSomeBySearchAndPaging]
	@sortOrder varchar(250) = null,
	@pageSize int = 0,
	@pageNumber int = 0,
	@totalRecordCount INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DECLARE @sqlString nvarchar(4000)
DECLARE @parmDefinition nvarchar(1000)
DECLARE @startingRow int

IF (@pageNumber = 0)
	SET @pageNumber = 1

IF (@pageSize = 0)
	SET @pageSize = 10

IF (@pageNumber = 1)
	SET @startingRow = 1
ELSE
	SET @startingRow = ((@pageNumber - 1) * @pageSize) + 1

-- Add the primary key for better sorting results
IF (ISNULL(@sortOrder, '') = '')
	SET @sortOrder = '[ErrorLog].[ErrorLogKey]'
ELSE
	SET @sortOrder = @sortOrder + ', [ErrorLog].[ErrorLogKey]'

SET @parmDefinition = N' @startingRow INT, @pageSize INT'

-- Get total records count
SELECT @totalRecordCount = COUNT(*)
FROM
	[ErrorLog] 

-- Determine records exist to retrieve
IF (@totalRecordCount > 0)
BEGIN

	SET @sqlString = N'SELECT * ' + 
		'FROM ( ' + 
		'	SELECT ROW_NUMBER() OVER ( ORDER BY ' + @sortOrder + ' ) AS RowNumber, ' + 
		'		[ErrorLog].[ErrorLogKey], ' +
		'		[ErrorLog].[Details], ' +
		'		[ErrorLog].[Session], ' +
		'		[ErrorLog].[DateAdded] ' +
		'	FROM [ErrorLog]  ' + 
		') AS FilteredResult ' + 
		'WHERE RowNumber >= @startingRow ' + 
		'AND RowNumber < (@startingRow + @pageSize) ' + 
		'ORDER BY RowNumber ' 

	EXECUTE sp_executesql @sqlString, @parmDefinition, @startingRow, @pageSize

	-- Get the Error Code for the statement just executed.
	SELECT @errorCode = @@ERROR
END

GO

-- *****************************************************

-- *****************************************************
-- * Table          : ErrorLog
-- * Procedure Name : gensp_ErrorLog_InsertOne
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_ErrorLog_InsertOne]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_ErrorLog_InsertOne]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_ErrorLog_InsertOne]
	@details VARCHAR(MAX),
	@session VARCHAR(MAX),
	@dateAdded DATETIME,
	@errorLogKey INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON


INSERT INTO [ErrorLog]
(
	[Details],
	[Session],
	[DateAdded]
)
VALUES
(
	@details,
	@session,
	@dateAdded
)
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR
-- Get the IDENTITY value for the row just inserted.
SELECT @errorLogKey = SCOPE_IDENTITY()

GO

-- *****************************************************

-- *****************************************************
-- * Table          : ErrorLog
-- * Procedure Name : gensp_ErrorLog_UpdateOneByErrorLogKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_ErrorLog_UpdateOneByErrorLogKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_ErrorLog_UpdateOneByErrorLogKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_ErrorLog_UpdateOneByErrorLogKey]
	@errorLogKey INT,
	@details VARCHAR(MAX),
	@session VARCHAR(MAX),
	@dateAdded DATETIME,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

UPDATE [ErrorLog]
SET
	[Details] = @details,
	[Session] = @session,
	[DateAdded] = @dateAdded
WHERE [ErrorLog].[ErrorLogKey] = @errorLogKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : ErrorLog
-- * Procedure Name : gensp_ErrorLog_DeleteOneByErrorLogKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_ErrorLog_DeleteOneByErrorLogKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_ErrorLog_DeleteOneByErrorLogKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_ErrorLog_DeleteOneByErrorLogKey]
	@errorLogKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DELETE FROM [ErrorLog]
WHERE [ErrorLog].[ErrorLogKey] = @errorLogKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Group
-- * Procedure Name : gensp_Group_SelectOneByGroupKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Group_SelectOneByGroupKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Group_SelectOneByGroupKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Group_SelectOneByGroupKey]
	@groupKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON
SELECT
	[Group].[GroupKey], 
	[Group].[Title], 
	[Group].[Description] 
FROM
	[Group] 
WHERE [Group].[GroupKey] = @groupKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Group
-- * Procedure Name : gensp_Group_SelectSomeBySearch
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Group_SelectSomeBySearch]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Group_SelectSomeBySearch]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Group_SelectSomeBySearch]
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

SELECT
	[Group].[GroupKey], 
	[Group].[Title], 
	[Group].[Description] 
FROM
	[Group] 

-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Group
-- * Procedure Name : gensp_Group_SelectSomeBySearchAndPaging
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Group_SelectSomeBySearchAndPaging]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Group_SelectSomeBySearchAndPaging]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Group_SelectSomeBySearchAndPaging]
	@sortOrder varchar(250) = null,
	@pageSize int = 0,
	@pageNumber int = 0,
	@totalRecordCount INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DECLARE @sqlString nvarchar(4000)
DECLARE @parmDefinition nvarchar(1000)
DECLARE @startingRow int

IF (@pageNumber = 0)
	SET @pageNumber = 1

IF (@pageSize = 0)
	SET @pageSize = 10

IF (@pageNumber = 1)
	SET @startingRow = 1
ELSE
	SET @startingRow = ((@pageNumber - 1) * @pageSize) + 1

-- Add the primary key for better sorting results
IF (ISNULL(@sortOrder, '') = '')
	SET @sortOrder = '[Group].[GroupKey]'
ELSE
	SET @sortOrder = @sortOrder + ', [Group].[GroupKey]'

SET @parmDefinition = N' @startingRow INT, @pageSize INT'

-- Get total records count
SELECT @totalRecordCount = COUNT(*)
FROM
	[Group] 

-- Determine records exist to retrieve
IF (@totalRecordCount > 0)
BEGIN

	SET @sqlString = N'SELECT * ' + 
		'FROM ( ' + 
		'	SELECT ROW_NUMBER() OVER ( ORDER BY ' + @sortOrder + ' ) AS RowNumber, ' + 
		'		[Group].[GroupKey], ' +
		'		[Group].[Title], ' +
		'		[Group].[Description] ' +
		'	FROM [Group]  ' + 
		') AS FilteredResult ' + 
		'WHERE RowNumber >= @startingRow ' + 
		'AND RowNumber < (@startingRow + @pageSize) ' + 
		'ORDER BY RowNumber ' 

	EXECUTE sp_executesql @sqlString, @parmDefinition, @startingRow, @pageSize

	-- Get the Error Code for the statement just executed.
	SELECT @errorCode = @@ERROR
END

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Group
-- * Procedure Name : gensp_Group_InsertOne
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Group_InsertOne]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Group_InsertOne]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Group_InsertOne]
	@title VARCHAR(150),
	@description VARCHAR(MAX),
	@groupKey INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON


INSERT INTO [Group]
(
	[Title],
	[Description]
)
VALUES
(
	@title,
	@description
)
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR
-- Get the IDENTITY value for the row just inserted.
SELECT @groupKey = SCOPE_IDENTITY()

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Group
-- * Procedure Name : gensp_Group_UpdateOneByGroupKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Group_UpdateOneByGroupKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Group_UpdateOneByGroupKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Group_UpdateOneByGroupKey]
	@groupKey INT,
	@title VARCHAR(150),
	@description VARCHAR(MAX),
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

UPDATE [Group]
SET
	[Title] = @title,
	[Description] = @description
WHERE [Group].[GroupKey] = @groupKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Group
-- * Procedure Name : gensp_Group_DeleteOneByGroupKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Group_DeleteOneByGroupKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Group_DeleteOneByGroupKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Group_DeleteOneByGroupKey]
	@groupKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DELETE FROM [Group]
WHERE [Group].[GroupKey] = @groupKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : GroupMember
-- * Procedure Name : gensp_GroupMember_SelectOneByGroupMemberKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_GroupMember_SelectOneByGroupMemberKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_GroupMember_SelectOneByGroupMemberKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_GroupMember_SelectOneByGroupMemberKey]
	@groupMemberKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON
SELECT
	[GroupMember].[GroupMemberKey], 
	[GroupMember].[GroupKey], 
	[GroupMember].[ResourceKey] 
FROM
	[GroupMember] 
WHERE [GroupMember].[GroupMemberKey] = @groupMemberKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : GroupMember
-- * Procedure Name : gensp_GroupMember_SelectSomeBySearch
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_GroupMember_SelectSomeBySearch]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_GroupMember_SelectSomeBySearch]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_GroupMember_SelectSomeBySearch]
	@groupKey INT = 0,
	@resourceKey INT = 0,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

SELECT
	[GroupMember].[GroupMemberKey], 
	[GroupMember].[GroupKey], 
	[GroupMember].[ResourceKey] 
FROM
	[GroupMember] 
WHERE ([GroupMember].[GroupKey] = @groupKey OR @groupKey IS NULL OR @groupKey = 0)
AND ([GroupMember].[ResourceKey] = @resourceKey OR @resourceKey IS NULL OR @resourceKey = 0)

-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : GroupMember
-- * Procedure Name : gensp_GroupMember_SelectSomeBySearchAndPaging
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_GroupMember_SelectSomeBySearchAndPaging]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_GroupMember_SelectSomeBySearchAndPaging]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_GroupMember_SelectSomeBySearchAndPaging]
	@groupKey INT = 0,
	@resourceKey INT = 0,
	@sortOrder varchar(250) = null,
	@pageSize int = 0,
	@pageNumber int = 0,
	@totalRecordCount INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DECLARE @sqlString nvarchar(4000)
DECLARE @parmDefinition nvarchar(1000)
DECLARE @startingRow int

IF (@pageNumber = 0)
	SET @pageNumber = 1

IF (@pageSize = 0)
	SET @pageSize = 10

IF (@pageNumber = 1)
	SET @startingRow = 1
ELSE
	SET @startingRow = ((@pageNumber - 1) * @pageSize) + 1

-- Add the primary key for better sorting results
IF (ISNULL(@sortOrder, '') = '')
	SET @sortOrder = '[GroupMember].[GroupMemberKey]'
ELSE
	SET @sortOrder = @sortOrder + ', [GroupMember].[GroupMemberKey]'

SET @parmDefinition = N' @startingRow INT, @pageSize INT, @groupKey INT, @resourceKey INT'

-- Get total records count
SELECT @totalRecordCount = COUNT(*)
FROM
	[GroupMember] 
WHERE ([GroupMember].[GroupKey] = @groupKey OR @groupKey IS NULL OR @groupKey = 0)
AND ([GroupMember].[ResourceKey] = @resourceKey OR @resourceKey IS NULL OR @resourceKey = 0)

-- Determine records exist to retrieve
IF (@totalRecordCount > 0)
BEGIN

	SET @sqlString = N'SELECT * ' + 
		'FROM ( ' + 
		'	SELECT ROW_NUMBER() OVER ( ORDER BY ' + @sortOrder + ' ) AS RowNumber, ' + 
		'		[GroupMember].[GroupMemberKey], ' +
		'		[GroupMember].[GroupKey], ' +
		'		[GroupMember].[ResourceKey] ' +
		'	FROM [GroupMember]  ' + 
		'	WHERE ([GroupMember].[GroupKey] = @groupKey OR @groupKey IS NULL OR @groupKey = 0) ' +
		'	AND ([GroupMember].[ResourceKey] = @resourceKey OR @resourceKey IS NULL OR @resourceKey = 0) ' +
		') AS FilteredResult ' + 
		'WHERE RowNumber >= @startingRow ' + 
		'AND RowNumber < (@startingRow + @pageSize) ' + 
		'ORDER BY RowNumber ' 

	EXECUTE sp_executesql @sqlString, @parmDefinition, @startingRow, @pageSize, @groupKey, @resourceKey

	-- Get the Error Code for the statement just executed.
	SELECT @errorCode = @@ERROR
END

GO

-- *****************************************************

-- *****************************************************
-- * Table          : GroupMember
-- * Procedure Name : gensp_GroupMember_InsertOne
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_GroupMember_InsertOne]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_GroupMember_InsertOne]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_GroupMember_InsertOne]
	@groupKey INT,
	@resourceKey INT,
	@groupMemberKey INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON


INSERT INTO [GroupMember]
(
	[GroupKey],
	[ResourceKey]
)
VALUES
(
	@groupKey,
	@resourceKey
)
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR
-- Get the IDENTITY value for the row just inserted.
SELECT @groupMemberKey = SCOPE_IDENTITY()

GO

-- *****************************************************

-- *****************************************************
-- * Table          : GroupMember
-- * Procedure Name : gensp_GroupMember_UpdateOneByGroupMemberKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_GroupMember_UpdateOneByGroupMemberKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_GroupMember_UpdateOneByGroupMemberKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_GroupMember_UpdateOneByGroupMemberKey]
	@groupMemberKey INT,
	@groupKey INT,
	@resourceKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

UPDATE [GroupMember]
SET
	[GroupKey] = @groupKey,
	[ResourceKey] = @resourceKey
WHERE [GroupMember].[GroupMemberKey] = @groupMemberKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : GroupMember
-- * Procedure Name : gensp_GroupMember_DeleteOneByGroupMemberKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_GroupMember_DeleteOneByGroupMemberKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_GroupMember_DeleteOneByGroupMemberKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_GroupMember_DeleteOneByGroupMemberKey]
	@groupMemberKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DELETE FROM [GroupMember]
WHERE [GroupMember].[GroupMemberKey] = @groupMemberKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : GroupModuleAccess
-- * Procedure Name : gensp_GroupModuleAccess_SelectOneByGroupModuleAccessKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_GroupModuleAccess_SelectOneByGroupModuleAccessKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_GroupModuleAccess_SelectOneByGroupModuleAccessKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_GroupModuleAccess_SelectOneByGroupModuleAccessKey]
	@groupModuleAccessKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON
SELECT
	[GroupModuleAccess].[GroupModuleAccessKey], 
	[GroupModuleAccess].[PortalKey], 
	[GroupModuleAccess].[GroupKey], 
	[GroupModuleAccess].[ModuleKey], 
	[GroupModuleAccess].[Access] 
FROM
	[GroupModuleAccess] 
WHERE [GroupModuleAccess].[GroupModuleAccessKey] = @groupModuleAccessKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : GroupModuleAccess
-- * Procedure Name : gensp_GroupModuleAccess_SelectSomeBySearch
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_GroupModuleAccess_SelectSomeBySearch]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_GroupModuleAccess_SelectSomeBySearch]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_GroupModuleAccess_SelectSomeBySearch]
	@portalKey INT = 0,
	@propertyKeyList VARCHAR(2000) = NULL,
	@groupKey INT = 0,
	@moduleKey INT = 0,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

-- Select PortalKey & UnitKey
SELECT PortalKey, UnitKey
INTO #T
FROM [dbo].[Split_PropertyKeyList](@propertyKeyList, ',')

SELECT
	[GroupModuleAccess].[GroupModuleAccessKey], 
	[GroupModuleAccess].[PortalKey], 
	[GroupModuleAccess].[GroupKey], 
	[GroupModuleAccess].[ModuleKey], 
	[GroupModuleAccess].[Access] 
FROM
	[GroupModuleAccess] 
	LEFT JOIN #T T ON [GroupModuleAccess].PortalKey = T.PortalKey 
WHERE ([GroupModuleAccess].[PortalKey] = @portalKey OR @portalKey IS NULL OR @portalKey = 0)
AND ([GroupModuleAccess].[GroupKey] = @groupKey OR @groupKey IS NULL OR @groupKey = 0)
AND ([GroupModuleAccess].[ModuleKey] = @moduleKey OR @moduleKey IS NULL OR @moduleKey = 0)
AND (T.PortalKey IS NOT NULL OR ISNULL(@propertyKeyList, '') = '') 

-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : GroupModuleAccess
-- * Procedure Name : gensp_GroupModuleAccess_SelectSomeBySearchAndPaging
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_GroupModuleAccess_SelectSomeBySearchAndPaging]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_GroupModuleAccess_SelectSomeBySearchAndPaging]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_GroupModuleAccess_SelectSomeBySearchAndPaging]
	@portalKey INT = 0,
	@propertyKeyList VARCHAR(2000) = NULL,
	@groupKey INT = 0,
	@moduleKey INT = 0,
	@sortOrder varchar(250) = null,
	@pageSize int = 0,
	@pageNumber int = 0,
	@totalRecordCount INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DECLARE @sqlString nvarchar(4000)
DECLARE @parmDefinition nvarchar(1000)
DECLARE @startingRow int

IF (@pageNumber = 0)
	SET @pageNumber = 1

IF (@pageSize = 0)
	SET @pageSize = 10

IF (@pageNumber = 1)
	SET @startingRow = 1
ELSE
	SET @startingRow = ((@pageNumber - 1) * @pageSize) + 1

-- Add the primary key for better sorting results
IF (ISNULL(@sortOrder, '') = '')
	SET @sortOrder = '[GroupModuleAccess].[GroupModuleAccessKey]'
ELSE
	SET @sortOrder = @sortOrder + ', [GroupModuleAccess].[GroupModuleAccessKey]'

SET @parmDefinition = N' @startingRow INT, @pageSize INT, @portalKey INT, @groupKey INT, @moduleKey INT'

-- Select PortalKey & UnitKey
SELECT PortalKey, UnitKey
INTO #T
FROM [dbo].[Split_PropertyKeyList](@propertyKeyList, ',')

-- Get total records count
SELECT @totalRecordCount = COUNT(*)
FROM
	[GroupModuleAccess] 
	LEFT JOIN #T T ON [GroupModuleAccess].PortalKey = T.PortalKey 
WHERE ([GroupModuleAccess].[PortalKey] = @portalKey OR @portalKey IS NULL OR @portalKey = 0)
AND ([GroupModuleAccess].[GroupKey] = @groupKey OR @groupKey IS NULL OR @groupKey = 0)
AND ([GroupModuleAccess].[ModuleKey] = @moduleKey OR @moduleKey IS NULL OR @moduleKey = 0)
AND (T.PortalKey IS NOT NULL OR ISNULL(@propertyKeyList, '') = '') 

-- Determine records exist to retrieve
IF (@totalRecordCount > 0)
BEGIN

	SET @sqlString = N'SELECT * ' + 
		'FROM ( ' + 
		'	SELECT ROW_NUMBER() OVER ( ORDER BY ' + @sortOrder + ' ) AS RowNumber, ' + 
		'		[GroupModuleAccess].[GroupModuleAccessKey], ' +
		'		[GroupModuleAccess].[PortalKey], ' +
		'		[GroupModuleAccess].[GroupKey], ' +
		'		[GroupModuleAccess].[ModuleKey], ' +
		'		[GroupModuleAccess].[Access] ' +
		'	FROM [GroupModuleAccess]  ' + 
		'	LEFT JOIN #T T ON [GroupModuleAccess].PortalKey = T.PortalKey ' + 
		'	WHERE ([GroupModuleAccess].[PortalKey] = @portalKey OR @portalKey IS NULL OR @portalKey = 0) ' +
		'	AND ([GroupModuleAccess].[GroupKey] = @groupKey OR @groupKey IS NULL OR @groupKey = 0) ' +
		'	AND ([GroupModuleAccess].[ModuleKey] = @moduleKey OR @moduleKey IS NULL OR @moduleKey = 0) ' +
		'' 

	IF (ISNULL(@propertyKeyList, '') <> '')
		SET @sqlString = @sqlString + 'AND (T.PortalKey IS NOT NULL) ' 

	SET @sqlString = @sqlString + 
		') AS FilteredResult ' + 
		'WHERE RowNumber >= @startingRow ' + 
		'AND RowNumber < (@startingRow + @pageSize) ' + 
		'ORDER BY RowNumber ' 

	EXECUTE sp_executesql @sqlString, @parmDefinition, @startingRow, @pageSize, @portalKey, @groupKey, @moduleKey

	-- Get the Error Code for the statement just executed.
	SELECT @errorCode = @@ERROR
END

GO

-- *****************************************************

-- *****************************************************
-- * Table          : GroupModuleAccess
-- * Procedure Name : gensp_GroupModuleAccess_InsertOne
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_GroupModuleAccess_InsertOne]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_GroupModuleAccess_InsertOne]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_GroupModuleAccess_InsertOne]
	@portalKey INT,
	@groupKey INT,
	@moduleKey INT,
	@access INT,
	@groupModuleAccessKey INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON


INSERT INTO [GroupModuleAccess]
(
	[PortalKey],
	[GroupKey],
	[ModuleKey],
	[Access]
)
VALUES
(
	@portalKey,
	@groupKey,
	@moduleKey,
	@access
)
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR
-- Get the IDENTITY value for the row just inserted.
SELECT @groupModuleAccessKey = SCOPE_IDENTITY()

GO

-- *****************************************************

-- *****************************************************
-- * Table          : GroupModuleAccess
-- * Procedure Name : gensp_GroupModuleAccess_UpdateOneByGroupModuleAccessKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_GroupModuleAccess_UpdateOneByGroupModuleAccessKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_GroupModuleAccess_UpdateOneByGroupModuleAccessKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_GroupModuleAccess_UpdateOneByGroupModuleAccessKey]
	@groupModuleAccessKey INT,
	@portalKey INT,
	@groupKey INT,
	@moduleKey INT,
	@access INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

UPDATE [GroupModuleAccess]
SET
	[PortalKey] = @portalKey,
	[GroupKey] = @groupKey,
	[ModuleKey] = @moduleKey,
	[Access] = @access
WHERE [GroupModuleAccess].[GroupModuleAccessKey] = @groupModuleAccessKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : GroupModuleAccess
-- * Procedure Name : gensp_GroupModuleAccess_DeleteOneByGroupModuleAccessKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_GroupModuleAccess_DeleteOneByGroupModuleAccessKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_GroupModuleAccess_DeleteOneByGroupModuleAccessKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_GroupModuleAccess_DeleteOneByGroupModuleAccessKey]
	@groupModuleAccessKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DELETE FROM [GroupModuleAccess]
WHERE [GroupModuleAccess].[GroupModuleAccessKey] = @groupModuleAccessKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Insurance
-- * Procedure Name : gensp_Insurance_SelectOneByInsuranceKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Insurance_SelectOneByInsuranceKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Insurance_SelectOneByInsuranceKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Insurance_SelectOneByInsuranceKey]
	@insuranceKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON
SELECT
	[Insurance].[InsuranceKey], 
	[Insurance].[VendorKey], 
	[Insurance].[CompanyName], 
	[Insurance].[PolicyNumber], 
	[Insurance].[InsuranceAmount], 
	[Insurance].[AgentName], 
	[Insurance].[Email], 
	[Insurance].[Work], 
	[Insurance].[CellPhone], 
	[Insurance].[Fax], 
	[Insurance].[Address], 
	[Insurance].[Address2], 
	[Insurance].[City], 
	[Insurance].[State], 
	[Insurance].[Zip], 
	[Insurance].[StartDate], 
	[Insurance].[EndDate], 
	[Insurance].[RenewalDate], 
	[Insurance].[Status] 
FROM
	[Insurance] 
WHERE [Insurance].[InsuranceKey] = @insuranceKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Insurance
-- * Procedure Name : gensp_Insurance_SelectSomeBySearch
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Insurance_SelectSomeBySearch]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Insurance_SelectSomeBySearch]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Insurance_SelectSomeBySearch]
	@vendorKey INT = 0,
	@state VARCHAR(2) = '',
	@status INT = 0,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

SELECT
	[Insurance].[InsuranceKey], 
	[Insurance].[VendorKey], 
	[Insurance].[CompanyName], 
	[Insurance].[PolicyNumber], 
	[Insurance].[InsuranceAmount], 
	[Insurance].[AgentName], 
	[Insurance].[Email], 
	[Insurance].[Work], 
	[Insurance].[CellPhone], 
	[Insurance].[Fax], 
	[Insurance].[Address], 
	[Insurance].[Address2], 
	[Insurance].[City], 
	[Insurance].[State], 
	[Insurance].[Zip], 
	[Insurance].[StartDate], 
	[Insurance].[EndDate], 
	[Insurance].[RenewalDate], 
	[Insurance].[Status] 
FROM
	[Insurance] 
WHERE ([Insurance].[VendorKey] = @vendorKey OR @vendorKey IS NULL OR @vendorKey = 0)
AND ([Insurance].[State] LIKE @state OR @state IS NULL OR @state = '')
AND ([Insurance].[Status] & ISNULL(@status, 7) = [Insurance].[Status])

-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Insurance
-- * Procedure Name : gensp_Insurance_SelectSomeBySearchAndPaging
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Insurance_SelectSomeBySearchAndPaging]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Insurance_SelectSomeBySearchAndPaging]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Insurance_SelectSomeBySearchAndPaging]
	@vendorKey INT = 0,
	@state VARCHAR(2) = '',
	@status INT = 0,
	@sortOrder varchar(250) = null,
	@pageSize int = 0,
	@pageNumber int = 0,
	@totalRecordCount INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DECLARE @sqlString nvarchar(4000)
DECLARE @parmDefinition nvarchar(1000)
DECLARE @startingRow int

IF (@pageNumber = 0)
	SET @pageNumber = 1

IF (@pageSize = 0)
	SET @pageSize = 10

IF (@pageNumber = 1)
	SET @startingRow = 1
ELSE
	SET @startingRow = ((@pageNumber - 1) * @pageSize) + 1

-- Add the primary key for better sorting results
IF (ISNULL(@sortOrder, '') = '')
	SET @sortOrder = '[Insurance].[InsuranceKey]'
ELSE
	SET @sortOrder = @sortOrder + ', [Insurance].[InsuranceKey]'

SET @parmDefinition = N' @startingRow INT, @pageSize INT, @vendorKey INT, @state VARCHAR(2), @status INT'

-- Get total records count
SELECT @totalRecordCount = COUNT(*)
FROM
	[Insurance] 
WHERE ([Insurance].[VendorKey] = @vendorKey OR @vendorKey IS NULL OR @vendorKey = 0)
AND ([Insurance].[State] LIKE @state OR @state IS NULL OR @state = '')
AND ([Insurance].[Status] & ISNULL(@status, 7) = [Insurance].[Status])

-- Determine records exist to retrieve
IF (@totalRecordCount > 0)
BEGIN

	SET @sqlString = N'SELECT * ' + 
		'FROM ( ' + 
		'	SELECT ROW_NUMBER() OVER ( ORDER BY ' + @sortOrder + ' ) AS RowNumber, ' + 
		'		[Insurance].[InsuranceKey], ' +
		'		[Insurance].[VendorKey], ' +
		'		[Insurance].[CompanyName], ' +
		'		[Insurance].[PolicyNumber], ' +
		'		[Insurance].[InsuranceAmount], ' +
		'		[Insurance].[AgentName], ' +
		'		[Insurance].[Email], ' +
		'		[Insurance].[Work], ' +
		'		[Insurance].[CellPhone], ' +
		'		[Insurance].[Fax], ' +
		'		[Insurance].[Address], ' +
		'		[Insurance].[Address2], ' +
		'		[Insurance].[City], ' +
		'		[Insurance].[State], ' +
		'		[Insurance].[Zip], ' +
		'		[Insurance].[StartDate], ' +
		'		[Insurance].[EndDate], ' +
		'		[Insurance].[RenewalDate], ' +
		'		[Insurance].[Status] ' +
		'	FROM [Insurance]  ' + 
		'	WHERE ([Insurance].[VendorKey] = @vendorKey OR @vendorKey IS NULL OR @vendorKey = 0) ' +
		'	AND ([Insurance].[State] LIKE @state OR @state IS NULL OR @state = '''') ' +
		'	AND ([Insurance].[Status] & ISNULL(@status, 7) = [Insurance].[Status]) ' +
		') AS FilteredResult ' + 
		'WHERE RowNumber >= @startingRow ' + 
		'AND RowNumber < (@startingRow + @pageSize) ' + 
		'ORDER BY RowNumber ' 

	EXECUTE sp_executesql @sqlString, @parmDefinition, @startingRow, @pageSize, @vendorKey, @state, @status

	-- Get the Error Code for the statement just executed.
	SELECT @errorCode = @@ERROR
END

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Insurance
-- * Procedure Name : gensp_Insurance_InsertOne
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Insurance_InsertOne]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Insurance_InsertOne]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Insurance_InsertOne]
	@vendorKey INT,
	@companyName VARCHAR(150),
	@policyNumber VARCHAR(150),
	@insuranceAmount MONEY,
	@agentName VARCHAR(150),
	@email VARCHAR(150),
	@work VARCHAR(150),
	@cellPhone VARCHAR(150),
	@fax VARCHAR(150),
	@address VARCHAR(100),
	@address2 VARCHAR(100),
	@city VARCHAR(50),
	@state VARCHAR(2),
	@zip VARCHAR(11),
	@startDate DATETIME,
	@endDate DATETIME,
	@renewalDate DATETIME,
	@status INT,
	@insuranceKey INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON


INSERT INTO [Insurance]
(
	[VendorKey],
	[CompanyName],
	[PolicyNumber],
	[InsuranceAmount],
	[AgentName],
	[Email],
	[Work],
	[CellPhone],
	[Fax],
	[Address],
	[Address2],
	[City],
	[State],
	[Zip],
	[StartDate],
	[EndDate],
	[RenewalDate],
	[Status]
)
VALUES
(
	@vendorKey,
	@companyName,
	@policyNumber,
	@insuranceAmount,
	@agentName,
	@email,
	@work,
	@cellPhone,
	@fax,
	@address,
	@address2,
	@city,
	@state,
	@zip,
	@startDate,
	@endDate,
	@renewalDate,
	@status
)
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR
-- Get the IDENTITY value for the row just inserted.
SELECT @insuranceKey = SCOPE_IDENTITY()

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Insurance
-- * Procedure Name : gensp_Insurance_UpdateOneByInsuranceKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Insurance_UpdateOneByInsuranceKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Insurance_UpdateOneByInsuranceKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Insurance_UpdateOneByInsuranceKey]
	@insuranceKey INT,
	@vendorKey INT,
	@companyName VARCHAR(150),
	@policyNumber VARCHAR(150),
	@insuranceAmount MONEY,
	@agentName VARCHAR(150),
	@email VARCHAR(150),
	@work VARCHAR(150),
	@cellPhone VARCHAR(150),
	@fax VARCHAR(150),
	@address VARCHAR(100),
	@address2 VARCHAR(100),
	@city VARCHAR(50),
	@state VARCHAR(2),
	@zip VARCHAR(11),
	@startDate DATETIME,
	@endDate DATETIME,
	@renewalDate DATETIME,
	@status INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

UPDATE [Insurance]
SET
	[VendorKey] = @vendorKey,
	[CompanyName] = @companyName,
	[PolicyNumber] = @policyNumber,
	[InsuranceAmount] = @insuranceAmount,
	[AgentName] = @agentName,
	[Email] = @email,
	[Work] = @work,
	[CellPhone] = @cellPhone,
	[Fax] = @fax,
	[Address] = @address,
	[Address2] = @address2,
	[City] = @city,
	[State] = @state,
	[Zip] = @zip,
	[StartDate] = @startDate,
	[EndDate] = @endDate,
	[RenewalDate] = @renewalDate,
	[Status] = @status
WHERE [Insurance].[InsuranceKey] = @insuranceKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Insurance
-- * Procedure Name : gensp_Insurance_DeleteOneByInsuranceKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Insurance_DeleteOneByInsuranceKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Insurance_DeleteOneByInsuranceKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Insurance_DeleteOneByInsuranceKey]
	@insuranceKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DELETE FROM [Insurance]
WHERE [Insurance].[InsuranceKey] = @insuranceKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Invoice
-- * Procedure Name : gensp_Invoice_SelectOneByInvoiceKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Invoice_SelectOneByInvoiceKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Invoice_SelectOneByInvoiceKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Invoice_SelectOneByInvoiceKey]
	@invoiceKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON
SELECT
	[Invoice].[InvoiceKey], 
	[Invoice].[VendorKey], 
	[Invoice].[ReferenceNumber], 
	[Invoice].[TransactionDate], 
	[Invoice].[DueDate], 
	[Invoice].[Amount], 
	[Invoice].[Balance], 
	[Invoice].[LastModificationTime], 
	[Invoice].[Status] 
FROM
	[Invoice] 
WHERE [Invoice].[InvoiceKey] = @invoiceKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Invoice
-- * Procedure Name : gensp_Invoice_SelectSomeBySearch
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Invoice_SelectSomeBySearch]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Invoice_SelectSomeBySearch]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Invoice_SelectSomeBySearch]
	@vendorKey INT = 0,
	@status INT = 0,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

SELECT
	[Invoice].[InvoiceKey], 
	[Invoice].[VendorKey], 
	[Invoice].[ReferenceNumber], 
	[Invoice].[TransactionDate], 
	[Invoice].[DueDate], 
	[Invoice].[Amount], 
	[Invoice].[Balance], 
	[Invoice].[LastModificationTime], 
	[Invoice].[Status] 
FROM
	[Invoice] 
WHERE ([Invoice].[VendorKey] = @vendorKey OR @vendorKey IS NULL OR @vendorKey = 0)
AND ([Invoice].[Status] & ISNULL(@status, 7) = [Invoice].[Status])

-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Invoice
-- * Procedure Name : gensp_Invoice_SelectSomeBySearchAndPaging
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Invoice_SelectSomeBySearchAndPaging]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Invoice_SelectSomeBySearchAndPaging]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Invoice_SelectSomeBySearchAndPaging]
	@vendorKey INT = 0,
	@status INT = 0,
	@sortOrder varchar(250) = null,
	@pageSize int = 0,
	@pageNumber int = 0,
	@totalRecordCount INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DECLARE @sqlString nvarchar(4000)
DECLARE @parmDefinition nvarchar(1000)
DECLARE @startingRow int

IF (@pageNumber = 0)
	SET @pageNumber = 1

IF (@pageSize = 0)
	SET @pageSize = 10

IF (@pageNumber = 1)
	SET @startingRow = 1
ELSE
	SET @startingRow = ((@pageNumber - 1) * @pageSize) + 1

-- Add the primary key for better sorting results
IF (ISNULL(@sortOrder, '') = '')
	SET @sortOrder = '[Invoice].[InvoiceKey]'
ELSE
	SET @sortOrder = @sortOrder + ', [Invoice].[InvoiceKey]'

SET @parmDefinition = N' @startingRow INT, @pageSize INT, @vendorKey INT, @status INT'

-- Get total records count
SELECT @totalRecordCount = COUNT(*)
FROM
	[Invoice] 
WHERE ([Invoice].[VendorKey] = @vendorKey OR @vendorKey IS NULL OR @vendorKey = 0)
AND ([Invoice].[Status] & ISNULL(@status, 7) = [Invoice].[Status])

-- Determine records exist to retrieve
IF (@totalRecordCount > 0)
BEGIN

	SET @sqlString = N'SELECT * ' + 
		'FROM ( ' + 
		'	SELECT ROW_NUMBER() OVER ( ORDER BY ' + @sortOrder + ' ) AS RowNumber, ' + 
		'		[Invoice].[InvoiceKey], ' +
		'		[Invoice].[VendorKey], ' +
		'		[Invoice].[ReferenceNumber], ' +
		'		[Invoice].[TransactionDate], ' +
		'		[Invoice].[DueDate], ' +
		'		[Invoice].[Amount], ' +
		'		[Invoice].[Balance], ' +
		'		[Invoice].[LastModificationTime], ' +
		'		[Invoice].[Status] ' +
		'	FROM [Invoice]  ' + 
		'	WHERE ([Invoice].[VendorKey] = @vendorKey OR @vendorKey IS NULL OR @vendorKey = 0) ' +
		'	AND ([Invoice].[Status] & ISNULL(@status, 7) = [Invoice].[Status]) ' +
		') AS FilteredResult ' + 
		'WHERE RowNumber >= @startingRow ' + 
		'AND RowNumber < (@startingRow + @pageSize) ' + 
		'ORDER BY RowNumber ' 

	EXECUTE sp_executesql @sqlString, @parmDefinition, @startingRow, @pageSize, @vendorKey, @status

	-- Get the Error Code for the statement just executed.
	SELECT @errorCode = @@ERROR
END

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Invoice
-- * Procedure Name : gensp_Invoice_InsertOne
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Invoice_InsertOne]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Invoice_InsertOne]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Invoice_InsertOne]
	@vendorKey INT,
	@referenceNumber VARCHAR(150),
	@transactionDate DATETIME,
	@dueDate DATETIME,
	@amount MONEY,
	@balance MONEY,
	@lastModificationTime DATETIME,
	@status INT,
	@invoiceKey INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON


INSERT INTO [Invoice]
(
	[VendorKey],
	[ReferenceNumber],
	[TransactionDate],
	[DueDate],
	[Amount],
	[Balance],
	[LastModificationTime],
	[Status]
)
VALUES
(
	@vendorKey,
	@referenceNumber,
	@transactionDate,
	@dueDate,
	@amount,
	@balance,
	@lastModificationTime,
	@status
)
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR
-- Get the IDENTITY value for the row just inserted.
SELECT @invoiceKey = SCOPE_IDENTITY()

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Invoice
-- * Procedure Name : gensp_Invoice_UpdateOneByInvoiceKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Invoice_UpdateOneByInvoiceKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Invoice_UpdateOneByInvoiceKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Invoice_UpdateOneByInvoiceKey]
	@invoiceKey INT,
	@vendorKey INT,
	@referenceNumber VARCHAR(150),
	@transactionDate DATETIME,
	@dueDate DATETIME,
	@amount MONEY,
	@balance MONEY,
	@lastModificationTime DATETIME,
	@status INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

UPDATE [Invoice]
SET
	[VendorKey] = @vendorKey,
	[ReferenceNumber] = @referenceNumber,
	[TransactionDate] = @transactionDate,
	[DueDate] = @dueDate,
	[Amount] = @amount,
	[Balance] = @balance,
	[LastModificationTime] = @lastModificationTime,
	[Status] = @status
WHERE [Invoice].[InvoiceKey] = @invoiceKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Invoice
-- * Procedure Name : gensp_Invoice_DeleteOneByInvoiceKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Invoice_DeleteOneByInvoiceKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Invoice_DeleteOneByInvoiceKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Invoice_DeleteOneByInvoiceKey]
	@invoiceKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DELETE FROM [Invoice]
WHERE [Invoice].[InvoiceKey] = @invoiceKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : InvoiceLine
-- * Procedure Name : gensp_InvoiceLine_SelectOneByInvoiceLineKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_InvoiceLine_SelectOneByInvoiceLineKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_InvoiceLine_SelectOneByInvoiceLineKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_InvoiceLine_SelectOneByInvoiceLineKey]
	@invoiceLineKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON
SELECT
	[InvoiceLine].[InvoiceLineKey], 
	[InvoiceLine].[InvoiceKey], 
	[InvoiceLine].[Quantity], 
	[InvoiceLine].[Rate], 
	[InvoiceLine].[Amount], 
	[InvoiceLine].[Description], 
	[InvoiceLine].[SortOrder] 
FROM
	[InvoiceLine] 
WHERE [InvoiceLine].[InvoiceLineKey] = @invoiceLineKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : InvoiceLine
-- * Procedure Name : gensp_InvoiceLine_SelectSomeBySearch
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_InvoiceLine_SelectSomeBySearch]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_InvoiceLine_SelectSomeBySearch]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_InvoiceLine_SelectSomeBySearch]
	@invoiceKey INT = 0,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

SELECT
	[InvoiceLine].[InvoiceLineKey], 
	[InvoiceLine].[InvoiceKey], 
	[InvoiceLine].[Quantity], 
	[InvoiceLine].[Rate], 
	[InvoiceLine].[Amount], 
	[InvoiceLine].[Description], 
	[InvoiceLine].[SortOrder] 
FROM
	[InvoiceLine] 
WHERE ([InvoiceLine].[InvoiceKey] = @invoiceKey OR @invoiceKey IS NULL OR @invoiceKey = 0)

-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : InvoiceLine
-- * Procedure Name : gensp_InvoiceLine_SelectSomeBySearchAndPaging
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_InvoiceLine_SelectSomeBySearchAndPaging]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_InvoiceLine_SelectSomeBySearchAndPaging]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_InvoiceLine_SelectSomeBySearchAndPaging]
	@invoiceKey INT = 0,
	@sortOrder varchar(250) = null,
	@pageSize int = 0,
	@pageNumber int = 0,
	@totalRecordCount INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DECLARE @sqlString nvarchar(4000)
DECLARE @parmDefinition nvarchar(1000)
DECLARE @startingRow int

IF (@pageNumber = 0)
	SET @pageNumber = 1

IF (@pageSize = 0)
	SET @pageSize = 10

IF (@pageNumber = 1)
	SET @startingRow = 1
ELSE
	SET @startingRow = ((@pageNumber - 1) * @pageSize) + 1

-- Add the primary key for better sorting results
IF (ISNULL(@sortOrder, '') = '')
	SET @sortOrder = '[InvoiceLine].[InvoiceLineKey]'
ELSE
	SET @sortOrder = @sortOrder + ', [InvoiceLine].[InvoiceLineKey]'

SET @parmDefinition = N' @startingRow INT, @pageSize INT, @invoiceKey INT'

-- Get total records count
SELECT @totalRecordCount = COUNT(*)
FROM
	[InvoiceLine] 
WHERE ([InvoiceLine].[InvoiceKey] = @invoiceKey OR @invoiceKey IS NULL OR @invoiceKey = 0)

-- Determine records exist to retrieve
IF (@totalRecordCount > 0)
BEGIN

	SET @sqlString = N'SELECT * ' + 
		'FROM ( ' + 
		'	SELECT ROW_NUMBER() OVER ( ORDER BY ' + @sortOrder + ' ) AS RowNumber, ' + 
		'		[InvoiceLine].[InvoiceLineKey], ' +
		'		[InvoiceLine].[InvoiceKey], ' +
		'		[InvoiceLine].[Quantity], ' +
		'		[InvoiceLine].[Rate], ' +
		'		[InvoiceLine].[Amount], ' +
		'		[InvoiceLine].[Description], ' +
		'		[InvoiceLine].[SortOrder] ' +
		'	FROM [InvoiceLine]  ' + 
		'	WHERE ([InvoiceLine].[InvoiceKey] = @invoiceKey OR @invoiceKey IS NULL OR @invoiceKey = 0) ' +
		') AS FilteredResult ' + 
		'WHERE RowNumber >= @startingRow ' + 
		'AND RowNumber < (@startingRow + @pageSize) ' + 
		'ORDER BY RowNumber ' 

	EXECUTE sp_executesql @sqlString, @parmDefinition, @startingRow, @pageSize, @invoiceKey

	-- Get the Error Code for the statement just executed.
	SELECT @errorCode = @@ERROR
END

GO

-- *****************************************************

-- *****************************************************
-- * Table          : InvoiceLine
-- * Procedure Name : gensp_InvoiceLine_InsertOne
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_InvoiceLine_InsertOne]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_InvoiceLine_InsertOne]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_InvoiceLine_InsertOne]
	@invoiceKey INT,
	@quantity INT,
	@rate MONEY,
	@amount MONEY,
	@description VARCHAR(5000),
	@sortOrder FLOAT(53),
	@invoiceLineKey INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON


INSERT INTO [InvoiceLine]
(
	[InvoiceKey],
	[Quantity],
	[Rate],
	[Amount],
	[Description],
	[SortOrder]
)
VALUES
(
	@invoiceKey,
	@quantity,
	@rate,
	@amount,
	@description,
	@sortOrder
)
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR
-- Get the IDENTITY value for the row just inserted.
SELECT @invoiceLineKey = SCOPE_IDENTITY()

GO

-- *****************************************************

-- *****************************************************
-- * Table          : InvoiceLine
-- * Procedure Name : gensp_InvoiceLine_UpdateOneByInvoiceLineKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_InvoiceLine_UpdateOneByInvoiceLineKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_InvoiceLine_UpdateOneByInvoiceLineKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_InvoiceLine_UpdateOneByInvoiceLineKey]
	@invoiceLineKey INT,
	@invoiceKey INT,
	@quantity INT,
	@rate MONEY,
	@amount MONEY,
	@description VARCHAR(5000),
	@sortOrder FLOAT(53),
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

UPDATE [InvoiceLine]
SET
	[InvoiceKey] = @invoiceKey,
	[Quantity] = @quantity,
	[Rate] = @rate,
	[Amount] = @amount,
	[Description] = @description,
	[SortOrder] = @sortOrder
WHERE [InvoiceLine].[InvoiceLineKey] = @invoiceLineKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : InvoiceLine
-- * Procedure Name : gensp_InvoiceLine_DeleteOneByInvoiceLineKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_InvoiceLine_DeleteOneByInvoiceLineKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_InvoiceLine_DeleteOneByInvoiceLineKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_InvoiceLine_DeleteOneByInvoiceLineKey]
	@invoiceLineKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DELETE FROM [InvoiceLine]
WHERE [InvoiceLine].[InvoiceLineKey] = @invoiceLineKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : LoginHistory
-- * Procedure Name : gensp_LoginHistory_SelectOneByLoginHistoryKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_LoginHistory_SelectOneByLoginHistoryKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_LoginHistory_SelectOneByLoginHistoryKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_LoginHistory_SelectOneByLoginHistoryKey]
	@loginHistoryKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON
SELECT
	[LoginHistory].[LoginHistoryKey], 
	[LoginHistory].[UserKey], 
	[LoginHistory].[DateAdded] 
FROM
	[LoginHistory] 
WHERE [LoginHistory].[LoginHistoryKey] = @loginHistoryKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : LoginHistory
-- * Procedure Name : gensp_LoginHistory_SelectSomeBySearch
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_LoginHistory_SelectSomeBySearch]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_LoginHistory_SelectSomeBySearch]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_LoginHistory_SelectSomeBySearch]
	@userKey INT = 0,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

SELECT
	[LoginHistory].[LoginHistoryKey], 
	[LoginHistory].[UserKey], 
	[LoginHistory].[DateAdded] 
FROM
	[LoginHistory] 
WHERE ([LoginHistory].[UserKey] = @userKey OR @userKey IS NULL OR @userKey = 0)

-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : LoginHistory
-- * Procedure Name : gensp_LoginHistory_SelectSomeBySearchAndPaging
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_LoginHistory_SelectSomeBySearchAndPaging]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_LoginHistory_SelectSomeBySearchAndPaging]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_LoginHistory_SelectSomeBySearchAndPaging]
	@userKey INT = 0,
	@sortOrder varchar(250) = null,
	@pageSize int = 0,
	@pageNumber int = 0,
	@totalRecordCount INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DECLARE @sqlString nvarchar(4000)
DECLARE @parmDefinition nvarchar(1000)
DECLARE @startingRow int

IF (@pageNumber = 0)
	SET @pageNumber = 1

IF (@pageSize = 0)
	SET @pageSize = 10

IF (@pageNumber = 1)
	SET @startingRow = 1
ELSE
	SET @startingRow = ((@pageNumber - 1) * @pageSize) + 1

-- Add the primary key for better sorting results
IF (ISNULL(@sortOrder, '') = '')
	SET @sortOrder = '[LoginHistory].[LoginHistoryKey]'
ELSE
	SET @sortOrder = @sortOrder + ', [LoginHistory].[LoginHistoryKey]'

SET @parmDefinition = N' @startingRow INT, @pageSize INT, @userKey INT'

-- Get total records count
SELECT @totalRecordCount = COUNT(*)
FROM
	[LoginHistory] 
WHERE ([LoginHistory].[UserKey] = @userKey OR @userKey IS NULL OR @userKey = 0)

-- Determine records exist to retrieve
IF (@totalRecordCount > 0)
BEGIN

	SET @sqlString = N'SELECT * ' + 
		'FROM ( ' + 
		'	SELECT ROW_NUMBER() OVER ( ORDER BY ' + @sortOrder + ' ) AS RowNumber, ' + 
		'		[LoginHistory].[LoginHistoryKey], ' +
		'		[LoginHistory].[UserKey], ' +
		'		[LoginHistory].[DateAdded] ' +
		'	FROM [LoginHistory]  ' + 
		'	WHERE ([LoginHistory].[UserKey] = @userKey OR @userKey IS NULL OR @userKey = 0) ' +
		') AS FilteredResult ' + 
		'WHERE RowNumber >= @startingRow ' + 
		'AND RowNumber < (@startingRow + @pageSize) ' + 
		'ORDER BY RowNumber ' 

	EXECUTE sp_executesql @sqlString, @parmDefinition, @startingRow, @pageSize, @userKey

	-- Get the Error Code for the statement just executed.
	SELECT @errorCode = @@ERROR
END

GO

-- *****************************************************

-- *****************************************************
-- * Table          : LoginHistory
-- * Procedure Name : gensp_LoginHistory_InsertOne
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_LoginHistory_InsertOne]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_LoginHistory_InsertOne]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_LoginHistory_InsertOne]
	@userKey INT,
	@dateAdded DATETIME,
	@loginHistoryKey INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON


INSERT INTO [LoginHistory]
(
	[UserKey],
	[DateAdded]
)
VALUES
(
	@userKey,
	@dateAdded
)
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR
-- Get the IDENTITY value for the row just inserted.
SELECT @loginHistoryKey = SCOPE_IDENTITY()

GO

-- *****************************************************

-- *****************************************************
-- * Table          : LoginHistory
-- * Procedure Name : gensp_LoginHistory_UpdateOneByLoginHistoryKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_LoginHistory_UpdateOneByLoginHistoryKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_LoginHistory_UpdateOneByLoginHistoryKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_LoginHistory_UpdateOneByLoginHistoryKey]
	@loginHistoryKey INT,
	@userKey INT,
	@dateAdded DATETIME,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

UPDATE [LoginHistory]
SET
	[UserKey] = @userKey,
	[DateAdded] = @dateAdded
WHERE [LoginHistory].[LoginHistoryKey] = @loginHistoryKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : LoginHistory
-- * Procedure Name : gensp_LoginHistory_DeleteOneByLoginHistoryKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_LoginHistory_DeleteOneByLoginHistoryKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_LoginHistory_DeleteOneByLoginHistoryKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_LoginHistory_DeleteOneByLoginHistoryKey]
	@loginHistoryKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DELETE FROM [LoginHistory]
WHERE [LoginHistory].[LoginHistoryKey] = @loginHistoryKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : LookUp
-- * Procedure Name : gensp_LookUp_SelectOneByLookUpKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_LookUp_SelectOneByLookUpKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_LookUp_SelectOneByLookUpKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_LookUp_SelectOneByLookUpKey]
	@lookUpKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON
SELECT
	[LookUp].[LookUpKey], 
	[LookUp].[LookUpTypeKey], 
	[LookUp].[Title], 
	[LookUp].[Value], 
	[LookUp].[SortOrder] 
FROM
	[LookUp] 
WHERE [LookUp].[LookUpKey] = @lookUpKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : LookUp
-- * Procedure Name : gensp_LookUp_SelectSomeBySearch
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_LookUp_SelectSomeBySearch]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_LookUp_SelectSomeBySearch]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_LookUp_SelectSomeBySearch]
	@lookUpTypeKey INT = 0,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

SELECT
	[LookUp].[LookUpKey], 
	[LookUp].[LookUpTypeKey], 
	[LookUp].[Title], 
	[LookUp].[Value], 
	[LookUp].[SortOrder] 
FROM
	[LookUp] 
WHERE ([LookUp].[LookUpTypeKey] = @lookUpTypeKey OR @lookUpTypeKey IS NULL OR @lookUpTypeKey = 0)

-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : LookUp
-- * Procedure Name : gensp_LookUp_SelectSomeBySearchAndPaging
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_LookUp_SelectSomeBySearchAndPaging]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_LookUp_SelectSomeBySearchAndPaging]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_LookUp_SelectSomeBySearchAndPaging]
	@lookUpTypeKey INT = 0,
	@sortOrder varchar(250) = null,
	@pageSize int = 0,
	@pageNumber int = 0,
	@totalRecordCount INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DECLARE @sqlString nvarchar(4000)
DECLARE @parmDefinition nvarchar(1000)
DECLARE @startingRow int

IF (@pageNumber = 0)
	SET @pageNumber = 1

IF (@pageSize = 0)
	SET @pageSize = 10

IF (@pageNumber = 1)
	SET @startingRow = 1
ELSE
	SET @startingRow = ((@pageNumber - 1) * @pageSize) + 1

-- Add the primary key for better sorting results
IF (ISNULL(@sortOrder, '') = '')
	SET @sortOrder = '[LookUp].[LookUpKey]'
ELSE
	SET @sortOrder = @sortOrder + ', [LookUp].[LookUpKey]'

SET @parmDefinition = N' @startingRow INT, @pageSize INT, @lookUpTypeKey INT'

-- Get total records count
SELECT @totalRecordCount = COUNT(*)
FROM
	[LookUp] 
WHERE ([LookUp].[LookUpTypeKey] = @lookUpTypeKey OR @lookUpTypeKey IS NULL OR @lookUpTypeKey = 0)

-- Determine records exist to retrieve
IF (@totalRecordCount > 0)
BEGIN

	SET @sqlString = N'SELECT * ' + 
		'FROM ( ' + 
		'	SELECT ROW_NUMBER() OVER ( ORDER BY ' + @sortOrder + ' ) AS RowNumber, ' + 
		'		[LookUp].[LookUpKey], ' +
		'		[LookUp].[LookUpTypeKey], ' +
		'		[LookUp].[Title], ' +
		'		[LookUp].[Value], ' +
		'		[LookUp].[SortOrder] ' +
		'	FROM [LookUp]  ' + 
		'	WHERE ([LookUp].[LookUpTypeKey] = @lookUpTypeKey OR @lookUpTypeKey IS NULL OR @lookUpTypeKey = 0) ' +
		') AS FilteredResult ' + 
		'WHERE RowNumber >= @startingRow ' + 
		'AND RowNumber < (@startingRow + @pageSize) ' + 
		'ORDER BY RowNumber ' 

	EXECUTE sp_executesql @sqlString, @parmDefinition, @startingRow, @pageSize, @lookUpTypeKey

	-- Get the Error Code for the statement just executed.
	SELECT @errorCode = @@ERROR
END

GO

-- *****************************************************

-- *****************************************************
-- * Table          : LookUp
-- * Procedure Name : gensp_LookUp_InsertOne
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_LookUp_InsertOne]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_LookUp_InsertOne]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_LookUp_InsertOne]
	@lookUpKey INT,
	@lookUpTypeKey INT,
	@title VARCHAR(150),
	@value INT,
	@sortOrder FLOAT(53),
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON


INSERT INTO [LookUp]
(
	[LookUpKey],
	[LookUpTypeKey],
	[Title],
	[Value],
	[SortOrder]
)
VALUES
(
	@lookUpKey,
	@lookUpTypeKey,
	@title,
	@value,
	@sortOrder
)
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : LookUp
-- * Procedure Name : gensp_LookUp_UpdateOneByLookUpKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_LookUp_UpdateOneByLookUpKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_LookUp_UpdateOneByLookUpKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_LookUp_UpdateOneByLookUpKey]
	@lookUpKey INT,
	@lookUpTypeKey INT,
	@title VARCHAR(150),
	@value INT,
	@sortOrder FLOAT(53),
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

UPDATE [LookUp]
SET
	[LookUpTypeKey] = @lookUpTypeKey,
	[Title] = @title,
	[Value] = @value,
	[SortOrder] = @sortOrder
WHERE [LookUp].[LookUpKey] = @lookUpKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : LookUp
-- * Procedure Name : gensp_LookUp_DeleteOneByLookUpKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_LookUp_DeleteOneByLookUpKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_LookUp_DeleteOneByLookUpKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_LookUp_DeleteOneByLookUpKey]
	@lookUpKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DELETE FROM [LookUp]
WHERE [LookUp].[LookUpKey] = @lookUpKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : LookUpType
-- * Procedure Name : gensp_LookUpType_SelectOneByLookUpTypeKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_LookUpType_SelectOneByLookUpTypeKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_LookUpType_SelectOneByLookUpTypeKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_LookUpType_SelectOneByLookUpTypeKey]
	@lookUpTypeKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON
SELECT
	[LookUpType].[LookUpTypeKey], 
	[LookUpType].[Title] 
FROM
	[LookUpType] 
WHERE [LookUpType].[LookUpTypeKey] = @lookUpTypeKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : LookUpType
-- * Procedure Name : gensp_LookUpType_SelectSomeBySearch
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_LookUpType_SelectSomeBySearch]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_LookUpType_SelectSomeBySearch]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_LookUpType_SelectSomeBySearch]
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

SELECT
	[LookUpType].[LookUpTypeKey], 
	[LookUpType].[Title] 
FROM
	[LookUpType] 

-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : LookUpType
-- * Procedure Name : gensp_LookUpType_SelectSomeBySearchAndPaging
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_LookUpType_SelectSomeBySearchAndPaging]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_LookUpType_SelectSomeBySearchAndPaging]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_LookUpType_SelectSomeBySearchAndPaging]
	@sortOrder varchar(250) = null,
	@pageSize int = 0,
	@pageNumber int = 0,
	@totalRecordCount INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DECLARE @sqlString nvarchar(4000)
DECLARE @parmDefinition nvarchar(1000)
DECLARE @startingRow int

IF (@pageNumber = 0)
	SET @pageNumber = 1

IF (@pageSize = 0)
	SET @pageSize = 10

IF (@pageNumber = 1)
	SET @startingRow = 1
ELSE
	SET @startingRow = ((@pageNumber - 1) * @pageSize) + 1

-- Add the primary key for better sorting results
IF (ISNULL(@sortOrder, '') = '')
	SET @sortOrder = '[LookUpType].[LookUpTypeKey]'
ELSE
	SET @sortOrder = @sortOrder + ', [LookUpType].[LookUpTypeKey]'

SET @parmDefinition = N' @startingRow INT, @pageSize INT'

-- Get total records count
SELECT @totalRecordCount = COUNT(*)
FROM
	[LookUpType] 

-- Determine records exist to retrieve
IF (@totalRecordCount > 0)
BEGIN

	SET @sqlString = N'SELECT * ' + 
		'FROM ( ' + 
		'	SELECT ROW_NUMBER() OVER ( ORDER BY ' + @sortOrder + ' ) AS RowNumber, ' + 
		'		[LookUpType].[LookUpTypeKey], ' +
		'		[LookUpType].[Title] ' +
		'	FROM [LookUpType]  ' + 
		') AS FilteredResult ' + 
		'WHERE RowNumber >= @startingRow ' + 
		'AND RowNumber < (@startingRow + @pageSize) ' + 
		'ORDER BY RowNumber ' 

	EXECUTE sp_executesql @sqlString, @parmDefinition, @startingRow, @pageSize

	-- Get the Error Code for the statement just executed.
	SELECT @errorCode = @@ERROR
END

GO

-- *****************************************************

-- *****************************************************
-- * Table          : LookUpType
-- * Procedure Name : gensp_LookUpType_InsertOne
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_LookUpType_InsertOne]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_LookUpType_InsertOne]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_LookUpType_InsertOne]
	@title VARCHAR(150),
	@lookUpTypeKey INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON


INSERT INTO [LookUpType]
(
	[Title]
)
VALUES
(
	@title
)
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR
-- Get the IDENTITY value for the row just inserted.
SELECT @lookUpTypeKey = SCOPE_IDENTITY()

GO

-- *****************************************************

-- *****************************************************
-- * Table          : LookUpType
-- * Procedure Name : gensp_LookUpType_UpdateOneByLookUpTypeKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_LookUpType_UpdateOneByLookUpTypeKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_LookUpType_UpdateOneByLookUpTypeKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_LookUpType_UpdateOneByLookUpTypeKey]
	@lookUpTypeKey INT,
	@title VARCHAR(150),
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

UPDATE [LookUpType]
SET
	[Title] = @title
WHERE [LookUpType].[LookUpTypeKey] = @lookUpTypeKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : LookUpType
-- * Procedure Name : gensp_LookUpType_DeleteOneByLookUpTypeKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_LookUpType_DeleteOneByLookUpTypeKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_LookUpType_DeleteOneByLookUpTypeKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_LookUpType_DeleteOneByLookUpTypeKey]
	@lookUpTypeKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DELETE FROM [LookUpType]
WHERE [LookUpType].[LookUpTypeKey] = @lookUpTypeKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Membership
-- * Procedure Name : gensp_Membership_SelectOneByMembershipKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Membership_SelectOneByMembershipKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Membership_SelectOneByMembershipKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Membership_SelectOneByMembershipKey]
	@membershipKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON
SELECT
	[Membership].[MembershipKey], 
	[Membership].[VendorKey], 
	[Membership].[StartDate], 
	[Membership].[EndDate], 
	[Membership].[RenewalDate], 
	[Membership].[AutomaticRenewal], 
	[Membership].[LastModificationTime] 
FROM
	[Membership] 
WHERE [Membership].[MembershipKey] = @membershipKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Membership
-- * Procedure Name : gensp_Membership_SelectSomeBySearch
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Membership_SelectSomeBySearch]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Membership_SelectSomeBySearch]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Membership_SelectSomeBySearch]
	@vendorKey INT = 0,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

SELECT
	[Membership].[MembershipKey], 
	[Membership].[VendorKey], 
	[Membership].[StartDate], 
	[Membership].[EndDate], 
	[Membership].[RenewalDate], 
	[Membership].[AutomaticRenewal], 
	[Membership].[LastModificationTime] 
FROM
	[Membership] 
WHERE ([Membership].[VendorKey] = @vendorKey OR @vendorKey IS NULL OR @vendorKey = 0)

-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Membership
-- * Procedure Name : gensp_Membership_SelectSomeBySearchAndPaging
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Membership_SelectSomeBySearchAndPaging]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Membership_SelectSomeBySearchAndPaging]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Membership_SelectSomeBySearchAndPaging]
	@vendorKey INT = 0,
	@sortOrder varchar(250) = null,
	@pageSize int = 0,
	@pageNumber int = 0,
	@totalRecordCount INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DECLARE @sqlString nvarchar(4000)
DECLARE @parmDefinition nvarchar(1000)
DECLARE @startingRow int

IF (@pageNumber = 0)
	SET @pageNumber = 1

IF (@pageSize = 0)
	SET @pageSize = 10

IF (@pageNumber = 1)
	SET @startingRow = 1
ELSE
	SET @startingRow = ((@pageNumber - 1) * @pageSize) + 1

-- Add the primary key for better sorting results
IF (ISNULL(@sortOrder, '') = '')
	SET @sortOrder = '[Membership].[MembershipKey]'
ELSE
	SET @sortOrder = @sortOrder + ', [Membership].[MembershipKey]'

SET @parmDefinition = N' @startingRow INT, @pageSize INT, @vendorKey INT'

-- Get total records count
SELECT @totalRecordCount = COUNT(*)
FROM
	[Membership] 
WHERE ([Membership].[VendorKey] = @vendorKey OR @vendorKey IS NULL OR @vendorKey = 0)

-- Determine records exist to retrieve
IF (@totalRecordCount > 0)
BEGIN

	SET @sqlString = N'SELECT * ' + 
		'FROM ( ' + 
		'	SELECT ROW_NUMBER() OVER ( ORDER BY ' + @sortOrder + ' ) AS RowNumber, ' + 
		'		[Membership].[MembershipKey], ' +
		'		[Membership].[VendorKey], ' +
		'		[Membership].[StartDate], ' +
		'		[Membership].[EndDate], ' +
		'		[Membership].[RenewalDate], ' +
		'		[Membership].[AutomaticRenewal], ' +
		'		[Membership].[LastModificationTime] ' +
		'	FROM [Membership]  ' + 
		'	WHERE ([Membership].[VendorKey] = @vendorKey OR @vendorKey IS NULL OR @vendorKey = 0) ' +
		') AS FilteredResult ' + 
		'WHERE RowNumber >= @startingRow ' + 
		'AND RowNumber < (@startingRow + @pageSize) ' + 
		'ORDER BY RowNumber ' 

	EXECUTE sp_executesql @sqlString, @parmDefinition, @startingRow, @pageSize, @vendorKey

	-- Get the Error Code for the statement just executed.
	SELECT @errorCode = @@ERROR
END

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Membership
-- * Procedure Name : gensp_Membership_InsertOne
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Membership_InsertOne]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Membership_InsertOne]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Membership_InsertOne]
	@vendorKey INT,
	@startDate DATETIME,
	@endDate DATETIME,
	@renewalDate DATETIME,
	@automaticRenewal BIT,
	@lastModificationTime DATETIME,
	@membershipKey INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON


INSERT INTO [Membership]
(
	[VendorKey],
	[StartDate],
	[EndDate],
	[RenewalDate],
	[AutomaticRenewal],
	[LastModificationTime]
)
VALUES
(
	@vendorKey,
	@startDate,
	@endDate,
	@renewalDate,
	@automaticRenewal,
	@lastModificationTime
)
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR
-- Get the IDENTITY value for the row just inserted.
SELECT @membershipKey = SCOPE_IDENTITY()

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Membership
-- * Procedure Name : gensp_Membership_UpdateOneByMembershipKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Membership_UpdateOneByMembershipKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Membership_UpdateOneByMembershipKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Membership_UpdateOneByMembershipKey]
	@membershipKey INT,
	@vendorKey INT,
	@startDate DATETIME,
	@endDate DATETIME,
	@renewalDate DATETIME,
	@automaticRenewal BIT,
	@lastModificationTime DATETIME,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

UPDATE [Membership]
SET
	[VendorKey] = @vendorKey,
	[StartDate] = @startDate,
	[EndDate] = @endDate,
	[RenewalDate] = @renewalDate,
	[AutomaticRenewal] = @automaticRenewal,
	[LastModificationTime] = @lastModificationTime
WHERE [Membership].[MembershipKey] = @membershipKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Membership
-- * Procedure Name : gensp_Membership_DeleteOneByMembershipKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Membership_DeleteOneByMembershipKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Membership_DeleteOneByMembershipKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Membership_DeleteOneByMembershipKey]
	@membershipKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DELETE FROM [Membership]
WHERE [Membership].[MembershipKey] = @membershipKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Message
-- * Procedure Name : gensp_Message_SelectOneByMessageKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Message_SelectOneByMessageKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Message_SelectOneByMessageKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Message_SelectOneByMessageKey]
	@messageKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON
SELECT
	[Message].[MessageKey], 
	[Message].[ModuleKey], 
	[Message].[ResourceKey], 
	[Message].[ObjectKey], 
	[Message].[Body], 
	[Message].[LastModificationTime], 
	[Message].[MessageStatus] 
FROM
	[Message] 
WHERE [Message].[MessageKey] = @messageKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Message
-- * Procedure Name : gensp_Message_SelectSomeBySearch
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Message_SelectSomeBySearch]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Message_SelectSomeBySearch]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Message_SelectSomeBySearch]
	@moduleKey INT = 0,
	@resourceKey INT = 0,
	@objectKey INT = 0,
	@messageStatus INT = 0,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

SELECT
	[Message].[MessageKey], 
	[Message].[ModuleKey], 
	[Message].[ResourceKey], 
	[Message].[ObjectKey], 
	[Message].[Body], 
	[Message].[LastModificationTime], 
	[Message].[MessageStatus] 
FROM
	[Message] 
WHERE ([Message].[ModuleKey] = @moduleKey OR @moduleKey IS NULL OR @moduleKey = 0)
AND ([Message].[ResourceKey] = @resourceKey OR @resourceKey IS NULL OR @resourceKey = 0)
AND ([Message].[ObjectKey] = @objectKey OR @objectKey IS NULL OR @objectKey = 0)
AND ([Message].[MessageStatus] & @messageStatus = [Message].[MessageStatus] OR @messageStatus IS NULL OR @messageStatus = 0)

-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Message
-- * Procedure Name : gensp_Message_SelectSomeBySearchAndPaging
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Message_SelectSomeBySearchAndPaging]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Message_SelectSomeBySearchAndPaging]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Message_SelectSomeBySearchAndPaging]
	@moduleKey INT = 0,
	@resourceKey INT = 0,
	@objectKey INT = 0,
	@messageStatus INT = 0,
	@sortOrder varchar(250) = null,
	@pageSize int = 0,
	@pageNumber int = 0,
	@totalRecordCount INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DECLARE @sqlString nvarchar(4000)
DECLARE @parmDefinition nvarchar(1000)
DECLARE @startingRow int

IF (@pageNumber = 0)
	SET @pageNumber = 1

IF (@pageSize = 0)
	SET @pageSize = 10

IF (@pageNumber = 1)
	SET @startingRow = 1
ELSE
	SET @startingRow = ((@pageNumber - 1) * @pageSize) + 1

-- Add the primary key for better sorting results
IF (ISNULL(@sortOrder, '') = '')
	SET @sortOrder = '[Message].[MessageKey]'
ELSE
	SET @sortOrder = @sortOrder + ', [Message].[MessageKey]'

SET @parmDefinition = N' @startingRow INT, @pageSize INT, @moduleKey INT, @resourceKey INT, @objectKey INT, @messageStatus INT'

-- Get total records count
SELECT @totalRecordCount = COUNT(*)
FROM
	[Message] 
WHERE ([Message].[ModuleKey] = @moduleKey OR @moduleKey IS NULL OR @moduleKey = 0)
AND ([Message].[ResourceKey] = @resourceKey OR @resourceKey IS NULL OR @resourceKey = 0)
AND ([Message].[ObjectKey] = @objectKey OR @objectKey IS NULL OR @objectKey = 0)
AND ([Message].[MessageStatus] & @messageStatus = [Message].[MessageStatus] OR @messageStatus IS NULL OR @messageStatus = 0)

-- Determine records exist to retrieve
IF (@totalRecordCount > 0)
BEGIN

	SET @sqlString = N'SELECT * ' + 
		'FROM ( ' + 
		'	SELECT ROW_NUMBER() OVER ( ORDER BY ' + @sortOrder + ' ) AS RowNumber, ' + 
		'		[Message].[MessageKey], ' +
		'		[Message].[ModuleKey], ' +
		'		[Message].[ResourceKey], ' +
		'		[Message].[ObjectKey], ' +
		'		[Message].[Body], ' +
		'		[Message].[LastModificationTime], ' +
		'		[Message].[MessageStatus] ' +
		'	FROM [Message]  ' + 
		'	WHERE ([Message].[ModuleKey] = @moduleKey OR @moduleKey IS NULL OR @moduleKey = 0) ' +
		'	AND ([Message].[ResourceKey] = @resourceKey OR @resourceKey IS NULL OR @resourceKey = 0) ' +
		'	AND ([Message].[ObjectKey] = @objectKey OR @objectKey IS NULL OR @objectKey = 0) ' +
		'	AND ([Message].[MessageStatus] & @messageStatus = [Message].[MessageStatus] OR @messageStatus IS NULL OR @messageStatus = 0) ' +
		') AS FilteredResult ' + 
		'WHERE RowNumber >= @startingRow ' + 
		'AND RowNumber < (@startingRow + @pageSize) ' + 
		'ORDER BY RowNumber ' 

	EXECUTE sp_executesql @sqlString, @parmDefinition, @startingRow, @pageSize, @moduleKey, @resourceKey, @objectKey, @messageStatus

	-- Get the Error Code for the statement just executed.
	SELECT @errorCode = @@ERROR
END

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Message
-- * Procedure Name : gensp_Message_InsertOne
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Message_InsertOne]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Message_InsertOne]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Message_InsertOne]
	@moduleKey INT,
	@resourceKey INT,
	@objectKey INT,
	@body VARCHAR(MAX),
	@lastModificationTime DATETIME,
	@messageStatus INT,
	@messageKey INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON


INSERT INTO [Message]
(
	[ModuleKey],
	[ResourceKey],
	[ObjectKey],
	[Body],
	[LastModificationTime],
	[MessageStatus]
)
VALUES
(
	@moduleKey,
	@resourceKey,
	@objectKey,
	@body,
	@lastModificationTime,
	@messageStatus
)
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR
-- Get the IDENTITY value for the row just inserted.
SELECT @messageKey = SCOPE_IDENTITY()

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Message
-- * Procedure Name : gensp_Message_UpdateOneByMessageKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Message_UpdateOneByMessageKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Message_UpdateOneByMessageKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Message_UpdateOneByMessageKey]
	@messageKey INT,
	@moduleKey INT,
	@resourceKey INT,
	@objectKey INT,
	@body VARCHAR(MAX),
	@lastModificationTime DATETIME,
	@messageStatus INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

UPDATE [Message]
SET
	[ModuleKey] = @moduleKey,
	[ResourceKey] = @resourceKey,
	[ObjectKey] = @objectKey,
	[Body] = @body,
	[LastModificationTime] = @lastModificationTime,
	[MessageStatus] = @messageStatus
WHERE [Message].[MessageKey] = @messageKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Message
-- * Procedure Name : gensp_Message_DeleteOneByMessageKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Message_DeleteOneByMessageKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Message_DeleteOneByMessageKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Message_DeleteOneByMessageKey]
	@messageKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DELETE FROM [Message]
WHERE [Message].[MessageKey] = @messageKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Module
-- * Procedure Name : gensp_Module_SelectOneByModuleKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Module_SelectOneByModuleKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Module_SelectOneByModuleKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Module_SelectOneByModuleKey]
	@moduleKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON
SELECT
	[Module].[ModuleKey], 
	[Module].[Title], 
	[Module].[Controller], 
	[Module].[Action], 
	[Module].[Image], 
	[Module].[Description] 
FROM
	[Module] 
WHERE [Module].[ModuleKey] = @moduleKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Module
-- * Procedure Name : gensp_Module_SelectSomeBySearch
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Module_SelectSomeBySearch]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Module_SelectSomeBySearch]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Module_SelectSomeBySearch]
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

SELECT
	[Module].[ModuleKey], 
	[Module].[Title], 
	[Module].[Controller], 
	[Module].[Action], 
	[Module].[Image], 
	[Module].[Description] 
FROM
	[Module] 

-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Module
-- * Procedure Name : gensp_Module_SelectSomeBySearchAndPaging
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Module_SelectSomeBySearchAndPaging]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Module_SelectSomeBySearchAndPaging]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Module_SelectSomeBySearchAndPaging]
	@sortOrder varchar(250) = null,
	@pageSize int = 0,
	@pageNumber int = 0,
	@totalRecordCount INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DECLARE @sqlString nvarchar(4000)
DECLARE @parmDefinition nvarchar(1000)
DECLARE @startingRow int

IF (@pageNumber = 0)
	SET @pageNumber = 1

IF (@pageSize = 0)
	SET @pageSize = 10

IF (@pageNumber = 1)
	SET @startingRow = 1
ELSE
	SET @startingRow = ((@pageNumber - 1) * @pageSize) + 1

-- Add the primary key for better sorting results
IF (ISNULL(@sortOrder, '') = '')
	SET @sortOrder = '[Module].[ModuleKey]'
ELSE
	SET @sortOrder = @sortOrder + ', [Module].[ModuleKey]'

SET @parmDefinition = N' @startingRow INT, @pageSize INT'

-- Get total records count
SELECT @totalRecordCount = COUNT(*)
FROM
	[Module] 

-- Determine records exist to retrieve
IF (@totalRecordCount > 0)
BEGIN

	SET @sqlString = N'SELECT * ' + 
		'FROM ( ' + 
		'	SELECT ROW_NUMBER() OVER ( ORDER BY ' + @sortOrder + ' ) AS RowNumber, ' + 
		'		[Module].[ModuleKey], ' +
		'		[Module].[Title], ' +
		'		[Module].[Controller], ' +
		'		[Module].[Action], ' +
		'		[Module].[Image], ' +
		'		[Module].[Description] ' +
		'	FROM [Module]  ' + 
		') AS FilteredResult ' + 
		'WHERE RowNumber >= @startingRow ' + 
		'AND RowNumber < (@startingRow + @pageSize) ' + 
		'ORDER BY RowNumber ' 

	EXECUTE sp_executesql @sqlString, @parmDefinition, @startingRow, @pageSize

	-- Get the Error Code for the statement just executed.
	SELECT @errorCode = @@ERROR
END

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Module
-- * Procedure Name : gensp_Module_InsertOne
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Module_InsertOne]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Module_InsertOne]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Module_InsertOne]
	@moduleKey INT,
	@title VARCHAR(150),
	@controller VARCHAR(150),
	@action VARCHAR(150),
	@image VARCHAR(150),
	@description VARCHAR(MAX),
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON


INSERT INTO [Module]
(
	[ModuleKey],
	[Title],
	[Controller],
	[Action],
	[Image],
	[Description]
)
VALUES
(
	@moduleKey,
	@title,
	@controller,
	@action,
	@image,
	@description
)
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Module
-- * Procedure Name : gensp_Module_UpdateOneByModuleKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Module_UpdateOneByModuleKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Module_UpdateOneByModuleKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Module_UpdateOneByModuleKey]
	@moduleKey INT,
	@title VARCHAR(150),
	@controller VARCHAR(150),
	@action VARCHAR(150),
	@image VARCHAR(150),
	@description VARCHAR(MAX),
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

UPDATE [Module]
SET
	[Title] = @title,
	[Controller] = @controller,
	[Action] = @action,
	[Image] = @image,
	[Description] = @description
WHERE [Module].[ModuleKey] = @moduleKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Module
-- * Procedure Name : gensp_Module_DeleteOneByModuleKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Module_DeleteOneByModuleKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Module_DeleteOneByModuleKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Module_DeleteOneByModuleKey]
	@moduleKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DELETE FROM [Module]
WHERE [Module].[ModuleKey] = @moduleKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Note
-- * Procedure Name : gensp_Note_SelectOneByNoteKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Note_SelectOneByNoteKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Note_SelectOneByNoteKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Note_SelectOneByNoteKey]
	@noteKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON
SELECT
	[Note].[NoteKey], 
	[Note].[ModuleKey], 
	[Note].[ResourceKey], 
	[Note].[ObjectKey], 
	[Note].[Description], 
	[Note].[LastModificationTime], 
	[Note].[Status] 
FROM
	[Note] 
WHERE [Note].[NoteKey] = @noteKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Note
-- * Procedure Name : gensp_Note_SelectSomeBySearch
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Note_SelectSomeBySearch]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Note_SelectSomeBySearch]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Note_SelectSomeBySearch]
	@moduleKey INT = 0,
	@resourceKey INT = 0,
	@objectKey INT = 0,
	@status INT = 0,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

SELECT
	[Note].[NoteKey], 
	[Note].[ModuleKey], 
	[Note].[ResourceKey], 
	[Note].[ObjectKey], 
	[Note].[Description], 
	[Note].[LastModificationTime], 
	[Note].[Status] 
FROM
	[Note] 
WHERE ([Note].[ModuleKey] = @moduleKey OR @moduleKey IS NULL OR @moduleKey = 0)
AND ([Note].[ResourceKey] = @resourceKey OR @resourceKey IS NULL OR @resourceKey = 0)
AND ([Note].[ObjectKey] = @objectKey OR @objectKey IS NULL OR @objectKey = 0)
AND ([Note].[Status] & ISNULL(@status, 7) = [Note].[Status])

-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Note
-- * Procedure Name : gensp_Note_SelectSomeBySearchAndPaging
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Note_SelectSomeBySearchAndPaging]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Note_SelectSomeBySearchAndPaging]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Note_SelectSomeBySearchAndPaging]
	@moduleKey INT = 0,
	@resourceKey INT = 0,
	@objectKey INT = 0,
	@status INT = 0,
	@sortOrder varchar(250) = null,
	@pageSize int = 0,
	@pageNumber int = 0,
	@totalRecordCount INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DECLARE @sqlString nvarchar(4000)
DECLARE @parmDefinition nvarchar(1000)
DECLARE @startingRow int

IF (@pageNumber = 0)
	SET @pageNumber = 1

IF (@pageSize = 0)
	SET @pageSize = 10

IF (@pageNumber = 1)
	SET @startingRow = 1
ELSE
	SET @startingRow = ((@pageNumber - 1) * @pageSize) + 1

-- Add the primary key for better sorting results
IF (ISNULL(@sortOrder, '') = '')
	SET @sortOrder = '[Note].[NoteKey]'
ELSE
	SET @sortOrder = @sortOrder + ', [Note].[NoteKey]'

SET @parmDefinition = N' @startingRow INT, @pageSize INT, @moduleKey INT, @resourceKey INT, @objectKey INT, @status INT'

-- Get total records count
SELECT @totalRecordCount = COUNT(*)
FROM
	[Note] 
WHERE ([Note].[ModuleKey] = @moduleKey OR @moduleKey IS NULL OR @moduleKey = 0)
AND ([Note].[ResourceKey] = @resourceKey OR @resourceKey IS NULL OR @resourceKey = 0)
AND ([Note].[ObjectKey] = @objectKey OR @objectKey IS NULL OR @objectKey = 0)
AND ([Note].[Status] & ISNULL(@status, 7) = [Note].[Status])

-- Determine records exist to retrieve
IF (@totalRecordCount > 0)
BEGIN

	SET @sqlString = N'SELECT * ' + 
		'FROM ( ' + 
		'	SELECT ROW_NUMBER() OVER ( ORDER BY ' + @sortOrder + ' ) AS RowNumber, ' + 
		'		[Note].[NoteKey], ' +
		'		[Note].[ModuleKey], ' +
		'		[Note].[ResourceKey], ' +
		'		[Note].[ObjectKey], ' +
		'		[Note].[Description], ' +
		'		[Note].[LastModificationTime], ' +
		'		[Note].[Status] ' +
		'	FROM [Note]  ' + 
		'	WHERE ([Note].[ModuleKey] = @moduleKey OR @moduleKey IS NULL OR @moduleKey = 0) ' +
		'	AND ([Note].[ResourceKey] = @resourceKey OR @resourceKey IS NULL OR @resourceKey = 0) ' +
		'	AND ([Note].[ObjectKey] = @objectKey OR @objectKey IS NULL OR @objectKey = 0) ' +
		'	AND ([Note].[Status] & ISNULL(@status, 7) = [Note].[Status]) ' +
		') AS FilteredResult ' + 
		'WHERE RowNumber >= @startingRow ' + 
		'AND RowNumber < (@startingRow + @pageSize) ' + 
		'ORDER BY RowNumber ' 

	EXECUTE sp_executesql @sqlString, @parmDefinition, @startingRow, @pageSize, @moduleKey, @resourceKey, @objectKey, @status

	-- Get the Error Code for the statement just executed.
	SELECT @errorCode = @@ERROR
END

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Note
-- * Procedure Name : gensp_Note_InsertOne
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Note_InsertOne]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Note_InsertOne]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Note_InsertOne]
	@moduleKey INT,
	@resourceKey INT,
	@objectKey INT,
	@description VARCHAR(MAX),
	@lastModificationTime DATETIME,
	@status INT,
	@noteKey INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON


INSERT INTO [Note]
(
	[ModuleKey],
	[ResourceKey],
	[ObjectKey],
	[Description],
	[LastModificationTime],
	[Status]
)
VALUES
(
	@moduleKey,
	@resourceKey,
	@objectKey,
	@description,
	@lastModificationTime,
	@status
)
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR
-- Get the IDENTITY value for the row just inserted.
SELECT @noteKey = SCOPE_IDENTITY()

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Note
-- * Procedure Name : gensp_Note_UpdateOneByNoteKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Note_UpdateOneByNoteKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Note_UpdateOneByNoteKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Note_UpdateOneByNoteKey]
	@noteKey INT,
	@moduleKey INT,
	@resourceKey INT,
	@objectKey INT,
	@description VARCHAR(MAX),
	@lastModificationTime DATETIME,
	@status INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

UPDATE [Note]
SET
	[ModuleKey] = @moduleKey,
	[ResourceKey] = @resourceKey,
	[ObjectKey] = @objectKey,
	[Description] = @description,
	[LastModificationTime] = @lastModificationTime,
	[Status] = @status
WHERE [Note].[NoteKey] = @noteKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Note
-- * Procedure Name : gensp_Note_DeleteOneByNoteKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Note_DeleteOneByNoteKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Note_DeleteOneByNoteKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Note_DeleteOneByNoteKey]
	@noteKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DELETE FROM [Note]
WHERE [Note].[NoteKey] = @noteKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Payment
-- * Procedure Name : gensp_Payment_SelectOneByPaymentKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Payment_SelectOneByPaymentKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Payment_SelectOneByPaymentKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Payment_SelectOneByPaymentKey]
	@paymentKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON
SELECT
	[Payment].[PaymentKey], 
	[Payment].[VendorKey], 
	[Payment].[PaymentTypeKey], 
	[Payment].[ReferenceNumber], 
	[Payment].[TransactionDate], 
	[Payment].[Amount], 
	[Payment].[Balance], 
	[Payment].[Description], 
	[Payment].[LastModificationTime] 
FROM
	[Payment] 
WHERE [Payment].[PaymentKey] = @paymentKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Payment
-- * Procedure Name : gensp_Payment_SelectSomeBySearch
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Payment_SelectSomeBySearch]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Payment_SelectSomeBySearch]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Payment_SelectSomeBySearch]
	@vendorKey INT = 0,
	@paymentTypeKey INT = 0,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

SELECT
	[Payment].[PaymentKey], 
	[Payment].[VendorKey], 
	[Payment].[PaymentTypeKey], 
	[Payment].[ReferenceNumber], 
	[Payment].[TransactionDate], 
	[Payment].[Amount], 
	[Payment].[Balance], 
	[Payment].[Description], 
	[Payment].[LastModificationTime] 
FROM
	[Payment] 
WHERE ([Payment].[VendorKey] = @vendorKey OR @vendorKey IS NULL OR @vendorKey = 0)
AND ([Payment].[PaymentTypeKey] = @paymentTypeKey OR @paymentTypeKey IS NULL OR @paymentTypeKey = 0)

-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Payment
-- * Procedure Name : gensp_Payment_SelectSomeBySearchAndPaging
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Payment_SelectSomeBySearchAndPaging]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Payment_SelectSomeBySearchAndPaging]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Payment_SelectSomeBySearchAndPaging]
	@vendorKey INT = 0,
	@paymentTypeKey INT = 0,
	@sortOrder varchar(250) = null,
	@pageSize int = 0,
	@pageNumber int = 0,
	@totalRecordCount INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DECLARE @sqlString nvarchar(4000)
DECLARE @parmDefinition nvarchar(1000)
DECLARE @startingRow int

IF (@pageNumber = 0)
	SET @pageNumber = 1

IF (@pageSize = 0)
	SET @pageSize = 10

IF (@pageNumber = 1)
	SET @startingRow = 1
ELSE
	SET @startingRow = ((@pageNumber - 1) * @pageSize) + 1

-- Add the primary key for better sorting results
IF (ISNULL(@sortOrder, '') = '')
	SET @sortOrder = '[Payment].[PaymentKey]'
ELSE
	SET @sortOrder = @sortOrder + ', [Payment].[PaymentKey]'

SET @parmDefinition = N' @startingRow INT, @pageSize INT, @vendorKey INT, @paymentTypeKey INT'

-- Get total records count
SELECT @totalRecordCount = COUNT(*)
FROM
	[Payment] 
WHERE ([Payment].[VendorKey] = @vendorKey OR @vendorKey IS NULL OR @vendorKey = 0)
AND ([Payment].[PaymentTypeKey] = @paymentTypeKey OR @paymentTypeKey IS NULL OR @paymentTypeKey = 0)

-- Determine records exist to retrieve
IF (@totalRecordCount > 0)
BEGIN

	SET @sqlString = N'SELECT * ' + 
		'FROM ( ' + 
		'	SELECT ROW_NUMBER() OVER ( ORDER BY ' + @sortOrder + ' ) AS RowNumber, ' + 
		'		[Payment].[PaymentKey], ' +
		'		[Payment].[VendorKey], ' +
		'		[Payment].[PaymentTypeKey], ' +
		'		[Payment].[ReferenceNumber], ' +
		'		[Payment].[TransactionDate], ' +
		'		[Payment].[Amount], ' +
		'		[Payment].[Balance], ' +
		'		[Payment].[Description], ' +
		'		[Payment].[LastModificationTime] ' +
		'	FROM [Payment]  ' + 
		'	WHERE ([Payment].[VendorKey] = @vendorKey OR @vendorKey IS NULL OR @vendorKey = 0) ' +
		'	AND ([Payment].[PaymentTypeKey] = @paymentTypeKey OR @paymentTypeKey IS NULL OR @paymentTypeKey = 0) ' +
		') AS FilteredResult ' + 
		'WHERE RowNumber >= @startingRow ' + 
		'AND RowNumber < (@startingRow + @pageSize) ' + 
		'ORDER BY RowNumber ' 

	EXECUTE sp_executesql @sqlString, @parmDefinition, @startingRow, @pageSize, @vendorKey, @paymentTypeKey

	-- Get the Error Code for the statement just executed.
	SELECT @errorCode = @@ERROR
END

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Payment
-- * Procedure Name : gensp_Payment_InsertOne
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Payment_InsertOne]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Payment_InsertOne]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Payment_InsertOne]
	@vendorKey INT,
	@paymentTypeKey INT,
	@referenceNumber VARCHAR(150),
	@transactionDate DATETIME,
	@amount MONEY,
	@balance MONEY,
	@description VARCHAR(5000),
	@lastModificationTime DATETIME,
	@paymentKey INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON


INSERT INTO [Payment]
(
	[VendorKey],
	[PaymentTypeKey],
	[ReferenceNumber],
	[TransactionDate],
	[Amount],
	[Balance],
	[Description],
	[LastModificationTime]
)
VALUES
(
	@vendorKey,
	@paymentTypeKey,
	@referenceNumber,
	@transactionDate,
	@amount,
	@balance,
	@description,
	@lastModificationTime
)
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR
-- Get the IDENTITY value for the row just inserted.
SELECT @paymentKey = SCOPE_IDENTITY()

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Payment
-- * Procedure Name : gensp_Payment_UpdateOneByPaymentKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Payment_UpdateOneByPaymentKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Payment_UpdateOneByPaymentKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Payment_UpdateOneByPaymentKey]
	@paymentKey INT,
	@vendorKey INT,
	@paymentTypeKey INT,
	@referenceNumber VARCHAR(150),
	@transactionDate DATETIME,
	@amount MONEY,
	@balance MONEY,
	@description VARCHAR(5000),
	@lastModificationTime DATETIME,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

UPDATE [Payment]
SET
	[VendorKey] = @vendorKey,
	[PaymentTypeKey] = @paymentTypeKey,
	[ReferenceNumber] = @referenceNumber,
	[TransactionDate] = @transactionDate,
	[Amount] = @amount,
	[Balance] = @balance,
	[Description] = @description,
	[LastModificationTime] = @lastModificationTime
WHERE [Payment].[PaymentKey] = @paymentKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Payment
-- * Procedure Name : gensp_Payment_DeleteOneByPaymentKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Payment_DeleteOneByPaymentKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Payment_DeleteOneByPaymentKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Payment_DeleteOneByPaymentKey]
	@paymentKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DELETE FROM [Payment]
WHERE [Payment].[PaymentKey] = @paymentKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : PaymentApplied
-- * Procedure Name : gensp_PaymentApplied_SelectOneByPaymentAppliedKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_PaymentApplied_SelectOneByPaymentAppliedKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_PaymentApplied_SelectOneByPaymentAppliedKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_PaymentApplied_SelectOneByPaymentAppliedKey]
	@paymentAppliedKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON
SELECT
	[PaymentApplied].[PaymentAppliedKey], 
	[PaymentApplied].[PaymentKey], 
	[PaymentApplied].[InvociceKey], 
	[PaymentApplied].[Amount], 
	[PaymentApplied].[LastModificationTime] 
FROM
	[PaymentApplied] 
WHERE [PaymentApplied].[PaymentAppliedKey] = @paymentAppliedKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : PaymentApplied
-- * Procedure Name : gensp_PaymentApplied_SelectSomeBySearch
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_PaymentApplied_SelectSomeBySearch]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_PaymentApplied_SelectSomeBySearch]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_PaymentApplied_SelectSomeBySearch]
	@paymentKey INT = 0,
	@invociceKey INT = 0,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

SELECT
	[PaymentApplied].[PaymentAppliedKey], 
	[PaymentApplied].[PaymentKey], 
	[PaymentApplied].[InvociceKey], 
	[PaymentApplied].[Amount], 
	[PaymentApplied].[LastModificationTime] 
FROM
	[PaymentApplied] 
WHERE ([PaymentApplied].[PaymentKey] = @paymentKey OR @paymentKey IS NULL OR @paymentKey = 0)
AND ([PaymentApplied].[InvociceKey] = @invociceKey OR @invociceKey IS NULL OR @invociceKey = 0)

-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : PaymentApplied
-- * Procedure Name : gensp_PaymentApplied_SelectSomeBySearchAndPaging
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_PaymentApplied_SelectSomeBySearchAndPaging]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_PaymentApplied_SelectSomeBySearchAndPaging]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_PaymentApplied_SelectSomeBySearchAndPaging]
	@paymentKey INT = 0,
	@invociceKey INT = 0,
	@sortOrder varchar(250) = null,
	@pageSize int = 0,
	@pageNumber int = 0,
	@totalRecordCount INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DECLARE @sqlString nvarchar(4000)
DECLARE @parmDefinition nvarchar(1000)
DECLARE @startingRow int

IF (@pageNumber = 0)
	SET @pageNumber = 1

IF (@pageSize = 0)
	SET @pageSize = 10

IF (@pageNumber = 1)
	SET @startingRow = 1
ELSE
	SET @startingRow = ((@pageNumber - 1) * @pageSize) + 1

-- Add the primary key for better sorting results
IF (ISNULL(@sortOrder, '') = '')
	SET @sortOrder = '[PaymentApplied].[PaymentAppliedKey]'
ELSE
	SET @sortOrder = @sortOrder + ', [PaymentApplied].[PaymentAppliedKey]'

SET @parmDefinition = N' @startingRow INT, @pageSize INT, @paymentKey INT, @invociceKey INT'

-- Get total records count
SELECT @totalRecordCount = COUNT(*)
FROM
	[PaymentApplied] 
WHERE ([PaymentApplied].[PaymentKey] = @paymentKey OR @paymentKey IS NULL OR @paymentKey = 0)
AND ([PaymentApplied].[InvociceKey] = @invociceKey OR @invociceKey IS NULL OR @invociceKey = 0)

-- Determine records exist to retrieve
IF (@totalRecordCount > 0)
BEGIN

	SET @sqlString = N'SELECT * ' + 
		'FROM ( ' + 
		'	SELECT ROW_NUMBER() OVER ( ORDER BY ' + @sortOrder + ' ) AS RowNumber, ' + 
		'		[PaymentApplied].[PaymentAppliedKey], ' +
		'		[PaymentApplied].[PaymentKey], ' +
		'		[PaymentApplied].[InvociceKey], ' +
		'		[PaymentApplied].[Amount], ' +
		'		[PaymentApplied].[LastModificationTime] ' +
		'	FROM [PaymentApplied]  ' + 
		'	WHERE ([PaymentApplied].[PaymentKey] = @paymentKey OR @paymentKey IS NULL OR @paymentKey = 0) ' +
		'	AND ([PaymentApplied].[InvociceKey] = @invociceKey OR @invociceKey IS NULL OR @invociceKey = 0) ' +
		') AS FilteredResult ' + 
		'WHERE RowNumber >= @startingRow ' + 
		'AND RowNumber < (@startingRow + @pageSize) ' + 
		'ORDER BY RowNumber ' 

	EXECUTE sp_executesql @sqlString, @parmDefinition, @startingRow, @pageSize, @paymentKey, @invociceKey

	-- Get the Error Code for the statement just executed.
	SELECT @errorCode = @@ERROR
END

GO

-- *****************************************************

-- *****************************************************
-- * Table          : PaymentApplied
-- * Procedure Name : gensp_PaymentApplied_InsertOne
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_PaymentApplied_InsertOne]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_PaymentApplied_InsertOne]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_PaymentApplied_InsertOne]
	@paymentKey INT,
	@invociceKey INT,
	@amount MONEY,
	@lastModificationTime DATETIME,
	@paymentAppliedKey INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON


INSERT INTO [PaymentApplied]
(
	[PaymentKey],
	[InvociceKey],
	[Amount],
	[LastModificationTime]
)
VALUES
(
	@paymentKey,
	@invociceKey,
	@amount,
	@lastModificationTime
)
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR
-- Get the IDENTITY value for the row just inserted.
SELECT @paymentAppliedKey = SCOPE_IDENTITY()

GO

-- *****************************************************

-- *****************************************************
-- * Table          : PaymentApplied
-- * Procedure Name : gensp_PaymentApplied_UpdateOneByPaymentAppliedKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_PaymentApplied_UpdateOneByPaymentAppliedKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_PaymentApplied_UpdateOneByPaymentAppliedKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_PaymentApplied_UpdateOneByPaymentAppliedKey]
	@paymentAppliedKey INT,
	@paymentKey INT,
	@invociceKey INT,
	@amount MONEY,
	@lastModificationTime DATETIME,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

UPDATE [PaymentApplied]
SET
	[PaymentKey] = @paymentKey,
	[InvociceKey] = @invociceKey,
	[Amount] = @amount,
	[LastModificationTime] = @lastModificationTime
WHERE [PaymentApplied].[PaymentAppliedKey] = @paymentAppliedKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : PaymentApplied
-- * Procedure Name : gensp_PaymentApplied_DeleteOneByPaymentAppliedKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_PaymentApplied_DeleteOneByPaymentAppliedKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_PaymentApplied_DeleteOneByPaymentAppliedKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_PaymentApplied_DeleteOneByPaymentAppliedKey]
	@paymentAppliedKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DELETE FROM [PaymentApplied]
WHERE [PaymentApplied].[PaymentAppliedKey] = @paymentAppliedKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Portal
-- * Procedure Name : gensp_Portal_SelectOneByPortalKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Portal_SelectOneByPortalKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Portal_SelectOneByPortalKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Portal_SelectOneByPortalKey]
	@portalKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON
SELECT
	[Portal].[PortalKey], 
	[Portal].[PortalID], 
	[Portal].[Title], 
	[Portal].[Url], 
	[Portal].[SiteImage], 
	[Portal].[HomePageImage], 
	[Portal].[Stylesheet], 
	[Portal].[Description], 
	[Portal].[NotificationSetting], 
	[Portal].[DateAdded], 
	[Portal].[LastModificationTime], 
	[Portal].[Status] 
FROM
	[Portal] 
WHERE [Portal].[PortalKey] = @portalKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Portal
-- * Procedure Name : gensp_Portal_SelectSomeBySearch
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Portal_SelectSomeBySearch]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Portal_SelectSomeBySearch]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Portal_SelectSomeBySearch]
	@status INT = 0,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

SELECT
	[Portal].[PortalKey], 
	[Portal].[PortalID], 
	[Portal].[Title], 
	[Portal].[Url], 
	[Portal].[SiteImage], 
	[Portal].[HomePageImage], 
	[Portal].[Stylesheet], 
	[Portal].[Description], 
	[Portal].[NotificationSetting], 
	[Portal].[DateAdded], 
	[Portal].[LastModificationTime], 
	[Portal].[Status] 
FROM
	[Portal] 
WHERE ([Portal].[Status] & ISNULL(@status, 7) = [Portal].[Status])

-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Portal
-- * Procedure Name : gensp_Portal_SelectSomeBySearchAndPaging
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Portal_SelectSomeBySearchAndPaging]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Portal_SelectSomeBySearchAndPaging]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Portal_SelectSomeBySearchAndPaging]
	@status INT = 0,
	@sortOrder varchar(250) = null,
	@pageSize int = 0,
	@pageNumber int = 0,
	@totalRecordCount INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DECLARE @sqlString nvarchar(4000)
DECLARE @parmDefinition nvarchar(1000)
DECLARE @startingRow int

IF (@pageNumber = 0)
	SET @pageNumber = 1

IF (@pageSize = 0)
	SET @pageSize = 10

IF (@pageNumber = 1)
	SET @startingRow = 1
ELSE
	SET @startingRow = ((@pageNumber - 1) * @pageSize) + 1

-- Add the primary key for better sorting results
IF (ISNULL(@sortOrder, '') = '')
	SET @sortOrder = '[Portal].[PortalKey]'
ELSE
	SET @sortOrder = @sortOrder + ', [Portal].[PortalKey]'

SET @parmDefinition = N' @startingRow INT, @pageSize INT, @status INT'

-- Get total records count
SELECT @totalRecordCount = COUNT(*)
FROM
	[Portal] 
WHERE ([Portal].[Status] & ISNULL(@status, 7) = [Portal].[Status])

-- Determine records exist to retrieve
IF (@totalRecordCount > 0)
BEGIN

	SET @sqlString = N'SELECT * ' + 
		'FROM ( ' + 
		'	SELECT ROW_NUMBER() OVER ( ORDER BY ' + @sortOrder + ' ) AS RowNumber, ' + 
		'		[Portal].[PortalKey], ' +
		'		[Portal].[PortalID], ' +
		'		[Portal].[Title], ' +
		'		[Portal].[Url], ' +
		'		[Portal].[SiteImage], ' +
		'		[Portal].[HomePageImage], ' +
		'		[Portal].[Stylesheet], ' +
		'		[Portal].[Description], ' +
		'		[Portal].[NotificationSetting], ' +
		'		[Portal].[DateAdded], ' +
		'		[Portal].[LastModificationTime], ' +
		'		[Portal].[Status] ' +
		'	FROM [Portal]  ' + 
		'	WHERE ([Portal].[Status] & ISNULL(@status, 7) = [Portal].[Status]) ' +
		') AS FilteredResult ' + 
		'WHERE RowNumber >= @startingRow ' + 
		'AND RowNumber < (@startingRow + @pageSize) ' + 
		'ORDER BY RowNumber ' 

	EXECUTE sp_executesql @sqlString, @parmDefinition, @startingRow, @pageSize, @status

	-- Get the Error Code for the statement just executed.
	SELECT @errorCode = @@ERROR
END

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Portal
-- * Procedure Name : gensp_Portal_InsertOne
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Portal_InsertOne]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Portal_InsertOne]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Portal_InsertOne]
	@portalID VARCHAR(255),
	@title VARCHAR(150),
	@url VARCHAR(255),
	@siteImage VARCHAR(150),
	@homePageImage VARCHAR(150),
	@stylesheet VARCHAR(150),
	@description VARCHAR(MAX),
	@notificationSetting INT,
	@dateAdded DATETIME,
	@lastModificationTime DATETIME,
	@status INT,
	@portalKey INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON


INSERT INTO [Portal]
(
	[PortalID],
	[Title],
	[Url],
	[SiteImage],
	[HomePageImage],
	[Stylesheet],
	[Description],
	[NotificationSetting],
	[DateAdded],
	[LastModificationTime],
	[Status]
)
VALUES
(
	@portalID,
	@title,
	@url,
	@siteImage,
	@homePageImage,
	@stylesheet,
	@description,
	@notificationSetting,
	@dateAdded,
	@lastModificationTime,
	@status
)
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR
-- Get the IDENTITY value for the row just inserted.
SELECT @portalKey = SCOPE_IDENTITY()

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Portal
-- * Procedure Name : gensp_Portal_UpdateOneByPortalKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Portal_UpdateOneByPortalKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Portal_UpdateOneByPortalKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Portal_UpdateOneByPortalKey]
	@portalKey INT,
	@portalID VARCHAR(255),
	@title VARCHAR(150),
	@url VARCHAR(255),
	@siteImage VARCHAR(150),
	@homePageImage VARCHAR(150),
	@stylesheet VARCHAR(150),
	@description VARCHAR(MAX),
	@notificationSetting INT,
	@dateAdded DATETIME,
	@lastModificationTime DATETIME,
	@status INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

UPDATE [Portal]
SET
	[PortalID] = @portalID,
	[Title] = @title,
	[Url] = @url,
	[SiteImage] = @siteImage,
	[HomePageImage] = @homePageImage,
	[Stylesheet] = @stylesheet,
	[Description] = @description,
	[NotificationSetting] = @notificationSetting,
	[DateAdded] = @dateAdded,
	[LastModificationTime] = @lastModificationTime,
	[Status] = @status
WHERE [Portal].[PortalKey] = @portalKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Portal
-- * Procedure Name : gensp_Portal_DeleteOneByPortalKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Portal_DeleteOneByPortalKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Portal_DeleteOneByPortalKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Portal_DeleteOneByPortalKey]
	@portalKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DELETE FROM [Portal]
WHERE [Portal].[PortalKey] = @portalKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Pricing
-- * Procedure Name : gensp_Pricing_SelectOneByPricingKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Pricing_SelectOneByPricingKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Pricing_SelectOneByPricingKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Pricing_SelectOneByPricingKey]
	@pricingKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON
SELECT
	[Pricing].[PricingKey], 
	[Pricing].[CompanyKey], 
	[Pricing].[PricingTypeKey], 
	[Pricing].[StartAmount], 
	[Pricing].[EndAmount], 
	[Pricing].[Fee], 
	[Pricing].[LastModificationTime], 
	[Pricing].[SortOrder] 
FROM
	[Pricing] 
WHERE [Pricing].[PricingKey] = @pricingKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Pricing
-- * Procedure Name : gensp_Pricing_SelectSomeBySearch
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Pricing_SelectSomeBySearch]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Pricing_SelectSomeBySearch]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Pricing_SelectSomeBySearch]
	@companyKey INT = 0,
	@pricingTypeKey INT = 0,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

SELECT
	[Pricing].[PricingKey], 
	[Pricing].[CompanyKey], 
	[Pricing].[PricingTypeKey], 
	[Pricing].[StartAmount], 
	[Pricing].[EndAmount], 
	[Pricing].[Fee], 
	[Pricing].[LastModificationTime], 
	[Pricing].[SortOrder] 
FROM
	[Pricing] 
WHERE ([Pricing].[CompanyKey] = @companyKey OR @companyKey IS NULL OR @companyKey = 0)
AND ([Pricing].[PricingTypeKey] = @pricingTypeKey OR @pricingTypeKey IS NULL OR @pricingTypeKey = 0)

-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Pricing
-- * Procedure Name : gensp_Pricing_SelectSomeBySearchAndPaging
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Pricing_SelectSomeBySearchAndPaging]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Pricing_SelectSomeBySearchAndPaging]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Pricing_SelectSomeBySearchAndPaging]
	@companyKey INT = 0,
	@pricingTypeKey INT = 0,
	@sortOrder varchar(250) = null,
	@pageSize int = 0,
	@pageNumber int = 0,
	@totalRecordCount INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DECLARE @sqlString nvarchar(4000)
DECLARE @parmDefinition nvarchar(1000)
DECLARE @startingRow int

IF (@pageNumber = 0)
	SET @pageNumber = 1

IF (@pageSize = 0)
	SET @pageSize = 10

IF (@pageNumber = 1)
	SET @startingRow = 1
ELSE
	SET @startingRow = ((@pageNumber - 1) * @pageSize) + 1

-- Add the primary key for better sorting results
IF (ISNULL(@sortOrder, '') = '')
	SET @sortOrder = '[Pricing].[PricingKey]'
ELSE
	SET @sortOrder = @sortOrder + ', [Pricing].[PricingKey]'

SET @parmDefinition = N' @startingRow INT, @pageSize INT, @companyKey INT, @pricingTypeKey INT'

-- Get total records count
SELECT @totalRecordCount = COUNT(*)
FROM
	[Pricing] 
WHERE ([Pricing].[CompanyKey] = @companyKey OR @companyKey IS NULL OR @companyKey = 0)
AND ([Pricing].[PricingTypeKey] = @pricingTypeKey OR @pricingTypeKey IS NULL OR @pricingTypeKey = 0)

-- Determine records exist to retrieve
IF (@totalRecordCount > 0)
BEGIN

	SET @sqlString = N'SELECT * ' + 
		'FROM ( ' + 
		'	SELECT ROW_NUMBER() OVER ( ORDER BY ' + @sortOrder + ' ) AS RowNumber, ' + 
		'		[Pricing].[PricingKey], ' +
		'		[Pricing].[CompanyKey], ' +
		'		[Pricing].[PricingTypeKey], ' +
		'		[Pricing].[StartAmount], ' +
		'		[Pricing].[EndAmount], ' +
		'		[Pricing].[Fee], ' +
		'		[Pricing].[LastModificationTime], ' +
		'		[Pricing].[SortOrder] ' +
		'	FROM [Pricing]  ' + 
		'	WHERE ([Pricing].[CompanyKey] = @companyKey OR @companyKey IS NULL OR @companyKey = 0) ' +
		'	AND ([Pricing].[PricingTypeKey] = @pricingTypeKey OR @pricingTypeKey IS NULL OR @pricingTypeKey = 0) ' +
		') AS FilteredResult ' + 
		'WHERE RowNumber >= @startingRow ' + 
		'AND RowNumber < (@startingRow + @pageSize) ' + 
		'ORDER BY RowNumber ' 

	EXECUTE sp_executesql @sqlString, @parmDefinition, @startingRow, @pageSize, @companyKey, @pricingTypeKey

	-- Get the Error Code for the statement just executed.
	SELECT @errorCode = @@ERROR
END

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Pricing
-- * Procedure Name : gensp_Pricing_InsertOne
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Pricing_InsertOne]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Pricing_InsertOne]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Pricing_InsertOne]
	@companyKey INT,
	@pricingTypeKey INT,
	@startAmount MONEY,
	@endAmount MONEY,
	@fee MONEY,
	@lastModificationTime DATETIME,
	@sortOrder FLOAT(53),
	@pricingKey INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON


INSERT INTO [Pricing]
(
	[CompanyKey],
	[PricingTypeKey],
	[StartAmount],
	[EndAmount],
	[Fee],
	[LastModificationTime],
	[SortOrder]
)
VALUES
(
	@companyKey,
	@pricingTypeKey,
	@startAmount,
	@endAmount,
	@fee,
	@lastModificationTime,
	@sortOrder
)
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR
-- Get the IDENTITY value for the row just inserted.
SELECT @pricingKey = SCOPE_IDENTITY()

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Pricing
-- * Procedure Name : gensp_Pricing_UpdateOneByPricingKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Pricing_UpdateOneByPricingKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Pricing_UpdateOneByPricingKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Pricing_UpdateOneByPricingKey]
	@pricingKey INT,
	@companyKey INT,
	@pricingTypeKey INT,
	@startAmount MONEY,
	@endAmount MONEY,
	@fee MONEY,
	@lastModificationTime DATETIME,
	@sortOrder FLOAT(53),
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

UPDATE [Pricing]
SET
	[CompanyKey] = @companyKey,
	[PricingTypeKey] = @pricingTypeKey,
	[StartAmount] = @startAmount,
	[EndAmount] = @endAmount,
	[Fee] = @fee,
	[LastModificationTime] = @lastModificationTime,
	[SortOrder] = @sortOrder
WHERE [Pricing].[PricingKey] = @pricingKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Pricing
-- * Procedure Name : gensp_Pricing_DeleteOneByPricingKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Pricing_DeleteOneByPricingKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Pricing_DeleteOneByPricingKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Pricing_DeleteOneByPricingKey]
	@pricingKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DELETE FROM [Pricing]
WHERE [Pricing].[PricingKey] = @pricingKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Promotion
-- * Procedure Name : gensp_Promotion_SelectOneByPromotionKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Promotion_SelectOneByPromotionKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Promotion_SelectOneByPromotionKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Promotion_SelectOneByPromotionKey]
	@promotionKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON
SELECT
	[Promotion].[PromotionKey], 
	[Promotion].[CompanyKey], 
	[Promotion].[Title], 
	[Promotion].[PromotionCode], 
	[Promotion].[Amount], 
	[Promotion].[Percentage], 
	[Promotion].[StartDate], 
	[Promotion].[EndDate], 
	[Promotion].[LastModificationTime] 
FROM
	[Promotion] 
WHERE [Promotion].[PromotionKey] = @promotionKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Promotion
-- * Procedure Name : gensp_Promotion_SelectSomeBySearch
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Promotion_SelectSomeBySearch]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Promotion_SelectSomeBySearch]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Promotion_SelectSomeBySearch]
	@companyKey INT = 0,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

SELECT
	[Promotion].[PromotionKey], 
	[Promotion].[CompanyKey], 
	[Promotion].[Title], 
	[Promotion].[PromotionCode], 
	[Promotion].[Amount], 
	[Promotion].[Percentage], 
	[Promotion].[StartDate], 
	[Promotion].[EndDate], 
	[Promotion].[LastModificationTime] 
FROM
	[Promotion] 
WHERE ([Promotion].[CompanyKey] = @companyKey OR @companyKey IS NULL OR @companyKey = 0)

-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Promotion
-- * Procedure Name : gensp_Promotion_SelectSomeBySearchAndPaging
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Promotion_SelectSomeBySearchAndPaging]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Promotion_SelectSomeBySearchAndPaging]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Promotion_SelectSomeBySearchAndPaging]
	@companyKey INT = 0,
	@sortOrder varchar(250) = null,
	@pageSize int = 0,
	@pageNumber int = 0,
	@totalRecordCount INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DECLARE @sqlString nvarchar(4000)
DECLARE @parmDefinition nvarchar(1000)
DECLARE @startingRow int

IF (@pageNumber = 0)
	SET @pageNumber = 1

IF (@pageSize = 0)
	SET @pageSize = 10

IF (@pageNumber = 1)
	SET @startingRow = 1
ELSE
	SET @startingRow = ((@pageNumber - 1) * @pageSize) + 1

-- Add the primary key for better sorting results
IF (ISNULL(@sortOrder, '') = '')
	SET @sortOrder = '[Promotion].[PromotionKey]'
ELSE
	SET @sortOrder = @sortOrder + ', [Promotion].[PromotionKey]'

SET @parmDefinition = N' @startingRow INT, @pageSize INT, @companyKey INT'

-- Get total records count
SELECT @totalRecordCount = COUNT(*)
FROM
	[Promotion] 
WHERE ([Promotion].[CompanyKey] = @companyKey OR @companyKey IS NULL OR @companyKey = 0)

-- Determine records exist to retrieve
IF (@totalRecordCount > 0)
BEGIN

	SET @sqlString = N'SELECT * ' + 
		'FROM ( ' + 
		'	SELECT ROW_NUMBER() OVER ( ORDER BY ' + @sortOrder + ' ) AS RowNumber, ' + 
		'		[Promotion].[PromotionKey], ' +
		'		[Promotion].[CompanyKey], ' +
		'		[Promotion].[Title], ' +
		'		[Promotion].[PromotionCode], ' +
		'		[Promotion].[Amount], ' +
		'		[Promotion].[Percentage], ' +
		'		[Promotion].[StartDate], ' +
		'		[Promotion].[EndDate], ' +
		'		[Promotion].[LastModificationTime] ' +
		'	FROM [Promotion]  ' + 
		'	WHERE ([Promotion].[CompanyKey] = @companyKey OR @companyKey IS NULL OR @companyKey = 0) ' +
		') AS FilteredResult ' + 
		'WHERE RowNumber >= @startingRow ' + 
		'AND RowNumber < (@startingRow + @pageSize) ' + 
		'ORDER BY RowNumber ' 

	EXECUTE sp_executesql @sqlString, @parmDefinition, @startingRow, @pageSize, @companyKey

	-- Get the Error Code for the statement just executed.
	SELECT @errorCode = @@ERROR
END

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Promotion
-- * Procedure Name : gensp_Promotion_InsertOne
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Promotion_InsertOne]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Promotion_InsertOne]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Promotion_InsertOne]
	@companyKey INT,
	@title VARCHAR(150),
	@promotionCode VARCHAR(150),
	@amount MONEY,
	@percentage FLOAT(53),
	@startDate DATETIME,
	@endDate DATETIME,
	@lastModificationTime DATETIME,
	@promotionKey INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON


INSERT INTO [Promotion]
(
	[CompanyKey],
	[Title],
	[PromotionCode],
	[Amount],
	[Percentage],
	[StartDate],
	[EndDate],
	[LastModificationTime]
)
VALUES
(
	@companyKey,
	@title,
	@promotionCode,
	@amount,
	@percentage,
	@startDate,
	@endDate,
	@lastModificationTime
)
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR
-- Get the IDENTITY value for the row just inserted.
SELECT @promotionKey = SCOPE_IDENTITY()

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Promotion
-- * Procedure Name : gensp_Promotion_UpdateOneByPromotionKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Promotion_UpdateOneByPromotionKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Promotion_UpdateOneByPromotionKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Promotion_UpdateOneByPromotionKey]
	@promotionKey INT,
	@companyKey INT,
	@title VARCHAR(150),
	@promotionCode VARCHAR(150),
	@amount MONEY,
	@percentage FLOAT(53),
	@startDate DATETIME,
	@endDate DATETIME,
	@lastModificationTime DATETIME,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

UPDATE [Promotion]
SET
	[CompanyKey] = @companyKey,
	[Title] = @title,
	[PromotionCode] = @promotionCode,
	[Amount] = @amount,
	[Percentage] = @percentage,
	[StartDate] = @startDate,
	[EndDate] = @endDate,
	[LastModificationTime] = @lastModificationTime
WHERE [Promotion].[PromotionKey] = @promotionKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Promotion
-- * Procedure Name : gensp_Promotion_DeleteOneByPromotionKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Promotion_DeleteOneByPromotionKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Promotion_DeleteOneByPromotionKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Promotion_DeleteOneByPromotionKey]
	@promotionKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DELETE FROM [Promotion]
WHERE [Promotion].[PromotionKey] = @promotionKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Property
-- * Procedure Name : gensp_Property_SelectOneByPropertyKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Property_SelectOneByPropertyKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Property_SelectOneByPropertyKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Property_SelectOneByPropertyKey]
	@propertyKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON
SELECT
	[Property].[PropertyKey], 
	[Property].[CompanyKey], 
	[Property].[Title], 
	[Property].[NumberOfUnits], 
	[Property].[Address], 
	[Property].[Address2], 
	[Property].[City], 
	[Property].[State], 
	[Property].[Zip], 
	[Property].[BidRequestAmount], 
	[Property].[MinimumInsuranceAmount], 
	[Property].[Description], 
	[Property].[Status] 
FROM
	[Property] 
WHERE [Property].[PropertyKey] = @propertyKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Property
-- * Procedure Name : gensp_Property_SelectSomeBySearch
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Property_SelectSomeBySearch]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Property_SelectSomeBySearch]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Property_SelectSomeBySearch]
	@companyKey INT = 0,
	@state VARCHAR(2) = '',
	@status INT = 0,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

SELECT
	[Property].[PropertyKey], 
	[Property].[CompanyKey], 
	[Property].[Title], 
	[Property].[NumberOfUnits], 
	[Property].[Address], 
	[Property].[Address2], 
	[Property].[City], 
	[Property].[State], 
	[Property].[Zip], 
	[Property].[BidRequestAmount], 
	[Property].[MinimumInsuranceAmount], 
	[Property].[Description], 
	[Property].[Status] 
FROM
	[Property] 
WHERE ([Property].[CompanyKey] = @companyKey OR @companyKey IS NULL OR @companyKey = 0)
AND ([Property].[State] LIKE @state OR @state IS NULL OR @state = '')
AND ([Property].[Status] & ISNULL(@status, 7) = [Property].[Status])

-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Property
-- * Procedure Name : gensp_Property_SelectSomeBySearchAndPaging
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Property_SelectSomeBySearchAndPaging]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Property_SelectSomeBySearchAndPaging]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Property_SelectSomeBySearchAndPaging]
	@companyKey INT = 0,
	@state VARCHAR(2) = '',
	@status INT = 0,
	@sortOrder varchar(250) = null,
	@pageSize int = 0,
	@pageNumber int = 0,
	@totalRecordCount INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DECLARE @sqlString nvarchar(4000)
DECLARE @parmDefinition nvarchar(1000)
DECLARE @startingRow int

IF (@pageNumber = 0)
	SET @pageNumber = 1

IF (@pageSize = 0)
	SET @pageSize = 10

IF (@pageNumber = 1)
	SET @startingRow = 1
ELSE
	SET @startingRow = ((@pageNumber - 1) * @pageSize) + 1

-- Add the primary key for better sorting results
IF (ISNULL(@sortOrder, '') = '')
	SET @sortOrder = '[Property].[PropertyKey]'
ELSE
	SET @sortOrder = @sortOrder + ', [Property].[PropertyKey]'

SET @parmDefinition = N' @startingRow INT, @pageSize INT, @companyKey INT, @state VARCHAR(2), @status INT'

-- Get total records count
SELECT @totalRecordCount = COUNT(*)
FROM
	[Property] 
WHERE ([Property].[CompanyKey] = @companyKey OR @companyKey IS NULL OR @companyKey = 0)
AND ([Property].[State] LIKE @state OR @state IS NULL OR @state = '')
AND ([Property].[Status] & ISNULL(@status, 7) = [Property].[Status])

-- Determine records exist to retrieve
IF (@totalRecordCount > 0)
BEGIN

	SET @sqlString = N'SELECT * ' + 
		'FROM ( ' + 
		'	SELECT ROW_NUMBER() OVER ( ORDER BY ' + @sortOrder + ' ) AS RowNumber, ' + 
		'		[Property].[PropertyKey], ' +
		'		[Property].[CompanyKey], ' +
		'		[Property].[Title], ' +
		'		[Property].[NumberOfUnits], ' +
		'		[Property].[Address], ' +
		'		[Property].[Address2], ' +
		'		[Property].[City], ' +
		'		[Property].[State], ' +
		'		[Property].[Zip], ' +
		'		[Property].[BidRequestAmount], ' +
		'		[Property].[MinimumInsuranceAmount], ' +
		'		[Property].[Description], ' +
		'		[Property].[Status] ' +
		'	FROM [Property]  ' + 
		'	WHERE ([Property].[CompanyKey] = @companyKey OR @companyKey IS NULL OR @companyKey = 0) ' +
		'	AND ([Property].[State] LIKE @state OR @state IS NULL OR @state = '''') ' +
		'	AND ([Property].[Status] & ISNULL(@status, 7) = [Property].[Status]) ' +
		') AS FilteredResult ' + 
		'WHERE RowNumber >= @startingRow ' + 
		'AND RowNumber < (@startingRow + @pageSize) ' + 
		'ORDER BY RowNumber ' 

	EXECUTE sp_executesql @sqlString, @parmDefinition, @startingRow, @pageSize, @companyKey, @state, @status

	-- Get the Error Code for the statement just executed.
	SELECT @errorCode = @@ERROR
END

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Property
-- * Procedure Name : gensp_Property_InsertOne
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Property_InsertOne]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Property_InsertOne]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Property_InsertOne]
	@companyKey INT,
	@title VARCHAR(150),
	@numberOfUnits INT,
	@address VARCHAR(100),
	@address2 VARCHAR(100),
	@city VARCHAR(50),
	@state VARCHAR(2),
	@zip VARCHAR(11),
	@bidRequestAmount MONEY,
	@minimumInsuranceAmount MONEY,
	@description VARCHAR(MAX),
	@status INT,
	@propertyKey INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON


INSERT INTO [Property]
(
	[CompanyKey],
	[Title],
	[NumberOfUnits],
	[Address],
	[Address2],
	[City],
	[State],
	[Zip],
	[BidRequestAmount],
	[MinimumInsuranceAmount],
	[Description],
	[Status]
)
VALUES
(
	@companyKey,
	@title,
	@numberOfUnits,
	@address,
	@address2,
	@city,
	@state,
	@zip,
	@bidRequestAmount,
	@minimumInsuranceAmount,
	@description,
	@status
)
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR
-- Get the IDENTITY value for the row just inserted.
SELECT @propertyKey = SCOPE_IDENTITY()

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Property
-- * Procedure Name : gensp_Property_UpdateOneByPropertyKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Property_UpdateOneByPropertyKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Property_UpdateOneByPropertyKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Property_UpdateOneByPropertyKey]
	@propertyKey INT,
	@companyKey INT,
	@title VARCHAR(150),
	@numberOfUnits INT,
	@address VARCHAR(100),
	@address2 VARCHAR(100),
	@city VARCHAR(50),
	@state VARCHAR(2),
	@zip VARCHAR(11),
	@bidRequestAmount MONEY,
	@minimumInsuranceAmount MONEY,
	@description VARCHAR(MAX),
	@status INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

UPDATE [Property]
SET
	[CompanyKey] = @companyKey,
	[Title] = @title,
	[NumberOfUnits] = @numberOfUnits,
	[Address] = @address,
	[Address2] = @address2,
	[City] = @city,
	[State] = @state,
	[Zip] = @zip,
	[BidRequestAmount] = @bidRequestAmount,
	[MinimumInsuranceAmount] = @minimumInsuranceAmount,
	[Description] = @description,
	[Status] = @status
WHERE [Property].[PropertyKey] = @propertyKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Property
-- * Procedure Name : gensp_Property_DeleteOneByPropertyKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Property_DeleteOneByPropertyKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Property_DeleteOneByPropertyKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Property_DeleteOneByPropertyKey]
	@propertyKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DELETE FROM [Property]
WHERE [Property].[PropertyKey] = @propertyKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : PropertyResource
-- * Procedure Name : gensp_PropertyResource_SelectOneByPropertyResourceKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_PropertyResource_SelectOneByPropertyResourceKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_PropertyResource_SelectOneByPropertyResourceKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_PropertyResource_SelectOneByPropertyResourceKey]
	@propertyResourceKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON
SELECT
	[PropertyResource].[PropertyResourceKey], 
	[PropertyResource].[PropertyKey], 
	[PropertyResource].[ResourceKey], 
	[PropertyResource].[DateAdded], 
	[PropertyResource].[Status] 
FROM
	[PropertyResource] 
WHERE [PropertyResource].[PropertyResourceKey] = @propertyResourceKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : PropertyResource
-- * Procedure Name : gensp_PropertyResource_SelectSomeBySearch
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_PropertyResource_SelectSomeBySearch]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_PropertyResource_SelectSomeBySearch]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_PropertyResource_SelectSomeBySearch]
	@propertyKey INT = 0,
	@resourceKey INT = 0,
	@status INT = 0,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

SELECT
	[PropertyResource].[PropertyResourceKey], 
	[PropertyResource].[PropertyKey], 
	[PropertyResource].[ResourceKey], 
	[PropertyResource].[DateAdded], 
	[PropertyResource].[Status] 
FROM
	[PropertyResource] 
WHERE ([PropertyResource].[PropertyKey] = @propertyKey OR @propertyKey IS NULL OR @propertyKey = 0)
AND ([PropertyResource].[ResourceKey] = @resourceKey OR @resourceKey IS NULL OR @resourceKey = 0)
AND ([PropertyResource].[Status] & ISNULL(@status, 7) = [PropertyResource].[Status])

-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : PropertyResource
-- * Procedure Name : gensp_PropertyResource_SelectSomeBySearchAndPaging
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_PropertyResource_SelectSomeBySearchAndPaging]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_PropertyResource_SelectSomeBySearchAndPaging]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_PropertyResource_SelectSomeBySearchAndPaging]
	@propertyKey INT = 0,
	@resourceKey INT = 0,
	@status INT = 0,
	@sortOrder varchar(250) = null,
	@pageSize int = 0,
	@pageNumber int = 0,
	@totalRecordCount INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DECLARE @sqlString nvarchar(4000)
DECLARE @parmDefinition nvarchar(1000)
DECLARE @startingRow int

IF (@pageNumber = 0)
	SET @pageNumber = 1

IF (@pageSize = 0)
	SET @pageSize = 10

IF (@pageNumber = 1)
	SET @startingRow = 1
ELSE
	SET @startingRow = ((@pageNumber - 1) * @pageSize) + 1

-- Add the primary key for better sorting results
IF (ISNULL(@sortOrder, '') = '')
	SET @sortOrder = '[PropertyResource].[PropertyResourceKey]'
ELSE
	SET @sortOrder = @sortOrder + ', [PropertyResource].[PropertyResourceKey]'

SET @parmDefinition = N' @startingRow INT, @pageSize INT, @propertyKey INT, @resourceKey INT, @status INT'

-- Get total records count
SELECT @totalRecordCount = COUNT(*)
FROM
	[PropertyResource] 
WHERE ([PropertyResource].[PropertyKey] = @propertyKey OR @propertyKey IS NULL OR @propertyKey = 0)
AND ([PropertyResource].[ResourceKey] = @resourceKey OR @resourceKey IS NULL OR @resourceKey = 0)
AND ([PropertyResource].[Status] & ISNULL(@status, 7) = [PropertyResource].[Status])

-- Determine records exist to retrieve
IF (@totalRecordCount > 0)
BEGIN

	SET @sqlString = N'SELECT * ' + 
		'FROM ( ' + 
		'	SELECT ROW_NUMBER() OVER ( ORDER BY ' + @sortOrder + ' ) AS RowNumber, ' + 
		'		[PropertyResource].[PropertyResourceKey], ' +
		'		[PropertyResource].[PropertyKey], ' +
		'		[PropertyResource].[ResourceKey], ' +
		'		[PropertyResource].[DateAdded], ' +
		'		[PropertyResource].[Status] ' +
		'	FROM [PropertyResource]  ' + 
		'	WHERE ([PropertyResource].[PropertyKey] = @propertyKey OR @propertyKey IS NULL OR @propertyKey = 0) ' +
		'	AND ([PropertyResource].[ResourceKey] = @resourceKey OR @resourceKey IS NULL OR @resourceKey = 0) ' +
		'	AND ([PropertyResource].[Status] & ISNULL(@status, 7) = [PropertyResource].[Status]) ' +
		') AS FilteredResult ' + 
		'WHERE RowNumber >= @startingRow ' + 
		'AND RowNumber < (@startingRow + @pageSize) ' + 
		'ORDER BY RowNumber ' 

	EXECUTE sp_executesql @sqlString, @parmDefinition, @startingRow, @pageSize, @propertyKey, @resourceKey, @status

	-- Get the Error Code for the statement just executed.
	SELECT @errorCode = @@ERROR
END

GO

-- *****************************************************

-- *****************************************************
-- * Table          : PropertyResource
-- * Procedure Name : gensp_PropertyResource_InsertOne
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_PropertyResource_InsertOne]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_PropertyResource_InsertOne]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_PropertyResource_InsertOne]
	@propertyKey INT,
	@resourceKey INT,
	@dateAdded DATETIME,
	@status INT,
	@propertyResourceKey INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON


INSERT INTO [PropertyResource]
(
	[PropertyKey],
	[ResourceKey],
	[DateAdded],
	[Status]
)
VALUES
(
	@propertyKey,
	@resourceKey,
	@dateAdded,
	@status
)
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR
-- Get the IDENTITY value for the row just inserted.
SELECT @propertyResourceKey = SCOPE_IDENTITY()

GO

-- *****************************************************

-- *****************************************************
-- * Table          : PropertyResource
-- * Procedure Name : gensp_PropertyResource_UpdateOneByPropertyResourceKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_PropertyResource_UpdateOneByPropertyResourceKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_PropertyResource_UpdateOneByPropertyResourceKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_PropertyResource_UpdateOneByPropertyResourceKey]
	@propertyResourceKey INT,
	@propertyKey INT,
	@resourceKey INT,
	@dateAdded DATETIME,
	@status INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

UPDATE [PropertyResource]
SET
	[PropertyKey] = @propertyKey,
	[ResourceKey] = @resourceKey,
	[DateAdded] = @dateAdded,
	[Status] = @status
WHERE [PropertyResource].[PropertyResourceKey] = @propertyResourceKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : PropertyResource
-- * Procedure Name : gensp_PropertyResource_DeleteOneByPropertyResourceKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_PropertyResource_DeleteOneByPropertyResourceKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_PropertyResource_DeleteOneByPropertyResourceKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_PropertyResource_DeleteOneByPropertyResourceKey]
	@propertyResourceKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DELETE FROM [PropertyResource]
WHERE [PropertyResource].[PropertyResourceKey] = @propertyResourceKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : PropertyVendorDistance
-- * Procedure Name : gensp_PropertyVendorDistance_SelectOneByPropertyVendorDistanceKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_PropertyVendorDistance_SelectOneByPropertyVendorDistanceKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_PropertyVendorDistance_SelectOneByPropertyVendorDistanceKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_PropertyVendorDistance_SelectOneByPropertyVendorDistanceKey]
	@propertyVendorDistanceKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON
SELECT
	[PropertyVendorDistance].[PropertyVendorDistanceKey], 
	[PropertyVendorDistance].[PropertyKey], 
	[PropertyVendorDistance].[VendorKey], 
	[PropertyVendorDistance].[Distance], 
	[PropertyVendorDistance].[LastModificationTime] 
FROM
	[PropertyVendorDistance] 
WHERE [PropertyVendorDistance].[PropertyVendorDistanceKey] = @propertyVendorDistanceKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : PropertyVendorDistance
-- * Procedure Name : gensp_PropertyVendorDistance_SelectSomeBySearch
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_PropertyVendorDistance_SelectSomeBySearch]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_PropertyVendorDistance_SelectSomeBySearch]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_PropertyVendorDistance_SelectSomeBySearch]
	@propertyKey INT = 0,
	@vendorKey INT = 0,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

SELECT
	[PropertyVendorDistance].[PropertyVendorDistanceKey], 
	[PropertyVendorDistance].[PropertyKey], 
	[PropertyVendorDistance].[VendorKey], 
	[PropertyVendorDistance].[Distance], 
	[PropertyVendorDistance].[LastModificationTime] 
FROM
	[PropertyVendorDistance] 
WHERE ([PropertyVendorDistance].[PropertyKey] = @propertyKey OR @propertyKey IS NULL OR @propertyKey = 0)
AND ([PropertyVendorDistance].[VendorKey] = @vendorKey OR @vendorKey IS NULL OR @vendorKey = 0)

-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : PropertyVendorDistance
-- * Procedure Name : gensp_PropertyVendorDistance_SelectSomeBySearchAndPaging
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_PropertyVendorDistance_SelectSomeBySearchAndPaging]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_PropertyVendorDistance_SelectSomeBySearchAndPaging]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_PropertyVendorDistance_SelectSomeBySearchAndPaging]
	@propertyKey INT = 0,
	@vendorKey INT = 0,
	@sortOrder varchar(250) = null,
	@pageSize int = 0,
	@pageNumber int = 0,
	@totalRecordCount INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DECLARE @sqlString nvarchar(4000)
DECLARE @parmDefinition nvarchar(1000)
DECLARE @startingRow int

IF (@pageNumber = 0)
	SET @pageNumber = 1

IF (@pageSize = 0)
	SET @pageSize = 10

IF (@pageNumber = 1)
	SET @startingRow = 1
ELSE
	SET @startingRow = ((@pageNumber - 1) * @pageSize) + 1

-- Add the primary key for better sorting results
IF (ISNULL(@sortOrder, '') = '')
	SET @sortOrder = '[PropertyVendorDistance].[PropertyVendorDistanceKey]'
ELSE
	SET @sortOrder = @sortOrder + ', [PropertyVendorDistance].[PropertyVendorDistanceKey]'

SET @parmDefinition = N' @startingRow INT, @pageSize INT, @propertyKey INT, @vendorKey INT'

-- Get total records count
SELECT @totalRecordCount = COUNT(*)
FROM
	[PropertyVendorDistance] 
WHERE ([PropertyVendorDistance].[PropertyKey] = @propertyKey OR @propertyKey IS NULL OR @propertyKey = 0)
AND ([PropertyVendorDistance].[VendorKey] = @vendorKey OR @vendorKey IS NULL OR @vendorKey = 0)

-- Determine records exist to retrieve
IF (@totalRecordCount > 0)
BEGIN

	SET @sqlString = N'SELECT * ' + 
		'FROM ( ' + 
		'	SELECT ROW_NUMBER() OVER ( ORDER BY ' + @sortOrder + ' ) AS RowNumber, ' + 
		'		[PropertyVendorDistance].[PropertyVendorDistanceKey], ' +
		'		[PropertyVendorDistance].[PropertyKey], ' +
		'		[PropertyVendorDistance].[VendorKey], ' +
		'		[PropertyVendorDistance].[Distance], ' +
		'		[PropertyVendorDistance].[LastModificationTime] ' +
		'	FROM [PropertyVendorDistance]  ' + 
		'	WHERE ([PropertyVendorDistance].[PropertyKey] = @propertyKey OR @propertyKey IS NULL OR @propertyKey = 0) ' +
		'	AND ([PropertyVendorDistance].[VendorKey] = @vendorKey OR @vendorKey IS NULL OR @vendorKey = 0) ' +
		') AS FilteredResult ' + 
		'WHERE RowNumber >= @startingRow ' + 
		'AND RowNumber < (@startingRow + @pageSize) ' + 
		'ORDER BY RowNumber ' 

	EXECUTE sp_executesql @sqlString, @parmDefinition, @startingRow, @pageSize, @propertyKey, @vendorKey

	-- Get the Error Code for the statement just executed.
	SELECT @errorCode = @@ERROR
END

GO

-- *****************************************************

-- *****************************************************
-- * Table          : PropertyVendorDistance
-- * Procedure Name : gensp_PropertyVendorDistance_InsertOne
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_PropertyVendorDistance_InsertOne]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_PropertyVendorDistance_InsertOne]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_PropertyVendorDistance_InsertOne]
	@propertyKey INT,
	@vendorKey INT,
	@distance FLOAT(53),
	@lastModificationTime DATETIME,
	@propertyVendorDistanceKey INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON


INSERT INTO [PropertyVendorDistance]
(
	[PropertyKey],
	[VendorKey],
	[Distance],
	[LastModificationTime]
)
VALUES
(
	@propertyKey,
	@vendorKey,
	@distance,
	@lastModificationTime
)
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR
-- Get the IDENTITY value for the row just inserted.
SELECT @propertyVendorDistanceKey = SCOPE_IDENTITY()

GO

-- *****************************************************

-- *****************************************************
-- * Table          : PropertyVendorDistance
-- * Procedure Name : gensp_PropertyVendorDistance_UpdateOneByPropertyVendorDistanceKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_PropertyVendorDistance_UpdateOneByPropertyVendorDistanceKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_PropertyVendorDistance_UpdateOneByPropertyVendorDistanceKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_PropertyVendorDistance_UpdateOneByPropertyVendorDistanceKey]
	@propertyVendorDistanceKey INT,
	@propertyKey INT,
	@vendorKey INT,
	@distance FLOAT(53),
	@lastModificationTime DATETIME,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

UPDATE [PropertyVendorDistance]
SET
	[PropertyKey] = @propertyKey,
	[VendorKey] = @vendorKey,
	[Distance] = @distance,
	[LastModificationTime] = @lastModificationTime
WHERE [PropertyVendorDistance].[PropertyVendorDistanceKey] = @propertyVendorDistanceKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : PropertyVendorDistance
-- * Procedure Name : gensp_PropertyVendorDistance_DeleteOneByPropertyVendorDistanceKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_PropertyVendorDistance_DeleteOneByPropertyVendorDistanceKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_PropertyVendorDistance_DeleteOneByPropertyVendorDistanceKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_PropertyVendorDistance_DeleteOneByPropertyVendorDistanceKey]
	@propertyVendorDistanceKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DELETE FROM [PropertyVendorDistance]
WHERE [PropertyVendorDistance].[PropertyVendorDistanceKey] = @propertyVendorDistanceKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Reminder
-- * Procedure Name : gensp_Reminder_SelectOneByReminderKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Reminder_SelectOneByReminderKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Reminder_SelectOneByReminderKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Reminder_SelectOneByReminderKey]
	@reminderKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON
SELECT
	[Reminder].[ReminderKey], 
	[Reminder].[ModuleKey], 
	[Reminder].[ResourceKey], 
	[Reminder].[ObjectKey], 
	[Reminder].[Description], 
	[Reminder].[ReminderDate], 
	[Reminder].[LastModificationTime], 
	[Reminder].[Status] 
FROM
	[Reminder] 
WHERE [Reminder].[ReminderKey] = @reminderKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Reminder
-- * Procedure Name : gensp_Reminder_SelectSomeBySearch
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Reminder_SelectSomeBySearch]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Reminder_SelectSomeBySearch]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Reminder_SelectSomeBySearch]
	@moduleKey INT = 0,
	@resourceKey INT = 0,
	@objectKey INT = 0,
	@status INT = 0,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

SELECT
	[Reminder].[ReminderKey], 
	[Reminder].[ModuleKey], 
	[Reminder].[ResourceKey], 
	[Reminder].[ObjectKey], 
	[Reminder].[Description], 
	[Reminder].[ReminderDate], 
	[Reminder].[LastModificationTime], 
	[Reminder].[Status] 
FROM
	[Reminder] 
WHERE ([Reminder].[ModuleKey] = @moduleKey OR @moduleKey IS NULL OR @moduleKey = 0)
AND ([Reminder].[ResourceKey] = @resourceKey OR @resourceKey IS NULL OR @resourceKey = 0)
AND ([Reminder].[ObjectKey] = @objectKey OR @objectKey IS NULL OR @objectKey = 0)
AND ([Reminder].[Status] & ISNULL(@status, 7) = [Reminder].[Status])

-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Reminder
-- * Procedure Name : gensp_Reminder_SelectSomeBySearchAndPaging
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Reminder_SelectSomeBySearchAndPaging]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Reminder_SelectSomeBySearchAndPaging]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Reminder_SelectSomeBySearchAndPaging]
	@moduleKey INT = 0,
	@resourceKey INT = 0,
	@objectKey INT = 0,
	@status INT = 0,
	@sortOrder varchar(250) = null,
	@pageSize int = 0,
	@pageNumber int = 0,
	@totalRecordCount INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DECLARE @sqlString nvarchar(4000)
DECLARE @parmDefinition nvarchar(1000)
DECLARE @startingRow int

IF (@pageNumber = 0)
	SET @pageNumber = 1

IF (@pageSize = 0)
	SET @pageSize = 10

IF (@pageNumber = 1)
	SET @startingRow = 1
ELSE
	SET @startingRow = ((@pageNumber - 1) * @pageSize) + 1

-- Add the primary key for better sorting results
IF (ISNULL(@sortOrder, '') = '')
	SET @sortOrder = '[Reminder].[ReminderKey]'
ELSE
	SET @sortOrder = @sortOrder + ', [Reminder].[ReminderKey]'

SET @parmDefinition = N' @startingRow INT, @pageSize INT, @moduleKey INT, @resourceKey INT, @objectKey INT, @status INT'

-- Get total records count
SELECT @totalRecordCount = COUNT(*)
FROM
	[Reminder] 
WHERE ([Reminder].[ModuleKey] = @moduleKey OR @moduleKey IS NULL OR @moduleKey = 0)
AND ([Reminder].[ResourceKey] = @resourceKey OR @resourceKey IS NULL OR @resourceKey = 0)
AND ([Reminder].[ObjectKey] = @objectKey OR @objectKey IS NULL OR @objectKey = 0)
AND ([Reminder].[Status] & ISNULL(@status, 7) = [Reminder].[Status])

-- Determine records exist to retrieve
IF (@totalRecordCount > 0)
BEGIN

	SET @sqlString = N'SELECT * ' + 
		'FROM ( ' + 
		'	SELECT ROW_NUMBER() OVER ( ORDER BY ' + @sortOrder + ' ) AS RowNumber, ' + 
		'		[Reminder].[ReminderKey], ' +
		'		[Reminder].[ModuleKey], ' +
		'		[Reminder].[ResourceKey], ' +
		'		[Reminder].[ObjectKey], ' +
		'		[Reminder].[Description], ' +
		'		[Reminder].[ReminderDate], ' +
		'		[Reminder].[LastModificationTime], ' +
		'		[Reminder].[Status] ' +
		'	FROM [Reminder]  ' + 
		'	WHERE ([Reminder].[ModuleKey] = @moduleKey OR @moduleKey IS NULL OR @moduleKey = 0) ' +
		'	AND ([Reminder].[ResourceKey] = @resourceKey OR @resourceKey IS NULL OR @resourceKey = 0) ' +
		'	AND ([Reminder].[ObjectKey] = @objectKey OR @objectKey IS NULL OR @objectKey = 0) ' +
		'	AND ([Reminder].[Status] & ISNULL(@status, 7) = [Reminder].[Status]) ' +
		') AS FilteredResult ' + 
		'WHERE RowNumber >= @startingRow ' + 
		'AND RowNumber < (@startingRow + @pageSize) ' + 
		'ORDER BY RowNumber ' 

	EXECUTE sp_executesql @sqlString, @parmDefinition, @startingRow, @pageSize, @moduleKey, @resourceKey, @objectKey, @status

	-- Get the Error Code for the statement just executed.
	SELECT @errorCode = @@ERROR
END

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Reminder
-- * Procedure Name : gensp_Reminder_InsertOne
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Reminder_InsertOne]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Reminder_InsertOne]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Reminder_InsertOne]
	@moduleKey INT,
	@resourceKey INT,
	@objectKey INT,
	@description VARCHAR(MAX),
	@reminderDate SMALLDATETIME,
	@lastModificationTime DATETIME,
	@status INT,
	@reminderKey INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON


INSERT INTO [Reminder]
(
	[ModuleKey],
	[ResourceKey],
	[ObjectKey],
	[Description],
	[ReminderDate],
	[LastModificationTime],
	[Status]
)
VALUES
(
	@moduleKey,
	@resourceKey,
	@objectKey,
	@description,
	@reminderDate,
	@lastModificationTime,
	@status
)
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR
-- Get the IDENTITY value for the row just inserted.
SELECT @reminderKey = SCOPE_IDENTITY()

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Reminder
-- * Procedure Name : gensp_Reminder_UpdateOneByReminderKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Reminder_UpdateOneByReminderKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Reminder_UpdateOneByReminderKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Reminder_UpdateOneByReminderKey]
	@reminderKey INT,
	@moduleKey INT,
	@resourceKey INT,
	@objectKey INT,
	@description VARCHAR(MAX),
	@reminderDate SMALLDATETIME,
	@lastModificationTime DATETIME,
	@status INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

UPDATE [Reminder]
SET
	[ModuleKey] = @moduleKey,
	[ResourceKey] = @resourceKey,
	[ObjectKey] = @objectKey,
	[Description] = @description,
	[ReminderDate] = @reminderDate,
	[LastModificationTime] = @lastModificationTime,
	[Status] = @status
WHERE [Reminder].[ReminderKey] = @reminderKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Reminder
-- * Procedure Name : gensp_Reminder_DeleteOneByReminderKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Reminder_DeleteOneByReminderKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Reminder_DeleteOneByReminderKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Reminder_DeleteOneByReminderKey]
	@reminderKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DELETE FROM [Reminder]
WHERE [Reminder].[ReminderKey] = @reminderKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Resource
-- * Procedure Name : gensp_Resource_SelectOneByResourceKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Resource_SelectOneByResourceKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Resource_SelectOneByResourceKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Resource_SelectOneByResourceKey]
	@resourceKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON
SELECT
	[Resource].[ResourceKey], 
	[Resource].[CompanyKey], 
	[Resource].[ResourceTypeKey], 
	[Resource].[FirstName], 
	[Resource].[LastName], 
	[Resource].[Title], 
	[Resource].[Email], 
	[Resource].[Email2], 
	[Resource].[CellPhone], 
	[Resource].[HomePhone], 
	[Resource].[HomePhone2], 
	[Resource].[Work], 
	[Resource].[Work2], 
	[Resource].[Fax], 
	[Resource].[Address], 
	[Resource].[Address2], 
	[Resource].[City], 
	[Resource].[State], 
	[Resource].[Zip], 
	[Resource].[PrimaryContact], 
	[Resource].[Description], 
	[Resource].[DateAdded], 
	[Resource].[LastModificationTime], 
	[Resource].[Status] 
FROM
	[Resource] 
WHERE [Resource].[ResourceKey] = @resourceKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Resource
-- * Procedure Name : gensp_Resource_SelectSomeBySearch
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Resource_SelectSomeBySearch]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Resource_SelectSomeBySearch]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Resource_SelectSomeBySearch]
	@companyKey INT = 0,
	@resourceTypeKey INT = 0,
	@state VARCHAR(2) = '',
	@status INT = 0,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

SELECT
	[Resource].[ResourceKey], 
	[Resource].[CompanyKey], 
	[Resource].[ResourceTypeKey], 
	[Resource].[FirstName], 
	[Resource].[LastName], 
	[Resource].[Title], 
	[Resource].[Email], 
	[Resource].[Email2], 
	[Resource].[CellPhone], 
	[Resource].[HomePhone], 
	[Resource].[HomePhone2], 
	[Resource].[Work], 
	[Resource].[Work2], 
	[Resource].[Fax], 
	[Resource].[Address], 
	[Resource].[Address2], 
	[Resource].[City], 
	[Resource].[State], 
	[Resource].[Zip], 
	[Resource].[PrimaryContact], 
	[Resource].[Description], 
	[Resource].[DateAdded], 
	[Resource].[LastModificationTime], 
	[Resource].[Status] 
FROM
	[Resource] 
WHERE ([Resource].[CompanyKey] = @companyKey OR @companyKey IS NULL OR @companyKey = 0)
AND ([Resource].[ResourceTypeKey] = @resourceTypeKey OR @resourceTypeKey IS NULL OR @resourceTypeKey = 0)
AND ([Resource].[State] LIKE @state OR @state IS NULL OR @state = '')
AND ([Resource].[Status] & ISNULL(@status, 7) = [Resource].[Status])

-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Resource
-- * Procedure Name : gensp_Resource_SelectSomeBySearchAndPaging
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Resource_SelectSomeBySearchAndPaging]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Resource_SelectSomeBySearchAndPaging]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Resource_SelectSomeBySearchAndPaging]
	@companyKey INT = 0,
	@resourceTypeKey INT = 0,
	@state VARCHAR(2) = '',
	@status INT = 0,
	@sortOrder varchar(250) = null,
	@pageSize int = 0,
	@pageNumber int = 0,
	@totalRecordCount INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DECLARE @sqlString nvarchar(4000)
DECLARE @parmDefinition nvarchar(1000)
DECLARE @startingRow int

IF (@pageNumber = 0)
	SET @pageNumber = 1

IF (@pageSize = 0)
	SET @pageSize = 10

IF (@pageNumber = 1)
	SET @startingRow = 1
ELSE
	SET @startingRow = ((@pageNumber - 1) * @pageSize) + 1

-- Add the primary key for better sorting results
IF (ISNULL(@sortOrder, '') = '')
	SET @sortOrder = '[Resource].[ResourceKey]'
ELSE
	SET @sortOrder = @sortOrder + ', [Resource].[ResourceKey]'

SET @parmDefinition = N' @startingRow INT, @pageSize INT, @companyKey INT, @resourceTypeKey INT, @state VARCHAR(2), @status INT'

-- Get total records count
SELECT @totalRecordCount = COUNT(*)
FROM
	[Resource] 
WHERE ([Resource].[CompanyKey] = @companyKey OR @companyKey IS NULL OR @companyKey = 0)
AND ([Resource].[ResourceTypeKey] = @resourceTypeKey OR @resourceTypeKey IS NULL OR @resourceTypeKey = 0)
AND ([Resource].[State] LIKE @state OR @state IS NULL OR @state = '')
AND ([Resource].[Status] & ISNULL(@status, 7) = [Resource].[Status])

-- Determine records exist to retrieve
IF (@totalRecordCount > 0)
BEGIN

	SET @sqlString = N'SELECT * ' + 
		'FROM ( ' + 
		'	SELECT ROW_NUMBER() OVER ( ORDER BY ' + @sortOrder + ' ) AS RowNumber, ' + 
		'		[Resource].[ResourceKey], ' +
		'		[Resource].[CompanyKey], ' +
		'		[Resource].[ResourceTypeKey], ' +
		'		[Resource].[FirstName], ' +
		'		[Resource].[LastName], ' +
		'		[Resource].[Title], ' +
		'		[Resource].[Email], ' +
		'		[Resource].[Email2], ' +
		'		[Resource].[CellPhone], ' +
		'		[Resource].[HomePhone], ' +
		'		[Resource].[HomePhone2], ' +
		'		[Resource].[Work], ' +
		'		[Resource].[Work2], ' +
		'		[Resource].[Fax], ' +
		'		[Resource].[Address], ' +
		'		[Resource].[Address2], ' +
		'		[Resource].[City], ' +
		'		[Resource].[State], ' +
		'		[Resource].[Zip], ' +
		'		[Resource].[PrimaryContact], ' +
		'		[Resource].[Description], ' +
		'		[Resource].[DateAdded], ' +
		'		[Resource].[LastModificationTime], ' +
		'		[Resource].[Status] ' +
		'	FROM [Resource]  ' + 
		'	WHERE ([Resource].[CompanyKey] = @companyKey OR @companyKey IS NULL OR @companyKey = 0) ' +
		'	AND ([Resource].[ResourceTypeKey] = @resourceTypeKey OR @resourceTypeKey IS NULL OR @resourceTypeKey = 0) ' +
		'	AND ([Resource].[State] LIKE @state OR @state IS NULL OR @state = '''') ' +
		'	AND ([Resource].[Status] & ISNULL(@status, 7) = [Resource].[Status]) ' +
		') AS FilteredResult ' + 
		'WHERE RowNumber >= @startingRow ' + 
		'AND RowNumber < (@startingRow + @pageSize) ' + 
		'ORDER BY RowNumber ' 

	EXECUTE sp_executesql @sqlString, @parmDefinition, @startingRow, @pageSize, @companyKey, @resourceTypeKey, @state, @status

	-- Get the Error Code for the statement just executed.
	SELECT @errorCode = @@ERROR
END

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Resource
-- * Procedure Name : gensp_Resource_InsertOne
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Resource_InsertOne]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Resource_InsertOne]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Resource_InsertOne]
	@companyKey INT,
	@resourceTypeKey INT,
	@firstName VARCHAR(50),
	@lastName VARCHAR(50),
	@title VARCHAR(150),
	@email VARCHAR(150),
	@email2 VARCHAR(150),
	@cellPhone VARCHAR(50),
	@homePhone VARCHAR(50),
	@homePhone2 VARCHAR(50),
	@work VARCHAR(50),
	@work2 VARCHAR(50),
	@fax VARCHAR(50),
	@address VARCHAR(100),
	@address2 VARCHAR(100),
	@city VARCHAR(50),
	@state VARCHAR(2),
	@zip VARCHAR(11),
	@primaryContact BIT,
	@description VARCHAR(MAX),
	@dateAdded DATETIME,
	@lastModificationTime DATETIME,
	@status INT,
	@resourceKey INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON


INSERT INTO [Resource]
(
	[CompanyKey],
	[ResourceTypeKey],
	[FirstName],
	[LastName],
	[Title],
	[Email],
	[Email2],
	[CellPhone],
	[HomePhone],
	[HomePhone2],
	[Work],
	[Work2],
	[Fax],
	[Address],
	[Address2],
	[City],
	[State],
	[Zip],
	[PrimaryContact],
	[Description],
	[DateAdded],
	[LastModificationTime],
	[Status]
)
VALUES
(
	@companyKey,
	@resourceTypeKey,
	@firstName,
	@lastName,
	@title,
	@email,
	@email2,
	@cellPhone,
	@homePhone,
	@homePhone2,
	@work,
	@work2,
	@fax,
	@address,
	@address2,
	@city,
	@state,
	@zip,
	@primaryContact,
	@description,
	@dateAdded,
	@lastModificationTime,
	@status
)
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR
-- Get the IDENTITY value for the row just inserted.
SELECT @resourceKey = SCOPE_IDENTITY()

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Resource
-- * Procedure Name : gensp_Resource_UpdateOneByResourceKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Resource_UpdateOneByResourceKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Resource_UpdateOneByResourceKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Resource_UpdateOneByResourceKey]
	@resourceKey INT,
	@companyKey INT,
	@resourceTypeKey INT,
	@firstName VARCHAR(50),
	@lastName VARCHAR(50),
	@title VARCHAR(150),
	@email VARCHAR(150),
	@email2 VARCHAR(150),
	@cellPhone VARCHAR(50),
	@homePhone VARCHAR(50),
	@homePhone2 VARCHAR(50),
	@work VARCHAR(50),
	@work2 VARCHAR(50),
	@fax VARCHAR(50),
	@address VARCHAR(100),
	@address2 VARCHAR(100),
	@city VARCHAR(50),
	@state VARCHAR(2),
	@zip VARCHAR(11),
	@primaryContact BIT,
	@description VARCHAR(MAX),
	@dateAdded DATETIME,
	@lastModificationTime DATETIME,
	@status INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

UPDATE [Resource]
SET
	[CompanyKey] = @companyKey,
	[ResourceTypeKey] = @resourceTypeKey,
	[FirstName] = @firstName,
	[LastName] = @lastName,
	[Title] = @title,
	[Email] = @email,
	[Email2] = @email2,
	[CellPhone] = @cellPhone,
	[HomePhone] = @homePhone,
	[HomePhone2] = @homePhone2,
	[Work] = @work,
	[Work2] = @work2,
	[Fax] = @fax,
	[Address] = @address,
	[Address2] = @address2,
	[City] = @city,
	[State] = @state,
	[Zip] = @zip,
	[PrimaryContact] = @primaryContact,
	[Description] = @description,
	[DateAdded] = @dateAdded,
	[LastModificationTime] = @lastModificationTime,
	[Status] = @status
WHERE [Resource].[ResourceKey] = @resourceKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Resource
-- * Procedure Name : gensp_Resource_DeleteOneByResourceKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Resource_DeleteOneByResourceKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Resource_DeleteOneByResourceKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Resource_DeleteOneByResourceKey]
	@resourceKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DELETE FROM [Resource]
WHERE [Resource].[ResourceKey] = @resourceKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Service
-- * Procedure Name : gensp_Service_SelectOneByServiceKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Service_SelectOneByServiceKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Service_SelectOneByServiceKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Service_SelectOneByServiceKey]
	@serviceKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON
SELECT
	[Service].[ServiceKey], 
	[Service].[ParentServiceKey], 
	[Service].[Title], 
	[Service].[Tags] 
FROM
	[Service] 
WHERE [Service].[ServiceKey] = @serviceKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Service
-- * Procedure Name : gensp_Service_SelectSomeBySearch
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Service_SelectSomeBySearch]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Service_SelectSomeBySearch]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Service_SelectSomeBySearch]
	@parentServiceKey INT = 0,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

SELECT
	[Service].[ServiceKey], 
	[Service].[ParentServiceKey], 
	[Service].[Title], 
	[Service].[Tags] 
FROM
	[Service] 
WHERE ([Service].[ParentServiceKey] = @parentServiceKey OR @parentServiceKey IS NULL OR @parentServiceKey = 0)

-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Service
-- * Procedure Name : gensp_Service_SelectSomeBySearchAndPaging
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Service_SelectSomeBySearchAndPaging]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Service_SelectSomeBySearchAndPaging]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Service_SelectSomeBySearchAndPaging]
	@parentServiceKey INT = 0,
	@sortOrder varchar(250) = null,
	@pageSize int = 0,
	@pageNumber int = 0,
	@totalRecordCount INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DECLARE @sqlString nvarchar(4000)
DECLARE @parmDefinition nvarchar(1000)
DECLARE @startingRow int

IF (@pageNumber = 0)
	SET @pageNumber = 1

IF (@pageSize = 0)
	SET @pageSize = 10

IF (@pageNumber = 1)
	SET @startingRow = 1
ELSE
	SET @startingRow = ((@pageNumber - 1) * @pageSize) + 1

-- Add the primary key for better sorting results
IF (ISNULL(@sortOrder, '') = '')
	SET @sortOrder = '[Service].[ServiceKey]'
ELSE
	SET @sortOrder = @sortOrder + ', [Service].[ServiceKey]'

SET @parmDefinition = N' @startingRow INT, @pageSize INT, @parentServiceKey INT'

-- Get total records count
SELECT @totalRecordCount = COUNT(*)
FROM
	[Service] 
WHERE ([Service].[ParentServiceKey] = @parentServiceKey OR @parentServiceKey IS NULL OR @parentServiceKey = 0)

-- Determine records exist to retrieve
IF (@totalRecordCount > 0)
BEGIN

	SET @sqlString = N'SELECT * ' + 
		'FROM ( ' + 
		'	SELECT ROW_NUMBER() OVER ( ORDER BY ' + @sortOrder + ' ) AS RowNumber, ' + 
		'		[Service].[ServiceKey], ' +
		'		[Service].[ParentServiceKey], ' +
		'		[Service].[Title], ' +
		'		[Service].[Tags] ' +
		'	FROM [Service]  ' + 
		'	WHERE ([Service].[ParentServiceKey] = @parentServiceKey OR @parentServiceKey IS NULL OR @parentServiceKey = 0) ' +
		') AS FilteredResult ' + 
		'WHERE RowNumber >= @startingRow ' + 
		'AND RowNumber < (@startingRow + @pageSize) ' + 
		'ORDER BY RowNumber ' 

	EXECUTE sp_executesql @sqlString, @parmDefinition, @startingRow, @pageSize, @parentServiceKey

	-- Get the Error Code for the statement just executed.
	SELECT @errorCode = @@ERROR
END

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Service
-- * Procedure Name : gensp_Service_InsertOne
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Service_InsertOne]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Service_InsertOne]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Service_InsertOne]
	@parentServiceKey INT,
	@title VARCHAR(150),
	@tags VARCHAR(MAX),
	@serviceKey INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON


INSERT INTO [Service]
(
	[ParentServiceKey],
	[Title],
	[Tags]
)
VALUES
(
	@parentServiceKey,
	@title,
	@tags
)
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR
-- Get the IDENTITY value for the row just inserted.
SELECT @serviceKey = SCOPE_IDENTITY()

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Service
-- * Procedure Name : gensp_Service_UpdateOneByServiceKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Service_UpdateOneByServiceKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Service_UpdateOneByServiceKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Service_UpdateOneByServiceKey]
	@serviceKey INT,
	@parentServiceKey INT,
	@title VARCHAR(150),
	@tags VARCHAR(MAX),
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

UPDATE [Service]
SET
	[ParentServiceKey] = @parentServiceKey,
	[Title] = @title,
	[Tags] = @tags
WHERE [Service].[ServiceKey] = @serviceKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Service
-- * Procedure Name : gensp_Service_DeleteOneByServiceKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Service_DeleteOneByServiceKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Service_DeleteOneByServiceKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Service_DeleteOneByServiceKey]
	@serviceKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DELETE FROM [Service]
WHERE [Service].[ServiceKey] = @serviceKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : ServiceArea
-- * Procedure Name : gensp_ServiceArea_SelectOneByServiceAreaKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_ServiceArea_SelectOneByServiceAreaKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_ServiceArea_SelectOneByServiceAreaKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_ServiceArea_SelectOneByServiceAreaKey]
	@serviceAreaKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON
SELECT
	[ServiceArea].[ServiceAreaKey], 
	[ServiceArea].[VendorKey], 
	[ServiceArea].[Address], 
	[ServiceArea].[Address2], 
	[ServiceArea].[City], 
	[ServiceArea].[State], 
	[ServiceArea].[Zip], 
	[ServiceArea].[Latitude], 
	[ServiceArea].[Longitude], 
	[ServiceArea].[Radius] 
FROM
	[ServiceArea] 
WHERE [ServiceArea].[ServiceAreaKey] = @serviceAreaKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : ServiceArea
-- * Procedure Name : gensp_ServiceArea_SelectSomeBySearch
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_ServiceArea_SelectSomeBySearch]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_ServiceArea_SelectSomeBySearch]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_ServiceArea_SelectSomeBySearch]
	@vendorKey INT = 0,
	@state VARCHAR(2) = '',
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

SELECT
	[ServiceArea].[ServiceAreaKey], 
	[ServiceArea].[VendorKey], 
	[ServiceArea].[Address], 
	[ServiceArea].[Address2], 
	[ServiceArea].[City], 
	[ServiceArea].[State], 
	[ServiceArea].[Zip], 
	[ServiceArea].[Latitude], 
	[ServiceArea].[Longitude], 
	[ServiceArea].[Radius] 
FROM
	[ServiceArea] 
WHERE ([ServiceArea].[VendorKey] = @vendorKey OR @vendorKey IS NULL OR @vendorKey = 0)
AND ([ServiceArea].[State] LIKE @state OR @state IS NULL OR @state = '')

-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : ServiceArea
-- * Procedure Name : gensp_ServiceArea_SelectSomeBySearchAndPaging
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_ServiceArea_SelectSomeBySearchAndPaging]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_ServiceArea_SelectSomeBySearchAndPaging]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_ServiceArea_SelectSomeBySearchAndPaging]
	@vendorKey INT = 0,
	@state VARCHAR(2) = '',
	@sortOrder varchar(250) = null,
	@pageSize int = 0,
	@pageNumber int = 0,
	@totalRecordCount INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DECLARE @sqlString nvarchar(4000)
DECLARE @parmDefinition nvarchar(1000)
DECLARE @startingRow int

IF (@pageNumber = 0)
	SET @pageNumber = 1

IF (@pageSize = 0)
	SET @pageSize = 10

IF (@pageNumber = 1)
	SET @startingRow = 1
ELSE
	SET @startingRow = ((@pageNumber - 1) * @pageSize) + 1

-- Add the primary key for better sorting results
IF (ISNULL(@sortOrder, '') = '')
	SET @sortOrder = '[ServiceArea].[ServiceAreaKey]'
ELSE
	SET @sortOrder = @sortOrder + ', [ServiceArea].[ServiceAreaKey]'

SET @parmDefinition = N' @startingRow INT, @pageSize INT, @vendorKey INT, @state VARCHAR(2)'

-- Get total records count
SELECT @totalRecordCount = COUNT(*)
FROM
	[ServiceArea] 
WHERE ([ServiceArea].[VendorKey] = @vendorKey OR @vendorKey IS NULL OR @vendorKey = 0)
AND ([ServiceArea].[State] LIKE @state OR @state IS NULL OR @state = '')

-- Determine records exist to retrieve
IF (@totalRecordCount > 0)
BEGIN

	SET @sqlString = N'SELECT * ' + 
		'FROM ( ' + 
		'	SELECT ROW_NUMBER() OVER ( ORDER BY ' + @sortOrder + ' ) AS RowNumber, ' + 
		'		[ServiceArea].[ServiceAreaKey], ' +
		'		[ServiceArea].[VendorKey], ' +
		'		[ServiceArea].[Address], ' +
		'		[ServiceArea].[Address2], ' +
		'		[ServiceArea].[City], ' +
		'		[ServiceArea].[State], ' +
		'		[ServiceArea].[Zip], ' +
		'		[ServiceArea].[Latitude], ' +
		'		[ServiceArea].[Longitude], ' +
		'		[ServiceArea].[Radius] ' +
		'	FROM [ServiceArea]  ' + 
		'	WHERE ([ServiceArea].[VendorKey] = @vendorKey OR @vendorKey IS NULL OR @vendorKey = 0) ' +
		'	AND ([ServiceArea].[State] LIKE @state OR @state IS NULL OR @state = '''') ' +
		') AS FilteredResult ' + 
		'WHERE RowNumber >= @startingRow ' + 
		'AND RowNumber < (@startingRow + @pageSize) ' + 
		'ORDER BY RowNumber ' 

	EXECUTE sp_executesql @sqlString, @parmDefinition, @startingRow, @pageSize, @vendorKey, @state

	-- Get the Error Code for the statement just executed.
	SELECT @errorCode = @@ERROR
END

GO

-- *****************************************************

-- *****************************************************
-- * Table          : ServiceArea
-- * Procedure Name : gensp_ServiceArea_InsertOne
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_ServiceArea_InsertOne]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_ServiceArea_InsertOne]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_ServiceArea_InsertOne]
	@vendorKey INT,
	@address VARCHAR(100),
	@address2 VARCHAR(100),
	@city VARCHAR(50),
	@state VARCHAR(2),
	@zip VARCHAR(11),
	@latitude FLOAT(53),
	@longitude FLOAT(53),
	@radius INT,
	@serviceAreaKey INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON


INSERT INTO [ServiceArea]
(
	[VendorKey],
	[Address],
	[Address2],
	[City],
	[State],
	[Zip],
	[Latitude],
	[Longitude],
	[Radius]
)
VALUES
(
	@vendorKey,
	@address,
	@address2,
	@city,
	@state,
	@zip,
	@latitude,
	@longitude,
	@radius
)
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR
-- Get the IDENTITY value for the row just inserted.
SELECT @serviceAreaKey = SCOPE_IDENTITY()

GO

-- *****************************************************

-- *****************************************************
-- * Table          : ServiceArea
-- * Procedure Name : gensp_ServiceArea_UpdateOneByServiceAreaKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_ServiceArea_UpdateOneByServiceAreaKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_ServiceArea_UpdateOneByServiceAreaKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_ServiceArea_UpdateOneByServiceAreaKey]
	@serviceAreaKey INT,
	@vendorKey INT,
	@address VARCHAR(100),
	@address2 VARCHAR(100),
	@city VARCHAR(50),
	@state VARCHAR(2),
	@zip VARCHAR(11),
	@latitude FLOAT(53),
	@longitude FLOAT(53),
	@radius INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

UPDATE [ServiceArea]
SET
	[VendorKey] = @vendorKey,
	[Address] = @address,
	[Address2] = @address2,
	[City] = @city,
	[State] = @state,
	[Zip] = @zip,
	[Latitude] = @latitude,
	[Longitude] = @longitude,
	[Radius] = @radius
WHERE [ServiceArea].[ServiceAreaKey] = @serviceAreaKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : ServiceArea
-- * Procedure Name : gensp_ServiceArea_DeleteOneByServiceAreaKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_ServiceArea_DeleteOneByServiceAreaKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_ServiceArea_DeleteOneByServiceAreaKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_ServiceArea_DeleteOneByServiceAreaKey]
	@serviceAreaKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DELETE FROM [ServiceArea]
WHERE [ServiceArea].[ServiceAreaKey] = @serviceAreaKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Session
-- * Procedure Name : gensp_Session_SelectOneBySessionKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Session_SelectOneBySessionKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Session_SelectOneBySessionKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Session_SelectOneBySessionKey]
	@sessionKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON
SELECT
	[Session].[SessionKey], 
	[Session].[SessionID], 
	[Session].[Salt], 
	[Session].[Data], 
	[Session].[LastModificationTime] 
FROM
	[Session] 
WHERE [Session].[SessionKey] = @sessionKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Session
-- * Procedure Name : gensp_Session_SelectSomeBySearch
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Session_SelectSomeBySearch]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Session_SelectSomeBySearch]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Session_SelectSomeBySearch]
	@sessionID UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000',
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

SELECT
	[Session].[SessionKey], 
	[Session].[SessionID], 
	[Session].[Salt], 
	[Session].[Data], 
	[Session].[LastModificationTime] 
FROM
	[Session] 
WHERE ([Session].[SessionID] = @sessionID OR @sessionID IS NULL OR @sessionID = '00000000-0000-0000-0000-000000000000')

-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Session
-- * Procedure Name : gensp_Session_SelectSomeBySearchAndPaging
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Session_SelectSomeBySearchAndPaging]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Session_SelectSomeBySearchAndPaging]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Session_SelectSomeBySearchAndPaging]
	@sessionID UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000',
	@sortOrder varchar(250) = null,
	@pageSize int = 0,
	@pageNumber int = 0,
	@totalRecordCount INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DECLARE @sqlString nvarchar(4000)
DECLARE @parmDefinition nvarchar(1000)
DECLARE @startingRow int

IF (@pageNumber = 0)
	SET @pageNumber = 1

IF (@pageSize = 0)
	SET @pageSize = 10

IF (@pageNumber = 1)
	SET @startingRow = 1
ELSE
	SET @startingRow = ((@pageNumber - 1) * @pageSize) + 1

-- Add the primary key for better sorting results
IF (ISNULL(@sortOrder, '') = '')
	SET @sortOrder = '[Session].[SessionKey]'
ELSE
	SET @sortOrder = @sortOrder + ', [Session].[SessionKey]'

SET @parmDefinition = N' @startingRow INT, @pageSize INT, @sessionID UNIQUEIDENTIFIER'

-- Get total records count
SELECT @totalRecordCount = COUNT(*)
FROM
	[Session] 
WHERE ([Session].[SessionID] = @sessionID OR @sessionID IS NULL OR @sessionID = '00000000-0000-0000-0000-000000000000')

-- Determine records exist to retrieve
IF (@totalRecordCount > 0)
BEGIN

	SET @sqlString = N'SELECT * ' + 
		'FROM ( ' + 
		'	SELECT ROW_NUMBER() OVER ( ORDER BY ' + @sortOrder + ' ) AS RowNumber, ' + 
		'		[Session].[SessionKey], ' +
		'		[Session].[SessionID], ' +
		'		[Session].[Salt], ' +
		'		[Session].[Data], ' +
		'		[Session].[LastModificationTime] ' +
		'	FROM [Session]  ' + 
		'	WHERE ([Session].[SessionID] = @sessionID OR @sessionID IS NULL) ' +
		') AS FilteredResult ' + 
		'WHERE RowNumber >= @startingRow ' + 
		'AND RowNumber < (@startingRow + @pageSize) ' + 
		'ORDER BY RowNumber ' 

	EXECUTE sp_executesql @sqlString, @parmDefinition, @startingRow, @pageSize, @sessionID

	-- Get the Error Code for the statement just executed.
	SELECT @errorCode = @@ERROR
END

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Session
-- * Procedure Name : gensp_Session_InsertOne
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Session_InsertOne]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Session_InsertOne]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Session_InsertOne]
	@sessionID UNIQUEIDENTIFIER,
	@salt VARCHAR(150),
	@data VARCHAR(MAX),
	@lastModificationTime DATETIME,
	@sessionKey INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON


INSERT INTO [Session]
(
	[SessionID],
	[Salt],
	[Data],
	[LastModificationTime]
)
VALUES
(
	@sessionID,
	@salt,
	@data,
	@lastModificationTime
)
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR
-- Get the IDENTITY value for the row just inserted.
SELECT @sessionKey = SCOPE_IDENTITY()

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Session
-- * Procedure Name : gensp_Session_UpdateOneBySessionKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Session_UpdateOneBySessionKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Session_UpdateOneBySessionKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Session_UpdateOneBySessionKey]
	@sessionKey INT,
	@sessionID UNIQUEIDENTIFIER,
	@salt VARCHAR(150),
	@data VARCHAR(MAX),
	@lastModificationTime DATETIME,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

UPDATE [Session]
SET
	[SessionID] = @sessionID,
	[Salt] = @salt,
	[Data] = @data,
	[LastModificationTime] = @lastModificationTime
WHERE [Session].[SessionKey] = @sessionKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Session
-- * Procedure Name : gensp_Session_DeleteOneBySessionKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Session_DeleteOneBySessionKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Session_DeleteOneBySessionKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Session_DeleteOneBySessionKey]
	@sessionKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DELETE FROM [Session]
WHERE [Session].[SessionKey] = @sessionKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : State
-- * Procedure Name : gensp_State_SelectOneByStateKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_State_SelectOneByStateKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_State_SelectOneByStateKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_State_SelectOneByStateKey]
	@stateKey VARCHAR(2),
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON
SELECT
	[State].[StateKey], 
	[State].[Title] 
FROM
	[State] 
WHERE [State].[StateKey] = @stateKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : State
-- * Procedure Name : gensp_State_SelectSomeBySearch
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_State_SelectSomeBySearch]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_State_SelectSomeBySearch]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_State_SelectSomeBySearch]
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

SELECT
	[State].[StateKey], 
	[State].[Title] 
FROM
	[State] 

-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : State
-- * Procedure Name : gensp_State_SelectSomeBySearchAndPaging
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_State_SelectSomeBySearchAndPaging]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_State_SelectSomeBySearchAndPaging]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_State_SelectSomeBySearchAndPaging]
	@sortOrder varchar(250) = null,
	@pageSize int = 0,
	@pageNumber int = 0,
	@totalRecordCount INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DECLARE @sqlString nvarchar(4000)
DECLARE @parmDefinition nvarchar(1000)
DECLARE @startingRow int

IF (@pageNumber = 0)
	SET @pageNumber = 1

IF (@pageSize = 0)
	SET @pageSize = 10

IF (@pageNumber = 1)
	SET @startingRow = 1
ELSE
	SET @startingRow = ((@pageNumber - 1) * @pageSize) + 1

-- Add the primary key for better sorting results
IF (ISNULL(@sortOrder, '') = '')
	SET @sortOrder = '[State].[StateKey]'
ELSE
	SET @sortOrder = @sortOrder + ', [State].[StateKey]'

SET @parmDefinition = N' @startingRow INT, @pageSize INT'

-- Get total records count
SELECT @totalRecordCount = COUNT(*)
FROM
	[State] 

-- Determine records exist to retrieve
IF (@totalRecordCount > 0)
BEGIN

	SET @sqlString = N'SELECT * ' + 
		'FROM ( ' + 
		'	SELECT ROW_NUMBER() OVER ( ORDER BY ' + @sortOrder + ' ) AS RowNumber, ' + 
		'		[State].[StateKey], ' +
		'		[State].[Title] ' +
		'	FROM [State]  ' + 
		') AS FilteredResult ' + 
		'WHERE RowNumber >= @startingRow ' + 
		'AND RowNumber < (@startingRow + @pageSize) ' + 
		'ORDER BY RowNumber ' 

	EXECUTE sp_executesql @sqlString, @parmDefinition, @startingRow, @pageSize

	-- Get the Error Code for the statement just executed.
	SELECT @errorCode = @@ERROR
END

GO

-- *****************************************************

-- *****************************************************
-- * Table          : State
-- * Procedure Name : gensp_State_InsertOne
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_State_InsertOne]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_State_InsertOne]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_State_InsertOne]
	@stateKey VARCHAR(2),
	@title VARCHAR(150),
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON


INSERT INTO [State]
(
	[StateKey],
	[Title]
)
VALUES
(
	@stateKey,
	@title
)
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : State
-- * Procedure Name : gensp_State_UpdateOneByStateKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_State_UpdateOneByStateKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_State_UpdateOneByStateKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_State_UpdateOneByStateKey]
	@stateKey VARCHAR(2),
	@title VARCHAR(150),
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

UPDATE [State]
SET
	[Title] = @title
WHERE [State].[StateKey] = @stateKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : State
-- * Procedure Name : gensp_State_DeleteOneByStateKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_State_DeleteOneByStateKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_State_DeleteOneByStateKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_State_DeleteOneByStateKey]
	@stateKey VARCHAR(2),
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DELETE FROM [State]
WHERE [State].[StateKey] = @stateKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Task
-- * Procedure Name : gensp_Task_SelectOneByTaskKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Task_SelectOneByTaskKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Task_SelectOneByTaskKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Task_SelectOneByTaskKey]
	@taskKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON
SELECT
	[Task].[TaskKey], 
	[Task].[ModuleKey], 
	[Task].[ResourceKey], 
	[Task].[AssignedToKey], 
	[Task].[ObjectKey], 
	[Task].[Subject], 
	[Task].[TaskStatus], 
	[Task].[TaskPriority], 
	[Task].[DueDate], 
	[Task].[StartDate], 
	[Task].[Description], 
	[Task].[DateAdded], 
	[Task].[DateCompleted], 
	[Task].[LastModificationTime] 
FROM
	[Task] 
WHERE [Task].[TaskKey] = @taskKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Task
-- * Procedure Name : gensp_Task_SelectSomeBySearch
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Task_SelectSomeBySearch]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Task_SelectSomeBySearch]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Task_SelectSomeBySearch]
	@moduleKey INT = 0,
	@resourceKey INT = 0,
	@assignedToKey INT = 0,
	@objectKey INT = 0,
	@taskStatus INT = 0,
	@taskPriority INT = 0,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

SELECT
	[Task].[TaskKey], 
	[Task].[ModuleKey], 
	[Task].[ResourceKey], 
	[Task].[AssignedToKey], 
	[Task].[ObjectKey], 
	[Task].[Subject], 
	[Task].[TaskStatus], 
	[Task].[TaskPriority], 
	[Task].[DueDate], 
	[Task].[StartDate], 
	[Task].[Description], 
	[Task].[DateAdded], 
	[Task].[DateCompleted], 
	[Task].[LastModificationTime] 
FROM
	[Task] 
WHERE ([Task].[ModuleKey] = @moduleKey OR @moduleKey IS NULL OR @moduleKey = 0)
AND ([Task].[ResourceKey] = @resourceKey OR @resourceKey IS NULL OR @resourceKey = 0)
AND ([Task].[AssignedToKey] = @assignedToKey OR @assignedToKey IS NULL OR @assignedToKey = 0)
AND ([Task].[ObjectKey] = @objectKey OR @objectKey IS NULL OR @objectKey = 0)
AND ([Task].[TaskStatus] & @taskStatus = [Task].[TaskStatus] OR @taskStatus IS NULL OR @taskStatus = 0)
AND ([Task].[TaskPriority] & @taskPriority = [Task].[TaskPriority] OR @taskPriority IS NULL OR @taskPriority = 0)

-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Task
-- * Procedure Name : gensp_Task_SelectSomeBySearchAndPaging
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Task_SelectSomeBySearchAndPaging]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Task_SelectSomeBySearchAndPaging]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Task_SelectSomeBySearchAndPaging]
	@moduleKey INT = 0,
	@resourceKey INT = 0,
	@assignedToKey INT = 0,
	@objectKey INT = 0,
	@taskStatus INT = 0,
	@taskPriority INT = 0,
	@sortOrder varchar(250) = null,
	@pageSize int = 0,
	@pageNumber int = 0,
	@totalRecordCount INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DECLARE @sqlString nvarchar(4000)
DECLARE @parmDefinition nvarchar(1000)
DECLARE @startingRow int

IF (@pageNumber = 0)
	SET @pageNumber = 1

IF (@pageSize = 0)
	SET @pageSize = 10

IF (@pageNumber = 1)
	SET @startingRow = 1
ELSE
	SET @startingRow = ((@pageNumber - 1) * @pageSize) + 1

-- Add the primary key for better sorting results
IF (ISNULL(@sortOrder, '') = '')
	SET @sortOrder = '[Task].[TaskKey]'
ELSE
	SET @sortOrder = @sortOrder + ', [Task].[TaskKey]'

SET @parmDefinition = N' @startingRow INT, @pageSize INT, @moduleKey INT, @resourceKey INT, @assignedToKey INT, @objectKey INT, @taskStatus INT, @taskPriority INT'

-- Get total records count
SELECT @totalRecordCount = COUNT(*)
FROM
	[Task] 
WHERE ([Task].[ModuleKey] = @moduleKey OR @moduleKey IS NULL OR @moduleKey = 0)
AND ([Task].[ResourceKey] = @resourceKey OR @resourceKey IS NULL OR @resourceKey = 0)
AND ([Task].[AssignedToKey] = @assignedToKey OR @assignedToKey IS NULL OR @assignedToKey = 0)
AND ([Task].[ObjectKey] = @objectKey OR @objectKey IS NULL OR @objectKey = 0)
AND ([Task].[TaskStatus] & @taskStatus = [Task].[TaskStatus] OR @taskStatus IS NULL OR @taskStatus = 0)
AND ([Task].[TaskPriority] & @taskPriority = [Task].[TaskPriority] OR @taskPriority IS NULL OR @taskPriority = 0)

-- Determine records exist to retrieve
IF (@totalRecordCount > 0)
BEGIN

	SET @sqlString = N'SELECT * ' + 
		'FROM ( ' + 
		'	SELECT ROW_NUMBER() OVER ( ORDER BY ' + @sortOrder + ' ) AS RowNumber, ' + 
		'		[Task].[TaskKey], ' +
		'		[Task].[ModuleKey], ' +
		'		[Task].[ResourceKey], ' +
		'		[Task].[AssignedToKey], ' +
		'		[Task].[ObjectKey], ' +
		'		[Task].[Subject], ' +
		'		[Task].[TaskStatus], ' +
		'		[Task].[TaskPriority], ' +
		'		[Task].[DueDate], ' +
		'		[Task].[StartDate], ' +
		'		[Task].[Description], ' +
		'		[Task].[DateAdded], ' +
		'		[Task].[DateCompleted], ' +
		'		[Task].[LastModificationTime] ' +
		'	FROM [Task]  ' + 
		'	WHERE ([Task].[ModuleKey] = @moduleKey OR @moduleKey IS NULL OR @moduleKey = 0) ' +
		'	AND ([Task].[ResourceKey] = @resourceKey OR @resourceKey IS NULL OR @resourceKey = 0) ' +
		'	AND ([Task].[AssignedToKey] = @assignedToKey OR @assignedToKey IS NULL OR @assignedToKey = 0) ' +
		'	AND ([Task].[ObjectKey] = @objectKey OR @objectKey IS NULL OR @objectKey = 0) ' +
		'	AND ([Task].[TaskStatus] & @taskStatus = [Task].[TaskStatus] OR @taskStatus IS NULL OR @taskStatus = 0) ' +
		'	AND ([Task].[TaskPriority] & @taskPriority = [Task].[TaskPriority] OR @taskPriority IS NULL OR @taskPriority = 0) ' +
		') AS FilteredResult ' + 
		'WHERE RowNumber >= @startingRow ' + 
		'AND RowNumber < (@startingRow + @pageSize) ' + 
		'ORDER BY RowNumber ' 

	EXECUTE sp_executesql @sqlString, @parmDefinition, @startingRow, @pageSize, @moduleKey, @resourceKey, @assignedToKey, @objectKey, @taskStatus, @taskPriority

	-- Get the Error Code for the statement just executed.
	SELECT @errorCode = @@ERROR
END

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Task
-- * Procedure Name : gensp_Task_InsertOne
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Task_InsertOne]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Task_InsertOne]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Task_InsertOne]
	@moduleKey INT,
	@resourceKey INT,
	@assignedToKey INT,
	@objectKey INT,
	@subject VARCHAR(150),
	@taskStatus INT,
	@taskPriority INT,
	@dueDate SMALLDATETIME,
	@startDate SMALLDATETIME,
	@description VARCHAR(MAX),
	@dateAdded DATETIME,
	@dateCompleted DATETIME,
	@lastModificationTime DATETIME,
	@taskKey INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON


INSERT INTO [Task]
(
	[ModuleKey],
	[ResourceKey],
	[AssignedToKey],
	[ObjectKey],
	[Subject],
	[TaskStatus],
	[TaskPriority],
	[DueDate],
	[StartDate],
	[Description],
	[DateAdded],
	[DateCompleted],
	[LastModificationTime]
)
VALUES
(
	@moduleKey,
	@resourceKey,
	@assignedToKey,
	@objectKey,
	@subject,
	@taskStatus,
	@taskPriority,
	@dueDate,
	@startDate,
	@description,
	@dateAdded,
	@dateCompleted,
	@lastModificationTime
)
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR
-- Get the IDENTITY value for the row just inserted.
SELECT @taskKey = SCOPE_IDENTITY()

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Task
-- * Procedure Name : gensp_Task_UpdateOneByTaskKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Task_UpdateOneByTaskKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Task_UpdateOneByTaskKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Task_UpdateOneByTaskKey]
	@taskKey INT,
	@moduleKey INT,
	@resourceKey INT,
	@assignedToKey INT,
	@objectKey INT,
	@subject VARCHAR(150),
	@taskStatus INT,
	@taskPriority INT,
	@dueDate SMALLDATETIME,
	@startDate SMALLDATETIME,
	@description VARCHAR(MAX),
	@dateAdded DATETIME,
	@dateCompleted DATETIME,
	@lastModificationTime DATETIME,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

UPDATE [Task]
SET
	[ModuleKey] = @moduleKey,
	[ResourceKey] = @resourceKey,
	[AssignedToKey] = @assignedToKey,
	[ObjectKey] = @objectKey,
	[Subject] = @subject,
	[TaskStatus] = @taskStatus,
	[TaskPriority] = @taskPriority,
	[DueDate] = @dueDate,
	[StartDate] = @startDate,
	[Description] = @description,
	[DateAdded] = @dateAdded,
	[DateCompleted] = @dateCompleted,
	[LastModificationTime] = @lastModificationTime
WHERE [Task].[TaskKey] = @taskKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : Task
-- * Procedure Name : gensp_Task_DeleteOneByTaskKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_Task_DeleteOneByTaskKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_Task_DeleteOneByTaskKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_Task_DeleteOneByTaskKey]
	@taskKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DELETE FROM [Task]
WHERE [Task].[TaskKey] = @taskKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : User
-- * Procedure Name : gensp_User_SelectOneByUserKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_User_SelectOneByUserKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_User_SelectOneByUserKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_User_SelectOneByUserKey]
	@userKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON
SELECT
	[User].[UserKey], 
	[User].[ResourceKey], 
	[User].[Username], 
	[User].[Password], 
	[User].[TokenReset], 
	[User].[ResetExpirationDate], 
	[User].[AccountLocked], 
	[User].[FirstTimeAccess], 
	[User].[DateAdded], 
	[User].[LastModificationTime], 
	[User].[Status] 
FROM
	[User] 
WHERE [User].[UserKey] = @userKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : User
-- * Procedure Name : gensp_User_SelectSomeBySearch
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_User_SelectSomeBySearch]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_User_SelectSomeBySearch]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_User_SelectSomeBySearch]
	@resourceKey INT = 0,
	@username VARCHAR(150) = '',
	@tokenReset VARCHAR(256) = '',
	@status INT = 0,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

SELECT
	[User].[UserKey], 
	[User].[ResourceKey], 
	[User].[Username], 
	[User].[Password], 
	[User].[TokenReset], 
	[User].[ResetExpirationDate], 
	[User].[AccountLocked], 
	[User].[FirstTimeAccess], 
	[User].[DateAdded], 
	[User].[LastModificationTime], 
	[User].[Status] 
FROM
	[User] 
WHERE ([User].[ResourceKey] = @resourceKey OR @resourceKey IS NULL OR @resourceKey = 0)
AND ([User].[Username] & @username = [User].[Username] OR @username IS NULL OR @username = '')
AND ([User].[TokenReset] & @tokenReset = [User].[TokenReset] OR @tokenReset IS NULL OR @tokenReset = '')
AND ([User].[Status] & ISNULL(@status, 7) = [User].[Status])

-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : User
-- * Procedure Name : gensp_User_SelectSomeBySearchAndPaging
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_User_SelectSomeBySearchAndPaging]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_User_SelectSomeBySearchAndPaging]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_User_SelectSomeBySearchAndPaging]
	@resourceKey INT = 0,
	@username VARCHAR(150) = '',
	@tokenReset VARCHAR(256) = '',
	@status INT = 0,
	@sortOrder varchar(250) = null,
	@pageSize int = 0,
	@pageNumber int = 0,
	@totalRecordCount INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DECLARE @sqlString nvarchar(4000)
DECLARE @parmDefinition nvarchar(1000)
DECLARE @startingRow int

IF (@pageNumber = 0)
	SET @pageNumber = 1

IF (@pageSize = 0)
	SET @pageSize = 10

IF (@pageNumber = 1)
	SET @startingRow = 1
ELSE
	SET @startingRow = ((@pageNumber - 1) * @pageSize) + 1

-- Add the primary key for better sorting results
IF (ISNULL(@sortOrder, '') = '')
	SET @sortOrder = '[User].[UserKey]'
ELSE
	SET @sortOrder = @sortOrder + ', [User].[UserKey]'

SET @parmDefinition = N' @startingRow INT, @pageSize INT, @resourceKey INT, @username VARCHAR(150), @tokenReset VARCHAR(256), @status INT'

-- Get total records count
SELECT @totalRecordCount = COUNT(*)
FROM
	[User] 
WHERE ([User].[ResourceKey] = @resourceKey OR @resourceKey IS NULL OR @resourceKey = 0)
AND ([User].[Username] & @username = [User].[Username] OR @username IS NULL OR @username = '')
AND ([User].[TokenReset] & @tokenReset = [User].[TokenReset] OR @tokenReset IS NULL OR @tokenReset = '')
AND ([User].[Status] & ISNULL(@status, 7) = [User].[Status])

-- Determine records exist to retrieve
IF (@totalRecordCount > 0)
BEGIN

	SET @sqlString = N'SELECT * ' + 
		'FROM ( ' + 
		'	SELECT ROW_NUMBER() OVER ( ORDER BY ' + @sortOrder + ' ) AS RowNumber, ' + 
		'		[User].[UserKey], ' +
		'		[User].[ResourceKey], ' +
		'		[User].[Username], ' +
		'		[User].[Password], ' +
		'		[User].[TokenReset], ' +
		'		[User].[ResetExpirationDate], ' +
		'		[User].[AccountLocked], ' +
		'		[User].[FirstTimeAccess], ' +
		'		[User].[DateAdded], ' +
		'		[User].[LastModificationTime], ' +
		'		[User].[Status] ' +
		'	FROM [User]  ' + 
		'	WHERE ([User].[ResourceKey] = @resourceKey OR @resourceKey IS NULL OR @resourceKey = 0) ' +
		'	AND ([User].[Username] & @username = [User].[Username] OR @username IS NULL OR @username = '') ' +
		'	AND ([User].[TokenReset] & @tokenReset = [User].[TokenReset] OR @tokenReset IS NULL OR @tokenReset = '') ' +
		'	AND ([User].[Status] & ISNULL(@status, 7) = [User].[Status]) ' +
		') AS FilteredResult ' + 
		'WHERE RowNumber >= @startingRow ' + 
		'AND RowNumber < (@startingRow + @pageSize) ' + 
		'ORDER BY RowNumber ' 

	EXECUTE sp_executesql @sqlString, @parmDefinition, @startingRow, @pageSize, @resourceKey, @username, @tokenReset, @status

	-- Get the Error Code for the statement just executed.
	SELECT @errorCode = @@ERROR
END

GO

-- *****************************************************

-- *****************************************************
-- * Table          : User
-- * Procedure Name : gensp_User_InsertOne
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_User_InsertOne]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_User_InsertOne]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_User_InsertOne]
	@resourceKey INT,
	@username VARCHAR(150),
	@password VARCHAR(256),
	@tokenReset VARCHAR(256),
	@resetExpirationDate DATETIME,
	@accountLocked BIT,
	@firstTimeAccess BIT,
	@dateAdded DATETIME,
	@lastModificationTime DATETIME,
	@status INT,
	@userKey INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON


INSERT INTO [User]
(
	[ResourceKey],
	[Username],
	[Password],
	[TokenReset],
	[ResetExpirationDate],
	[AccountLocked],
	[FirstTimeAccess],
	[DateAdded],
	[LastModificationTime],
	[Status]
)
VALUES
(
	@resourceKey,
	@username,
	@password,
	@tokenReset,
	@resetExpirationDate,
	@accountLocked,
	@firstTimeAccess,
	@dateAdded,
	@lastModificationTime,
	@status
)
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR
-- Get the IDENTITY value for the row just inserted.
SELECT @userKey = SCOPE_IDENTITY()

GO

-- *****************************************************

-- *****************************************************
-- * Table          : User
-- * Procedure Name : gensp_User_UpdateOneByUserKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_User_UpdateOneByUserKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_User_UpdateOneByUserKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_User_UpdateOneByUserKey]
	@userKey INT,
	@resourceKey INT,
	@username VARCHAR(150),
	@password VARCHAR(256),
	@tokenReset VARCHAR(256),
	@resetExpirationDate DATETIME,
	@accountLocked BIT,
	@firstTimeAccess BIT,
	@dateAdded DATETIME,
	@lastModificationTime DATETIME,
	@status INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

UPDATE [User]
SET
	[ResourceKey] = @resourceKey,
	[Username] = @username,
	[Password] = @password,
	[TokenReset] = @tokenReset,
	[ResetExpirationDate] = @resetExpirationDate,
	[AccountLocked] = @accountLocked,
	[FirstTimeAccess] = @firstTimeAccess,
	[DateAdded] = @dateAdded,
	[LastModificationTime] = @lastModificationTime,
	[Status] = @status
WHERE [User].[UserKey] = @userKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : User
-- * Procedure Name : gensp_User_DeleteOneByUserKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_User_DeleteOneByUserKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_User_DeleteOneByUserKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_User_DeleteOneByUserKey]
	@userKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DELETE FROM [User]
WHERE [User].[UserKey] = @userKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : UserAgreement
-- * Procedure Name : gensp_UserAgreement_SelectOneByUserAgreementKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_UserAgreement_SelectOneByUserAgreementKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_UserAgreement_SelectOneByUserAgreementKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_UserAgreement_SelectOneByUserAgreementKey]
	@userAgreementKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON
SELECT
	[UserAgreement].[UserAgreementKey], 
	[UserAgreement].[UserKey], 
	[UserAgreement].[AgreementKey], 
	[UserAgreement].[DateAdded] 
FROM
	[UserAgreement] 
WHERE [UserAgreement].[UserAgreementKey] = @userAgreementKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : UserAgreement
-- * Procedure Name : gensp_UserAgreement_SelectSomeBySearch
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_UserAgreement_SelectSomeBySearch]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_UserAgreement_SelectSomeBySearch]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_UserAgreement_SelectSomeBySearch]
	@userKey INT = 0,
	@agreementKey INT = 0,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

SELECT
	[UserAgreement].[UserAgreementKey], 
	[UserAgreement].[UserKey], 
	[UserAgreement].[AgreementKey], 
	[UserAgreement].[DateAdded] 
FROM
	[UserAgreement] 
WHERE ([UserAgreement].[UserKey] = @userKey OR @userKey IS NULL OR @userKey = 0)
AND ([UserAgreement].[AgreementKey] = @agreementKey OR @agreementKey IS NULL OR @agreementKey = 0)

-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : UserAgreement
-- * Procedure Name : gensp_UserAgreement_SelectSomeBySearchAndPaging
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_UserAgreement_SelectSomeBySearchAndPaging]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_UserAgreement_SelectSomeBySearchAndPaging]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_UserAgreement_SelectSomeBySearchAndPaging]
	@userKey INT = 0,
	@agreementKey INT = 0,
	@sortOrder varchar(250) = null,
	@pageSize int = 0,
	@pageNumber int = 0,
	@totalRecordCount INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DECLARE @sqlString nvarchar(4000)
DECLARE @parmDefinition nvarchar(1000)
DECLARE @startingRow int

IF (@pageNumber = 0)
	SET @pageNumber = 1

IF (@pageSize = 0)
	SET @pageSize = 10

IF (@pageNumber = 1)
	SET @startingRow = 1
ELSE
	SET @startingRow = ((@pageNumber - 1) * @pageSize) + 1

-- Add the primary key for better sorting results
IF (ISNULL(@sortOrder, '') = '')
	SET @sortOrder = '[UserAgreement].[UserAgreementKey]'
ELSE
	SET @sortOrder = @sortOrder + ', [UserAgreement].[UserAgreementKey]'

SET @parmDefinition = N' @startingRow INT, @pageSize INT, @userKey INT, @agreementKey INT'

-- Get total records count
SELECT @totalRecordCount = COUNT(*)
FROM
	[UserAgreement] 
WHERE ([UserAgreement].[UserKey] = @userKey OR @userKey IS NULL OR @userKey = 0)
AND ([UserAgreement].[AgreementKey] = @agreementKey OR @agreementKey IS NULL OR @agreementKey = 0)

-- Determine records exist to retrieve
IF (@totalRecordCount > 0)
BEGIN

	SET @sqlString = N'SELECT * ' + 
		'FROM ( ' + 
		'	SELECT ROW_NUMBER() OVER ( ORDER BY ' + @sortOrder + ' ) AS RowNumber, ' + 
		'		[UserAgreement].[UserAgreementKey], ' +
		'		[UserAgreement].[UserKey], ' +
		'		[UserAgreement].[AgreementKey], ' +
		'		[UserAgreement].[DateAdded] ' +
		'	FROM [UserAgreement]  ' + 
		'	WHERE ([UserAgreement].[UserKey] = @userKey OR @userKey IS NULL OR @userKey = 0) ' +
		'	AND ([UserAgreement].[AgreementKey] = @agreementKey OR @agreementKey IS NULL OR @agreementKey = 0) ' +
		') AS FilteredResult ' + 
		'WHERE RowNumber >= @startingRow ' + 
		'AND RowNumber < (@startingRow + @pageSize) ' + 
		'ORDER BY RowNumber ' 

	EXECUTE sp_executesql @sqlString, @parmDefinition, @startingRow, @pageSize, @userKey, @agreementKey

	-- Get the Error Code for the statement just executed.
	SELECT @errorCode = @@ERROR
END

GO

-- *****************************************************

-- *****************************************************
-- * Table          : UserAgreement
-- * Procedure Name : gensp_UserAgreement_InsertOne
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_UserAgreement_InsertOne]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_UserAgreement_InsertOne]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_UserAgreement_InsertOne]
	@userKey INT,
	@agreementKey INT,
	@dateAdded DATETIME,
	@userAgreementKey INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON


INSERT INTO [UserAgreement]
(
	[UserKey],
	[AgreementKey],
	[DateAdded]
)
VALUES
(
	@userKey,
	@agreementKey,
	@dateAdded
)
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR
-- Get the IDENTITY value for the row just inserted.
SELECT @userAgreementKey = SCOPE_IDENTITY()

GO

-- *****************************************************

-- *****************************************************
-- * Table          : UserAgreement
-- * Procedure Name : gensp_UserAgreement_UpdateOneByUserAgreementKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_UserAgreement_UpdateOneByUserAgreementKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_UserAgreement_UpdateOneByUserAgreementKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_UserAgreement_UpdateOneByUserAgreementKey]
	@userAgreementKey INT,
	@userKey INT,
	@agreementKey INT,
	@dateAdded DATETIME,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

UPDATE [UserAgreement]
SET
	[UserKey] = @userKey,
	[AgreementKey] = @agreementKey,
	[DateAdded] = @dateAdded
WHERE [UserAgreement].[UserAgreementKey] = @userAgreementKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : UserAgreement
-- * Procedure Name : gensp_UserAgreement_DeleteOneByUserAgreementKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_UserAgreement_DeleteOneByUserAgreementKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_UserAgreement_DeleteOneByUserAgreementKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_UserAgreement_DeleteOneByUserAgreementKey]
	@userAgreementKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DELETE FROM [UserAgreement]
WHERE [UserAgreement].[UserAgreementKey] = @userAgreementKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : VendorRating
-- * Procedure Name : gensp_VendorRating_SelectOneByVendorRatingKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_VendorRating_SelectOneByVendorRatingKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_VendorRating_SelectOneByVendorRatingKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_VendorRating_SelectOneByVendorRatingKey]
	@vendorRatingKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON
SELECT
	[VendorRating].[VendorRatingKey], 
	[VendorRating].[VendorKey], 
	[VendorRating].[ResourceKey], 
	[VendorRating].[RatingOne], 
	[VendorRating].[RatingTwo], 
	[VendorRating].[RatingThree], 
	[VendorRating].[RatingFour], 
	[VendorRating].[RatingFive], 
	[VendorRating].[LastModificationTime] 
FROM
	[VendorRating] 
WHERE [VendorRating].[VendorRatingKey] = @vendorRatingKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : VendorRating
-- * Procedure Name : gensp_VendorRating_SelectSomeBySearch
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_VendorRating_SelectSomeBySearch]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_VendorRating_SelectSomeBySearch]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_VendorRating_SelectSomeBySearch]
	@vendorKey INT = 0,
	@resourceKey INT = 0,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

SELECT
	[VendorRating].[VendorRatingKey], 
	[VendorRating].[VendorKey], 
	[VendorRating].[ResourceKey], 
	[VendorRating].[RatingOne], 
	[VendorRating].[RatingTwo], 
	[VendorRating].[RatingThree], 
	[VendorRating].[RatingFour], 
	[VendorRating].[RatingFive], 
	[VendorRating].[LastModificationTime] 
FROM
	[VendorRating] 
WHERE ([VendorRating].[VendorKey] = @vendorKey OR @vendorKey IS NULL OR @vendorKey = 0)
AND ([VendorRating].[ResourceKey] = @resourceKey OR @resourceKey IS NULL OR @resourceKey = 0)

-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : VendorRating
-- * Procedure Name : gensp_VendorRating_SelectSomeBySearchAndPaging
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_VendorRating_SelectSomeBySearchAndPaging]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_VendorRating_SelectSomeBySearchAndPaging]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_VendorRating_SelectSomeBySearchAndPaging]
	@vendorKey INT = 0,
	@resourceKey INT = 0,
	@sortOrder varchar(250) = null,
	@pageSize int = 0,
	@pageNumber int = 0,
	@totalRecordCount INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DECLARE @sqlString nvarchar(4000)
DECLARE @parmDefinition nvarchar(1000)
DECLARE @startingRow int

IF (@pageNumber = 0)
	SET @pageNumber = 1

IF (@pageSize = 0)
	SET @pageSize = 10

IF (@pageNumber = 1)
	SET @startingRow = 1
ELSE
	SET @startingRow = ((@pageNumber - 1) * @pageSize) + 1

-- Add the primary key for better sorting results
IF (ISNULL(@sortOrder, '') = '')
	SET @sortOrder = '[VendorRating].[VendorRatingKey]'
ELSE
	SET @sortOrder = @sortOrder + ', [VendorRating].[VendorRatingKey]'

SET @parmDefinition = N' @startingRow INT, @pageSize INT, @vendorKey INT, @resourceKey INT'

-- Get total records count
SELECT @totalRecordCount = COUNT(*)
FROM
	[VendorRating] 
WHERE ([VendorRating].[VendorKey] = @vendorKey OR @vendorKey IS NULL OR @vendorKey = 0)
AND ([VendorRating].[ResourceKey] = @resourceKey OR @resourceKey IS NULL OR @resourceKey = 0)

-- Determine records exist to retrieve
IF (@totalRecordCount > 0)
BEGIN

	SET @sqlString = N'SELECT * ' + 
		'FROM ( ' + 
		'	SELECT ROW_NUMBER() OVER ( ORDER BY ' + @sortOrder + ' ) AS RowNumber, ' + 
		'		[VendorRating].[VendorRatingKey], ' +
		'		[VendorRating].[VendorKey], ' +
		'		[VendorRating].[ResourceKey], ' +
		'		[VendorRating].[RatingOne], ' +
		'		[VendorRating].[RatingTwo], ' +
		'		[VendorRating].[RatingThree], ' +
		'		[VendorRating].[RatingFour], ' +
		'		[VendorRating].[RatingFive], ' +
		'		[VendorRating].[LastModificationTime] ' +
		'	FROM [VendorRating]  ' + 
		'	WHERE ([VendorRating].[VendorKey] = @vendorKey OR @vendorKey IS NULL OR @vendorKey = 0) ' +
		'	AND ([VendorRating].[ResourceKey] = @resourceKey OR @resourceKey IS NULL OR @resourceKey = 0) ' +
		') AS FilteredResult ' + 
		'WHERE RowNumber >= @startingRow ' + 
		'AND RowNumber < (@startingRow + @pageSize) ' + 
		'ORDER BY RowNumber ' 

	EXECUTE sp_executesql @sqlString, @parmDefinition, @startingRow, @pageSize, @vendorKey, @resourceKey

	-- Get the Error Code for the statement just executed.
	SELECT @errorCode = @@ERROR
END

GO

-- *****************************************************

-- *****************************************************
-- * Table          : VendorRating
-- * Procedure Name : gensp_VendorRating_InsertOne
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_VendorRating_InsertOne]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_VendorRating_InsertOne]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_VendorRating_InsertOne]
	@vendorKey INT,
	@resourceKey INT,
	@ratingOne INT,
	@ratingTwo INT,
	@ratingThree INT,
	@ratingFour INT,
	@ratingFive INT,
	@lastModificationTime DATETIME,
	@vendorRatingKey INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON


INSERT INTO [VendorRating]
(
	[VendorKey],
	[ResourceKey],
	[RatingOne],
	[RatingTwo],
	[RatingThree],
	[RatingFour],
	[RatingFive],
	[LastModificationTime]
)
VALUES
(
	@vendorKey,
	@resourceKey,
	@ratingOne,
	@ratingTwo,
	@ratingThree,
	@ratingFour,
	@ratingFive,
	@lastModificationTime
)
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR
-- Get the IDENTITY value for the row just inserted.
SELECT @vendorRatingKey = SCOPE_IDENTITY()

GO

-- *****************************************************

-- *****************************************************
-- * Table          : VendorRating
-- * Procedure Name : gensp_VendorRating_UpdateOneByVendorRatingKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_VendorRating_UpdateOneByVendorRatingKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_VendorRating_UpdateOneByVendorRatingKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_VendorRating_UpdateOneByVendorRatingKey]
	@vendorRatingKey INT,
	@vendorKey INT,
	@resourceKey INT,
	@ratingOne INT,
	@ratingTwo INT,
	@ratingThree INT,
	@ratingFour INT,
	@ratingFive INT,
	@lastModificationTime DATETIME,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

UPDATE [VendorRating]
SET
	[VendorKey] = @vendorKey,
	[ResourceKey] = @resourceKey,
	[RatingOne] = @ratingOne,
	[RatingTwo] = @ratingTwo,
	[RatingThree] = @ratingThree,
	[RatingFour] = @ratingFour,
	[RatingFive] = @ratingFive,
	[LastModificationTime] = @lastModificationTime
WHERE [VendorRating].[VendorRatingKey] = @vendorRatingKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : VendorRating
-- * Procedure Name : gensp_VendorRating_DeleteOneByVendorRatingKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_VendorRating_DeleteOneByVendorRatingKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_VendorRating_DeleteOneByVendorRatingKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_VendorRating_DeleteOneByVendorRatingKey]
	@vendorRatingKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DELETE FROM [VendorRating]
WHERE [VendorRating].[VendorRatingKey] = @vendorRatingKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : VendorService
-- * Procedure Name : gensp_VendorService_SelectOneByVendorServiceKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_VendorService_SelectOneByVendorServiceKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_VendorService_SelectOneByVendorServiceKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_VendorService_SelectOneByVendorServiceKey]
	@vendorServiceKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON
SELECT
	[VendorService].[VendorServiceKey], 
	[VendorService].[VendorKey], 
	[VendorService].[ServiceKey], 
	[VendorService].[SortOrder] 
FROM
	[VendorService] 
WHERE [VendorService].[VendorServiceKey] = @vendorServiceKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : VendorService
-- * Procedure Name : gensp_VendorService_SelectSomeBySearch
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_VendorService_SelectSomeBySearch]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_VendorService_SelectSomeBySearch]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_VendorService_SelectSomeBySearch]
	@vendorKey INT = 0,
	@serviceKey INT = 0,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

SELECT
	[VendorService].[VendorServiceKey], 
	[VendorService].[VendorKey], 
	[VendorService].[ServiceKey], 
	[VendorService].[SortOrder] 
FROM
	[VendorService] 
WHERE ([VendorService].[VendorKey] = @vendorKey OR @vendorKey IS NULL OR @vendorKey = 0)
AND ([VendorService].[ServiceKey] = @serviceKey OR @serviceKey IS NULL OR @serviceKey = 0)

-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : VendorService
-- * Procedure Name : gensp_VendorService_SelectSomeBySearchAndPaging
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_VendorService_SelectSomeBySearchAndPaging]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_VendorService_SelectSomeBySearchAndPaging]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_VendorService_SelectSomeBySearchAndPaging]
	@vendorKey INT = 0,
	@serviceKey INT = 0,
	@sortOrder varchar(250) = null,
	@pageSize int = 0,
	@pageNumber int = 0,
	@totalRecordCount INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DECLARE @sqlString nvarchar(4000)
DECLARE @parmDefinition nvarchar(1000)
DECLARE @startingRow int

IF (@pageNumber = 0)
	SET @pageNumber = 1

IF (@pageSize = 0)
	SET @pageSize = 10

IF (@pageNumber = 1)
	SET @startingRow = 1
ELSE
	SET @startingRow = ((@pageNumber - 1) * @pageSize) + 1

-- Add the primary key for better sorting results
IF (ISNULL(@sortOrder, '') = '')
	SET @sortOrder = '[VendorService].[VendorServiceKey]'
ELSE
	SET @sortOrder = @sortOrder + ', [VendorService].[VendorServiceKey]'

SET @parmDefinition = N' @startingRow INT, @pageSize INT, @vendorKey INT, @serviceKey INT'

-- Get total records count
SELECT @totalRecordCount = COUNT(*)
FROM
	[VendorService] 
WHERE ([VendorService].[VendorKey] = @vendorKey OR @vendorKey IS NULL OR @vendorKey = 0)
AND ([VendorService].[ServiceKey] = @serviceKey OR @serviceKey IS NULL OR @serviceKey = 0)

-- Determine records exist to retrieve
IF (@totalRecordCount > 0)
BEGIN

	SET @sqlString = N'SELECT * ' + 
		'FROM ( ' + 
		'	SELECT ROW_NUMBER() OVER ( ORDER BY ' + @sortOrder + ' ) AS RowNumber, ' + 
		'		[VendorService].[VendorServiceKey], ' +
		'		[VendorService].[VendorKey], ' +
		'		[VendorService].[ServiceKey], ' +
		'		[VendorService].[SortOrder] ' +
		'	FROM [VendorService]  ' + 
		'	WHERE ([VendorService].[VendorKey] = @vendorKey OR @vendorKey IS NULL OR @vendorKey = 0) ' +
		'	AND ([VendorService].[ServiceKey] = @serviceKey OR @serviceKey IS NULL OR @serviceKey = 0) ' +
		') AS FilteredResult ' + 
		'WHERE RowNumber >= @startingRow ' + 
		'AND RowNumber < (@startingRow + @pageSize) ' + 
		'ORDER BY RowNumber ' 

	EXECUTE sp_executesql @sqlString, @parmDefinition, @startingRow, @pageSize, @vendorKey, @serviceKey

	-- Get the Error Code for the statement just executed.
	SELECT @errorCode = @@ERROR
END

GO

-- *****************************************************

-- *****************************************************
-- * Table          : VendorService
-- * Procedure Name : gensp_VendorService_InsertOne
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_VendorService_InsertOne]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_VendorService_InsertOne]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_VendorService_InsertOne]
	@vendorKey INT,
	@serviceKey INT,
	@sortOrder FLOAT(53),
	@vendorServiceKey INT OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON


INSERT INTO [VendorService]
(
	[VendorKey],
	[ServiceKey],
	[SortOrder]
)
VALUES
(
	@vendorKey,
	@serviceKey,
	@sortOrder
)
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR
-- Get the IDENTITY value for the row just inserted.
SELECT @vendorServiceKey = SCOPE_IDENTITY()

GO

-- *****************************************************

-- *****************************************************
-- * Table          : VendorService
-- * Procedure Name : gensp_VendorService_UpdateOneByVendorServiceKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_VendorService_UpdateOneByVendorServiceKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_VendorService_UpdateOneByVendorServiceKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_VendorService_UpdateOneByVendorServiceKey]
	@vendorServiceKey INT,
	@vendorKey INT,
	@serviceKey INT,
	@sortOrder FLOAT(53),
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

UPDATE [VendorService]
SET
	[VendorKey] = @vendorKey,
	[ServiceKey] = @serviceKey,
	[SortOrder] = @sortOrder
WHERE [VendorService].[VendorServiceKey] = @vendorServiceKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

-- *****************************************************
-- * Table          : VendorService
-- * Procedure Name : gensp_VendorService_DeleteOneByVendorServiceKey
-- *****************************************************
-- ********************************
-- * Drop If Exists
-- ********************************
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[gensp_VendorService_DeleteOneByVendorServiceKey]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[gensp_VendorService_DeleteOneByVendorServiceKey]
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[gensp_VendorService_DeleteOneByVendorServiceKey]
	@vendorServiceKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

DELETE FROM [VendorService]
WHERE [VendorService].[VendorServiceKey] = @vendorServiceKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR

GO

-- *****************************************************

