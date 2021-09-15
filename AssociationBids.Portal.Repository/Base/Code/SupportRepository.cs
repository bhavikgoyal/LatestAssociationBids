using AssociationBids.Portal.Model;
using DB_con;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationBids.Portal.Repository.Base
{
    public class SupportRepository : BaseRepository, ISupportRepository
    {
        ConnectionCls obj_con = null;
        
        public SupportRepository()
        {
            obj_con = new ConnectionCls();
        }

        public SupportRepository(string connectionString)
            : base(connectionString) { }

        public List<SupportModel> SearchBidRequest(long PageSize, long PageIndex, string PropertyName,string VendorName,string CompanyName, string Sort, int BidStatus, Int32 Resourcekey, string BidRequestStatus, int Modulekey, DateTime FromDate, DateTime ToDate)
        {
            List<SupportModel> itemList = new List<SupportModel>();
            try
            {
                int BidRequestStatus1 = 0;
                string Type = "";
                if (BidRequestStatus == "600, 601") { BidRequestStatus1 = 1; }
                else if (BidRequestStatus == "602, 603") { BidRequestStatus1 = 2; }
                else if (BidRequestStatus == "0,0") { BidRequestStatus1 = 0; }                
                if (BidStatus == 2) { Type = "Bid Requests"; }
                else if (BidStatus == 3) { Type = "Work Orders"; }
                string storedProcedure = "site_BidRequest_SelectIndexPagingSupport";
                string storedProcedure1 = "site_BidRequest_SelectIndexPagingSupportwithVendorname";
                string ss = "";
                //int BidRequestStatus1 = Convert.ToInt32(BidRequestStatus.Split(',')[0]);
                //int BidRequestStatus2 = Convert.ToInt32(BidRequestStatus.Split(',')[1]);
                //string storedProcedure = "site_BidRequest_SelectIndexPagingSupportReport";
                if (VendorName != null && VendorName != "")
                {
                     ss = storedProcedure1;
                }
                else
                {
                     ss = storedProcedure;
                }
             
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(ss))
                    {
                        // add the stored procedure input parameters 
                        commandWrapper.AddInputParameter("@PageSize", SqlDbType.BigInt, (PageSize == 0) ? 0 : PageSize);
                        commandWrapper.AddInputParameter("@PageIndex", SqlDbType.BigInt, (PageIndex == 0) ? 0 : PageIndex);
                        commandWrapper.AddInputParameter("@PropertyName", SqlDbType.NText, (PropertyName == "") ? "" : PropertyName);
                        commandWrapper.AddInputParameter("@VendorName", SqlDbType.NText, (VendorName == "") ? "" : VendorName);
                        commandWrapper.AddInputParameter("@CompanyName", SqlDbType.NText, (CompanyName == "") ? "" : CompanyName);
                        commandWrapper.AddInputParameter("@Sort", SqlDbType.NText, (Sort == "") ? "" : Sort);
                        commandWrapper.AddInputParameter("@Type", SqlDbType.NText, (Type == "") ? "" : Type);
                        commandWrapper.AddInputParameter("@Resourcekey", SqlDbType.Int, (Resourcekey == 0) ? 0 : Resourcekey);
                        commandWrapper.AddInputParameter("@Modulekey", SqlDbType.Int, Modulekey);
                        commandWrapper.AddInputParameter("@BidRequestStatus", SqlDbType.Int, (BidRequestStatus1 == 0) ? 0 : BidRequestStatus1);

                        if (FromDate.ToString() == "1/1/0001 12:00:00 AM")
                        {
                            commandWrapper.AddInputParameter("@StartDate", SqlDbType.NText, "");
                        }
                        else
                        {
                            commandWrapper.AddInputParameter("@StartDate", SqlDbType.NText, FromDate);
                        }

                        if (ToDate.ToString() == "1/1/0001 12:00:00 AM")
                        {
                            commandWrapper.AddInputParameter("@EndDate", SqlDbType.NText, "");
                        }
                        else
                        {

                            DateTime date2 = ToDate.AddDays(1);
                            commandWrapper.AddInputParameter("@EndDate", SqlDbType.NText, date2);
                        }

                        //commandWrapper.AddInputParameter("@BidRequestStatus1", SqlDbType.Int, (BidRequestStatus2 == 0) ? 0 : BidRequestStatus2);
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            SupportModel item = null;
                            while (dataReader.Read())
                            {
                                item = new SupportModel();
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

        public List<SupportModel> SearchBidRequest1(long PageSize, long PageIndex, string PropertyName, string VendorName, string CompanyName, string Sort, int BidStatus, Int32 Resourcekey, string BidRequestStatus, int Modulekey, DateTime FromDate, DateTime ToDate)
        {
            List<SupportModel> itemList = new List<SupportModel>();
            try
            {
                int BidRequestStatus1 = 0;
                string Type = "";
                if (BidRequestStatus == "600, 601") { BidRequestStatus1 = 1; }
                else if (BidRequestStatus == "602, 603") { BidRequestStatus1 = 2; }
                else if (BidRequestStatus == "0,0") { BidRequestStatus1 = 0; }
                if (BidStatus == 2) { Type = "Bid Requests"; }
                else if (BidStatus == 3) { Type = "Work Orders"; }


                //int BidRequestStatus1 = Convert.ToInt32(BidRequestStatus.Split(',')[0]);
                //int BidRequestStatus2 = Convert.ToInt32(BidRequestStatus.Split(',')[1]);
                //string storedProcedure = "site_BidRequest_SelectIndexPagingSupportReport";
                string storedProcedure = "Site_GetDataforVendorName_Support";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters 
                       
                        commandWrapper.AddInputParameter("@Name", SqlDbType.NText, (VendorName == "") ? "" : VendorName);
                       


                        //commandWrapper.AddInputParameter("@BidRequestStatus1", SqlDbType.Int, (BidRequestStatus2 == 0) ? 0 : BidRequestStatus2);
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            SupportModel item = null;
                            while (dataReader.Read())
                            {
                                item = new SupportModel();
                                LoadStaff1(dataReader, item);
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




        protected void LoadStaff(DBDataReader dataReader, SupportModel item)
        {
            item.BidRequestKey = dataReader.GetValueInt("BidRequestKey");
            item.Title = dataReader.GetValueText("Title");
            item.Property = dataReader.GetValueText("PropertyName");
            item.CompanyName = dataReader.GetValueText("CompanyName");
            item.VendorName = dataReader.GetValueText("VendorName");
            item.VendorUsername = dataReader.GetValueText("vname");
            item.Description = dataReader.GetValueText("Description");
            item.Type = dataReader.GetValueText("Type");
            item.StartDates = dataReader.GetValueText("StartDate");
            item.BidReqStatus = dataReader.GetValueText("BidRequestStatus");
            item.TotalRecords = dataReader.GetValueInt("TotalRecord");
            item.WinFee = dataReader.GetValueDecimal("WinFee");
            item.WinFees = Common.Utility.FormatNumberHelper(item.WinFee, true);
            try
            {
                item.priority = dataReader.GetValueInt("priority");
                item.NotificationId = dataReader.GetValueText("NotificationId");
                item.NotificationType = dataReader.GetValueText("NotificationType");
            }
            catch { }

        }


        protected void LoadStaff1(DBDataReader dataReader, SupportModel item)
        {
            item.BidRequestKey = dataReader.GetValueInt("BidRequestKey");
           
            

        }

    }
}
