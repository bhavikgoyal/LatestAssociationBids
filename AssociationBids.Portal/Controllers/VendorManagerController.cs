using AssociationBids.Portal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AssociationBids.Portal.Service.Base;
using AssociationBids.Portal.Service.Base.Interface;
using AssociationBids.Portal.Repository.Admin;
using System.Web;
using System.IO;
using System.Data.OleDb;
using System.Text.RegularExpressions;
using AssociationBids.Portal.Common;
using AssociationBids.Portal.Service.Base.Code;
using System.Threading.Tasks;

namespace AssociationBids.Portal.Controllers
{
    public class VendorManagerController : Controller
    {
        private readonly AssociationBids.Portal.Service.Base.Interface.IVendorManagerService __vendorManagerservice;
        private readonly AssociationBids.Portal.Service.Base.IResourceService __resourceservice;
        private readonly AssociationBids.Portal.Service.Base.IVendorServiceService __serviceVendor;
        private readonly AssociationBids.Portal.Service.Base.Interface.IMembershipService _mEmberShipService;

        public VendorManagerController()
        {

        }
        
        public VendorManagerController(IVendorManagerService vendorManagerService, IResourceService resourceService, IVendorServiceService service, AssociationBids.Portal.Service.Base.Interface.IMembershipService mEmaberShipSeervice)
        {
            this.__resourceservice = resourceService;
            this.__vendorManagerservice = vendorManagerService;
            this.__serviceVendor = service;
            this._mEmberShipService = mEmaberShipSeervice;
        }


        // GET: VendorManager
        public ActionResult Index()
        {

            //Service

            IList<VendorManagerVendorModel> lstservice = __vendorManagerservice.GetAllService();
            List<System.Web.Mvc.SelectListItem> lstservicelist = new List<System.Web.Mvc.SelectListItem>();

            for (int i = 0; i < lstservice.Count; i++)
            {
                System.Web.Mvc.SelectListItem sli = new System.Web.Mvc.SelectListItem();
                sli.Text = Convert.ToString(lstservice[i].ServiceTitle1);
                sli.Value = Convert.ToString(lstservice[i].ServiceKey);
                lstservicelist.Add(sli);
            }
            ViewBag.lstservice = lstservicelist;

            ////Property
            //IList<VendorModel> lstproperty = __vendorservice.GetAllProperty();
            //List<System.Web.Mvc.SelectListItem> lstpropertylist = new List<System.Web.Mvc.SelectListItem>();

            //for (int i = 0; i < lstproperty.Count; i++)
            //{
            //    System.Web.Mvc.SelectListItem sli = new System.Web.Mvc.SelectListItem();
            //    sli.Text = Convert.ToString(lstproperty[i].Title);
            //    sli.Value = Convert.ToString(lstproperty[i].PropertyKey);
            //    lstpropertylist.Add(sli);
            //}
            //ViewBag.lstproperty = lstpropertylist;
            return View("Vendorlist");
        }
        public ActionResult ViewVendor()
        {
            return View("ViewVendor");
        }
        public ActionResult InvitedVendorsList()
        {
            try
            {
                TempData["Sucessmessage"] = TempData["SMessage"];
                TempData["Error"] = TempData["Er"];
                TempData["Warning"] = TempData["Wa"];
                List<VendorManagerVendorModel> companyList = __vendorManagerservice.VendorManager_GetAllManagementCompany();
                List<System.Web.Mvc.SelectListItem> lstComapny = new List<System.Web.Mvc.SelectListItem>();
                System.Web.Mvc.SelectListItem selectItem = new System.Web.Mvc.SelectListItem();
                selectItem.Text = "--Please Select--";
                selectItem.Value = "0";
                lstComapny.Add(selectItem);
                for (int i = 0; i < companyList.Count; i++)
                {
                    System.Web.Mvc.SelectListItem item = new System.Web.Mvc.SelectListItem();
                    item.Text = Convert.ToString(companyList[i].CompanyName);
                    item.Value = Convert.ToString(companyList[i].CompanyKey);
                    lstComapny.Add(item);
                }
                ViewBag.companyList = lstComapny;
            }
            catch { }

            return View("InvitedVendorlist");
        }
        public ActionResult UnapprovedVendorList()
        {

            return View("UnapprovedVendorList");
        }

        // GET: AddVendor
        public ActionResult InviteVendor()
        {
            try
            {

                IList<VendorManagerVendorModel> lststate = null;
                //list of state
                lststate = __vendorManagerservice.GetAllState();
                List<System.Web.Mvc.SelectListItem> lststatelist = new List<System.Web.Mvc.SelectListItem>();
                System.Web.Mvc.SelectListItem sli2 = new System.Web.Mvc.SelectListItem();
                sli2.Text = "--Please Select--";
                sli2.Value = "0";
                lststatelist.Add(sli2);
                for (int i = 0; i < lststate.Count; i++)
                {
                    System.Web.Mvc.SelectListItem sli = new System.Web.Mvc.SelectListItem();
                    sli.Text = Convert.ToString(lststate[i].State);
                    sli.Value = Convert.ToString(lststate[i].StateKey);
                    lststatelist.Add(sli);
                }
                ViewBag.lststate = lststatelist;

            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
                return View("InviteVendor");
            }

            return View("InviteVendor");
        }

        [HttpPost]
        public ActionResult InviteVendor(VendorManagerVendorModel collection)
        {
            try
            {

                IList<VendorManagerVendorModel> lststate = null;
                //list of state
                lststate = __vendorManagerservice.GetAllState();
                List<System.Web.Mvc.SelectListItem> lststatelist = new List<System.Web.Mvc.SelectListItem>();
                System.Web.Mvc.SelectListItem sli2 = new System.Web.Mvc.SelectListItem();
                sli2.Text = "--Please Select--";
                sli2.Value = "0";
                lststatelist.Add(sli2);
                for (int i = 0; i < lststate.Count; i++)
                {
                    System.Web.Mvc.SelectListItem sli = new System.Web.Mvc.SelectListItem();
                    sli.Text = Convert.ToString(lststate[i].State);
                    sli.Value = Convert.ToString(lststate[i].StateKey);
                    lststatelist.Add(sli);
                }

                List<bool> ErrorMsg = __vendorManagerservice.CheckDuplicatedEmailAndCompanyName(collection.Email, collection.LegalName);
                if (ErrorMsg[0] != true && ErrorMsg[1] != true)
                {
                    Int64 value = 0;
                    int ResourceKey = Convert.ToInt32(Session["resourceid"]);
                    value = __vendorManagerservice.VendorManagerInviteVendor(collection, ResourceKey);
                    if (value != 0)
                    {
                        TempData["Sucessmessage"] = "Invitation Sent.";
                        //TempData["Sucessmessage"] = "Invitation has been sent.";
                        TempData["SMessage"] = "Invitation Sent.";
                    }
                    else
                    {
                        TempData["Error"] = "Invitation not sent Please try Again";
                    }


                    return RedirectToAction("InvitedVendorsList");
                }
                else if (ErrorMsg[0] == true && ErrorMsg[1] == false)
                {
                    ViewBag.lststate = lststatelist;
                    TempData["Error"] = "Email is Already Registered";
                }
                else
                {
                    ViewBag.lststate = lststatelist;
                    TempData["Error"] = "Company Name is Already Registered";

                }
                return View(collection);
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
                return View();
            }
        }

        public ActionResult Update_InviteVendor(int CompanyKey)
        {
            try
            {

                IList<VendorManagerVendorModel> lststate = null;
                //list of state
                lststate = __vendorManagerservice.GetAllState();
                List<System.Web.Mvc.SelectListItem> lststatelist = new List<System.Web.Mvc.SelectListItem>();
                System.Web.Mvc.SelectListItem sli2 = new System.Web.Mvc.SelectListItem();
                sli2.Text = "--Please Select--";
                sli2.Value = "0";
                lststatelist.Add(sli2);
                for (int i = 0; i < lststate.Count; i++)
                {
                    System.Web.Mvc.SelectListItem sli = new System.Web.Mvc.SelectListItem();
                    sli.Text = Convert.ToString(lststate[i].State);
                    sli.Value = Convert.ToString(lststate[i].StateKey);
                    lststatelist.Add(sli);
                }
                ViewBag.lststate = lststatelist;
                VendorManagerVendorModel vm = new VendorManagerVendorModel();
                vm = __vendorManagerservice.GetVendorByCompanyKeyForInviteView(CompanyKey);
                vm.LegalName = vm.CompanyName;
                var resource = __vendorManagerservice.GetResourceForInviteVendor(CompanyKey);
                vm.ContactPerson = resource.FirstName +" " + resource.LastName;
                vm.Work2 = resource.CellPhone;
                vm.Email = resource.Email;
                ViewBag.isForEdit = true;
                ViewBag.cid = resource.CompanyKey;
                ViewBag.rid = resource.ResourceKey;
                return View("InviteVendor",vm);

            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
                return View("InviteVendor");
            }

            
        }

        [HttpPost]
        public ActionResult Update_InviteVendor(VendorManagerVendorModel collection,int CompanyKey,int ResourceKey)
        {
            try
            {

                IList<VendorManagerVendorModel> lststate = null;
                //list of state
                lststate = __vendorManagerservice.GetAllState();
                List<System.Web.Mvc.SelectListItem> lststatelist = new List<System.Web.Mvc.SelectListItem>();
                System.Web.Mvc.SelectListItem sli2 = new System.Web.Mvc.SelectListItem();
                sli2.Text = "--Please Select--";
                sli2.Value = "0";
                lststatelist.Add(sli2);
                for (int i = 0; i < lststate.Count; i++)
                {
                    System.Web.Mvc.SelectListItem sli = new System.Web.Mvc.SelectListItem();
                    sli.Text = Convert.ToString(lststate[i].State);
                    sli.Value = Convert.ToString(lststate[i].StateKey);
                    lststatelist.Add(sli);
                }

                bool isEmailValid = __vendorManagerservice.CheckDuplicatedEmailByResourceKey(collection.Email,ResourceKey);
                bool isCompanyValid = __vendorManagerservice.CheckDuplicatedCompanyNameByCompanyKey(collection.LegalName,CompanyKey);
                if (isEmailValid != true && isCompanyValid != true)
                {
                    Int64 value = 0;
                    collection.VendorKey = ResourceKey;
                    value = __vendorManagerservice.VendorManager_Update_InviteVendor(collection);
                    if (value != 0)
                    {
                        TempData["Sucessmessage"] = "Invitation resent.";
                        TempData["SMessage"] = "Invitation resent.";
                    }
                    else
                    {
                        TempData["Er"] = "Invitation not resent.";
                        TempData["Error"] = "Invitation not resent Please Try Again";
                    }
                    //return RedirectToAction("InvitedVendorView", new { CompanyKey });
                    return RedirectToAction("InvitedVendorsList");

                }
                else if (isEmailValid == true && isCompanyValid == false)
                {
                    ViewBag.lststate = lststatelist;
                    TempData["Error"] = "Email is Already Registered";
                }
                else
                {
                    ViewBag.lststate = lststatelist;
                    TempData["Error"] = "Company Name is Already Registered";

                }
                return RedirectToAction("InvitedVendorView", new { CompanyKey });
                
            
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
                return View();
            }
        }

        public ActionResult AddPolicy(int CompanyKey)
        {
            ViewBag.cid = CompanyKey;
            return View();
        }
        public ActionResult AddPolicyVendor()
        {
            //ViewBag.cid = CompanyKey;
            return View();
        }

        public ActionResult VendorView(int CompanyKey = 0)
        {
            if (CompanyKey == 0)
                return RedirectToAction("Index");

            VendorManagerVendorModel vendorv = new VendorManagerVendorModel();
            ResourceModel resourcem = new ResourceModel();
            VendorManagerModel vm = new VendorManagerModel();

            try
            {
                IList<VendorManagerVendorModel> lstservice = null;
                VendorManagerVendorModel vendor = new VendorManagerVendorModel();

                var lstRadiuslist = new List<SelectListItem>()
            {
              new SelectListItem{ Value="0",Text="Please Select",Selected=true},
              new SelectListItem{ Value="1",Text="10 miles"},
              new SelectListItem{ Value="2",Text="15 miles"},
              new SelectListItem{ Value="3",Text="20 miles"},
              new SelectListItem{ Value="4",Text="25 miles"},
            };
                ViewBag.lstRadius = lstRadiuslist;

         
                //Service
                lstservice = __vendorManagerservice.GetAllService();
                List<System.Web.Mvc.SelectListItem> lstservicelist = new List<System.Web.Mvc.SelectListItem>();

                for (int i = 0; i < lstservice.Count; i++)
                {
                    System.Web.Mvc.SelectListItem sli = new System.Web.Mvc.SelectListItem();
                    sli.Text = Convert.ToString(lstservice[i].ServiceTitle1);
                    sli.Value = Convert.ToString(lstservice[i].ServiceKey);
                    lstservicelist.Add(sli);
                }
                ViewBag.lstservice = lstservicelist;
                vendor = __vendorManagerservice.GetVendorByCompanyKey(CompanyKey);
                resourcem = __vendorManagerservice.GetResourceByCompanyKey(CompanyKey);
                if (vendor != null)
                {

                    ViewBag.Address = vendor.Address;
                    //if (vendor.Address2 == null)
                    //{
                    //    vendor.Address2 = "";
                    //}
                    ViewBag.Address2 = vendor.Address2;
                    ViewBag.City = vendor.City;
                    ViewBag.Zip = vendor.Zip;
                    ViewBag.State = vendor.State;
                }
                if (resourcem == null)
                    resourcem = new ResourceModel();

                IList<VendorManagerModel> docs = new List<VendorManagerModel>();
                docs = __vendorManagerservice.GetbindDocument(CompanyKey);
                List<InsuranceModel> insList = __vendorManagerservice.GetInsuranceByCompanyKey(CompanyKey);
                ViewBag.InsuranceList = insList;
                ViewBag.InsuranceDocs = docs;
                vm.Vendor = vendor;
                vm.Resource = resourcem;
                if (vm.Resource != null)
                {
                    VendorManagerModel managerModel = __vendorManagerservice.GetUserPrimaryByCompanyKeyAndResourceKey(CompanyKey, vm.Resource.ResourceKey);
                    if (managerModel.UserModel != null)
                    {
                        vm.UserModel = managerModel.UserModel;
                        try
                        {
                            vm.UserModel.Password = Common.Security.Decrypt(managerModel.UserModel.Password);
                        }
                        catch
                        {
                            vm.UserModel.Password = "1";
                        }
                    }
                }
                else
                    vm.Resource = new ResourceModel();
                if (vm.Vendor == null)
                    vm.Vendor = new VendorManagerVendorModel();
                   vm.Radius = resourcem.Radius;
                return View(vm);
                
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
                return View(new VendorManagerModel());
            }
        }

        public ActionResult InvitedVendorView(int CompanyKey = 0)
        {
            if (CompanyKey == 0)
                return RedirectToAction("InvitedVendorsList");
            VendorManagerVendorModel vendorv = new VendorManagerVendorModel();
            ResourceModel resourcem = new ResourceModel();
            VendorManagerModel vm = new VendorManagerModel();

            VendorManagerVendorModel vendor = new VendorManagerVendorModel();


            IList<VendorManagerVendorModel> lstservice = null;
           

            var lstRadiuslist = new List<SelectListItem>()
            {
              new SelectListItem{ Value="0",Text="Please Select",Selected=true},
              new SelectListItem{ Value="1",Text="10 miles"},
              new SelectListItem{ Value="2",Text="15 miles"},
              new SelectListItem{ Value="3",Text="20 miles"},
              new SelectListItem{ Value="4",Text="25 miles"},
            };
            ViewBag.lstRadius = lstRadiuslist;


            vendor = __vendorManagerservice.GetVendorByCompanyKeyForInviteView(CompanyKey);
            resourcem = __vendorManagerservice.GetResourceForInviteVendor(CompanyKey);
            if(vendor != null)
                vendor.Work2 = vendor.CellPhone;
            IList<VendorManagerModel> docs = new List<VendorManagerModel>();
            docs = __vendorManagerservice.GetbindDocument(CompanyKey);

            if (vendor != null)
            {


                ViewBag.Address = vendor.Address;
                //if (vendor.Address2 == null)
                //{
                //    vendor.Address2 = "";
                //}
                ViewBag.Address2 = vendor.Address2;
                ViewBag.City = vendor.City;
                ViewBag.Zip = vendor.Zip;
                ViewBag.State = vendor.State;
            }

            List<InsuranceModel> insList = __vendorManagerservice.GetInsuranceByCompanyKey(CompanyKey);
            ViewBag.InsuranceList = insList;
            ViewBag.InsuranceDocs = docs;
            vm.Vendor = vendor;
            vm.Resource = resourcem;
            if (vm.Resource != null)
            {
                VendorManagerModel managerModel = __vendorManagerservice.GetUserPrimaryByCompanyKeyAndResourceKey(CompanyKey, vm.Resource.ResourceKey);
                if (managerModel.UserModel != null)
                {
                    vm.UserModel = managerModel.UserModel;
                    try
                    {
                        vm.UserModel.Password = Common.Security.Decrypt(managerModel.UserModel.Password);
                    }
                    catch
                    {
                        vm.UserModel.Password = "1";
                    }
                }
            }
            else
                vm.Resource = new ResourceModel();
            if (vm.Vendor == null)
                vm.Vendor = new VendorManagerVendorModel();
            vm.Radius = resourcem.Radius;
            return View(vm);


        }

        public ActionResult UnapprovedVendorView(int CompanyKey = 0)
        {
            if (CompanyKey == 0)
                return RedirectToAction("UnapprovedVendorList");
            VendorManagerVendorModel vendorv = new VendorManagerVendorModel();
            ResourceModel resourcem = new ResourceModel();
            VendorManagerModel vm = new VendorManagerModel();
            var lstRadiuslist = new List<SelectListItem>()
                    {
                    //new SelectListItem{ Value="0",Text="Please Select",Selected=true},
                    new SelectListItem{ Value="1",Text="10 miles"},
                    new SelectListItem{ Value="2",Text="15 miles"},
                    new SelectListItem{ Value="3",Text="20 miles"},
                    new SelectListItem{ Value="4",Text="25 miles"},
                    };

            ViewBag.lstRadius = lstRadiuslist;

            VendorManagerVendorModel vendor = new VendorManagerVendorModel();

            vendor = __vendorManagerservice.GetUnapprovedVendorByCompanyKey(CompanyKey);
            resourcem = __vendorManagerservice.GetResourceByCompanyKey(CompanyKey);

            if (vendor != null)
            {


                ViewBag.Address = vendor.Address;
                //if (vendor.Address2 == null)
                //{
                //    vendor.Address2 = "";
                //}
                ViewBag.Address2 = vendor.Address2;
                ViewBag.City = vendor.City;
                ViewBag.Zip = vendor.Zip;
                ViewBag.State = vendor.State;
                ViewBag.Radius1 = vendor.Radius;
            }
            IList<VendorManagerModel> docs = new List<VendorManagerModel>();
            docs = __vendorManagerservice.GetbindDocument(CompanyKey);
            List<InsuranceModel> insList = __vendorManagerservice.GetInsuranceByCompanyKey(CompanyKey);
            ViewBag.InsuranceList = insList;
            ViewBag.InsuranceDocs = docs;
            vm.Vendor = vendor;
            vm.Resource = resourcem;
            if (vm.Resource != null)
            {
                vm.Radius = resourcem.Radius;
                VendorManagerModel managerModel = __vendorManagerservice.GetUserPrimaryByCompanyKeyAndResourceKey(CompanyKey, vm.Resource.ResourceKey);
                if (managerModel.UserModel != null)
                {
                    vm.UserModel = managerModel.UserModel;
                    try
                    {
                        vm.UserModel.Password = Common.Security.Decrypt(managerModel.UserModel.Password);
                    }
                    catch
                    {
                        vm.UserModel.Password = "1";
                    }
                }
            }
            else
                vm.Resource = new ResourceModel();
            if (vm.Vendor == null)
                vm.Vendor = new VendorManagerVendorModel();
        
        
            return View(vm);
            
        }
        public ActionResult AproVendorEdit(string param, int CompanyKey = 0)
        {
            if (CompanyKey == 0)
                return RedirectToAction("Index");
            try
            {
                var lstRadiuslist = new List<SelectListItem>()
                    {
                    //new SelectListItem{ Value="0",Text="Please Select",Selected=true},
                    new SelectListItem{ Value="1",Text="10 miles"},
                    new SelectListItem{ Value="2",Text="15 miles"},
                    new SelectListItem{ Value="3",Text="20 miles"},
                    new SelectListItem{ Value="4",Text="25 miles"},
                    };

                ViewBag.lstRadius = lstRadiuslist;

                IList<VendorManagerVendorModel> lststate = null;
                IList<VendorManagerVendorModel> lstservice = null;
                //Service
                lstservice = __vendorManagerservice.AppoGetAllService("--Select Service--");
                List<System.Web.Mvc.SelectListItem> lstservicelist = new List<System.Web.Mvc.SelectListItem>();
                SelectListItem sServ = new SelectListItem();
                sServ.Text = "--- Select Service ---";
                sServ.Value = "0";
                lstservicelist.Add(sServ);
               
                for (int i = 0; i < lstservice.Count; i++)
                {
                    System.Web.Mvc.SelectListItem sli = new System.Web.Mvc.SelectListItem();
                    sli.Text = Convert.ToString(lstservice[i].ServiceTitle1);
                    sli.Value = Convert.ToString(lstservice[i].ServiceKey);
                    lstservicelist.Add(sli);
                }
                ViewBag.lstservice = lstservicelist;
                ViewBag.lstservice2 = lstservicelist;
                ViewBag.lstservice3 = lstservicelist;

                //list of state
                lststate = __vendorManagerservice.GetAllState();
                List<System.Web.Mvc.SelectListItem> lststatelist = new List<System.Web.Mvc.SelectListItem>();
                System.Web.Mvc.SelectListItem sli2 = new System.Web.Mvc.SelectListItem();
                sli2.Text = "--Please Select--";
                sli2.Value = "0";
                lststatelist.Add(sli2);
                for (int i = 0; i < lststate.Count; i++)
                {
                    System.Web.Mvc.SelectListItem sli = new System.Web.Mvc.SelectListItem();
                    sli.Text = Convert.ToString(lststate[i].State);
                    sli.Value = Convert.ToString(lststate[i].StateKey);
                    lststatelist.Add(sli);
                }
                ViewBag.lststate = lststatelist;
                //VendorModel resources = null;
                //ResourceModel resourceModel = null;
                VendorManagerModel vendorManager = new VendorManagerModel();
              
                vendorManager.Vendor = __vendorManagerservice.GetVendorByCompanyKey(CompanyKey);
                IList<VendorManagerVendorModel> Services = null;
                Services = __vendorManagerservice.GetServiceByCompany(CompanyKey);
                vendorManager.Vendor.ServiceKey = 0;
                vendorManager.Vendor.ServiceKey1 = 0;
                vendorManager.Vendor.ServiceKey2 = 0;
                for (int i = 0; i < Services.Count; i++)
                {
                    if(i == 0)
                    {
                        vendorManager.Vendor.ServiceKey = Services[i].ServiceKey;
                    }
                    if (i == 1)
                    {
                        vendorManager.Vendor.ServiceKey1 = Services[i].ServiceKey;
                    }
                    if (i == 2)
                    {
                        vendorManager.Vendor.ServiceKey2 = Services[i].ServiceKey;
                    }
                }
                    vendorManager.Resource = __vendorManagerservice.GetResourceByCompanyKey(CompanyKey);
                if (vendorManager.Resource != null)
                {
                    var uModel = __vendorManagerservice.GetUserPrimaryByCompanyKeyAndResourceKey(CompanyKey, vendorManager.Resource.ResourceKey);
                    if (uModel.UserModel != null)
                    {
                        vendorManager.UserModel = uModel.UserModel;
                        IUserService userService = new UserService();
                        var user = userService.Get(vendorManager.UserModel.UserKey);
                        ViewBag.uid = vendorManager.UserModel.UserKey;
                        if (user.Password != "1")
                            user.Password = AssociationBids.Portal.Common.Security.Decrypt(user.Password);
                        vendorManager.UserModel = user;
                        ViewBag.Data = vendorManager.UserModel;
                    }


                }


                var items = new List<SelectListItem>();
                foreach (var a in lstservicelist)
                {
                    SelectListItem item = new SelectListItem();

                    item.Text = a.Text;
                    item.Value = a.Value;
                    items.Add(item);
                }

                var vList = new AssociationBids.Portal.Repository.Base.VendorServiceRepository().GetAll();

                List<VendorServiceModel> tempList = new List<VendorServiceModel>();
                foreach (var data in vList)
                {
                    if (data.VendorKey == CompanyKey)
                        tempList.Add(data);
                }


                var sList = __vendorManagerservice.Getbindservice(CompanyKey);
                List<VendorManagerServiceModel> venServList = new List<VendorManagerServiceModel>();
                int i1 = 0;
                foreach (var ven in sList)
                {
                    VendorManagerServiceModel vmService = new VendorManagerServiceModel();
                    vmService.Id = i1++;
                    vmService.Title = ven.Vendor.Title;
                    vmService.value = ven.ServiceModel.ServiceKey;
                    vmService.VendorServiceKey = ven.ServiceModel.VendorServiceKey;
                    venServList.Add(vmService);
                }
                ViewBag.Service = venServList.ToList();
                ViewBag.ServiceCount = sList.Count;
                ViewBag.Services = items;
                ViewBag.address = vendorManager.Vendor.Address;
                ViewBag.Radius = vendorManager.Vendor.Radius;
                ViewBag.address2 = vendorManager.Vendor.Address2;
                ViewBag.City = vendorManager.Vendor.City;
                ViewBag.Zip = vendorManager.Vendor.Zip;
                ViewBag.State = vendorManager.Vendor.State;
                ViewBag.cid = CompanyKey;
                return View(vendorManager);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult VendorEdit(string param, int CompanyKey = 0)
        {
            if (CompanyKey == 0)
                return RedirectToAction("Index");


            var lstRadiuslist = new List<SelectListItem>()
                    {
                    //new SelectListItem{ Value="0",Text="Please Select",Selected=true},
                    new SelectListItem{ Value="1",Text="10 miles"},
                    new SelectListItem{ Value="2",Text="15 miles"},
                    new SelectListItem{ Value="3",Text="20 miles"},
                    new SelectListItem{ Value="4",Text="25 miles"},
                    };

            ViewBag.lstRadius = lstRadiuslist;
            try
            {
                IList<VendorManagerVendorModel> lststate = null;
                IList<VendorManagerVendorModel> lstservice = null;
                //Service
                lstservice = __vendorManagerservice.AppoGetAllService("--Select Service--");
                List<System.Web.Mvc.SelectListItem> lstservicelist = new List<System.Web.Mvc.SelectListItem>();
                SelectListItem sServ = new SelectListItem();
                sServ.Text = "--- Select Service ---";
                sServ.Value = "0";
                lstservicelist.Add(sServ);

                for (int i = 0; i < lstservice.Count; i++)
                {
                    System.Web.Mvc.SelectListItem sli = new System.Web.Mvc.SelectListItem();
                    sli.Text = Convert.ToString(lstservice[i].ServiceTitle1);
                    sli.Value = Convert.ToString(lstservice[i].ServiceKey);
                    lstservicelist.Add(sli);
                }
                ViewBag.lstservice = lstservicelist;
                ViewBag.lstservice2 = lstservicelist;
                ViewBag.lstservice3 = lstservicelist;

                //list of state
                lststate = __vendorManagerservice.GetAllState();
                List<System.Web.Mvc.SelectListItem> lststatelist = new List<System.Web.Mvc.SelectListItem>();
                System.Web.Mvc.SelectListItem sli2 = new System.Web.Mvc.SelectListItem();
                sli2.Text = "--Please Select--";
                sli2.Value = "0";
                lststatelist.Add(sli2);
                for (int i = 0; i < lststate.Count; i++)
                {
                    System.Web.Mvc.SelectListItem sli = new System.Web.Mvc.SelectListItem();
                    sli.Text = Convert.ToString(lststate[i].State);
                    sli.Value = Convert.ToString(lststate[i].StateKey);
                    lststatelist.Add(sli);
                }
                ViewBag.lststate = lststatelist;
                //VendorModel resources = null;
                //ResourceModel resourceModel = null;
                VendorManagerModel vendorManager = new VendorManagerModel();
              

                vendorManager.Vendor = __vendorManagerservice.GetVendorByCompanyKey(CompanyKey);


                vendorManager.Resource = __vendorManagerservice.GetResourceByCompanyKey(CompanyKey);


                ViewBag.Address = vendorManager.Vendor.Address;
                //if (vendor.Address2 == null)
                //{
                //    vendor.Address2 = "";
                //}
                ViewBag.Address2 = vendorManager.Vendor.Address2;
                ViewBag.City = vendorManager.Vendor.City;
                ViewBag.Zip = vendorManager.Vendor.Zip;
                ViewBag.State = vendorManager.Vendor.State;
                IList<VendorManagerVendorModel> Services = null;
                Services = __vendorManagerservice.GetServiceByCompany(CompanyKey);
                vendorManager.Vendor.ServiceKey = 0;
                vendorManager.Vendor.ServiceKey1 = 0;
                vendorManager.Vendor.ServiceKey2 = 0;
                for (int i = 0; i < Services.Count; i++)
                {
                    if (i == 0)
                    {
                        vendorManager.Vendor.ServiceKey = Services[i].ServiceKey;
                    }
                    if (i == 1)
                    {
                        vendorManager.Vendor.ServiceKey1 = Services[i].ServiceKey;
                    }
                    if (i == 2)
                    {
                        vendorManager.Vendor.ServiceKey2 = Services[i].ServiceKey;
                    }
                }


                if (vendorManager.Resource != null)
                {
                    var uModel = __vendorManagerservice.GetUserPrimaryByCompanyKeyAndResourceKey(CompanyKey, vendorManager.Resource.ResourceKey);
                    if (uModel.UserModel != null)
                    {
                        vendorManager.UserModel = uModel.UserModel;
                        IUserService userService = new UserService();
                        var user = userService.Get(vendorManager.UserModel.UserKey);
                        ViewBag.uid = vendorManager.UserModel.UserKey;
                        if(user.Password != "1")
                            user.Password = AssociationBids.Portal.Common.Security.Decrypt(user.Password);
                        vendorManager.UserModel = user;
                        ViewBag.Data = vendorManager.UserModel;
                    }


                }


                var items = new List<SelectListItem>();
                foreach (var a in lstservicelist)
                {
                    SelectListItem item = new SelectListItem();

                    item.Text = a.Text;
                    item.Value = a.Value;
                    items.Add(item);
                }

                var vList = new AssociationBids.Portal.Repository.Base.VendorServiceRepository().GetAll();

                List<VendorServiceModel> tempList = new List<VendorServiceModel>();
                foreach (var data in vList)
                {
                    if (data.VendorKey == CompanyKey)
                        tempList.Add(data);
                }


                var sList = __vendorManagerservice.Getbindservice(CompanyKey);
                List<VendorManagerServiceModel> venServList = new List<VendorManagerServiceModel>();
                int i1 = 0;
                foreach (var ven in sList)
                {
                    VendorManagerServiceModel vmService = new VendorManagerServiceModel();
                    vmService.Id = i1++;
                    vmService.Title = ven.Vendor.Title;
                    vmService.value = ven.ServiceModel.ServiceKey;
                    vmService.VendorServiceKey = ven.ServiceModel.VendorServiceKey;
                    venServList.Add(vmService);
                }
                ViewBag.Service = venServList.ToList();
                ViewBag.ServiceCount = sList.Count;
                ViewBag.Services = items;
                ViewBag.address = vendorManager.Vendor.Address;
                ViewBag.Radius = vendorManager.Vendor.Radius;
                ViewBag.address2 = vendorManager.Vendor.Address2;
                ViewBag.City = vendorManager.Vendor.City;
                ViewBag.Zip = vendorManager.Vendor.Zip;
                ViewBag.State = vendorManager.Vendor.State;
                ViewBag.cid = CompanyKey;
                vendorManager.Radius = vendorManager.Resource.Radius;
                return View(vendorManager);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult VendorEdit(int CompanyKey, VendorManagerModel collection, string param,string values)
        {
            VendorManagerVendorModel vendorv = new VendorManagerVendorModel();
            ResourceModel resource = new ResourceModel();
            string returnView = "";
            if (param == "")
                param = Request["param"];
            try
            {
                Int64 value = 0;
                
                if (param == "contact")
                {
                    resource = __vendorManagerservice.GetResourceByCompanyKey(CompanyKey);
                    resource.FirstName = collection.Resource.FirstName;
                    resource.LastName = collection.Resource.LastName;
                    resource.Work = collection.Resource.Work;
                    resource.Work2 = collection.Resource.Work2;
                    resource.CellPhone = collection.Resource.CellPhone;
                    resource.Email = collection.Resource.Email;
                    resource.Radius = collection.Radius;
                    resource.PrimaryContact = true;
                    bool b = __vendorManagerservice.CheckDuplicatedEmailByResourceKey(resource.Email, resource.ResourceKey);
                    if (b == true)
                        value = 2;
                    else
                    {
                        bool s = __resourceservice.Edit(resource);
                        if (s == false)
                            value = 1;
                    }

                }
                else if (param == "company")
                {
                    vendorv = __vendorManagerservice.GetVendorByCompanyKey(CompanyKey);
                    vendorv.CompanyName = collection.Vendor.CompanyName;
                    vendorv.Work = collection.Vendor.Work;
                    vendorv.Work2 = collection.Vendor.Work2;
                    vendorv.Website = collection.Vendor.Website;
                    vendorv.Fax = collection.Vendor.Fax;
                    vendorv.Address = collection.Vendor.Address;
                    vendorv.Address2 = collection.Vendor.Address2;
                    vendorv.City = collection.Vendor.City;
                    vendorv.State = collection.Vendor.State;
                    vendorv.Zip = collection.Vendor.Zip;
                    vendorv.Radius = collection.Radius;

                    bool s = __vendorManagerservice.CheckDuplicatedCompanyNameByCompanyKey(vendorv.CompanyName, CompanyKey);
                    if (s)
                        value = 2;
                    else
                    {
                        long status = value = __vendorManagerservice.VendorManagerUpdate(vendorv);
                        if (status != 0)
                            value = 1;
                    }

                }
                returnView = Request.UrlReferrer.ToString().Contains("AproVendorEdit") ? "UnapprovedVendorView" : "VendorView";
                if (returnView == "")
                    returnView = "index";
                if (value == 0)
                {
                    TempData["Sucessmessage"] = "Record has been updated successfully.";
                    System.Web.Routing.RouteValueDictionary rvd = new System.Web.Routing.RouteValueDictionary();
                    rvd.Add("CompanyKey", CompanyKey);
                    if (values == "IVendor")
                    {
                        return RedirectToAction("InvitedVendorView", "VendorManager", rvd);
                    }
                    else
                    {
                        return RedirectToAction(returnView, "VendorManager", rvd);
                    }
                }
                else
                {
                    if (value == 2)
                    {
                        if (param == "company")
                            TempData["Error"] = "Company Name Already Registerd";
                        else
                            TempData["Error"] = "Email id Already Registerd";
                    }
                    else
                        TempData["Error"] = "Opps.... Something went wrong.";
                    System.Web.Routing.RouteValueDictionary rvd = new System.Web.Routing.RouteValueDictionary();
                    rvd.Add("CompanyKey", CompanyKey);
                    rvd.Add("param", param);

                    if (values == "IVendor")
                    {
                        return RedirectToAction("InvitedVendorView", "VendorManager", rvd);
                    }
                    else {
                        return RedirectToAction(returnView, "VendorManager", rvd);
                    }

                }

            }
            catch (Exception ex)
            {
                return View(collection);

            }

        }
        [HttpPost]
        public ActionResult VendorEditForService(int CompanyKey, VendorManagerModel vm)
        {
            //var a = Request["ServiceKey1"];
            List<VendorServiceModel> vList = new List<VendorServiceModel>();
            var list1 = Request.Form.AllKeys.Where(w => w.StartsWith("Service")).ToList();
            var l = Request.Form.GetValues("Services");
            if (l != null)
                list1 = l.ToList();
            else
                list1 = new List<string>();

            var ids = Request.Form.GetValues("ServiceId");
            List<VendorManagerModel> sList = __vendorManagerservice.Getbindservice(CompanyKey).ToList();

            //list1.RemoveAt(0);
            if (ids == null)
                ids = new List<string>().ToArray();
            for (int i = 0; i < list1.Count; i++)
            {
                if (i >= ids.Length)
                    break;
                var val = list1[i];
                VendorManagerModel sVar = new VendorManagerModel();
                sVar = sList.Where(w => w.ServiceModel.VendorServiceKey == Convert.ToInt32(ids[i])).FirstOrDefault();
                if (sVar != null)
                {
                    sVar.ServiceModel.ServiceKey = Convert.ToInt32(val);
                    sVar.ServiceModel.VendorKey = CompanyKey;
                    vList.Add(sVar.ServiceModel);
                }
            }
            //int count = ids.Length;
            List<string> rList = new List<string>();
            List<string> UList = new List<string>();
            List<string> InsertList = new List<string>();

            if (ids.Length != list1.Count)
            {
                if (list1.Count < ids.Length)
                {
                    foreach (var item in ids)
                    {
                        var val = vList.Where(w => w.VendorServiceKey == Convert.ToInt32(item)).FirstOrDefault();
                        if (val == null)
                            rList.Add(item);
                    }
                }
                else
                {
                    foreach (var item in list1)
                    {
                        var val = vList.Where(w => w.ServiceKey == Convert.ToInt32(item)).FirstOrDefault();
                        if (val == null)
                            InsertList.Add(item);
                    }
                }
            }

            int j = 0;
            int updatedService = 0, DeletedService = 0, insertedService = 0, Errors = 0;
            foreach (var u in vList)
            {
                var nameData = sList.Where(w => w.ServiceModel.ServiceKey == u.ServiceKey).FirstOrDefault();
                string name = "";
                if (nameData != null)
                    name = nameData.Vendor.Title;
                bool val = __serviceVendor.Update(u);
                if (val)
                {
                    updatedService++;
                }
                else
                {
                    Errors++;
                }
                j++;
            }
            foreach (var u in rList)
            {
                bool val = __serviceVendor.Delete(Convert.ToInt32(u));
                if (val)
                {
                    DeletedService++;
                }
                else
                {
                    Errors++;
                }
            }
            foreach (var u in InsertList)
            {
                VendorServiceModel vs = new VendorServiceModel();
                vs.ServiceKey = Convert.ToInt32(u);
                vs.VendorKey = Convert.ToInt32(CompanyKey);
                bool val = __serviceVendor.Create(vs);
                if (val)
                {
                    insertedService++;
                }
                else
                {
                    Errors++;
                }
            }
            if (Errors > 0)
                TempData["Error"] = "(" + Errors + ") Service(s) Not Updated";
            int total = insertedService + DeletedService + updatedService;
            if (total > 0)
                TempData["Sucessmessage"] = "Service(s) Updates Successfully";

            System.Web.Routing.RouteValueDictionary rvd = new System.Web.Routing.RouteValueDictionary();
            rvd.Add("CompanyKey", CompanyKey);
            rvd.Add("param", "service");
            return RedirectToAction("UnapprovedVendorView", rvd);
        }

        public ActionResult VendorManagerAddService(int CompanyKey)
        {
            bool Status = false;
            VendorServiceModel vendorService = new VendorServiceModel();
            vendorService.VendorKey = CompanyKey;
            vendorService.ServiceKey = Convert.ToInt32(Request["VendorServce"]);
            Status = __serviceVendor.Create(vendorService);


            if (Status == true)
            {
                TempData["Sucessmessage"] = "Service has been Added successfully.";

            }
            else
            {
                ViewData["Error"] = "Opps.... Something went wrong.";

            }
            System.Web.Routing.RouteValueDictionary rvd = new System.Web.Routing.RouteValueDictionary();
            rvd.Add("CompanyKey", CompanyKey);
            return RedirectToAction("UnapprovedVendorView", rvd);
        }

        public ActionResult Delete(int CompanyKey, int VendorServiceKey)
        {

            long Status = 0;
            Status = __vendorManagerservice.VendorManagerServiceDelete(CompanyKey, VendorServiceKey);
            if (Status == 0)
            {
                TempData["Delete"] = "Service has been deleted successfully.";
            }
            else
            {
                ViewData["Error"] = "Opps....Something went wrong.";
            }

            System.Web.Routing.RouteValueDictionary rvd = new System.Web.Routing.RouteValueDictionary();
            rvd.Add("CompanyKey", CompanyKey);
            return RedirectToAction("UnapprovedVendorView", rvd);
        }

        public JsonResult DeleteAsync(int CompanyKey, int VendorServiceKey)
        {

            long Status = 0;
            Status = __vendorManagerservice.VendorManagerServiceDelete(CompanyKey, VendorServiceKey);

            return Json(new { message = Status }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult AddInsuranceWithDoc(int CompanyKey, VendorManagerModel model, HttpPostedFileBase[] files)
        {
            if (files == null || files.Length == 0)
                return RedirectToAction("AddPolicy");
            VendorManagerVendorModel vm = __vendorManagerservice.GetVendorByCompanyKey(CompanyKey);
            IDocumentService document = new DocumentService();

            model.Insurance.VendorKey = CompanyKey;
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

            long iKey = __vendorManagerservice.VendorManagerAddInsurance(model.Insurance);
            if (files != null && iKey != 0 && files.Length > 0 && files[0] != null)
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
                    //Directory.CreateDirectory(Server.MapPath("~/Document/Insurance/" + CompanyKey + "/" + iKey));
                    //file.SaveAs(Server.MapPath("~/Document/Insurance/" + CompanyKey + "/" + iKey + "/") + file.FileName);

                    Directory.CreateDirectory(Server.MapPath("~/Document/Insurance/" + iKey));
                    file.SaveAs(Server.MapPath("~/Document/Insurance/" + iKey + file.FileName));
                }
            }

            System.Web.Routing.RouteValueDictionary rvd = new System.Web.Routing.RouteValueDictionary();
            rvd.Add("CompanyKey", CompanyKey);
            return RedirectToAction("VendorView", rvd);
        }

        public ActionResult AddInsuranceWithDocVendor( VendorManagerModel model, HttpPostedFileBase[] files)
        {
            int CompanyKey =Convert.ToInt32(Session["CompanyKey"]);
            if (files == null || files.Length == 0)
                return RedirectToAction("AddPolicy");
            VendorManagerVendorModel vm = __vendorManagerservice.GetVendorByCompanyKey(CompanyKey);
            IDocumentService document = new DocumentService();

            model.Insurance.VendorKey = CompanyKey;
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

            long iKey = __vendorManagerservice.VendorManagerAddInsurance(model.Insurance);
            if (files != null && iKey != 0)
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

            System.Web.Routing.RouteValueDictionary rvd = new System.Web.Routing.RouteValueDictionary();
            rvd.Add("CompanyKey", CompanyKey);
            return RedirectToAction("UnapprovedVendorView", rvd);
        }

        //public ActionResult ViewEditUserAccount(int UserKey,int CompanyKey)
        //{

        //    //VendorModel resources = null;
        //    //ResourceModel resourceModel = null;
        //    IUserService userService = new UserService();
        //    var user = userService.Get(UserKey);
        //    ViewBag.uid = UserKey;            
        //    VendorManagerModel vendorManager = new VendorManagerModel();
        //    vendorManager.Vendor = __vendorManagerservice.GetVendorByCompanyKey(CompanyKey);
        //    vendorManager.Resource = __vendorManagerservice.GetResourceByCompanyKey(CompanyKey);
        //    vendorManager.UserModel = user;
        //    ViewBag.Data = vendorManager.UserModel;



        //    var sList = __vendorManagerservice.Getbindservice(CompanyKey);
        //    List<VendorManagerServiceModel> venServList = new List<VendorManagerServiceModel>();
        //    int i1 = 0;
        //    foreach (var ven in sList)
        //    {
        //        VendorManagerServiceModel vmService = new VendorManagerServiceModel();
        //        vmService.Id = i1++;
        //        vmService.Title = ven.Vendor.Title;
        //        vmService.value = ven.ServiceModel.ServiceKey;
        //        vmService.VendorServiceKey = ven.ServiceModel.VendorServiceKey;
        //        venServList.Add(vmService);
        //    }
        //    ViewBag.Service = venServList.ToList();
        //    ViewBag.ServiceCount = sList.Count;

        //    ViewBag.cid = CompanyKey;
        //    return View("VendorEdit",vendorManager);


        //}

        [HttpPost]
        public ActionResult UserAccountChangePassword(int UserKey, int CompanyKey,string values)
        {
            IUserService userService = new UserService();
            var user = userService.Get(UserKey);
            if (Request["ConfirmPassword"] == Request["UserModel_Password"])
            {
                string newPass = Request["UserModel_Password"];
                bool r = __vendorManagerservice.VendorManagerChangePassword(newPass, UserKey);
                if (r == false)
                    TempData["Error"] = "Somethingwent wrong please try later...";
                else
                    TempData["Sucessmessage"] = "Password Changed.";
            }
            else
            {
                TempData["Error"] = "Password and Confirm Password not mached..";
            }
            System.Web.Routing.RouteValueDictionary rvd = new System.Web.Routing.RouteValueDictionary();
            rvd.Add("CompanyKey", CompanyKey);
            if (values == "IuserAccount")
            {
                return RedirectToAction("InvitedVendorView", "VendorManager", rvd);
            }
            else 
            {
                return RedirectToAction("VendorView", "VendorManager", rvd);
            }
        }

        public JsonResult bindservice(int CompanyKey)
        {
            try
            {
                IList<VendorManagerModel> servicelist = null;
                IList<VendorManagerServiceModel> sList = new List<VendorManagerServiceModel>();
                servicelist = __vendorManagerservice.Getbindservice(CompanyKey);
                int id = 0;
                foreach (var s in servicelist)
                {
                    VendorManagerServiceModel sm = new VendorManagerServiceModel();
                    sm.Id = id++;
                    sm.Title = s.Vendor.Title;
                    sm.value = s.ServiceModel.ServiceKey;
                    sm.VendorServiceKey = s.ServiceModel.VendorServiceKey;
                    sList.Add(sm);
                }

                return Json(sList, JsonRequestBehavior.AllowGet);

            }
            catch
            {
                return Json(null);
            }
        }

        public JsonResult bindDocument(int CompanyKey)
        {
            try
            {
                IList<VendorManagerModel> Documentlist = null;

                Documentlist = __vendorManagerservice.GetbindDocument(CompanyKey);

                return Json(Documentlist, JsonRequestBehavior.AllowGet);

            }
            catch
            {
                return Json(null);
            }
        }
        public JsonResult GetInsuranceDetails(int CompanyKey, int InsuranceKey)
        {
            try
            {
                IList<VendorManagerModel> Documentlist = null;

                Documentlist = __vendorManagerservice.GetbindDocumentByInsuranceKey(CompanyKey, InsuranceKey);
                return Json(Documentlist, JsonRequestBehavior.AllowGet);

            }
            catch
            {
                return Json(null);
            }
        }

        public JsonResult BindBidRequest(int CompanyKey,int index,int pageSize,string Search,string sort)
        {
            IList<BidRequestModel> bidRequests = null;
            
            bidRequests = __vendorManagerservice.BidRequestsIndexPagingByCompanyKey(CompanyKey, index, pageSize, Search.Trim(), sort);

            return Json(bidRequests, JsonRequestBehavior.AllowGet);
        }
        public JsonResult BindWorkOrder(int CompanyKey, int index, int pageSize, string Search, string sort)
        {
            IList<BidRequestModel> bidRequests = null;

            bidRequests = __vendorManagerservice.WordOrderIndexPagingByCompanyKey(CompanyKey, index, pageSize, Search.Trim(), sort);

            return Json(bidRequests, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UploadVendor()
        {
            if (Session["resourceid"] == null)
                return RedirectToAction("InvitedVendorslist");
            var file = Request.Files;
            HttpCookie cookieS = new HttpCookie("Success");
            HttpCookie cookieE = new HttpCookie("Error");
            List<string> errorList = new List<string>();
            if (file[0].FileName.EndsWith(".csv"))
            {

                int CompanyKey = Convert.ToInt32(Request.Form.Get("CompanyKey"));
                if (CompanyKey == 0)
                {
                    Response.Cookies["Error"].Value = "Please Select Management Company";
                    TempData["Er"] = "Please Select Management Company";
                    return RedirectToAction("InvitedVendorslist");
                }
                var filePath = Server.MapPath("~/Document/UploadedVendors/CSV/");
                int RKey = Convert.ToInt32(Session["ResourceId"]);
                string currentTimeStamp = Math.Floor(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds).ToString();
                string randomFileName = RKey+"_"+CompanyKey + "_"+ currentTimeStamp + Path.GetExtension(file[0].FileName);
                var fileName = Path.Combine(filePath, randomFileName);
                Directory.CreateDirectory(filePath);
                if (System.IO.File.Exists(fileName))
                    System.IO.File.Delete(fileName);

                file[0].SaveAs(fileName);
                int LineStartsFrom = 0;
                List<VendorManagerVendorModel> vendors = ReadCSVFile(fileName, errorList,LineStartsFrom);
                
                if (errorList.Count > 0)
                {
                    Response.Cookies["Error"].Value = "";
                    TempData["Er"] = "Failed to Invite.";
                    string errorString = "Failed to Invite.\n";
                    foreach (var error in errorList)
                    {
                        errorString += "\n<i class='fa fa-circle' aria-hidden='true' style='font-size: 12px;'></i> " + error + ".";
                    }
                    TempData["Er"] = errorString;
                }
                else
                {
                    TempData["wa"] = "Error: ";
                    int line = LineStartsFrom;
                    List<string> warnings = new List<string>();
                    foreach (var v in vendors)
                    {
                        line++;
                        bool st = InsertVendorFromCSV(v,CompanyKey);
                        if (!st)
                        {
                            TempData["wa"] += "<i class='fa fa-circle' aria-hidden='true' style='font-size: 12px;'></i> Line " + line + " - Failed to inite: " + v.LegalName;
                            warnings.Add("Failed to inite: " + v.LegalName);
                        }
                    }
                    if(warnings.Count == 0)
                    {
                        TempData["wa"] = null;
                        Response.Cookies["Success"].Value = "Vendors are Invited...";
                        TempData["SMessage"] = "Vendors are Invited...";
                        cookieS.Value = "Vendors are Invited...";
                    }
                }
            }
            else
            {
                TempData["Er"] = "Please Choose Valid .csv File.";
            }
            return RedirectToAction("InvitedVendorsList");
        }
        private List<VendorManagerVendorModel> ReadCSVFile(string fileName,List<string> GeterrorList,int GetLineStarts)
        {
            List<VendorManagerVendorModel> listB = null;
            try {
                using (var reader = new StreamReader(fileName))
                {
                    List<string> listA = new List<string>();
                    listB = new List<VendorManagerVendorModel>();
                    IStateService stateService = new StateService();
                    
                    bool isCurrentLineContainsError = false;
                    string CurrentLineError = "";
                    var stateList = stateService.GetAll(new StateFilterModel());
                    bool isHeadings = false;
                    int TotalLines = 0;
                    int StartsFrom = 0;
                    while (!reader.EndOfStream)
                    {
                        TotalLines++;
                        var line = reader.ReadLine();
                        var values = line.Split(',');
                        if (values.Length >= 9)
                        {

                            VendorManagerVendorModel m = new VendorManagerVendorModel();

                            for (int i = 0; i < values.Length; i++)
                            {
                                if ((line.ToLower().Replace(" ", "").Contains("companyname") && line.ToLower().Replace(" ", "").Contains("email")))
                                {
                                    listA.Add(values[i]);
                                    isHeadings = true;
                                    StartsFrom = TotalLines;

                                }
                                else
                                {
                                    isHeadings = false;
                                    if (listA[i].ToLower().Replace(" ", "") == "companyname")
                                    {
                                        if (IsNullOrEmpty(values[i]))
                                        {
                                            if (isCurrentLineContainsError)
                                                CurrentLineError += ", CompanyName is Blank";
                                            else
                                            {
                                                CurrentLineError = "Line " + TotalLines + " - CompanyName is Blank";
                                                isCurrentLineContainsError = true;
                                            }
                                        }
                                        else
                                        {
                                            var s = __vendorManagerservice.CheckDuplicatedEmailAndCompanyName("", values[i]);
                                            if (s[1])
                                            {
                                                if (isCurrentLineContainsError)
                                                    CurrentLineError += ", Company already Registered";
                                                else
                                                {
                                                    CurrentLineError = "Line " + TotalLines + " - Company already Registered";
                                                    isCurrentLineContainsError = true;
                                                }
                                            }
                                            else
                                            {
                                                m.LegalName = values[i];
                                            }
                                        }

                                    }
                                    else if (listA[i].ToLower().Replace(" ", "") == "contactperson" || listA[i].ToLower().Replace(" ", "") == "contractperson")
                                    {
                                        if (IsNullOrEmpty(values[i]))
                                        {
                                            //if (isCurrentLineContainsError)
                                            //    CurrentLineError += ", Contact Person is Blank";
                                            //else
                                            //{
                                            //    CurrentLineError = "Line " + TotalLines + " - Contact Person is Blank";
                                            //    isCurrentLineContainsError = true;
                                            //}
                                        }
                                        else
                                        {
                                            m.ContactPerson = values[i];
                                        }
                                    }
                                    else if (listA[i].ToLower().Replace(" ", "") == "address1")
                                    {
                                        if (IsNullOrEmpty(values[i]))
                                        {
                                            //if (isCurrentLineContainsError)
                                            //    CurrentLineError += ", Address1 is Blank";
                                            //else
                                            //{
                                            //    CurrentLineError = "Line " + TotalLines + " - Address1 is Blank";
                                            //    isCurrentLineContainsError = true;
                                            //}
                                        }
                                        else
                                        {
                                            m.Address = values[i];
                                        }
                                    }
                                    else if (listA[i].ToLower().Replace(" ", "") == "address2")
                                    {
                                        m.Address2 = values[i];
                                    }
                                    else if (listA[i].ToLower().Replace(" ", "") == "phone")
                                    {
                                        m.Work2 = values[i];
                                    }
                                    else if (listA[i].ToLower().Replace(" ", "") == "email")
                                    {
                                        if (IsNullOrEmpty(values[i]))
                                        {
                                            if (isCurrentLineContainsError)
                                                CurrentLineError += ", Email is Blank";
                                            else
                                            {
                                                CurrentLineError = "Line " + TotalLines + " - Email is Blank";
                                                isCurrentLineContainsError = true;
                                            }
                                        }
                                        else if (!ValidateEmail(values[i]))
                                        {
                                            if (isCurrentLineContainsError)
                                                CurrentLineError += ", Email is Not Valid";
                                            else
                                            {
                                                CurrentLineError = "Line " + TotalLines + " - Email is Not Valid";
                                                isCurrentLineContainsError = true;
                                            }
                                        }
                                        else
                                        {
                                            var s = __vendorManagerservice.CheckDuplicatedEmailAndCompanyName(values[i], "");
                                            if (s[1])
                                            {
                                                if (isCurrentLineContainsError)
                                                    CurrentLineError += ", Email already Registered";
                                                else
                                                {
                                                    CurrentLineError = "Line " + TotalLines + " - Email already Registered";
                                                    isCurrentLineContainsError = true;
                                                }
                                            }
                                            else
                                            {
                                                m.Email = values[i];
                                            }
                                        }
                                    }
                                    else if (listA[i].ToLower().Replace(" ", "") == "city")
                                    {
                                        if (IsNullOrEmpty(values[i]))
                                        {
                                            //if (isCurrentLineContainsError)
                                            //    CurrentLineError += ", City is Blank";
                                            //else
                                            //{
                                            //    CurrentLineError = "Line " + TotalLines + " - City is Blank";
                                            //    isCurrentLineContainsError = true;
                                            //}
                                        }
                                        else
                                        {
                                            m.City = values[i];
                                        }
                                    }
                                    else if (listA[i].ToLower().Replace(" ", "") == "state")
                                    {
                                        var s = stateList.Where(w => w.StateKey == values[i] || w.Title.Replace(" ", "").ToLower() == values[i].Replace(" ", "").ToLower()).FirstOrDefault();
                                        if (IsNullOrEmpty(values[i]))
                                        {
                                            //if (isCurrentLineContainsError)
                                            //    CurrentLineError += ", State is Blank";
                                            //else
                                            //{
                                            //    CurrentLineError = "Line " + TotalLines + " - State is Blank";
                                            //    isCurrentLineContainsError = true;
                                            //}
                                        }
                                        else if (s == null)
                                        {
                                            if (isCurrentLineContainsError)
                                                CurrentLineError += ", State is not valid";
                                            else
                                            {
                                                CurrentLineError = "Line " + TotalLines + " - State is not valid";
                                                isCurrentLineContainsError = true;
                                            }
                                        }
                                        else
                                        {
                                            m.State = s.StateKey;
                                        }
                                    }
                                    else if (listA[i].ToLower().Replace(" ", "") == "zip")
                                    {
                                        if (IsNullOrEmpty(values[i]))
                                        {
                                            //if (isCurrentLineContainsError)
                                            //    CurrentLineError += ", Zip is Blank";
                                            //else
                                            //{
                                            //    CurrentLineError = "Line " + TotalLines + " - Zip is Blank";
                                            //    isCurrentLineContainsError = true;
                                            //}
                                        }
                                        else
                                        {
                                            m.Zip = values[i];
                                        }
                                    }

                                }
                            }
                            if (isHeadings)
                                GetLineStarts = TotalLines;



                            //if (!isHeadings && m.LegalName.Trim() != "" && m.Email.Trim() != "")
                            if (!isHeadings)
                                listB.Add(m);
                            if (CurrentLineError != "" && CurrentLineError.Length > 0)
                                GeterrorList.Add(CurrentLineError + ".");
                            isCurrentLineContainsError = false;
                        }
                        else
                        {
                            CurrentLineError = "No data found in file";
                            GeterrorList.Add(CurrentLineError);
                            isCurrentLineContainsError = true;
                        }


                    }
                    if(TotalLines==1)
                    {
                        CurrentLineError = "No data found in file";
                        GeterrorList.Add(CurrentLineError);
                        isCurrentLineContainsError = true;
                    }
                    


                    reader.Close();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            return listB;
        }
        
        private bool InsertVendorFromCSV(VendorManagerVendorModel vendor,int CompanyKey)
        {
            var status = __vendorManagerservice.CheckDuplicatedEmailAndCompanyName(vendor.Email, vendor.LegalName);
            if (status[0] == false && status[1] == false)
            {
                long s = -1;
                try
                {
                    vendor.CompanyKey = CompanyKey;
                    int ResourceKey = Convert.ToInt32(Session["resourceid"]);
                    s = __vendorManagerservice.VendorManagerInviteVendor(vendor, ResourceKey);
                    if (s <= 0)
                        return false;
                    else
                    {
                        return true;
                    }
                }
                catch (Exception e)
                {
                    return false;
                }
            }
            else
                return false;
        }

        public bool ValidationVendorEntry(VendorManagerVendorModel vendor,List<string> errors,int CurrentLineNo)
        {
            bool isValid = true;
            Regex regex = new Regex("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$");
            if (vendor.LegalName == null || vendor.LegalName.Trim() == "")
            {
                errors.Add("Line " + CurrentLineNo + " - Company Name can't be Empty");
                isValid = false;
            }
            else if(vendor.Address == null || vendor.Address.Trim() == "")
            {
                errors.Add("Line " + CurrentLineNo + " - Address can't be Empty");
                isValid = false;
            }
            else if (vendor.City == null || vendor.City.Trim() == "")
            {
                errors.Add("Line " + CurrentLineNo + " - City can't be Empty");
                isValid = false;
            }
            else if (vendor.State == null || vendor.State.Trim() == "")
            {
                errors.Add("Line " + CurrentLineNo + " - State can't be Empty");
                isValid = false;
            }
            else if (vendor.Zip == null || vendor.Zip.Trim() == "")
            {
                errors.Add("Line " + CurrentLineNo + " - Zip can't be Empty");
                isValid = false;
            }
            else if (vendor.ContactPerson == null || vendor.ContactPerson.Trim() == "")
            {
                errors.Add("Line " + CurrentLineNo + " - Contact Person can't be Empty");
                isValid = false;
            }
            else if (vendor.Email == null || vendor.Email.Trim() == "")
            {
                errors.Add("Line " + CurrentLineNo + " - Email can't be Empty");
                isValid = false;
            }
            else if(!regex.IsMatch(vendor.Email))
            {
                errors.Add("Line " + CurrentLineNo + " - Email is Not Valid.");
                isValid = false;
            }
            return isValid;
        }

        private bool IsNullOrEmpty(String str)
        {
            return (str == null || str.Trim() == "");
        }
        private bool ValidateEmail(String email)
        {
            Regex test = new Regex("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$");
            return test.IsMatch(email);            
        }
        [HttpPost]
        public async Task<ActionResult> ApproveVendor(int CompanyKey)
        {

            var con = DependencyResolver.Current.GetService<APISController>();
            con.ControllerContext = new ControllerContext(Request.RequestContext, con);
            string status = "";

            bool st;
             status = await con.MemberShipFeeForApproveNew(CompanyKey);
            if (status == "done")
            {
                st = __vendorManagerservice.VendorManagerApproveVendor(CompanyKey);
                if (st)
                    TempData["Sucessmessage"] = "Vendor has been approved successfully.";
                else
                    TempData["Error"] = "Something went wrong.....";
            }


            else if (status == "NotFound")
            {

                st = __vendorManagerservice.VendorManagerApproveVendor(CompanyKey);
                if (st)
                    TempData["Sucessmessage"] = "Vendor has been approved successfully but Membership fee not deducted.";
                else
                    TempData["Error"] = "Something went wrong.....";
              
            }

            
            else
            {
                TempData["Error"] = "Something went wrong...";
            }
                

            return RedirectToAction("UnapprovedVendorList");//View("UnapprovedVendorlist");
        }

        [HttpPost]
        public ActionResult MarkasDuplicate(int CompanyKey)
        {
            bool status = false;
            status = __vendorManagerservice.VendorManagerMarkDuplicateVendor(CompanyKey);
            if (status)
                TempData["Sucessmessage"] = "Vendor Marked as Duplicate.";
            else
                TempData["Error"] = "Something went wrong...";

            return View("UnapprovedVendorlist");
        }
        public ActionResult DownloadFile(int DocumentKey, int CompanyKey, int InsuranceKey)
        {
            var referal = HttpContext.Request.UrlReferrer.AbsolutePath.Split('/');
            string refView = referal[referal.Length - 1];
            try
            {
                var doc = __vendorManagerservice.GetbindDocumentByInsuranceKey(CompanyKey, InsuranceKey);
                string ff = doc.Where(w => w.Document.DocumentKey == DocumentKey).FirstOrDefault().Document.FileName;
                string fname = Server.MapPath("~/Document/Insurance/"+InsuranceKey+""+ff);
                string filename = Server.MapPath("~/Document/Insurance/"+CompanyKey+"/"+InsuranceKey + "/" + ff);
                string ffname = Server.MapPath("~/Document/Insurance/" + InsuranceKey + " " + ff);
                string ffpath = Server.MapPath("~/Document/Insurance/" + InsuranceKey + " "+ ff);
                string fpath = Server.MapPath("~/Document/Insurance/"+InsuranceKey+ff);
                //string path = Server.MapPath("~/Document/Insurance/" + CompanyKey + "/" + InsuranceKey + "/" + filename);
                string path = Server.MapPath("~/Document/Insurance/"+CompanyKey+"/" + InsuranceKey + "/" + ff);

                if (System.IO.File.Exists(filename))
                {
                    byte[] fileBytes = System.IO.File.ReadAllBytes(path);
                    if (fileBytes.Length > 0)
                    {
                    
                        return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, ff);
                    }
                }
                else if (System.IO.File.Exists(fname))
                {

                    byte[] fileBytes = System.IO.File.ReadAllBytes(fpath);
                    if (fileBytes.Length > 0)
                    {
                        return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, ff);
                    }
                }

                else if (System.IO.File.Exists(ffname))
                {

                    byte[] fileBytes = System.IO.File.ReadAllBytes(ffpath);
                    if (fileBytes.Length > 0)
                    {
                        return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, ff);
                    }
                }

                TempData["Error"] = "File not found";
                System.Web.Routing.RouteValueDictionary rvd = new System.Web.Routing.RouteValueDictionary();
                rvd.Add("CompanyKey", CompanyKey);
                return RedirectToAction(refView, rvd);
            }
            catch
            {
                TempData["Error"] = "Opps... Something went wrong.";
                System.Web.Routing.RouteValueDictionary rvd = new System.Web.Routing.RouteValueDictionary();
                rvd.Add("CompanyKey", CompanyKey);
                return RedirectToAction(refView, rvd);
            }


        }
        public ActionResult DownloadSample()
        {
            string path = Server.MapPath("~/Document/SampleFiles/");
            string filename = "SampleVendorFile.csv";
            if (System.IO.File.Exists(Path.Combine(path, filename)))
            {
                byte[] filebytes = System.IO.File.ReadAllBytes(Path.Combine(path,filename));
                if(filebytes.Length > 0)
                {
                    return File(filebytes, System.Net.Mime.MediaTypeNames.Application.Octet, "SampleVendorFile.csv");
                }
            }
            return null;
        }
        [HttpPost]
        public ActionResult DeleteDocument(int DocumentKey, int CompanyKey, int InsuranceKey)
        {
            try
            {
                var doc = __vendorManagerservice.GetbindDocumentByInsuranceKey(CompanyKey, InsuranceKey);
                string filename = doc.Where(w => w.Document.DocumentKey == DocumentKey).FirstOrDefault().Document.FileName;
                string path = Server.MapPath("~/Document/Insurance/" + CompanyKey + "/" + InsuranceKey + "/" + filename);

                bool res = __vendorManagerservice.VendorManagerRemoveDocument(DocumentKey);
                if (res)
                {
                    res = __vendorManagerservice.VendorManagerRemoveInsurance(InsuranceKey);
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                }


                if (res)
                    TempData["Sucessmessage"] = "Insurance Deleted.";
                else
                    TempData["Error"] = "Opps... Something went wrong.";
            }
            catch
            {
                TempData["Error"] = "Opps... Something went wrong.";
            }
            System.Web.Routing.RouteValueDictionary rvd = new System.Web.Routing.RouteValueDictionary();
            rvd.Add("CompanyKey", CompanyKey);
            return RedirectToAction("VendorView", rvd);

        }
        public JsonResult IndexvendorPaging(Int64 PageSize, Int64 PageIndex, string Search, String Sort)
        {
            IList<VendorManagerVendorModel> itemList = null;
            itemList = __vendorManagerservice.SearchApprovedVendor(PageSize, PageIndex, Search.Trim(), Sort);
            return Json(itemList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult IndexvendorPagingInvited(Int64 PageSize, Int64 PageIndex, string Search, String Sort)
        {
            IList<VendorManagerVendorModel> itemList = null;
            itemList = __vendorManagerservice.SearchPendingVendor(PageSize, PageIndex, Search.Trim(), Sort);
            return Json(itemList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult IndexvendorPagingUnapproved(Int64 PageSize, Int64 PageIndex, string Search, String Sort,string Duplicate)
        {
            IList<VendorManagerVendorModel> itemList = null;
            long ResourceKey = Convert.ToInt64(Session["resourceid"]);
            itemList = __vendorManagerservice.SearchUnapprovedVendor(PageSize, PageIndex, Search.Trim(), Sort,ResourceKey, Duplicate);
            if (itemList.Count > 0)
                itemList.ToList().ForEach(f => f.isPriority = false);
            return Json(itemList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult IndexvendorPagingUnapprovedPriority(Int64 PageSize, Int64 PageIndex, string Search, String Sort, string Duplicate)
        {
            IList<VendorManagerVendorModel> itemList = null;
            long ResourceKey = Convert.ToInt64(Session["resourceid"]);
            itemList = __vendorManagerservice.SearchUnapprovedVendorPriority(PageSize, PageIndex, Search.Trim(), Sort, ResourceKey, Duplicate);
            if (itemList.Count > 0)
                itemList.ToList().ForEach(f => f.isPriority = true);
            return Json(itemList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ViewBidRequest(int BidRequestKey,int CompanyKey)
        {
            VendorManagerModel managerModel = new VendorManagerModel();
            managerModel = __vendorManagerservice.VendorManagerGetBidRequestDetails(BidRequestKey, CompanyKey);
            
            return View(managerModel);
        }
        public JsonResult GetVenderBidRequestDocumentListJson(Int32 PageSize, Int32 PageIndex, string Sort, string Search, Int64 BidVendorKey, string TableName)
        {
            var bidRequestModel = __vendorManagerservice.SearchBidRequestVenderDocjson(PageSize, PageIndex, Search, Sort, BidVendorKey, TableName);
            return Json(bidRequestModel, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SendInsertMessage(string Body, string Status, Int64 ObjectKey, Int64 ResourceKey, string ModuleKeyName)
        {
            var bidRequestModel = this.__vendorManagerservice.SendInsertMessage(Body, Status, ObjectKey, ResourceKey, ModuleKeyName);
            return Json(bidRequestModel, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SendInsertMessageList(Int64 ObjectKey, Int64 ResourceKey, string ModuleKeyName, string UpdatemsgStatus)
        {
            var bidRequestModel = this.__vendorManagerservice.SendInsertMessageList(ObjectKey, ResourceKey, ModuleKeyName, UpdatemsgStatus);
            return Json(bidRequestModel, JsonRequestBehavior.AllowGet);
        }
        public JsonResult MessageNewCount(Int64 ObjectKey, Int64 ResourceKey, string ModuleKeyName)
        {
            var bidRequestModel = __vendorManagerservice.MessageNewCount(0, 0, ModuleKeyName, Convert.ToInt64(Session["userid"]));
            return Json(bidRequestModel, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetVenderBidListJson(Int64 BidVendorKey, Int64 ResourceKey)
        {
            IBidRequestService vendorService = new BidRequestService();
           
            var bidRequestModel = vendorService.GetVenderBidList(BidVendorKey, ResourceKey);
            return Json(bidRequestModel, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ViewWorkOrder(int BidRequestKey, int CompanyKey)
        {
            VendorManagerModel managerModel = new VendorManagerModel();
            managerModel = __vendorManagerservice.VendorManagerGetWorkOrderDetails(BidRequestKey, CompanyKey);

            return View(managerModel);
        }
        public JsonResult SearchVendorByBidRequest()
        {
            List<BidRequestModel> lstVendor = null;
            int BidRequestKey = Convert.ToInt32(Request.Form["BidRequestKey"].ToString());
            int modulekey = Convert.ToInt32(Request.Form["Modulekey"].ToString());
            lstVendor = __vendorManagerservice.SearchVendorByBidRequest(BidRequestKey, modulekey, Convert.ToInt32(Session["resourceid"]));
            return Json(lstVendor, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchAllVendor()
        {
            try
            {
                int IsStaredVendor = 0;
                bool value = false;
                List<BidRequestModel> lstAllVendor = null;
                BidRequestModel bidRequestModel = new BidRequestModel();

                string SearchVendorName = Request.Form["SearchVendorName"].ToString();
                string SearchCompanyName = Request.Form["SearchCompanyName"].ToString();
                IsStaredVendor = Convert.ToInt32(Request.Form["IsStaredVendor"].ToString());
                int LastWorkedBefore = Convert.ToInt32(Request.Form["LastWorkedBefore"].ToString());
                int BidRequestKey = Convert.ToInt32(Request.Form["BidRequestKey"].ToString());

                lstAllVendor = __vendorManagerservice.SearchAllVendor(BidRequestKey, SearchVendorName, SearchCompanyName, IsStaredVendor, LastWorkedBefore);
                //lstAllVendor = _bidRequestservice.SearchAllVendor();

                return Json(lstAllVendor);
            }
            catch (Exception ex)
            {
                return Json(null);
            }

        }
        public JsonResult bindBidRequestDocument(int BidRequestKey)
        {
            try
            {
                IList<BidRequestModel> Documentlist = null;
                Documentlist = __vendorManagerservice.GetbindDocumentByBidRequestKey(BidRequestKey,106);
                return Json(Documentlist, JsonRequestBehavior.AllowGet);

            }
            catch
            {
                return Json(null);
            }
        }
        public JsonResult Noteslist(int BidRequestKey)
        {
            try
            {
                IList<BidRequestModel> Noteslist = null;
                Noteslist = __vendorManagerservice.GetbindBidRequestNotes(BidRequestKey);
                return Json(Noteslist, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(null);
            }
        }

        public JsonResult Notessave()
        {
            try
            {
                bool value = false;
                if (Request.Form.Count > 0)
                {
                    string notes = "";
                    string description = "";
                    int BidRequestKey = 0;
                    int Resourcekey = 0;
                    notes = Request.Form["txtnotetitle"].ToString();
                    description = Request.Form["txtnotedescription"].ToString();
                    BidRequestKey = Convert.ToInt32(Request.Form["BidRequestKey"]);
                    Resourcekey = Convert.ToInt32(Request.Form["ResourceKey"]);
                    value = __vendorManagerservice.InsertNotes(notes, description, BidRequestKey, Resourcekey);
                }
                if (value == true)
                {
                    return Json("Sucess", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("failed", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToLower().ToString());
                return Json(null);
            }
        }

        public JsonResult NotesRemove(int Noteid)
        {
            try
            {
                bool val = false;
                val = __vendorManagerservice.NotesRemove(Noteid);
                return Json(val, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(null);
            }
        }

        public ActionResult VendorManagerBidRequest()
        {
            return View();
        }
        [HttpPost]
        public JsonResult EditVendorService()
        {
            int CompanyKey = 0;
            string Service = "";
            bool val = false;
            CompanyKey = Convert.ToInt32(Request.Form["vendorkey"]);
            Service = Convert.ToString(Request.Form["Service"]);
          
            val = __vendorManagerservice.EditService(CompanyKey, Service);
            return Json(val, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Membership()
        {
            int VendorKey = Convert.ToInt32(Session["CompanyKey"]);
            List<MembershipModel> MemberShipRecord = null;
            MembershipModel membership = new MembershipModel();
            membership = _mEmberShipService.GetDataViewEdit(VendorKey);
            
            //return View(membership);
            return Json(membership, JsonRequestBehavior.AllowGet);
        }
    }


    public class VendorManagerServiceModel
    {
        public int Id { get; set; }
        public int VendorServiceKey { get; set; }
        public int value { get; set; }
        public string Title { get; set; }
    }
}