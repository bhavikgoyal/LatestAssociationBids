using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public interface IServiceRepository : IBaseRepository
    {
        
        List<ServiceModel> SearchUser(Int64 PageSize, Int64 PageIndex, string Search, String Sort);
        ServiceModel GetDataViewEdit(int ServiceKey);
        //Int64 Insert(EmailTempletModel  item, string strinbuilder, string strinbuilder1);
        Int64 Insert(ServiceModel item);
        IList<ServiceModel> GetAll();
        long ServiceEdit(ServiceModel item);
        Int64 Serviceupdates(ServiceModel item);
        //bool Delete(int id);
        bool Remove(int id);
    }
}
