using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using AssociationBids.GlobalUtilities;
using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base.Code;
using AssociationBids.Portal.Repository.Base.Interface;
using DB_con;

namespace AssociationBids.Portal.Repository.Base
{
    public class BidRequestRepository : BaseRepository, IBidRequestRepository
    {
        ConnectionCls obj_con = null;
        public BidRequestRepository()
        {
            obj_con = new ConnectionCls();
        }

        public BidRequestRepository(string connectionString)
            : base(connectionString) { }



        public virtual bool Create(BidRequestModel item)
        {
            bool status = false;

            try
            {
                string storedProcedure = "gensp_BidRequest_InsertOne";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@propertyKey", SqlDbType.VarChar, (item.PropertyKey == "") ? SqlString.Null : item.PropertyKey);
                        //commandWrapper.AddInputParameter("@propertyKey", SqlDbType.Int, (item.PropertyKey == 0) ? SqlInt32.Null : item.PropertyKey);
                        commandWrapper.AddInputParameter("@resourceKey", SqlDbType.Int, (item.ResourceKey == 0) ? SqlInt32.Null : item.ResourceKey);
                        commandWrapper.AddInputParameter("@serviceKey", SqlDbType.Int, (item.ServiceKey == 0) ? SqlInt32.Null : item.ServiceKey);
                        commandWrapper.AddInputParameter("@title", SqlDbType.VarChar, String.IsNullOrEmpty(item.Title) ? 0 : item.Title.Length, String.IsNullOrEmpty(item.Title) ? SqlString.Null : item.Title);
                        commandWrapper.AddInputParameter("@bidDueDate", SqlDbType.SmallDateTime, (item.BidDueDate == DateTime.MinValue) ? SqlDateTime.Null : item.BidDueDate);
                        commandWrapper.AddInputParameter("@startDate", SqlDbType.SmallDateTime, (item.StartDate == DateTime.MinValue) ? SqlDateTime.Null : item.StartDate);
                        commandWrapper.AddInputParameter("@endDate", SqlDbType.SmallDateTime, (item.EndDate == DateTime.MinValue) ? SqlDateTime.Null : item.EndDate);
                        commandWrapper.AddInputParameter("@description", SqlDbType.VarChar, String.IsNullOrEmpty(item.Description) ? 0 : item.Description.Length, String.IsNullOrEmpty(item.Description) ? SqlString.Null : item.Description);
                        commandWrapper.AddInputParameter("@dateAdded", SqlDbType.DateTime, (item.DateAdded == DateTime.MinValue) ? SqlDateTime.Null : item.DateAdded);
                        commandWrapper.AddInputParameter("@lastModificationTime", SqlDbType.DateTime, (item.LastModificationTime == DateTime.MinValue) ? SqlDateTime.Null : item.LastModificationTime);
                        commandWrapper.AddInputParameter("@bidRequestStatus", SqlDbType.Int, (item.BidRequestStatus == 0) ? SqlInt32.Null : item.BidRequestStatus);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@bidRequestKey", SqlDbType.Int);
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        db.ExecuteNonQuery(commandWrapper);

                        // save the newly inserted key value
                        item.BidRequestKey = commandWrapper.GetValueInt("@bidRequestKey");

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

        public virtual bool Update(BidRequestModel item)
        {
            bool status = false;

            try
            {
                string storedProcedure = "gensp_BidRequest_UpdateOneByBidRequestKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        commandWrapper.AddInputParameter("@bidRequestKey", SqlDbType.Int, (item.BidRequestKey == 0) ? SqlInt32.Null : item.BidRequestKey);
                        commandWrapper.AddInputParameter("@propertyKey", SqlDbType.VarChar, (item.PropertyKey == "") ? SqlString.Null : item.PropertyKey);
                        commandWrapper.AddInputParameter("@serviceKey", SqlDbType.Int, (item.ServiceKey == 0) ? SqlInt32.Null : item.ServiceKey);
                        commandWrapper.AddInputParameter("@title", SqlDbType.VarChar, String.IsNullOrEmpty(item.Title) ? 0 : item.Title.Length, String.IsNullOrEmpty(item.Title) ? SqlString.Null : item.Title);
                        commandWrapper.AddInputParameter("@bidDueDate", SqlDbType.SmallDateTime, (item.BidDueDate == DateTime.MinValue) ? SqlDateTime.Null : item.BidDueDate);
                        commandWrapper.AddInputParameter("@ResponseDueDates", SqlDbType.SmallDateTime, (item.ResponseDueDate == DateTime.MinValue) ? SqlDateTime.Null : item.ResponseDueDate);
                        commandWrapper.AddInputParameter("@description", SqlDbType.VarChar, String.IsNullOrEmpty(item.Description) ? 0 : item.Description.Length, String.IsNullOrEmpty(item.Description) ? SqlString.Null : item.Description);
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


        public virtual bool Delete(int id)
        {
            bool status = false;

            try
            {
                string storedProcedure = "gensp_BidRequest_DeleteOneByBidRequestKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@bidRequestKey", SqlDbType.Int, id);

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

        public virtual BidRequestModel Get(int id)
        {
            BidRequestModel item = null;

            try
            {
                string storedProcedure = "gensp_BidRequest_SelectOneByBidRequestKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@bidRequestKey", SqlDbType.Int, id);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            while (dataReader.Read())
                            {
                                item = new BidRequestModel();

                                Load(dataReader, item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_BidRequest_SelectOneByBidRequestKey");
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

        public virtual IList<BidRequestModel> GetAll()
        {
            return GetAll(new BidRequestFilterModel());
        }

        public virtual IList<BidRequestModel> GetAll(BidRequestFilterModel filter)
        {
            List<BidRequestModel> itemList = new List<BidRequestModel>();

            try
            {
                string storedProcedure = "gensp_BidRequest_SelectSomeBySearch";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@propertyKey", SqlDbType.Int, (filter.PropertyKey == 0) ? SqlInt32.Null : filter.PropertyKey);
                        commandWrapper.AddInputParameter("@resourceKey", SqlDbType.Int, (filter.ResourceKey == 0) ? SqlInt32.Null : filter.ResourceKey);
                        commandWrapper.AddInputParameter("@serviceKey", SqlDbType.Int, (filter.ServiceKey == 0) ? SqlInt32.Null : filter.ServiceKey);
                        commandWrapper.AddInputParameter("@bidRequestStatus", SqlDbType.Int, (filter.BidRequestStatus == 0) ? SqlInt32.Null : filter.BidRequestStatus);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            BidRequestModel item = null;
                            while (dataReader.Read())
                            {
                                item = new BidRequestModel();

                                Load(dataReader, item);

                                itemList.Add(item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_BidRequest_SelectSomeBySearch");
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

        public virtual IList<BidRequestModel> GetAll(BidRequestFilterModel filter, PagingModel paging)
        {
            List<BidRequestModel> itemList = new List<BidRequestModel>();

            try
            {
                string storedProcedure = "gensp_BidRequest_SelectSomeBySearchAndPaging";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@propertyKey", SqlDbType.Int, (filter.PropertyKey == 0) ? SqlInt32.Null : filter.PropertyKey);
                        commandWrapper.AddInputParameter("@resourceKey", SqlDbType.Int, (filter.ResourceKey == 0) ? SqlInt32.Null : filter.ResourceKey);
                        commandWrapper.AddInputParameter("@serviceKey", SqlDbType.Int, (filter.ServiceKey == 0) ? SqlInt32.Null : filter.ServiceKey);
                        commandWrapper.AddInputParameter("@bidRequestStatus", SqlDbType.Int, (filter.BidRequestStatus == 0) ? SqlInt32.Null : filter.BidRequestStatus);

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
                            BidRequestModel item = null;
                            while (dataReader.Read())
                            {
                                item = new BidRequestModel();

                                Load(dataReader, item);

                                itemList.Add(item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_BidRequest_SelectSomeBySearchAndPaging");
                        }

                        // set total record count for paging
                        paging.TotalRecordCount = commandWrapper.GetValueInt("@totalRecordCount");
                    }
                }
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
            }

            return itemList;
        }

        protected void Load(DBDataReader dataReader, BidRequestModel item)
        {
            item.BidRequestKey = dataReader.GetValueInt("BidRequestKey");
            item.PropertyKey = dataReader.GetValueText("PropertyKey");
            item.ResourceKey = dataReader.GetValueInt("ResourceKey");
            item.ServiceKey = dataReader.GetValueInt("ServiceKey");
            item.Title = dataReader.GetValueText("Title");
            item.BidDueDate = dataReader.GetValueDateTime("BidDueDate");
            item.StartDate = dataReader.GetValueDateTime("StartDate");
            item.EndDate = dataReader.GetValueDateTime("EndDate");
            item.Description = dataReader.GetValueText("Description");
            item.DateAdded = dataReader.GetValueDateTime("DateAdded");
            item.LastModificationTime = dataReader.GetValueDateTime("LastModificationTime");
            item.BidRequestStatus = dataReader.GetValueInt("BidRequestStatus");
        }

        public List<BidRequestModel> SearchBidRequest(long PageSize, long PageIndex, string Search, string Sort, Int32 Resourcekey, Int32 PropertyKey, string BidRequestStatus, int Modulekey)
        {
            List<BidRequestModel> itemList = new List<BidRequestModel>();
            try
            {
                int BidRequestStatus1 = 0;
                if (BidRequestStatus == "600, 601") { BidRequestStatus1 = 1; }
                else if (BidRequestStatus == "602, 603") { BidRequestStatus1 = 2; }
                else if (BidRequestStatus == "0,0") { BidRequestStatus1 = 0; }
                //int BidRequestStatus1 = Convert.ToInt32(BidRequestStatus.Split(',')[0]);
                //int BidRequestStatus2 = Convert.ToInt32(BidRequestStatus.Split(',')[1]);
                string storedProcedure = "site_BidRequest_SelectIndexPagingBidRequest_New_Copy";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters 
                        commandWrapper.AddInputParameter("@PageSize", SqlDbType.BigInt, (PageSize == 0) ? 0 : PageSize);
                        commandWrapper.AddInputParameter("@PageIndex", SqlDbType.BigInt, (PageIndex == 0) ? 0 : PageIndex);
                        commandWrapper.AddInputParameter("@Search", SqlDbType.NText, (Search == "") ? "" : Search);
                        commandWrapper.AddInputParameter("@Sort", SqlDbType.NText, (Sort == "") ? "" : Sort);
                        commandWrapper.AddInputParameter("@Resourcekey", SqlDbType.Int, (Resourcekey == 0) ? 0 : Resourcekey);
                        commandWrapper.AddInputParameter("@PropertyKey", SqlDbType.Int, (PropertyKey == 0) ? 0 : PropertyKey);
                        commandWrapper.AddInputParameter("@Modulekey", SqlDbType.Int, Modulekey);
                        commandWrapper.AddInputParameter("@BidRequestStatus", SqlDbType.Int, (BidRequestStatus1 == 0) ? 0 : BidRequestStatus1);
                        //commandWrapper.AddInputParameter("@BidRequestStatus1", SqlDbType.Int, (BidRequestStatus2 == 0) ? 0 : BidRequestStatus2);
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            BidRequestModel item = null;
                            while (dataReader.Read())
                            {
                                item = new BidRequestModel();
                                LoadStaff(dataReader, item);
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

        public List<BidRequestModel> SearchBidRequestPriority(long PageSize, long PageIndex, string Search, string Sort, Int32 Resourcekey, Int32 PropertyKey, string BidRequestStatus, int Modulekey)
        {
            List<BidRequestModel> itemList = new List<BidRequestModel>();
            try
            {
                int BidRequestStatus1 = 0;
                if (BidRequestStatus == "600, 601") { BidRequestStatus1 = 1; }
                else if (BidRequestStatus == "602, 603") { BidRequestStatus1 = 2; }
                else if (BidRequestStatus == "0,0") { BidRequestStatus1 = 0; }
                //int BidRequestStatus1 = Convert.ToInt32(BidRequestStatus.Split(',')[0]);
                //int BidRequestStatus2 = Convert.ToInt32(BidRequestStatus.Split(',')[1]);
                string storedProcedure = "site_BidRequest_SelectIndexPagingBidRequestPriority";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters 
                        commandWrapper.AddInputParameter("@PageSize", SqlDbType.BigInt, (PageSize == 0) ? 0 : PageSize);
                        commandWrapper.AddInputParameter("@PageIndex", SqlDbType.BigInt, (PageIndex == 0) ? 0 : PageIndex);
                        commandWrapper.AddInputParameter("@Search", SqlDbType.NText, (Search == "") ? "" : Search);
                        commandWrapper.AddInputParameter("@Sort", SqlDbType.NText, (Sort == "") ? "" : Sort);
                        commandWrapper.AddInputParameter("@Resourcekey", SqlDbType.Int, (Resourcekey == 0) ? 0 : Resourcekey);
                        commandWrapper.AddInputParameter("@PropertyKey", SqlDbType.Int, (PropertyKey == 0) ? 0 : PropertyKey);
                        commandWrapper.AddInputParameter("@Modulekey", SqlDbType.Int, Modulekey);
                        commandWrapper.AddInputParameter("@BidRequestStatus", SqlDbType.Int, (BidRequestStatus1 == 0) ? 0 : BidRequestStatus1);
                        //commandWrapper.AddInputParameter("@BidRequestStatus1", SqlDbType.Int, (BidRequestStatus2 == 0) ? 0 : BidRequestStatus2);
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            BidRequestModel item = null;
                            while (dataReader.Read())
                            {
                                item = new BidRequestModel();
                                LoadStaff(dataReader, item);
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


        public List<BidRequestModel> SearchBidRequestVender(long PageSize, long PageIndex, string Search, string Sort, long BidRequestKey)
        {
            List<BidRequestModel> itemList = new List<BidRequestModel>();
            try
            {
                string storedProcedure = "site_BidRequest_SelectIndexPagingBidRequestVender";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters 
                        commandWrapper.AddInputParameter("@PageSize", SqlDbType.BigInt, (PageSize == 0) ? 0 : PageSize);
                        commandWrapper.AddInputParameter("@PageIndex", SqlDbType.BigInt, (PageIndex == 0) ? 0 : PageIndex);
                        commandWrapper.AddInputParameter("@Search", SqlDbType.NText, (Search == "") ? "" : Search);
                        commandWrapper.AddInputParameter("@Sort", SqlDbType.NText, (Sort == "") ? "" : Sort);
                        commandWrapper.AddInputParameter("@BidRequestKey", SqlDbType.BigInt, (BidRequestKey == 0) ? 0 : BidRequestKey);
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            //DataTable dataTable = ConvertDatareadertoDataTable(dataReader);
                            BidRequestModel item = null;
                            while (dataReader.Read())
                            {

                                item = new BidRequestModel();
                                LoadVender(dataReader, item);
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
        public string SearchBidRequestVenderjson(long PageSize, long PageIndex, string Search, string Sort, long BidVendorKey, Int64 CompanyKey, Int64 UserId, string BidRequestStatus, string BiddueDateFrom, string BiddueDateTo, string ModuleController, long ResourceKey)
        {
            try
            {
                int BidRequestStatus1 = 0;
                if (BidRequestStatus == "700, 701, 702")
                {
                    BidRequestStatus1 = 1;
                }
                else if (BidRequestStatus == "703, 704, 803")
                {
                    BidRequestStatus1 = 2;
                }
                if (ModuleController == "WorkOrder")
                {
                    if (BidRequestStatus == "700, 701, 702")
                    {
                        BidRequestStatus1 = 3;
                    }


                }
                obj_con.clearParameter();
                obj_con.addParameter("@PageSize", string.IsNullOrEmpty(Convert.ToString(PageSize)) ? 0 : PageSize);
                obj_con.addParameter("@PageIndex", string.IsNullOrEmpty(Convert.ToString(PageIndex)) ? 0 : PageIndex);
                obj_con.addParameter("@Search", Search);
                obj_con.addParameter("@Sort", Sort);
                obj_con.addParameter("@BidVendorKey", string.IsNullOrEmpty(Convert.ToString(BidVendorKey)) ? 0 : BidVendorKey);
                obj_con.addParameter("@CompanyKey", string.IsNullOrEmpty(Convert.ToString(CompanyKey)) ? 0 : CompanyKey);
                obj_con.addParameter("@UserId", string.IsNullOrEmpty(Convert.ToString(UserId)) ? 0 : UserId);
                obj_con.addParameter("@BidRequestStatus", string.IsNullOrEmpty(Convert.ToString(BidRequestStatus1)) ? 0 : BidRequestStatus1);
                obj_con.addParameter("@BiddueDateFrom", BiddueDateFrom);
                obj_con.addParameter("@BiddueDateTo", BiddueDateTo);
                obj_con.addParameter("@ModuleController", ModuleController);
                obj_con.addParameter("@ResourceKey", ResourceKey);
                DataTable dt = obj_con.ConvertDatareadertoDataTable(obj_con.ExecuteReader("site_BidRequest_SelectIndexPagingBidRequestVender_Copy", CommandType.StoredProcedure));
                obj_con.CommitTransaction();
                obj_con.closeConnection();
                DataColumn col1 = new DataColumn();
                col1.ColumnName = "ActAmount";
                dt.Columns.Add(col1);
                if (dt != null && dt.Rows.Count == 1)
                {

                    DataColumn col = new DataColumn();
                    col.ColumnName = "isExpired";
                    dt.Columns.Add(col);
                    var resDate = dt.Rows[0]["VendorResponseDueDate"];
                    if (Convert.ToDateTime(resDate) < Convert.ToDateTime(DateTime.Today.ToString("MM/dd/yyyy")))
                    {
                        dt.Rows[0]["isExpired"] = true;
                    }
                    else
                    {
                        dt.Rows[0]["isExpired"] = false;
                    }
                    var TotalAmt = dt.Rows[0]["TotalamountAccpect"];




                    if (TotalAmt != null)
                    {
                        string Amt = Convert.ToString(TotalAmt);
                        Amt = Common.Utility.FormatNumberHelper(Amt, true);

                        dt.Rows[0]["ActAmount"] = Amt;
                       
                    }
                    else if (TotalAmt == null)
                    {
                        dt.Rows[0]["ActAmount"] = "";
                        
                    }
                }


                else if (dt != null)
                {
                    foreach (DataRow dtRow in dt.Rows)
                    {


                        var TotalAmt = dtRow["TotalamountAccpect"];

                        string Amt = Convert.ToString(TotalAmt);
                        Amt = Common.Utility.FormatNumberHelper(Amt, true);
                        dtRow["ActAmount"] = Amt;


                    }
                }



                return AssoBidsUtility.ConvertDataTableTojSonString(dt);
            }
            catch (Exception ex)
            {
                throw new Exception("site_BidRequestDoc_SelectIndexPagingBidRequestVender" + ex.ToString());
            }
        }
        public string SearchBidRequestVenderjsonPriority(long PageSize, long PageIndex, string Search, string Sort, long BidVendorKey, Int64 CompanyKey, Int64 UserId, string BidRequestStatus, string BiddueDateFrom, string BiddueDateTo, string ModuleController, long ResourceKey)
        {
            try
            {
                int BidRequestStatus1 = 0;
                if (BidRequestStatus == "700, 701, 702")
                {
                    BidRequestStatus1 = 1;
                }
                else if (BidRequestStatus == "703, 803")
                {
                    BidRequestStatus1 = 2;
                }

                if (ModuleController == "WorkOrder")
                {
                    if (BidRequestStatus == "700, 701, 702")
                    {
                        BidRequestStatus1 = 3;
                    }


                }

                obj_con.clearParameter();
                obj_con.addParameter("@PageSize", string.IsNullOrEmpty(Convert.ToString(PageSize)) ? 0 : PageSize);
                obj_con.addParameter("@PageIndex", string.IsNullOrEmpty(Convert.ToString(PageIndex)) ? 0 : PageIndex);
                obj_con.addParameter("@Search", Search);
                obj_con.addParameter("@Sort", Sort);
                obj_con.addParameter("@BidVendorKey", string.IsNullOrEmpty(Convert.ToString(BidVendorKey)) ? 0 : BidVendorKey);
                obj_con.addParameter("@CompanyKey", string.IsNullOrEmpty(Convert.ToString(CompanyKey)) ? 0 : CompanyKey);
                obj_con.addParameter("@UserId", string.IsNullOrEmpty(Convert.ToString(UserId)) ? 0 : UserId);
                obj_con.addParameter("@BidRequestStatus", string.IsNullOrEmpty(Convert.ToString(BidRequestStatus1)) ? 0 : BidRequestStatus1);
                obj_con.addParameter("@BiddueDateFrom", BiddueDateFrom);
                obj_con.addParameter("@BiddueDateTo", BiddueDateTo);
                obj_con.addParameter("@ModuleController", ModuleController);
                obj_con.addParameter("@ResourceKey", ResourceKey);
                DataTable dt = obj_con.ConvertDatareadertoDataTable(obj_con.ExecuteReader("site_BidRequest_SelectIndexPagingBidRequestVenderPriority", CommandType.StoredProcedure));
                obj_con.CommitTransaction();
                obj_con.closeConnection();
                DataColumn col1 = new DataColumn();
                col1.ColumnName = "ActAmount";
                dt.Columns.Add(col1);
                if (dt != null && dt.Rows.Count == 1)
                {

                    DataColumn col = new DataColumn();
                    col.ColumnName = "isExpired";
                    dt.Columns.Add(col);
                    //var resDate = dt.Rows[0]["VendorResponseDueDate"];
                    var resDate = dt.Rows[0]["VendorRespondDueDate"];
                    
                    if (Convert.ToDateTime(resDate) < Convert.ToDateTime(DateTime.Today.ToString("MM/dd/yyyy")))
                    {
                        dt.Rows[0]["isExpired"] = true;
                    }
                    else
                    {
                        dt.Rows[0]["isExpired"] = false;
                    }
                    var TotalAmt = dt.Rows[0]["TotalamountAccpect"];




                    if (TotalAmt != null)
                    {
                        string Amt = Convert.ToString(TotalAmt);
                        Amt = Common.Utility.FormatNumberHelper(Amt, true);

                        dt.Rows[0]["ActAmount"] = Amt;
                       

                    }
                    else if (TotalAmt == null)
                    {
                        dt.Rows[0]["ActAmount"] = "";
                        
                    }




                }



                else if (dt != null)
                {
                    foreach (DataRow dtRow in dt.Rows)
                    {


                        var TotalAmt = dtRow["TotalamountAccpect"];

                        string Amt = Convert.ToString(TotalAmt);
                        Amt = Common.Utility.FormatNumberHelper(Amt, true);
                        dtRow["ActAmount"] = Amt;


                    }
                }




                return AssoBidsUtility.ConvertDataTableTojSonString(dt);
            }
            catch (Exception ex)
            {
                throw new Exception("site_BidRequestDoc_SelectIndexPagingBidRequestVender" + ex.ToString());
            }
        }

        public string SearchBidRequestVenderDocjson(long PageSize, long PageIndex, string Search, string Sort, long BidVendorKey, string TableName)
        {
            try
            {
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
        public string ApceptRejectVenderBidrequest(Int64 BidVendorKey, string Status, string IsAssigned)
        {

            try
            {
                obj_con.clearParameter();
                obj_con.BeginTransaction();
                obj_con.addParameter("@BidVendorKey", string.IsNullOrEmpty(Convert.ToString(BidVendorKey)) ? 0 : BidVendorKey);
                obj_con.addParameter("@Status", string.IsNullOrEmpty(Convert.ToString(Status)) ? "" : Status);

                //obj_con.ExecuteNoneQuery("site_BidVendor_StatusUpdate", CommandType.StoredProcedure);
                obj_con.ExecuteNoneQuery("site_BidVendor_StatusUpdate_New", CommandType.StoredProcedure);

                obj_con.CommitTransaction();
                obj_con.closeConnection();

                if (Status == "Not Interested")
                {
                    if (IsAssigned == "true")
                    {
                        mailsend(Convert.ToInt32(BidVendorKey), "Rejectp", "", "", "", "");
                    }
                }

                //mailsend(Convert.ToInt32(BidVendorKey), "Accept");

                return "Success";
            }
            catch (Exception ex)
            {
                obj_con.RollbackTransaction();
                throw new Exception("site_BidVendor_StatusUpdate" + ex.ToString());
            }
        }

        public virtual BidRequestModel mailsend(int BidVendorKey, string Status, string MessageBody, string Property, string ContactDetail, string BidAmounts)
        {
            BidRequestModel item = null;

            try
            {
                EmailSendModel email = new EmailSendModel();
                string storedProcedure = "site_User_GetForAdminMail";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        if (Status == "Rejectp")
                        {
                            commandWrapper.AddInputParameter("@BidVendorKey", SqlDbType.Int, BidVendorKey);
                            //commandWrapper.AddInputParameter("@Status", SqlDbType.NVarChar, "Vendor");
                            commandWrapper.AddInputParameter("@Status", SqlDbType.NVarChar, "PropertyManager");
                        }
                        if (Status == "Acceptp")
                        {
                            commandWrapper.AddInputParameter("@BidVendorKey", SqlDbType.Int, BidVendorKey);
                            commandWrapper.AddInputParameter("@Status", SqlDbType.NVarChar, "Vendor");
                        }
                        if (Status == "Cancel")
                        {
                            commandWrapper.AddInputParameter("@BidVendorKey", SqlDbType.Int, BidVendorKey);
                            //commandWrapper.AddInputParameter("@Status", SqlDbType.NVarChar, "PropertyManager");
                            commandWrapper.AddInputParameter("@Status", SqlDbType.NVarChar, "Vendor");
                        }
                        if (Status == "Vendor")
                        {
                            commandWrapper.AddInputParameter("@BidVendorKey", SqlDbType.Int, BidVendorKey);
                            commandWrapper.AddInputParameter("@Status", SqlDbType.NVarChar, "PropertyManager");
                        }
                        // add stored procedure output parameters
                        //commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);
                        List<EmailSendModel> emailItems = new List<EmailSendModel>();

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {

                            while (dataReader.Read())
                            {
                                item = new BidRequestModel();

                                LoadViewEdit1(dataReader, item);
                                if (Status == "Accept")
                                {
                                    //mailsendAsync(item.Email, item.FirstName, item.CompanyName, item.BidName, Status);
                                    email = CreateMailTemplate(item.Email, item.FirstName, item.CompanyName, item.BidName, Status, Property, ContactDetail, BidAmounts, MessageBody);
                                }
                                else if (Status == "Cancel")
                                {
                                    //mailsendAsync(item.EmailId, item.FirstName, item.CompanyName, item.BidName, Status);
                                    email = CreateMailTemplate(item.EmailId, item.FirstName, item.CompanyName, item.BidName, Status, Property, ContactDetail, BidAmounts, MessageBody);
                                }
                                else if (Status == "Rejectp")
                                {
                                    Status = "VendoMessage";
                                    //mailsendAsync(item.EmailId, item.FirstName, item.CompanyName, item.BidName, Status);
                                    email = CreateMailTemplate(item.EmailId, item.FirstName, item.CompanyName, item.BidName, Status, Property, ContactDetail, BidAmounts, MessageBody);
                                }
                                else if (Status == "Acceptp")
                                {
                                    Status = "Accept";
                                    //mailsendAsync(item.EmailId, item.FirstName, item.CompanyName, item.BidName, Status);
                                    email = CreateMailTemplate(item.EmailId, item.FirstName, item.CompanyName, item.BidName, Status, Property, ContactDetail, BidAmounts, MessageBody);
                                }
                                if (Status == "Vendor")
                                {
                                    email = CreateMailTemplate(item.EmailId, item.FirstName, item.CompanyName, item.BidName, Status, Property, ContactDetail, BidAmounts, MessageBody);
                                }
                                //if (Status == "Rejectp")
                                //{
                                //    Status = "VendoMessage";
                                //    email = CreateMailTemplate(item.EmailId, item.FirstName, item.CompanyName, item.BidName, Status);
                                //}
                                //else
                                //{
                                //    email = CreateMailTemplate(item.EmailId, item.FirstName, item.CompanyName, item.BidName, Status);
                                //}
                                
                                EmailRepository emailRepos = new EmailRepository();
                                EmailModel emailModel = new EmailModel();
                                PropertyRepository ProRepos = new PropertyRepository();
                                
                                if (email != null && email.To != "" && email.From != "")
                                {
                                    int bidVendorId = item.BidVendorKey == 0 ? BidVendorKey : item.BidVendorKey;
                                    var BidVendor = GetBidVendorByBidVendorKey(bidVendorId);
                                    var BidRequest = GetDataBidRequestViewEdit(BidVendor.BidRequestKey);
                                    var property = ProRepos.Get(Convert.ToInt32(BidRequest.PropertyKey));
                                    VendorManagerRepository res = new VendorManagerRepository();
                                    var Resource = res.GetResourceForInviteVendor(property.CompanyKey);
                                    emailModel.Body = email .Body;
                                    emailModel.DateAdded = DateTime.Now;
                                    emailModel.DateSent = DateTime.Now;
                                    emailModel.From = email.From;
                                    emailModel.Subject = email.Subject;
                                    emailModel.To = email.To;
                                    emailModel.ModuleKey = BidRequest.ModuleKey;
                                    emailModel.ObjectKey = BidVendor.BidVendorKey;
                                    emailModel.ResourceKey = Resource.ResourceKey;
                                    emailModel.EmailStatus = 500;

                                    bool isInserted = emailRepos.Create(emailModel);
                                    if (isInserted)
                                        email.EmailKey = emailModel.EmailKey;
                                    emailItems.Add(email);
                                }
                            }
                        }

                        if (emailItems.Count > 0)
                        {
                            Task.Run(() => SendMailAsync(emailItems)).ConfigureAwait(false);
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
        private async Task SendMailAsync(List<EmailSendModel> sendModel)
        {
            EmailRepository emailRepos = new EmailRepository();
            EmailModel email = null;
            try
            {
                foreach (var emailItem in sendModel)
                {
                    MailMessage msg = new MailMessage();

                    msg.From = new MailAddress(emailItem.From);
                    msg.To.Add(emailItem.To);
                    msg.Subject = emailItem.Subject;
                    //msg.Body += "Click the following link to Reset your password.";
                    //msg.Body += Environment.NewLine;
                    msg.IsBodyHtml = emailItem.IsHtml;

                    msg.Body = emailItem.Body;
                    email = emailRepos.Get(Convert.ToInt32(emailItem.EmailKey));

                    using (SmtpClient client = new SmtpClient())
                    {
                        client.EnableSsl = false;
                        client.UseDefaultCredentials = false;
                        client.Credentials = new NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["smtpUser"], System.Configuration.ConfigurationManager.AppSettings["smtpPass"]);
                        client.Host = System.Configuration.ConfigurationManager.AppSettings["smtpServer"];
                        client.Port = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["smtpPort"]);
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        await client.SendMailAsync(msg);
                        email.EmailStatus = 502;
                        emailRepos.Update(email);
                    }
                }
            }
            catch (Exception ex)
            {
                if(email != null)
                {
                    email.EmailStatus = 501;
                    emailRepos.Update(email);
                }
            }
        }
        protected void LoadViewEdit1(DBDataReader dataReader, BidRequestModel item)
        {
            item.FirstName = dataReader.GetValueText("FirstName");
            item.EmailId = dataReader.GetValueText("Email");
            item.CompanyName = dataReader.GetValueText("CompanyName");
            item.BidName = dataReader.GetValueText("BidName");
        }

        public void mailsendAsync(string fromemail, string Name, string CompanyName, string BidName, string Status)
        {
            try
            {
                IList<Model.EmailTemplateModel> EmailTemplate = null;
                MailMessage msg = new MailMessage();
                string LinkUrl = System.Configuration.ConfigurationManager.AppSettings["LinkUrl"];
                string strBody = string.Empty;
                EmailTemplate = GetAll1(Status);
                string body = EmailTemplate[0].Body;
                body = body.Replace("[MemberName]", Convert.ToString(Name.ToString().Trim()));
                body = body.Replace("[CompanyName]", Convert.ToString(CompanyName.ToString().Trim()));
                body = body.Replace("[BidName]", Convert.ToString(BidName.ToString().Trim()));
                body = body.Replace("[FirstName]", Convert.ToString(Name.ToString().Trim()));
                body = body.Replace("[EmailAdderess]", Convert.ToString(fromemail.ToString().Trim()));
                body = body.Replace("[BidName]", Convert.ToString(BidName.ToString().Trim()));
                //body = body.Replace("[Property]", Convert.ToString(Property.ToString().Trim()));
                //body = body.Replace("[Property]", Convert.ToString(PropertyManager.ToString().Trim()));

                string Subject = EmailTemplate[0].EmailSubject;
                Subject = Subject.Replace("[BidName]", Convert.ToString(BidName.ToString().Trim()));
                Subject = Subject.Replace("[CompanyName]", Convert.ToString(CompanyName.ToString().Trim()));

                msg.From = new MailAddress(System.Configuration.ConfigurationManager.AppSettings["senderemail"]);
                msg.To.Add(fromemail);

                msg.Subject = Subject;

                msg.IsBodyHtml = true;

                msg.Body += body;


                using (SmtpClient client = new SmtpClient())
                {
                    client.EnableSsl = false;
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

        public EmailSendModel CreateMailTemplate(string fromemail, string Name, string CompanyName, string BidName, string Status, string Property, string PropertyContactDetail, string BidAmounts, string MessageBody)
        {
            EmailSendModel msg = null;
            try
            {

                msg = new EmailSendModel();
                IList<Model.EmailTemplateModel> EmailTemplate = null;

                string LinkUrl = System.Configuration.ConfigurationManager.AppSettings["LinkUrl"];
                string strBody = string.Empty;

                EmailTemplate = GetAll1(Status);

                string body = EmailTemplate[0].Body;

                body = body.Replace("[MemberName]", Convert.ToString(Name.ToString().Trim()));
                body = body.Replace("[CompanyName]", Convert.ToString(CompanyName.ToString().Trim()));
                body = body.Replace("[BidName]", Convert.ToString(BidName.ToString().Trim()));
                body = body.Replace("[FirstName]", Convert.ToString(Name.ToString().Trim()));
                body = body.Replace("[EmailAdderess]", Convert.ToString(fromemail.ToString().Trim()));
                body = body.Replace("[BidName]", Convert.ToString(BidName.ToString().Trim()));
                body = body.Replace("[Property]", Convert.ToString(Property.ToString().Trim()));
                body = body.Replace("[PropertyContactDetail]", Convert.ToString(PropertyContactDetail.ToString().Trim()));
                body = body.Replace("[Title]", Convert.ToString(BidName.ToString().Trim()));
                body = body.Replace("[ChargeAmount]", Convert.ToString(BidAmounts.ToString().Trim()));
                body = body.Replace("[MessageBody]", Convert.ToString(MessageBody.ToString().Trim()));

                string Subject = EmailTemplate[0].EmailSubject;
                Subject = Subject.Replace("[MemberName]", Convert.ToString(Name.ToString().Trim()));
                Subject = Subject.Replace("[BidName]", Convert.ToString(BidName.ToString().Trim()));
                Subject = Subject.Replace("[CompanyName]", Convert.ToString(CompanyName.ToString().Trim()));

                msg.From = System.Configuration.ConfigurationManager.AppSettings["senderemail"];
                msg.To = fromemail;
                msg.Subject = Subject;

                msg.IsHtml = true;

                msg.Body += body;

            }
            catch (Exception ex)
            {
            }
            return msg;
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
                        if (Status == "Accept")
                        {
                            commandWrapper.AddInputParameter("@LookUpTitle", SqlDbType.NText, "Bid award");
                        }
                        else if (Status == "Cancel")
                        {
                            commandWrapper.AddInputParameter("@LookUpTitle", SqlDbType.NText, "Bid Reject Email");
                        }
                        else if (Status == "VendoMessage")
                        {
                            commandWrapper.AddInputParameter("@LookUpTitle", SqlDbType.NText, "Bid Rejection");
                        }
                        else if (Status == "Vendor")
                        {
                            commandWrapper.AddInputParameter("@LookUpTitle", SqlDbType.NText, "Comments Email");
                        }
                        else if (Status == "Date Extension")
                        {
                            commandWrapper.AddInputParameter("@LookUpTitle", SqlDbType.NText, "Bid Date Extension");
                        }
                        else if (Status == "Bid Date Extended")
                        {
                            commandWrapper.AddInputParameter("@LookUpTitle", SqlDbType.NText, "Bid Date Extended");
                        }
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



        public string SendInsertMessage(string Body, string Status, Int64 ObjectKey, Int64 ResourceKey, string ModuleKeyName, string Title)
        {

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
                obj_con.closeConnection();

                if (Title == "Vendor")
                {
                    mailsend(Convert.ToInt32(ObjectKey), Title, Body, "", "", "");
                }

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

        public int GetMaxBidRequestKey( string  Title)
        {
            int BidKey = 0;
            try
            {
                obj_con.clearParameter();
                obj_con.BeginTransaction();
                obj_con.addParameter("@Title", Title);
                obj_con.addParameter("@BidKey", BidKey, DBTrans.Insert);
                obj_con.ExecuteNoneQuery("site_Bid_GetMaxBidRequestKey", CommandType.StoredProcedure);
                obj_con.CommitTransaction();
                obj_con.closeConnection();

                return BidKey = Convert.ToInt32(obj_con.getValue("@BidKey"));
               

               
            }
            catch (Exception ex)
            {
                obj_con.RollbackTransaction();
                throw new Exception("site_Bid_GetMaxBidRequestKey" + ex.ToString());
            }
        }

        public int DeleteDoc(string title, string FileName)
        {
            int BidKey = 0;
            try
            {
                obj_con.clearParameter();
                obj_con.BeginTransaction();
                obj_con.addParameter("@Title", title);
                obj_con.addParameter("@Filename", FileName);
                obj_con.addParameter("@BidKey", BidKey, DBTrans.Insert);
                obj_con.ExecuteNoneQuery("site_Bid_DeleteDoc", CommandType.StoredProcedure);
                obj_con.CommitTransaction();
                obj_con.closeConnection();

                return BidKey = Convert.ToInt32(obj_con.getValue("@BidKey"));



            }
            catch (Exception ex)
            {
                obj_con.RollbackTransaction();
                throw new Exception("site_Bid_DeleteDoc" + ex.ToString());
            }
        }

        public Int64 SubmitVenderBid(Int64 BidVendorKey, string Status, Int64 ResourceKey, string Title, string Total, string Description, string LastModificationTime)
        {
            Int64 BidKey = 0;
            try
            {
                obj_con.clearParameter();
                obj_con.BeginTransaction();
                obj_con.addParameter("@BidVendorKey", string.IsNullOrEmpty(Convert.ToString(BidVendorKey)) ? 0 : BidVendorKey);
                obj_con.addParameter("@Status", string.IsNullOrEmpty(Convert.ToString(Status)) ? "" : Status);
                obj_con.addParameter("@ResourceKey", string.IsNullOrEmpty(Convert.ToString(ResourceKey)) ? 0 : ResourceKey);
                obj_con.addParameter("@Title", string.IsNullOrEmpty(Convert.ToString(Title)) ? "" : Title);
                obj_con.addParameter("@Total", string.IsNullOrEmpty(Convert.ToString(Total)) ? "" : Total);
                obj_con.addParameter("@Description", string.IsNullOrEmpty(Convert.ToString(Description)) ? "" : Description);
                obj_con.addParameter("@LastModificationTime", string.IsNullOrEmpty(Convert.ToString(LastModificationTime)) ? "" : LastModificationTime);
                obj_con.addParameter("@BidKey", BidKey, DBTrans.Insert);
                obj_con.ExecuteNoneQuery("site_Bid_InsertOrUpdates", CommandType.StoredProcedure);
                obj_con.CommitTransaction();
                obj_con.closeConnection();
                return BidKey = Convert.ToInt64(obj_con.getValue("@BidKey"));
            }
            catch (Exception ex)
            {
                obj_con.RollbackTransaction();
                throw new Exception("site_Bid_InsertOrUpdates" + ex.ToString());
            }
        }
        public string GetVenderBidList(Int64 BidVendorKey, Int64 ResourceKey)
        {
            try
            {
                obj_con.clearParameter();
                obj_con.addParameter("@BidVendorKey", BidVendorKey);
                obj_con.addParameter("@ResourceKey", string.IsNullOrEmpty(Convert.ToString(ResourceKey)) ? 0 : ResourceKey);
                DataTable dt = obj_con.ConvertDatareadertoDataTable(obj_con.ExecuteReader("site_Bid_ListSelectIndexPaging", CommandType.StoredProcedure));
                obj_con.CommitTransaction();
                obj_con.closeConnection();

                DataColumn col1 = new DataColumn();
                col1.ColumnName = "ActAmount";
                dt.Columns.Add(col1);
                if (dt != null && dt.Rows.Count == 1)
                {

                  
                    var TotalAmt = dt.Rows[0]["Total"];
                    int amount = Convert.ToInt32(TotalAmt);



                    if (TotalAmt != null && amount != 0)
                    {
                        string Amt = Convert.ToString(TotalAmt);
                        Amt = Common.Utility.FormatNumberHelper(Amt, true);

                        dt.Rows[0]["ActAmount"] = Amt;

                    }
                    else if (TotalAmt == null || amount == 0)
                    {
                        dt.Rows[0]["ActAmount"] = "";

                    }
                }

                return AssoBidsUtility.ConvertDataTableTojSonString(dt);
            }
            catch (Exception ex)
            {
                throw new Exception("site_Bid_ListSelectIndexPaging" + ex.ToString());
            }
        }
        public BidRequestModel bindvendorinformation(int BidRequestKey)
        {
            BidRequestModel item = new BidRequestModel();
            try
            {
                string storedProcedure = "site_Vendorinformaion_BidRequest";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        commandWrapper.AddInputParameter("@BidRequestKey", SqlDbType.Int, BidRequestKey);
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {

                            while (dataReader.Read())
                            {
                                item = new BidRequestModel();
                                BindVendorinformation(dataReader, item);

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
        protected void LoadVendorByBidRequestEmail(DBDataReader dataReader, BidRequestModel item)
        {
            int bidrequest = item.BidRequestKey;
            item.CompanyName = dataReader.GetValueText("Name");
            item.VendorName = dataReader.GetValueText("ContactPerson");
            item.ContactPerson = dataReader.GetValueText("ContactPerson");
            item.Property = dataReader.GetValueText("Property");
            item.Service = dataReader.GetValueText("Service");
            item.Email = dataReader.GetValueText("Email");
            item.ResponseDueDates = dataReader.GetValueText("ResponseDueDate");
            item.BidDueDates = dataReader.GetValueText("BidDueDate");
            item.Title = dataReader.GetValueText("Title");
            try
            {
                item.BidVendorKey = dataReader.GetValueInt("BidVendorKey");
            }
            catch { }
        }
        protected void LoadVendorByBidRequest(DBDataReader dataReader, BidRequestModel item)
        {
            int bidrequest = item.BidRequestKey;
            item.Name = dataReader.GetValueText("Name");
            item.ContactPerson = dataReader.GetValueText("ContactPerson");
            item.Service = dataReader.GetValueText("Service");
            item.CompanyKey = dataReader.GetValueInt("CompanyKey");
            item.Email = dataReader.GetValueText("Email");
            item.ResponseDueDates = dataReader.GetValueText("RespondByDate");
            item.DefaultResponseDueDates = dataReader.GetValueText("DefaultRespondByDate");
            item.Descrip = dataReader.GetValueText("Descrip");
            item.BidAmount = dataReader.GetValueDecimal("BidAmount");
            
            //item.BidAmounts = Convert.ToString(string.Format("{0:0.00}", item.BidAmount));
            item.BidAmounts = Common.Utility.FormatNumberHelper(item.BidAmount, true);
            item.BidReqStatus = dataReader.GetValueText("BidVendorStatus");
            item.BidVendorStatus = dataReader.GetValueText("BidRequeststatus");
            item.VendorKey = dataReader.GetValueInt("VendorKey");
            item.BidVendorKey = dataReader.GetValueInt("BidVendorKey");
            item.TotalApceptRecord = dataReader.GetValueText("TotalApceptRecord");
            item.IsAssigned = dataReader.GetValueBool("IsAssigned");
            try
            {
                item.priority = dataReader.GetValueInt("priority");
                item.NotificationId = dataReader.GetValueText("NotificationId");
                item.NotificationType = dataReader.GetValueText("NotificationType");
            }
            catch { }
        }
        public virtual Int64 DocumentInsertDynamic(string ObjectKey, string FileName, string FileSize, string Controller, string Action)
        {
            Int64 DocumentKey = 0;
            try
            {
                obj_con.clearParameter();
                obj_con.BeginTransaction();
                obj_con.addParameter("@ObjectKey", ObjectKey);
                obj_con.addParameter("@FileName", FileName);
                obj_con.addParameter("@FileSize", FileSize);
                obj_con.addParameter("@Controller", Controller);
                obj_con.addParameter("@Action", Action);
                obj_con.addParameter("@DocumentKey", DocumentKey, DBTrans.Insert);
                obj_con.ExecuteNoneQuery("site_Document_InsertDynmc", CommandType.StoredProcedure);
                obj_con.CommitTransaction();
                obj_con.closeConnection();
                return DocumentKey = Convert.ToInt64(obj_con.getValue("@DocumentKey"));
            }
            catch (Exception ex)
            {
                obj_con.RollbackTransaction();
                throw new Exception("site_Document_InsertDynmc" + ex.ToString());
            }
        }
        public string GetDocumentListDynmc(Int64 ObjectKey, string ControllerName, string ActionName)
        {
            try
            {
                obj_con.clearParameter();
                obj_con.addParameter("@ObjectKey", ObjectKey);
                obj_con.addParameter("@Controller", ControllerName);
                obj_con.addParameter("@Action", ActionName);
                DataTable dt = obj_con.ConvertDatareadertoDataTable(obj_con.ExecuteReader("site_DocumentList_Dynmc", CommandType.StoredProcedure));
                obj_con.CommitTransaction();
                obj_con.closeConnection();
                return AssoBidsUtility.ConvertDataTableTojSonString(dt);
            }
            catch (Exception ex)
            {
                throw new Exception("site_DocumentList_Dynmc" + ex.ToString());
            }
        }
        public string DeleteDocumentListDynmc(Int64 DocumentKey)
        {
            try
            {
                obj_con.clearParameter();
                obj_con.addParameter("@DocumentKey", DocumentKey);
                DataTable dt = obj_con.ConvertDatareadertoDataTable(obj_con.ExecuteReader("site_DocumentDelete_Dynmc", CommandType.StoredProcedure));
                obj_con.CommitTransaction();
                obj_con.closeConnection();
                return AssoBidsUtility.ConvertDataTableTojSonString(dt);
            }
            catch (Exception ex)
            {
                throw new Exception("site_DocumentList_Dynmc" + ex.ToString());
            }
        }
        public List<System.Web.Mvc.SelectListItem> GetStatusListForDDL(string para1, string para2)
        {
            try
            {
                Boolean PleaseSelect = true;
                obj_con.clearParameter();
                obj_con.addParameter("@para1", para1);
                obj_con.addParameter("@para2", para2);
                DataTable dt = obj_con.ConvertDatareadertoDataTable(obj_con.ExecuteReader("site_BidRequest_GetStatusListForDDL", CommandType.StoredProcedure));
                obj_con.CommitTransaction();
                obj_con.closeConnection();
                List<System.Web.Mvc.SelectListItem> lst = new List<System.Web.Mvc.SelectListItem>();
                System.Web.Mvc.SelectListItem sli1 = new System.Web.Mvc.SelectListItem();
                if (PleaseSelect == true)
                {
                    sli1.Text = "Show All";
                    sli1.Value = "0";
                    lst.Add(sli1);
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    System.Web.Mvc.SelectListItem sli = new System.Web.Mvc.SelectListItem();
                    sli.Text = Convert.ToString(dt.Rows[i][0]);
                    sli.Value = Convert.ToString(dt.Rows[i][1]);
                    lst.Add(sli);
                }
                //sli1 = new System.Web.Mvc.SelectListItem();
                //sli1.Text = "Rejected";
                //sli1.Value = "803";
                //lst.Add(sli1);
                return lst;
            }
            catch (Exception ex)
            {
                throw new Exception("site_BidRequest_GetStatusListForDDL");
            }
        }
        protected void LoadVender(DBDataReader dataReader, BidRequestModel item)
        {

            item.BidRequestKey = dataReader.GetValueInt("BidRequestKey");
            item.DateAdded = dataReader.GetValueDateTime("DateAdded");
            item.Title = dataReader.GetValueText("Title");
            item.ResponseDueDate = dataReader.GetValueDateTime("DefaultRespondByDate");
            item.Description = dataReader.GetValueText("Description");
            item.Property = dataReader.GetValueText("Property");
            item.Service = dataReader.GetValueText("Service");
            item.BidDueDate = dataReader.GetValueDateTime("BidDueDate");

            item.NoofBids = dataReader.GetValueInt("NoofBids");
            item.BidRequestStatus = dataReader.GetValueInt("BidRequestStatus");
            item.TotalRecords = dataReader.GetValueInt("TotalRecord");

            //BrNoofUnit



            //BrContName
            //BrContCompany
            //BrContPhone
            //BrContEmail

            ////item.NumberOfUnits = dataReader.GetValueInt("NumberOfUnits");
            item.CompanyName = dataReader.GetValueText("CompanyName");
            item.Property = dataReader.GetValueText("Property");
            item.Address = dataReader.GetValueText("Address");
            item.Address2 = dataReader.GetValueText("Address2");
            item.Zip = dataReader.GetValueText("Zip");
            item.City = dataReader.GetValueText("City");
            item.State = dataReader.GetValueText("State");

            item.Work = dataReader.GetValueText("WorkNumber");
            item.ContactPerson = dataReader.GetValueText("ContactName");

        }
        protected void LoadStaff(DBDataReader dataReader, BidRequestModel item)
        {

            item.BidRequestKey = dataReader.GetValueInt("BidRequestKey");
            item.Title = dataReader.GetValueText("Title");
            item.StartDates = dataReader.GetValueText("StartDate");
            item.BidDueDates = dataReader.GetValueText("BidDueDate");
            item.NoofBids = dataReader.GetValueInt("NoofBids");
            item.BidReqStatus = dataReader.GetValueText("BidRequestStatus");
            item.TotalRecords = dataReader.GetValueInt("TotalRecord");
            item.Property = dataReader.GetValueText("PropertyName");
            item.NewMessageCount = dataReader.GetValueInt("NewMsg");
            try
            {
                item.priority = dataReader.GetValueInt("priority");
                item.NotificationId = dataReader.GetValueText("NotificationId");
                item.NotificationType = dataReader.GetValueText("NotificationType");
            }
            catch { }

        }

        public IList<BidRequestModel> GetAllProperty(Int32 ResourceKey, Int32 CompanyKey)
        {
            List<BidRequestModel> itemList = new List<BidRequestModel>();

            try
            {
                string storedProcedure = "site_Property_GetAllforbid";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        commandWrapper.AddInputParameter("@ResourceKey", SqlDbType.Int, ResourceKey);
                        commandWrapper.AddInputParameter("@CompanyKey", SqlDbType.Int, CompanyKey);
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            BidRequestModel item = null;
                            while (dataReader.Read())
                            {
                                item = new BidRequestModel();
                                GetAllProperty(dataReader, item);
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

        public virtual BidRequestModel getbiddate(int Comapanykey)
        {
            BidRequestModel item = null;

            try
            {
                string storedProcedure = "site_Bidrequest_GetBidDate";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@Comapanykey", SqlDbType.Int, Comapanykey);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            while (dataReader.Read())
                            {
                                item = new BidRequestModel();

                                Getbiddate(dataReader, item);
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


        public IList<LookUpModel> GetBidRequetStatusFilter()
        {
            List<LookUpModel> itemList = new List<LookUpModel>();

            try
            {
                string storedProcedure = "site_LookUp_GetBidRequestStatusFilter";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            LookUpModel item = null;
                            while (dataReader.Read())
                            {
                                item = new LookUpModel();
                                GetBidRequetStatusFilter(dataReader, item);
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

        protected void GetAllProperty(DBDataReader dataReader, BidRequestModel item)
        {
            item.PropertyKey = dataReader.GetValueText("PropertyKey");
            item.Property = dataReader.GetValueText("Title");
        }

        protected void Getbiddate(DBDataReader dataReader, BidRequestModel item)
        {
            item.BidRequestResponseDays = dataReader.GetValueInt("BidRequestResponseDays");
            item.BidSubmitDays = dataReader.GetValueInt("BidSubmitDays");
        }

        protected void GetBidRequetStatusFilter(DBDataReader dataReader, LookUpModel item)
        {
            item.Title = dataReader.GetValueText("Title");
            item.LookUpKey1 = dataReader.GetValueText("LookUpKey");
        }

        public IList<BidRequestModel> GetAllService()
        {
            List<BidRequestModel> itemList = new List<BidRequestModel>();

            try
            {
                string storedProcedure = "site_Service_GetAll";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            BidRequestModel item = null;
                            while (dataReader.Read())
                            {
                                item = new BidRequestModel();
                                GetAllService(dataReader, item);
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
        protected void GetAllService(DBDataReader dataReader, BidRequestModel item)
        {
            item.PropertyKey = dataReader.GetValueText("ServiceKey");
            item.Property = dataReader.GetValueText("Title");
        }

        public virtual bool Insert(BidRequestModel item)
        {
            bool status = false;

            try
            {
                item.BidRequestStatus = 1;
                string storedProcedure = "site_BidRequest_InsertOne";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        if (item.ResponseDueDate == DateTime.MinValue)
                        {
                            item.ResponseDueDate = DateTime.Now.AddDays(2);
                        }
                        if (item.BidDueDate == DateTime.MinValue)
                        {
                            item.BidDueDate = DateTime.Now.AddDays(14);
                        }
                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@propertyKey", SqlDbType.Int, Convert.ToInt32(item.Property));
                        commandWrapper.AddInputParameter("@resourceKey", SqlDbType.Int, item.ResourceKey);
                        commandWrapper.AddInputParameter("@modulekey", SqlDbType.Int, item.ModuleKey);
                        commandWrapper.AddInputParameter("@serviceKey", SqlDbType.Int, Convert.ToInt32(item.Service));
                        commandWrapper.AddInputParameter("@title", SqlDbType.VarChar, String.IsNullOrEmpty(item.Title) ? 0 : item.Title.Length, String.IsNullOrEmpty(item.Title) ? SqlString.Null : item.Title);
                        commandWrapper.AddInputParameter("@bidDueDate", SqlDbType.SmallDateTime, (item.BidDueDate == DateTime.MinValue) ? SqlDateTime.Null : item.BidDueDate);
                        commandWrapper.AddInputParameter("@ResponseDueDate", SqlDbType.DateTime, (item.ResponseDueDate == DateTime.MinValue) ? SqlDateTime.Null : item.ResponseDueDate);
                        commandWrapper.AddInputParameter("@description", SqlDbType.VarChar, String.IsNullOrEmpty(item.Description) ? 0 : item.Description.Length, String.IsNullOrEmpty(item.Description) ? SqlString.Null : item.Description);
                        commandWrapper.AddInputParameter("@bidRequestStatus", SqlDbType.Int, (item.BidRequestStatus == 0) ? SqlInt32.Null : item.BidRequestStatus);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@bidRequestKey", SqlDbType.Int);
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        db.ExecuteNonQuery(commandWrapper);

                        // save the newly inserted key value
                        item.BidRequestKey = commandWrapper.GetValueInt("@bidRequestKey");

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

        public virtual bool DocInsert(BidRequestModel bidRequestModel, string FileName, string FileSize)
        {
            bool status = false;

            try
            {
                if (bidRequestModel.Property != null && bidRequestModel.Property != "")
                {
                    status = Insert(bidRequestModel);
                }
                
                string storedProcedure = "site_Document_BidRequestInsert";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@BidRequestKey", SqlDbType.Int, bidRequestModel.BidRequestKey);
                        commandWrapper.AddInputParameter("@modulekeyval", SqlDbType.Int, bidRequestModel.ModuleKey);
                        commandWrapper.AddInputParameter("@FileName", SqlDbType.VarChar, String.IsNullOrEmpty(FileName) ? 0 : FileName.Length, String.IsNullOrEmpty(FileName) ? SqlString.Null : FileName);
                        commandWrapper.AddInputParameter("@FileSize", SqlDbType.VarChar, String.IsNullOrEmpty(FileSize) ? 0 : FileSize.Length, String.IsNullOrEmpty(FileSize) ? SqlString.Null : FileSize);
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
        public virtual bool UpdateStatus(string status, int BidRequestKey)
        {
            bool status1 = false;

            try
            {

                string storedProcedure = "site_BidRequest_StatusUpdate";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        commandWrapper.AddInputParameter("@status", SqlDbType.VarChar, status);
                        commandWrapper.AddInputParameter("@BidRequestKey", SqlDbType.Int, BidRequestKey);
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);
                        db.ExecuteNonQuery(commandWrapper);
                        status1 = (commandWrapper.GetValueInt("@errorCode") == 0);
                    }
                }
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
            }
            return status1;
        }
        public DataTable UpdateStatusDBReturb(string status, int BidRequestKey)
        {
            DataTable dt = new DataTable();
            bool status1 = false;
            int status11 = 0;
            int WorkOrderKey = 0;
            try
            {
                if (status == "Rejectp")
                {
                    mailsend(BidRequestKey, "Cancel", "", "", "", "");
                }
                //obj_con.clearParameter();
                //obj_con.addParameter("@status", status);
                //obj_con.addParameter("@BidRequestKey", string.IsNullOrEmpty(Convert.ToString(BidRequestKey)) ? 0 : BidRequestKey);


                //obj_con.addParameter("@errorCode", 0);

                //dt = obj_con.ConvertDatareadertoDataTable(obj_con.ExecuteReader("site_BidRequest_StatusUpdate", CommandType.StoredProcedure));

                //obj_con.CommitTransaction();
                //obj_con.closeConnection();


                string storedProcedure = "site_BidRequest_StatusUpdate";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@status", SqlDbType.NVarChar, status);
                        commandWrapper.AddInputParameter("@BidRequestKey", SqlDbType.Int, BidRequestKey);
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);
                        commandWrapper.AddOutputParameter("@WorkOrderkey", SqlDbType.Int);
                        db.ExecuteNonQuery(commandWrapper);
                        WorkOrderKey = commandWrapper.GetValueInt("@WorkOrderkey");
                        status11 = commandWrapper.GetValueInt("@errorCode");
                        status1 = (commandWrapper.GetValueInt("@errorCode") == 0);
                    }
                }

                if (status == "InProgress")
                {
                    SearchVendorByBidRequestForEmailSend(BidRequestKey, 100, status);
                }

                else if (status == "Acceptp")
                {
                    string ContactDetail = GetPropertyDetailForMail(BidRequestKey);
                    string Property = ContactDetail.Split(',')[0];
                    string ContactDetails = ContactDetail.Split(',')[1];

                    mailsend(BidRequestKey, status, "", Property, ContactDetails, "");

                }
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("site_BidRequest_StatusUpdate" + ex.ToString());
            }
        }

        public int UpdateStatusDBReturb1(string status, int BidRequestKey, int ParentBidRequestKey)
        {
            DataTable dt = new DataTable();
            bool status1 = false;
            int status11 = 0;
            int WorkOrderKey = 0;
            try
            {
                if (status == "Rejectp")
                {
                    mailsend(BidRequestKey, "Cancel", "", "", "", "");
                }
                //obj_con.clearParameter();
                //obj_con.addParameter("@status", status);
                //obj_con.addParameter("@BidRequestKey", string.IsNullOrEmpty(Convert.ToString(BidRequestKey)) ? 0 : BidRequestKey);


                //obj_con.addParameter("@errorCode", 0);

                //dt = obj_con.ConvertDatareadertoDataTable(obj_con.ExecuteReader("site_BidRequest_StatusUpdate", CommandType.StoredProcedure));

                //obj_con.CommitTransaction();
                //obj_con.closeConnection();


                string storedProcedure = "site_BidRequest_StatusUpdate";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@status", SqlDbType.NVarChar, status);
                        commandWrapper.AddInputParameter("@BidRequestKey", SqlDbType.Int, BidRequestKey);
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);
                        commandWrapper.AddOutputParameter("@WorkOrderkey", SqlDbType.Int);
                        db.ExecuteNonQuery(commandWrapper);
                        WorkOrderKey = commandWrapper.GetValueInt("@WorkOrderkey");
                        status11 = commandWrapper.GetValueInt("@errorCode");
                        status1 = (commandWrapper.GetValueInt("@errorCode") == 0);
                    }
                }

                if (status == "InProgress")
                {
                    SearchVendorByBidRequestForEmailSend(BidRequestKey, 100, status);
                }

                else if (status == "Acceptp")
                {
                    string ContactDetail = GetPropertyDetailForMail(BidRequestKey);
                    string Property = ContactDetail.Split(',')[0];
                    string BidAmounts = ContactDetail.Split(',')[1];
                    int splitCount = ContactDetail.Split(',').Length;
                    string ContactDetails = "";
                    for (int i = 2; i < splitCount; i++)
                    {
                        ContactDetails += ContactDetail.Split(',')[i] + " , ";
                    }

                    //ContactDetails = ContactDetails.Length - 2.ToString();
                    ContactDetails = ContactDetails.Remove(ContactDetails.Length - 2);
                    if (BidAmounts != "")

                    {


                        BidAmounts = BidAmounts + ".00"; 
                    }

                    mailsend(BidRequestKey, status, "", Property, ContactDetails, BidAmounts);

                }
                return WorkOrderKey;
            }
            catch (Exception ex)
            {
                throw new Exception("site_BidRequest_StatusUpdate" + ex.ToString());
            }
        }

        public int UpdateStatusDBReturbCancel(string status, int BidRequestKey)
        {
            DataTable dt = new DataTable();
            bool status1 = false;
            int status11 = 0;

            try
            {
                string storedProcedure = "site_BidRequest_StatusUpdateCancel";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@status", SqlDbType.NVarChar, status);
                        commandWrapper.AddInputParameter("@BidRequestKey", SqlDbType.Int, BidRequestKey);
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        db.ExecuteNonQuery(commandWrapper);
                        status11 = commandWrapper.GetValueInt("@errorCode");
                        status1 = (commandWrapper.GetValueInt("@errorCode") == 0);

                    }
                }
                return status11;
            }
            catch (Exception ex)
            {
                throw new Exception("site_BidRequest_StatusUpdate" + ex.ToString());
            }
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

        public List<BidRequestModel> SearchVendorByBidRequest(int BidRequestKey, int modulekey, long ResourceKey)
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
                        commandWrapper.AddInputParameter("@ResourceKey", SqlDbType.Int, ResourceKey);
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

        public List<BidRequestModel> SearchVendorByBidRequestForEmailSend(int BidRequestKey, int modulekey, string status)
        {
            List<BidRequestModel> itemList = new List<BidRequestModel>();
            List<EmailSendModel> emailItems = new List<EmailSendModel>();
            try
            {
                string storedProcedure = "site_BidRequest_GetInformationForEmail";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        commandWrapper.AddInputParameter("@BidRequestkey", SqlDbType.Int, (BidRequestKey == 0) ? 0 : BidRequestKey);
                        //commandWrapper.AddInputParameter("@Modulekey", SqlDbType.Int, modulekey);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            BidRequestModel item = null;
                            while (dataReader.Read())
                            {
                                item = new BidRequestModel();
                                LoadVendorByBidRequestEmail(dataReader, item);
                                itemList.Add(item);

                                //VendorInvetationmailsend(0, item.Email, item.ContactPerson, item.CompanyName, item.ResponseDueDates, item.Service, item.Property
                                //        , item.Title, item.BidDueDates, item.VendorName, status);
                                EmailSendModel emailSend = CreateEmailForVendorsBidRequest(0, item.Email, item.ContactPerson, item.CompanyName, item.ResponseDueDates, item.Service, item.Property
                                            , item.Title, item.BidDueDates, item.VendorName, status);

                                if (emailSend != null && emailSend.To != "" && emailSend.From != "")
                                {
                                    EmailRepository emailRepos = new EmailRepository();
                                    PropertyRepository ProRepos = new PropertyRepository();
                                    EmailModel emailModel = new EmailModel();

                                    var BidVendor = GetBidVendorByBidVendorKey(item.BidVendorKey);
                                    var BidRequest = GetDataBidRequestViewEdit(BidVendor.BidRequestKey);
                                    var property = ProRepos.Get(Convert.ToInt32(BidRequest.PropertyKey));
                                    VendorManagerRepository res = new VendorManagerRepository();
                                    var Resource = res.GetResourceForInviteVendor(property.CompanyKey);
                                    emailModel.Body = emailSend.Body;
                                    emailModel.DateAdded = DateTime.Now;
                                    emailModel.DateSent = DateTime.Now;
                                    emailModel.From = emailSend.From;
                                    emailModel.Subject = emailSend.Subject;
                                    emailModel.To = emailSend.To;
                                    emailModel.ModuleKey = BidRequest.ModuleKey;
                                    emailModel.ObjectKey = BidVendor.BidVendorKey;
                                    emailModel.ResourceKey = Resource.ResourceKey;
                                    emailModel.EmailStatus = 500;

                                    bool isInserted = emailRepos.Create(emailModel);
                                    if (isInserted)
                                        emailSend.EmailKey = emailModel.EmailKey;
                                    emailItems.Add(emailSend);
                                }
                            }
                        }
                    }
                    if (emailItems.Count > 0)
                    {
                        Task.Run(() => SendMailAsync(emailItems)).ConfigureAwait(false);
                    }
                }
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
            }
            return itemList;
        }

        public List<BidRequestModel> SearchVendorByBidRequestForEmailSendForReject(int BidRequestKey, int modulekey, string status)
        {
            List<BidRequestModel> itemList = new List<BidRequestModel>();
            try
            {
                List<EmailSendModel> emailItems = new List<EmailSendModel>();
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
                                if (item.BidReqStatus == "Submitted")
                                {
                                    //VendorInvetationmailsend(0, item.Email, item.ContactPerson, item.CompanyName, item.ResponseDueDates, item.Service, item.Property
                                    //        , item.Title, item.BidDueDates, item.VendorName, status);
                                    EmailSendModel mailItem = CreateEmailForVendorsBidRequest(0, item.Email, item.ContactPerson, item.CompanyName, item.ResponseDueDates, item.Service, item.Property
                                            , item.Title, item.BidDueDates, item.VendorName, status);
                                    if (mailItem != null && mailItem.From != "" && mailItem.To != "")
                                    {
                                        EmailRepository emailRepos = new EmailRepository();
                                        EmailModel emailModel = new EmailModel();
                                        PropertyRepository ProRepos = new PropertyRepository();
                                        var BidVendor = GetBidVendorByBidVendorKey(item.BidVendorKey);
                                        var BidRequest = GetDataBidRequestViewEdit(BidVendor.BidRequestKey);
                                        var property = ProRepos.Get(Convert.ToInt32(BidRequest.PropertyKey));
                                        VendorManagerRepository res = new VendorManagerRepository();
                                        var Resource = res.GetResourceForInviteVendor(property.CompanyKey);
                                        emailModel.Body = mailItem.Body;
                                        emailModel.DateAdded = DateTime.Now;
                                        emailModel.DateSent = DateTime.Now;
                                        emailModel.From = mailItem.From;
                                        emailModel.Subject = mailItem.Subject;
                                        emailModel.To = mailItem.To;
                                        emailModel.ModuleKey = BidRequest.ModuleKey;
                                        emailModel.ObjectKey = BidVendor.BidVendorKey;
                                        emailModel.ResourceKey = Resource.ResourceKey;
                                        emailModel.EmailStatus = 500;

                                        bool isInserted = emailRepos.Create(emailModel);
                                        if (isInserted)
                                            mailItem.EmailKey = emailModel.EmailKey;

                                        emailItems.Add(mailItem);
                                    }
                                }
                            }
                        }
                    }
                }
                if (emailItems.Count > 0)
                {
                    Task.Run(() => SendMailAsync(emailItems)).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
            }
            return itemList;
        }

        public void VendorInvetationmailsend(int status, string fromemail, string UserName, string CompanyName, string ResponseDueDates, string Service, string Property
                                    , string Title, string BidDueDate, string VendorName, string statuss)
        {
            try
            {
                //Repository.Base.Code.PMVendorRepository staffDirectoryRepository = new Repository.Base.Code.PMVendorRepository();
                IList<Model.EmailTemplateModel> EmailTemplate = null;
                EmailTemplate = GetAllData(statuss);
                string LinkUrl = System.Configuration.ConfigurationManager.AppSettings["LinkUrl"];
                string body = EmailTemplate[0].Body;
                body = body.Replace("[MemberName]", Convert.ToString(UserName.ToString().Trim()));
                body = body.Replace("[Property]", Convert.ToString(Property.ToString().Trim()));
                body = body.Replace("[Services]", Convert.ToString(Service.ToString().Trim()));
                body = body.Replace("[Title]", Convert.ToString(Title.ToString().Trim()));
                body = body.Replace("[ResponseDueDate]", Convert.ToString(ResponseDueDates.ToString().Trim()));
                body = body.Replace("[BidDueDate]", Convert.ToString(BidDueDate.ToString().Trim()));
                body = body.Replace("[VendorName]", Convert.ToString(VendorName.ToString().Trim()));
                body = body.Replace("[CompanyName]", Convert.ToString(CompanyName.ToString().Trim()));

                MailMessage msg = new MailMessage();
                string strBody = string.Empty;
                string Subject = EmailTemplate[0].EmailSubject;
                Subject = Subject.Replace("[CompanyName]", CompanyName.ToString().Trim());
                Subject = Subject.Replace("[BidName]", Title.ToString().Trim());
                msg.From = new MailAddress(System.Configuration.ConfigurationManager.AppSettings["senderemail"]);
                msg.To.Add(fromemail);
                msg.Subject = Subject;
                msg.IsBodyHtml = true;
                msg.Body += body;
                using (SmtpClient client = new SmtpClient())
                {
                    client.EnableSsl = false;
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

        public EmailSendModel CreateEmailForVendorsBidRequest(int status, string fromemail, string UserName, string CompanyName, string ResponseDueDates, string Service, string Property, string Title, string BidDueDate, string VendorName, string statuss)
        {
            EmailSendModel msg = null;
            try
            {
                //Repository.Base.Code.PMVendorRepository staffDirectoryRepository = new Repository.Base.Code.PMVendorRepository();
                IList<Model.EmailTemplateModel> EmailTemplate = null;
                EmailTemplate = GetAllData(statuss);
                string LinkUrl = System.Configuration.ConfigurationManager.AppSettings["LinkUrl"];
                string body = EmailTemplate[0].Body;
                body = body.Replace("[MemberName]", UserName.ToString().Trim());
                //body = body.Replace("[VendorRegistrationLink]", LinkUrl + "/Registration/Registration?CompanyKey=" + status);
                body = body.Replace("[Property]", Property.ToString().Trim());
                body = body.Replace("[Services]", Service.ToString().Trim());
                body = body.Replace("[BidName]", Title.ToString().Trim());
                body = body.Replace("[Title]", Title.ToString().Trim());
                body = body.Replace("[ResponseDueDate]", ResponseDueDates.ToString().Trim());
                body = body.Replace("[BidDueDate]", BidDueDate.ToString().Trim());
                body = body.Replace("[VendorName]", VendorName.ToString().Trim());
                body = body.Replace("[CompanyName]", CompanyName.ToString().Trim());
                //body = body.Replace("[MemberCompanyName]", Name);

                msg = new EmailSendModel();
                string strBody = string.Empty;

                string Subject = EmailTemplate[0].EmailSubject;
                Subject = Subject.Replace("[CompanyName]", CompanyName.ToString().Trim());
                Subject = Subject.Replace("[VendorName]", CompanyName.ToString().Trim());
                Subject = Subject.Replace("[BidName]", Title.ToString().Trim());

                msg.From = System.Configuration.ConfigurationManager.AppSettings["senderemail"];
                msg.To = fromemail;
                msg.Subject = Subject;
                msg.IsHtml = true;

                msg.Body += body;

            }
            catch (Exception ex)
            {
                msg = null;
            }
            return msg;
        }


        public virtual IList<EmailTemplateModel> GetAllData(string Status)
        {
            List<EmailTemplateModel> itemList = new List<EmailTemplateModel>();
            try
            {
                string storedProcedure = "site_EmailTemplate_SelectAll";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        if (Status == "Rejectp")
                        {
                            commandWrapper.AddInputParameter("@LookUpTitle", SqlDbType.NText, "Bid rejection");
                        }
                        else
                        {
                            commandWrapper.AddInputParameter("@LookUpTitle", SqlDbType.NText, "Bid Invitation");
                        }
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
        protected void Load(DBDataReader dataReader, EmailTemplateModel item)
        {
            item.EmailTemplateKey = dataReader.GetValueInt("EmailTemplateKey");
            item.EmailTitle = dataReader.GetValueText("EmailTitle");
            item.Title = dataReader.GetValueText("Title");
            item.EmailSubject = dataReader.GetValueText("EmailSubject");
            item.Body = dataReader.GetValueText("Body");
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
        protected void LoadAllVendor(DBDataReader dataReader, BidRequestModel item)
        {
            item.VendorName = dataReader.GetValueText("VendorName");
            item.CompanyName = dataReader.GetValueText("CompanyName");
            item.LastWorkDate = dataReader.GetValueText("LastWorkDate");
            if (item.LastWorkDate == "1/1\r\n/1900" || item.LastWorkDate.Replace("\r\n","").Trim() == "1/1/1900")
            {
                item.LastWorkDate = "";
            }

          
            item.VendorKey = dataReader.GetValueInt("CompanyKey");
        }

        public virtual bool VendorAddForService(int VendorKey, int ServiceKey, int BidRequestKey, DateTime ResponseDueDate, int ResourceKey, string BidVendorID, string btval)
        {
            bool status = false;

            try
            {
                //status = Insert(bidRequestModel);
                //string storedProcedure = "site_VendorService_BidRequestVendorServiceInsert";
                // string storedProcedure = "site_BidVendor_Insert";


                string storedProcedure = "";
                if (btval == " Add New Vendor")
                {
                    storedProcedure = "site_BidVendor_AddWorkOrderInsert_New";
                }
                else
                {
                    storedProcedure = "site_BidVendor_Insert_New";
                }

                Random r = new Random();
                BidVendorID = r.Next().ToString();
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@VendorKey", SqlDbType.Int, VendorKey);
                        commandWrapper.AddInputParameter("@IsAssigned", SqlDbType.Bit, true);
                        commandWrapper.AddInputParameter("@BidRequestKey", SqlDbType.Int, BidRequestKey);
                        commandWrapper.AddInputParameter("@RespondByDate", SqlDbType.DateTime, ResponseDueDate.Year < 200 ? ResponseDueDate.AddYears(2000 - ResponseDueDate.Year) : ResponseDueDate);
                        commandWrapper.AddInputParameter("@ResourceKey", SqlDbType.Int, ResourceKey);
                        commandWrapper.AddInputParameter("@BidVendorID", SqlDbType.VarChar, BidVendorID);
                        commandWrapper.AddInputParameter("@BidVendorStatus", SqlDbType.Int, 1);
                        commandWrapper.AddOutputParameter("@BidVendorKey", SqlDbType.Int, 0);

                        db.ExecuteNonQuery(commandWrapper);
                        if(commandWrapper.GetValueInt("@BidVendorKey") > 0)
                        {
                            int bidvendorkey = commandWrapper.GetValueInt("@BidVendorKey");
                            SendMailToBidVendorByBidVendorKey(bidvendorkey);
                            
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

        public virtual void InsertNotificationForBidVendorByBidVendorKey(int BidVendorKey)
        {
            ABNotificationRepository aBNotification = new ABNotificationRepository();
            VendorManagerRepository vm = new VendorManagerRepository();
            PropertyRepository pr = new PropertyRepository();
            
            var BidVendorData = GetBidVendorByBidVendorKey(BidVendorKey);
            var BidRequestData = GetDataBidRequestViewEdit(BidVendorData.BidRequestKey);
            var CompanyData = GetDataViewEdit(BidVendorData.VendorKey);
            var ResourceData = vm.GetResourceForInviteVendor(BidVendorData.VendorKey);
            var PropertyData = pr.Get(Convert.ToInt32(BidRequestData.PropertyKey));
            var ManagerResourceData = vm.GetResourceForInviteVendor(PropertyData.CompanyKey);
            aBNotification.InsertNotification("BidReqStatus", BidRequestData.ModuleKey, BidVendorData.BidRequestKey, Convert.ToInt64(ManagerResourceData.ResourceKey),
                "New Bid Reqeust.", ResourceData.ResourceKey);
        }
        public virtual void SendMailToBidVendorByBidVendorKey(int BidVendorKey)
        {
            VendorManagerRepository vm = new VendorManagerRepository();
            LookUpRepository look = new LookUpRepository();

            var BidVendorData = GetBidVendorByBidVendorKey(BidVendorKey);
            var BidRequestData = GetDataBidRequestViewEdit(BidVendorData.BidRequestKey);
            var CompanyData = GetDataViewEdit(BidVendorData.VendorKey);
            var ResourceData = vm.GetResourceForInviteVendor(BidVendorData.VendorKey);
            string respondbydate = (BidVendorData.RespondByDate == null) ? "" : BidVendorData.RespondByDate.ToString("MM/dd/yyyy");
            string bidduedate = (BidRequestData.BidDueDate == null) ? "" : BidRequestData.BidDueDate.ToString("MM/dd/yyyy");
            var lookup = look.Get(BidVendorData.BidVendorStatus);
            var emailSend = CreateEmailForVendorsBidRequest(0, ResourceData.Email, ResourceData.Username, ResourceData.Company, respondbydate,
                BidRequestData.Service, BidRequestData.Property, BidRequestData.Title, bidduedate, (ResourceData.FirstName + " " + ResourceData.LastName), lookup.Title);

            if (emailSend != null && emailSend.To != "" && emailSend.From != "")
            {
                EmailRepository emailRepos = new EmailRepository();
                PropertyRepository ProRepos = new PropertyRepository();
                EmailModel emailModel = new EmailModel();

                var property = ProRepos.Get(Convert.ToInt32(BidRequestData.PropertyKey));
                var Resource = ResourceData;
                emailModel.Body = emailSend.Body;
                emailModel.DateAdded = DateTime.Now;
                emailModel.DateSent = DateTime.Now;
                emailModel.From = emailSend.From;
                emailModel.Subject = emailSend.Subject;
                emailModel.To = emailSend.To;
                emailModel.ModuleKey = BidRequestData.ModuleKey;
                emailModel.ObjectKey = BidVendorData.BidVendorKey;
                emailModel.ResourceKey = Resource.ResourceKey;
                emailModel.EmailStatus = 500;

                bool isInserted = emailRepos.Create(emailModel);
                if (isInserted)
                    emailSend.EmailKey = emailModel.EmailKey;

                List<EmailSendModel> emailItems = new List<EmailSendModel>();
                emailItems.Add(emailSend);
                Task.Run(() => SendMailAsync(emailItems)).ConfigureAwait(false);

            }
        }

        public virtual BidRequestModel GetDataViewEdit(int id)
        {
            BidRequestModel item = null;

            try
            {
                string storedProcedure = "site_vendor_SelectOneByvendorKey";
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
                                item = new BidRequestModel();

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
        protected void LoadViewEdit(DBDataReader dataReader, BidRequestModel item)
        {
            item.CompanyKey = dataReader.GetValueInt("CompanyKey");

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

        public IList<BidRequestModel> GetbindDocument(int CompanyKey, int ModuleKey)
        {
            List<BidRequestModel> itemList = new List<BidRequestModel>();

            try
            {

                string storedProcedure = "site_VendorDocument_SelectAll";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        commandWrapper.AddInputParameter("@InsuranceKey", SqlDbType.Int, CompanyKey);
                        commandWrapper.AddInputParameter("@ModuleKey", SqlDbType.Int, ModuleKey);
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            BidRequestModel item = null;
                            while (dataReader.Read())
                            {
                                item = new BidRequestModel();
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

        public IList<BidRequestModel> GetbindWorkOrderDocument(int CompanyKey)
        {
            List<BidRequestModel> itemList = new List<BidRequestModel>();

            try
            {

                string storedProcedure = "site_VendorWorkOrderDocument_SelectAll";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        commandWrapper.AddInputParameter("@InsuranceKey", SqlDbType.Int, CompanyKey);
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            BidRequestModel item = null;
                            while (dataReader.Read())
                            {
                                item = new BidRequestModel();
                                GetbindWorkOrderDocument(dataReader, item);
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
        protected void GetbindDocument(DBDataReader dataReader, BidRequestModel item)
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

        protected void GetbindWorkOrderDocument(DBDataReader dataReader, BidRequestModel item)
        {
            try
            {
                item.BidVendorKey = Convert.ToInt32(dataReader.GetValueText("ObjectKey"));
                item.FileName = dataReader.GetValueText("FileName");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IList<BidRequestModel> Getbindservice(int CompanyKey)
        {
            List<BidRequestModel> itemList = new List<BidRequestModel>();

            try
            {

                string storedProcedure = "site_VendorService_GetAll";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        commandWrapper.AddInputParameter("@serviceKey", SqlDbType.Int, CompanyKey);
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            BidRequestModel item = null;
                            while (dataReader.Read())
                            {
                                item = new BidRequestModel();
                                Getbindservice(dataReader, item);
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
        protected void Getbindservice(DBDataReader dataReader, BidRequestModel item)
        {
            try
            {
                item.ServiceTitle = dataReader.GetValueText("Title");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IList<BidRequestModel> Searchinsurance(int CompanyKey)
        {
            List<BidRequestModel> itemList = new List<BidRequestModel>();
            try
            {
                string storedProcedure = "site_insurance_SelectIndexPaging";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters 
                        commandWrapper.AddInputParameter("@CompanyKey", SqlDbType.Int, CompanyKey);
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            BidRequestModel item = null;
                            while (dataReader.Read())
                            {
                                item = new BidRequestModel();
                                LoadInsurance(dataReader, item);
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
        protected void LoadInsurance(DBDataReader dataReader, BidRequestModel item)
        {
            item.InsuranceKey = dataReader.GetValueInt("InsuranceKey");
            item.PolicyNumber = dataReader.GetValueText("PolicyNumber");

            item.InsuranceAmount = dataReader.GetValueDouble("InsuranceAmount");
            item.StartDate = dataReader.GetValueDateTime("StartDate");
            item.EndDate = dataReader.GetValueDateTime("EndDate");
        }

        public virtual BidRequestModel GetDataBidRequestViewEdit(int id)
        {
            BidRequestModel item = null;

            try
            {
                string storedProcedure = "site_BidRequest_SelectOneByBidRequestKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@BidRequestKey", SqlDbType.Int, id);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            while (dataReader.Read())
                            {
                                item = new BidRequestModel();
                                GetDataBidRequestViewEdit(dataReader, item);
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
        protected void GetDataBidRequestViewEdit(DBDataReader dataReader, BidRequestModel item)
        {
            item.Property = dataReader.GetValueText("Property");
            item.Service = dataReader.GetValueText("Service");
            item.Title = dataReader.GetValueText("Title");
            item.Description = dataReader.GetValueText("Description");
            item.PropertyKey = dataReader.GetValueText("PropertyKey");
            item.ServiceKey = dataReader.GetValueInt("ServiceKey");
            item.ResponseDueDate = dataReader.GetValueDateTime("DefaultRespondByDate");
            item.BidDueDate = dataReader.GetValueDateTime("BidDueDate");
            item.ResponseDueDates = dataReader.GetValueText("DefaultRespondByDates");
            item.BidDueDates = dataReader.GetValueText("BidDueDates");
            item.BidReqStatus = dataReader.GetValueText("bidstatus");
            item.WCount = dataReader.GetValueInt("WCount");
            item.CompanyKey = dataReader.GetValueInt("Ckey");
            try
            {
                item.ModuleKey = dataReader.GetValueInt("ModuleKey");
            }
            catch { }

        }
        public virtual bool UpdateDocInsert(int BidRequestKey, string FileName, string FileSize, int ModuleKey)
        {
            bool status = false;

            try
            {
                string storedProcedure = "site_Document_BidRequestInsert";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@BidRequestKey", SqlDbType.Int, BidRequestKey);
                        commandWrapper.AddInputParameter("@FileName", SqlDbType.VarChar, String.IsNullOrEmpty(FileName) ? 0 : FileName.Length, String.IsNullOrEmpty(FileName) ? SqlString.Null : FileName);
                        commandWrapper.AddInputParameter("@FileSize", SqlDbType.VarChar, String.IsNullOrEmpty(FileSize) ? 0 : FileSize.Length, String.IsNullOrEmpty(FileSize) ? SqlString.Null : FileSize);
                        commandWrapper.AddInputParameter("@modulekeyval", SqlDbType.Int, ModuleKey);
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

        public virtual bool DocumentDelete(int BidRequestKey, string Docname)
        {
            bool status = false;

            try
            {
                string storedProcedure = "site_Document_BidRequestDelete";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@BidRequestKey", SqlDbType.Int, BidRequestKey);
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
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
            }
            return status;
        }

        public virtual bool InsertRating(string Message, int Rating1, int Rating2, int Rating3, int ResourceKey, int BidrequestKey)
        {
            bool status = false;

            try
            {
                string storedProcedure = "site_vendorRating_Insert";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@Message", SqlDbType.VarChar, Message);
                        commandWrapper.AddInputParameter("@Rating1", SqlDbType.Int, Rating1);
                        commandWrapper.AddInputParameter("@Rating2", SqlDbType.Int, Rating2);
                        commandWrapper.AddInputParameter("@Rating3", SqlDbType.Int, Rating3);
                        commandWrapper.AddInputParameter("@ResourceKey", SqlDbType.Int, ResourceKey);
                        commandWrapper.AddInputParameter("@BidRequestKey", SqlDbType.Int, BidrequestKey);


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


        public virtual bool InsertRatingNew(string Message, int Rating1, int ResourceKey, int BidrequestKey)
        {
            bool status1 = false;

            try
            {
                string storedProcedure = "site_vendorRating_Insert";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@Message", SqlDbType.VarChar, Message);
                        commandWrapper.AddInputParameter("@Rating1", SqlDbType.Int, Rating1);
                        commandWrapper.AddInputParameter("@Rating2", SqlDbType.Int, SqlInt32.Null);
                        commandWrapper.AddInputParameter("@Rating3", SqlDbType.Int, SqlInt32.Null);
                        commandWrapper.AddInputParameter("@ResourceKey", SqlDbType.Int, ResourceKey);
                        commandWrapper.AddInputParameter("@BidRequestKey", SqlDbType.Int, BidrequestKey);


                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        db.ExecuteNonQuery(commandWrapper);

                        status1 = (commandWrapper.GetValueInt("@errorCode") == 0);
                    }
                }
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
            }
            return status1;
        }

        public bool BidRequestStatusUpdate(int BidRequestKey, string status)
        {
            bool isUpdated = false;

            using (Database db = new Database())
            {
                //DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper("site_BidRequest_StatusUpdate_Close");
                using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper("site_BidRequest_StatusUpdate_Close"))
                {
                    commandWrapper.AddInputParameter("@BidRequestKey", SqlDbType.Int, BidRequestKey);
                    commandWrapper.AddInputParameter("@Status", SqlDbType.Text, status);
                    commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);
                    db.ExecuteNonQuery(commandWrapper);
                    if (commandWrapper.GetValueInt("@errorCode") == 0)
                    {
                        isUpdated = true;
                    }
                }
            }
            return isUpdated;
        }


        public IList<BidRequestModel> GetbindBidRequestDocument(int BidRequestKey)
        {
            List<BidRequestModel> itemList = new List<BidRequestModel>();

            try
            {

                string storedProcedure = "site_Document_BidRequestSelectAll";
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
                                GetbindBidRequestDocument(dataReader, item);
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
        protected void GetbindBidRequestDocument(DBDataReader dataReader, BidRequestModel item)
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
        protected void BindVendorinformation(DBDataReader dataReader, BidRequestModel item)
        {
            try
            {
                item.CompanyName = dataReader.GetValueText("CompanyName");
                item.ContactPerson = dataReader.GetValueText("ContactPerson");
                item.WorkPhone1 = dataReader.GetValueText("WorkPhone1");
                item.WorkPhone2 = dataReader.GetValueText("WorkPhone2");
                item.Fax = dataReader.GetValueText("Fax");
                item.Email = dataReader.GetValueText("Email");
                item.Address1 = dataReader.GetValueText("Address1");
                item.Address2 = dataReader.GetValueText("Address2");
                item.City = dataReader.GetValueText("City");
                item.State = dataReader.GetValueText("State");
                item.Zip = dataReader.GetValueText("Zip");
                item.InsuranceAmount = dataReader.GetValueDouble("InsuranceAmount");
                item.InsuranceExprie = dataReader.GetValueText("InsuranceExprie");
                item.InsuranceDate = dataReader.GetValueText("InsuranceDate");
                item.InsuranceKey = dataReader.GetValueInt("InsuranceKey");
                item.CompanyKey = dataReader.GetValueInt("CompanyKey");
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message);
            }
        }
        public IList<BidRequestModel> GetbindBidRequestNotes(int BidRequestKey)
        {
            List<BidRequestModel> itemList = new List<BidRequestModel>();

            try
            {

                string storedProcedure = "site_Notes_BidRequestSelectAll";
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


        public bool UpdateResponseDueDate(string ResponseDueDate, int BidRequestKey, int BidVendorKey)
        {
            bool status = false;
            try
            {
                DateTime ResponseBidDuedate = Convert.ToDateTime(ResponseDueDate).AddDays(15);
                string storedProcedure = "site_PMBidRequest_UpdateResponseDueDate";
                using (Database db = new Database())
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        commandWrapper.AddInputParameter("@BidVendorKey", SqlDbType.Int, BidVendorKey);
                        commandWrapper.AddInputParameter("@BidRequestKey", SqlDbType.Int, BidRequestKey);
                        commandWrapper.AddInputParameter("@RespondDueDate", SqlDbType.DateTime, Convert.ToDateTime(ResponseDueDate));
                        commandWrapper.AddInputParameter("@ResponseBidDuedate", SqlDbType.DateTime, Convert.ToDateTime(ResponseBidDuedate));
                        
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);
                        db.ExecuteNonQuery(commandWrapper);
                        if (commandWrapper.GetValueInt("@errorCode") == 0)
                        {
                            status = true;
                        }
                    }
                }
            }
            catch
            {

            }
            return status;
        }

        public BidVendorModel GetBidVendorByBidVendorKey(int BidVendorKey)
        {
            BidVendorModel bidVendor = new BidVendorModel();
            try
            {
                string storedProcedure = "site_BidVendor_GetByBidVendorKey";
                using (Database db = new Database())
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        commandWrapper.AddInputParameter("@BidVendorKey", SqlDbType.Int, BidVendorKey);
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);
                        DBDataReader reader = db.ExecuteReader(commandWrapper);
                        if (reader != null)
                        {
                            if (commandWrapper.GetValueInt("@errorCode") == 0)
                            {
                                while (reader.Read())
                                {
                                    LoadBidVendor(reader, bidVendor);
                                }
                            }
                            reader.Dispose();
                        }
                        return bidVendor;
                    }
                }
            }
            catch
            {
                return null;
            }

        }

        private void LoadBidVendor(DBDataReader reader, BidVendorModel bidVendor)
        {
            bidVendor.BidRequestKey = reader.GetValueInt("BidRequestKey");
            bidVendor.BidVendorID = reader.GetValueText("BidVendorID");
            bidVendor.BidVendorKey = reader.GetValueInt("BidVendorKey");
            bidVendor.BidVendorStatus = reader.GetValueInt("BidVendorStatus");
            bidVendor.DateAdded = reader.GetValueDateTime("DateAdded");
            bidVendor.IsAssigned = reader.GetValueBool("IsAssigned");
            bidVendor.LastModificationTime = reader.GetValueDateTime("LastModificationTime");
            bidVendor.ResourceKey = reader.GetValueInt("ResourceKey");
            bidVendor.RespondByDate = reader.GetValueDateTime("RespondByDate");
            bidVendor.ForResourceKey = reader.GetValueText("ForResource");
            bidVendor.VendorKey = reader.GetValueInt("VendorKey");
        }
        public bool DeleteBidVendorByBidVendorKey(int BidVendorKey)
        {
            bool status = false;
            try
            {
                string storedProcedure = "site_BidVendor_DeleteByBidVendorKey";
                using (Database db = new Database())
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        commandWrapper.AddInputParameter("@BidVendorKey", SqlDbType.Int, BidVendorKey);
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);
                        db.ExecuteNonQuery(commandWrapper);
                        if (commandWrapper.GetValueInt("@errorCode") == 0)
                        {
                            status = true;
                        }
                    }
                }
            }
            catch
            {

            }
            return status;
        }

        public List<BidRequestModel> LoadBidStatus(long ResourceKey)
        {
            List<BidRequestModel> items = new List<BidRequestModel>();
            try
            {
                string storedProcedure = "site_ABNotification_GetAcceptRejectBid";
                using (Database db = new Database())
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        commandWrapper.AddInputParameter("@ResourceKey", SqlDbType.Int, ResourceKey);
                        var reader = db.ExecuteReader(commandWrapper);

                        while (reader.Read())
                        {
                            BidRequestModel model = new BidRequestModel();
                            model.BidRequestKey = reader.GetValueInt("BidRequestKey");
                            model.NotificationId = reader.GetValueText("Id");
                            model.NotificationType = reader.GetValueText("NotificationType");
                            model.Title = reader.GetValueText("Title");
                            model.ModuleKey = reader.GetValueInt("ModuleKey");
                            items.Add(model);
                        }
                    }
                }
            }
            catch
            {
                items = null;
            }
            return items;
        }


        public string GetPropertyDetailForMail(int BidRequestKey)
        {
            List<BidRequestModel> itemList = new List<BidRequestModel>();
            string ContactDetail = "";
            try
            {

                string storedProcedure = "site_BidRequest_GetPropertyForMail";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        commandWrapper.AddInputParameter("@BidVendorKey", SqlDbType.Int, BidRequestKey);
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            BidRequestModel item = null;
                            while (dataReader.Read())
                            {
                                item = new BidRequestModel();
                                GetPropertyDetailForMail(dataReader, item);
                                itemList.Add(item);
                                 ContactDetail = item.Property + "," + item.BidAmounts + ",<br> Contact Name : " + item.Name + ",<br> Phone : " + item.WorkPhone1 +
                                 "<br> Email : " + item.Email;
                            }
                            
                           
                            //<br> Company Name : " + item.CompanyName
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message);
            }

            return ContactDetail;
        }
        protected void GetPropertyDetailForMail(DBDataReader dataReader, BidRequestModel item)
        {
            try
            {
                item.WorkPhone1 = dataReader.GetValueText("WorkNumber");
                item.Property = dataReader.GetValueText("Property");
                item.CompanyName = dataReader.GetValueText("CompanyName");
                item.Email = dataReader.GetValueText("Email");
                item.Name = dataReader.GetValueText("ContactName");
                item.BidAmounts = dataReader.GetValueText("BidRequestAmount");
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
            }
        }
        public int PMWorkOrdersCheckComeFromBIdRequest(string BidRequestKey)
        {
            DataTable dt = new DataTable();
            int value = 0;
            obj_con.clearParameter();
            obj_con.addParameter("@BidRequestKey", BidRequestKey);

            dt = obj_con.ConvertDatareadertoDataTable(obj_con.ExecuteReader("site_BidRequest_GetWorkOrderCreatedByBidRequest", CommandType.StoredProcedure));
            value = Convert.ToInt32(dt.Rows[0]["Count"].ToString());
            obj_con.CommitTransaction();
            obj_con.closeConnection();
            return value;
        }

        public bool DateExtensionRequest(string BidName, string ManagerName, string ManagerCompanyName, string ManagerEmail, string VendorName)
        {
            try
            {
                DateExtensionnMailSend(BidName, ManagerName, ManagerCompanyName, ManagerEmail, VendorName, "Date Extension");
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void DateExtensionnMailSend(string BidName, string ManagerName, string ManagerCompanyName, string ManagerEmail, string VendorName, string Status)
        {
            try
            {
                //string Status = "Date Extension";
                IList<Model.EmailTemplateModel> EmailTemplate = null;
                MailMessage msg = new MailMessage();
                string LinkUrl = System.Configuration.ConfigurationManager.AppSettings["LinkUrl"];
                string strBody = string.Empty;
                EmailTemplate = GetAll1(Status);
                string body = EmailTemplate[0].Body;
                body = body.Replace("[MemberName]", Convert.ToString(ManagerName.ToString().Trim()));
                body = body.Replace("[VendorName]", Convert.ToString(VendorName.ToString().Trim()));
                body = body.Replace("[BidName]", Convert.ToString(BidName.ToString().Trim()));


                string Subject = EmailTemplate[0].EmailSubject;
                Subject = Subject.Replace("[BidName]", Convert.ToString(BidName.ToString().Trim()));
                Subject = Subject.Replace("[VendorName]", Convert.ToString(VendorName.ToString().Trim()));

                msg.From = new MailAddress(System.Configuration.ConfigurationManager.AppSettings["senderemail"]);
                //msg.To.Add(ManagerEmail);
                msg.To.Add(ManagerEmail);
                msg.Subject = Subject;

                msg.IsBodyHtml = true;

                msg.Body += body;

                using (SmtpClient client = new SmtpClient())
                {
                    client.EnableSsl = false;
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


        public VendorModel BindVendorDetail(int BidRequestKey, int ResourceKey, string EmailSend)
        {
            VendorModel items = new VendorModel();
            try
            {
                string storedProcedure = "site_BidVendor_GetVendorListForManagerNoti";
                using (Database db = new Database())
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        commandWrapper.AddInputParameter("@BidRequestKey", SqlDbType.Int, BidRequestKey);
                        commandWrapper.AddInputParameter("@ResourceKey", SqlDbType.Int, ResourceKey);
                        var reader = db.ExecuteReader(commandWrapper);

                        while (reader.Read())
                        {
                            VendorModel model = new VendorModel();
                            model.CompanyName = reader.GetValueText("CompanyName");
                            model.ContactPerson = reader.GetValueText("ContactPerson");
                            model.Title = reader.GetValueText("BidName");
                            model.Work = reader.GetValueText("WorkPhone1");
                            model.Work2 = reader.GetValueText("WorkPhone2");
                            model.Fax = reader.GetValueText("Fax");
                            model.Email = reader.GetValueText("Email");
                            model.Address = reader.GetValueText("Address1");
                            model.Address2 = reader.GetValueText("Address2");
                            model.City = reader.GetValueText("City");
                            model.State = reader.GetValueText("State");
                            model.Zip = reader.GetValueText("Zip");
                            items = model;

                            if (EmailSend == "Yes")
                            {
                                DateExtensionnMailSend(model.Title, model.ContactPerson, model.Email, "Bid Date Extended");
                            }
                        }
                    }
                }
            }
            catch
            {
                items = null;
            }
            return items;
        }


   




        public void DateExtensionnMailSend(string BidName, string VendorName, string VendorEmail, string Status)
        {
            try
            {
                //string Status = "Date Extension";
                IList<Model.EmailTemplateModel> EmailTemplate = null;
                MailMessage msg = new MailMessage();
                string LinkUrl = System.Configuration.ConfigurationManager.AppSettings["LinkUrl"];
                string strBody = string.Empty;
                EmailTemplate = GetAll1(Status);
                string body = EmailTemplate[0].Body;
                body = body.Replace("[MemberName]", Convert.ToString(VendorName.ToString().Trim()));
                body = body.Replace("[VendorName]", Convert.ToString(VendorName.ToString().Trim()));
                body = body.Replace("[BidName]", Convert.ToString(BidName.ToString().Trim()));


                string Subject = EmailTemplate[0].EmailSubject;
                Subject = Subject.Replace("[BidName]", Convert.ToString(BidName.ToString().Trim()));
                Subject = Subject.Replace("[VendorName]", Convert.ToString(VendorName.ToString().Trim()));

                msg.From = new MailAddress(System.Configuration.ConfigurationManager.AppSettings["senderemail"]);
                //msg.To.Add(ManagerEmail);
                msg.To.Add(VendorEmail);
                msg.Subject = Subject;

                msg.IsBodyHtml = true;

                msg.Body += body;

                using (SmtpClient client = new SmtpClient())
                {
                    client.EnableSsl = false;
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
    }
}
