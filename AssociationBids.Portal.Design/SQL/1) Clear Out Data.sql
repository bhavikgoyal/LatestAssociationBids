
use [AssociationBids]
go

set nocount on
go

-- clear out all data

delete from [InvoiceLine]
delete from [Invoice]
delete from [Payment]
delete from [Membership]
delete from [PaymentMethod]
delete from [CompanyVendor]

delete from [VendorRating]
delete from [Note]
delete from [StarVendor]
delete from [Insurance]

delete from [Bid]
delete from [BidVendor]
delete from [BidRequest]

delete from [Message]
delete from [ABNotification]
delete from [PushNotification]

delete from [PropertyResource]
delete from [Property]
delete from [Pricing]

delete from [Email]
delete from [User]
delete from [Resource]
delete from [Company]

print 'Complete!'