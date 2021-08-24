use [AssociationBids]
go
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


  
  
-- [site_BidRequest_GetVendorsForWorkReport] 0,106 ,'','',0,0,7360,3216           
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
