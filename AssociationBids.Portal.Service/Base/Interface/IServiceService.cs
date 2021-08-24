using AssociationBids.Portal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationBids.Portal.Service.Base.Interface
{
    
    public interface IServiceService : IBaseService
    {
        List<ServiceModel> SearchUser(long PageSize, long PageIndex, string Search, string Sort);
        bool Validate(ServiceModel item);
        ServiceModel GetDataViewEdit(int ServiceKey);
        Int64 Insert(ServiceModel item);
        //bool Update(ServiceModel item);
        bool Delete(int id);
        Int64 ServiceEdit(ServiceModel item);
        ServiceModel Get(int id);
        IList<ServiceModel> GetAll();
        //IList<ServiceModel> GetAll(ServiceFilterModel filter);
        //IList<ServiceModel> GetAll(ServiceFilterModel filter, PagingModel paging);
    }
}
