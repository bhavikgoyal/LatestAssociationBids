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
    public class VendorwithOpeninvoiceRepository: BaseRepository, IVendorswithopeninvoiceRepository
    {
        public VendorwithOpeninvoiceRepository() { }

        public VendorwithOpeninvoiceRepository(string connectionString)
          : base(connectionString) { }
       
        public List<VendorswithinvoiceModel> SearchStaff(long ReportPageSize, long PageIndex, string Search, string Sort)
        {
            List<VendorswithinvoiceModel> itemList = new List<VendorswithinvoiceModel>();
            try
            {
                string storedProcedure = "site_Vendors_invoicesIndexPaging";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters 
                        commandWrapper.AddInputParameter("@ReportPageSize", SqlDbType.Int, (ReportPageSize == 0) ? 0 : ReportPageSize);
                        commandWrapper.AddInputParameter("@PageIndex", SqlDbType.Int, (PageIndex == 0) ? 0 : PageIndex);
                        commandWrapper.AddInputParameter("@Search", SqlDbType.Text, (Search == "") ? "" : Search);
                        commandWrapper.AddInputParameter("@Sort", SqlDbType.Text, (Sort == "") ? "" : Sort);
                        //commandWrapper.AddInputParameter("@CompanyKey", SqlDbType.Int, (CompanyKey == "") ? "" : CompanyKey);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            VendorswithinvoiceModel item = null;
                            while (dataReader.Read())
                            {
                                item = new VendorswithinvoiceModel();
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
        protected void LoadStaff(DBDataReader dataReader, VendorswithinvoiceModel item)
        {

            
            item.VendorName = dataReader.GetValueText("CompanyName");
            item.ContactPerson = dataReader.GetValueText("Name");
            item.InvoiceDate = dataReader.GetValueText("InvoiceDate");
            item.Balance = dataReader.GetValueDecimal("Balance");
            

            //item.NumberOfUnits = dataReader.GetValueInt("NumberOfUnits");
            item.TotalRecords = dataReader.GetValueInt("TotalRecord");
            //item.City = dataReader.GetValueText("City");
            //item.State = dataReader.GetValueText("State");
            //item.Company = dataReader.GetValueText("Company");
        }
        public List<VendorswithinvoiceModel> Top5vendor(long ReportPageSize, long PageIndex, string Search, string Sort, long CompanyKey)
        {
            List<VendorswithinvoiceModel> itemList = new List<VendorswithinvoiceModel>();
            try
            {
                string storedProcedure = "site_BidVendor_Top5VendorsInEachCatagory";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters 
                        commandWrapper.AddInputParameter("@ReportPageSize", SqlDbType.Int, (ReportPageSize == 0) ? 0 : ReportPageSize);
                        commandWrapper.AddInputParameter("@PageIndex", SqlDbType.Int, (PageIndex == 0) ? 0 : PageIndex);
                        commandWrapper.AddInputParameter("@Search", SqlDbType.Text, (Search == "") ? "" : Search);
                        commandWrapper.AddInputParameter("@Sort", SqlDbType.Text, (Sort == "") ? "" : Sort);
                        //commandWrapper.AddInputParameter("@CompanyKey", SqlDbType.Text, (CompanyKey.ToString() == "") ? "" : CompanyKey.ToString());

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            VendorswithinvoiceModel item = null;
                            while (dataReader.Read())
                            {
                                item = new VendorswithinvoiceModel();
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
        protected void LoadStaff2(DBDataReader dataReader, VendorswithinvoiceModel item)
        {


            item.ServiceName = dataReader.GetValueText("serviename");
            item.CompanyName = dataReader.GetValueText("companyname");
            item.TotalRecords = dataReader.GetValueInt("TotalRecord");
            item.TotalBidValue = dataReader.GetValueDecimal("BidTotalvalue");
            //item.InvoiceDate = dataReader.GetValueText("InvoiceDate");

            //item.NumberOfUnits = dataReader.GetValueInt("NumberOfUnits");
            //item.TotalRecords = dataReader.GetValueInt("TotalRecord");
            //item.City = dataReader.GetValueText("City");
            //item.State = dataReader.GetValueText("State");
            //item.Company = dataReader.GetValueText("Company");
        }
        public List<VendorswithinvoiceModel> VendorwithAccept(long ReportPageSize, long PageIndex, string Search, string Sort, long CompanyKey)
        {
            List<VendorswithinvoiceModel> itemList = new List<VendorswithinvoiceModel>();
            try
            {
                string storedProcedure = "site_Vendors_with_the_most_bids_acceptedIndexPaging";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters 
                        commandWrapper.AddInputParameter("@ReportPageSize", SqlDbType.Int, (ReportPageSize == 0) ? 0 : ReportPageSize);
                        commandWrapper.AddInputParameter("@PageIndex", SqlDbType.Int, (PageIndex == 0) ? 0 : PageIndex);
                        //commandWrapper.AddInputParameter("@Search", SqlDbType.Text, (Search == "") ? "" : Search);
                        //commandWrapper.AddInputParameter("@Sort", SqlDbType.Text, (Sort == "") ? "" : Sort);
                        //commandWrapper.AddInputParameter("@CompanyKey", SqlDbType.Text, (CompanyKey.ToString() == "") ? "" : CompanyKey.ToString());

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            VendorswithinvoiceModel item = null;
                            while (dataReader.Read())
                            {
                                item = new VendorswithinvoiceModel();
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
        protected void LoadStaff3(DBDataReader dataReader, VendorswithinvoiceModel item)
        {


            item.AcceptBid = dataReader.GetValueText("AcceptedBids");
            item.VendorName = dataReader.GetValueText("VendorName");
            //item.Email = dataReader.GetValueText("Email");
            //item.Balance = dataReader.GetValueDecimal("Balance");
            //item.InvoiceDate = dataReader.GetValueText("InvoiceDate");

            //item.NumberOfUnits = dataReader.GetValueInt("NumberOfUnits");
            item.TotalRecords = dataReader.GetValueInt("TotalRecord");
            //item.City = dataReader.GetValueText("City");
            //item.State = dataReader.GetValueText("State");
            //item.Company = dataReader.GetValueText("Company");
        }
        public List<VendorswithinvoiceModel> VendorwithSubmit(long ReportPageSize, long PageIndex, string Search, string Sort, long CompanyKey)
        {
            List<VendorswithinvoiceModel> itemList = new List<VendorswithinvoiceModel>();
            try
            {
                string storedProcedure = "site_Vendors_with_the_most_bids_submitted_IndexPaging";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters 
                        commandWrapper.AddInputParameter("@ReportPageSize", SqlDbType.Int, (ReportPageSize == 0) ? 0 : ReportPageSize);
                        commandWrapper.AddInputParameter("@PageIndex", SqlDbType.Int, (PageIndex == 0) ? 0 : PageIndex);
                        //commandWrapper.AddInputParameter("@Search", SqlDbType.Text, (Search == "") ? "" : Search);
                        //commandWrapper.AddInputParameter("@Sort", SqlDbType.Text, (Sort == "") ? "" : Sort);
                        //commandWrapper.AddInputParameter("@CompanyKey", SqlDbType.Int, (CompanyKey.ToString() == "") ? "" : CompanyKey.ToString());

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            VendorswithinvoiceModel item = null;
                            while (dataReader.Read())
                            {
                                item = new VendorswithinvoiceModel();
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
        protected void LoadStaff4(DBDataReader dataReader, VendorswithinvoiceModel item)
        {


            item.SubmittedBid = dataReader.GetValueText("SubmittedBids");
            item.VendorName = dataReader.GetValueText("VendorName");
            //item.Email = dataReader.GetValueText("Email");
            //item.Balance = dataReader.GetValueDecimal("Balance");
            //item.InvoiceDate = dataReader.GetValueText("InvoiceDate");

            //item.NumberOfUnits = dataReader.GetValueInt("NumberOfUnits");
            item.TotalRecords = dataReader.GetValueInt("TotalRecord");
            //item.City = dataReader.GetValueText("City");
            //item.State = dataReader.GetValueText("State");
            //item.Company = dataReader.GetValueText("Company");
        }
        public List<VendorswithinvoiceModel> VendorwithNotAccept(long ReportPageSize, long PageIndex, string Search, string Sort, long CompanyKey)
        {
            List<VendorswithinvoiceModel> itemList = new List<VendorswithinvoiceModel>();
            try
            {
                string storedProcedure = "site_Vendors_with_the_most_bids_not_accepted_IndexPaging";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters 
                        commandWrapper.AddInputParameter("@ReportPageSize", SqlDbType.Int, (ReportPageSize == 0) ? 0 : ReportPageSize);
                        commandWrapper.AddInputParameter("@PageIndex", SqlDbType.Int, (PageIndex == 0) ? 0 : PageIndex);
                        //commandWrapper.AddInputParameter("@Search", SqlDbType.Text, (Search == "") ? "" : Search);
                        //commandWrapper.AddInputParameter("@Sort", SqlDbType.Text, (Sort == "") ? "" : Sort);
                        //commandWrapper.AddInputParameter("@CompanyKey", SqlDbType.Int, (CompanyKey.ToString() == "") ? "" : CompanyKey.ToString());

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            VendorswithinvoiceModel item = null;
                            while (dataReader.Read())
                            {
                                item = new VendorswithinvoiceModel();
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
        protected void LoadStaff5(DBDataReader dataReader, VendorswithinvoiceModel item)
        {


            item.RejectedBid = dataReader.GetValueText("RejectedBids");
            item.VendorName = dataReader.GetValueText("vendorname");
            //item.Email = dataReader.GetValueText("Email");
            //item.Balance = dataReader.GetValueDecimal("Balance");
            //item.InvoiceDate = dataReader.GetValueText("InvoiceDate");

            //item.NumberOfUnits = dataReader.GetValueInt("NumberOfUnits");
            item.TotalRecords = dataReader.GetValueInt("TotalRecord");
            //item.City = dataReader.GetValueText("City");
            //item.State = dataReader.GetValueText("State");
            //item.Company = dataReader.GetValueText("Company");
        }

        public IList<VendorswithinvoiceModel> GetAllState()
        {
            List<VendorswithinvoiceModel> itemList = new List<VendorswithinvoiceModel>();

            try
            {
                string storedProcedure = "site_Service_SelectAll";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            VendorswithinvoiceModel item = null;
                            while (dataReader.Read())
                            {
                                item = new VendorswithinvoiceModel();
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
        protected void GetAllState(DBDataReader dataReader, VendorswithinvoiceModel item)
        {
            item.StateKey = dataReader.GetValueText("ServiceKey");
            item.State = dataReader.GetValueText("Title");
        }
    }
}

