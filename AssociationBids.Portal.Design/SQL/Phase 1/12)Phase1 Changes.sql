use [associationbids]
go

-- api_BidReqeust_Approved_Reject 'Accept',14936,0,0
alter Procedure [dbo].[api_BidReqeust_Approved_Reject]
@status VARCHAR(150),
@BidVendorKey int, 
@errorCode INT OUTPUT,
@WorkorderKey INT Output
AS
SET NOCOUNT ON
Begin

declare @bidrequestkey1 int
declare @workkey int = 0
declare @workbidvenkey int
select @bidrequestkey1 = bv.BidRequestKey from BidVendor as bv 
inner join BidRequest as br on br.BidRequestKey  = bv.BidRequestKey
where bv.BidVendorKey = @BidVendorKey
Declare @ModuleKey int
Select top(1) @ModuleKey=ModuleKey From Module where Module.Controller='BidRequest'

if (@status = 'Accept')
begin
declare @statusTypeKey2 int
select  @statusTypeKey2 = (select l.LookUpKey from LookUp as l  where l.LookUpTypeKey = lu.LookUpTypeKey  and Title = 'Completed') from LookUpType as lu where Title = 'Bid Request Status'

declare @statusTypeKey3 int
select  @statusTypeKey3 = (select l.LookUpKey from LookUp as l  where l.LookUpTypeKey = lu.LookUpTypeKey  and Title = 'Accepted') from LookUpType as lu where Title = 'Bid Status'

declare @statusTypeKey4 int
select  @statusTypeKey4 = (select l.LookUpKey from LookUp as l  where l.LookUpTypeKey = lu.LookUpTypeKey  and Title = 'Rejected') from LookUpType as lu where Title = 'Bid Status'


--update BidVendor set BidVendorStatus = @statusTypeKey3 where BidVendorKey = @BidRequestKey
update BidVendor set BidVendorStatus = @statusTypeKey3 where BidVendorKey = @BidVendorKey
update BidVendor set BidVendorStatus = @statusTypeKey4 where BidRequestKey = @bidrequestkey1 and  BidVendorStatus   != @statusTypeKey3

if((select ModuleKey from BidRequest where BidRequestKey = @bidrequestkey1)=@ModuleKey)
begin

update BidRequest set BidRequestStatus = @statusTypeKey2 where BidRequestKey =  @bidrequestkey1
INSERT INTO BidRequest
                         (PropertyKey, ResourceKey, ServiceKey, Title, BidDueDate, StartDate, EndDate, Description, DateAdded, LastModificationTime, BidRequestStatus, DefaultRespondByDate, ModuleKey)
        (select top(1) PropertyKey,ResourceKey,ServiceKey,Title,BidDueDate,StartDate,EndDate,Description,DateAdded,LastModificationTime,601,DefaultRespondByDate,106 from BidRequest where BidRequestKey = @bidrequestkey1)

		select  @workkey = @@IDENTITY
		select @WorkorderKey = @workkey
		print(@WorkorderKey)

INSERT INTO BidVendor
                         (BidRequestKey, VendorKey, ResourceKey, BidVendorID, IsAssigned, RespondByDate, DateAdded, LastModificationTime, BidVendorStatus)
(select top(1) @workkey,VendorKey,ResourceKey,BidVendorID,IsAssigned,RespondByDate ,DateAdded,LastModificationTime,802 from BidVendor where BidVendorKey= @BidVendorKey and BidRequestKey = @bidrequestkey1 and BidVendorStatus = @statusTypeKey3)
select  @workbidvenkey = @@IDENTITY


INSERT INTO Document
                         ( ModuleKey, ObjectKey, FileName, FileSize, LastModificationTime)
select (select ModuleKey from Module where Controller = 'PMWorkOrders'),@workkey,FileName,FileSize,LastModificationTime 
from Document where ObjectKey = @bidrequestkey1  and ModuleKey in ((select ModuleKey from Module where Controller = 'PMBidRequests'),100) 

INSERT INTO Document
                         ( ModuleKey, ObjectKey, FileName, FileSize, LastModificationTime)
select (select ModuleKey from Module where Controller = 'PMWorkOrders'),@workbidvenkey,FileName,FileSize,LastModificationTime 
from Document where ObjectKey = @BidVendorKey  and ModuleKey in ((select ModuleKey from Module where Controller = 'PMBidRequests'),100) 


INSERT INTO Bid
                         (BidVendorKey, ResourceKey, Title, Total, Description, LastModificationTime, BidStatus)
(select (select BidVendorKey from BidVendor where BidVendorKey = @workbidvenkey),ResourceKey,Title,Total,Description,LastModificationTime,802 from Bid where BidVendorKey =@BidVendorKey)



INSERT INTO Note
                         ( ModuleKey, ResourceKey, ObjectKey, Description, LastModificationTime,Status)
select (select ModuleKey from Module where Controller = 'BidRequest'),ResourceKey,@workkey,Description,LastModificationTime,Status from Note where ObjectKey = @bidrequestkey1  and ModuleKey in (select ModuleKey from Module where Controller = 'BidRequest'
) 

INSERT INTO Message
 ( ModuleKey, ResourceKey, ObjectKey, Body, LastModificationTime,MessageStatus)
select (select ModuleKey from Module where Controller = 'WorkOrder'),ResourceKey,@workbidvenkey,Body,LastModificationTime,MessageStatus from Message 
where ObjectKey = @BidVendorKey  and ModuleKey in (select ModuleKey from Module where Controller = 'BidRequest') 

end
set @errorCode = @@ERROR
end

else if @status = 'Reject'
begin
declare @statusTypeKey5 int
select  @statusTypeKey5 = (select l.LookUpKey from LookUp as l  where l.LookUpTypeKey = lu.LookUpTypeKey  and Title = 'Rejected') from LookUpType as lu where Title = 'Bid Status'
update BidVendor set BidVendorStatus = @statusTypeKey5 where BidVendorKey = @BidVendorKey
set @errorCode = @@ERROR
end
Else
Begin
set @errorCode = 1
End


Declare @Table1 table
(
FileCopyTo nvarchar(max)
)
Declare @Table2 table
(
FilePastTo nvarchar(max)
)
insert into @Table1 select Convert(nvarchar, ObjectKey)+' '+FileName as FileCopyTo
from Document where ObjectKey = @bidrequestkey1  and ModuleKey in (select ModuleKey from Module where Controller = 'PMBidRequests')

insert into @Table2 select Convert(nvarchar, ObjectKey)+' '+FileName as FilePastTo
from Document where ObjectKey = @workkey  and ModuleKey in (select ModuleKey from Module where Controller = 'PMWorkOrders')

select j.FileCopyTo,k.FilePastTo from 
(select a.FileCopyTo  , ROW_NUMBER() over(order by (SELECT 1000)) rownum from @Table1 as a 

) j

inner join 
(select a.FilePastTo, ROW_NUMBER() over(order by (SELECT 1000)) rownum from @Table2 as a) k
on j.rownum = k.rownum

end
GO


---------------------------------------------


-- api_PM_GetDocumentsForBid 106,10900
Alter Procedure [dbo].[api_PM_GetDocumentsForBid]
@ModuleKey int,
@ObjectKey int
As
Begin
--if(@ModuleKey = 100)
--begin

--select * from Document as d where (ModuleKey = (select ModuleKey from Module where Controller = 'PMBidRequests') or ModuleKey = 100)  and d.ObjectKey = @ObjectKey
--end
--else
--begin
--select * from Document as d where (ModuleKey = (select ModuleKey from Module where Controller = 'PMWorkOrders') or ModuleKey = 106)  and d.ObjectKey = @ObjectKey
--end

select * from Document as d 
where ((ModuleKey = (select ModuleKey from Module 
	where Controller = 'PMWorkOrders') or (ModuleKey = (select ModuleKey from Module 
		where Controller = 'PMBidRequests') or ModuleKey = 106 or  ModuleKey = 100))  and d.ObjectKey = @ObjectKey)

End
GO

----------------------------------------------------------------------
--api_Vendors_GetAllVendorsForBidRequest  88,0
  
Alter procedure [dbo].[api_Vendors_GetAllVendorsForBidRequest]  
@BidRequestKey int,  
@errorCode int output  
As  
begin  
  
declare @resourceKey int  
select @resourceKey = ResourceKey from BidRequest where BidRequestKey = @BidRequestKey  
  
DECLARE @GEO1 GEOGRAPHY, @LAT VARCHAR(10), @LONG VARCHAR(10)                      
                      
select @LAT =isnull(Latitude,''), @LONG =isnull(Longitude,'') from Property where PropertyKey = (select br.propertykey from bidrequest br where br.BidRequestKey = @BidRequestkey)                      
                      
SET @geo1= geography::Point(@LAT, @LONG, 4326)                  
  
  declare  @moduleKey int   
declare  @parentkey int   
  
select  @moduleKey  =  ( select  ModuleKey from BidRequest where BidRequestKey = @BidRequestKey)     
select  @parentkey  =  (select ParentBidRequestKey from BidRequest where BidRequestKey = @BidRequestKey)    

if(@moduleKey = 100 and @parentkey  is  not null)  
begin 

--Select Name as CompanyName, LegalName as VendorName, '' as LastWorkDate, CompanyKey as VendorKey from  Company as cp  
--inner join VendorService as vs on vs.VendorKey = cp.CompanyKey where vs.ServiceKey = @ServiceKey  
  
  
select distinct CompanyKey, ResourceKey,CompanyName, VendorName,Work,Work2,Email, Fax, Address,Address2,City,State, Zip,WebSite,  
 isStared,    
      (select  max(LastModificationTime)       
from BidVendor where bidvendorkey in (select bidvendorkey from bidvendor bv1        
join BidRequest br1 on br1.BidRequestKey = bv1.BidRequestKey        
where vendorkey = i.CompanyKey and br1.BidRequestStatus = 602) and BidVendorStatus = 802) as 'LastWorkDate'  
 ,ModuleKey,StateKey from  
 (select cp.CompanyKey, Name as CompanyName, isnull(rs.FirstName,'') + ' ' + isnull(rs.LastName,'') as 'VendorName',  
 isnull((select top 1 1 from StarVendor cv where cv.VendorKey = cp.CompanyKey and ResourceKey = @resourceKey),0) as 'isStared',  
 rs.ResourceKey,rs.Work,rs.Work2,rs.Email,rs.Fax,rs.Address,rs.Address2,rs.City,  
 case When ((select Title from State where StateKey = rs.State) = null or (select Title from State where StateKey = rs.State) = '') then rs.State  
 else (select Title from State where StateKey = rs.State) end as State,rs.Zip,cp.WebSite,  
 (select ModuleKey from BidRequest where BidRequestKey = @BidRequestKey) as ModuleKey,rs.State as StateKey  
from Company as cp   
inner join resource as rs on rs.CompanyKey = cp.CompanyKey and rs.PrimaryContact = 1  
inner join VendorService as vs on vs.VendorKey = cp.CompanyKey and vs.ServiceKey in (select br.ServiceKey from BidRequest as br where br.BidRequestKey = @BidRequestkey)  
inner join ServiceArea as sa on sa.vendorkey = vs.vendorkey    
where cp.CompanyTypeKey in (select LookUpKey from LookUp where lookup.LookUpTypeKey in (select LookUpType.lookuptypekey from LookUpType where LookUpType.Title = 'Company Type') and LookUp.Title in ('Company Vendor','Vendor'))  
and cp.companykey not in (select bv.vendorkey from BidVendor bv where bv.BidRequestKey = @BidRequestKey)  
and cp.status = 101 and (Convert(float, LEFT(CONVERT(VARCHAR,(@geo1.STDistance(geography::Point(ISNULL(sa.Latitude,0), ISNULL(sa.Longitude,0), 4326)))/1000),5))/1.609344) < (sa.radius * 5) + 5  
)i  
  
select @errorCode = @@Error  
  end

else  
begin
select distinct CompanyKey, ResourceKey,CompanyName, VendorName,Work,Work2,Email, Fax, Address,Address2,City,State, Zip,WebSite,  
 isStared,    
(select  max (LastModificationTime)         
from BidVendor where bidvendorkey in (select bidvendorkey from bidvendor bv1        
join BidRequest br1 on br1.BidRequestKey = bv1.BidRequestKey        
where vendorkey = i.CompanyKey and br1.BidRequestStatus = 602 and ModuleKey = 106) and BidVendorStatus = 802) as 'LastWorkDate'           
 ,ModuleKey,StateKey from  
 (select cp.CompanyKey, Name as CompanyName, isnull(rs.FirstName,'') + ' ' + isnull(rs.LastName,'') as 'VendorName',  
 isnull((select top 1 1 from StarVendor cv where cv.VendorKey = cp.CompanyKey and ResourceKey = @resourceKey),0) as 'isStared',  
 rs.ResourceKey,rs.Work,rs.Work2,rs.Email,rs.Fax,rs.Address,rs.Address2,rs.City,  
 case When ((select Title from State where StateKey = rs.State) = null or (select Title from State where StateKey = rs.State) = '') then rs.State  
 else (select Title from State where StateKey = rs.State) end as State,rs.Zip,cp.WebSite,  
 (select ModuleKey from BidRequest where BidRequestKey = @BidRequestKey) as ModuleKey,rs.State as StateKey  
from Company as cp   
inner join resource as rs on rs.CompanyKey = cp.CompanyKey and rs.PrimaryContact = 1  
inner join VendorService as vs on vs.VendorKey = cp.CompanyKey and vs.ServiceKey in (select br.ServiceKey from BidRequest as br where br.BidRequestKey = @BidRequestkey)  
inner join ServiceArea as sa on sa.vendorkey = vs.vendorkey    
where cp.CompanyTypeKey in (select LookUpKey from LookUp where lookup.LookUpTypeKey in (select LookUpType.lookuptypekey from LookUpType where LookUpType.Title = 'Company Type') and LookUp.Title in ('Company Vendor','Vendor'))  
and cp.companykey not in (select bv.vendorkey from BidVendor bv where bv.BidRequestKey = @BidRequestKey)  
and cp.status = 101 and (Convert(float, LEFT(CONVERT(VARCHAR,(@geo1.STDistance(geography::Point(ISNULL(sa.Latitude,0), ISNULL(sa.Longitude,0), 4326)))/1000),5))/1.609344) < (sa.radius * 5) + 5  
)i  
  
select @errorCode = @@Error  

end


end
GO

-------------------------------------------------------------------


alter procedure [dbo].[Site_GetAllVendorBidNotSubmmited]          
as          
begin          
        
select  pm.CardHolderFirstName+pm.CardHolderLastName As CardHoldername, Pm.CvvNumber as Cvvnumber, BR.Title, bv.vendorkey, br.bidrequestKey,    
CONVERT(varchar,bv.BidvendorKey) as bvid, (Select LookUpkey from  LookUp where title = 'No Bid Fee') as PayMentType,    
pm.MaskedCCNumber as ccNumber,(select top 1  CONVERT(int,Fee) from  Pricing where  PricingTypeKey = 1201 ) as Amt,     
R.Email,R.Zip as PostalCode, R.Address as Line1, r.Address2 as add2, pm.StripeTokenID as stripeToken,pm.PaymentMethodID,pm.CardExpiryMonth,r.city, pm.CardExpiryYear,R.State As State  ,R.Description as Description     
from  BidVendor Bv          
inner  join PaymentMethod Pm on  Pm.companyKey = Bv.vendorKey           
inner  join   Company  c on   c.CompanyKey =  bv.VendorKey          
inner  join  BidRequest br on  br.BidRequestKey = bv.BidRequestKey         
inner  join  LookUp  l on l.LookUpKey = bv.BidVendorStatus          
inner join  Resource  R  on R.CompanyKey =  c.CompanyKey          
 where  bv.BidVendorStatus= 702   and  bv.respondbydate < getdate() and      
 bv.bidVendorKey not in (select  ReferenceNumber from  Payment where  ReferenceNumber = bv.bidVendorKey )       
 

 update bidvendor     
set bidvendor.bidvendorstatus = 703   
where bidvendor.bidvendorstatus = 702    
and respondbydate < getdate()    
and (select bidrequeststatus from bidrequest where bidrequest.bidrequestkey = bidvendor.bidrequestkey) = 601    

end
GO

------------------------------------------------------------------------------------------------


---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

  
--site_Property_GetAllforbid 1062, 0
Alter PROCEDURE [dbo].[site_Property_GetAllforbid]  
@ResourceKey int,  
@CompanyKey int


AS  
begin 

declare @GroupKey int  
-- select  @GroupKey =    count(GroupKey) from GroupMember Where ResourceKey= @ResourceKey and  GroupKey = 1  
select  @GroupKey =    count(GroupKey) from GroupMember Where ResourceKey=  CASE WHEN @ResourceKey = 0 Then ResourceKey  else @ResourceKey END and  GroupKey = 1  

if @GroupKey  > 0  
begin  
select distinct p.PropertyKey, TRIM(p.Title) as Title   
from [Property]  p 

left  join  PropertyResource pr on  p.PropertyKey = p.PropertyKey
-- where  companykey = @CompanyKey   order by Title asc
where companykey = CASE WHEN @CompanyKey = 0 Then companykey  else @CompanyKey END order by Title asc

end  

else
begin
select  p.PropertyKey, TRIM(p.Title) as Title  from  PropertyResource pr 
inner  join  property p on  p.PropertyKey = pr.PropertyKey
where  Pr.ResourceKey = @ResourceKey
end  
end
GO


----------------------------------------------------------------------------------------

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

-- [site_BidRequest_GetVendorsForBidRequestByAlgorithm] 3123,106  
Alter procedure [dbo].[site_User_GetForAdmin]  
@ResourceKey int,
@errorCode int  output
As  
begin  
 Select rs.ResourceKey,rs.Email, rs.FirstName+ ' '+rs.LastName as FirstName from [user] as us  
 inner join Resource as rs on rs.ResourceKey = us.ResourceKey
 inner join company as cp on cp.CompanyKey = rs.CompanyKey where rs.ResourceKey = @ResourceKey

 --select * from company
end
GO



----------------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
Alter PROCEDURE [dbo].[site_Vendor_Insert_New]    
    
 @CompanyName    varchar(500),    
 @Address  varchar(500),    
 @Address2  varchar(500),    
 @City   varchar(500),    
 @StateKey  varchar(2),    
 @zip   varchar(150),    
 @FirstName   varchar(150),    
 @LastName   varchar(150),  
  @Work   nvarchar(max),  
 @Work2   varchar(150),    
 @Email          varchar(500),    
 @Description    varchar(500),    
 @Fax   varchar(500),    
 @Website  varchar(500),     
 @Title       varchar(500),    
 @Resourcekey    int,    
 @companyvalue INT OUTPUT,
 @ResValue INT OUTPUT
    
AS    
SET NOCOUNT ON    
    
 if(@StateKey='0')    
  begin    
  set @StateKey=null    
  end    
declare @vendorkey int    
declare @companytype int    
declare @status int    
declare @insurancekey int    

declare @Resourcevalue int    
select @companytype = LookUpKey from LookUp where Title = 'Company Vendor'     
select @status = LookUpKey from LookUp where Title = 'Pending'    

    -- If else change on 15/05/2021
    if not exists (Select CompanyKey from Company where Name = @CompanyName)
    Begin
		insert into Company(Name,[State],[Address],Address2,City,Zip,Fax,Website,[Status],[Description],CompanyTypeKey,CompanyID,PortalKey)    
		values(@CompanyName,@StateKey,@Address,@Address2,@City,@zip,@Fax,@Website,100,@Description,@companytype,@CompanyName,3)    
    
           set @vendorkey = @@identity    
			select  @companyvalue = @@IDENTITY    
	 end
	 else
	 begin
			set @vendorkey = (Select CompanyKey from Company where Name = @CompanyName)
			select  @companyvalue = (Select CompanyKey from Company where Name = @CompanyName) 
	 end
     declare @ResourceTypeKey int    
     select  @ResourceTypeKey = LookUpKey  from LookUp where Title = 'Vendor'    
         
    Insert into Resource (CompanyKey,FirstName, LastName,ResourceTypeKey,CellPhone,HomePhone2, Email,Fax, Address, Address2, City, State, Zip,DateAdded,LastModificationTime, PrimaryContact,Status)     
       values     
       (@vendorkey,@FirstName,@LastName,@ResourceTypeKey, @Work2,@Work, @Email, @Fax, @Address, @Address2, @City, @StateKey, @Zip, getdate(), GETDATE(),1,101)    
    select @ResValue  = @@IDENTITY
    
    insert into CompanyVendor(CompanyKey,VendorKey,LastModificationTime,Status)values((select r.companykey from Resource r where r.ResourceKey = @Resourcekey),@vendorkey,getdate(),101)    
    
    
    insert into ServiceArea (VendorKey,Address,Address2,City,State,Zip,Radius) values(@vendorkey,@Address,@Address2,@City,@StateKey,@zip,1)
GO











--------------------------------------------------------------------------------------------
DROP TABLE [dbo].[ReportEmail]
GO

CREATE TABLE [dbo].[ReportEmail] (
    [ReportEmailKey]   INT           IDENTITY (1, 1) NOT NULL,
    [ResourceKey]      INT           NULL,
    [RequestedOn]      DATETIME      NULL,
    [IsDetailedReport] BIT           NULL,
    [DocumentName]     VARCHAR (255) NULL,
    [IncludeCOI]       BIT           NULL,
    [IsSent]           BIT           NULL,
    [SentOn]           DATETIME      NULL,
    [VendorList]       VARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([ReportEmailKey] ASC),
    FOREIGN KEY ([ResourceKey]) REFERENCES [dbo].[Resource] ([ResourceKey])
);
GO

---------------------------------------------------------------------------------------------------------------

DROP VIEW [dbo].[Vsite_User_Getinformation]
GO

CREATE view [dbo].[Vsite_User_Getinformation]
as 
     SELECT     u.UserKey, u.Password, u.Username, lu.Title as Usertype
     FROM        [User] u INNER JOIN
                  Resource r ON r.ResourceKey = u.ResourceKey INNER JOIN
                  LookUp lu ON lu.LookUpKey = r.ResourceTypeKey
GO

--------------------------------------------------------------------------------------------------------------

DROP VIEW [dbo].[Vsite_User_Getinformation_New]
GO

CREATE view [dbo].[Vsite_User_Getinformation_New]
as 
     SELECT     u.UserKey, u.Password, u.Username, lu.Title,c.PortalKey,p.Title as Usertype
     FROM        [User] u INNER JOIN
                  Resource r ON r.ResourceKey = u.ResourceKey 
				  Join Company c on c.CompanyKey = r.CompanyKey
                  INNER JOIN LookUp lu ON lu.LookUpKey = r.ResourceTypeKey
				  join Portal p on p.PortalKey = c.PortalKey
GO



-------------------------------------------------------------------------------------------------------------------------

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

-- [site_BidRequest_GetVendorsForBidReportByAlgorithm] 0,100                 
alter procedure [dbo].[site_BidRequest_GetVendorsForBidReport]                  
@BidRequestkey int,                  
@Modulekey int = 100     
As                  
begin                  
                  
declare @count int                  
                  
                  
DECLARE @GEO1 GEOGRAPHY, @LAT VARCHAR(10), @LONG VARCHAR(10)                
                
select @LAT =isnull(Latitude,''), @LONG =isnull(Longitude,'') from Property where PropertyKey = (select br.propertykey from bidrequest br where br.BidRequestKey = @BidRequestkey)                
                
SET @geo1= geography::Point(@LAT, @LONG, 4326)                
                
select @count = count(1) from BidVendor where BidVendor.BidRequestKey = @BidRequestkey                  
                  
if(@count = 0 and @Modulekey =100)                  
begin                  
 insert into BidVendor                  
 select top 5 @BidRequestkey, cp.CompanyKey, null, '', 0, DATEADD(Day,2,getdate()), getdate(), getdate(),                   
 (select LookUpKey from LookUp where lookup.LookUpTypeKey = (select LookUpType.lookuptypekey from LookUpType where LookUpType.Title = 'Bid Vendor Status') and LookUp.Title in ('In Progress'))                  
  from Company as cp                     
 inner join VendorService as vs on cp.CompanyKey = vs.VendorKey and  vs.ServiceKey = (select br.ServiceKey from BidRequest as br where br.BidRequestKey = @BidRequestkey)                  
 inner join ServiceArea as sa on sa.vendorkey = vs.vendorkey                
 where cp.CompanyTypeKey in (select LookUpKey from LookUp where lookup.LookUpTypeKey in (select LookUpType.lookuptypekey from LookUpType where LookUpType.Title = 'Company Type') and                   
 LookUp.Title in ('Company Vendor','Vendor')) and cp.Status = 101                 
        
 and (Convert(float, LEFT(CONVERT(VARCHAR,(@geo1.STDistance(geography::Point(ISNULL(sa.Latitude,0), ISNULL(sa.Longitude,0), 4326)))/1000),5))/1.609344) < (sa.radius * 5) + 5                
        order by NEWID()           
                
end                  
                
Declare @TotalApceptRecord int                  
select  @TotalApceptRecord=count(1)                  
from BidVendor bv                  
inner join Company c on c.companyKey = bv.VendorKey                    
where bv.BidRequestKey = @BidRequestkey              
                  
                  
                  
select @TotalApceptRecord as TotalApceptRecord, Name,bv.BidVendorKey,bv.VendorKey,               
(select top 1 isnull(Resource.FirstName, '') + ' ' + isnull(Resource.LastName,'') from Resource where Resource.CompanyKey = c.CompanyKey and Resource.PrimaryContact = 1)  as 'ContactPerson',               
convert(varchar(10),bb.LastModificationTime,101) as RespondByDate,convert(varchar(10),bv.RespondByDate,101) as DefaultRespondByDates,convert(varchar(10),bv.DateAdded,101) as VendorStartdate,
convert(varchar(12),br.DefaultRespondByDate,107) as DefaultRespondByDate,convert(varchar(12),br.DefaultRespondByDate,101) as DefaultRespondByDatess,  bb.Description as Descrip,                
isnull((select top(1) Total from bid where bid.BidVendorKey = bv.bidvendorkey and bid.BidStatus in (select LookUpKey from LookUp where lookup.LookUpTypeKey = (select LookUpType.lookuptypekey from LookUpType where LookUpType.Title = 'Bid Status') and LookUp.Title in ('Submitted','Accepted'))),0) as BidAmount,                  
(select top 1 service.Title from Service where Service.ServiceKey = (select BidRequest.ServiceKey from BidRequest where BidRequestKey = @BidRequestkey)) as Service, bv.VendorKey as CompanyKey,                  
(select top 1 l.Title from [LookUp] l where l.LookUpKey = bv.BidVendorStatus) as BidVendorStatus, (select top 1 l.Title from [LookUp] l where l.LookUpKey = br.BidRequestStatus) as BidRequeststatus,            
(select top 1 isnull(Resource.Email,'') from Resource where Resource.CompanyKey = c.CompanyKey and Resource.PrimaryContact = 1)  as 'Email',               
bv.IsAssigned ,br.Title as BidName,br.BidDueDate as BidDueDate,pr.Title as PropertyName
,isnull((select top(1) InsuranceAmount from Insurance where Insurance.VendorKey = bv.VendorKey),0) as Insurance
from BidVendor bv                  
inner join Company c on c.companyKey = bv.VendorKey              
inner join BidRequest br on  br.BidRequestKey = bv.BidRequestKey 
inner join Property pr on pr.PropertyKey = br.PropertyKey   
left  join  bid   bb  on  bb.BidVendorKey = bv.BidVendorKey          
where bv.BidRequestKey = @BidRequestkey           
  --      if(@BidvendorKey != 0)      
  --begin      
  -- declare @Bidvendor int       
  --select  @Bidvendor = (Select  Max(BidVendorKey)+1 from  BidVendor)      
  --update Document       
  --set objectKey = @Bidvendor      
  --where objectKey =  @BidvendorKey      
  --end      
      
           
end   
--select convert(varchar, getdate(), 107)

-- select * from bidvendor
GO

-------------------------------------------------------------------------------------------------------------------------------------------------------



---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  
      
-- [site_BidRequest_GetVendorsForBidRequestByAlgorithm_Copy] 4253,7363            
alter procedure [dbo].[site_BidRequest_GetVendorsForWorkOrderByAlgorithm_Copy]            
--[site_BidRequest_GetVendorsForBidRequestByAlgorithm_Copy] 1106,7361,100    
@BidRequestkey int,          
@ResourceKey int,    
@Modulekey int = 100,
@StartDate nvarchar(50),
@EndDate nvarchar(50),
@PropertyKey int,
@BidRequestStatus int
As            
begin            
            
declare @count int            
            
                  
DECLARE @GEO1 GEOGRAPHY, @LAT VARCHAR(10), @LONG VARCHAR(10)                
                
select @LAT =isnull(Latitude,''), @LONG =isnull(Longitude,'') from Property where PropertyKey = (select br.propertykey from bidrequest br where br.BidRequestKey = @BidRequestkey)                
                
SET @geo1= geography::Point(@LAT, @LONG, 4326)                
                       
          
select @count = count(1) from BidVendor where BidVendor.BidRequestKey = @BidRequestkey            
            
if(@count = 0 and @Modulekey =100)            
begin            
 insert into BidVendor            
 select  top 5 @BidRequestkey, cp.CompanyKey, null, '', 0,(Select top 1 DefaultRespondByDate from BidRequest where BidRequestKey = @BidRequestkey), getdate(), getdate(),           -- DATEADD(Day,2,getdate())  
 (select LookUpKey from LookUp where lookup.LookUpTypeKey = (select LookUpType.lookuptypekey from LookUpType where LookUpType.Title = 'Bid Vendor Status') and LookUp.Title in ('In Progress'))            
  from Company as cp               
 inner join VendorService as vs on cp.CompanyKey = vs.VendorKey and  vs.ServiceKey = (select br.ServiceKey from BidRequest as br where br.BidRequestKey = @BidRequestkey)            
 inner join ServiceArea as sa on sa.vendorkey = vs.vendorkey          
 where cp.CompanyTypeKey in (select LookUpKey from LookUp where lookup.LookUpTypeKey in (select LookUpType.lookuptypekey from LookUpType where LookUpType.Title = 'Company Type') and             
 LookUp.Title in ('Company Vendor','Vendor')) and cp.Status = 101           
  
        
 and (Convert(float, LEFT(CONVERT(VARCHAR,(@geo1.STDistance(geography::Point(ISNULL(sa.Latitude,0), ISNULL(sa.Longitude,0), 4326)))/1000),5))/1.609344) < (sa.radius * 5) + 5         
       order by NEWID()      
          
end            
    
Declare @TotalApceptRecord int            
select  @TotalApceptRecord=count(1)            
from BidVendor bv            
inner join Company c on c.companyKey = bv.VendorKey              
where bv.BidRequestKey = @BidRequestkey        
            
            
            
select   @TotalApceptRecord as TotalApceptRecord, Name,bv.BidVendorKey,bv.VendorKey,         
(select top 1 isnull(Resource.FirstName, '') + ' ' + isnull(Resource.LastName,'') from Resource where Resource.CompanyKey = c.CompanyKey and Resource.PrimaryContact = 1)  as 'ContactPerson',         
--convert(varchar(10),bb.LastModificationTime,101) as RespondByDate,convert(varchar(10),br.DefaultRespondByDate,101) as DefaultRespondByDate,  bb.Description as Descrip,       
convert(varchar(10),bb.LastModificationTime,101) as RespondByDate,convert(varchar(10),bv.RespondByDate,101) as DefaultRespondByDate,  bb.Description as Descrip,         
isnull((select top(1) Total from bid where bid.BidVendorKey = bv.bidvendorkey and bid.BidStatus in (select LookUpKey from LookUp where lookup.LookUpTypeKey = (select LookUpType.lookuptypekey from LookUpType where LookUpType.Title = 'Bid Status') and LookUp.Title in ('Submitted','Accepted'))),0) as BidAmount,            
(select top 1 service.Title from Service where Service.ServiceKey = (select BidRequest.ServiceKey from BidRequest where BidRequestKey = @BidRequestkey)) as Service, bv.VendorKey as CompanyKey,            
(select top 1 l.Title from [LookUp] l where l.LookUpKey = bv.BidVendorStatus) as BidVendorStatus, (select top 1 l.Title from [LookUp] l where l.LookUpKey = br.BidRequestStatus) as BidRequeststatus,      
(select top 1 isnull(Resource.Email,'') from Resource where Resource.CompanyKey = c.CompanyKey and Resource.PrimaryContact = 1)  as 'Email',     bv.IsAssigned ,    
   (select top 1(select (CASE WHEN (ab.ModuleKey = @modulekey) and ab.ForResource = @ResourceKey and ab.Status = 900    
   THEN 1 ELSE 0 END)) AS priority from ABNotification ab where ObjectKey = br.BidRequestKey and ab.ForResource =@ResourceKey    
   and ByResource = (select top 1 ResourceKey from Resource where CompanyKey = bv.VendorKey)    
   and ab.status = 900)AS priority,    
   (select top 1 (CASE WHEN (ab.ModuleKey = @modulekey and ab.ForResource = @ResourceKey) and ab.Status = 900    
   THEN (select STUFF((select ',' + Convert(    
   nvarchar(max), ab1.Id) from ABNotification ab1    
   inner join [ABNotification] as vs on vs.Id = ab1.Id where ab1.status = 900     
   and vs.ForResource  = @ResourceKey and vs.ObjectKey = br.BidRequestKey    
    FOR XML PATH('')    
),1,1,'')) ELSE '0' END) from ABNotification ab where ObjectKey = br.BidRequestKey and ab.ForResource = @ResourceKey    
and ByResource = (select top 1 ResourceKey from Resource where CompanyKey = bv.VendorKey)     
and ab.status = 900)AS NotificationId,    
    
(select top 1 (CASE WHEN (ab.ModuleKey = @modulekey and ab.ForResource = @ResourceKey and ab.Status = 900)    
   THEN (select STUFF((select ',' + Convert(    
   nvarchar(max), ab1.NotificationType) from ABNotification ab1    
   inner join [ABNotification] as vs on vs.Id = ab1.Id where ab1.status = 900    
   and vs.ForResource  = @ResourceKey and vs.ObjectKey = br.BidRequestKey    
    FOR XML PATH('')    
),1,1,'')) ELSE '0' END) from ABNotification ab where ObjectKey = br.BidRequestKey and ab.ForResource = @ResourceKey    
and ByResource = (select top 1 ResourceKey from Resource where CompanyKey = bv.VendorKey)    
and ab.status = 900)AS NotificationType    
from BidVendor bv            
inner join Company c on c.companyKey = bv.VendorKey        
inner join BidRequest br on  br.BidRequestKey = bv.BidRequestKey
inner join Property pr on pr.PropertyKey = br.PropertyKey 
left  join  bid   bb  on  bb.BidVendorKey = bv.BidVendorKey    
where bv.BidRequestKey =case when @BidRequestkey  = 0 then bv.BidRequestKey else @BidRequestkey end
 and (pr.PropertyKey = CONVERT(nvarchar,@PropertyKey) or CONVERT(nvarchar,@PropertyKey) = 0 )
 --and ((br.BidRequestStatus in (600,601) and CONVERT(nvarchar,@BidRequestStatus) = 1) or (br.BidRequestStatus in (602,603,604) and CONVERT(nvarchar,@BidRequestStatus) = 2)  or CONVERT(nvarchar,@BidRequestStatus) = 0)
 and ((br.BidRequestStatus in (601) and CONVERT(nvarchar,@BidRequestStatus) = 1) or (br.BidRequestStatus in (602,603) and CONVERT(nvarchar,@BidRequestStatus) = 2)  or CONVERT(nvarchar,@BidRequestStatus) = 0) 
 and ((br.DateAdded >= @StartDate and br.DateAdded <= @EndDate ) or (br.DateAdded >= @StartDate  and @EndDate = '') or (br.DateAdded <= @EndDate and @StartDate = '') or (@StartDate = '' and @EndDate = '')) 
 and br.ModuleKey = @Modulekey  ORDER BY pr.Title ASC
end
GO

-----------------------------------------------------------------------------------------------------------------------------------------------


---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

-- [site_BidRequest_GetVendorsForWorkReport] 0,106 ,'','',0,0,0,0         
alter procedure [dbo].[site_BidRequest_GetVendorsForWorkReport]              
@BidRequestkey int,            
@Modulekey int = 106,  
@StartDate nvarchar(50),  
@EndDate nvarchar(50),  
@PropertyKey int,  
@BidRequestStatus int,
@ResourceKey int = 0,
@CompanyKey int
As              
begin   
  declare @GroupKey int  
-- select  @GroupKey =    count(GroupKey) from GroupMember Where ResourceKey= @ResourceKey and  GroupKey = 1  
select  @GroupKey =    count(GroupKey) from GroupMember Where ResourceKey=  CASE WHEN @ResourceKey = 0 Then ResourceKey  else @ResourceKey END and  GroupKey = 1  

if @GroupKey  > 0  
begin  
  select DISTINCT name,bv.BidVendorKey,bv.VendorKey, 
  (select top 1 isnull(Resource.FirstName, '') + ' ' + isnull(Resource.LastName,'') from Resource where Resource.CompanyKey = c.CompanyKey and Resource.PrimaryContact = 1)  as 'ContactPerson' ,
convert(varchar(10),bb.LastModificationTime,101) as RespondByDate,convert(varchar(10),bv.RespondByDate,101) as DefaultRespondByDates,convert(varchar(10),bv.DateAdded,101) as VendorStartdate,  
convert(varchar(12),br.DefaultRespondByDate,107) as DefaultRespondByDate,convert(varchar(12),br.DefaultRespondByDate,101) as DefaultRespondByDatess,  bb.Description as Descrip,
isnull((select top(1) Total from bid where bid.BidVendorKey = bv.bidvendorkey and bid.BidStatus in (select LookUpKey from LookUp where lookup.LookUpTypeKey = (select LookUpType.lookuptypekey from LookUpType where LookUpType.Title = 'Bid Status') and LookUp.Title in ('Submitted','Accepted'))),0) as BidAmount,                    
(select top 1 l.Title from [LookUp] l where l.LookUpKey = bv.BidVendorStatus) as BidVendorStatus, (select top 1 l.Title from [LookUp] l where l.LookUpKey = br.BidRequestStatus) as BidRequeststatus,
bv.IsAssigned ,br.Title as BidName,br.BidDueDate as BidDueDate,pr.Title as PropertyName,pr.PropertyKey as PropertyKey,inv.InsuranceAmount as Insurance,br.Description as bidDescription 

from BidRequest as br
  inner join bidvendor as bv on bv.BidRequestKey = br.BidRequestKey
  inner join Company c on c.companyKey = bv.VendorKey
  inner join Property pr on pr.PropertyKey = br.PropertyKey 
  inner join PropertyResource prr on prr.PropertyKey = br.PropertyKey 
  left join bid  as bb on bb.BidVendorKey = bv.BidVendorKey
  inner join Insurance inv on inv.VendorKey = bv.VendorKey
  where bv.BidRequestKey =case when @BidRequestkey  = 0 then bv.BidRequestKey else @BidRequestkey end  
-- and (pr.PropertyKey = CONVERT(nvarchar,@PropertyKey) or (CONVERT(nvarchar,@PropertyKey) = 0 and @UserKey = 0) or 1=1)
 and pr.companykey = CASE WHEN @CompanyKey = 0 Then pr.companykey  else @CompanyKey END
-- and ((br.BidRequestStatus in (600,601) and CONVERT(nvarchar,@BidRequestStatus) = 1) or (br.BidRequestStatus in (602,603,604) and CONVERT(nvarchar,@BidRequestStatus) = 2)  or CONVERT(nvarchar,@BidRequestStatus) = 0)  
 and ((br.BidRequestStatus in (601) and CONVERT(nvarchar,@BidRequestStatus) = 1) or (br.BidRequestStatus in (602,603) and CONVERT(nvarchar,@BidRequestStatus) = 2)  or CONVERT(nvarchar,@BidRequestStatus) = 0)   
 and ((br.DateAdded >= @StartDate and br.DateAdded <= @EndDate ) or (br.DateAdded >= @StartDate  and @EndDate = '') or (br.DateAdded <= @EndDate and @StartDate = '') or (@StartDate = '' and @EndDate = ''))   
 and br.ModuleKey = @Modulekey  ORDER BY pr.Title ASC 
  
end  

else
begin
select DISTINCT name,bv.BidVendorKey,bv.VendorKey, 
(select top 1 isnull(Resource.FirstName, '') + ' ' + isnull(Resource.LastName,'') from Resource where Resource.CompanyKey = c.CompanyKey and Resource.PrimaryContact = 1)  as 'ContactPerson' ,
convert(varchar(10),bb.LastModificationTime,101) as RespondByDate,convert(varchar(10),bv.RespondByDate,101) as DefaultRespondByDates,convert(varchar(10),bv.DateAdded,101) as VendorStartdate,  
convert(varchar(12),br.DefaultRespondByDate,107) as DefaultRespondByDate,convert(varchar(12),br.DefaultRespondByDate,101) as DefaultRespondByDatess,  bb.Description as Descrip,
isnull((select top(1) Total from bid where bid.BidVendorKey = bv.bidvendorkey and bid.BidStatus in (select LookUpKey from LookUp where lookup.LookUpTypeKey = (select LookUpType.lookuptypekey from LookUpType where LookUpType.Title = 'Bid Status') and LookUp.Title in ('Submitted','Accepted'))),0) as BidAmount,                    
(select top 1 l.Title from [LookUp] l where l.LookUpKey = bv.BidVendorStatus) as BidVendorStatus, (select top 1 l.Title from [LookUp] l where l.LookUpKey = br.BidRequestStatus) as BidRequeststatus,
bv.IsAssigned ,br.Title as BidName,br.BidDueDate as BidDueDate,pr.Title as PropertyName,pr.PropertyKey as PropertyKey,inv.InsuranceAmount as Insurance,br.Description as bidDescription 

from BidRequest as br
  inner join bidvendor as bv on bv.BidRequestKey = br.BidRequestKey
  inner join Company c on c.companyKey = bv.VendorKey
  inner join Property pr on pr.PropertyKey = br.PropertyKey 
  inner join PropertyResource prr on prr.PropertyKey = br.PropertyKey 
  left join bid  as bb on bb.BidVendorKey = bv.BidVendorKey
  inner join Insurance inv on inv.VendorKey = bv.VendorKey
  where bv.BidRequestKey =case when @BidRequestkey  = 0 then bv.BidRequestKey else @BidRequestkey end  
-- and (pr.PropertyKey = CONVERT(nvarchar,@PropertyKey) or (CONVERT(nvarchar,@PropertyKey) = 0 and @UserKey = 0) or 1=1)
and prr.resourceKey = case when @ResourceKey  = 0 then prr.ResourceKey else @ResourceKey end  
-- and ((br.BidRequestStatus in (600,601) and CONVERT(nvarchar,@BidRequestStatus) = 1) or (br.BidRequestStatus in (602,603,604) and CONVERT(nvarchar,@BidRequestStatus) = 2)  or CONVERT(nvarchar,@BidRequestStatus) = 0)  
 and ((br.BidRequestStatus in (601) and CONVERT(nvarchar,@BidRequestStatus) = 1) or (br.BidRequestStatus in (602,603) and CONVERT(nvarchar,@BidRequestStatus) = 2)  or CONVERT(nvarchar,@BidRequestStatus) = 0)   
 and ((br.DateAdded >= @StartDate and br.DateAdded <= @EndDate ) or (br.DateAdded >= @StartDate  and @EndDate = '') or (br.DateAdded <= @EndDate and @StartDate = '') or (@StartDate = '' and @EndDate = ''))   
 and br.ModuleKey = @Modulekey  ORDER BY pr.Title ASC 
 end  
end
  --select * from BidRequest as br
  --inner join BidVendor as bv on bv.BidRequestKey = br.bidrequestkey
  --where ModuleKey = 106
  -- select * from property
GO

---------------------------------------------------------------------------------------------------------------------------------

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


  
-- site_BidRequest_SelectIndexPagingBidReport_New_Copy 1500,1,'','order by Title asc',0,0,1,100,'01/25/2021','01/25/2021'
alter procedure [dbo].[site_BidRequest_SelectIndexPagingBidReport]  
@PageSize int,                             
@PageIndex int,                             
@Search nvarchar(max),                                
@Sort nvarchar(max),  
@ResourceKey int,  
@PropertyKey int,  
@BidRequestStatus int,  
@modulekey int  = 100 ,
@StartDate nvarchar(50),
@EndDate nvarchar(50)
AS  
BEGIN  
  declare @qrywhere varchar(max)                      
  declare @qrytotal varchar(max)                       
  declare @qry varchar(max)      
    --For Cout total unread message  
declare @MessageStatusNew nvarchar(100)  
(select top(1) @MessageStatusNew=LookUpKey from lookup where LookUpTypeKey in (Select lt.LookUpTypeKey From lookuptype lt where lt.Title='Message Status') and Title='New')  
  --@BidRequestStatus 
  --open 1
  --close 2 
  --all 0
declare @ModuleKeyForMessage nvarchar(100)  
if(@modulekey=100)  
begin  
Select @ModuleKeyForMessage=ModuleKey From Module where Controller='BidRequest'  
end  
else  
begin  
Select @ModuleKeyForMessage=ModuleKey From Module where Controller='WorkOrder'  
  end  
  set @qrytotal =   'declare @total int    
  select  @total  = count(*) from [BidRequest] as br  
  
   inner join Property as ps on ps.PropertyKey = br.PropertyKey   
where br.modulekey = ' + CONVERT(nvarchar,@modulekey) + ' and  ((br.Title like ''%'+ @Search +'%'') or (ps.Title like ''%'+ @Search +'%'') or ps.Title + '' '' + br.Title like ''%'+@Search+'%''   or ( '''+@Search+''' = '''' ))   
and (ps.PropertyKey = '+CONVERT(nvarchar,@PropertyKey)+' or '+CONVERT(nvarchar,@PropertyKey)+' = 0 )  
and ((br.BidRequestStatus in (600,601) and ' + CONVERT(nvarchar,@BidRequestStatus)+' = 1) or (br.BidRequestStatus in (602,603,604) and ' + CONVERT(nvarchar,@BidRequestStatus)+' = 2)  or '+ CONVERT(nvarchar,@BidRequestStatus)+' = 0)
and ((br.DateAdded >= '''+@StartDate+''' and br.DateAdded <= '''+@EndDate+''' ) or (br.DateAdded >= '''+@StartDate+'''  and '''+@EndDate+''' ='''') or (br.DateAdded <= '''+@EndDate+''' and '''+@StartDate +'''='''') or ('''+@StartDate +'''='''' and '''+@EndDate+''' ='''')) 
and ((br.propertykey in (select xp.propertykey from Property xp where xp.CompanyKey = (select Companykey from [Resource] xr where xr.ResourceKey = ' + Convert(varchar,@resourcekey) + ')  
and (xp.PropertyKey in (select xpr.propertykey from PropertyResource xpr where xpr.ResourceKey = ' + Convert(varchar,@resourcekey) + ') or   
 ' + Convert(varchar,@resourcekey) + ' in (select xgp.ResourceKey from GroupMember xgp where xgp.GroupKey in (select xg.groupkey from [Group] xg where xg.title = ''Administrator'')))))  
 or (' + Convert(varchar,@resourcekey) + ' = 0))'   
  
  set @qry = ' select   *,     
  (Select Count(1) From Message Where  MessageStatus='''+@MessageStatusNew+''' and ObjectKey in (Select bv.BidVendorKey From BidVendor bv where  bv.BidRequestKey in  
    (Select br.BidRequestKey From BidRequest br where br.BidRequestKey =  j.BidRequestKey and ModuleKey='''+@ModuleKeyForMessage+''') and bv.VendorKey not in   
 (Select CompanyKey From Resource where '+Convert(nvarchar(50),'ResourceKey')+'='+ Convert(nvarchar(50), @ResourceKey)+'))) as NewMsg  
  
  from ( select * , @total  as TotalRecord, row_number() over('+@Sort+') as  rownum from (   
  select br.BidRequestKey,br.Title,ps.Title as PropertyName  
  ,(case when convert(varchar(10),br.DateAdded,101)=''01/01/1900'' then '''' else convert(varchar(10),br.DateAdded,101) end) as StartDate,convert(varchar(10),BidDueDate,101) as BidDueDate  
 ,(select count(1) from Bid b where b.BidVendorKey in (Select BidVendorKey from BidVendor bv where bv.BidRequestKey = br.BidRequestKey) and b.BidStatus  = 801) as NoofBids  
 , (select top 1 l.Title from LookUpType lt inner join [LookUp] l on lt.LookUpTypeKey = l.LookUpTypeKey and l.LookUpKey = br.BidRequestStatus) as BidRequestStatus  ,
   
   (select top 1(select (CASE WHEN (ab.ModuleKey = '+CONVERT(varchar,@modulekey)+' and ab.ForResource = '+CONVERT(varchar,@ResourceKey)+' and ab.Status = 900) 
	  THEN 1 ELSE 0 END)) AS priority from ABNotification ab where ObjectKey = br.BidRequestKey and ab.ForResource ='+CONVERT(varchar,@ResourceKey)+' and ab.status = 900)AS priority,
	  (select top 1 (CASE WHEN (ab.ModuleKey = '+CONVERT(varchar,@modulekey)+' and ab.ForResource ='+CONVERT(varchar,@ResourceKey)+' and ab.Status = 900)
	  THEN (select STUFF((select '','' + Convert(
	  nvarchar(max), ab1.Id) from ABNotification ab1
		 inner join [ABNotification] as vs on vs.Id = ab1.Id where ab1.status = 900 
		 and vs.ForResource  = '+CONVERT(varchar,@ResourceKey)+' and vs.ObjectKey = br.BidRequestKey
    FOR XML PATH('''')
),1,1,'''')) ELSE ''0'' END) from ABNotification ab where ObjectKey = br.BidRequestKey and ab.ForResource ='+CONVERT(varchar,@ResourceKey)+' and ab.status = 900)AS NotificationId,

(select top 1 (CASE WHEN (ab.ModuleKey = '+CONVERT(varchar,@modulekey)+' and ab.ForResource ='+CONVERT(varchar,@ResourceKey)+' and ab.Status = 900)
	  THEN (select STUFF((select '','' + Convert(
	  nvarchar(max), ab1.NotificationType) from ABNotification ab1
		 inner join [ABNotification] as vs on vs.Id = ab1.Id where ab1.status = 900
		 and vs.ForResource  = '+CONVERT(varchar,@ResourceKey)+' and vs.ObjectKey = br.BidRequestKey
    FOR XML PATH('''')
),1,1,'''')) ELSE ''0'' END) from ABNotification ab where ObjectKey = br.BidRequestKey and ab.ForResource ='+CONVERT(varchar,@ResourceKey)+' and ab.status = 900)AS NotificationType
   from [BidRequest] as br  
  
   inner join Property as ps on ps.PropertyKey = br.PropertyKey   
  
where br.modulekey = ' + CONVERT(nvarchar,@modulekey) + ' and  ((br.Title like ''%'+ @Search +'%'') or (ps.Title like ''%'+ @Search +'%'') or ps.Title + '' '' + br.Title like ''%'+@Search+'%''   or ( '''+@Search+''' = '''' ))  
and (ps.PropertyKey = '+CONVERT(nvarchar,@PropertyKey)+' or '+CONVERT(nvarchar,@PropertyKey)+' = 0 ) 
and ((br.BidRequestStatus in (600,601) and ' + CONVERT(nvarchar,@BidRequestStatus)+' = 1) or (br.BidRequestStatus in (602,603,604) and ' + CONVERT(nvarchar,@BidRequestStatus)+' = 2)  or '+ CONVERT(nvarchar,@BidRequestStatus)+' = 0)
and ((br.DateAdded >= '''+@StartDate+''' and br.DateAdded <= '''+@EndDate+''' ) or (br.DateAdded >= '''+@StartDate+'''  and '''+@EndDate+''' ='''') or (br.DateAdded <= '''+@EndDate+''' and '''+@StartDate +'''='''') or ('''+@StartDate +'''='''' and '''+@EndDate+''' ='''')) 
and ((br.propertykey in (select xp.propertykey from Property xp where xp.CompanyKey = (select Companykey from [Resource] xr where xr.ResourceKey = ' + Convert(varchar,@resourcekey) + ')  
and (xp.PropertyKey in (select xpr.propertykey from PropertyResource xpr where xpr.ResourceKey = ' + Convert(varchar,@resourcekey) + ') or   
 ' + Convert(varchar,@resourcekey) + ' in (select xgp.ResourceKey from GroupMember xgp where xgp.GroupKey in (select xg.groupkey from [Group] xg where xg.title = ''Administrator'')))))  
 or (' + Convert(varchar,@resourcekey) + ' = 0))  
  ) i   
  )j  
  where rownum between  '+Convert(nvarchar(10),( (@pageIndex-1) * @pageSize  ) + 1 ) + ' and '  + Convert(varchar(10),( (@pageIndex-1) * @pageSize  ) + @pageSize)   
      print(@qrytotal + @qry)          
      exec( @qrytotal + @qry)            
end  
  --select * from bidrequest where propertykey = 2106 and bidrequeststatus >= 600 and bidrequeststatus <= 601
GO




------------------------------------------------------------------------------------------------------------------------------------------------


---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


  
  
-- site_BidRequest_SelectIndexPagingBidRequestPriority 500,1,'','order by Title asc',1063,2106,0,0
alter procedure [dbo].[site_BidRequest_SelectIndexPagingBidReportPriority]  
@PageSize int,                             
@PageIndex int,                             
@Search nvarchar(max),                                
@Sort nvarchar(max),  
@ResourceKey int,  
@PropertyKey int,  
@BidRequestStatus int,  
@modulekey int  = 100  
AS  
BEGIN  
  declare @qrywhere varchar(max)                      
  declare @qrytotal varchar(max)                       
  declare @qry varchar(max)      
    --For Cout total unread message  
declare @MessageStatusNew nvarchar(100)  
(select top(1) @MessageStatusNew=LookUpKey from lookup where LookUpTypeKey in (Select lt.LookUpTypeKey From lookuptype lt where lt.Title='Message Status') and Title='New')  
  --@BidRequestStatus 
  --open 1
  --close 2 
  --all 0
declare @ModuleKeyForMessage nvarchar(100)  
if(@modulekey=100)  
begin  
Select @ModuleKeyForMessage=ModuleKey From Module where Controller='BidRequest'  
end  
else  
begin  
Select @ModuleKeyForMessage=ModuleKey From Module where Controller='WorkOrder'  
  end  
  set @qrytotal =   'declare @total int    
  select  @total  = count(*) from [BidRequest] as br  
  
   inner join Property as ps on ps.PropertyKey = br.PropertyKey   
where br.modulekey = ' + CONVERT(nvarchar,@modulekey) + ' and  ((br.Title like ''%'+ @Search +'%'') or (ps.Title like ''%'+ @Search +'%'') or ps.Title + '' '' + br.Title like ''%'+@Search+'%''   or ( '''+@Search+''' = '''' ))   
and (ps.PropertyKey = '+CONVERT(nvarchar,@PropertyKey)+' or '+CONVERT(nvarchar,@PropertyKey)+' = 0 )  
and ((br.BidRequestStatus in (600,601) and ' + CONVERT(nvarchar,@BidRequestStatus)+' = 1) or (br.BidRequestStatus in (602,603) and ' + CONVERT(nvarchar,@BidRequestStatus)+' = 2)  or '+ CONVERT(nvarchar,@BidRequestStatus)+' = 0)
  
and ((br.propertykey in (select xp.propertykey from Property xp where xp.CompanyKey = (select Companykey from [Resource] xr where xr.ResourceKey = ' + Convert(varchar,@resourcekey) + ')  
and (xp.PropertyKey in (select xpr.propertykey from PropertyResource xpr where xpr.ResourceKey = ' + Convert(varchar,@resourcekey) + ') or   
 ' + Convert(varchar,@resourcekey) + ' in (select xgp.ResourceKey from GroupMember xgp where xgp.GroupKey in (select xg.groupkey from [Group] xg where xg.title = ''Administrator'')))))  
 or (' + Convert(varchar,@resourcekey) + ' = 0))'   
  
  set @qry = ' ;with cusTable as (select   *,     
  (Select Count(1) From Message Where  MessageStatus='''+@MessageStatusNew+''' and ObjectKey in (Select bv.BidVendorKey From BidVendor bv where  bv.BidRequestKey in  
    (Select br.BidRequestKey From BidRequest br where br.BidRequestKey =  j.BidRequestKey and ModuleKey='''+@ModuleKeyForMessage+''') and bv.VendorKey not in   
 (Select CompanyKey From Resource where '+Convert(nvarchar(50),'ResourceKey')+'='+ Convert(nvarchar(50), @ResourceKey)+'))) as NewMsg  
  
  from ( select * , @total  as TotalRecord, row_number() over('+@Sort+') as  rownum2 from (   
  select Distinct(br.BidRequestKey),br.Title,ps.Title as PropertyName  
  ,(case when convert(varchar(10),br.DateAdded,101)=''01/01/1900'' then '''' else convert(varchar(10),br.DateAdded,101) end) as StartDate,convert(varchar(10),BidDueDate,101) as BidDueDate  
 ,(select count(1) from Bid b where b.BidVendorKey in (Select BidVendorKey from BidVendor bv where bv.BidRequestKey = br.BidRequestKey) and b.BidStatus  = 801) as NoofBids  
 , (select top 1 l.Title from LookUpType lt inner join [LookUp] l on lt.LookUpTypeKey = l.LookUpTypeKey and l.LookUpKey = br.BidRequestStatus) as BidRequestStatus,
 (select (CASE WHEN (ab.ModuleKey = '+CONVERT(varchar,@ModuleKey)+' and ab.ForResource = '+CONVERT(varchar,@ResourceKey)+' and ab.Status = 900) THEN 1 ELSE 0 END)) AS priority,
		(CASE WHEN (ab.ModuleKey = '+CONVERT(varchar,@ModuleKey)+' and ab.ForResource = '+CONVERT(varchar,@ResourceKey)+' and ab.Status = 900) THEN 
		(select STUFF((select '','' + Convert(nvarchar(max), ab1.Id) from ABNotification ab1
		 inner join [ABNotification] as vs on vs.Id = ab1.Id where ab1.status = 900 and vs.ForResource  = '+Convert(varchar,@ResourceKey)+' and vs.ObjectKey = br.BidRequestKey
    FOR XML PATH('''')
),1,1,'''')) ELSE ''0'' END)as NotificationId,
		(CASE WHEN (ab.ModuleKey = '+CONVERT(varchar,@ModuleKey)+'  and ab.ForResource = '+CONVERT(varchar,@ResourceKey)+' and ab.Status = 900) THEN 
         (select STUFF((select '','' + Convert(nvarchar(max), ab1.NotificationType) from ABNotification ab1
		 inner join [ABNotification] as vs on vs.Id = ab1.Id where ab1.status = 900 and vs.ForResource  = '+Convert(varchar,@ResourceKey)+' and vs.ObjectKey = br.BidRequestKey
    FOR XML PATH('''')
),1,1,'''')) ELSE null END) as NotificationType
   from [BidRequest] as br  
  
   inner join Property as ps on ps.PropertyKey = br.PropertyKey   
  LEFT Outer Join ABNotification ab on ab.ObjectKey = br.BidRequestKey and ab.status = 900 and ab.ForResource = '+Convert(varchar,@ResourceKey)+' 
where br.modulekey = ' + CONVERT(nvarchar,@modulekey) + ' and  ((br.Title like ''%'+ @Search +'%'') or (ps.Title like ''%'+ @Search +'%'') or ps.Title + '' '' + br.Title like ''%'+@Search+'%''   or ( '''+@Search+''' = '''' ))  
and (ps.PropertyKey = '+CONVERT(nvarchar,@PropertyKey)+' or '+CONVERT(nvarchar,@PropertyKey)+' = 0 ) 
and ((br.BidRequestStatus in (600,601) and ' + CONVERT(nvarchar,@BidRequestStatus)+' = 1) or (br.BidRequestStatus in (602,603) and ' + CONVERT(nvarchar,@BidRequestStatus)+' = 2)  or '+ CONVERT(nvarchar,@BidRequestStatus)+' = 0)
  
and ((br.propertykey in (select xp.propertykey from Property xp where xp.CompanyKey = (select Companykey from [Resource] xr where xr.ResourceKey = ' + Convert(varchar,@resourcekey) + ')  
and (xp.PropertyKey in (select xpr.propertykey from PropertyResource xpr where xpr.ResourceKey = ' + Convert(varchar,@resourcekey) + ') or   
 ' + Convert(varchar,@resourcekey) + ' in (select xgp.ResourceKey from GroupMember xgp where xgp.GroupKey in (select xg.groupkey from [Group] xg where xg.title = ''Administrator'')))))  
 or (' + Convert(varchar,@resourcekey) + ' = 0))  
  ) i   
  )j ), q2 as( select * from(select *,ROW_NUMBER() over (partition by priority '+@Sort+') as rownum1 from cusTable)i)
  select * from (select *,ROW_NUMBER() over (order by priority desc) as rownum from q2)k 
  where rownum between  '+Convert(nvarchar(10),( (@pageIndex-1) * @pageSize  ) + 1 ) + ' and '  + Convert(varchar(10),( (@pageIndex-1) * @pageSize  ) + @pageSize)   
      print(@qrytotal + @qry)          
      exec( @qrytotal + @qry)            
end  
  



  --select * from bidrequest where propertykey = 2106 and bidrequeststatus >= 600 and bidrequeststatus <= 601
GO


------------------------------------------------------------------------------------------------------------------------------

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- site_BidRequest_SelectIndexPagingBidReport_New_Copy 1500,1,'','order by Title asc',0,0,1,100,'01/25/2021','01/25/2021'
alter procedure [dbo].[site_BidRequest_SelectIndexPagingSupportReport]  
@PageSize int,                             
@PageIndex int,                             
@PropertyName nvarchar(max),      
@CompanyName nvarchar(max),  
@VendorName nvarchar(max),  
@Sort nvarchar(max),  
@ResourceKey int,  
  
@BidRequestStatus int,  
@modulekey int  = 0 ,
@StartDate nvarchar(50),
@EndDate nvarchar(50)
AS  
BEGIN  
  declare @qrywhere varchar(max)                      
  declare @qrytotal varchar(max)                       
  declare @qry varchar(max)      
    --For Cout total unread message  
declare @MessageStatusNew nvarchar(100)  
(select top(1) @MessageStatusNew=LookUpKey from lookup where LookUpTypeKey in (Select lt.LookUpTypeKey From lookuptype lt where lt.Title='Message Status') and Title='New')  
  --@BidRequestStatus 
  --open 1
  --close 2 
  --all 0
declare @ModuleKeyForMessage nvarchar(100)  
if(@modulekey=100)  
begin  
Select @ModuleKeyForMessage=ModuleKey From Module where Controller='BidRequest'  
end  
else  
begin  
Select @ModuleKeyForMessage=ModuleKey From Module where Controller='WorkOrder'  
  end  
  set @qrytotal =   'declare @total int    
  select  @total  = count(*) from [BidRequest] as br  
  
   inner join Property as ps on ps.PropertyKey = br.PropertyKey   
where 
((br.modulekey in (100) and ' + CONVERT(nvarchar,@modulekey)+' = 1) or (br.modulekey in (106) and ' + CONVERT(nvarchar,@modulekey)+' = 2)  or '+ CONVERT(nvarchar,@modulekey)+' = 0)
and  ((br.Title like ''%'+ @PropertyName +'%'') or (ps.Title like ''%'+ @PropertyName +'%'') or ps.Title + '' '' + br.Title like ''%'+@PropertyName+'%''   or ( '''+@PropertyName+''' = '''' ))   
and  ((br.Title like ''%'+ @CompanyName +'%'') or (ps.Title like ''%'+ @CompanyName +'%'') or ps.Title + '' '' + br.Title like ''%'+@CompanyName+'%''   or ( '''+@CompanyName+''' = '''' ))   
and  ((br.Title like ''%'+ @VendorName +'%'') or (ps.Title like ''%'+ @VendorName +'%'') or ps.Title + '' '' + br.Title like ''%'+@VendorName+'%''   or ( '''+@VendorName+''' = '''' ))   

and ((br.BidRequestStatus in (600,601) and ' + CONVERT(nvarchar,@BidRequestStatus)+' = 1) or (br.BidRequestStatus in (602,603,604) and ' + CONVERT(nvarchar,@BidRequestStatus)+' = 2)  or '+ CONVERT(nvarchar,@BidRequestStatus)+' = 0)
and ((br.DateAdded >= '''+@StartDate+''' and br.DateAdded <= '''+@EndDate+''' ) or (br.DateAdded >= '''+@StartDate+'''  and '''+@EndDate+''' ='''') or (br.DateAdded <= '''+@EndDate+''' and '''+@StartDate +'''='''') or ('''+@StartDate +'''='''' and '''+@EndDate+''' ='''')) 
and ((br.propertykey in (select xp.propertykey from Property xp where xp.CompanyKey = (select Companykey from [Resource] xr where xr.ResourceKey = ' + Convert(varchar,@resourcekey) + ')  
and (xp.PropertyKey in (select xpr.propertykey from PropertyResource xpr where xpr.ResourceKey = ' + Convert(varchar,@resourcekey) + ') or   
 ' + Convert(varchar,@resourcekey) + ' in (select xgp.ResourceKey from GroupMember xgp where xgp.GroupKey in (select xg.groupkey from [Group] xg where xg.title = ''Administrator'')))))  
 or (' + Convert(varchar,@resourcekey) + ' = 0))'   
  
  set @qry = ' select   *,     
  (Select Count(1) From Message Where  MessageStatus='''+@MessageStatusNew+''' and ObjectKey in (Select bv.BidVendorKey From BidVendor bv where  bv.BidRequestKey in  
    (Select br.BidRequestKey From BidRequest br where br.BidRequestKey =  j.BidRequestKey and ModuleKey='''+@ModuleKeyForMessage+''') and bv.VendorKey not in   
 (Select CompanyKey From Resource where '+Convert(nvarchar(50),'ResourceKey')+'='+ Convert(nvarchar(50), @ResourceKey)+'))) as NewMsg  
  
  from ( select * , @total  as TotalRecord, row_number() over('+@Sort+') as  rownum from (   
  select br.BidRequestKey,br.Title,ps.Title as PropertyName  
  ,(case when convert(varchar(10),br.DateAdded,101)=''01/01/1900'' then '''' else convert(varchar(10),br.DateAdded,101) end) as StartDate
  
 , (select top 1 l.Title from LookUpType lt inner join [LookUp] l on lt.LookUpTypeKey = l.LookUpTypeKey and l.LookUpKey = br.BidRequestStatus) as BidRequestStatus  ,
   
   (select top 1(select (CASE WHEN (ab.ModuleKey = '+CONVERT(varchar,@modulekey)+' and ab.ForResource = '+CONVERT(varchar,@ResourceKey)+' and ab.Status = 900) 
	  THEN 1 ELSE 0 END)) AS priority from ABNotification ab where ObjectKey = br.BidRequestKey and ab.ForResource ='+CONVERT(varchar,@ResourceKey)+' and ab.status = 900)AS priority,
	  (select top 1 (CASE WHEN (ab.ModuleKey = '+CONVERT(varchar,@modulekey)+' and ab.ForResource ='+CONVERT(varchar,@ResourceKey)+' and ab.Status = 900)
	  THEN (select STUFF((select '','' + Convert(
	  nvarchar(max), ab1.Id) from ABNotification ab1
		 inner join [ABNotification] as vs on vs.Id = ab1.Id where ab1.status = 900 
		 and vs.ForResource  = '+CONVERT(varchar,@ResourceKey)+' and vs.ObjectKey = br.BidRequestKey
    FOR XML PATH('''')
),1,1,'''')) ELSE ''0'' END) from ABNotification ab where ObjectKey = br.BidRequestKey and ab.ForResource ='+CONVERT(varchar,@ResourceKey)+' and ab.status = 900)AS NotificationId,

(select top 1 (CASE WHEN (ab.ModuleKey = '+CONVERT(varchar,@modulekey)+' and ab.ForResource ='+CONVERT(varchar,@ResourceKey)+' and ab.Status = 900)
	  THEN (select STUFF((select '','' + Convert(
	  nvarchar(max), ab1.NotificationType) from ABNotification ab1
		 inner join [ABNotification] as vs on vs.Id = ab1.Id where ab1.status = 900
		 and vs.ForResource  = '+CONVERT(varchar,@ResourceKey)+' and vs.ObjectKey = br.BidRequestKey
    FOR XML PATH('''')
),1,1,'''')) ELSE ''0'' END) from ABNotification ab where ObjectKey = br.BidRequestKey and ab.ForResource ='+CONVERT(varchar,@ResourceKey)+' and ab.status = 900)AS NotificationType
   from [BidRequest] as br  
  
   inner join Property as ps on ps.PropertyKey = br.PropertyKey   
  
where ((br.modulekey in (100) and ' + CONVERT(nvarchar,@modulekey)+' = 1) or (br.modulekey in (106) and ' + CONVERT(nvarchar,@modulekey)+' = 2)  or '+ CONVERT(nvarchar,@modulekey)+' = 0)
and  ((br.Title like ''%'+ @PropertyName +'%'') or (ps.Title like ''%'+ @PropertyName +'%'') or ps.Title + '' '' + br.Title like ''%'+@PropertyName+'%''   or ( '''+@PropertyName+''' = '''' ))   
and  ((br.Title like ''%'+ @CompanyName +'%'') or (ps.Title like ''%'+ @CompanyName +'%'') or ps.Title + '' '' + br.Title like ''%'+@CompanyName+'%''   or ( '''+@CompanyName+''' = '''' ))   
and  ((br.Title like ''%'+ @VendorName +'%'') or (ps.Title like ''%'+ @VendorName +'%'') or ps.Title + '' '' + br.Title like ''%'+@VendorName+'%''   or ( '''+@VendorName+''' = '''' ))   
and ((br.BidRequestStatus in (600,601) and ' + CONVERT(nvarchar,@BidRequestStatus)+' = 1) or (br.BidRequestStatus in (602,603,604) and ' + CONVERT(nvarchar,@BidRequestStatus)+' = 2)  or '+ CONVERT(nvarchar,@BidRequestStatus)+' = 0)
and ((br.DateAdded >= '''+@StartDate+''' and br.DateAdded <= '''+@EndDate+''' ) or (br.DateAdded >= '''+@StartDate+'''  and '''+@EndDate+''' ='''') or (br.DateAdded <= '''+@EndDate+''' and '''+@StartDate +'''='''') or ('''+@StartDate +'''='''' and '''+@EndDate+''' ='''')) 
and ((br.propertykey in (select xp.propertykey from Property xp where xp.CompanyKey = (select Companykey from [Resource] xr where xr.ResourceKey = ' + Convert(varchar,@resourcekey) + ')  
and (xp.PropertyKey in (select xpr.propertykey from PropertyResource xpr where xpr.ResourceKey = ' + Convert(varchar,@resourcekey) + ') or   
 ' + Convert(varchar,@resourcekey) + ' in (select xgp.ResourceKey from GroupMember xgp where xgp.GroupKey in (select xg.groupkey from [Group] xg where xg.title = ''Administrator'')))))  
 or (' + Convert(varchar,@resourcekey) + ' = 0))  
  ) i   
  )j  
  where rownum between  '+Convert(nvarchar(10),( (@pageIndex-1) * @pageSize  ) + 1 ) + ' and '  + Convert(varchar(10),( (@pageIndex-1) * @pageSize  ) + @pageSize)   
      print(@qrytotal + @qry)          
      exec( @qrytotal + @qry)            
end  
  --select * from bidrequest where propertykey = 2106 and bidrequeststatus >= 600 and bidrequeststatus <= 601
GO

------------------------------------------------------------------------------------------------------------------------------




---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- site_Property_SelectIndexPaging 50,1,'','order by u.Title desc',0
alter procedure [dbo].[site_Property_AdminReportSelectIndexPaging]
@PageSize int,                           
@PageIndex int,                           
@Search nvarchar(max),
@Sort nvarchar(max),
@resourcekey int
AS
BEGIN
  declare @qrywhere varchar(max)                    
  declare @qrytotal varchar(max)                     
  declare @qry varchar(max)    
    
--  set @qrywhere = 'where ( (u.Title like  ''%'+ @Search +'%'') or (u.Address like ''%'+ @Search +'%'') 
--  or (u.NumberOfUnits like ''%'+ @Search +'%'') 
--  or ( '''+@Search+''' = '''' ) ) and 
--  ((u.propertykey in (select xp.propertykey from Property xp where xp.CompanyKey = (select Companykey from [Resource] xr where xr.ResourceKey = ' + Convert(varchar,@resourcekey) + ')
--and (xp.PropertyKey in (select xpr.propertykey from PropertyResource xpr where xpr.ResourceKey = ' + Convert(varchar,@resourcekey) + ') or 
--	' + Convert(varchar,@resourcekey) + ' in (select xgp.ResourceKey from GroupMember xgp where xgp.GroupKey in (select xg.groupkey from [Group] xg where xg.title = ''Administrator'')))))
--	or (' + Convert(varchar,@resourcekey) + ' = 0))' 
  
  set @qrytotal =   'declare @total int  
  select  @total  = count(*) 
  from  [Property] as p 
inner join company as c on c.CompanyKey = p.CompanyKey
   '

  set @qry = 'select * from (  
 select c.Name, p.title as PropertyName,(0) as Counts from [Property] as p 
inner join company as c on c.CompanyKey = p.CompanyKey
   
  ) i where rownum between  '+Convert(nvarchar(10),( (@pageIndex-1) * @pageSize  ) + 1 )+ ' and '  + Convert(varchar(10),( (@pageIndex-1) * @pageSize  ) + @pageSize) 
      print(@qrytotal + @qry)             
      exec( @qrytotal + @qry)          
END
GO

----------------------------------------------------------------------------------------------------------------------------------



---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


-- [site_ReportEmail_GetForMail] 1062,1062,0,'Abc',0,0           
alter procedure [dbo].[site_ReportEmail_GetForMail]              


As              
begin   
	Select * from ReportEmail where IsSent = 0 
end
-- select * from ReportEmail
-- select * from Resource
GO





-------------------------------------------------------------------------------------

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

-- [site_ReportEmail_Insert] 1062,1062,0,'Abc',0,0           
alter procedure [dbo].[site_ReportEmail_Insert]              
@ResourceKey int,
@IsDetailedReport bit,
@DocumentName nvarchar(200),
@IncludeCOI bit,
@IsSent bit,
@VendorList nvarchar(max)

As              
begin   
	insert into ReportEmail (ResourceKey,RequestedOn,IsDetailedReport,DocumentName,IncludeCOI,IsSent,SentOn,VendorList) 
	values (@ResourceKey,getdate(),@IsDetailedReport,@DocumentName,@IncludeCOI,@IsSent,'',@VendorList)
end
-- select * from ReportEmail
-- select * from Resource
GO

------------------------------------------------------------------------------------------------------------

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

-- [site_BidRequest_GetVendorsForBidRequestByAlgorithm] 3123,106  
alter procedure [dbo].[site_ReportEmail_UpdateIsSent]  
@ReportEmailKey int
As  
begin  
Update ReportEmail set IsSent = 1 , SentOn = getdate() where ReportEmailKey = @ReportEmailKey 
-- select * from ReportEmail
end
GO











