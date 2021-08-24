using AssociationBids.Portal.Model;
//using ClosedXML.Excel;
//using DocumentFormat.OpenXml.Wordprocessing;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
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
using Document = iTextSharp.text.Document;
using Font = iTextSharp.text.Font;
using PageSize = iTextSharp.text.PageSize;

namespace AssociationBids.Portal.Controllers
{

    public class VendorsWithOpenInvoiceController : Controller
    {
        private readonly AssociationBids.Portal.Service.Base.IVendorswithopeninvoiceService _VendorsWithOpeninvoiceServices;

        public VendorsWithOpenInvoiceController(AssociationBids.Portal.Service.Base.IVendorswithopeninvoiceService VendorsWithOpeninvoiceServices)
        {
            this._VendorsWithOpeninvoiceServices = VendorsWithOpeninvoiceServices;
        }
        // GET: VendorsWithOpenInvoice
        public ActionResult VendorsWithOpenInvoicelist()
        {
            return View();
        }
        public ActionResult Top5vendorInEachCategoryList()
        {
            IList<VendorswithinvoiceModel> lststate = null;
            lststate = _VendorsWithOpeninvoiceServices.GetAllState();
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

        public ActionResult PMTop5vendorInEachCategoryList()
        {
            IList<VendorswithinvoiceModel> lststate = null;
            lststate = _VendorsWithOpeninvoiceServices.GetAllState();
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

        public ActionResult Vendorswiththemostbidsaccepted()
        {
            return View();
        }
        public ActionResult Vendorswiththemostbidsubimtlist()
        {
            return View();
        }
        public ActionResult Vendorswiththemostbidsnotacceptedlist()
        {
            return View();
        }

        public ActionResult PMVendorswiththemostbidsaccepted()
        {
            return View();
        }

        public ActionResult PMVendorswiththemostbidsnotacceptedlist()
        {
            return View();
        }

        public ActionResult PMVendorswiththemostbidsubimtlist()
        {
            return View();
        }
        public JsonResult IndexrevendorsinvoicePaging(Int64 ReportPageSize, Int64 PageIndex, string Search, String Sort)
        {
            int CompanyKey = Convert.ToInt32(Session["CompanyKey"]);
            List<VendorswithinvoiceModel> lstEmailTemplate = null;
            lstEmailTemplate = _VendorsWithOpeninvoiceServices.SearchStaff(ReportPageSize, PageIndex, Search.Trim(), Sort);
            return Json(lstEmailTemplate, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IndexTop5revendorsCategoryPaging(Int64 ReportPageSize, Int64 PageIndex, string Search, String Sort)
        {
            int CompanyKey = 0;
            if (Convert.ToInt32(Session["PortalKey"]) == 1)
            {
                CompanyKey = 0;
            }
            else
            {
                CompanyKey = Convert.ToInt32(Session["CompanyKey"]);
            }
            List<VendorswithinvoiceModel> lstEmailTemplate = null;
            lstEmailTemplate = _VendorsWithOpeninvoiceServices.Top5vendor(ReportPageSize, PageIndex, Search.Trim(), Sort, CompanyKey);
            return Json(lstEmailTemplate, JsonRequestBehavior.AllowGet);
        }

        public JsonResult VendorswiththemostbidsacceptedIndexPaging(Int64 ReportPageSize, Int64 PageIndex, string Search, String Sort)
        {
            int CompanyKey = 0;
            if (Convert.ToInt32(Session["PortalKey"]) == 1)
            {
                CompanyKey = 0;
            }
            else
            {
                CompanyKey = Convert.ToInt32(Session["CompanyKey"]);
            }
            List<VendorswithinvoiceModel> lstEmailTemplate = null;
            lstEmailTemplate = _VendorsWithOpeninvoiceServices.VendorwithAccept(ReportPageSize, PageIndex, Search.Trim(), Sort, CompanyKey);
            return Json(lstEmailTemplate, JsonRequestBehavior.AllowGet);
        }
        public JsonResult VendorswiththemostbidsSubmitedIndexPaging(Int64 ReportPageSize, Int64 PageIndex, string Search, String Sort)
        {
            int CompanyKey = 0;
            if (Convert.ToInt32(Session["PortalKey"]) == 1)
            {
                CompanyKey = 0;
            }
            else
            {
                CompanyKey = Convert.ToInt32(Session["CompanyKey"]);
            }
            List<VendorswithinvoiceModel> lstEmailTemplate = null;
            lstEmailTemplate = _VendorsWithOpeninvoiceServices.VendorwithSubmit(ReportPageSize, PageIndex, Search.Trim(), Sort, CompanyKey);
            return Json(lstEmailTemplate, JsonRequestBehavior.AllowGet);
        }
        public JsonResult VendorswiththemostbidsnotacceptedIndexPaging(Int64 ReportPageSize, Int64 PageIndex, string Search, String Sort)
        {
            int CompanyKey = 0;
            if (Convert.ToInt32(Session["PortalKey"]) == 1)
            {
                CompanyKey = 0;
            }
            else
            {
                CompanyKey = Convert.ToInt32(Session["CompanyKey"]);
            }
            List<VendorswithinvoiceModel> lstEmailTemplate = null;
            lstEmailTemplate = _VendorsWithOpeninvoiceServices.VendorwithNotAccept(ReportPageSize, PageIndex, Search.Trim(), Sort, CompanyKey);
            return Json(lstEmailTemplate, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ExportToExcel(int ReportPage, int PageSize, string Search, string Short, string PageName)
        {
            DataTable dtProduct = new DataTable();
            List<VendorswithinvoiceModel> lstEmailTemplate = null;
            lstEmailTemplate = _VendorsWithOpeninvoiceServices.SearchStaff(ReportPage, PageSize, Search, Short);
            dtProduct = ConvertListToDataTable(lstEmailTemplate);

            if (dtProduct.Rows.Count > 0)
            {
                ExportToExcel1(dtProduct, "Vendors with open invoices");
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
            return Json(lstEmailTemplate, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ExportToPdf(int ReportPage, int PageSize, string Search, string Short, string PageName)
        {
            DataTable dtProduct = new DataTable();
            List<VendorswithinvoiceModel> lstEmailTemplate = null;
            lstEmailTemplate = _VendorsWithOpeninvoiceServices.SearchStaff(ReportPage, PageSize, Search, Short);
            dtProduct = ConvertListToDataTable(lstEmailTemplate);

            if (dtProduct.Rows.Count > 0)
            {
                ExportToPDF1(dtProduct, "Vendors with open invoices");
            }
            return Json(lstEmailTemplate, JsonRequestBehavior.AllowGet);
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

        static DataTable ConvertListToDataTable(List<VendorswithinvoiceModel> list)
        {
            // New table.
            DataTable table = new DataTable();

            // Get max columns.
            int columns = 0;
            table.Columns.Add("Vendor Name");
            table.Columns.Add("Email");
            table.Columns.Add("Contact Person Name");
            table.Columns.Add("Invoice Date");
            table.Columns.Add("Balance");
            columns = 5;

            // Add columns.
            //for (int i = 0; i < columns; i++)
            //{
            //    table.Columns.Add();
            //}

            // Add rows.
            for (int i = 0; i < list.Count; i++)
            {
                table.Rows.Add(list[i].CompanyName, list[i].Email, list[i].Name, list[i].InvoiceDate, list[i].Balance);

            }

            return table;
        }

        static DataTable ConvertListToDataTableTop5(List<VendorswithinvoiceModel> list,string PageName)
        {
            // New table.
            DataTable table = new DataTable();
            if (PageName == "Top5vendorInEachCategoryList" || PageName == "PMTop5vendorInEachCategoryList")
            {

                int columnss = 0;
                table.Columns.Add("Service Name");
                columnss++;
                table.Columns.Add("Company Name");
                columnss++;
                table.Columns.Add("Total Bid Value");
                columnss++;

            }
            else if (PageName == "Vendorswiththemostbidsaccepted" || PageName == "PMVendorswiththemostbidsaccepted")
            {
                int columnss = 0;
                table.Columns.Add("Vendor Name");
                columnss++;
                table.Columns.Add("Accepted Bids");
                columnss++;
            }
            else if (PageName == "Vendorswiththemostbidsnotacceptedlist" || PageName == "PMVendorswiththemostbidsnotacceptedlist")
            {
                int columnss = 0;

                table.Columns.Add("Vendor Name");
                columnss++;
                table.Columns.Add("Rejected Bids");
                columnss++;
            }
            else if (PageName == "Vendorswiththemostbidsubimtlist" || PageName == "PMVendorswiththemostbidsubimtlist")
            {
                int columnss = 0;

                table.Columns.Add("Vendor Name");
                columnss++;
                table.Columns.Add("Submitted Bids");
                columnss++;

            }
            else
            { // Get max columns.
                int columns = 0;
                table.Columns.Add("Vendor Name");
                table.Columns.Add("Company Name");
                //table.Columns.Add("Email");H:\JayDodiya\Backup 30-06-2020\New GithubAssociation\AssociationBids.Portal\Views\VendorsWithOpenInvoice\Vendorswiththemostbidsnotacceptedlist.cshtml
                //table.Columns.Add("Balance");
                //table.Columns.Add("InvoiceDate");
                columns = 2;
            }

            // Add columns.
            //for (int i = 0; i < columns; i++)
            //{
            //    table.Columns.Add();
            //}

            // Add rows.
            for (int i = 0; i < list.Count; i++)
            {
                if (PageName == "Top5vendorInEachCategoryList" || PageName == "PMTop5vendorInEachCategoryList")
                {
                    string TotalBidValue = Convert.ToString(String.Format("{0:0.00}", list[i].TotalBidValue));
                    table.Rows.Add(list[i].ServiceName, list[i].CompanyName, TotalBidValue);
                }
                else if (PageName == "Vendorswiththemostbidsaccepted" || PageName == "PMVendorswiththemostbidsaccepted")
                {
                    table.Rows.Add(list[i].VendorName, list[i].AcceptBid);
                }
                else if (PageName == "Vendorswiththemostbidsnotacceptedlist" || PageName == "PMVendorswiththemostbidsnotacceptedlist")
                {
                    table.Rows.Add(list[i].VendorName, list[i].RejectedBid);
                }
                else if (PageName == "Vendorswiththemostbidsubimtlist" || PageName == "PMVendorswiththemostbidsubimtlist")
                {
                    table.Rows.Add(list[i].VendorName, list[i].SubmittedBid);
                }

                else { table.Rows.Add(list[i].VendorName, list[i].CompanyName); }
                
            }
            return table;
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

        public ActionResult ExportToExcelTop5(int ReportPage, int PageSize, string Search, string Short, string PageName)
        {
            int CompanyKey = 0;
            string ExcelName = "";
            int PortalKey = Convert.ToInt32(Session["PortalKey"]);
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
            List<VendorswithinvoiceModel> lstEmailTemplate = null;

            if (PageName == "Top5vendorInEachCategoryList" || PageName == "PMTop5vendorInEachCategoryList")
            {
                ExcelName = "Top 5 vendors in each catagory";
                lstEmailTemplate = _VendorsWithOpeninvoiceServices.Top5vendor(ReportPage, PageSize, Search, Short, CompanyKey);
            }
            else if (PageName == "Vendorswiththemostbidsubimtlist" || PageName == "PMVendorswiththemostbidsubimtlist")
            {
                ExcelName = "Vendors with the most bids submitted";
                lstEmailTemplate = _VendorsWithOpeninvoiceServices.VendorwithSubmit(ReportPage, PageSize, Search, Short, CompanyKey);
            }
            else if (PageName == "Vendorswiththemostbidsaccepted" || PageName == "PMVendorswiththemostbidsaccepted")
            {
                ExcelName = "Vendors with the most bids accepted";
                lstEmailTemplate = _VendorsWithOpeninvoiceServices.VendorwithAccept(ReportPage, PageSize, Search, Short, CompanyKey);
            }
            else if (PageName == "Vendorswiththemostbidsnotacceptedlist" || PageName == "PMVendorswiththemostbidsnotacceptedlist")
            {
                ExcelName = "Vendors with the most bids not accepted";
                lstEmailTemplate = _VendorsWithOpeninvoiceServices.VendorwithNotAccept(ReportPage, PageSize, Search, Short, CompanyKey);
            }
            dtProduct = ConvertListToDataTableTop5(lstEmailTemplate,PageName);
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

        public ActionResult ExportToPdfTop5(int ReportPage, int PageSize, string Search, string Short, string PageName)
        {
            int CompanyKey = 0;
            string PDFName = "";
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
            List<VendorswithinvoiceModel> lstEmailTemplate = null;
            if (PageName == "Top5vendorInEachCategoryList" || PageName == "PMTop5vendorInEachCategoryList")
            {
                PDFName = "Top 5 vendors in each catagory";
                lstEmailTemplate = _VendorsWithOpeninvoiceServices.Top5vendor(ReportPage, PageSize, Search, Short, CompanyKey);
            }
            else if (PageName == "Vendorswiththemostbidsubimtlist" || PageName == "PMVendorswiththemostbidsubimtlist")
            {
                PDFName = "Vendors with the most bids submitted";
                lstEmailTemplate = _VendorsWithOpeninvoiceServices.VendorwithSubmit(ReportPage, PageSize, Search, Short, CompanyKey);
            }
            else if (PageName == "Vendorswiththemostbidsaccepted" || PageName == "PMVendorswiththemostbidsaccepted")
            {
                PDFName = "Vendors with the most bids accepted";
                lstEmailTemplate = _VendorsWithOpeninvoiceServices.VendorwithAccept(ReportPage, PageSize, Search, Short, CompanyKey);
            }
            else if (PageName == "Vendorswiththemostbidsnotacceptedlist" || PageName == "PMVendorswiththemostbidsnotacceptedlist")
            {
                PDFName = "Vendors with the most bids not accepted";
                lstEmailTemplate = _VendorsWithOpeninvoiceServices.VendorwithNotAccept(ReportPage, PageSize, Search, Short, CompanyKey);
            }
            dtProduct = ConvertListToDataTableTop5(lstEmailTemplate, PageName);

            if (dtProduct.Rows.Count > 0)
            {
                ExportToPDF1(dtProduct, PDFName);
            }
            return View();
        }
    }
}