using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace AssociationBids.Portal.Repository.Base.Code
{
    public class PushNotificationRepository : BaseRepository, IPushNotificationRepository
    {
        public PushNotificationRepository() { }

        public PushNotificationRepository(string connectionString)
         : base(connectionString) { }


        public long Create(PushNotificationModel pushNotification)
        {
            long id = 0;
            string storedProcedure = "site_PushNotification_Insert";
            using (Database db = new Database(ConnectionString))
            {
                if (pushNotification != null)
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        commandWrapper.AddInputParameter("@ResourceKey", SqlDbType.Int, pushNotification.ResourceKey);
                        commandWrapper.AddInputParameter("@RegistrationToken", SqlDbType.NVarChar, pushNotification.RegistrationToken);
                        commandWrapper.AddInputParameter("@NotificationType", SqlDbType.NVarChar, pushNotification.Type);
                        commandWrapper.AddInputParameter("@NotificationBody", SqlDbType.NVarChar, pushNotification.Body);
                        commandWrapper.AddOutputParameter("@Id", SqlDbType.Int);
                        id = db.ExecuteNonQuery(commandWrapper);
                    }
                }
            }
            return id;
        }

        public List<PushNotificationModel> PushNotification_SelectALL()
        {
            List<PushNotificationModel> notification = new List<PushNotificationModel>();
            string storedProcedure = "site_PushNotification_SelectAll";
            using (Database db = new Database(ConnectionString))
            {
                using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                {
                    var reader = db.ExecuteReader(commandWrapper);
                    while (reader.Read())
                    {
                        PushNotificationModel pm = new PushNotificationModel();
                        pm.Body = reader.GetValueText("Body");
                        pm.DateAdded = reader.GetValueDateTime("DateAdded");
                        pm.DateSent = reader.GetValueDateTime("DateSent");
                        pm.ErrorMessage = reader.GetValueText("ErrorMessage");
                        pm.PushNotificationKey = reader.GetValueInt("PushNotificationKey");
                        pm.RegistrationToken = reader.GetValueText("RegistrationToken");
                        pm.ResourceKey = reader.GetValueInt("ResourceKey");
                        pm.Status = reader.GetValueText("Status");
                        pm.Type = reader.GetValueText("PushNotificationType");
                        notification.Add(pm);
                    }
                }

            }
            return notification;
        }

        public List<PushNotificationModel> PushNotification_SelectByResourceKey(long ResourceKey)
        {
            List<PushNotificationModel> notification = new List<PushNotificationModel>();
            string storedProcedure = "site_PushNotification_SelectByResourceKey";
            using (Database db = new Database(ConnectionString))
            {
                using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                {
                    commandWrapper.AddInputParameter("@ResourceKey", SqlDbType.Int, ResourceKey);
                    var reader = db.ExecuteReader(commandWrapper);
                    while (reader.Read())
                    {
                        PushNotificationModel pm = new PushNotificationModel();
                        pm.Body = reader.GetValueText("Body");
                        pm.DateAdded = reader.GetValueDateTime("DateAdded");
                        pm.DateSent = reader.GetValueDateTime("DateSent");
                        pm.ErrorMessage = reader.GetValueText("ErrorMessage");
                        pm.PushNotificationKey = reader.GetValueInt("PushNotificationKey");
                        pm.RegistrationToken = reader.GetValueText("RegistrationToken");
                        pm.ResourceKey = reader.GetValueInt("ResourceKey");
                        pm.Status = reader.GetValueText("Status");
                        pm.Type = reader.GetValueText("PushNotificationType");
                        notification.Add(pm);
                    }
                }

            }
            return notification;
        }

        public List<PushNotificationModel> PushNotification_SelectById(long Id)
        {
            List<PushNotificationModel> notification = new List<PushNotificationModel>();
            string storedProcedure = "site_PushNotification_SelectById";
            using (Database db = new Database(ConnectionString))
            {
                using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                {
                    commandWrapper.AddInputParameter("@PushNotificationKey", SqlDbType.Int, Id);
                    var reader = db.ExecuteReader(commandWrapper);
                    while (reader.Read())
                    {
                        
                        PushNotificationModel noti = new PushNotificationModel();
                        noti.ForUserKey = reader.GetValueInt("ForUser");
                        noti.Body = reader.GetValueText("Body");
                        noti.DateAdded = reader.GetValueDateTime("DateAdded");
                        noti.DateSent = reader.GetValueDateTime("DateSent");
                        noti.ErrorMessage = reader.GetValueText("ErrorMessage");
                        noti.ForResourceKey = reader.GetValueInt("ForResource");
                        noti.ModuleKey = reader.GetValueInt("ModuleKey");
                        noti.ObjectKey = reader.GetValueInt("ObjectKey");
                        noti.ObjectTitle = reader.GetValueText("ObjectTitle");
                        noti.PushNotificationKey = reader.GetValueInt("PushNotificationKey");
                        noti.RegistrationToken = reader.GetValueText("RegistrationToken");
                        noti.ResourceKey = reader.GetValueInt("ResourceKey");
                        noti.Status = reader.GetValueText("Status");
                        noti.Type = reader.GetValueText("PushNotificationType");
                        noti.ByVendorName = reader.GetValueText("ByVendorName");
                        noti.ByCompanyName = reader.GetValueText("ByCompanyName");
                        noti.BidVendorKey = reader.GetValueInt("BidVendorKey");
                        noti.BidDueDate = reader.GetValueText("BidDueDate");
                        noti.ResponseDueDate = reader.GetValueText("ResponseDueDate");
                        noti.ExpiryDate = reader.GetValueText("ExpiryDate");
                        noti.BidVendorStatusKey = reader.GetValueText("BidVendorStatusKey");
                        noti.BidVendorStatus = reader.GetValueText("BidVendorStatus");
                        long notiid = reader.GetValueInt("NotificationKey");
                        if (notiid == 0)
                            notiid = reader.GetValueInt("AbNotificationId");
                        noti.NotificationId = notiid;
                        notification.Add(noti);
                    }
                }

            }
            return notification;
        }

        public List<PushNotificationModel> PushNotification_SelectByType(long ResourceKey,string Type)
        {
            List<PushNotificationModel> notification = new List<PushNotificationModel>();
            string storedProcedure = "site_PushNotification_SelectByType";
            using (Database db = new Database(ConnectionString))
            {
                using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                {
                    commandWrapper.AddInputParameter("@ResourceKey", SqlDbType.Int, ResourceKey);
                    commandWrapper.AddInputParameter("@PushNotificationType", SqlDbType.VarChar, Type);
                    var reader = db.ExecuteReader(commandWrapper);
                    while (reader.Read())
                    {
                        PushNotificationModel pm = new PushNotificationModel();
                        pm.Body = reader.GetValueText("Body");
                        pm.DateAdded = reader.GetValueDateTime("DateAdded");
                        pm.DateSent = reader.GetValueDateTime("DateSent");
                        pm.ErrorMessage = reader.GetValueText("ErrorMessage");
                        pm.PushNotificationKey = reader.GetValueInt("PushNotificationKey");
                        pm.RegistrationToken = reader.GetValueText("RegistrationToken");
                        pm.ResourceKey = reader.GetValueInt("ResourceKey");
                        pm.Status = reader.GetValueText("Status");
                        pm.Type = reader.GetValueText("PushNotificationType");
                        notification.Add(pm);
                    }
                }

            }
            return notification;
        }

        public bool UpdateStatus(long PushNotificationKey, string MulticastId,string MessageId,string errorText)
        {
            bool status = false;
            string storedProcedure = "site_PushNotification_UpdateStatus";
            using (Database db = new Database(ConnectionString))
            {
                using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                {
                    commandWrapper.AddInputParameter("@PushNotificationKey", SqlDbType.Int, PushNotificationKey);
                    commandWrapper.AddInputParameter("@MulticastId", SqlDbType.NVarChar,MulticastId);
                    commandWrapper.AddInputParameter("@MessageId", SqlDbType.NVarChar,MessageId);
                    commandWrapper.AddInputParameter("@errorText", SqlDbType.NVarChar,errorText);
                    commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);
                    db.ExecuteNonQuery(commandWrapper);
                    if (commandWrapper.GetValueInt("@errorCode") == 0)
                        status = true;
                }
            }
            return status;
        }

        public List<string> GetUserTokensByUserKey(long UserKey)
        {
            List<string> registrationTokens = new List<string>();
            string storedProcedure = "site_UserToken_GetRegistrationTokenByUserKey";
            using (Database db = new Database(ConnectionString))
            {
                using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                {
                    commandWrapper.AddInputParameter("@UserKey", SqlDbType.Int, UserKey);
                    var reader = db.ExecuteReader(commandWrapper);
                    while (reader.Read())
                    {
                        string regToken = reader.GetValueText("RegistrationToken");

                        if (regToken != null && regToken.Trim() != "")
                        {
                            regToken += " #DeviceType# " + reader.GetValueText("ClientBrowser");
                            registrationTokens.Add(regToken);
                        }
                    }
                }
            }
            return registrationTokens;
        }

        public List<PushNotificationModel> SelectALLUnreadPushNotification(long UserKey)
        {
            List<PushNotificationModel> pushNotifications = new List<PushNotificationModel>();
            string storedProcedure = "site_PushNotification_SelectOnlyNew";
            using (Database db = new Database(ConnectionString))
            {
                DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure);
                commandWrapper.AddInputParameter("@UserKey", System.Data.SqlDbType.Int, UserKey);
                var notificatios = db.ExecuteReader(commandWrapper);
                while (notificatios.Read())
                {
                    PushNotificationModel noti = new PushNotificationModel();
                    noti.ForUserKey = notificatios.GetValueInt("ForUser");
                    noti.Body = notificatios.GetValueText("Body");
                    noti.DateAdded = notificatios.GetValueDateTime("DateAdded");
                    noti.DateSent = notificatios.GetValueDateTime("DateSent");
                    noti.ErrorMessage = notificatios.GetValueText("ErrorMessage");
                    noti.ForResourceKey = notificatios.GetValueInt("ForResource");
                    noti.ModuleKey = notificatios.GetValueInt("ModuleKey");
                    noti.ObjectKey = notificatios.GetValueInt("ObjectKey");
                    noti.ObjectTitle = notificatios.GetValueText("ObjectTitle");
                    noti.PushNotificationKey = notificatios.GetValueInt("PushNotificationKey");
                    noti.RegistrationToken = notificatios.GetValueText("RegistrationToken");
                    noti.ResourceKey = notificatios.GetValueInt("ResourceKey");
                    noti.Status = notificatios.GetValueText("Status");
                    noti.Type = notificatios.GetValueText("PushNotificationType");
                    noti.ByVendorName = notificatios.GetValueText("ByVendorName");
                    noti.ByCompanyName = notificatios.GetValueText("ByCompanyName");
                    noti.BidVendorKey = notificatios.GetValueInt("BidVendorKey");
                    noti.BidDueDate = notificatios.GetValueText("BidDueDate");
                    noti.ResponseDueDate = notificatios.GetValueText("ResponseDueDate");
                    noti.ExpiryDate = notificatios.GetValueText("ExpiryDate");
                    noti.BidVendorStatusKey = notificatios.GetValueText("BidVendorStatusKey");
                    noti.BidVendorStatus = notificatios.GetValueText("BidVendorStatus");
                    long notiid = notificatios.GetValueInt("NotificationKey");
                    if (notiid == 0)
                        notiid = notificatios.GetValueInt("AbNotificationId");
                    noti.NotificationId = notiid;
                    pushNotifications.Add(noti);
                }

            }
            return pushNotifications;
        }

        public PushResponse SendPushNotificationById(long PushNotificationKey)
        {
            List<long> Users = new List<long>();
            
            List<PushResponse> pushResponses = new List<PushResponse>();
            List<PushNotificationModel> pushNotifications = new List<PushNotificationModel>();
            PushNotificationJsonModel notiPayload = new PushNotificationJsonModel();
            pushNotifications = PushNotification_SelectById(PushNotificationKey);
            Users.AddRange(pushNotifications.Select(s => s.ForUserKey));
            int ii = 0;
            foreach (var noti in pushNotifications)
            {
                if (ii > 0)
                    break;

                List<string> usertokens = new List<string>();

                notiPayload = new PushNotificationJsonModel();
                notiPayload.date_added = noti.DateAdded.ToString("M/d/yyyy hh:mm:ss tt");
                notiPayload.last_modification_date = new DateTime().ToString("M/d/yyyy hh:mm:ss tt");
                notiPayload.for_resource_key = noti.ForResourceKey;
                notiPayload.notification_type = noti.Type;
                notiPayload.object_key = noti.ObjectKey;
                notiPayload.title = noti.ObjectTitle;
                notiPayload.by_company_name = noti.ByCompanyName;
                notiPayload.by_vendor_name = noti.ByVendorName;
                notiPayload.notification_text = noti.Body;
                notiPayload.module_key = noti.ModuleKey;
                notiPayload.bid_vendor_id = noti.BidVendorKey;
                notiPayload.status = noti.Status;
                notiPayload.bid_vendor_status = noti.BidVendorStatus;
                notiPayload.BidDueDate = noti.BidDueDate;
                notiPayload.BidDueDate = noti.BidDueDate;
                notiPayload.ExpiryDate = noti.ExpiryDate;
                notiPayload.notification_id = Convert.ToString(noti.NotificationId);
                LoadPushNotification(notiPayload);
                notiPayload.title = (notiPayload.module_key == 100) ? "Bid Request" : (notiPayload.module_key == 106) ? "Work Order" : noti.ObjectTitle;
                var lists = GetUserTokensByUserKey(noti.ForUserKey);
                PushResponse p = new PushResponse();
                if (lists.Count > 0)
                {
                    foreach (var l in lists)
                    {
                        p = sendpushnew(l, notiPayload);
                        //string multicastId = "0";
                        //string messageId = "0";
                        //string errorText = "";
                        //try
                        //{
                        //    multicastId = p.multicast_id;
                        //    messageId = p.results[0].message_id;
                        //    if (p.failure == 1)
                        //        errorText = p.results[0].error;
                        //}
                        //catch {
                        //    try
                        //    {
                        //        if (p.failure == 1)
                        //            errorText = p.results[0].error;
                        //    }
                        //    catch { }
                        //}
                        //service.UpdateStatus(noti.PushNotificationKey, multicastId, messageId, errorText);
                        ii++;
                    }
                }
                else
                {
                    p.results = new List<Result>(1);
                    p.results.Add(new Result());
                    p.results[0].error = "Custome Error: RegistrationToken not found";
                    p.results[0].message_id = "0";
                    p.failure = 1;
                    p.success = 0;
                    p.multicast_id = "0";
                }
                return p;
            }
            return new PushResponse();
        }

        private static PushResponse sendpush(string UserToken, object NotificationPayLoad)
        {
            PushResponse obj_p = new PushResponse();
            string FirebaseApiID = ConfigurationManager.AppSettings["FirebaseApiID"];
            string FirebaseSenderID = ConfigurationManager.AppSettings["FirebaseSenderID"];

            if (FirebaseSenderID == null || FirebaseSenderID == null)
            {
                obj_p.failure = 1;
                obj_p.results.Add(new Result("No Firebase Sender ID or API key found"));
                return obj_p;
            }

            WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
            //WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/v1/projects/association-bids-415e0/messages:send");

            tRequest.Method = "POST";
            tRequest.Headers.Add(string.Format("Authorization: key={0}", FirebaseApiID));
            tRequest.Headers.Add(string.Format("Sender: id={0}", FirebaseSenderID));
            tRequest.ContentType = "application/json";
            var payload = new
            {
                to = UserToken,  /*"eg8t-PgE5zQ:APA91bFdDqeoQUoM0UXji-aSbBathyHH0YVRkLmLjKLTEOBpiD00ahpGwq_ORiczMQ2lVNAMKTvc9oJHg-T_LAQyCsWbgUgdPp0tJPe9oCMiEzZsCd17eQ6mG1I45sunRXBa-TJ5QNlT",*/
                priority = "high",
                notification = new { content_available = true },
                data = NotificationPayLoad,
                project_id = "association-bids-415e0",
            };
            var n = new PushNotificationJsonModel();
            try
            {
                n = (PushNotificationJsonModel)NotificationPayLoad;
            }
            catch { n = null; }
            var noti = new
            {
                to = UserToken,
                priority = "high",
                notification = new
                {
                    title = "New Notification",
                    text = "You have new notification",
                    content_available = true,
                    sound = "enabled",
                    id = "AB",
                    data = NotificationPayLoad
                },
                project_id = "association-bids-415e0",
            };
            if (n != null)
            {
                string Notititle = n.title;
                if (n.module_key == 100)
                    Notititle = "Bid Request " + Notititle;
                else if (n.module_key == 106)
                    Notititle = "Work Order " + Notititle;
                noti = new
                {
                    to = UserToken,
                    priority = "high",
                    notification = new
                    {
                        title = Notititle,
                        text = n.notification_text.Replace('.', ' ') + " " + n.by_vendor_name,
                        content_available = true,
                        sound = "enabled",
                        id = "AB",
                        data = NotificationPayLoad
                    },
                    project_id = "association-bids-415e0",
                };
            }
            string postbody = JsonConvert.SerializeObject(noti).ToString();
            Byte[] byteArray = Encoding.UTF8.GetBytes(postbody);
            tRequest.ContentLength = byteArray.Length;
            using (Stream dataStream = tRequest.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
                using (WebResponse tResponse = tRequest.GetResponse())
                {
                    using (Stream dataStreamResponse = tResponse.GetResponseStream())
                    {
                        if (dataStreamResponse != null) using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                                obj_p = JsonConvert.DeserializeObject<PushResponse>(sResponseFromServer);
                            }
                    }
                }
            }
            return obj_p;
        }
        private static PushResponse sendpushnew(string UserToken, object NotificationPayLoad)
        {
            PushResponse obj_p = new PushResponse();
            string FirebaseApiID = ConfigurationManager.AppSettings["FirebaseApiID"];
            string FirebaseSenderID = ConfigurationManager.AppSettings["FirebaseSenderID"];

            if (FirebaseSenderID == null || FirebaseSenderID == null)
            {
                obj_p.failure = 1;
                obj_p.results.Add(new Result("No Firebase Sender ID or API key found"));
                return obj_p;
            }

            WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
            //WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/v1/projects/association-bids-415e0/messages:send");
            string DeviceType = "";
            string Token = "";
            try
            {
                Token = Regex.Split(UserToken, "#DeviceType#")[0].Trim();
                DeviceType = Regex.Split(UserToken, "#DeviceType#")[1].Trim();
            }
            catch { }

            tRequest.Method = "POST";
            tRequest.Headers.Add(string.Format("Authorization: key={0}", FirebaseApiID));
            tRequest.Headers.Add(string.Format("Sender: id={0}", FirebaseSenderID));
            tRequest.ContentType = "application/json";
            var payload = new
            {
                to = Token,  /*"eg8t-PgE5zQ:APA91bFdDqeoQUoM0UXji-aSbBathyHH0YVRkLmLjKLTEOBpiD00ahpGwq_ORiczMQ2lVNAMKTvc9oJHg-T_LAQyCsWbgUgdPp0tJPe9oCMiEzZsCd17eQ6mG1I45sunRXBa-TJ5QNlT",*/
                priority = "high",
                notification = new { content_available = true },
                data = NotificationPayLoad,
                project_id = "association-bids-415e0",
            };
            var n = new PushNotificationJsonModel();
            try
            {
                n = (PushNotificationJsonModel)NotificationPayLoad;
            }
            catch { n = null; }
            var noti = new
            {
                to = Token,
                priority = "high",
                data = new
                {
                    title = "New Notification",
                    text = "You have new notification",
                    content_available = true,
                    sound = "enabled",
                    id = "AB",
                    data = NotificationPayLoad
                },
                NotificationPayLoad,
                project_id = "association-bids-415e0",
            };
            var notiIos = new
            {
                to = Token,
                priority = "high",
                notification = new
                {
                    title = "New Notification",
                    text = "You have new notification",
                    body = "You have new notification",
                    content_available = true,
                    sound = "enabled",
                    id = "AB",
                    data = NotificationPayLoad
                },
                project_id = "association-bids-415e0",
            };
            if (n != null)
            {
                string Notititle = n.BidTitle;
                //if (n.module_key == 100)
                //    Notititle = "Bid Request " + Notititle;
                //else if (n.module_key == 106)
                //    Notititle = "Work Order " + Notititle;
                noti = new
                {
                    to = Token,
                    priority = "high",
                    data = new
                    {
                        title = Notititle,
                        text = n.notification_text,
                        content_available = true,
                        sound = "enabled",
                        id = "AB",
                        data = NotificationPayLoad
                    },
                    NotificationPayLoad,
                    project_id = "association-bids-415e0",
                };
                if (DeviceType.ToLower().Contains("ios"))
                {
                    notiIos = new
                    {
                        to = Token,
                        priority = "high",
                        notification = new
                        {
                            title = Notititle,
                            text = "Demo Text",
                            body = n.notification_text,
                            content_available = true,
                            sound = "enabled",
                            id = "AB",
                            data = NotificationPayLoad
                        },
                        project_id = "association-bids-415e0",
                    };
                }
            }
            string postbody = "";
            if (DeviceType.ToLower().Contains("ios"))
                postbody = JsonConvert.SerializeObject(notiIos).ToString();
            else
                postbody = JsonConvert.SerializeObject(noti).ToString();

            Byte[] byteArray = Encoding.UTF8.GetBytes(postbody);
            tRequest.ContentLength = byteArray.Length;
            using (Stream dataStream = tRequest.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
                using (WebResponse tResponse = tRequest.GetResponse())
                {
                    using (Stream dataStreamResponse = tResponse.GetResponseStream())
                    {
                        if (dataStreamResponse != null) using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                                obj_p = JsonConvert.DeserializeObject<PushResponse>(sResponseFromServer);
                            }
                    }
                }
            }
            return obj_p;
        }

        public void LoadPushNotification(PushNotificationJsonModel model)
        {
            try
            {
                using (Database db = new Database(ConnectionString))
                {
                    DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper("site_NotificationTemplate_ByType");
                    commandWrapper.AddInputParameter("@NotificationType", SqlDbType.NVarChar, model.notification_type);
                    var reader = db.ExecuteReader(commandWrapper);
                    while (reader.Read())
                    {
                        model.notification_text = ReplaceTextForNotification(model, reader.GetValueText("Body"));
                        model.BidTitle = ReplaceTextForNotification(model, reader.GetValueText("PushNotificationTitle"));
                        if (model.module_key == 100)
                            model.title = "Bid Request";
                        else if (model.module_key == 106)
                            model.title = "Work Order";
                    }
                }
            }
            catch { }
        }

        private static string ReplaceTextForNotification(PushNotificationJsonModel reader, string text)
        {
            string txt = text;

            string htmlTagPattern = "<.*?>";
            //var regexCss = new Regex("$(\\<script(.+?)\\)|(\\<style(.+?)\\)", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            //txt = regexCss.Replace(txt, string.Empty);
            txt = Regex.Replace(txt, htmlTagPattern, string.Empty);
            txt = Regex.Replace(txt, @"^\s+$[\r\n]*", "", RegexOptions.Multiline);
            //txt = txt.Replace(" ", string.Empty);
            txt = txt.Replace("&nbsp;", " ");
            txt = txt.Replace("[ContactPersonName]", reader.by_vendor_name);
            txt = txt.Replace("[BidName]", reader.title);
            txt = txt.Replace("[PolicyNumber]", reader.title);
            txt = txt.Replace("[CCNumber]", reader.title);
            txt = txt.Replace("[CompanyName]", reader.by_company_name);
            txt = txt.Replace("[ResponseDueDate]", reader.ResponseDueDate);
            txt = txt.Replace("[BidDueDate]", reader.BidDueDate);
            txt = txt.Replace("[MembershipExpiryDate]", reader.ExpiryDate);
            txt = txt.Replace("[CCExpiryDate]", reader.ExpiryDate);
            txt = txt.Replace("[InsuranceExpiryDate]", reader.ExpiryDate);
            txt = txt.Replace("[Status]", reader.status);
            txt = txt.Replace("[BidVendorStatus]", reader.bid_vendor_status);

            return txt;
        }

    }
}
