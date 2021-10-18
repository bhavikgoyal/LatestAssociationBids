using AssociationBids.GlobalUtilities;
using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base.Interface;
using DB_con;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AssociationBids.Portal.Repository.Base.Code
{
    public class VendorManagerRepository : BaseRepository, IVendorManagerRepository
    {
        public VendorManagerRepository() { }

        public VendorManagerRepository(string connectionString)
         : base(connectionString) { }


        public List<VendorManagerVendorModel> SearchVendor(long PageSize, long PageIndex, string Search, string Sort)
        {
            List<VendorManagerVendorModel> itemList = new List<VendorManagerVendorModel>();
            try
            {
                string storedProcedure = "site_VendorManager_SelectIndexPaging";
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
                            VendorManagerVendorModel item = null;
                            while (dataReader.Read())
                            {
                                item = new VendorManagerVendorModel();
                                LoadVendor(dataReader, item);
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

        public List<VendorManagerVendorModel> SearchApprovedVendor(long PageSize, long PageIndex, string Search, string Sort)
        {
            List<VendorManagerVendorModel> itemList = new List<VendorManagerVendorModel>();
            try
            {
                string storedProcedure = "site_VendorManager_Approved_SelectIndexPaging";
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
                            VendorManagerVendorModel item = null;
                            while (dataReader.Read())
                            {
                                item = new VendorManagerVendorModel();
                                LoadVendor(dataReader, item);
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
        public List<VendorManagerVendorModel> SearchUnapprovedVendor(long PageSize, long PageIndex, string Search, string Sort,long ResourceKey, string Duplicate)
        {
            List<VendorManagerVendorModel> itemList = new List<VendorManagerVendorModel>();
            try
            {
                string storedProcedure = "site_VendorManager_Unapproved_SelectIndexPaging";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters 
                        commandWrapper.AddInputParameter("@ResourceKey", SqlDbType.BigInt, ResourceKey);
                        commandWrapper.AddInputParameter("@PageSize", SqlDbType.BigInt, (PageSize == 0) ? 0 : PageSize);
                        commandWrapper.AddInputParameter("@PageIndex", SqlDbType.BigInt, (PageIndex == 0) ? 0 : PageIndex);
                        commandWrapper.AddInputParameter("@Search", SqlDbType.NText, (Search == "") ? "" : Search);
                        commandWrapper.AddInputParameter("@Sort", SqlDbType.NText, (Sort == "") ? "" : Sort);
                        commandWrapper.AddInputParameter("@Duplicate", SqlDbType.VarChar, String.IsNullOrEmpty(Duplicate) ? 0 : Duplicate.Length, String.IsNullOrEmpty(Duplicate) ? SqlString.Null : Duplicate);
                            
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            VendorManagerVendorModel item = null;
                            while (dataReader.Read())
                            {
                                item = new VendorManagerVendorModel();
                                LoadVendor(dataReader, item);
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

        public List<VendorManagerVendorModel> SearchUnapprovedVendorPriority(long PageSize, long PageIndex, string Search, string Sort,long ResourceKey, string Duplicate)
        {
            List<VendorManagerVendorModel> itemList = new List<VendorManagerVendorModel>();
            try
            {
                string storedProcedure = "site_VendorManager_Unapproved_SelectIndexPagingPriority";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters 
                        commandWrapper.AddInputParameter("@ResourceKey", SqlDbType.BigInt, ResourceKey);
                        commandWrapper.AddInputParameter("@PageSize", SqlDbType.BigInt, (PageSize == 0) ? 0 : PageSize);
                        commandWrapper.AddInputParameter("@PageIndex", SqlDbType.BigInt, (PageIndex == 0) ? 0 : PageIndex);
                        commandWrapper.AddInputParameter("@Search", SqlDbType.NText, (Search == "") ? "" : Search);
                        commandWrapper.AddInputParameter("@Sort", SqlDbType.NText, (Sort == "") ? "" : Sort);
                        commandWrapper.AddInputParameter("@Duplicate", SqlDbType.VarChar, String.IsNullOrEmpty(Duplicate) ? 0 : Duplicate.Length, String.IsNullOrEmpty(Duplicate) ? SqlString.Null : Duplicate);


                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            VendorManagerVendorModel item = null;
                            while (dataReader.Read())
                            {
                                item = new VendorManagerVendorModel();
                                LoadVendor(dataReader, item);
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
        public virtual IList<BidRequestModel> BidRequestsIndexPagingByCompanyKey(int CompanyKey, int index, int pageSize, string Search, string sort)
        {
            List<BidRequestModel> itemList = new List<BidRequestModel>();

            try
            {
                string storedProcedure = "site_VendorManager_BidReq_SelectIndexPaging";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters 
                        commandWrapper.AddInputParameter("@companyKey", SqlDbType.Int, (CompanyKey == 0) ? SqlInt32.Null : CompanyKey);
                        commandWrapper.AddInputParameter("@PageSize", SqlDbType.Int, (pageSize == 0) ? 0 : pageSize);
                        commandWrapper.AddInputParameter("@PageIndex", SqlDbType.Int, (index == 0) ? 0 : index);
                        commandWrapper.AddInputParameter("@Search", SqlDbType.NText, (Search == "") ? "" : Search);
                        commandWrapper.AddInputParameter("@Sort", SqlDbType.NText, (sort == "") ? "" : sort);
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            BidRequestModel item = null;
                            while (dataReader.Read())
                            {
                                item = new BidRequestModel();
                                LoadViewEditBidRequest(dataReader, item);
                                itemList.Add(item);
                            }
                        }


                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: site_VendorManager_BidRequest_SelectPaging");
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

        public virtual IList<BidRequestModel> WordOrderIndexPagingByCompanyKey(int CompanyKey, int index, int pageSize, string Search, string sort)
        {
            List<BidRequestModel> itemList = new List<BidRequestModel>();

            try
            {
                string storedProcedure = "site_VendorManager_WorkOrder_SelectIndexPaging";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters 
                        commandWrapper.AddInputParameter("@companyKey", SqlDbType.Int, (CompanyKey == 0) ? SqlInt32.Null : CompanyKey);
                        commandWrapper.AddInputParameter("@PageSize", SqlDbType.Int, (pageSize == 0) ? 0 : pageSize);
                        commandWrapper.AddInputParameter("@PageIndex", SqlDbType.Int, (index == 0) ? 0 : index);
                        commandWrapper.AddInputParameter("@Search", SqlDbType.NText, (Search == "") ? "" : Search);
                        commandWrapper.AddInputParameter("@Sort", SqlDbType.NText, (sort == "") ? "" : sort);
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            BidRequestModel item = null;
                            while (dataReader.Read())
                            {
                                item = new BidRequestModel();
                                LoadViewEditBidRequest(dataReader, item);
                                itemList.Add(item);
                            }
                        }


                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: site_VendorManager_WorkOrder_SelectPaging");
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

        public List<VendorManagerVendorModel> SearchPendingVendor(long PageSize, long PageIndex, string Search, string Sort)
        {
            List<VendorManagerVendorModel> itemList = new List<VendorManagerVendorModel>();
            try
            {
                string storedProcedure = "site_VendorManager_Pending_SelectIndexPaging";
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
                            VendorManagerVendorModel item = null;
                            while (dataReader.Read())
                            {
                                item = new VendorManagerVendorModel();
                                LoadVendor(dataReader, item);
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

        public IList<VendorManagerVendorModel> GetAllService()
        {
            List<VendorManagerVendorModel> itemList = new List<VendorManagerVendorModel>();

            try
            {

                string storedProcedure = "site_Service_GetAll";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            VendorManagerVendorModel item = null;
                            while (dataReader.Read())
                            {
                                item = new VendorManagerVendorModel();
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


        public IList<VendorManagerVendorModel> AppoGetAllService(string PleaseSelect)
        {
            List<VendorManagerVendorModel> itemList = new List<VendorManagerVendorModel>();

            try
            {

                string storedProcedure = "site_Service_GetAll";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            VendorManagerVendorModel item = null;
                            while (dataReader.Read())
                            {
                                item = new VendorManagerVendorModel();
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
        public IList<VendorManagerVendorModel> GetAllState()
        {
            List<VendorManagerVendorModel> itemList = new List<VendorManagerVendorModel>();

            try
            {

                string storedProcedure = "site_State_SelectAll";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            VendorManagerVendorModel item = null;
                            while (dataReader.Read())
                            {
                                item = new VendorManagerVendorModel();
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

        protected void GetAllState(DBDataReader dataReader, VendorManagerVendorModel item)
        {
            item.StateKey = dataReader.GetValueText("StateKey");
            item.State = dataReader.GetValueText("Title");
        }

        protected void GetAllService(DBDataReader dataReader, VendorManagerVendorModel item)
        {
            item.ServiceKey = dataReader.GetValueInt("ServiceKey");
            item.ServiceTitle1 = dataReader.GetValueText("Title");

        }


        public long VendorManagerEdit(VendorManagerVendorModel item)
        {
            Int64 status = 0;

            try
            {

                string storedProcedure = "site_VendorManager_Update";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@Radius", SqlDbType.Int, (item.Radius == 0) ? SqlInt32.Null : item.Radius);
                        commandWrapper.AddInputParameter("@companyKey", SqlDbType.Int, (item.CompanyKey == 0) ? SqlInt32.Null : item.CompanyKey);
                        commandWrapper.AddInputParameter("@companyName ", SqlDbType.VarChar, String.IsNullOrEmpty(item.CompanyName) ? 0 : item.CompanyName.Length, String.IsNullOrEmpty(item.CompanyName) ? SqlString.Null : item.CompanyName);
                        commandWrapper.AddInputParameter("@Address", SqlDbType.VarChar, String.IsNullOrEmpty(item.Address) ? 0 : item.Address.Length, String.IsNullOrEmpty(item.Address) ? SqlString.Null : item.Address);
                        commandWrapper.AddInputParameter("@Address2", SqlDbType.VarChar, String.IsNullOrEmpty(item.Address2) ? 0 : item.Address2.Length, String.IsNullOrEmpty(item.Address2) ? SqlString.Null : item.Address2);
                        commandWrapper.AddInputParameter("@City", SqlDbType.VarChar, String.IsNullOrEmpty(item.City) ? 0 : item.City.Length, String.IsNullOrEmpty(item.City) ? SqlString.Null : item.City);
                        commandWrapper.AddInputParameter("@StateKey", SqlDbType.VarChar, String.IsNullOrEmpty(item.State) ? 0 : item.State.Length, String.IsNullOrEmpty(item.State) ? SqlString.Null : item.State);
                        commandWrapper.AddInputParameter("@Zip", SqlDbType.VarChar, String.IsNullOrEmpty(item.Zip) ? 0 : item.Zip.Length, String.IsNullOrEmpty(item.Zip) ? SqlString.Null : item.Zip);
                        commandWrapper.AddInputParameter("@Work", SqlDbType.VarChar, String.IsNullOrEmpty(item.Work) ? 0 : item.Work.Length, String.IsNullOrEmpty(item.Work) ? SqlString.Null : item.Work);
                        commandWrapper.AddInputParameter("@Work2", SqlDbType.VarChar, String.IsNullOrEmpty(item.Work2) ? 0 : item.Work.Length, String.IsNullOrEmpty(item.Work2) ? SqlString.Null : item.Work2);
                        commandWrapper.AddInputParameter("@Fax", SqlDbType.VarChar, String.IsNullOrEmpty(item.Fax) ? 0 : item.Fax.Length, String.IsNullOrEmpty(item.Fax) ? SqlString.Null : item.Fax);
                        commandWrapper.AddInputParameter("@Website", SqlDbType.VarChar, String.IsNullOrEmpty(item.Website) ? 0 : item.Website.Length, String.IsNullOrEmpty(item.Website) ? SqlString.Null : item.Website);

                        commandWrapper.AddInputParameter("@email", SqlDbType.VarChar, String.IsNullOrEmpty(item.Email) ? 0 : item.Email.Length, String.IsNullOrEmpty(item.Email) ? SqlString.Null : item.Email);
                        commandWrapper.AddInputParameter("@Status", SqlDbType.Int, (Convert.ToInt32(item.Status) == 0) ? SqlInt32.Null : Convert.ToInt32(item.Status));

                        db.ExecuteNonQuery(commandWrapper);
                        //status = commandWrapper.GetValueInt("@ResourceValue");

                        return status;
                    }
                }
            }
            catch (Exception ex)
            {
                // error occured...
                return -1;
                //throw;
            }
        }

        public long VendorManagerServiceDelete(int CompanyKey,int VendorServiceKey)
        {
            Int64 status = 0;

            try
            {
                string storedProcedure = "site_VendorManager_Service_Delete";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@CompanyKey", SqlDbType.Int, (CompanyKey == 0) ? SqlInt32.Null : CompanyKey);
                        commandWrapper.AddInputParameter("@VendorServiceKey ", SqlDbType.Int, (VendorServiceKey == 0) ? SqlInt32.Null : VendorServiceKey);

                        db.ExecuteNonQuery(commandWrapper);
                        //status = commandWrapper.GetValueInt("@ResourceValue");

                        return status;
                    }
                }
            }
            catch (Exception ex)
            {
                // error occured...e
                throw;
            }
        }

        public IList<VendorManagerVendorModel> GetServiceByCompany(int CompanyKey)
        {
            List<VendorManagerVendorModel> itemList = new List<VendorManagerVendorModel>();

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
                            VendorManagerVendorModel item = null;
                            while (dataReader.Read())
                            {
                                item = new VendorManagerVendorModel();
                                LoadViewEdit(dataReader, item);
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




        public virtual VendorManagerVendorModel GetVendorByCompanyKey(int CompanyKey)
        {
            VendorManagerVendorModel item = null;

            try
            {
                string storedProcedure = "site_vendorManager_SelectOneByvendorKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@CompanyKey", SqlDbType.Int, CompanyKey);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            while (dataReader.Read())
                            {
                                item = new VendorManagerVendorModel();

                                LoadViewEdit(dataReader, item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return item;
        }

        public virtual VendorManagerVendorModel GetVendorByCompanyKeyForInviteView(int CompanyKey)
        {
            VendorManagerVendorModel item = null;

            try
            {
                string storedProcedure = "site_vendorManager_SelectOneByvendorKeyForInviteView";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@CompanyKey", SqlDbType.Int, CompanyKey);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            while (dataReader.Read())
                            {
                                item = new VendorManagerVendorModel();

                                LoadViewEditForInviteView(dataReader, item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return item;
        }

        public virtual VendorManagerVendorModel GetUnapprovedVendorByCompanyKey(int CompanyKey)
        {
            VendorManagerVendorModel item = null;

            try
            {
                string storedProcedure = "site_vendorManager_Unapproved_SelectOneByvendorKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@CompanyKey", SqlDbType.Int, CompanyKey);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            while (dataReader.Read())
                            {
                                item = new VendorManagerVendorModel();

                                LoadViewEditUnapprovedVendor(dataReader, item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return item;
        }

        public virtual ResourceModel GetResourceByCompanyKey(int CompanyKey)
        {
            ResourceModel item = null;

            try
            {
                string storedProcedure = "site_VendorManager_Resource_SelectOneByCompanyKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@CompanyKey", SqlDbType.Int, CompanyKey);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            while (dataReader.Read())
                            {
                                item = new ResourceModel();

                                LoadViewEditResource(dataReader, item);
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
        public virtual ResourceModel GetResourceForInviteVendor(int CompanyKey)
        {
            ResourceModel item = null;

            try
            {
                string storedProcedure = "site_VendorManager_Resource_SelectOneForInvite";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@CompanyKey", SqlDbType.Int, CompanyKey);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            while (dataReader.Read())
                            {
                                item = new ResourceModel();

                                LoadViewEditResource(dataReader, item);
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

        public virtual ResourceModel GetResourceByCompanyKeyForNotification(int CompanyKey)
        {
            ResourceModel item = null;

            try
            {
                string storedProcedure = "site_VendorManager_Resource_SelectByCompanyKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@CompanyKey", SqlDbType.Int, CompanyKey);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            while (dataReader.Read())
                            {
                                item = new ResourceModel();

                                LoadViewEditResource(dataReader, item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: site_VendorManager_Resource_SelectByCompanyKey");
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


        protected void LoadViewEdit(DBDataReader dataReader, VendorManagerVendorModel item)
        {
            item.CompanyKey = dataReader.GetValueInt("CompanyKey");
            item.CompanyName = dataReader.GetValueText("CompanyName");
            item.Name = dataReader.GetValueText("Name");
            item.Fax = dataReader.GetValueText("Fax");
            item.Address = dataReader.GetValueText("Address");
            item.Address2 = dataReader.GetValueText("Address2");
            item.City = dataReader.GetValueText("City");
            item.State = dataReader.GetValueText("State");
            item.Zip = dataReader.GetValueText("Zip");
            item.Website = dataReader.GetValueText("Website");
            item.CellPhone = dataReader.GetValueText("CellPhone");
            item.Work = dataReader.GetValueText("Work");
            item.Work2 = dataReader.GetValueText("Work2");
            item.Email = dataReader.GetValueText("Email");          
            item.ServiceKey = dataReader.GetValueInt("ServiceKey");
            item.ServiceKey1 = dataReader.GetValueInt("ServiceKey");
            item.ServiceKey2 = dataReader.GetValueInt("ServiceKey");
            item.Radius= dataReader.GetValueInt("radius");

        }

        protected void LoadViewEditForInviteView(DBDataReader dataReader, VendorManagerVendorModel item)
        {
            item.CompanyKey = dataReader.GetValueInt("CompanyKey");
            item.CompanyName = dataReader.GetValueText("CompanyName");
            item.Name = dataReader.GetValueText("Name");
            item.Fax = dataReader.GetValueText("Fax");
            item.Address = dataReader.GetValueText("Address");
            item.Address2 = dataReader.GetValueText("Address2");
            item.City = dataReader.GetValueText("City");
            item.State = dataReader.GetValueText("State");
            item.Zip = dataReader.GetValueText("Zip");
            item.Website = dataReader.GetValueText("Website");
            item.CellPhone = dataReader.GetValueText("CellPhone");
            item.Work = dataReader.GetValueText("Work");
            item.Work2 = dataReader.GetValueText("Work2");
            item.Email = dataReader.GetValueText("Email");

        }

        protected void LoadViewEditUnapprovedVendor(DBDataReader dataReader, VendorManagerVendorModel item)
        {
            item.CompanyKey = dataReader.GetValueInt("CompanyKey");
            item.CompanyName = dataReader.GetValueText("CompanyName");
            item.Name = dataReader.GetValueText("Name");
            item.Fax = dataReader.GetValueText("Fax");
            item.Address = dataReader.GetValueText("Address");
            item.Address2 = dataReader.GetValueText("Address2");
            item.City = dataReader.GetValueText("City");
            item.State = dataReader.GetValueText("State");
            item.Zip = dataReader.GetValueText("Zip");
            item.Website = dataReader.GetValueText("Website");
            item.CellPhone = dataReader.GetValueText("CellPhone");
            item.Work = dataReader.GetValueText("Work");
            item.Work2 = dataReader.GetValueText("Work2");
            item.Email = dataReader.GetValueText("Email");
            item.Favorite = dataReader.GetValueText("Favorite");
            item.Radius= dataReader.GetValueInt("radius");

        }

        protected void LoadViewEditBidRequest(DBDataReader dataReader, BidRequestModel item)
        {
            item.BidRequestKey = dataReader.GetValueInt("BidRequestKey");
            item.CompanyKey = dataReader.GetValueInt("PropertyKey");
            item.ResourceKey = dataReader.GetValueInt("ResourceKey");
            item.ServiceKey = dataReader.GetValueInt("ServiceKey");
            item.Title = dataReader.GetValueText("Title");
            item.BidDueDate = dataReader.GetValueDateTime("BidDueDate");
            item.BidDueDates = item.BidDueDate.ToString("MM/dd/yyyy");
  

            item.StartDate = dataReader.GetValueDateTime("StartDate");
            item.StartDates = item.StartDate.ToString("MM/dd/yyyy");
            item.EndDate = dataReader.GetValueDateTime("EndDate");
            item.EndDates = item.EndDate.ToString("MM/dd/yyyy");
            item.Description = dataReader.GetValueText("Description");
            item.DateAdded = dataReader.GetValueDateTime("DateAdded");

            item.DateAddeds = item.DateAdded.ToString("MM/dd/yyyy");
            item.LastModificationTime = dataReader.GetValueDateTime("LastModificationTime");
            item.BidRequestStatus = dataReader.GetValueInt("BidRequestStatus");
            item.BidReqStatus = dataReader.GetValueText("BidStatus");
            item.Property = dataReader.GetValueText("Propertyname");
            item.NoofBids = dataReader.GetValueInt("NumberOfUnits");
        }
        protected void LoadViewEditResource(DBDataReader dataReader, ResourceModel item)
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
            item.Username = dataReader.GetValueText("Username");
            item.Company = dataReader.GetValueText("CompanyName");
            item.StateKey = dataReader.GetValueText("StateName");
            item.AccountLocked = dataReader.GetValueBool("AccountLocked");
            try
            {
                item.MaskedCCNumber = dataReader.GetValueText("MaskedCCNumber");
            }
            catch { }
            try
            {
                item.Radius = dataReader.GetValueInt("radius");
            }
            catch { }


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

        public long VendorManagerAddInsurance(InsuranceModel item)
        {
            long id = 0;
            try
            {
                using (Database db = new Database(ConnectionString))
                {
                    //DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper("gensp_Insurance_InsertOne");
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper("gensp_Insurance_InsertOne"))
                    {
                        commandWrapper.AddInputParameter("@vendorKey", SqlDbType.Int, (item.VendorKey == 0) ? SqlInt32.Null : item.VendorKey);
                        commandWrapper.AddInputParameter("@companyName ", SqlDbType.VarChar, String.IsNullOrEmpty(item.CompanyName) ? 0 : item.CompanyName.Length, String.IsNullOrEmpty(item.CompanyName) ? SqlString.Null : item.CompanyName);
                        commandWrapper.AddInputParameter("@policyNumber ", SqlDbType.VarChar, String.IsNullOrEmpty(item.PolicyNumber) ? 0 : item.PolicyNumber.Length, String.IsNullOrEmpty(item.PolicyNumber) ? SqlString.Null : item.PolicyNumber);
                        commandWrapper.AddInputParameter("@insuranceAmount", SqlDbType.Money, (item.InsuranceAmount == 0) ? 0 : item.InsuranceAmount);
                        commandWrapper.AddInputParameter("@agentName", SqlDbType.VarChar, String.IsNullOrEmpty(item.AgentName) ? 0 : item.AgentName.Length, String.IsNullOrEmpty(item.AgentName) ? SqlString.Null : item.AgentName);
                        commandWrapper.AddInputParameter("@email", SqlDbType.VarChar, String.IsNullOrEmpty(item.Email) ? 0 : item.Email.Length, String.IsNullOrEmpty(item.Email) ? SqlString.Null : item.Email);
                        commandWrapper.AddInputParameter("@Work", SqlDbType.VarChar, String.IsNullOrEmpty(item.Work) ? 0 : item.Work.Length, String.IsNullOrEmpty(item.Work) ? SqlString.Null : item.Work);
                        commandWrapper.AddInputParameter("@cellphone", SqlDbType.VarChar, String.IsNullOrEmpty(item.CellPhone) ? 0 : item.CellPhone.Length, String.IsNullOrEmpty(item.CellPhone) ? SqlString.Null : item.CellPhone);
                        commandWrapper.AddInputParameter("@Fax", SqlDbType.VarChar, String.IsNullOrEmpty(item.Fax) ? 0 : item.Fax.Length, String.IsNullOrEmpty(item.Fax) ? SqlString.Null : item.Fax);
                        commandWrapper.AddInputParameter("@Address", SqlDbType.VarChar, String.IsNullOrEmpty(item.Address) ? 0 : item.Address.Length, String.IsNullOrEmpty(item.Address) ? SqlString.Null : item.Address);
                        commandWrapper.AddInputParameter("@Address2", SqlDbType.VarChar, String.IsNullOrEmpty(item.Address2) ? 0 : item.Address2.Length, String.IsNullOrEmpty(item.Address2) ? SqlString.Null : item.Address2);
                        commandWrapper.AddInputParameter("@City", SqlDbType.VarChar, String.IsNullOrEmpty(item.City) ? 0 : item.City.Length, String.IsNullOrEmpty(item.City) ? SqlString.Null : item.City);
                        commandWrapper.AddInputParameter("@State", SqlDbType.VarChar, String.IsNullOrEmpty(item.State) ? 0 : item.State.Length, String.IsNullOrEmpty(item.State) ? SqlString.Null : item.State);
                        commandWrapper.AddInputParameter("@Zip", SqlDbType.VarChar, String.IsNullOrEmpty(item.Zip) ? 0 : item.Zip.Length, String.IsNullOrEmpty(item.Zip) ? SqlString.Null : item.Zip);

                        commandWrapper.AddInputParameter("@startDate", SqlDbType.DateTime, item.StartDate);

                        commandWrapper.AddInputParameter("@endDate", SqlDbType.DateTime, item.EndDate);
                        commandWrapper.AddInputParameter("@renewalDate", SqlDbType.DateTime, item.RenewalDate.Year < 2000 ? SqlDateTime.Null : item.RenewalDate);

                        commandWrapper.AddInputParameter("@Status", SqlDbType.Int, 1);
                        commandWrapper.AddOutputParameter("@insuranceKey", SqlDbType.Int);
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        id = db.ExecuteNonQuery(commandWrapper);
                        id = commandWrapper.GetValueInt("@insuranceKey");
                        if (id <= 0 || commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            id = 0;
                        }

                    }
                }
            }
            catch(Exception ex) {

            }
            return id;
        }
        public IList<VendorManagerModel> GetbindDocument(int CompanyKey)
        {
            List<VendorManagerModel> itemList = new List<VendorManagerModel>();

            try
            {

                string storedProcedure = "site_VendorManagerDocument_SelectAllByCompanyKeyV2";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        commandWrapper.AddInputParameter("@CompanyKey", SqlDbType.Int, CompanyKey);
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            VendorManagerModel item = null;
                            while (dataReader.Read())
                            {
                                item = new VendorManagerModel();
                                item.Document = new DocumentModel();
                                item.Insurance = new InsuranceModel();
                                item.Resource = new ResourceModel();
                                item.ServiceModel = new VendorServiceModel();
                                item.Vendor = new VendorManagerVendorModel();
                                GetbindDocument(dataReader, item);
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

        public IList<VendorManagerModel> GetbindDocumentByInsuranceKey(int CompanyKey, int InsuranceKey)
        {
            List<VendorManagerModel> itemList = new List<VendorManagerModel>();

            try
            {

                string storedProcedure = "site_VendorManagerDocument_SelectByCompanyAndInsuranceKeyV2";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        commandWrapper.AddInputParameter("@CompanyKey", SqlDbType.Int, CompanyKey);
                        commandWrapper.AddInputParameter("@InsuranceKey", SqlDbType.Int, InsuranceKey);
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            VendorManagerModel item = null;
                            while (dataReader.Read())
                            {
                                item = new VendorManagerModel();
                                item.Document = new DocumentModel();
                                item.Insurance = new InsuranceModel();
                                item.Resource = new ResourceModel();
                                item.ServiceModel = new VendorServiceModel();
                                item.Vendor = new VendorManagerVendorModel();
                                GetbindDocument(dataReader, item);
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

        public bool VendorManagerRemoveInsurance(int InsuranceKey)
        {
            bool status = false;

            try
            {
                string storedProcedure = "gensp_Insurance_DeleteOneByInsuranceKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@InsuranceKey", SqlDbType.Int, (InsuranceKey == 0) ? SqlInt32.Null : InsuranceKey);
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);
                        int a = db.ExecuteNonQuery(commandWrapper);
                        //status = commandWrapper.GetValueInt("@ResourceValue");
                        if (a == -1)
                            return true;

                        return status;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }
        
        public bool VendorManagerRemoveDocument(int DocumentKey)
        {
            try
            {
                string storedProcedure = "site_VendorManagerDocument_Delete";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@DocumentKey ", SqlDbType.Int, (DocumentKey == 0) ? SqlInt32.Null : DocumentKey);
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);
                        int a = db.ExecuteNonQuery(commandWrapper);
                        //status = commandWrapper.GetValueInt("@ResourceValue");
                        if (a == -1)
                            return true;

                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<VendorManagerVendorModel> VendorManager_GetAllManagementCompany()
        {
            List<VendorManagerVendorModel> companyList = new List<VendorManagerVendorModel>();

            try
            {
                string storedProcedure = "_site_VendorManager_GetAll_CompanyManagement";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        var reader = db.ExecuteReader(commandWrapper);

                        while (reader.Read())
                        {
                            VendorManagerVendorModel vendor = new VendorManagerVendorModel();
                            LoadManagementCompany(reader, vendor);
                            companyList.Add(vendor);
                        }
                    }
                }

            }
                catch(Exception ex)
            {

            }

            return companyList;
        }

        protected void LoadManagementCompany(DBDataReader reader,VendorManagerVendorModel item)
        {
            item.CompanyKey = reader.GetValueInt("CompanyKey");
            item.CompanyName = reader.GetValueText("CompanyName");
            item.Status = reader.GetValueInt("Status");
        }

        public Int64 VendorManagerInviteVendor(VendorManagerVendorModel item,  int ResourceKey)
        {
            int status = 0;
            item.Password = System.Configuration.ConfigurationManager.AppSettings["Password"];
            try
            {
                try
                {
                    if (item.ContactPerson != null)
                    {
                        var a = item.ContactPerson.Split(' ')[1];
                    }
                    else
                    {
                        item.ContactPerson += " ";
                    }
                }
                catch
                {
                    item.ContactPerson += " ";
                }
                string storedProcedure = "site_VendorManager_InviteVendor_New";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        
                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@Resource", SqlDbType.Int, ResourceKey);
                        commandWrapper.AddInputParameter("@CompanyName ", SqlDbType.VarChar, String.IsNullOrEmpty(item.LegalName) ? 0 : item.LegalName.Length, String.IsNullOrEmpty(item.LegalName) ? SqlString.Null : item.LegalName);
                        commandWrapper.AddInputParameter("@Address", SqlDbType.VarChar, String.IsNullOrEmpty(item.Address) ? 0 : item.Address.Length, String.IsNullOrEmpty(item.Address) ? SqlString.Null : item.Address);
                        commandWrapper.AddInputParameter("@Address2", SqlDbType.VarChar, String.IsNullOrEmpty(item.Address2) ? 0 : item.Address2.Length, String.IsNullOrEmpty(item.Address2) ? SqlString.Null : item.Address2);
                        commandWrapper.AddInputParameter("@City", SqlDbType.VarChar, String.IsNullOrEmpty(item.City) ? 0 : item.City.Length, String.IsNullOrEmpty(item.City) ? SqlString.Null : item.City);
                        commandWrapper.AddInputParameter("@StateKey", SqlDbType.VarChar, String.IsNullOrEmpty(item.State) ? 0 : item.State.Length, String.IsNullOrEmpty(item.State) ? SqlString.Null : item.State);
                        commandWrapper.AddInputParameter("@Zip", SqlDbType.VarChar, String.IsNullOrEmpty(item.Zip) ? 0 : item.Zip.Length, String.IsNullOrEmpty(item.Zip) ? SqlString.Null : item.Zip);
                        commandWrapper.AddInputParameter("@FirstName", SqlDbType.VarChar, String.IsNullOrEmpty(item.ContactPerson.Split(' ')[0]) ? 0 : item.ContactPerson.Split(' ')[0].Length, String.IsNullOrEmpty(item.ContactPerson.Split(' ')[0]) ? SqlString.Null : item.ContactPerson.Split(' ')[0]);
                        commandWrapper.AddInputParameter("@LastName", SqlDbType.VarChar, String.IsNullOrEmpty(item.ContactPerson.Split(' ')[1]) ? 0 : item.ContactPerson.Split(' ')[1].Length, String.IsNullOrEmpty(item.ContactPerson.Split(' ')[1]) ? SqlString.Null : item.ContactPerson.Split(' ')[1]);
                        commandWrapper.AddInputParameter("@Work", SqlDbType.VarChar, String.IsNullOrEmpty(item.Work) ? 0 : item.Work.Length, String.IsNullOrEmpty(item.Work) ? SqlString.Null : item.Work);
                        commandWrapper.AddInputParameter("@Email", SqlDbType.VarChar, String.IsNullOrEmpty(item.Email) ? 0 : item.Email.Length, String.IsNullOrEmpty(item.Email) ? SqlString.Null : item.Email);
                        commandWrapper.AddInputParameter("@Fax", SqlDbType.VarChar, String.IsNullOrEmpty(item.Fax) ? 0 : item.Fax.Length, String.IsNullOrEmpty(item.Fax) ? SqlString.Null : item.Fax);
                        commandWrapper.AddInputParameter("@Website", SqlDbType.VarChar, String.IsNullOrEmpty(item.Website) ? 0 : item.Website.Length, String.IsNullOrEmpty(item.Website) ? SqlString.Null : item.Website);

                        commandWrapper.AddInputParameter("@Password", SqlDbType.VarChar, item.Password);
                        commandWrapper.AddInputParameter("@Title ", SqlDbType.VarChar, String.IsNullOrEmpty(item.Title) ? 0 : item.Title.Length, String.IsNullOrEmpty(item.Title) ? SqlString.Null : item.Title);
                        if (item.CompanyKey != 0)
                        {
                            commandWrapper.AddInputParameter("@CompanyKey", SqlDbType.Int, item.CompanyKey);
                        }
                        commandWrapper.AddInputParameter("@Description", SqlDbType.VarChar, String.IsNullOrEmpty(item.Description) ? 0 : item.Description.Length, String.IsNullOrEmpty(item.Description) ? SqlString.Null : item.Description);

                        commandWrapper.AddOutputParameter("@companyvalue", SqlDbType.Int);
                        db.ExecuteNonQuery(commandWrapper);
                        status = commandWrapper.GetValueInt("@companyvalue");
                        if (status != 0)
                        {

                            VendorInvetationmailsend(status, item.Email, item.ContactPerson, item.LegalName, "Invite");
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                // error occured...
                //throw;
            }

            return status;
        }



        public Int64 RegVendorManagerInviteVendor(VendorManagerVendorModel item, int ResourceKey)
        {
            int status = 0;
            item.Password = System.Configuration.ConfigurationManager.AppSettings["Password"];
            try
            {
                
                string storedProcedure = "site_VendorManager_InviteVendor_New";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {


                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@Resource", SqlDbType.Int, ResourceKey);
                        commandWrapper.AddInputParameter("@CompanyName ", SqlDbType.VarChar, String.IsNullOrEmpty(item.LegalName) ? 0 : item.LegalName.Length, String.IsNullOrEmpty(item.LegalName) ? SqlString.Null : item.LegalName);
                        commandWrapper.AddInputParameter("@Address", SqlDbType.VarChar, String.IsNullOrEmpty(item.Address) ? 0 : item.Address.Length, String.IsNullOrEmpty(item.Address) ? SqlString.Null : item.Address);
                        commandWrapper.AddInputParameter("@Address2", SqlDbType.VarChar, String.IsNullOrEmpty(item.Address2) ? 0 : item.Address2.Length, String.IsNullOrEmpty(item.Address2) ? SqlString.Null : item.Address2);
                        commandWrapper.AddInputParameter("@City", SqlDbType.VarChar, String.IsNullOrEmpty(item.City) ? 0 : item.City.Length, String.IsNullOrEmpty(item.City) ? SqlString.Null : item.City);
                        commandWrapper.AddInputParameter("@StateKey", SqlDbType.VarChar, String.IsNullOrEmpty(item.State) ? 0 : item.State.Length, String.IsNullOrEmpty(item.State) ? SqlString.Null : item.State);
                        commandWrapper.AddInputParameter("@Zip", SqlDbType.VarChar, String.IsNullOrEmpty(item.Zip) ? 0 : item.Zip.Length, String.IsNullOrEmpty(item.Zip) ? SqlString.Null : item.Zip);
                        commandWrapper.AddInputParameter("@FirstName", SqlDbType.VarChar, String.IsNullOrEmpty(item.FirstName) ? 0 : item.FirstName.Length, String.IsNullOrEmpty(item.FirstName) ? SqlString.Null : item.FirstName);
                        commandWrapper.AddInputParameter("@LastName", SqlDbType.VarChar, String.IsNullOrEmpty(item.LastName) ? 0 : item.LastName.Length, String.IsNullOrEmpty(item.LastName) ? SqlString.Null : item.LastName);

                        commandWrapper.AddInputParameter("@Work2", SqlDbType.VarChar, String.IsNullOrEmpty(item.Work2) ? 0 : item.Work2.Length, String.IsNullOrEmpty(item.Work2) ? SqlString.Null : item.Work2);
                        commandWrapper.AddInputParameter("@Email", SqlDbType.VarChar, String.IsNullOrEmpty(item.Email) ? 0 : item.Email.Length, String.IsNullOrEmpty(item.Email) ? SqlString.Null : item.Email);
                        commandWrapper.AddInputParameter("@Fax", SqlDbType.VarChar, String.IsNullOrEmpty(item.Fax) ? 0 : item.Fax.Length, String.IsNullOrEmpty(item.Fax) ? SqlString.Null : item.Fax);
                        commandWrapper.AddInputParameter("@Website", SqlDbType.VarChar, String.IsNullOrEmpty(item.Website) ? 0 : item.Website.Length, String.IsNullOrEmpty(item.Website) ? SqlString.Null : item.Website);

                        commandWrapper.AddInputParameter("@Password", SqlDbType.VarChar, item.Password);
                        commandWrapper.AddInputParameter("@Title ", SqlDbType.VarChar, String.IsNullOrEmpty(item.Title) ? 0 : item.Title.Length, String.IsNullOrEmpty(item.Title) ? SqlString.Null : item.Title);
                        if (item.CompanyKey != 0)
                        {
                            commandWrapper.AddInputParameter("@CompanyKey", SqlDbType.Int, item.CompanyKey);
                        }
                        commandWrapper.AddInputParameter("@Description", SqlDbType.VarChar, String.IsNullOrEmpty(item.Description) ? 0 : item.Description.Length, String.IsNullOrEmpty(item.Description) ? SqlString.Null : item.Description);

                        commandWrapper.AddOutputParameter("@companyvalue", SqlDbType.Int);
                        db.ExecuteNonQuery(commandWrapper);
                        status = commandWrapper.GetValueInt("@companyvalue");
                        

                    }
                }
            }
            catch (Exception ex)
            {
                // error occured...
                //throw;
            }

            return status;
        }



        public Int64 VendorManager_Update_InviteVendor(VendorManagerVendorModel item)
        {
            int status = 0;

            try
            {
                try
                {
                    if (item.ContactPerson != null)
                    {
                        item.ContactPerson = item.ContactPerson.Trim();
                        var a = item.ContactPerson.Split(' ')[1];
                    }
                    else
                    {
                        item.ContactPerson += " ";
                    }
                }
                catch
                {
                    item.ContactPerson += " ";
                }
                string storedProcedure = "_site_VendorManager_Update_InvitedVendor";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@CompanyKey", SqlDbType.Int, item.CompanyKey == 0 ? SqlInt32.Null : item.CompanyKey);
                        commandWrapper.AddInputParameter("@ResourceKey", SqlDbType.Int, item.VendorKey == 0 ? SqlInt32.Null : item.VendorKey);
                        commandWrapper.AddInputParameter("@CompanyName ", SqlDbType.VarChar, String.IsNullOrEmpty(item.LegalName) ? 0 : item.LegalName.Length, String.IsNullOrEmpty(item.LegalName) ? SqlString.Null : item.LegalName);
                        commandWrapper.AddInputParameter("@Address", SqlDbType.VarChar, String.IsNullOrEmpty(item.Address) ? 0 : item.Address.Length, String.IsNullOrEmpty(item.Address) ? SqlString.Null : item.Address);
                        commandWrapper.AddInputParameter("@Address2", SqlDbType.VarChar, String.IsNullOrEmpty(item.Address2) ? 0 : item.Address2.Length, String.IsNullOrEmpty(item.Address2) ? SqlString.Null : item.Address2);
                        commandWrapper.AddInputParameter("@City", SqlDbType.VarChar, String.IsNullOrEmpty(item.City) ? 0 : item.City.Length, String.IsNullOrEmpty(item.City) ? SqlString.Null : item.City);
                        commandWrapper.AddInputParameter("@State", SqlDbType.VarChar, String.IsNullOrEmpty(item.State) ? 0 : item.State.Length, String.IsNullOrEmpty(item.State) ? SqlString.Null : item.State);
                        commandWrapper.AddInputParameter("@Zip", SqlDbType.VarChar, String.IsNullOrEmpty(item.Zip) ? 0 : item.Zip.Length, String.IsNullOrEmpty(item.Zip) ? SqlString.Null : item.Zip);
                        commandWrapper.AddInputParameter("@FirstName", SqlDbType.VarChar, String.IsNullOrEmpty(item.ContactPerson.Split(' ')[0]) ? 0 : item.ContactPerson.Split(' ')[0].Length, String.IsNullOrEmpty(item.ContactPerson.Split(' ')[0]) ? SqlString.Null : item.ContactPerson.Split(' ')[0]);
                        commandWrapper.AddInputParameter("@LastName", SqlDbType.VarChar, String.IsNullOrEmpty(item.ContactPerson.Split(' ')[1]) ? 0 : item.ContactPerson.Split(' ')[1].Length, String.IsNullOrEmpty(item.ContactPerson.Split(' ')[1]) ? SqlString.Null : item.ContactPerson.Split(' ')[1]);
                        commandWrapper.AddInputParameter("@Cellphone", SqlDbType.VarChar, String.IsNullOrEmpty(item.Work) ? 0 : item.Work.Length, String.IsNullOrEmpty(item.Work) ? SqlString.Null : item.Work);
                        commandWrapper.AddInputParameter("@Email", SqlDbType.VarChar, String.IsNullOrEmpty(item.Email) ? 0 : item.Email.Length, String.IsNullOrEmpty(item.Email) ? SqlString.Null : item.Email);

                        status = db.ExecuteNonQuery(commandWrapper);

                        if (status != 0)
                        {

                            VendorInvetationmailsend(item.CompanyKey, item.Email, item.ContactPerson, item.LegalName, "Invite");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // error occured...
                //throw;
            }

            return status;
        }
        public List<bool> CheckDuplicatedEmailAndCompanyName(string Email,string CompanyName)
        {
            List<bool> Status = new List<bool>();
            int Emailvalue = 0;
            int Compvalue = 0;
            try
            {
                string storedProcedure = "site_VendorManager_CheckDuplicatedEmailAndCompanyName";

                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@Email", SqlDbType.VarChar, String.IsNullOrEmpty(Email) ? 0 : Email.Length, String.IsNullOrEmpty(Email) ? SqlString.Null : Email);
                        commandWrapper.AddInputParameter("@CompanyName", SqlDbType.VarChar, String.IsNullOrEmpty(CompanyName) ? 0 : CompanyName.Length, String.IsNullOrEmpty(CompanyName) ? SqlString.Null : CompanyName);
                        commandWrapper.AddOutputParameter("@EmailStatus", SqlDbType.Int);
                        commandWrapper.AddOutputParameter("@CompStatus", SqlDbType.Int);

                        db.ExecuteNonQuery(commandWrapper);
                        Emailvalue = commandWrapper.GetValueInt("@EmailStatus");
                        Compvalue = commandWrapper.GetValueInt("@CompStatus");

                        if (Emailvalue != 1)
                        {
                            Status.Add(true);
                        }
                        else
                        {
                            Status.Add(false);
                        }
                        if (Compvalue != 1)
                            Status.Add(true);
                        else
                            Status.Add(false);
                    }
                }
            }
            catch (Exception ex)
            {
                // error occured...
                throw;
            }

            return Status;
        }
        public bool CheckDuplicatedEmailByResourceKey(string Email, int ResourceKey)
        {
            bool Status = false;
            int Emailvalue = 0;
            try
            {
                string storedProcedure = "site_VendorManager_CheckDuplicatedEmailByResourceKey";

                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@Email", SqlDbType.VarChar, String.IsNullOrEmpty(Email) ? 0 : Email.Length, String.IsNullOrEmpty(Email) ? SqlString.Null : Email);
                        commandWrapper.AddInputParameter("@ResourceKey", SqlDbType.Int, ResourceKey == 0 ? SqlInt32.Null : ResourceKey);
                        commandWrapper.AddOutputParameter("@EmailStatus", SqlDbType.Int);

                        db.ExecuteNonQuery(commandWrapper);
                        Emailvalue = commandWrapper.GetValueInt("@EmailStatus");

                        if (Emailvalue != 1)
                        {
                            Status = true;
                        }
                        else
                        {
                            Status = false; ;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // error occured...
                throw;
            }

            return Status;
        }
        public bool CheckDuplicatedCompanyNameByCompanyKey(string CompanyName, int CompanyKey)
        {
            bool Status = false;
            int Compvalue = 0;
            try
            {
                string storedProcedure = "site_VendorManager_CheckDuplicatedCompanyNameByCompanyKey";

                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@CompanyName", SqlDbType.VarChar, String.IsNullOrEmpty(CompanyName) ? 0 : CompanyName.Length, String.IsNullOrEmpty(CompanyName) ? SqlString.Null : CompanyName);
                        commandWrapper.AddInputParameter("@CompanyKey", SqlDbType.Int, CompanyKey == 0 ? SqlInt32.Null : CompanyKey);
                        commandWrapper.AddOutputParameter("@CompStatus", SqlDbType.Int);

                        db.ExecuteNonQuery(commandWrapper);
                        Compvalue = commandWrapper.GetValueInt("@CompStatus");

                        if (Compvalue != 1)
                        {
                            Status = true;
                        }
                        else
                        {
                            Status = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // error occured...
                throw;
            }

            return Status;
        }

        public bool VendorManagerChangePassword(string NewPassword, int UserKey)
        {
            bool Status = true;
            string pass = "";
            pass = Security.Encrypt(NewPassword);
            try
            {
                string storedProcedure = "site_VendorManager_User_ChangePassword";

                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@UserId", SqlDbType.Int, UserKey == 0 ? SqlInt32.Null : UserKey);
                        commandWrapper.AddInputParameter("@NewPassword", SqlDbType.Text, pass);
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        db.ExecuteNonQuery(commandWrapper);
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            Status = false;
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                Status = false;
                // error occured...
                throw;
            }

            return Status;
        }


        public bool VendorManagerApproveVendor(int CompanyKey)
        {
            bool status = false;
            try
            {
                string storedProcedure = "site_VendorManager_Approve_Pending";

                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@CompanyKey", SqlDbType.Int, (CompanyKey == 0) ? SqlInt32.Null : CompanyKey);
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);
                        db.ExecuteNonQuery(commandWrapper);

                        int error = commandWrapper.GetValueInt("@errorCode");

                        if (error == 0)
                        {

                            VendorManagerVendorModel Vendor = new VendorManagerVendorModel();
                            Vendor = VendorManager_AprovalEmailList(CompanyKey);

                            status = true;
                        }
                        else
                        {
                            status = false;
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

        public VendorManagerVendorModel VendorManager_AprovalEmailList(int CompanyKey)
        {
            VendorManagerVendorModel companyList = new VendorManagerVendorModel();
            List<EmailSendModel> emailList = new List<EmailSendModel>();
            try
            {
                string storedProcedure = "site_VendorManager_AcceptEmailSendDetail";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        commandWrapper.AddInputParameter("@CompanyKey", SqlDbType.Int, (CompanyKey == 0) ? SqlInt32.Null : CompanyKey);
                        var reader = db.ExecuteReader(commandWrapper);

                        while (reader.Read())
                        {
                            EmailSendModel email = new EmailSendModel();
                            var byresource = Convert.ToInt32(HttpContext.Current.Session["resourceid"]);
                            VendorManagerVendorModel vendor = new VendorManagerVendorModel();
                            //AStaffDirectoryRepository staffdirectory = new AStaffDirectoryRepository();
                            BidRequestRepository bid = new BidRequestRepository();
                            VendorManager_AprovalEmailList(reader, vendor);
                            //staffdirectory.mailsendAsync(vendor.UserKey, vendor.Email, vendor.UserName, vendor.ResetExpirationDate, vendor.CompanyName, "Vendor Aporval");
                            email = CreateMailTemplateForVendor(vendor.UserKey, vendor.Email, vendor.Name, vendor.ResetExpirationDate, vendor.CompanyName, "Vendor Aporval");

                            EmailModel emailTemplate = new EmailModel();
                            emailTemplate.Body = email.Body;
                            emailTemplate.DateAdded = DateTime.Now;
                            emailTemplate.Subject = email.Subject;
                            emailTemplate.DateSent = DateTime.Now;
                            emailTemplate.EmailStatus = 500;
                            emailTemplate.From = System.Configuration.ConfigurationManager.AppSettings["senderemail"];
                            emailTemplate.To = email.To;
                            emailTemplate.ObjectKey = CompanyKey;
                            emailTemplate.ResourceKey = byresource;
                            emailTemplate.ModuleKey = 705;
                            EmailRepository emaillog = new EmailRepository();
                            bool isinserted = emaillog.Create(emailTemplate);
                            if (isinserted)
                                email.EmailKey = emailTemplate.EmailKey;
                            if (email != null && email.From != "" && email.To != "")
                            {
                                emailList.Add(email);
                            }
                        }
                    }
                }
                if(emailList.Count > 0)
                {
                    Task.Run(() => SendMailAsync(emailList)).ConfigureAwait(false);
                }

            }
            catch (Exception ex)
            {

            }

            return companyList;
        }

        protected void VendorManager_AprovalEmailList(DBDataReader reader, VendorManagerVendorModel item)
        {
            item.UserKey = reader.GetValueInt("UserKey");
            item.Email = reader.GetValueText("Email");
            item.UserName = reader.GetValueText("UserName");
            item.ResetExpirationDate = reader.GetValueText("ResetExpirationDate");
            item.CompanyName = reader.GetValueText("Name");
            item.Name = reader.GetValueText("ContactName"); 
        }

        public bool VendorManagerMarkDuplicateVendor(int CompanyKey)
        {
            bool status = false;
            try
            {
                string storedProcedure = "site_VendorManager_MarkDuplicate_Unapproved";

                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@CompanyKey", SqlDbType.Int, (CompanyKey == 0) ? SqlInt32.Null : CompanyKey);
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);
                        db.ExecuteNonQuery(commandWrapper);

                        int error = commandWrapper.GetValueInt("@errorCode");

                        if (error == 0)
                        {
                            status = true;
                        }
                        else
                        {
                            status = false;
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

        public EmailSendModel CreateMailTemplateForVendor(int UserKey, string fromemail, string UserName, string ResetExpirationDate, string CompanyName, string Status)
        {
            EmailSendModel email = new EmailSendModel();
            try
            {
                AStaffDirectoryRepository aStaff = new AStaffDirectoryRepository();
                IList<EmailTemplateModel> EmailTemplate = null;
                EmailTemplate = aStaff.GetEmailAll(Status);
                string LinkUrl = System.Configuration.ConfigurationManager.AppSettings["LinkUrl"];
                string body = EmailTemplate[0].Body;
                body = body.Replace("[MemberName]", UserName);
                body = body.Replace("[ResetPasswordlink]", LinkUrl + "/ChangePassword/ResetPassword?UserKey=" + UserKey);
                body = body.Replace("[SendDate]", ResetExpirationDate);
                body = body.Replace("[LinkExpiryDate]", ResetExpirationDate);
                body = body.Replace("[MemberEmail]", fromemail);
                body = body.Replace("[MemberCompanyName]", CompanyName);
                body = body.Replace("[CompanyName]", CompanyName);


                //MailMessage msg = new MailMessage();
                string strBody = string.Empty;

                string Subject = EmailTemplate[0].EmailSubject;
                Subject = Subject.Replace("[CompanyName]", CompanyName);

                email.From = System.Configuration.ConfigurationManager.AppSettings["senderemail"];
                email.To = fromemail;
                email.Subject = Subject;
                email.IsHtml = true;
            
                email.Body += body;

                //using (SmtpClient client = new SmtpClient())
                //{
                //    client.EnableSsl = false;
                //    client.UseDefaultCredentials = false;
                //    client.Credentials = new NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["smtpUser"], System.Configuration.ConfigurationManager.AppSettings["smtpPass"]);
                //    client.Host = System.Configuration.ConfigurationManager.AppSettings["smtpServer"];
                //    client.Port = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["smtpPort"]);
                //    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                //    client.Send(msg);
                //}
            }
            catch (Exception ex)
            {
                email = null;
            }
            return email;
        }

        private async Task SendMailAsync(List<EmailSendModel> sendModel)
        {
            EmailRepository emailRepos = new EmailRepository();
            EmailModel emailModel = null;
            try
            {
                foreach (var emailItem in sendModel)
                {
                    MailMessage msg = new MailMessage();

                    msg.From = new MailAddress(emailItem.From);
                    msg.To.Add(emailItem.To);
                    msg.Subject = emailItem.Subject;
                  
                    msg.IsBodyHtml = emailItem.IsHtml;
                  
                    msg.Body = emailItem.Body;
                    emailModel = emailRepos.Get(Convert.ToInt32(emailItem.EmailKey));
                    using (SmtpClient client = new SmtpClient())
                    {
                        client.EnableSsl = false;
                        client.UseDefaultCredentials = false;
                        client.Credentials = new NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["smtpUser"], System.Configuration.ConfigurationManager.AppSettings["smtpPass"]);
                        client.Host = System.Configuration.ConfigurationManager.AppSettings["smtpServer"];
                        client.Port = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["smtpPort"]);
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        await client.SendMailAsync(msg);
                        emailModel.EmailStatus = 502;
                        emailRepos.Update(emailModel);
                    }
                }
            }
            catch (Exception ex)
            {
                if(emailModel != null)
                {
                    emailModel.EmailStatus = 501;
                    emailRepos.Update(emailModel);
                }
            }
        }

        public void mailsendAsync(int status, string fromemail)
        {
            try
            {
              
            }
            catch (Exception ex)
            {
            }
        }

        public void VendorInvetationmailsend(int status, string fromemail, string UserName,string CompanyName,string Status)
        {
            try
            {
                Repository.Base.Code.PMVendorRepository staffDirectoryRepository = new Repository.Base.Code.PMVendorRepository();
                IList<Model.EmailTemplateModel> EmailTemplate = null;
                string LinkUrl = System.Configuration.ConfigurationManager.AppSettings["LinkUrl"];
                EmailTemplate = GetAll1(Status);

                string body = EmailTemplate[0].Body;
                body = body.Replace("[MemberName]", Convert.ToString(UserName.ToString().Trim()));
                body = body.Replace("[VendorRegistrationLink]", LinkUrl+ "/Registration/Registration?CompanyKey=" + status);
                body = body.Replace("[CompanyName]", Convert.ToString(CompanyName.ToString().Trim()));
                body = body.Replace("[ContactPerson]", Convert.ToString(UserName.ToString().Trim()));
                body = body.Replace("[Email]", Convert.ToString(fromemail.ToString().Trim()));
                
                string Subject = EmailTemplate[0].EmailSubject;
                Subject = Subject.Replace("[CompanyName]", Convert.ToString(CompanyName.ToString().Trim()));

                MailMessage msg = new MailMessage();
                string strBody = string.Empty;

                msg.From = new MailAddress(System.Configuration.ConfigurationManager.AppSettings["senderemail"]);
                msg.To.Add(fromemail);
               
                msg.Subject = Subject;
                msg.IsBodyHtml = true;
                msg.Body += body;
                var byresource = Convert.ToInt32(HttpContext.Current.Session["resourceid"]);
                EmailModel emailTemplate = new EmailModel();
                emailTemplate.Body = body;
                emailTemplate.DateAdded = DateTime.Now;
                emailTemplate.Subject = Subject;
                emailTemplate.DateSent = DateTime.Now;
                emailTemplate.EmailStatus = 500;
                emailTemplate.From = System.Configuration.ConfigurationManager.AppSettings["senderemail"];
                emailTemplate.To = fromemail;
                emailTemplate.ObjectKey = status;
                emailTemplate.ResourceKey = byresource;
                emailTemplate.ModuleKey = 705;
                EmailRepository emaillog = new EmailRepository();
                bool isinserted = emaillog.Create(emailTemplate);
                using (SmtpClient client = new SmtpClient())
                {
                    client.EnableSsl = false;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["smtpUser"], System.Configuration.ConfigurationManager.AppSettings["smtpPass"]);
                    client.Host = System.Configuration.ConfigurationManager.AppSettings["smtpServer"];
                    client.Port = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["smtpPort"]);
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.Send(msg);
                    emailTemplate.EmailStatus = 502;
                    emaillog.Update(emailTemplate);
                }
            }
            catch (Exception ex)
            {
            }
        }
        public void VendorInvetationmailsend1(int status, string fromemail, string UserName, string CompanyName, string Status)
        {
            try
            {
                Repository.Base.Code.PMVendorRepository staffDirectoryRepository = new Repository.Base.Code.PMVendorRepository();
                IList<Model.EmailTemplateModel> EmailTemplate = null;
                string LinkUrl = System.Configuration.ConfigurationManager.AppSettings["LinkUrl"];
                EmailTemplate = GetAll(Status);

                string body = EmailTemplate[0].Body;
                body = body.Replace("[MemberName]", Convert.ToString(UserName.ToString().Trim()));
                body = body.Replace("[VendorRegistrationLink]", LinkUrl + "/Registration/Registration?CompanyKey=" + status);
                body = body.Replace("[CompanyName]", Convert.ToString(CompanyName.ToString().Trim()));
                body = body.Replace("[ContactPerson]", Convert.ToString(UserName.ToString().Trim()));
                body = body.Replace("[Email]", Convert.ToString(fromemail.ToString().Trim()));

                string Subject = EmailTemplate[0].EmailSubject;
                Subject = Subject.Replace("[CompanyName]", Convert.ToString(CompanyName.ToString().Trim()));

                MailMessage msg = new MailMessage();
                string strBody = string.Empty;

                msg.From = new MailAddress(System.Configuration.ConfigurationManager.AppSettings["senderemail"]);
                msg.To.Add(fromemail);

                msg.Subject = Subject;
                msg.IsBodyHtml = true;
                msg.Body += body;
                var byresource = Convert.ToInt32(HttpContext.Current.Session["resourceid"]);
                EmailModel emailTemplate = new EmailModel();
                emailTemplate.Body = body;
                emailTemplate.DateAdded = DateTime.Now;
                emailTemplate.Subject = Subject;
                emailTemplate.DateSent = DateTime.Now;
                emailTemplate.EmailStatus = 500;
                emailTemplate.From = System.Configuration.ConfigurationManager.AppSettings["senderemail"];
                emailTemplate.To = fromemail;
                emailTemplate.ObjectKey = status;
                emailTemplate.ResourceKey = EmailTemplate[0].Superadminkey;
                emailTemplate.ModuleKey = 705;
                EmailRepository emaillog = new EmailRepository();
                bool isinserted = emaillog.Create(emailTemplate);
                using (SmtpClient client = new SmtpClient())
                {
                    client.EnableSsl = false;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["smtpUser"], System.Configuration.ConfigurationManager.AppSettings["smtpPass"]);
                    client.Host = System.Configuration.ConfigurationManager.AppSettings["smtpServer"];
                    client.Port = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["smtpPort"]);
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.Send(msg);
                    emailTemplate.EmailStatus = 502;
                    emaillog.Update(emailTemplate);
                }
            }
            catch (Exception ex)
            {
            }
        }
        public virtual IList<EmailTemplateModel> GetAll(string Status)
        {
            List<EmailTemplateModel> itemList = new List<EmailTemplateModel>();
            try
            {
                string storedProcedure = "site_EmailTemplate_SelectAll";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        commandWrapper.AddInputParameter("@LookUpTitle", SqlDbType.NText, "Invite Partial Registration");
                        //commandWrapper.AddInputParameter("@LookUpTitle", SqlDbType.NText, "Vendor invitation");

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            EmailTemplateModel item = null;
                            while (dataReader.Read())
                            {
                                item = new EmailTemplateModel();
                                Load(dataReader, item);
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

        public virtual IList<EmailTemplateModel> GetAll1(string Status)
        {
            List<EmailTemplateModel> itemList = new List<EmailTemplateModel>();
            try
            {
                string storedProcedure = "site_EmailTemplate_SelectAll";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        commandWrapper.AddInputParameter("@LookUpTitle", SqlDbType.NText, "Partial Registration");
                        //commandWrapper.AddInputParameter("@LookUpTitle", SqlDbType.NText, "Vendor invitation");

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            EmailTemplateModel item = null;
                            while (dataReader.Read())
                            {
                                item = new EmailTemplateModel();
                                Load(dataReader, item);
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



        public List<InsuranceModel> GetInsuranceByCompanyKey(int CompanyKey)
        {
            List<InsuranceModel> insurance = new List<InsuranceModel>();

            try
            {
                string storedProcedure = "site_Insurance_SelectByCompanyKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        commandWrapper.AddInputParameter("@CompanyKey", SqlDbType.Int, CompanyKey);
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            InsuranceModel item = null;
                            if (commandWrapper.GetValueInt("@errorCode") == 0)
                            {
                                while (dataReader.Read())
                                {
                                    item = new InsuranceModel();
                                    LoadInsurance(dataReader, item);
                                    insurance.Add(item);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
            }

            return insurance;
        }
        protected void Load(DBDataReader dataReader, EmailTemplateModel item)
        {
            item.EmailTemplateKey = dataReader.GetValueInt("EmailTemplateKey");
            item.EmailTitle = dataReader.GetValueText("EmailTitle");
            item.Title = dataReader.GetValueText("Title");
            item.EmailSubject = dataReader.GetValueText("EmailSubject");
            item.Body = dataReader.GetValueText("Body");
            item.Superadminkey = dataReader.GetValueInt("superadminkey");
        }


        public VendorManagerModel GetUserPrimaryByCompanyKeyAndResourceKey(int CompanyKey,int ResourceKey)
        {
            VendorManagerModel userData = new VendorManagerModel();
            
            try
            {
                string storedProcedure = "site_VendorManager_getUserBy_R_C_key";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@CompanyKey ", SqlDbType.Int, (CompanyKey == 0) ? SqlInt32.Null : CompanyKey);
                        commandWrapper.AddInputParameter("@ResourceKey", SqlDbType.Int, (ResourceKey == 0) ? SqlInt32.Null : ResourceKey);
                        var item = db.ExecuteReader(commandWrapper);
                        if (item.Read())
                        {
                            userData.Document = new DocumentModel();
                            userData.Resource = new ResourceModel();
                            userData.ServiceModel = new VendorServiceModel();
                            userData.UserModel = new UserModel();
                            userData.Insurance = new InsuranceModel();
                            userData.Vendor = new VendorManagerVendorModel();
                            userData.Vendor.CompanyKey = item.GetValueInt("CompanyKey");
                            userData.Resource.ResourceKey = item.GetValueInt("ResourceKey");
                            userData.Vendor.Name = item.GetValueText("Name");
                            userData.Vendor.Email = item.GetValueText("Email");
                            userData.Resource.CellPhone = item.GetValueText("Cellphone");
                            userData.Vendor.Address = item.GetValueText("Address");
                            userData.Vendor.City = item.GetValueText("City");
                            userData.Resource.FirstName = item.GetValueText("FirstName");
                            userData.Resource.LastName = item.GetValueText("LastName");
                            userData.UserModel.UserKey = item.GetValueInt("UserKey");
                            userData.UserModel.Username = item.GetValueText("Username");
                            userData.UserModel.Password = item.GetValueText("Password");
                            userData.UserModel.FirstTimeAccess = item.GetValueBool("FirstTimeAccess");
                            userData.UserModel.Status = item.GetValueInt("Status");
                        }
                        //status = commandWrapper.GetValueInt("@ResourceValue");

                    }
                }
            }
            catch (Exception ex)
            {
                
            }
            return userData;
        }

        protected void GetbindDocument(DBDataReader dataReader, VendorManagerModel item)
        {
            try
            {
                item.Insurance.InsuranceKey = dataReader.GetValueInt("InsuranceKey");
                item.Vendor.CompanyKey = dataReader.GetValueInt("VendorKey");
                item.Insurance.VendorKey = dataReader.GetValueInt("VendorKey");
                item.Insurance.PolicyNumber = dataReader.GetValueText("PolicyNumber");
                item.Insurance.InsuranceAmount = dataReader.GetValueDecimal("InsuranceAmount");
                item.Insurance.AgentName = dataReader.GetValueText("AgentName");
                item.Insurance.Email = dataReader.GetValueText("Email");
                item.Vendor.Email = dataReader.GetValueText("Email");
                item.Vendor.CompanyName = dataReader.GetValueText("CompanyName");
                item.Document.DocumentKey = dataReader.GetValueInt("DocumentKey");
                item.Document.FileName = dataReader.GetValueText("FileName");
                item.Document.ModuleKey = dataReader.GetValueInt("ModuleKey");
                item.Document.FileSize = dataReader.GetValueDouble("FileSize");
                item.Insurance.StartDate = dataReader.GetValueDateTime("StartDate");
                item.Insurance.EndDate = dataReader.GetValueDateTime("EndDate");
                item.Insurance.RenewalDate = dataReader.GetValueDateTime("RenewalDate");
                try
                {
                    item.Insurance.StartDates = item.Insurance.StartDate.ToString("MM/dd/yyyy");
                }
                catch (Exception)
                { }
                item.Insurance.EndDate = dataReader.GetValueDateTime("EndDate");
                try
                {
                    item.Insurance.EndDates = item.Insurance.EndDate.ToString("MM/dd/yyyy");
                }
                catch (Exception)
                { }
                item.Insurance.RenewalDate = dataReader.GetValueDateTime("RenewalDate");
                try
                {
                    item.Insurance.RenewalDates = item.Insurance.RenewalDate.ToString("MM/dd/yyyy");
                }
                catch (Exception)
                { }
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        protected void LoadInsurance(DBDataReader dataReader, InsuranceModel item)
        {
            try
            {
                item.InsuranceKey = dataReader.GetValueInt("InsuranceKey");
                item.VendorKey = dataReader.GetValueInt("VendorKey");
                item.PolicyNumber = dataReader.GetValueText("PolicyNumber");
                item.InsuranceAmount = dataReader.GetValueDecimal("InsuranceAmount");
                item.AgentName = dataReader.GetValueText("AgentName");
                item.StartDate = dataReader.GetValueDateTime("StartDate");
                item.EndDate = dataReader.GetValueDateTime("EndDate");
                item.RenewalDate = dataReader.GetValueDateTime("RenewalDate");
                item.Status = dataReader.GetValueInt("Status");

            }
            catch (Exception ex)
            {

                throw;
            }

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
        protected void LoadVendor(DBDataReader dataReader, VendorManagerVendorModel item)
        {

            item.CompanyKey = dataReader.GetValueInt("CompanyKey");
            item.LegalName = dataReader.GetValueText("Name");
            item.Status = dataReader.GetValueText("favorite") == "Pending" ? 0 : 1;
            item.Work = dataReader.GetValueText("Work");
            item.Title = dataReader.GetValueText("Title");
            item.Favorite = dataReader.GetValueText("favorite");
            item.ContactPerson = dataReader.GetValueText("VendorName");
            item.TotalRecords = dataReader.GetValueInt("TotalRecord");
            try
            {
                try
                {
                    item.CellPhone = dataReader.GetValueText("cellphone");
                }
                catch { }
                item.priority = dataReader.GetValueInt("priority");
                item.NotificationId = dataReader.GetValueInt("NotificationId");
                item.NotificationType = dataReader.GetValueText("NotificationType");
            }
            catch { }
        }

        public VendorManagerModel VendorManagerGetBidRequestDetails(int BidRequestKey,int CompanyKey)
        {
            VendorManagerModel vm = new VendorManagerModel();
            vm.BidRequest = new BidRequestModel();
            try
            {
                string storedProcedure = "site_VendorManager_BidReq_BidReqKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        commandWrapper.AddInputParameter("@CompanyKey", SqlDbType.Int, CompanyKey == 0 ? SqlInt32.Null : CompanyKey);
                        commandWrapper.AddInputParameter("@BidRequestKey", SqlDbType.Int, BidRequestKey == 0 ? SqlInt32.Null : BidRequestKey);
                        DBDataReader datareader = db.ExecuteReader(commandWrapper);
                        LoadViewEditBidWorkDetails(datareader, vm);
                    }
                }

            }
            catch
            {

            }
            return vm;
        }

        public string SearchBidRequestVenderDocjson(long PageSize, long PageIndex, string Search, string Sort, long BidVendorKey, string TableName)
        {
            try
            {
                ConnectionCls obj_con = new ConnectionCls();
                obj_con.clearParameter();
                obj_con.addParameter("@PageSize", string.IsNullOrEmpty(Convert.ToString(PageSize)) ? 0 : PageSize);
                obj_con.addParameter("@PageIndex", string.IsNullOrEmpty(Convert.ToString(PageIndex)) ? 0 : PageIndex);
                obj_con.addParameter("@Search", Search);
                obj_con.addParameter("@Sort", Sort);
                obj_con.addParameter("@BidVendorKey", string.IsNullOrEmpty(Convert.ToString(BidVendorKey)) ? 0 : BidVendorKey);
                obj_con.addParameter("@TableName", string.IsNullOrEmpty(Convert.ToString(TableName)) ? "" : TableName);
                
                DataTable dt = obj_con.ConvertDatareadertoDataTable(obj_con.ExecuteReader("site_BidRequestDoc_SelectIndexPagingBidRequestVender", CommandType.StoredProcedure));
                obj_con.CommitTransaction();
                obj_con.closeConnection();
                return AssoBidsUtility.ConvertDataTableTojSonString(dt);
            }
            catch (Exception ex)
            {
                throw new Exception("site_BidRequestDoc_SelectIndexPagingBidRequestVender" + ex.ToString());
            }
        }

        public string SendInsertMessage(string Body, string Status, Int64 ObjectKey, Int64 ResourceKey, string ModuleKeyName)
        {
            ConnectionCls obj_con = new ConnectionCls();
            try
            {
                obj_con.clearParameter();
                obj_con.BeginTransaction();
                obj_con.addParameter("@Body", string.IsNullOrEmpty(Convert.ToString(Body)) ? "" : Body);
                obj_con.addParameter("@Status", string.IsNullOrEmpty(Convert.ToString(Status)) ? "" : Status);
                obj_con.addParameter("@ObjectKey", string.IsNullOrEmpty(Convert.ToString(ObjectKey)) ? 0 : ObjectKey);
                obj_con.addParameter("@ResourceKey", string.IsNullOrEmpty(Convert.ToString(ResourceKey)) ? 0 : ResourceKey);
                obj_con.addParameter("@ModuleKeyName", string.IsNullOrEmpty(Convert.ToString(ModuleKeyName)) ? "" : ModuleKeyName);


                obj_con.ExecuteNoneQuery("site_Message_Insert", CommandType.StoredProcedure);
                obj_con.CommitTransaction();
                return "Success";
            }
            catch (Exception ex)
            {
                obj_con.RollbackTransaction();
                throw new Exception("site_Message_Insert" + ex.ToString());
            }
        }
        public string SendInsertMessageList(Int64 ObjectKey, Int64 ResourceKey, string ModuleKeyName, string UpdatemsgStatus)
        {
            ConnectionCls obj_con = new ConnectionCls();
            try
            {
                obj_con.clearParameter();
                obj_con.addParameter("@PageSize", string.IsNullOrEmpty(Convert.ToString(0)) ? 50 : 50);
                obj_con.addParameter("@PageIndex", string.IsNullOrEmpty(Convert.ToString(0)) ? 1 : 1);
                obj_con.addParameter("@Search", "");
                obj_con.addParameter("@Sort", "");
                obj_con.addParameter("@ObjectKey", ObjectKey);
                obj_con.addParameter("@ResourceKey", ResourceKey);
                obj_con.addParameter("@ModuleKeyName", string.IsNullOrEmpty(Convert.ToString(ModuleKeyName)) ? "" : ModuleKeyName);

                obj_con.addParameter("@UpdatemsgStatus", string.IsNullOrEmpty(Convert.ToString(UpdatemsgStatus)) ? "0" : UpdatemsgStatus);
                DataTable dt = obj_con.ConvertDatareadertoDataTable(obj_con.ExecuteReader("site_Message_ListSelectIndexPaging", CommandType.StoredProcedure));
                obj_con.CommitTransaction();
                obj_con.closeConnection();
                return AssoBidsUtility.ConvertDataTableTojSonString(dt);
            }
            catch (Exception ex)
            {
                throw new Exception("site_Message_ListSelectIndexPaging" + ex.ToString());
            }
        }
        
        public string MessageNewCount(Int64 ObjectKey, Int64 ResourceKey, string ModuleKeyName, Int64 UserId)
        {
            ConnectionCls obj_con = new ConnectionCls();
            try
            {
                obj_con.clearParameter();
                obj_con.addParameter("@UserId", UserId);
                obj_con.addParameter("@ObjectKey", ObjectKey);
                obj_con.addParameter("@ResourceKey", ResourceKey);
                obj_con.addParameter("@ModuleKeyName", string.IsNullOrEmpty(Convert.ToString(ModuleKeyName)) ? "" : ModuleKeyName);
                DataTable dt = obj_con.ConvertDatareadertoDataTable(obj_con.ExecuteReader("site_Message_NewListCount", CommandType.StoredProcedure));
                obj_con.CommitTransaction();
                obj_con.closeConnection();
                return AssoBidsUtility.ConvertDataTableTojSonString(dt);
            }
            catch (Exception ex)
            {
                throw new Exception("site_Message_NewListCount" + ex.ToString());
            }
        }


        public VendorManagerModel VendorManagerGetWorkOrderDetails(int BidRequestKey, int CompanyKey)
        {
            VendorManagerModel vm = new VendorManagerModel();
            vm.BidRequest = new BidRequestModel();
            try
            {
                string storedProcedure = "site_VendorManager_WorkOrder_BidReqKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        commandWrapper.AddInputParameter("@CompanyKey", SqlDbType.Int, CompanyKey == 0 ? SqlInt32.Null : CompanyKey);
                        commandWrapper.AddInputParameter("@BidRequestKey", SqlDbType.Int, BidRequestKey == 0 ? SqlInt32.Null : BidRequestKey);
                        DBDataReader datareader = db.ExecuteReader(commandWrapper);
                        LoadViewEditBidWorkDetails(datareader, vm);
                    }
                }

            }
            catch
            {

            }
            return vm;
        }

        public List<BidRequestModel> SearchVendorByBidRequest(int BidRequestKey, int modulekey)
        {
            List<BidRequestModel> itemList = new List<BidRequestModel>();
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
                            BidRequestModel item = null;
                            while (dataReader.Read())
                            {
                                item = new BidRequestModel();
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
        public List<BidRequestModel> SearchVendorByBidRequest(int BidRequestKey, int modulekey, int resourcekey)
        {
            List<BidRequestModel> itemList = new List<BidRequestModel>();
            try
            {
                string storedProcedure = "site_BidRequest_GetVendorsForBidRequestByAlgorithm_Copy";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        commandWrapper.AddInputParameter("@BidRequestkey", SqlDbType.Int, (BidRequestKey == 0) ? 0 : BidRequestKey);
                        commandWrapper.AddInputParameter("@Modulekey", SqlDbType.Int, modulekey);
                        commandWrapper.AddInputParameter("@ResourceKey", SqlDbType.Int, resourcekey);
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            BidRequestModel item = null;
                            while (dataReader.Read())
                            {
                                item = new BidRequestModel();
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
        public List<BidRequestModel> SearchAllVendor(int BidRequestKey, string SearchVendorName, string SearchCompanyName, int IsStaredVendor, int LastWorkedBefore)
        {
            List<BidRequestModel> itemList = new List<BidRequestModel>();
            try
            {
                string storedProcedure = "site_Company_GetAllVendorsForBidRequest";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters 
                        commandWrapper.AddInputParameter("@BidRequestkey", SqlDbType.Int, (BidRequestKey == 0) ? 0 : BidRequestKey);
                        commandWrapper.AddInputParameter("@VendorName", SqlDbType.VarChar, SearchVendorName);
                        commandWrapper.AddInputParameter("@CompanyName", SqlDbType.VarChar, SearchCompanyName);
                        if (IsStaredVendor == 1)
                        {
                            commandWrapper.AddInputParameter("@IsStaredVendor", SqlDbType.Bit, true);
                        }
                        else if (IsStaredVendor == 2)
                        {
                            commandWrapper.AddInputParameter("@IsStaredVendor", SqlDbType.Bit, false);
                        }
                        else if (IsStaredVendor == 0)
                        {
                            commandWrapper.AddInputParameter("@IsStaredVendor", SqlDbType.Bit, DBNull.Value);
                        }
                        commandWrapper.AddInputParameter("@lastWorkedWith", SqlDbType.Int, LastWorkedBefore);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            BidRequestModel item = null;
                            while (dataReader.Read())
                            {
                                item = new BidRequestModel();
                                LoadAllVendor(dataReader, item);
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

        public IList<BidRequestModel> GetbindDocumentByBidRequestKey(int BidRequestKey, int ModuleKey)
        {
            List<BidRequestModel> itemList = new List<BidRequestModel>();

            try
            {

                string storedProcedure = "site_VendorManager_Document_SelectAll";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        commandWrapper.AddInputParameter("@InsuranceKey", SqlDbType.Int, BidRequestKey);
                        commandWrapper.AddInputParameter("@ModuleKey", SqlDbType.Int, ModuleKey);
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            BidRequestModel item = null;
                            while (dataReader.Read())
                            {
                                item = new BidRequestModel();
                                GetbindDocumentByBidRequestKey(dataReader, item);
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
        protected void GetbindDocumentByBidRequestKey(DBDataReader dataReader, BidRequestModel item)
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

        protected void LoadVendorByBidRequest(DBDataReader dataReader, BidRequestModel item)
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
        protected void LoadAllVendor(DBDataReader dataReader, BidRequestModel item)
        {
            item.VendorName = dataReader.GetValueText("VendorName");
            item.CompanyName = dataReader.GetValueText("CompanyName");
            item.LastWorkDate = dataReader.GetValueText("Last Work Date");
            item.VendorKey = dataReader.GetValueInt("CompanyKey");
        }


        public IList<BidRequestModel> GetbindBidRequestNotes(int BidRequestKey)
        {
            List<BidRequestModel> itemList = new List<BidRequestModel>();

            try
            {

                string storedProcedure = "site_VendorManager_Notes_BidRequestSelectAll";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        commandWrapper.AddInputParameter("@BidRequestKey", SqlDbType.Int, BidRequestKey);
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            BidRequestModel item = null;
                            while (dataReader.Read())
                            {
                                item = new BidRequestModel();
                                GetbindBidRequestNotes(dataReader, item);
                                itemList.Add(item);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message);
            }

            return itemList;
        }

        public virtual bool InsertNotes(string title, string description, int BidRequestKey, int Resourcekey)
        {
            bool status = false;
            try
            {

                string storedProcedure = "site_Notes_BidRequestInsert";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        commandWrapper.AddInputParameter("@BidRequestKey", SqlDbType.Int, BidRequestKey);
                        commandWrapper.AddInputParameter("@Resourcekey", SqlDbType.Int, Resourcekey);
                        commandWrapper.AddInputParameter("@Description", SqlDbType.VarChar, String.IsNullOrEmpty(description) ? 0 : description.Length, String.IsNullOrEmpty(description) ? SqlString.Null : description);
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

        public virtual bool NotesRemove(int Noteid)
        {

            bool val = false;
            try
            {

                string storedProcedure = "site_Notes_BidRequestDelete";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        commandWrapper.AddInputParameter("@Noteid", SqlDbType.Int, Noteid);
                        db.ExecuteNonQuery(commandWrapper);
                    }
                }
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message);
            }

            return val;
        }

        protected void GetbindBidRequestNotes(DBDataReader dataReader, BidRequestModel item)
        {
            try
            {
                item.NotesDescription = dataReader.GetValueText("Description");
                item.Notesdatetime = dataReader.GetValueText("LastModificationTime");
                item.NoteKey = dataReader.GetValueInt("NoteKey");
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
            }
        }


        protected void LoadViewEditBidWorkDetails(DBDataReader reader,VendorManagerModel vm)
        {
            reader.Read();
            vm.BidRequest.BidRequestKey = reader.GetValueInt("BidRequestKey");
            vm.BidRequest.PropertyKey = reader.GetValueText("PropertyKey");
            vm.BidRequest.ResourceKey = reader.GetValueInt("ResourceKey");
            vm.BidRequest.ServiceKey = reader.GetValueInt("ServiceKey");
            vm.BidRequest.CompanyKey = reader.GetValueInt("CompanyKey");
            vm.BidRequest.Title = reader.GetValueText("Title");
            vm.BidRequest.BidDueDate = reader.GetValueDateTime("BidDueDate");
            vm.BidRequest.StartDate = reader.GetValueDateTime("StartDate");
            vm.BidRequest.EndDate = reader.GetValueDateTime("EndDate");
            vm.BidRequest.Description = reader.GetValueText("Description");
            vm.BidRequest.DateAdded = reader.GetValueDateTime("DateAdded");
            vm.BidRequest.LastModificationTime = reader.GetValueDateTime("LastModificationTime");
            vm.BidRequest.ResponseDueDate = reader.GetValueDateTime("RespondByDate");
            vm.BidRequest.ServiceTitle = reader.GetValueText("ServiceName");
            vm.BidRequest.Property = reader.GetValueText("PropertyName");
            vm.BidRequest.NoofBids = reader.GetValueInt("NumberOfUnits");
            vm.BidRequest.Address = reader.GetValueText("Address");
            vm.BidRequest.Address2 = reader.GetValueText("Address2");
            vm.BidRequest.City = reader.GetValueText("City");
            vm.BidRequest.State = reader.GetValueText("State");
            vm.BidRequest.Zip = reader.GetValueText("Zip");
            vm.BidRequest.CompanyName = reader.GetValueText("CompanyName");
            vm.BidRequest.VendorName = reader.GetValueText("VendorName");
            vm.BidRequest.Email = reader.GetValueText("Email");
            vm.BidRequest.CellPhone = reader.GetValueText("CellPhone");
            vm.BidRequest.BidRequestStatus = reader.GetValueInt("BidRequestStatus");
            //vm.BidRequest.BidReqStatus = reader.GetValueText("BidStatus");
            reader.Dispose();
        }

        public virtual bool EditService(int CompnyKey, string servicekey)
        {
            bool status = false;
            try
            {

                string storedProcedure = "site_VendorService_update";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        commandWrapper.AddInputParameter("@ServiceTitle1", SqlDbType.VarChar , servicekey);
                        commandWrapper.AddInputParameter("@vendorkey", SqlDbType.Int, CompnyKey);
                        
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);
                        db.ExecuteNonQuery(commandWrapper);
                        status = (commandWrapper.GetValueInt("@errorCode") == 0);

                        
                    }
                }
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message);
            }

            return status;
        }

        public Int64 RegistrationManagerInviteVendor(VendorManagerVendorModel item, int ResourceKey)
        {
            int status = 0;
            item.Password = System.Configuration.ConfigurationManager.AppSettings["Password"];
            try
            {
                try
                {
                    if (item.ContactPerson != null)
                    {
                        var a = item.ContactPerson.Split(' ')[1];
                    }
                    else
                    {
                        item.ContactPerson += " ";
                    }
                }
                catch
                {
                    item.ContactPerson += " ";
                }
                string storedProcedure = "site_RegistrationManager_InviteVendor_New";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {


                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@Resource", SqlDbType.Int, ResourceKey);
                        commandWrapper.AddInputParameter("@LegalName ", SqlDbType.VarChar, String.IsNullOrEmpty(item.LegalName) ? 0 : item.LegalName.Length, String.IsNullOrEmpty(item.LegalName) ? SqlString.Null : item.LegalName);
                        commandWrapper.AddInputParameter("@TaxID", SqlDbType.VarChar, String.IsNullOrEmpty(item.TaxID) ? 0 : item.TaxID.Length, String.IsNullOrEmpty(item.TaxID) ? SqlString.Null : item.TaxID);
                        commandWrapper.AddInputParameter("@CompanyName ", SqlDbType.VarChar, String.IsNullOrEmpty(item.CompanyName) ? 0 : item.CompanyName.Length, String.IsNullOrEmpty(item.CompanyName) ? SqlString.Null : item.CompanyName);
                        commandWrapper.AddInputParameter("@Address", SqlDbType.VarChar, String.IsNullOrEmpty(item.Address) ? 0 : item.Address.Length, String.IsNullOrEmpty(item.Address) ? SqlString.Null : item.Address);
                        commandWrapper.AddInputParameter("@Address2", SqlDbType.VarChar, String.IsNullOrEmpty(item.Address2) ? 0 : item.Address2.Length, String.IsNullOrEmpty(item.Address2) ? SqlString.Null : item.Address2);
                        commandWrapper.AddInputParameter("@City", SqlDbType.VarChar, String.IsNullOrEmpty(item.City) ? 0 : item.City.Length, String.IsNullOrEmpty(item.City) ? SqlString.Null : item.City);
                        commandWrapper.AddInputParameter("@StateKey", SqlDbType.VarChar, String.IsNullOrEmpty(item.State) ? 0 : item.State.Length, String.IsNullOrEmpty(item.State) ? SqlString.Null : item.State);
                        commandWrapper.AddInputParameter("@Zip", SqlDbType.VarChar, String.IsNullOrEmpty(item.Zip) ? 0 : item.Zip.Length, String.IsNullOrEmpty(item.Zip) ? SqlString.Null : item.Zip);
                        commandWrapper.AddInputParameter("@FirstName", SqlDbType.VarChar, String.IsNullOrEmpty(item.FirstName) ? 0 : item.FirstName.Length, String.IsNullOrEmpty(item.FirstName) ? SqlString.Null : item.FirstName);
                        commandWrapper.AddInputParameter("@LastName", SqlDbType.VarChar, String.IsNullOrEmpty(item.LastName) ? 0 : item.LastName.Length, String.IsNullOrEmpty(item.LastName) ? SqlString.Null : item.LastName);
                        commandWrapper.AddInputParameter("@Work", SqlDbType.VarChar, String.IsNullOrEmpty(item.Work) ? 0 : item.Work.Length, String.IsNullOrEmpty(item.Work) ? SqlString.Null : item.Work);
                        commandWrapper.AddInputParameter("@Work2", SqlDbType.VarChar, String.IsNullOrEmpty(item.Work2) ? 0 : item.Work2.Length, String.IsNullOrEmpty(item.Work2) ? SqlString.Null : item.Work2);
                        commandWrapper.AddInputParameter("@Email", SqlDbType.VarChar, String.IsNullOrEmpty(item.Email) ? 0 : item.Email.Length, String.IsNullOrEmpty(item.Email) ? SqlString.Null : item.Email);
                        commandWrapper.AddInputParameter("@Fax", SqlDbType.VarChar, String.IsNullOrEmpty(item.Fax) ? 0 : item.Fax.Length, String.IsNullOrEmpty(item.Fax) ? SqlString.Null : item.Fax);
                        commandWrapper.AddInputParameter("@Website", SqlDbType.VarChar, String.IsNullOrEmpty(item.Website) ? 0 : item.Website.Length, String.IsNullOrEmpty(item.Website) ? SqlString.Null : item.Website);
                        commandWrapper.AddInputParameter("@ServiceTitle1", SqlDbType.VarChar, String.IsNullOrEmpty(item.ServiceTitle1) ? 0 : item.ServiceTitle1.Length, String.IsNullOrEmpty(item.ServiceTitle1) ? SqlString.Null : item.ServiceTitle1);

                        commandWrapper.AddInputParameter("@Password", SqlDbType.VarChar, item.Password);
                        commandWrapper.AddInputParameter("@Title ", SqlDbType.VarChar, String.IsNullOrEmpty(item.Title) ? 0 : item.Title.Length, String.IsNullOrEmpty(item.Title) ? SqlString.Null : item.Title);

                        commandWrapper.AddInputParameter("@latitude", SqlDbType.Decimal, item.Latitude);
                        commandWrapper.AddInputParameter("@longitude", SqlDbType.Decimal, item.Longitude);
                        commandWrapper.AddInputParameter("@RadiusKey", SqlDbType.VarChar, String.IsNullOrEmpty(item.Radius.ToString()) ? 0 : item.Radius.ToString().Length, String.IsNullOrEmpty(item.Radius.ToString()) ? SqlString.Null : item.Radius.ToString());

                        if (item.CompanyKey != 0)
                        {
                            commandWrapper.AddInputParameter("@CompanyKey", SqlDbType.Int, item.CompanyKey);
                        }
                        commandWrapper.AddInputParameter("@Description", SqlDbType.VarChar, String.IsNullOrEmpty(item.Description) ? 0 : item.Description.Length, String.IsNullOrEmpty(item.Description) ? SqlString.Null : item.Description);

                        commandWrapper.AddOutputParameter("@companyvalue", SqlDbType.Int);
                        db.ExecuteNonQuery(commandWrapper);
                        status = commandWrapper.GetValueInt("@companyvalue");
                        if (status != 0)
                        {

                            VendorInvetationmailsend1(status, item.Email, item.ContactPerson, item.CompanyName, "Invite");
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                // error occured...
                //throw;
            }

            return status;
        }
    }
}
