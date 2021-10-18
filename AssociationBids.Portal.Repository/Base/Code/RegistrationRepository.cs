using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace AssociationBids.Portal.Repository.Base.Code
{
    public class RegistrationRepository : BaseRepository, IRegistrationRepository
    {
        private long ResKey = 0;
        private long CompKey = 0;
        public RegistrationRepository()
        {
        }
        public IList<RegistrationModel> GetAllService()
        {
            List<RegistrationModel> itemList = new List<RegistrationModel>();

            try
            {

                string storedProcedure = "site_Service_GetAll";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            RegistrationModel item = null;
                            while (dataReader.Read())
                            {
                                item = new RegistrationModel();
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

        public IList<RegistrationModel> GetAllState()
        {
            List<RegistrationModel> itemList = new List<RegistrationModel>();
            try
            {

                string storedProcedure = "site_State_SelectAll";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            RegistrationModel item = null;
                            while (dataReader.Read())
                            {
                                item = new RegistrationModel();
                                GetAllState(dataReader, item);
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
        protected void GetAllState(DBDataReader dataReader, RegistrationModel item)
        {
            try
            {

                item.StateKey = dataReader.GetValueText("StateKey");
                item.State = dataReader.GetValueText("Title");
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());

            }
        }
        protected void GetAllService(DBDataReader dataReader, RegistrationModel item)
        {
            try
            {
                item.ServiceKey = dataReader.GetValueInt("ServiceKey");
                item.ServiceTitle1 = dataReader.GetValueText("Title");
                item.ServiceTitle2 = dataReader.GetValueText("Title");
                item.ServiceTitle3 = dataReader.GetValueText("Title");

            }
            catch (Exception ex)
            {

                Common.Error.WriteErrorsToFile(ex.Message.ToString());
            }
           
        }


        public long Insert(RegistrationModel item)
        {
            Int64 Resourcekey = 0;
            Int64 status1 = 0;
            try
            {                              
                string companyname = item.CompanyName;
                string LegalName = item.LegalName;
                string TaxID = item.TaxID;
                string Website = item.Website;
                string ServiceTitle1 = item.ServiceTitle1;
                string EmailId = item.EmailId;
                string storedProcedure = "site_Company_Insert_New";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters

                        commandWrapper.AddInputParameter("@CompanyName ", SqlDbType.VarChar, String.IsNullOrEmpty(item.CompanyName) ? 0 : item.CompanyName.Length, String.IsNullOrEmpty(item.CompanyName) ? SqlString.Null : item.CompanyName);
                        commandWrapper.AddInputParameter("@LegalName", SqlDbType.VarChar, String.IsNullOrEmpty(item.LegalName) ? 0 : item.LegalName.Length, String.IsNullOrEmpty(item.LegalName) ? SqlString.Null : item.LegalName);
                        commandWrapper.AddInputParameter("@TaxID", SqlDbType.VarChar, String.IsNullOrEmpty(item.TaxID) ? 0 : item.TaxID.Length, String.IsNullOrEmpty(item.TaxID) ? SqlString.Null : item.TaxID);
                        commandWrapper.AddInputParameter("@Address", SqlDbType.VarChar, String.IsNullOrEmpty(item.Address) ? 0 : item.Address.Length, String.IsNullOrEmpty(item.Address) ? SqlString.Null : item.Address);
                        commandWrapper.AddInputParameter("@Address2", SqlDbType.VarChar, String.IsNullOrEmpty(item.Address2) ? 0 : item.Address2.Length, String.IsNullOrEmpty(item.Address2) ? SqlString.Null : item.Address2);
                        commandWrapper.AddInputParameter("@City", SqlDbType.VarChar, String.IsNullOrEmpty(item.City) ? 0 : item.City.Length, String.IsNullOrEmpty(item.City) ? SqlString.Null : item.City);
                        commandWrapper.AddInputParameter("@StateKey", SqlDbType.VarChar, String.IsNullOrEmpty(item.State) ? 0 : item.State.Length, String.IsNullOrEmpty(item.State) ? SqlString.Null : item.State);
                        commandWrapper.AddInputParameter("@Zip", SqlDbType.VarChar, String.IsNullOrEmpty(item.Zip) ? 0 : item.Zip.Length, String.IsNullOrEmpty(item.Zip) ? SqlString.Null : item.Zip);
                        commandWrapper.AddInputParameter("@Work", SqlDbType.VarChar, String.IsNullOrEmpty(item.Work) ? 0 : item.Work.Length, String.IsNullOrEmpty(item.Work) ? SqlString.Null : item.Work);
                        commandWrapper.AddInputParameter("@Work2", SqlDbType.VarChar, String.IsNullOrEmpty(item.Work2) ? 0 : item.Work2.Length, String.IsNullOrEmpty(item.Work2) ? SqlString.Null : item.Work2);
                        commandWrapper.AddInputParameter("@Fax", SqlDbType.VarChar, String.IsNullOrEmpty(item.Fax) ? 0 : item.Fax.Length, String.IsNullOrEmpty(item.Fax) ? SqlString.Null : item.Fax);
                        commandWrapper.AddInputParameter("@Website", SqlDbType.VarChar, String.IsNullOrEmpty(item.Website) ? 0 : item.Website.Length, String.IsNullOrEmpty(item.Website) ? SqlString.Null : item.Website);
                        commandWrapper.AddInputParameter("@Email", SqlDbType.VarChar, String.IsNullOrEmpty(item.EmailId) ? 0 : item.EmailId.Length, String.IsNullOrEmpty(item.EmailId) ? SqlString.Null : item.EmailId);
                        commandWrapper.AddInputParameter("@FirstName", SqlDbType.VarChar, String.IsNullOrEmpty(item.FirstName) ? 0 : item.FirstName.Length, String.IsNullOrEmpty(item.FirstName) ? SqlString.Null : item.FirstName);
                        commandWrapper.AddInputParameter("@LastName", SqlDbType.VarChar, String.IsNullOrEmpty(item.LastName) ? 0 : item.LastName.Length, String.IsNullOrEmpty(item.LastName) ? SqlString.Null : item.LastName);

                        commandWrapper.AddInputParameter("@ServiceTitle1", SqlDbType.VarChar, String.IsNullOrEmpty(item.ServiceTitle1) ? 0 : item.ServiceTitle1.Length, String.IsNullOrEmpty(item.ServiceTitle1) ? SqlString.Null : item.ServiceTitle1);
                        commandWrapper.AddInputParameter("@RadiusKey", SqlDbType.VarChar, String.IsNullOrEmpty(item.Radius) ? 0 : item.Radius.Length, String.IsNullOrEmpty(item.Radius) ? SqlString.Null : item.Radius);
                        commandWrapper.AddInputParameter("@ServiceAddress", SqlDbType.VarChar, String.IsNullOrEmpty(item.ServiceAddress) ? 0 : item.ServiceAddress.Length, String.IsNullOrEmpty(item.ServiceAddress) ? SqlString.Null : item.ServiceAddress);
                        commandWrapper.AddInputParameter("@CVVNUmber", SqlDbType.VarChar, String.IsNullOrEmpty(item.CVV) ? 0 : item.CVV.Length, String.IsNullOrEmpty(item.CVV) ? SqlString.Null : item.CVV);

                        commandWrapper.AddInputParameter("@TokenId ", SqlDbType.VarChar, item.StripeTokenID);
                        commandWrapper.AddInputParameter("@PMId ", SqlDbType.VarChar, item.PMId);
                        commandWrapper.AddInputParameter("@CardNumber ", SqlDbType.VarChar, String.IsNullOrEmpty(item.CardNumber) ? 0 : item.CardNumber.Length, String.IsNullOrEmpty(item.CardNumber) ? SqlString.Null : item.CardNumber);
                        commandWrapper.AddInputParameter("@cardexpirymonth", SqlDbType.VarChar, String.IsNullOrEmpty(item.ValidTillMM) ? 0 : item.ValidTillMM.Length, String.IsNullOrEmpty(item.ValidTillMM) ? SqlString.Null : item.ValidTillMM);
                        commandWrapper.AddInputParameter("@cardexpiryYear", SqlDbType.VarChar, String.IsNullOrEmpty(item.ValidTillYY) ? 0 : item.ValidTillYY.Length, String.IsNullOrEmpty(item.ValidTillYY) ? SqlString.Null : item.ValidTillYY);
                        commandWrapper.AddInputParameter("@CardHolderFirstname", SqlDbType.VarChar, String.IsNullOrEmpty(item.FirstName) ? 0 : item.FirstName.Length, String.IsNullOrEmpty(item.FirstName) ? SqlString.Null : item.FirstName);
                        commandWrapper.AddInputParameter("@CardHolderLastname", SqlDbType.VarChar, String.IsNullOrEmpty(item.LastName) ? 0 : item.LastName.Length, String.IsNullOrEmpty(item.LastName) ? SqlString.Null : item.LastName);

                        commandWrapper.AddInputParameter("@FileSize", SqlDbType.VarChar, String.IsNullOrEmpty(item.FileSize) ? 0 : item.FileSize.Length, String.IsNullOrEmpty(item.FileSize) ? SqlString.Null : item.FileSize);
                        commandWrapper.AddInputParameter("@FileName", SqlDbType.VarChar, String.IsNullOrEmpty(item.FileName) ? 0 : item.FileName.Length, String.IsNullOrEmpty(item.FileName) ? SqlString.Null : item.FileName);
                        commandWrapper.AddInputParameter("@VendorKeyval", SqlDbType.Int, item.CompanyKey);
                        commandWrapper.AddInputParameter("@Resourcekey", SqlDbType.Int, item.Resourcekey);
                        commandWrapper.AddInputParameter("@latitude", SqlDbType.Decimal, item.Latitude);
                        commandWrapper.AddInputParameter("@longitude", SqlDbType.Decimal, item.Longitude);

                        commandWrapper.AddOutputParameter("@companyvalue", SqlDbType.Int);
                        commandWrapper.AddOutputParameter("@Resourcevalue", SqlDbType.Int);
                        db.ExecuteNonQuery(commandWrapper);
                        status1 = commandWrapper.GetValueInt("@companyvalue");
                        Resourcekey = commandWrapper.GetValueInt("@Resourcevalue");
                        CompKey = status1;
                        ResKey = Resourcekey;
                        if (Resourcekey != 0)
                        {
                            string service = "";
                            string item2 = item.ServiceTitle2v.Replace("--- Select Service ---", "");
                            string item3 = item.ServiceTitle3v.Replace("--- Select Service ---", "");
                            string item1 = item.ServiceTitle1v.Replace("--- Select Service ---", "");
                            if (item2 != "" && item3 != "")
                            {

                                service = item1 + ", " + item2 + ", " + item1;
                            }
                            else if (item2 == "")
                            {
                                if (item1 != "")
                                {
                                    service = item1 + ", " + item3;
                                }
                                else
                                {
                                    service = item3;
                                }
                            }
                            else if (item3 == "")
                            {
                                if (item1 != "")
                                {
                                    service = item1 + ", " + item2;
                                }
                                else
                                {
                                    service = item2;
                                }
                            }
                            else
                            {
                                service = item.ServiceTitle1v;
                            }
                            mailsendAsyncNew(EmailId, companyname, companyname, LegalName, TaxID, Website, ServiceTitle1, "Vendor");
                            mailsend(companyname, LegalName, TaxID, Website, service, EmailId);
                            if (item.AgreementKey != 0)
                            {
                                InsertAgreement(ResKey, item.AgreementKey, item.BindAgreementDetails);
                            }
                         
                            if (status1 != 0)
                            {
                                ChangeVendorStatus(Convert.ToInt32(status1));
                                IABNotificationRepository notificationService = new ABNotificationRepository();
                                notificationService.InsertNotification("VendorReg", 705, status1, Resourcekey, "New Vendor Register");
                            }
                        }



                    }
                }
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
            }

            return status1;
        }

        public bool InsertAgreement(long ResKey, int AgreementKey,string Description)
        {
            bool status = false;

            try
            {

                string storedProcedure = "site_UserAgreement_Insert";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@ResourceKey", SqlDbType.VarChar, ResKey);
                        commandWrapper.AddInputParameter("@AgreementKey", SqlDbType.VarChar, AgreementKey);
                        commandWrapper.AddInputParameter("@Description", SqlDbType.VarChar, Description);

                        //commandWrapper.AddInputParameter("@EmailId", SqlDbType.VarChar, );
                        //commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);
                        db.ExecuteNonQuery(commandWrapper);
                        //status = (commandWrapper.GetValueInt("@errorCode") == 0);
                    }
                }
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
            }

            return status;

        }


        public int IsCompanyName(int Id, string name)
        {

            int value = 0;
            try
            {

                string storedProcedure = "site_Company_Exists_New";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters                   
                        commandWrapper.AddInputParameter("@Name", SqlDbType.VarChar, String.IsNullOrEmpty(name) ? 0 : name.Length, String.IsNullOrEmpty(name) ? SqlString.Null : name);
                        commandWrapper.AddInputParameter("@id", SqlDbType.Int,Id);
                        commandWrapper.AddOutputParameter("@count", SqlDbType.Int);
                        db.ExecuteNonQuery(commandWrapper);
                        value = commandWrapper.GetValueInt("@count");

                    }
                }
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
            }

            return value;
        }

        public bool PaymentInsert(RegistrationModel registrationModel,  string CardNumber, int Month, int Year, string CVV, string Firstname, string lastname)
        {
            bool status = false;

            try
            {

                string storedProcedure = "site_Payment_Insert";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@CardNumber ", SqlDbType.VarChar, CVV);
                        commandWrapper.AddInputParameter("@StripeTokenID", SqlDbType.VarChar, String.IsNullOrEmpty(registrationModel.StripeTokenID) ? 0 : registrationModel.StripeTokenID.Length, String.IsNullOrEmpty(registrationModel.StripeTokenID) ? SqlString.Null : registrationModel.StripeTokenID);

                        //commandWrapper.AddInputParameter("@EmailId", SqlDbType.VarChar, );
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

        public bool PaymentInsert_New(string TokenId, string CVV)
        {
            bool status = false;

            try
            {

                string storedProcedure = "site_Payment_Insert";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@CardNumber ", SqlDbType.VarChar, CVV);
                        commandWrapper.AddInputParameter("@StripeTokenID", SqlDbType.VarChar, String.IsNullOrEmpty(TokenId) ? 0 : TokenId.Length, String.IsNullOrEmpty(TokenId) ? SqlString.Null : TokenId);

                        //commandWrapper.AddInputParameter("@EmailId", SqlDbType.VarChar, );
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


        public virtual RegistrationModel mailsend(string CompanyName, string LegalName, string TaxID, string Website, string ServiceTitle1, string EmailAddress)
        {
            RegistrationModel item = null;
            List<EmailSendModel> mailList = new List<EmailSendModel>();
            try
            {
                string storedProcedure = "site_User_GetForAdmin";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters
                        //commandWrapper.AddInputParameter("@CompanyKey", SqlDbType.Int, companykey);
                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {

                            while (dataReader.Read())
                            {
                                item = new RegistrationModel();

                                LoadViewEdit1(dataReader, item);
                                //mailsendAsync(item.EmailId, item.FirstName, CompanyName, LegalName, TaxID, Website, ServiceTitle1, "Admin");
                                EmailSendModel emailSend = CreateMailTemplateForAdmin(item.EmailId, item.FirstName, CompanyName, LegalName, TaxID, Website, ServiceTitle1, "Admin");

                                EmailModel emailTemplate = new EmailModel();
                                emailTemplate.Body = emailSend.Body;
                                emailTemplate.DateAdded = DateTime.Now;
                                emailTemplate.Subject = emailSend.Subject;
                                emailTemplate.DateSent = DateTime.Now;
                                emailTemplate.EmailStatus = 500;
                                emailTemplate.From = System.Configuration.ConfigurationManager.AppSettings["senderemail"];
                                emailTemplate.To = emailSend.To;
                                emailTemplate.ObjectKey = Convert.ToInt32(CompKey);
                                emailTemplate.ResourceKey = item.Resourcekey;
                                emailTemplate.ModuleKey = 705;
                                EmailRepository emaillog = new EmailRepository();
                                bool isinserted = emaillog.Create(emailTemplate);
                                if (isinserted)
                                    emailSend.EmailKey = emailTemplate.EmailKey;
                                if (emailSend.To.Trim() != "" && emailSend.From.Trim() != "")
                                    mailList.Add(emailSend);
                            }
                        }
                    }
                }
                if(mailList.Count > 0 && mailList[0].From.Trim() != "")
                {
                    Task.Run(() => SendEmailAsync(mailList)).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
            }

            return item;
        }

        protected void LoadViewEdit1(DBDataReader dataReader, RegistrationModel item)
        {
            item.FirstName = dataReader.GetValueText("FirstName");
            item.EmailId = dataReader.GetValueText("Email");
            try
            {
                item.Resourcekey = dataReader.GetValueInt("ResourceKey");
            }
            catch
            {

            }
        }

        public void mailsendAsync(string fromemail, string Name,string CompanyName,string LegalName,string TaxID,string Website,string ServiceTitle1,string Status)
        {
            try
            {
                IList<Model.EmailTemplateModel> EmailTemplate = null;
                string LinkUrl = System.Configuration.ConfigurationManager.AppSettings["LinkUrl"];
                MailMessage msg = new MailMessage();
                string strBody = string.Empty;
                EmailTemplate = GetAll(Status);
                string body = EmailTemplate[0].Body;

                body = body.Replace("[MemberName]", Name.ToString().Trim());
                body = body.Replace("[CompanyName]", CompanyName.ToString().Trim());
                body = body.Replace("[LegalName]", LegalName);
                body = body.Replace("[TaxId]", TaxID);
                body = body.Replace("[WebSite]", Website);
                body = body.Replace("[Service]", ServiceTitle1);

                string Subject = EmailTemplate[0].EmailSubject;
                Subject = Subject.Replace("[CompanyName]", CompanyName.ToString().Trim());
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

        public void mailsendAsyncNew(string fromemail, string Name, string CompanyName, string LegalName, string TaxID, string Website, string ServiceTitle1, string Status)
        {
            try
            {
                List<EmailSendModel> mailList = new List<EmailSendModel>();
                EmailSendModel emailSend = new EmailSendModel();
                IList<Model.EmailTemplateModel> EmailTemplate = null;
                string LinkUrl = System.Configuration.ConfigurationManager.AppSettings["LinkUrl"];
                //MailMessage msg = new MailMessage();
                string strBody = string.Empty;
                EmailTemplate = GetAll(Status);
                string body = EmailTemplate[0].Body;

                body = body.Replace("[MemberName]", Name.ToString().Trim());
                body = body.Replace("[CompanyName]", CompanyName.ToString().Trim());
                body = body.Replace("[LegalName]", LegalName);
                body = body.Replace("[TaxId]", TaxID);
                body = body.Replace("[WebSite]", Website);
                body = body.Replace("[Service]", ServiceTitle1);

                string Subject = EmailTemplate[0].EmailSubject;
                Subject = Subject.Replace("[CompanyName]", CompanyName.ToString().Trim());
                emailSend.From =System.Configuration.ConfigurationManager.AppSettings["senderemail"];
                emailSend.To = fromemail;
                emailSend.Subject = Subject;
                emailSend.IsHtml = true;
                emailSend.Body = body;

                EmailModel emailTemplate = new EmailModel();
                emailTemplate.Body = body;
                emailTemplate.DateAdded = DateTime.Now;
                emailTemplate.Subject = Subject;
                emailTemplate.DateSent = DateTime.Now;
                emailTemplate.EmailStatus = 500;
                emailTemplate.From = System.Configuration.ConfigurationManager.AppSettings["senderemail"];
                emailTemplate.To = fromemail;
                emailTemplate.ObjectKey = Convert.ToInt32(CompKey);
                emailTemplate.ResourceKey = Convert.ToInt32(ResKey);
                emailTemplate.ModuleKey = 705;
                EmailRepository emaillog = new EmailRepository();
                bool isinserted = emaillog.Create(emailTemplate);
                if (isinserted)
                    emailSend.EmailKey = emailTemplate.EmailKey;



                if (emailSend.From.Trim() != "" && emailSend.To.Trim() != "")
                    mailList.Add(emailSend);

                if (mailList.Count > 0)
                    Task.Run(() => SendEmailAsync(mailList)).ConfigureAwait(false);
                //using (SmtpClient client = new SmtpClient())
                //{
                //    client.EnableSsl = true;
                //    client.UseDefaultCredentials = false;
                //    client.Credentials = new NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["smtpUser"], System.Configuration.ConfigurationManager.AppSettings["smtpPass"]);
                //    client.Host = System.Configuration.ConfigurationManager.AppSettings["smtpServer"];
                //    client.Port = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["smtpPort"]);
                //    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                //    await client.SendMailAsync(msg);
                //}
            }
            catch (Exception ex)
            {
            }
        }


        public EmailSendModel CreateMailTemplateForAdmin(string fromemail, string Name, string CompanyName, string LegalName, string TaxID, string Website, string ServiceTitle1, string Status)
        {
            EmailSendModel emailTemplate = new EmailSendModel();
            try
            {
                IList<Model.EmailTemplateModel> EmailTemplate = null;
                string LinkUrl = System.Configuration.ConfigurationManager.AppSettings["LinkUrl"];
                MailMessage msg = new MailMessage();
                string strBody = string.Empty;
                EmailTemplate = GetAll(Status);
                string body = EmailTemplate[0].Body;

                body = body.Replace("[MemberName]", Name.ToString().Trim());
                body = body.Replace("[CompanyName]", CompanyName.ToString().Trim());
                body = body.Replace("[LegalName]", LegalName);
                body = body.Replace("[TaxId]", TaxID);
                body = body.Replace("[WebSite]", Website);
                body = body.Replace("[Service]", ServiceTitle1);

                string Subject = EmailTemplate[0].EmailSubject;
                Subject = Subject.Replace("[CompanyName]", CompanyName.ToString().Trim());
                msg.From = new MailAddress(System.Configuration.ConfigurationManager.AppSettings["senderemail"]);
                msg.To.Add(fromemail);
                msg.Subject = Subject;

                msg.IsBodyHtml = true;


                msg.Body += body;
                emailTemplate.Body = msg.Body;
                emailTemplate.From = System.Configuration.ConfigurationManager.AppSettings["senderemail"];
                emailTemplate.IsHtml = msg.IsBodyHtml;
                emailTemplate.Subject = msg.Subject;
                emailTemplate.To = fromemail;


                
                //using (SmtpClient client = new SmtpClient())
                //{
                //    client.EnableSsl = true;
                //    client.UseDefaultCredentials = false;
                //    client.Credentials = new NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["smtpUser"], System.Configuration.ConfigurationManager.AppSettings["smtpPass"]);
                //    client.Host = System.Configuration.ConfigurationManager.AppSettings["smtpServer"];
                //    client.Port = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["smtpPort"]);
                //    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                //    await client.SendMailAsync(msg);
                //}
            }
            catch (Exception ex)
            {
            }
            return emailTemplate;
        }

        public async Task SendEmailAsync(List<EmailSendModel> emailList)
        {
            EmailRepository emailRepos = new EmailRepository();
            EmailModel emailModel = null;
            foreach (var email in emailList)
            {
                try
                {
                    MailMessage msg = new MailMessage();
                    msg.To.Add(email.To);
                    msg.From = new MailAddress(email.From);
                    msg.Subject = email.Subject;
                    msg.Body = email.Body;
                    msg.IsBodyHtml = email.IsHtml;
                    emailModel = emailRepos.Get(Convert.ToInt32(email.EmailKey));
                    using (SmtpClient client = new SmtpClient())
                    {
                        client.EnableSsl = true;
                        client.UseDefaultCredentials = false;
                        client.Credentials = new NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["smtpUser"], System.Configuration.ConfigurationManager.AppSettings["smtpPass"]);
                        client.Host = System.Configuration.ConfigurationManager.AppSettings["smtpServer"];
                        client.Port = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["smtpPort"]);
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        await client.SendMailAsync(msg);
                        emailModel.EmailStatus = 502;
                        emailRepos.Update(emailModel);
                    }
                }
                catch
                {
                    if (emailModel != null)
                    {
                        emailModel.EmailStatus = 501;
                        emailRepos.Update(emailModel);
                    }
                }

            }
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
                        if (Status == "Admin")
                        {
                            commandWrapper.AddInputParameter("@LookUpTitle", SqlDbType.NText, "Vendor registration");
                        }
                        else if (Status == "Vendor")
                        {
                            commandWrapper.AddInputParameter("@LookUpTitle", SqlDbType.NText, "Vendor Registration Confirm");
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

        public int IsEmailExist(string name,int ResourceKey)
        {

            int value = 0;
            try
            {

                string storedProcedure = "site_Resource_CheckDuplicatedEmail_Registration";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters                   
                        commandWrapper.AddInputParameter("@Email", SqlDbType.VarChar, String.IsNullOrEmpty(name) ? 0 : name.Length, String.IsNullOrEmpty(name) ? SqlString.Null : name);
                        commandWrapper.AddInputParameter("@ResourceKey", SqlDbType.Int, ResourceKey);
                        commandWrapper.AddOutputParameter("@Status", SqlDbType.Int);
                        db.ExecuteNonQuery(commandWrapper);
                        value = commandWrapper.GetValueInt("@Status");

                    }
                }
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
            }

            return value;
        }
       

        public virtual RegistrationModel Getvendordetails(int companykey)
        {
            RegistrationModel item = null;

            try
            {

                string storedProcedure = "site_Registration_SelectOneByCompanyKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@CompanyKey", SqlDbType.Int, companykey);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            while (dataReader.Read())
                            {
                                item = new RegistrationModel();

                                LoadViewEdit(dataReader, item);
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

        public virtual RegistrationModel GeAgreementDetails(int companykey)
        {
            RegistrationModel item = null;

            try
            {

                string storedProcedure = "site_Agreement_SelectShowForVendorRegistration";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        //commandWrapper.AddInputParameter("@CompanyKey", SqlDbType.Int, companykey);

                        // add stored procedure output parameters
                        //commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            while (dataReader.Read())
                            {
                                item = new RegistrationModel();

                                LoadAgreementViewEdit(dataReader, item);
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

        protected void LoadAgreementViewEdit(DBDataReader dataReader, RegistrationModel item)
        {
            item.BindAgreementDetails = dataReader.GetValueText("Description");
            item.AgreementKey = dataReader.GetValueInt("AgreementKey");
        }
        protected void LoadViewEdit(DBDataReader dataReader, RegistrationModel item)
        {


            item.CompanyKey = dataReader.GetValueInt("CompanyKey");

            item.LegalName = dataReader.GetValueText("LegalName");
            item.CompanyName = dataReader.GetValueText("Name");
            item.TaxID = dataReader.GetValueText("TaxID");


            item.FirstName = dataReader.GetValueText("FirstName");
            item.LastName = dataReader.GetValueText("LastName");
            item.EmailId = dataReader.GetValueText("Email");

            item.Fax = dataReader.GetValueText("Fax");
            item.Address = dataReader.GetValueText("Address");
            item.Address2 = dataReader.GetValueText("Address2");
            item.City = dataReader.GetValueText("City");
            item.State = dataReader.GetValueText("State");
            item.Zip = dataReader.GetValueText("Zip");
            item.Website = dataReader.GetValueText("Website");

            item.Work = dataReader.GetValueText("Work");
            item.Work2 = dataReader.GetValueText("Work2");
            item.Resourcekey = dataReader.GetValueInt("Resourcekey");
            item.ServiceTitle1 = dataReader.GetValueText("title");


            item.Radius = dataReader.GetValueText("Radius");




        }

        protected void ChangeVendorStatus(int CompanyKey)
        {
            try
            {
                using (Database db = new Database(ConnectionString))
                {
                    string Query = "update Company set Status = 102 where CompanyKey = " + CompanyKey;
                    using (DBCommandWrapper commandWrapper = db.GetSqlStringCommandWrapper(Query))
                    {
                        //DBCommandWrapper commandWrapper = db.GetSqlStringCommandWrapper(Query);
                        db.ExecuteNonQuery(commandWrapper);
                    }
                }
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
            }
        }

        public virtual bool GetLinkExpiredCheck(int companykey)
        {
            RegistrationModel item = null;
            bool status = false;
            try
            {

                string storedProcedure = "site_UserRegistrationLink_CheckLinkExpired";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@CompanyKey", SqlDbType.Int, companykey);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            while (dataReader.Read())
                            {
                                item = new RegistrationModel();

                                status=  GetLinkExpiredCheck(dataReader);
                                
                            }
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
        protected bool  GetLinkExpiredCheck(DBDataReader dataReader)
        {
            bool status = false;
            status = dataReader.GetValueBool("Link_Expiration");
            

            return status;

        }
    }
}
