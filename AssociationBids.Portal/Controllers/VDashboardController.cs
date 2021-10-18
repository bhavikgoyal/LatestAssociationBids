using AssociationBids.Portal.Model;
using AssociationBids.Portal.Service.Base;
using AssociationBids.Portal.Service.Base.Code;
using AssociationBids.Portal.Service.Base.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AssociationBids.Portal.Controllers
{
    public class VDashboardController : Controller
    {
        private IVendorPolicyService _vendorPolicy;

        public VDashboardController(IVendorPolicyService policyService)
        {
            _vendorPolicy = policyService;
        }

        public ActionResult Index()
        {
            long ResourceKey = Convert.ToInt64(Session["resourceid"]);
            long CompanyKey = Convert.ToInt64(Session["CompanyKey"]);

            CheckForExipiryDocuments(ResourceKey, CompanyKey);



            return RedirectToAction("Index", "VenderBidrequest");
        }


        public ActionResult CompleteInsurance()
        {
           
            return View();
        }
        [HttpPost]
        public ActionResult CompleteInsurance(VendorManagerModel model, HttpPostedFileBase[] files)
        {
            int CompanyKey = Convert.ToInt32(Session["companykey"]);
            VendorManagerVendorModel vm = _vendorPolicy.GetVendorByCompanyKey(CompanyKey);
            IDocumentService document = new DocumentService();
            model.Insurance.VendorKey = CompanyKey;
            model.Insurance.CompanyName = vm.CompanyName;
            model.Insurance.Address = vm.Address;
            model.Insurance.Address2 = vm.Address2;
            model.Insurance.CellPhone = vm.CellPhone;
            model.Insurance.City = vm.City;
            model.Insurance.CompanyName = vm.CompanyName;
            model.Insurance.Email = vm.Email;
            model.Insurance.Fax = vm.Fax;
            model.Insurance.State = vm.State;
            model.Insurance.Work = vm.Work;
            model.Insurance.Zip = vm.Zip;
            long iKey = _vendorPolicy.VendorManagerAddInsurance(model.Insurance);
            if (files != null && iKey != 0)
            {
                if (files.Length > 0 && files[0] != null)
                {
                    foreach (var file in files)
                    {
                        var module = new ModuleService().GetAll(new ModuleFilterModel());
                        var key = module.Where(w => w.Title == "Insurance").FirstOrDefault().ModuleKey;
                        DocumentModel dm = new DocumentModel();
                        dm.ObjectKey = Convert.ToInt32(iKey);
                        dm.ModuleKey = key;
                        dm.FileName = file.FileName;
                        dm.FileSize = file.ContentLength;
                        dm.LastModificationTime = DateTime.Now;
                        document.Create(dm);
                        Directory.CreateDirectory(Server.MapPath("~/Document/Insurance/" + iKey));
                        file.SaveAs(Server.MapPath("~/Document/Insurance/" + iKey + file.FileName));
                    }
                }
            }
            return RedirectToAction("Index", "VDashboard");
        }

        public ActionResult Skipins()
        {
            return RedirectToAction("Index", "VDashboard");
        }


        private void CheckForExipiryDocuments(long ResourceKey,long CompanyKey)
        {
            IVendorPolicyService policyService = new VendorPolicyService();
            IABNotificationService notificationService = new ABNotificationService();
            var insurance = policyService.GetInsurancePaging(Convert.ToInt32(CompanyKey), 1000, 1, "", "order by EndDate desc");
            if(insurance != null && insurance.Count > 0)
            {
                for (int i = 0; i < insurance.Count; i++)
                {
                    if (insurance[i].EndDate.AddDays(-30) < DateTime.Now && insurance[i].EndDate > DateTime.Now && insurance[i].StartDate < DateTime.Now)
                    {
                        var Noti = notificationService.GetABNotificationsAllByModuleAndType(ResourceKey, 302, "");
                        Model.ABNotificationModel data = null;
                        if(Noti != null)
                            data = Noti.Where(w => w.ObjectKey == insurance[i].InsuranceKey).FirstOrDefault();
                        if (data == null)
                        {
                            notificationService.InsertNotification("InsuranceExpiry", 302, insurance[i].InsuranceKey, ResourceKey, "Insurance is about to expire.", ResourceKey);
                        }

                    }
                    else if (insurance[i].EndDate < DateTime.Now)
                    {
                        var Noti = notificationService.GetABNotificationsAllByModuleAndType(ResourceKey, 302, "InsuranceExpired");
                        Model.ABNotificationModel data = null;
                        if (Noti != null) 
                            data = Noti.Where(w => w.ObjectKey == insurance[i].InsuranceKey).FirstOrDefault();
                        if (data == null)
                        {
                            notificationService.InsertNotification("InsuranceExpired", 302, insurance[i].InsuranceKey, ResourceKey, "Insurance is expired.", ResourceKey);
                        }
                    }
                }
            }

            IMembershipService membershipService = new MembershipService();
            var member = membershipService.GetDataViewEdit(Convert.ToInt32(CompanyKey));
            if (member != null)
            {
                if (member.EndDate < DateTime.Now && member.AutomaticRenewal == false)
                {
                    var Noti = notificationService.GetABNotificationsAllByModuleAndType(ResourceKey, 300, "MembershipExpired");
                    Model.ABNotificationModel data = null;
                    if (Noti != null)
                        data = Noti.Where(w => w.ObjectKey == member.MembershipKey).FirstOrDefault();
                    if (data == null)
                    {
                        notificationService.InsertNotification("MembershipExpired", 300, member.MembershipKey, ResourceKey, "Membership is expired.", ResourceKey);
                    }
                }
                else if (member.EndDate.AddDays(-30) < DateTime.Now && member.EndDate > DateTime.Now)
                {
                    var Noti = notificationService.GetABNotificationsAllByModuleAndType(ResourceKey, 300, "");
                    Model.ABNotificationModel data = null;
                    if (Noti != null)
                        data = Noti.Where(w => w.ObjectKey == member.MembershipKey).FirstOrDefault();
                    if (data == null)
                    {
                        notificationService.InsertNotification("MembershipExpiry", 300, member.MembershipKey, ResourceKey, "Membership is about to expire.", ResourceKey);
                    }

                }
            }
            IvBillingService billingService = new vBillingService();
            var billing = billingService.LoadBillingListByResurceKey(ResourceKey);
            if(billing.Count > 0)
            {
                foreach(var b in billing)
                {
                    var noti = notificationService.GetABNotificationsAllByModuleAndType(ResourceKey, 301, "");
                    Model.ABNotificationModel data = null;
                    if (noti != null)
                        data = noti.Where(w => w.ObjectKey == b.PaymentMethodKey).FirstOrDefault();
                    if (data != null)
                        continue;
                    int year = 0;
                    int month = 0;
                    
                    DateTime expDate = DateTime.Now.AddMonths(2);
                    try
                    {
                        year = Convert.ToInt32("20"+b.ValidTillYY);
                        month = Convert.ToInt32(b.ValidTillMM);
                        expDate = new DateTime(year, month, 1).AddMonths(1);
                    }
                    catch { }
                    if (expDate.AddMonths(-1) < DateTime.Now && expDate > DateTime.Now)
                    {
                        notificationService.InsertNotification("CCExpiry", 301, b.PaymentMethodKey, ResourceKey, "Credit Card is about to expire.", ResourceKey);
                    }
                    else if(expDate < DateTime.Now)
                    {
                        notificationService.InsertNotification("CCExpired", 301, b.PaymentMethodKey, ResourceKey, "Credit Card is expired.", ResourceKey);
                    }
                }
            }
        }
     
    }
}