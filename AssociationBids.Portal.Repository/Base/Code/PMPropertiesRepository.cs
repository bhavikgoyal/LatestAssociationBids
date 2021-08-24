using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;

namespace AssociationBids.Portal.Repository.Base.Code
{
    public class PMPropertiesRepository : BaseRepository, IPMPropertiesRepository
    {
        public PMPropertiesRepository() { }

        public PMPropertiesRepository(string connectionString)
          : base(connectionString) { }

        protected void Load(DBDataReader dataReader, PropertyModel item)
        {

            item.PropertyKey = dataReader.GetValueInt("PropertyKey");
            item.CompanyKey = dataReader.GetValueInt("CompanyKey");
            item.Title = dataReader.GetValueText("Title");
            item.NumberOfUnits = dataReader.GetValueInt("NumberOfUnits");
            item.Address = dataReader.GetValueText("Address");
            item.Address2 = dataReader.GetValueText("Address2");
            item.City = dataReader.GetValueText("City");
            item.State = dataReader.GetValueText("State");
            item.Zip = dataReader.GetValueText("Zip");
            item.BidRequestAmount = dataReader.GetValueDecimal("BidRequestAmount");
            item.MinimumInsuranceAmount = dataReader.GetValueDecimal("MinimumInsuranceAmount");
            item.Description = dataReader.GetValueText("Description");
            item.Status = dataReader.GetValueInt("Status");

        }
        protected void LoadViewEdit(DBDataReader dataReader, PropertyModel item)
        {


            item.PropertyKey = dataReader.GetValueInt("PropertyKey");
            item.CompanyKey = dataReader.GetValueInt("CompanyKey");
            item.Title = dataReader.GetValueText("Title");
            item.NumberOfUnits = dataReader.GetValueInt("NumberOfUnits");
            item.Address = dataReader.GetValueText("Address");
            item.Address2 = dataReader.GetValueText("Address2");
            item.City = dataReader.GetValueText("City");
            item.State = dataReader.GetValueText("State");
            item.Zip = dataReader.GetValueText("Zip");
            item.BidRequestAmount = dataReader.GetValueDecimal("BidRequestAmount");
            item.MinimumInsuranceAmount = dataReader.GetValueDecimal("MinimumInsuranceAmount");
            item.Description = dataReader.GetValueText("Description");
            item.Status = dataReader.GetValueInt("Status");
        }

        protected void LoadProperty(DBDataReader dataReader, PropertyModel item)
        {

            item.PropertyKey = dataReader.GetValueInt("PropertyKey");
            item.Title = dataReader.GetValueText("Title");
            item.Address = dataReader.GetValueText("Address");
            item.NumberOfUnits = dataReader.GetValueInt("NumberOfUnits");
            item.TotalRecords = dataReader.GetValueInt("TotalRecord");
        }
        protected void Loadbidrequest(DBDataReader dataReader, PropertyModel item)
        {

            item.BidRequestKey = dataReader.GetValueInt("BidRequestKey");
            item.Title = dataReader.GetValueText("Title");
            item.BidDueDate = dataReader.GetValueText("BidDueDate");
            item.NoofBid = dataReader.GetValueInt("NoofBid");
            item.BidRequestStatus = dataReader.GetValueText("BidRequestStatus");
            item.sstartddate = dataReader.GetValueText("sstartddate");

        }

        protected void GetbindDocumentBid(DBDataReader dataReader, BidRequestModel item)
        {
            try
            {
                item.FileName = dataReader.GetValueText("FileName");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IList<BidRequestModel> GetbindDocumentBid(int CompanyKey, int ModuleKey)
        {
            List<BidRequestModel> itemList = new List<BidRequestModel>();

            try
            {
                string storedProcedure = "site_VendorDocument_SelectAll_V2";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        commandWrapper.AddInputParameter("@InsuranceKey", SqlDbType.Int, CompanyKey);
                        //commandWrapper.AddInputParameter("@ModuleKey", SqlDbType.Int, ModuleKey);
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            BidRequestModel item = null;
                            while (dataReader.Read())
                            {
                                item = new BidRequestModel();
                                GetbindDocumentBid(dataReader, item);
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

        protected void LoadWorkOrder(DBDataReader dataReader, PropertyModel item)
        {

            item.BidRequestKey = dataReader.GetValueInt("BidRequestKey");
            item.Title = dataReader.GetValueText("Title");
            item.Propertyname = dataReader.GetValueText("Propertyname");
            item.BidDueDate = dataReader.GetValueText("BidDueDate");
            item.NoofBid = dataReader.GetValueInt("NoofBid");
            item.BidRequestStatus = dataReader.GetValueText("BidRequestStatus");
            item.sstartddate = dataReader.GetValueText("sstartddate");

        }

        protected void LoadHistoryTabdata(DBDataReader dataReader, PropertyModel item)
        {

            item.BidRequestKey = dataReader.GetValueInt("BidRequestKey");
            item.PropertyKey = dataReader.GetValueInt("PropertyKey");
            item.Title = dataReader.GetValueText("Title");
            item.ServiceTitle = dataReader.GetValueText("ServiceTitle");
            item.Propertyname = dataReader.GetValueText("Propertyname");
            item.BidDueDate = dataReader.GetValueText("BidDueDate"); 
            item.NoofBid = dataReader.GetValueInt("NoofBid");
            item.Description = dataReader.GetValueText("Description");
            item.BidRequestStatus = dataReader.GetValueText("BidRequestStatus");
            item.sstartddate = dataReader.GetValueText("sstartddate");

        }
        protected void LoadVendorByBidRequest(DBDataReader dataReader, PropertyModel item)
        {
            int bidrequest = item.BidRequestKey;
            item.Name = dataReader.GetValueText("Name");
            item.ContactPerson = dataReader.GetValueText("ContactPerson");
            item.Service = dataReader.GetValueText("Service");
            item.CompanyKey = dataReader.GetValueInt("CompanyKey");
            item.ResponseDueDates = dataReader.GetValueText("RespondByDate");
            item.BidAmount = dataReader.GetValueDecimal("BidAmount");
            item.BidReqStatus = dataReader.GetValueText("BidVendorStatus");
            item.VendorKey = dataReader.GetValueInt("VendorKey");
            item.BidVendorKey = dataReader.GetValueInt("BidVendorKey");
        }


        public List<PropertyModel> SearchVendorByBidRequest(int BidRequestKey, int modulekey)
        {
            List<PropertyModel> itemList = new List<PropertyModel>();
            try
            {
                string storedProcedure = "site_BidRequest_GetVendorsForBidRequestByAlgorithm";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        commandWrapper.AddInputParameter("@BidRequestkey", SqlDbType.Int, (BidRequestKey == 0) ? 0 : BidRequestKey);
                        commandWrapper.AddInputParameter("@Modulekey", SqlDbType.Int, modulekey);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            PropertyModel item = null;
                            while (dataReader.Read())
                            {
                                item = new PropertyModel();
                                LoadVendorByBidRequest(dataReader, item);
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

        public List<PropertyModel> SearchVendorByBidRequest(int BidRequestKey, int modulekey,int Resourcekey)
        {
            List<PropertyModel> itemList = new List<PropertyModel>();
            try
            {
                string storedProcedure = "site_BidRequest_GetVendorsForBidRequestByAlgorithm_Copy";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        commandWrapper.AddInputParameter("@BidRequestkey", SqlDbType.Int, (BidRequestKey == 0) ? 0 : BidRequestKey);
                        commandWrapper.AddInputParameter("@Modulekey", SqlDbType.Int, modulekey);
                        commandWrapper.AddInputParameter("@ResourceKey", SqlDbType.Int, Resourcekey);



                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            PropertyModel item = null;
                            while (dataReader.Read())
                            {
                                item = new PropertyModel();
                                LoadVendorByBidRequest(dataReader, item);
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
        public List<PropertyModel> SearchBidrequest(long PageSize, long PageIndex, string Sort, string PropertyKey)
        {
            List<PropertyModel> itemList = new List<PropertyModel>();
            try
            {
                string storedProcedure = "site_Property_SelectBidRequestFormanager";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters 
                        commandWrapper.AddInputParameter("@PageSize", SqlDbType.BigInt, (PageSize == 0) ? 0 : PageSize);
                        commandWrapper.AddInputParameter("@PageIndex", SqlDbType.BigInt, (PageIndex == 0) ? 0 : PageIndex);
                        commandWrapper.AddInputParameter("@Sort", SqlDbType.NText, (Sort == "") ? "" : Sort);
                        commandWrapper.AddInputParameter("@PropertyKey", SqlDbType.NText, (PropertyKey == "") ? "" : PropertyKey);
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            PropertyModel item = null;
                            while (dataReader.Read())
                            {
                                item = new PropertyModel();
                                Loadbidrequest(dataReader, item);
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

        public List<PropertyModel> SearchWorkOrder(long PageSize, long PageIndex, string Sort, string PropertyKey)
        {
            List<PropertyModel> itemList = new List<PropertyModel>();
            try
            {
                string storedProcedure = "site_Property_SelectworkorderFormanager";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters 
                        commandWrapper.AddInputParameter("@PageSize", SqlDbType.BigInt, (PageSize == 0) ? 0 : PageSize);
                        commandWrapper.AddInputParameter("@PageIndex", SqlDbType.BigInt, (PageIndex == 0) ? 0 : PageIndex);
                        commandWrapper.AddInputParameter("@Sort", SqlDbType.NText, (Sort == "") ? "" : Sort);
                        commandWrapper.AddInputParameter("@PropertyKey", SqlDbType.NText, (PropertyKey == "") ? "" : PropertyKey);
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            PropertyModel item = null;
                            while (dataReader.Read())
                            {
                                item = new PropertyModel();
                                LoadWorkOrder(dataReader, item);
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

        public List<PropertyModel> SearchUser(long PageSize, long PageIndex, string Search, string Sort, int ResourceKey)
        {
            List<PropertyModel> itemList = new List<PropertyModel>();
            try
            {

                string storedProcedure = "site_Property_SelectIndexPaging";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters 
                        commandWrapper.AddInputParameter("@PageSize", SqlDbType.BigInt, (PageSize == 0) ? 0 : PageSize);
                        commandWrapper.AddInputParameter("@PageIndex", SqlDbType.BigInt, (PageIndex == 0) ? 0 : PageIndex);
                        commandWrapper.AddInputParameter("@Search", SqlDbType.NText, (Search == "") ? "" : Search);
                        commandWrapper.AddInputParameter("@Sort", SqlDbType.NText, (Sort == "") ? "" : Sort);
                        commandWrapper.AddInputParameter("@resourcekey", SqlDbType.BigInt, (ResourceKey == 0) ? 0 : ResourceKey);
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            PropertyModel item = null;
                            while (dataReader.Read())
                            {
                                item = new PropertyModel();
                                LoadProperty(dataReader, item);
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

        public PropertyModel GetUsersDetails(PropertyModel PMPropertiesModel)
        {
            throw new NotImplementedException();
        }
        public virtual PropertyModel GetDataViewEdit(int id)
        {
            PropertyModel item = null;

            try
            {
                string storedProcedure = "site_Property_SelectOneByPropertyKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@PropertyKey", SqlDbType.Int, id);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            while (dataReader.Read())
                            {
                                item = new PropertyModel();

                                LoadViewEdit(dataReader, item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
            }

            return item;
        }

        public virtual PropertyModel GetBidDataForProperties(int BidRequestKey)
        {
            PropertyModel item = null;

            try
            {
                string storedProcedure = "site_Property_getBidrequestDetails";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@BidRequestKey", SqlDbType.Int, BidRequestKey);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            while (dataReader.Read())
                            {
                                item = new PropertyModel();

                                LoadHistoryTabdata(dataReader, item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
            }

            return item;
        }

        public IList<PropertyModel> GetAll(PropertyFilterModel propertyFilterModel, PagingModel paging)
        {
            throw new NotImplementedException();
        }

        protected void GetAllCompany(DBDataReader dataReader, PropertyModel item)
        {
            item.CompanyKey = dataReader.GetValueInt("CompanyKey");
            item.Company = dataReader.GetValueText("Name");

        }
        protected void GetAllState(DBDataReader dataReader, PropertyModel item)
        {
            item.StateKey = dataReader.GetValueText("StateKey");
            item.State = dataReader.GetValueText("Title");
        }
        protected void GetAllManager(DBDataReader dataReader, PropertyModel item)
        {

            item.ResourceKey = dataReader.GetValueText("ResourceKey");
            item.Name = dataReader.GetValueText("Name");


        }

        protected void GetResorceKey(DBDataReader dataReader, PropertyModel item)
        {

            item.ResourceKey = dataReader.GetValueText("ResourceKey");
        }


        protected void GetbindDocument(DBDataReader dataReader, PropertyModel item)
        {
            try
            {
                item.FileName = dataReader.GetValueText("FileName");
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public IList<PropertyModel> GetbindDocument(int PropertyKey)
        {
            List<PropertyModel> itemList = new List<PropertyModel>();

            try
            {

                string storedProcedure = "site_Document_SelectAll";
                storedProcedure = "site_Document_SelectAll_New";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        commandWrapper.AddInputParameter("@PropertyKey", SqlDbType.Int, PropertyKey);
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            PropertyModel item = null;
                            while (dataReader.Read())
                            {
                                item = new PropertyModel();
                                GetbindDocument(dataReader, item);
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

        public int GetGroupKey(string managername)
        {
            int status = 0;
            List<PropertyModel> itemList = new List<PropertyModel>();
            try
            {

                string storedProcedure = "site_GetGroupkey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@ResourceKey", SqlDbType.VarChar, managername);
                        ////commandWrapper.AddOutputParameter("@ResourceKey", SqlDbType.Int);
                        //db.ExecuteNonQuery(commandWrapper);
                        ////status = commandWrapper.GetValueInt("@ResourceKey");
                        ///

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            PropertyModel item = null;
                            while (dataReader.Read())
                            {
                                item = new PropertyModel();
                                GetResorceKey(dataReader, item);
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

            return status;
        }


        public Int64 Insert(PropertyModel item, string strinbuilder, string strinbuilder1)
        {
            Int64 status = 0;

            try
            {

                string storedProcedure = "site_Property_Insert";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@CompanyKey", SqlDbType.Int, (item.CompanyKey == 0) ? SqlInt32.Null : item.CompanyKey);
                        commandWrapper.AddInputParameter("@Title", SqlDbType.VarChar, String.IsNullOrEmpty(item.Title) ? 0 : item.Title.Length, String.IsNullOrEmpty(item.Title) ? SqlString.Null : item.Title);
                        commandWrapper.AddInputParameter("@NumberOfUnits", SqlDbType.Int, (item.NumberOfUnits == 0) ? SqlInt32.Null : item.NumberOfUnits);
                        commandWrapper.AddInputParameter("@Address", SqlDbType.VarChar, String.IsNullOrEmpty(item.Address) ? 0 : item.Address.Length, String.IsNullOrEmpty(item.Address) ? SqlString.Null : item.Address);
                        commandWrapper.AddInputParameter("@Address2", SqlDbType.VarChar, String.IsNullOrEmpty(item.Address2) ? 0 : item.Address2.Length, String.IsNullOrEmpty(item.Address2) ? SqlString.Null : item.Address2);
                        commandWrapper.AddInputParameter("@City", SqlDbType.VarChar, String.IsNullOrEmpty(item.City) ? 0 : item.City.Length, String.IsNullOrEmpty(item.City) ? SqlString.Null : item.City);
                        commandWrapper.AddInputParameter("@State", SqlDbType.VarChar, String.IsNullOrEmpty(item.State) ? 0 : item.State.Length, String.IsNullOrEmpty(item.State) ? SqlString.Null : item.State);
                        commandWrapper.AddInputParameter("@Zip", SqlDbType.VarChar, String.IsNullOrEmpty(item.Zip) ? 0 : item.Zip.Length, String.IsNullOrEmpty(item.Zip) ? SqlString.Null : item.Zip);
                        commandWrapper.AddInputParameter("@BidRequestAmount", SqlDbType.Money, (item.BidRequestAmount == 0) ? SqlMoney.Null : item.BidRequestAmount);
                        commandWrapper.AddInputParameter("@MinimumInsuranceAmount", SqlDbType.Money, (item.MinimumInsuranceAmount == 0) ? SqlMoney.Null : item.MinimumInsuranceAmount);
                        commandWrapper.AddInputParameter("@Description", SqlDbType.VarChar, String.IsNullOrEmpty(item.Description) ? 0 : item.Description.Length, String.IsNullOrEmpty(item.Description) ? SqlString.Null : item.Description);
                        commandWrapper.AddInputParameter("@Status", SqlDbType.Int, (item.Status == 0) ? SqlInt32.Null : item.Status);
                        commandWrapper.AddInputParameter("@ResourceKey", SqlDbType.VarChar, String.IsNullOrEmpty(item.ResourceKey) ? 0 : item.ResourceKey.Length, String.IsNullOrEmpty(item.ResourceKey) ? SqlString.Null : item.ResourceKey);
                        commandWrapper.AddInputParameter("@FileName", SqlDbType.VarChar, String.IsNullOrEmpty(strinbuilder) ? 0 : strinbuilder.Length, String.IsNullOrEmpty(strinbuilder) ? SqlString.Null : strinbuilder);
                        commandWrapper.AddInputParameter("@FileSize", SqlDbType.VarChar, String.IsNullOrEmpty(strinbuilder1) ? 0 : strinbuilder1.Length, String.IsNullOrEmpty(strinbuilder1) ? SqlString.Null : strinbuilder1);
                        commandWrapper.AddInputParameter("@Latitude", SqlDbType.VarChar, String.IsNullOrEmpty(item.Latitude) ? 0 : item.Latitude.Length, String.IsNullOrEmpty(item.Latitude) ? SqlString.Null : item.Latitude);
                        commandWrapper.AddInputParameter("@Longitude", SqlDbType.VarChar, String.IsNullOrEmpty(item.Longitude) ? 0 : item.Longitude.Length, String.IsNullOrEmpty(item.Longitude) ? SqlString.Null : item.Longitude);
                        commandWrapper.AddOutputParameter("@Propertyvalue", SqlDbType.Int);
                        db.ExecuteNonQuery(commandWrapper);
                        status = commandWrapper.GetValueInt("@Propertyvalue");
                    }
                }
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
            }

            return status;
        }


        public virtual bool DocInsert(int PropertyKey, string strinbuilder, string strinbuilder1)
        {
            bool status = false;

            try
            {

                string storedProcedure = "site_Document_Insert";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@PropertyKey", SqlDbType.Int, PropertyKey);
                        commandWrapper.AddInputParameter("@FileName", SqlDbType.VarChar, String.IsNullOrEmpty(strinbuilder) ? 0 : strinbuilder.Length, String.IsNullOrEmpty(strinbuilder) ? SqlString.Null : strinbuilder);
                        commandWrapper.AddInputParameter("@FileSize", SqlDbType.VarChar, String.IsNullOrEmpty(strinbuilder1) ? 0 : strinbuilder1.Length, String.IsNullOrEmpty(strinbuilder1) ? SqlString.Null : strinbuilder1);
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);
                        db.ExecuteNonQuery(commandWrapper);
                        status = (commandWrapper.GetValueInt("@errorCode") == 0);
                    }
                }
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
            }

            return status;
        }

        public virtual bool Updatemanager(PropertyModel item, int PropertyKey, string managername)
        {
            bool status = false;

            try
            {

                string storedProcedure = "site_Manger_Update";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters

                        commandWrapper.AddInputParameter("@PropertyKey", SqlDbType.Int, PropertyKey);
                        commandWrapper.AddInputParameter("@ResourceKey", SqlDbType.VarChar, String.IsNullOrEmpty(managername) ? 0 : managername.Length, String.IsNullOrEmpty(managername) ? SqlString.Null : managername);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        db.ExecuteNonQuery(commandWrapper);

                        status = (commandWrapper.GetValueInt("@errorCode") == 0);
                    }
                }
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
            }

            return status;
        }


        public IList<PropertyModel> GetAllCompany()
        {
            List<PropertyModel> itemList = new List<PropertyModel>();

            try
            {
                string storedProcedure = "site_Company_SelectAll";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            PropertyModel item = null;
                            while (dataReader.Read())
                            {
                                item = new PropertyModel();
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
        public IList<PropertyModel> GetAllState()
        {
            List<PropertyModel> itemList = new List<PropertyModel>();

            try
            {

                string storedProcedure = "site_State_SelectAll";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            PropertyModel item = null;
                            while (dataReader.Read())
                            {
                                item = new PropertyModel();
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
        public IList<PropertyModel> GetAllManager(int ResourceId, int PropertyKey, int CompanyKey)
        {
            List<PropertyModel> itemList = new List<PropertyModel>();

            try
            {

                string storedProcedure = "site_Manager_SelectAll";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        commandWrapper.AddInputParameter("@ResourceKey", SqlDbType.Int, ResourceId);
                        commandWrapper.AddInputParameter("@CompanyKey", SqlDbType.Int, CompanyKey);
                        commandWrapper.AddInputParameter("@PropertyKey", SqlDbType.Int, PropertyKey);
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            PropertyModel item = null;
                            while (dataReader.Read())
                            {
                                item = new PropertyModel();
                                GetAllManager(dataReader, item);
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


        public IList<PropertyModel> GetManagerForAdd(int ResourceId, int PropertyKey, int CompanyKey)
        {
            List<PropertyModel> itemList = new List<PropertyModel>();

            try
            {

                string storedProcedure = "site_Property_AddManger";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        commandWrapper.AddInputParameter("@ResourceKey", SqlDbType.Int, ResourceId);
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            PropertyModel item = null;
                            while (dataReader.Read())
                            {
                                item = new PropertyModel();
                                GetAllManager(dataReader, item);
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

        public IList<PropertyModel> GetAll(PropertyFilterModel filter)
        {
            throw new NotImplementedException();
        }
        public virtual bool PropertyEdit(PropertyModel item)
        {
            bool status = false;

            try
            {
                item.Status = 1;
                string storedProcedure = "site_Property_Update";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@PropertyKey", SqlDbType.Int, (item.PropertyKey == 0) ? SqlInt32.Null : item.PropertyKey);
                        commandWrapper.AddInputParameter("@CompanyKey", SqlDbType.Int, (item.CompanyKey == 0) ? SqlInt32.Null : item.CompanyKey);
                        commandWrapper.AddInputParameter("@Title", SqlDbType.VarChar, String.IsNullOrEmpty(item.Title) ? 0 : item.Title.Length, String.IsNullOrEmpty(item.Title) ? SqlString.Null : item.Title);
                        commandWrapper.AddInputParameter("@NumberOfUnits", SqlDbType.Int, (item.NumberOfUnits == 0) ? SqlInt32.Null : item.NumberOfUnits);
                        commandWrapper.AddInputParameter("@Address", SqlDbType.VarChar, String.IsNullOrEmpty(item.Address) ? 0 : item.Address.Length, String.IsNullOrEmpty(item.Address) ? SqlString.Null : item.Address);
                        commandWrapper.AddInputParameter("@Address2", SqlDbType.VarChar, String.IsNullOrEmpty(item.Address2) ? 0 : item.Address2.Length, String.IsNullOrEmpty(item.Address2) ? SqlString.Null : item.Address2);
                        commandWrapper.AddInputParameter("@City", SqlDbType.VarChar, String.IsNullOrEmpty(item.City) ? 0 : item.City.Length, String.IsNullOrEmpty(item.City) ? SqlString.Null : item.City);
                        commandWrapper.AddInputParameter("@State", SqlDbType.VarChar, String.IsNullOrEmpty(item.State) ? 0 : item.State.Length, String.IsNullOrEmpty(item.State) ? SqlString.Null : item.State);
                        commandWrapper.AddInputParameter("@Zip", SqlDbType.VarChar, String.IsNullOrEmpty(item.Zip) ? 0 : item.Zip.Length, String.IsNullOrEmpty(item.Zip) ? SqlString.Null : item.Zip);
                        commandWrapper.AddInputParameter("@BidRequestAmount", SqlDbType.Money, (item.BidRequestAmount == 0) ? SqlMoney.Null : item.BidRequestAmount);
                        commandWrapper.AddInputParameter("@MinimumInsuranceAmount", SqlDbType.Money, (item.MinimumInsuranceAmount == 0) ? SqlMoney.Null : item.MinimumInsuranceAmount);
                        commandWrapper.AddInputParameter("@Description", SqlDbType.VarChar, String.IsNullOrEmpty(item.Description) ? 0 : item.Description.Length, String.IsNullOrEmpty(item.Description) ? SqlString.Null : item.Description);
                        commandWrapper.AddInputParameter("@Status", SqlDbType.Int, (item.Status == 0) ? SqlInt32.Null : item.Status);
                        commandWrapper.AddInputParameter("@Latitude", SqlDbType.VarChar, String.IsNullOrEmpty(item.Latitude) ? 0 : item.Latitude.Length, String.IsNullOrEmpty(item.Latitude) ? SqlString.Null : item.Latitude);
                        commandWrapper.AddInputParameter("@Longitude", SqlDbType.VarChar, String.IsNullOrEmpty(item.Longitude) ? 0 : item.Longitude.Length, String.IsNullOrEmpty(item.Longitude) ? SqlString.Null : item.Longitude);
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
        public IList<PropertyModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public virtual bool Remove(int id)
        {
            bool status = false;

            try
            {
                string storedProcedure = "site_Property_Delete";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@PropertyKey", SqlDbType.Int, id);

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

        public virtual bool DocumentDelete(int PropertyKey, string Docname)
        {
            bool status = false;

            try
            {
                string storedProcedure = "site_Document_Delete";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@PropertyKey", SqlDbType.Int, PropertyKey);
                        commandWrapper.AddInputParameter("@Docname", SqlDbType.VarChar, Docname.Trim());

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


        public virtual bool ManagerDelete(int PropertyKey, string ResourceKey)
        {
            bool status = false;

            try
            {
                string storedProcedure = "site_Manager_Delete";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@PropertyKey", SqlDbType.Int, PropertyKey);
                        commandWrapper.AddInputParameter("@ResourceKey", SqlDbType.VarChar, ResourceKey);

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


        public virtual PropertyModel checkmanager(string managername)
        {
            PropertyModel item = null;

            try
            {
                string storedProcedure = "site_GetGroupkey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@ResourceKey", SqlDbType.VarChar, managername);


                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {

                            while (dataReader.Read())
                            {
                                item = new PropertyModel();
                                GetResorceKey(dataReader, item);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
            }

            return item;
        }


        public IList<PropertyModel> GetPropertyManagerToAddProperty(int ResourceId, int CompanyKey)
        {
            List<PropertyModel> itemList = new List<PropertyModel>();

            try
            {

                string storedProcedure = "site_Manager_GetPropertyManagerToAddProperty";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        commandWrapper.AddInputParameter("@ResourceKey", SqlDbType.Int, ResourceId);
                        commandWrapper.AddInputParameter("@CompanyKey", SqlDbType.Int, CompanyKey);
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            PropertyModel item = null;
                            while (dataReader.Read())
                            {
                                item = new PropertyModel();
                                GetAllManager(dataReader, item);
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

        //public virtual checkmanager(string managername)
        //{
        //    //int status = 0;
        //    List<PropertyModel> itemList = new List<PropertyModel>();
        //    try
        //    {
        //        string storedProcedure = "site_GetGroupkey";
        //        using (Database db = new Database(ConnectionString))
        //        {
        //            DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure);
        //            // add the stored procedure input parameters
        //            commandWrapper.AddInputParameter("@ResourceKey", SqlDbType.VarChar, managername);


        //            using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
        //            {
        //                PropertyModel item = null;
        //                while (dataReader.Read())
        //                {
        //                    item = new PropertyModel();
        //                    GetResorceKey(dataReader, item);
        //                    itemList.Add(item);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // error occured...

        //    }

        //    //return itemList;
        //}

    }

}
