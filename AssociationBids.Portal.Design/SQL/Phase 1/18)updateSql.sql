use [AssociationBids]
go

create Procedure site_BidRequest_GetVVDate
@bidVendorKey  int
as
begin
select  convert(varchar(10),LastModificationTime,101) as Lastdate from Bid where BidVendorKey = @bidVendorKey

end