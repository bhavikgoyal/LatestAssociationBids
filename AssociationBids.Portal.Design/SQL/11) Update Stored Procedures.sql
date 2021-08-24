USE [AssociationBids]
GO
/****** Object:  StoredProcedure [dbo].[api_PM_GetBidRequestList]    Script Date: 1/22/2021 11:34:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
  
  
  
-- api_PM_GetBidRequestList 'workorder',2098,0,'all'  
alter Procedure [dbo].[api_PM_GetBidRequestList]  
@BidType nvarchar(10),  
@UserKey int,  
@PropertyKey int,  
@Status nvarchar(50)  
AS  
Begin  
 declare @ResourceKey int  
 declare @BidRequestKey int  
 declare @ModuleKey int  
 declare @GroupKey int  
 declare @status1 Table (st varchar(20))  
 declare @qry nvarchar(max)  
 select @ResourceKey = ResourceKey from [User] where UserKey = @UserKey  
 if(@BidType = 'Bid')  
 Begin  
  select @ModuleKey = ModuleKey from Module where Title = 'Bid Requests'  
 end  
 else if(@BidType = 'WorkOrder')  
 begin  
  select @ModuleKey = ModuleKey from Module where Title = 'Work Orders'  
 end  
 else  
 begin  
   Set @ResourceKey = 0  
 end  
   
 Select top 1 @GroupKey = GroupKey from GroupMember where ResourceKey = @ResourceKey order by GroupKey asc  
  
 select @qry = case when (@Status = 'open' or @Status = '')  then  
  'Select Title from LookUp where Title in(''In Progress'',''Submitted'')'  
  when @Status = 'close' then   
  'Select Title from LookUp where Title in(''Completed'',''Closed'')'  
  when @Status = 'all' then  
  'Select Title from LookUp where Title in(''In Progress'',''Submitted'',''Completed'',''Closed'')'  
  End  
   
  print(@qry)  
  
 Insert into @status1 Exec(@qry)  
    
 if(@ResourceKey != 0 and @GroupKey = 1)  
 begin  
  Select br.BidRequestKey,br.ResourceKey,br.ModuleKey,br.Title,br.BidDueDate, br.DateAdded,  
  br.StartDate, br.EndDate, br.DefaultRespondByDate,p.PropertyKey,p.Title as PropertyTitle,s.ServiceKey,s.Title as ServiceTitle,  
  l.Title as Status, m.Title as BidType,(select COUNT(1) from BidRequest  
  where BidRequestStatus not in(select LookUpKey from LookUp where Title = 'In Progress') and   
  ResourceKey = @ResourceKey and ModuleKey = @ModuleKey) as BidCount, (select count(1) from Bid b where b.BidVendorKey in (Select BidVendorKey from BidVendor bv where bv.BidRequestKey = br.BidRequestKey) and b.BidStatus  = 801) as NoOfBids  
  ,(select case when br.BidRequestKey = (select top 1 ObjectKey from (select top 1 ObjectKey from ABNotification where ModuleKey = @ModuleKey and Status = 900 --and NotificationType = 'BidVendorStatus'  
  and ForResource = @ResourceKey and ObjectKey = br.BidRequestKey) as ObjectKey)  
  then 1 else 0 end) as hasNotification,  
  (select top 1 Id from ABNotification where ModuleKey = @ModuleKey and Status = 900 and ForResource = @ResourceKey and ObjectKey = br.BidRequestKey and NotificationType = 'BidReqStatus') as NotificationId,  
  (select top 1 NotificationType from ABNotification where ModuleKey = @ModuleKey and Status = 900 and ForResource = @ResourceKey and ObjectKey = br.BidRequestKey and NotificationType = 'BidReqStatus') as NotificationType  
  from BidRequest br  
  join Property p on p.PropertyKey = br.PropertyKey  
  join Service s on s.ServiceKey = br.ServiceKey  
  join LookUp l on l.LookUpKey = br.BidRequestStatus  
  join Module m on m.ModuleKey = br.ModuleKey  
  where br.ModuleKey = @ModuleKey   
  and br.PropertyKey in (select PropertyKey from Property   
   where CompanyKey in(select CompanyKey from Resource where ResourceKey = @ResourceKey)) and  
  br.PropertyKey = Case when @PropertyKey != 0 Then @PropertyKey Else br.PropertyKey END  
  And l.Title in (select st from @status1)  
  order by br.DateAdded desc  
    
    
 end  
 Else if(@ResourceKey != 0)  
 Begin  
  Select br.BidRequestKey,br.ResourceKey,br.ModuleKey,br.Title,br.BidDueDate, br.DateAdded,  
  br.StartDate, br.EndDate, br.DefaultRespondByDate,p.PropertyKey,p.Title as PropertyTitle,s.ServiceKey,s.Title as ServiceTitle,  
  l.Title as Status, m.Title as BidType,(select COUNT(1) from BidRequest  
  where BidRequestStatus not in(select LookUpKey from LookUp where Title = 'In Progress') and   
  ResourceKey = @ResourceKey and ModuleKey = @ModuleKey) as BidCount,(select count(1) from Bid b where b.BidVendorKey in (Select BidVendorKey from BidVendor bv where bv.BidRequestKey = br.BidRequestKey) and b.BidStatus  = 801) as NoOfBids  
  ,(select case when br.BidRequestKey = (select top 1 ObjectKey from (select top 1 ObjectKey from ABNotification where ModuleKey = @ModuleKey and Status = 900 --and NotificationType = 'BidVendorStatus'  
  and ForResource = @ResourceKey and ObjectKey = br.BidRequestKey) as ObjectKey) then 1 else 0 end) as hasNotification,  
  (select top 1 Id from ABNotification where ModuleKey = @ModuleKey and Status = 900 and ForResource = @ResourceKey and ObjectKey = br.BidRequestKey and NotificationType = 'BidReqStatus') as NotificationId,  
  (select top 1 NotificationType from ABNotification where ModuleKey = @ModuleKey and Status = 900 and ForResource = @ResourceKey and ObjectKey = br.BidRequestKey and NotificationType = 'BidReqStatus') as NotificationType  
  from BidRequest br  
  join Property p on p.PropertyKey = br.PropertyKey  
  join Service s on s.ServiceKey = br.ServiceKey  
  join LookUp l on l.LookUpKey = br.BidRequestStatus  
  join Module m on m.ModuleKey = br.ModuleKey  
  where br.ModuleKey = @ModuleKey   
  and br.PropertyKey in (select PropertyKey from PropertyResource where ResourceKey = @ResourceKey) and  
  br.PropertyKey = Case when @PropertyKey != 0 Then @PropertyKey Else br.PropertyKey END  
  And l.Title in (select st from @status1)  
  order by br.DateAdded desc  
 End  
END  
GO
/****** Object:  StoredProcedure [dbo].[Site_GetAllVendorBidNotSubmmited]    Script Date: 1/22/2021 11:34:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
      
alter  procedure [dbo].[Site_GetAllVendorBidNotSubmmited]        
as        
begin        
  

update bidvendor   
set bidvendor.bidvendorstatus = 703  
where bidvendor.bidvendorstatus = 700  
and respondbydate < getdate()  
and (select bidrequeststatus from bidrequest where bidrequest.bidrequestkey = bidvendor.bidrequestkey) = 601  
  
  
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
end  


GO


USE [AssociationBids]
GO
/****** Object:  StoredProcedure [dbo].[Site_GetAllVendorBidNotSubmmited]    Script Date: 1/25/2021 6:30:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
    
        
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
