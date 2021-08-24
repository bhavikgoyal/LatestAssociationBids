using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base.Interface;
using Stripe;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Net;
using System.Net.Mail;
using AssociationBids.Portal.Common;
using Invoice = AssociationBids.Portal.Common.Invoice;
using System.Web;

namespace AssociationBids.Portal.Repository.Base.Code
{
    public class APIRepositery : BaseRepository, IAPIRepositery
    {

        public APIRepositery() { }

        public APIRepositery(string connectionString)
            : base(connectionString) { }


        protected void LoadBidResponsePassed(DBDataReader dataReader, APIModel item)
        {

            item.BidRequestKey = dataReader.GetValueInt("BidRequestKey");

        }
        protected void LoadVendorByBidRequest(DBDataReader dataReader, APIModel item)
        {

            item.Name = dataReader.GetValueText("Title");
            item.ContactPerson = dataReader.GetValueText("ContactPerson");
            item.Email = dataReader.GetValueText("Email");
            item.ResponseDueDates = dataReader.GetValueText("BidDueDate");
            item.CompanyName = dataReader.GetValueText("CompanyName");

        }

        protected void CardExpiredetails(DBDataReader dataReader, APIModel item)
        {
            item.cardExpirydate = dataReader.GetValueText("CardExpiryDate");
            item.ContactPerson = dataReader.GetValueText("ContactPerson");
            item.Email = dataReader.GetValueText("Email");
            item.CardNumber = dataReader.GetValueText("CardNumber");
        }
        protected void BidAbdWorkOrderEmailSent(DBDataReader dataReader, ReportEmailModel item)
        {
            item.ReportEmailKey = dataReader.GetValueInt("ReportEmailKey");
            item.DocumentName = dataReader.GetValueText("DocumentName");
            item.IncludeCOI = dataReader.GetValueBool("IncludeCOI");
            item.IsDetailedReport = dataReader.GetValueBool("IsDetailedReport");
            item.IsSent = dataReader.GetValueBool("IsSent");
            item.ResourceKey = dataReader.GetValueInt("ResourceKey");
            item.VendorList = dataReader.GetValueText("VendorList");
        }
        protected void InsauranceRenewalDetails(DBDataReader dataReader, APIModel item)
        {


            item.Renewaldate = dataReader.GetValueText("RenewalDate");
            item.ContactPerson = dataReader.GetValueText("ConatactPerson");
            item.Email = dataReader.GetValueText("Email");
            item.PolicyNumber = dataReader.GetValueText("PolicyNumber");
        }

        protected void MembershipRenewal(DBDataReader dataReader, APIModel item)
        {


            item.Renewaldate = dataReader.GetValueText("RenewalDate");
            item.ContactPerson = dataReader.GetValueText("ContactPerson");
            item.Email = dataReader.GetValueText("Email");

        }


        protected void WinFeechargeMail(DBDataReader dataReader, APIModel item)
        {
            item.CardHoldername = dataReader.GetValueText("CardHoldername");
            item.CardExpiryMonth = dataReader.GetValueInt("CardExpiryMonth");
            item.CardExpiryYear = dataReader.GetValueInt("CardExpiryYear");
            item.PaymentTypeKey = dataReader.GetValueInt("PayMentType");
            item.Address1 = dataReader.GetValueText("Line1");
            item.City = dataReader.GetValueText("city");
            item.State = dataReader.GetValueText("State");
            item.PostalCode = dataReader.GetValueText("PostalCode");
            item.Email = dataReader.GetValueText("Email");
            item.stripeToken = dataReader.GetValueText("stripeToken");
            item.PaymentMethodId = dataReader.GetValueText("PaymentMethodID");
            item.Title = dataReader.GetValueText("Title");
            item.VendorKey = dataReader.GetValueInt("VendorKey");
            item.CardNumber = dataReader.GetValueText("ccNumber");
            item.CVV = dataReader.GetValueText("Cvvnumber");
            item.Amount = dataReader.GetValueInt("Amt");
            item.Address2 = dataReader.GetValueText("add2");
            item.BidvendorId = dataReader.GetValueText("bvid");

        }

        protected void Invitemailforvendor(DBDataReader dataReader, APIModel item)
        {
          
            item.Email = dataReader.GetValueText("Email");
            item.VendorName = dataReader.GetValueText("Vendorname");
            item.CompanyName = dataReader.GetValueText("CompanyName");
            item.bdate = dataReader.GetValueText("BidDueDate");
            item.Title = dataReader.GetValueText("BidTitle");
            item.propertyTitle = dataReader.GetValueText("PropertyTitle");

        }
        protected void MembershipfeeLoaddata(DBDataReader dataReader, APIModel item)
        {
            item.CardHoldername = dataReader.GetValueText("CardHoldername");
            item.CardExpiryMonth = dataReader.GetValueInt("CardExpiryMonth");
            item.CardExpiryYear = dataReader.GetValueInt("CardExpiryYear");
            item.PaymentTypeKey = dataReader.GetValueInt("PayMentType");
            item.Address1 = dataReader.GetValueText("Line1");
            item.City = dataReader.GetValueText("city");
            item.State = dataReader.GetValueText("State");
            item.PostalCode = dataReader.GetValueText("PostalCode");
            item.Email = dataReader.GetValueText("Email");
            item.stripeToken = dataReader.GetValueText("stripeToken");
            
            item.CardNumber = dataReader.GetValueText("ccNumber");
            item.CVV = dataReader.GetValueText("Cvvnumber");
            item.Amount = dataReader.GetValueInt("Amt");
            item.Address2 = dataReader.GetValueText("add2");
            item.VendorKey = dataReader.GetValueInt("vkey");
            item.RNumber = dataReader.GetValueText("Rnumber");

            try
            {
                item.PaymentMethodId = dataReader.GetValueText("PaymentMethodID");
            }
            catch { }

        }


        public bool DowmloadInvoice(string imagepath, string destinationPath, int Vendorkey, string fromemail, string UserName, string Title, int Amount, string addressline1, string addressline2, string zip, string City, string state)
        {

            bool status = false;

            if (!System.IO.Directory.Exists(destinationPath))
            {
                System.IO.Directory.CreateDirectory(destinationPath);
            }

            Invoicecollection obj = new Invoicecollection();

            string date = DateTime.Now.ToString();

            obj.createFile(destinationPath);
            obj.openDocument();
            //obj.addHeaderToMainTable1("The Kanam Residency Housing & Commercial Co-Op. Service  Society Limited \n Kudasan, Gandhinagar");
            obj.addHeaderCheckboxList2(imagepath, date, Vendorkey);
            obj.horizontalline();
            obj.addReciptDetailsTable3(fromemail, addressline1, addressline2, zip, City, state);
            obj.addreciptdatefor4(Title, Amount);
            //obj.addReciptAmountDetailTable5(reportPath, Amount, PaymentMode, Notes, Discount, Penalty, BasicAmount);
            //obj.addReciptAmountTypeDetailTable6();
            //obj.addNotesAtFooter7(ReferenceBy, PaymentMode);
            obj.addFootercontent8();
            obj.addFooterRow9();
            //obj.InsertMaintenanceblockunit(PostBlockUnitMaintenanceID, RecepitNo, PaymentMode, Notes, ValidforMonth, Amount, Addedon);
            obj.closeDocument();
            string filename = obj.getFilename();


            //filename = "http://"+Request.Host.Value + "/Documents/CollectionInvoices/" + filename+""; 

            if (filename != "")
            {
                status = true;
            }
            return status;
        }

        public IList<APIModel> GetBidRequestListPassedResponsedate()
        {
            List<APIModel> itemList = new List<APIModel>();

            try
            {
                string storedProcedure = "Site_GetAllBidresponseDuespassed";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {



                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            APIModel item = null;
                            while (dataReader.Read())
                            {
                                item = new APIModel();
                                LoadBidResponsePassed(dataReader, item);
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

        public IList<APIModel> SendMailToBidDueRemiders(string lookUpTitle)
        {
            List<APIModel> itemList = new List<APIModel>();

            try
            {
                string storedProcedure = "site_getallbidduevendors";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {



                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            APIModel item = null;
                            while (dataReader.Read())
                            {
                                item = new APIModel();
                                LoadVendorByBidRequest(dataReader, item);
                                itemList.Add(item);
                                VendorRemindermailsend(item.Email, item.ContactPerson, item.Name, item.ResponseDueDates, lookUpTitle, item.CompanyName);
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


        public IList<APIModel> SendMailToBidSubmissionReminderDue(string lookUpTitle)
        {
            List<APIModel> itemList = new List<APIModel>();

            try
            {
                string storedProcedure = "site_getallbiddSubmission_Reminders";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {



                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            APIModel item = null;
                            while (dataReader.Read())
                            {
                                item = new APIModel();
                                LoadVendorByBidRequest(dataReader, item);
                                itemList.Add(item);
                                VendorRemindermailsend(item.Email, item.ContactPerson, item.Name, item.ResponseDueDates, lookUpTitle,"");
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

        public IList<APIModel> CardExpiremail(string lookUpTitle)
        {
            List<APIModel> itemList = new List<APIModel>();

            try
            {
                string storedProcedure = "Site_GetCardExpireDetails";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        if (lookUpTitle != "Credit card expired") {
                            commandWrapper.AddInputParameter("@lookUpTitle ", SqlDbType.Int, 1);
                        }
                        else {
                            commandWrapper.AddInputParameter("@lookUpTitle ", SqlDbType.Int, 2);
                        }

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            APIModel item = null;
                            while (dataReader.Read())
                            {
                                item = new APIModel();
                                CardExpiredetails(dataReader, item);
                                itemList.Add(item);
                                CardExpiryMailsend(item.Email, item.ContactPerson, item.CardNumber, item.cardExpirydate, lookUpTitle);
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
        
        public IList<ReportEmailModel> GetReportList()
        {
            List<ReportEmailModel> itemList = new List<ReportEmailModel>();

            try
            {
                string storedProcedure = "site_ReportEmail_GetForMail";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            ReportEmailModel item = null;
                            while (dataReader.Read())
                            {
                                item = new ReportEmailModel();
                                BidAbdWorkOrderEmailSent(dataReader, item);
                                itemList.Add(item);
                                //CardExpiryMailsend(item.Email, item.ContactPerson, item.CardNumber, item.cardExpirydate, lookUpTitle);
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

        public bool BidAbdWorkOrderEmailSent(ReportEmailModel reportEmail)
        {
            RegistrationModel item = null;
            List<RegistrationModel> items = new List<RegistrationModel>();
            List<EmailSendModel> mailList = new List<EmailSendModel>();
            try
            {
                string storedProcedure = "site_User_GetForAdmin";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@ResourceKey", SqlDbType.Int, reportEmail.ResourceKey);
                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {

                            while (dataReader.Read())
                            {
                                item = new RegistrationModel();
                                LoadViewEdit1(dataReader, item);
                                items.Add(item);
                            }
                        }
                    }
                }
                if (items[0].Resourcekey == reportEmail.ResourceKey)
                {
                    string ReportTypeName = "";
                    if (reportEmail.IsDetailedReport == true)
                    {
                        ReportTypeName = "DetailReport"; 
                    }
                    else 
                    { 
                        ReportTypeName = "SummaryReport"; 
                    }
                    mailsendAsync(reportEmail.ReportDocumentFilePath, reportEmail.InsuranceDocumentFilePath, ReportTypeName, items[0].Resourcekey, items[0].EmailId, items[0].FirstName);
                    IsSentEmail(reportEmail.ReportEmailKey); 
                }
                
                return true;
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
                return false;
            }
            }

        public bool IsSentEmail(int ReportEmailKey)
        {
            
            try
            {
                string storedProcedure = "site_ReportEmail_UpdateIsSent";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@ReportEmailKey", SqlDbType.Int, ReportEmailKey);

                        db.ExecuteNonQuery(commandWrapper);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
                return false;
            }

        }

        public void mailsendAsync(string FilePath,string InsuranceDocumentFilePath, string ReportTypeName, int Resourcekey, string EmailId, string FirstName)
        {
            try
            {
                string Status = "Admin";
                IList<Model.EmailTemplateModel> EmailTemplate = null;
                MailMessage msg = new MailMessage();
                string LinkUrl = System.Configuration.ConfigurationManager.AppSettings["LinkUrl"];
                string strBody = string.Empty;
                EmailTemplate = GetAll(Status);
                string body = EmailTemplate[0].Body;

                body = body.Replace("[MemberName]", FirstName.ToString().Trim());

                string Subject = ReportTypeName;

                msg.From = new MailAddress(System.Configuration.ConfigurationManager.AppSettings["senderemail"]);
                //msg.To.Add(EmailId);
                msg.To.Add(EmailId);

                msg.Subject = Subject;

                msg.IsBodyHtml = true;

                msg.Body += body;

                Attachment attachment;
                if (FilePath != "") 
                {
                    attachment = new Attachment(FilePath);
                    msg.Attachments.Add(attachment);
                }
                if (InsuranceDocumentFilePath != null && InsuranceDocumentFilePath != "")
                {
                    string[] IFilePath = InsuranceDocumentFilePath.Split(',');
                    for (int i = 0; i < IFilePath.Length; i++)
                    {
                        attachment = new Attachment(IFilePath[i]);
                        msg.Attachments.Add(attachment);
                    }
                }
                
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
                            commandWrapper.AddInputParameter("@LookUpTitle", SqlDbType.NText, "Bid Report");
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



        protected void LoadViewEdit1(DBDataReader dataReader, RegistrationModel item)
        {
            item.FirstName = dataReader.GetValueText("FirstName");
            item.EmailId = dataReader.GetValueText("Email");
            item.FirstName = dataReader.GetValueText("FirstName");
            try
            {
                item.Resourcekey = dataReader.GetValueInt("ResourceKey");
            }
            catch
            {

            }
        }

        public IList<APIModel> InsaurenceRenawalmail(string lookUpTitle)
        {
            List<APIModel> itemList = new List<APIModel>();

            try
            {
                string storedProcedure = "site_GetallInsaurnceRenamwal";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {



                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            APIModel item = null;
                            while (dataReader.Read())
                            {
                                item = new APIModel();
                                InsauranceRenewalDetails(dataReader, item);
                                itemList.Add(item);
                                InsaurenceRenawalMail(item.Email, item.ContactPerson, item.PolicyNumber, item.Renewaldate, lookUpTitle);
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


        public IList<APIModel> MembershipRenewalMail(string lookUpTitle)
        {
            List<APIModel> itemList = new List<APIModel>();

            try
            {
                string storedProcedure = "site_GetallMembershipRenewal";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {



                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            APIModel item = null;
                            while (dataReader.Read())
                            {
                                item = new APIModel();
                                MembershipRenewal(dataReader, item);
                                itemList.Add(item);
                                MembershipRenewal(item.Email, item.ContactPerson, item.Renewaldate, lookUpTitle);
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
        public IList<APIModel> MembershipExpired(string lookUpTitle)
        {
            List<APIModel> itemList = new List<APIModel>();

            try
            {
                string storedProcedure = "site_GetallMembershipExpire";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {



                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            APIModel item = null;
                            while (dataReader.Read())
                            {
                                item = new APIModel();
                                MembershipRenewal(dataReader, item);
                                itemList.Add(item);
                                MembershipRenewal(item.Email, item.ContactPerson, item.Renewaldate, lookUpTitle);
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


        public IList<APIModel> InsaurenceExpiredmail(string lookUpTitle)
        {
            List<APIModel> itemList = new List<APIModel>();

            try
            {
                string storedProcedure = "site_GetallInsaurnceExpired";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {



                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            APIModel item = null;
                            while (dataReader.Read())
                            {
                                item = new APIModel();
                                InsauranceRenewalDetails(dataReader, item);
                                itemList.Add(item);
                                InsaurenceRenawalMail(item.Email, item.ContactPerson, item.PolicyNumber, item.Renewaldate, lookUpTitle);
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


        public IList<APIModel> BidFinePayment(string lookUpTitle)
        {
            List<APIModel> itemList = new List<APIModel>();


            try
            {
                string storedProcedure = "Site_GetAllVendorBidNotSubmmited";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            APIModel item = null;
                            while (dataReader.Read())
                            {
                                item = new APIModel();
                                WinFeechargeMail(dataReader, item);
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
        public IList<APIModel> InvitemailToVendor(string lookUpTitle)
        {
            List<APIModel> itemList = new List<APIModel>();
            try
            {
                string storedProcedure = "site_BidRequest_GetReplacementforRejected";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {



                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            APIModel item = null;
                            while (dataReader.Read())
                            {
                                item = new APIModel();
                                Invitemailforvendor(dataReader, item);
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

        public IList<APIModel> WinFeechargeMailAsync(string lookUpTitle)
        {
            List<APIModel> itemList = new List<APIModel>();


            try
            {
                string storedProcedure = "Site_GetVendordetailBidAccpetedforPayment";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {



                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            APIModel item = null;
                            while (dataReader.Read())
                            {
                                item = new APIModel();
                                WinFeechargeMail(dataReader, item);
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

        public IList<APIModel> MembershipFees(string lookUpTitle)
        {
            List<APIModel> itemList = new List<APIModel>();


            try
            {
                string storedProcedure = "Site_GetVendordetailMembershipFeePayment";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            APIModel item = null;
                            while (dataReader.Read())
                            {
                                item = new APIModel();
                                MembershipfeeLoaddata(dataReader, item);
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

        public bool PaymentmodalInsert(string CVV, string StripToken)
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
                        commandWrapper.AddInputParameter("@StripeTokenID", SqlDbType.VarChar, StripToken);
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
                string Remarks = ex.Message;
                InsertIntoErrorLog(Remarks);
            }

            return status;

        }

        public bool MembershipInsert(int VendorKey)
        {
            bool status = false;

            try
            {

                string storedProcedure = "site_MembershipPayment_Insert";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@VendorKey ", SqlDbType.Int, VendorKey);

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
                string Remarks = ex.Message;
                InsertIntoErrorLog(Remarks);
            }

            return status;

        }

        public bool Updatemembership(int VendorKey)
        {
            bool status = false;

            try
            {

                string storedProcedure = "site_MembershipPayment_Update";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@VendorKey ", SqlDbType.Int, VendorKey);

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
                string Remarks = ex.Message;
                InsertIntoErrorLog(Remarks);
            }

            return status;

        }


        public bool UpdateVendorStatus(int BidVendorKey)
        {
            bool status = false;

            try
            {

                string storedProcedure = "site_BidVendorStatus_Update";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@BidVendorKey ", SqlDbType.Int, BidVendorKey);

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
                string Remarks = ex.Message;
                InsertIntoErrorLog(Remarks);
            }

            return status;

        }

        public bool insertBidVendorForBid(int BidRequestKey)
        {
            bool status = false;

            try
            {

                string storedProcedure = "site_INsertAutomaticallyVendorForBid ";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@bidRequestKey", SqlDbType.Int, BidRequestKey);

                        //commandWrapper.AddInputParameter("@EmailId", SqlDbType.VarChar, );
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);
                        var reader = db.ExecuteReader(commandWrapper);
                        status = (commandWrapper.GetValueInt("@errorCode") == 0);
                        while (reader.Read())
                        {
                            BidRequestRepository bidRequest = new BidRequestRepository();
                            int bidvendorKey = reader.GetValueInt("BidVendorKey");
                            bidRequest.SendMailToBidVendorByBidVendorKey(bidvendorKey);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
                string Remarks = ex.Message;
                InsertIntoErrorLog(Remarks);
            }

            return status;

        }

        public bool Insertpaymet(string RefrenceNumber, int Amount, int vendorKey, int PricingTypeKey, string stripeToken)
        {
            bool status = false;

            try
            {
                if (RefrenceNumber == null)
                {
                    RefrenceNumber = "0";
                }

                string storedProcedure = "site_PaymentSP_Insert";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@RefrenceNumber ", SqlDbType.VarChar, RefrenceNumber);
                        commandWrapper.AddInputParameter("@Amount", SqlDbType.VarChar, Amount);
                        commandWrapper.AddInputParameter("@Balance", SqlDbType.VarChar, Amount);
                        if (stripeToken == "" || stripeToken == null)
                        {
                            commandWrapper.AddInputParameter("@stripeToken", SqlDbType.VarChar, DBNull.Value);
                        }
                        else
                        {
                            commandWrapper.AddInputParameter("@stripeToken", SqlDbType.VarChar, stripeToken);
                        }
                        
                        commandWrapper.AddInputParameter("@VendorKey", SqlDbType.Int, vendorKey);
                        commandWrapper.AddInputParameter("@PricingTypeKey", SqlDbType.VarChar, PricingTypeKey);
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
                string Remarks = ex.Message;
                InsertIntoErrorLog(Remarks);
            }

            return status;

        }

        public bool Insertpaymetfail(string RefrenceNumber, int Amount, int vendorKey, int PricingTypeKey, string stripeToken)
        {
            bool status = false;

            try
            {

                string storedProcedure = "site_PaymentFail_Insert";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@RefrenceNumber ", SqlDbType.VarChar, RefrenceNumber);
                        commandWrapper.AddInputParameter("@Amount", SqlDbType.VarChar, "0");
                        commandWrapper.AddInputParameter("@Balance", SqlDbType.Int, Amount);
                        if (stripeToken == "" || stripeToken == null)
                        {
                            commandWrapper.AddInputParameter("@stripeToken", SqlDbType.VarChar, DBNull.Value);
                        }
                        else
                        {
                            commandWrapper.AddInputParameter("@stripeToken", SqlDbType.VarChar, stripeToken);
                        }
                        commandWrapper.AddInputParameter("@VendorKey", SqlDbType.Int, vendorKey);
                        commandWrapper.AddInputParameter("@PricingTypeKey", SqlDbType.VarChar, PricingTypeKey);
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
                string Remarks = ex.Message;
                InsertIntoErrorLog(Remarks);
            }

            return status;

        }

        public virtual APIModel GetDataforMembershipPayment(int id)
        {
            APIModel item = null;

            try
            {
                string storedProcedure = "site_Membership_FeePayment";
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
                                item = new APIModel();

                                MembershipfeeLoaddata(dataReader, item);
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
        public virtual APIModel GetdataWinfeechargePaymentAccpectBid(int id, string BidVendorkey)
        {
            APIModel item = null;

            try
            {
                string storedProcedure = "site_getvendorBidFeepaymentDetails";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@BidRequestKey", SqlDbType.Int, id);
                        commandWrapper.AddInputParameter("@BidVendorkey", SqlDbType.VarChar, BidVendorkey);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            while (dataReader.Read())
                            {
                                item = new APIModel();

                                WinFeechargeMail(dataReader, item);
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

        protected void Load(DBDataReader dataReader, EmailTemplateModel item)
        {
            item.EmailTemplateKey = dataReader.GetValueInt("EmailTemplateKey");
            item.EmailTitle = dataReader.GetValueText("EmailTitle");
            item.EmailSubject = dataReader.GetValueText("EmailSubject");
            item.Body = dataReader.GetValueText("Body");
        }

        public Int64 InsertIntoErrorLog(string Remarks)
        {
            Int64 status = 0;

            try
            {
                string storedProcedure = "site_Errorlog_insert";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters

                        commandWrapper.AddInputParameter("@remarks", SqlDbType.VarChar, Remarks);


                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        db.ExecuteNonQuery(commandWrapper);

                        status = commandWrapper.GetValueInt("@errorCode");
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

        public void VendorRemindermailsend(string fromemail, string UserName, string Title, string ResponseDueDates, string lookUpTitle,string CompanyName)
        {
            try
            {
                string LinkUrl = System.Configuration.ConfigurationManager.AppSettings["LinkUrl"];
                //Repository.Base.Code.PMVendorRepository staffDirectoryRepository = new Repository.Base.Code.PMVendorRepository();
                IList<Model.EmailTemplateModel> EmailTemplate = null;
                EmailTemplate = GetAllData(lookUpTitle);
                string body = EmailTemplate[0].Body;
                body = body.Replace("[MemberName]", UserName);
                //body = body.Replace("[VendorRegistrationLink]", LinkUrl + "/Registration/Registration?CompanyKey=" + status);              
                body = body.Replace("[Title]", Title);
                body = body.Replace("[ResponseDueDate]", ResponseDueDates);
                body = body.Replace("[MemberCompanyName]", CompanyName);
                body = body.Replace("[BidName]", Title);
                //body = body.Replace("[Proposal reminder Link]", LinkUrl + "/Registration/Registration");
                body = body.Replace("[Proposal reminder Link]", LinkUrl);

                MailMessage msg = new MailMessage();
                string strBody = string.Empty;

                string Subject = EmailTemplate[0].EmailSubject;
                Subject = Subject.Replace("[CompanyName]", CompanyName);
                Subject = Subject.Replace("[BidName]", Title);

                msg.From = new MailAddress(System.Configuration.ConfigurationManager.AppSettings["senderemail"]);
                msg.To.Add(fromemail);
                msg.Subject = Subject;
                msg.IsBodyHtml = true;              
                msg.Body += body;

                EmailModel emailTemplate = new EmailModel();
                emailTemplate.Body = body;
                emailTemplate.DateAdded = DateTime.Now;
                emailTemplate.Subject = msg.Subject;
                emailTemplate.DateSent = DateTime.Now;
                emailTemplate.EmailStatus = 500;
                emailTemplate.From = System.Configuration.ConfigurationManager.AppSettings["senderemail"];
                emailTemplate.To = fromemail;
                emailTemplate.ObjectKey = 0;
                emailTemplate.ResourceKey = 0;
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

                string Remarks = ex.Message;
                InsertIntoErrorLog(Remarks);

            }
        }

        public void CardExpiryMailsend(string fromemail, string UserName, string CardNumber, string cardExpiryDate, string lookUpTitle)
        {
            try
            {
                //Repository.Base.Code.PMVendorRepository staffDirectoryRepository = new Repository.Base.Code.PMVendorRepository();
                string LinkUrl = System.Configuration.ConfigurationManager.AppSettings["LinkUrl"];
                IList<Model.EmailTemplateModel> EmailTemplate = null;
                EmailTemplate = GetAllData(lookUpTitle);
                string body = EmailTemplate[0].Body;
                body = body.Replace("[MemberName]", UserName);
                body = body.Replace("[LogInLink]", LinkUrl + "/Login/Index");              
                body = body.Replace("[CreditCardExpireDate]", cardExpiryDate);
                body = body.Replace("[CreditCardNo]", CardNumber);
                //body = body.Replace("[MemberCompanyName]", Name);

                MailMessage msg = new MailMessage();
                string strBody = string.Empty;

                msg.From = new MailAddress(System.Configuration.ConfigurationManager.AppSettings["senderemail"]);
                msg.To.Add(fromemail);
                msg.Subject = EmailTemplate[0].EmailSubject;
                msg.IsBodyHtml = true;
              
                msg.Body += body;

                EmailModel emailTemplate = new EmailModel();
                emailTemplate.Body = body;
                emailTemplate.DateAdded = DateTime.Now;
                emailTemplate.Subject = msg.Subject;
                emailTemplate.DateSent = DateTime.Now;
                emailTemplate.EmailStatus = 500;
                emailTemplate.From = System.Configuration.ConfigurationManager.AppSettings["senderemail"];
                emailTemplate.To = fromemail;
                emailTemplate.ObjectKey = 0;
                emailTemplate.ResourceKey = 0;
                emailTemplate.ModuleKey = 301;
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

                string Remarks = ex.Message;
                InsertIntoErrorLog(Remarks);

            }
        }


        public void InsaurenceRenawalMail(string fromemail, string UserName, string PolicyNumber, string RenewalDate, string lookUpTitle)
        {
            try
            {
                //Repository.Base.Code.PMVendorRepository staffDirectoryRepository = new Repository.Base.Code.PMVendorRepository();
                string LinkUrl = System.Configuration.ConfigurationManager.AppSettings["LinkUrl"];
                IList<Model.EmailTemplateModel> EmailTemplate = null;
                EmailTemplate = GetAllData(lookUpTitle);
                string body = EmailTemplate[0].Body;
                body = body.Replace("[MemberName]", UserName);
                body = body.Replace("[InsuranceRenewLink]", LinkUrl + "?isinsurance=1");
                body = body.Replace("[InsuranceRenewallink]", LinkUrl + "?isinsurance=1");
                body = body.Replace("[InsuranceExpireDate]", RenewalDate);
                body = body.Replace("[InsuranceRenewalDate]", RenewalDate);
                body = body.Replace("[PolicyNumber]", PolicyNumber);
                //body = body.Replace("[MemberCompanyName]", Name);

                MailMessage msg = new MailMessage();
                string strBody = string.Empty;

                msg.From = new MailAddress(System.Configuration.ConfigurationManager.AppSettings["senderemail"]);
                msg.To.Add(fromemail);
                msg.Subject = EmailTemplate[0].EmailSubject;
                msg.IsBodyHtml = true;
           
                msg.Body += body;

                EmailModel emailTemplate = new EmailModel();
                emailTemplate.Body = body;
                emailTemplate.DateAdded = DateTime.Now;
                emailTemplate.Subject = msg.Subject;
                emailTemplate.DateSent = DateTime.Now;
                emailTemplate.EmailStatus = 500;
                emailTemplate.From = System.Configuration.ConfigurationManager.AppSettings["senderemail"];
                emailTemplate.To = fromemail;
                emailTemplate.ObjectKey = 0;
                emailTemplate.ResourceKey = 0;
                emailTemplate.ModuleKey = 302;
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

                string Remarks = ex.Message;
                InsertIntoErrorLog(Remarks);

            }
        }


        public void MembershipRenewal(string fromemail, string UserName, string RenewalDate, string lookUpTitle)
        {
            try
            {
                //Repository.Base.Code.PMVendorRepository staffDirectoryRepository = new Repository.Base.Code.PMVendorRepository();
                string LinkUrl = System.Configuration.ConfigurationManager.AppSettings["LinkUrl"];
                IList<Model.EmailTemplateModel> EmailTemplate = null;
                EmailTemplate = GetAllData(lookUpTitle);
                string body = EmailTemplate[0].Body;
                body = body.Replace("[MemberName]", UserName);
                body = body.Replace("[LoginLink]", LinkUrl + "/Login/Index");

                body = body.Replace("[RenewalDate]", RenewalDate);
                //body = body.Replace("[MemberCompanyName]", Name);

                MailMessage msg = new MailMessage();
                string strBody = string.Empty;

                msg.From = new MailAddress(System.Configuration.ConfigurationManager.AppSettings["senderemail"]);
                msg.To.Add(fromemail);
                msg.Subject = EmailTemplate[0].EmailSubject;
                msg.IsBodyHtml = true;
            
                msg.Body += body;

                EmailModel emailTemplate = new EmailModel();
                emailTemplate.Body = body;
                emailTemplate.DateAdded = DateTime.Now;
                emailTemplate.Subject = msg.Subject;
                emailTemplate.DateSent = DateTime.Now;
                emailTemplate.EmailStatus = 500;
                emailTemplate.From = System.Configuration.ConfigurationManager.AppSettings["senderemail"];
                emailTemplate.To = fromemail;
                emailTemplate.ObjectKey = 0;
                emailTemplate.ResourceKey = 0;
                emailTemplate.ModuleKey = 300;
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

                string Remarks = ex.Message;
                InsertIntoErrorLog(Remarks);

            }
        }

        public void InsaurenceExpiredMail(string fromemail, string UserName, string PolicyNumber, string ExpireDate, string lookUpTitle)
        {
            try
            {
                //Repository.Base.Code.PMVendorRepository staffDirectoryRepository = new Repository.Base.Code.PMVendorRepository();
                IList<Model.EmailTemplateModel> EmailTemplate = null;
                EmailTemplate = GetAllData(lookUpTitle);
                string body = EmailTemplate[0].Body;
                body = body.Replace("[MemberName]", UserName);
                //body = body.Replace("[VendorRegistrationLink]", LinkUrl + "/Registration/Registration?CompanyKey=" + status);              
                body = body.Replace("[InsuranceExpireDate]", ExpireDate);
                body = body.Replace("[PolicyNumber]", PolicyNumber);
                //body = body.Replace("[MemberCompanyName]", Name);

                MailMessage msg = new MailMessage();
                string strBody = string.Empty;

                msg.From = new MailAddress(System.Configuration.ConfigurationManager.AppSettings["senderemail"]);
                msg.To.Add(fromemail);
                msg.Subject = EmailTemplate[0].EmailSubject;
                msg.IsBodyHtml = true;
               
                msg.Body += body;
                EmailModel emailTemplate = new EmailModel();
                emailTemplate.Body = body;
                emailTemplate.DateAdded = DateTime.Now;
                emailTemplate.Subject = msg.Subject;
                emailTemplate.DateSent = DateTime.Now;
                emailTemplate.EmailStatus = 500;
                emailTemplate.From = System.Configuration.ConfigurationManager.AppSettings["senderemail"];
                emailTemplate.To = fromemail;
                emailTemplate.ObjectKey = 0;
                emailTemplate.ResourceKey = 0;
                emailTemplate.ModuleKey = 302;
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

                string Remarks = ex.Message;
                InsertIntoErrorLog(Remarks);

            }
        }


        public bool WinFeeMain(string fromemail, string UserName, int Amount, string Title, string lookUpTitle)
        {
            bool status = false;
            try
            {
                if (Title != "Membership Fee") {
                    lookUpTitle = "Bid Fine";
                }
                else 
                {
                    lookUpTitle = "MembershipFees";
                }

                //Repository.Base.Code.PMVendorRepository staffDirectoryRepository = new Repository.Base.Code.PMVendorRepository();
                IList<Model.EmailTemplateModel> EmailTemplate = null;
                EmailTemplate = GetAllData(lookUpTitle);
                string body = EmailTemplate[0].Body;
                body = body.Replace("[MemberName]", UserName);
                //body = body.Replace("[VendorRegistrationLink]", LinkUrl + "/Registration/Registration?CompanyKey=" + status);              
                body = body.Replace("[ChargeAmount]", Amount.ToString());
                body = body.Replace("[Title]", Title);
                //body = body.Replace("[MemberCompanyName]", Name);

                MailMessage msg = new MailMessage();
                string strBody = string.Empty;

                msg.From = new MailAddress(System.Configuration.ConfigurationManager.AppSettings["senderemail"]);
                msg.To.Add(fromemail);
                msg.Subject = EmailTemplate[0].EmailSubject;
                msg.IsBodyHtml = true;
               
                msg.Body += body;
                int companyKey = Convert.ToInt32(HttpContext.Current.Session["TempCompanyKey"] == null ? 0 : HttpContext.Current.Session["TempCompanyKey"]);
                int ResourceKey = Convert.ToInt32(HttpContext.Current.Session["resourceid"] == null ? 0 : HttpContext.Current.Session["resourceid"]);
                EmailModel emailTemplate = new EmailModel();
                emailTemplate.Body = body;
                emailTemplate.DateAdded = DateTime.Now;
                emailTemplate.Subject = msg.Subject;
                emailTemplate.DateSent = DateTime.Now;
                emailTemplate.EmailStatus = 500;
                emailTemplate.From = System.Configuration.ConfigurationManager.AppSettings["senderemail"];
                emailTemplate.To = fromemail;
                emailTemplate.ObjectKey = companyKey;
                emailTemplate.ResourceKey = ResourceKey;
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

                string Remarks = ex.Message;
                InsertIntoErrorLog(Remarks);

            }
            HttpContext.Current.Session.Remove("TempCompanyKey");
            return status;
        }

        public bool Invitemail(string fromemail, string Title, string PropertyTitle, string VendorName, string CompanyName, string BidDueDate, string lookUpTitle)
        {
            bool status = false;
            try
            {
                //Repository.Base.Code.PMVendorRepository staffDirectoryRepository = new Repository.Base.Code.PMVendorRepository();
                IList<Model.EmailTemplateModel> EmailTemplate = null;
                EmailTemplate = GetAllData(lookUpTitle);
                string body = EmailTemplate[0].Body;
                body = body.Replace("[MemberName]", VendorName);
                //body = body.Replace("[VendorRegistrationLink]", LinkUrl + "/Registration/Registration?CompanyKey=" + status);              
               
                body = body.Replace("[Title]", Title);
                body = body.Replace("[Property]", PropertyTitle);
                body = body.Replace("[BidDueDate] ", BidDueDate);   
                body = body.Replace("[CompanyName]", CompanyName);
                body = body.Replace("[VendorName]", VendorName);
                //body = body.Replace("[MemberCompanyName]", Name);

                MailMessage msg = new MailMessage();
                string strBody = string.Empty;

                msg.From = new MailAddress(System.Configuration.ConfigurationManager.AppSettings["senderemail"]);
                msg.To.Add(fromemail);
                msg.Subject = EmailTemplate[0].EmailSubject;
                msg.IsBodyHtml = true;
             
                msg.Body += body;
                EmailModel emailTemplate = new EmailModel();
                emailTemplate.Body = body;
                emailTemplate.DateAdded = DateTime.Now;
                emailTemplate.Subject = msg.Subject;
                emailTemplate.DateSent = DateTime.Now;
                emailTemplate.EmailStatus = 500;
                emailTemplate.From = System.Configuration.ConfigurationManager.AppSettings["senderemail"];
                emailTemplate.To = fromemail;
                emailTemplate.ObjectKey = 0;
                emailTemplate.ResourceKey = 0;
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

                string Remarks = ex.Message;
                InsertIntoErrorLog(Remarks);

            }
            
            return status;
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
                var ins = dataReader.GetValueDecimal("InsuranceAmount");
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
    }
}