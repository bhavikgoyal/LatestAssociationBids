using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base.Interface
{
    public interface IVendorswithopeninvoiceRepository :IBaseRepository
    {
        List<VendorswithinvoiceModel> SearchStaff(Int64 ReportPageSize, Int64 PageIndex, string Search, String Sort);
        List<VendorswithinvoiceModel> Top5vendor(Int64 ReportPageSize, Int64 PageIndex, string Search, String Sort, Int64 CompanyKey);
        List<VendorswithinvoiceModel> VendorwithAccept(Int64 ReportPageSize, Int64 PageIndex, string Search, String Sort, Int64 CompanyKey);
        List<VendorswithinvoiceModel> VendorwithSubmit(Int64 ReportPageSize, Int64 PageIndex, string Search, String Sort, Int64 CompanyKey);
        List<VendorswithinvoiceModel> VendorwithNotAccept(Int64 ReportPageSize, Int64 PageIndex, string Search, String Sort, Int64 CompanyKey);
        IList<VendorswithinvoiceModel> GetAllState();
    }
}
