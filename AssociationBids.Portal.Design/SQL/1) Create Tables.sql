
USE [AssociationBids]
GO

------------------------------------------------------------------------------------------
----- DROP TABLES
------------------------------------------------------------------------------------------

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PaymentApplied]') AND type in (N'U'))
DROP TABLE [dbo].[PaymentApplied]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Payment]') AND type in (N'U'))
DROP TABLE [dbo].[Payment]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceLine]') AND type in (N'U'))
DROP TABLE [dbo].[InvoiceLine]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Invoice]') AND type in (N'U'))
DROP TABLE [dbo].[Invoice]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Promotion]') AND type in (N'U'))
DROP TABLE [dbo].[Promotion]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Membership]') AND type in (N'U'))
DROP TABLE [dbo].[Membership]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pricing]') AND type in (N'U'))
DROP TABLE [dbo].[Pricing]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Reminder]') AND type in (N'U'))
DROP TABLE [dbo].[Reminder]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Calendar]') AND type in (N'U'))
DROP TABLE [Calendar]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Task]') AND type in (N'U'))
DROP TABLE [dbo].[Task]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Note]') AND type in (N'U'))
DROP TABLE [dbo].[Note]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Document]') AND type in (N'U'))
DROP TABLE [dbo].[Document]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Message]') AND type in (N'U'))
DROP TABLE [dbo].[Message]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmailDetail]') AND type in (N'U'))
DROP TABLE [dbo].[EmailDetail]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Email]') AND type in (N'U'))
DROP TABLE [dbo].[Email]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[VendorRating]') AND type in (N'U'))
DROP TABLE [dbo].[VendorRating]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Bid]') AND type in (N'U'))
DROP TABLE [dbo].[Bid]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BidVendor]') AND type in (N'U'))
DROP TABLE [dbo].[BidVendor]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BidRequest]') AND type in (N'U'))
DROP TABLE [dbo].[BidRequest]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PropertyVendorDistance]') AND type in (N'U'))
DROP TABLE [dbo].[PropertyVendorDistance]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PropertyResource]') AND type in (N'U'))
DROP TABLE [dbo].[PropertyResource]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Property]') AND type in (N'U'))
DROP TABLE [dbo].[Property]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Insurance]') AND type in (N'U'))
DROP TABLE [dbo].[Insurance]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GroupModuleAccess]') AND type in (N'U'))
DROP TABLE [dbo].[GroupModuleAccess]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GroupMember]') AND type in (N'U'))
DROP TABLE [dbo].[GroupMember]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Group]') AND type in (N'U'))
DROP TABLE [dbo].[Group]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LoginHistory]') AND type in (N'U'))
DROP TABLE [dbo].[LoginHistory]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserAgreement]') AND type in (N'U'))
DROP TABLE [dbo].[UserAgreement]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User]') AND type in (N'U'))
DROP TABLE [dbo].[User]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Resource]') AND type in (N'U'))
DROP TABLE [dbo].[Resource]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ServiceArea]') AND type in (N'U'))
DROP TABLE [dbo].[ServiceArea]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[VendorService]') AND type in (N'U'))
DROP TABLE [dbo].[VendorService]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CompanyVendor]') AND type in (N'U'))
DROP TABLE [dbo].[CompanyVendor]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Company]') AND type in (N'U'))
DROP TABLE [dbo].[Company]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Agreement]') AND type in (N'U'))
DROP TABLE [dbo].[Agreement]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Service]') AND type in (N'U'))
DROP TABLE [dbo].[Service]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Portal]') AND type in (N'U'))
DROP TABLE [dbo].[Portal]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Module]') AND type in (N'U'))
DROP TABLE [dbo].[Module]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ErrorLog]') AND type in (N'U'))
DROP TABLE [dbo].[ErrorLog]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Session]') AND type in (N'U'))
DROP TABLE [dbo].[Session]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LookUp]') AND type in (N'U'))
DROP TABLE [dbo].[LookUp]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LookUpType]') AND type in (N'U'))
DROP TABLE [dbo].[LookUpType]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[State]') AND type in (N'U'))
DROP TABLE [dbo].[State]
GO

------------------------------------------------------------------------------------------
----- CREATE PORTAL TABLES
------------------------------------------------------------------------------------------

CREATE TABLE [State] (
	[StateKey] VARCHAR(2) NOT NULL 
		PRIMARY KEY  CLUSTERED ,
	[Title] VARCHAR(150) NOT NULL 
)
GO

CREATE TABLE [LookUpType] (
	[LookUpTypeKey] INT IDENTITY (1 , 1) NOT NULL 
		PRIMARY KEY  CLUSTERED ,
	[Title] VARCHAR(150) NOT NULL 
)
GO

CREATE TABLE [LookUp] (
	[LookUpKey] INT NOT NULL  
		PRIMARY KEY  CLUSTERED ,
	[LookUpTypeKey] INT NOT NULL
		REFERENCES [LookUpType] (LookUpTypeKey) ,
	[Title] VARCHAR(150) NOT NULL ,
	[Value] INT NULL ,
	[SortOrder] FLOAT NULL
)
GO

CREATE TABLE [Session] (
	[SessionKey] INT IDENTITY (1 , 1) NOT NULL 
		PRIMARY KEY  CLUSTERED ,
	[SessionID] UNIQUEIDENTIFIER NOT NULL ,
	[Salt] VARCHAR(150) NOT NULL ,
	[Data] VARCHAR(MAX) NULL ,
	[LastModificationTime] DATETIME NOT NULL
		DEFAULT GETDATE()
)
GO

IF EXISTS (SELECT * FROM dbo.sysindexes WHERE name = N'SessionID' AND id = object_id(N'[Session]'))
DROP INDEX [Session].[SessionID]
GO

CREATE INDEX [SessionID] ON [Session] (SessionID)
GO

CREATE TABLE [ErrorLog] (
	[ErrorLogKey] INT IDENTITY (1 , 1) NOT NULL 
		PRIMARY KEY  CLUSTERED ,
	[Details] VARCHAR(MAX) NOT NULL ,
	[Session] VARCHAR(MAX) NULL ,
	[DateAdded] DATETIME NOT NULL
		DEFAULT GETDATE()
)
GO

CREATE TABLE [Module] (
	[ModuleKey] INT NOT NULL  
		PRIMARY KEY  CLUSTERED ,
	[Title] VARCHAR(150) NOT NULL ,
	[Controller] VARCHAR(150) NULL ,
	[Action] VARCHAR(150) NULL ,
	[Image] VARCHAR(150) NULL ,
	[Description] VARCHAR(MAX) NULL
)
GO

CREATE TABLE [Portal] (
	[PortalKey] INT IDENTITY (1 , 1) NOT NULL 
		PRIMARY KEY  CLUSTERED ,
	[PortalID] VARCHAR(255) NOT NULL UNIQUE ,
	[Title] VARCHAR(150) NOT NULL ,
	[Url] VARCHAR(255) NULL ,
	[SiteImage] VARCHAR(150) NULL ,
	[HomePageImage] VARCHAR(150) NULL ,
	[Stylesheet] VARCHAR(150) NULL ,
	[Description] VARCHAR(MAX) NULL ,
	[NotificationSetting] INT NULL ,
	[DateAdded] DATETIME NOT NULL 
		DEFAULT GETDATE() ,
	[LastModificationTime] DATETIME NOT NULL 
		DEFAULT GETDATE() ,
	[Status] INT NOT NULL
)
GO

IF EXISTS (SELECT * FROM dbo.sysindexes WHERE name = N'PortalID' AND id = object_id(N'[Portal]'))
DROP INDEX [Portal].[PortalID]
GO

CREATE INDEX [PortalID] ON [Portal] (PortalID)
GO

CREATE TABLE [Service] (
	[ServiceKey] INT IDENTITY (1 , 1) NOT NULL 
		PRIMARY KEY  CLUSTERED ,
	[ParentServiceKey] INT NULL
		REFERENCES [Service] (ServiceKey) ,
	[Title] VARCHAR(150) NOT NULL ,
	[Tags] VARCHAR(MAX) NULL 
)
GO

CREATE TABLE [Agreement] (
	[AgreementKey] INT IDENTITY (1 , 1) NOT NULL 
		PRIMARY KEY  CLUSTERED ,
	[PortalKey] INT NOT NULL
		REFERENCES [Portal] (PortalKey) ,
	[Title] VARCHAR(150) NOT NULL ,
	[AgreementDate] DATETIME NOT NULL 
		DEFAULT GETDATE() ,
	[Description] VARCHAR(MAX) NULL ,
	[LastModificationTime] DATETIME NOT NULL 
		DEFAULT GETDATE() ,
	[Status] INT NOT NULL
)
GO

CREATE TABLE [Company] (
	[CompanyKey] INT IDENTITY (1 , 1) NOT NULL 
		PRIMARY KEY  CLUSTERED ,
	[ParentCompanyKey] INT NULL
		REFERENCES [Company] (CompanyKey) ,
	[RelatedCompanyKey] INT NULL
		REFERENCES [Company] (CompanyKey) ,
	[CompanyTypeKey] INT NOT NULL
		REFERENCES [LookUp] (LookUpKey) ,
	[PortalKey] INT NULL
		REFERENCES [Portal] (PortalKey) ,
	[CompanyID] VARCHAR(255) NULL UNIQUE ,
	[Name] VARCHAR(150) NOT NULL ,
	[LegalName] VARCHAR(150) NULL ,
	[TaxID] VARCHAR(50) NULL ,
	[Work] VARCHAR(50) NULL ,
	[Work2] VARCHAR(50) NULL ,
	[Fax] VARCHAR(50) NULL ,
	[Address] VARCHAR(100) NULL ,
	[Address2] VARCHAR(100) NULL ,
	[City] VARCHAR(50) NULL ,
	[State] VARCHAR(2) NULL 
		REFERENCES [State] (StateKey) ,
	[Zip] VARCHAR(11) NULL ,
	[Website] VARCHAR(255) NULL ,
	[Description] VARCHAR(MAX) NULL ,
	[BidRequestResponseDays] INT NULL ,
	[BidSubmitDays] INT NULL ,
	[BidRequestAmount] MONEY NULL ,
	[NotificationPreference] INT NULL ,
	[DateAdded] DATETIME NOT NULL 
		DEFAULT GETDATE() ,
	[LastModificationTime] DATETIME NOT NULL 
		DEFAULT GETDATE() ,
	[Status] INT NOT NULL
)
GO

IF EXISTS (SELECT * FROM dbo.sysindexes WHERE name = N'CompanyID' AND id = object_id(N'[Company]'))
DROP INDEX [Company].[CompanyID]
GO

CREATE INDEX [CompanyID] ON [Company] (CompanyID)
GO

CREATE TABLE [CompanyVendor] (
	[CompanyVendorKey] INT IDENTITY (1 , 1) NOT NULL 
		PRIMARY KEY  CLUSTERED ,
	[CompanyKey] INT NOT NULL
		REFERENCES [Company] (CompanyKey) ,
	[VendorKey] INT NOT NULL
		REFERENCES [Company] (CompanyKey) ,
	[LastModificationTime] DATETIME NOT NULL 
		DEFAULT GETDATE() ,
	[Status] INT NOT NULL
)
GO

CREATE TABLE [VendorService] (
	[VendorServiceKey] INT IDENTITY (1 , 1) NOT NULL 
		PRIMARY KEY  CLUSTERED ,
	[VendorKey] INT NOT NULL
		REFERENCES [Company] (CompanyKey) 
		ON DELETE CASCADE ,
	[ServiceKey] INT NOT NULL
		REFERENCES [Service] (ServiceKey) ,
	[SortOrder] FLOAT NULL
)
GO

CREATE TABLE [ServiceArea] (
	[ServiceAreaKey] INT IDENTITY (1 , 1) NOT NULL 
		PRIMARY KEY  CLUSTERED ,
	[VendorKey] INT NOT NULL
		REFERENCES [Company] (CompanyKey)
		ON DELETE CASCADE ,
	[Address] VARCHAR(100) NOT NULL ,
	[Address2] VARCHAR(100) NULL ,
	[City] VARCHAR(50) NOT NULL ,
	[State] VARCHAR(2) NOT NULL 
		REFERENCES [State] (StateKey) ,
	[Zip] VARCHAR(11) NOT NULL ,
	[Latitude] FLOAT NULL ,
	[Longitude] FLOAT NULL ,
	[Radius] INT NOT NULL
)
GO

CREATE TABLE [Insurance] (
	[InsuranceKey] INT IDENTITY (1 , 1) NOT NULL 
		PRIMARY KEY  CLUSTERED ,
	[VendorKey] INT NOT NULL
		REFERENCES [Company] (CompanyKey) ,
	[CompanyName] VARCHAR(150) NOT NULL ,
	[PolicyNumber] VARCHAR(150) NULL ,
	[InsuranceAmount] MONEY NULL ,
	[AgentName] VARCHAR(150) NULL ,
	[Email] VARCHAR(150) NULL ,
	[Work] VARCHAR(150) NULL ,
	[CellPhone] VARCHAR(150) NULL ,
	[Fax] VARCHAR(150) NULL ,
	[Address] VARCHAR(100) NOT NULL ,
	[Address2] VARCHAR(100) NULL ,
	[City] VARCHAR(50) NOT NULL ,
	[State] VARCHAR(2) NOT NULL 
		REFERENCES [State] (StateKey) ,
	[Zip] VARCHAR(11) NOT NULL ,
	[StartDate] DATETIME NOT NULL ,
	[EndDate] DATETIME NOT NULL ,
	[RenewalDate] DATETIME NULL ,
	[Status] INT NOT NULL
)
GO

CREATE TABLE [Resource] (
	[ResourceKey] INT IDENTITY (1 , 1) NOT NULL 
		PRIMARY KEY  CLUSTERED ,
	[CompanyKey] INT NOT NULL
		REFERENCES [Company] (CompanyKey) ,
	[ResourceTypeKey] INT NOT NULL
		REFERENCES [LookUp] (LookUpKey) ,
	[FirstName] VARCHAR(50) NULL ,
	[LastName] VARCHAR(50) NULL ,
	[Title] VARCHAR(150) NULL ,
	[Email] VARCHAR(150) NULL ,
	[Email2] VARCHAR(150) NULL ,
	[CellPhone] VARCHAR(50) NULL ,
	[HomePhone] VARCHAR(50) NULL ,
	[HomePhone2] VARCHAR(50) NULL ,
	[Work] VARCHAR(50) NULL ,
	[Work2] VARCHAR(50) NULL ,
	[Fax] VARCHAR(50) NULL ,
	[Address] VARCHAR(100) NULL ,
	[Address2] VARCHAR(100) NULL ,
	[City] VARCHAR(50) NULL ,
	[State] VARCHAR(2) NULL 
		REFERENCES [State] (StateKey) ,
	[Zip] VARCHAR(11) NULL ,
	[PrimaryContact] BIT NULL ,
	[Description] VARCHAR(MAX) NULL ,
	[DateAdded] DATETIME NOT NULL 
		DEFAULT GETDATE() ,
	[LastModificationTime] DATETIME NOT NULL 
		DEFAULT GETDATE() ,
	[Status] INT NOT NULL 
)
GO

CREATE TABLE [User] (
	[UserKey] INT IDENTITY (1 , 1) NOT NULL 
		PRIMARY KEY  CLUSTERED ,
	[ResourceKey] INT NOT NULL
		REFERENCES [Resource] (ResourceKey) ,
	[Username] VARCHAR(150) NOT NULL ,
	[Password] VARCHAR(256) NOT NULL ,
	[TokenReset] VARCHAR(256) NULL ,
	[ResetExpirationDate] DATETIME NULL ,
	[AccountLocked] BIT NULL ,
	[FirstTimeAccess] BIT NULL ,
	[DateAdded] DATETIME NOT NULL 
		DEFAULT GETDATE() ,
	[LastModificationTime] DATETIME NOT NULL 
		DEFAULT GETDATE() ,
	[Status] INT NOT NULL
)
GO

CREATE TABLE [UserAgreement] (
	[UserAgreementKey] INT IDENTITY (1 , 1) NOT NULL 
		PRIMARY KEY  CLUSTERED ,
	[UserKey] INT NOT NULL
		REFERENCES [User] (UserKey) ,
	[AgreementKey] INT NOT NULL
		REFERENCES [Agreement] (AgreementKey) ,
	[DateAdded] DATETIME NOT NULL 
		DEFAULT GETDATE() 
)
GO

CREATE TABLE [LoginHistory] (
	[LoginHistoryKey] INT IDENTITY (1 , 1) NOT NULL 
		PRIMARY KEY  CLUSTERED ,
	[UserKey] INT NOT NULL
		REFERENCES [User] (UserKey) ,
	[DateAdded] DATETIME NOT NULL 
		DEFAULT GETDATE() 
)
GO

CREATE TABLE [Group] (
	[GroupKey] INT IDENTITY (1 , 1) NOT NULL 
		PRIMARY KEY  CLUSTERED ,
	[Title] VARCHAR(150) NOT NULL ,
	[Description] VARCHAR(MAX) NULL 
)
GO

CREATE TABLE [GroupMember] (
	[GroupMemberKey] INT IDENTITY (1 , 1) NOT NULL 
		PRIMARY KEY  CLUSTERED ,
	[GroupKey] INT NOT NULL
		REFERENCES [Group] (GroupKey) ,
	[ResourceKey] INT NOT NULL 
		REFERENCES [Resource] (ResourceKey) 
		ON DELETE CASCADE 
)
GO

CREATE TABLE [GroupModuleAccess] (
	[GroupModuleAccessKey] INT IDENTITY (1 , 1) NOT NULL 
		PRIMARY KEY  CLUSTERED ,
	[PortalKey] INT NOT NULL
		REFERENCES [Portal] (PortalKey) ,
	[GroupKey] INT NOT NULL
		REFERENCES [Group] (GroupKey) 
		ON DELETE CASCADE ,
	[ModuleKey] INT NOT NULL
		REFERENCES [Module] (ModuleKey) ,
	[Access] INT NOT NULL
)
GO

CREATE TABLE [Property] (
	[PropertyKey] INT IDENTITY (1 , 1) NOT NULL 
		PRIMARY KEY  CLUSTERED ,
	[CompanyKey] INT NOT NULL
		REFERENCES [Company] (CompanyKey) ,
	[Title] VARCHAR(150) NOT NULL ,
	[NumberOfUnits] INT NULL ,
	[Address] VARCHAR(100) NULL ,
	[Address2] VARCHAR(100) NULL ,
	[City] VARCHAR(50) NULL ,
	[State] VARCHAR(2) NULL 
		REFERENCES [State] (StateKey) ,
	[Zip] VARCHAR(11) NULL ,
	[BidRequestAmount] MONEY NULL ,
	[MinimumInsuranceAmount] MONEY NULL ,
	[Description] VARCHAR(MAX) NULL ,
	[Status] INT NOT NULL
)
GO

CREATE TABLE [PropertyResource] (
	[PropertyResourceKey] INT IDENTITY (1 , 1) NOT NULL 
		PRIMARY KEY  CLUSTERED ,
	[PropertyKey] INT NOT NULL
		REFERENCES [Property] (PropertyKey) 
		ON DELETE CASCADE ,
	[ResourceKey] INT NOT NULL
		REFERENCES [Group] (GroupKey) 
		ON DELETE CASCADE ,
	[DateAdded] DATETIME NOT NULL 
		DEFAULT GETDATE() ,
	[Status] INT NOT NULL
)
GO

CREATE TABLE [PropertyVendorDistance] (
	[PropertyVendorDistanceKey] INT IDENTITY (1 , 1) NOT NULL 
		PRIMARY KEY  CLUSTERED ,
	[PropertyKey] INT NOT NULL
		REFERENCES [Company] (CompanyKey) ,
	[VendorKey] INT NOT NULL
		REFERENCES [Company] (CompanyKey) ,
	[Distance] FLOAT NULL ,
	[LastModificationTime] DATETIME NOT NULL 
		DEFAULT GETDATE() 
)
GO

CREATE TABLE [BidRequest] (
	[BidRequestKey] INT IDENTITY (1 , 1) NOT NULL 
		PRIMARY KEY  CLUSTERED ,
	[PropertyKey] INT NOT NULL
		REFERENCES [Property] (PropertyKey) ,
	[ResourceKey] INT NOT NULL
		REFERENCES [Resource] (ResourceKey) ,
	[ServiceKey] INT NOT NULL
		REFERENCES [Service] (ServiceKey) ,
	[Title] VARCHAR(150) NOT NULL ,
	[BidDueDate] SMALLDATETIME NULL ,
	[StartDate] SMALLDATETIME NULL ,
	[EndDate] SMALLDATETIME NULL ,
	[Description] VARCHAR(MAX) NULL ,
	[DateAdded] DATETIME NOT NULL 
		DEFAULT GETDATE() ,
	[LastModificationTime] DATETIME NOT NULL 
		DEFAULT GETDATE() ,
	[BidRequestStatus] INT NOT NULL 
)
GO

CREATE TABLE [BidVendor] (
	[BidVendorKey] INT IDENTITY (1 , 1) NOT NULL 
		PRIMARY KEY  CLUSTERED ,
	[BidRequestKey] INT NOT NULL
		REFERENCES [BidRequest] (BidRequestKey) ,
	[VendorKey] INT NOT NULL
		REFERENCES [Company] (CompanyKey) ,
	[ResourceKey] INT NULL
		REFERENCES [Resource] (ResourceKey) ,
	[BidVendorID] VARCHAR(255) NOT NULL UNIQUE ,
	[IsAssigned] BIT NULL ,
	[RespondByDate] DATETIME NULL ,	
	[DateAdded] DATETIME NOT NULL 
		DEFAULT GETDATE() ,
	[LastModificationTime] DATETIME NOT NULL 
		DEFAULT GETDATE() ,
	[BidVendorStatus] INT NOT NULL 
)
GO

IF EXISTS (SELECT * FROM dbo.sysindexes WHERE name = N'BidVendorID' AND id = object_id(N'[BidVendor]'))
DROP INDEX [BidVendor].[BidVendorID]
GO

CREATE INDEX [BidVendorID] ON [BidVendor] (BidVendorID)
GO

CREATE TABLE [Bid] (
	[BidKey] INT IDENTITY (1 , 1) NOT NULL 
		PRIMARY KEY  CLUSTERED ,
	[BidVendorKey] INT NOT NULL
		REFERENCES [BidVendor] (BidVendorKey) ,
	[ResourceKey] INT NULL
		REFERENCES [Resource] (ResourceKey) ,
	[Title] VARCHAR(150) NOT NULL ,
	[Total] MONEY NULL ,
	[Description] VARCHAR(MAX) NULL ,
	[LastModificationTime] DATETIME NOT NULL 
		DEFAULT GETDATE() ,
	[BidStatus] INT NOT NULL
)
GO

CREATE TABLE [VendorRating] (
	[VendorRatingKey] INT IDENTITY (1 , 1) NOT NULL 
		PRIMARY KEY  CLUSTERED ,
	[VendorKey] INT NOT NULL
		REFERENCES [Company] (CompanyKey) ,
	[ResourceKey] INT NULL
		REFERENCES [Resource] (ResourceKey) ,
	[RatingOne] INT NULL ,
	[RatingTwo] INT NULL ,
	[RatingThree] INT NULL ,
	[RatingFour] INT NULL ,
	[RatingFive] INT NULL ,
	[LastModificationTime] DATETIME NOT NULL 
		DEFAULT GETDATE()
)
GO

CREATE TABLE [Note] (
	[NoteKey] INT IDENTITY(1, 1) NOT NULL
		PRIMARY KEY  CLUSTERED ,
	[ModuleKey] INT NOT NULL
		REFERENCES [Module] (ModuleKey) ,
	[ResourceKey] INT NOT NULL
		REFERENCES [Resource] (ResourceKey) ,
	[ObjectKey] INT NULL ,
	[Description] VARCHAR(MAX) NOT NULL ,
	[LastModificationTime] DATETIME NOT NULL 
		DEFAULT GETDATE() ,
	[Status] INT NOT NULL
)
GO

IF EXISTS (SELECT * FROM dbo.sysindexes WHERE name = N'ObjectKey' AND id = object_id(N'[Note]'))
DROP INDEX [Note].[ObjectKey]
GO

CREATE INDEX [ObjectKey] ON [Note] (ObjectKey)
GO

CREATE TABLE [Task] (
	[TaskKey] INT IDENTITY (1 , 1) NOT NULL 
		PRIMARY KEY  CLUSTERED ,
	[ModuleKey] INT NOT NULL
		REFERENCES [Module] (ModuleKey) ,
	[ResourceKey] INT NOT NULL
		REFERENCES [Resource] (ResourceKey) ,
	[AssignedToKey] INT NULL
		REFERENCES [Resource] (ResourceKey) ,
	[ObjectKey] INT NULL ,
	[Subject] VARCHAR(150) NOT NULL ,
	[TaskStatus] INT NOT NULL ,
	[TaskPriority] INT NOT NULL ,
	[DueDate] SMALLDATETIME NULL ,
	[StartDate] SMALLDATETIME NULL ,
	[Description] VARCHAR(MAX) NULL ,
	[DateAdded] DATETIME NOT NULL 
		DEFAULT GETDATE() ,
	[DateCompleted] DATETIME NULL ,
	[LastModificationTime] DATETIME NOT NULL 
		DEFAULT GETDATE() 
)
GO

IF EXISTS (SELECT * FROM dbo.sysindexes WHERE name = N'ObjectKey' AND id = object_id(N'[Task]'))
DROP INDEX [Task].[ObjectKey]
GO

CREATE INDEX [ObjectKey] ON [Task] (ObjectKey)
GO


CREATE TABLE [Calendar] (
	[CalendarKey] INT IDENTITY (1, 1) NOT NULL 
		PRIMARY KEY  CLUSTERED ,
	[ModuleKey] INT NOT NULL
		REFERENCES [Module] (ModuleKey) ,
	[ResourceKey] INT NOT NULL
		REFERENCES [Resource] (ResourceKey) ,
	[ObjectKey] INT NULL ,
	[Subject] VARCHAR(150) NOT NULL ,
	[StartDate] SMALLDATETIME NOT NULL ,
	[EndDate] SMALLDATETIME NULL ,
	[AllDayEvent] BIT NULL ,
	[Location] VARCHAR(150) NULL ,
	[Description] VARCHAR(MAX) NULL ,
	[LastModificationTime] DATETIME NOT NULL 
		DEFAULT GETDATE() ,
	[Status] INT NOT NULL
)
GO

IF EXISTS (SELECT * FROM dbo.sysindexes WHERE name = N'ObjectKey' AND id = object_id(N'[Calendar]'))
DROP INDEX [Calendar].[ObjectKey]
GO

CREATE INDEX [ObjectKey] ON [Calendar] (ObjectKey)
GO


CREATE TABLE [Reminder] (
	[ReminderKey] INT 
		IDENTITY (1 , 1) NOT NULL 
		PRIMARY KEY  CLUSTERED ,
	[ModuleKey] INT NOT NULL
		REFERENCES [Module] (ModuleKey) ,
	[ResourceKey] INT NOT NULL
		REFERENCES [Resource] (ResourceKey) ,
	[ObjectKey] INT NULL ,
	[Description] VARCHAR(MAX) NULL ,
	[ReminderDate] SMALLDATETIME NULL ,
	[LastModificationTime] DATETIME NOT NULL 
		DEFAULT GETDATE() ,
	[Status] INT NOT NULL 
)
GO

IF EXISTS (SELECT * FROM dbo.sysindexes WHERE name = N'ObjectKey' AND id = object_id(N'[Reminder]'))
DROP INDEX [Reminder].[ObjectKey]
GO

CREATE INDEX [ObjectKey] ON [Reminder] (ObjectKey)
GO

CREATE TABLE [Document] (
	[DocumentKey] INT IDENTITY (1 , 1) NOT NULL 
		PRIMARY KEY  CLUSTERED ,
	[ModuleKey] INT NOT NULL
		REFERENCES [Module] (ModuleKey) ,
	[ObjectKey] INT NULL ,
	[FileName] VARCHAR(150) NOT NULL ,
	[FileSize] FLOAT NULL ,
	[LastModificationTime] DATETIME NOT NULL
		DEFAULT GETDATE()
)
GO

IF EXISTS (SELECT * FROM dbo.sysindexes WHERE name = N'ObjectKey' AND id = object_id(N'[Document]'))
DROP INDEX [Document].[ObjectKey]
GO

CREATE INDEX [ObjectKey] ON [Document] (ObjectKey)
GO

CREATE TABLE [Email] (
	[EmailKey] INT IDENTITY(1, 1) NOT NULL
		PRIMARY KEY  CLUSTERED ,
	[ModuleKey] INT NOT NULL
		REFERENCES [Module] (ModuleKey) ,
	[ResourceKey] INT NOT NULL
		REFERENCES [Resource] (ResourceKey) ,
	[ObjectKey] INT NULL ,
	[From] VARCHAR(MAX) NULL ,
	[To] VARCHAR(MAX) NULL ,
	[Cc] VARCHAR(MAX) NULL ,
	[Bcc] VARCHAR(MAX) NULL ,
	[Subject] VARCHAR(MAX) NULL ,
	[Body] VARCHAR(MAX) NULL ,
	[DateAdded] DATETIME NOT NULL 
		DEFAULT GETDATE() ,
	[DateSent] SMALLDATETIME NULL ,
	[EmailStatus] INT NOT NULL
)
GO

IF EXISTS (SELECT * FROM dbo.sysindexes WHERE name = N'ObjectKey' AND id = object_id(N'[Email]'))
DROP INDEX [Email].[ObjectKey]
GO

CREATE INDEX [ObjectKey] ON [Email] (ObjectKey)
GO

CREATE TABLE [Message] (
	[MessageKey] INT 
		IDENTITY (1 , 1) NOT NULL 
		PRIMARY KEY  CLUSTERED ,
	[ModuleKey] INT NOT NULL
		REFERENCES [Module] (ModuleKey) ,
	[ResourceKey] INT NOT NULL
		REFERENCES [Resource] (ResourceKey) ,
	[ObjectKey] INT NULL ,
	[Body] VARCHAR(MAX) NULL ,
	[LastModificationTime] DATETIME NOT NULL 
		DEFAULT GETDATE() ,
	[MessageStatus] INT NOT NULL 
)
GO

IF EXISTS (SELECT * FROM dbo.sysindexes WHERE name = N'ObjectKey' AND id = object_id(N'[Message]'))
DROP INDEX [Message].[ObjectKey]
GO

CREATE INDEX [ObjectKey] ON [Message] (ObjectKey)
GO

CREATE TABLE [Membership] (
	[MembershipKey] INT 
		IDENTITY (1 , 1) NOT NULL 
		PRIMARY KEY  CLUSTERED ,
	[VendorKey] INT NOT NULL
		REFERENCES [Company] (CompanyKey) ,
	[StartDate] DATETIME NOT NULL ,
	[EndDate] DATETIME NULL ,
	[RenewalDate] DATETIME NULL ,
	[AutomaticRenewal] BIT NULL ,
	[LastModificationTime] DATETIME NOT NULL 
		DEFAULT GETDATE()
)
GO

CREATE TABLE [Pricing] (
	[PricingKey] INT 
		IDENTITY (1 , 1) NOT NULL 
		PRIMARY KEY  CLUSTERED ,
	[CompanyKey] INT NOT NULL
		REFERENCES [Company] (CompanyKey) ,
	[PricingTypeKey] INT NOT NULL
		REFERENCES [LookUp] (LookUpKey) ,
	[StartAmount] MONEY NULL ,
	[EndAmount] MONEY NULL ,
	[Fee] MONEY NOT NULL ,
	[LastModificationTime] DATETIME NOT NULL 
		DEFAULT GETDATE() ,
	[SortOrder] FLOAT NULL 
)
GO

CREATE TABLE [Promotion] (
	[PromotionKey] INT 
		IDENTITY (1 , 1) NOT NULL 
		PRIMARY KEY  CLUSTERED ,
	[CompanyKey] INT NOT NULL
		REFERENCES [Company] (CompanyKey) ,
	[Title] VARCHAR(150) NOT NULL ,
	[PromotionCode] VARCHAR(150) NOT NULL ,
	[Amount] MONEY NULL ,
	[Percentage] FLOAT NOT NULL ,
	[StartDate] DATETIME NOT NULL ,
	[EndDate] DATETIME NULL ,
	[LastModificationTime] DATETIME NOT NULL 
		DEFAULT GETDATE()
)
GO

CREATE TABLE [Invoice] (
	[InvoiceKey] INT 
		IDENTITY (1 , 1) NOT NULL 
		PRIMARY KEY  CLUSTERED ,
	[VendorKey] INT NOT NULL
		REFERENCES [Company] (CompanyKey) ,
	[ReferenceNumber] VARCHAR(150) NULL ,
	[TransactionDate] DATETIME NOT NULL 
		DEFAULT GETDATE() ,
	[DueDate] DATETIME NOT NULL 
		DEFAULT GETDATE() ,
	[Amount] MONEY NOT NULL ,
	[Balance] MONEY NULL ,
	[LastModificationTime] DATETIME NOT NULL 
		DEFAULT GETDATE() ,
	[Status] INT NOT NULL 
)
GO

CREATE TABLE [InvoiceLine] (
	[InvoiceLineKey] INT 
		IDENTITY (1 , 1) NOT NULL 
		PRIMARY KEY  CLUSTERED ,
	[InvoiceKey] INT NOT NULL
		REFERENCES [Invoice] (InvoiceKey) ,
	[Quantity] INT NULL ,
	[Rate] MONEY NULL ,
	[Amount] MONEY NOT NULL ,
	[Description] VARCHAR(5000) NULL ,
	[SortOrder] FLOAT NULL 
)
GO

CREATE TABLE [Payment] (
	[PaymentKey] INT IDENTITY(1, 1) NOT NULL
		PRIMARY KEY  CLUSTERED ,
	[VendorKey] INT NOT NULL
		REFERENCES [Company] (CompanyKey) ,
	[PaymentTypeKey] INT NOT NULL
		REFERENCES [LookUp] (LookUpKey) ,
	[ReferenceNumber] VARCHAR(150) NULL ,
	[TransactionDate] DATETIME NOT NULL 
		DEFAULT GETDATE() ,
	[Amount] MONEY NOT NULL ,
	[Balance] MONEY NULL ,
	[Description] VARCHAR(5000) NULL ,
	[LastModificationTime] DATETIME NOT NULL 
		DEFAULT GETDATE() ,
)
GO

CREATE TABLE [PaymentApplied] (
	[PaymentAppliedKey] INT IDENTITY(1, 1) NOT NULL
		PRIMARY KEY  CLUSTERED ,
	[PaymentKey] INT NOT NULL
		REFERENCES [Payment] (PaymentKey) 
		ON DELETE CASCADE ,
	[InvociceKey] INT NOT NULL
		REFERENCES [Invoice] (InvoiceKey)
		ON DELETE CASCADE ,
	[Amount] MONEY NOT NULL ,
	[LastModificationTime] DATETIME NOT NULL 
		DEFAULT GETDATE()
)
GO

--CREATE TABLE [EmailActivity] (
--	[EntityActivityKey] INT IDENTITY(1, 1) NOT NULL
--		PRIMARY KEY  CLUSTERED ,
--	[ModuleKey] INT NOT NULL
--		REFERENCES [Module] (ModuleKey) ,
--	[EntityKey] INT NOT NULL
--		REFERENCES [Entity] (EntityKey) ,
--	[LastModificationTime] SMALLDATETIME NOT NULL 
--		DEFAULT GETDATE() ,
--	[EmailStatus] INT NULL 
--)
--GO


--------------------------------------------------------------------
----- TRIGGERS
--------------------------------------------------------------------

