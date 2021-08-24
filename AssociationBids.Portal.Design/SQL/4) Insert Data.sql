
USE [AssociationBids]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[config_System_InsertData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[config_System_InsertData]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
CREATE PROCEDURE [dbo].[config_System_InsertData]
AS
SET NOCOUNT ON

DECLARE @approvedStatus INT = 2,
		@portalKey INT


--------------------------------------------------------------------
-----	PORTAL
--------------------------------------------------------------------

IF EXISTS (SELECT 1 FROM [Portal] WHERE [PortalID] = 'portal')
BEGIN
	SELECT @portalKey = PortalKey FROM [Portal] WHERE [PortalID] = 'portal'
END
ELSE
BEGIN
	INSERT INTO [Portal] ([PortalID], [Title], [DateAdded], [LastModificationTime], [Status])
	values ( 'portal', 'Association Bids Portal', GETDATE(), GETDATE(), @approvedStatus )

	-- Get the IDENTITY value for the row just inserted.
	SELECT @portalKey = SCOPE_IDENTITY()
END

IF EXISTS (SELECT 1 FROM [Portal] WHERE [PortalID] = 'company')
BEGIN
	SELECT @portalKey = PortalKey FROM [Portal] WHERE [PortalID] = 'company'
END
ELSE
BEGIN
	INSERT INTO [Portal] ([PortalID], [Title], [DateAdded], [LastModificationTime], [Status])
	values ( 'company', 'Company Portal', GETDATE(), GETDATE(), @approvedStatus )
END

IF EXISTS (SELECT 1 FROM [Portal] WHERE [PortalID] = 'vendor')
BEGIN
	SELECT @portalKey = PortalKey FROM [Portal] WHERE [PortalID] = 'vendor'
END
ELSE
BEGIN
	INSERT INTO [Portal] ([PortalID], [Title], [DateAdded], [LastModificationTime], [Status])
	values ( 'vendor', 'Vendor Portal', GETDATE(), GETDATE(), @approvedStatus )
END


--------------------------------------------------------------------
-----	GROUPS
--------------------------------------------------------------------

CREATE TABLE #G ( [Title] VARCHAR(150) )

INSERT INTO #G ( Title ) VALUES  ( 'Administrator' )
INSERT INTO #G ( Title ) VALUES  ( 'Supervisor' )
INSERT INTO #G ( Title ) VALUES  ( 'Property Manager' )
INSERT INTO #G ( Title ) VALUES  ( 'Staff' )
INSERT INTO #G ( Title ) VALUES  ( 'Vendor' )
INSERT INTO #G ( Title ) VALUES  ( 'Guest' )
INSERT INTO #G ( Title ) VALUES  ( 'Other' )

INSERT INTO [Group] ( [Title] )
SELECT T.Title
FROM #G T
LEFT JOIN [Group] G ON T.Title = G.Title
WHERE G.GroupKey IS NULL

DROP TABLE #G

--------------------------------------------------------------------
-----	MODULES
--------------------------------------------------------------------
-- select * from Module order by title

CREATE TABLE #M ([Id] INT UNIQUE, [Title] VARCHAR(150), [Controller] VARCHAR(150), [Action] VARCHAR(150), [Image] VARCHAR(150) )

BEGIN TRANSACTION

INSERT INTO #M ([Id], [Title], [Controller], [Action], [Image]) VALUES (1, 'Home', 'Home', 'Index', 'home.png')
INSERT INTO #M ([Id], [Title], [Controller], [Action], [Image]) VALUES (2, 'Billing', 'Billing', 'Index', 'billing.png')
INSERT INTO #M ([Id], [Title], [Controller], [Action], [Image]) VALUES (3, 'Settings', 'Settings', 'Index', 'settings.png')

INSERT INTO #M ([Id], [Title], [Controller], [Action], [Image]) VALUES (100, 'Bid Requests', 'BidRequest', 'Index', 'bid-request.png')
INSERT INTO #M ([Id], [Title], [Controller], [Action], [Image]) VALUES (101, 'Bids', 'Bid', 'Index', 'bid.png')
INSERT INTO #M ([Id], [Title], [Controller], [Action], [Image]) VALUES (102, 'Properties', 'Property', 'Index', 'property.png')
INSERT INTO #M ([Id], [Title], [Controller], [Action], [Image]) VALUES (103, 'Staff', 'Staff', 'Index', 'staff.png')
INSERT INTO #M ([Id], [Title], [Controller], [Action], [Image]) VALUES (104, 'Vendors', 'Vendor', 'Index', 'vendor.png')
INSERT INTO #M ([Id], [Title], [Controller], [Action], [Image]) VALUES (105, 'Verify Vendors', 'VendorVerify', 'Index', 'vendor-verify.png')
INSERT INTO #M ([Id], [Title], [Controller], [Action], [Image]) VALUES (106, 'Work Orders', 'WorkOrder', 'Index', 'work-order.png')

INSERT INTO #M ([Id], [Title], [Controller], [Action], [Image]) VALUES (200, 'Invoices', 'Invoice', 'Index', 'invoice.png')
INSERT INTO #M ([Id], [Title], [Controller], [Action], [Image]) VALUES (201, 'Payments', 'Payment', 'Index', 'payment.png')

INSERT INTO #M ([Id], [Title], [Controller], [Action], [Image]) VALUES (300, 'Account', 'Account', 'Index', 'password.png')
INSERT INTO #M ([Id], [Title], [Controller], [Action], [Image]) VALUES (301, 'Credit Cards', 'CreditCard', 'Index', 'credit-card.png')
INSERT INTO #M ([Id], [Title], [Controller], [Action], [Image]) VALUES (302, 'Insurance', 'Insurance', 'Index', 'insurance.png')
INSERT INTO #M ([Id], [Title], [Controller], [Action], [Image]) VALUES (303, 'Profile', 'Profile', 'Index', 'user.png')
INSERT INTO #M ([Id], [Title], [Controller], [Action], [Image]) VALUES (304, 'Services', 'CompanyService', 'Index', 'service.png')
INSERT INTO #M ([Id], [Title], [Controller], [Action], [Image]) VALUES (305, 'Service Areas', 'ServiceArea', 'Index', 'service-area.png')

INSERT INTO #M ([Id], [Title], [Controller], [Action], [Image]) VALUES (701, 'Company', 'Company', 'Index', '')
INSERT INTO #M ([Id], [Title], [Controller], [Action], [Image]) VALUES (702, 'Documents', 'Document', 'Index', '')
INSERT INTO #M ([Id], [Title], [Controller], [Action], [Image]) VALUES (703, 'Messages', 'Message', 'Index', '')
INSERT INTO #M ([Id], [Title], [Controller], [Action], [Image]) VALUES (704, 'Notes', 'Note', 'Index', '')
INSERT INTO #M ([Id], [Title], [Controller], [Action], [Image]) VALUES (705, 'Register', 'Register', 'Index', '')
INSERT INTO #M ([Id], [Title], [Controller], [Action], [Image]) VALUES (706, 'Resources', 'Resource', 'Index', '')
INSERT INTO #M ([Id], [Title], [Controller], [Action], [Image]) VALUES (707, 'Reminders', 'Reminder', 'Index', '')
INSERT INTO #M ([Id], [Title], [Controller], [Action], [Image]) VALUES (708, 'Tasks', 'Task', 'Index', '')
INSERT INTO #M ([Id], [Title], [Controller], [Action], [Image]) VALUES (709, 'Reward Points', 'Rewards', 'Index', 'reward-points.png')


INSERT INTO [Module] ([ModuleKey], [Title], [Controller], [Action], [Image])
SELECT t.Id, t.[Title], t.[Controller], t.[Action], t.[Image]
FROM #M t
LEFT JOIN [Module] m ON (t.Title = m.Title AND t.Controller = m.Controller)
WHERE m.ModuleKey IS NULL
ORDER BY t.Id


COMMIT TRANSACTION

DROP TABLE #M


----------------------------------------------------------------------
-----	 Disable Modules
----------------------------------------------------------------------

CREATE TABLE #MO (Title VARCHAR(150))

/*

INSERT INTO #MO (Title) VALUES ('Sales')


UPDATE Module
SET [Status] = 1	-- Pending Approval
FROM Module M
JOIN #MO M2 ON M.Title = M2.Title

*/

DROP TABLE #MO




--------------------------------------------------------------------
-----	GROUP MODULE ACCESS
--------------------------------------------------------------------

/*
Read	1
Create	2
Update	4
Delete	8
*/

DECLARE @groupKey INT = 0
DECLARE @access INT = 0

SELECT @groupKey = GroupKey FROM [Group] WHERE Title = 'Administrator'
SELECT @access = 15

INSERT INTO [GroupModuleAccess] (PortalKey, GroupKey, ModuleKey, Access)
SELECT @portalKey, @groupKey, m.ModuleKey, @access
FROM [Module] m
LEFT JOIN [GroupModuleAccess] gma ON (m.ModuleKey = gma.ModuleKey and gma.GroupKey = @groupKey and gma.Access = @access)
WHERE gma.GroupModuleAccessKey IS NULL



CREATE TABLE #GMA (ModuleTitle varchar(150), Supervisor INT, PropertyManager INT, Vendor INT, Guest INT)

-----	GROUP MODULE ACCESS (PROPERTY MANAGER)

INSERT INTO #GMA (ModuleTitle, Supervisor, PropertyManager, Vendor, Guest) VALUES ('Home', 1, 1, 1, 0)
INSERT INTO #GMA (ModuleTitle, Supervisor, PropertyManager, Vendor, Guest) VALUES ('Billing', 1, 0, 1, 0)
INSERT INTO #GMA (ModuleTitle, Supervisor, PropertyManager, Vendor, Guest) VALUES ('Settings', 1, 1, 1, 0)
INSERT INTO #GMA (ModuleTitle, Supervisor, PropertyManager, Vendor, Guest) VALUES ('Bid Requests', 15, 15, 1, 0)
INSERT INTO #GMA (ModuleTitle, Supervisor, PropertyManager, Vendor, Guest) VALUES ('Bids', 5, 5, 7, 0)
INSERT INTO #GMA (ModuleTitle, Supervisor, PropertyManager, Vendor, Guest) VALUES ('Properties', 15, 15, 0, 0)
INSERT INTO #GMA (ModuleTitle, Supervisor, PropertyManager, Vendor, Guest) VALUES ('Staff', 7, 0, 0, 0)
INSERT INTO #GMA (ModuleTitle, Supervisor, PropertyManager, Vendor, Guest) VALUES ('Vendors', 7, 1, 0, 0)
INSERT INTO #GMA (ModuleTitle, Supervisor, PropertyManager, Vendor, Guest) VALUES ('Verify Vendors', 7, 0, 0, 0)
INSERT INTO #GMA (ModuleTitle, Supervisor, PropertyManager, Vendor, Guest) VALUES ('Work Orders', 15, 15, 5, 0)
INSERT INTO #GMA (ModuleTitle, Supervisor, PropertyManager, Vendor, Guest) VALUES ('Invoices', 7, 0, 1, 0)
INSERT INTO #GMA (ModuleTitle, Supervisor, PropertyManager, Vendor, Guest) VALUES ('Payments', 7, 0, 7, 0)
INSERT INTO #GMA (ModuleTitle, Supervisor, PropertyManager, Vendor, Guest) VALUES ('Account', 5, 5, 5, 0)
INSERT INTO #GMA (ModuleTitle, Supervisor, PropertyManager, Vendor, Guest) VALUES ('Credit Cards', 1, 0, 7, 0)
INSERT INTO #GMA (ModuleTitle, Supervisor, PropertyManager, Vendor, Guest) VALUES ('Insurance', 0, 0, 7, 0)
INSERT INTO #GMA (ModuleTitle, Supervisor, PropertyManager, Vendor, Guest) VALUES ('Profile', 5, 5, 5, 0)
INSERT INTO #GMA (ModuleTitle, Supervisor, PropertyManager, Vendor, Guest) VALUES ('Services', 0, 0, 7, 0)
INSERT INTO #GMA (ModuleTitle, Supervisor, PropertyManager, Vendor, Guest) VALUES ('Service Areas', 0, 0, 7, 0)
INSERT INTO #GMA (ModuleTitle, Supervisor, PropertyManager, Vendor, Guest) VALUES ('Company', 7, 1, 5, 0)
INSERT INTO #GMA (ModuleTitle, Supervisor, PropertyManager, Vendor, Guest) VALUES ('Documents', 15, 15, 15, 0)
INSERT INTO #GMA (ModuleTitle, Supervisor, PropertyManager, Vendor, Guest) VALUES ('Messages', 1, 1, 1, 0)
INSERT INTO #GMA (ModuleTitle, Supervisor, PropertyManager, Vendor, Guest) VALUES ('Notes', 7, 7, 7, 0)
INSERT INTO #GMA (ModuleTitle, Supervisor, PropertyManager, Vendor, Guest) VALUES ('Register', 0, 0, 7, 3)
INSERT INTO #GMA (ModuleTitle, Supervisor, PropertyManager, Vendor, Guest) VALUES ('Resources', 7, 7, 7, 0)
INSERT INTO #GMA (ModuleTitle, Supervisor, PropertyManager, Vendor, Guest) VALUES ('Reminders', 7, 7, 7, 0)
INSERT INTO #GMA (ModuleTitle, Supervisor, PropertyManager, Vendor, Guest) VALUES ('Tasks', 7, 7, 7, 0)
INSERT INTO #GMA (ModuleTitle, Supervisor, PropertyManager, Vendor, Guest) VALUES ('Reward Points', 7, 1, 0, 0)


if exists (select 1 from #GMA where ModuleTitle not in ( select Title from [Module] ) )
begin
	print 'modules missing'
	select moduletitle from #GMA where ModuleTitle not in ( select Title from [Module] )
end

INSERT INTO [GroupModuleAccess] (PortalKey, GroupKey, ModuleKey, Access)
SELECT @portalKey, g.GroupKey, m.ModuleKey, gma.Supervisor
FROM #GMA gma
JOIN [Group] g on g.Title = 'Supervisor'
JOIN [Module] m on gma.ModuleTitle = m.Title
LEFT JOIN [GroupModuleAccess] gma2 ON (m.ModuleKey = gma2.ModuleKey and g.GroupKey = gma2.GroupKey and gma.Supervisor = gma2.Access)
WHERE gma.Supervisor > 0
and gma2.GroupModuleAccessKey IS NULL

union

SELECT @portalKey, g.GroupKey, m.ModuleKey, gma.PropertyManager
FROM #GMA gma
JOIN [Group] g on g.Title = 'Property Manager'
JOIN [Module] m on gma.ModuleTitle = m.Title
LEFT JOIN [GroupModuleAccess] gma2 ON (m.ModuleKey = gma2.ModuleKey and g.GroupKey = gma2.GroupKey and gma.PropertyManager = gma2.Access)
WHERE gma.PropertyManager > 0
and gma2.GroupModuleAccessKey IS NULL

union

SELECT @portalKey, g.GroupKey, m.ModuleKey, gma.Vendor
FROM #GMA gma
JOIN [Group] g on g.Title = 'Vendor'
JOIN [Module] m on gma.ModuleTitle = m.Title
LEFT JOIN [GroupModuleAccess] gma2 ON (m.ModuleKey = gma2.ModuleKey and g.GroupKey = gma2.GroupKey and gma.Vendor = gma2.Access)
WHERE gma.Vendor > 0
and gma2.GroupModuleAccessKey IS NULL

union

SELECT @portalKey, g.GroupKey, m.ModuleKey, gma.Guest
FROM #GMA gma
JOIN [Group] g on g.Title = 'Guest'
JOIN [Module] m on gma.ModuleTitle = m.Title
LEFT JOIN [GroupModuleAccess] gma2 ON (m.ModuleKey = gma2.ModuleKey and g.GroupKey = gma2.GroupKey and gma.Guest = gma2.Access)
WHERE gma.Guest > 0
and gma2.GroupModuleAccessKey IS NULL



--------------------------------------------------------------------
-----	REPORTS
--------------------------------------------------------------------
/*
CREATE TABLE #R ( [ModuleTitle] VARCHAR(150), [Title] VARCHAR(150), [Controller] VARCHAR(150), [Action] VARCHAR(150) )

INSERT INTO #R ([ModuleTitle] ,[Title], [Controller], [Action]) VALUES ('Resident Directory', 'Resident Directory', 'Reports', 'Residents')
INSERT INTO #R ([ModuleTitle] ,[Title], [Controller], [Action]) VALUES ('Resident Directory', 'Mailing Labels', 'Reports', 'MailingLabels')
     
INSERT INTO [Report] ([ModuleKey], [Title], [Controller], [Action])
SELECT m.ModuleKey, t.[Title], t.[Controller], t.[Action]
FROM #R t
JOIN [Module] m on t.ModuleTitle = m.TItle
LEFT JOIN [Report] r ON (m.ModuleKey = r.ModuleKey AND t.Title = r.Title AND t.Controller = r.Controller AND t.Action = r.Action)
WHERE r.ReportKey IS NULL

DROP TABLE #R
*/

--------------------------------------------------------------------
----- STATE
--------------------------------------------------------------------

CREATE TABLE #S ( StateKey VARCHAR(2), Title VARCHAR(150) )

INSERT INTO #S ( [StateKey], [Title] ) VALUES ( 'AL', 'Alabama' )
INSERT INTO #S ( [StateKey], [Title] ) VALUES ( 'AK', 'Alaska' )
INSERT INTO #S ( [StateKey], [Title] ) VALUES ( 'AZ', 'Arizona' )
INSERT INTO #S ( [StateKey], [Title] ) VALUES ( 'AR', 'Arkansas' )
INSERT INTO #S ( [StateKey], [Title] ) VALUES ( 'CA', 'California' )
INSERT INTO #S ( [StateKey], [Title] ) VALUES ( 'CO', 'Colorado' )
INSERT INTO #S ( [StateKey], [Title] ) VALUES ( 'CT', 'Connecticut' )
INSERT INTO #S ( [StateKey], [Title] ) VALUES ( 'DE', 'Delaware' )
INSERT INTO #S ( [StateKey], [Title] ) VALUES ( 'FL', 'Florida' )
INSERT INTO #S ( [StateKey], [Title] ) VALUES ( 'GA', 'Georgia' )
INSERT INTO #S ( [StateKey], [Title] ) VALUES ( 'HI', 'Hawaii' )
INSERT INTO #S ( [StateKey], [Title] ) VALUES ( 'ID', 'Idaho' )
INSERT INTO #S ( [StateKey], [Title] ) VALUES ( 'IL', 'Illinois' )
INSERT INTO #S ( [StateKey], [Title] ) VALUES ( 'IN', 'Indiana' )
INSERT INTO #S ( [StateKey], [Title] ) VALUES ( 'IA', 'Iowa' )
INSERT INTO #S ( [StateKey], [Title] ) VALUES ( 'KS', 'Kansas' )
INSERT INTO #S ( [StateKey], [Title] ) VALUES ( 'KY', 'Kentucky' )
INSERT INTO #S ( [StateKey], [Title] ) VALUES ( 'LA', 'Louisiana ' )
INSERT INTO #S ( [StateKey], [Title] ) VALUES ( 'ME', 'Maine' )
INSERT INTO #S ( [StateKey], [Title] ) VALUES ( 'MD', 'Maryland' )
INSERT INTO #S ( [StateKey], [Title] ) VALUES ( 'MA', 'Massachusetts' )
INSERT INTO #S ( [StateKey], [Title] ) VALUES ( 'MI', 'Michigan' )
INSERT INTO #S ( [StateKey], [Title] ) VALUES ( 'MN', 'Minnesota' )
INSERT INTO #S ( [StateKey], [Title] ) VALUES ( 'MS', 'Mississippi' )
INSERT INTO #S ( [StateKey], [Title] ) VALUES ( 'MO', 'Missouri' )
INSERT INTO #S ( [StateKey], [Title] ) VALUES ( 'MT', 'Montana' )
INSERT INTO #S ( [StateKey], [Title] ) VALUES ( 'NE', 'Nebraska' )
INSERT INTO #S ( [StateKey], [Title] ) VALUES ( 'NV', 'Nevada' )
INSERT INTO #S ( [StateKey], [Title] ) VALUES ( 'NH', 'New Hampshire' )
INSERT INTO #S ( [StateKey], [Title] ) VALUES ( 'NJ', 'New Jersey' )
INSERT INTO #S ( [StateKey], [Title] ) VALUES ( 'NM', 'New Mexico' )
INSERT INTO #S ( [StateKey], [Title] ) VALUES ( 'NY', 'New York' )
INSERT INTO #S ( [StateKey], [Title] ) VALUES ( 'NC', 'North Carolina' )
INSERT INTO #S ( [StateKey], [Title] ) VALUES ( 'ND', 'North Dakota' )
INSERT INTO #S ( [StateKey], [Title] ) VALUES ( 'OH', 'Ohio' )
INSERT INTO #S ( [StateKey], [Title] ) VALUES ( 'OK', 'Oklahoma' )
INSERT INTO #S ( [StateKey], [Title] ) VALUES ( 'OR', 'Oregon' )
INSERT INTO #S ( [StateKey], [Title] ) VALUES ( 'PA', 'Pennsylvania' )
INSERT INTO #S ( [StateKey], [Title] ) VALUES ( 'RI', 'Rhode Island' )
INSERT INTO #S ( [StateKey], [Title] ) VALUES ( 'SC', 'South Carolina' )
INSERT INTO #S ( [StateKey], [Title] ) VALUES ( 'SD', 'South Dakota' )
INSERT INTO #S ( [StateKey], [Title] ) VALUES ( 'TN', 'Tennessee' )
INSERT INTO #S ( [StateKey], [Title] ) VALUES ( 'TX', 'Texas' )
INSERT INTO #S ( [StateKey], [Title] ) VALUES ( 'UT', 'Utah' )
INSERT INTO #S ( [StateKey], [Title] ) VALUES ( 'VT', 'Vermont' )
INSERT INTO #S ( [StateKey], [Title] ) VALUES ( 'VA', 'Virginia' )
INSERT INTO #S ( [StateKey], [Title] ) VALUES ( 'WA', 'Washington' )
INSERT INTO #S ( [StateKey], [Title] ) VALUES ( 'DC', 'Washington, D.C.' )
INSERT INTO #S ( [StateKey], [Title] ) VALUES ( 'WV', 'West Virginia' )
INSERT INTO #S ( [StateKey], [Title] ) VALUES ( 'WI', 'Wisconsin' )
INSERT INTO #S ( [StateKey], [Title] ) VALUES ( 'WY', 'Wyoming' )

INSERT INTO dbo.State ( StateKey, Title )
SELECT t.StateKey, t.Title
FROM #S t
LEFT JOIN dbo.State s ON t.Title = s.Title
WHERE s.StateKey IS NULL

DROP TABLE #S

--------------------------------------------------------------------
----- SERVICE
--------------------------------------------------------------------


set nocount on

create table #R (ServiceKey varchar(50), Title varchar(150))

insert into #R (ServiceKey, Title) values ('accountant-cpa','Accountant/CPA')
insert into #R (ServiceKey, Title) values ('air-conditioning','Air Conditioning')
insert into #R (ServiceKey, Title) values ('architects-architectural-review','Architects/Architectural Review')
insert into #R (ServiceKey, Title) values ('asphalt-paving-maintenace-repair','Asphalt Paving/Maintenace/Repair')
insert into #R (ServiceKey, Title) values ('attorneys','Attorneys')
insert into #R (ServiceKey, Title) values ('balcony-restoration','Balcony Restoration')
insert into #R (ServiceKey, Title) values ('banking-financial-services','Banking/Financial Services')
insert into #R (ServiceKey, Title) values ('builders-developers','Builders/Developers')
insert into #R (ServiceKey, Title) values ('cableinternetphone','Cable/Internet/Phone')
insert into #R (ServiceKey, Title) values ('carpentry','Carpentry')
insert into #R (ServiceKey, Title) values ('carpentry-cleaning','Carpet Cleaning')
insert into #R (ServiceKey, Title) values ('carpentry-installation','Carpet Installation')
insert into #R (ServiceKey, Title) values ('catch-basin-cleaning','Catch Basin Cleaning')
insert into #R (ServiceKey, Title) values ('collections','Collections')
insert into #R (ServiceKey, Title) values ('concierge-services','Concierge Services')
insert into #R (ServiceKey, Title) values ('concrete-repair','Concrete Repair')
insert into #R (ServiceKey, Title) values ('construction','Construction')
insert into #R (ServiceKey, Title) values ('consulting','Consulting')
insert into #R (ServiceKey, Title) values ('credit-reporting','Credit Reporting')
insert into #R (ServiceKey, Title) values ('deck-products-and-services','Deck Products and Services')
insert into #R (ServiceKey, Title) values ('disaster-planning','Disaster Planning')
insert into #R (ServiceKey, Title) values ('electrical-service','Electrical Service')
insert into #R (ServiceKey, Title) values ('elevators','Elevators')
insert into #R (ServiceKey, Title) values ('emergency-restoration-services','Emergency Restoration Services')
insert into #R (ServiceKey, Title) values ('engineers','Engineers')
insert into #R (ServiceKey, Title) values ('environmental-safety-inspections','Environmental & Safety Inspections')
insert into #R (ServiceKey, Title) values ('environmental-services','Environmental Services')
insert into #R (ServiceKey, Title) values ('fire-safety-equipment','Fire Safety Equipment')
insert into #R (ServiceKey, Title) values ('flooring','Flooring')
insert into #R (ServiceKey, Title) values ('fountains','Fountains')
insert into #R (ServiceKey, Title) values ('general-contractor','General Contractor')
insert into #R (ServiceKey, Title) values ('grout-and-tile-cleaningstone-polishing','Grout and Tile Cleaning/Stone Polishing')
insert into #R (ServiceKey, Title) values ('gutters','Gutters')
insert into #R (ServiceKey, Title) values ('heating-ventilating-air-conditioning','Heating, Ventilating, Air Conditioning')
insert into #R (ServiceKey, Title) values ('insulation','Insulation')
insert into #R (ServiceKey, Title) values ('insurance','Insurance')
insert into #R (ServiceKey, Title) values ('irrigation','Irrigation')
insert into #R (ServiceKey, Title) values ('lake-and-pond-management','Lake and Pond Management')
insert into #R (ServiceKey, Title) values ('landscapinglawn-care','Landscaping/Lawn Care')
insert into #R (ServiceKey, Title) values ('laundry-room-equipmentmaintenance','Laundry Room Equipment/Maintenance')
insert into #R (ServiceKey, Title) values ('lender','Lender')
insert into #R (ServiceKey, Title) values ('lighting','Lighting')
insert into #R (ServiceKey, Title) values ('locksmith','Locksmith')
insert into #R (ServiceKey, Title) values ('mailing-services','Mailing Services')
insert into #R (ServiceKey, Title) values ('maintenance','Maintenance')
insert into #R (ServiceKey, Title) values ('marketing','Marketing')
insert into #R (ServiceKey, Title) values ('masonry','Masonry')
insert into #R (ServiceKey, Title) values ('newsletterspublicationsprinting','Newsletters/Publications/Printing')
insert into #R (ServiceKey, Title) values ('painting-services-and-retailers','Painting Services and Retailers')
insert into #R (ServiceKey, Title) values ('panel-brick-repair','Panel Brick Repair')
insert into #R (ServiceKey, Title) values ('parkingtowing','Parking/Towing')
insert into #R (ServiceKey, Title) values ('pest-control','Pest Control')
insert into #R (ServiceKey, Title) values ('pet-waste-removal','Pet Waste Removal')
insert into #R (ServiceKey, Title) values ('plumbing','Plumbing')
insert into #R (ServiceKey, Title) values ('pool-services','Pool Services')
insert into #R (ServiceKey, Title) values ('recreationalplayground-equipment','Recreational/Playground Equipment')
insert into #R (ServiceKey, Title) values ('reserve-studies','Reserve Studies')
insert into #R (ServiceKey, Title) values ('restoration-services','Restoration Services')
insert into #R (ServiceKey, Title) values ('roofing','Roofing')
insert into #R (ServiceKey, Title) values ('roofing-manufacturer','Roofing Manufacturer')
insert into #R (ServiceKey, Title) values ('sealcoating','Sealcoating')
insert into #R (ServiceKey, Title) values ('security-products-and-services','Security Products and Services')
insert into #R (ServiceKey, Title) values ('siding','Siding')
insert into #R (ServiceKey, Title) values ('snow-removal','Snow Removal')
insert into #R (ServiceKey, Title) values ('tree-care-services','Tree Care Services')
insert into #R (ServiceKey, Title) values ('utilitysolarenergy-services','Utility/Solar/Energy Services')
insert into #R (ServiceKey, Title) values ('ventilating','Ventilating')
insert into #R (ServiceKey, Title) values ('waste-management-services','Waste Management Services')
insert into #R (ServiceKey, Title) values ('waterproofing','Waterproofing')
insert into #R (ServiceKey, Title) values ('websites-internet-service','Websites / Internet Service')
insert into #R (ServiceKey, Title) values ('windows-and-doors','Windows and Doors')

insert into [Service] (Title)
select s.Title
from #R s
left join [Service] s2 on s.Title = s2.Title
where s2.ServiceKey is null

drop table #R


--------------------------------------------------------------------
----- AGREEMENT
--------------------------------------------------------------------

if not exists (select 1 from [Agreement])
begin

	declare @agreement varchar(max) = 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.

    Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem. Ut enim ad minima veniam, quis nostrum exercitationem ullam corporis suscipit laboriosam, nisi ut aliquid ex ea commodi consequatur? Quis autem vel eum iure reprehenderit qui in ea voluptate velit esse quam nihil molestiae consequatur, vel illum qui dolorem eum fugiat quo voluptas nulla pariatur?'

	insert into [Agreement] (PortalKey, Title, AgreementDate, [Description], LastModificationTime, [Status])
	values (@portalKey, 'User Agreement', '1/1/2019', @agreement, GETDATE(), @approvedStatus)
end



SET NOCOUNT OFF
GO


EXEC [dbo].[config_System_InsertData]
GO

