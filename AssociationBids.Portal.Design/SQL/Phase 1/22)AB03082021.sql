---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  
    
      
        
          
          
          
            
            
-- [site_BidRequest_GetVendorsForWorkReport] 0,106 ,'','',0,0,7360,3216                     
Alter procedure [dbo].[site_BidRequest_GetVendorsForWorkReport]                          
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
bv.IsAssigned ,br.Title as BidName,br.BidDueDate as BidDueDate,pr.Title as PropertyName,pr.PropertyKey as PropertyKey          
-- ,inv.InsuranceAmount as Insurance          
,(select Top 1 InsuranceAmount from insurance where VendorKey = bv.VendorKey and EndDate > getdate()) as Insurance          
,'' as Insurance          
,br.Description as bidDescription             
,br.BidRequestKey            
          
from BidRequest as br            
  inner join bidvendor as bv on bv.BidRequestKey = br.BidRequestKey            
  inner join Company c on c.companyKey = bv.VendorKey            
  inner join Property pr on pr.PropertyKey = br.PropertyKey             
  inner join PropertyResource prr on prr.PropertyKey = br.PropertyKey             
  left join bid  as bb on bb.BidVendorKey = bv.BidVendorKey            
  --left join Insurance inv on inv.VendorKey = bv.VendorKey            
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
bv.IsAssigned ,br.Title as BidName,br.BidDueDate as BidDueDate,pr.Title as PropertyName,pr.PropertyKey as PropertyKey          
-- ,inv.InsuranceAmount as Insurance          
,(select Top 1 InsuranceAmount from insurance where VendorKey = bv.VendorKey and EndDate > getdate()) as Insurance          
,br.Description as bidDescription             
 ,br.BidRequestKey             
from BidRequest as br            
  inner join bidvendor as bv on bv.BidRequestKey = br.BidRequestKey            
  inner join Company c on c.companyKey = bv.VendorKey            
  inner join Property pr on pr.PropertyKey = br.PropertyKey             
  inner join PropertyResource prr on prr.PropertyKey = br.PropertyKey             
  left join bid  as bb on bb.BidVendorKey = bv.BidVendorKey            
  --left join Insurance inv on inv.VendorKey = bv.VendorKey            
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

  go
  ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  
    
      
        
          
            
              
                
                  
                    
                    
                    
-- [site_BidRequest_GetVendorsForBidReport] 8217,100                                     
Alter procedure [dbo].[site_BidRequest_GetVendorsForBidReport]                                      
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
,isnull((select top(1) InsuranceAmount from Insurance where Insurance.VendorKey = bv.VendorKey ORDER BY InsuranceKey DESC),0) as Insurance,br.Description as BidDescription          
from BidVendor bv                                      
inner join Company c on c.companyKey = bv.VendorKey                                  
inner join BidRequest br on  br.BidRequestKey = bv.BidRequestKey                     
inner join Property pr on pr.PropertyKey = br.PropertyKey                       
left  join  bid   bb  on  bb.BidVendorKey = bv.BidVendorKey                              
where bv.BidRequestKey = @BidRequestkey and bv.BidVendorStatus != 703  and  bv.BidVendorStatus  != 700            
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
-- select * from insurance 