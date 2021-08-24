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

namespace AssociationBids.Portal.Repository.Base
{
    public class ForgotPasswordRepository : BaseRepository, IForgotPasswordRepository
    {
        public ForgotPasswordRepository() { }

        public ForgotPasswordRepository(string connectionString)
            : base(connectionString) { }

        public bool CheckEmail(string Email)
        {
            List<ForgotPasswordModel> itemList = new List<ForgotPasswordModel>();
            bool Status = false;
            int value = 0;
            try
            {
                string storedProcedure = "site_Resource_ForgotPasswordCheckEmail";

                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@Email", SqlDbType.VarChar, String.IsNullOrEmpty(Email) ? 0 : Email.Length, String.IsNullOrEmpty(Email) ? SqlString.Null : Email);
                        //commandWrapper.AddOutputParameter("@Status", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            ForgotPasswordModel item = null;
                            while (dataReader.Read())
                            {
                                item = new ForgotPasswordModel();
                                DataBind(dataReader, item);
                                itemList.Add(item);
                                Status = ResetPassword(item.UserKey);
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
            return Status;
        }

        protected void DataBind(DBDataReader dataReader, ForgotPasswordModel item)
        {
            item.UserKey = dataReader.GetValueInt("UserKey");
            //item.State = dataReader.GetValueText("Title");
        }

        public bool ResetPassword(int UserKey)
        {
            bool status = false;
            int Data = 0;
            List<ForgotPasswordModel> itemList = new List<ForgotPasswordModel>();
            ForgotPasswordModel item = null;
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
                            //EmailSend email = new EmailSend();
                            while (dataReader.Read())
                            {
                                item = new ForgotPasswordModel();
                                ResetPassword(dataReader, item);
                                mailsendAsync(UserKey, item.email, item.UserName, item.ResetExpirationDate, item.CompanyName);
                                status = true;
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

        protected void ResetPassword(DBDataReader dataReader, ForgotPasswordModel item)
        {
            item.email = dataReader.GetValueText("Email");
            item.UserName = dataReader.GetValueText("Username");
            item.ResetExpirationDate = dataReader.GetValueText("ResetExpirationDate");
            item.CompanyName = dataReader.GetValueText("Name");
        }

        public void mailsendAsync(int UserKey, string fromemail, string UserName, string ResetExpirationDate, string CompanyName)
        {
            try
            {
                IList<EmailTemplateModel> EmailTemplate = null;
                string LinkUrl = System.Configuration.ConfigurationManager.AppSettings["LinkUrl"];
                EmailTemplate = GetAll();

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
                    client.EnableSsl = true;
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

                        commandWrapper.AddInputParameter("@LookUpTitle", SqlDbType.NText, "Forgot Password");

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
