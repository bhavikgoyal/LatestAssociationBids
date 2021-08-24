using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace AssociationBids.Portal.Repository.Base
{
    public class ResourceRepository : BaseRepository, IResourceRepository
    {
        public ResourceRepository() { }

        public ResourceRepository(string connectionString)
            : base(connectionString) { }

        public virtual bool Create(ResourceModel item)
        {
            bool status = false;

            try
            {
                string storedProcedure = "gensp_Resource_InsertOne";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@companyKey", SqlDbType.Int, (item.CompanyKey == 0) ? SqlInt32.Null : item.CompanyKey);

                        commandWrapper.AddInputParameter("@firstName", SqlDbType.VarChar, String.IsNullOrEmpty(item.FirstName) ? 0 : item.FirstName.Length, String.IsNullOrEmpty(item.FirstName) ? SqlString.Null : item.FirstName);
                        commandWrapper.AddInputParameter("@lastName", SqlDbType.VarChar, String.IsNullOrEmpty(item.LastName) ? 0 : item.LastName.Length, String.IsNullOrEmpty(item.LastName) ? SqlString.Null : item.LastName);
                        commandWrapper.AddInputParameter("@title", SqlDbType.VarChar, String.IsNullOrEmpty(item.Title) ? 0 : item.Title.Length, String.IsNullOrEmpty(item.Title) ? SqlString.Null : item.Title);
                        commandWrapper.AddInputParameter("@email", SqlDbType.VarChar, String.IsNullOrEmpty(item.Email) ? 0 : item.Email.Length, String.IsNullOrEmpty(item.Email) ? SqlString.Null : item.Email);
                        commandWrapper.AddInputParameter("@email2", SqlDbType.VarChar, String.IsNullOrEmpty(item.Email2) ? 0 : item.Email2.Length, String.IsNullOrEmpty(item.Email2) ? SqlString.Null : item.Email2);
                        commandWrapper.AddInputParameter("@cellPhone", SqlDbType.VarChar, String.IsNullOrEmpty(item.CellPhone) ? 0 : item.CellPhone.Length, String.IsNullOrEmpty(item.CellPhone) ? SqlString.Null : item.CellPhone);
                        commandWrapper.AddInputParameter("@homePhone", SqlDbType.VarChar, String.IsNullOrEmpty(item.HomePhone) ? 0 : item.HomePhone.Length, String.IsNullOrEmpty(item.HomePhone) ? SqlString.Null : item.HomePhone);
                        commandWrapper.AddInputParameter("@homePhone2", SqlDbType.VarChar, String.IsNullOrEmpty(item.HomePhone2) ? 0 : item.HomePhone2.Length, String.IsNullOrEmpty(item.HomePhone2) ? SqlString.Null : item.HomePhone2);
                        commandWrapper.AddInputParameter("@work", SqlDbType.VarChar, String.IsNullOrEmpty(item.Work) ? 0 : item.Work.Length, String.IsNullOrEmpty(item.Work) ? SqlString.Null : item.Work);
                        commandWrapper.AddInputParameter("@work2", SqlDbType.VarChar, String.IsNullOrEmpty(item.Work2) ? 0 : item.Work2.Length, String.IsNullOrEmpty(item.Work2) ? SqlString.Null : item.Work2);
                        commandWrapper.AddInputParameter("@fax", SqlDbType.VarChar, String.IsNullOrEmpty(item.Fax) ? 0 : item.Fax.Length, String.IsNullOrEmpty(item.Fax) ? SqlString.Null : item.Fax);
                        commandWrapper.AddInputParameter("@address", SqlDbType.VarChar, String.IsNullOrEmpty(item.Address) ? 0 : item.Address.Length, String.IsNullOrEmpty(item.Address) ? SqlString.Null : item.Address);
                        commandWrapper.AddInputParameter("@address2", SqlDbType.VarChar, String.IsNullOrEmpty(item.Address2) ? 0 : item.Address2.Length, String.IsNullOrEmpty(item.Address2) ? SqlString.Null : item.Address2);
                        commandWrapper.AddInputParameter("@city", SqlDbType.VarChar, String.IsNullOrEmpty(item.City) ? 0 : item.City.Length, String.IsNullOrEmpty(item.City) ? SqlString.Null : item.City);
                        commandWrapper.AddInputParameter("@state", SqlDbType.VarChar, String.IsNullOrEmpty(item.State) ? 0 : item.State.Length, String.IsNullOrEmpty(item.State) ? SqlString.Null : item.State);
                        commandWrapper.AddInputParameter("@zip", SqlDbType.VarChar, String.IsNullOrEmpty(item.Zip) ? 0 : item.Zip.Length, String.IsNullOrEmpty(item.Zip) ? SqlString.Null : item.Zip);
                        commandWrapper.AddInputParameter("@primaryContact", SqlDbType.Bit, (item.PrimaryContact == false) ? SqlBoolean.Null : item.PrimaryContact);
                        commandWrapper.AddInputParameter("@description", SqlDbType.VarChar, String.IsNullOrEmpty(item.Description) ? 0 : item.Description.Length, String.IsNullOrEmpty(item.Description) ? SqlString.Null : item.Description);
                        commandWrapper.AddInputParameter("@dateAdded", SqlDbType.DateTime, (item.DateAdded == DateTime.MinValue) ? SqlDateTime.Null : item.DateAdded);
                        commandWrapper.AddInputParameter("@lastModificationTime", SqlDbType.DateTime, (item.LastModificationTime == DateTime.MinValue) ? SqlDateTime.Null : item.LastModificationTime);
                        commandWrapper.AddInputParameter("@status", SqlDbType.Int, (item.Status == 0) ? SqlInt32.Null : item.Status);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@resourceKey", SqlDbType.Int);
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        db.ExecuteNonQuery(commandWrapper);

                        // save the newly inserted key value
                        item.ResourceKey = commandWrapper.GetValueInt("@resourceKey");

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


        protected void GetAllService(DBDataReader dataReader, ResourceModel item)
        {
            item.ServiceKey = dataReader.GetValueInt("ServiceKey");
            item.ServiceTitle1 = dataReader.GetValueText("Title");

        }

        protected void LoadViewEditservice(DBDataReader dataReader, ResourceModel item)
        {
           
            item.ServiceKey = dataReader.GetValueInt("ServiceKey");
            item.ServiceKey1 = dataReader.GetValueInt("ServiceKey");
            item.ServiceKey2 = dataReader.GetValueInt("ServiceKey");
            item.Radius = dataReader.GetValueInt("Radius");
            item.VendorKey= dataReader.GetValueInt("VendorKey");
            

        }
        protected void Getbindservice(DBDataReader dataReader, VendorManagerModel item)
        {
            try
            {
                item.Vendor.Title = dataReader.GetValueText("Title");
                item.ServiceModel.ServiceKey = dataReader.GetValueInt("ServiceKey");
                item.ServiceModel.VendorServiceKey = dataReader.GetValueInt("VendorServiceKey");
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public IList<VendorManagerModel> Getbindservice(int CompanyKey)
        {
            List<VendorManagerModel> itemList = new List<VendorManagerModel>();

            try
            {

                string storedProcedure = "site_VendorManagerService_GetAllByCompanyKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        commandWrapper.AddInputParameter("@serviceKey", SqlDbType.Int, CompanyKey);
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {

                            while (dataReader.Read())
                            {
                                VendorManagerModel item = new VendorManagerModel();
                                item.Vendor = new VendorManagerVendorModel();
                                item.ServiceModel = new VendorServiceModel();
                                item.Resource = new ResourceModel();
                                Getbindservice(dataReader, item);
                                itemList.Add(item);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                // error occured...
                throw;
            }

            return itemList;
        }



        public IList<ResourceModel> GetServiceByCompany(int CompanyKey)
        {
            List<ResourceModel> itemList = new List<ResourceModel>();

            try
            {
                string storedProcedure = "site_vendorManager_SelectOneByvendorKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        commandWrapper.AddInputParameter("@CompanyKey", SqlDbType.Int, (CompanyKey == 0) ? SqlInt32.Null : CompanyKey);
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            ResourceModel item = null;
                            while (dataReader.Read())
                            {
                                item = new ResourceModel();
                                LoadViewEditservice(dataReader, item);
                                itemList.Add(item);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
            }
            return itemList;
        }

        public IList<ResourceModel> AppoGetAllService(string PleaseSelect)
        {
            List<ResourceModel> itemList = new List<ResourceModel>();

            try
            {

                string storedProcedure = "site_Service_GetAll";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            ResourceModel item = null;
                            while (dataReader.Read())
                            {
                                item = new ResourceModel();
                                GetAllService(dataReader, item);
                                itemList.Add(item);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                // error occured...
                throw;
            }

            return itemList;
        }




        public virtual bool Update(ResourceModel item)
        {
            bool status = false;

            try
            {
                string storedProcedure = "gensp_Resource_UpdateOneByResourceKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@resourceKey", SqlDbType.Int, (item.ResourceKey == 0) ? SqlInt32.Null : item.ResourceKey);
                        commandWrapper.AddInputParameter("@companyKey", SqlDbType.Int, (item.CompanyKey == 0) ? SqlInt32.Null : item.CompanyKey);
                        commandWrapper.AddInputParameter("@resourceTypeKey", SqlDbType.Int, (item.ResourceTypeKey == 0) ? SqlInt32.Null : item.ResourceTypeKey);
                        commandWrapper.AddInputParameter("@firstName", SqlDbType.VarChar, String.IsNullOrEmpty(item.FirstName) ? 0 : item.FirstName.Length, String.IsNullOrEmpty(item.FirstName) ? SqlString.Null : item.FirstName);
                        commandWrapper.AddInputParameter("@lastName", SqlDbType.VarChar, String.IsNullOrEmpty(item.LastName) ? 0 : item.LastName.Length, String.IsNullOrEmpty(item.LastName) ? SqlString.Null : item.LastName);
                        commandWrapper.AddInputParameter("@title", SqlDbType.VarChar, String.IsNullOrEmpty(item.Title) ? 0 : item.Title.Length, String.IsNullOrEmpty(item.Title) ? SqlString.Null : item.Title);
                        commandWrapper.AddInputParameter("@email", SqlDbType.VarChar, String.IsNullOrEmpty(item.Email) ? 0 : item.Email.Length, String.IsNullOrEmpty(item.Email) ? SqlString.Null : item.Email);
                        commandWrapper.AddInputParameter("@email2", SqlDbType.VarChar, String.IsNullOrEmpty(item.Email2) ? 0 : item.Email2.Length, String.IsNullOrEmpty(item.Email2) ? SqlString.Null : item.Email2);
                        commandWrapper.AddInputParameter("@cellPhone", SqlDbType.VarChar, String.IsNullOrEmpty(item.CellPhone) ? 0 : item.CellPhone.Length, String.IsNullOrEmpty(item.CellPhone) ? SqlString.Null : item.CellPhone);
                        commandWrapper.AddInputParameter("@homePhone", SqlDbType.VarChar, String.IsNullOrEmpty(item.HomePhone) ? 0 : item.HomePhone.Length, String.IsNullOrEmpty(item.HomePhone) ? SqlString.Null : item.HomePhone);
                        commandWrapper.AddInputParameter("@homePhone2", SqlDbType.VarChar, String.IsNullOrEmpty(item.HomePhone2) ? 0 : item.HomePhone2.Length, String.IsNullOrEmpty(item.HomePhone2) ? SqlString.Null : item.HomePhone2);
                        commandWrapper.AddInputParameter("@work", SqlDbType.VarChar, String.IsNullOrEmpty(item.Work) ? 0 : item.Work.Length, String.IsNullOrEmpty(item.Work) ? SqlString.Null : item.Work);
                        commandWrapper.AddInputParameter("@work2", SqlDbType.VarChar, String.IsNullOrEmpty(item.Work2) ? 0 : item.Work2.Length, String.IsNullOrEmpty(item.Work2) ? SqlString.Null : item.Work2);
                        commandWrapper.AddInputParameter("@fax", SqlDbType.VarChar, String.IsNullOrEmpty(item.Fax) ? 0 : item.Fax.Length, String.IsNullOrEmpty(item.Fax) ? SqlString.Null : item.Fax);
                        commandWrapper.AddInputParameter("@address", SqlDbType.VarChar, String.IsNullOrEmpty(item.Address) ? 0 : item.Address.Length, String.IsNullOrEmpty(item.Address) ? SqlString.Null : item.Address);
                        commandWrapper.AddInputParameter("@address2", SqlDbType.VarChar, String.IsNullOrEmpty(item.Address2) ? 0 : item.Address2.Length, String.IsNullOrEmpty(item.Address2) ? SqlString.Null : item.Address2);
                        commandWrapper.AddInputParameter("@city", SqlDbType.VarChar, String.IsNullOrEmpty(item.City) ? 0 : item.City.Length, String.IsNullOrEmpty(item.City) ? SqlString.Null : item.City);
                        commandWrapper.AddInputParameter("@state", SqlDbType.VarChar, String.IsNullOrEmpty(item.State) ? 0 : item.State.Length, String.IsNullOrEmpty(item.State) ? SqlString.Null : item.State);
                        commandWrapper.AddInputParameter("@zip", SqlDbType.VarChar, String.IsNullOrEmpty(item.Zip) ? 0 : item.Zip.Length, String.IsNullOrEmpty(item.Zip) ? SqlString.Null : item.Zip);
                        commandWrapper.AddInputParameter("@primaryContact", SqlDbType.Bit, (item.PrimaryContact == false) ? SqlBoolean.Null : item.PrimaryContact);

                        commandWrapper.AddInputParameter("@description", SqlDbType.VarChar, String.IsNullOrEmpty(item.Description) ? 0 : item.Description.Length, String.IsNullOrEmpty(item.Description) ? SqlString.Null : item.Description);
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
                string storedProcedure = "gensp_Resource_DeleteOneByResourceKey";
                using (Database db = new Database(ConnectionString))
                {
                    DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure);

                    // add the stored procedure input parameters
                    commandWrapper.AddInputParameter("@resourceKey", SqlDbType.Int, id);

                    // add stored procedure output parameters
                    commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                    db.ExecuteNonQuery(commandWrapper);

                    status = (commandWrapper.GetValueInt("@errorCode") == 0);
                }
            }
            catch (Exception)
            {
                // error occured...
                throw;
            }

            return status;
        }

        public virtual ResourceModel Get(int id)
        {
            ResourceModel item = null;

            try
            {
                string storedProcedure = "gensp_Resource_SelectOneByResourceKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@resourceKey", SqlDbType.Int, id);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            while (dataReader.Read())
                            {
                                item = new ResourceModel();

                                Load(dataReader, item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_Resource_SelectOneByResourceKey");
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

        public virtual IList<ResourceModel> GetAll()
        {
            return GetAll(new ResourceFilterModel());
        }

        public virtual IList<ResourceModel> GetAll(ResourceFilterModel filter)
        {
            List<ResourceModel> itemList = new List<ResourceModel>();

            try
            {
                string storedProcedure = "gensp_Resource_SelectSomeBySearch";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@companyKey", SqlDbType.Int, (filter.CompanyKey == 0) ? SqlInt32.Null : filter.CompanyKey);
                        commandWrapper.AddInputParameter("@resourceTypeKey", SqlDbType.Int, (filter.ResourceTypeKey == 0) ? SqlInt32.Null : filter.ResourceTypeKey);
                        commandWrapper.AddInputParameter("@state", SqlDbType.VarChar, GetFilterValue(filter.State).Length, String.IsNullOrEmpty(filter.State) ? SqlString.Null : GetFilterValue(filter.State));
                        commandWrapper.AddInputParameter("@status", SqlDbType.Int, (filter.Status == 0) ? SqlInt32.Null : filter.Status);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            ResourceModel item = null;
                            while (dataReader.Read())
                            {
                                item = new ResourceModel();

                                Load(dataReader, item);

                                itemList.Add(item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_Resource_SelectSomeBySearch");
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

        public virtual IList<ResourceModel> GetAll(ResourceFilterModel filter, PagingModel paging)
        {
            List<ResourceModel> itemList = new List<ResourceModel>();

            try
            {
                string storedProcedure = "gensp_Resource_SelectSomeBySearchAndPaging";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@companyKey", SqlDbType.Int, (filter.CompanyKey == 0) ? SqlInt32.Null : filter.CompanyKey);
                        commandWrapper.AddInputParameter("@resourceTypeKey", SqlDbType.Int, (filter.ResourceTypeKey == 0) ? SqlInt32.Null : filter.ResourceTypeKey);
                        commandWrapper.AddInputParameter("@state", SqlDbType.VarChar, GetFilterValue(filter.State).Length, String.IsNullOrEmpty(filter.State) ? SqlString.Null : GetFilterValue(filter.State));
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
                            ResourceModel item = null;
                            while (dataReader.Read())
                            {
                                item = new ResourceModel();

                                Load(dataReader, item);

                                itemList.Add(item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_Resource_SelectSomeBySearchAndPaging");
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

        protected void Load(DBDataReader dataReader, ResourceModel item)
        {

            item.ResourceKey = dataReader.GetValueInt("ResourceKey");
            item.CompanyKey = dataReader.GetValueInt("CompanyKey");
            item.ResourceTypeKey = dataReader.GetValueInt("ResourceTypeKey");
            item.FirstName = dataReader.GetValueText("FirstName");
            item.LastName = dataReader.GetValueText("LastName");
            item.Title = dataReader.GetValueText("Title");
            item.Email = dataReader.GetValueText("Email");
            item.Email2 = dataReader.GetValueText("Email2");
            item.CellPhone = dataReader.GetValueText("CellPhone");
            item.HomePhone = dataReader.GetValueText("HomePhone");
            item.HomePhone2 = dataReader.GetValueText("HomePhone2");
            item.Work = dataReader.GetValueText("Work");
            item.Work2 = dataReader.GetValueText("Work2");
            item.Fax = dataReader.GetValueText("Fax");
            item.Address = dataReader.GetValueText("Address");
            item.Address2 = dataReader.GetValueText("Address2");
            item.City = dataReader.GetValueText("City");
            item.State = dataReader.GetValueText("State");
            item.Zip = dataReader.GetValueText("Zip");
            item.PrimaryContact = dataReader.GetValueBool("PrimaryContact");
            item.Description = dataReader.GetValueText("Description");
            item.DateAdded = dataReader.GetValueDateTime("DateAdded");
            item.LastModificationTime = dataReader.GetValueDateTime("LastModificationTime");
            item.Status = dataReader.GetValueInt("Status");
     
        }
        protected void LoadViewEdit(DBDataReader dataReader, ResourceModel item)
        {
            item.Radius= dataReader.GetValueInt("Radius");
            item.ResourceKey = dataReader.GetValueInt("ResourceKey");
            item.CompanyKey = dataReader.GetValueInt("CompanyKey");
            item.ResourceTypeKey = dataReader.GetValueInt("ResourceTypeKey");
            item.FirstName = dataReader.GetValueText("FirstName");
            item.LastName = dataReader.GetValueText("LastName");
            item.Title = dataReader.GetValueText("Title");
            item.Email = dataReader.GetValueText("Email");
            item.Email2 = dataReader.GetValueText("Email2");
            item.CellPhone = dataReader.GetValueText("CellPhone");
            item.HomePhone = dataReader.GetValueText("HomePhone");
            item.HomePhone2 = dataReader.GetValueText("HomePhone2");
            item.Work = dataReader.GetValueText("Work");
            item.Work2 = dataReader.GetValueText("Work2");
            item.Fax = dataReader.GetValueText("Fax");
            item.Address = dataReader.GetValueText("Address");
            item.Address2 = dataReader.GetValueText("Address2");
            item.City = dataReader.GetValueText("City");
            item.State = dataReader.GetValueText("State");
            item.Zip = dataReader.GetValueText("Zip");
            item.PrimaryContact = dataReader.GetValueBool("PrimaryContact");
            item.Description = dataReader.GetValueText("Description");
            item.DateAdded = dataReader.GetValueDateTime("DateAdded");
            item.LastModificationTime = dataReader.GetValueDateTime("LastModificationTime");
            item.Status = dataReader.GetValueInt("Status");
            item.Username = dataReader.GetValueText("Username");
            item.Company = dataReader.GetValueText("CompanyName");
            item.StateKey = dataReader.GetValueText("StateName");
            item.AccountLocked = dataReader.GetValueBool("AccountLocked");
            item.FileName= dataReader.GetValueText("FileName");

        }
        protected void LoadUser(DBDataReader dataReader, ResourceModel item)
        {

            item.ResourceKey = dataReader.GetValueInt("ResourceKey");
            item.FirstName = dataReader.GetValueText("FirstName");
            item.LastName = dataReader.GetValueText("LastName");
            
            item.Email = dataReader.GetValueText("Email");
            item.City = dataReader.GetValueText("City");
            item.State = dataReader.GetValueText("State");
            item.Company = dataReader.GetValueText("Company");

            item.TotalRecords = dataReader.GetValueInt("TotalRecord");




        }

        protected void GetAllCompany(DBDataReader dataReader, ResourceModel item)
        {
            item.CompanyKey = dataReader.GetValueInt("CompanyKey");
            item.Company = dataReader.GetValueText("Name");
        }
        protected void GetAllState(DBDataReader dataReader, ResourceModel item)
        {
            item.StateKey = dataReader.GetValueText("StateKey");
            item.State = dataReader.GetValueText("Title");
        }

        public List<ResourceModel> SearchUser(long PageSize, long PageIndex, string Search, string Sort)
        {
            List<ResourceModel> itemList = new List<ResourceModel>();
            try
            {
                string storedProcedure = "site_User_SelectIndexPaging";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters 
                        commandWrapper.AddInputParameter("@PageSize", SqlDbType.BigInt, (PageSize == 0) ? 0 : PageSize);
                        commandWrapper.AddInputParameter("@PageIndex", SqlDbType.BigInt, (PageIndex == 0) ? 0 : PageIndex);
                        commandWrapper.AddInputParameter("@Search", SqlDbType.NText, (Search == "") ? "" : Search);
                        commandWrapper.AddInputParameter("@Sort", SqlDbType.NText, (Sort == "") ? "" : Sort);
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            ResourceModel item = null;
                            while (dataReader.Read())
                            {
                                item = new ResourceModel();
                                LoadUser(dataReader, item);
                                itemList.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // error occured...
                throw;
            }

            return itemList;
        }

        public bool Insert(ResourceModel item)
        {
            bool status = false;

            try
            {
                           
                string passwordval = "";
                string password = "";
                passwordval = Security.RandomPassword();
                password = Security.Encrypt(passwordval);
                string storedProcedure = "site_Resource_Insert";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@CompanyKey", SqlDbType.Int, (item.CompanyKey == 0) ? SqlInt32.Null : item.CompanyKey);
                        commandWrapper.AddInputParameter("@FirstName", SqlDbType.VarChar, String.IsNullOrEmpty(item.FirstName) ? 0 : item.FirstName.Length, String.IsNullOrEmpty(item.FirstName) ? SqlString.Null : item.FirstName);
                        commandWrapper.AddInputParameter("@LastName", SqlDbType.VarChar, String.IsNullOrEmpty(item.LastName) ? 0 : item.LastName.Length, String.IsNullOrEmpty(item.LastName) ? SqlString.Null : item.LastName);
                        commandWrapper.AddInputParameter("@Title", SqlDbType.VarChar, String.IsNullOrEmpty(item.Title) ? 0 : item.Title.Length, String.IsNullOrEmpty(item.Title) ? SqlString.Null : item.Title);
                        commandWrapper.AddInputParameter("@Email", SqlDbType.VarChar, String.IsNullOrEmpty(item.Email) ? 0 : item.Email.Length, String.IsNullOrEmpty(item.Email) ? SqlString.Null : item.Email);
                        commandWrapper.AddInputParameter("@Email2", SqlDbType.VarChar, String.IsNullOrEmpty(item.Email2) ? 0 : item.Email2.Length, String.IsNullOrEmpty(item.Email2) ? SqlString.Null : item.Email2);
                        commandWrapper.AddInputParameter("@CellPhone", SqlDbType.VarChar, String.IsNullOrEmpty(item.CellPhone) ? 0 : item.CellPhone.Length, String.IsNullOrEmpty(item.CellPhone) ? SqlString.Null : item.CellPhone);
                        commandWrapper.AddInputParameter("@HomePhone", SqlDbType.VarChar, String.IsNullOrEmpty(item.HomePhone) ? 0 : item.HomePhone.Length, String.IsNullOrEmpty(item.HomePhone) ? SqlString.Null : item.HomePhone);
                        commandWrapper.AddInputParameter("@HomePhone2", SqlDbType.VarChar, String.IsNullOrEmpty(item.HomePhone2) ? 0 : item.HomePhone2.Length, String.IsNullOrEmpty(item.HomePhone2) ? SqlString.Null : item.HomePhone2);
                        commandWrapper.AddInputParameter("@Work", SqlDbType.VarChar, String.IsNullOrEmpty(item.Work) ? 0 : item.Work.Length, String.IsNullOrEmpty(item.Work) ? SqlString.Null : item.Work);
                        commandWrapper.AddInputParameter("@Work2", SqlDbType.VarChar, String.IsNullOrEmpty(item.Work2) ? 0 : item.Work2.Length, String.IsNullOrEmpty(item.Work2) ? SqlString.Null : item.Work2);
                        commandWrapper.AddInputParameter("@Fax", SqlDbType.VarChar, String.IsNullOrEmpty(item.Fax) ? 0 : item.Fax.Length, String.IsNullOrEmpty(item.Fax) ? SqlString.Null : item.Fax);
                        commandWrapper.AddInputParameter("@Address", SqlDbType.VarChar, String.IsNullOrEmpty(item.Address) ? 0 : item.Address.Length, String.IsNullOrEmpty(item.Address) ? SqlString.Null : item.Address);
                        commandWrapper.AddInputParameter("@Address2", SqlDbType.VarChar, String.IsNullOrEmpty(item.Address2) ? 0 : item.Address2.Length, String.IsNullOrEmpty(item.Address2) ? SqlString.Null : item.Address2);
                        commandWrapper.AddInputParameter("@City", SqlDbType.VarChar, String.IsNullOrEmpty(item.City) ? 0 : item.City.Length, String.IsNullOrEmpty(item.City) ? SqlString.Null : item.City);
                        commandWrapper.AddInputParameter("@State", SqlDbType.VarChar, String.IsNullOrEmpty(item.State) ? 0 : item.State.Length, String.IsNullOrEmpty(item.State) ? SqlString.Null : item.State);
                        commandWrapper.AddInputParameter("@Zip", SqlDbType.VarChar, String.IsNullOrEmpty(item.Zip) ? 0 : item.Zip.Length, String.IsNullOrEmpty(item.Zip) ? SqlString.Null : item.Zip);
                        commandWrapper.AddInputParameter("@PrimaryContact", SqlDbType.Bit, (item.PrimaryContact == false) ? SqlBoolean.Null : item.PrimaryContact);
                        commandWrapper.AddInputParameter("@Description", SqlDbType.VarChar, String.IsNullOrEmpty(item.Description) ? 0 : item.Description.Length, String.IsNullOrEmpty(item.Description) ? SqlString.Null : item.Description);
                        commandWrapper.AddInputParameter("@Username", SqlDbType.VarChar, String.IsNullOrEmpty(item.Username) ? 0 : item.Username.Length, String.IsNullOrEmpty(item.Username) ? SqlString.Null : item.Username);
                        commandWrapper.AddInputParameter("@Password", SqlDbType.VarChar, password);
                        commandWrapper.AddInputParameter("@Status", SqlDbType.Int, (item.Status == 0) ? SqlInt32.Null : item.Status);
                        commandWrapper.AddInputParameter("@AccountLocked", SqlDbType.Bit, (item.AccountLocked == false) ? SqlBoolean.Null : item.AccountLocked);
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);
                        db.ExecuteNonQuery(commandWrapper);
                        status = (commandWrapper.GetValueInt("@errorCode") == 0);
                        if (status == true)
                        {
                            mailsendAsync(passwordval, item.Email);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // error occured...
                throw;
            }

            return status;
        }
        public void mailsendAsync(string password,string fromemail)
        {
            try
            {
                MailMessage msg = new MailMessage();
                msg.From = new MailAddress(System.Configuration.ConfigurationManager.AppSettings["senderemail"]);
                msg.To.Add(fromemail);
                msg.Subject = "Password";
                msg.Body = "Your Password Is: " + password;
                using (SmtpClient client = new SmtpClient())
                {
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["smtpUser"], System.Configuration.ConfigurationManager.AppSettings["smtpPass"]);
                    client.Host = System.Configuration.ConfigurationManager.AppSettings["smtpServer"];
                    client.Port = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["smtpPort"]);
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.Send(msg);
                }
            }
            catch (Exception ex)
            {

               
            }
          
        }
        public IList<ResourceModel> GetAllCompany()
        {
            List<ResourceModel> itemList = new List<ResourceModel>();

            try
            {
                string storedProcedure = "site_Company_SelectAll";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            ResourceModel item = null;
                            while (dataReader.Read())
                            {
                                item = new ResourceModel();
                                GetAllCompany(dataReader, item);
                                itemList.Add(item);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                // error occured...
                throw;
            }

            return itemList;
        }
        public IList<ResourceModel> GetAllState()
        {
            List<ResourceModel> itemList = new List<ResourceModel>();

            try
            {

                string storedProcedure = "site_State_SelectAll";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            ResourceModel item = null;
                            while (dataReader.Read())
                            {
                                item = new ResourceModel();
                                GetAllState(dataReader, item);
                                itemList.Add(item);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                // error occured...
                throw;
            }

            return itemList;
        }
        public virtual ResourceModel GetDataViewEdit(int id)
        {
            ResourceModel item = null;
            
            try
            {
                string storedProcedure = "site_Resource_SelectOneByResourceKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@resourceKey", SqlDbType.Int, id);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            while (dataReader.Read())
                            {
                                item = new ResourceModel();

                                LoadViewEdit(dataReader, item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_Resource_SelectOneByResourceKey");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // error occured...
                throw;
            }

            return item;
        }
        public virtual ResourceModel GetDataViewEditByCompanyKey(int id)
        {
            ResourceModel item = null;

            try
            {
                string storedProcedure = "site_Resource_SelectOneByCompanyKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@CompanyKey", SqlDbType.Int, id);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            while (dataReader.Read())
                            {
                                item = new ResourceModel();

                                LoadViewEdit(dataReader, item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_Resource_SelectOneByResourceKey");
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
        public virtual bool Edit(ResourceModel item)
        {
            bool status = false;

            try
            {
                string storedProcedure = "site_Resource_Update";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@resourceKey", SqlDbType.Int, (item.ResourceKey == 0) ? SqlInt32.Null : item.ResourceKey);
                        commandWrapper.AddInputParameter("@companyKey", SqlDbType.Int, (item.CompanyKey == 0) ? SqlInt32.Null : item.CompanyKey);
                        commandWrapper.AddInputParameter("@resourceTypeKey", SqlDbType.Int, (item.ResourceTypeKey == 0) ? SqlInt32.Null : item.ResourceTypeKey);
                        commandWrapper.AddInputParameter("@username", SqlDbType.VarChar, String.IsNullOrEmpty(item.Username) ? 0 : item.Username.Length, String.IsNullOrEmpty(item.Username) ? SqlString.Null : item.Username);
                        commandWrapper.AddInputParameter("@firstName", SqlDbType.VarChar, String.IsNullOrEmpty(item.FirstName) ? 0 : item.FirstName.Length, String.IsNullOrEmpty(item.FirstName) ? SqlString.Null : item.FirstName);
                        commandWrapper.AddInputParameter("@lastName", SqlDbType.VarChar, String.IsNullOrEmpty(item.LastName) ? 0 : item.LastName.Length, String.IsNullOrEmpty(item.LastName) ? SqlString.Null : item.LastName);
                        commandWrapper.AddInputParameter("@title", SqlDbType.VarChar, String.IsNullOrEmpty(item.Title) ? 0 : item.Title.Length, String.IsNullOrEmpty(item.Title) ? SqlString.Null : item.Title);
                        commandWrapper.AddInputParameter("@email", SqlDbType.VarChar, String.IsNullOrEmpty(item.Email) ? 0 : item.Email.Length, String.IsNullOrEmpty(item.Email) ? SqlString.Null : item.Email);
                        commandWrapper.AddInputParameter("@email2", SqlDbType.VarChar, String.IsNullOrEmpty(item.Email2) ? 0 : item.Email2.Length, String.IsNullOrEmpty(item.Email2) ? SqlString.Null : item.Email2);
                        commandWrapper.AddInputParameter("@cellPhone", SqlDbType.VarChar, String.IsNullOrEmpty(item.CellPhone) ? 0 : item.CellPhone.Length, String.IsNullOrEmpty(item.CellPhone) ? SqlString.Null : item.CellPhone);
                        commandWrapper.AddInputParameter("@homePhone", SqlDbType.VarChar, String.IsNullOrEmpty(item.HomePhone) ? 0 : item.HomePhone.Length, String.IsNullOrEmpty(item.HomePhone) ? SqlString.Null : item.HomePhone);
                        commandWrapper.AddInputParameter("@homePhone2", SqlDbType.VarChar, String.IsNullOrEmpty(item.HomePhone2) ? 0 : item.HomePhone2.Length, String.IsNullOrEmpty(item.HomePhone2) ? SqlString.Null : item.HomePhone2);
                        commandWrapper.AddInputParameter("@work", SqlDbType.VarChar, String.IsNullOrEmpty(item.Work) ? 0 : item.Work.Length, String.IsNullOrEmpty(item.Work) ? SqlString.Null : item.Work);
                        commandWrapper.AddInputParameter("@work2", SqlDbType.VarChar, String.IsNullOrEmpty(item.Work2) ? 0 : item.Work2.Length, String.IsNullOrEmpty(item.Work2) ? SqlString.Null : item.Work2);
                        commandWrapper.AddInputParameter("@fax", SqlDbType.VarChar, String.IsNullOrEmpty(item.Fax) ? 0 : item.Fax.Length, String.IsNullOrEmpty(item.Fax) ? SqlString.Null : item.Fax);
                        commandWrapper.AddInputParameter("@address", SqlDbType.VarChar, String.IsNullOrEmpty(item.Address) ? 0 : item.Address.Length, String.IsNullOrEmpty(item.Address) ? SqlString.Null : item.Address);
                        commandWrapper.AddInputParameter("@address2", SqlDbType.VarChar, String.IsNullOrEmpty(item.Address2) ? 0 : item.Address2.Length, String.IsNullOrEmpty(item.Address2) ? SqlString.Null : item.Address2);
                        commandWrapper.AddInputParameter("@city", SqlDbType.VarChar, String.IsNullOrEmpty(item.City) ? 0 : item.City.Length, String.IsNullOrEmpty(item.City) ? SqlString.Null : item.City);
                        commandWrapper.AddInputParameter("@state", SqlDbType.VarChar, String.IsNullOrEmpty(item.State) ? 0 : item.State.Length, String.IsNullOrEmpty(item.State) ? SqlString.Null : item.State);
                        commandWrapper.AddInputParameter("@zip", SqlDbType.VarChar, String.IsNullOrEmpty(item.Zip) ? 0 : item.Zip.Length, String.IsNullOrEmpty(item.Zip) ? SqlString.Null : item.Zip);
                        commandWrapper.AddInputParameter("@primaryContact", SqlDbType.Bit, (item.PrimaryContact == false) ? SqlBoolean.Null : item.PrimaryContact);
                        commandWrapper.AddInputParameter("@AccountLocked", SqlDbType.Bit, (item.AccountLocked == false) ? SqlBoolean.Null : item.AccountLocked);
                        commandWrapper.AddInputParameter("@description", SqlDbType.VarChar, String.IsNullOrEmpty(item.Description) ? 0 : item.Description.Length, String.IsNullOrEmpty(item.Description) ? SqlString.Null : item.Description);
                        commandWrapper.AddInputParameter("@status", SqlDbType.Int, (item.Status == 0) ? SqlInt32.Null : item.Status);
                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);
                        db.ExecuteNonQuery(commandWrapper);
                        status = (commandWrapper.GetValueInt("@errorCode") == 0);
                    }
                }
            }
            catch (Exception ex)
            {
                // error occured...
                throw;
            }

            return status;
        }
        public virtual bool PropertyMangerProfileEdit(ResourceModel item)
        {
            bool status = false;

            try
            {
                string storedProcedure = "site_Resource_UpdateProfile";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@VendorKey", SqlDbType.Int, (item.VendorKey == 0) ? SqlInt32.Null : item.VendorKey);
                        commandWrapper.AddInputParameter("@RadiusKey", SqlDbType.Int, (item.Radius == 0) ? SqlInt32.Null : item.Radius);
                        commandWrapper.AddInputParameter("@resourceKey", SqlDbType.Int, (item.ResourceKey == 0) ? SqlInt32.Null : item.ResourceKey);
                        commandWrapper.AddInputParameter("@title", SqlDbType.VarChar, String.IsNullOrEmpty(item.Title) ? 0 : item.Title.Length, String.IsNullOrEmpty(item.Title) ? SqlString.Null : item.Title);
                        commandWrapper.AddInputParameter("@firstName", SqlDbType.VarChar, String.IsNullOrEmpty(item.FirstName) ? 0 : item.FirstName.Length, String.IsNullOrEmpty(item.FirstName) ? SqlString.Null : item.FirstName);
                        commandWrapper.AddInputParameter("@lastName", SqlDbType.VarChar, String.IsNullOrEmpty(item.LastName) ? 0 : item.LastName.Length, String.IsNullOrEmpty(item.LastName) ? SqlString.Null : item.LastName);
                        commandWrapper.AddInputParameter("@email", SqlDbType.VarChar, String.IsNullOrEmpty(item.Email) ? 0 : item.Email.Length, String.IsNullOrEmpty(item.Email) ? SqlString.Null : item.Email);
                        commandWrapper.AddInputParameter("@cellPhone", SqlDbType.VarChar, String.IsNullOrEmpty(item.CellPhone) ? 0 : item.CellPhone.Length, String.IsNullOrEmpty(item.CellPhone) ? SqlString.Null : item.CellPhone);
                        commandWrapper.AddInputParameter("@work", SqlDbType.VarChar, String.IsNullOrEmpty(item.Work) ? 0 : item.Work.Length, String.IsNullOrEmpty(item.Work) ? SqlString.Null : item.Work);
                        commandWrapper.AddInputParameter("@work2", SqlDbType.VarChar, String.IsNullOrEmpty(item.Work2) ? 0 : item.Work2.Length, String.IsNullOrEmpty(item.Work2) ? SqlString.Null : item.Work2);
                        commandWrapper.AddInputParameter("@address", SqlDbType.VarChar, String.IsNullOrEmpty(item.Address) ? 0 : item.Address.Length, String.IsNullOrEmpty(item.Address) ? SqlString.Null : item.Address);
                        commandWrapper.AddInputParameter("@address2", SqlDbType.VarChar, String.IsNullOrEmpty(item.Address2) ? 0 : item.Address2.Length, String.IsNullOrEmpty(item.Address2) ? SqlString.Null : item.Address2);
                        commandWrapper.AddInputParameter("@city", SqlDbType.VarChar, String.IsNullOrEmpty(item.City) ? 0 : item.City.Length, String.IsNullOrEmpty(item.City) ? SqlString.Null : item.City);
                        commandWrapper.AddInputParameter("@state", SqlDbType.VarChar, String.IsNullOrEmpty(item.State) ? 0 : item.State.Length, String.IsNullOrEmpty(item.State) ? SqlString.Null : item.State);
                        commandWrapper.AddInputParameter("@zip", SqlDbType.VarChar, String.IsNullOrEmpty(item.Zip) ? 0 : item.Zip.Length, String.IsNullOrEmpty(item.Zip) ? SqlString.Null : item.Zip);
                        commandWrapper.AddInputParameter("@latitude", SqlDbType.Decimal, item.Latitude);
                        commandWrapper.AddInputParameter("@longitude", SqlDbType.Decimal, item.Longitude);
                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);
                        db.ExecuteNonQuery(commandWrapper);
                        status = (commandWrapper.GetValueInt("@errorCode") == 0);
                    }
                }
            }
            catch (Exception ex)
            {
                // error occured...
                throw new Exception("USP_Resource_UpdateUpdate: "+ex.Message); ;
            }

            return status;
        }

        public virtual bool PropertyMangerProfileImage(Int32 ResourceKey, string Title, string Controller, string Action, string ImageName, Int64 ImageLength) {
            bool status = false;

            try
            {
                string storedProcedure = "site_Resource_UpdateProfileImage";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@ResourceKey", SqlDbType.Int, (ResourceKey == 0) ? SqlInt32.Null : ResourceKey);
                        commandWrapper.AddInputParameter("@Title", SqlDbType.VarChar, String.IsNullOrEmpty(Title) ? 0 : Title.Length, String.IsNullOrEmpty(Title) ? SqlString.Null : Title);
                        commandWrapper.AddInputParameter("@Controller", SqlDbType.VarChar, String.IsNullOrEmpty(Controller) ? 0 : Controller.Length, String.IsNullOrEmpty(Controller) ? SqlString.Null : Controller);
                        commandWrapper.AddInputParameter("@Action", SqlDbType.VarChar, String.IsNullOrEmpty(Action) ? 0 : Action.Length, String.IsNullOrEmpty(Action) ? SqlString.Null : Action);
                        commandWrapper.AddInputParameter("@ImageName", SqlDbType.VarChar, String.IsNullOrEmpty(ImageName) ? 0 : ImageName.Length, String.IsNullOrEmpty(ImageName) ? SqlString.Null : ImageName);
                        commandWrapper.AddInputParameter("@ImageLength", SqlDbType.BigInt, (ImageLength == 0) ? SqlInt64.Null : ImageLength);
                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);
                        db.ExecuteNonQuery(commandWrapper);
                        status = (commandWrapper.GetValueInt("@errorCode") == 0);
                    }
                }
            }
            catch (Exception ex)
            {
                // error occured...
                throw new Exception("site_Resource_UpdateProfileImage: " + ex.Message); ;
            }

            return status;
        }

        public virtual string SaveProfilePassword(Int32 ResourceId, string OldPassword, string NewPassword) {
            string status ="";

            try
            {
                OldPassword = Security.Encrypt(OldPassword);
                NewPassword = Security.Encrypt(NewPassword);

                string storedProcedure = "site_Resource_SaveProfilePassword";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@ResourceKey", SqlDbType.Int, (ResourceId == 0) ? SqlInt32.Null : ResourceId);
                        commandWrapper.AddInputParameter("@OldPassword", SqlDbType.VarChar, String.IsNullOrEmpty(OldPassword) ? 0 : OldPassword.Length, String.IsNullOrEmpty(OldPassword) ? SqlString.Null : OldPassword);
                        commandWrapper.AddInputParameter("@NewPassword", SqlDbType.VarChar, String.IsNullOrEmpty(NewPassword) ? 0 : NewPassword.Length, String.IsNullOrEmpty(NewPassword) ? SqlString.Null : NewPassword);
                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.VarChar, 4000);
                        db.ExecuteNonQuery(commandWrapper);
                        status = Convert.ToString(commandWrapper.GetValueText("@errorCode"));
                    }
                }
            }
            catch (Exception ex)
            {
                // error occured...
                throw new Exception("site_Resource_SaveProfilePassword: " + ex.Message); ;
            }

            return status;
        }


        public virtual bool Remove(int id)
        {
            bool status = false;

            try
            {
                string storedProcedure = "site_Resource_Delete";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@ResourceKey", SqlDbType.Int, id);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        db.ExecuteNonQuery(commandWrapper);

                        status = (commandWrapper.GetValueInt("@errorCode") == 0);
                    }
                }
            }
            catch (Exception ex)
            {
                // error occured...
                
            }

            return status;
        }
    }
}
