using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base;

namespace AssociationBids.Portal.Service.Base
{
    public class InsuranceService : BaseService, IInsuranceService
    {
        protected IInsuranceRepository __repository;

        public InsuranceService()
            : this(new InsuranceRepository()) { }

        public InsuranceService(string connectionString)
            : this(new InsuranceRepository(connectionString)) { }

        public InsuranceService(IInsuranceRepository repository)
        {
            ConnectionString = repository.ConnectionString;

            __repository = repository;
        }

        public virtual bool Validate(InsuranceModel item)
        {
            if (!Util.IsValidInt(item.VendorKey))
            {
                AddError("VendorKey", "Vendor can not be empty.");
            }
            if (!Util.IsValidText(item.CompanyName))
            {
                AddError("CompanyName", "Company Name can not be empty.");
            }
            if (!Util.IsValidText(item.Address))
            {
                AddError("Address", "Address can not be empty.");
            }
            if (!Util.IsValidText(item.City))
            {
                AddError("City", "City can not be empty.");
            }
            if (!Util.IsValidText(item.State))
            {
                AddError("State", "State can not be empty.");
            }
            if (!Util.IsValidText(item.Zip))
            {
                AddError("Zip", "Zip can not be empty.");
            }
            if (!Util.IsValidDateTime(item.StartDate))
            {
                AddError("StartDate", "Start Date can not be empty.");
            }
            if (!Util.IsValidDateTime(item.EndDate))
            {
                AddError("EndDate", "End Date can not be empty.");
            }
            if (!Util.IsValidInt(item.Status))
            {
                AddError("Status", "Status can not be empty.");
            }

            if (!IsValid)
            {
                AddError("", "Error(s) encountered!");
            }

            return IsValid;
        }

        public virtual bool IsFilterEnabled(InsuranceFilterModel filter)
        {
            InsuranceFilterModel defaultFilter = CreateFilter();

            if (defaultFilter.VendorKey != filter.VendorKey)
                return true;

            if (defaultFilter.State != filter.State)
                return true;

            if (defaultFilter.Status != filter.Status)
                return true;

            return false;
        }

        public virtual InsuranceFilterModel CreateFilter()
        {
            InsuranceFilterModel filter = new InsuranceFilterModel();

            if (SiteSettings.AdminPortal)
            {
                filter.Status = (int)LookUpType.RecordStatus.PendingApprovalOrApproved;
            }
            else
            {
                filter.Status = (int)LookUpType.RecordStatus.Approved;
            }

            return UpdateFilter(filter);
        }

        public virtual InsuranceFilterModel CreateFilter(InsuranceModel item)
        {
            InsuranceFilterModel filter = new InsuranceFilterModel();

            filter.VendorKey = item.VendorKey;
            filter.State = item.State;
            filter.Status = item.Status;

            return UpdateFilter(filter);
        }

        public virtual InsuranceFilterModel UpdateFilter(InsuranceFilterModel filter)
        {
            return filter;
        }

        public virtual InsuranceModel Create()
        {
            ResetSiteSettings();

            InsuranceModel item = new InsuranceModel();

            item.Status = (int)LookUpType.RecordStatus.PendingApproval;

            return item;
        }

        public virtual bool Create(InsuranceModel item)
        {
            if (Validate(item))
            {
                return __repository.Create(item);
            }
            else
            {
                return false;
            }
        }

        public virtual bool Update(InsuranceModel item)
        {
            if (Validate(item))
            {
                return __repository.Update(item);
            }
            else
            {
                return false;
            }
        }

        public virtual bool Delete(int id)
        {
            return __repository.Delete(id);
        }

        public virtual InsuranceModel Get(int id)
        {
            return __repository.Get(id);
        }

        public virtual IList<InsuranceModel> GetAll()
        {
            InsuranceFilterModel filter = CreateFilter();

            return GetAll(filter);
        }

        public virtual IList<InsuranceModel> GetAll(InsuranceFilterModel filter)
        {
            return __repository.GetAll(UpdateFilter(filter));
        }

        public virtual IList<InsuranceModel> GetAll(InsuranceFilterModel filter, PagingModel paging)
        {
            IList<InsuranceModel> itemList = __repository.GetAll(UpdateFilter(filter), paging);

            // Account for last record update on a page
            if (itemList.Count == 0 && paging.TotalRecordCount > 0)
            {
                paging.PageNumber--;
                return GetAll(filter, paging);
            }
            else
            {
                return itemList;
            }
        }
    }
}
