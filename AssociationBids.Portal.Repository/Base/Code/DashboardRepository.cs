using AssociationBids.Portal.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace AssociationBids.Portal.Repository.Base
{
    public class DashboardRepository : BaseRepository, IDashboardRepository
    {

        public DashboardRepository() { }

        public DashboardRepository(string connectionString)
          : base(connectionString) { }
        

        public List<ADashboardModel> BindVendorsDashboard()
        {
            List<ADashboardModel> itemList = new List<ADashboardModel>();
            try
            {
                string storedProcedure = "Site_Dashboard_GetvednordataforAdminPortal";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                     

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            ADashboardModel item = null;
                            while (dataReader.Read())
                            {
                             
                                item = new ADashboardModel();
                                LoadVendors(dataReader, item);
                                itemList.Add(item);

                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
               
                throw;
            }
            return itemList;
        }
        
        public List<ADashboardModelLineChart> BindAdminProjectsLineChartValue(int ckey,int PortalKey, int ResourceKey)
        {
            List<ADashboardModelLineChart> itemList = new List<ADashboardModelLineChart>();
            try
            {
             
                string storedProcedure = "";
                if (PortalKey == 3)
                {
                    storedProcedure = "site_dashboard_VendorLineChart_Copy";
                }
                else if (PortalKey == 2)
                {
                    storedProcedure = "site_dashboard_MangerLineChart";
                }
                else
                {
                    storedProcedure = "site_dashboard_AdministratorLineChart";
                }

                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {


                        commandWrapper.AddInputParameter("@CompanyKey", SqlDbType.Int, ckey);
                        commandWrapper.AddInputParameter("@ResourceKey", SqlDbType.Int, ResourceKey);
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            ADashboardModelLineChart item = null;
                            while (dataReader.Read())
                            {

                                item = new ADashboardModelLineChart();
                                LoadlinechartValue(dataReader, item);
                                itemList.Add(item);

                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {

                throw;
            }
            return itemList;
        }
        public List<ADashboardModel> BindAdminProjectsValue(int ckey, int PortalKey, int ResourceKey)
        {
            List<ADashboardModel> itemList = new List<ADashboardModel>();
            try
            {
               
                string storedProcedure = "";
                if (PortalKey == 3)
                {
                    storedProcedure = "site_dashboard_VendorProjectsValue_Copy";
                }
                else if (PortalKey == 2)
                {
                    storedProcedure = "site_dashboard_MangerProjectsValue";
                }
                else
                {
                    storedProcedure = "site_dashboard_AdministratorProjectsValue";
                }


                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        commandWrapper.AddInputParameter("@CompanyKey", SqlDbType.Int, ckey);
                        commandWrapper.AddInputParameter("@ResourceKey", SqlDbType.Int, ResourceKey);
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            ADashboardModel item = null;
                            while (dataReader.Read())
                            {

                                item = new ADashboardModel();
                                LoadProjectsValue(dataReader, item);
                                itemList.Add(item);

                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {

                throw;
            }
            return itemList;
        }
        public List<ADashboardModel> PieChartBidRequest(int ckey, int PortalKey, int ResourceKey)
        {
            List<ADashboardModel> itemList = new List<ADashboardModel>();
            try
            {

                string storedProcedure = "";
                if (PortalKey == 3)
                {
                     storedProcedure = "site_dashboard_VendorDataforamountandPie";
                }
                else if (PortalKey == 2)
                {
                     storedProcedure = "site_dashboard_ManagerDataforamountandPie";
                }
                else
                {
                     storedProcedure = "site_dashboard_Administratordata";
                }

                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        commandWrapper.AddInputParameter("@CompanyKey", SqlDbType.Int, ckey);
                        commandWrapper.AddInputParameter("@ResourceKey", SqlDbType.Int, ResourceKey);
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            ADashboardModel item = null;
                            while (dataReader.Read())
                            {

                                item = new ADashboardModel();
                                LoadStaff(dataReader, item);
                                itemList.Add(item);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return itemList;
        }

        public List<ADashboardModel> PieChartForVendor(int ckey)
        {
            List<ADashboardModel> itemList = new List<ADashboardModel>();
            try
            {
                string storedProcedure = "site_dashboard_VendorData";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        commandWrapper.AddInputParameter("@CompanyKey", SqlDbType.Int, ckey);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            ADashboardModel item = null;
                            while (dataReader.Read())
                            {

                                item = new ADashboardModel();
                                LoadVendor(dataReader, item);
                                itemList.Add(item);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return itemList;
        }
        protected void LoadVendor(DBDataReader dataReader, ADashboardModel item)
        {
            item.NotIntersted = dataReader.GetValueText("NotInterstedBidRequest");
            item.BidNotSubmitted = dataReader.GetValueText("OpenBidRequest");
            item.BidSubmitted = dataReader.GetValueText("SubmittedBidRequest");
            item.Awarded = dataReader.GetValueText("AwardedBidRequest");
            item.Rejected = dataReader.GetValueText("RejectedBidRequest");
            item.TotalAmount = Convert.ToString(String.Format("{0:0.000}", dataReader.GetValueDecimal("TotalAmount")));
        }
        protected void LoadStaff(DBDataReader dataReader, ADashboardModel item)
        {

            item.OpenBidRequest = dataReader.GetValueText("OpenBidRequest");            
            item.AwardedBidRequest = dataReader.GetValueText("AwardedBidRequest");
            item.ClosedBidRequest = dataReader.GetValueText("ClosedBidRequest");            
            item.TotalAmount = Convert.ToString(String.Format("{0:#,##0.##}"+".00", dataReader.GetValueDecimal("TotalAmount")));
            item.TotalAmount = Common.Utility.FormatNumberHelper(dataReader.GetValueDecimal("TotalAmount"), true);
            item.Type = dataReader.GetValueText("Type");
        }
        protected void LoadVendors(DBDataReader dataReader, ADashboardModel item)
        {
            item.CancelMembership = dataReader.GetValueText("CancelMembership");
            item.ActiveVendors = dataReader.GetValueText("ActiveVendors");
            item.RenewedVendors = dataReader.GetValueText("RenewedVendors");

            item.LastYearRenewedVendors = dataReader.GetValueText("LastYearRenewedVendors");
            item.lastyearActiveVendors = dataReader.GetValueText("lastyearActiveVendors");
            item.LastYearCancelMembership = dataReader.GetValueText("LastYearCancelMembership");
            if (item.LastYearRenewedVendors == null)
            {
                item.LastYearRenewedVendors = "0";
            }
            if (item.LastYearRenewedVendors == null)
            {
                item.LastYearRenewedVendors = "0";
            }
            if (item.LastYearRenewedVendors == null)
            {
                item.LastYearRenewedVendors = "0";
            }


        }
        protected void LoadProjectsValue(DBDataReader dataReader, ADashboardModel item)
        {
            item.Monthly = Convert.ToString(String.Format("{0:0}", dataReader.GetValueDecimal("Monthly"))); 
            item.Yearly = Convert.ToString(String.Format("{0:0}", dataReader.GetValueDecimal("Yearly"))); 
            item.Quaterly = Convert.ToString(String.Format("{0:0}", dataReader.GetValueDecimal("Quaterly")));
    

        }
        protected void LoadlinechartValue(DBDataReader dataReader, ADashboardModelLineChart item)
        {
            item.dayOfYear = dataReader.GetValueText("dayOfYear");
            item.projectcount = dataReader.GetValueText("projectcount");
         


        }

    }
}
