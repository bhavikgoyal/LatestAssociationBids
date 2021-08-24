using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base.Interface;

using System.Data;
using System.Net.Mail;
using System.Net;
using AssociationBids.GlobalUtilities;
using DB_con;

namespace AssociationBids.Portal.Repository.Base.Code
{
    public class VendorInvoiceRepositery : BaseRepository, IVendorInvoiceRepositery
    {
        ConnectionCls obj_con = null;
        public VendorInvoiceRepositery()
        {
            obj_con = new ConnectionCls();
        }



        public VendorInvoiceRepositery(string connectionString)
          : base(connectionString) { }


        protected void LoadInvoicedata(DBDataReader dataReader, VendorInvoiceModal item)

        {

            item.VendorKey = dataReader.GetValueInt("Vendorkey");
            item.Date = dataReader.GetValueText("Date");
            item.Amt = dataReader.GetValueText("amount");
            item.BAl = dataReader.GetValueText("balance");
            item.RefrenceNumber = dataReader.GetValueText("ReferenceNumber");
            item.InvoiceNo = dataReader.GetValueText("ReferenceNumber");
            item.TotalRecords = dataReader.GetValueText("TotalRecords");
            item.InvoiceKey = dataReader.GetValueInt("InvoiceKey");
            item.RefundRequestKey = dataReader.GetValueInt("refundRequestKey");
            item.Description = dataReader.GetValueText("Description");
            item.Status = dataReader.GetValueBool("MarkAsRefund");

        }
        protected void LoadRefundData(DBDataReader dataReader, VendorInvoiceModal item)

        {

            item.VendorKey = dataReader.GetValueInt("Vendorkey");
            item.Date = dataReader.GetValueText("Date");
            item.Amt = dataReader.GetValueText("amount");
            item.Stripe_tokenID = dataReader.GetValueText("Stripe_TokenID");
            item.RefrenceNumber = dataReader.GetValueText("ReferenceNumber");
            item.InvoiceNo = dataReader.GetValueText("ReferenceNumber");
            item.TotalRecords = dataReader.GetValueText("TotalRecords");
            item.InvoiceKey = dataReader.GetValueInt("InvoiceKey");
            item.Reason = dataReader.GetValueText("Reason");
            item.vendorname = dataReader.GetValueText("vendorname");
            item.Status = dataReader.GetValueBool("MarkAsRefund");
             item.Description = dataReader.GetValueText("Description");
            try
            {
                item.priority = dataReader.GetValueInt("priority");
                item.NotificationId = dataReader.GetValueText("NotificationId");
                item.NotificationType = dataReader.GetValueText("NotificationType");
            }
            catch { }

        }


        protected void LoadViewEdit(DBDataReader dataReader, VendorInvoiceModal item)

        {

            item.Title = dataReader.GetValueText("Title");
            item.InvoiceDate = dataReader.GetValueText("invoiceDate");
            item.DueDate = dataReader.GetValueText("bidDueDate");
            item.Amt = dataReader.GetValueText("Amount");
            item.BAl = dataReader.GetValueText("Balance");
            item.InvoiceNo = dataReader.GetValueText("RferenceNumber");
            item.State = dataReader.GetValueText("state");
            item.City = dataReader.GetValueText("city");
            item.Address1 = dataReader.GetValueText("Address");
            item.Address2 = dataReader.GetValueText("Address2");
            item.PostalCode = dataReader.GetValueText("Zip");
            item.Email = dataReader.GetValueText("Email");
            item.Stripe_tokenID = dataReader.GetValueText("Strip_TokenID");
            item.VendorKey = dataReader.GetValueInt("vendorKey");
            item.InvoiceKey = dataReader.GetValueInt("invoicekey");
            item.TransDate = dataReader.GetValueDateTime("TransactionDate");
            item.RefrenceNumber = dataReader.GetValueText("RferenceNumber");
            item.Status = dataReader.GetValueBool("MarkAsRefund");
            item.RefundRequestKey = dataReader.GetValueInt("RefundRequestKey");
            item.Memberfeekey = dataReader.GetValueInt("memberfeekey");
            item.CompanyName = dataReader.GetValueText("CompanyName");
            item.ResourceKey = dataReader.GetValueInt("ResourceKey");
            item.RefundAmount = dataReader.GetValueText("RefundAmount");
            item.MarkasRefund = dataReader.GetValueBool("MarkAsRefund") == true ? 1 : 0;
        }


        protected void loadForCheck(DBDataReader dataReader, VendorInvoiceModal item)

        {


            item.InvoiceKey = dataReader.GetValueInt("invoicekey");
            item.RefundRequestKey = dataReader.GetValueInt("RefundRequestKey");


        }



        public bool UpdateRefundRequest(int InvoiceKey, string Reason, string StripeTokenID)
        {
            bool status = false;

            try
            {


                string storedProcedure = "Site_RefundRequest_RefundPayment";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters

                        commandWrapper.AddInputParameter("@Reason", SqlDbType.VarChar, Reason);
                        commandWrapper.AddInputParameter("@StripeTokenID", SqlDbType.VarChar, StripeTokenID);
                        commandWrapper.AddInputParameter("@InvoiceKey", SqlDbType.Int, InvoiceKey);

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

            }

            return status;

        }

        public virtual VendorInvoiceModal CheckRefundRequest(int Vendorkey, int InvoiceKey)
        {
            VendorInvoiceModal item = null;

            try
            {
                string storedProcedure = "Site_check_RefundRequest";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@Vendorkey", SqlDbType.Int, Vendorkey);
                        commandWrapper.AddInputParameter("@InvoiceKey", SqlDbType.Int, InvoiceKey);
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            while (dataReader.Read())
                            {
                                item = new VendorInvoiceModal();

                                loadForCheck(dataReader, item);



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




        public bool InsertRefundRequest(int Vendorkey, int InvoiceKey, string RefrenceNumber, DateTime date, string Amount, string Reason, string Strip_TokenID, int ResourceKey)
        {
            bool status = false;

            try
            {

                string storedProcedure = "Site_RefundRequest_Insert";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters

                        commandWrapper.AddInputParameter("@Vendorkey", SqlDbType.Int, Vendorkey);
                        commandWrapper.AddInputParameter("@InvoiceKey", SqlDbType.Int, InvoiceKey);
                        commandWrapper.AddInputParameter("@ResourceKey", SqlDbType.Int, ResourceKey);
                        commandWrapper.AddInputParameter("@date", SqlDbType.DateTime, date);
                        commandWrapper.AddInputParameter("@Amount", SqlDbType.VarChar, Amount);
                        commandWrapper.AddInputParameter("@RefrenceNumber", SqlDbType.VarChar, RefrenceNumber);
                        commandWrapper.AddInputParameter("@Reason", SqlDbType.VarChar, Reason);
                        commandWrapper.AddInputParameter("@StripeTokenID", SqlDbType.VarChar, Strip_TokenID);
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

            }

            return status;

        }
        protected void InsauranceRenewalDetails(DBDataReader dataReader, VendorInvoiceModal item)
        {



            item.ContactPerson = dataReader.GetValueText("ConatctPerson");
            item.Email = dataReader.GetValueText("Email");

        }




        public IList<VendorInvoiceModal> RefundMailToAdmin(string lookUpTitle, string Title, string Amount,string VendorName)
        {
            List<VendorInvoiceModal> itemList = new List<VendorInvoiceModal>();

            try
            {
                string storedProcedure = "site_GetallAdministrator_RefundMail";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {



                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            VendorInvoiceModal item = null;
                            while (dataReader.Read())
                            {
                                item = new VendorInvoiceModal();
                                InsauranceRenewalDetails(dataReader, item);
                                itemList.Add(item);
                                MailToRefundAdmin(item.Email, item.ContactPerson, Title, Amount, lookUpTitle, VendorName);
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

        public IList<VendorInvoiceModal> RefundMailToVendor(string lookUpTitle, string Title, string Amount,int ResourceKey)
        {
            List<VendorInvoiceModal> itemList = new List<VendorInvoiceModal>();

            try
            {
                string storedProcedure = "site_GetallVendor_RefundMail";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        commandWrapper.AddInputParameter("@ResourceKey", SqlDbType.Int, ResourceKey);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            VendorInvoiceModal item = null;
                            while (dataReader.Read())
                            {
                                item = new VendorInvoiceModal();
                                InsauranceRenewalDetails(dataReader, item);
                                itemList.Add(item);
                                InsaurenceRenawalMail(item.Email, item.ContactPerson, Title, Amount, lookUpTitle, "");
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
      
        public void InsaurenceRenawalMail(string fromemail, string UserName, string Title, string Amount, string lookUpTitle,string VendorName)
        {
            try
            {
                //Repository.Base.Code.PMVendorRepository staffDirectoryRepository = new Repository.Base.Code.PMVendorRepository();
                IList<Model.EmailTemplateModel> EmailTemplate = null;

                if (lookUpTitle == "Refund Process")
                {
                    lookUpTitle = "Refund Processed";
                }

                EmailTemplate = GetAllData(lookUpTitle);
             
                string body = EmailTemplate[0].Body;
                body = body.Replace("[MemberName]", UserName);
                //body = body.Replace("[VendorRegistrationLink]","#");        
                body = body.Replace("[Title]", Title);
                if (lookUpTitle!= "Refund Processed")
                {
                    body = body.Replace("[VendorName]", VendorName);
                }
              
                body = body.Replace("[ChargeAmount]", Amount);
                //body = body.Replace("[MemberCompanyName]", Name);

                MailMessage msg = new MailMessage();
                string strBody = string.Empty;

                msg.From = new MailAddress(System.Configuration.ConfigurationManager.AppSettings["senderemail"]);
                msg.To.Add(fromemail);
                msg.Subject = EmailTemplate[0].EmailSubject;
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

                string Remarks = ex.Message;


            }
        }

        public void MailToRefundAdmin(string fromemail, string UserName, string Title, string Amount, string lookUpTitle, string VendorName)
        {
            try
            {
                //Repository.Base.Code.PMVendorRepository staffDirectoryRepository = new Repository.Base.Code.PMVendorRepository();
                IList<Model.EmailTemplateModel> EmailTemplate = null;
                EmailTemplate = GetAllData(lookUpTitle);
                string body = EmailTemplate[0].Body;
                body = body.Replace("[MemberName]", " ");
                //body = body.Replace("[VendorRegistrationLink]","#");        
                body = body.Replace("[Title]", Title);
                body = body.Replace("[VendorName]", VendorName);
                body = body.Replace("[ChargeAmount]", Amount);
                //body = body.Replace("[MemberCompanyName]", Name);

                MailMessage msg = new MailMessage();
                string strBody = string.Empty;

                msg.From = new MailAddress(System.Configuration.ConfigurationManager.AppSettings["senderemail"]);
                msg.To.Add(System.Configuration.ConfigurationManager.AppSettings["RefundToMail"]);
                msg.Subject = EmailTemplate[0].EmailSubject;
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

                string Remarks = ex.Message;


            }
        }



        public string SendInsertMessage(string Body, string Status, Int64 ObjectKey, Int64 ResourceKey, string ModuleKeyName, string Title)
        {

            try
            {
                obj_con.clearParameter();
                obj_con.BeginTransaction();
                obj_con.addParameter("@Body", string.IsNullOrEmpty(Convert.ToString(Body)) ? "" : Body);
                obj_con.addParameter("@Status", string.IsNullOrEmpty(Convert.ToString(Status)) ? "" : Status);
                obj_con.addParameter("@ObjectKey", string.IsNullOrEmpty(Convert.ToString(ObjectKey)) ? 0 : ObjectKey);
                obj_con.addParameter("@ResourceKey", string.IsNullOrEmpty(Convert.ToString(ResourceKey)) ? 0 : ResourceKey);
                obj_con.addParameter("@ModuleKeyName", string.IsNullOrEmpty(Convert.ToString(ModuleKeyName)) ? "" : ModuleKeyName);


                obj_con.ExecuteNoneQuery("site_Message_Insert", CommandType.StoredProcedure);
                obj_con.CommitTransaction();
                obj_con.closeConnection();



                return "Success";
            }
            catch (Exception ex)
            {
                obj_con.RollbackTransaction();
                throw new Exception("site_Message_Insert" + ex.ToString());
            }
        }
        public string SendInsertMessageList(Int64 ObjectKey, Int64 ResourceKey, string ModuleKeyName, string UpdatemsgStatus)
        {
            try
            {
                obj_con.clearParameter();
                obj_con.addParameter("@PageSize", string.IsNullOrEmpty(Convert.ToString(0)) ? 50 : 50);
                obj_con.addParameter("@PageIndex", string.IsNullOrEmpty(Convert.ToString(0)) ? 1 : 1);
                obj_con.addParameter("@Search", "");
                obj_con.addParameter("@Sort", "");
                obj_con.addParameter("@ObjectKey", ObjectKey);
                obj_con.addParameter("@ResourceKey", ResourceKey);
                obj_con.addParameter("@ModuleKeyName", string.IsNullOrEmpty(Convert.ToString(ModuleKeyName)) ? "" : ModuleKeyName);

                obj_con.addParameter("@UpdatemsgStatus", string.IsNullOrEmpty(Convert.ToString(UpdatemsgStatus)) ? "0" : UpdatemsgStatus);
                DataTable dt = obj_con.ConvertDatareadertoDataTable(obj_con.ExecuteReader("site_Message_ListSelectIndexPaging", CommandType.StoredProcedure));
                obj_con.CommitTransaction();
                obj_con.closeConnection();
                return AssoBidsUtility.ConvertDataTableTojSonString(dt);
            }
            catch (Exception ex)
            {
                throw new Exception("site_Message_ListSelectIndexPaging" + ex.ToString());
            }
        }
        public string MessageNewCount(Int64 ObjectKey, Int64 ResourceKey, string ModuleKeyName, Int64 UserId)
        {
            try
            {
                obj_con.clearParameter();
                obj_con.addParameter("@UserId", UserId);
                obj_con.addParameter("@ObjectKey", ObjectKey);
                obj_con.addParameter("@ResourceKey", ResourceKey);
                obj_con.addParameter("@ModuleKeyName", string.IsNullOrEmpty(Convert.ToString(ModuleKeyName)) ? "" : ModuleKeyName);
                DataTable dt = obj_con.ConvertDatareadertoDataTable(obj_con.ExecuteReader("site_Message_NewListCount", CommandType.StoredProcedure));
                obj_con.CommitTransaction();
                obj_con.closeConnection();
                return AssoBidsUtility.ConvertDataTableTojSonString(dt);
            }
            catch (Exception ex)
            {
                throw new Exception("site_Message_NewListCount" + ex.ToString());
            }
        }



        public virtual VendorInvoiceModal GetDataViewEdit(int id)
        {
            VendorInvoiceModal item = null;

            try
            {
                string storedProcedure = "site_Invoice_SelectOneByInvoiceKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@InvoiceKey", SqlDbType.Int, id);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            while (dataReader.Read())
                            {
                                item = new VendorInvoiceModal();

                                LoadViewEdit(dataReader, item);
                                if (item.Amt != "")
                                {
                                    item.Amount =  Common.Utility.FormatNumberHelper(item.Amt, true); 
                                }
                                if (item.BAl != "")
                                {
                                    item.Balance =  Common.Utility.FormatNumberHelper(item.BAl, true); ;
                                }
                                if (item.RefundAmount != "")
                                    item.RefundAmount =  Common.Utility.FormatNumberHelper(item.RefundAmount, true); ;

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

        public List<VendorInvoiceModal> SearchUser(long PageSize, long PageIndex, string Search, string Sort, int CompanyKey)
        {
            List<VendorInvoiceModal> itemList = new List<VendorInvoiceModal>();
            try
            {


                string storedProcedure = "site_InvoieForVendor_SelectIndexPaging";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters 
                        commandWrapper.AddInputParameter("@PageSize", SqlDbType.BigInt, (PageSize == 0) ? 0 : PageSize);
                        commandWrapper.AddInputParameter("@PageIndex", SqlDbType.BigInt, (PageIndex == 0) ? 0 : PageIndex);
                        commandWrapper.AddInputParameter("@Search", SqlDbType.NText, (Search == "") ? "" : Search);
                        commandWrapper.AddInputParameter("@Sort", SqlDbType.NText, (Sort == "") ? "" : Sort);
                        commandWrapper.AddInputParameter("@CompanyKey", SqlDbType.BigInt, (CompanyKey == 0) ? 0 : CompanyKey);
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            VendorInvoiceModal item = null;
                            while (dataReader.Read())
                            {
                                item = new VendorInvoiceModal();
                                LoadInvoicedata(dataReader, item);

                                if (item.Amt != "")
                                {
                                    //item.Amt = item.Amt.Substring(0, item.Amt.Length - 2);
                                    //item.Amount = "$" + item.Amt;
                                    item.Amount = Common.Utility.FormatNumberHelper(item.Amt, true);
                                }
                                if (item.BAl != "")
                                {
                                    //item.BAl = item.BAl.Substring(0, item.BAl.Length - 2);
                                    //item.Balance = "$" + item.BAl;
                                    item.Balance = Common.Utility.FormatNumberHelper(item.BAl, true);
                                }

                                if (item.Amount == "$0.00")
                                {
                                    item.Amount = item.Balance;
                                }



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

        public List<VendorInvoiceModal> RefundVendorList(long PageSize, long PageIndex, string Search, string Sort,long ResourceKey = 0)
        {
            List<VendorInvoiceModal> itemList = new List<VendorInvoiceModal>();
            try
            {


                string storedProcedure = "site_RefundREquest_SelectIndexPaging_Copy";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters 
                        commandWrapper.AddInputParameter("@ResourceKey", SqlDbType.BigInt, ResourceKey);
                        commandWrapper.AddInputParameter("@PageSize", SqlDbType.BigInt, (PageSize == 0) ? 0 : PageSize);
                        commandWrapper.AddInputParameter("@PageIndex", SqlDbType.BigInt, (PageIndex == 0) ? 0 : PageIndex);
                        commandWrapper.AddInputParameter("@Search", SqlDbType.NText, (Search == "") ? "" : Search);
                        commandWrapper.AddInputParameter("@Sort", SqlDbType.NText, (Sort == "") ? "" : Sort);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            VendorInvoiceModal item = null;
                            while (dataReader.Read())
                            {
                                item = new VendorInvoiceModal();
                                LoadRefundData(dataReader, item);

                                if (item.Amt != "")
                                {
                                    //item.Amt = item.Amt.Substring(0, item.Amt.Length - 2);
                                    //item.Amount = "$" + item.Amt;
                                    item.Amount = Common.Utility.FormatNumberHelper(item.Amt, true);
                                }
                                if (item.BAl != null)
                                {
                                    //item.BAl = item.BAl.Substring(0, item.BAl.Length - 2);
                                    //item.Balance = "$" + item.BAl;
                                    item.Balance = Common.Utility.FormatNumberHelper(item.BAl, true);
                                }


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

        public List<VendorInvoiceModal> RefundVendorListPriority(long PageSize, long PageIndex, string Search, string Sort,long ResourceKey)
        {
            List<VendorInvoiceModal> itemList = new List<VendorInvoiceModal>();
            try
            {


                string storedProcedure = "site_RefundREquest_SelectIndexPagingPriority";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters 
                        commandWrapper.AddInputParameter("@ResourceKey", SqlDbType.BigInt, ResourceKey);
                        commandWrapper.AddInputParameter("@PageSize", SqlDbType.BigInt, (PageSize == 0) ? 0 : PageSize);
                        commandWrapper.AddInputParameter("@PageIndex", SqlDbType.BigInt, (PageIndex == 0) ? 0 : PageIndex);
                        commandWrapper.AddInputParameter("@Search", SqlDbType.NText, (Search == "") ? "" : Search);
                        commandWrapper.AddInputParameter("@Sort", SqlDbType.NText, (Sort == "") ? "" : Sort);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            VendorInvoiceModal item = null;
                            while (dataReader.Read())
                            {
                                item = new VendorInvoiceModal();
                                LoadRefundData(dataReader, item);

                                if (item.Amt != "")
                                {
                                    //item.Amt = item.Amt.Substring(0, item.Amt.Length - 2);
                                    //item.Amount = "$" + item.Amt;
                                    item.Amount = Common.Utility.FormatNumberHelper(item.Amt, true);
                                }
                                if (item.BAl != null)
                                {
                                    //item.BAl = item.BAl.Substring(0, item.BAl.Length - 2);
                                    //item.Balance = "$" + item.BAl;
                                    item.Balance = Common.Utility.FormatNumberHelper(item.BAl, true);
                                }


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

}
