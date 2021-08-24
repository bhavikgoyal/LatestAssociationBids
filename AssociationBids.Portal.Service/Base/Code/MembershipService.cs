using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base.Code;
using AssociationBids.Portal.Repository.Base.Interface;
using AssociationBids.Portal.Service.Base.Interface;

namespace AssociationBids.Portal.Service.Base
{
    public class MembershipService : BaseService, IMembershipService
    {
        protected IMembershipRepository _MembershipeRepository;
       

        public MembershipService()
         : this(new MembershipRepository()) { }

        public MembershipService(string connectionString)
           : this(new MembershipRepository(connectionString)) { }

        public MembershipService(MembershipRepository mEmbershipRepository)
        {
            ConnectionString = mEmbershipRepository.ConnectionString;

            _MembershipeRepository = mEmbershipRepository;
        }


        public virtual bool Validate(MembershipModel item)
        {
            if (!Util.IsValidInt(item.VendorKey))
            {
                AddError("VendorKey", "Vendor can not be empty.");
            }
            if (!Util.IsValidDateTime(item.StartDate))
            {
                AddError("StartDate", "Start Date can not be empty.");
            }

            if (!IsValid)
            {
                AddError("", "Error(s) encountered!");
            }

            return IsValid;
        }

        public virtual bool IsFilterEnabled(MembershipFilterModel filter)
        {
            MembershipFilterModel defaultFilter = CreateFilter();

            if (defaultFilter.VendorKey != filter.VendorKey)
                return true;

            return false;
        }

        public virtual MembershipFilterModel CreateFilter()
        {
            MembershipFilterModel filter = new MembershipFilterModel();

            return UpdateFilter(filter);
        }

        public virtual MembershipFilterModel CreateFilter(MembershipModel item)
        {
            MembershipFilterModel filter = new MembershipFilterModel();

            filter.VendorKey = item.VendorKey;

            return UpdateFilter(filter);
        }

        public virtual MembershipFilterModel UpdateFilter(MembershipFilterModel filter)
        {
            return filter;
        }

        public virtual MembershipModel Create()
        {
            ResetSiteSettings();

            MembershipModel item = new MembershipModel();

            return item;
        }

        public virtual bool Create(MembershipModel item)
        {
            item.LastModificationTime = DateTime.Now;

            if (Validate(item))
            {
                return _MembershipeRepository.Create(item);
            }
            else
            {
                return false;
            }
        }

        //public virtual bool Update(MembershipModel item)
        //{
        //    item.LastModificationTime = DateTime.Now;

        //    if (Validate(item))
        //    {
        //        return __repository.Update(item);
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        //public virtual bool Delete(int id)
        //{
        //    return __repository.Delete(id);
        //}

        //public virtual MembershipModel Get(int id)
        //{
        //    return __repository.Get(id);
        //}

        //public virtual IList<MembershipModel> GetAll()
        //{
        //    MembershipFilterModel filter = CreateFilter();

        //    return GetAll(filter);
        //}

        //public virtual IList<MembershipModel> GetAll(MembershipFilterModel filter)
        //{
        //    return __repository.GetAll(UpdateFilter(filter));
        //}

        //public virtual IList<MembershipModel> GetAll(MembershipFilterModel filter, PagingModel paging)
        //{
        //    IList<MembershipModel> itemList = __repository.GetAll(UpdateFilter(filter), paging);

        //    // Account for last record update on a page
        //    if (itemList.Count == 0 && paging.TotalRecordCount > 0)
        //    {
        //        paging.PageNumber--;
        //        return GetAll(filter, paging);
        //    }
        //    else
        //    {
        //        return itemList;
        //    }
        //}

        public MembershipModel GetDataViewEdit(int VendorKey)
        {
            return _MembershipeRepository.GetDataViewEdit(VendorKey);
        }
    }
}
