USE [AssociationBids]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetLookUpTypeKey]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[GetLookUpTypeKey]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO

CREATE FUNCTION [dbo].[GetLookUpTypeKey](@title varchar(150))
RETURNS INT
AS
BEGIN
	DECLARE @lookUpTypeKey INT		
	SET @lookUpTypeKey = 0
	
	SELECT @lookUpTypeKey = LookUpTypeKey
	FROM LookUpType
	WHERE Title = @title
	
RETURN (@lookUpTypeKey)
END

GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetLookUpKey]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[GetLookUpKey]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO

CREATE FUNCTION [dbo].[GetLookUpKey](@lookUpTypeTitle varchar(150), @lookUpTitle varchar(150))
RETURNS INT
AS
BEGIN
	DECLARE @lookUpKey INT	
	SET @lookUpKey = 0
	
	SELECT @lookUpKey = LookUpKey
	FROM [LookUp]
	WHERE Title = @lookUpTitle
	AND LookUpTypeKey = dbo.GetLookUpTypeKey(@lookUpTypeTitle)
	
RETURN (@lookUpKey)
END

GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetLookUpValue]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[GetLookUpValue]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO

CREATE FUNCTION [dbo].[GetLookUpValue](@lookUpTypeTitle varchar(150), @lookUpTitle varchar(150))
RETURNS INT
AS
BEGIN
	DECLARE @lookUpValue INT	
	SET @lookUpValue = 0
	
	SELECT @lookUpValue = Value
	FROM [LookUp]
	WHERE Title = @lookUpTitle
	AND LookUpTypeKey = dbo.GetLookUpTypeKey(@lookUpTypeTitle)
	
RETURN (@lookUpValue)
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[config_LookUpType_InsertOne]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[config_LookUpType_InsertOne]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[config_LookUpType_InsertOne]
	@title varchar(150),
	@lookUpTypeKey int OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON
SET @lookUpTypeKey = dbo.GetLookUpTypeKey(@title)
IF (@lookUpTypeKey <= 0)
BEGIN	
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
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[config_LookUp_InsertOne]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[config_LookUp_InsertOne]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[config_LookUp_InsertOne]
	@lookUpTypeTitle varchar(150),
	@lookUpTitle varchar(150),
	@lookUpValue int,
	@sortOrder float(53),
	@lookUpKey int OUTPUT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON
DECLARE @lookUpTypeKey INT

SET @lookUpKey = dbo.GetLookUpKey(@lookUpTypeTitle, @lookUpTitle)

IF (@lookUpKey <= 0)
BEGIN
	SET @lookUpTypeKey = dbo.GetLookUpTypeKey(@lookUpTypeTitle)
	
	IF NOT EXISTS (SELECT 1 FROM LookUp WHERE LookUpTypeKey = @lookUpTypeKey)
		SET @lookUpKey = (@lookUpTypeKey * 100)
	ELSE
	BEGIN
		SELECT @lookUpKey = Max(LookUpKey)
		FROM LookUp
		WHERE LookUpTypeKey = @lookUpTypeKey
		
		SET @lookUpKey = @lookUpKey + 1
	END
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
		@lookUpTitle,
		@lookUpValue,
		@sortOrder
	)
	-- Get the Error Code for the statement just executed.
	SELECT @errorCode = @@ERROR
END

GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetModuleKey]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[GetModuleKey]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO

CREATE FUNCTION [dbo].[GetModuleKey](@moduleTitle varchar(150))
RETURNS INT
AS
BEGIN
	DECLARE @moduleKey INT	
	SET @moduleKey = 0
	
	SELECT @moduleKey = ModuleKey
	FROM [Module]
	WHERE Title = @moduleTitle
	
RETURN (@moduleKey)
END

GO



GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[site_Session_SelectOneBySessionID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[site_Session_SelectOneBySessionID]
GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO



-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[site_Session_SelectOneBySessionID]
	@sessionID UNIQUEIDENTIFIER,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON
SELECT
	[Session].SessionKey,
	[Session].SessionID,
	[Session].Salt,
	[Session].Data,
	[Session].LastModificationTime
FROM
	[Session] 
WHERE SessionID = @sessionID

-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR



GO