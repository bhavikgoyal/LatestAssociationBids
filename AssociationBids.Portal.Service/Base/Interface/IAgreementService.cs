using AssociationBids.Portal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationBids.Portal.Service.Base.Interface
{
    public interface IAgreementService : IBaseService 
    {
        List<AgreementModel> SearchUser(long PageSize, long PageIndex, string Search, string Sort);
        bool Validate(AgreementModel item);
        bool IsFilterEnabled(AgreementFilterModel filter);
        AgreementFilterModel CreateFilter();
        AgreementFilterModel CreateFilter(AgreementModel item);
        AgreementFilterModel UpdateFilter(AgreementFilterModel filter);
        AgreementModel Insert();
        int Insert(AgreementModel item);
        bool Update(AgreementModel item);
        bool Delete(int AgreementKey);
        AgreementModel GetDataViewEdit(int AgreementKey);
        Int64 AgreementEdit(AgreementModel item);
        AgreementModel Get(int id);
        IList<AgreementModel> GetAll();
        IList<AgreementModel> GetAll(AgreementFilterModel filter);
        IList<AgreementModel> GetAll(AgreementFilterModel filter, PagingModel paging);

        List<AgreementModel> AdvancedSearchAgreement(long PageSize, long PageIndex, string Search, string Status, string Sort);
    }
}
