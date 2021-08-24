using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using DB_con;

namespace AssociationBids.Portal.Repository.Base
{
    public class StaffDirectoryRepository : BaseRepository, IStaffDirectoryRepository
    {


        ConnectionCls obj_con = null;
        public StaffDirectoryRepository()
        {
            obj_con = new ConnectionCls();
        }

        public StaffDirectoryRepository(string connectionString)
            : base(connectionString) { }

        public List<StaffDirectoryModel> SearchStaff(long PageSize, long PageIndex, string Search, string Sort, string CompanyKey)
        {
            List<StaffDirectoryModel> itemList = new List<StaffDirectoryModel>();
            try
            {
                string storedProcedure = "site_Resource_SelectIndexPagingFormanager";
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
                            StaffDirectoryModel item = null;
                            while (dataReader.Read())
                            {
                                item = new StaffDirectoryModel();
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

        public List<StaffDirectoryModel> AdvancedSearchStaff(long PageSize, long PageIndex, string Search, string status,string Sort, string CompanyKey)
        {
            List<StaffDirectoryModel> itemList = new List<StaffDirectoryModel>();
            try
            {
                string storedProcedure = "site_Resource_SelectIndexPagingFormanager_New";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters 
                        commandWrapper.AddInputParameter("@PageSize", SqlDbType.BigInt, (PageSize == 0) ? 0 : PageSize);
                        commandWrapper.AddInputParameter("@PageIndex", SqlDbType.BigInt, (PageIndex == 0) ? 0 : PageIndex);
                        commandWrapper.AddInputParameter("@Search", SqlDbType.NText, (Search == "") ? "" : Search);
                        commandWrapper.AddInputParameter("@Status", SqlDbType.NText, (status == "") ? "" : status);
                        commandWrapper.AddInputParameter("@Sort", SqlDbType.NText, (Sort == "") ? "" : Sort);
                        commandWrapper.AddInputParameter("@CompanyKey", SqlDbType.NText, (CompanyKey == "") ? "" : CompanyKey);
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            StaffDirectoryModel item = null;
                            while (dataReader.Read())
                            {
                                item = new StaffDirectoryModel();
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

        protected void LoadStaff(DBDataReader dataReader, StaffDirectoryModel item)
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

        public Int64 Insert(StaffDirectoryModel item)
        {
            Int64 status = 0;

            try
            {
                if (item.FileName == null)
                {
                    item.FileName = "";
                }


                if (item.lst == null || item.lst == "" || item.lst == "0")
                {
                    item.lst = "1100";
                }
                if (item.State == "0")
                {
                    item.State = "";
                }

                string groupid = "";
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
                        commandWrapper.AddInputParameter("@Status", SqlDbType.Int, (Convert.ToInt32(item.Status) == 0) ? SqlInt32.Null : Convert.ToInt32(item.Status));
                        commandWrapper.AddInputParameter("@Zip", SqlDbType.VarChar, String.IsNullOrEmpty(item.Zip) ? 0 : item.Zip.Length, String.IsNullOrEmpty(item.Zip) ? SqlString.Null : item.Zip);

                        commandWrapper.AddInputParameter("@PrimaryContact", SqlDbType.Bit, (item.PrimaryContact == false) ? SqlBoolean.Null : item.PrimaryContact);
                        commandWrapper.AddInputParameter("@Description", SqlDbType.VarChar, String.IsNullOrEmpty(item.Description) ? 0 : item.Description.Length, String.IsNullOrEmpty(item.Description) ? SqlString.Null : item.Description);
                        commandWrapper.AddInputParameter("@Username", SqlDbType.VarChar, String.IsNullOrEmpty(item.UserName) ? 0 : item.UserName.Length, String.IsNullOrEmpty(item.UserName) ? SqlString.Null : item.UserName);
                        commandWrapper.AddInputParameter("@Password", SqlDbType.VarChar, password);

                        commandWrapper.AddInputParameter("@FileName", SqlDbType.VarChar, String.IsNullOrEmpty(item.FileName) ? 0 : item.FileName.Length, String.IsNullOrEmpty(item.FileName) ? SqlString.Null : item.FileName);
                        commandWrapper.AddInputParameter("@FileSize", SqlDbType.Int, item.FileSize);
                        commandWrapper.AddInputParameter("@lst", SqlDbType.VarChar, item.lst);

                        
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
                        commandWrapper.AddOutputParameter("@UserKey", SqlDbType.Int);
                       

                        db.ExecuteNonQuery(commandWrapper);
                        status = commandWrapper.GetValueInt("@ResourceValue");
                        item.UserKey = commandWrapper.GetValueInt("@UserKey");

                        if (status != 0)
                        {
                            
                            if (groupid != "Property Manager")
                            {
                                mailsendAsync(item.UserKey, item.Email, item.FirstName + " " + item.LastName, item.ResetExpirationDate, item.CompanyName, "Reset Password", status);
                            }
                            else 
                            {
                                mailsendAsync(item.UserKey, item.Email, item.FirstName + " " + item.LastName, item.ResetExpirationDate, item.CompanyName, "Manager registration", status);
                            }
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
                        if (Status == "Reset Password") {
                            commandWrapper.AddInputParameter("@LookUpTitle", SqlDbType.NText, "Reset Password");
                        }
                        else if (Status == "Manager registration")
                        {
                            commandWrapper.AddInputParameter("@LookUpTitle", SqlDbType.NText, "Manager registration");

                        }
                        else if(Status == "Email Change")
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

        public void mailsendAsync(int UserKey, string fromemail, string UserName, string ResetExpirationDate, string CompanyName,string Status, Int64 Reskey = 0)
        {
            try
            {
                IList<EmailTemplateModel> EmailTemplate = null;
                string LinkUrl = System.Configuration.ConfigurationManager.AppSettings["LinkUrl"];
                EmailTemplate = GetAll(Status);

                string body = EmailTemplate[0].Body;
                body = body.Replace("[MemberName]", UserName);
                body = body.Replace("[ResetPasswordlink]", LinkUrl + "/ChangePassword/ResetPassword?UserKey=" + UserKey);
                body = body.Replace("[SendDate]", ResetExpirationDate);
                body = body.Replace("[LinkExpiryDate]", ResetExpirationDate);
                body = body.Replace("[MemberCompanyName]", CompanyName);
                body = body.Replace("[MemberEmail]", fromemail);


                string Subject = EmailTemplate[0].EmailSubject;
                Subject = Subject.Replace("[MemberName]", UserName);

             

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

                EmailRepository emailRepos = new EmailRepository();
                EmailModel emailModel = new EmailModel();
                emailModel.Body = msg.Body;
                emailModel.DateAdded = DateTime.Now;
                emailModel.DateSent = DateTime.Now;
                emailModel.From = System.Configuration.ConfigurationManager.AppSettings["senderemail"];
                emailModel.Subject = Subject;
                emailModel.To = fromemail;
                emailModel.EmailStatus = 502;
                emailModel.ModuleKey = 711;
                // Here we  Pass  the UserKey Place  of  ResourceKey
                emailModel.ResourceKey = Convert.ToInt32(Reskey);
                
                bool isInserted = emailRepos.Create(emailModel);


            }
            catch (Exception ex)
            {
            }
        }

        public IList<StaffDirectoryModel> GetAllState()
        {
            List<StaffDirectoryModel> itemList = new List<StaffDirectoryModel>();

            try
            {
                //string storedProcedure = "site_State_GetAll";
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


    

        public List<StaffDirectoryModel> GetDataViewEdit(int id)
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
            catch (Exception ex)
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
            item.Password = Security.Decrypt(Password);
            item.GroupId = dataReader.GetValueText("GroupKey");
            item.FileName = dataReader.GetValueText("FileName");
        }

        public Int64 StaffDirectoryEditStaff(StaffDirectoryModel item)
        {
            Int64 status = 0;

            try
            {

                if (item.lst == null)
                {
                    item.lst = "1100";
                }

                if (item.State == "0")
                {
                    item.State = "";
                }
                

                //item.Status = "1";
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
                        //commandWrapper.AddInputParameter("@lst", SqlDbType.VarChar, item.lst);

                        db.ExecuteNonQuery(commandWrapper);
                        //status = commandWrapper.GetValueInt("@ResourceValue");
                        if (item.OldEmail != item.Email)
                        {
                            mailsendAsync(item.UserKey, item.Email, item.FirstName+" "+ item.LastName, item.ResetExpirationDate, item.CompanyName,"Email Change");
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
                string storedProcedure = "site_Resource_StaffDirectoryGroupEdit";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@ResourceKey", SqlDbType.Int, (item.ResourceKey == 0) ? SqlInt32.Null : item.ResourceKey);
                        commandWrapper.AddInputParameter("@GroupId", SqlDbType.VarChar, item.GroupId);

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
                                mailsendAsync(UserKey, item.Email, item.UserName, item.ResetExpirationDate, item.CompanyName,"Reset Password");
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
            catch(Exception ex)
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


        protected void GetResorceKey(DBDataReader dataReader, StaffDirectoryModel item)
        {
            item.GgroupKey = dataReader.GetValueText("GroupKey");
        }

        public Int64 checkUserrole(int ResourceKey)
        {
            Int64 status = 0;
            try
            {
                obj_con.clearParameter();
                obj_con.BeginTransaction();
                obj_con.addParameter("@ResourceKey", ResourceKey);              
                obj_con.addParameter("@Groupkey", status, DBTrans.Insert);
                obj_con.ExecuteNoneQuery("site_checkUserRole", CommandType.StoredProcedure);
                obj_con.CommitTransaction();
                obj_con.closeConnection();
                return status = Convert.ToInt64(obj_con.getValue("@Groupkey"));
            }
            catch (Exception ex)    
            {
                obj_con.RollbackTransaction();
                throw new Exception("site_checkUserRole" + ex.ToString());
            }
        }
    }
}
