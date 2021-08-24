USE [AssociationBids]
GO

DECLARE @approvedStatus INT = 101,
		@companyTypeKey INT = 1001,	-- Management Company
		@portalKey INT = 2,	-- Management Company Portal
		@companyKey INT = 0,
		@resourceTypeKey INT = 1100	-- Staff

---------------------------------------------------------------------
-----	MANAGEMENT COMPANY
---------------------------------------------------------------------

CREATE TABLE #C ([ID] VARCHAR(150) NULL, [Name] VARCHAR(150) NULL, [Address] VARCHAR(150) NULL, [Address2] VARCHAR(150) NULL, [City] VARCHAR(150) NULL, [State] VARCHAR(150) NULL, [Zip] VARCHAR(150) NULL)

INSERT INTO #C ([ID], [Name], [Address], [Address2], [City], [State], [Zip]) VALUES ('management_company_1', 'Management Company 1', 'P.O.Box 12345', '', 'Tampa', 'FL', '33682')

INSERT [Company] ([CompanyTypeKey], [PortalKey], [CompanyID], [Name], [Address], [Address2], [City], [State], [Zip], [BidRequestResponseDays], [BidSubmitDays], [DateAdded], [LastModificationTime], [Status])
SELECT @companyTypeKey, @portalKey AS [PortalKey], s.ID, s.[Name], s.[Address], s.[Address2], s.[City], s.[State], s.[Zip], 2 AS [BidRequestResponseDays], 10 AS [BidSubmitDays], GETDATE(), GETDATE(), @approvedStatus
FROM #C s
LEFT JOIN [Company] t ON s.[Name] = t.[Name]
WHERE t.CompanyKey IS NULL

-- Get CompanyKey
SELECT @companyKey = CompanyKey
FROM [Company]
WHERE [Name] = 'Management Company 1'

---------------------------------------------------------------------
-----	PROPERTY
---------------------------------------------------------------------

CREATE TABLE #P ([ID] VARCHAR(150) NULL, [Name] VARCHAR(150) NULL, [Address] VARCHAR(150) NULL, [Address2] VARCHAR(150) NULL, [City] VARCHAR(150) NULL, [State] VARCHAR(150) NULL, [Zip] VARCHAR(150) NULL, [Latitude] FLOAT NULL, [Longitude] FLOAT NULL)

INSERT INTO #P ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('1', 'Property 1', '16400 Bonneville Drive', NULL, 'Tampa', 'FL', '33624', '28.108', '-82.5165')
INSERT INTO #P ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('2', 'Property 2', '179 Lakeview Drive', NULL, 'Oldsmar', 'FL', '34677', '28.0488', '-82.6773')
INSERT INTO #P ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('3', 'Property 3', '3210 59th Street', NULL, 'St. Petersburg', 'FL', '33707', '27.7368', '-82.7129')
INSERT INTO #P ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('4', 'Property 4', '1621 Gulf Blvd.', NULL, 'Clearwater', 'FL', '33767', '27.9385', '-82.8378')
INSERT INTO #P ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('5', 'Property 5', '217 North 12th Street', NULL, 'Tampa', 'FL', '33602', '27.9495', '-82.4466')

INSERT INTO [Property] ([CompanyKey], [Title], [NumberOfUnits], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude], [Status])
SELECT @companyKey, p.[Name], 50 AS [NumberOfUnits], p.[Address], p.[Address2], p.[City], p.[State], p.[Zip], p.[Latitude], p.[Longitude], @approvedStatus
FROM #P p
LEFT JOIN [Property] t on p.[Name] = t.[Title]
WHERE t.PropertyKey IS NULL

---------------------------------------------------------------------
-----	MANAGEMENT COMPANY CONTACT
---------------------------------------------------------------------

CREATE TABLE #R ([ID] VARCHAR(150) NULL, [GroupName] VARCHAR(150) NULL, [FirstName] VARCHAR(150) NULL, [LastName] VARCHAR(150) NULL, [Email] VARCHAR(150) NULL) 

INSERT INTO #R ([ID], [GroupName], [FirstName], [LastName], [Email]) VALUES ('0', 'Administrator', 'Company 1', 'Contact', 'company_1@associationbids.com')
INSERT INTO #R ([ID], [GroupName], [FirstName], [LastName], [Email]) VALUES ('1', 'Property Manager', 'Manager 1', 'Contact', 'manager_1@associationbids.com')
INSERT INTO #R ([ID], [GroupName], [FirstName], [LastName], [Email]) VALUES ('2', 'Property Manager', 'Manager 2', 'Contact', 'manager_2@associationbids.com')
INSERT INTO #R ([ID], [GroupName], [FirstName], [LastName], [Email]) VALUES ('3', 'Property Manager', 'Manager 3', 'Contact', 'manager_3@associationbids.com')
INSERT INTO #R ([ID], [GroupName], [FirstName], [LastName], [Email]) VALUES ('4', 'Property Manager', 'Manager 4', 'Contact', 'manager_4@associationbids.com')
INSERT INTO #R ([ID], [GroupName], [FirstName], [LastName], [Email]) VALUES ('5', 'Property Manager', 'Manager 5', 'Contact', 'manager_5@associationbids.com')

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
JOIN [Resource] r ON (r.[CompanyKey] = @companyKey AND s.[FirstName] = r.[FirstName] AND s.[LastName] = r.[LastName])
JOIN [Group] g ON s.[GroupName] = g.[Title]
LEFT JOIN [GroupMember] gm on (g.[GroupKey] = gm.[GroupKey] AND r.[ResourceKey] = gm.[ResourceKey])
WHERE gm.GroupMemberKey IS NULL

-- Property Resource
INSERT INTO [PropertyResource] ([PropertyKey], [ResourceKey], [DateAdded], [Status])
SELECT p.PropertyKey, r.ResourceKey, GETDATE(), @approvedStatus
FROM #R s
JOIN #P t ON s.[ID] = t.[ID]
JOIN [Resource] r ON (r.[CompanyKey] = @companyKey AND s.[FirstName] = r.[FirstName] AND s.[LastName] = r.[LastName])
JOIN [Property] p ON (t.[Name] = p.[Title])
LEFT JOIN [PropertyResource] pr on (p.[PropertyKey] = pr.[PropertyKey] AND r.[ResourceKey] = pr.[ResourceKey])
WHERE pr.PropertyResourceKey IS NULL

-- Users
INSERT [dbo].[User] ([ResourceKey], [Username], [Password], [DateAdded], [LastModificationTime], [Status]) 
SELECT r.ResourceKey, r.Email AS [Username], N'Oe58gljdfcwjVTIy6GQJc1Gh370CamYQwVbQATi6pgM=' AS [Password], GETDATE() AS [DateAdded], GETDATE() AS [LastModificationTime], @approvedStatus
FROM #R s
JOIN [Company] c ON c.CompanyKey = @companyKey
JOIN [Resource] r ON (c.[CompanyKey] = r.[CompanyKey] AND s.[FirstName] = r.[FirstName] AND ISNULL(s.[LastName],'') = ISNULL(r.[LastName],''))
LEFT JOIN [User] u on r.ResourceKey = u.ResourceKey
WHERE u.UserKey IS NULL

---------------------------------------------------------------------
-----	VENDOR
---------------------------------------------------------------------

SET @companyTypeKey = 1003	-- Company Vendor (Need to change this to 1002 = Vendor)
SET @portalKey = 3	-- Vendor Portal

CREATE TABLE #V ([ID] VARCHAR(150) NULL, [Name] VARCHAR(150) NULL, [Address] VARCHAR(150) NULL, [Address2] VARCHAR(150) NULL, [City] VARCHAR(150) NULL, [State] VARCHAR(150) NULL, [Zip] VARCHAR(150) NULL, [Latitude] FLOAT NULL, [Longitude] FLOAT NULL)

INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('1', 'Vendor Company 1', 'PO Box 82981', '', 'Tampa', 'FL', '33682-2981', '28.06', '-82.46')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('2', 'Vendor Company 2', '1733 Squirrel Prairie Rd.', '', 'Brooksville', 'FL', '34604', '28.487', '-82.4536')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('3', 'Vendor Company 3', 'P.O. Box 740655', '', 'Atlanta', 'GA', '30374-0655', '33.75', '-84.39')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('4', 'Vendor Company 4', '16609 Ashton Green Drive', '', 'Lutz', 'FL', '33558', '28.1763', '-82.5078')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('5', 'Vendor Company 5', '7627 Blue Spring Drive.', '', 'LandLakes', 'FL', '34637', '28.2902', '-82.4644')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('6', 'Vendor Company 6', 'P.O. Box 1080', '', 'Seffner', 'FL', '33583', '27.98', '-82.28')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('7', 'Vendor Company 7', '302 W U.S. Highway 92', '', 'Seffner', 'FL', '33584', '28.0014', '-82.2909')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('8', 'Vendor Company 8', '14618 N. Dale Mabry Hwy.', '', 'Tampa', 'FL', '33618', '28.0771', '-82.5023')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('9', 'Vendor Company 9', 'PO Box 930471', '', 'Atlanta', 'GA', '31193', '33.66', '-84.39')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('10', 'Vendor Company 10', 'P.O. BOX. 445', '', 'Lutz', 'FL', '33548', '28.1341', '-82.4807')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('11', 'Vendor Company 11', '5414 Cloud Peak Dr.', '', 'Lutz', 'FL', '33558', '28.1763', '-82.5078')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('12', 'Vendor Company 12', 'P.O. Box 1678', '', 'Lutz', 'FL', '33548', '28.1341', '-82.4807')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('13', 'Vendor Company 13', '16635 Ashton Green Drive', '', 'Lutz', 'FL', '33558', '28.1763', '-82.5078')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('14', 'Vendor Company 14', '29642 Birds Eye Drive', '', 'Wesley Chapel', 'FL', '33543', '28.2108', '-82.2909')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('15', 'Vendor Company 15', '12505 N. Nebraska Ave.', '', 'Tampa', 'FL', '33612', '28.0564', '-82.4428')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('16', 'Vendor Company 16', '1957 Sever Drive', '', 'Clearwater', 'FL', '33764', '27.9385', '-82.7401')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('17', 'Vendor Company 17', '334 East Lake Rd., Suite 133', '', 'Palm Harbor', 'FL', '34685', '28.0987', '-82.6861')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('18', 'Vendor Company 18', '8124 Washington St.', '', 'Port Richey', 'FL', '34668', '28.3031', '-82.7023')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('19', 'Vendor Company 19', '2950 N 28 Terrace', '', 'Hollywood', 'FL', '33020', '26.0182', '-80.1596')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('20', 'Vendor Company 20', 'PO Box 5816', '', 'Clearwater', 'FL', '33758', '27.9656', '-82.8006')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('21', 'Vendor Company 21', 'PO Box 6412', '', 'Sun City Center', 'FL', '33571', '27.722', '-82.4338')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('22', 'Vendor Company 22', '1690 Pine Island Rd.', '', 'Merritt Island', 'FL', '32953', '28.4709', '-80.6993')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('23', 'Vendor Company 23', '1553 Savannah Ave.', '', 'Tarpon Springs', 'FL', '34689', '28.1437', '-82.7671')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('24', 'Vendor Company 24', '11788 66th Street North', '', 'Largo', 'FL', '33773', '27.878', '-82.7536')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('25', 'Vendor Company 25', '2788 Summerdale Drive', '', 'Clearwater', 'FL', '33761', '28.0305', '-82.7185')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('26', 'Vendor Company 26', '5601 Haines Rd. N.', '', 'St. Petersburg', 'FL', '33714', '27.8185', '-82.6753')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('27', 'Vendor Company 27', '179 Lakeview Way', '', 'Oldsmar', 'FL', '34677', '28.046', '-82.6807')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('28', 'Vendor Company 28', '17420 N. U.S. Hwy. 41, Suite 107', '', 'Lutz', 'FL', '33549', '28.1495', '-82.4428')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('29', 'Vendor Company 29', '156 Lakeview Way', '', 'Oldsmar', 'FL', '34677', '28.046', '-82.6807')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('30', 'Vendor Company 30', '35863 US Hwy. 19 N.', '', 'Palm Harbor', 'FL', '34684', '28.0716', '-82.7239')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('31', 'Vendor Company 31', '1005 19th Street North', '', 'St. Petersburg', 'FL', '33713', '27.7897', '-82.6807')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('32', 'Vendor Company 32', '10380 SW Village Center Dr. # 353', '', 'Port St Lucie', 'FL', '34987', '27.2978', '-80.5216')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('33', 'Vendor Company 33', '7177 30th Ave N', '', 'St. Petersburg', 'FL', '33710', '27.792', '-82.7239')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('34', 'Vendor Company 34', '8126 Blaikie Court', '', 'Sarasota', 'FL', '34240', '27.3275', '-82.3343')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('35', 'Vendor Company 35', '9470 Lakeview Drive', '', 'New Port Richey', 'FL', '34654', '28.2985', '-82.6159')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('36', 'Vendor Company 36', '10950 47th Street', '', 'Clearwater', 'FL', '33762', '27.9007', '-82.6861')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('37', 'Vendor Company 37', '5631 Ryals Road', '', 'Zephyrhills', 'FL', '33541', '28.2303', '-82.2257')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('38', 'Vendor Company 38', 'D/B/A Alliance Fire & Safety PO Box 208', '', 'Venice', 'FL', '34284', '27.0999', '-82.449')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('39', 'Vendor Company 39', '144 Annwood Road', '', 'Palm Harbor', 'FL', '34685', '28.0987', '-82.6861')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('40', 'Vendor Company 40', 'P.O. Box 10340', '', 'Tampa', 'FL', '33679', '27.9499', '-82.51')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('41', 'Vendor Company 41', '11602 Tarpon Springs Road', '', 'Odessa', 'FL', '33556', '28.1222', '-82.5835')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('42', 'Vendor Company 42', '3520 66th Ave N', '', 'Pinellas Park', 'FL', '33781', '27.8436', '-82.7077')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('43', 'Vendor Company 43', '4214 Canterberry Drive', '', 'Holiday', 'FL', '34691', '28.1902', '-82.7671')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('44', 'Vendor Company 44', '511 Central Park Drive', '', 'Largo', 'FL', '33771', '27.9047', '-82.7617')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('45', 'Vendor Company 45', '6150 54th Ave N Suite A', '', 'Kenneth City', 'FL', '33709', '27.822', '-82.7401')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('46', 'Vendor Company 46', '5004 Newton Avenue S', '', 'Gulfport', 'FL', '33707', '27.7514', '-82.7293')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('47', 'Vendor Company 47', '5155 - 1st Avenue S', '', 'St Petersburg', 'FL', '33707', '27.7514', '-82.7293')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('48', 'Vendor Company 48', '2830 Scherer Dr., Ste.300', '', 'St Petersburg', 'FL', '33716', '27.8757', '-82.6537')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('49', 'Vendor Company 49', 'PO Box 255', '', 'Valrico', 'FL', '33595', '27.938', '-82.2462')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('50', 'Vendor Company 50', '13003  US 19 N.', '', 'Clearwater', 'FL', '33764', '27.9385', '-82.7401')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('51', 'Vendor Company 51', '26 Citrus Drive', '', 'Palm Harbor', 'FL', '34684', '28.0716', '-82.7239')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('52', 'Vendor Company 52', '751 Canyon Drive Suite 100', '', 'Coppell', 'FL', 'TX 75019', '32.9619', '-96.9961')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('53', 'Vendor Company 53', '1391 Lady Marion Lane', '', 'Dunedin', 'FL', '34698', '28.034', '-82.7832')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('54', 'Vendor Company 54', '1391 Lady Marion Lane', '', 'St Petersburg', 'FL', '33707', '27.7514', '-82.7293')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('55', 'Vendor Company 55', '1851 Bonita Way South', '', 'St Petersburg', 'FL', '33712', '27.748', '-82.6645')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('56', 'Vendor Company 56', '8440 Bay Pines blvd', '', 'St Petersburg', 'FL', '33709', '27.822', '-82.7401')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('57', 'Vendor Company 57', '6330 Pine Hill Rd. #17', '', 'Port Richey', 'FL', '34668', '28.3031', '-82.7023')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('58', 'Vendor Company 58', '9225 Ulmerton Rd. Suite 410', '', 'Largo', 'FL', '33771', '27.9047', '-82.7617')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('59', 'Vendor Company 59', '6400 123rd Avenue N', '', 'Largo', 'FL', '33773', '27.878', '-82.7536')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('60', 'Vendor Company 60', '12001 31st Court N', '', 'St. Petersbrg', 'FL', ' 33716-1854', '27.8757', '-82.6537')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('61', 'Vendor Company 61', '2101 Calumet St.', '', 'Clearwater', 'FL', '33765', '27.9765', '-82.7428')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('62', 'Vendor Company 62', '8080 Ulmerton Road,  Suite D', '', 'Largo', 'FL', '33771-3922', '27.9047', '-82.7617')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('63', 'Vendor Company 63', 'PO Box 997', '', 'Tarpon Springs', 'FL', '34688', '28.1391', '-82.6807')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('64', 'Vendor Company 64', '12600 S Belcher Road, #101A', '', 'Largo', 'FL', '33773', '27.878', '-82.7536')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('65', 'Vendor Company 65', '380 Scarlet Blvd.', '', 'Oldsmar', 'FL', '34677', '28.046', '-82.6807')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('66', 'Vendor Company 66', 'P.O. Box 12107', '', 'St. Petersburg', 'FL', '33733', '27.77', '-82.68')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('67', 'Vendor Company 67', '1398 Main Street', '', 'Dunedin', 'FL', '34698', '28.034', '-82.7832')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('68', 'Vendor Company 68', 'P.O. Box 509058', '', 'San Diego', 'CA', '92150-9058', '32.98', '-117.08')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('69', 'Vendor Company 69', '36314 US 19', '', 'Palm Harbor', 'FL', '34684', '28.0716', '-82.7239')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('70', 'Vendor Company 70', '111 North Missouri Avenue', '', 'Largo', 'FL', '33770', '27.918', '-82.794')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('71', 'Vendor Company 71', 'Dept. 56 - 4220584634 P.O. Box 78004', '', 'Pheonix', 'AZ', '85062-8004', '33.45', '-111.97')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('72', 'Vendor Company 72', '13075 US Hwy 19 N', '', 'Clearwater', 'FL', '33764', '27.9385', '-82.7401')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('73', 'Vendor Company 73', 'PO Box 3363', '', 'Seminole', 'FL', '33775', '27.8574', '-82.7952')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('74', 'Vendor Company 74', '3750 70th Avenue North', '', 'Pinellas Park', 'FL', '33781', '27.8436', '-82.7077')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('75', 'Vendor Company 75', '1904 Harding Street', '', 'Clearwater', 'FL', '33765', '27.9765', '-82.7428')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('76', 'Vendor Company 76', 'P.O. Box 2134', '', 'Carol Stream', 'FL', '60132-2134', '41.92', '-88.12')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('77', 'Vendor Company 77', '4712 W. Leila Ave', '', 'Tampa', 'FL', '33616', '27.8687', '-82.524')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('78', 'Vendor Company 78', '4120 Mink Rd.', '', 'Sarasota', 'FL', '34235', '27.3647', '-82.4807')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('79', 'Vendor Company 79', '4372 North Access Rd.', '', 'Englewood', 'FL', '34224', '26.9288', '-82.3126')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('80', 'Vendor Company 80', '12819 126th Terrace N.', '', 'Largo', 'FL', '33774', '27.8848', '-82.8264')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('81', 'Vendor Company 81', '369 Mears Blvd', '', 'Oldsmar', 'FL', '34677', '28.046', '-82.6807')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('82', 'Vendor Company 82', 'Attn: Accounts Receivable PO Box 88741', '', 'Chicago', 'IL', '60680', '41.88', '-87.64')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('83', 'Vendor Company 83', '719 S. Missouri Ave', '', 'Clearwater', 'FL', '33756', '27.9413', '-82.794')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('84', 'Vendor Company 84', 'PO Box 13257', '', 'Tampa', 'FL', '33681', '27.896', '-82.5125')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('85', 'Vendor Company 85', '2111 N 15th Street', '', 'Tampa', 'FL', '33605', '27.9565', '-82.4265')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('86', 'Vendor Company 86', 'PO Box 2929', '', 'Land O Lakes', 'FL', '34639', '28.2425', '-82.4428')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('87', 'Vendor Company 87', '8707 N. Highland Ave.', '', 'Tampa', 'FL', '33604-1332', '28.0165', '-82.459')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('88', 'Vendor Company 88', '9556 Gallagher Road', '', 'Dover', 'FL', '33527', '27.9732', '-82.204')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('89', 'Vendor Company 89', '6600 N. Florida Ave', '', 'Tampa', 'FL', '33604', '28.0165', '-82.459')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('90', 'Vendor Company 90', 'Dept. CH 10320', '', 'Tampa', 'FL', '33604', '28.0165', '-82.459')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('91', 'Vendor Company 91', 'PO Box 273604', '', 'Tampa', 'FL', '33688', '28.0619', '-82.5036')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('92', 'Vendor Company 92', '8803 Industrial Drive', '', 'Tampa', 'FL', '33637', '28.0474', '-82.3611')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('93', 'Vendor Company 93', '2161 E County Rd 540A #229', '', 'Lakeland', 'FL', '33813', '27.9581', '-81.9426')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('94', 'Vendor Company 94', 'QFC8100 Park Blvd. C27', '', 'Pinellas Park', 'FL', '33781', '27.8436', '-82.7077')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('95', 'Vendor Company 95', '14919 Coldwater Lane', '', 'Tampa', 'FL', '33624', '28.0899', '-82.524')
INSERT INTO #V ([ID], [Name], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude]) VALUES ('96', 'Vendor Company 96', '2415 28th Avenue N', '', 'St. Petersburg', 'FL', '33713', '27.7897', '-82.6807')

INSERT [Company] ([CompanyTypeKey], [PortalKey], [CompanyID], [Name], [Address], [Address2], [City], [State], [Zip], [DateAdded], [LastModificationTime], [Status])
SELECT @companyTypeKey, @portalKey AS [PortalKey], lower(replace(s.[Name], ' ', '_')) as [CompanyID], s.[Name], s.[Address], s.[Address2], s.[City], s.[State], s.[Zip], GETDATE(), GETDATE(), @approvedStatus
FROM #V s
LEFT JOIN [Company] t ON s.[Name] = t.[Name]
WHERE t.CompanyKey IS NULL

---------------------------------------------------------------------
-----	COMPANY VENDOR
---------------------------------------------------------------------

INSERT [dbo].[CompanyVendor] ([CompanyKey], [VendorKey], [LastModificationTime], [Status]) 
SELECT @companyKey AS [CompanyKey], t.[CompanyKey] AS [VendorKey], GETDATE(), 100 AS [Status]
FROM #V s
JOIN [Company] t ON s.[Name] = t.[Name]
LEFT JOIN [CompanyVendor] cv ON (cv.CompanyKey = @companyKey AND t.CompanyKey = cv.VendorKey)
WHERE cv.CompanyVendorKey IS NULL

---------------------------------------------------------------------
-----	VENDOR SERVICE
---------------------------------------------------------------------

DECLARE @serviceKey INT

SELECT @serviceKey = ServiceKey
FROM [Service]
WHERE Title = 'Landscaping/Lawn Care'

INSERT [dbo].[VendorService] ([VendorKey], [ServiceKey], [SortOrder]) 
SELECT t.[CompanyKey] AS [VendorKey], @serviceKey AS [ServiceKey], 1 AS [SortOrder]
FROM #V s
JOIN [Company] t ON s.[Name] = t.[Name]

---------------------------------------------------------------------
-----	SERVICE AREA
---------------------------------------------------------------------

DECLARE @radiusInMiles INT = 50

INSERT [dbo].[ServiceArea] ([VendorKey], [Address], [Address2], [City], [State], [Zip], [Latitude], [Longitude], [Radius])
SELECT t.[CompanyKey] AS [VendorKey], s.[Address], s.[Address2], s.[City], s.[State], s.[Zip], s.[Latitude], s.[Longitude], @radiusInMiles AS [Radius]
FROM #V s
JOIN [Company] t ON s.[Name] = t.[Name]

---------------------------------------------------------------------
-----	VENDOR CONTACT
---------------------------------------------------------------------

SET @resourceTypeKey = 1101	-- Contact

INSERT [dbo].[Resource] ([CompanyKey], [ResourceTypeKey], [FirstName], [LastName], [Email], [Address], [Address2], [City], [State], [Zip], [PrimaryContact], [DateAdded], [LastModificationTime], [Status]) 

SELECT t.[CompanyKey], @resourceTypeKey, ('Vendor ' + s.ID) AS [FirstName], 'Contact' AS [LastName], ('vendor_' + s.ID + '@associationbids.com') AS [Email],
	s.Address, s.Address2, s.City, s.State, s.Zip, 1 as PrimaryContact, GETDATE() AS [DateAdded], GETDATE() AS [LastModificationTime], @approvedStatus
FROM #V s
JOIN [Company] t ON s.[Name] = t.[Name]
LEFT JOIN [Resource] r ON (t.[CompanyKey] = r.[CompanyKey] AND r.[Email] = 'vendor_' + s.ID + '@associationbids.com')
WHERE r.ResourceKey IS NULL

---------------------------------------------------------------------
-----	USER
---------------------------------------------------------------------

INSERT [dbo].[User] ([ResourceKey], [Username], [Password], [DateAdded], [LastModificationTime], [Status]) 
SELECT r.ResourceKey, r.Email AS [Username], N'Oe58gljdfcwjVTIy6GQJc1Gh370CamYQwVbQATi6pgM=' AS [Password], GETDATE() AS [DateAdded], GETDATE() AS [LastModificationTime], @approvedStatus
FROM #V s
JOIN [Company] t ON s.[Name] = t.[Name]
JOIN [Resource] r ON t.[CompanyKey] = r.[CompanyKey]
LEFT JOIN [User] u on r.ResourceKey = u.ResourceKey
WHERE u.UserKey IS NULL

---------------------------------------------------------------------

DROP TABLE #C
DROP TABLE #P
DROP TABLE #R
DROP TABLE #V
GO
