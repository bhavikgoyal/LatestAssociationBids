using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base;
using DB_con;

namespace AssociationBids.Portal.Repository.Base
{
    public class BidReportRepository : BaseRepository, IBidReportRepository
    {
        ConnectionCls obj_con = null;
        int CompKey = 0;
        public BidReportRepository()
        {
            obj_con = new ConnectionCls();
        }

        public BidReportRepository(string connectionString)
            : base(connectionString) { }
        public List<BidRequestModel> SearchBidRequest(long PageSize, long PageIndex, string Search, string Sort, Int32 Resourcekey, Int32 PropertyKey, string BidRequestStatus, int Modulekey,DateTime FromDate, DateTime ToDate)
        {
            List<BidRequestModel> itemList = new List<BidRequestModel>();
            try
            {
                int BidRequestStatus1 = 0;
                if (BidRequestStatus == "600, 601") { BidRequestStatus1 = 1; }
                else if (BidRequestStatus == "602, 603") { BidRequestStatus1 = 2; }
                else if (BidRequestStatus == "0,0") { BidRequestStatus1 = 0; }
                //int BidRequestStatus1 = Convert.ToInt32(BidRequestStatus.Split(',')[0]);
                //int BidRequestStatus2 = Convert.ToInt32(BidRequestStatus.Split(',')[1]);
                string storedProcedure = "site_BidRequest_SelectIndexPagingBidReport";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters 
                        commandWrapper.AddInputParameter("@PageSize", SqlDbType.BigInt, (PageSize == 0) ? 0 : PageSize);
                        commandWrapper.AddInputParameter("@PageIndex", SqlDbType.BigInt, (PageIndex == 0) ? 0 : PageIndex);
                        commandWrapper.AddInputParameter("@Search", SqlDbType.NText, (Search == "") ? "" : Search);
                        commandWrapper.AddInputParameter("@Sort", SqlDbType.NText, (Sort == "") ? "" : Sort);
                        commandWrapper.AddInputParameter("@Resourcekey", SqlDbType.Int, (Resourcekey == 0) ? 0 : Resourcekey);
                        commandWrapper.AddInputParameter("@PropertyKey", SqlDbType.Int, (PropertyKey == 0) ? 0 : PropertyKey);
                        commandWrapper.AddInputParameter("@Modulekey", SqlDbType.Int, Modulekey);
                        commandWrapper.AddInputParameter("@BidRequestStatus", SqlDbType.Int, (BidRequestStatus1 == 0) ? 0 : BidRequestStatus1);
                        if (FromDate.ToString() == "1/1/0001 12:00:00 AM")
                        {
                            commandWrapper.AddInputParameter("@StartDate", SqlDbType.NText, "");
                        }
                        else 
                        {
                            commandWrapper.AddInputParameter("@StartDate", SqlDbType.NText, FromDate);
                        }

                        if (ToDate.ToString() == "1/1/0001 12:00:00 AM")
                        {
                            commandWrapper.AddInputParameter("@EndDate", SqlDbType.NText, "");
                        }
                        else
                        {
                            DateTime date2 = ToDate.AddDays(1);
                            commandWrapper.AddInputParameter("@EndDate", SqlDbType.NText, date2);
                        }
                        
                        //commandWrapper.AddInputParameter("@BidRequestStatus1", SqlDbType.Int, (BidRequestStatus2 == 0) ? 0 : BidRequestStatus2);
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            BidRequestModel item = null;
                            while (dataReader.Read())
                            {
                                item = new BidRequestModel();
                                LoadStaff(dataReader, item);
                                if (dataReader.GetValueText("BidRequestStatus") == "In Progress")
                                {

                                }
                                else
                                {
                                    itemList.Add(item);

                                }
                               
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

        public List<BidRequestModel> SearchBidRequestPriority(long PageSize, long PageIndex, string Search, string Sort, Int32 Resourcekey, Int32 PropertyKey, string BidRequestStatus, int Modulekey)
        {
            List<BidRequestModel> itemList = new List<BidRequestModel>();
            try
            {
                int BidRequestStatus1 = 0;
                if (BidRequestStatus == "600, 601") { BidRequestStatus1 = 1; }
                else if (BidRequestStatus == "602, 603") { BidRequestStatus1 = 2; }
                else if (BidRequestStatus == "0,0") { BidRequestStatus1 = 0; }
                //int BidRequestStatus1 = Convert.ToInt32(BidRequestStatus.Split(',')[0]);
                //int BidRequestStatus2 = Convert.ToInt32(BidRequestStatus.Split(',')[1]);
                string storedProcedure = "site_BidRequest_SelectIndexPagingBidReportPriority";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters 
                        commandWrapper.AddInputParameter("@PageSize", SqlDbType.BigInt, (PageSize == 0) ? 0 : PageSize);
                        commandWrapper.AddInputParameter("@PageIndex", SqlDbType.BigInt, (PageIndex == 0) ? 0 : PageIndex);
                        commandWrapper.AddInputParameter("@Search", SqlDbType.NText, (Search == "") ? "" : Search);
                        commandWrapper.AddInputParameter("@Sort", SqlDbType.NText, (Sort == "") ? "" : Sort);
                        commandWrapper.AddInputParameter("@Resourcekey", SqlDbType.Int, (Resourcekey == 0) ? 0 : Resourcekey);
                        commandWrapper.AddInputParameter("@PropertyKey", SqlDbType.Int, (PropertyKey == 0) ? 0 : PropertyKey);
                        commandWrapper.AddInputParameter("@Modulekey", SqlDbType.Int, Modulekey);
                        commandWrapper.AddInputParameter("@BidRequestStatus", SqlDbType.Int, (BidRequestStatus1 == 0) ? 0 : BidRequestStatus1);
                        //commandWrapper.AddInputParameter("@BidRequestStatus1", SqlDbType.Int, (BidRequestStatus2 == 0) ? 0 : BidRequestStatus2);
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            BidRequestModel item = null;
                            while (dataReader.Read())
                            {
                                item = new BidRequestModel();
                               
                                LoadStaff(dataReader, item);

                                if (dataReader.GetValueText("BidRequestStatus") == "In Progress")
                                {

                                }
                                else
                                {
                                    itemList.Add(item);
                                }
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
        public string getVendorDate(int BidVendorKey)
        {
            string itemlist = "";

            try
            {
                string storedProcedure = "";
              
                    storedProcedure = "site_BidRequest_GetVVDate";
               
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        commandWrapper.AddInputParameter("@BidVendorKey", SqlDbType.Int, (BidVendorKey == 0) ? 0 : BidVendorKey);
                      

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                           
                            while (dataReader.Read())
                            {
                                var ins = dataReader.GetValueText("Lastdate");
                                if (ins == null || ins == "" || ins == "01/01/1900")
                                {
                                    itemlist = "";
                                }
                                else
                                {
                                    itemlist = dataReader.GetValueText("Lastdate");
                                }

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
            }
            return itemlist;
        }



        public List<BidRequestModel> SearchVendorByBidRequest(int BidRequestKey, long modulekey)
        {
            List<BidRequestModel> itemList = new List<BidRequestModel>();
            try
            {
                string storedProcedure = "";
                if (modulekey == 106)
                {
                   storedProcedure = "site_BidRequest_GetVendorsForWorkReport";
                }
                else
                {
                    storedProcedure = "site_BidRequest_GetVendorsForBidReport";
                }
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        commandWrapper.AddInputParameter("@BidRequestkey", SqlDbType.Int, (BidRequestKey == 0) ? 0 : BidRequestKey);
                        commandWrapper.AddInputParameter("@Modulekey", SqlDbType.Int, modulekey);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            BidRequestModel item = null;
                            while (dataReader.Read())
                            {
                                item = new BidRequestModel();
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

        public List<BidRequestModel> SearchVendorByWorkReport(int BidRequestKey, long modulekey,long ResourceKey,long CompanyKey,string BidRequestStatus,int PropertyKey, DateTime FromDate, DateTime ToDate)
        {
            List<BidRequestModel> itemList = new List<BidRequestModel>();
            try
            {
                int BidRequestStatus1 = 0;
                if (BidRequestStatus == "600, 601") { BidRequestStatus1 = 1; }
                else if (BidRequestStatus == "602, 603") { BidRequestStatus1 = 2; }
                else if (BidRequestStatus == "0,0") { BidRequestStatus1 = 0; }
                string storedProcedure = "";
                
                storedProcedure = "site_BidRequest_GetVendorsForWorkReport";
                
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        commandWrapper.AddInputParameter("@BidRequestkey", SqlDbType.Int, (BidRequestKey == 0) ? 0 : BidRequestKey);
                        commandWrapper.AddInputParameter("@Modulekey", SqlDbType.Int, modulekey);
                        commandWrapper.AddInputParameter("@PropertyKey", SqlDbType.Int, (PropertyKey == 0) ? 0 : PropertyKey);
                        commandWrapper.AddInputParameter("@BidRequestStatus", SqlDbType.Int, (BidRequestStatus1 == 0) ? 0 : BidRequestStatus1);
                        commandWrapper.AddInputParameter("@ResourceKey", SqlDbType.Int, (ResourceKey == 0) ? 0 : ResourceKey);
                        commandWrapper.AddInputParameter("@CompanyKey", SqlDbType.Int, (CompanyKey == 0) ? 0 : CompanyKey);
                        if (FromDate.ToString() == "1/1/0001 12:00:00 AM")
                        {
                            commandWrapper.AddInputParameter("@StartDate", SqlDbType.NText, "");
                        }
                        else
                        {
                            commandWrapper.AddInputParameter("@StartDate", SqlDbType.NText, FromDate);
                        }

                        if (ToDate.ToString() == "1/1/0001 12:00:00 AM")
                        {
                            commandWrapper.AddInputParameter("@EndDate", SqlDbType.NText, "");
                        }
                        else
                        {
                            DateTime date2 = ToDate.AddDays(1);
                            commandWrapper.AddInputParameter("@EndDate", SqlDbType.NText, date2);
                        }

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            BidRequestModel item = null;
                            while (dataReader.Read())
                            {
                                item = new BidRequestModel();
                                LoadVendorByWorkReport(dataReader, item);

                                if (dataReader.GetValueText("BidRequeststatus") == "In Progress")
                                {

                                }
                                else
                                {
                                    itemList.Add(item);

                                }
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

        public List<BidRequestModel> SearchVendorByPMWorkOrder(int BidRequestKey, int modulekey, long ResourceKey, string BidRequestStatus, int PropertyKey, DateTime FromDate, DateTime ToDate)
        {
            List<BidRequestModel> itemList = new List<BidRequestModel>();
            try
            {
                int BidRequestStatus1 = 0;
                if (BidRequestStatus == "600, 601") { BidRequestStatus1 = 1; }
                else if (BidRequestStatus == "602, 603") { BidRequestStatus1 = 2; }
                else if (BidRequestStatus == "0,0") { BidRequestStatus1 = 0; }
                //string storedProcedure = "";


                //string storedProcedure = "site_BidRequest_GetVendorsForBidRequestByAlgorithm_Copy";
                string storedProcedure = "site_BidRequest_GetVendorsForBidRequestByAlgorithm_Copy";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        commandWrapper.AddInputParameter("@BidRequestkey", SqlDbType.Int, (BidRequestKey == 0) ? 0 : BidRequestKey);
                        commandWrapper.AddInputParameter("@ResourceKey", SqlDbType.Int, ResourceKey);
                        commandWrapper.AddInputParameter("@Modulekey", SqlDbType.Int, modulekey);
                        commandWrapper.AddInputParameter("@PropertyKey", SqlDbType.Int, (PropertyKey == 0) ? 0 : PropertyKey);
                        commandWrapper.AddInputParameter("@BidRequestStatus", SqlDbType.Int, (BidRequestStatus1 == 0) ? 0 : BidRequestStatus1);

                        if (FromDate.ToString() == "1/1/0001 12:00:00 AM")
                        {
                            commandWrapper.AddInputParameter("@StartDate", SqlDbType.NText, "");
                        }
                        else
                        {
                            commandWrapper.AddInputParameter("@StartDate", SqlDbType.NText, FromDate);
                        }

                        if (ToDate.ToString() == "1/1/0001 12:00:00 AM")
                        {
                            commandWrapper.AddInputParameter("@EndDate", SqlDbType.NText, "");
                        }
                        else
                        {
                            commandWrapper.AddInputParameter("@EndDate", SqlDbType.NText, ToDate);
                        }
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            BidRequestModel item = null;
                            while (dataReader.Read())
                            {
                                item = new BidRequestModel();
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

        protected void LoadStaff(DBDataReader dataReader, BidRequestModel item)
        {

            item.BidRequestKey = dataReader.GetValueInt("BidRequestKey");
            item.Title = dataReader.GetValueText("Title");
            item.StartDates = dataReader.GetValueText("StartDate");
            item.BidDueDates = dataReader.GetValueText("BidDueDate");
            item.NoofBids = dataReader.GetValueInt("NoofBids");
            item.BidReqStatus = dataReader.GetValueText("BidRequestStatus");
            item.TotalRecords = dataReader.GetValueInt("TotalRecord");
            item.Property = dataReader.GetValueText("PropertyName");
            item.NewMessageCount = dataReader.GetValueInt("NewMsg");
            

            try
            {
                item.priority = dataReader.GetValueInt("priority");
                item.NotificationId = dataReader.GetValueText("NotificationId");
                item.NotificationType = dataReader.GetValueText("NotificationType");
            }
            catch { }

        }

        protected void LoadDates(DBDataReader dataReader, BidRequestModel item)
        {
            var ins = dataReader.GetValueText("LastModificationTime");
            if (ins != null && ins != "")
            {
                item.VendorSSdates = "";
            }
            else
            {
                item.VendorSSdates = dataReader.GetValueText("LastModificationTime");
            }

           
           
        }



        protected void LoadVendorByBidRequest(DBDataReader dataReader, BidRequestModel item)
        {
            int bidrequest = item.BidRequestKey;
            item.Name = dataReader.GetValueText("Name");
            item.ContactPerson = dataReader.GetValueText("ContactPerson");
            item.Service = dataReader.GetValueText("Service");
            item.CompanyKey = dataReader.GetValueInt("CompanyKey");
            item.Email = dataReader.GetValueText("Email");
            //item.ResponseDueDates = dataReader.GetValueText("RespondByDate");
            item.VendorStartdates = dataReader.GetValueText("VendorStartdate");
            item.ResponseDueDates = dataReader.GetValueText("DefaultRespondByDate");
            item.ResponseDueDatess = dataReader.GetValueText("DefaultRespondByDatess");
            item.DefaultResponseDueDates = dataReader.GetValueText("DefaultRespondByDates");
            item.Descrip = dataReader.GetValueText("Descrip");
            item.Description = dataReader.GetValueText("BidDescription");
            item.BidAmount = dataReader.GetValueDecimal("BidAmount");
            //item.BidAmounts = Convert.ToString(string.Format("{0:0.00}", item.BidAmount));
            item.BidAmounts = Common.Utility.FormatNumberHelper(item.BidAmount, true);
            item.BidAmountT = Common.Utility.FormatNumberHelper(Decimal.Round(item.BidAmount),true);

            item.InsuranceAmount = Convert.ToDouble(dataReader.GetValueText("Insurance"));
            item.InsuranceAmounts = Common.Utility.FormatNumberHelper(item.InsuranceAmount, true);
            item.InsuranceAmountT = Common.Utility.FormatNumberHelper(Decimal.Round(Convert.ToDecimal(item.InsuranceAmount)),true);

            item.BidReqStatus = dataReader.GetValueText("BidVendorStatus");
            item.BidVendorStatus = dataReader.GetValueText("BidRequeststatus");
            item.VendorKey = dataReader.GetValueInt("VendorKey");
            item.BidVendorKey = dataReader.GetValueInt("BidVendorKey");
            item.TotalApceptRecord = dataReader.GetValueText("TotalApceptRecord");
            item.IsAssigned = dataReader.GetValueBool("IsAssigned");
            item.BidName = dataReader.GetValueText("BidName");
            item.BidDueDates = dataReader.GetValueText("BidDueDate");
            item.Property = dataReader.GetValueText("PropertyName");
            try
            {
                item.priority = dataReader.GetValueInt("priority");
                item.NotificationId = dataReader.GetValueText("NotificationId");
                item.NotificationType = dataReader.GetValueText("NotificationType");
            }
            catch { }
        }

        protected void LoadVendorByWorkReport(DBDataReader dataReader, BidRequestModel item)
        {
            int bidrequest = item.BidRequestKey;
            item.Name = dataReader.GetValueText("name");
            item.ContactPerson = dataReader.GetValueText("ContactPerson");
            ////item.Service = dataReader.GetValueText("Service");
            item.BidRequestKey = dataReader.GetValueInt("BidRequestKey");
            //item.Email = dataReader.GetValueText("Email");
            ////item.ResponseDueDates = dataReader.GetValueText("RespondByDate");
            item.VendorStartdates = dataReader.GetValueText("VendorStartdate");
            var ins = dataReader.GetValueText("Insurance");
            if (ins != null && ins != "")
            {
                item.InsuranceAmount = Convert.ToDouble(dataReader.GetValueText("Insurance"));
            }
        

            ////item.VendorStartdates = dataReader.GetValueText("VendorStartdate");
            item.ResponseDueDates = dataReader.GetValueText("DefaultRespondByDate");
            item.ResponseDueDatess = dataReader.GetValueText("DefaultRespondByDatess");
            item.DefaultResponseDueDates = dataReader.GetValueText("DefaultRespondByDates");
            item.Description = dataReader.GetValueText("bidDescription");
            
            ////sitem.DefaultResponseDueDates = dataReader.GetValueText("DefaultRespondByDate");
            item.Descrip = dataReader.GetValueText("Descrip");
            item.BidAmount = dataReader.GetValueDecimal("BidAmount");
            ////item.BidAmounts = Convert.ToString(string.Format("{0:0.00}", item.BidAmount));
            item.BidAmounts = Common.Utility.FormatNumberHelper(item.BidAmount, true);
            item.BidAmountT = Common.Utility.FormatNumberHelper(Decimal.Round(item.BidAmount), true);

            item.InsuranceAmounts = Common.Utility.FormatNumberHelper(item.InsuranceAmount, true);
            item.InsuranceAmountT = Common.Utility.FormatNumberHelper(Decimal.Round(Convert.ToDecimal(item.InsuranceAmount)), true);
            item.BidReqStatus = dataReader.GetValueText("BidVendorStatus");
            ////item.BidReqStatus = dataReader.GetValueText("BidRequeststatus");
            item.BidVendorStatus = dataReader.GetValueText("BidRequeststatus");
            ////item.BidVendorStatus = dataReader.GetValueText("BidVendorStatus");
            item.VendorKey = dataReader.GetValueInt("VendorKey");
            item.BidVendorKey = dataReader.GetValueInt("BidVendorKey");
            //item.TotalApceptRecord = dataReader.GetValueText("TotalApceptRecord");
            item.IsAssigned = dataReader.GetValueBool("IsAssigned");
            item.BidName = dataReader.GetValueText("BidName");
            item.BidDueDates = dataReader.GetValueText("BidDueDate");
            item.Property = dataReader.GetValueText("PropertyName");
            item.PropertyKey = dataReader.GetValueText("PropertyKey");
            try
            {
                //item.priority = dataReader.GetValueInt("priority");
                //item.NotificationId = dataReader.GetValueText("NotificationId");
                //item.NotificationType = dataReader.GetValueText("NotificationType");
            }
            catch { }
        }

        protected void NotesByWorkReport(DBDataReader dataReader, NoteModel item)
        {
            item.NoteKey = dataReader.GetValueInt("NoteKey");
            item.ModuleKey = dataReader.GetValueInt("ModuleKey");
            item.Description = dataReader.GetValueText("Description");
        }

        public List<BidRequestModel> EmailReport(string FilePath, long ResourceKey,string ReportTypeName)
        {
            List<BidRequestModel> itemList = new List<BidRequestModel>();
            try
            {
                mailsend(FilePath, ReportTypeName, Convert.ToInt32(ResourceKey));
                //mailsendAsync(FilePath, ReportTypeName);
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
            }
            return itemList;
        }

        public void mailsendAsync(string FilePath,string ReportTypeName,int Resourcekey,string EmailId,string FirstName)
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
                attachment = new Attachment(FilePath);
                msg.Attachments.Add(attachment);

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

        public virtual RegistrationModel mailsend(string FilePath,string ReportTypeName,int ResourceKey)
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
                        //commandWrapper.AddInputParameter("@CompanyKey", SqlDbType.Int, companykey);
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
                for (int i=0; i<items.Count();i++) {
                    if (items[i].Resourcekey == ResourceKey)
                    {
                        mailsendAsync(FilePath, ReportTypeName, items[i].Resourcekey, items[i].EmailId,items[i].FirstName);
                    }
                }
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
            }

            return item;
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

        protected void Load(DBDataReader dataReader, EmailTemplateModel item)
        {
            item.EmailTemplateKey = dataReader.GetValueInt("EmailTemplateKey");
            item.EmailTitle = dataReader.GetValueText("EmailTitle");
            item.Title = dataReader.GetValueText("Title");
            item.EmailSubject = dataReader.GetValueText("EmailSubject");
            item.Body = dataReader.GetValueText("Body");
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

        public virtual bool Insert(ReportEmailModel item)
        {
            bool status = false;

            try
            {
                //item.BidRequestStatus = 1;
                string storedProcedure = "site_ReportEmail_Insert";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        
                        // add the stored procedure input parameters
                        //commandWrapper.AddInputParameter("@RequestedBy", SqlDbType.Int, item.ResourceKey);
                        commandWrapper.AddInputParameter("@ResourceKey", SqlDbType.Int, item.ResourceKey);
                        commandWrapper.AddInputParameter("@IsDetailedReport", SqlDbType.Bit, item.IsDetailedReport);
                        commandWrapper.AddInputParameter("@DocumentName", SqlDbType.VarChar, item.DocumentName);
                        commandWrapper.AddInputParameter("@IncludeCOI", SqlDbType.Bit, item.IncludeCOI);
                        commandWrapper.AddInputParameter("@IsSent", SqlDbType.Bit, item.IsSent);
                        commandWrapper.AddInputParameter("@VendorList", SqlDbType.VarChar, item.VendorList);
                        // add stored procedure output parameters
                        //commandWrapper.AddOutputParameter("@bidRequestKey", SqlDbType.Int);
                        //commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        db.ExecuteNonQuery(commandWrapper);

                        // save the newly inserted key value
                        //item.BidRequestKey = commandWrapper.GetValueInt("@bidRequestKey");

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

        public List<NoteModel> NotesByWorkReport(int BidRequestKey, int ModuleKey)
        {
            List<NoteModel> itemList = new List<NoteModel>();
            try
            {
                string storedProcedure = "";

                storedProcedure = "site_Note_NoteForBidWorkReport";

                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        commandWrapper.AddInputParameter("@BidRequestkey", SqlDbType.Int, (BidRequestKey == 0) ? 0 : BidRequestKey);
                        commandWrapper.AddInputParameter("@ModuleKey", SqlDbType.Int, ModuleKey);
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            NoteModel item = null;
                            while (dataReader.Read())
                            {
                                item = new NoteModel();
                                NotesByWorkReport(dataReader, item);
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
    }
}
