using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base;
using System.Data;
using System.Data.SqlTypes;
using System.Net;
using System.Net.Mail;

namespace AssociationBids.Portal.Repository.Base
{
    public class AStaffDirectoryRepository : BaseRepository, IAStaffDirectoryRepository
    {

        public AStaffDirectoryRepository() { }

        public AStaffDirectoryRepository(string connectionString)
            : base(connectionString) { }

        public List<AStaffDirectoryModel> SearchStaff(long PageSize, long PageIndex, string Search, string Sort, string CompanyKey)
        {
            List<AStaffDirectoryModel> itemList = new List<AStaffDirectoryModel>();
            try
            {
                
                string storedProcedure = "site_Resource_SelectIndexPaging";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters 
                        commandWrapper.AddInputParameter("@PageSize", SqlDbType.BigInt, (PageSize == 0) ? 0 : PageSize);
                        commandWrapper.AddInputParameter("@PageIndex", SqlDbType.BigInt, (PageIndex == 0) ? 0 : PageIndex);
                        commandWrapper.AddInputParameter("@Search", SqlDbType.NText, (Search == "") ? "" : Search);
                        commandWrapper.AddInputParameter("@Sort", SqlDbType.NText, (Sort == "") ? "" : Sort);
                        commandWrapper.AddInputParameter("@CompanyKey", SqlDbType.NText, (CompanyKey == "") ? "" : CompanyKey);
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            AStaffDirectoryModel item = null;
                            while (dataReader.Read())
                            {
                                item = new AStaffDirectoryModel();
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

        public List<AStaffDirectoryModel> AdvancedSearchStaff(long PageSize, long PageIndex, string Search, string Status,string Sort, string CompanyKey)
        {
            List<AStaffDirectoryModel> itemList = new List<AStaffDirectoryModel>();
            try
            {

                string storedProcedure = "site_Resource_SelectIndexPaging_New";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters 
                        commandWrapper.AddInputParameter("@PageSize", SqlDbType.BigInt, (PageSize == 0) ? 0 : PageSize);
                        commandWrapper.AddInputParameter("@PageIndex", SqlDbType.BigInt, (PageIndex == 0) ? 0 : PageIndex);
                        commandWrapper.AddInputParameter("@Search", SqlDbType.NText, (Search == "") ? "" : Search);
                        commandWrapper.AddInputParameter("@Status", SqlDbType.NText, (Status == "") ? "" : Status);
                        commandWrapper.AddInputParameter("@Sort", SqlDbType.NText, (Sort == "") ? "" : Sort);
                        commandWrapper.AddInputParameter("@CompanyKey", SqlDbType.NText, (CompanyKey == "") ? "" : CompanyKey);
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            AStaffDirectoryModel item = null;
                            while (dataReader.Read())
                            {
                                item = new AStaffDirectoryModel();
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

        protected void LoadStaff(DBDataReader dataReader, AStaffDirectoryModel item)
        {

            item.ResourceKey = dataReader.GetValueInt("ResourceKey");
            item.FirstName = dataReader.GetValueText("FirstName");
            item.LastName = dataReader.GetValueText("LastName");
            item.Status = dataReader.GetValueInt("Status");
            item.CellPhone = dataReader.GetValueText("Phone");
            item.Email = dataReader.GetValueText("Email");
            //item.NumberOfUnits = dataReader.GetValueInt("NumberOfUnits");
            item.TotalRecords = dataReader.GetValueInt("TotalRecord");
            //item.City = dataReader.GetValueText("City");
            //item.State = dataReader.GetValueText("State");
            //item.Company = dataReader.GetValueText("Company");
        }

        public Int64 Insert(AStaffDirectoryModel item, Int32 st)
        {
            Int64 status = 0;
            try
            {   if(item.FileName == null)
                {
                    item.FileName = "";
                }

                if (item.CompanyKey == 0 )
                {
                    item.CompanyKey = 15;
                }
                string passwordval = "";
                string password = "";
                //passwordval = Security.RandomPassword();
                password = Security.Encrypt(item.Password);
                //Guid code = Guid.NewGuid();
                //item.Status = "1";
                string storedProcedure = "site_Resource_AStaffDirectoryInsert";
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
                        if (item.State == "0" || item.State == "" || item.State == null)
                        {
                            commandWrapper.AddInputParameter("@State", SqlDbType.VarChar, DBNull.Value);
                        }
                        else
                        {
                            commandWrapper.AddInputParameter("@State", SqlDbType.VarChar, item.State);
                        }
                        commandWrapper.AddInputParameter("@Zip", SqlDbType.VarChar, String.IsNullOrEmpty(item.Zip) ? 0 : item.Zip.Length, String.IsNullOrEmpty(item.Zip) ? SqlString.Null : item.Zip);
                        commandWrapper.AddInputParameter("@PrimaryContact", SqlDbType.Bit, (item.PrimaryContact == false) ? SqlBoolean.Null : item.PrimaryContact);
                        commandWrapper.AddInputParameter("@Description", SqlDbType.VarChar, String.IsNullOrEmpty(item.Description) ? 0 : item.Description.Length, String.IsNullOrEmpty(item.Description) ? SqlString.Null : item.Description);
                        commandWrapper.AddInputParameter("@Username", SqlDbType.VarChar, String.IsNullOrEmpty(item.UserName) ? 0 : item.UserName.Length, String.IsNullOrEmpty(item.UserName) ? SqlString.Null : item.UserName);
                        commandWrapper.AddInputParameter("@Password", SqlDbType.VarChar, password);
                        commandWrapper.AddInputParameter("@Status", SqlDbType.Int, (Convert.ToInt32(item.Status) == 0) ? SqlInt32.Null : Convert.ToInt32(item.Status));
                        //commandWrapper.AddInputParameter("@AccountLocked", SqlDbType.Bit, (item.AccountLocked == false) ? SqlBoolean.Null : item.AccountLocked);
                        commandWrapper.AddInputParameter("@FileName", SqlDbType.VarChar, item.FileName);
                        commandWrapper.AddInputParameter("@FileSize", SqlDbType.Int, item.FileSize);


                        commandWrapper.AddInputParameter("@lst", SqlDbType.VarChar, "1100");



                        //commandWrapper.AddInputParameter("@GroupId", SqlDbType.VarChar, item.GroupId);
                        commandWrapper.AddOutputParameter("@ResourceValue", SqlDbType.Int);
                        commandWrapper.AddOutputParameter("@UserKey", SqlDbType.Int);

                        db.ExecuteNonQuery(commandWrapper);
                        status = commandWrapper.GetValueInt("@ResourceValue");
                        item.UserKey = commandWrapper.GetValueInt("@UserKey");

                        if (status != 0)
                        {
                            mailsendAsync(item.UserKey, item.Email, item.FirstName + " " + item.LastName, item.ResetExpirationDate, item.CompanyName, "Reset Password");
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
                EmailTemplate = GetEmailAll(Status);
                string LinkUrl = System.Configuration.ConfigurationManager.AppSettings["LinkUrl"];
                string body = EmailTemplate[0].Body;
                body = body.Replace("[MemberName]", UserName);
                body = body.Replace("[ResetPasswordlink]", LinkUrl+ "/ChangePassword/ResetPassword?UserKey=" + UserKey);
                body = body.Replace("[SendDate]", ResetExpirationDate);
                body = body.Replace("[LinkExpiryDate]", ResetExpirationDate);
                body = body.Replace("[MemberEmail]", fromemail);
                body = body.Replace("[MemberCompanyName]", CompanyName);
                body = body.Replace("[CompanyName]", CompanyName);

                string Subject = EmailTemplate[0].EmailSubject;
                Subject = Subject.Replace("[MemberName]", Convert.ToString(UserName.ToString().Trim()));
               
                MailMessage msg = new MailMessage();
                string strBody = string.Empty;

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

        public IList<AStaffDirectoryModel> GetAllState()
        {
            List<AStaffDirectoryModel> itemList = new List<AStaffDirectoryModel>();

            try
            {
                string storedProcedure = "site_State_SelectAll";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            AStaffDirectoryModel item = null;
                            while (dataReader.Read())
                            {
                                item = new AStaffDirectoryModel();
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
        protected void GetAllState(DBDataReader dataReader, AStaffDirectoryModel item)
        {
            item.StateKey = dataReader.GetValueText("StateKey");
            item.State = dataReader.GetValueText("Title");
        }

        public List<AStaffDirectoryModel> GetAllGroup()
        {
            List<AStaffDirectoryModel> itemList = new List<AStaffDirectoryModel>();

            try
            {
                string storedProcedure = "site_Group_SelectAll";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            AStaffDirectoryModel item = null;
                            while (dataReader.Read())
                            {
                                item = new AStaffDirectoryModel();
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
        protected void GetAllGroup(DBDataReader dataReader, AStaffDirectoryModel item)
        {
            item.GroupKey = Convert.ToInt32(dataReader.GetValueText("GroupKey"));
            item.GroupName = dataReader.GetValueText("Title");
        }

        public List<AStaffDirectoryModel> GetDataViewEdit(int id)
        {
            List<AStaffDirectoryModel> itemList = new List<AStaffDirectoryModel>();
            AStaffDirectoryModel item = null;
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
                                item = new AStaffDirectoryModel();
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
            catch (Exception ex)
            {
                // error occured...
                throw;
            }
            return itemList;
        }

        protected void LoadViewEdit(DBDataReader dataReader, AStaffDirectoryModel item)
        {
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
            item.Status = dataReader.GetValueInt("Status");
            item.State = dataReader.GetValueText("State");
            item.Zip = dataReader.GetValueText("Zip");
            item.PrimaryContact = dataReader.GetValueBool("PrimaryContact");
            item.Description = dataReader.GetValueText("Description");
            //item.DateAdded = dataReader.GetValueDateTime("DateAdded");
            //item.LastModificationTime = dataReader.GetValueDateTime("LastModificationTime");
            item.UserName = dataReader.GetValueText("Username");
            Password = dataReader.GetValueText("Password");
            try
            {
                item.Password = Security.Decrypt(Password);

            }
            catch (Exception)
            {
            }
            item.GroupId = dataReader.GetValueText("GroupKey");
        }

        public Int64 StaffDirectoryEditStaff(AStaffDirectoryModel item)
        {
            Int64 status = 0;

            try
            {
                //item.Status = "1";
                string storedProcedure = "site_Resource_AStaffEdit";
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
                        if (item.State == "0" || item.State == "" || item.State == null)
                        {
                            commandWrapper.AddInputParameter("@State", SqlDbType.VarChar, DBNull.Value);
                        }
                        else
                        {
                            commandWrapper.AddInputParameter("@State", SqlDbType.VarChar, item.State);
                        }
                        commandWrapper.AddInputParameter("@Zip", SqlDbType.VarChar, String.IsNullOrEmpty(item.Zip) ? 0 : item.Zip.Length, String.IsNullOrEmpty(item.Zip) ? SqlString.Null : item.Zip);
                        commandWrapper.AddInputParameter("@PrimaryContact", SqlDbType.Bit, (item.PrimaryContact == false) ? SqlBoolean.Null : item.PrimaryContact);
                        commandWrapper.AddInputParameter("@Description", SqlDbType.VarChar, String.IsNullOrEmpty(item.Description) ? 0 : item.Description.Length, String.IsNullOrEmpty(item.Description) ? SqlString.Null : item.Description);
                        commandWrapper.AddInputParameter("@Status", SqlDbType.Int, (Convert.ToInt32(item.Status) == 0) ? SqlInt32.Null : Convert.ToInt32(item.Status));
                        commandWrapper.AddInputParameter("@lst", SqlDbType.VarChar, "1100");
                        db.ExecuteNonQuery(commandWrapper);
                        //status = commandWrapper.GetValueInt("@ResourceValue");
                        
                        if (item.OldEmail != item.Email)
                        {
                            mailsendAsync(item.UserKey, item.Email, item.FirstName + " " + item.LastName, item.ResetExpirationDate, item.CompanyName, "Email Change");
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

        public Int64 StaffDirectoryEditGroup(AStaffDirectoryModel item)
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
                        commandWrapper.AddInputParameter("@ResourceKey", SqlDbType.Int, (item.CompanyKey == 0) ? SqlInt32.Null : item.CompanyKey);
                        commandWrapper.AddInputParameter("@GroupId", SqlDbType.VarChar, item.GroupId);

                        db.ExecuteNonQuery(commandWrapper);
                        status = commandWrapper.GetValueInt("@ResourceValue");

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

        public Int64 StaffDirectoryEditUser(AStaffDirectoryModel item)
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
                            while (dataReader.Read())
                            {
                                item = new StaffDirectoryModel();
                                ResetPassword(dataReader, item);
                                mailsendAsync(UserKey, item.Email, item.UserName, item.ResetExpirationDate, item.CompanyName, "Reset Password");
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

            }
            return status;
        }

        protected void ResetPassword(DBDataReader dataReader, StaffDirectoryModel item)
        {
            item.Email = dataReader.GetValueText("Email");
            item.UserName = dataReader.GetValueText("Username");
            item.ResetExpirationDate = dataReader.GetValueText("ResetExpirationDate");
            item.CompanyName = dataReader.GetValueText("Name");
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

        public virtual bool Remove(int ResourceKey)
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

        public long Insert(AStaffDirectoryModel item)
        {
            throw new NotImplementedException();
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
                        else if (Status == "Vendor Aporval")
                        {
                            commandWrapper.AddInputParameter("@LookUpTitle", SqlDbType.NText, "Vendor approval");
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
