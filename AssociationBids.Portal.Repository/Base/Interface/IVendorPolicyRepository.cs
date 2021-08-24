using AssociationBids.Portal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationBids.Portal.Repository.Base.Interface
{
    public interface IVendorPolicyRepository : IBaseRepository
    {
        List<InsuranceModel> GetInsurancePaging(int CompanyKey, int PageSize, int PageIndex, string Search, string Sort);
        IList<VendorManagerModel> GetbindDocumentByInsuranceKey(int CompanyKey, int InsuranceKey);
        VendorManagerVendorModel GetVendorByCompanyKey(int CompanyKey);
        long VendorManagerAddInsurance(InsuranceModel item);
        bool DocumentDelete(int BidRequestKey, string Docname);
        bool UpdateDocInsert(int BidRequestKey, string FileName, string FileSize, int ModuleKey);
        long VendorManagerEditInsurance(InsuranceModel item);
    }
}
