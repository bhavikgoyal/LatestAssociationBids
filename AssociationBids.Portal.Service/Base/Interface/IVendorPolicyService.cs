using AssociationBids.Portal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationBids.Portal.Service.Base.Interface
{
    public interface IVendorPolicyService : IBaseService
    {
        List<InsuranceModel> GetInsurancePaging(int CompanyKey, int PageSize, int PageIndex, string Search, string Sort);
        IList<VendorManagerModel> GetbindDocumentByInsuranceKey(int CompanyKey, int InsuranceKey);
        VendorManagerVendorModel GetVendorByCompanyKey(int CompanyKey);
        long VendorManagerAddInsurance(InsuranceModel item);
        long VendorManagerEditInsurance(InsuranceModel item);
        bool DocumentDelete(int BidRequestKey, string Docname);
        bool UpdateDocInsert(int BidRequestKey, string FileName, string FileSize, int ModuleKey);
    }
}
