using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public interface IAgreementRepository : IBaseRepository
    {
        bool Update(AgreementModel item);
        bool Delete(int id);
        AgreementModel Get(int id);
        IList<AgreementModel> GetAll();
        IList<AgreementModel> GetAll(AgreementFilterModel filter);
        IList<AgreementModel> GetAll(AgreementFilterModel filter, PagingModel paging);
        List<AgreementModel> SearchUser(Int64 PageSize, Int64 PageIndex, string Search, String Sort);
        AgreementModel GetDataViewEdit(int EmailTemplateKey );
        int Insert(AgreementModel item);
        Int64 AgreenemtEdit(AgreementModel item);
        Int64 Agreementupdates(AgreementModel item);


        List<AgreementModel> AdvancedSearchAgreement(long PageSize, long PageIndex, string Search, string Status, string Sort);

    }
}
