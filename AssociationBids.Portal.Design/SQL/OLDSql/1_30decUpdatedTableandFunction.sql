use [AssociationBids]
go


CREATE FUNCTION [dbo].[fn_Split](@ValueList VARCHAR(8000), @Delimiter VARCHAR(50) = ',')
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
/****** Object:  UserDefinedFunction [dbo].[GetLookUpKey]    Script Date: 12/30/2020 1:18:11 PM ******/
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
/****** Object:  UserDefinedFunction [dbo].[GetLookUpTypeKey]    Script Date: 12/30/2020 1:18:11 PM ******/
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
/****** Object:  UserDefinedFunction [dbo].[GetLookUpValue]    Script Date: 12/30/2020 1:18:11 PM ******/
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
/****** Object:  UserDefinedFunction [dbo].[GetModuleKey]    Script Date: 12/30/2020 1:18:11 PM ******/
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
/****** Object:  UserDefinedFunction [dbo].[GetSortOrder]    Script Date: 12/30/2020 1:18:11 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE FUNCTION [dbo].[GetSortOrder](@sortOrder VARCHAR(1000))
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
/****** Object:  UserDefinedFunction [dbo].[Split_Int]    Script Date: 12/30/2020 1:18:11 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO


CREATE FUNCTION [dbo].[Split_Int](@ValueList VARCHAR(8000), @Delimiter VARCHAR(50) = ',')
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
/****** Object:  UserDefinedFunction [dbo].[Split_VarChar]    Script Date: 12/30/2020 1:18:11 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO


CREATE FUNCTION [dbo].[Split_VarChar](@ValueList VARCHAR(8000), @Delimiter VARCHAR(50) = ',')
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
/****** Object:  UserDefinedFunction [dbo].[SplitString]    Script Date: 12/30/2020 1:18:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
------  START Procedure SplitString  ------

CREATE FUNCTION [dbo].[SplitString]
(    
      @Input NVARCHAR(MAX),
      @Character CHAR(1)
)
RETURNS @Output TABLE (
      Item NVARCHAR(1000)
)
AS
BEGIN
      DECLARE @StartIndex INT, @EndIndex INT
 
      SET @StartIndex = 1
      IF SUBSTRING(@Input, LEN(@Input) - 1, LEN(@Input)) <> @Character
      BEGIN
            SET @Input = @Input + @Character
      END
 
      WHILE CHARINDEX(@Character, @Input) > 0
      BEGIN
            SET @EndIndex = CHARINDEX(@Character, @Input)
           
            INSERT INTO @Output(Item)
            SELECT ltrim(rtrim(SUBSTRING(@Input, @StartIndex, @EndIndex - 1)))
           
            SET @Input = ltrim(rtrim(SUBSTRING(@Input, @EndIndex + 1, LEN(@Input))))
      END
 
      RETURN
END

------  END Procedure SplitString  ------
GO
/****** Object:  Table [dbo].[ABNotification]    Script Date: 12/30/2020 1:18:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ABNotification](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NotificationType] [varchar](50) NULL,
	[ModuleKey] [int] NULL,
	[ObjectKey] [int] NULL,
	[ByResource] [int] NULL,
	[ForResource] [int] NULL,
	[NotificationText] [nvarchar](max) NULL,
	[DateAdded] [datetime] NULL,
	[LastModificationDate] [datetime] NULL,
	[Status] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Agreement]    Script Date: 12/30/2020 1:18:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Agreement](
	[AgreementKey] [int] IDENTITY(1,1) NOT NULL,
	[PortalKey] [int] NOT NULL,
	[Title] [varchar](150) NOT NULL,
	[AgreementDate] [datetime] NOT NULL,
	[Description] [varchar](max) NULL,
	[LastModificationTime] [datetime] NOT NULL,
	[Status] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[AgreementKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Bid]    Script Date: 12/30/2020 1:18:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bid](
	[BidKey] [int] IDENTITY(1,1) NOT NULL,
	[BidVendorKey] [int] NOT NULL,
	[ResourceKey] [int] NULL,
	[Title] [varchar](150) NOT NULL,
	[Total] [money] NULL,
	[Description] [varchar](max) NULL,
	[LastModificationTime] [datetime] NOT NULL,
	[BidStatus] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[BidKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BidRequest]    Script Date: 12/30/2020 1:18:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BidRequest](
	[BidRequestKey] [int] IDENTITY(1,1) NOT NULL,
	[PropertyKey] [int] NOT NULL,
	[ResourceKey] [int] NOT NULL,
	[ServiceKey] [int] NOT NULL,
	[Title] [varchar](150) NOT NULL,
	[BidDueDate] [smalldatetime] NULL,
	[StartDate] [smalldatetime] NULL,
	[EndDate] [smalldatetime] NULL,
	[Description] [varchar](max) NULL,
	[DateAdded] [datetime] NOT NULL,
	[LastModificationTime] [datetime] NOT NULL,
	[BidRequestStatus] [int] NOT NULL,
	[DefaultRespondByDate] [datetime] NULL,
	[ModuleKey] [int] NULL,
	[ParentBidRequestKey] [numeric](18, 0) NULL,
PRIMARY KEY CLUSTERED 
(
	[BidRequestKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BidVendor]    Script Date: 12/30/2020 1:18:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BidVendor](
	[BidVendorKey] [int] IDENTITY(1,1) NOT NULL,
	[BidRequestKey] [int] NOT NULL,
	[VendorKey] [int] NOT NULL,
	[ResourceKey] [int] NULL,
	[BidVendorID] [varchar](255) NOT NULL,
	[IsAssigned] [bit] NULL,
	[RespondByDate] [datetime] NULL,
	[DateAdded] [datetime] NOT NULL,
	[LastModificationTime] [datetime] NOT NULL,
	[BidVendorStatus] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[BidVendorKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Calendar]    Script Date: 12/30/2020 1:18:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Calendar](
	[CalendarKey] [int] IDENTITY(1,1) NOT NULL,
	[ModuleKey] [int] NOT NULL,
	[ResourceKey] [int] NOT NULL,
	[ObjectKey] [int] NULL,
	[Subject] [varchar](150) NOT NULL,
	[StartDate] [smalldatetime] NOT NULL,
	[EndDate] [smalldatetime] NULL,
	[AllDayEvent] [bit] NULL,
	[Location] [varchar](150) NULL,
	[Description] [varchar](max) NULL,
	[LastModificationTime] [datetime] NOT NULL,
	[Status] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CalendarKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Company]    Script Date: 12/30/2020 1:18:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company](
	[CompanyKey] [int] IDENTITY(1,1) NOT NULL,
	[ParentCompanyKey] [int] NULL,
	[RelatedCompanyKey] [int] NULL,
	[CompanyTypeKey] [int] NOT NULL,
	[PortalKey] [int] NULL,
	[CompanyID] [varchar](255) NULL,
	[Name] [varchar](150) NOT NULL,
	[LegalName] [varchar](150) NULL,
	[TaxID] [varchar](50) NULL,
	[Work] [varchar](50) NULL,
	[Work2] [varchar](50) NULL,
	[Fax] [varchar](50) NULL,
	[Address] [varchar](100) NULL,
	[Address2] [varchar](100) NULL,
	[City] [varchar](50) NULL,
	[State] [varchar](2) NULL,
	[Zip] [varchar](11) NULL,
	[Website] [varchar](255) NULL,
	[Description] [varchar](max) NULL,
	[BidRequestResponseDays] [int] NULL,
	[BidSubmitDays] [int] NULL,
	[BidRequestAmount] [money] NULL,
	[NotificationPreference] [int] NULL,
	[DateAdded] [datetime] NOT NULL,
	[LastModificationTime] [datetime] NOT NULL,
	[Status] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CompanyKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[CompanyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CompanyVendor]    Script Date: 12/30/2020 1:18:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CompanyVendor](
	[CompanyVendorKey] [int] IDENTITY(1,1) NOT NULL,
	[CompanyKey] [int] NOT NULL,
	[VendorKey] [int] NOT NULL,
	[LastModificationTime] [datetime] NOT NULL,
	[Status] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CompanyVendorKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Document]    Script Date: 12/30/2020 1:18:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Document](
	[DocumentKey] [int] IDENTITY(1,1) NOT NULL,
	[ModuleKey] [int] NOT NULL,
	[ObjectKey] [int] NULL,
	[FileName] [varchar](150) NOT NULL,
	[FileSize] [float] NULL,
	[LastModificationTime] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[DocumentKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Email]    Script Date: 12/30/2020 1:18:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Email](
	[EmailKey] [int] IDENTITY(1,1) NOT NULL,
	[ModuleKey] [int] NOT NULL,
	[ResourceKey] [int] NOT NULL,
	[ObjectKey] [int] NULL,
	[From] [varchar](max) NULL,
	[To] [varchar](max) NULL,
	[Cc] [varchar](max) NULL,
	[Bcc] [varchar](max) NULL,
	[Subject] [varchar](max) NULL,
	[Body] [varchar](max) NULL,
	[DateAdded] [datetime] NOT NULL,
	[DateSent] [smalldatetime] NULL,
	[EmailStatus] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[EmailKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmailTemplate]    Script Date: 12/30/2020 1:18:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmailTemplate](
	[EmailTemplateKey] [int] IDENTITY(1,1) NOT NULL,
	[EmailTitle] [nvarchar](200) NULL,
	[EmailSubject] [nvarchar](max) NULL,
	[Body] [nvarchar](max) NULL,
	[DateAdded] [datetime] NULL,
	[lookUpType] [int] NULL,
 CONSTRAINT [PK_EmailTemplate] PRIMARY KEY CLUSTERED 
(
	[EmailTemplateKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ErrorLog]    Script Date: 12/30/2020 1:18:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ErrorLog](
	[ErrorLogKey] [int] IDENTITY(1,1) NOT NULL,
	[Details] [varchar](max) NOT NULL,
	[Session] [varchar](max) NULL,
	[DateAdded] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ErrorLogKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Group]    Script Date: 12/30/2020 1:18:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Group](
	[GroupKey] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](150) NOT NULL,
	[Description] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[GroupKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GroupMember]    Script Date: 12/30/2020 1:18:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GroupMember](
	[GroupMemberKey] [int] IDENTITY(1,1) NOT NULL,
	[GroupKey] [int] NOT NULL,
	[ResourceKey] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[GroupMemberKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GroupModuleAccess]    Script Date: 12/30/2020 1:18:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GroupModuleAccess](
	[GroupModuleAccessKey] [int] IDENTITY(1,1) NOT NULL,
	[PortalKey] [int] NOT NULL,
	[GroupKey] [int] NOT NULL,
	[ModuleKey] [int] NOT NULL,
	[Access] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[GroupModuleAccessKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Insurance]    Script Date: 12/30/2020 1:18:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Insurance](
	[InsuranceKey] [int] IDENTITY(1,1) NOT NULL,
	[VendorKey] [int] NOT NULL,
	[CompanyName] [varchar](150) NOT NULL,
	[PolicyNumber] [varchar](150) NULL,
	[InsuranceAmount] [money] NULL,
	[AgentName] [varchar](150) NULL,
	[Email] [varchar](150) NULL,
	[Work] [varchar](150) NULL,
	[CellPhone] [varchar](150) NULL,
	[Fax] [varchar](150) NULL,
	[Address] [varchar](100) NOT NULL,
	[Address2] [varchar](100) NULL,
	[City] [varchar](50) NOT NULL,
	[State] [varchar](2) NOT NULL,
	[Zip] [varchar](11) NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[RenewalDate] [datetime] NULL,
	[Status] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[InsuranceKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Invoice]    Script Date: 12/30/2020 1:18:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoice](
	[InvoiceKey] [int] IDENTITY(1,1) NOT NULL,
	[VendorKey] [int] NOT NULL,
	[ReferenceNumber] [varchar](150) NULL,
	[TransactionDate] [datetime] NOT NULL,
	[DueDate] [datetime] NOT NULL,
	[Amount] [money] NOT NULL,
	[Balance] [money] NULL,
	[LastModificationTime] [datetime] NOT NULL,
	[Status] [int] NOT NULL,
	[Strip_TokenID] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[InvoiceKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InvoiceLine]    Script Date: 12/30/2020 1:18:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvoiceLine](
	[InvoiceLineKey] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceKey] [int] NOT NULL,
	[Quantity] [int] NULL,
	[Rate] [money] NULL,
	[Amount] [money] NOT NULL,
	[Description] [varchar](5000) NULL,
	[SortOrder] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[InvoiceLineKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoginHistory]    Script Date: 12/30/2020 1:18:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoginHistory](
	[LoginHistoryKey] [int] IDENTITY(1,1) NOT NULL,
	[UserKey] [int] NOT NULL,
	[DateAdded] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[LoginHistoryKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LookUp]    Script Date: 12/30/2020 1:18:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LookUp](
	[LookUpKey] [int] NOT NULL,
	[LookUpTypeKey] [int] NOT NULL,
	[Title] [varchar](150) NOT NULL,
	[Value] [int] NULL,
	[SortOrder] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[LookUpKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LookUpType]    Script Date: 12/30/2020 1:18:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LookUpType](
	[LookUpTypeKey] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](150) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[LookUpTypeKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Membership]    Script Date: 12/30/2020 1:18:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Membership](
	[MembershipKey] [int] IDENTITY(1,1) NOT NULL,
	[VendorKey] [int] NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NULL,
	[RenewalDate] [datetime] NULL,
	[AutomaticRenewal] [bit] NULL,
	[LastModificationTime] [datetime] NOT NULL,
	[RenewalStatus] [bit] NULL,
	[CancelMembership] [bit] NULL,
	[Invoicekey] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[MembershipKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Message]    Script Date: 12/30/2020 1:18:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Message](
	[MessageKey] [int] IDENTITY(1,1) NOT NULL,
	[ModuleKey] [int] NOT NULL,
	[ResourceKey] [int] NOT NULL,
	[ObjectKey] [int] NULL,
	[Body] [varchar](max) NULL,
	[LastModificationTime] [datetime] NOT NULL,
	[MessageStatus] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MessageKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Module]    Script Date: 12/30/2020 1:18:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Module](
	[ModuleKey] [int] NOT NULL,
	[Title] [varchar](150) NOT NULL,
	[Controller] [varchar](150) NULL,
	[Action] [varchar](150) NULL,
	[Image] [varchar](150) NULL,
	[Description] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[ModuleKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Note]    Script Date: 12/30/2020 1:18:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Note](
	[NoteKey] [int] IDENTITY(1,1) NOT NULL,
	[ModuleKey] [int] NOT NULL,
	[ResourceKey] [int] NOT NULL,
	[ObjectKey] [int] NULL,
	[Description] [varchar](max) NOT NULL,
	[LastModificationTime] [datetime] NOT NULL,
	[Status] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[NoteKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payment]    Script Date: 12/30/2020 1:18:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payment](
	[PaymentKey] [int] IDENTITY(1,1) NOT NULL,
	[VendorKey] [int] NOT NULL,
	[PaymentTypeKey] [int] NOT NULL,
	[ReferenceNumber] [varchar](150) NULL,
	[TransactionDate] [datetime] NOT NULL,
	[Amount] [money] NOT NULL,
	[Balance] [money] NULL,
	[Description] [varchar](5000) NULL,
	[LastModificationTime] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PaymentKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PaymentApplied]    Script Date: 12/30/2020 1:18:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentApplied](
	[PaymentAppliedKey] [int] IDENTITY(1,1) NOT NULL,
	[PaymentKey] [int] NOT NULL,
	[InvociceKey] [int] NOT NULL,
	[Amount] [money] NOT NULL,
	[LastModificationTime] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PaymentAppliedKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PaymentMethod]    Script Date: 12/30/2020 1:18:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentMethod](
	[PaymentMethodKey] [int] IDENTITY(1,1) NOT NULL,
	[CompanyKey] [int] NOT NULL,
	[CardHolderFirstName] [nvarchar](max) NOT NULL,
	[CardHolderLastName] [nvarchar](max) NULL,
	[MaskedCCNumber] [nvarchar](max) NOT NULL,
	[StripeTokenID] [nvarchar](max) NOT NULL,
	[AddedOn] [datetime] NOT NULL,
	[AddedByResourceKey] [int] NOT NULL,
	[LastModificationTime] [datetime] NOT NULL,
	[Status] [int] NOT NULL,
	[PrimaryMethod] [bit] NOT NULL,
	[CardExpiryMonth] [nvarchar](50) NULL,
	[CardExpiryYear] [nvarchar](50) NULL,
	[CvvNumber] [nvarchar](max) NULL,
	[PaymentMethodID] [varchar](100) NULL,
 CONSTRAINT [PK_PaymentMethod] PRIMARY KEY CLUSTERED 
(
	[PaymentMethodKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PaymentModel]    Script Date: 12/30/2020 1:18:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentModel](
	[PaymentModelID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[CardNumber] [nvarchar](max) NULL,
	[StripeTokenID] [nvarchar](max) NULL,
	[AddedOn] [datetime] NULL,
	[LastModificationTime] [datetime] NULL,
 CONSTRAINT [PK_PaymentModel_1] PRIMARY KEY CLUSTERED 
(
	[PaymentModelID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Portal]    Script Date: 12/30/2020 1:18:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Portal](
	[PortalKey] [int] IDENTITY(1,1) NOT NULL,
	[PortalID] [varchar](255) NOT NULL,
	[Title] [varchar](150) NOT NULL,
	[Url] [varchar](255) NULL,
	[SiteImage] [varchar](150) NULL,
	[HomePageImage] [varchar](150) NULL,
	[Stylesheet] [varchar](150) NULL,
	[Description] [varchar](max) NULL,
	[NotificationSetting] [int] NULL,
	[DateAdded] [datetime] NOT NULL,
	[LastModificationTime] [datetime] NOT NULL,
	[Status] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PortalKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[PortalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pricing]    Script Date: 12/30/2020 1:18:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pricing](
	[PricingKey] [int] IDENTITY(1,1) NOT NULL,
	[CompanyKey] [int] NOT NULL,
	[PricingTypeKey] [int] NOT NULL,
	[StartAmount] [money] NULL,
	[EndAmount] [money] NULL,
	[Fee] [money] NOT NULL,
	[LastModificationTime] [datetime] NOT NULL,
	[SortOrder] [float] NULL,
	[FeeType] [varchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[PricingKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Promotion]    Script Date: 12/30/2020 1:18:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Promotion](
	[PromotionKey] [int] IDENTITY(1,1) NOT NULL,
	[CompanyKey] [int] NOT NULL,
	[Title] [varchar](150) NOT NULL,
	[PromotionCode] [varchar](150) NOT NULL,
	[Amount] [money] NULL,
	[Percentage] [float] NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NULL,
	[LastModificationTime] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PromotionKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Property]    Script Date: 12/30/2020 1:18:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Property](
	[PropertyKey] [int] IDENTITY(1,1) NOT NULL,
	[CompanyKey] [int] NOT NULL,
	[Title] [varchar](150) NOT NULL,
	[NumberOfUnits] [int] NULL,
	[Address] [varchar](100) NULL,
	[Address2] [varchar](100) NULL,
	[City] [varchar](50) NULL,
	[State] [varchar](2) NULL,
	[Zip] [varchar](11) NULL,
	[BidRequestAmount] [money] NULL,
	[MinimumInsuranceAmount] [money] NULL,
	[Description] [varchar](max) NULL,
	[Status] [int] NOT NULL,
	[Latitude] [varchar](max) NULL,
	[Longitude] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[PropertyKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PropertyResource]    Script Date: 12/30/2020 1:18:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PropertyResource](
	[PropertyResourceKey] [int] IDENTITY(1,1) NOT NULL,
	[PropertyKey] [int] NOT NULL,
	[ResourceKey] [int] NOT NULL,
	[DateAdded] [datetime] NOT NULL,
	[Status] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PropertyResourceKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PropertyVendorDistance]    Script Date: 12/30/2020 1:18:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PropertyVendorDistance](
	[PropertyVendorDistanceKey] [int] IDENTITY(1,1) NOT NULL,
	[PropertyKey] [int] NOT NULL,
	[VendorKey] [int] NOT NULL,
	[Distance] [float] NULL,
	[LastModificationTime] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PropertyVendorDistanceKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PushNotification]    Script Date: 12/30/2020 1:18:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PushNotification](
	[PushNotificationKey] [int] IDENTITY(1,1) NOT NULL,
	[PushNotificationResourceKey] [int] NULL,
	[RegistrationToken] [varchar](500) NULL,
	[PushNotificationType] [varchar](50) NULL,
	[Body] [varchar](500) NULL,
	[DateAdded] [datetime] NULL,
	[DateSent] [smalldatetime] NULL,
	[PushNotificationStatus] [int] NULL,
	[ErrorMessage] [varchar](1000) NULL,
	[ModuleKey] [int] NULL,
	[ObjectKey] [int] NULL,
	[ForResource] [int] NULL,
	[MulticastId] [nvarchar](200) NULL,
	[MessageId] [nvarchar](200) NULL,
	[AbNotificationKey] [int] NULL,
 CONSTRAINT [PK_Notification] PRIMARY KEY CLUSTERED 
(
	[PushNotificationKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PushNotificationTemplate]    Script Date: 12/30/2020 1:18:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PushNotificationTemplate](
	[PushNotificaionTemplateKey] [int] IDENTITY(1,1) NOT NULL,
	[PushNotificationTitle] [varchar](100) NULL,
	[Body] [varchar](500) NULL,
	[DateAdded] [datetime] NULL,
	[NTSubject] [varchar](max) NULL,
	[PushNotificationType] [int] NULL,
 CONSTRAINT [PK_NotificationTemplate] PRIMARY KEY CLUSTERED 
(
	[PushNotificaionTemplateKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RefundRequest]    Script Date: 12/30/2020 1:18:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RefundRequest](
	[RefundRequestKey] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceKey] [int] NOT NULL,
	[VendorKey] [int] NOT NULL,
	[AddedByResourceKey] [int] NOT NULL,
	[TransactionDate] [datetime] NOT NULL,
	[RefundAmount] [money] NOT NULL,
	[LastModificationTime] [datetime] NOT NULL,
	[Reason] [nvarchar](max) NULL,
	[Stripe_TokenID] [nvarchar](max) NULL,
	[MarkAsRefund] [bit] NOT NULL,
	[ReferenceNumber] [nvarchar](50) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reminder]    Script Date: 12/30/2020 1:18:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reminder](
	[ReminderKey] [int] IDENTITY(1,1) NOT NULL,
	[ModuleKey] [int] NOT NULL,
	[ResourceKey] [int] NOT NULL,
	[ObjectKey] [int] NULL,
	[Description] [varchar](max) NULL,
	[ReminderDate] [smalldatetime] NULL,
	[LastModificationTime] [datetime] NOT NULL,
	[Status] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ReminderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Resource]    Script Date: 12/30/2020 1:18:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Resource](
	[ResourceKey] [int] IDENTITY(1,1) NOT NULL,
	[CompanyKey] [int] NOT NULL,
	[ResourceTypeKey] [int] NOT NULL,
	[FirstName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[Title] [varchar](150) NULL,
	[Email] [varchar](150) NULL,
	[Email2] [varchar](150) NULL,
	[CellPhone] [varchar](50) NULL,
	[HomePhone] [varchar](50) NULL,
	[HomePhone2] [varchar](50) NULL,
	[Work] [varchar](50) NULL,
	[Work2] [varchar](50) NULL,
	[Fax] [varchar](50) NULL,
	[Address] [varchar](100) NULL,
	[Address2] [varchar](100) NULL,
	[City] [varchar](50) NULL,
	[State] [varchar](2) NULL,
	[Zip] [varchar](11) NULL,
	[PrimaryContact] [bit] NULL,
	[Description] [varchar](max) NULL,
	[DateAdded] [datetime] NOT NULL,
	[LastModificationTime] [datetime] NOT NULL,
	[Status] [int] NOT NULL,
	[RegistrationToken] [varchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[ResourceKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Service]    Script Date: 12/30/2020 1:18:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Service](
	[ServiceKey] [int] IDENTITY(1,1) NOT NULL,
	[ParentServiceKey] [int] NULL,
	[Title] [varchar](150) NOT NULL,
	[Tags] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[ServiceKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServiceArea]    Script Date: 12/30/2020 1:18:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiceArea](
	[ServiceAreaKey] [int] IDENTITY(1,1) NOT NULL,
	[VendorKey] [int] NOT NULL,
	[Address] [nvarchar](100) NULL,
	[Address2] [varchar](100) NULL,
	[City] [nvarchar](50) NULL,
	[State] [varchar](2) NULL,
	[Zip] [nvarchar](11) NULL,
	[Latitude] [float] NULL,
	[Longitude] [float] NULL,
	[Radius] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ServiceAreaKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Session]    Script Date: 12/30/2020 1:18:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Session](
	[SessionKey] [int] IDENTITY(1,1) NOT NULL,
	[SessionID] [uniqueidentifier] NOT NULL,
	[Salt] [varchar](150) NOT NULL,
	[Data] [varchar](max) NULL,
	[LastModificationTime] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[SessionKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StarVendor]    Script Date: 12/30/2020 1:18:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StarVendor](
	[StarVendorKey] [int] IDENTITY(1,1) NOT NULL,
	[VendorKey] [int] NOT NULL,
	[ResourceKey] [int] NULL,
	[AddedOn] [datetime] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[State]    Script Date: 12/30/2020 1:18:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[State](
	[StateKey] [varchar](2) NOT NULL,
	[Title] [varchar](150) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[StateKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Task]    Script Date: 12/30/2020 1:18:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Task](
	[TaskKey] [int] IDENTITY(1,1) NOT NULL,
	[ModuleKey] [int] NOT NULL,
	[ResourceKey] [int] NOT NULL,
	[AssignedToKey] [int] NULL,
	[ObjectKey] [int] NULL,
	[Subject] [varchar](150) NOT NULL,
	[TaskStatus] [int] NOT NULL,
	[TaskPriority] [int] NOT NULL,
	[DueDate] [smalldatetime] NULL,
	[StartDate] [smalldatetime] NULL,
	[Description] [varchar](max) NULL,
	[DateAdded] [datetime] NOT NULL,
	[DateCompleted] [datetime] NULL,
	[LastModificationTime] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TaskKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblUserSession]    Script Date: 12/30/2020 1:18:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblUserSession](
	[PKID] [int] IDENTITY(1,1) NOT NULL,
	[UserPKID] [int] NULL,
	[Active] [int] NULL,
	[DateStarted] [datetime] NULL,
	[DateEnded] [datetime] NULL,
	[ClientIPAddress] [varchar](50) NULL,
	[ClientHost] [varchar](50) NULL,
	[ClientUserName] [varchar](50) NULL,
	[ClientBrowser] [varchar](100) NULL,
 CONSTRAINT [PK_tblUserSession] PRIMARY KEY CLUSTERED 
(
	[PKID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TearmCondition]    Script Date: 12/30/2020 1:18:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TearmCondition](
	[TearmConditionId] [int] IDENTITY(1,1) NOT NULL,
	[TearmConditionTitle] [nvarchar](100) NULL,
	[TearmConditionBody] [nvarchar](100) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 12/30/2020 1:18:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserKey] [int] IDENTITY(1,1) NOT NULL,
	[ResourceKey] [int] NOT NULL,
	[Username] [varchar](150) NOT NULL,
	[Password] [varchar](256) NOT NULL,
	[TokenReset] [varchar](256) NULL,
	[ResetExpirationDate] [datetime] NULL,
	[AccountLocked] [bit] NULL,
	[FirstTimeAccess] [bit] NULL,
	[DateAdded] [datetime] NOT NULL,
	[LastModificationTime] [datetime] NOT NULL,
	[Status] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserAgreement]    Script Date: 12/30/2020 1:18:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserAgreement](
	[UserAgreementKey] [int] IDENTITY(1,1) NOT NULL,
	[UserKey] [int] NOT NULL,
	[AgreementKey] [int] NOT NULL,
	[DateAdded] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserAgreementKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRegistrationToken]    Script Date: 12/30/2020 1:18:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRegistrationToken](
	[UserKey] [int] NULL,
	[RegistrationToken] [nvarchar](500) NULL,
	[AddedDateTime] [datetime] NULL,
	[PKID] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VendorRating]    Script Date: 12/30/2020 1:18:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VendorRating](
	[VendorRatingKey] [int] IDENTITY(1,1) NOT NULL,
	[VendorKey] [int] NOT NULL,
	[ResourceKey] [int] NULL,
	[RatingOne] [int] NULL,
	[RatingTwo] [int] NULL,
	[RatingThree] [int] NULL,
	[RatingFour] [int] NULL,
	[RatingFive] [int] NULL,
	[LastModificationTime] [datetime] NOT NULL,
	[Message] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[VendorRatingKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VendorService]    Script Date: 12/30/2020 1:18:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VendorService](
	[VendorServiceKey] [int] IDENTITY(1,1) NOT NULL,
	[VendorKey] [int] NOT NULL,
	[ServiceKey] [int] NOT NULL,
	[SortOrder] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[VendorServiceKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Agreement] ADD  DEFAULT (getdate()) FOR [AgreementDate]
GO
ALTER TABLE [dbo].[Agreement] ADD  DEFAULT (getdate()) FOR [LastModificationTime]
GO
ALTER TABLE [dbo].[Bid] ADD  DEFAULT (getdate()) FOR [LastModificationTime]
GO
ALTER TABLE [dbo].[BidRequest] ADD  DEFAULT (getdate()) FOR [DateAdded]
GO
ALTER TABLE [dbo].[BidRequest] ADD  DEFAULT (getdate()) FOR [LastModificationTime]
GO
ALTER TABLE [dbo].[BidVendor] ADD  DEFAULT (getdate()) FOR [DateAdded]
GO
ALTER TABLE [dbo].[BidVendor] ADD  DEFAULT (getdate()) FOR [LastModificationTime]
GO
ALTER TABLE [dbo].[Calendar] ADD  DEFAULT (getdate()) FOR [LastModificationTime]
GO
ALTER TABLE [dbo].[Company] ADD  DEFAULT (getdate()) FOR [DateAdded]
GO
ALTER TABLE [dbo].[Company] ADD  DEFAULT (getdate()) FOR [LastModificationTime]
GO
ALTER TABLE [dbo].[CompanyVendor] ADD  DEFAULT (getdate()) FOR [LastModificationTime]
GO
ALTER TABLE [dbo].[Document] ADD  DEFAULT (getdate()) FOR [LastModificationTime]
GO
ALTER TABLE [dbo].[Email] ADD  DEFAULT (getdate()) FOR [DateAdded]
GO
ALTER TABLE [dbo].[ErrorLog] ADD  DEFAULT (getdate()) FOR [DateAdded]
GO
ALTER TABLE [dbo].[Invoice] ADD  DEFAULT (getdate()) FOR [TransactionDate]
GO
ALTER TABLE [dbo].[Invoice] ADD  DEFAULT (getdate()) FOR [DueDate]
GO
ALTER TABLE [dbo].[Invoice] ADD  DEFAULT (getdate()) FOR [LastModificationTime]
GO
ALTER TABLE [dbo].[LoginHistory] ADD  DEFAULT (getdate()) FOR [DateAdded]
GO
ALTER TABLE [dbo].[Membership] ADD  DEFAULT (getdate()) FOR [LastModificationTime]
GO
ALTER TABLE [dbo].[Message] ADD  DEFAULT (getdate()) FOR [LastModificationTime]
GO
ALTER TABLE [dbo].[Note] ADD  DEFAULT (getdate()) FOR [LastModificationTime]
GO
ALTER TABLE [dbo].[Payment] ADD  DEFAULT (getdate()) FOR [TransactionDate]
GO
ALTER TABLE [dbo].[Payment] ADD  DEFAULT (getdate()) FOR [LastModificationTime]
GO
ALTER TABLE [dbo].[PaymentApplied] ADD  DEFAULT (getdate()) FOR [LastModificationTime]
GO
ALTER TABLE [dbo].[Portal] ADD  DEFAULT (getdate()) FOR [DateAdded]
GO
ALTER TABLE [dbo].[Portal] ADD  DEFAULT (getdate()) FOR [LastModificationTime]
GO
ALTER TABLE [dbo].[Pricing] ADD  DEFAULT (getdate()) FOR [LastModificationTime]
GO
ALTER TABLE [dbo].[Pricing] ADD  DEFAULT ('Fixed') FOR [FeeType]
GO
ALTER TABLE [dbo].[Promotion] ADD  DEFAULT (getdate()) FOR [LastModificationTime]
GO
ALTER TABLE [dbo].[PropertyResource] ADD  DEFAULT (getdate()) FOR [DateAdded]
GO
ALTER TABLE [dbo].[PropertyVendorDistance] ADD  DEFAULT (getdate()) FOR [LastModificationTime]
GO
ALTER TABLE [dbo].[Reminder] ADD  DEFAULT (getdate()) FOR [LastModificationTime]
GO
ALTER TABLE [dbo].[Resource] ADD  DEFAULT (getdate()) FOR [DateAdded]
GO
ALTER TABLE [dbo].[Resource] ADD  DEFAULT (getdate()) FOR [LastModificationTime]
GO
ALTER TABLE [dbo].[Session] ADD  DEFAULT (getdate()) FOR [LastModificationTime]
GO
ALTER TABLE [dbo].[StarVendor] ADD  DEFAULT (getdate()) FOR [AddedOn]
GO
ALTER TABLE [dbo].[Task] ADD  DEFAULT (getdate()) FOR [DateAdded]
GO
ALTER TABLE [dbo].[Task] ADD  DEFAULT (getdate()) FOR [LastModificationTime]
GO
ALTER TABLE [dbo].[User] ADD  DEFAULT (getdate()) FOR [DateAdded]
GO
ALTER TABLE [dbo].[User] ADD  DEFAULT (getdate()) FOR [LastModificationTime]
GO
ALTER TABLE [dbo].[UserAgreement] ADD  DEFAULT (getdate()) FOR [DateAdded]
GO
ALTER TABLE [dbo].[UserRegistrationToken] ADD  DEFAULT ((-1)) FOR [PKID]
GO
ALTER TABLE [dbo].[VendorRating] ADD  DEFAULT (getdate()) FOR [LastModificationTime]
GO
ALTER TABLE [dbo].[ABNotification]  WITH CHECK ADD  CONSTRAINT [FK_Noti_ModuleKey] FOREIGN KEY([ModuleKey])
REFERENCES [dbo].[Module] ([ModuleKey])
GO
ALTER TABLE [dbo].[ABNotification] CHECK CONSTRAINT [FK_Noti_ModuleKey]
GO
ALTER TABLE [dbo].[Agreement]  WITH CHECK ADD FOREIGN KEY([PortalKey])
REFERENCES [dbo].[Portal] ([PortalKey])
GO
ALTER TABLE [dbo].[Bid]  WITH CHECK ADD FOREIGN KEY([ResourceKey])
REFERENCES [dbo].[Resource] ([ResourceKey])
GO
ALTER TABLE [dbo].[Bid]  WITH CHECK ADD  CONSTRAINT [FK_Bid_BidVendor] FOREIGN KEY([BidVendorKey])
REFERENCES [dbo].[BidVendor] ([BidVendorKey])
GO
ALTER TABLE [dbo].[Bid] CHECK CONSTRAINT [FK_Bid_BidVendor]
GO
ALTER TABLE [dbo].[BidRequest]  WITH CHECK ADD FOREIGN KEY([PropertyKey])
REFERENCES [dbo].[Property] ([PropertyKey])
GO
ALTER TABLE [dbo].[BidRequest]  WITH CHECK ADD FOREIGN KEY([ResourceKey])
REFERENCES [dbo].[Resource] ([ResourceKey])
GO
ALTER TABLE [dbo].[BidRequest]  WITH CHECK ADD FOREIGN KEY([ServiceKey])
REFERENCES [dbo].[Service] ([ServiceKey])
GO
ALTER TABLE [dbo].[BidVendor]  WITH CHECK ADD FOREIGN KEY([BidRequestKey])
REFERENCES [dbo].[BidRequest] ([BidRequestKey])
GO
ALTER TABLE [dbo].[BidVendor]  WITH CHECK ADD FOREIGN KEY([ResourceKey])
REFERENCES [dbo].[Resource] ([ResourceKey])
GO
ALTER TABLE [dbo].[BidVendor]  WITH CHECK ADD FOREIGN KEY([VendorKey])
REFERENCES [dbo].[Company] ([CompanyKey])
GO
ALTER TABLE [dbo].[Calendar]  WITH CHECK ADD FOREIGN KEY([ModuleKey])
REFERENCES [dbo].[Module] ([ModuleKey])
GO
ALTER TABLE [dbo].[Calendar]  WITH CHECK ADD FOREIGN KEY([ResourceKey])
REFERENCES [dbo].[Resource] ([ResourceKey])
GO
ALTER TABLE [dbo].[Company]  WITH CHECK ADD FOREIGN KEY([CompanyTypeKey])
REFERENCES [dbo].[LookUp] ([LookUpKey])
GO
ALTER TABLE [dbo].[Company]  WITH CHECK ADD FOREIGN KEY([ParentCompanyKey])
REFERENCES [dbo].[Company] ([CompanyKey])
GO
ALTER TABLE [dbo].[Company]  WITH CHECK ADD FOREIGN KEY([PortalKey])
REFERENCES [dbo].[Portal] ([PortalKey])
GO
ALTER TABLE [dbo].[Company]  WITH CHECK ADD FOREIGN KEY([RelatedCompanyKey])
REFERENCES [dbo].[Company] ([CompanyKey])
GO
ALTER TABLE [dbo].[Company]  WITH CHECK ADD FOREIGN KEY([State])
REFERENCES [dbo].[State] ([StateKey])
GO
ALTER TABLE [dbo].[CompanyVendor]  WITH CHECK ADD FOREIGN KEY([CompanyKey])
REFERENCES [dbo].[Company] ([CompanyKey])
GO
ALTER TABLE [dbo].[CompanyVendor]  WITH CHECK ADD FOREIGN KEY([VendorKey])
REFERENCES [dbo].[Company] ([CompanyKey])
GO
ALTER TABLE [dbo].[Document]  WITH CHECK ADD FOREIGN KEY([ModuleKey])
REFERENCES [dbo].[Module] ([ModuleKey])
GO
ALTER TABLE [dbo].[Email]  WITH CHECK ADD FOREIGN KEY([ModuleKey])
REFERENCES [dbo].[Module] ([ModuleKey])
GO
ALTER TABLE [dbo].[Email]  WITH CHECK ADD FOREIGN KEY([ResourceKey])
REFERENCES [dbo].[Resource] ([ResourceKey])
GO
ALTER TABLE [dbo].[EmailTemplate]  WITH CHECK ADD FOREIGN KEY([lookUpType])
REFERENCES [dbo].[LookUp] ([LookUpKey])
GO
ALTER TABLE [dbo].[EmailTemplate]  WITH CHECK ADD FOREIGN KEY([lookUpType])
REFERENCES [dbo].[LookUp] ([LookUpKey])
GO
ALTER TABLE [dbo].[EmailTemplate]  WITH CHECK ADD FOREIGN KEY([lookUpType])
REFERENCES [dbo].[LookUp] ([LookUpKey])
GO
ALTER TABLE [dbo].[EmailTemplate]  WITH CHECK ADD FOREIGN KEY([lookUpType])
REFERENCES [dbo].[LookUp] ([LookUpKey])
GO
ALTER TABLE [dbo].[EmailTemplate]  WITH CHECK ADD FOREIGN KEY([lookUpType])
REFERENCES [dbo].[LookUp] ([LookUpKey])
GO
ALTER TABLE [dbo].[EmailTemplate]  WITH CHECK ADD FOREIGN KEY([lookUpType])
REFERENCES [dbo].[LookUp] ([LookUpKey])
GO
ALTER TABLE [dbo].[EmailTemplate]  WITH CHECK ADD FOREIGN KEY([lookUpType])
REFERENCES [dbo].[LookUp] ([LookUpKey])
GO
ALTER TABLE [dbo].[EmailTemplate]  WITH CHECK ADD FOREIGN KEY([lookUpType])
REFERENCES [dbo].[LookUp] ([LookUpKey])
GO
ALTER TABLE [dbo].[GroupMember]  WITH CHECK ADD FOREIGN KEY([GroupKey])
REFERENCES [dbo].[Group] ([GroupKey])
GO
ALTER TABLE [dbo].[GroupMember]  WITH CHECK ADD FOREIGN KEY([ResourceKey])
REFERENCES [dbo].[Resource] ([ResourceKey])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[GroupModuleAccess]  WITH CHECK ADD FOREIGN KEY([GroupKey])
REFERENCES [dbo].[Group] ([GroupKey])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[GroupModuleAccess]  WITH CHECK ADD FOREIGN KEY([ModuleKey])
REFERENCES [dbo].[Module] ([ModuleKey])
GO
ALTER TABLE [dbo].[GroupModuleAccess]  WITH CHECK ADD FOREIGN KEY([PortalKey])
REFERENCES [dbo].[Portal] ([PortalKey])
GO
ALTER TABLE [dbo].[Insurance]  WITH CHECK ADD FOREIGN KEY([State])
REFERENCES [dbo].[State] ([StateKey])
GO
ALTER TABLE [dbo].[Insurance]  WITH CHECK ADD FOREIGN KEY([VendorKey])
REFERENCES [dbo].[Company] ([CompanyKey])
GO
ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD FOREIGN KEY([VendorKey])
REFERENCES [dbo].[Company] ([CompanyKey])
GO
ALTER TABLE [dbo].[InvoiceLine]  WITH CHECK ADD FOREIGN KEY([InvoiceKey])
REFERENCES [dbo].[Invoice] ([InvoiceKey])
GO
ALTER TABLE [dbo].[LoginHistory]  WITH CHECK ADD FOREIGN KEY([UserKey])
REFERENCES [dbo].[User] ([UserKey])
GO
ALTER TABLE [dbo].[LookUp]  WITH CHECK ADD FOREIGN KEY([LookUpTypeKey])
REFERENCES [dbo].[LookUpType] ([LookUpTypeKey])
GO
ALTER TABLE [dbo].[Membership]  WITH CHECK ADD FOREIGN KEY([VendorKey])
REFERENCES [dbo].[Company] ([CompanyKey])
GO
ALTER TABLE [dbo].[Message]  WITH CHECK ADD FOREIGN KEY([ModuleKey])
REFERENCES [dbo].[Module] ([ModuleKey])
GO
ALTER TABLE [dbo].[Message]  WITH CHECK ADD FOREIGN KEY([ResourceKey])
REFERENCES [dbo].[Resource] ([ResourceKey])
GO
ALTER TABLE [dbo].[Note]  WITH CHECK ADD FOREIGN KEY([ModuleKey])
REFERENCES [dbo].[Module] ([ModuleKey])
GO
ALTER TABLE [dbo].[Note]  WITH CHECK ADD FOREIGN KEY([ResourceKey])
REFERENCES [dbo].[Resource] ([ResourceKey])
GO
ALTER TABLE [dbo].[Payment]  WITH CHECK ADD FOREIGN KEY([PaymentTypeKey])
REFERENCES [dbo].[LookUp] ([LookUpKey])
GO
ALTER TABLE [dbo].[Payment]  WITH CHECK ADD FOREIGN KEY([VendorKey])
REFERENCES [dbo].[Company] ([CompanyKey])
GO
ALTER TABLE [dbo].[PaymentApplied]  WITH CHECK ADD FOREIGN KEY([InvociceKey])
REFERENCES [dbo].[Invoice] ([InvoiceKey])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PaymentApplied]  WITH CHECK ADD FOREIGN KEY([PaymentKey])
REFERENCES [dbo].[Payment] ([PaymentKey])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PaymentMethod]  WITH NOCHECK ADD  CONSTRAINT [FK_PaymentMethod_Company] FOREIGN KEY([CompanyKey])
REFERENCES [dbo].[Company] ([CompanyKey])
GO
ALTER TABLE [dbo].[PaymentMethod] CHECK CONSTRAINT [FK_PaymentMethod_Company]
GO
ALTER TABLE [dbo].[PaymentMethod]  WITH NOCHECK ADD  CONSTRAINT [FK_PaymentMethod_Resource] FOREIGN KEY([AddedByResourceKey])
REFERENCES [dbo].[Resource] ([ResourceKey])
GO
ALTER TABLE [dbo].[PaymentMethod] CHECK CONSTRAINT [FK_PaymentMethod_Resource]
GO
ALTER TABLE [dbo].[Pricing]  WITH CHECK ADD FOREIGN KEY([CompanyKey])
REFERENCES [dbo].[Company] ([CompanyKey])
GO
ALTER TABLE [dbo].[Pricing]  WITH CHECK ADD FOREIGN KEY([PricingTypeKey])
REFERENCES [dbo].[LookUp] ([LookUpKey])
GO
ALTER TABLE [dbo].[Promotion]  WITH CHECK ADD FOREIGN KEY([CompanyKey])
REFERENCES [dbo].[Company] ([CompanyKey])
GO
ALTER TABLE [dbo].[Property]  WITH CHECK ADD FOREIGN KEY([CompanyKey])
REFERENCES [dbo].[Company] ([CompanyKey])
GO
ALTER TABLE [dbo].[Property]  WITH CHECK ADD FOREIGN KEY([State])
REFERENCES [dbo].[State] ([StateKey])
GO
ALTER TABLE [dbo].[PropertyResource]  WITH CHECK ADD FOREIGN KEY([PropertyKey])
REFERENCES [dbo].[Property] ([PropertyKey])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PropertyResource]  WITH CHECK ADD FOREIGN KEY([ResourceKey])
REFERENCES [dbo].[Resource] ([ResourceKey])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PropertyVendorDistance]  WITH CHECK ADD FOREIGN KEY([PropertyKey])
REFERENCES [dbo].[Company] ([CompanyKey])
GO
ALTER TABLE [dbo].[PropertyVendorDistance]  WITH CHECK ADD FOREIGN KEY([VendorKey])
REFERENCES [dbo].[Company] ([CompanyKey])
GO
ALTER TABLE [dbo].[PushNotification]  WITH NOCHECK ADD  CONSTRAINT [FK_Notification_Resource] FOREIGN KEY([PushNotificationResourceKey])
REFERENCES [dbo].[Resource] ([ResourceKey])
GO
ALTER TABLE [dbo].[PushNotification] CHECK CONSTRAINT [FK_Notification_Resource]
GO
ALTER TABLE [dbo].[PushNotificationTemplate]  WITH NOCHECK ADD FOREIGN KEY([PushNotificationType])
REFERENCES [dbo].[LookUp] ([LookUpKey])
GO
ALTER TABLE [dbo].[Reminder]  WITH CHECK ADD FOREIGN KEY([ModuleKey])
REFERENCES [dbo].[Module] ([ModuleKey])
GO
ALTER TABLE [dbo].[Reminder]  WITH CHECK ADD FOREIGN KEY([ResourceKey])
REFERENCES [dbo].[Resource] ([ResourceKey])
GO
ALTER TABLE [dbo].[Resource]  WITH CHECK ADD FOREIGN KEY([CompanyKey])
REFERENCES [dbo].[Company] ([CompanyKey])
GO
ALTER TABLE [dbo].[Resource]  WITH CHECK ADD FOREIGN KEY([ResourceTypeKey])
REFERENCES [dbo].[LookUp] ([LookUpKey])
GO
ALTER TABLE [dbo].[Resource]  WITH CHECK ADD FOREIGN KEY([State])
REFERENCES [dbo].[State] ([StateKey])
GO
ALTER TABLE [dbo].[Service]  WITH CHECK ADD FOREIGN KEY([ParentServiceKey])
REFERENCES [dbo].[Service] ([ServiceKey])
GO
ALTER TABLE [dbo].[ServiceArea]  WITH CHECK ADD FOREIGN KEY([State])
REFERENCES [dbo].[State] ([StateKey])
GO
ALTER TABLE [dbo].[ServiceArea]  WITH CHECK ADD FOREIGN KEY([VendorKey])
REFERENCES [dbo].[Company] ([CompanyKey])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Task]  WITH CHECK ADD FOREIGN KEY([AssignedToKey])
REFERENCES [dbo].[Resource] ([ResourceKey])
GO
ALTER TABLE [dbo].[Task]  WITH CHECK ADD FOREIGN KEY([ModuleKey])
REFERENCES [dbo].[Module] ([ModuleKey])
GO
ALTER TABLE [dbo].[Task]  WITH CHECK ADD FOREIGN KEY([ResourceKey])
REFERENCES [dbo].[Resource] ([ResourceKey])
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD FOREIGN KEY([ResourceKey])
REFERENCES [dbo].[Resource] ([ResourceKey])
GO
ALTER TABLE [dbo].[UserAgreement]  WITH CHECK ADD FOREIGN KEY([AgreementKey])
REFERENCES [dbo].[Agreement] ([AgreementKey])
GO
ALTER TABLE [dbo].[UserAgreement]  WITH CHECK ADD FOREIGN KEY([UserKey])
REFERENCES [dbo].[User] ([UserKey])
GO
ALTER TABLE [dbo].[VendorRating]  WITH CHECK ADD FOREIGN KEY([ResourceKey])
REFERENCES [dbo].[Resource] ([ResourceKey])
GO
ALTER TABLE [dbo].[VendorRating]  WITH CHECK ADD FOREIGN KEY([VendorKey])
REFERENCES [dbo].[Company] ([CompanyKey])
GO
ALTER TABLE [dbo].[VendorService]  WITH CHECK ADD FOREIGN KEY([ServiceKey])
REFERENCES [dbo].[Service] ([ServiceKey])
GO
ALTER TABLE [dbo].[VendorService]  WITH CHECK ADD FOREIGN KEY([VendorKey])
REFERENCES [dbo].[Company] ([CompanyKey])
ON DELETE CASCADE
GO
