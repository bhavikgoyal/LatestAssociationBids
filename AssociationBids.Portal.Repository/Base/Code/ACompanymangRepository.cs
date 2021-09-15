using AssociationBids.Portal.Common;
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
    public class ACompanymangRepository : BaseRepository, IACompanymangRepository
    {
        public ACompanymangRepository() { }

        public ACompanymangRepository(string connectionString)
        : base(connectionString) { }

        protected void Load(DBDataReader dataReader, CompanyModel item)
        {

            item.CompanyKey = dataReader.GetValueInt("CompanyKey");
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
            item.Email = dataReader.GetValueText("Email");
            item.PrimaryContact = dataReader.GetValueBool("PrimaryContact");
            item.CellPhone = dataReader.GetValueText("CellPhone");
            item.BidRequestResponseDays = dataReader.GetValueInt("BidRequestResponseDays");
            item.BidSubmitDays = dataReader.GetValueInt("BidSubmitDays");
            item.BidRequestAmount = dataReader.GetValueDecimal("BidRequestAmount");
         
        }

        protected void LoadCompany(DBDataReader dataReader, CompanyModel item)
        {

            item.CompanyKey = dataReader.GetValueInt("CompanyKey");
            item.LegalName = dataReader.GetValueText("LegalName");
            item.Name = dataReader.GetValueText("Name");

            item.Work = dataReader.GetValueText("Work");
            item.Address = dataReader.GetValueText("Address");

            item.Status = dataReader.GetValueInt("Status");
            item.TotalRecords = dataReader.GetValueInt("TotalRecord");
        }

        protected void LoadViewEdit(DBDataReader dataReader, CompanyModel item)
        {


            item.CompanyKey = dataReader.GetValueInt("CompanyKey");
          
            item.Name = dataReader.GetValueText("Name");
            item.LegalName = dataReader.GetValueText("LegalName");
            item.FirstName = dataReader.GetValueText("FirstName");
            item.LastName = dataReader.GetValueText("LastName");


            item.Work = dataReader.GetValueText("Work");
            item.Work2 = dataReader.GetValueText("Work2");
            item.Work = dataReader.GetValueText("Work");
            item.Work2 = dataReader.GetValueText("Work2");
            item.Fax = dataReader.GetValueText("Fax");
            item.Address = dataReader.GetValueText("Address");
            item.Address2 = dataReader.GetValueText("Address2");
            item.City = dataReader.GetValueText("City");
            item.State = dataReader.GetValueText("State");
            item.StateName = dataReader.GetValueText("StateName");
            item.Zip = dataReader.GetValueText("Zip");
            item.Website = dataReader.GetValueText("Website");
            item.Description = dataReader.GetValueText("Description");
            item.Email = dataReader.GetValueText("Email");
            item.PrimaryContact = dataReader.GetValueBool("PrimaryContact");
            item.CellPhone = dataReader.GetValueText("CellPhone");
            item.BidRequestAmount = dataReader.GetValueDecimal("BidRequestAmount");
            item.BidRequestAmounts = dataReader.GetValueDecimal("BidRequestAmount").ToString("00.00");
            item.BidRequestAmountss = Convert.ToDouble(dataReader.GetValueDecimal("BidRequestAmount"));
            item.BidRequestResponseDays = dataReader.GetValueInt("BidRequestResponseDays");
            item.BidSubmitDays = dataReader.GetValueInt("BidSubmitDays");
            item.ResourceKey=dataReader.GetValueInt("ResourceKey");
        }

        public List<CompanyModel> SearchCompany(long PageSize, long PageIndex, string Search, string Sort, string State, int Status)
        {
            List<CompanyModel> itemList = new List<CompanyModel>();
            try
            {
                string storedProcedure = "site_AdminCompany_SelectIndexPaging";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters 
                        commandWrapper.AddInputParameter("@PageSize", SqlDbType.BigInt, (PageSize == 0) ? 0 : PageSize);
                        commandWrapper.AddInputParameter("@PageIndex", SqlDbType.BigInt, (PageIndex == 0) ? 0 : PageIndex);
                        commandWrapper.AddInputParameter("@Search", SqlDbType.NText, (Search == "") ? "" : Search);
                        commandWrapper.AddInputParameter("@Sort", SqlDbType.NText, (Sort == "") ? "" : Sort);
                        commandWrapper.AddInputParameter("@State", SqlDbType.NText, (State == "") ? "0" : State);
                        commandWrapper.AddInputParameter("@Status", SqlDbType.BigInt, (Status == 0) ? 0 : Status);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            CompanyModel item = null;
                            while (dataReader.Read())
                            {
                                item = new CompanyModel();
                                LoadCompany(dataReader, item);
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

        public IList<CompanyModel> GetAll(CompanyFilterModel companyFilter, PagingModel paging)
        {
            throw new NotImplementedException();
        }

        protected void GetAllStatee(DBDataReader dataReader, CompanyModel item)
        {
            item.StateKey = dataReader.GetValueText("StateKey");
            item.State = dataReader.GetValueText("Title");
        }

        public IList<CompanyModel> GetAll(CompanyFilterModel filter)
        {
            throw new NotImplementedException();
        }

        public IList<CompanyModel> GetAllStatee()
        {
            List<CompanyModel> itemList = new List<CompanyModel>();

            try
            {

                string storedProcedure = "site_State_SelectAll";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            CompanyModel item = null;
                            while (dataReader.Read())
                            {
                                item = new CompanyModel();
                                GetAllStatee(dataReader, item);
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

        public Int64 Insert(CompanyModel item)
        {
            Int64 status = 1;
            item.Status = 101;
                        

            try
            {
                item.Password = System.Configuration.ConfigurationManager.AppSettings["Password"];

                string storedProcedure = "site_AdminCompany_Insert";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters

                        commandWrapper.AddInputParameter("@LegalName", SqlDbType.VarChar, String.IsNullOrEmpty(item.LegalName) ? 0 : item.LegalName.Length, String.IsNullOrEmpty(item.LegalName) ? SqlString.Null : item.LegalName);
                        commandWrapper.AddInputParameter("@CompanyName", SqlDbType.VarChar, String.IsNullOrEmpty(item.Name) ? 0 : item.Name.Length, String.IsNullOrEmpty(item.Name) ? SqlString.Null : item.Name);
                        commandWrapper.AddInputParameter("@Address", SqlDbType.VarChar, String.IsNullOrEmpty(item.Address) ? 0 : item.Address.Length, String.IsNullOrEmpty(item.Address) ? SqlString.Null : item.Address);
                        commandWrapper.AddInputParameter("@Address2", SqlDbType.VarChar, String.IsNullOrEmpty(item.Address2) ? 0 : item.Address2.Length, String.IsNullOrEmpty(item.Address2) ? SqlString.Null : item.Address2);
                        commandWrapper.AddInputParameter("@City", SqlDbType.VarChar, String.IsNullOrEmpty(item.City) ? 0 : item.City.Length, String.IsNullOrEmpty(item.City) ? SqlString.Null : item.City);
                        commandWrapper.AddInputParameter("@StateKey", SqlDbType.VarChar, String.IsNullOrEmpty(item.State) ? 0 : item.State.Length, String.IsNullOrEmpty(item.State) ? SqlString.Null : item.State);
                        commandWrapper.AddInputParameter("@Zip", SqlDbType.VarChar, String.IsNullOrEmpty(item.Zip) ? 0 : item.Zip.Length, String.IsNullOrEmpty(item.Zip) ? SqlString.Null : item.Zip);
                        commandWrapper.AddInputParameter("@Work", SqlDbType.VarChar, String.IsNullOrEmpty(item.Work) ? 0 : item.Work.Length, String.IsNullOrEmpty(item.Work) ? SqlString.Null : item.Work);
                        commandWrapper.AddInputParameter("@Work2", SqlDbType.VarChar, String.IsNullOrEmpty(item.Work2) ? 0 : item.Work2.Length, String.IsNullOrEmpty(item.Work2) ? SqlString.Null : item.Work2);
                        commandWrapper.AddInputParameter("@Workss", SqlDbType.VarChar, String.IsNullOrEmpty(item.Work) ? 0 : item.Work.Length, String.IsNullOrEmpty(item.Work) ? SqlString.Null : item.Work);
                        commandWrapper.AddInputParameter("@Workss2", SqlDbType.VarChar, String.IsNullOrEmpty(item.Work2) ? 0 : item.Work2.Length, String.IsNullOrEmpty(item.Work2) ? SqlString.Null : item.Work2);
                        commandWrapper.AddInputParameter("@Fax", SqlDbType.VarChar, String.IsNullOrEmpty(item.Fax) ? 0 : item.Fax.Length, String.IsNullOrEmpty(item.Fax) ? SqlString.Null : item.Fax);
                        commandWrapper.AddInputParameter("@Website", SqlDbType.VarChar, String.IsNullOrEmpty(item.Website) ? 0 : item.Website.Length, String.IsNullOrEmpty(item.Website) ? SqlString.Null : item.Website);
                        commandWrapper.AddInputParameter("@PrimaryContact", SqlDbType.Bit, (item.PrimaryContact == false) ? SqlBoolean.Null : item.PrimaryContact);
                        commandWrapper.AddInputParameter("@firstname", SqlDbType.VarChar, String.IsNullOrEmpty(item.FirstName) ? 0 : item.FirstName.Length, String.IsNullOrEmpty(item.FirstName) ? SqlString.Null : item.FirstName);
                        commandWrapper.AddInputParameter("@lastname", SqlDbType.VarChar, String.IsNullOrEmpty(item.LastName) ? 0 : item.LastName.Length, String.IsNullOrEmpty(item.LastName) ? SqlString.Null : item.LastName);
                        commandWrapper.AddInputParameter("@Email", SqlDbType.VarChar, String.IsNullOrEmpty(item.Email) ? 0 : item.Email.Length, String.IsNullOrEmpty(item.Email) ? SqlString.Null : item.Email);
                        commandWrapper.AddInputParameter("@CellPhone", SqlDbType.VarChar, String.IsNullOrEmpty(item.CellPhone) ? 0 : item.CellPhone.Length, String.IsNullOrEmpty(item.CellPhone) ? SqlString.Null : item.CellPhone);
                        commandWrapper.AddInputParameter("@Description", SqlDbType.VarChar, String.IsNullOrEmpty(item.Description) ? 0 : item.Description.Length, String.IsNullOrEmpty(item.Description) ? SqlString.Null : item.Description);
                        commandWrapper.AddInputParameter("@BidRequestResponseDays", SqlDbType.Int, (item.BidRequestResponseDays == 0) ? SqlInt32.Null : item.BidRequestResponseDays);
                        commandWrapper.AddInputParameter("@BidSubmitDays", SqlDbType.Int, (item.BidSubmitDays == 0) ? SqlInt32.Null : item.BidSubmitDays);
                        commandWrapper.AddInputParameter("@BidRequestAmount", SqlDbType.Money, (item.BidRequestAmount == 0) ? SqlMoney.Null : item.BidRequestAmount);
                        commandWrapper.AddInputParameter("@Password", SqlDbType.VarChar, item.Password);

                        commandWrapper.AddInputParameter("@Status", SqlDbType.Int, (item.Status == 0) ? SqlInt32.Null : item.Status);

                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);
                        db.ExecuteNonQuery(commandWrapper);
                        status = commandWrapper.GetValueInt("@errorCode");




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
        
        public UserModel InsertedCompanyUser(CompanyModel item)
        {
            Int64 status = 101;

          
         
            UserModel uItem = new UserModel();

            if (item.Password == null)
            {
                item.Password = System.Configuration.ConfigurationManager.AppSettings["Password"];
            }
            try
            {

                string storedProcedure = "site_AdminCompany_InsertedUser";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters

                        commandWrapper.AddInputParameter("@CompanyKey", SqlDbType.Int, (item.CompanyKey == 0 ? 0 : item.CompanyKey.ToString().Length), item.CompanyKey == 0 ? SqlInt32.Null : item.CompanyKey);
                        commandWrapper.AddInputParameter("@LegalName", SqlDbType.VarChar, String.IsNullOrEmpty(item.LegalName) ? 0 : item.LegalName.Length, String.IsNullOrEmpty(item.LegalName) ? SqlString.Null : item.LegalName);
                        commandWrapper.AddInputParameter("@CompanyName", SqlDbType.VarChar, String.IsNullOrEmpty(item.Name) ? 0 : item.Name.Length, String.IsNullOrEmpty(item.Name) ? SqlString.Null : item.Name);
                        commandWrapper.AddInputParameter("@Address", SqlDbType.VarChar, String.IsNullOrEmpty(item.Address) ? 0 : item.Address.Length, String.IsNullOrEmpty(item.Address) ? SqlString.Null : item.Address);
                        commandWrapper.AddInputParameter("@Address2", SqlDbType.VarChar, String.IsNullOrEmpty(item.Address2) ? 0 : item.Address2.Length, String.IsNullOrEmpty(item.Address2) ? SqlString.Null : item.Address2);
                        commandWrapper.AddInputParameter("@City", SqlDbType.VarChar, String.IsNullOrEmpty(item.City) ? 0 : item.City.Length, String.IsNullOrEmpty(item.City) ? SqlString.Null : item.City);
                        commandWrapper.AddInputParameter("@StateKey", SqlDbType.VarChar, String.IsNullOrEmpty(item.State) ? 0 : item.State.Length, String.IsNullOrEmpty(item.State) ? SqlString.Null : item.State);
                        commandWrapper.AddInputParameter("@Zip", SqlDbType.VarChar, String.IsNullOrEmpty(item.Zip) ? 0 : item.Zip.Length, String.IsNullOrEmpty(item.Zip) ? SqlString.Null : item.Zip);
                        commandWrapper.AddInputParameter("@Work", SqlDbType.VarChar, String.IsNullOrEmpty(item.Work) ? 0 : item.Work.Length, String.IsNullOrEmpty(item.Work) ? SqlString.Null : item.Work);
                        commandWrapper.AddInputParameter("@Work2", SqlDbType.VarChar, String.IsNullOrEmpty(item.Work2) ? 0 : item.Work2.Length, String.IsNullOrEmpty(item.Work2) ? SqlString.Null : item.Work2);
                        commandWrapper.AddInputParameter("@Workss", SqlDbType.VarChar, String.IsNullOrEmpty(item.Work) ? 0 : item.Work.Length, String.IsNullOrEmpty(item.Work) ? SqlString.Null : item.Work);
                        commandWrapper.AddInputParameter("@Workss2", SqlDbType.VarChar, String.IsNullOrEmpty(item.Work2) ? 0 : item.Work2.Length, String.IsNullOrEmpty(item.Work2) ? SqlString.Null : item.Work2);
                        commandWrapper.AddInputParameter("@Fax", SqlDbType.VarChar, String.IsNullOrEmpty(item.Fax) ? 0 : item.Fax.Length, String.IsNullOrEmpty(item.Fax) ? SqlString.Null : item.Fax);
                        commandWrapper.AddInputParameter("@Website", SqlDbType.VarChar, String.IsNullOrEmpty(item.Website) ? 0 : item.Website.Length, String.IsNullOrEmpty(item.Website) ? SqlString.Null : item.Website);
                        commandWrapper.AddInputParameter("@PrimaryContact", SqlDbType.Bit, (item.PrimaryContact == false) ? SqlBoolean.Null : item.PrimaryContact);
                        commandWrapper.AddInputParameter("@firstname", SqlDbType.VarChar, String.IsNullOrEmpty(item.FirstName) ? 0 : item.FirstName.Length, String.IsNullOrEmpty(item.FirstName) ? SqlString.Null : item.FirstName);
                        commandWrapper.AddInputParameter("@lastname", SqlDbType.VarChar, String.IsNullOrEmpty(item.LastName) ? 0 : item.LastName.Length, String.IsNullOrEmpty(item.LastName) ? SqlString.Null : item.LastName);
                        commandWrapper.AddInputParameter("@Email", SqlDbType.VarChar, String.IsNullOrEmpty(item.Email) ? 0 : item.Email.Length, String.IsNullOrEmpty(item.Email) ? SqlString.Null : item.Email);
                        commandWrapper.AddInputParameter("@CellPhone", SqlDbType.VarChar, String.IsNullOrEmpty(item.CellPhone) ? 0 : item.CellPhone.Length, String.IsNullOrEmpty(item.CellPhone) ? SqlString.Null : item.CellPhone);
                        commandWrapper.AddInputParameter("@Description", SqlDbType.VarChar, String.IsNullOrEmpty(item.Description) ? 0 : item.Description.Length, String.IsNullOrEmpty(item.Description) ? SqlString.Null : item.Description);
                        commandWrapper.AddInputParameter("@BidRequestResponseDays", SqlDbType.Int, (item.BidRequestResponseDays == 0) ? SqlInt32.Null : item.BidRequestResponseDays);
                        commandWrapper.AddInputParameter("@BidSubmitDays", SqlDbType.Int, (item.BidSubmitDays == 0) ? SqlInt32.Null : item.BidSubmitDays);
                        commandWrapper.AddInputParameter("@BidRequestAmount", SqlDbType.Money, (item.BidRequestAmount == 0) ? SqlMoney.Null : item.BidRequestAmount);
                        commandWrapper.AddInputParameter("@Password", SqlDbType.VarChar, item.Password);

                        commandWrapper.AddInputParameter("@Status", SqlDbType.Int, 101);

                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            while (dataReader.Read())
                            {
                                InsertedCompanyUser(dataReader, uItem);
                            }
                        }
                        status = commandWrapper.GetValueInt("@errorCode");

                    }
                }
            }
            catch (Exception ex)
            {
                // error occured...
                throw;
            }

            return uItem;
        }

        protected void InsertedCompanyUser(DBDataReader dataReader, UserModel uItem)
        {
            uItem.ResourceKey = dataReader.GetValueInt("ResourceKey");
            uItem.Username = dataReader.GetValueText("Username");
            uItem.Password = dataReader.GetValueText("Password");
            uItem.Status= dataReader.GetValueInt("Status");
            uItem.UserKey = dataReader.GetValueInt("UserKey");
        }

        public bool CheckDuplicatedEmaill(string Email)
        {
            bool Status = false;
            int value = 0;
            try
            {
                string storedProcedure = "site_AdminCompany_CheckDuplicatedEmail";

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

        public virtual CompanyModel GetDataViewEditt(int id)
        {
            CompanyModel item = new CompanyModel();

            try
            {
                string storedProcedure = "site_ACompanymanagement_SelectOneBycompanyKey";
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
                                item = new CompanyModel();

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

        public Int64 CompanyEdit(CompanyModel item)
        {
            Int64 status = 0;

            try
            {

                item.Status = 101;
                if (item.State == "0")
                {
                    item.State = "";
                }
                string storedProcedure = "site_Admincompany_Update";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@companyKey", SqlDbType.Int, (item.CompanyKey == 0) ? SqlInt32.Null : item.CompanyKey);
                        //commandWrapper.AddInputParameter("@companyName ", SqlDbType.VarChar, String.IsNullOrEmpty(item.Name) ? 0 : item.Name.Length, String.IsNullOrEmpty(item.Name) ? SqlString.Null : item.Name);
                        commandWrapper.AddInputParameter("@legalName ", SqlDbType.VarChar, String.IsNullOrEmpty(item.LegalName) ? 0 : item.LegalName.Length, String.IsNullOrEmpty(item.LegalName) ? SqlString.Null : item.LegalName);
                        commandWrapper.AddInputParameter("@CompanyName ", SqlDbType.VarChar, String.IsNullOrEmpty(item.Name) ? 0 : item.Name.Length, String.IsNullOrEmpty(item.Name) ? SqlString.Null : item.Name);

                        commandWrapper.AddInputParameter("@work ", SqlDbType.VarChar, String.IsNullOrEmpty(item.Work) ? 0 : item.Work.Length, String.IsNullOrEmpty(item.Work) ? SqlString.Null : item.Work);
                        commandWrapper.AddInputParameter("@work2 ", SqlDbType.VarChar, String.IsNullOrEmpty(item.Work2) ? 0 : item.Work2.Length, String.IsNullOrEmpty(item.Work2) ? SqlString.Null : item.Work2);

                        commandWrapper.AddInputParameter("@fax ", SqlDbType.VarChar, String.IsNullOrEmpty(item.Fax) ? 0 : item.Fax.Length, String.IsNullOrEmpty(item.Fax) ? SqlString.Null : item.Fax);
                        commandWrapper.AddInputParameter("@address", SqlDbType.VarChar, String.IsNullOrEmpty(item.Address) ? 0 : item.Address.Length, String.IsNullOrEmpty(item.Address) ? SqlString.Null : item.Address);
                        commandWrapper.AddInputParameter("@address2", SqlDbType.VarChar, String.IsNullOrEmpty(item.Address2) ? 0 : item.Address2.Length, String.IsNullOrEmpty(item.Address2) ? SqlString.Null : item.Address2);
                        commandWrapper.AddInputParameter("@city", SqlDbType.VarChar, String.IsNullOrEmpty(item.City) ? 0 : item.City.Length, String.IsNullOrEmpty(item.City) ? SqlString.Null : item.City);
                        commandWrapper.AddInputParameter("@StateKey", SqlDbType.VarChar, String.IsNullOrEmpty(item.State) ? 0 : item.State.Length, String.IsNullOrEmpty(item.State) ? SqlString.Null : item.State);
                        commandWrapper.AddInputParameter("@zip", SqlDbType.VarChar, String.IsNullOrEmpty(item.Zip) ? 0 : item.Zip.Length, String.IsNullOrEmpty(item.Zip) ? SqlString.Null : item.Zip);
                        commandWrapper.AddInputParameter("@website", SqlDbType.VarChar, String.IsNullOrEmpty(item.Website) ? 0 : item.Website.Length, String.IsNullOrEmpty(item.Website) ? SqlString.Null : item.Website);

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
                throw;
            }
        }

        public Int64 CompanydefaultEdit(CompanyModel item)
        {
            Int64 status = 0;

            try
            {

                item.Status = 101;
                string storedProcedure = "site_Admincompanydefault_Update";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@companyKey", SqlDbType.Int, (item.CompanyKey == 0) ? SqlInt32.Null : item.CompanyKey);
                        //commandWrapper.AddInputParameter("@companyName ", SqlDbType.VarChar, String.IsNullOrEmpty(item.Name) ? 0 : item.Name.Length, String.IsNullOrEmpty(item.Name) ? SqlString.Null : item.Name);


                        commandWrapper.AddInputParameter("@description", SqlDbType.VarChar, String.IsNullOrEmpty(item.Description) ? 0 : item.Description.Length, String.IsNullOrEmpty(item.Description) ? SqlString.Null : item.Description);
                        commandWrapper.AddInputParameter("@BidRequestResponseDays", SqlDbType.Int, (item.BidRequestResponseDays == 0) ? SqlInt32.Null : item.BidRequestResponseDays);
                        commandWrapper.AddInputParameter("@BidSubmitDays", SqlDbType.Int, (item.BidSubmitDays == 0) ? SqlInt32.Null : item.BidSubmitDays);
                        commandWrapper.AddInputParameter("@BidRequestAmount", SqlDbType.Money, (item.BidRequestAmount == 0) ? SqlMoney.Null : item.BidRequestAmount);

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
                throw;
            }
        }

        public Int64 APrimarycontactEdit(CompanyModel item)
        {
            Int64 status = 0;

            try
            {

                item.Status = 101;
                string storedProcedure = "site_Adminprimarycompany_Update";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@companyKey", SqlDbType.Int, (item.CompanyKey == 0) ? SqlInt32.Null : item.CompanyKey);

                        commandWrapper.AddInputParameter("@firstname ", SqlDbType.VarChar, String.IsNullOrEmpty(item.FirstName) ? 0 : item.FirstName.Length, String.IsNullOrEmpty(item.FirstName) ? SqlString.Null : item.FirstName);
                        commandWrapper.AddInputParameter("@work ", SqlDbType.VarChar, String.IsNullOrEmpty(item.Work) ? 0 : item.Work.Length, String.IsNullOrEmpty(item.Work) ? SqlString.Null : item.Work);
                        commandWrapper.AddInputParameter("@work2 ", SqlDbType.VarChar, String.IsNullOrEmpty(item.Work2) ? 0 : item.Work2.Length, String.IsNullOrEmpty(item.Work2) ? SqlString.Null : item.Work2);

                        commandWrapper.AddInputParameter("@PrimaryContact", SqlDbType.Bit, (item.PrimaryContact == false) ? SqlBoolean.Null : item.PrimaryContact);
                        commandWrapper.AddInputParameter("@Email", SqlDbType.VarChar, String.IsNullOrEmpty(item.Email) ? 0 : item.Email.Length, String.IsNullOrEmpty(item.Email) ? SqlString.Null : item.Email);
                        commandWrapper.AddInputParameter("@CellPhone", SqlDbType.VarChar, String.IsNullOrEmpty(item.CellPhone) ? 0 : item.CellPhone.Length, String.IsNullOrEmpty(item.CellPhone) ? SqlString.Null : item.CellPhone);
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
                throw;
            }
        }

        protected void Getbindservice(DBDataReader dataReader, CompanyModel item)
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

        public IList<CompanyModel> Getbindservice(int CompanyKey)
        {
            List<CompanyModel> itemList = new List<CompanyModel>();

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
                            CompanyModel item = null;
                            while (dataReader.Read())
                            {
                                item = new CompanyModel();
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
        protected void GetAllService(DBDataReader dataReader, CompanyModel item)
        {
            item.ServiceKey = dataReader.GetValueInt("ServiceKey");
            item.ServiceTitle1 = dataReader.GetValueText("Title");

        }

        public IList<CompanyModel> GetAllService()
        {
            List<CompanyModel> itemList = new List<CompanyModel>();

            try
            {

                string storedProcedure = "site_Service_GetAll";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            CompanyModel item = null;
                            while (dataReader.Read())
                            {
                                item = new CompanyModel();
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

        public Int64 Removee(int CompanyKey,int ResourceKey)
        {
            int  status = 0;

            try
            {
                string storedProcedure = "site_Admincompany_Delete";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@CompanyKey", SqlDbType.Int, CompanyKey);
                        commandWrapper.AddInputParameter("@ResourceKey", SqlDbType.Int, ResourceKey);
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);
                        db.ExecuteNonQuery(commandWrapper);

                        status = commandWrapper.GetValueInt("@errorCode");
                    }
                }
            }
            catch (Exception ex)
            {
                
            }

            return status;
        }

        //property module
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

        public List<PropertyModel> SearchUser(long PageSize, long PageIndex, string Search, string Sort)
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

        public List<PropertyModel> SearchCompanyViewProperty(long PageSize, long PageIndex, string Search, string Sort,Int32 CompanyKey)
        {
            List<PropertyModel> itemList = new List<PropertyModel>();
            try
            {
                string storedProcedure = "site_Property_CompanyViewPaging";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters 
                        commandWrapper.AddInputParameter("@PageSize", SqlDbType.BigInt, (PageSize == 0) ? 0 : PageSize);
                        commandWrapper.AddInputParameter("@PageIndex", SqlDbType.BigInt, (PageIndex == 0) ? 0 : PageIndex);
                        commandWrapper.AddInputParameter("@Search", SqlDbType.NText, (Search == "") ? "" : Search);
                        commandWrapper.AddInputParameter("@Sort", SqlDbType.NText, (Sort == "") ? "" : Sort);
                        commandWrapper.AddInputParameter("@CompanyKey", SqlDbType.Int, (CompanyKey == 0) ? 0 : CompanyKey);
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
            item.Groupkey = dataReader.GetValueInt("ResourceKey");
            item.GroupName = dataReader.GetValueText("Name");


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
                // error occured...
                throw;
            }

            return itemList;
        }

        public int GetGroupKey(string managername)
        {
            int status = 0;

            try
            {

                string storedProcedure = "site_GetGroupkey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@ResourceKey", SqlDbType.VarChar, managername);
                        commandWrapper.AddOutputParameter("@Groupkey", SqlDbType.Int);
                        db.ExecuteNonQuery(commandWrapper);
                        status = commandWrapper.GetValueInt("@Groupkey");




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
                        commandWrapper.AddOutputParameter("@Propertyvalue", SqlDbType.Int);
                        db.ExecuteNonQuery(commandWrapper);
                        status = commandWrapper.GetValueInt("@Propertyvalue");




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
                // error occured...
                throw;
            }

            return status;
        }

        public virtual bool Updatemanager(int PropertyKey, string managername)
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
                        commandWrapper.AddInputParameter("@Resourcekey", SqlDbType.VarChar, managername);

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
        
        public IList<PropertyModel> GetAllManager(int ResourceKey)
        {
            List<PropertyModel> itemList = new List<PropertyModel>();

            try
            {

                string storedProcedure = "site_Manager_SelectAll";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        commandWrapper.AddInputParameter("@ResourceKey", SqlDbType.Int, ResourceKey);
                        commandWrapper.AddInputParameter("@PropertyKey", SqlDbType.Int, 1);
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
                        commandWrapper.AddInputParameter("@Latitude", SqlDbType.VarChar, String.IsNullOrEmpty(item.Latitude) ? 0 : item.Latitude.Length, String.IsNullOrEmpty(item.Latitude) ? SqlString.Null : item.Latitude);
                        commandWrapper.AddInputParameter("@Longitude", SqlDbType.VarChar, String.IsNullOrEmpty(item.Longitude) ? 0 : item.Longitude.Length, String.IsNullOrEmpty(item.Longitude) ? SqlString.Null : item.Longitude);
                        commandWrapper.AddInputParameter("@Status", SqlDbType.Int, (item.Status == 0) ? SqlInt32.Null : item.Status);
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

        public virtual bool RemoveStaffDirecroty(Int32 ResourceKey)
        {
            bool status = false;

            try
            {
                string storedProcedure = "site_StaffDirectory_Delete";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@ResourceKey", SqlDbType.Int, ResourceKey);

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
                        commandWrapper.AddInputParameter("@Docname", SqlDbType.VarChar, Docname);

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

        public virtual bool ManagerDelete(int PropertyKey, int ResourceKey)
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

        public int checkmanager(int PropertyKey)
        {
            int status = 0;

            try
            {
                string storedProcedure = "site_Property_checkManager";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@PropertyKey", SqlDbType.Int, PropertyKey);


                        commandWrapper.AddOutputParameter("@resourcekey", SqlDbType.Int);
                        db.ExecuteNonQuery(commandWrapper);
                        status = commandWrapper.GetValueInt("@resourcekey");
                    }
                }
            }
            catch (Exception ex)
            {
                // error occured...

            }

            return status;
        }

        //USER MODULE
        public List<ResourceModel> Searchcompany(int CompanyKey)
        {
            List<ResourceModel> itemList = new List<ResourceModel>();
            try
            {
                string storedProcedure = "site_Adminucompanyuser_SelectIndexPaging";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters 
                        commandWrapper.AddInputParameter("@CompanyKey", SqlDbType.Int, CompanyKey);
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            ResourceModel item = null;
                            while (dataReader.Read())
                            {
                                item = new ResourceModel();
                                LoadAuser(dataReader, item);
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

        public IList<ResourceModel> GetAll(ResourceFilterModel staffDirectoryModel, PagingModel paging)
        {
            throw new NotImplementedException();
        }

        protected void LoadAuser(DBDataReader dataReader, ResourceModel item)
        {

            item.CompanyKey = dataReader.GetValueInt("CompanyKey");
            item.FirstName = dataReader.GetValueText("FirstName");
            item.ResourceKey = dataReader.GetValueInt("ResourceKey");
      
            item.Email = dataReader.GetValueText("Email");
            item.PrimaryContact = dataReader.GetValueBool("PrimaryContact");
            item.TotalRecords = dataReader.GetValueInt("TotalRecords");
            
           
        }

        public Int64 Insert(StaffDirectoryModel item)
        {
            Int64 status = 0;

            try
            {
                if (item.FileName == null)
                {
                    item.FileName = "";

                }


                if (item.lst == "0")
                {
                    item.lst = "1100";
                }


                string passwordval = "";
                string password = "";
                //passwordval = Security.RandomPassword();
                password = Security.Encrypt(item.Password);
                //Guid code = Guid.NewGuid();
                //item.Status = "1";
                string storedProcedure = "site_Resource_StaffDirectionInsert";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@CompanyKey", SqlDbType.Int, (item.CompanyKey == 0) ? SqlInt32.Null : item.CompanyKey);
                        commandWrapper.AddInputParameter("@FirstName", SqlDbType.VarChar, String.IsNullOrEmpty(item.FirstName) ? 0 : item.FirstName.Length, String.IsNullOrEmpty(item.FirstName) ? SqlString.Null : item.FirstName);
                        commandWrapper.AddInputParameter("@LastName", SqlDbType.VarChar, String.IsNullOrEmpty(item.LastName) ? 0 : item.LastName.Length, String.IsNullOrEmpty(item.LastName) ? SqlString.Null : item.LastName);
                        //commandWrapper.AddInputParameter("@Title", SqlDbType.VarChar, String.IsNullOrEmpty(item.Title) ? 0 : item.Title.Length, String.IsNullOrEmpty(item.Title) ? SqlString.Null : item.Title);
                        commandWrapper.AddInputParameter("@Email", SqlDbType.VarChar, String.IsNullOrEmpty(item.Email) ? 0 : item.Email.Length, String.IsNullOrEmpty(item.Email) ? SqlString.Null : item.Email);
                        commandWrapper.AddInputParameter("@Email2", SqlDbType.VarChar, String.IsNullOrEmpty(item.Email2) ? 0 : item.Email2.Length, String.IsNullOrEmpty(item.Email2) ? SqlString.Null : item.Email2);
                        commandWrapper.AddInputParameter("@CellPhone", SqlDbType.VarChar, String.IsNullOrEmpty(item.CellPhone) ? 0 : item.CellPhone.Length, String.IsNullOrEmpty(item.CellPhone) ? SqlString.Null : item.CellPhone);
                        //commandWrapper.AddInputParameter("@HomePhone", SqlDbType.VarChar, String.IsNullOrEmpty(item.HomePhone) ? 0 : item.HomePhone.Length, String.IsNullOrEmpty(item.HomePhone) ? SqlString.Null : item.HomePhone);
                        //commandWrapper.AddInputParameter("@HomePhone2", SqlDbType.VarChar, String.IsNullOrEmpty(item.HomePhone2) ? 0 : item.HomePhone2.Length, String.IsNullOrEmpty(item.HomePhone2) ? SqlString.Null : item.Phone2);
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
                        commandWrapper.AddInputParameter("@Username", SqlDbType.VarChar, String.IsNullOrEmpty(item.UserName) ? 0 : item.UserName.Length, String.IsNullOrEmpty(item.UserName) ? SqlString.Null : item.UserName);
                        commandWrapper.AddInputParameter("@Password", SqlDbType.VarChar, password);
                        commandWrapper.AddInputParameter("@Status", SqlDbType.Int, 101);
                        //commandWrapper.AddInputParameter("@AccountLocked", SqlDbType.Bit, (item.AccountLocked == false) ? SqlBoolean.Null : item.AccountLocked);
                        commandWrapper.AddInputParameter("@FileName", SqlDbType.VarChar, String.IsNullOrEmpty(item.FileName) ? 0 : item.FileName.Length, String.IsNullOrEmpty(item.FileName) ? SqlString.Null : item.FileName);
                        commandWrapper.AddInputParameter("@FileSize", SqlDbType.Int, item.FileSize);
                        //commandWrapper.AddInputParameter("@lst", SqlDbType.VarChar, item.lst);

                        string groupid = "";
                        if (!string.IsNullOrEmpty(item.GroupId))
                        {
                            foreach (string str in item.GroupId.Split(','))
                            {
                                groupid = groupid + "," + str.Trim();
                            }
                            groupid = groupid.Trim().Trim(',').Trim();
                        }
                        commandWrapper.AddInputParameter("@GroupId", SqlDbType.VarChar, groupid);
                        commandWrapper.AddOutputParameter("@ResourceValue", SqlDbType.Int);

                        db.ExecuteNonQuery(commandWrapper);
                        status = commandWrapper.GetValueInt("@ResourceValue");

                        if (status != 0)
                        {
                            mailsendAsync(item.UserKey, item.Email, item.FirstName + " " + item.LastName, item.ResetExpirationDate, item.CompanyName,"Reset Password");
                        }
                        return status;
                    }
                }
            }
            catch (Exception ex)
            {
                // error occured...
                throw;
            }

            //return status;
        }

        public Int64 ACompanyStaffInsert(StaffDirectoryModel item)
        {
            Int64 status = 0;

            try
            {
                if (item.FileName == null)
                {
                    item.FileName = "";
                }

                if (item.lst == "0")
                {
                    item.lst = "1100";
                }
                item.Status = 101;
                string passwordval = "";
                string password = "";
                //passwordval = Security.RandomPassword();
                password = Security.Encrypt(item.Password);
                //Guid code = Guid.NewGuid();
                //item.Status = "1";
                string storedProcedure = "site_Resource_CompanyManagementStaffInsert";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@CompanyKey", SqlDbType.Int, (item.CompanyKey == 0) ? SqlInt32.Null : item.CompanyKey);
                        commandWrapper.AddInputParameter("@FirstName", SqlDbType.VarChar, String.IsNullOrEmpty(item.FirstName) ? 0 : item.FirstName.Length, String.IsNullOrEmpty(item.FirstName) ? SqlString.Null : item.FirstName);
                        commandWrapper.AddInputParameter("@LastName", SqlDbType.VarChar, String.IsNullOrEmpty(item.LastName) ? 0 : item.LastName.Length, String.IsNullOrEmpty(item.LastName) ? SqlString.Null : item.LastName);
                        //commandWrapper.AddInputParameter("@Title", SqlDbType.VarChar, String.IsNullOrEmpty(item.Title) ? 0 : item.Title.Length, String.IsNullOrEmpty(item.Title) ? SqlString.Null : item.Title);
                        commandWrapper.AddInputParameter("@Email", SqlDbType.VarChar, String.IsNullOrEmpty(item.Email) ? 0 : item.Email.Length, String.IsNullOrEmpty(item.Email) ? SqlString.Null : item.Email);
                        commandWrapper.AddInputParameter("@Email2", SqlDbType.VarChar, String.IsNullOrEmpty(item.Email2) ? 0 : item.Email2.Length, String.IsNullOrEmpty(item.Email2) ? SqlString.Null : item.Email2);
                        commandWrapper.AddInputParameter("@CellPhone", SqlDbType.VarChar, String.IsNullOrEmpty(item.CellPhone) ? 0 : item.CellPhone.Length, String.IsNullOrEmpty(item.CellPhone) ? SqlString.Null : item.CellPhone);
                        //commandWrapper.AddInputParameter("@HomePhone", SqlDbType.VarChar, String.IsNullOrEmpty(item.HomePhone) ? 0 : item.HomePhone.Length, String.IsNullOrEmpty(item.HomePhone) ? SqlString.Null : item.HomePhone);
                        //commandWrapper.AddInputParameter("@HomePhone2", SqlDbType.VarChar, String.IsNullOrEmpty(item.HomePhone2) ? 0 : item.HomePhone2.Length, String.IsNullOrEmpty(item.HomePhone2) ? SqlString.Null : item.Phone2);
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
                        commandWrapper.AddInputParameter("@Username", SqlDbType.VarChar, String.IsNullOrEmpty(item.UserName) ? 0 : item.UserName.Length, String.IsNullOrEmpty(item.UserName) ? SqlString.Null : item.UserName);
                        commandWrapper.AddInputParameter("@Password", SqlDbType.VarChar, password);
                        commandWrapper.AddInputParameter("@Status", SqlDbType.Int, (Convert.ToInt32(item.Status) == 0) ? SqlInt32.Null : Convert.ToInt32(item.Status));
                        //commandWrapper.AddInputParameter("@AccountLocked", SqlDbType.Bit, (item.AccountLocked == false) ? SqlBoolean.Null : item.AccountLocked);
                        commandWrapper.AddInputParameter("@FileName", SqlDbType.VarChar, String.IsNullOrEmpty(item.FileName) ? 0 : item.FileName.Length, String.IsNullOrEmpty(item.FileName) ? SqlString.Null : item.FileName);
                        commandWrapper.AddInputParameter("@FileSize", SqlDbType.Int, item.FileSize);
                        //commandWrapper.AddInputParameter("@lst", SqlDbType.VarChar, item.lst);

                        string groupid = "";
                        if (!string.IsNullOrEmpty(item.GroupId))
                        {
                            foreach (string str in item.GroupId.Split(','))
                            {
                                groupid = groupid + "," + str.Trim();
                            }
                            groupid = groupid.Trim().Trim(',').Trim();
                        }
                        commandWrapper.AddInputParameter("@GroupId", SqlDbType.VarChar, groupid);
                        commandWrapper.AddOutputParameter("@ResourceValue", SqlDbType.Int);

                        db.ExecuteNonQuery(commandWrapper);
                        status = commandWrapper.GetValueInt("@ResourceValue");

                        if (status != 0)
                        {
                            mailsendAsync(item.UserKey, item.Email, item.FirstName + " " + item.LastName, item.ResetExpirationDate, item.CompanyName,"Reset Password");
                        }
                        return status;
                    }
                }
            }
            catch (Exception ex)
            {
                // error occured...
                throw;
            }

            //return status;
        }

        public void mailsendAsync(int UserKey, string fromemail, string UserName, string ResetExpirationDate, string CompanyName,string Status)
        {
            try
            {
                IList<EmailTemplateModel> EmailTemplate = null;
                string LinkUrl = System.Configuration.ConfigurationManager.AppSettings["LinkUrl"];
                EmailTemplate = GetEmailAll(Status);

                string body = EmailTemplate[0].Body;
                body = body.Replace("[MemberName]", UserName);
                body = body.Replace("[ResetPasswordlink]", LinkUrl + "/ChangePassword/ResetPassword?UserKey=" + UserKey);
                body = body.Replace("[SendDate]", "");
                body = body.Replace("[LinkExpiryDate]", ResetExpirationDate);
                body = body.Replace("[MemberEmail]", fromemail);
                body = body.Replace("[MemberCompanyName]", CompanyName);

                string Subject = EmailTemplate[0].EmailSubject;
                Subject = Subject.Replace("[MemberName]", Convert.ToString(UserName.ToString().Trim()));


                MailMessage msg = new MailMessage();
                string strBody = string.Empty;

                msg.From = new MailAddress(System.Configuration.ConfigurationManager.AppSettings["senderemail"]);
                msg.To.Add(fromemail);
                msg.Subject = Subject;
                msg.IsBodyHtml = true;                
                msg.Body += body;


                EmailModel emailModel = new EmailModel();

                int resourcecKey = Convert.ToInt32(HttpContext.Current.Session["resourceid"]);
                emailModel.Body = body;
                emailModel.DateAdded = DateTime.Now;
                emailModel.DateSent = DateTime.Now;
                emailModel.From = System.Configuration.ConfigurationManager.AppSettings["senderemail"];
                emailModel.Subject = msg.Subject;
                emailModel.To = fromemail;
                emailModel.ModuleKey = 303;//Profile
                emailModel.ObjectKey = UserKey;
                emailModel.ResourceKey = resourcecKey;
                emailModel.EmailStatus = 500;
                EmailRepository emaillog = new EmailRepository();
                bool isinserted = emaillog.Create(emailModel);
                using (SmtpClient client = new SmtpClient())
                {
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["smtpUser"], System.Configuration.ConfigurationManager.AppSettings["smtpPass"]);
                    client.Host = System.Configuration.ConfigurationManager.AppSettings["smtpServer"];
                    client.Port = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["smtpPort"]);
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.Send(msg);
                    emailModel.EmailStatus = 502;
                    emaillog.Update(emailModel);
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void mailsendAsyncForEmailChange(int UserKey, string fromemail, string newEmail, string oldEmail,string UserName, string ResetExpirationDate, string CompanyName, string Status)
        {
            try
            {
                IList<EmailTemplateModel> EmailTemplate = null;
                string LinkUrl = System.Configuration.ConfigurationManager.AppSettings["LinkUrl"];
                EmailTemplate = GetEmailAll(Status);

                string body = EmailTemplate[0].Body;
                body = body.Replace("[MemberName]", UserName);
                body = body.Replace("[ResetPasswordlink]", LinkUrl + "/ChangePassword/ResetPassword?UserKey=" + UserKey);
                body = body.Replace("[SendDate]", ResetExpirationDate);
                body = body.Replace("[LinkExpiryDate]", ResetExpirationDate);
                body = body.Replace("[OldEmail]", oldEmail);
                body = body.Replace("[NewEmail]", newEmail);
                body = body.Replace("[MemberCompanyName]", CompanyName);

                string Subject = EmailTemplate[0].EmailSubject;
                Subject = Subject.Replace("[MemberName]", Convert.ToString(UserName.ToString().Trim()));


                MailMessage msg = new MailMessage();
                string strBody = string.Empty;

                msg.From = new MailAddress(System.Configuration.ConfigurationManager.AppSettings["senderemail"]);
                msg.To.Add(fromemail);
                msg.Subject = Subject;
                msg.IsBodyHtml = true;
                msg.Body += body;

                EmailModel emailModel = new EmailModel();

                int resourcecKey = Convert.ToInt32(HttpContext.Current.Session["resourceid"]);
                emailModel.Body = body;
                emailModel.DateAdded = DateTime.Now;
                emailModel.DateSent = DateTime.Now;
                emailModel.From = System.Configuration.ConfigurationManager.AppSettings["senderemail"];
                emailModel.Subject = msg.Subject;
                emailModel.To = fromemail;
                emailModel.ModuleKey = 303;//Profile
                emailModel.ObjectKey = UserKey;
                emailModel.ResourceKey = resourcecKey;
                emailModel.EmailStatus = 500;
                EmailRepository emaillog = new EmailRepository();
                bool isinserted = emaillog.Create(emailModel);

                using (SmtpClient client = new SmtpClient())
                {
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["smtpUser"], System.Configuration.ConfigurationManager.AppSettings["smtpPass"]);
                    client.Host = System.Configuration.ConfigurationManager.AppSettings["smtpServer"];
                    client.Port = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["smtpPort"]);
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.Send(msg);
                    emailModel.EmailStatus = 502;
                    emaillog.Update(emailModel);
                }
            }
            catch (Exception ex)
            {
            }
        }

        public virtual IList<EmailTemplateModel> GetAllData(string lookUpTitle)
        {
            List<EmailTemplateModel> itemList = new List<EmailTemplateModel>();
            try
            {
                string storedProcedure = "site_EmailTemplate_SelectAll";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        commandWrapper.AddInputParameter("@LookUpTitle", SqlDbType.NText, lookUpTitle);

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
        public bool WinFeeMain(string fromemail, string lookUpTitle, int UserKey, string UserName, DateTime ResetExpirationDate, string CompanyName)
        {
            bool status = false;
            try
            {
                string date = DateTime.UtcNow.ToString("MM-dd-yyyy");
               
                string Status = "Reset Password";

                //Repository.Base.Code.PMVendorRepository staffDirectoryRepository = new Repository.Base.Code.PMVendorRepository();
                IList<Model.EmailTemplateModel> EmailTemplate = null;
                EmailTemplate = GetEmailAll(Status);
                string LinkUrl = System.Configuration.ConfigurationManager.AppSettings["LinkUrl"];
                string body = EmailTemplate[0].Body;
                body = body.Replace("[MemberName]", UserName);
                body = body.Replace("[ResetPasswordlink]", LinkUrl + "/ChangePassword/ResetPassword?UserKey=" + UserKey);
                body = body.Replace("[SendDate]", date);
                body = body.Replace("[LinkExpiryDate]", date);
                body = body.Replace("[MemberEmail]", fromemail);
                body = body.Replace("[MemberCompanyName]", CompanyName);


                MailMessage msg = new MailMessage();
                string strBody = string.Empty;

                msg.From = new MailAddress(System.Configuration.ConfigurationManager.AppSettings["senderemail"]);
                msg.To.Add(fromemail);
                msg.Subject = EmailTemplate[0].EmailSubject;
                msg.IsBodyHtml = true;
               
                msg.Body += body;

                EmailModel emailModel = new EmailModel();

                int resourcecKey = Convert.ToInt32(HttpContext.Current.Session["resourceid"]);
                emailModel.Body = body;
                emailModel.DateAdded = DateTime.Now;
                emailModel.DateSent = DateTime.Now;
                emailModel.From = System.Configuration.ConfigurationManager.AppSettings["senderemail"];
                emailModel.Subject = msg.Subject;
                emailModel.To = fromemail;
                emailModel.ModuleKey = 303;//Profile
                emailModel.ObjectKey = UserKey;
                emailModel.ResourceKey = resourcecKey;
                emailModel.EmailStatus = 500;
                EmailRepository emaillog = new EmailRepository();
                bool isinserted = emaillog.Create(emailModel);
                using (SmtpClient client = new SmtpClient())
                {
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["smtpUser"], System.Configuration.ConfigurationManager.AppSettings["smtpPass"]);
                    client.Host = System.Configuration.ConfigurationManager.AppSettings["smtpServer"];
                    client.Port = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["smtpPort"]);
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.Send(msg);
                    emailModel.EmailStatus = 502;
                    emaillog.Update(emailModel);
                }
            }

            catch (Exception ex)
            {

                string Remarks = ex.Message;
               

            }

            return status;
        }



        public IList<StaffDirectoryModel> GetAllStateee()
        {
            List<StaffDirectoryModel> itemList = new List<StaffDirectoryModel>();

            try
            {
                string storedProcedure = "site_State_SelectAll";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            StaffDirectoryModel item = null;
                            while (dataReader.Read())
                            {
                                item = new StaffDirectoryModel();
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
        protected void GetAllState(DBDataReader dataReader, StaffDirectoryModel item)
        {
            item.StateKey = dataReader.GetValueText("StateKey");
            item.State = dataReader.GetValueText("Title");
        }

        public List<StaffDirectoryModel> GetAllGroup()
        {
            List<StaffDirectoryModel> itemList = new List<StaffDirectoryModel>();

            try
            {
                string storedProcedure = "site_Group_SelectAll";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            StaffDirectoryModel item = null;
                            while (dataReader.Read())
                            {
                                item = new StaffDirectoryModel();
                                GetAllGroup(dataReader, item);
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

        public List<StaffDirectoryModel> GetDataViewEdittt(int id)
        {
            List<StaffDirectoryModel> itemList = new List<StaffDirectoryModel>();
            StaffDirectoryModel item = null;
            try
            {
                string storedProcedure = "site_Resource_StaffSelectOneByResourceKey";
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
                                item = new StaffDirectoryModel();
                                LoadViewEdit(dataReader, item);
                                itemList.Add(item);
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
            return itemList;
        }

        protected void GetAllGroup(DBDataReader dataReader, StaffDirectoryModel item)
        {
            item.GroupKey = Convert.ToInt32(dataReader.GetValueText("GroupKey"));
            item.GroupName = dataReader.GetValueText("Title");
        }
        protected void GetAllGroupkey(DBDataReader dataReader, StaffDirectoryModel item)
        {
            item.GroupKey = Convert.ToInt32(dataReader.GetValueText("GroupKey"));

        }

        public List<StaffDirectoryModel> GetDataviewGroupCheckbox(int id)
        {
            List<StaffDirectoryModel> itemList = new List<StaffDirectoryModel>();
            StaffDirectoryModel item = null;
            try
            {
                string storedProcedure = "site_Resource_GetDataviewGroupCheckbox";
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
                                item = new StaffDirectoryModel();
                                GetAllGroupkey(dataReader, item);
                                itemList.Add(item);
                            }
                        }
                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: site_Resource_GetDataviewGroupCheckbox");
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

        protected void LoadViewEdit(DBDataReader dataReader, StaffDirectoryModel item)
        {
            int Status = 0;
            string Password = "";
            item.ResourceKey = dataReader.GetValueInt("ResourceKey");
            item.ResourceTypeKey = dataReader.GetValueInt("ResourceTypeKey");
            item.UserKey = dataReader.GetValueInt("UserKey");
            item.FirstName = dataReader.GetValueText("FirstName");
            item.LastName = dataReader.GetValueText("LastName");
            item.Email = dataReader.GetValueText("Email");
            item.Email2 = dataReader.GetValueText("Email2");
            item.CellPhone = dataReader.GetValueText("CellPhone");
            item.Work = dataReader.GetValueText("Work");
            item.Work2 = dataReader.GetValueText("Work2");
            item.Fax = dataReader.GetValueText("Fax");
            item.Address = dataReader.GetValueText("Address");
            item.Address2 = dataReader.GetValueText("Address2");
            item.City = dataReader.GetValueText("City");
            item.State = dataReader.GetValueText("State");
            item.Zip = dataReader.GetValueText("Zip");
            item.PrimaryContact = dataReader.GetValueBool("PrimaryContact");
            item.Status = dataReader.GetValueInt("Status");

            item.Description = dataReader.GetValueText("Description");
            //item.DateAdded = dataReader.GetValueDateTime("DateAdded");
            //item.LastModificationTime = dataReader.GetValueDateTime("LastModificationTime");
            item.UserName = dataReader.GetValueText("Username");
            Password = dataReader.GetValueText("Password");
            item.Password = (string.IsNullOrEmpty(Password)?"" : Security.Decrypt(Password));
            item.GroupId = dataReader.GetValueText("GroupKey");
            item.FileName = dataReader.GetValueText("FileName");
        }

        public Int64 StaffDirectoryEditStaff(StaffDirectoryModel item)
        {
            Int64 status = 0;

            try
            {

                if (item.lst == "0")
                {
                    item.lst = "1100";
                }

                item.Status = 101;
                string storedProcedure = "site_Resource_StaffEdit";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@ResourceKey", SqlDbType.Int, (item.ResourceKey == 0) ? SqlInt32.Null : item.ResourceKey);
                        commandWrapper.AddInputParameter("@FirstName", SqlDbType.VarChar, String.IsNullOrEmpty(item.FirstName) ? 0 : item.FirstName.Length, String.IsNullOrEmpty(item.FirstName) ? SqlString.Null : item.FirstName);
                        commandWrapper.AddInputParameter("@LastName", SqlDbType.VarChar, String.IsNullOrEmpty(item.LastName) ? 0 : item.LastName.Length, String.IsNullOrEmpty(item.LastName) ? SqlString.Null : item.LastName);
                        commandWrapper.AddInputParameter("@Email", SqlDbType.VarChar, String.IsNullOrEmpty(item.Email) ? 0 : item.Email.Length, String.IsNullOrEmpty(item.Email) ? SqlString.Null : item.Email);
                        commandWrapper.AddInputParameter("@Email2", SqlDbType.VarChar, String.IsNullOrEmpty(item.Email2) ? 0 : item.Email2.Length, String.IsNullOrEmpty(item.Email2) ? SqlString.Null : item.Email2);
                        commandWrapper.AddInputParameter("@CellPhone", SqlDbType.VarChar, String.IsNullOrEmpty(item.CellPhone) ? 0 : item.CellPhone.Length, String.IsNullOrEmpty(item.CellPhone) ? SqlString.Null : item.CellPhone);
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
                        commandWrapper.AddInputParameter("@Status", SqlDbType.Int, (Convert.ToInt32(item.Status) == 0) ? SqlInt32.Null : Convert.ToInt32(item.Status));
                        //      commandWrapper.AddInputParameter("@lst", SqlDbType.VarChar, item.lst);

                        db.ExecuteNonQuery(commandWrapper);
                        //status = commandWrapper.GetValueInt("@ResourceValue");
                        if (item.OldEmail != item.Email)
                        {
                            mailsendAsyncForEmailChange(item.UserKey, item.Email,item.Email, item.OldEmail, item.FirstName + " " + item.LastName, item.ResetExpirationDate, item.CompanyName, "Email Change");
                            mailsendAsyncForEmailChange(item.UserKey, item.OldEmail,item.Email, item.OldEmail, item.FirstName + " " + item.LastName, item.ResetExpirationDate, item.CompanyName, "Email Change");
                        }
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

        public Int64 StaffDirectoryEditGroup(StaffDirectoryModel item)
        {
            Int64 status = 0;

            try
            {
                string storedProcedure = "site_Resource_StaffGroupEdit";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@ResourceKey", SqlDbType.Int, (item.ResourceKey == 0) ? SqlInt32.Null : item.ResourceKey);
                        string groupid = "";
                        if (!string.IsNullOrEmpty(item.GroupId))
                        {
                            foreach (string str in item.GroupId.Split(','))
                            {
                                groupid = groupid + "," + str.Trim();
                            }
                            groupid = groupid.Trim().Trim(',').Trim();
                        }
                        commandWrapper.AddInputParameter("@GroupId", SqlDbType.VarChar, groupid);

                        db.ExecuteNonQuery(commandWrapper);
                        //status = commandWrapper.GetValueInt("@ResourceValue");

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

        public Int64 StaffDirectoryEditUser(StaffDirectoryModel item)
        {
            Int64 status = 0;

            try
            {
                string storedProcedure = "site_Resource_StaffGroupUser";
                using (Database db = new Database(ConnectionString))
                {
                    if (item.FileName == null)
                    {
                        item.FileName = "";
                    }
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@ResourceKey", SqlDbType.Int, (item.ResourceKey == 0) ? SqlInt32.Null : item.ResourceKey);
                        commandWrapper.AddInputParameter("@FileName", SqlDbType.VarChar, item.FileName);
                        commandWrapper.AddInputParameter("@FileSize", SqlDbType.Int, item.FileSize);
                        commandWrapper.AddInputParameter("@UserName", SqlDbType.VarChar, item.UserName);
                        db.ExecuteNonQuery(commandWrapper);

                        //status = commandWrapper.GetValueInt("@ResourceValue");

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

        public StaffDirectoryModel GetStaffDirectoryByResource(Int32 ResourceKey)
        {
            try
            {
                string storedProcedure = "site_Resource_StaffGroupUser";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@ResourceKey", SqlDbType.Int, (ResourceKey == 0) ? SqlInt32.Null : ResourceKey);
                        db.ExecuteNonQuery(commandWrapper);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            StaffDirectoryModel item = new StaffDirectoryModel();
                            while (dataReader.Read())
                            {
                                item = new StaffDirectoryModel();
                                GetStaffDirectoryByResource(dataReader, item);
                            }
                            return item;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // error occured...
                throw;
            }
        }

        protected void GetStaffDirectoryByResource(DBDataReader dataReader, StaffDirectoryModel item)
        {
            int Status = 0;
            string Password = "";
            item.ResourceKey = dataReader.GetValueInt("ResourceKey");
            item.ResourceTypeKey = dataReader.GetValueInt("ResourceTypeKey");
            item.UserKey = dataReader.GetValueInt("UserKey");
            item.FirstName = dataReader.GetValueText("FirstName");
            item.LastName = dataReader.GetValueText("LastName");
            item.Email = dataReader.GetValueText("Email");
            item.Email2 = dataReader.GetValueText("Email2");
            item.CellPhone = dataReader.GetValueText("CellPhone");
            item.Work = dataReader.GetValueText("Work");
            item.Work2 = dataReader.GetValueText("Work2");
            item.Fax = dataReader.GetValueText("Fax");
            item.Address = dataReader.GetValueText("Address");
            item.Address2 = dataReader.GetValueText("Address2");
            item.City = dataReader.GetValueText("City");
            item.State = dataReader.GetValueText("State");
            item.Zip = dataReader.GetValueText("Zip");
            item.PrimaryContact = dataReader.GetValueBool("PrimaryContact");
            item.Status = dataReader.GetValueInt("Status");

            item.Description = dataReader.GetValueText("Description");
            item.UserName = dataReader.GetValueText("Username");
            Password = dataReader.GetValueText("Password");
            item.Password = (string.IsNullOrEmpty(Password) ? "" : Security.Decrypt(Password));
            item.GroupId = dataReader.GetValueText("GroupTitle");
            item.FileName = dataReader.GetValueText("FileName");
        }

        public bool ResetPassword(int UserKey)
        {
            bool status = false;
            int Data = 0;
            List<StaffDirectoryModel> itemList = new List<StaffDirectoryModel>();
            StaffDirectoryModel item = null;
            try
            {
                string storedProcedure = "site_User_StaffResetPassword";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@UserKey", SqlDbType.Int, UserKey);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            Boolean IsDataExist = false;
                            while (dataReader.Read())
                            {
                                IsDataExist = true;
                                item = new StaffDirectoryModel();
                                ResetPassword(dataReader, item);
                                mailsendAsync(UserKey, item.Email, item.FirstName + " " + item.LastName, item.ResetExpirationDate, item.CompanyName,"Reset Password");
                            }
                            if (!IsDataExist)
                            {
                                throw new Exception("User does not exist.");
                            }
                        }
                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_Resource_SelectOneByResourceKey");
                        }
                        return status;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return status;
        }

        protected void ResetPassword(DBDataReader dataReader, StaffDirectoryModel item)
        {
            item.ResetExpirationDate = dataReader.GetValueText("ResetExpirationDate");
            item.Email = dataReader.GetValueText("Email");
            item.CompanyName = dataReader.GetValueText("Name");
            item.FirstName = dataReader.GetValueText("FirstName");
            item.LastName = dataReader.GetValueText("LastName");
        }

        public bool CheckDuplicatedEmail(string Email)
        {
            //List<StaffDirectoryModel> itemList = new List<StaffDirectoryModel>();
            bool Status = false;
            int value = 0;
            try
            {
                string storedProcedure = "site_Resource_CheckDuplicatedEmail";

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

        public virtual bool Removeee(int ResourceKey)
        {
            bool status = false;

            try
            {
                string storedProcedure = "site_Resource_StaffDelete";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@resourceKey", SqlDbType.Int, ResourceKey);
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

        public virtual IList<EmailTemplateModel> GetEmailAll(string Status)
        {
            List<EmailTemplateModel> itemList = new List<EmailTemplateModel>();
            try
            {
                string storedProcedure = "site_EmailTemplate_SelectAll";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        if (Status == "Reset Password")
                        {
                            commandWrapper.AddInputParameter("@LookUpTitle", SqlDbType.NText, "Reset Password");
                        }
                        else if (Status == "Email Change")
                        {
                            commandWrapper.AddInputParameter("@LookUpTitle", SqlDbType.NText, "Email Change");
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
    }

}

    