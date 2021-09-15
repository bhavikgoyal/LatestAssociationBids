using AssociationBids.GlobalUtilities;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base.Interface;
using DB_con;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace AssociationBids.Portal.Repository.Base.Code
{
    public class ABNotificationRepository : BaseRepository, IABNotificationRepository
    {
        ConnectionCls obj_con = null;
        public ABNotificationRepository() { obj_con = new ConnectionCls(); }

        public ABNotificationRepository(string connectionString)
         : base(connectionString) { }

        public bool InsertNotification(string NotificationType, int ModuleKey, long ObjectKey, long ByResourceKey,string Text,long ForResourceKey = 0)
        {
            bool status = true;
            try
            {


   
                 



                    int Bvkey = GetBidVendorIdResourceKey(ByResourceKey, ObjectKey);
                    int portal = GetPortalFromResourceKey(ByResourceKey);
                    if (portal == 0)
                        return false;
                    List<long> resources = new List<long>();
                    if (portal == 3)
                    {
                        if (ModuleKey == 302 || ModuleKey == 301 || ModuleKey == 300)
                        {
                            resources.Add(ForResourceKey);
                        }
                        else
                        {
                            resources = GetResourceByNotificationTypeVendor(NotificationType, ModuleKey, ObjectKey);
                        }
                    }
                    else if (portal == 2)
                    {
                        resources = GetResourceByNotificationTypePM(NotificationType, ModuleKey, ObjectKey, ForResourceKey);
                        if (NotificationType == "BidReqMsg" || NotificationType == "BidReqStatusReject" || NotificationType == "BidReqStatusRejByAcceptOther" || NotificationType == "BidReqDate")
                        {
                            BidVendorRepository bv = new BidVendorRepository();
                            var bidVendor = bv.Get(Convert.ToInt32(ObjectKey));
                            ObjectKey = bidVendor.BidRequestKey;

                        }
                        if (NotificationType == "BidReqStatusAccept")
                            ModuleKey = 106;
                    }
                    else if (portal == 1)
                        resources = GetResourceByNotificationTypeAdmin(NotificationType, ModuleKey, ObjectKey);

                    string storedProcedure = "site_ABNotification_InsertSingle";
                    string storedProcedure1 = "site_PushNotification_Insert";
                    int NotiKey = 0;
                    if (Text == "Bid Vendor Status Not Interested" && Bvkey == 0)
                    {


                    }
                    else
                    {
                        IPushNotificationRepository notificationRepository = new PushNotificationRepository();
                        using (Database db = new Database(ConnectionString))
                        {
                            if (resources != null)
                            {
                                for (int i = 0; i < resources.Count; i++)
                            {
                                if (NotificationType  == "BidReqDate" && ForResourceKey != 0)
                                {
                                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                                    {
                                        commandWrapper.AddInputParameter("@NotificationType", SqlDbType.VarChar, NotificationType);
                                        commandWrapper.AddInputParameter("@ModuleKey", SqlDbType.Int, ModuleKey);
                                        commandWrapper.AddInputParameter("@ObjectKey", SqlDbType.Int, ObjectKey);
                                        commandWrapper.AddInputParameter("@ByResource", SqlDbType.Int, ByResourceKey);
                                        commandWrapper.AddInputParameter("@ForResource", SqlDbType.Int, resources[i]);
                                        commandWrapper.AddInputParameter("@BidVendorKey", SqlDbType.Int, ForResourceKey);
                                        commandWrapper.AddInputParameter("@NotificationText", SqlDbType.NVarChar, Text);
                                        commandWrapper.AddOutputParameter("@Id", SqlDbType.Int);
                                        int id = db.ExecuteNonQuery(commandWrapper);
                                        NotiKey = commandWrapper.GetValueInt("@Id");




                                        //if (id > 0)
                                        //    status = true;
                                    }

                                }
                                else
                                {
                                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                                    {
                                        commandWrapper.AddInputParameter("@NotificationType", SqlDbType.VarChar, NotificationType);
                                        commandWrapper.AddInputParameter("@ModuleKey", SqlDbType.Int, ModuleKey);
                                        commandWrapper.AddInputParameter("@ObjectKey", SqlDbType.Int, ObjectKey);
                                        commandWrapper.AddInputParameter("@ByResource", SqlDbType.Int, ByResourceKey);
                                        commandWrapper.AddInputParameter("@ForResource", SqlDbType.Int, resources[i]);
                                        commandWrapper.AddInputParameter("@NotificationText", SqlDbType.NVarChar, Text);
                                        commandWrapper.AddOutputParameter("@Id", SqlDbType.Int);
                                        int id = db.ExecuteNonQuery(commandWrapper);
                                        NotiKey = commandWrapper.GetValueInt("@Id");




                                        //if (id > 0)
                                        //    status = true;
                                    }
                                }
                               
                                   
                                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure1))
                                    {
                                        commandWrapper.AddInputParameter("@NotificationType", SqlDbType.VarChar, NotificationType);
                                        commandWrapper.AddInputParameter("@ModuleKey", SqlDbType.Int, ModuleKey);
                                        commandWrapper.AddInputParameter("@ObjectKey", SqlDbType.Int, ObjectKey);
                                        commandWrapper.AddInputParameter("@ResourceKey", SqlDbType.Int, ByResourceKey);
                                        commandWrapper.AddInputParameter("@ForResource", SqlDbType.Int, resources[i]);
                                        commandWrapper.AddInputParameter("@NotificationBody", SqlDbType.NVarChar, Text);
                                        commandWrapper.AddInputParameter("@RegistrationToken", SqlDbType.NVarChar, "");
                                        commandWrapper.AddInputParameter("@NotificationKey", SqlDbType.Int, NotiKey);
                                        commandWrapper.AddOutputParameter("@PushNotificationId", SqlDbType.Int);

                                        int id = db.ExecuteNonQuery(commandWrapper);
                                        int pushKey = commandWrapper.GetValueInt("@PushNotificationId");
                                        if (pushKey > 0)
                                        {
                                            try
                                            {
                                                PushResponse p = notificationRepository.SendPushNotificationById(pushKey);
                                                if (p != null)
                                                {
                                                    if (p.success == 1)
                                                        notificationRepository.UpdateStatus(pushKey, p.multicast_id, p.results[0].message_id, "");
                                                    else
                                                        notificationRepository.UpdateStatus(pushKey, p.multicast_id, "0", p.results[0].error);
                                                }
                                            }
                                            catch { }
                                        }
                                        //if (id > 0)
                                        //    status = true;
                                    }
                                }
                            }

                        }

                    }
                
              
            }
            catch (Exception ex)
            {
                status = false;
            }

            return status;
        }


        public bool UpdateStatsVendordash(long NotificationId, string Status, int Objectkey)
        {
            bool status = false;

            try
            {
                string storedProcedure = "site_ABNotification_UpdateStatusforVendor";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        commandWrapper.AddInputParameter("@Id", SqlDbType.Int, NotificationId);
                        commandWrapper.AddInputParameter("@Status", SqlDbType.VarChar, Status);
                        commandWrapper.AddInputParameter("@Objectkey", SqlDbType.Int, Objectkey);
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);
                        db.ExecuteNonQuery(commandWrapper);
                        if (commandWrapper.GetValueInt("@errorCode") == 0)
                            status = true;
                    }
                }
            }
            catch (Exception ex)
            {
                status = false;
            }

            return status;
        }
        public bool InsertNotificationdashborad(string NotificationType, int ModuleKey, long ObjectKey, long ByResourceKey, string Text, long ForResourceKey = 0)
        {
            bool status = true;
            try
            {


                if (NotificationType == "BidReqDate")
                {
                    int NotiKey = 0;
                    string storedProcedure = "site_ABNotification_InsertSingle";
                    string storedProcedure1 = "site_PushNotification_Insert";
                    IPushNotificationRepository notificationRepository = new PushNotificationRepository();
                    using (Database db = new Database(ConnectionString))
                    {


                        using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                        {
                            commandWrapper.AddInputParameter("@NotificationType", SqlDbType.VarChar, NotificationType);
                            commandWrapper.AddInputParameter("@ModuleKey", SqlDbType.Int, ModuleKey);
                            commandWrapper.AddInputParameter("@ObjectKey", SqlDbType.Int, ObjectKey);
                            commandWrapper.AddInputParameter("@ByResource", SqlDbType.Int, ForResourceKey);
                            commandWrapper.AddInputParameter("@ForResource", SqlDbType.Int, ByResourceKey);
                            commandWrapper.AddInputParameter("@NotificationText", SqlDbType.NVarChar, Text);
                            commandWrapper.AddOutputParameter("@Id", SqlDbType.Int);
                            int id = db.ExecuteNonQuery(commandWrapper);

                            NotiKey = commandWrapper.GetValueInt("@Id");



                            //if (id > 0)
                            //    status = true;
                        }
                        using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure1))
                        {
                            commandWrapper.AddInputParameter("@NotificationType", SqlDbType.VarChar, NotificationType);
                            commandWrapper.AddInputParameter("@ModuleKey", SqlDbType.Int, ModuleKey);
                            commandWrapper.AddInputParameter("@ObjectKey", SqlDbType.Int, ObjectKey);
                            commandWrapper.AddInputParameter("@ResourceKey", SqlDbType.Int, ByResourceKey);
                            commandWrapper.AddInputParameter("@ForResource", SqlDbType.Int, ByResourceKey);
                            commandWrapper.AddInputParameter("@NotificationBody", SqlDbType.NVarChar, Text);
                            commandWrapper.AddInputParameter("@RegistrationToken", SqlDbType.NVarChar, "");
                            commandWrapper.AddInputParameter("@NotificationKey", SqlDbType.Int, NotiKey);
                            commandWrapper.AddOutputParameter("@PushNotificationId", SqlDbType.Int);

                            int id = db.ExecuteNonQuery(commandWrapper);
                            int pushKey = commandWrapper.GetValueInt("@PushNotificationId");
                            if (pushKey > 0)
                            {
                                try
                                {
                                    PushResponse p = notificationRepository.SendPushNotificationById(pushKey);
                                    if (p != null)
                                    {
                                        if (p.success == 1)
                                            notificationRepository.UpdateStatus(pushKey, p.multicast_id, p.results[0].message_id, "");
                                        else
                                            notificationRepository.UpdateStatus(pushKey, p.multicast_id, "0", p.results[0].error);
                                    }
                                }
                                catch { }
                            }
                            //if (id > 0)
                            //    status = true;
                        }



                    }
                }

        

            }
            catch (Exception ex)
            {
                status = false;
            }

            return status;
        }
        private string GenerateLink(int ModuleKey, long ObjectKey, long ResourceKey, string NotificationType)
        {
            string link = "";
            try
            {
                string storedProcedure = "site_Portal_SelectByResourceKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        commandWrapper.AddInputParameter("@ResourceKey", SqlDbType.Int, ResourceKey);
                        var reader = db.ExecuteReader(commandWrapper);
                        reader.Read();

                        int portalkey = reader.GetValueInt("PortalKey");
                        if(portalkey == 1)
                        {
                            if(ModuleKey == 200)
                            {
                                if (NotificationType == "RefundReq")
                                    link = "/VendorInvoice/RefundInvoiceView?InvoiceKey=" + ObjectKey;
                                else
                                    link = "/VendorInvoice/vendorRefundIndex#msg?data=" + ObjectKey;
                            }
                            
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                link = "#";
            }
            return link;
        }

        private string GenerateLinkNew(int ModuleKey, long ResourceKey)
        {
            string link = "";
            try
            {
                string storedProcedure = "site_Portal_SelectByResourceKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        commandWrapper.AddInputParameter("@ResourceKey", SqlDbType.Int, ResourceKey);
                        var reader = db.ExecuteReader(commandWrapper);
                        reader.Read();

                        int portalkey = reader.GetValueInt("PortalKey");
                        if (portalkey == 1)
                        {
                            if (ModuleKey == 200)
                            {
                                link = "/VendorInvoice/vendorRefundIndex";
                            }
                            else if(ModuleKey == 705)
                            {
                                link = "/VendorManager/UnapprovedVendorList";
                            }
                            
                        }
                        else if (portalkey == 2)
                        {
                            if (ModuleKey == 100)
                                link = "/PMBidRequests/PMBidRequestList";
                            else if(ModuleKey == 106)
                                link = "/PMWorkOrders/PMWorkOrdersList";
                        }
                        else if(portalkey == 3)
                        {
                            if(ModuleKey == 100)
                            {
                                link = "/VenderBidRequest/BidRequests";
                            }
                            else if(ModuleKey == 106)
                            {
                                link = "/VenderWorkOrders/VenderWorkorder";
                            }
                            else if(ModuleKey == 302)
                            {
                                link = "/VendorPolicy/PolicyList";
                            }
                            else if(ModuleKey == 301)
                            {
                                link = "/vBilling/vBillingList";
                            }
                            else if(ModuleKey == 300)
                            {
                                link = "/VendorMembership/MemberShipFind";
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                link = "#";
            }
            return link;
        }

        private int GetPortalFromResourceKey(long ResourceKey)
        {
            int portalkey = 0;
            try
            {
                string storedProcedure = "site_Portal_SelectByResourceKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        commandWrapper.AddInputParameter("@ResourceKey", SqlDbType.Int, ResourceKey);
                        var reader = db.ExecuteReader(commandWrapper);
                        reader.Read();

                        portalkey = reader.GetValueInt("PortalKey");

                    }
                }
            }
            catch (Exception ex)
            {
                portalkey = 0;
            }
            return portalkey;
        }


        private int GetBidVendorIdResourceKey(long ResourceKey, long bidrequestkey)
        {
            int bidkey = 0;
            try
            {
                string storedProcedure = "site_Bidvendor_SelectByResourceKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        commandWrapper.AddInputParameter("@ResourceKey", SqlDbType.Int, ResourceKey);
                        commandWrapper.AddInputParameter("@BidrequestKey", SqlDbType.Int, bidrequestkey);
                      
                        var reader = db.ExecuteReader(commandWrapper);
                        reader.Read();

                        bidkey = reader.GetValueInt("bvkey");

                    }
                }
            }
            catch (Exception ex)
            {
                bidkey = 0;
            }
            return bidkey;
        }

        private string GetNotificationTitle(string NotificationType,int ModuleKey)
        {
            string title = "";

            //switch (NotificationType)
            //{
            //    case "RefundReq":title = "Refund Request";
            //            break;
            //    case "RefundReqMsg": title = "Refund Request Message";
            //        break;
            //    default: title = "New Notification";
            //        break;
            //}
            switch (ModuleKey)
            {
                case 200:
                    title = "Refund Request";
                    break;
                case 705:
                    title = "Vendor Registration";
                    break;
                case 100:
                    title = "Bid Request";
                    break;
                case 106:
                    title = "Work Order";
                    break;
                case 302:
                    title = "Insurance Expiration";
                    break;
                case 301:
                    title = "Credit Card Expiration";
                    break;
                case 300:
                    title = "Membership Expiration";
                    break;
                default:
                    title = "New Notification";
                    break;
            }
            return title;
        }

        private string GenerateNotificationText(string NotificationType, int ModuleKey,string ByResource,string BidVendorStatus)
        {
            string title = "";

            //switch (NotificationType)
            //{
            //    case "RefundReq":title = "Refund Request";
            //            break;
            //    case "RefundReqMsg": title = "Refund Request Message";
            //        break;
            //    default: title = "New Notification";
            //        break;
            //}
            switch (NotificationType)
            {
                case "BidReqMsg":
                    title = "You have a Message from " + ByResource;
                    break;
                case "BidReqStatus":
                    title = "You have New Bid Request from " + ByResource;
                    break;
                case "BidReqStatusAccept":
                    title = "Your Bid has been accepted by " + ByResource;
                    break;
                case "BidReqStatusReject":
                    title = "Your Bid has been rejected by " + ByResource; ;
                    break;
                case "BidReqStatusRejByAcceptOther":
                    title = "Your Bid has been automatic rejected by accepting other vendor Bid";
                    break;
                case "BidVendorStatus":
                    title = BidVendorStatus;
                    break;
                case "RefundReqMsg":
                    title = "You have New Refund Request Message from " + ByResource;
                    break;
                case "RefundReq":
                    title = "You have new Refund Request from " + ByResource;
                    break;
                case "MembershipExpiry":
                    title = "Your Membership is expired.";
                    break;
                case "InsuranceExpired":
                    title = "Your Insurance is expired";
                    break;
                case "InsuranceExpiry":
                    title = "Your Insurance is expire soon.";
                    break;
                case "VendorReg":
                    title = "New Vendor has Registered.";
                    break;
                case "CCExpiry":
                    title = "Your Credit Card is expired.";
                    break;
                default:
                    title = "You have new Notification.";
                    break;
            }
            return title;
        }


        protected List<long> GetResourceByNotificationTypeVendor(string NotificationType,int ModuleKey,long ObjectKey)
        {
            List<long> resources = new List<long>();

            try
            {
                string storedProcedure = "site_ABNotification_GetResourceByType";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        commandWrapper.AddInputParameter("@NotificationType", SqlDbType.VarChar, NotificationType);
                        commandWrapper.AddInputParameter("@ObjectKey", SqlDbType.Int, ObjectKey);
                        commandWrapper.AddInputParameter("@ModuleKey", SqlDbType.Int, ModuleKey);
                        var reader = db.ExecuteReader(commandWrapper);
                        while (reader.Read())
                        {
                            int ResourceKey = reader.GetValueInt("ResourceKey");
                            resources.Add(ResourceKey);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resources = null;
            }

            return resources;
        }


        protected List<long> GetResourceByNotificationTypePM(string NotificationType, int ModuleKey, long ObjectKey,long ForResourceKey)
        {
            List<long> resources = new List<long>();

            try
            {
                BidVendorRepository bv = new BidVendorRepository();
                VendorManagerRepository vmr = new VendorManagerRepository();
                BidRequestRepository b = new BidRequestRepository();
                if (NotificationType == "BidReqMsg")
                {
                    var bidVendor = bv.Get(Convert.ToInt32(ObjectKey));
                    var vm = vmr.GetResourceForInviteVendor(bidVendor.VendorKey);
                    ForResourceKey = vm.ResourceKey;
                    resources.Add(ForResourceKey);
                }
                else if (NotificationType == "BidReqStatus")
                {
                    if (ForResourceKey == 0)
                    {
                        var bvList = b.SearchVendorByBidRequest(Convert.ToInt32(ObjectKey), ModuleKey, ForResourceKey);
                        bvList.ForEach(f => resources.Add(f.VendorKey));
                        resources = new List<long>();
                        foreach (var b1 in bvList)
                        {
                            var vm = vmr.GetResourceByCompanyKeyForNotification(b1.VendorKey);
                            resources.Add(vm.ResourceKey);
                        }
                    }
                    else
                        resources.Add(ForResourceKey);
                }
                else if (NotificationType == "BidReqStatusAccept")
                {
                    //var bidVendor = bv.Get(Convert.ToInt32(ObjectKey));

                    var vm = new ResourceModel();// vmr.GetResourceForInviteVendor(bidVendor.VendorKey);
                    //ForResourceKey = vm.ResourceKey;
                    //resources.Add(ForResourceKey);
                    //if (resources.Count > 0)
                    {
                        var bvList = b.SearchVendorByBidRequest(Convert.ToInt32(ObjectKey), 106);
                        foreach (var b1 in bvList)
                        {
                            vm = vmr.GetResourceForInviteVendor(b1.VendorKey);
                            if (vm != null)
                                resources.Add(vm.ResourceKey);
                        }
                    }
                }
                else if (NotificationType == "BidReqStatusRejByAcceptOther")
                {
                    var bidVendor = bv.Get(Convert.ToInt32(ObjectKey));
                    var bvList = b.SearchVendorByBidRequest(Convert.ToInt32(bidVendor.BidRequestKey), ModuleKey);
                    foreach (var b1 in bvList)
                    {
                        var vm = vmr.GetResourceForInviteVendor(b1.VendorKey);
                                                
                        if (vm != null)
                        {
                            
                            var noti = GetABNotificationsAllByModuleAndType(vm.ResourceKey, ModuleKey, "");
                            noti = noti.Where(w => (w.NotificationType == "BidReqStatus" || w.NotificationType == "BidReqMsg") && w.Status == "900" ).ToList();
                            if(noti != null && noti.Count > 0)
                            {
                                foreach(var n in noti)
                                {
                                    UpdateStatus(n.Id, "read");
                                }
                            }
                        }
                        if (b1.VendorKey == bidVendor.VendorKey)
                            continue;
                        
                        resources.Add(vm.ResourceKey);
                    }
                    UpdateStatusForAcceptedBid(ForResourceKey, bidVendor.BidRequestKey, ModuleKey, "read");

                }
                else if (NotificationType == "BidReqStatusReject")
                {
                    var bidVendor = bv.Get(Convert.ToInt32(ObjectKey));
                    var vm = vmr.GetResourceForInviteVendor(bidVendor.VendorKey);
                    ForResourceKey = vm.ResourceKey;
                    resources.Add(ForResourceKey);
                }
            }
            catch (Exception ex)
            {
                resources = null;
            }

            return resources;
        }

        protected List<long> GetResourceByNotificationTypeAdmin(string NotificationType, int ModuleKey, long ObjectKey)
        {
            List<long> resources = new List<long>();

            try
            {
                string storedProcedure = "site_ABNotification_GetResourceByTypeAdmin";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        commandWrapper.AddInputParameter("@NotificationType", SqlDbType.VarChar, NotificationType);
                        commandWrapper.AddInputParameter("@ObjectKey", SqlDbType.Int, ObjectKey);
                        commandWrapper.AddInputParameter("@ModuleKey", SqlDbType.Int, ModuleKey);
                        var reader = db.ExecuteReader(commandWrapper);
                        while (reader.Read())
                        {
                            int ResourceKey = reader.GetValueInt("ResourceKey");
                            resources.Add(ResourceKey);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resources = null;
            }

            return resources;
        }
        public List<ABNotificationModel> GetABNotificationsFive(long ResourceKey)
        {
            List<ABNotificationModel> items = new List<ABNotificationModel>();
            try
            {
                string storedProcedure = "site_ABNotification_SelectTopFiveNew";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        int count = 0;
                        commandWrapper.AddInputParameter("@ResourceKey", SqlDbType.Int, ResourceKey);
                        var reader = db.ExecuteReader(commandWrapper);
                        while (reader.Read())
                        {
                            ABNotificationModel noti = new ABNotificationModel();
                            noti.Id = reader.GetValueInt("Id");
                            noti.NotificationType = reader.GetValueText("NotificationType");
                            noti.ModuleKey = reader.GetValueInt("ModuleKey");
                            noti.ObjectKey = reader.GetValueInt("ObjectKey");
                            noti.ByResource = reader.GetValueInt("ByResource");
                            noti.ForResource = reader.GetValueInt("ForResource");
                            noti.NotificationText = reader.GetValueText("NotificationText");
                            noti.DateAdded = reader.GetValueDateTime("DateAdded");
                            noti.LastModificationDate = reader.GetValueDateTime("LastModificationDate");
                            noti.Status = reader.GetValueText("StatusTitle");
                            noti.resLink = GenerateLink(noti.ModuleKey, noti.ObjectKey, ResourceKey,noti.NotificationType);
                            noti.ByCompanyName = reader.GetValueText("ByCompanyName");
                            noti.ByVendorName = reader.GetValueText("ByVendorName");
                            //noti.NotificationText += " By "+noti.ByVendorName;
                            noti.NotificationText = GenerateNotificationText(noti.NotificationType,noti.ModuleKey,noti.ByVendorName, noti.NotificationText);
                            noti.Title = GetNotificationTitle(noti.NotificationType,noti.ModuleKey);
                            if(noti.Status == "New")
                                count++;
                            items.Add(noti);
                        }
                        items.ToList().ForEach(f => f.NewNotificationCount = count);
                    }
                }
            }
            catch (Exception ex)
            {
                items = null;
            }

            return items;
        }

        public string GetFiveNotificationsForVendorFiveNew(long ResourceKey)
        {
            try
            {
                //string items = "";
                //string storedProcedure = "site_ABNotification_SelectTopFiveNew";
                //using (Database db = new Database(ConnectionString))
                //{
                //    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                //    {
                //        int count = 0;
                //        commandWrapper.AddInputParameter("@ResourceKey", SqlDbType.Int, ResourceKey);
                //        var reader = db.ExecuteReader(commandWrapper);
                //        while (reader.Read())
                //        {
                //            noti.Id = reader.GetValueInt("Id");
                //            noti.NotificationType = reader.GetValueText("NotificationType");
                //            noti.ModuleKey = reader.GetValueInt("ModuleKey");
                //            noti.ObjectKey = reader.GetValueInt("ObjectKey");
                //            noti.ByResource = reader.GetValueInt("ByResource");
                //            noti.ForResource = reader.GetValueInt("ForResource");
                //            noti.NotificationText = reader.GetValueText("NotificationText");
                //            noti.DateAdded = reader.GetValueDateTime("DateAdded");
                //            noti.LastModificationDate = reader.GetValueDateTime("LastModificationDate");
                //            noti.Status = reader.GetValueText("StatusTitle");
                //            noti.resLink = GenerateLink(noti.ModuleKey, noti.ObjectKey, ResourceKey, noti.NotificationType);
                //            noti.ByCompanyName = reader.GetValueText("ByCompanyName");
                //            noti.ByVendorName = reader.GetValueText("ByVendorName");
                //            //noti.NotificationText += " By "+noti.ByVendorName;
                //            noti.NotificationText = GenerateNotificationText(noti.NotificationType, noti.ModuleKey, noti.ByVendorName, noti.NotificationText);
                //            noti.Title = GetNotificationTitle(noti.NotificationType, noti.ModuleKey);
                //            if (noti.Status == "New")
                //                count++;
                //            items.Add(noti);
                //        }
                //        items.ToList().ForEach(f => f.NewNotificationCount = count);
                //    }
                //}
                obj_con.clearParameter();               
                obj_con.addParameter("@ResourceKey", ResourceKey);
                DataTable dt = obj_con.ConvertDatareadertoDataTable(obj_con.ExecuteReader("site_Notification_SelectTopFiveNew", CommandType.StoredProcedure));
                obj_con.CommitTransaction();
                obj_con.closeConnection();
                return AssoBidsUtility.ConvertDataTableTojSonString(dt);
            }
            catch (Exception ex)
            {
                throw new Exception("site_ABNotification_SelectTopFiveNew" + ex.ToString());

            }           
        }
        public List<ABNotificationModel> GetABNotificationsModule(long ResourceKey)
        {
            List<ABNotificationModel> items = new List<ABNotificationModel>();
            try
            {
                string storedProcedure = "site_ABNotification_GetNotificationModule";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        int count = 0;
                        commandWrapper.AddInputParameter("@ResourceKey", SqlDbType.Int, ResourceKey);
                        var reader = db.ExecuteReader(commandWrapper);
                        while (reader.Read())
                        {
                            ABNotificationModel noti = new ABNotificationModel();
                            noti.ModuleKey = reader.GetValueInt("ModuleKey");
                            noti.Status = reader.GetValueText("Title");
                            noti.resLink = GenerateLinkNew(noti.ModuleKey, ResourceKey);
                            noti.Title = GetNotificationTitle("", noti.ModuleKey);
                            noti.NewNotificationCount = reader.GetValueInt("Total");
                            if (noti.Status == "New")
                                count += noti.NewNotificationCount;
                            items.Add(noti);
                        }
                        items.ToList().ForEach(f => f.TotalCount = count);
                    }
                }
            }
            catch (Exception ex)
            {
                items = null;
            }

            return items;
        }
        public List<ABNotificationModel> GetABNotificationsAll(long ResourceKey)
        {
            List<ABNotificationModel> items = new List<ABNotificationModel>();
            try
            {
                string storedProcedure = "site_ABNotification_SelectAll";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        commandWrapper.AddInputParameter("@ResourceKey", SqlDbType.Int, ResourceKey);
                        var reader = db.ExecuteReader(commandWrapper);
                        while (reader.Read())
                        {
                            ABNotificationModel noti = new ABNotificationModel();
                            noti.Id = reader.GetValueInt("Id");
                            noti.NotificationType = reader.GetValueText("NotificationType");
                            noti.ModuleKey = reader.GetValueInt("ModuleKey");
                            noti.ObjectKey = reader.GetValueInt("ObjectKey");
                            noti.ByResource = reader.GetValueInt("ByResource");
                            noti.ForResource = reader.GetValueInt("ForResource");
                            noti.NotificationText = reader.GetValueText("NotificationText");
                            noti.DateAdded = reader.GetValueDateTime("DateAdded");
                            noti.LastModificationDate = reader.GetValueDateTime("LastModificationDate");
                            noti.Status = reader.GetValueText("Status");
                            items.Add(noti);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                items = null;
            }

            return items;
        }

        public List<ABNotificationModel> GetABNotificationsAllByModuleAndType(long ResourceKey,int ModuleKey,string NotificationType)
        {
            List<ABNotificationModel> items = new List<ABNotificationModel>();
            try
            {
                string storedProcedure = "site_ABNotification_SelectAllByModuleAndType";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        commandWrapper.AddInputParameter("@ResourceKey", SqlDbType.Int, ResourceKey);
                        commandWrapper.AddInputParameter("@ModuleKey", SqlDbType.Int, ModuleKey);
                        commandWrapper.AddInputParameter("@NotificationType", SqlDbType.VarChar, NotificationType);
                        var reader = db.ExecuteReader(commandWrapper);
                        while (reader.Read())
                        {
                            ABNotificationModel noti = new ABNotificationModel();
                            noti.Id = reader.GetValueInt("Id");
                            noti.NotificationType = reader.GetValueText("NotificationType");
                            noti.ModuleKey = reader.GetValueInt("ModuleKey");
                            noti.ObjectKey = reader.GetValueInt("ObjectKey");
                            noti.ByResource = reader.GetValueInt("ByResource");
                            noti.ForResource = reader.GetValueInt("ForResource");
                            noti.NotificationText = reader.GetValueText("NotificationText");
                            noti.DateAdded = reader.GetValueDateTime("DateAdded");
                            noti.LastModificationDate = reader.GetValueDateTime("LastModificationDate");
                            noti.Status = reader.GetValueText("Status");
                            items.Add(noti);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                items = null;
            }

            return items;
        }
        public List<ABNotificationModel> GetABNotificationsAllByObjectAndModule(long ResourceKey, int ModuleKey, long ObjectKey)
        {
            List<ABNotificationModel> items = new List<ABNotificationModel>();
            try
            {
                string storedProcedure = "site_ABNotification_SelectByObjectAndModule";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        commandWrapper.AddInputParameter("@ResourceKey", SqlDbType.Int, ResourceKey);
                        commandWrapper.AddInputParameter("@ModuleKey", SqlDbType.Int, ModuleKey);
                        commandWrapper.AddInputParameter("@ObjectKey", SqlDbType.Int, ObjectKey);
                        var reader = db.ExecuteReader(commandWrapper);
                        while (reader.Read())
                        {
                            ABNotificationModel noti = new ABNotificationModel();
                            noti.Id = reader.GetValueInt("Id");
                            noti.NotificationType = reader.GetValueText("NotificationType");
                            noti.ModuleKey = reader.GetValueInt("ModuleKey");
                            noti.ObjectKey = reader.GetValueInt("ObjectKey");
                            noti.ByResource = reader.GetValueInt("ByResource");
                            noti.ForResource = reader.GetValueInt("ForResource");
                            noti.NotificationText = reader.GetValueText("NotificationText");
                            noti.DateAdded = reader.GetValueDateTime("DateAdded");
                            noti.LastModificationDate = reader.GetValueDateTime("LastModificationDate");
                            noti.Status = reader.GetValueText("Status");
                            items.Add(noti);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                items = null;
            }

            return items;
        }
        public ABNotificationModel GetABNotificationByNotificationId(long ResourceKey,long NotificationId)
        {
            ABNotificationModel item = new ABNotificationModel();
            try
            {
                string storedProcedure = "site_ABNotification_SelectByNotificationId";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        commandWrapper.AddInputParameter("@ResourceKey", SqlDbType.Int, ResourceKey);
                        commandWrapper.AddInputParameter("@NotificationId", SqlDbType.Int, NotificationId);
                        var reader = db.ExecuteReader(commandWrapper);
                        var read = reader.Read();
                        if(read)
                        {
                            item.Id = reader.GetValueInt("Id");
                            item.NotificationType = reader.GetValueText("NotificationType");
                            item.ModuleKey = reader.GetValueInt("ModuleKey");
                            item.ObjectKey = reader.GetValueInt("ObjectKey");
                            item.ByResource = reader.GetValueInt("ByResource");
                            item.ForResource = reader.GetValueInt("ForResource");
                            item.NotificationText = reader.GetValueText("NotificationText");
                            item.DateAdded = reader.GetValueDateTime("DateAdded");
                            item.LastModificationDate = reader.GetValueDateTime("LastModificationDate");
                            item.Status = reader.GetValueText("Status");
                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                item = null;
            }

            return item;
        }

        public bool UpdateStatus(long NotificationId,string Status)
        {
            bool status = false;

            try
            {
                string storedProcedure = "site_ABNotification_UpdateStatus";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        commandWrapper.AddInputParameter("@Id", SqlDbType.Int, NotificationId);
                        commandWrapper.AddInputParameter("@Status", SqlDbType.VarChar, Status);
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);
                        db.ExecuteNonQuery(commandWrapper);
                        if (commandWrapper.GetValueInt("@errorCode") == 0)
                            status = true;
                    }
                }
            }
            catch (Exception ex)
            {
                status = false;
            }

            return status;
        }

        public bool UpdateStatusByObjectKey(long ObjectKey, string Status,string NotiType)
        {
            bool status = false;

            try
            {
                string storedProcedure = "site_ABNotification_UpdateStatusByObjectKeyAndType";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        commandWrapper.AddInputParameter("@ObjectKey", SqlDbType.Int, ObjectKey);
                        commandWrapper.AddInputParameter("@Status", SqlDbType.VarChar, Status);
                        commandWrapper.AddInputParameter("@NotificationType", SqlDbType.VarChar, NotiType);
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);
                        db.ExecuteNonQuery(commandWrapper);
                        if (commandWrapper.GetValueInt("@errorCode") == 0)
                            status = true;
                    }
                }
            }
            catch (Exception ex)
            {
                status = false;
            }

            return status;
        }

        private bool UpdateStatusForAcceptedBid(long ResourceKey, long ObjectKey, int ModuleKey,string Status)
        {
            bool status = false;

            try
            {
                string storedProcedure = "site_ABNotification_UpdateAcceptedBid";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        commandWrapper.AddInputParameter("@ResourceKey", SqlDbType.Int, ResourceKey);
                        commandWrapper.AddInputParameter("@ObjectKey", SqlDbType.Int, ObjectKey);
                        commandWrapper.AddInputParameter("@ModuleKey", SqlDbType.Int, ModuleKey);
                        commandWrapper.AddInputParameter("@Status", SqlDbType.VarChar, Status);
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);
                        db.ExecuteNonQuery(commandWrapper);
                        if (commandWrapper.GetValueInt("@errorCode") == 0)
                            status = true;
                    }
                }
            }
            catch (Exception ex)
            {
                status = false;
            }

            return status;
        }

        public string GetFiveNotificationsForManagerFiveNew(long ResourceKey, int BIDSUBMITDAYS)
        {
            try
            {
                obj_con.clearParameter();
                obj_con.addParameter("@ResourceKey", ResourceKey);
                //obj_con.addParameter("@ResourceKey", ResourceKey);
                DataTable dt = obj_con.ConvertDatareadertoDataTable(obj_con.ExecuteReader("site_ABNotification_SelectTopFiveNew", CommandType.StoredProcedure));
              
                obj_con.CommitTransaction();
                obj_con.closeConnection();
                return AssoBidsUtility.ConvertDataTableTojSonString(dt);
            }
            catch (Exception ex)
            {
                throw new Exception("site_ABNotification_SelectTopFiveNew" + ex.ToString());

            }
        }

        public string RequestSendOrNot(long ResourceKey, string BidName)
        {
            try
            {
                string Status = "True";
                obj_con.clearParameter();
                obj_con.addParameter("@ResourceKey", ResourceKey);
                //obj_con.addParameter("@BidName", BidName);
                DataTable dt = obj_con.ConvertDatareadertoDataTable(obj_con.ExecuteReader("site_ABNotification_SelectRequestSendOrNot", CommandType.StoredProcedure));
                obj_con.CommitTransaction();
                obj_con.closeConnection();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string BidNames = dt.Rows[i]["BidName"].ToString();
                    if (BidNames == BidName) 
                    {
                        Status = "False";
                        break;
                    }
                    else
                    {
                        Status = "True";
                    }

                }
                //return AssoBidsUtility.ConvertDataTableTojSonString(dt);
                return Status;
            }
            catch (Exception ex)
            {
                throw new Exception("site_ABNotification_SelectRequestSendOrNot" + ex.ToString());

            }
        }

    }
}
