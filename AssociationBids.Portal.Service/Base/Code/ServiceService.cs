using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base;
using AssociationBids.Portal.Service.Base.Interface;

namespace AssociationBids.Portal.Service.Base
{
    public class ServiceService : BaseService, IServiceService
    {
        protected IServiceRepository __Servicerepository;

        public ServiceService()
            : this(new ServiceRepository()) { }

        public ServiceService(string connectionString)
            : this(new ServiceRepository(connectionString)) { }

        public ServiceService(IServiceRepository repository)
        {
            ConnectionString = repository.ConnectionString;

            __Servicerepository = repository;
        }

        public virtual bool Validate(ServiceModel item)
        {
            if (!Util.IsValidText(item.Title))
            {
                AddError("Title", "Title can not be empty.");
            }

            if (!IsValid)
            {
                AddError("", "Error(s) encountered!");
            }

            return IsValid;
        }

        public virtual bool Delete(int id)
        {
            return __Servicerepository.Remove(id);
        }

        //public virtual ServiceModel Get(int id)
        //{
        //    return __Servicerepository.Get(id);
        //}
       
        public List<ServiceModel> SearchUser(long PageSize, long PageIndex, string Search, string Sort)
        {
            return __Servicerepository.SearchUser(PageSize, PageIndex,Search, Sort);
        }

        public ServiceModel GetDataViewEdit(int ServiceKey)
        {
            return __Servicerepository.GetDataViewEdit(ServiceKey);
        }

        public long Insert(ServiceModel item)
        {
            return __Servicerepository.Insert(item);
        }

        public long ServiceEdit(ServiceModel item)
        {
            return __Servicerepository.ServiceEdit(item);
        }

        public IList<ServiceModel> GetAll()
        {
            return __Servicerepository.GetAll();
        }

        public ServiceModel Get(int id)
        {
            throw new NotImplementedException();
        }

      
    }
}
