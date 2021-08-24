using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using AssociationBids.Portal.Repository.Base.Interface;
using DB_con;

namespace AssociationBids.Portal.Repository.Base.Code
{
    public class AcitivitybyManagementCompanyRepository : BaseRepository, IAcitivitybyManagementCompanyRepository
    {
        public AcitivitybyManagementCompanyRepository() { }

        public AcitivitybyManagementCompanyRepository(string connectionString)
          : base(connectionString) { }

        public List<AcitivitybyManagementCompanyModel> Activity(long ReportPageSize, long PageIndex, string Search, string Sort,long CompanyKey)
        {
            List<AcitivitybyManagementCompanyModel> itemList = new List<AcitivitybyManagementCompanyModel>();
            try
            {
                string storedProcedure = "site_Company_Acitivity_by_Management_Company_IndexPaging";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters 
                        commandWrapper.AddInputParameter("@ReportPageSize", SqlDbType.Int, (ReportPageSize == 0) ? 0 : ReportPageSize);
                        commandWrapper.AddInputParameter("@PageIndex", SqlDbType.Int, (PageIndex == 0) ? 0 : PageIndex);
                        //commandWrapper.AddInputParameter("@Search", SqlDbType.Text, (Search == "") ? "" : Search);
                        commandWrapper.AddInputParameter("@Sort", SqlDbType.Text, (Sort == "") ? "" : Sort);
                        commandWrapper.AddInputParameter("@CompanyKey", SqlDbType.Text, (CompanyKey.ToString() == "") ? "" : CompanyKey.ToString());

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            AcitivitybyManagementCompanyModel item = null;
                            while (dataReader.Read())
                            {
                                item = new AcitivitybyManagementCompanyModel();
                                LoadStaff(dataReader, item);
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
        protected void LoadStaff(DBDataReader dataReader, AcitivitybyManagementCompanyModel item)
        {


            item.CompanyName = dataReader.GetValueText("CompanyName");
            item.ResourceType = dataReader.GetValueText("ResourceType");
            item.Title = dataReader.GetValueText("Title");
            
            //item.BidTitle = dataReader.GetValueText("BidTitle");
            //item.Titless = dataReader.GetValueText("Title");

            //item.NumberOfUnits = dataReader.GetValueInt("NumberOfUnits");
            item.TotalRecords = dataReader.GetValueInt("TotalRecord");
            //item.City = dataReader.GetValueText("City");
            //item.State = dataReader.GetValueText("State");
            //item.Company = dataReader.GetValueText("Company");
        }
        public List<AcitivitybyManagementCompanyModel> ActivityVendor(long ReportPageSize, long PageIndex, string Search, string Sort, long CompanyKey)
        {
            List<AcitivitybyManagementCompanyModel> itemList = new List<AcitivitybyManagementCompanyModel>();
            if (Search == "")
            {
                
            }
            else if(Search != "0")
            {
                CompanyKey = Convert.ToInt32(Search);
            }
            try
            {
                string storedProcedure = "site_Company_Acitivity_by_Vendor_Company_IndexPaging";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters 
                        commandWrapper.AddInputParameter("@ReportPageSize", SqlDbType.Int, (ReportPageSize == 0) ? 0 : ReportPageSize);
                        commandWrapper.AddInputParameter("@PageIndex", SqlDbType.Int, (PageIndex == 0) ? 0 : PageIndex);
                        //commandWrapper.AddInputParameter("@Search", SqlDbType.Text, (Search == "") ? "" : Search);
                        commandWrapper.AddInputParameter("@Sort", SqlDbType.Text, (Sort == "") ? "" : Sort);
                        commandWrapper.AddInputParameter("@VendorKey", SqlDbType.Text, (CompanyKey.ToString() == "") ? "" : CompanyKey.ToString());

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            AcitivitybyManagementCompanyModel item = null;
                            while (dataReader.Read())
                            {
                                item = new AcitivitybyManagementCompanyModel();
                                LoadStaff2(dataReader, item);
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
        protected void LoadStaff2(DBDataReader dataReader, AcitivitybyManagementCompanyModel item)
        {


            item.CompanyName = dataReader.GetValueText("CompanyName");
            item.ResourceType = dataReader.GetValueText("ResourceType");
            item.Title = dataReader.GetValueText("Title");
            item.Statuss = dataReader.GetValueText("Status");
            //item.Titless = dataReader.GetValueText("Title");

            //item.NumberOfUnits = dataReader.GetValueInt("NumberOfUnits");
            item.TotalRecords = dataReader.GetValueInt("TotalRecord");
            //item.City = dataReader.GetValueText("City");
            //item.State = dataReader.GetValueText("State");
            //item.Company = dataReader.GetValueText("Company");
        }

        public List<AcitivitybyManagementCompanyModel> VendorPortalActivity(long ReportPageSize, long PageIndex, string Search, string Sort, long ResourceKey)
        {
            List<AcitivitybyManagementCompanyModel> itemList = new List<AcitivitybyManagementCompanyModel>();
            try
            {
                string storedProcedure = "site_Company_Acitivity_by_Vendor_Company_IndexPagingForVendor";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters 
                        commandWrapper.AddInputParameter("@ReportPageSize", SqlDbType.Int, (ReportPageSize == 0) ? 0 : ReportPageSize);
                        commandWrapper.AddInputParameter("@PageIndex", SqlDbType.Int, (PageIndex == 0) ? 0 : PageIndex);
                        commandWrapper.AddInputParameter("@Search", SqlDbType.Text, (Search == "") ? "" : Search);
                        commandWrapper.AddInputParameter("@Sort", SqlDbType.Text, (Sort == "") ? "" : Sort);
                        commandWrapper.AddInputParameter("@ResourceKey", SqlDbType.Text, (ResourceKey.ToString() == "") ? "" : ResourceKey.ToString());

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            AcitivitybyManagementCompanyModel item = null;
                            while (dataReader.Read())
                            {
                                item = new AcitivitybyManagementCompanyModel();
                                LoadStaff3(dataReader, item);
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
        protected void LoadStaff3(DBDataReader dataReader, AcitivitybyManagementCompanyModel item)
        {


            item.CompanyName = dataReader.GetValueText("CompanyName");
            item.Title = dataReader.GetValueText("PropertyTitle");
            item.BidRequest = dataReader.GetValueDecimal("BidRequestAmount");
            item.BidTitle = dataReader.GetValueText("BidTitle");
            item.Titless = dataReader.GetValueText("Title");

            //item.NumberOfUnits = dataReader.GetValueInt("NumberOfUnits");
            item.TotalRecords = dataReader.GetValueInt("TotalRecord");
            //item.City = dataReader.GetValueText("City");
            //item.State = dataReader.GetValueText("State");
            //item.Company = dataReader.GetValueText("Company");
        }


        public List<AcitivitybyManagementCompanyModel> ActivityAssociation(long ReportPageSize, long PageIndex, string Search, string Sort, long CompanyKey)
        {
            List<AcitivitybyManagementCompanyModel> itemList = new List<AcitivitybyManagementCompanyModel>();
            try
            {
                string storedProcedure = "site_Company_Acitivity_by_AssociationBids_Company_IndexPaging";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters 
                        commandWrapper.AddInputParameter("@ReportPageSize", SqlDbType.Int, (ReportPageSize == 0) ? 0 : ReportPageSize);
                        commandWrapper.AddInputParameter("@PageIndex", SqlDbType.Int, (PageIndex == 0) ? 0 : PageIndex);
                        commandWrapper.AddInputParameter("@PropertyKey", SqlDbType.Text, (Search == "") ? "" : Search);
                        commandWrapper.AddInputParameter("@Sort", SqlDbType.Text, (Sort == "") ? "" : Sort);
                        commandWrapper.AddInputParameter("@CompanyKey", SqlDbType.Text, (CompanyKey.ToString() == "") ? "" : CompanyKey.ToString());

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            AcitivitybyManagementCompanyModel item = null;
                            while (dataReader.Read())
                            {
                                item = new AcitivitybyManagementCompanyModel();
                                LoadStaff4(dataReader, item);
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
        protected void LoadStaff4(DBDataReader dataReader, AcitivitybyManagementCompanyModel item)
        {


            item.Title = dataReader.GetValueText("PropertyName");
            item.ResourceType = dataReader.GetValueText("ResourceType");
            item.Titles = dataReader.GetValueText("Title");
            item.Status = dataReader.GetValueText("Status");

            //item.NumberOfUnits = dataReader.GetValueInt("NumberOfUnits");
            item.TotalRecords = dataReader.GetValueInt("TotalRecord");
            //item.City = dataReader.GetValueText("City");
            //item.State = dataReader.GetValueText("State");
            //item.Company = dataReader.GetValueText("Company");
        }
        public List<AcitivitybyManagementCompanyModel> ActivityByManager(long ReportPageSize, long PageIndex, string Search, string Sort, long CompanyKey,long PortalKey)
        {
            List<AcitivitybyManagementCompanyModel> itemList = new List<AcitivitybyManagementCompanyModel>();
            try
            {
                string storedProcedure = "site_Company_Acitivity_by_Manager_Company_IndexPaging";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters 
                        commandWrapper.AddInputParameter("@ReportPageSize", SqlDbType.Int, (ReportPageSize == 0) ? 0 : ReportPageSize);
                        commandWrapper.AddInputParameter("@PageIndex", SqlDbType.Int, (PageIndex == 0) ? 0 : PageIndex);
                        //commandWrapper.AddInputParameter("@Search", SqlDbType.Text, (Search == "") ? "" : Search);
                        commandWrapper.AddInputParameter("@Sort", SqlDbType.Text, (Sort == "") ? "" : Sort);
                        commandWrapper.AddInputParameter("@CompanyKey", SqlDbType.Text, (CompanyKey.ToString() == "") ? "" : CompanyKey.ToString());
                        commandWrapper.AddInputParameter("@ManagerKey", SqlDbType.Text, (Search.ToString() == "") ? "" : Search.ToString());

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            AcitivitybyManagementCompanyModel item = null;
                            while (dataReader.Read())
                            {
                                item = new AcitivitybyManagementCompanyModel();
                                LoadStaff5(dataReader, item);
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
        protected void LoadStaff5(DBDataReader dataReader, AcitivitybyManagementCompanyModel item)
        {


            item.Title = dataReader.GetValueText("PropertyName");
            item.ResourceType = dataReader.GetValueText("ResourceType");
            item.Titles = dataReader.GetValueText("Title");
            item.Status = dataReader.GetValueText("Status");


            //item.NumberOfUnits = dataReader.GetValueInt("NumberOfUnits");
            item.TotalRecords = dataReader.GetValueInt("TotalRecord");
            //item.City = dataReader.GetValueText("City");
            //item.State = dataReader.GetValueText("State");
            //item.Company = dataReader.GetValueText("Company");
        }

        public List<AcitivitybyManagementCompanyModel> GetAllState()
        {
            List<AcitivitybyManagementCompanyModel> itemList = new List<AcitivitybyManagementCompanyModel>();

            try
            {
                string storedProcedure = "site_Report_CompanyGet";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            AcitivitybyManagementCompanyModel item = null;
                            while (dataReader.Read())
                            {
                                item = new AcitivitybyManagementCompanyModel();
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
        protected void GetAllState(DBDataReader dataReader, AcitivitybyManagementCompanyModel item)
        {
            item.StateKey = dataReader.GetValueText("CompanyKey");
            item.State = dataReader.GetValueText("CompanyName");
        }

        public List<AcitivitybyManagementCompanyModel> GetAllVendorList(int CompanyKey)
        {
            List<AcitivitybyManagementCompanyModel> itemList = new List<AcitivitybyManagementCompanyModel>();

            try
            {
                string storedProcedure = "site_Report_VendorCompany_GetAll";
                using (Database db = new Database(ConnectionString))
                {
                    
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        commandWrapper.AddInputParameter("@CompanyKey", SqlDbType.Int, (CompanyKey == 0) ? 0 : CompanyKey);
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            AcitivitybyManagementCompanyModel item = null;
                            while (dataReader.Read())
                            {
                                item = new AcitivitybyManagementCompanyModel();
                                GetAllVendorList(dataReader, item);
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
        protected void GetAllVendorList(DBDataReader dataReader, AcitivitybyManagementCompanyModel item)
        {
            item.StateKey = dataReader.GetValueText("vendorkey");
            item.State = dataReader.GetValueText("VendorName");
        }

        public List<AcitivitybyManagementCompanyModel> GetAllProperty(int CompanyKey)
        {
            List<AcitivitybyManagementCompanyModel> itemList = new List<AcitivitybyManagementCompanyModel>();

            try
            {
                string storedProcedure = "site_Report_PropertyWiseData_GetAll";
                using (Database db = new Database(ConnectionString))
                {

                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        commandWrapper.AddInputParameter("@CompanyKey", SqlDbType.Int, (CompanyKey == 0) ? 0 : CompanyKey);
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            AcitivitybyManagementCompanyModel item = null;
                            while (dataReader.Read())
                            {
                                item = new AcitivitybyManagementCompanyModel();
                                GetAllProperty(dataReader, item);
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
        protected void GetAllProperty(DBDataReader dataReader, AcitivitybyManagementCompanyModel item)
        {
            item.StateKey = dataReader.GetValueText("PropertyKey");
            item.State = dataReader.GetValueText("Title");
        }

        public List<AcitivitybyManagementCompanyModel> GetAllManager(int CompanyKey)
        {
            List<AcitivitybyManagementCompanyModel> itemList = new List<AcitivitybyManagementCompanyModel>();

            try
            {
                string storedProcedure = "site_Report_ManagerWiseData_GetAll";
                using (Database db = new Database(ConnectionString))
                {

                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        commandWrapper.AddInputParameter("@CompanyKey", SqlDbType.Int, (CompanyKey == 0) ? 0 : CompanyKey);
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            AcitivitybyManagementCompanyModel item = null;
                            while (dataReader.Read())
                            {
                                item = new AcitivitybyManagementCompanyModel();
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
        protected void GetAllManager(DBDataReader dataReader, AcitivitybyManagementCompanyModel item)
        {
            item.StateKey = dataReader.GetValueText("ManagerKey");
            item.State = dataReader.GetValueText("Manager");
        }
    }
}
