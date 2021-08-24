using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using AssociationBids.Portal.Model;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

namespace AssociationBids.Portal.Controllers
{
    public class AcitivitybyManagementCompanyController : Controller
    {
        private readonly AssociationBids.Portal.Service.Base.Interface.IAcitivitybyManagementCompanyService _AcitivitybyManagementCompanyService;

        public AcitivitybyManagementCompanyController(AssociationBids.Portal.Service.Base.Interface.IAcitivitybyManagementCompanyService AcitivitybyManagementCompanyService)
        {
            this._AcitivitybyManagementCompanyService = AcitivitybyManagementCompanyService;
        }
        
        // GET: AcitivitybyManagementCompany
        public ActionResult AcitivitybyManagementCompanyList()
        {

            IList<AcitivitybyManagementCompanyModel> lststate = null;
            lststate = _AcitivitybyManagementCompanyService.GetAllState();
            List<System.Web.Mvc.SelectListItem> lststatelist = new List<System.Web.Mvc.SelectListItem>();
            System.Web.Mvc.SelectListItem sli2 = new System.Web.Mvc.SelectListItem();
            sli2.Text = "Please Select";
            sli2.Value = "0";
            lststatelist.Add(sli2);
            for (int i = 0; i < lststate.Count; i++)
            {
                System.Web.Mvc.SelectListItem sli = new System.Web.Mvc.SelectListItem();
                sli.Text = Convert.ToString(lststate[i].State);
                sli.Value = Convert.ToString(lststate[i].StateKey);
                lststatelist.Add(sli);
            }


            ViewBag.lstStatus = lststatelist;
            return View();
        }

        public ActionResult PMAcitivitybyManagementCompanyList()
        {
            IList<AcitivitybyManagementCompanyModel> lststate = null;
            lststate = _AcitivitybyManagementCompanyService.GetAllState();
            List<System.Web.Mvc.SelectListItem> lststatelist = new List<System.Web.Mvc.SelectListItem>();
            System.Web.Mvc.SelectListItem sli2 = new System.Web.Mvc.SelectListItem();
            sli2.Text = "Please Select";
            sli2.Value = "0";
            lststatelist.Add(sli2);
            for (int i = 0; i < lststate.Count; i++)
            {
                System.Web.Mvc.SelectListItem sli = new System.Web.Mvc.SelectListItem();
                sli.Text = Convert.ToString(lststate[i].State);
                sli.Value = Convert.ToString(lststate[i].StateKey);
                lststatelist.Add(sli);
            }


            ViewBag.lstStatus = lststatelist;
            return View();
        }

        public ActionResult ActivitybyVendorList()
        {
            int CompanyKey = 0;
            int PortalKey = Convert.ToInt32(Session["PortalKey"]);
            if (PortalKey == 1)
            {
                CompanyKey = Convert.ToInt32(0);
            }
            else
            {
                CompanyKey = Convert.ToInt32(Session["CompanyKey"]);
            }
            IList<AcitivitybyManagementCompanyModel> lststate = null;
            lststate = _AcitivitybyManagementCompanyService.GetAllVendorList(CompanyKey);
            List<System.Web.Mvc.SelectListItem> lststatelist = new List<System.Web.Mvc.SelectListItem>();
            System.Web.Mvc.SelectListItem sli2 = new System.Web.Mvc.SelectListItem();
            sli2.Text = "Please Select";
            sli2.Value = "0";
            lststatelist.Add(sli2);
            for (int i = 0; i < lststate.Count; i++)
            {
                System.Web.Mvc.SelectListItem sli = new System.Web.Mvc.SelectListItem();
                sli.Text = Convert.ToString(lststate[i].State);
                sli.Value = Convert.ToString(lststate[i].StateKey);
                lststatelist.Add(sli);
            }


            ViewBag.lstStatus = lststatelist;
            return View();
        }

        public ActionResult PMActivitybyVendorList()
        {
            int CompanyKey = 0;
            int PortalKey = Convert.ToInt32(Session["PortalKey"]);
            if (PortalKey == 1)
            {
                CompanyKey = Convert.ToInt32(0);
            }
            else
            {
                CompanyKey = Convert.ToInt32(Session["CompanyKey"]);
            }
            IList<AcitivitybyManagementCompanyModel> lststate = null;
            lststate = _AcitivitybyManagementCompanyService.GetAllVendorList(CompanyKey);
            List<System.Web.Mvc.SelectListItem> lststatelist = new List<System.Web.Mvc.SelectListItem>();
            System.Web.Mvc.SelectListItem sli2 = new System.Web.Mvc.SelectListItem();
            sli2.Text = "Please Select";
            sli2.Value = "0";
            lststatelist.Add(sli2);
            for (int i = 0; i < lststate.Count; i++)
            {
                System.Web.Mvc.SelectListItem sli = new System.Web.Mvc.SelectListItem();
                sli.Text = Convert.ToString(lststate[i].State);
                sli.Value = Convert.ToString(lststate[i].StateKey);
                lststatelist.Add(sli);
            }


            ViewBag.lstStatus = lststatelist;
            return View();
        }

        public ActionResult VDActivitybyVendorList()
        {
            return View();
        }

        public ActionResult PMActivitybyAssociationList()
        {
            int CompanyKey = 0;
            CompanyKey = Convert.ToInt32(Session["CompanyKey"]);

            IList<AcitivitybyManagementCompanyModel> lststate = null;
            lststate = _AcitivitybyManagementCompanyService.GetAllProperty(CompanyKey);
            List<System.Web.Mvc.SelectListItem> lststatelist = new List<System.Web.Mvc.SelectListItem>();
            System.Web.Mvc.SelectListItem sli2 = new System.Web.Mvc.SelectListItem();
            sli2.Text = "Please Select";
            sli2.Value = "0";
            lststatelist.Add(sli2);
            for (int i = 0; i < lststate.Count; i++)
            {
                System.Web.Mvc.SelectListItem sli = new System.Web.Mvc.SelectListItem();
                sli.Text = Convert.ToString(lststate[i].State);
                sli.Value = Convert.ToString(lststate[i].StateKey);
                lststatelist.Add(sli);
            }

            ViewBag.lstStatus = lststatelist;
            return View();
        }

        public ActionResult PMActivitybyManagerList()
        {
            int CompanyKey = 0;
            CompanyKey = Convert.ToInt32(Session["CompanyKey"]);

            IList<AcitivitybyManagementCompanyModel> lststate = null;
            lststate = _AcitivitybyManagementCompanyService.GetAllManager(CompanyKey);
            List<System.Web.Mvc.SelectListItem> lststatelist = new List<System.Web.Mvc.SelectListItem>();
            System.Web.Mvc.SelectListItem sli2 = new System.Web.Mvc.SelectListItem();
            sli2.Text = "Please Select";
            sli2.Value = "0";
            lststatelist.Add(sli2);
            for (int i = 0; i < lststate.Count; i++)
            {
                System.Web.Mvc.SelectListItem sli = new System.Web.Mvc.SelectListItem();
                sli.Text = Convert.ToString(lststate[i].State);
                sli.Value = Convert.ToString(lststate[i].StateKey);
                lststatelist.Add(sli);
            }


            ViewBag.lstStatus = lststatelist;
            return View();
        }

        public JsonResult AcitivitybyManagementCompanyIndexPaging(Int64 ReportPageSize, Int64 PageIndex, string Search, String Sort)
        {
            int CompanyKey = 0;
            int PortalKey = Convert.ToInt32(Session["PortalKey"]);
            if (PortalKey == 1)
            {
                CompanyKey = Convert.ToInt32(Search);
            }
            else
            {
                CompanyKey = Convert.ToInt32(Session["CompanyKey"]);
            }
            List<AcitivitybyManagementCompanyModel> lstEmailTemplate = null;
            lstEmailTemplate = _AcitivitybyManagementCompanyService.Activity(ReportPageSize, PageIndex, Search.Trim(), Sort, CompanyKey);
            return Json(lstEmailTemplate, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AcitivitybyVendorIndexPaging(Int64 ReportPageSize, Int64 PageIndex, string Search, String Sort)
        {
            int CompanyKey = 0;
            int PortalKey = Convert.ToInt32(Session["PortalKey"]);
            if (PortalKey == 1)
            {
                CompanyKey = Convert.ToInt32(Search);
            }
            else
            {
                CompanyKey = Convert.ToInt32(Session["CompanyKey"]);
            }
            List<AcitivitybyManagementCompanyModel> lstEmailTemplate = null;
            lstEmailTemplate = _AcitivitybyManagementCompanyService.ActivityVendor(ReportPageSize, PageIndex, Search.Trim(), Sort, CompanyKey);
            return Json(lstEmailTemplate, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AcitivitybyVendorPortalIndexPaging(Int64 ReportPageSize, Int64 PageIndex, string Search, String Sort)
        {
            int ResourceKey = Convert.ToInt32(Session["resourceid"]);

            List<AcitivitybyManagementCompanyModel> lstEmailTemplate = null;
            lstEmailTemplate = _AcitivitybyManagementCompanyService.VendorPortalActivity(ReportPageSize, PageIndex, Search.Trim(), Sort, ResourceKey);
            return Json(lstEmailTemplate, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AcitivitybyAssociationIndexPaging(Int64 ReportPageSize, Int64 PageIndex, string Search, String Sort)
        {
            int CompanyKey = Convert.ToInt32(Session["CompanyKey"]);

            List<AcitivitybyManagementCompanyModel> lstEmailTemplate = null;
            lstEmailTemplate = _AcitivitybyManagementCompanyService.ActivityAssociation(ReportPageSize, PageIndex, Search.Trim(), Sort, CompanyKey);
            return Json(lstEmailTemplate, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AcitivitybyManagerIndexPaging(Int64 ReportPageSize, Int64 PageIndex, string Search, String Sort)
        {
            int CompanyKey = Convert.ToInt32(Session["CompanyKey"]);
            int PortalKey = Convert.ToInt32(Session["PortalKey"]);

            List<AcitivitybyManagementCompanyModel> lstEmailTemplate = null;
            lstEmailTemplate = _AcitivitybyManagementCompanyService.ActivityByManager(ReportPageSize, PageIndex, Search.Trim(), Sort, CompanyKey, PortalKey);
            return Json(lstEmailTemplate, JsonRequestBehavior.AllowGet);
        }

        
        public ActionResult ExportToExcel(int ReportPage, int PageSize, string Search, string Short, string PageName)
        {
            int CompanyKey = 0;
            string ExcelName = "";
            int PortalKey = Convert.ToInt32(Session["PortalKey"]);
            int ResourceKey = Convert.ToInt32(Session["resourceid"]);
            if (PortalKey == 1)
            {
                CompanyKey = 0;
            }
            else
            {
                CompanyKey = Convert.ToInt32(Session["CompanyKey"]);
            }
            if (Search == "undefined ")
            {
                Search = "";
            }
            
            DataTable dtProduct = new DataTable();
            List<AcitivitybyManagementCompanyModel> lstEmailTemplate = null;

                
            if (PageName == "AcitivitybyManagementCompanyList" || PageName == "PMAcitivitybyManagementCompanyList")
            {
                ExcelName = "Acitivity by Management Company";
                lstEmailTemplate = _AcitivitybyManagementCompanyService.Activity(ReportPage, PageSize, Search, Short, CompanyKey);
            }
            else if (PageName == "ActivitybyVendorList" || PageName == "PMActivitybyVendorList")
            {
                ExcelName = "Activity by Vendor";
                lstEmailTemplate = _AcitivitybyManagementCompanyService.ActivityVendor(ReportPage, PageSize, Search, Short, CompanyKey);
            }
            else if (PageName == "VDActivitybyVendorList")
            {
                ExcelName = "Activity by Vendor";
                lstEmailTemplate = _AcitivitybyManagementCompanyService.ActivityVendor(ReportPage, PageSize, Search, Short, CompanyKey);
            }
            else if (PageName == "PMActivitybyAssociationList")
            {
                ExcelName = "Activity by Property";
                lstEmailTemplate = _AcitivitybyManagementCompanyService.ActivityAssociation(ReportPage, PageSize, Search, Short, CompanyKey);
            }
            else if (PageName == "PMActivitybyManagerList")
            {
                ExcelName = "Activity by Manager";
                lstEmailTemplate = _AcitivitybyManagementCompanyService.ActivityByManager(ReportPage, PageSize, Search, Short, CompanyKey,PortalKey);
            }

            dtProduct = ConvertListToDataTable(lstEmailTemplate, PageName);
            if (dtProduct.Rows.Count > 0)
            {
                ExportToExcel1(dtProduct, ExcelName);
            }
            //using (XLWorkbook woekBook = new XLWorkbook())
            //{
            //    woekBook.Worksheets.Add(dtProduct);
            //    using (MemoryStream stream = new MemoryStream())
            //    {
            //        woekBook.SaveAs(stream);
            //         File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ProductDetails.xlsx");
            //        
            //    }
            //}
            return View();
        }

        public ActionResult ExportToPdf(int ReportPage, int PageSize, string Search, string Short, string PageName)
        {
            int CompanyKey = 0;
            string PdfName = "";
            int PortalKey = Convert.ToInt32(Session["PortalKey"]);
            int ResourceKey = Convert.ToInt32(Session["resourceid"]);
            if (Convert.ToInt32(Session["PortalKey"]) == 1)
            {
                CompanyKey = 0;
            }
            else
            {
                CompanyKey = Convert.ToInt32(Session["CompanyKey"]);
            }
            if (Search == "undefined ")
            {
                Search = "";
            }
            DataTable dtProduct = new DataTable();
            List<AcitivitybyManagementCompanyModel> lstEmailTemplate = null;

            if (PageName == "AcitivitybyManagementCompanyList" || PageName == "PMAcitivitybyManagementCompanyList")
            {
                PdfName = "Acitivity by Management Company";
                lstEmailTemplate = _AcitivitybyManagementCompanyService.Activity(ReportPage, PageSize, Search, Short, CompanyKey);
            }
            else if (PageName == "ActivitybyVendorList" || PageName == "PMActivitybyVendorList")
            {
                PdfName = "Activity by Vendor";
                lstEmailTemplate = _AcitivitybyManagementCompanyService.ActivityVendor(ReportPage, PageSize, Search, Short, CompanyKey);
            }
            else if (PageName == "VDActivitybyVendorList")
            {
                PdfName = "Activity by Vendor";
                lstEmailTemplate = _AcitivitybyManagementCompanyService.ActivityVendor(ReportPage, PageSize, Search, Short, CompanyKey);
            }
            else if (PageName == "PMActivitybyAssociationList")
            {
                PdfName = "Activity by Property";
                lstEmailTemplate = _AcitivitybyManagementCompanyService.ActivityAssociation(ReportPage, PageSize, Search, Short, CompanyKey);
            }
            else if (PageName == "PMActivitybyManagerList")
            {
                PdfName = "Activity by Manager";
                lstEmailTemplate = _AcitivitybyManagementCompanyService.ActivityByManager(ReportPage, PageSize, Search, Short, CompanyKey, PortalKey);
            }

            dtProduct = ConvertListToDataTable(lstEmailTemplate,PageName);

            if (dtProduct.Rows.Count > 0)
            {
                ExportToPDF1(dtProduct, PdfName);
            }
            return View();
        }

        public void ExportToExcel1(DataTable objDt, string Filename)
        {
            string attachment = "attachment; filename=" + Filename + ".xls";
            HttpContext.Response.Clear();
            HttpContext.Response.ClearHeaders();
            HttpContext.Response.ClearContent();
            HttpContext.Response.AddHeader("content-disposition", attachment);
            HttpContext.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Response.ContentEncoding = Encoding.UTF8;
            string tab = "";

            foreach (DataColumn dc in objDt.Columns)
            {
                HttpContext.Response.Write(tab + dc.ColumnName);
                tab = "\t";
            }
            HttpContext.Response.Write("\n");
            int i;
            foreach (DataRow dr in objDt.Rows)
            {
                tab = "";
                for (i = 0; i < objDt.Columns.Count; i++)
                {
                    HttpContext.Response.Write(tab + dr[i].ToString().Replace("\r\n", ""));
                    tab = "\t";
                }
                HttpContext.Response.Write("\n");
            }
            HttpContext.Response.Flush();
            HttpContext.Response.End();
        }

        public void ExportToPDF1(DataTable tab, string FileName)
        {
            HttpContext.Response.ContentType = "application/pdf";
            HttpContext.Response.AddHeader("content-disposition", "attachment;filename=" + FileName + ".pdf");
            HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            hw.AddStyleAttribute("font-size", "8pt");
            GridView GridView1 = new GridView();

            if (tab.Rows.Count == 0)
            {
                tab = new DataTable();
                tab.Columns.Add("Message", typeof(string));
                DataRow dr = tab.NewRow();
                dr["Message"] = "No record found";
                tab.Rows.Add(dr);
            }

            GridView1.DataSource = tab;
            GridView1.DataBind();

            GridView1.AllowPaging = false;
            GridView1.DataBind();
            GridView1.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, HttpContext.Response.OutputStream);
            pdfDoc.Open();

            htmlparser.Parse(sr);
            pdfDoc.Close();
            HttpContext.Response.Write(pdfDoc);
            HttpContext.Response.End();
        }
        static DataTable ConvertListToDataTable(List<AcitivitybyManagementCompanyModel> list, string PageName)
        {
            // New table.
            DataTable table = new DataTable();

            // Get max columns.
            int columns = 0;
            if (PageName == "AcitivitybyManagementCompanyList" || PageName == "PMAcitivitybyManagementCompanyList")
            {
                table.Columns.Add("Company Name");
                columns++;
                table.Columns.Add("Resource Type");
                columns++;
                table.Columns.Add("Title");
                columns++;
            }
            else if (PageName == "ActivitybyVendorList" || PageName == "PMActivitybyVendorList")
            {
                table.Columns.Add("Vendor Name");
                columns++;
                table.Columns.Add("Resource Type");
                columns++;
                table.Columns.Add("Title");
                columns++;
                table.Columns.Add("Status");
                columns++;
            }
            else if (PageName == "VDActivitybyVendorList")
            {
              
                table.Columns.Add("Resource Type");
                columns++;
                table.Columns.Add("Title");
                columns++;
                table.Columns.Add("Status");
                columns++;
            }
            else if (PageName == "PMActivitybyAssociationList"  || PageName == "PMActivitybyManagerList")
            {
                table.Columns.Add("Property Title");
                columns++;
                table.Columns.Add("Resource Type");
                columns++;
                table.Columns.Add("Title");
                columns++;
                table.Columns.Add("Status");
                columns++;
            }
            else if (PageName == "AcitivitybyManagementCompanyList" || PageName == "ActivitybyVendorList" || PageName == "VDActivitybyVendorList")
            {
                table.Columns.Add("CompanyName");
                columns++;
            }
            else if (PageName == "AcitivitybyManagementCompanyList" || PageName == "ActivitybyVendorList" || PageName == "VDActivitybyVendorList")
            {
                table.Columns.Add("PropertyTitle");
                columns++;
                table.Columns.Add("BidRequestAmount");
                columns++;
                table.Columns.Add("BidTitle");
                columns++;
                table.Columns.Add("Title");
                columns++;
            }
            else if (PageName == "PMActivitybyManagerList")
            {
                table.Columns.Add("ManagerName");
                columns++;
            }
            //columns = 5;

            // Add columns.
            //for (int i = 0; i < columns; i++)
            //{
            //    table.Columns.Add();
            //}

            // Add rows.
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Title == "3rdJuly20PropertyA/t" || list[i].Title == "3rdJuly20PropertyA\t")
                {
                    list[i].Title = list[i].Title.Replace("\t" ,"");
                }
                if (PageName == "AcitivitybyManagementCompanyList" || PageName == "PMAcitivitybyManagementCompanyList")
                {
                    table.Rows.Add(list[i].CompanyName, list[i].ResourceType, list[i].Title);
                }
                else if (PageName == "ActivitybyVendorList" || PageName == "PMActivitybyVendorList")
                {
                    table.Rows.Add(list[i].CompanyName, list[i].ResourceType, list[i].Title, list[i].Statuss);
                }
                else if (PageName == "VDActivitybyVendorList")
                {
                    table.Rows.Add(list[i].ResourceType, list[i].Title, list[i].Statuss);
                }
                else if (PageName == "PMActivitybyAssociationList" || PageName == "PMActivitybyManagerList")
                {
                    table.Rows.Add(list[i].Title, list[i].ResourceType, list[i].Titles, list[i].Status);
                    //columns++;
                }
                //else if (PageName == "AcitivitybyManagementCompanyList" || PageName == "ActivitybyVendorList" || PageName == "VDActivitybyVendorList")
                //{
                //    table.Rows.Add(list[i].CompanyName, list[i].Title, list[i].BidRequest, list[i].BidTitle, list[i].Titless);
                //}
                //else if(PageName == "PMAcitivitybyManagementCompanyList" || PageName == "PMActivitybyVendorList" || PageName == "PMActivitybyAssociationList")
                //{
                //    table.Rows.Add(list[i].Title, list[i].BidRequest, list[i].BidTitle, list[i].Titless);
                //}
            }
            return table;
        }
    }
}