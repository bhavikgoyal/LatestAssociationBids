using System;
using System.Threading;
using System.Collections.Generic;
using System.Net.Mail;

namespace AssociationBids.Portal.Common
{
    public class Email : IDisposable
    {
        /*
        const String HOST = "smtp.1and1.com";
        const int PORT = 25;
        const String SMTP_USERNAME = "noreply@associationbids.com";
        const String SMTP_PASSWORD = "noreply599";
         * */

        const String HOST = "smtp.socketlabs.com";
        const int PORT = 25;
        const String SMTP_USERNAME = "server34693";
        const String SMTP_PASSWORD = "Qp8j9SNq73Way2LRk6";

        #region Local Variables
        private System.Net.Mail.MailMessage __message;
        private System.Net.Mail.SmtpClient __smtpServer;
        #endregion

        public Email(System.Net.Mail.MailMessage message)
        {
            // Set local variables
            __message = message;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // dispose managed resources
                __smtpServer.Dispose();
            }
            // free native resources
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #region Properties
        public System.Net.Mail.SmtpClient SmtpServer
        {
            get
            {
                if (__smtpServer == null)
                {
                    if (Util.AppSettingsExist("Mail.UseLocalSMTP") && Util.GetAppSettingsBool("Mail.UseLocalSMTP"))
                    {
                        __smtpServer = new System.Net.Mail.SmtpClient();
                        __smtpServer = new System.Net.Mail.SmtpClient(Util.GetAppSettings("smtpServer"), Util.GetAppSettingsInt("smtpPort"));

                        __smtpServer.Host = Util.GetAppSettings("Mail.SMTPServer");
                        __smtpServer.UseDefaultCredentials = true;
                        __smtpServer.Credentials = new System.Net.NetworkCredential(Util.GetAppSettings("smtpUser"), Util.GetAppSettings("smtpPass"));
                    }
                    else
                    {
                        __smtpServer = new System.Net.Mail.SmtpClient(HOST, PORT);

                        __smtpServer.Credentials = new System.Net.NetworkCredential(SMTP_USERNAME, SMTP_PASSWORD);
                    }
                }

                return __smtpServer;
            }
            set
            {
                __smtpServer = value;
            }
        }

        public System.Net.Mail.MailMessage Message
        {
            get { return __message; }
            set { __message = value; }
        }
        #endregion

        #region Methods
        public void SendMailAsync()
        {
            try
            {
                Email email = new Email(__message);
                email.SmtpServer = SmtpServer;

                Thread thread = new Thread(new ThreadStart(email.SendMail));
                thread.Start();
            }
            catch (Exception e)
            {
                Error.LogError(e, false);
            }
        }

        public void SendMail()
        {
            try
            {
                if (Util.GetAppSettingsBool("Mail.Enabled"))
                {
                    if (__message.IsBodyHtml)
                    {
                        UpdateMessageBody(__message);
                    }

                    UpdateMessageFrom(__message);

                    RemoveDuplicateEmailAddresses();

                    SmtpServer.Send(__message);
                }
            }
            catch (Exception e)
            {
                Error.LogError(e, false);
            }
        }

        // Only the *.associationbids.com and *.breakawaymgmt.com domains are allowed to send emails
        private void UpdateMessageFrom(System.Net.Mail.MailMessage message)
        {
            string adminEmail = Util.GetAppSettings("Mail.SiteAdministrator");
            if (message.From.Address != adminEmail)
            {
                message.ReplyToList.Add(message.From);
                message.From = new MailAddress(adminEmail, message.From.DisplayName);
            }
        }
        

        private void UpdateMessageBody(System.Net.Mail.MailMessage message)
        {
            System.Text.StringBuilder body = new System.Text.StringBuilder();

            // Append HTML tags and StyleSheet
            body.Append("<html><head>");
            body.Append("<style type=\"text/css\">");
            body.Append("<!-- ");
            body.Append("body, td { font-family: Verdana, Arial; font-size: 12px; line-height: 20px; }");
            body.Append(" -->");
            body.Append("</style>");
            body.Append("</head><body>");

            // Append original Body content
            body.Append(Util.FormatText(message.Body));

            body.Append("</body></html>");

            message.Body = body.ToString();
        }

        private void RemoveDuplicateEmailAddresses()
        {
            IList<MailAddress> addList = new List<MailAddress>();
            IList<MailAddress> removeListTo = new List<MailAddress>();
            IList<MailAddress> removeListCc = new List<MailAddress>();
            IList<MailAddress> removeListBcc = new List<MailAddress>();

            ProcessEmailAddresses(__message.To, addList, removeListTo);
            ProcessEmailAddresses(__message.CC, addList, removeListCc);
            ProcessEmailAddresses(__message.Bcc, addList, removeListBcc);

            ProcessEmailAddresses(__message.To, removeListTo);
            ProcessEmailAddresses(__message.CC, removeListCc);
            ProcessEmailAddresses(__message.Bcc, removeListBcc);
        }

        private void ProcessEmailAddresses(MailAddressCollection emailList, IList<MailAddress> addList, IList<MailAddress> removeList)
        {
            foreach (MailAddress email in emailList)
            {
                if (addList.Contains(email))
                {
                    removeList.Add(email);
                }
                else
                {
                    addList.Add(email);
                }
            }
        }

        private void ProcessEmailAddresses(MailAddressCollection emailList, IList<MailAddress> removeList)
        {
            foreach(MailAddress email in removeList)
            {
                emailList.Remove(email);
            }
        }
        #endregion
    }
}
