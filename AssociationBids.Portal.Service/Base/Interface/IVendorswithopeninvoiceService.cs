using AssociationBids.Portal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AssociationBids.Portal.Service.Base
{
    public interface IVendorswithopeninvoiceService : IBaseService
    {
        List<VendorswithinvoiceModel> SearchStaff(Int64 ReportPageSize, Int64 PageIndex, string Search, String Sort );
        List<VendorswithinvoiceModel> Top5vendor(Int64 ReportPageSize, Int64 PageIndex, string Search, String Sort,Int64 CompanyKey);
        List<VendorswithinvoiceModel> VendorwithAccept(Int64 ReportPageSize, Int64 PageIndex, string Search, String Sort, Int64 CompanyKey);
        List<VendorswithinvoiceModel> VendorwithSubmit(Int64 ReportPageSize, Int64 PageIndex, string Search, String Sort, Int64 CompanyKey);
        List<VendorswithinvoiceModel> VendorwithNotAccept(Int64 ReportPageSize, Int64 PageIndex, string Search, String Sort, Int64 CompanyKey);
        IList<VendorswithinvoiceModel> GetAllState();
    }
}
