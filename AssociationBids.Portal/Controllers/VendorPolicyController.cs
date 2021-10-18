using AssociationBids.Portal.Model;
using AssociationBids.Portal.Service.Base;
using AssociationBids.Portal.Service.Base.Code;
using AssociationBids.Portal.Service.Base.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace AssociationBids.Portal.Controllers
{
    public class VendorPolicyController : Controller
    {
        private IVendorPolicyService _vendorPolicy;

        public VendorPolicyController(IVendorPolicyService policyService)
        {
            _vendorPolicy = policyService;
        }

        // GET: VendorPolicy
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PolicyList()
        {
            return View();
        }
        public ActionResult PolicyView()
        {
            return View();
        }
        public ActionResult AddPolicy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddPolicy(VendorManagerModel model, HttpPostedFileBase[] files)
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
                        //Directory.CreateDirectory(Server.MapPath("~/Document/Insurance/" + iKey));
                        //file.SaveAs(Server.MapPath("~/Document/Insurance/" +iKey+ file.FileName));


                        
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/Document/Insurance/"), iKey + file.FileName);

                        Directory.CreateDirectory(Server.MapPath("~/Document/Insurance/" + iKey));
                        file.SaveAs(Server.MapPath("~/Document/Insurance/" + iKey + fileName));



                    }
                }
            }

            if (iKey > 0)
                TempData["Success"] = "Insurance Added";
            else
                TempData["Error"] = "Something went Wrong...";
            return RedirectToAction("PolicyList");
        }


        [HttpPost]
        public JsonResult IndexinsurancePaging(int PageSize, int PageIndex, string Search, string Sort)
        {
            try
            {
                var c = Session["companykey"];
                int CompanyKey = Convert.ToInt32(c);
                List<InsuranceModel> itemList = _vendorPolicy.GetInsurancePaging(CompanyKey, PageSize, PageIndex, Search, Sort);

                return Json(itemList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(null);
            }
        }

        public JsonResult GetInsuranceDetails(int InsuranceKey)
        {
            try
            {
                IList<VendorManagerModel> Documentlist = null;
                int CompanyKey = Convert.ToInt32(Session["companykey"]);
                Documentlist = _vendorPolicy.GetbindDocumentByInsuranceKey(CompanyKey, InsuranceKey);
                List<System.Web.Mvc.SelectListItem> Documentlistsss = new List<System.Web.Mvc.SelectListItem>();
                string[] doc = new string[Documentlist.Count];
                for (int i = 0; i < Documentlist.Count; i++)
                {
                    if (Documentlist[i].Document.FileName != "")
                    {
                        var Text = Convert.ToString(Documentlist[i].Document.FileName);
                        string imagelist = "../Document/Insurance/" + "BidRequestKeyTemp " + Documentlist[i].Document.FileName;
                        if (Documentlist[i].Document.FileName.Contains(InsuranceKey + " "))
                            imagelist = "../Document/Insurance/"+ Documentlist[i].Document.FileName;
                        var path = Server.MapPath(imagelist);

                        string input = Documentlist[i].Document.FileName;
                        string[] values = input.Split('.');
                        var checkext = values[1];

                        doc[i] = imagelist;
                    }

                }
                return Json(Documentlist, JsonRequestBehavior.AllowGet);

            }
            catch
            {
                return Json(null);
            }
        }

        public ActionResult DownloadFile(int DocumentKey, int CompanyKey, int InsuranceKey)
        {
            try
            {
                var doc = _vendorPolicy.GetbindDocumentByInsuranceKey(CompanyKey, InsuranceKey);
                string filename = doc.Where(w => w.Document.DocumentKey == DocumentKey).FirstOrDefault().Document.FileName;

                string path = Server.MapPath("~/Document/Insurance/" + InsuranceKey+filename);
                byte[] fileBytes = System.IO.File.ReadAllBytes(path);
                if (fileBytes.Length > 0)
                {
                    return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, filename);
                }

                TempData["Error"] = "File not found";

                return RedirectToAction("PolicyList");
            }
            catch
            {
                TempData["Error"] = "Opps... Something went wrong.";

                return RedirectToAction("PolicyList");
            }


        }

        public ActionResult EditPolicy(int InsuranceKey)
        {
            //return Json(Documentlist, JsonRequestBehInsuranceKeyavior.AllowGet);
            ViewBag.InsuranceKey = InsuranceKey;

            return View();
        }

        [HttpPost]
        public ActionResult EditPolicy(VendorManagerModel model)
        {
            try
            {
                int insurancekey = model.Insurance.InsuranceKey;
                int CompanyKey = Convert.ToInt32(Session["companykey"]);


                model.Insurance.VendorKey = CompanyKey;

                long iKey = _vendorPolicy.VendorManagerEditInsurance(model.Insurance);

                if (iKey > 0)
                    TempData["Success"] = "Insurance Updated";
                else
                    TempData["Error"] = "Something went Wrong...";

                return RedirectToAction("PolicyList");
            }
            catch (Exception ex)
            {
                return RedirectToAction("PolicyList");
            }
        }
        public ActionResult DocumentDelete(int InsuranceKey, string Docname)
        {
            try
            {
                string Directory = Server.MapPath("~/Document/Insurance/");
                string fileName = Docname;
                string path = Directory + InsuranceKey + " " + Docname;
                FileInfo fileInfo = new FileInfo(path);
                if (fileInfo.Exists)
                {
                    fileInfo.Delete();
                }
                bool value = false;
                value = _vendorPolicy.DocumentDelete(InsuranceKey, Docname);
                return RedirectToAction("EditPolicy", new { InsuranceKey });
            }
            catch (Exception ex)
            {
                return RedirectToAction("EditPolicy", new { InsuranceKey });
            }
        }

        public JsonResult UpdateDocUpload()
        {
            try
            {
                bool value = false;
                List<BidRequestModel> lstVendor = null;
                VendorManagerModel bidRequestModel = new VendorManagerModel();
                if (Request.Form.Count > 0)
                {
                    int InsuranceKey = 0;
                    StringBuilder FileName = new StringBuilder();
                    StringBuilder FileSize = new StringBuilder();
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        var file = Request.Files[i];
                        FileName.Append(Path.GetFileName(file.FileName));
                        FileName.Append(",");
                        FileSize.Append(file.ContentLength);
                        FileSize.Append(",");
                    }
                    if (Request.Files.Count != 0)
                    {
                        FileName.Remove(FileName.Length - 1, 1);
                        FileSize.Remove(FileSize.Length - 1, 1);
                    }

                    InsuranceKey = Convert.ToInt32(Request.Form["InsuranceKey"]);
                    //int modulekey = Convert.ToInt32(Request.Form["Modulekey"]);
                    var module = new ModuleService().GetAll(new ModuleFilterModel());
                    var key = module.Where(w => w.Title == "Insurance").FirstOrDefault().ModuleKey;
                    value = _vendorPolicy.UpdateDocInsert(InsuranceKey, FileName.ToString(), FileSize.ToString(), key);

                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        var file = Request.Files[i];
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/Document/Insurance/"), InsuranceKey+fileName);

                        Directory.CreateDirectory(Server.MapPath("~/Document/Insurance/" + InsuranceKey));
                        file.SaveAs(Server.MapPath("~/Document/Insurance/" + InsuranceKey + fileName));
                    


                    }

                    //bidRequestModel.Insurance.InsuranceKey = InsuranceKey;
                }
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(null);
            }
        }

        public JsonResult UpdateRegistrationDocUpload()
        {
            try
            {
                bool value = false;
                List<BidRequestModel> lstVendor = null;
                VendorManagerModel bidRequestModel = new VendorManagerModel();
                if (Request.Form.Count > 0)
                {
                    int InsuranceKey = 0;
                    StringBuilder FileName = new StringBuilder();
                    StringBuilder FileSize = new StringBuilder();
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        var file = Request.Files[i];
                        FileName.Append(Path.GetFileName(file.FileName));
                        FileName.Append(",");
                        FileSize.Append(file.ContentLength);
                        FileSize.Append(",");

                        InsuranceKey = Convert.ToInt32(Request.Form["InsuranceKey"]);
                        //int modulekey = Convert.ToInt32(Request.Form["Modulekey"]);
                        var module = new ModuleService().GetAll(new ModuleFilterModel());
                        var key = module.Where(w => w.Title == "Insurance").FirstOrDefault().ModuleKey;
                        value = _vendorPolicy.UpdateDocInsert(InsuranceKey, file.FileName, file.ContentLength.ToString(), key);
                    }
                    if (Request.Files.Count != 0)
                    {
                        FileName.Remove(FileName.Length - 1, 1);
                        FileSize.Remove(FileSize.Length - 1, 1);
                    }

                    

                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        var file = Request.Files[i];
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/Document/Insurance/"), InsuranceKey + fileName);

                        Directory.CreateDirectory(Server.MapPath("~/Document/Insurance/" + InsuranceKey));
                        file.SaveAs(Server.MapPath("~/Document/Insurance/" + InsuranceKey + fileName));



                    }

                    //bidRequestModel.Insurance.InsuranceKey = InsuranceKey;
                }
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(null);
            }
        }
    }
}