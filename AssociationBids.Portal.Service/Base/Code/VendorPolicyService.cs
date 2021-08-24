using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base.Code;
using AssociationBids.Portal.Repository.Base.Interface;
using AssociationBids.Portal.Service.Base.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationBids.Portal.Service.Base.Code
{
    public class VendorPolicyService : BaseService, IVendorPolicyService
    {
        protected IVendorPolicyRepository _vendorPolicyService;
        public VendorPolicyService()
           : this(new VendorPolicyRepository()) { }

        public VendorPolicyService(string connectionString)
           : this(new VendorPolicyRepository(connectionString)) { }

        public VendorPolicyService(VendorPolicyRepository vendorservice)
        {
            ConnectionString = vendorservice.ConnectionString;

            _vendorPolicyService = vendorservice;
        }

        public List<InsuranceModel> GetInsurancePaging(int CompanyKey, int PageSize, int PageIndex, string Search, string Sort)
        {
            return _vendorPolicyService.GetInsurancePaging(CompanyKey, PageSize, PageIndex, Search, Sort);
        }

        public IList<VendorManagerModel> GetbindDocumentByInsuranceKey(int CompanyKey, int InsuranceKey)
        {
            return _vendorPolicyService.GetbindDocumentByInsuranceKey(CompanyKey, InsuranceKey);
        }

        public VendorManagerVendorModel GetVendorByCompanyKey(int CompanyKey)
        {
            return _vendorPolicyService.GetVendorByCompanyKey(CompanyKey);
        }

        public long VendorManagerAddInsurance(InsuranceModel item)
        {
            return _vendorPolicyService.VendorManagerAddInsurance(item);
        }
        public bool DocumentDelete(int BidRequestKey, string Docname)
        {
            return _vendorPolicyService.DocumentDelete(BidRequestKey, Docname);
        }

        public bool UpdateDocInsert(int BidRequestKey, string FileName, string FileSize, int ModuleKey)
        {
            return _vendorPolicyService.UpdateDocInsert(BidRequestKey, FileName, FileSize, ModuleKey);
        }
        public long VendorManagerEditInsurance(InsuranceModel item)
        {
            return _vendorPolicyService.VendorManagerEditInsurance(item);
        }
    }
}
