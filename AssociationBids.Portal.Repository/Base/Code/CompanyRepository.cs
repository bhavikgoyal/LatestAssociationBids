using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public class CompanyRepository : BaseRepository, ICompanyRepository
    {
        public CompanyRepository() { }

        public CompanyRepository(string connectionString)
            : base(connectionString) { }

        public virtual bool Create(CompanyModel item)
        {
        	bool status = false;

        	try
        	{
        	    string storedProcedure = "gensp_Company_InsertOne";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@parentCompanyKey", SqlDbType.Int, (item.ParentCompanyKey == 0) ? SqlInt32.Null : item.ParentCompanyKey);
                        commandWrapper.AddInputParameter("@relatedCompanyKey", SqlDbType.Int, (item.RelatedCompanyKey == 0) ? SqlInt32.Null : item.RelatedCompanyKey);
                        commandWrapper.AddInputParameter("@companyTypeKey", SqlDbType.Int, (item.CompanyTypeKey == 0) ? SqlInt32.Null : item.CompanyTypeKey);
                        commandWrapper.AddInputParameter("@portalKey", SqlDbType.Int, (item.PortalKey == 0) ? SqlInt32.Null : item.PortalKey);
                        commandWrapper.AddInputParameter("@companyID", SqlDbType.VarChar, String.IsNullOrEmpty(item.CompanyID) ? 0 : item.CompanyID.Length, String.IsNullOrEmpty(item.CompanyID) ? SqlString.Null : item.CompanyID);
                        commandWrapper.AddInputParameter("@name", SqlDbType.VarChar, String.IsNullOrEmpty(item.Name) ? 0 : item.Name.Length, String.IsNullOrEmpty(item.Name) ? SqlString.Null : item.Name);
                        commandWrapper.AddInputParameter("@legalName", SqlDbType.VarChar, String.IsNullOrEmpty(item.LegalName) ? 0 : item.LegalName.Length, String.IsNullOrEmpty(item.LegalName) ? SqlString.Null : item.LegalName);
                        commandWrapper.AddInputParameter("@taxID", SqlDbType.VarChar, String.IsNullOrEmpty(item.TaxID) ? 0 : item.TaxID.Length, String.IsNullOrEmpty(item.TaxID) ? SqlString.Null : item.TaxID);
                        commandWrapper.AddInputParameter("@work", SqlDbType.VarChar, String.IsNullOrEmpty(item.Work) ? 0 : item.Work.Length, String.IsNullOrEmpty(item.Work) ? SqlString.Null : item.Work);
                        commandWrapper.AddInputParameter("@work2", SqlDbType.VarChar, String.IsNullOrEmpty(item.Work2) ? 0 : item.Work2.Length, String.IsNullOrEmpty(item.Work2) ? SqlString.Null : item.Work2);
                        commandWrapper.AddInputParameter("@fax", SqlDbType.VarChar, String.IsNullOrEmpty(item.Fax) ? 0 : item.Fax.Length, String.IsNullOrEmpty(item.Fax) ? SqlString.Null : item.Fax);
                        commandWrapper.AddInputParameter("@address", SqlDbType.VarChar, String.IsNullOrEmpty(item.Address) ? 0 : item.Address.Length, String.IsNullOrEmpty(item.Address) ? SqlString.Null : item.Address);
                        commandWrapper.AddInputParameter("@address2", SqlDbType.VarChar, String.IsNullOrEmpty(item.Address2) ? 0 : item.Address2.Length, String.IsNullOrEmpty(item.Address2) ? SqlString.Null : item.Address2);
                        commandWrapper.AddInputParameter("@city", SqlDbType.VarChar, String.IsNullOrEmpty(item.City) ? 0 : item.City.Length, String.IsNullOrEmpty(item.City) ? SqlString.Null : item.City);
                        commandWrapper.AddInputParameter("@state", SqlDbType.VarChar, String.IsNullOrEmpty(item.State) ? 0 : item.State.Length, String.IsNullOrEmpty(item.State) ? SqlString.Null : item.State);
                        commandWrapper.AddInputParameter("@zip", SqlDbType.VarChar, String.IsNullOrEmpty(item.Zip) ? 0 : item.Zip.Length, String.IsNullOrEmpty(item.Zip) ? SqlString.Null : item.Zip);
                        commandWrapper.AddInputParameter("@website", SqlDbType.VarChar, String.IsNullOrEmpty(item.Website) ? 0 : item.Website.Length, String.IsNullOrEmpty(item.Website) ? SqlString.Null : item.Website);
                        commandWrapper.AddInputParameter("@description", SqlDbType.VarChar, String.IsNullOrEmpty(item.Description) ? 0 : item.Description.Length, String.IsNullOrEmpty(item.Description) ? SqlString.Null : item.Description);
                        commandWrapper.AddInputParameter("@bidRequestResponseDays", SqlDbType.Int, (item.BidRequestResponseDays == 0) ? SqlInt32.Null : item.BidRequestResponseDays);
                        commandWrapper.AddInputParameter("@bidSubmitDays", SqlDbType.Int, (item.BidSubmitDays == 0) ? SqlInt32.Null : item.BidSubmitDays);
                        commandWrapper.AddInputParameter("@bidRequestAmount", SqlDbType.Money, (item.BidRequestAmount == 0) ? SqlMoney.Null : item.BidRequestAmount);
                        commandWrapper.AddInputParameter("@notificationPreference", SqlDbType.Int, (item.NotificationPreference == 0) ? SqlInt32.Null : item.NotificationPreference);
                        commandWrapper.AddInputParameter("@dateAdded", SqlDbType.DateTime, (item.DateAdded == DateTime.MinValue) ? SqlDateTime.Null : item.DateAdded);
                        commandWrapper.AddInputParameter("@lastModificationTime", SqlDbType.DateTime, (item.LastModificationTime == DateTime.MinValue) ? SqlDateTime.Null : item.LastModificationTime);
                        commandWrapper.AddInputParameter("@status", SqlDbType.Int, (item.Status == 0) ? SqlInt32.Null : item.Status);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@companyKey", SqlDbType.Int);
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        db.ExecuteNonQuery(commandWrapper);

                        // save the newly inserted key value
                        item.CompanyKey = commandWrapper.GetValueInt("@companyKey");

                        status = (commandWrapper.GetValueInt("@errorCode") == 0);
                    }
                }
        	}
        	catch (Exception)
        	{
        	    // error occured...
        	    throw;
        	}

        	return status;
        }

        public virtual bool Update(CompanyModel item)
        {
        	bool status = false;

        	try
        	{
        	    string storedProcedure = "gensp_Company_UpdateOneByCompanyKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@companyKey", SqlDbType.Int, (item.CompanyKey == 0) ? SqlInt32.Null : item.CompanyKey);
                        commandWrapper.AddInputParameter("@parentCompanyKey", SqlDbType.Int, (item.ParentCompanyKey == 0) ? SqlInt32.Null : item.ParentCompanyKey);
                        commandWrapper.AddInputParameter("@relatedCompanyKey", SqlDbType.Int, (item.RelatedCompanyKey == 0) ? SqlInt32.Null : item.RelatedCompanyKey);
                        commandWrapper.AddInputParameter("@companyTypeKey", SqlDbType.Int, (item.CompanyTypeKey == 0) ? SqlInt32.Null : item.CompanyTypeKey);
                        commandWrapper.AddInputParameter("@portalKey", SqlDbType.Int, (item.PortalKey == 0) ? SqlInt32.Null : item.PortalKey);
                        commandWrapper.AddInputParameter("@companyID", SqlDbType.VarChar, String.IsNullOrEmpty(item.CompanyID) ? 0 : item.CompanyID.Length, String.IsNullOrEmpty(item.CompanyID) ? SqlString.Null : item.CompanyID);
                        commandWrapper.AddInputParameter("@name", SqlDbType.VarChar, String.IsNullOrEmpty(item.Name) ? 0 : item.Name.Length, String.IsNullOrEmpty(item.Name) ? SqlString.Null : item.Name);
                        commandWrapper.AddInputParameter("@legalName", SqlDbType.VarChar, String.IsNullOrEmpty(item.LegalName) ? 0 : item.LegalName.Length, String.IsNullOrEmpty(item.LegalName) ? SqlString.Null : item.LegalName);
                        commandWrapper.AddInputParameter("@taxID", SqlDbType.VarChar, String.IsNullOrEmpty(item.TaxID) ? 0 : item.TaxID.Length, String.IsNullOrEmpty(item.TaxID) ? SqlString.Null : item.TaxID);
                        commandWrapper.AddInputParameter("@work", SqlDbType.VarChar, String.IsNullOrEmpty(item.Work) ? 0 : item.Work.Length, String.IsNullOrEmpty(item.Work) ? SqlString.Null : item.Work);
                        commandWrapper.AddInputParameter("@work2", SqlDbType.VarChar, String.IsNullOrEmpty(item.Work2) ? 0 : item.Work2.Length, String.IsNullOrEmpty(item.Work2) ? SqlString.Null : item.Work2);
                        commandWrapper.AddInputParameter("@fax", SqlDbType.VarChar, String.IsNullOrEmpty(item.Fax) ? 0 : item.Fax.Length, String.IsNullOrEmpty(item.Fax) ? SqlString.Null : item.Fax);
                        commandWrapper.AddInputParameter("@address", SqlDbType.VarChar, String.IsNullOrEmpty(item.Address) ? 0 : item.Address.Length, String.IsNullOrEmpty(item.Address) ? SqlString.Null : item.Address);
                        commandWrapper.AddInputParameter("@address2", SqlDbType.VarChar, String.IsNullOrEmpty(item.Address2) ? 0 : item.Address2.Length, String.IsNullOrEmpty(item.Address2) ? SqlString.Null : item.Address2);
                        commandWrapper.AddInputParameter("@city", SqlDbType.VarChar, String.IsNullOrEmpty(item.City) ? 0 : item.City.Length, String.IsNullOrEmpty(item.City) ? SqlString.Null : item.City);
                        commandWrapper.AddInputParameter("@state", SqlDbType.VarChar, String.IsNullOrEmpty(item.State) ? 0 : item.State.Length, String.IsNullOrEmpty(item.State) ? SqlString.Null : item.State);
                        commandWrapper.AddInputParameter("@zip", SqlDbType.VarChar, String.IsNullOrEmpty(item.Zip) ? 0 : item.Zip.Length, String.IsNullOrEmpty(item.Zip) ? SqlString.Null : item.Zip);
                        commandWrapper.AddInputParameter("@website", SqlDbType.VarChar, String.IsNullOrEmpty(item.Website) ? 0 : item.Website.Length, String.IsNullOrEmpty(item.Website) ? SqlString.Null : item.Website);
                        commandWrapper.AddInputParameter("@description", SqlDbType.VarChar, String.IsNullOrEmpty(item.Description) ? 0 : item.Description.Length, String.IsNullOrEmpty(item.Description) ? SqlString.Null : item.Description);
                        commandWrapper.AddInputParameter("@bidRequestResponseDays", SqlDbType.Int, (item.BidRequestResponseDays == 0) ? SqlInt32.Null : item.BidRequestResponseDays);
                        commandWrapper.AddInputParameter("@bidSubmitDays", SqlDbType.Int, (item.BidSubmitDays == 0) ? SqlInt32.Null : item.BidSubmitDays);
                        commandWrapper.AddInputParameter("@bidRequestAmount", SqlDbType.Money, (item.BidRequestAmount == 0) ? SqlMoney.Null : item.BidRequestAmount);
                        commandWrapper.AddInputParameter("@notificationPreference", SqlDbType.Int, (item.NotificationPreference == 0) ? SqlInt32.Null : item.NotificationPreference);
                        commandWrapper.AddInputParameter("@dateAdded", SqlDbType.DateTime, (item.DateAdded == DateTime.MinValue) ? SqlDateTime.Null : item.DateAdded);
                        commandWrapper.AddInputParameter("@lastModificationTime", SqlDbType.DateTime, (item.LastModificationTime == DateTime.MinValue) ? SqlDateTime.Null : item.LastModificationTime);
                        commandWrapper.AddInputParameter("@status", SqlDbType.Int, (item.Status == 0) ? SqlInt32.Null : item.Status);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        db.ExecuteNonQuery(commandWrapper);

                        status = (commandWrapper.GetValueInt("@errorCode") == 0);
                    }
                }
        	}
        	catch (Exception)
        	{
        	    // error occured...
        	    throw;
        	}

        	return status;
        }

        public virtual bool Delete(int id)
        {
        	bool status = false;

        	try
        	{
        	    string storedProcedure = "gensp_Company_DeleteOneByCompanyKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@companyKey", SqlDbType.Int, id);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        db.ExecuteNonQuery(commandWrapper);

                        status = (commandWrapper.GetValueInt("@errorCode") == 0);
                    }
                }
        	}
        	catch (Exception)
        	{
        	    // error occured...
        	    throw;
        	}

        	return status;
        }

        public virtual CompanyModel Get(int id)
        {
            CompanyModel item = null;

            try
            {
                string storedProcedure = "gensp_Company_SelectOneByCompanyKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@companyKey", SqlDbType.Int, id);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            while (dataReader.Read())
                            {
                                item = new CompanyModel();

                                Load(dataReader, item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_Company_SelectOneByCompanyKey");
                        }
                    }
                }
            }
            catch (Exception)
            {
                // error occured...
                throw;
            }

            return item;
        }

        public virtual IList<CompanyModel> GetAll()
        {
            return GetAll(new CompanyFilterModel());
        }

        public virtual IList<CompanyModel> GetAll(CompanyFilterModel filter)
        {
            List<CompanyModel> itemList = new List<CompanyModel>();

            try
            {
                string storedProcedure = "gensp_Company_SelectSomeBySearch";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@parentCompanyKey", SqlDbType.Int, (filter.ParentCompanyKey == 0) ? SqlInt32.Null : filter.ParentCompanyKey);
                        commandWrapper.AddInputParameter("@relatedCompanyKey", SqlDbType.Int, (filter.RelatedCompanyKey == 0) ? SqlInt32.Null : filter.RelatedCompanyKey);
                        commandWrapper.AddInputParameter("@companyTypeKey", SqlDbType.Int, (filter.CompanyTypeKey == 0) ? SqlInt32.Null : filter.CompanyTypeKey);
                        commandWrapper.AddInputParameter("@portalKey", SqlDbType.Int, (filter.PortalKey == 0) ? SqlInt32.Null : filter.PortalKey);
                        commandWrapper.AddInputParameter("@propertyKeyList", SqlDbType.VarChar, filter.PropertyKeyList.Length, String.IsNullOrEmpty(filter.PropertyKeyList) ? SqlString.Null : filter.PropertyKeyList);
                        commandWrapper.AddInputParameter("@state", SqlDbType.VarChar, GetFilterValue(filter.State).Length, String.IsNullOrEmpty(filter.State) ? SqlString.Null : GetFilterValue(filter.State));
                        commandWrapper.AddInputParameter("@name", SqlDbType.VarChar, GetFilterValue(filter.Name).Length, String.IsNullOrEmpty(filter.Name) ? SqlString.Null : GetFilterValue(filter.Name));
                        commandWrapper.AddInputParameter("@status", SqlDbType.Int, (filter.Status == 0) ? SqlInt32.Null : filter.Status);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            CompanyModel item = null;
                            while (dataReader.Read())
                            {
                                item = new CompanyModel();

                                Load(dataReader, item);

                                itemList.Add(item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_Company_SelectSomeBySearch");
                        }
                    }
                }
            }
            catch (Exception)
            {
                // error occured...
                throw;
            }

            return itemList;
        }

        public virtual IList<CompanyModel> GetAll(CompanyFilterModel filter, PagingModel paging)
        {
            List<CompanyModel> itemList = new List<CompanyModel>();

            try
            {
                string storedProcedure = "gensp_Company_SelectSomeBySearchAndPaging";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@parentCompanyKey", SqlDbType.Int, (filter.ParentCompanyKey == 0) ? SqlInt32.Null : filter.ParentCompanyKey);
                        commandWrapper.AddInputParameter("@relatedCompanyKey", SqlDbType.Int, (filter.RelatedCompanyKey == 0) ? SqlInt32.Null : filter.RelatedCompanyKey);
                        commandWrapper.AddInputParameter("@companyTypeKey", SqlDbType.Int, (filter.CompanyTypeKey == 0) ? SqlInt32.Null : filter.CompanyTypeKey);
                        commandWrapper.AddInputParameter("@portalKey", SqlDbType.Int, (filter.PortalKey == 0) ? SqlInt32.Null : filter.PortalKey);
                        commandWrapper.AddInputParameter("@propertyKeyList", SqlDbType.VarChar, filter.PropertyKeyList.Length, String.IsNullOrEmpty(filter.PropertyKeyList) ? SqlString.Null : filter.PropertyKeyList);
                        commandWrapper.AddInputParameter("@state", SqlDbType.VarChar, GetFilterValue(filter.State).Length, String.IsNullOrEmpty(filter.State) ? SqlString.Null : GetFilterValue(filter.State));
                        commandWrapper.AddInputParameter("@name", SqlDbType.VarChar, GetFilterValue(filter.Name).Length, String.IsNullOrEmpty(filter.Name) ? SqlString.Null : GetFilterValue(filter.Name));
                        commandWrapper.AddInputParameter("@status", SqlDbType.Int, (filter.Status == 0) ? SqlInt32.Null : filter.Status);

                        // add paging parameters
                        commandWrapper.AddInputParameter("@sortOrder", SqlDbType.VarChar, String.IsNullOrEmpty(paging.SortOrder) ? 0 : paging.SortOrder.Length, String.IsNullOrEmpty(paging.SortOrder) ? SqlString.Null : paging.SortOrder);
                        commandWrapper.AddInputParameter("@pageSize", SqlDbType.Int, paging.PageSize);
                        commandWrapper.AddInputParameter("@pageNumber", SqlDbType.Int, paging.PageNumber);

                        // add paging stored procedure output parameters
                        commandWrapper.AddOutputParameter("@totalRecordCount", SqlDbType.Int);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            CompanyModel item = null;
                            while (dataReader.Read())
                            {
                                item = new CompanyModel();

                                Load(dataReader, item);

                                itemList.Add(item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_Company_SelectSomeBySearchAndPaging");
                        }

                        // set total record count for paging
                        paging.TotalRecordCount = commandWrapper.GetValueInt("@totalRecordCount");
                    }
                }
            }
            catch (Exception)
            {
                // error occured...
                throw;
            }

            return itemList;
        }

        protected void Load(DBDataReader dataReader, CompanyModel item)
        {
            item.CompanyKey = dataReader.GetValueInt("CompanyKey");
            item.ParentCompanyKey = dataReader.GetValueInt("ParentCompanyKey");
            item.RelatedCompanyKey = dataReader.GetValueInt("RelatedCompanyKey");
            item.CompanyTypeKey = dataReader.GetValueInt("CompanyTypeKey");
            item.PortalKey = dataReader.GetValueInt("PortalKey");
            item.CompanyID = dataReader.GetValueText("CompanyID");
            item.Name = dataReader.GetValueText("Name");
            item.LegalName = dataReader.GetValueText("LegalName");
            item.TaxID = dataReader.GetValueText("TaxID");
            item.Work = dataReader.GetValueText("Work");
            item.Work2 = dataReader.GetValueText("Work2");
            item.Fax = dataReader.GetValueText("Fax");
            item.Address = dataReader.GetValueText("Address");
            item.Address2 = dataReader.GetValueText("Address2");
            item.City = dataReader.GetValueText("City");
            item.State = dataReader.GetValueText("State");
            item.Zip = dataReader.GetValueText("Zip");
            item.Website = dataReader.GetValueText("Website");
            item.Description = dataReader.GetValueText("Description");
            item.BidRequestResponseDays = dataReader.GetValueInt("BidRequestResponseDays");
            item.BidSubmitDays = dataReader.GetValueInt("BidSubmitDays");
            item.BidRequestAmount = dataReader.GetValueDecimal("BidRequestAmount");
            item.NotificationPreference = dataReader.GetValueInt("NotificationPreference");
            item.DateAdded = dataReader.GetValueDateTime("DateAdded");
            item.LastModificationTime = dataReader.GetValueDateTime("LastModificationTime");
            item.Status = dataReader.GetValueInt("Status");
        }
    }
}
