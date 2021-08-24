using AssociationBids.Portal.Repository.Base.Interface;
using AssociationBids.Portal.Repository.Base.Code;
using AssociationBids.Portal.Service.Base.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Service.Base.Code
{
    public class VendorsWithOpeninvoiceServices : BaseService, IVendorswithopeninvoiceService
    {
        protected IVendorswithopeninvoiceRepository __vVendorsWithOpeninvoiceServices;


        public VendorsWithOpeninvoiceServices()
           : this(new VendorwithOpeninvoiceRepository()) { }

        public VendorsWithOpeninvoiceServices(string connectionString)
           : this(new VendorwithOpeninvoiceRepository(connectionString)) { }

        public VendorsWithOpeninvoiceServices(VendorwithOpeninvoiceRepository vVendorsWithOpeninvoiceServices)
        {
            ConnectionString = vVendorsWithOpeninvoiceServices.ConnectionString;

            __vVendorsWithOpeninvoiceServices = vVendorsWithOpeninvoiceServices;
        }
      
        public List<VendorswithinvoiceModel> SearchStaff(long ReportPageSize, long PageIndex, string Search, string Sort)
        {
            return __vVendorsWithOpeninvoiceServices.SearchStaff(ReportPageSize, PageIndex, Search, Sort);
        }
        public List<VendorswithinvoiceModel> Top5vendor(long ReportPageSize, long PageIndex, string Search, string Sort,Int64 CompanyKey)
        {
            return __vVendorsWithOpeninvoiceServices.Top5vendor(ReportPageSize, PageIndex, Search, Sort, CompanyKey);
        }
        public List<VendorswithinvoiceModel> VendorwithAccept(long ReportPageSize, long PageIndex, string Search, string Sort, Int64 CompanyKey)
        {
            return __vVendorsWithOpeninvoiceServices.VendorwithAccept(ReportPageSize, PageIndex, Search, Sort, CompanyKey);
        }
        public List<VendorswithinvoiceModel> VendorwithSubmit(long ReportPageSize, long PageIndex, string Search, string Sort, Int64 CompanyKey)
        {
            return __vVendorsWithOpeninvoiceServices.VendorwithSubmit(ReportPageSize, PageIndex, Search, Sort, CompanyKey);
        }
        public List<VendorswithinvoiceModel> VendorwithNotAccept(long ReportPageSize, long PageIndex, string Search, string Sort, Int64 CompanyKey)
        {
            return __vVendorsWithOpeninvoiceServices.VendorwithNotAccept(ReportPageSize, PageIndex, Search, Sort, CompanyKey);
        }

        public IList<VendorswithinvoiceModel> GetAllState()
        {
            return __vVendorsWithOpeninvoiceServices.GetAllState();
        }
    }
}
