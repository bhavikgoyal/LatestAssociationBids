USE [AssociationBids]
GO

DECLARE @approvedStatus INT = 101,
		@companyTypeKey INT = 1000,	-- Administration
		@portalKey INT = 1,	-- Administration Portal
		@companyKey INT = 0,
		@resourceTypeKey INT = 1100	-- Staff

---------------------------------------------------------------------
-----	ASSOCIATION BIDS COMPANY
---------------------------------------------------------------------

CREATE TABLE #C ([ID] VARCHAR(150) NULL, [Name] VARCHAR(150) NULL, [Address] VARCHAR(150) NULL, [Address2] VARCHAR(150) NULL, [City] VARCHAR(150) NULL, [State] VARCHAR(150) NULL, [Zip] VARCHAR(150) NULL)

INSERT INTO #C ([ID], [Name], [Address], [Address2], [City], [State], [Zip]) VALUES ('association_bids', 'Association Bids', NULL, NULL, NULL, NULL, NULL)

INSERT [Company] ([CompanyTypeKey], [PortalKey], [CompanyID], [Name], [Address], [Address2], [City], [State], [Zip], [BidRequestResponseDays], [BidSubmitDays], [DateAdded], [LastModificationTime], [Status])
SELECT @companyTypeKey, @portalKey AS [PortalKey], s.ID, s.[Name], s.[Address], s.[Address2], s.[City], s.[State], s.[Zip], 2 AS [BidRequestResponseDays], 10 AS [BidSubmitDays], GETDATE(), GETDATE(), @approvedStatus
FROM #C s
LEFT JOIN [Company] t ON s.[Name] = t.[Name]
WHERE t.CompanyKey IS NULL

-- Get CompanyKey
SELECT @companyKey = CompanyKey
FROM [Company]
WHERE [Name] = 'Association Bids'

---------------------------------------------------------------------
-----	ASSOCIATION BIDS CONTACT
---------------------------------------------------------------------

CREATE TABLE #R ([ID] VARCHAR(150) NULL, [GroupName] VARCHAR(150) NULL, [FirstName] VARCHAR(150) NULL, [LastName] VARCHAR(150) NULL, [Email] VARCHAR(150) NULL) 

INSERT INTO #R ([ID], [GroupName], [FirstName], [LastName], [Email]) VALUES ('0', 'Administrator', 'Administrator', NULL, 'admin@associationbids.com')
INSERT INTO #R ([ID], [GroupName], [FirstName], [LastName], [Email]) VALUES ('1', 'Staff', 'Staff', 'Contact', 'staff@associationbids.com')

INSERT [dbo].[Resource] ([CompanyKey], [ResourceTypeKey], [FirstName], [LastName], [Email], [Address], [Address2], [City], [State], [Zip], [PrimaryContact], [DateAdded], [LastModificationTime], [Status]) 

SELECT c.[CompanyKey], @resourceTypeKey, s.[FirstName], s.[LastName], s.[Email], c.Address, c.Address2, c.City, c.State, c.Zip, 
	CASE
		WHEN s.[ID] = '0' THEN 1
		ELSE 0
	END AS PrimaryContact, GETDATE() AS [DateAdded], GETDATE() AS [LastModificationTime], @approvedStatus
FROM #R s
JOIN [Company] c ON c.CompanyKey = @companyKey
LEFT JOIN [Resource] r ON (c.[CompanyKey] = r.[CompanyKey] AND s.[FirstName] = r.[FirstName] AND ISNULL(s.[LastName],'') = ISNULL(r.[LastName],''))
WHERE r.ResourceKey IS NULL

-- Group Members
INSERT INTO [GroupMember] ([GroupKey], [ResourceKey])
SELECT g.GroupKey, r.ResourceKey
FROM #R s
JOIN [Resource] r ON (r.[CompanyKey] = @companyKey AND s.[FirstName] = r.[FirstName] AND ISNULL(s.[LastName],'') = ISNULL(r.[LastName],''))
JOIN [Group] g ON s.[GroupName] = g.[Title]
LEFT JOIN [GroupMember] gm on (g.[GroupKey] = gm.[GroupKey] AND r.[ResourceKey] = gm.[ResourceKey])
WHERE gm.GroupMemberKey IS NULL

-- Users
INSERT [dbo].[User] ([ResourceKey], [Username], [Password], [DateAdded], [LastModificationTime], [Status]) 
SELECT r.ResourceKey, r.Email AS [Username], N'Oe58gljdfcwjVTIy6GQJc1Gh370CamYQwVbQATi6pgM=' AS [Password], GETDATE() AS [DateAdded], GETDATE() AS [LastModificationTime], @approvedStatus
FROM #R s
JOIN [Company] c ON c.CompanyKey = @companyKey
JOIN [Resource] r ON (c.[CompanyKey] = r.[CompanyKey] AND s.[FirstName] = r.[FirstName] AND ISNULL(s.[LastName],'') = ISNULL(r.[LastName],''))
LEFT JOIN [User] u on r.ResourceKey = u.ResourceKey
WHERE u.UserKey IS NULL


--------------------------------------------------------------------
----- PRICING
--------------------------------------------------------------------

if not exists (select 1 from [Pricing] where CompanyKey = @companyKey)
begin

INSERT INTO [dbo].[Pricing] ([CompanyKey], [PricingTypeKey], [StartAmount], [EndAmount], [Fee], [LastModificationTime], [SortOrder], [FeeType])
     VALUES (@companyKey, 1200, null, null, 125.00, getdate(), null, 'Fixed')
INSERT INTO [dbo].[Pricing] ([CompanyKey], [PricingTypeKey], [StartAmount], [EndAmount], [Fee], [LastModificationTime], [SortOrder], [FeeType])
     VALUES (@companyKey, 1201, null, null, 25.00, getdate(), null, 'Fixed')
INSERT INTO [dbo].[Pricing] ([CompanyKey], [PricingTypeKey], [StartAmount], [EndAmount], [Fee], [LastModificationTime], [SortOrder], [FeeType])
     VALUES (@companyKey, 1202, null, 500000, 5.00, getdate(), 1, 'Percentage')
INSERT INTO [dbo].[Pricing] ([CompanyKey], [PricingTypeKey], [StartAmount], [EndAmount], [Fee], [LastModificationTime], [SortOrder], [FeeType])
     VALUES (@companyKey, 1202, 500001, 100000000, 5000.00, getdate(), 2, 'Fixed')

end

DROP TABLE #C
DROP TABLE #R

GO