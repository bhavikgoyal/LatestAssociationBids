using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AssociationBids.Portal.Repository.Base.Code
{
    public class PMVendorRepository : BaseRepository, IPMVendorRepository
    {
        public PMVendorRepository() { }

        public PMVendorRepository(string connectionString)
         : base(connectionString) { }

        protected void Load(DBDataReader dataReader, VendorModel item)
        {
            item.CompanyKey = dataReader.GetValueInt("CompanyKey");
            item.LegalName = dataReader.GetValueText("LegalName");
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
        }

        protected void LoadViewEdit(DBDataReader dataReader, VendorModel item)
        {
            item.CompanyKey = dataReader.GetValueInt("CompanyKey");
            item.CompanyName = dataReader.GetValueText("CompanyName");
            item.Name = dataReader.GetValueText("Name");
            item.Fax = dataReader.GetValueText("Fax");
            item.VendorKey = dataReader.GetValueInt("VendorKey");
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
            item.invited = dataReader.GetValueText("invited");
            item.Status = dataReader.GetValueInt("status");
        }

        protected void Loadbidrequest(DBDataReader dataReader, VendorModel item)
        {
            item.BidRequestKey = dataReader.GetValueInt("BidRequestKey");
            item.CompanyKey = dataReader.GetValueInt("CompanyKey");
            item.Title = dataReader.GetValueText("Title");       
            item.BidDueDate = dataReader.GetValueText("BidDueDate");
            item.NoofBid = dataReader.GetValueInt("NoofBid");
            item.BidRequestStatus = dataReader.GetValueText("BidRequestStatus");
            item.sstartddate = dataReader.GetValueText("sstartddate");
        }

        protected void LoadWorkOrder(DBDataReader dataReader, VendorModel item)
        {
            item.BidRequestKey = dataReader.GetValueInt("BidRequestKey");
            item.CompanyKey = dataReader.GetValueInt("CompanyKey");
            item.Title = dataReader.GetValueText("Title");
            item.Propertyname = dataReader.GetValueText("Propertyname");
            item.BidDueDate = dataReader.GetValueText("BidDueDate");
            item.NoofBid = dataReader.GetValueInt("NoofBid");
            item.BidRequestStatus = dataReader.GetValueText("BidRequestStatus");
            item.sstartddate = dataReader.GetValueText("sstartddate");
        }

        protected void LoadVendor(DBDataReader dataReader, VendorModel item)
        {
            item.CompanyKey = dataReader.GetValueInt("CompanyKey");
            item.LegalName = dataReader.GetValueText("Name");
            item.Work = dataReader.GetValueText("Work");
            item.Title = dataReader.GetValueText("SeriveTitle");
            item.TotalRecords = dataReader.GetValueInt("TotalRecord");
            item.star = dataReader.GetValueText("star");
            item.invited = dataReader.GetValueText("invited");
            item.Duplicate = dataReader.GetValueText("Duplicate");
        }

        protected void loadFeedback(DBDataReader dataReader, VendorModel item)
        {
            item.VendorKey = dataReader.GetValueInt("VendorKey");
            item.RatingOne = dataReader.GetValueInt("RatingOne");
            item.RatingTwo = dataReader.GetValueInt("RatingTwo");
            item.RatingThree = dataReader.GetValueInt("RatingThree");
            item.RatingFour = dataReader.GetValueInt("RatingFour");
            item.RatingFive = dataReader.GetValueInt("RatingFive");
            item.lastmodtime = dataReader.GetValueText("lastmodtime");
            item.Message = dataReader.GetValueText("Message");
            item.TotalRecords = dataReader.GetValueInt("TotalRecord");
        }

        protected void LoadInsurance(DBDataReader dataReader, VendorModel item)
        {
            item.InsuranceKey = dataReader.GetValueInt("InsuranceKey");
            item.PolicyNumber = dataReader.GetValueText("PolicyNumber");
            item.RenewalDate = dataReader.GetValueDateTime("RenewalDate");
            item.InsuranceAmount = dataReader.GetValueInt("InsuranceAmount");
            item.StartDate = dataReader.GetValueDateTime("StartDate");
            item.EndDate = dataReader.GetValueDateTime("EndDate");
        }

        protected void LoadHistoryTabdata(DBDataReader dataReader, VendorModel item)
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
            item.Address = dataReader.GetValueText("Address");
            item.Address2 = dataReader.GetValueText("Address2");
            item.City = dataReader.GetValueText("City");
            item.State = dataReader.GetValueText("State");
            item.Zip = dataReader.GetValueText("Zip");
            item.CompanyName = dataReader.GetValueText("CompanyName");
            item.VendorName = dataReader.GetValueText("VendorName");
            item.Email = dataReader.GetValueText("Email");
            item.CellPhone = dataReader.GetValueText("CellPhone");
        }

        protected void LoadVendorByBidRequest(DBDataReader dataReader, VendorModel item)
        {
            int bidrequest = item.BidRequestKey;
            item.Name = dataReader.GetValueText("Name");
            item.ContactPerson = dataReader.GetValueText("ContactPerson");
            item.Service = dataReader.GetValueText("Service");
            item.CompanyKey = dataReader.GetValueInt("CompanyKey");
            item.ResponseDueDates = dataReader.GetValueText("DefaultRespondByDate");
            item.BidAmount = dataReader.GetValueDecimal("BidAmount");
            item.BidReqStatus = dataReader.GetValueText("BidVendorStatus");
            item.VendorKey = dataReader.GetValueInt("VendorKey");
            item.BidVendorKey = dataReader.GetValueInt("BidVendorKey");
        }

        public List<VendorModel> SearchVendorByBidRequest(int BidRequestKey, int modulekey)
        {
            List<VendorModel> itemList = new List<VendorModel>();
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
                            VendorModel item = null;
                            while (dataReader.Read())
                            {
                                item = new VendorModel();
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

        public List<VendorModel> SearchVendorByBidRequest(int BidRequestKey, int modulekey, int resourcekey)
        {
            List<VendorModel> itemList = new List<VendorModel>();
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
                            VendorModel item = null;
                            while (dataReader.Read())
                            {
                                item = new VendorModel();
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
        public virtual VendorModel GetBidDataForProperties(int BidRequestKey,int CompanyKey)
        {
            VendorModel item = null;

            try
            {
                string storedProcedure = "site_Vendor_getBidrequestDetails";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@BidRequestKey", SqlDbType.Int, BidRequestKey);
                        commandWrapper.AddInputParameter("@CompanyKey", SqlDbType.Int, CompanyKey);
                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            while (dataReader.Read())
                            {
                                item = new VendorModel();
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

        public Int64 Insert(VendorModel item, int ResourceKey)
        {
            int status = 0;
            int ResKey = 0; 
            try
            {
                string firstname = "";
                string lastname = "";
                try
                {
                    if (item.Work != null)
                    {
                        firstname = item.Work.Split(' ')[0];
                        lastname = item.Work.Split(' ')[1];
                    }
                }
                catch { }
                string storedProcedure = "site_Vendor_Insert_New";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@Resourcekey", SqlDbType.Int, ResourceKey);
                        commandWrapper.AddInputParameter("@CompanyName ", SqlDbType.VarChar, String.IsNullOrEmpty(item.LegalName) ? 0 : item.LegalName.Length, String.IsNullOrEmpty(item.LegalName) ? SqlString.Null : item.LegalName);
                        commandWrapper.AddInputParameter("@Address", SqlDbType.VarChar, String.IsNullOrEmpty(item.Address) ? 0 : item.Address.Length, String.IsNullOrEmpty(item.Address) ? SqlString.Null : item.Address);
                        commandWrapper.AddInputParameter("@Address2", SqlDbType.VarChar, String.IsNullOrEmpty(item.Address2) ? 0 : item.Address2.Length, String.IsNullOrEmpty(item.Address2) ? SqlString.Null : item.Address2);
                        commandWrapper.AddInputParameter("@City", SqlDbType.VarChar, String.IsNullOrEmpty(item.City) ? 0 : item.City.Length, String.IsNullOrEmpty(item.City) ? SqlString.Null : item.City);
                        commandWrapper.AddInputParameter("@StateKey", SqlDbType.VarChar, String.IsNullOrEmpty(item.State) ? 0 : item.State.Length, String.IsNullOrEmpty(item.State) ? SqlString.Null : item.State);
                        commandWrapper.AddInputParameter("@Zip", SqlDbType.VarChar, String.IsNullOrEmpty(item.Zip) ? 0 : item.Zip.Length, String.IsNullOrEmpty(item.Zip) ? SqlString.Null : item.Zip);
                        commandWrapper.AddInputParameter("@Work", SqlDbType.VarChar, String.IsNullOrEmpty(item.Work) ? 0 : item.Work.Length, String.IsNullOrEmpty(item.Work) ? SqlString.Null : item.Work);
                        commandWrapper.AddInputParameter("@FirstName", SqlDbType.VarChar, String.IsNullOrEmpty(firstname) ? 0 : firstname.Length, String.IsNullOrEmpty(firstname) ? SqlString.Null : firstname);
                        commandWrapper.AddInputParameter("@LastName", SqlDbType.VarChar, String.IsNullOrEmpty(lastname) ? 0 : lastname.Length, String.IsNullOrEmpty(lastname) ? SqlString.Null : lastname);
                        commandWrapper.AddInputParameter("@Work2", SqlDbType.VarChar, String.IsNullOrEmpty(item.Work2) ? 0 : item.Work2.Length, String.IsNullOrEmpty(item.Work2) ? SqlString.Null : item.Work2);
                        commandWrapper.AddInputParameter("@Email", SqlDbType.VarChar, String.IsNullOrEmpty(item.Email) ? 0 : item.Email.Length, String.IsNullOrEmpty(item.Email) ? SqlString.Null : item.Email);
                        commandWrapper.AddInputParameter("@Fax", SqlDbType.VarChar, String.IsNullOrEmpty(item.Fax) ? 0 : item.Fax.Length, String.IsNullOrEmpty(item.Fax) ? SqlString.Null : item.Fax);
                        commandWrapper.AddInputParameter("@Website", SqlDbType.VarChar, String.IsNullOrEmpty(item.Website) ? 0 : item.Website.Length, String.IsNullOrEmpty(item.Website) ? SqlString.Null : item.Website);
                        //commandWrapper.AddInputParameter("@ServiceTitle1", SqlDbType.VarChar, String.IsNullOrEmpty(item.ServiceTitle1) ? 0 : item.ServiceTitle1.Length, String.IsNullOrEmpty(item.ServiceTitle1) ? SqlString.Null : item.ServiceTitle1);
                        commandWrapper.AddInputParameter("@Title ", SqlDbType.VarChar, String.IsNullOrEmpty(item.Title) ? 0 : item.Title.Length, String.IsNullOrEmpty(item.Title) ? SqlString.Null : item.Title);
                        //commandWrapper.AddInputParameter("@CellPhone", SqlDbType.VarChar, String.IsNullOrEmpty(item.CellPhone) ? 0 : item.CellPhone.Length, String.IsNullOrEmpty(item.CellPhone) ? SqlString.Null : item.CellPhone);
                        //commandWrapper.AddInputParameter("@PolicyNumber", SqlDbType.VarChar, String.IsNullOrEmpty(item.PolicyNumber) ? 0 : item.PolicyNumber.Length, String.IsNullOrEmpty(item.PolicyNumber) ? SqlString.Null : item.PolicyNumber);
                        //commandWrapper.AddInputParameter("@InsuranceAmount", SqlDbType.Int, (item.InsuranceAmount == 0) ? SqlInt32.Null : item.InsuranceAmount);
                        //commandWrapper.AddInputParameter("@StartDate", SqlDbType.DateTime, (item.StartDate == DateTime.MinValue) ? SqlDateTime.Null : item.StartDate);
                        //commandWrapper.AddInputParameter("@EndDate", SqlDbType.DateTime, (item.EndDate == DateTime.MinValue) ? SqlDateTime.Null : item.EndDate);
                        //commandWrapper.AddInputParameter("@RenewalDate", SqlDbType.DateTime, (item.RenewalDate == DateTime.MinValue) ? SqlDateTime.Null : item.RenewalDate);
                        commandWrapper.AddInputParameter("@Description", SqlDbType.VarChar, String.IsNullOrEmpty(item.Description) ? 0 : item.Description.Length, String.IsNullOrEmpty(item.Description) ? SqlString.Null : item.Description);

                        commandWrapper.AddOutputParameter("@companyvalue", SqlDbType.Int);
                        commandWrapper.AddOutputParameter("@ResValue", SqlDbType.Int);

                        db.ExecuteNonQuery(commandWrapper);
                        status = commandWrapper.GetValueInt("@companyvalue");
                        ResKey = commandWrapper.GetValueInt("@ResValue");
                        if (status != 0)
                        {
                            VendorInvetationmailsend(status, item.Email, item.Work, item.LegalName, ResKey);
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
       
        public Int64 VendorEdit(VendorModel item)
        {
            Int64 status = 0;
            try
            {
                item.Status = 101;
                if (item.State == "0" || item.State == null)
                {
                    item.State = "";
                }


                string firstname = "";
                string lastname = "";
                try
                {
                    if (item.Name != null)
                    {
                        firstname = item.Name.Split(' ')[0];
                        lastname = item.Name.Split(' ')[1];
                    }
                }
                catch { }
                string storedProcedure = "site_Vendor_Update_New";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@companyKey", SqlDbType.Int, (item.CompanyKey == 0) ? SqlInt32.Null : item.CompanyKey);
                        commandWrapper.AddInputParameter("@companyName ", SqlDbType.VarChar, String.IsNullOrEmpty(item.CompanyName) ? 0 : item.CompanyName.Length, String.IsNullOrEmpty(item.CompanyName) ? SqlString.Null : item.CompanyName);
                        commandWrapper.AddInputParameter("@Address", SqlDbType.VarChar, String.IsNullOrEmpty(item.Address) ? 0 : item.Address.Length, String.IsNullOrEmpty(item.Address) ? SqlString.Null : item.Address);
                        commandWrapper.AddInputParameter("@Address2", SqlDbType.VarChar, String.IsNullOrEmpty(item.Address2) ? 0 : item.Address2.Length, String.IsNullOrEmpty(item.Address2) ? SqlString.Null : item.Address2);
                        commandWrapper.AddInputParameter("@City", SqlDbType.VarChar, String.IsNullOrEmpty(item.City) ? 0 : item.City.Length, String.IsNullOrEmpty(item.City) ? SqlString.Null : item.City);
                        commandWrapper.AddInputParameter("@StateKey", SqlDbType.VarChar, String.IsNullOrEmpty(item.State) ? 0 : item.State.Length, String.IsNullOrEmpty(item.State) ? SqlString.Null : item.State);
                        commandWrapper.AddInputParameter("@Zip", SqlDbType.VarChar, String.IsNullOrEmpty(item.Zip) ? 0 : item.Zip.Length, String.IsNullOrEmpty(item.Zip) ? SqlString.Null : item.Zip);
                       commandWrapper.AddInputParameter("@Work", SqlDbType.VarChar, String.IsNullOrEmpty(item.Work) ? 0 : item.Work.Length, String.IsNullOrEmpty(item.Work) ? SqlString.Null : item.Work);
                        commandWrapper.AddInputParameter("@CellPhone", SqlDbType.VarChar, String.IsNullOrEmpty(item.CellPhone) ? 0 : item.CellPhone.Length, String.IsNullOrEmpty(item.CellPhone) ? SqlString.Null : item.CellPhone);
                        commandWrapper.AddInputParameter("@FirstName", SqlDbType.VarChar, String.IsNullOrEmpty(firstname) ? 0 : firstname.Length, String.IsNullOrEmpty(firstname) ? SqlString.Null : firstname);
                        commandWrapper.AddInputParameter("@LastName", SqlDbType.VarChar, String.IsNullOrEmpty(lastname) ? 0 : lastname.Length, String.IsNullOrEmpty(lastname) ? SqlString.Null : lastname);
                        commandWrapper.AddInputParameter("@email", SqlDbType.VarChar, String.IsNullOrEmpty(item.Email) ? 0 : item.Email.Length, String.IsNullOrEmpty(item.Email) ? SqlString.Null : item.Email);
                        commandWrapper.AddInputParameter("@Status", SqlDbType.Int, (Convert.ToInt32(item.Status) == 0) ? SqlInt32.Null : Convert.ToInt32(item.Status));

                        db.ExecuteNonQuery(commandWrapper);
                        //status = commandWrapper.GetValueInt("@ResourceValue");

                        //VendorInvetationmailsend(item.Status, item.Email, item.Name,item.CompanyName);
                        VendorInvetationmailsend(item.CompanyKey, item.Email, item.Name, item.CompanyName);

                        return status;
                    }
                }
            }
            catch (Exception ex)
            {
                // error occured...
                throw;
            }
        }

        public virtual bool insuranceEdit(VendorModel item)
        {
            bool status = false;

            try
            {
                item.Status = 101;
                string storedProcedure = "site_Vendor_Update";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@CompanyName ", SqlDbType.VarChar, String.IsNullOrEmpty(item.CompanyName) ? 0 : item.CompanyName.Length, String.IsNullOrEmpty(item.CompanyName) ? SqlString.Null : item.CompanyName);
                        commandWrapper.AddInputParameter("@Address", SqlDbType.VarChar, String.IsNullOrEmpty(item.Address) ? 0 : item.Address.Length, String.IsNullOrEmpty(item.Address) ? SqlString.Null : item.Address);
                        commandWrapper.AddInputParameter("@Address2", SqlDbType.VarChar, String.IsNullOrEmpty(item.Address2) ? 0 : item.Address2.Length, String.IsNullOrEmpty(item.Address2) ? SqlString.Null : item.Address2);
                        commandWrapper.AddInputParameter("@City", SqlDbType.VarChar, String.IsNullOrEmpty(item.City) ? 0 : item.City.Length, String.IsNullOrEmpty(item.City) ? SqlString.Null : item.City);
                        commandWrapper.AddInputParameter("@StateKey", SqlDbType.VarChar, String.IsNullOrEmpty(item.State) ? 0 : item.State.Length, String.IsNullOrEmpty(item.State) ? SqlString.Null : item.State);
                        commandWrapper.AddInputParameter("@Zip", SqlDbType.VarChar, String.IsNullOrEmpty(item.Zip) ? 0 : item.Zip.Length, String.IsNullOrEmpty(item.Zip) ? SqlString.Null : item.Zip);
                        commandWrapper.AddInputParameter("@Work", SqlDbType.VarChar, String.IsNullOrEmpty(item.Work) ? 0 : item.Work.Length, String.IsNullOrEmpty(item.Work) ? SqlString.Null : item.Work);
                        commandWrapper.AddInputParameter("@Email", SqlDbType.VarChar, String.IsNullOrEmpty(item.Email) ? 0 : item.Email.Length, String.IsNullOrEmpty(item.Email) ? SqlString.Null : item.Email);
                        commandWrapper.AddInputParameter("@policynumber", SqlDbType.VarChar, String.IsNullOrEmpty(item.PolicyNumber) ? 0 : item.PolicyNumber.Length, String.IsNullOrEmpty(item.PolicyNumber) ? SqlString.Null : item.PolicyNumber);
                        commandWrapper.AddInputParameter("@insuranceamount", SqlDbType.Money, (item.InsuranceAmount == 0) ? SqlMoney.Null : item.InsuranceAmount);
                        commandWrapper.AddInputParameter("@startdate", SqlDbType.DateTime, (item.StartDate == DateTime.MinValue) ? SqlDateTime.Null : item.StartDate);
                        commandWrapper.AddInputParameter("@enddate", SqlDbType.DateTime, (item.EndDate == DateTime.MinValue) ? SqlDateTime.Null : item.EndDate);
                        commandWrapper.AddInputParameter("@renewaldate", SqlDbType.DateTime, (item.RenewalDate == DateTime.MinValue) ? SqlDateTime.Null : item.RenewalDate);
                        commandWrapper.AddInputParameter("@Status", SqlDbType.Int, (Convert.ToInt32(item.Status) == 0) ? SqlInt32.Null : Convert.ToInt32(item.Status));

                        db.ExecuteNonQuery(commandWrapper);

                        return status;
                    }
                }
            }
            catch (Exception ex)
            {
                // error occured...
                throw;
            }
        }

        protected void Getbindservice(DBDataReader dataReader, VendorModel item)
        {
            try
            {
                item.Title = dataReader.GetValueText("Title");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void GetbindDocument(DBDataReader dataReader, VendorModel item)
        {
            try
            {
                item.FileName = dataReader.GetValueText("FileName");
                item.DocumentId = dataReader.GetValueInt("DocumentKey");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IList<VendorModel> GetbindDocument(int CompanyKey, int ModuleKey)
        {
            List<VendorModel> itemList = new List<VendorModel>();

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
                            VendorModel item = null;
                            while (dataReader.Read())
                            {
                                item = new VendorModel();
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

        public IList<VendorModel> GetbindDocument1(int CompanyKey, int ModuleKey)
        {
            List<VendorModel> itemList = new List<VendorModel>();

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
                            VendorModel item = null;
                            while (dataReader.Read())
                            {
                                item = new VendorModel();
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

        public IList<VendorModel> GetbindDocumentByCompanyKey(int CompanyKey)
        {
            List<VendorModel> itemList = new List<VendorModel>();
            try
            {
                string storedProcedure = "site_VendorDocument_SelectAll_VendorView";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        commandWrapper.AddInputParameter("@CompanyKey", SqlDbType.Int, CompanyKey);
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            VendorModel item = null;
                            while (dataReader.Read())
                            {
                                item = new VendorModel();
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

        public IList<VendorModel> Getbindservice(int CompanyKey)
        {
            List<VendorModel> itemList = new List<VendorModel>();

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
                            VendorModel item = null;
                            while (dataReader.Read())
                            {
                                item = new VendorModel();
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

        public List<VendorModel> SearchVendor(long PageSize, long PageIndex, string Search, string Sort, int ResourceKey, string service, string checkstar, string Invited,string Duplicate)
        {
            List<VendorModel> itemList = new List<VendorModel>();
            try
            {
                if(service == null || service == "--Please Select--")
                {
                    service = "";
                }
                
                string storedProcedure = "site_Vendor_SelectIndexPagingNew";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters 
                        commandWrapper.AddInputParameter("@PageSize", SqlDbType.BigInt, (PageSize == 0) ? 0 : PageSize);
                        commandWrapper.AddInputParameter("@PageIndex", SqlDbType.BigInt, (PageIndex == 0) ? 0 : PageIndex);
                        commandWrapper.AddInputParameter("@Search", SqlDbType.NText, (Search == "") ? "" : Search);
                        commandWrapper.AddInputParameter("@service", SqlDbType.NText, (service == "") ? "" : service);
                        commandWrapper.AddInputParameter("@Sort", SqlDbType.NText, (Sort == "") ? "" : Sort);
                        commandWrapper.AddInputParameter("@checkstar", SqlDbType.VarChar, String.IsNullOrEmpty(checkstar) ? 0 : checkstar.Length, String.IsNullOrEmpty(checkstar) ? SqlString.Null : checkstar);
                        commandWrapper.AddInputParameter("@invited", SqlDbType.VarChar, String.IsNullOrEmpty(Invited) ? 0 : Invited.Length, String.IsNullOrEmpty(Invited) ? SqlString.Null : Invited);
                        commandWrapper.AddInputParameter("@Duplicate", SqlDbType.VarChar, String.IsNullOrEmpty(Duplicate) ? 0 : Duplicate.Length, String.IsNullOrEmpty(Duplicate) ? SqlString.Null : Duplicate);
                        commandWrapper.AddInputParameter("@resourceKey", SqlDbType.BigInt, (ResourceKey == 0) ? 0 : ResourceKey);
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            VendorModel item = null;
                            while (dataReader.Read())
                            {
                                item = new VendorModel();
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

        public List<VendorModel> SearchBidrequest(long PageSize, long PageIndex, string Sort, string CompanyKey)
        {
            List<VendorModel> itemList = new List<VendorModel>();
            try
            {
                string storedProcedure = "site_Vendor_SelectBidRequestFormanager";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters 
                        commandWrapper.AddInputParameter("@PageSize", SqlDbType.BigInt, (PageSize == 0) ? 0 : PageSize);
                        commandWrapper.AddInputParameter("@PageIndex", SqlDbType.BigInt, (PageIndex == 0) ? 0 : PageIndex);
                        commandWrapper.AddInputParameter("@Sort", SqlDbType.NText, (Sort == "") ? "" : Sort);
                        commandWrapper.AddInputParameter("@CompanyKey", SqlDbType.NText, (CompanyKey == "") ? "" : CompanyKey);
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            VendorModel item = null;
                            while (dataReader.Read())
                            {
                                item = new VendorModel();
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

        public List<VendorModel> SearchWorkOrder(long PageSize, long PageIndex, string Sort, string CompanyKey)
        {
            List<VendorModel> itemList = new List<VendorModel>();
            try
            {
                string storedProcedure = "site_Vendor_SelectworkorderFormanager";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters 
                        commandWrapper.AddInputParameter("@PageSize", SqlDbType.BigInt, (PageSize == 0) ? 0 : PageSize);
                        commandWrapper.AddInputParameter("@PageIndex", SqlDbType.BigInt, (PageIndex == 0) ? 0 : PageIndex);
                        commandWrapper.AddInputParameter("@Sort", SqlDbType.NText, (Sort == "") ? "" : Sort);
                        commandWrapper.AddInputParameter("@CompanyKey", SqlDbType.NText, (CompanyKey == "") ? "" : CompanyKey);
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            VendorModel item = null;
                            while (dataReader.Read())
                            {
                                item = new VendorModel();
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

        public List<VendorModel> SearchFeedbackvendor(long PageSize, long PageIndex, string Sort,string CompanyKey)
        {
            List<VendorModel> itemList = new List<VendorModel>();
            try
            {
                string storedProcedure = "site_Vendor_starvendorfeedbackpaging";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        commandWrapper.AddInputParameter("@PageSize", SqlDbType.BigInt, (PageSize == 0) ? 0 : PageSize);
                        commandWrapper.AddInputParameter("@PageIndex", SqlDbType.BigInt, (PageIndex == 0) ? 0 : PageIndex);
                        commandWrapper.AddInputParameter("@Sort", SqlDbType.NText, (Sort == "") ? "" : Sort);
                        commandWrapper.AddInputParameter("@CompanyKey", SqlDbType.NText, (CompanyKey == "") ? "" : CompanyKey);
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            VendorModel item = null;
                            while (dataReader.Read())
                            {
                                item = new VendorModel();
                                loadFeedback(dataReader, item);
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

        public List<VendorModel> Searchinsurance(int CompanyKey)
        {
            List<VendorModel> itemList = new List<VendorModel>();
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
                            VendorModel item = null;
                            while (dataReader.Read())
                            {
                                item = new VendorModel();
                                LoadInsurance(dataReader, item);
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

        public IList<VendorModel> GetAll(VendorFilterModel vendorFilter, PagingModel paging)
        {
            throw new NotImplementedException();
        }

        protected void GetAllState(DBDataReader dataReader, VendorModel item)
        {
            item.StateKey = dataReader.GetValueText("StateKey");
            item.State = dataReader.GetValueText("Title");
        }

        protected void GetAllService(DBDataReader dataReader, VendorModel item)
        {
            item.ServiceKey = dataReader.GetValueInt("ServiceKey");
            item.ServiceTitle1 = dataReader.GetValueText("Title");
        }

        protected void GetAllProperty(DBDataReader dataReader, VendorModel item)
        {
            item.PropertyKey = dataReader.GetValueInt("PropertyKey");
            item.Title = dataReader.GetValueText("Title");
        }

        public IList<VendorModel> GetAll(VendorFilterModel filter)
        {
            throw new NotImplementedException();
        }

        public virtual VendorModel GetDataViewEdit(int id)
        {
            VendorModel item = null;

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
                                item = new VendorModel();
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

        public IList<VendorModel> GetAllService()
        {
            List<VendorModel> itemList = new List<VendorModel>();

            try
            {
                string storedProcedure = "site_Service_GetAll";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            VendorModel item = null;
                            while (dataReader.Read())
                            {
                                item = new VendorModel();
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

        public IList<VendorModel> GetAllProperty(int ResourceKey)
        {
            List<VendorModel> itemList = new List<VendorModel>();

            try
            {
                string storedProcedure = "site_VendorProperty_GetAll";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        commandWrapper.AddInputParameter("@ResourceKey", SqlDbType.Int, ResourceKey);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            VendorModel item = null;
                            while (dataReader.Read())
                            {
                                item = new VendorModel();
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

        public IList<VendorModel> GetAllState()
        {
            List<VendorModel> itemList = new List<VendorModel>();

            try
            {
                string storedProcedure = "site_State_SelectAll";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            VendorModel item = null;
                            while (dataReader.Read())
                            {
                                item = new VendorModel();
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


        public bool CheckDuplicatedEmail(string Email)
        {
            bool Status = false;
            int value = 0;
            try
            {
                string storedProcedure = "site_Vendor_CheckDuplicatedEmail";

                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@Email", SqlDbType.VarChar, String.IsNullOrEmpty(Email) ? 0 : Email.Length, String.IsNullOrEmpty(Email) ? SqlString.Null : Email);
                        commandWrapper.AddOutputParameter("@Status", SqlDbType.Int);

                        db.ExecuteNonQuery(commandWrapper);
                        value = commandWrapper.GetValueInt("@Status");

                        if (value != 1)
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

      

        public void VendorInvetationmailsend(int status, string fromemail, string UserName,string CompanyName,int ResKey = 0)
        {
            try
            {
                Repository.Base.Code.PMVendorRepository staffDirectoryRepository = new Repository.Base.Code.PMVendorRepository();
                IList<Model.EmailTemplateModel> EmailTemplate = null;
                
                EmailTemplate = staffDirectoryRepository.GetAll();
                string LinkUrl = System.Configuration.ConfigurationManager.AppSettings["LinkUrl"];
                string body = EmailTemplate[0].Body;
                body = body.Replace("[MemberName]", UserName);
                body = body.Replace("[VendorRegistrationLink]", LinkUrl + "/Registration/Registration?CompanyKey=" + status);
                body = body.Replace("[CompanyName]", CompanyName);
                body = body.Replace("[ContactPerson]", UserName);
                body = body.Replace("[Email]", fromemail);


                string Subject = EmailTemplate[0].EmailSubject;
                Subject = Subject.Replace("[CompanyName]", CompanyName);


                MailMessage msg = new MailMessage();
                string strBody = string.Empty;

                msg.From = new MailAddress(System.Configuration.ConfigurationManager.AppSettings["senderemail"]);
                msg.To.Add(fromemail);
                msg.Subject = Subject;
                msg.IsBodyHtml = true;
                msg.Body += body;

                var byresource = Convert.ToInt32(HttpContext.Current.Session["resourcekey"]);
                EmailModel emailTemplate = new EmailModel();
                emailTemplate.Body = body;
                emailTemplate.DateAdded = DateTime.Now;
                emailTemplate.Subject = Subject;
                emailTemplate.DateSent = DateTime.Now;
                emailTemplate.EmailStatus = 500;
                emailTemplate.From = System.Configuration.ConfigurationManager.AppSettings["senderemail"];
                emailTemplate.To = fromemail;
                emailTemplate.ObjectKey = status;
                emailTemplate.ResourceKey = ResKey;
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

        public virtual IList<EmailTemplateModel> GetAll()
        {
            List<EmailTemplateModel> itemList = new List<EmailTemplateModel>();
            try
            {
                string storedProcedure = "site_EmailTemplate_SelectAll";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        commandWrapper.AddInputParameter("@LookUpTitle", SqlDbType.NText, "Vendor invitation");

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

        public virtual bool Remove(int CompanyKey)
        {
            bool status = false;

            try
            {
                string storedProcedure = "site_vendor_Delete";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@companyKey", SqlDbType.Int, CompanyKey);
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);
                        db.ExecuteNonQuery(commandWrapper);

                        status = (commandWrapper.GetValueInt("@errorCode") == 0);
                    }
                }
            }
            catch (Exception)
            {
                return status;
            }
            return status;
        }

        public bool RemoveService(int CompanyKey, string servicename)
        {
            bool status = false;

            try
            {
                string storedProcedure = "site_Servicevendor_Delete";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@companyKey", SqlDbType.Int, CompanyKey);
                        commandWrapper.AddInputParameter("@servicename", SqlDbType.Text, servicename);
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

        public bool RemoveDocument(int CompanyKey, int docId)
        {
            bool status = false;
            try
            {
                string storedProcedure = "site_Documentevendor_Delete";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@documentid", SqlDbType.Int, docId);
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

        public bool MarkstarOrNot(int CompanyKey, int ResourceKey)
        {
            bool status = false;

            try
            {
                string storedProcedure = "site_Vendor_MarkAsStar";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@CompanyKey", SqlDbType.Int, CompanyKey);
                        commandWrapper.AddInputParameter("@ResourceKey", SqlDbType.Int, ResourceKey);
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

        protected void GetbindDocumentInsurance(DBDataReader dataReader, VendorModel item)
        {
            try
            {
                item.Insurance.InsuranceKey = dataReader.GetValueInt("InsuranceKey");
                //item.Vendor.CompanyKey = dataReader.GetValueInt("VendorKey");
                item.Insurance.VendorKey = dataReader.GetValueInt("VendorKey");
                item.Insurance.PolicyNumber = dataReader.GetValueText("PolicyNumber");
                item.Insurance.InsuranceAmount = dataReader.GetValueDecimal("InsuranceAmount");
                item.Insurance.AgentName = dataReader.GetValueText("AgentName");
                item.Insurance.Email = dataReader.GetValueText("Email");
                //item.Vendor.Email = dataReader.GetValueText("Email");
                //item.Vendor.CompanyName = dataReader.GetValueText("CompanyName");
                //item.Document.DocumentKey = dataReader.GetValueInt("DocumentKey");
                //item.Document.FileName = dataReader.GetValueText("FileName");
                //item.Document.ModuleKey = dataReader.GetValueInt("ModuleKey");
                //item.Document.FileSize = dataReader.GetValueDouble("FileSize");
                item.Insurance.StartDate = dataReader.GetValueDateTime("StartDate");
                item.Insurance.EndDate = dataReader.GetValueDateTime("EndDate");
                item.Insurance.RenewalDate = dataReader.GetValueDateTime("RenewalDate");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IList<VendorModel> GetbindDocumentInsurance(int CompanyKey, int ModuleKey)
        {
            List<VendorModel> itemList = new List<VendorModel>();

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
                            VendorModel item = null;
                            while (dataReader.Read())
                            {
                                item = new VendorModel();
                                GetbindDocumentInsurance(dataReader, item);
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

        public IList<VendorModel> GetbindDocument12(int CompanyKey)
        {
            List<VendorModel> itemList = new List<VendorModel>();

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
                            VendorModel item = null;
                            while (dataReader.Read())
                            {
                                item = new VendorModel();
                                item.Document = new DocumentModel();
                                item.Insurance = new InsuranceModel();
                                //item.Resource = new ResourceModel();
                                //item.ServiceModel = new VendorServiceModel();
                                //item.Vendor = new VendorManagerVendorModel();
                                GetbindDocument12(dataReader, item);
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

        protected void GetbindDocument12(DBDataReader dataReader, VendorModel item)
        {
            try
            {
                item.Insurance.InsuranceKey = dataReader.GetValueInt("InsuranceKey");
                //item.Vendor.CompanyKey = dataReader.GetValueInt("VendorKey");
                item.Insurance.VendorKey = dataReader.GetValueInt("VendorKey");
                item.Insurance.PolicyNumber = dataReader.GetValueText("PolicyNumber");
                item.Insurance.InsuranceAmount = dataReader.GetValueDecimal("InsuranceAmount");
                item.Insurance.AgentName = dataReader.GetValueText("AgentName");
                item.Email = dataReader.GetValueText("Email");
                //item.Vendor.Email = dataReader.GetValueText("Email");
                //item.Vendor.CompanyName = dataReader.GetValueText("CompanyName");
                item.Document.DocumentKey = dataReader.GetValueInt("DocumentKey");
                item.Document.FileName = dataReader.GetValueText("FileName");
                item.Document.ModuleKey = dataReader.GetValueInt("ModuleKey");
                item.Document.FileSize = dataReader.GetValueDouble("FileSize");
                item.Insurance.StartDates = dataReader.GetValueText("StartDate");
                item.Insurance.EndDates = dataReader.GetValueText("EndDate");
                item.Insurance.RenewalDates = dataReader.GetValueText("RenewalDate");

            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public IList<VendorModel> GetbindDocumentByInsuranceKey(int CompanyKey, int InsuranceKey)
        {
            List<VendorModel> itemList = new List<VendorModel>();

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
                            VendorModel item = null;
                            while (dataReader.Read())
                            {
                                item = new VendorModel();
                                item.Document = new DocumentModel();
                                item.Insurance = new InsuranceModel();
                                //item.Resource = new ResourceModel();
                                //item.ServiceModel = new VendorServiceModel();
                                //item.Vendor = new VendorManagerVendorModel();
                                GetbindDocument12(dataReader, item);
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
    }

    //protected void GetbindDocument12(DBDataReader dataReader, VendorManagerModel item)
    //{
    //    try
    //    {
    //        item.Insurance.InsuranceKey = dataReader.GetValueInt("InsuranceKey");
    //        item.Vendor.CompanyKey = dataReader.GetValueInt("VendorKey");
    //        item.Insurance.VendorKey = dataReader.GetValueInt("VendorKey");
    //        item.Insurance.PolicyNumber = dataReader.GetValueText("PolicyNumber");
    //        item.Insurance.InsuranceAmount = dataReader.GetValueDecimal("InsuranceAmount");
    //        item.Insurance.AgentName = dataReader.GetValueText("AgentName");
    //        item.Insurance.Email = dataReader.GetValueText("Email");
    //        item.Vendor.Email = dataReader.GetValueText("Email");
    //        item.Vendor.CompanyName = dataReader.GetValueText("CompanyName");
    //        item.Document.DocumentKey = dataReader.GetValueInt("DocumentKey");
    //        item.Document.FileName = dataReader.GetValueText("FileName");
    //        item.Document.ModuleKey = dataReader.GetValueInt("ModuleKey");
    //        item.Document.FileSize = dataReader.GetValueDouble("FileSize");
    //        item.Insurance.StartDate = dataReader.GetValueDateTime("StartDate");
    //        item.Insurance.EndDate = dataReader.GetValueDateTime("EndDate");
    //        item.Insurance.RenewalDate = dataReader.GetValueDateTime("RenewalDate");

    //    }
    //    catch (Exception ex)
    //    {

    //        throw;
    //    }

    //}
}
