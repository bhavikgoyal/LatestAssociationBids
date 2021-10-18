
using AssociationBids.Portal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Drawing;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml;
using Color = DocumentFormat.OpenXml.Wordprocessing.Color;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.IO.Packaging;

using DocumentFormat.OpenXml;
using A = DocumentFormat.OpenXml.Drawing;
using DW = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using PIC = DocumentFormat.OpenXml.Drawing.Pictures;
using DocumentFormat.OpenXml.Drawing.Wordprocessing;
using Wps = DocumentFormat.OpenXml.Office2010.Word.DrawingShape;
using System.Reflection;

namespace AssociationBids.Portal.Controllers
{
    public class BidReportController : Controller
    {
        // GET: BidReport
        private readonly AssociationBids.Portal.Service.Base.IBidRequestService _bidRequestservice;
        private readonly AssociationBids.Portal.Service.Base.IBidReportService _bidReportservice;
        private readonly AssociationBids.Portal.Service.Base.Interface.IAPIService __IAPIservice;

        public BidReportController(AssociationBids.Portal.Service.Base.IBidRequestService bidRequestService, AssociationBids.Portal.Service.Base.IBidReportService bidReportService, AssociationBids.Portal.Service.Base.Interface.IAPIService APIservicsse)
        {
            this._bidRequestservice = bidRequestService;
            this._bidReportservice = bidReportService;
            this.__IAPIservice = APIservicsse;
        }
        public ActionResult AdminBidReport()
        {
            Int32 resourcekey = Convert.ToInt32(Session["resourceid"]);
            IList<BidRequestModel> lstProperty = null;
            lstProperty = _bidRequestservice.GetAllProperty(0, 0);

            List<System.Web.Mvc.SelectListItem> lstPropertylist = new List<System.Web.Mvc.SelectListItem>();
            System.Web.Mvc.SelectListItem sli2 = new System.Web.Mvc.SelectListItem();
            sli2.Text = "--Please Select--";
            sli2.Value = "0";
            lstPropertylist.Add(sli2);
            for (int i = 0; i < lstProperty.Count; i++)
            {
                System.Web.Mvc.SelectListItem sli = new System.Web.Mvc.SelectListItem();
                sli.Text = Convert.ToString(lstProperty[i].Property);
                sli.Value = Convert.ToString(lstProperty[i].PropertyKey);
                lstPropertylist.Add(sli);
            }

            ViewBag.lstProperty = lstPropertylist;

            //var lstBidStatus = new List<SelectListItem>()
            //{
            //  new SelectListItem{ Value="0",Text="Please Select",Selected=true},
            //  new SelectListItem{ Value="1",Text="Pending"},
            //  new SelectListItem{ Value="2",Text="Approve"},
            //  new SelectListItem{ Value="3",Text="Unapprove"},
            //};
            //ViewBag.lstBidStatus = lstBidStatus;

            //list of Bid Request Status
            IList<LookUpModel> lstBidReqStatus = _bidRequestservice.GetBidRequetStatusFilter();
            List<System.Web.Mvc.SelectListItem> lstBidReqStatuslist = new List<System.Web.Mvc.SelectListItem>();
            lstBidReqStatuslist.Add(new SelectListItem() { Text = "Show All", Value = "0,0" });
            for (int i = 0; i < lstBidReqStatus.Count; i++)
            {
                lstBidReqStatuslist.Add(new SelectListItem() { Text = Convert.ToString(lstBidReqStatus[i].Title), Value = Convert.ToString(lstBidReqStatus[i].LookUpKey1) });
            }
            ViewBag.lstBidStatus = lstBidReqStatuslist;
            return View();
        }
        public JsonResult IndexreStaffPaging(Int64 PageSize, Int64 PageIndex, string Search, String Sort, Int32 PropertyKey,string BidRequestStatus, int Modulekey, string StartDate, string BidDueDate)
        {
            List<BidRequestModel> lstuser = null;
            DateTime FromDate = new DateTime();
            DateTime ToDate = new DateTime();
            if (StartDate != "")
            {
                FromDate = Convert.ToDateTime(StartDate);
            }
            if (BidDueDate != "")
            {
                ToDate = Convert.ToDateTime(BidDueDate);
            }
            //Int32 resourcekey = Convert.ToInt32(Session["resourceid"]);
            Int32 resourcekey = 0;
            lstuser = _bidReportservice.SearchBidRequest(PageSize, PageIndex, Search.Trim(), Sort, resourcekey, PropertyKey, BidRequestStatus, Modulekey, FromDate, ToDate);
            lstuser.ForEach(f => f.ispriorityrecord = false);

            return Json(lstuser, JsonRequestBehavior.AllowGet);

        }
        public JsonResult IndexreStaffPagingPriority(Int64 PageSize, Int64 PageIndex, string Search, String Sort, Int32 PropertyKey,string BidRequestStatus, int Modulekey, string StartDate, string BidDueDate)
        {
            List<BidRequestModel> lstuser = null;
            //Int32 resourcekey = Convert.ToInt32(Session["resourceid"]);
            Int32 resourcekey = 0;
            lstuser = _bidReportservice.SearchBidRequestPriority(PageSize, PageIndex, Search.Trim(), Sort, resourcekey, PropertyKey, BidRequestStatus, Modulekey);
            lstuser.ForEach(f => f.ispriorityrecord = true);
            return Json(lstuser, JsonRequestBehavior.AllowGet);
        }
        public JsonResult PMIndexreStaffPaging(Int64 PageSize, Int64 PageIndex, string Search, String Sort, Int32 PropertyKey,string BidRequestStatus, int Modulekey, string StartDate, string BidDueDate)
        {
            List<BidRequestModel> lstuser = null;
            DateTime FromDate = new DateTime();
            DateTime ToDate = new DateTime();
            if (StartDate != "")
            {
                FromDate = Convert.ToDateTime(StartDate);
            }
            if (BidDueDate != "")
            {
                ToDate = Convert.ToDateTime(BidDueDate);
            }
            Int32 resourcekey = Convert.ToInt32(Session["resourceid"]);
            //Int32 resourcekey = 0;
            lstuser = _bidReportservice.SearchBidRequest(PageSize, PageIndex, Search.Trim(), Sort, resourcekey, PropertyKey, BidRequestStatus, Modulekey, FromDate, ToDate);
            lstuser.ForEach(f => f.ispriorityrecord = false);

            return Json(lstuser, JsonRequestBehavior.AllowGet);

        }
        public JsonResult PMIndexreStaffPagingPriority(Int64 PageSize, Int64 PageIndex, string Search, String Sort, Int32 PropertyKey,string BidRequestStatus, int Modulekey, string StartDate, string BidDueDate)
        {
            List<BidRequestModel> lstuser = null;
            Int32 resourcekey = Convert.ToInt32(Session["resourceid"]);
            //Int32 resourcekey = 0;
            lstuser = _bidReportservice.SearchBidRequestPriority(PageSize, PageIndex, Search.Trim(), Sort, resourcekey, PropertyKey, BidRequestStatus, Modulekey);
            lstuser.ForEach(f => f.ispriorityrecord = true);
            return Json(lstuser, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AdminWorkOrderReport()
        {
            return View();
        }
        public JsonResult SearchVendorByBidRequest()
        {
            List<BidRequestModel> lstVendor = null;
            //long ResourceKey = Convert.ToInt64(Session["resourceid"]);
            int BidRequestKey = Convert.ToInt32(Request.Form["BidRequestKey"].ToString());
            int modulekey = Convert.ToInt32(Request.Form["Modulekey"].ToString());
            lstVendor = _bidReportservice.SearchVendorByBidRequest(BidRequestKey, modulekey);
            return Json(lstVendor, JsonRequestBehavior.AllowGet);
        }
        public JsonResult PMSearchVendorByBidRequest()
        {
            List<BidRequestModel> lstVendor = null;
            long ResourceKey = Convert.ToInt64(Session["resourceid"]);
            int BidRequestKey = Convert.ToInt32(Request.Form["BidRequestKey"].ToString());
            int modulekey = Convert.ToInt32(Request.Form["Modulekey"].ToString());

            lstVendor = _bidReportservice.SearchVendorByBidRequest(BidRequestKey, modulekey);
            return Json(lstVendor, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SearchVendorByWorkReport()
        {
            List<BidRequestModel> lstVendor = null;
            List<BidRequestModel> lstVendors = new List<BidRequestModel>();
            //long ResourceKey = Convert.ToInt64(Session["resourceid"]);
            //long UserKey = Convert.ToInt64(Session["UserKey"]);
            long ResourceKey = 0;
            long CompanyKey = 0;
            int BidRequestKey = Convert.ToInt32(Request.Form["BidRequestKey"].ToString());
            int modulekey = Convert.ToInt32(Request.Form["Modulekey"].ToString());

            string BidRequestStatus = Request.Form["BidRequestStatus"].ToString();
            string PropertyKey = Request.Form["PropertyKey"].ToString();
            string PropertyKeys = "0";
            if (PropertyKey == "")
            {
                PropertyKeys = "0";
            }
            string FromDates = Request.Form["FromDate"].ToString();
            string ToDates = Request.Form["ToDate"].ToString();
            DateTime FromDate = new DateTime();
            DateTime ToDate = new DateTime();
            if (FromDates != "")
            {
                FromDate = Convert.ToDateTime(FromDates);
            }
            if (ToDates != "")
            {
                ToDate = Convert.ToDateTime(ToDates);
            }
            lstVendor = _bidReportservice.SearchVendorByWorkReport(BidRequestKey, modulekey, ResourceKey, CompanyKey, BidRequestStatus, Convert.ToInt32(PropertyKeys), FromDate, ToDate);
            string BidSelectedVendor = "";
            string[] BidVendorKeys = PropertyKey.Split(',');
            if (PropertyKey != "")
            {
                for (int i = 0; i < lstVendor.Count; i++)
                {
                    BidSelectedVendor = "Cbl_" + lstVendor[i].PropertyKey;

                    for (int k = 0; k < BidVendorKeys.Length; k++)
                    {
                        if (BidVendorKeys[k] == BidSelectedVendor)
                        {
                            lstVendors.Add(lstVendor[i]);
                        }
                    }
                }
            }
            else
            {
                lstVendors = lstVendor;
            }
            return Json(lstVendors, JsonRequestBehavior.AllowGet);
        }
        public JsonResult BindProperty()
        {
            IList<BidRequestModel> lstVendor = null;
            List<BidRequestModel> lstPropertys = new List<BidRequestModel>();
            //long ResourceKey = Convert.ToInt64(Session["resourceid"]);
            //long UserKey = Convert.ToInt64(Session["UserKey"]);
            long ResourceKey = 0;
            long CompanyKey = 0;

            lstVendor = _bidRequestservice.GetAllProperty(0, 0);

            for (int i = 0; i < lstVendor.Count; i++)
            {
                BidRequestModel BidRequestModel = new BidRequestModel();
                //System.Web.Mvc.SelectListItem sli = new System.Web.Mvc.SelectListItem();
                //sli.Text = Convert.ToString(lstProperty[i].Property);
                //sli.Value = Convert.ToString(lstProperty[i].PropertyKey);
                BidRequestModel.Property = Convert.ToString(lstVendor[i].Property);
                BidRequestModel.PropertyKey = Convert.ToString(lstVendor[i].PropertyKey);
                lstPropertys.Add(BidRequestModel);
            }
            return Json(lstVendor, JsonRequestBehavior.AllowGet);
        }
        public JsonResult PMBindProperty()
        {
            IList<BidRequestModel> lstVendor = null;
            List<BidRequestModel> lstPropertys = new List<BidRequestModel>();
            int ResourceKey = Convert.ToInt32(Session["resourceid"]);
            //long UserKey = Convert.ToInt64(Session["UserKey"]);
            int CompanyKey = Convert.ToInt32(Session["CompanyKey"]);

            lstVendor = _bidRequestservice.GetAllProperty(ResourceKey, CompanyKey);

            for (int i = 0; i < lstVendor.Count; i++)
            {
                BidRequestModel BidRequestModel = new BidRequestModel();
                //System.Web.Mvc.SelectListItem sli = new System.Web.Mvc.SelectListItem();
                //sli.Text = Convert.ToString(lstProperty[i].Property);
                //sli.Value = Convert.ToString(lstProperty[i].PropertyKey);
                BidRequestModel.Property = Convert.ToString(lstVendor[i].Property);
                BidRequestModel.PropertyKey = Convert.ToString(lstVendor[i].PropertyKey);
                lstPropertys.Add(BidRequestModel);
            }
            return Json(lstVendor, JsonRequestBehavior.AllowGet);
        }
        public JsonResult PMSearchVendorByWorkReport()
        {
            List<BidRequestModel> lstVendor = null;
            List<BidRequestModel> lstVendors = new List<BidRequestModel>();

            long ResourceKey = Convert.ToInt64(Session["resourceid"]);
            //long UserKey = Convert.ToInt64(Session["userid"]);
            long CompanyKey = Convert.ToInt64(Session["CompanyKey"]);
            int BidRequestKey = Convert.ToInt32(Request.Form["BidRequestKey"].ToString());
            int modulekey = Convert.ToInt32(Request.Form["Modulekey"].ToString());

            string BidRequestStatus = Request.Form["BidRequestStatus"].ToString();
            string PropertyKey = Request.Form["PropertyKey"].ToString();
            string PropertyKeys = "0";
            if (PropertyKey == "")
            {
                PropertyKeys = "0";
            }
            string FromDates = Request.Form["FromDate"].ToString();
            string ToDates = Request.Form["ToDate"].ToString();
            DateTime FromDate = new DateTime();
            DateTime ToDate = new DateTime();
            if (FromDates != "")
            {
                FromDate = Convert.ToDateTime(FromDates);
            }

            if (ToDates != "")
            {
                ToDate = Convert.ToDateTime(ToDates);
            }
            //lstVendor = _bidReportservice.SearchVendorByPMWorkOrder(BidRequestKey, modulekey,ResourceKey, BidRequestStatus, PropertyKey, FromDate, ToDate);
            lstVendor = _bidReportservice.SearchVendorByWorkReport(BidRequestKey, modulekey, ResourceKey, CompanyKey, BidRequestStatus, Convert.ToInt32(PropertyKeys), FromDate, ToDate);

            string BidSelectedVendor = "";
            string[] BidVendorKeys = PropertyKey.Split(',');
            if (PropertyKey != "")
            {
                for (int i = 0; i < lstVendor.Count; i++)
                {
                    BidSelectedVendor = "Cbl_" + lstVendor[i].PropertyKey;

                    for (int k = 0; k < BidVendorKeys.Length; k++)
                    {
                        if (BidVendorKeys[k] == BidSelectedVendor)
                        {
                            lstVendors.Add(lstVendor[i]);
                        }
                    }
                }
            }
            else
            {
                lstVendors = lstVendor;
            }

            return Json(lstVendors, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ViewAdminGeneratedReport(string BidRequestId)
        {
            string[] bidRequestId = BidRequestId.Split(',');
            string bidval = "";
            for (int i = 0; i < bidRequestId.Length; i++)
            {
                bidval += bidRequestId[i].Split('_')[1] + ",";
            }
            //string val = (bidval.Length - 1).ToString();
            string val = bidval.Remove(bidval.Length - 1, 1);
            ViewBag.BidRequestId = val;
            return View();
        }
        private static Drawing GetImageElement(string imagePartId,string fileName,string pictureName,double width,double height)
        {
            double englishMetricUnitsPerInch = 1828800;
            double pixelsPerInch = 96;

            //calculate size in emu
            double emuWidth = width * englishMetricUnitsPerInch / pixelsPerInch;
            double emuHeight = height * englishMetricUnitsPerInch / pixelsPerInch;

            var element = new Drawing(
                new DW.Inline(
                    new DW.Extent { Cx = (Int64Value)2600325, Cy = (Int64Value)666750},
                    new DW.EffectExtent { LeftEdge = 0L, TopEdge = 0L, RightEdge = 0L, BottomEdge = 0L },
                    new DW.DocProperties { Id = (UInt32Value)1U, Name = pictureName },
                    new DW.NonVisualGraphicFrameDrawingProperties(
                    new A.GraphicFrameLocks { NoChangeAspect = true }),
                    new A.Graphic(
                        new A.GraphicData(
                            new PIC.Picture(
                                new PIC.NonVisualPictureProperties(
                                    new PIC.NonVisualDrawingProperties { Id = (UInt32Value)0U, Name = fileName },
                                    new PIC.NonVisualPictureDrawingProperties()),
                                new PIC.BlipFill(
                                    new A.Blip(
                                        new A.BlipExtensionList(
                                            new A.BlipExtension { Uri = "{28A0092B-C50C-407E-A947-70E740481C1C}" }))
                                    {
                                        Embed = imagePartId,
                                        CompressionState = A.BlipCompressionValues.Print
                                    },
                                            new A.Stretch(new A.FillRectangle())),
                                new PIC.ShapeProperties(
                                    new A.Transform2D(
                                        new A.Offset { X = 0L, Y = 0L },
                                        new A.Extents { Cx = (Int64Value)2600325, Cy = (Int64Value)666750 }),
                                    new A.PresetGeometry(
                                        new A.AdjustValueList())
                                    { Preset = A.ShapeTypeValues.Rectangle })))
                        {
                            Uri = "http://schemas.openxmlformats.org/drawingml/2006/picture"
                        }))
                {
                    DistanceFromTop = 0U,
                    DistanceFromBottom = 0U,
                    DistanceFromLeft = 0U,
                    DistanceFromRight = 0U,
                    EditId = "50D07946"
                });
            return element;
        }
        public ActionResult ViewAdminWorkOrderReport()
        {
            //var lststatus = new List<SelectListItem>()
            //{
            //  new SelectListItem{ Value="0",Text="Please Select",Selected=true},
            //  new SelectListItem{ Value="1",Text="Pending"},
            //  new SelectListItem{ Value="2",Text="Approve"},
            //  new SelectListItem{ Value="3",Text="Unapprove"},
            //};
            //ViewBag.lststatus = lststatus;
            IList<BidRequestModel> lstProperty = null;
            lstProperty = _bidRequestservice.GetAllProperty(0, 0);
            List<BidRequestModel> lstPropertys = new List<BidRequestModel>();
            List<System.Web.Mvc.SelectListItem> lstPropertylist = new List<System.Web.Mvc.SelectListItem>();
            System.Web.Mvc.SelectListItem sli2 = new System.Web.Mvc.SelectListItem();
            sli2.Text = "--Please Select--";
            sli2.Value = "0";
            lstPropertylist.Add(sli2);
            for (int i = 0; i < lstProperty.Count; i++)
            {
                BidRequestModel BidRequestModel = new BidRequestModel();
                //System.Web.Mvc.SelectListItem sli = new System.Web.Mvc.SelectListItem();
                //sli.Text = Convert.ToString(lstProperty[i].Property);
                //sli.Value = Convert.ToString(lstProperty[i].PropertyKey);
                BidRequestModel.Property = Convert.ToString(lstProperty[i].Property);
                BidRequestModel.PropertyKey = Convert.ToString(lstProperty[i].PropertyKey);
                lstPropertys.Add(BidRequestModel);
            }
            //ViewBag.lstProperty = lstPropertylist;
            ViewBag.lstProperty = lstPropertys;

            IList<LookUpModel> lstBidReqStatus = _bidRequestservice.GetBidRequetStatusFilter();
            List<System.Web.Mvc.SelectListItem> lstBidReqStatuslist = new List<System.Web.Mvc.SelectListItem>();
            lstBidReqStatuslist.Add(new SelectListItem() { Text = "Show All", Value = "0,0" });
            for (int i = 0; i < lstBidReqStatus.Count; i++)
            {
                lstBidReqStatuslist.Add(new SelectListItem() { Text = Convert.ToString(lstBidReqStatus[i].Title), Value = Convert.ToString(lstBidReqStatus[i].LookUpKey1) });
            }
            ViewBag.lstBidStatus = lstBidReqStatuslist;
            return View();
        }
        private static void AddImageToBody(WordprocessingDocument wordDoc, string relationshipId)
        {
            // Define the reference of the image.
            var element =
                 new Drawing(
                     new DW.Inline(
                         new DW.Extent() { Cx = 990000L, Cy = 792000L },
                         new DW.EffectExtent()
                         {
                             LeftEdge = 0L,
                             TopEdge = 0L,
                             RightEdge = 0L,
                             BottomEdge = 0L
                         },
                         new DW.DocProperties()
                         {
                             Id = (UInt32Value)1U,
                             Name = "Picture 1"
                         },
                         new DW.NonVisualGraphicFrameDrawingProperties(
                             new A.GraphicFrameLocks() { NoChangeAspect = true }),
                         new A.Graphic(
                             new A.GraphicData(
                                 new PIC.Picture(
                                     new PIC.NonVisualPictureProperties(
                                         new PIC.NonVisualDrawingProperties()
                                         {
                                             Id = (UInt32Value)0U,
                                             Name = "New Bitmap Image.jpg"
                                         },
                                         new PIC.NonVisualPictureDrawingProperties()),
                                     new PIC.BlipFill(
                                         new A.Blip(
                                             new A.BlipExtensionList(
                                                 new A.BlipExtension()
                                                 {
                                                     Uri =
                                                        "{28A0092B-C50C-407E-A947-70E740481C1C}"
                                                 })
                                         )
                                         {
                                             Embed = relationshipId,
                                             CompressionState =
                                             A.BlipCompressionValues.Print
                                         },
                                         new A.Stretch(
                                             new A.FillRectangle())),
                                     new PIC.ShapeProperties(
                                         new A.Transform2D(
                                             new A.Offset() { X = 0L, Y = 0L },
                                             new A.Extents() { Cx = 990000L, Cy = 792000L }),
                                         new A.PresetGeometry(
                                             new A.AdjustValueList()
                                         )
                                         { Preset = A.ShapeTypeValues.Rectangle }))
                             )
                             { Uri = "https://schemas.openxmlformats.org/drawingml/2006/picture" })
                     )
                     {
                         DistanceFromTop = (UInt32Value)0U,
                         DistanceFromBottom = (UInt32Value)0U,
                         DistanceFromLeft = (UInt32Value)0U,
                         DistanceFromRight = (UInt32Value)0U,
                         EditId = "50D07946"
                     });

            // Append the reference to body, the element should be in a Run.
            wordDoc.MainDocumentPart.Document.Body.AppendChild(new Paragraph(new Run(element)));
        }
        private static TableCell CreateCell(string text)
        {
            TableCell TableCell = new TableCell();


            if (text == "Vendor")
            {
                TableCell.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "6500" }));
            }
            else
            {
                TableCell.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "3000" }));
            }

            Paragraph Paragraph1 = new Paragraph();
            Run run1 = new Run();
            run1.Append(new Text(text));
            //create runproperties and append a "Bold" to them
            RunProperties run1Properties = new RunProperties();
            run1Properties.Append(new Bold());
            //set the first runs RunProperties to the RunProperties containing the bold
            run1.RunProperties = run1Properties;

            ParagraphProperties paraProperties = new ParagraphProperties();
            Justification justification = new Justification() { Val = JustificationValues.Center };
            paraProperties.Append(justification);

            Paragraph1.Append(paraProperties);
            Paragraph1.Append(run1);

            //if (Size == 3)
            //{
            //    TableCellProperties tableCellProperties1 = new TableCellProperties();
            //    TableCellWidth tableCellWidth1 = new TableCellWidth() { Width = "10", Type = TableWidthUnitValues.Dxa };
            //    tableCellProperties1.Append(tableCellWidth1);
            //    TableCell.Append(tableCellProperties1);
            //}
            TableCell.Append(Paragraph1);
            return TableCell;
            //return new TableCell(new Paragraph(new Run(new Text(text))));
        }
        //public JsonResult PMDownloadEmail()
        //{
        //    string ReportTypeName = Request.Form["ReportTypeName"].ToString();
        //    string datetime = DateTime.Now.ToString("yyyyMMddHHmmssfff");
        //    string ReportTypeNames = ReportTypeName + datetime;
        //    string fileName = Server.MapPath("~/Documents/Reports/") + "abdocdownload" + ".docx";
        //    using (WordprocessingDocument document = WordprocessingDocument.Open(fileName,true))
        //    {
        //        // Get the main document part
              
                
        //    }
        //    string[] file = new string[1];
        //    /// Documents / Reports
        //    file[0] = "../Documents/Reports/" + "abdocdownload" + ".docx";
        //    // DownloadFile(Server.MapPath("~/Documents/Reports/"), file.Name);
        //    //Process.Start("WINWORD.EXE", fileName);

        //    return Json(file, JsonRequestBehavior.AllowGet);

        //}
        public JsonResult PMDownloadEmail()
        {
            try
            {
                List<BidRequestModel> lstVendor = null;
                long ResourceKey = Convert.ToInt64(Session["resourceid"]);
                string BidRequestKey = Request.Form["BidRequestKey"].ToString();
                string VendorSelected = Request.Form["VendorSelected"].ToString();
                int modulekey = Convert.ToInt32(Request.Form["Modulekey"].ToString());
                string ReportTypeName = Request.Form["ReportTypeName"].ToString();
                string ReportType = Request.Form["ReportType"].ToString();
                string Include_COI = Request.Form["Include_COI"].ToString();

                string datetime = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                string ReportTypeNames = ReportTypeName + datetime;

                bool IsDetailReport = false;
                bool IncludeCOI = false;
                bool IsSent = false;
                string VendorKeyList = "";
                //string BidRequestStatus = Request.Form["BidRequestStatus"].ToString();
                //int PropertyKey = Convert.ToInt32(Request.Form["PropertyKey"].ToString());
                //string FromDates = Request.Form["FromDate"].ToString();
                //string ToDates = Request.Form["ToDate"].ToString();
                //DateTime FromDate = new DateTime();
                //DateTime ToDate = new DateTime();
                //if (FromDates != "")
                //{
                //    FromDate = Convert.ToDateTime(FromDates);
                //}

                //if (ToDates != "")
                //{
                //    ToDate = Convert.ToDateTime(ToDates);
                //}

                string[] BidRequestKeys = BidRequestKey.Split(',');
                string[] BidVendorKeys = VendorSelected.Split(',');
                //string[] SelectedVendor = VendorSelected.Split(',');

                //string fileName = @"D:\JayDodiya\AssociationBids 19-04-2021\" + ReportType +".docx";
                //string fileName = @"D:\Sundar\GitHub\AssociationBids\AssociationBids.Portal\Documents\Reports" + ReportType + ".docx";

                //var FileName = Path.GetFileName(ReportType + ".docx");

                string fileName = Server.MapPath("~/Documents/Reports/") + ReportTypeNames + ".docx";
                string fileNamen = Server.MapPath("~/Documents/Reports/") + "mydoc" + ".docx";

                int newPageCount = 0;
                using (WordprocessingDocument doc = WordprocessingDocument.Open(fileNamen, true))
                {
                    //// Defines the MainDocumentPart            
                    MainDocumentPart mainDocumentPart = doc.MainDocumentPart;

                  

                    Body body = mainDocumentPart.Document.AppendChild(new Body());
                    for (int i = 0; i < BidRequestKeys.Length; i++)
                    {

                        string BidSelectedVendor = "";
                        //lstVendor = _bidReportservice.SearchVendorByPMWorkOrder(Convert.ToInt32(BidRequestKeys[i]), modulekey, ResourceKey, BidRequestStatus, PropertyKey, FromDate, ToDate);
                        lstVendor = _bidReportservice.SearchVendorByBidRequest(Convert.ToInt32(BidRequestKeys[i]), modulekey);

                        if (ReportTypeName == "SummaryReport")
                        {

                            if (lstVendor.Count > 0)
                            {
                                IsDetailReport = false;
                                IncludeCOI = false;

                                //Paragraph p1 = header_default.InsertParagraph();


                                //p4.Color = Color.Blue;

                                //doc.InsertParagraph(Environment.NewLine);
                                //doc.InsertParagraph("Property Name : " + lstVendor[0].Property);
                                //doc.InsertParagraph("Bid Name : " + lstVendor[0].BidName);
                                //doc.InsertParagraph("Bid Due Date : " + lstVendor[0].BidDueDates);

                                Table table = new Table();
                                TableRow row = null;
                                TableProperties tableProp = new TableProperties();

                                TableWidth tableWidth = new TableWidth() { Width = "5000", Type = TableWidthUnitValues.Pct };

                                TableBorders tblBorders = new TableBorders();

                                TopBorder topBorder = new TopBorder();

                                topBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);

                                topBorder.Color = "#000000";

                                tblBorders.AppendChild(topBorder);
                                BottomBorder bottomBorder = new BottomBorder();
                                bottomBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                                bottomBorder.Color = "#000000";
                                tblBorders.AppendChild(bottomBorder);
                                RightBorder rightBorder = new RightBorder();
                                rightBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                                rightBorder.Color = "#000000";
                                tblBorders.AppendChild(rightBorder);
                                LeftBorder leftBorder = new LeftBorder();
                                leftBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                                leftBorder.Color = "#000000"; 
                                tblBorders.AppendChild(leftBorder);
                                InsideHorizontalBorder insideHBorder = new InsideHorizontalBorder();
                                insideHBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                                insideHBorder.Color = "#000000";
                                tblBorders.AppendChild(insideHBorder);
                                InsideVerticalBorder insideVBorder = new InsideVerticalBorder();
                                insideVBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);
                                insideVBorder.Color = "#000000";
                                tblBorders.AppendChild(insideVBorder);
                                //// Add the table borders to the properties
                                tableProp.AppendChild(tblBorders);
                                tableProp.Append(tableWidth);
                                table.AppendChild<TableProperties>(tableProp);
                                row = new TableRow();
                                row.Append(CreateCell("Vendor"));
                                row.Append(CreateCell("Bid Amount"));
                                row.Append(CreateCell("Insurance Amount"));
                                row.Append(CreateCell("Start Date"));
                                //t.Rows[0].Cells[0].Paragraphs.First().Append("Vendor").Bold();
                                //t.Rows[0].Cells[1].Paragraphs.First().Append("Bid Amount").Bold();
                                //t.Rows[0].Cells[2].Paragraphs.First().Append("Insurance Amount").Bold();
                                //t.Rows[0].Cells[3].Paragraphs.First().Append("Start Date").Bold();
                                //t.Alignment = Alignment.center;
                                //t.Design = TableDesign.ColorfulList;
                                //Fill cells by adding text.  
                                //t.Rows[0].Cells[3].Paragraphs.First().Append("DD").Bold();
                                string BlankRow = "";
                                int VendorBidCount = 0;
                                //ApplyFooter(doc, 1000);
                                for (int j = 0; j < lstVendor.Count; j++)
                                {
                                    var tr = new TableRow();
                                    int Count = 0;
                                    BidSelectedVendor = "Cbl_" + BidRequestKeys[i] + "_" + lstVendor[j].BidVendorKey;



                                   string  DateSS = "";
                                    
                                    DateSS = _bidReportservice.getVendorDate(Convert.ToInt32(lstVendor[j].BidVendorKey));
                                    if (DateSS == null)
                                    {
                                        DateSS = "";

                                    }
                                    

                                    for (int k = 0; k < BidVendorKeys.Length; k++)
                                    {
                                        if (BidVendorKeys[k] == BidSelectedVendor)
                                        {
                                            if (VendorBidCount == 0)
                                            {
                                                if (newPageCount != 0)
                                                {
                                                    Paragraph newPara = new Paragraph(new Run(new Break() { Type = BreakValues.Page }, new Text("")));
                                                    body.Append(newPara);
                                                    newPageCount = 0;
                                                }
                                                //Guid obj = Guid.NewGuid();
                                                ////var newHeaderPart = mainDocPart.AddNewPart<HeaderPart>();
                                                //var imagePart = mainDocumentPart.AddImagePart(ImagePartType.Jpeg);


                                                ////System.Drawing.Image image = System.Drawing.Image.FromFile("‪D:\\1.jpg");
                                                ////using (FileStream fs = new FileStream(Server.MapPath("~/Content/themes/assets/images/brand/logo.png"), FileMode.Open))
                                                ////{
                                                ////    imgPart.FeedData(fs);
                                                ////}
                                                ////AddImageToBody(doc, mainDocumentPart.GetIdOfPart(imgPart));
                                                //using (FileStream stream = new FileStream(Server.MapPath("~/Content/themes/assets/images/brand/logo.png"), FileMode.Open))
                                                //{
                                                //    imagePart.FeedData(stream);
                                                //}
                                                //var text = new Text("Hello Open XML world");
                                                //var run = new Run(text);
                                                //var paragraph = new Paragraph(run);
                                                //Drawing imageElement = GetImageElement(mainDocumentPart.GetIdOfPart(imagePart), Server.MapPath("~/Content/themes/assets/images/brand/logo.png"), "my image", 22, 22);
                                                //ParagraphProperties paragraphProperties1 = new ParagraphProperties();
                                                //Justification justification1 = new Justification() { Val = JustificationValues.Center };
                                                //paragraphProperties1.Append(justification1);
                                                //paragraph.Append(paragraphProperties1);
                                                //body.AppendChild(new Paragraph(new Run(imageElement)));




                                                Paragraph p1 = new Paragraph();
                                                Run r = new Run();
                                                RunProperties rp1 = new RunProperties();
                                                rp1.Bold = new Bold();
                                                r.Append(rp1);
                                                Text t1 = new Text("Bid Request Summary Reports");
                                                r.Append(t1);
                                                p1.Append(r);
                                                body.Append(p1);
                                                Paragraph p2 = new Paragraph();
                                                Run r2 = new Run();
                                                RunProperties rp2 = new RunProperties();
                                                rp2.Color = new Color() { Val = "#0000FF" };
                                                r2.Append(rp2);
                                                Text t2 = new Text("_____________________________________________________________________________________");
                                                r2.Append(t2);
                                                p2.Append(r2);
                                                body.Append(p2);
                                                table.Append(row);




                                                Paragraph Paragraph1789Property = new Paragraph();
                                                Run run1789Property = new Run();
                                                run1789Property.Append(new Text(lstVendor[0].Property));
                                                //RunProperties run1Properties = new RunProperties();
                                                //run1Properties.Append(new Bold());
                                                //run1.RunProperties = run1Properties;
                                                ParagraphProperties paraProperties11Property = new ParagraphProperties();
                                                Justification justification11Property = new Justification() { Val = JustificationValues.Left };
                                                paraProperties11Property.Append(justification11Property);
                                                Paragraph1789Property.Append(paraProperties11Property);
                                                Paragraph1789Property.Append(run1789Property);
                                                body.Append(Paragraph1789Property);

                                                Paragraph Paragraph1789Name = new Paragraph();
                                                Run run1789Name = new Run();
                                                run1789Name.Append(new Text(lstVendor[0].BidName));
                                                //RunProperties run1Properties = new RunProperties();
                                                //run1Properties.Append(new Bold());
                                                //run1.RunProperties = run1Properties;
                                                ParagraphProperties paraProperties11Name = new ParagraphProperties();
                                                Justification justification11Name = new Justification() { Val = JustificationValues.Left };
                                                paraProperties11Name.Append(justification11Name);
                                                Paragraph1789Name.Append(paraProperties11Name);
                                                Paragraph1789Name.Append(run1789Name);
                                                body.Append(Paragraph1789Name);

                                                Paragraph Paragraph17819Name = new Paragraph();
                                                Run run17891Name = new Run();
                                                run17891Name.Append(new Text(lstVendor[0].Description));
                                                //RunProperties run1Properties = new RunProperties();
                                                //run1Properties.Append(new Bold());
                                                //run1.RunProperties = run1Properties;
                                                ParagraphProperties paraProperties111Name = new ParagraphProperties();
                                                Justification justification111Name = new Justification() { Val = JustificationValues.Left };
                                                paraProperties111Name.Append(justification111Name);
                                                Paragraph17819Name.Append(paraProperties111Name);
                                                Paragraph17819Name.Append(run17891Name);
                                                body.Append(Paragraph17819Name);

                                                Table tbl11 = new Table();
                                              
                                                TableRow tr121 = new TableRow();
                                                TableCell tcName121 = new TableCell(new Paragraph(new Run(new Text(""))));
                                                tcName121.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2800" }));
                                                TableCell tcName1214 = new TableCell(new Paragraph(new Run(new Text(""))));
                                                tcName1214.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "1500" }));

                                                Paragraph Paragraph1789 = new Paragraph();
                                                Run run1789 = new Run();
                                                run1789.Append(new Text("Response Due:"));
                                                //RunProperties run1Properties = new RunProperties();
                                                //run1Properties.Append(new Bold());
                                                //run1.RunProperties = run1Properties;
                                                ParagraphProperties paraProperties11 = new ParagraphProperties();
                                                Justification justification11 = new Justification() { Val = JustificationValues.Right };
                                                paraProperties11.Append(justification11);
                                                Paragraph1789.Append(paraProperties11);
                                                Paragraph1789.Append(run1789);


                                                TableCell tcId121 = new TableCell();
                                                Paragraph Paragraph178 = new Paragraph();
                                                Run run178 = new Run();
                                                run178.Append(new Text(lstVendor[0].ResponseDueDates));
                                                //RunProperties run1Properties = new RunProperties();
                                                //run1Properties.Append(new Bold());
                                                //run1.RunProperties = run1Properties;
                                                ParagraphProperties paraProperties = new ParagraphProperties();
                                                Justification justification = new Justification() { Val = JustificationValues.Right };
                                                paraProperties.Append(justification);
                                                Paragraph178.Append(paraProperties);
                                                Paragraph178.Append(run178);
                                                tcId121.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2800" }));
                                                tcId121.Append(Paragraph1789);
                                                TableCell tcId1211 = new TableCell();
                                                tcId1211.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2900" }));
                                                tcId1211.Append(Paragraph178);
                                                //table.GetBorder(0);
                                                //table.SetWidthsPercentage(100%);
                                                tr121.Append(tcName121, tcName1214, tcId121, tcId1211);
                                               
                                                tbl11.AppendChild(tr121);
                                                body.AppendChild(tbl11);

                                               

                                                //for (var jj = 0; jj <= 3; jj++)
                                                //{
                                                //    var tc = new TableCell();

                                                //    if (jj == 0)
                                                //    {
                                                //        tc.Append(new Paragraph(new Run(new Text(lstVendor[j].Name))));
                                                //    }
                                                //    if (jj == 1)
                                                //    {
                                                //        tc.Append(new Paragraph(new Run(new Text(lstVendor[j].BidAmounts.Split('.')[0]))));

                                                //    }
                                                //    if (jj == 2)
                                                //    {
                                                //        tc.Append(new Paragraph(new Run(new Text(lstVendor[j].InsuranceAmounts.Split('.')[0]))));

                                                //    }
                                                //    if (jj == 3)
                                                //    {
                                                //        tc.Append(new Paragraph(new Run(new Text(lstVendor[j].VendorStartdates))));

                                                //    }
                                                //    // Assume you want columns that are automatically sized.
                                                //    tc.Append(new TableCellProperties(
                                                //        new TableCellWidth { Type = TableWidthUnitValues.Auto }));
                                                //    tr.Append(tc);
                                                //}


                                                //table.Append(tr);
                                                ////doc.InsertTable(table);
                                                ////t = (Table)doc.AddTable(lstVendor.Count + 1, 4);
                                                ////t.Rows[0].Cells[0].Width = 133;
                                                ////t.Rows[0].Cells[1].Width = 80;
                                                ////t.Rows[0].Cells[2].Width = 80;
                                                ////t.Rows[0].Cells[0].Paragraphs.First().Append("Vendor").Bold();
                                                ////t.Rows[0].Cells[1].Paragraphs.First().Append("Bid Amount").Bold();
                                                ////t.Rows[0].Cells[2].Paragraphs.First().Append("Insurance Amount").Bold();
                                                ////t.Rows[0].Cells[3].Paragraphs.First().Append("Start Date").Bold();
                                                //VendorBidCount++;
                                                ////doc.InsertParagraph(Environment.NewLine);
                                            }
                                            VendorKeyList += lstVendor[j].VendorKey + ",";
                                            var trs = new TableRow();
                                            for (var jj = 0; jj <= 3; jj++)
                                            {
                                                var tc = new TableCell();
                                                if (jj != 0)
                                                {
                                                    tc.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "3000" }));
                                                }


                                                if (jj == 0)
                                                {

                                                    tc.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "6500" }));
                                                    tc.Append(new Paragraph(new Run(new Text(lstVendor[j].Name))));
                                                }
                                                if (jj == 1)
                                                {
                                                    Paragraph Paragraph1 = new Paragraph();
                                                    Run run1 = new Run();
                                                    Text T1 = new Text();
                                                    T1.Text = lstVendor[j].BidAmountT.Split('.')[0].ToString() + "  ";
                                                    T1.Space = SpaceProcessingModeValues.Preserve;
                                                    run1.Append(T1);


                                                    //run1.Append(new Text(lstVendor[j].BidAmountT.Split('.')[0]));                                                    
                                                    //RunProperties run1Properties = new RunProperties();
                                                    //run1Properties.Append(new Bold());
                                                    //run1.RunProperties = run1Properties;
                                                    ParagraphProperties paraProperties = new ParagraphProperties();
                                                    Justification justification = new Justification() { Val = JustificationValues.Right };
                                                    paraProperties.Append(justification);
                                                    Paragraph1.Append(paraProperties);
                                                    Paragraph1.Append(run1);
                                                    //tc.Append(new Paragraph(new Run(new Text(lstVendor[j].BidAmounts.Split('.')[0]))));
                                                    //tc.Append(new Paragraph(new Run(new Text(lstVendor[j].BidAmountT.Split('.')[0]))));
                                                    tc.Append(Paragraph1);
                                                }
                                                if (jj == 2)
                                                {
                                                    Paragraph Paragraph1 = new Paragraph();
                                                    Run run1 = new Run();

                                                    Text T1 = new Text();
                                                    T1.Text = lstVendor[j].InsuranceAmountT.Split('.')[0].ToString() + "  ";
                                                    T1.Space = SpaceProcessingModeValues.Preserve;
                                                    run1.Append(T1);


                                                    //run1.Append(new Text(lstVendor[j].InsuranceAmountT.Split('.')[0]));                                                    
                                                    //RunProperties run1Properties = new RunProperties();
                                                    //run1Properties.Append(new Bold());
                                                    //run1.RunProperties = run1Properties;
                                                    ParagraphProperties paraProperties = new ParagraphProperties();
                                                    Justification justification = new Justification() { Val = JustificationValues.Right };
                                                    paraProperties.Append(justification);
                                                    Paragraph1.Append(paraProperties);
                                                    Paragraph1.Append(run1);
                                                    //tc.Append(new Paragraph(new Run(new Text(lstVendor[j].InsuranceAmounts.Split('.')[0]))));
                                                    //tc.Append(new Paragraph(new Run(new Text(lstVendor[j].InsuranceAmountT.Split('.')[0]))));
                                                   
                                                    tc.Append(Paragraph1);
                                                }
                                                if (jj == 3)
                                                {
                                                    Paragraph Paragraph1 = new Paragraph();
                                                    Run run1 = new Run();
                                                    Text T1 = new Text();
                                                    T1.Text = DateSS + "  ";
                                                    T1.Space = SpaceProcessingModeValues.Preserve;
                                                    run1.Append(T1);

                                                    //run1.Append(new Text(lstVendor[j].VendorStartdates));
                                                    //RunProperties run1Properties = new RunProperties();
                                                    //run1Properties.Append(new Bold());
                                                    //run1.RunProperties = run1Properties;
                                                    ParagraphProperties paraProperties = new ParagraphProperties();
                                                    Justification justification = new Justification() { Val = JustificationValues.Right };
                                                    paraProperties.Append(justification);
                                                    Paragraph1.Append(paraProperties);
                                                    Paragraph1.Append(run1);
                                                    tc.Append(Paragraph1);

                                                }
                                                // Assume you want columns that are automatically sized.

                                                trs.Append(tc);
                                                VendorBidCount++;
                                            }


                                            table.Append(trs);




                                            //t.Rows[j + 1].Cells[0].Paragraphs.First().Append(lstVendor[j].Name);
                                            //t.Rows[j + 1].Cells[1].Paragraphs.First().Append(lstVendor[j].BidAmounts.Split('.')[0]).Alignment = Alignment.right;
                                            //t.Rows[j + 1].Cells[2].Paragraphs.First().Append(lstVendor[j].InsuranceAmounts.Split('.')[0]).Alignment = Alignment.right;
                                            //t.Rows[j + 1].Cells[3].Paragraphs.First().Append(lstVendor[j].VendorStartdates);
                                            Count++;
                                        }
                                    }
                                    if (Count == 0)
                                    {
                                        BlankRow += j + 1 + ",";
                                    }
                                }
                                body.AppendChild(table);
                                string[] BlankRowsList = BlankRow.Split(',');
                                int count = 0;
                                for (int k = 0; k < BlankRowsList.Length - 1; k++)
                                {
                                    if (VendorBidCount != 0)
                                    {
                                        int val = Convert.ToInt32(BlankRowsList[k]);
                                        //t[val - count].Remove();
                                        count++;
                                    }
                                }
                                if (VendorBidCount != 0)
                                {
                                    //doc.InsertTable(t);
                                }
                                //doc.InsertParagraph("__________________________________________________________________________________" + Environment.NewLine);
                                if (BidRequestKeys.Length - 1 != i)
                                {
                                    if (VendorBidCount != 0)
                                    {
                                        //Paragraph p3 = doc.InsertParagraph();
                                        ////p2.AppendLine("Hello First page.");
                                        //p3.InsertPageBreakAfterSelf();
                                        newPageCount = 1;
                                    }
                                    else
                                    {
                                        newPageCount = 0;
                                    }
                                }
                                //doc.DifferentFirstPage = true;
                                //doc.DifferentOddAndEvenPages = true;

                            }
                        }

                        else if (ReportTypeName == "DetailReport")
                        {
                            if (lstVendor.Count > 0)
                            {
                                IsDetailReport = true;
                                if (Include_COI == "true")
                                {
                                    IncludeCOI = true;
                                }
                                else
                                {
                                    IncludeCOI = false;
                                }


                                //Paragraph p1 = header_default.InsertParagraph();



                                Table table = new Table();
                                TableRow row = null;
                                TableProperties tableProp = new TableProperties();

                                TableWidth tableWidth = new TableWidth() { Width = "5000", Type = TableWidthUnitValues.Pct };

                                TableBorders tblBorders = new TableBorders();

                                TopBorder topBorder = new TopBorder();

                                topBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);

                                topBorder.Color = "#000000";

                                tblBorders.AppendChild(topBorder);



                                BottomBorder bottomBorder = new BottomBorder();

                                bottomBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);

                                bottomBorder.Color = "#000000";

                                tblBorders.AppendChild(bottomBorder);



                                RightBorder rightBorder = new RightBorder();

                                rightBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);

                                rightBorder.Color = "#000000";

                                tblBorders.AppendChild(rightBorder);



                                LeftBorder leftBorder = new LeftBorder();

                                leftBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);

                                leftBorder.Color = "#000000";

                                tblBorders.AppendChild(leftBorder);



                                InsideHorizontalBorder insideHBorder = new InsideHorizontalBorder();

                                insideHBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);

                                insideHBorder.Color = "#000000";

                                tblBorders.AppendChild(insideHBorder);



                                InsideVerticalBorder insideVBorder = new InsideVerticalBorder();

                                insideVBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);

                                insideVBorder.Color = "#000000";

                                tblBorders.AppendChild(insideVBorder);



                                //// Add the table borders to the properties

                                tableProp.AppendChild(tblBorders);
                                tableProp.Append(tableWidth);

                                table.AppendChild<TableProperties>(tableProp);

                                row = new TableRow();
                                row.Append(CreateCell("Vendor"));
                                row.Append(CreateCell("Bid Amount"));
                                row.Append(CreateCell("Insurance Amount"));
                                row.Append(CreateCell("Start Date"));
                                //t.Rows[0].Cells[0].Paragraphs.First().Append("Vendor").Bold();
                                //t.Rows[0].Cells[1].Paragraphs.First().Append("Bid Amount").Bold();
                                //t.Rows[0].Cells[2].Paragraphs.First().Append("Insurance Amount").Bold();
                                //t.Rows[0].Cells[3].Paragraphs.First().Append("Start Date").Bold();



                                string BlankRow = "";
                                int RowCountWithAmount = 0;
                                int VendorCount = 0;
                                for (var j = 0; j < lstVendor.Count; j++)
                                {


                                    string DateSS = "";

                                    DateSS = _bidReportservice.getVendorDate(Convert.ToInt32(lstVendor[j].BidVendorKey));
                                    if (DateSS == null)
                                    {
                                        DateSS = "";

                                    }

                                    var tr = new TableRow();
                                    int Count = 0;
                                    BidSelectedVendor = "Cbl_" + BidRequestKeys[i] + "_" + lstVendor[j].BidVendorKey;
                                    for (int k = 0; k < BidVendorKeys.Length; k++)
                                    {
                                        if (BidVendorKeys[k] == BidSelectedVendor)
                                        {
                                            VendorKeyList += lstVendor[j].VendorKey + ",";
                                            if (lstVendor[j].BidAmounts.Split('.')[0] != "$0")
                                            {
                                                if (VendorCount == 0)
                                                {
                                                    if (newPageCount != 0)
                                                    {
                                                        Paragraph newPara = new Paragraph(new Run(new Break() { Type = BreakValues.Page }, new Text("")));
                                                        body.Append(newPara);
                                                        newPageCount = 0;

                                                    }
                                                    //Guid obj = Guid.NewGuid();
                                                    ////var newHeaderPart = mainDocPart.AddNewPart<HeaderPart>();
                                                    //var imagePart = mainDocumentPart.AddImagePart(ImagePartType.Jpeg);


                                                    ////System.Drawing.Image image = System.Drawing.Image.FromFile("‪D:\\1.jpg");
                                                    ////using (FileStream fs = new FileStream(Server.MapPath("~/Content/themes/assets/images/brand/logo.png"), FileMode.Open))
                                                    ////{
                                                    ////    imgPart.FeedData(fs);
                                                    ////}
                                                    ////AddImageToBody(doc, mainDocumentPart.GetIdOfPart(imgPart));
                                                    //using (FileStream stream = new FileStream(Server.MapPath("~/Content/themes/assets/images/brand/logo.png"), FileMode.Open))
                                                    //{
                                                    //    imagePart.FeedData(stream);
                                                    //}
                                                    //var text = new Text("Hello Open XML world");
                                                    //var run = new Run(text);
                                                    //var paragraph = new Paragraph(run);
                                                    //Drawing imageElement = GetImageElement(mainDocumentPart.GetIdOfPart(imagePart), Server.MapPath("~/Content/themes/assets/images/brand/logo.png"), "my image", 22, 22);
                                                    //ParagraphProperties paragraphProperties1 = new ParagraphProperties();
                                                    //Justification justification1 = new Justification() { Val = JustificationValues.Center };
                                                    //paragraphProperties1.Append(justification1);
                                                    //paragraph.Append(paragraphProperties1);
                                                    //body.AppendChild(new Paragraph(new Run(imageElement)));




                                                    Paragraph p1 = new Paragraph();
                                                    Run r = new Run();
                                                    RunProperties rp1 = new RunProperties();
                                                    rp1.Bold = new Bold();
                                                    r.Append(rp1);
                                                    Text t1 = new Text("Bid Request Summary Reports");
                                                    r.Append(t1);
                                                    p1.Append(r);
                                                    body.Append(p1);

                                                    Paragraph p2 = new Paragraph();
                                                    Run r2 = new Run();
                                                    RunProperties rp2 = new RunProperties();
                                                    rp2.Color = new Color() { Val = "#0000FF" };
                                                    r2.Append(rp2);
                                                    Text t2 = new Text("_____________________________________________________________________________________");
                                                    r2.Append(t2);
                                                    p2.Append(r2);
                                                    body.Append(p2);

                                                    table.Append(row);




                                                    Paragraph Paragraph1789Property = new Paragraph();
                                                    Run run1789Property = new Run();
                                                    run1789Property.Append(new Text(lstVendor[0].Property));
                                                    //RunProperties run1Properties = new RunProperties();
                                                    //run1Properties.Append(new Bold());
                                                    //run1.RunProperties = run1Properties;
                                                    ParagraphProperties paraProperties11Property = new ParagraphProperties();
                                                    Justification justification11Property = new Justification() { Val = JustificationValues.Left };
                                                    paraProperties11Property.Append(justification11Property);
                                                    Paragraph1789Property.Append(paraProperties11Property);
                                                    Paragraph1789Property.Append(run1789Property);
                                                    body.Append(Paragraph1789Property);





                                                    Paragraph Paragraph1789Name = new Paragraph();
                                                    Run run1789Name = new Run();
                                                    run1789Name.Append(new Text(lstVendor[0].BidName));
                                                    //RunProperties run1Properties = new RunProperties();
                                                    //run1Properties.Append(new Bold());
                                                    //run1.RunProperties = run1Properties;
                                                    ParagraphProperties paraProperties11Name = new ParagraphProperties();
                                                    Justification justification11Name = new Justification() { Val = JustificationValues.Left };
                                                    paraProperties11Name.Append(justification11Name);
                                                    Paragraph1789Name.Append(paraProperties11Name);
                                                    Paragraph1789Name.Append(run1789Name);
                                                    body.Append(Paragraph1789Name);

                                                    Paragraph Paragraph17891Name = new Paragraph();
                                                    Run run17891Name = new Run();
                                                    run17891Name.Append(new Text(lstVendor[0].Description));
                                                    //RunProperties run1Properties = new RunProperties();
                                                    //run1Properties.Append(new Bold());
                                                    //run1.RunProperties = run1Properties;
                                                    ParagraphProperties paraProperties111Name = new ParagraphProperties();
                                                    Justification justification111Name = new Justification() { Val = JustificationValues.Left };
                                                    paraProperties111Name.Append(justification111Name);
                                                    Paragraph17891Name.Append(paraProperties111Name);
                                                    Paragraph17891Name.Append(run17891Name);
                                                    body.Append(Paragraph17891Name);



                                                    Table tbl11 = new Table();
                                                  
                                                    TableRow tr121 = new TableRow();
                                                    TableCell tcName121 = new TableCell(new Paragraph(new Run(new Text(""))));

                                                    tcName121.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2800" }));
                                                    TableCell tcName1214 = new TableCell(new Paragraph(new Run(new Text(""))));
                                                    tcName1214.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2800" }));
                                                    Paragraph Paragraph1789 = new Paragraph();
                                                    Run run1789 = new Run();
                                                    run1789.Append(new Text("Response Due:"));
                                                    //RunProperties run1Properties = new RunProperties();
                                                    //run1Properties.Append(new Bold());
                                                    //run1.RunProperties = run1Properties;
                                                    ParagraphProperties paraProperties11 = new ParagraphProperties();
                                                    Justification justification11 = new Justification() { Val = JustificationValues.Right };
                                                    paraProperties11.Append(justification11);
                                                    Paragraph1789.Append(paraProperties11);
                                                    Paragraph1789.Append(run1789);


                                                    TableCell tcId121 = new TableCell();
                                                    Paragraph Paragraph178 = new Paragraph();
                                                    Run run178 = new Run();
                                                    run178.Append(new Text(lstVendor[0].ResponseDueDates));
                                                    //RunProperties run1Properties = new RunProperties();
                                                    //run1Properties.Append(new Bold());
                                                    //run1.RunProperties = run1Properties;
                                                    ParagraphProperties paraProperties = new ParagraphProperties();
                                                    Justification justification = new Justification() { Val = JustificationValues.Right };
                                                    paraProperties.Append(justification);
                                                    Paragraph178.Append(paraProperties);
                                                    Paragraph178.Append(run178);
                                                    tcId121.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2800" }));
                                                    tcId121.Append(Paragraph1789);
                                                    TableCell tcId1211 = new TableCell();
                                                    tcId1211.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2900" }));
                                                    tcId1211.Append(Paragraph178);
                                                    //table.GetBorder(0);
                                                    //table.SetWidthsPercentage(100%);
                                                    tr121.Append(tcName121, tcName1214, tcId121, tcId1211);
                                                   
                                                    tbl11.AppendChild(tr121);
                                                    body.AppendChild(tbl11);










                                                    //for (var jj = 0; jj <= 3; jj++)
                                                    //{
                                                    //    var tc = new TableCell();

                                                    //    if (jj == 0)
                                                    //    {
                                                    //        tc.Append(new Paragraph(new Run(new Text(lstVendor[j].Name))));

                                                    //    }
                                                    //    if (jj == 1)
                                                    //    {
                                                    //        tc.Append(new Paragraph(new Run(new Text(lstVendor[j].BidAmounts.Split('.')[0]))));

                                                    //    }
                                                    //    if (jj == 2)
                                                    //    {
                                                    //        tc.Append(new Paragraph(new Run(new Text(lstVendor[j].InsuranceAmounts.Split('.')[0]))));

                                                    //    }
                                                    //    if (jj == 3)
                                                    //    {
                                                    //        tc.Append(new Paragraph(new Run(new Text(lstVendor[j].VendorStartdates))));

                                                    //    }


                                                    //    // Assume you want columns that are automatically sized.
                                                    //    tc.Append(new TableCellProperties(
                                                    //        new TableCellWidth { Type = TableWidthUnitValues.Auto }));
                                                    //    tr.Append(tc);

                                                    //}
                                                    //table.Append(tr);

                                                    ////t = (Table)doc.AddTable(lstVendor.Count + 1, 4);
                                                    //////t.Alignment = Alignment.center;
                                                    //////t.Design = TableDesign.ColorfulList;
                                                    //////Fill cells by adding text.  

                                                    ////t.Rows[0].Cells[0].Width = 133;
                                                    ////t.Rows[0].Cells[1].Width = 80;
                                                    ////t.Rows[0].Cells[2].Width = 80;
                                                    ////t.Rows[0].Cells[0].Paragraphs.First().Append("Vendor").Bold();
                                                    ////t.Rows[0].Cells[1].Paragraphs.First().Append("Bid Amount").Bold();
                                                    ////t.Rows[0].Cells[2].Paragraphs.First().Append("Insurance Amount").Bold();
                                                    ////t.Rows[0].Cells[3].Paragraphs.First().Append("Start Date").Bold();
                                                    ////t.Rows[0].Cells[3].Paragraphs.First().Append("DD").Bold();
                                                    //VendorCount++;
                                                }
                                                var trr = new TableRow();
                                                for (var jj = 0; jj <= 3; jj++)
                                                {
                                                    var tc = new TableCell();


                                                    if (jj != 0)
                                                    {
                                                        tc.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "3000" }));
                                                    }


                                                    if (jj == 0)
                                                    {

                                                        tc.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "6500" }));
                                                        tc.Append(new Paragraph(new Run(new Text(lstVendor[j].Name))));
                                                    }
                                                    if (jj == 1)
                                                    {
                                                        //tc.Append(new Paragraph(new Run(new Text(lstVendor[j].BidAmounts.Split('.')[0]))));

                                                        Paragraph Paragraph178 = new Paragraph();
                                                        Run run178 = new Run();

                                                        Text T1 = new Text();
                                                        T1.Text = lstVendor[j].BidAmountT.Split('.')[0].ToString() + " ";
                                                        T1.Space = SpaceProcessingModeValues.Preserve;
                                                        run178.Append(T1);

                                                        //RunProperties run1Properties = new RunProperties();
                                                        //run1Properties.Append(new Bold());
                                                        //run1.RunProperties = run1Properties;
                                                        ParagraphProperties paraProperties = new ParagraphProperties();
                                                        Justification justification = new Justification() { Val = JustificationValues.Right };
                                                        paraProperties.Append(justification);
                                                        Paragraph178.Append(paraProperties);
                                                        Paragraph178.Append(run178);

                                                      

                                                        tc.Append(Paragraph178);

                                                    }
                                                    if (jj == 2)
                                                    {

                                                        Paragraph Paragraph178 = new Paragraph();
                                                        Run run178 = new Run();
                                                        Text T1 = new Text();
                                                        T1.Text = lstVendor[j].InsuranceAmountT.Split('.')[0].ToString() + " ";
                                                        T1.Space = SpaceProcessingModeValues.Preserve;
                                                        run178.Append(T1);



                                                        //run178.Append(new Text(lstVendor[j].InsuranceAmountT.Split('.')[0]));
                                                        //RunProperties run1Properties = new RunProperties();
                                                        //run1Properties.Append(new Bold());
                                                        //run1.RunProperties = run1Properties;
                                                        ParagraphProperties paraProperties = new ParagraphProperties();
                                                        Justification justification = new Justification() { Val = JustificationValues.Right };
                                                        paraProperties.Append(justification);
                                                        Paragraph178.Append(paraProperties);
                                                        Paragraph178.Append(run178);
                                                        //tc.Append(new Paragraph(new Run(new Text(lstVendor[j].InsuranceAmounts.Split('.')[0]))));
                                                        tc.Append(Paragraph178);

                                                    }
                                                    if (jj == 3)
                                                    {

                                                        Paragraph Paragraph178 = new Paragraph();
                                                        Run run178 = new Run();
                                                        Text T1 = new Text();
                                                        T1.Text = DateSS + " ";
                                                        T1.Space = SpaceProcessingModeValues.Preserve;
                                                        run178.Append(T1);

                                                        //run178.Append(new Text(lstVendor[j].VendorStartdates));
                                                        //RunProperties run1Properties = new RunProperties();
                                                        //run1Properties.Append(new Bold());
                                                        //run1.RunProperties = run1Properties;
                                                        ParagraphProperties paraProperties = new ParagraphProperties();
                                                        Justification justification = new Justification() { Val = JustificationValues.Right };
                                                        paraProperties.Append(justification);
                                                        Paragraph178.Append(paraProperties);
                                                        Paragraph178.Append(run178);
                                                        tc.Append(Paragraph178);

                                                    }
                                                    // Assume you want columns that are automatically sized.

                                                    trr.Append(tc);

                                                }
                                                VendorCount++;

                                                table.Append(trr);




                                                //t.Rows[j + 1].Cells[0].Paragraphs.First().Append(lstVendor[j].Name);
                                                //t.Rows[j + 1].Cells[1].Paragraphs.First().Append(lstVendor[j].BidAmounts.Split('.')[0]).Alignment = Alignment.right;
                                                //t.Rows[j + 1].Cells[2].Paragraphs.First().Append(lstVendor[j].InsuranceAmounts.Split('.')[0]).Alignment = Alignment.right;
                                                //t.Rows[j + 1].Cells[3].Paragraphs.First().Append(lstVendor[j].VendorStartdates);

                                                Count++;
                                                RowCountWithAmount++;
                                            }




                                        }
                                    }

                                    if (Count == 0)
                                    {
                                        BlankRow += j + 1 + ",";
                                    }
                                }


                                body.AppendChild(table);


                                string[] BlankRowsList = BlankRow.Split(',');
                                int count = 0;
                                for (int k = 0; k < BlankRowsList.Length - 1; k++)
                                {
                                    if (RowCountWithAmount != 0)
                                    {
                                        int val = Convert.ToInt32(BlankRowsList[k]);
                                        // t.Rows[val - count].Remove();
                                        count++;
                                    }
                                }
                                if (RowCountWithAmount != 0)
                                {
                                    Paragraph newPara = new Paragraph(new Run(new Break() { Type = BreakValues.Page }, new Text("")));
                                    body.Append(newPara);
                                    newPageCount = 0;
                                }


                                for (int j = 0; j < lstVendor.Count; j++)
                                {



                                    string DateSS = "";

                                    DateSS = _bidReportservice.getVendorDate(Convert.ToInt32(lstVendor[j].BidVendorKey));
                                    if (DateSS == null)
                                    {
                                        DateSS = "";

                                    }
                                    BidSelectedVendor = "Cbl_" + BidRequestKeys[i] + "_" + lstVendor[j].BidVendorKey;
                                    for (int k = 0; k < BidVendorKeys.Length; k++)
                                    {
                                        if (BidVendorKeys[k] == BidSelectedVendor)
                                        {
                                            if (lstVendor[j].BidAmounts != "$0.00")
                                            {
                                                if (newPageCount != 0)
                                                {
                                                    Paragraph newPara = new Paragraph(new Run(new Break() { Type = BreakValues.Page }, new Text("")));
                                                    body.Append(newPara);
                                                    newPageCount = 0;

                                                }
                                                //Guid obj = Guid.NewGuid();
                                                ////var newHeaderPart = mainDocPart.AddNewPart<HeaderPart>();
                                                //var imagePart = mainDocumentPart.AddImagePart(ImagePartType.Jpeg);


                                                ////System.Drawing.Image image = System.Drawing.Image.FromFile("‪D:\\1.jpg");
                                                ////using (FileStream fs = new FileStream(Server.MapPath("~/Content/themes/assets/images/brand/logo.png"), FileMode.Open))
                                                ////{
                                                ////    imgPart.FeedData(fs);
                                                ////}
                                                ////AddImageToBody(doc, mainDocumentPart.GetIdOfPart(imgPart));
                                                //using (FileStream stream = new FileStream(Server.MapPath("~/Content/themes/assets/images/brand/logo.png"), FileMode.Open))
                                                //{
                                                //    imagePart.FeedData(stream);
                                                //}
                                                //var text = new Text("Hello Open XML world");
                                                //var run = new Run(text);
                                                //var paragraph = new Paragraph(run);
                                                //Drawing imageElement = GetImageElement(mainDocumentPart.GetIdOfPart(imagePart), Server.MapPath("~/Content/themes/assets/images/brand/logo.png"), "my image", 22, 22);
                                                //ParagraphProperties paragraphProperties1 = new ParagraphProperties();
                                                //Justification justification1 = new Justification() { Val = JustificationValues.Center };
                                                //paragraphProperties1.Append(justification1);
                                                //paragraph.Append(paragraphProperties1);
                                                //body.AppendChild(new Paragraph(new Run(imageElement)));


                                                Paragraph p4 = new Paragraph();
                                                Run r4 = new Run();
                                                RunProperties rp14 = new RunProperties();
                                                rp14.Bold = new Bold();

                                                r4.Append(rp14);
                                                Text t14 = new Text("Bid Request Detail Report");
                                                r4.Append(t14);
                                                p4.Append(r4);
                                                body.Append(p4);

                                                Paragraph p2 = new Paragraph();
                                                Run r2 = new Run();
                                                RunProperties rp2 = new RunProperties();
                                                rp2.Color = new Color() { Val = "#0000FF" };
                                                r2.Append(rp2);
                                                Text t2 = new Text("_____________________________________________________________________________________");
                                                r2.Append(t2);
                                                p2.Append(r2);
                                                body.Append(p2);


                                                Paragraph Paragraph1789Property = new Paragraph();
                                                Run run1789Property = new Run();
                                                run1789Property.Append(new Text(lstVendor[0].Property));
                                                //RunProperties run1Properties = new RunProperties();
                                                //run1Properties.Append(new Bold());
                                                //run1.RunProperties = run1Properties;
                                                ParagraphProperties paraProperties11Property = new ParagraphProperties();
                                                Justification justification11Property = new Justification() { Val = JustificationValues.Left };
                                                paraProperties11Property.Append(justification11Property);
                                                Paragraph1789Property.Append(paraProperties11Property);
                                                Paragraph1789Property.Append(run1789Property);
                                                body.Append(Paragraph1789Property);





                                                


                                                Paragraph Paragraph1789BidName = new Paragraph();
                                                Run run1789BidName = new Run();
                                                run1789BidName.Append(new Text(lstVendor[j].BidName));
                                                //RunProperties run1Properties = new RunProperties();
                                                //run1Properties.Append(new Bold());
                                                //run1.RunProperties = run1Properties;
                                                ParagraphProperties paraProperties11BidName = new ParagraphProperties();
                                                Justification justification11BidName = new Justification() { Val = JustificationValues.Left };
                                                paraProperties11BidName.Append(justification11BidName);
                                                Paragraph1789BidName.Append(paraProperties11BidName);
                                                Paragraph1789BidName.Append(run1789BidName);
                                                body.Append(Paragraph1789BidName);

                                     
                                              
                                                Paragraph Paragraph1789 = new Paragraph();
                                                Run run1789 = new Run();


                                                Text T1 = new Text();
                                                T1.Text = "Start Date:    " + DateSS;
                                                T1.Space = SpaceProcessingModeValues.Preserve;
                                                run1789.Append(T1);
                                                //RunProperties run1Properties = new RunProperties();
                                                //run1Properties.Append(new Bold());
                                                //run1.RunProperties = run1Properties;
                                                ParagraphProperties paraProperties11 = new ParagraphProperties();
                                                Justification justification11 = new Justification() { Val = JustificationValues.Right };
                                                paraProperties11.Append(justification11);
                                                Paragraph1789.Append(paraProperties11);
                                                Paragraph1789.Append(run1789);
                                                body.Append(Paragraph1789);

                                                Paragraph Paragraph17894o = new Paragraph();
                                                Run run17894o = new Run();

                                                Text T2 = new Text();
                                                T2.Text = "Bid Amount:   " + lstVendor[j].BidAmounts;
                                                T2.Space = SpaceProcessingModeValues.Preserve;
                                                run17894o.Append(T2);
                                                //RunProperties run1Properties = new RunProperties();
                                                //run1Properties.Append(new Bold());
                                                //run1.RunProperties = run1Properties;
                                                ParagraphProperties paraProperties114o = new ParagraphProperties();
                                                Justification justification114o = new Justification() { Val = JustificationValues.Right };
                                                paraProperties114o.Append(justification114o);
                                                Paragraph17894o.Append(paraProperties114o);
                                                Paragraph17894o.Append(run17894o);
                                                body.Append(Paragraph17894o);


                                                //TableRow tr115 = new TableRow();
                                                //TableCell tcId1117 = new TableCell(new Paragraph(new Run(new Text("testdsfkjdshfdkjshfdskjhfdskjftestdsfkjdshfdkjshfdskjhfdskjftestdsfkjdshfdkjshfdskjhfdskjftestdsfkjdshfdkjshfdskjhfdskjf"))));
                                                //tcId1117.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "5000" }));
                                                //tr115.Append(tcId1117);
                                                //table.GetBorder(0);
                                                //table.SetWidthsPercentage(100%);

                                                Paragraph Paragraph1789Name = new Paragraph();
                                                Run run1789Name = new Run();
                                                run1789Name.Append(new Text(lstVendor[j].Name));
                                                //RunProperties run1Properties = new RunProperties();
                                                //run1Properties.Append(new Bold());
                                                //run1.RunProperties = run1Properties;
                                                ParagraphProperties paraProperties11Name = new ParagraphProperties();
                                                Justification justification11Name = new Justification() { Val = JustificationValues.Left };
                                                paraProperties11Name.Append(justification11Name);
                                                Paragraph1789Name.Append(paraProperties11Name);
                                                Paragraph1789Name.Append(run1789Name);
                                                body.Append(Paragraph1789Name);


                                                Paragraph Paragraph17894ob = new Paragraph();
                                                Run run17894ob = new Run();
                                                run17894ob.Append(new Text(lstVendor[j].Descrip));
                                                //RunProperties run1Properties = new RunProperties();
                                                //run1Properties.Append(new Bold());
                                                //run1.RunProperties = run1Properties;
                                                ParagraphProperties paraProperties114ob = new ParagraphProperties();
                                                Justification justification114ob = new Justification() { Val = JustificationValues.Left };
                                                paraProperties114ob.Append(justification114ob);
                                                Paragraph17894ob.Append(paraProperties114ob);
                                                Paragraph17894ob.Append(run17894ob);
                                                body.Append(Paragraph17894ob);

                                                List<NoteModel> NotesList = new List<NoteModel>();
                                                NotesList = _bidReportservice.NotesByWorkReport(lstVendor[j].BidRequestKey, 100);
                                                for (int l = 0; l < NotesList.Count; l++)
                                                {
                                                    Paragraph Paragraph17894ob1 = new Paragraph();
                                                    Run run17894ob1 = new Run();
                                                    run17894ob1.Append(new Text(NotesList[j].Description));
                                                    //RunProperties run1Properties = new RunProperties();
                                                    //run1Properties.Append(new Bold());p
                                                    //run1.RunProperties = run1Properties;
                                                    ParagraphProperties paraProperties114ob1 = new ParagraphProperties();
                                                    Justification justification114ob1 = new Justification() { Val = JustificationValues.Left };
                                                    paraProperties114ob1.Append(justification114ob1);
                                                    Paragraph17894ob1.Append(paraProperties114ob1);
                                                    Paragraph17894ob1.Append(run17894ob1);
                                                    body.Append(Paragraph17894ob1);

                                                }




                                                //table.Rows[0].Cells[0].Paragraphs.First().Append(lstVendor[j].Property);
                                                //table.Rows[1].Cells[0].Paragraphs.First().Append(lstVendor[j].BidName);
                                                //tables.Rows[0].Cells[0].Paragraphs.First().Append(lstVendor[j].BidName);
                                                //tables.Rows[0].Cells[1].Paragraphs.First().Append("Start Date: " + lstVendor[j].ResponseDueDates);
                                                //tables.Rows[1].Cells[1].Paragraphs.First().Append("Bid Amount: " + lstVendor[j].BidAmounts);
                                                ////table.Rows[4].Cells[0].Paragraphs.First().Append(lstVendor[j].Descrip);

                                                //doc.InsertTable(tables);
                                                //doc.InsertParagraph(lstVendor[j].Descrip + Environment.NewLine);
                                                //for (int h = 0; h < 22; h++)
                                                //{
                                                //    doc.InsertParagraph(Environment.NewLine);
                                                //}
                                                //doc.InsertParagraph("__________________________________________________________________________________" + Environment.NewLine);
                                                //doc.Sections[0].SectionBreakType = SectionBreakType.defaultNextPage;
                                                //doc.Sections[0].Paragraphs[3].AppendBreak(BreakType.PageBreak);

                                                //doc.Sections[0].GetType();
                                                //doc.Sections[0].Paragraphs[3].AppendBreak(BreakType.PageBreak);
                                                if (BidVendorKeys.Length - 1 != j)
                                                {
                                                    if (BidRequestKeys.Length - 1 == i && (lstVendor.Count - 1 == j || BidVendorKeys.Length - 2 == k))
                                                    {

                                                    }
                                                    else
                                                    {
                                                        //if (RowCountWithAmount - 1 < RowCountWithAmount && RowCountWithAmount > 1)
                                                        //{
                                                        //    Paragraph p5 = doc.InsertParagraph();
                                                        //    p5.InsertPageBreakAfterSelf();
                                                        //}
                                                        if (VendorCount != 0)
                                                        {
                                                            //Paragraph p3 = doc.InsertParagraph();
                                                            ////p2.AppendLine("Hello First page.");
                                                            //p3.InsertPageBreakAfterSelf();
                                                            newPageCount = 1;
                                                        }
                                                        else
                                                        {
                                                            newPageCount = 0;
                                                        }
                                                    }

                                                }
                                            }
                                        }
                                    }
                                }
                            }

                        }



                        //else if (ReportTypeName == "DetailReport")
                        //{
                        //    if (lstVendor.Count > 0)
                        //    {
                        //        IsDetailReport = true;
                        //        if (Include_COI == "true")
                        //        {
                        //            IncludeCOI = true;
                        //        }
                        //        else
                        //        {
                        //            IncludeCOI = false;
                        //        }
                        //        //doc.AddHeaders();
                        //        //doc.AddFooters();
                        //        // Header header_default = doc.Headers.Odd;
                        //        //Footer footer_first = doc.Footers.Odd;
                        //        //Paragraph p1 = header_default.InsertParagraph();
                        //        Paragraph p4 = footer_first.InsertParagraph();
                        //        p4.Append("__________________________________________________________________________________").Color(Color.Blue);
                        //        Table t = null;

                        //        string BlankRow = "";
                        //        int RowCountWithAmount = 0;
                        //        int VendorCount = 0;
                        //        for (int j = 0; j < lstVendor.Count; j++)
                        //        {

                        //            int Count = 0;
                        //            BidSelectedVendor = "Cbl_" + BidRequestKeys[i] + "_" + lstVendor[j].BidVendorKey;
                        //            for (int k = 0; k < BidVendorKeys.Length; k++)
                        //            {
                        //                if (BidVendorKeys[k] == BidSelectedVendor)
                        //                {
                        //                    VendorKeyList += lstVendor[j].VendorKey + ",";
                        //                    if (lstVendor[j].BidAmounts.Split('.')[0] != "$0")
                        //                    {
                        //                        if (VendorCount == 0)
                        //                        {
                        //                            if (newPageCount != 0)
                        //                            {
                        //                                Paragraph p3 = doc.InsertParagraph();
                        //                                //p2.AppendLine("Hello First page.");
                        //                                p3.InsertPageBreakAfterSelf();
                        //                                newPageCount = 0;
                        //                            }
                        //                            Paragraph p1 = doc.InsertParagraph();
                        //                            p1.Append("Bid Request Summary Report").Bold();
                        //                            p1.FontSize(13);
                        //                            p1.Append(Environment.NewLine);
                        //                            p1.Append("__________________________________________________________________________________").Color(Color.Blue);

                        //                            doc.InsertParagraph(Environment.NewLine);
                        //                            Table table = (Table)doc.AddTable(2, 2);
                        //                            //table.GetBorder(0);
                        //                            //table.SetWidthsPercentage(100%);
                        //                            table.Rows[0].Height = 20;
                        //                            table.Rows[1].Height = 25;
                        //                            table.Rows[0].Cells[0].Width = 250;
                        //                            table.Rows[0].Cells[1].Width = 250;

                        //                            //cell.CellFormat.Borders.Top.LineStyle = LineStyle.None;
                        //                            table.Design = TableDesign.None;
                        //                            table.Rows[0].Cells[0].Paragraphs.First().Append(lstVendor[0].Property);
                        //                            table.Rows[1].Cells[0].Paragraphs.First().Append(lstVendor[0].BidName);
                        //                            table.Rows[1].Cells[1].Paragraphs.First().Append("Response Due: " + lstVendor[0].ResponseDueDates);

                        //                            doc.InsertTable(table);

                        //                            t = (Table)doc.AddTable(lstVendor.Count + 1, 4);
                        //                            //t.Alignment = Alignment.center;
                        //                            //t.Design = TableDesign.ColorfulList;
                        //                            //Fill cells by adding text.  

                        //                            t.Rows[0].Cells[0].Width = 133;
                        //                            t.Rows[0].Cells[1].Width = 80;
                        //                            t.Rows[0].Cells[2].Width = 80;
                        //                            t.Rows[0].Cells[0].Paragraphs.First().Append("Vendor").Bold();
                        //                            t.Rows[0].Cells[1].Paragraphs.First().Append("Bid Amount").Bold();
                        //                            t.Rows[0].Cells[2].Paragraphs.First().Append("Insurance Amount").Bold();
                        //                            t.Rows[0].Cells[3].Paragraphs.First().Append("Start Date").Bold();
                        //                            //t.Rows[0].Cells[3].Paragraphs.First().Append("DD").Bold();
                        //                            VendorCount++;
                        //                        }

                        //                        t.Rows[j + 1].Cells[0].Paragraphs.First().Append(lstVendor[j].Name);
                        //                        t.Rows[j + 1].Cells[1].Paragraphs.First().Append(lstVendor[j].BidAmounts.Split('.')[0]).Alignment = Alignment.right;
                        //                        t.Rows[j + 1].Cells[2].Paragraphs.First().Append(lstVendor[j].InsuranceAmounts.Split('.')[0]).Alignment = Alignment.right;
                        //                        t.Rows[j + 1].Cells[3].Paragraphs.First().Append(lstVendor[j].VendorStartdates);

                        //                        Count++;
                        //                        RowCountWithAmount++;
                        //                    }
                        //                }
                        //            }

                        //            if (Count == 0)
                        //            {
                        //                BlankRow += j + 1 + ",";
                        //            }
                        //        }
                        //        string[] BlankRowsList = BlankRow.Split(',');
                        //        int count = 0;
                        //        for (int k = 0; k < BlankRowsList.Length - 1; k++)
                        //        {
                        //            if (RowCountWithAmount != 0)
                        //            {
                        //                int val = Convert.ToInt32(BlankRowsList[k]);
                        //                t.Rows[val - count].Remove();
                        //                count++;
                        //            }
                        //        }
                        //        if (RowCountWithAmount != 0)
                        //        {
                        //            doc.InsertTable(t);
                        //            Paragraph p2 = doc.InsertParagraph();
                        //            p2.InsertPageBreakAfterSelf();
                        //        }


                        //        for (int j = 0; j < lstVendor.Count; j++)
                        //        {

                        //            BidSelectedVendor = "Cbl_" + BidRequestKeys[i] + "_" + lstVendor[j].BidVendorKey;
                        //            for (int k = 0; k < BidVendorKeys.Length; k++)
                        //            {
                        //                if (BidVendorKeys[k] == BidSelectedVendor)
                        //                {
                        //                    if (lstVendor[j].BidAmounts != "$0.00")
                        //                    {
                        //                        if (newPageCount != 0)
                        //                        {
                        //                            Paragraph p5 = doc.InsertParagraph();
                        //                            //p2.AppendLine("Hello First page.");
                        //                            p5.InsertPageBreakAfterSelf();
                        //                            newPageCount = 0;
                        //                        }
                        //                        Paragraph p3 = doc.InsertParagraph();
                        //                        p3.Append("Bid Request Detail Report").Bold();
                        //                        p3.FontSize(13);
                        //                        p3.Append(Environment.NewLine);
                        //                        p3.Append("__________________________________________________________________________________").Color(Color.Blue);

                        //                        doc.InsertParagraph(Environment.NewLine);

                        //                        Table tables = (Table)doc.AddTable(2, 2);
                        //                        tables.Design = TableDesign.None;
                        //                        tables.Rows[0].Height = 20;
                        //                        tables.Rows[1].Height = 20;
                        //                        //table.Rows[2].Height = 20;
                        //                        //table.Rows[3].Height = 20;
                        //                        //table.Rows[4].Height = 20;

                        //                        tables.Rows[0].Cells[0].Width = 250;
                        //                        tables.Rows[0].Cells[1].Width = 250;

                        //                        doc.InsertParagraph(lstVendor[j].Property + Environment.NewLine);
                        //                        doc.InsertParagraph(lstVendor[j].Name + Environment.NewLine);
                        //                        //table.Rows[0].Cells[0].Paragraphs.First().Append(lstVendor[j].Property);
                        //                        //table.Rows[1].Cells[0].Paragraphs.First().Append(lstVendor[j].BidName);
                        //                        tables.Rows[0].Cells[0].Paragraphs.First().Append(lstVendor[j].BidName);
                        //                        tables.Rows[0].Cells[1].Paragraphs.First().Append("Start Date: " + lstVendor[j].ResponseDueDates);
                        //                        tables.Rows[1].Cells[1].Paragraphs.First().Append("Bid Amount: " + lstVendor[j].BidAmounts);
                        //                        //table.Rows[4].Cells[0].Paragraphs.First().Append(lstVendor[j].Descrip);

                        //                        doc.InsertTable(tables);
                        //                        doc.InsertParagraph(lstVendor[j].Descrip + Environment.NewLine);
                        //                        //for (int h = 0; h < 22; h++)
                        //                        //{
                        //                        //    doc.InsertParagraph(Environment.NewLine);
                        //                        //}
                        //                        //doc.InsertParagraph("__________________________________________________________________________________" + Environment.NewLine);
                        //                        //doc.Sections[0].SectionBreakType = SectionBreakType.defaultNextPage;
                        //                        //doc.Sections[0].Paragraphs[3].AppendBreak(BreakType.PageBreak);

                        //                        //doc.Sections[0].GetType();
                        //                        //doc.Sections[0].Paragraphs[3].AppendBreak(BreakType.PageBreak);
                        //                        if (BidVendorKeys.Length - 1 != j)
                        //                        {
                        //                            if (BidRequestKeys.Length - 1 == i && lstVendor.Count - 1 == j || BidVendorKeys.Length - 2 == k)
                        //                            {

                        //                            }
                        //                            else
                        //                            {
                        //                                //if (RowCountWithAmount - 1 < RowCountWithAmount && RowCountWithAmount > 1)
                        //                                //{
                        //                                //    Paragraph p5 = doc.InsertParagraph();
                        //                                //    p5.InsertPageBreakAfterSelf();
                        //                                //}
                        //                                if (VendorCount != 0)
                        //                                {
                        //                                    //Paragraph p3 = doc.InsertParagraph();
                        //                                    ////p2.AppendLine("Hello First page.");
                        //                                    //p3.InsertPageBreakAfterSelf();
                        //                                    newPageCount = 1;
                        //                                }
                        //                                else
                        //                                {
                        //                                    newPageCount = 0;
                        //                                }
                        //                            }

                        //                        }
                        //                    }
                        //                }
                        //            }
                        //        }
                        //    }
                        //}
                    }





                    SectionProperties objSectionProperties =
                         new SectionProperties();
                    FooterPart objFootPart = mainDocumentPart.AddNewPart<FooterPart>();
                    Footer objFooter = new Footer();
                    objFootPart.Footer = objFooter;
                    SdtBlock objSdtBlock_1 = new SdtBlock();
                    SdtContentBlock objSdtContentBlock_1 =
                        new SdtContentBlock();
                    SdtBlock objSdtBlock_2 = new SdtBlock();
                    SdtContentBlock objSdtContentBlock_2 =
                        new SdtContentBlock();
                    Paragraph objParagraph_1 = new Paragraph();
                    ParagraphProperties objParagraphProperties =
                        new ParagraphProperties();
                    ParagraphStyleId objParagraphStyleId =
                        new ParagraphStyleId() { Val = "Footer" };
                    objParagraphProperties.Append(objParagraphStyleId);
                    Justification objJustification =
                        new Justification() { Val = JustificationValues.Right };
                    objParagraphProperties.Append(objJustification);
                    objParagraph_1.Append(objParagraphProperties);
                    Run objRun_1 = new Run();
                    Text objText_1 = new Text();
                    objText_1.Text = " Page ";
                    objText_1.Space = SpaceProcessingModeValues.Preserve;
                    objRun_1.Append(objText_1);
                    objParagraph_1.Append(objRun_1);
                    Run objRun_2 = new Run();
                    FieldChar objFieldChar_1 =
                        new FieldChar()
                        { FieldCharType = FieldCharValues.Begin };
                    objRun_2.Append(objFieldChar_1);
                    objParagraph_1.Append(objRun_2);
                    Run objRun_3 = new Run();
                    FieldCode objFieldCode_1 =
                        new FieldCode()
                        { Space = SpaceProcessingModeValues.Preserve };
                    objFieldCode_1.Text = "PAGE ";
                    objFieldCode_1.Space = SpaceProcessingModeValues.Preserve;
                    objRun_3.Append(objFieldCode_1);
                    objParagraph_1.Append(objRun_3);
                    Run objRun_4 = new Run();
                    FieldChar objFieldChar_2 =
                        new FieldChar()
                        { FieldCharType = FieldCharValues.Separate };
                    objRun_4.Append(objFieldChar_2);
                    objParagraph_1.Append(objRun_4);
                    Run objRun_5 = new Run();
                    Text objText_2 = new Text();
                    objText_2.Text = "2 ";

                    objText_2.Space = SpaceProcessingModeValues.Preserve;
                    objRun_5.Append(objText_2);
                    objParagraph_1.Append(objRun_5);
                    Run objRun_6 = new Run();
                    FieldChar objFieldChar_3 =
                        new FieldChar() { FieldCharType = FieldCharValues.End };
                    objRun_6.Append(objFieldChar_3);
                    objParagraph_1.Append(objRun_6);
                    Run objRun_7 = new Run();
                    Text objText_3 = new Text();
                    objText_3.Text = " of ";
                    objText_3.Space = SpaceProcessingModeValues.Preserve;
                    objRun_7.Append(objText_3);
                    objParagraph_1.Append(objRun_7);
                    Run objRun_8 = new Run();
                    FieldChar objFieldChar_4 =
                        new FieldChar()
                        { FieldCharType = FieldCharValues.Begin };
                    objRun_8.Append(objFieldChar_4);
                    objParagraph_1.Append(objRun_8);
                    Run objRun_9 = new Run();
                    FieldCode objFieldCode_2 =
                        new FieldCode()
                        { Space = SpaceProcessingModeValues.Preserve };
                    objFieldCode_2.Text = "NUMPAGES  ";
                    objFieldCode_2.Space = SpaceProcessingModeValues.Preserve;
                    objRun_9.Append(objFieldCode_2);
                    objParagraph_1.Append(objRun_9);
                    Run objRun_10 = new Run();
                    FieldChar objFieldChar_5 =
                        new FieldChar()
                        { FieldCharType = FieldCharValues.Separate };
                    objRun_10.Append(objFieldChar_5);
                    objParagraph_1.Append(objRun_10);
                    Run objRun_11 = new Run();
                    Text objText_4 = new Text();
                    objText_4.Text = "2";
                    objText_4.Space = SpaceProcessingModeValues.Preserve;
                    objRun_11.Append(objText_4);
                    objParagraph_1.Append(objRun_11);
                    Run objRun_12 = new Run();
                    FieldChar objFieldChar_6 =
                        new FieldChar() { FieldCharType = FieldCharValues.End };
                    objRun_12.Append(objFieldChar_6);
                    objParagraph_1.Append(objRun_12);
                    objSdtContentBlock_2.Append(objParagraph_1);
                    objSdtBlock_2.Append(objSdtContentBlock_2);
                    objSdtContentBlock_1.Append(objSdtBlock_2);
                    objSdtBlock_1.Append(objSdtContentBlock_1);
                    objFooter.Append(objSdtBlock_1);
                    string strFootrID =
                        mainDocumentPart.GetIdOfPart(objFootPart);
                    FooterReference objFooterReference = new FooterReference()
                    {
                        Type = HeaderFooterValues.Default,
                        Id = strFootrID
                    };
                    objSectionProperties.Append(objFooterReference);
                    body.AppendChild(objSectionProperties);
                    DocumentSettingsPart objDocumentSettingPart = mainDocumentPart.DocumentSettingsPart;
                    objDocumentSettingPart.Settings = new Settings();
                    Compatibility objCompatibility = new Compatibility();
                    CompatibilitySetting objCompatibilitySetting =
                        new CompatibilitySetting()
                        {
                            Name = CompatSettingNameValues.CompatibilityMode,
                            Uri = "http://schemas.microsoft.com/office/word",
                            Val = "14"
                        };
                    objCompatibility.Append(objCompatibilitySetting);
                    objDocumentSettingPart.Settings.Append(objCompatibility);
                    mainDocumentPart.Document.Save();
                }
                //doc.Save();

                //string fileName = Server.MapPath("~/Documents/Reports/") + ReportType + ".docx";
                string filePath = fileName;

                if (ReportType == "EmailReport")
                {
                    //_bidReportservice.EmailReport(fileName, ResourceKey, ReportTypeName);


                    using (var mainDoc = WordprocessingDocument.Open(fileNamen, false))
                    using (var resultDoc = WordprocessingDocument.Create(fileName, WordprocessingDocumentType.Document))
                    {
                        // copy parts from source document to new document
                        foreach (var part in mainDoc.Parts)
                            resultDoc.AddPart(part.OpenXmlPart, part.RelationshipId);
                        // perform replacements in resultDoc.MainDocumentPart
                        // ...
                    }
                    string[] file = new string[1];
                    file[0] = "../Documents/Reports/" + ReportTypeNames + ".docx";
                    SaveReport(Convert.ToInt32(ResourceKey), IsDetailReport, ReportTypeNames, IncludeCOI, IsSent, VendorKeyList);
                    updatefile();
                    ReportEmailModel lstuser = new ReportEmailModel();
                    IList<VendorModel> Documentlist = null;
                    string lookUpTitle = "BidEmailSent";
                    //lstuser = __IAPIservice.GetReportList();
                    bool status = false;
                    //for (int i = 0; i < lstuser.Count; i++)
                    //{
                        //if (ReportTypeNames == ReportTypeNames)
                        //{
                            lstuser.ReportDocumentFilePath = Server.MapPath("~/Documents/Reports/") + ReportTypeNames + ".docx";
                    //lstuser[i].ReportDocumentFilePath = "../Documents/Reports/" + lstuser[i].DocumentName + ".docx";
                    lstuser.DocumentName = ReportTypeNames;
                    lstuser.IsDetailedReport = IsDetailReport;
                    lstuser.IsSent = IsSent;
                    lstuser.IncludeCOI = IncludeCOI;
                    lstuser.ResourceKey = Convert.ToInt32(ResourceKey);
                    lstuser.VendorList = VendorKeyList;
                    if (lstuser.IncludeCOI == true)
                            {
                                string[] VendorList = VendorKeyList.Split(',');
                                for (int j = 0; j < VendorList.Length - 1; j++)
                                {


                                    if (VendorList[j].Contains("Cbl_"))
                                    {
                                        VendorList[j] = VendorList[j].Replace("Cbl_", "");
                                    }

                                    Documentlist = bindDocument12(Convert.ToInt32(VendorList[j]));
                                    int count = 0;
                                    for (int k = 0; k < Documentlist.Count; k++)
                                    {
                                        string pathd = Server.MapPath("~/Document/Insurance/" + Convert.ToInt32(VendorList[j]) + "/" + Documentlist[k].Insurance.InsuranceKey + "/" + Documentlist[k].Document.FileName);
                                        string fileNamenew = pathd;
                                        string path = fileNamenew;
                                        FileInfo filem = new FileInfo(path);
                                        if (filem.Exists)//check file exsit or not  
                                        {
                                            lstuser.InsuranceDocumentFilePath += path + ",";
                                        }
                                        else 
                                        {
                                            string pathd1 = Server.MapPath("~/Document/Insurance/"  + Documentlist[k].Insurance.InsuranceKey  + Documentlist[k].Document.FileName);

                                            FileInfo filem1 = new FileInfo(pathd1);
                                            if (filem1.Exists)//check file exsit or not  
                                            {
                                                lstuser.InsuranceDocumentFilePath += pathd1 + ",";
                                            }
                                        }
                                

                                    }
                                }
                                if (lstuser.InsuranceDocumentFilePath != "" && lstuser.InsuranceDocumentFilePath != null)
                                {
                                    lstuser.InsuranceDocumentFilePath = lstuser.InsuranceDocumentFilePath.Remove(lstuser.InsuranceDocumentFilePath.Length - 1, 1);
                                }
                            }

                            status = __IAPIservice.BidAbdWorkOrderEmailSent(lstuser);
                        //}
                    //}



                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {


                    //string BidRequestStatus = Request.Form["BidRequestStatus"].ToString();
                    //int PropertyKey = Convert.ToInt32(Request.Form["PropertyKey"].ToString());
                    //string FromDates = Request.Form["FromDate"].ToString();
                    //string ToDates = Request.Form["ToDate"].ToString();
                    //DateTime FromDate = new DateTime();
                    //DateTime ToDate = new DateTime();
                    //if (FromDates != "")
                    //{
                    //    FromDate = Convert.ToDateTime(FromDates);
                    //}

                    //if (ToDates != "")
                    //{
                    //    ToDate = Convert.ToDateTime(ToDates);
                    //}


                    //string fileName = @"D:\JayDodiya\AssociationBids 19-04-2021\" + ReportType +".docx";
                    //string fileName = @"D:\Sundar\GitHub\AssociationBids\AssociationBids.Portal\Documents\Reports" + ReportType + ".docx";

                    //var FileName = Path.GetFileName(ReportType + ".docx");




                    using (var mainDoc = WordprocessingDocument.Open(fileNamen, false))
                    using (var resultDoc = WordprocessingDocument.Create(fileName,
                      WordprocessingDocumentType.Document))
                    {
                        // copy parts from source document to new document
                        foreach (var part in mainDoc.Parts)
                            resultDoc.AddPart(part.OpenXmlPart, part.RelationshipId);
                        // perform replacements in resultDoc.MainDocumentPart
                        // ...
                    }

                    string[] file = new string[1];
                    /// Documents / Reports
                    //
                    file[0] = "../Documents/Reports/" + ReportTypeNames + ".docx";
                    // DownloadFile(Server.MapPath("~/Documents/Reports/"), file.Name);
                    //Process.Start("WINWORD.EXE", fileName);
                    updatefile();
                    return Json(file, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(ex, JsonRequestBehavior.AllowGet);
            }
        }
        public void updatefile()
        {

            
            string fileNamen = Server.MapPath("~/Documents/Reports/") + "mydoc" + ".docx";
            string path = fileNamen;
            FileInfo file = new FileInfo(path);
            if (file.Exists)//check file exsit or not  
            {
                file.Delete();
                
            }

            string fileName = Server.MapPath("~/Documents/Reports/") + "Universal" + ".docx";
              fileNamen = Server.MapPath("~/Documents/Reports/") + "mydoc" + ".docx";

            using (var mainDoc = WordprocessingDocument.Open(fileName, false))
            using (var resultDoc = WordprocessingDocument.Create(fileNamen,
              WordprocessingDocumentType.Document))
            {
                // copy parts from source document to new document
                foreach (var part in mainDoc.Parts)
                    resultDoc.AddPart(part.OpenXmlPart, part.RelationshipId);
                // perform replacements in resultDoc.MainDocumentPart
                // ...
            }

           
        }
        public JsonResult PMDownloadEmailWorkReport()
        {
            try
            {
                List<BidRequestModel> lstVendor = new List<BidRequestModel>(); ;
                List<BidRequestModel> lstVendors = null;
                long ResourceKey = Convert.ToInt64(Session["resourceid"]);
                //long UserKey = Convert.ToInt64(Session["userid"]);
                long CompanyKey = Convert.ToInt64(Session["CompanyKey"]);
                string BidRequestKey = Request.Form["BidRequestKey"].ToString();
                string VendorSelected = Request.Form["VendorSelected"].ToString();
                int modulekey = Convert.ToInt32(Request.Form["Modulekey"].ToString());
                string ReportTypeName = Request.Form["ReportTypeName"].ToString();
                string datetime = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                string ReportTypeNames = ReportTypeName + datetime;
                string ReportType = Request.Form["ReportType"].ToString();

                string BidRequestStatus = Request.Form["BidRequestStatus"].ToString();
                string PropertyKey = Request.Form["PropertyKey"].ToString();
                string PropertyKeys = "0";
                if (PropertyKey == "")
                {
                    PropertyKey = "0";
                }

                string FromDates = Request.Form["FromDate"].ToString();
                string ToDates = Request.Form["ToDate"].ToString();
                string Include_COI = Request.Form["Include_COI"].ToString();
                string VendorKeyList = "";

                DateTime FromDate = new DateTime();
                DateTime ToDate = new DateTime();
                if (FromDates != "")
                {
                    FromDate = Convert.ToDateTime(FromDates);
                }

                if (ToDates != "")
                {
                    ToDate = Convert.ToDateTime(ToDates);
                }

                string[] BidRequestKeys = BidRequestKey.Split(',');
                string[] BidVendorKeys = VendorSelected.Split(',');
                string[] PropertyKeySelected = PropertyKey.Split(',');
                //string[] SelectedVendor = VendorSelected.Split(',');

                //string fileName = @"D:\JayDodiya\AssociationBids 19-04-2021\" + ReportType +".docx";
                //string fileName = @"D:\Sundar\GitHub\AssociationBids\AssociationBids.Portal\Documents\Reports" + ReportType + ".docx";

                //var FileName = Path.GetFileName(ReportType + ".docx");
                //string datetime = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                string fileName = Server.MapPath("~/Documents/Reports/") + ReportTypeName + datetime + ".docx";
                
                string fileNamen = Server.MapPath("~/Documents/Reports/") + "mydoc" + ".docx";

                bool IsDetailReport = false;
                bool IncludeCOI = false;
                bool IsSent = false;
                int newPageCount = 0;
                using (WordprocessingDocument doc = WordprocessingDocument.Open(fileNamen, true))
                {
                    //// Defines the MainDocumentPart            
                    MainDocumentPart mainDocumentPart = doc.MainDocumentPart;
                
                    Body body = mainDocumentPart.Document.AppendChild(new Body());

                    for (int i = 0; i < BidRequestKeys.Length; i++)
                    {
                        string BidSelectedVendor = "";
                        string PropertySelected = "";
                        //lstVendor = _bidReportservice.SearchVendorByWorkReport(Convert.ToInt32(BidRequestKeys[i]), modulekey,ResourceKey,CompanyKey, BidRequestStatus, PropertyKey, FromDate, ToDate);

                        lstVendors = _bidReportservice.SearchVendorByWorkReport(Convert.ToInt32(BidRequestKeys[i]), modulekey, ResourceKey, CompanyKey, BidRequestStatus, Convert.ToInt32(PropertyKeys), FromDate, ToDate);
                        for (int j = 0; j < lstVendors.Count; j++)
                        {




                            if (PropertyKey != "" && PropertyKey != "0")
                            {
                                int Count = 0;
                                PropertySelected = "Cbl_" + lstVendors[j].PropertyKey;

                                for (int k = 0; k < PropertyKeySelected.Length; k++)
                                {
                                    if (PropertyKeySelected[k] == PropertySelected)
                                    {
                                        //if ()
                                        //{
                                        lstVendor.Add(lstVendors[j]);
                                        //} 
                                    }
                                }
                            }
                            else
                            {
                                lstVendor.Add(lstVendors[j]);
                            }
                        }
                        //lstVendor = _bidReportservice.SearchVendorByPMWorkOrder(Convert.ToInt32(BidRequestKeys[i]), modulekey, ResourceKey, BidRequestStatus, PropertyKey, FromDate, ToDate);
                        //lstVendor = _bidReportservice.SearchVendorByWorkReport(Convert.ToInt32(BidRequestKeys[i]), modulekey, BidRequestStatus, PropertyKey, FromDate, ToDate);
                        if (ReportTypeName == "SummaryReport")
                        {
                            if (lstVendor.Count > 0)
                            {
                                IsDetailReport = false;
                                IncludeCOI = false;

                                Table table = new Table();
                                TableRow row = null;
                                TableProperties tableProp = new TableProperties();

                                TableWidth tableWidth = new TableWidth() { Width = "5000", Type = TableWidthUnitValues.Pct };

                                TableBorders tblBorders = new TableBorders();

                                TopBorder topBorder = new TopBorder();

                                topBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);

                                topBorder.Color = "#000000";

                                tblBorders.AppendChild(topBorder);



                                BottomBorder bottomBorder = new BottomBorder();

                                bottomBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);

                                bottomBorder.Color = "#000000";

                                tblBorders.AppendChild(bottomBorder);



                                RightBorder rightBorder = new RightBorder();

                                rightBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);

                                rightBorder.Color = "#000000";

                                tblBorders.AppendChild(rightBorder);



                                LeftBorder leftBorder = new LeftBorder();

                                leftBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);

                                leftBorder.Color = "#000000";

                                tblBorders.AppendChild(leftBorder);



                                InsideHorizontalBorder insideHBorder = new InsideHorizontalBorder();

                                insideHBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);

                                insideHBorder.Color = "#000000";

                                tblBorders.AppendChild(insideHBorder);



                                InsideVerticalBorder insideVBorder = new InsideVerticalBorder();

                                insideVBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);

                                insideVBorder.Color = "#000000";

                                tblBorders.AppendChild(insideVBorder);



                                //// Add the table borders to the properties

                                tableProp.AppendChild(tblBorders);
                                tableProp.Append(tableWidth);

                                table.AppendChild<TableProperties>(tableProp);


                                row = new TableRow();




                                row.Append(CreateCell("Property"));
                                row.Append(CreateCell("Title"));
                                row.Append(CreateCell("Selected Vendor"));
                                row.Append(CreateCell("Amount"));
                                row.Append(CreateCell("Insurance Amount"));
                                row.Append(CreateCell("Start Date"));
                                //t.Rows[0].Cells[0].Paragraphs.First().Append("Vendor").Bold();
                                //t.Rows[0].Cells[1].Paragraphs.First().Append("Bid Amount").Bold();
                                //t.Rows[0].Cells[2].Paragraphs.First().Append("Insurance Amount").Bold();
                                //t.Rows[0].Cells[3].Paragraphs.First().Append("Start Date").Bold();
                                table.Append(row);

                                //Guid obj = Guid.NewGuid();
                                ////var newHeaderPart = mainDocPart.AddNewPart<HeaderPart>();
                                //var imagePart = mainDocumentPart.AddImagePart(ImagePartType.Jpeg);


                                ////System.Drawing.Image image = System.Drawing.Image.FromFile("‪D:\\1.jpg");
                                ////using (FileStream fs = new FileStream(Server.MapPath("~/Content/themes/assets/images/brand/logo.png"), FileMode.Open))
                                ////{
                                ////    imgPart.FeedData(fs);
                                ////}
                                ////AddImageToBody(doc, mainDocumentPart.GetIdOfPart(imgPart));
                                //using (FileStream stream = new FileStream(Server.MapPath("~/Content/themes/assets/images/brand/logo.png"), FileMode.Open))
                                //{
                                //    imagePart.FeedData(stream);
                                //}
                                //var text = new Text("Hello Open XML world");
                                //var run = new Run(text);
                                //var paragraph = new Paragraph(run);
                                //Drawing imageElement = GetImageElement(mainDocumentPart.GetIdOfPart(imagePart), Server.MapPath("~/Content/themes/assets/images/brand/logo.png"), "my image", 22, 22);
                                //ParagraphProperties paragraphProperties1 = new ParagraphProperties();
                                //Justification justification1 = new Justification() { Val = JustificationValues.Center };
                                //paragraphProperties1.Append(justification1);
                                //paragraph.Append(paragraphProperties1);
                                //body.AppendChild(new Paragraph(new Run(imageElement)));




                                //Paragraph p1 = header_default.InsertParagraph();
                                Paragraph p1 = new Paragraph();
                                Run r = new Run();
                                RunProperties rp1 = new RunProperties();
                                rp1.Bold = new Bold();
                                r.Append(rp1);
                                Text t1 = new Text("Work Order Summary Report");
                                r.Append(t1);
                                p1.Append(r);
                                body.Append(p1);
                                Paragraph p2 = new Paragraph();
                                Run r2 = new Run();
                                RunProperties rp2 = new RunProperties();
                                rp2.Color = new Color() { Val = "#0000FF" };
                                r2.Append(rp2);
                                Text t2 = new Text("_____________________________________________________________________________________");
                                r2.Append(t2);
                                p2.Append(r2);
                                body.Append(p2);



                                //t.Alignment = Alignment.center;
                                //t.Design = TableDesign.ColorfulList;
                                //Fill cells by adding text.  


                                string BlankRow = "";
                                string PropertyName = "";
                                int count = 0;
                                for (int j = 0; j < lstVendor.Count; j++)
                                {

                                    string DateSS = "";

                                    DateSS = _bidReportservice.getVendorDate(Convert.ToInt32(lstVendor[j].BidVendorKey));
                                    if (DateSS == null)
                                    {
                                        DateSS = "";

                                    }


                                    int Count = 0;
                                    BidSelectedVendor = "Cbl_" + lstVendor[j].BidVendorKey;

                                    for (int k = 0; k < BidVendorKeys.Length; k++)
                                    {
                                        if (BidVendorKeys[k] == BidSelectedVendor)
                                        {
                                            VendorKeyList += lstVendor[j].VendorKey + ",";
                                            if (PropertyName == lstVendor[j].Property)
                                            {
                                                count++;

                                            }
                                            else
                                            {
                                                PropertyName = lstVendor[j].Property;
                                                count = 0;
                                            }

                                            //t.MergeCellsInColumn(0, 1, 3);
                                            var tr = new TableRow();
                                            for (var jj = 0; jj <= 5; jj++)
                                            {
                                                var tc = new TableCell();

                                                if (jj == 0)
                                                {
                                                    tc.Append(new Paragraph(new Run(new Text(lstVendor[j].Property))));
                                                }
                                                if (jj == 1)
                                                {


                                                    Paragraph Paragraph1 = new Paragraph();
                                                    Run run1 = new Run();
                                                    Text T1 = new Text();
                                                    T1.Text = lstVendor[j].BidName.ToString() + "  ";
                                                    T1.Space = SpaceProcessingModeValues.Preserve;
                                                    run1.Append(T1);
                                                    //run1.Append(new Text(lstVendor[j].BidAmountT.Split('.')[0]));                                                    
                                                    //RunProperties run1Properties = new RunProperties();
                                                    //run1Properties.Append(new Bold());
                                                    //run1.RunProperties = run1Properties;
                                                    ParagraphProperties paraProperties = new ParagraphProperties();
                                                    Justification justification = new Justification() { Val = JustificationValues.Right };
                                                    paraProperties.Append(justification);
                                                    Paragraph1.Append(paraProperties);
                                                    Paragraph1.Append(run1);
                                                    tc.Append(Paragraph1);




                                                    //tc.Append(new Paragraph(new Run(new Text(lstVendor[j].BidName))));

                                                }
                                                if (jj == 2)
                                                {
                                                    Paragraph Paragraph1 = new Paragraph();
                                                    Run run1 = new Run();

                                                    Text T1 = new Text();
                                                    T1.Text = lstVendor[j].Name.ToString() + "  ";
                                                    T1.Space = SpaceProcessingModeValues.Preserve;
                                                    run1.Append(T1);


                                                    //run1.Append(new Text(lstVendor[j].BidAmountT.Split('.')[0]));                                                    
                                                    //RunProperties run1Properties = new RunProperties();
                                                    //run1Properties.Append(new Bold());
                                                    //run1.RunProperties = run1Properties;
                                                    ParagraphProperties paraProperties = new ParagraphProperties();
                                                    Justification justification = new Justification() { Val = JustificationValues.Right };
                                                    paraProperties.Append(justification);
                                                    Paragraph1.Append(paraProperties);
                                                    Paragraph1.Append(run1);
                                                    tc.Append(Paragraph1);

                                                }
                                                if (jj == 3)
                                                {



                                                    Paragraph Paragraph1 = new Paragraph();
                                                    Run run1 = new Run();

                                                    Text T1 = new Text();
                                                    T1.Text = lstVendor[j].BidAmountT.Split('.')[0].ToString() + "  ";
                                                    T1.Space = SpaceProcessingModeValues.Preserve;
                                                    run1.Append(T1);


                                                    //run1.Append(new Text(lstVendor[j].BidAmountT.Split('.')[0]));                                                    
                                                    //RunProperties run1Properties = new RunProperties();
                                                    //run1Properties.Append(new Bold());
                                                    //run1.RunProperties = run1Properties;
                                                    ParagraphProperties paraProperties = new ParagraphProperties();
                                                    Justification justification = new Justification() { Val = JustificationValues.Right };
                                                    paraProperties.Append(justification);
                                                    Paragraph1.Append(paraProperties);
                                                    Paragraph1.Append(run1);


                                                    //tc.Append(new Paragraph(new Run(new Text(lstVendor[j].BidAmounts.Split('.')[0]))));
                                                    tc.Append(Paragraph1);
                                                    

                                                }
                                                if (jj == 4)
                                                {



                                                    Paragraph Paragraph1 = new Paragraph();
                                                    Run run1 = new Run();

                                                    Text T1 = new Text();
                                                    T1.Text = lstVendor[j].InsuranceAmountT.Split('.')[0].ToString() + "  ";
                                                    T1.Space = SpaceProcessingModeValues.Preserve;
                                                    run1.Append(T1);


                                                    //run1.Append(new Text(lstVendor[j].BidAmountT.Split('.')[0]));                                                    
                                                    //RunProperties run1Properties = new RunProperties();
                                                    //run1Properties.Append(new Bold());
                                                    //run1.RunProperties = run1Properties;
                                                    ParagraphProperties paraProperties = new ParagraphProperties();
                                                    Justification justification = new Justification() { Val = JustificationValues.Right };
                                                    paraProperties.Append(justification);
                                                    Paragraph1.Append(paraProperties);
                                                    Paragraph1.Append(run1);
                                                    //tc.Append(new Paragraph(new Run(new Text(lstVendor[j].InsuranceAmounts).ToString()))));
                                                    tc.Append(Paragraph1);

                                                }
                                                if (jj == 5)
                                                {
                                                    Paragraph Paragraph1 = new Paragraph();
                                                    Run run1 = new Run();
                                                    Text T1 = new Text();
                                                    T1.Text = DateSS + "  ";
                                                    T1.Space = SpaceProcessingModeValues.Preserve;
                                                    run1.Append(T1);
                                                    //run1.Append(new Text(lstVendor[j].BidAmountT.Split('.')[0]));                                                    
                                                    //RunProperties run1Properties = new RunProperties();
                                                    //run1Properties.Append(new Bold());
                                                    //run1.RunProperties = run1Properties;
                                                    ParagraphProperties paraProperties = new ParagraphProperties();
                                                    Justification justification = new Justification() { Val = JustificationValues.Right };
                                                    paraProperties.Append(justification);
                                                    Paragraph1.Append(paraProperties);
                                                    Paragraph1.Append(run1);
                                                    tc.Append(Paragraph1);

                                                }
                                                // Assume you want columns that are automatically sized.
                                              
                                                tr.Append(tc);
                                            }


                                            table.Append(tr);
                                            //t.Rows[j + 1].Cells[0].Paragraphs.First().Append(lstVendor[j].Property);
                                            //t.Rows[j + 1].Cells[1].Paragraphs.First().Append(lstVendor[j].BidName);
                                            //t.Rows[j + 1].Cells[2].Paragraphs.First().Append(lstVendor[j].Name);
                                            //t.Rows[j + 1].Cells[3].Paragraphs.First().Append(lstVendor[j].BidAmounts.Split('.')[0]).Alignment = Alignment.right;
                                            //t.Rows[j + 1].Cells[4].Paragraphs.First().Append(lstVendor[j].InsuranceAmounts.Split('.')[0]).Alignment = Alignment.right;
                                            //t.Rows[j + 1].Cells[5].Paragraphs.First().Append(lstVendor[j].VendorStartdates);
                                            Count++;
                                        }
                                    }
                                    if (Count == 0)
                                    {
                                        BlankRow += j + 1 + ",";
                                    }
                                }



                                body.AppendChild(table);
                              
                                string[] BlankRowsList = BlankRow.Split(',');
                                int counts = 0;
                                for (int k = 0; k < BlankRowsList.Length - 1; k++)
                                {
                                    int val = Convert.ToInt32(BlankRowsList[k]);

                                    counts++;
                                }



                            }
                        }
                        else if (ReportTypeName == "DetailReport")
                        {

                            if (lstVendor.Count > 0)
                            {


                                Table table = new Table();
                                TableRow row = null;
                                TableProperties tableProp = new TableProperties();

                                TableWidth tableWidth = new TableWidth() { Width = "5000", Type = TableWidthUnitValues.Pct };

                                TableBorders tblBorders = new TableBorders();

                                TopBorder topBorder = new TopBorder();

                                topBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);

                                topBorder.Color = "#000000";

                                tblBorders.AppendChild(topBorder);



                                BottomBorder bottomBorder = new BottomBorder();

                                bottomBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);

                                bottomBorder.Color = "#000000";

                                tblBorders.AppendChild(bottomBorder);



                                RightBorder rightBorder = new RightBorder();

                                rightBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);

                                rightBorder.Color = "#000000";

                                tblBorders.AppendChild(rightBorder);



                                LeftBorder leftBorder = new LeftBorder();

                                leftBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);

                                leftBorder.Color = "#000000";

                                tblBorders.AppendChild(leftBorder);



                                InsideHorizontalBorder insideHBorder = new InsideHorizontalBorder();

                                insideHBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);

                                insideHBorder.Color = "#000000";

                                tblBorders.AppendChild(insideHBorder);



                                InsideVerticalBorder insideVBorder = new InsideVerticalBorder();

                                insideVBorder.Val = new EnumValue<BorderValues>(BorderValues.Thick);

                                insideVBorder.Color = "#000000";

                                tblBorders.AppendChild(insideVBorder);



                                //// Add the table borders to the properties

                                tableProp.AppendChild(tblBorders);
                                tableProp.Append(tableWidth);

                                table.AppendChild<TableProperties>(tableProp);


                                row = new TableRow();


                                row.Append(CreateCell("Property"));
                                row.Append(CreateCell("Title"));
                                row.Append(CreateCell("Selected Vendor"));
                                row.Append(CreateCell("Amount"));
                                row.Append(CreateCell("Insurance Amount"));
                                row.Append(CreateCell("Start Date"));

                                //t.Rows[0].Cells[0].Paragraphs.First().Append("Vendor").Bold();
                                //t.Rows[0].Cells[1].Paragraphs.First().Append("Bid Amount").Bold();
                                //t.Rows[0].Cells[2].Paragraphs.First().Append("Insurance Amount").Bold();
                                //t.Rows[0].Cells[3].Paragraphs.First().Append("Start Date").Bold();
                                table.Append(row);

                                IsDetailReport = true;
                                if (Include_COI == "true")
                                {
                                    IncludeCOI = true;
                                }
                                else
                                {
                                    IncludeCOI = false;
                                }
                                //Guid obj = Guid.NewGuid();
                                ////var newHeaderPart = mainDocPart.AddNewPart<HeaderPart>();
                                //var imagePart = mainDocumentPart.AddImagePart(ImagePartType.Jpeg);


                                ////System.Drawing.Image image = System.Drawing.Image.FromFile("‪D:\\1.jpg");
                                ////using (FileStream fs = new FileStream(Server.MapPath("~/Content/themes/assets/images/brand/logo.png"), FileMode.Open))
                                ////{
                                ////    imgPart.FeedData(fs);
                                ////}
                                ////AddImageToBody(doc, mainDocumentPart.GetIdOfPart(imgPart));
                                //using (FileStream stream = new FileStream(Server.MapPath("~/Content/themes/assets/images/brand/logo.png"), FileMode.Open))
                                //{
                                //    imagePart.FeedData(stream);
                                //}
                                //var text = new Text("Hello Open XML world");
                                //var run = new Run(text);
                                //var paragraph = new Paragraph(run);
                                //Drawing imageElement = GetImageElement(mainDocumentPart.GetIdOfPart(imagePart), Server.MapPath("~/Content/themes/assets/images/brand/logo.png"), "my image", 22, 22);
                                //ParagraphProperties paragraphProperties1 = new ParagraphProperties();
                                //Justification justification1 = new Justification() { Val = JustificationValues.Center };
                                //paragraphProperties1.Append(justification1);
                                //paragraph.Append(paragraphProperties1);
                                //body.AppendChild(new Paragraph(new Run(imageElement)));


                                Paragraph p1 = new Paragraph();
                                Run r = new Run();
                                RunProperties rp1 = new RunProperties();
                                rp1.Bold = new Bold();
                                r.Append(rp1);
                                Text t1 = new Text("Work Order Summary Report");
                                r.Append(t1);
                                p1.Append(r);
                                body.Append(p1);
                                Paragraph p2 = new Paragraph();
                                Run r2 = new Run();
                                RunProperties rp2 = new RunProperties();
                                rp2.Color = new Color() { Val = "#0000FF" };
                                r2.Append(rp2);
                                Text t2 = new Text("_____________________________________________________________________________________");
                                r2.Append(t2);
                                p2.Append(r2);
                                body.Append(p2); 

                                //t.Rows[0].Cells[3].Paragraphs.First().Append("DD").Bold();
                                string BlankRow = "";
                                string PropertyName = "";
                                int count = 0;
                                for (int j = 0; j < lstVendor.Count; j++)
                                {
                                    string DateSS = "";

                                    DateSS = _bidReportservice.getVendorDate(Convert.ToInt32(lstVendor[j].BidVendorKey));
                                    if (DateSS == null)
                                    {
                                        DateSS = "";

                                    }


                                    int Count = 0;
                                    BidSelectedVendor = "Cbl_" + lstVendor[j].BidVendorKey;

                                    for (int k = 0; k < BidVendorKeys.Length; k++)
                                    {
                                        if (BidVendorKeys[k] == BidSelectedVendor)
                                        {
                                            VendorKeyList += lstVendor[j].VendorKey + ",";
                                            if (PropertyName == lstVendor[j].Property)
                                            {
                                                count++;

                                            }
                                            else
                                            {
                                                PropertyName = lstVendor[j].Property;
                                                count = 0;
                                            }

                                            //t.MergeCellsInColumn(0, 1, 3);
                                            var trr = new TableRow();
                                            for (var jj = 0; jj <= 5; jj++)
                                            {
                                                var tc = new TableCell();

                                                if (jj == 0)
                                                {
                                                    tc.Append(new Paragraph(new Run(new Text(lstVendor[j].Property))));
                                                }
                                                if (jj == 1)
                                                {


                                                    Paragraph Paragraph1 = new Paragraph();
                                                    Run run1 = new Run();
                                                    Text T1 = new Text();
                                                    T1.Text = lstVendor[j].BidName.ToString() + "  ";
                                                    T1.Space = SpaceProcessingModeValues.Preserve;
                                                    run1.Append(T1);
                                                    //run1.Append(new Text(lstVendor[j].BidAmountT.Split('.')[0]));                                                    
                                                    //RunProperties run1Properties = new RunProperties();
                                                    //run1Properties.Append(new Bold());
                                                    //run1.RunProperties = run1Properties;
                                                    ParagraphProperties paraProperties = new ParagraphProperties();
                                                    Justification justification = new Justification() { Val = JustificationValues.Right };
                                                    paraProperties.Append(justification);
                                                    Paragraph1.Append(paraProperties);
                                                    Paragraph1.Append(run1);
                                                    tc.Append(Paragraph1);



                                                    //tc.Append(new Paragraph(new Run(new Text(lstVendor[j].BidName))));

                                                }
                                                if (jj == 2)
                                                {
                                                    Paragraph Paragraph1 = new Paragraph();
                                                    Run run1 = new Run();

                                                    Text T1 = new Text();
                                                    T1.Text = lstVendor[j].Name.ToString() + "  ";
                                                    T1.Space = SpaceProcessingModeValues.Preserve;
                                                    run1.Append(T1);
                                                    //run1.Append(new Text(lstVendor[j].BidAmountT.Split('.')[0]));                                                    
                                                    //RunProperties run1Properties = new RunProperties();
                                                    //run1Properties.Append(new Bold());
                                                    //run1.RunProperties = run1Properties;
                                                    ParagraphProperties paraProperties = new ParagraphProperties();
                                                    Justification justification = new Justification() { Val = JustificationValues.Right };
                                                    paraProperties.Append(justification);
                                                    Paragraph1.Append(paraProperties);
                                                    Paragraph1.Append(run1);
                                                    tc.Append(Paragraph1);
                                                }
                                                if (jj == 3)
                                                {
                                                    Paragraph Paragraph1 = new Paragraph();
                                                    Run run1 = new Run();
                                                    Text T1 = new Text();
                                                    T1.Text = lstVendor[j].BidAmountT.Split('.')[0].ToString() + "  ";
                                                    T1.Space = SpaceProcessingModeValues.Preserve;
                                                    run1.Append(T1);
                                                    //run1.Append(new Text(lstVendor[j].BidAmountT.Split('.')[0]));                                                    
                                                    //RunProperties run1Properties = new RunProperties();
                                                    //run1Properties.Append(new Bold());
                                                    //run1.RunProperties = run1Properties;
                                                    ParagraphProperties paraProperties = new ParagraphProperties();
                                                    Justification justification = new Justification() { Val = JustificationValues.Right };
                                                    paraProperties.Append(justification);
                                                    Paragraph1.Append(paraProperties);
                                                    Paragraph1.Append(run1);
                                                    //tc.Append(new Paragraph(new Run(new Text(lstVendor[j].BidAmounts.Split('.')[0]))));
                                                    tc.Append(Paragraph1);


                                                }
                                                if (jj == 4)
                                                {



                                                    Paragraph Paragraph1 = new Paragraph();
                                                    Run run1 = new Run();

                                                    Text T1 = new Text();
                                                    T1.Text = lstVendor[j].InsuranceAmountT.Split('.')[0].ToString() + "  ";
                                                    T1.Space = SpaceProcessingModeValues.Preserve;
                                                    run1.Append(T1);


                                                    //run1.Append(new Text(lstVendor[j].BidAmountT.Split('.')[0]));                                                    
                                                    //RunProperties run1Properties = new RunProperties();
                                                    //run1Properties.Append(new Bold());
                                                    //run1.RunProperties = run1Properties;
                                                    ParagraphProperties paraProperties = new ParagraphProperties();
                                                    Justification justification = new Justification() { Val = JustificationValues.Right };
                                                    paraProperties.Append(justification);
                                                    Paragraph1.Append(paraProperties);
                                                    Paragraph1.Append(run1);
                                                    //tc.Append(new Paragraph(new Run(new Text(lstVendor[j].InsuranceAmounts).ToString()))));
                                                    tc.Append(Paragraph1);

                                                }
                                                if (jj == 5)
                                                {

                                                    Paragraph Paragraph1 = new Paragraph();
                                                    Run run1 = new Run();

                                                    Text T1 = new Text();
                                                    T1.Text = DateSS + "  ";
                                                    T1.Space = SpaceProcessingModeValues.Preserve;
                                                    run1.Append(T1);


                                                    //run1.Append(new Text(lstVendor[j].BidAmountT.Split('.')[0]));                                                    
                                                    //RunProperties run1Properties = new RunProperties();
                                                    //run1Properties.Append(new Bold());
                                                    //run1.RunProperties = run1Properties;
                                                    ParagraphProperties paraProperties = new ParagraphProperties();
                                                    Justification justification = new Justification() { Val = JustificationValues.Right };
                                                    paraProperties.Append(justification);
                                                    Paragraph1.Append(paraProperties);
                                                    Paragraph1.Append(run1);

                                                    tc.Append(Paragraph1);

                                                }
                                                // Assume you want columns that are automatically sized.
                                              
                                                trr.Append(tc);
                                            }

                                            table.Append(trr);

                                            Count++;
                                        }
                                    }
                                    if (Count == 0)
                                    {
                                        BlankRow += j + 1 + ",";
                                    }
                                }





                                string[] BlankRowsList = BlankRow.Split(',');
                                int counts = 0;
                                for (int k = 0; k < BlankRowsList.Length - 1; k++)
                                {
                                    int val = Convert.ToInt32(BlankRowsList[k]);
                                    // t.Rows[val - counts].Remove();
                                    counts++; 
                                };
                                //doc.InsertTable(t);
                                body.AppendChild(table);

                                Paragraph newPara = new Paragraph(new Run(new Break() { Type = BreakValues.Page }, new Text("")));
                                body.Append(newPara);
                                Guid obj1 = Guid.NewGuid();
                                //var newHeaderPart = mainDocPart.AddNewPart<HeaderPart>();




                                int VendorCount = 0;
                                for (int j = 0; j < lstVendor.Count; j++)
                                {

                                    string DateSS = "";
                                    DateSS = _bidReportservice.getVendorDate(Convert.ToInt32(lstVendor[j].BidVendorKey));
                                    if (DateSS == null)
                                    {
                                        DateSS = "";

                                    }
                                    BidSelectedVendor = "Cbl_" + lstVendor[j].BidVendorKey;
                                    for (int k = 0; k < BidVendorKeys.Length; k++)
                                    {
                                        if (BidVendorKeys[k] == BidSelectedVendor)
                                        {
                                            if (VendorCount != 0 && lstVendor[j].BidAmounts != "$0.00")
                                            {
                                                Paragraph newPara1 = new Paragraph(new Run(new Break() { Type = BreakValues.Page }, new Text("")));
                                                body.Append(newPara1);
                                                newPageCount = 0;
                                            }

                                            //var imagePart1 = mainDocumentPart.AddImagePart(ImagePartType.Jpeg);

                                            ////System.Drawing.Image image = System.Drawing.Image.FromFile("‪D:\\1.jpg");
                                            ////using (FileStream fs = new FileStream(Server.MapPath("~/Content/themes/assets/images/brand/logo.png"), FileMode.Open))
                                            ////{
                                            ////    imgPart.FeedData(fs);
                                            ////}
                                            ////AddImageToBody(doc, mainDocumentPart.GetIdOfPart(imgPart));
                                            //using (FileStream stream1 = new FileStream(Server.MapPath("~/Content/themes/assets/images/brand/logo.png"), FileMode.Open))
                                            //{
                                            //    imagePart1.FeedData(stream1);
                                            //}
                                            //var text1 = new Text("Hello Open XML world");
                                            //var run1 = new Run(text1);
                                            //var paragraph1 = new Paragraph(run1);
                                            //Drawing imageElement1 = GetImageElement(mainDocumentPart.GetIdOfPart(imagePart1), Server.MapPath("~/Content/themes/assets/images/brand/logo.png"), "my image", 22, 22);
                                            //ParagraphProperties paragraphProperties11 = new ParagraphProperties();
                                            //Justification justification11 = new Justification() { Val = JustificationValues.Center };
                                            //paragraphProperties11.Append(justification11);
                                            //paragraph1.Append(paragraphProperties11);
                                            //body.AppendChild(new Paragraph(new Run(imageElement1)));


                                            if (lstVendor[j].BidAmounts != "$0.00")
                                            {
                                                Paragraph p11 = new Paragraph();
                                                Run r1 = new Run();
                                                RunProperties rp11 = new RunProperties();
                                                rp11.Bold = new Bold();
                                                r1.Append(rp11);
                                                Text t11 = new Text("Work Order Details Report");
                                                r1.Append(t11);
                                                p11.Append(r1);
                                                body.Append(p11);
                                                Paragraph p21 = new Paragraph();
                                                Run r21 = new Run();
                                                RunProperties rp21 = new RunProperties();
                                                rp21.Color = new Color() { Val = "#0000FF" };
                                                r21.Append(rp21);
                                                Text t21 = new Text("_____________________________________________________________________________________");
                                                r21.Append(t21);
                                                p21.Append(r21);
                                                body.Append(p21);






                                                Paragraph Paragraph1789Property = new Paragraph();
                                                Run run1789Property = new Run();
                                                run1789Property.Append(new Text(lstVendor[j].Property));
                                                //RunProperties run1Properties = new RunProperties();
                                                //run1Properties.Append(new Bold());
                                                //run1.RunProperties = run1Properties;
                                                ParagraphProperties paraProperties11Property = new ParagraphProperties();
                                                Justification justification11Property = new Justification() { Val = JustificationValues.Left };
                                                paraProperties11Property.Append(justification11Property);
                                                Paragraph1789Property.Append(paraProperties11Property);
                                                Paragraph1789Property.Append(run1789Property);
                                                body.Append(Paragraph1789Property);





                                                Paragraph Paragraph1789Name = new Paragraph();
                                                Run run1789Name = new Run();
                                                run1789Name.Append(new Text(lstVendor[j].BidName));
                                                //RunProperties run1Properties = new RunProperties();
                                                //run1Properties.Append(new Bold());
                                                //run1.RunProperties = run1Properties;
                                                ParagraphProperties paraProperties11Name = new ParagraphProperties();
                                                Justification justification11Name = new Justification() { Val = JustificationValues.Left };
                                                paraProperties11Name.Append(justification11Name);
                                                Paragraph1789Name.Append(paraProperties11Name);
                                                Paragraph1789Name.Append(run1789Name);
                                                body.Append(Paragraph1789Name);


                                               
                                                Paragraph Paragraph17894ob = new Paragraph();
                                                Run run17894ob = new Run();
                                                run17894ob.Append(new Text(lstVendor[j].Description));
                                                //RunProperties run1Properties = new RunProperties();
                                                //run1Properties.Append(new Bold());
                                                //run1.RunProperties = run1Properties;
                                                ParagraphProperties paraProperties114ob = new ParagraphProperties();
                                                Justification justification114ob = new Justification() { Val = JustificationValues.Left };
                                                paraProperties114ob.Append(justification114ob);
                                                Paragraph17894ob.Append(paraProperties114ob);
                                                Paragraph17894ob.Append(run17894ob);
                                                body.Append(Paragraph17894ob);



                                                Paragraph Paragraph1789 = new Paragraph();
                                                Run run1789 = new Run();


                                                Text T1 = new Text();
                                                T1.Text = "Start Date:    " + DateSS;
                                                T1.Space = SpaceProcessingModeValues.Preserve;
                                                run1789.Append(T1);
                                                //RunProperties run1Properties = new RunProperties();
                                                //run1Properties.Append(new Bold());
                                                //run1.RunProperties = run1Properties;
                                                ParagraphProperties paraProperties11 = new ParagraphProperties();
                                                Justification justification11 = new Justification() { Val = JustificationValues.Right };
                                                paraProperties11.Append(justification11);
                                                Paragraph1789.Append(paraProperties11);
                                                Paragraph1789.Append(run1789);
                                                body.Append(Paragraph1789);

                                                Paragraph Paragraph17894o = new Paragraph();
                                                Run run17894o = new Run();

                                                Text T2 = new Text();
                                                T2.Text = "Bid Amount:   " + lstVendor[j].BidAmounts;
                                                T2.Space = SpaceProcessingModeValues.Preserve;
                                                run17894o.Append(T2);
                                                //RunProperties run1Properties = new RunProperties();
                                                //run1Properties.Append(new Bold());
                                                //run1.RunProperties = run1Properties;
                                                ParagraphProperties paraProperties114o = new ParagraphProperties();
                                                Justification justification114o = new Justification() { Val = JustificationValues.Right };
                                                paraProperties114o.Append(justification114o);
                                                Paragraph17894o.Append(paraProperties114o);
                                                Paragraph17894o.Append(run17894o);
                                                body.Append(Paragraph17894o);

                                                //Table tbl11 = new Table();
                                                //TableRow tr111 = new TableRow();
                                                //TableCell tcName111 = new TableCell(new Paragraph(new Run(new Text(lstVendor[j].Property))));
                                                //tcName111.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2800" }));
                                                //TableCell tcId111 = new TableCell(new Paragraph(new Run(new Text(""))));
                                                //tcId111.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2800" }));
                                                //tr111.Append(tcName111, tcId111);

                                                //TableRow tr121 = new TableRow();
                                                //TableCell tcName121 = new TableCell(new Paragraph(new Run(new Text((lstVendor[j].BidName)))));
                                                //tcName121.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2800" }));

                                                //TableRow tr113 = new TableRow();
                                                //TableCell tcName1114 = new TableCell(new Paragraph(new Run(new Text(lstVendor[k].Name))));
                                                //tcName1114.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2800" }));
                                                //TableCell tcId1119 = new TableCell(new Paragraph(new Run(new Text(""))));
                                                //tcId1119.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2800" }));


                                                //Paragraph Paragraph1789 = new Paragraph();
                                                //Run run1789 = new Run();
                                                //run1789.Append(new Text("Start Date:"));
                                                ////RunProperties run1Properties = new RunProperties();
                                                ////run1Properties.Append(new Bold());
                                                ////run1.RunProperties = run1Properties;
                                                //ParagraphProperties paraProperties11 = new ParagraphProperties();
                                                //Justification justification111 = new Justification() { Val = JustificationValues.Right };
                                                //paraProperties11.Append(justification111);
                                                //Paragraph1789.Append(paraProperties11);
                                                //Paragraph1789.Append(run1789);



                                                //TableCell tcName1113 = new TableCell();
                                                //tcName1113.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2800" }));
                                                //tcName1113.Append(Paragraph1789);

                                                //Paragraph Paragraph17894 = new Paragraph();
                                                //Run run17894 = new Run();
                                                //run17894.Append(new Text(lstVendor[j].ResponseDueDates));
                                                ////RunProperties run1Properties = new RunProperties();
                                                ////run1Properties.Append(new Bold());
                                                ////run1.RunProperties = run1Properties;
                                                //ParagraphProperties paraProperties114 = new ParagraphProperties();
                                                //Justification justification114 = new Justification() { Val = JustificationValues.Right };
                                                //paraProperties114.Append(justification114);
                                                //Paragraph17894.Append(paraProperties114);
                                                //Paragraph17894.Append(run17894);



                                                //TableCell tcName11134 = new TableCell();
                                                //tcName11134.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2800" }));
                                                //tcName11134.Append(Paragraph17894);
                                                //tr113.Append(tcName1114, tcId1119, tcName1113, tcName11134);


                                                //TableRow tr11340 = new TableRow();
                                                //TableCell tcName111440 = new TableCell(new Paragraph(new Run(new Text(""))));
                                                //tcName111440.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2800" }));
                                                //TableCell tcId111940 = new TableCell(new Paragraph(new Run(new Text(""))));
                                                //tcId111940.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2800" }));

                                                //Paragraph Paragraph17894o = new Paragraph();
                                                //Run run17894o = new Run();
                                                //run17894o.Append(new Text("Bid Amount:"));
                                                ////RunProperties run1Properties = new RunProperties();
                                                ////run1Properties.Append(new Bold());
                                                ////run1.RunProperties = run1Properties;
                                                //ParagraphProperties paraProperties114o = new ParagraphProperties();
                                                //Justification justification114o = new Justification() { Val = JustificationValues.Right };
                                                //paraProperties114o.Append(justification114o);
                                                //Paragraph17894o.Append(paraProperties114o);
                                                //Paragraph17894o.Append(run17894o);



                                                //TableCell tcName11130 = new TableCell();
                                                //tcName11130.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2800" }));
                                                //tcName11130.Append(Paragraph17894o);

                                                //Paragraph Paragraph17894q = new Paragraph();
                                                //Run run17894q = new Run();
                                                //run17894q.Append(new Text(lstVendor[j].BidAmounts));
                                                ////RunProperties run1Properties = new RunProperties();
                                                ////run1Properties.Append(new Bold());
                                                ////run1.RunProperties = run1Properties;
                                                //ParagraphProperties paraProperties114q = new ParagraphProperties();
                                                //Justification justification114q = new Justification() { Val = JustificationValues.Right };
                                                //paraProperties114q.Append(justification114q);
                                                //Paragraph17894q.Append(paraProperties114q);
                                                //Paragraph17894q.Append(run17894q);
                                                //TableCell tcName11134q = new TableCell();
                                                //tcName11134q.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "5000" }));
                                                //tcName11134q.Append(Paragraph17894q);
                                                //tr11340.Append(tcName111440, tcId111940, tcName11130, tcName11134q);

                                                ////TableRow tr115 = new TableRow();
                                                ////TableCell tcId1117 = new TableCell(new Paragraph(new Run(new Text(lstVendor[j].Description))));
                                                ////tcId1117.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "5000" }));
                                                ////tr115.Append(tcId1117);


                                                ////table.GetBorder(0);
                                                ////table.SetWidthsPercentage(100%);
                                                //tr121.Append(tcName121);
                                                //tbl11.AppendChild(tr111);
                                                //tbl11.AppendChild(tr121);
                                                //tbl11.AppendChild(tr113);
                                                //tbl11.AppendChild(tr11340);
                                                ////tbl11.AppendChild(tr115);
                                                ////List<NoteModel> NotesList = new List<NoteModel>();
                                                ////NotesList = _bidReportservice.NotesByWorkReport(lstVendor[j].BidRequestKey, 106);
                                                ////for (int l = 0; l < NotesList.Count; l++)
                                                ////{
                                                ////    TableRow tr1156 = new TableRow();
                                                ////    TableCell tcId11176 = new TableCell(new Paragraph(new Run(new Text(NotesList[l].Description))));
                                                ////    tcId11176.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "5000" }));
                                                ////    tr1156.Append(tcId11176);
                                                ////    tbl11.AppendChild(tr1156);
                                                ////}

                                                //body.AppendChild(tbl11);

                                                Paragraph Paragraph1789BidName = new Paragraph();
                                                Run run1789BidName = new Run();
                                                run1789BidName.Append(new Text(lstVendor[k].Name));
                                                //RunProperties run1Properties = new RunProperties();
                                                //run1Properties.Append(new Bold());
                                                //run1.RunProperties = run1Properties;
                                                ParagraphProperties paraProperties11BidName = new ParagraphProperties();
                                                Justification justification11BidName = new Justification() { Val = JustificationValues.Left };
                                                paraProperties11BidName.Append(justification11BidName);
                                                Paragraph1789BidName.Append(paraProperties11BidName);
                                                Paragraph1789BidName.Append(run1789BidName);
                                                body.Append(Paragraph1789BidName);

                                                Paragraph Paragraph178941ob = new Paragraph();
                                                Run run178941ob = new Run();
                                                run178941ob.Append(new Text(lstVendor[j].Descrip));
                                                //RunProperties run1Properties = new RunProperties();
                                                //run1Properties.Append(new Bold());
                                                //run1.RunProperties = run1Properties;
                                                ParagraphProperties paraProperties1141ob = new ParagraphProperties();
                                                Justification justification1141ob = new Justification() { Val = JustificationValues.Left };
                                                paraProperties1141ob.Append(justification1141ob);
                                                Paragraph178941ob.Append(paraProperties1141ob);
                                                Paragraph178941ob.Append(run178941ob);
                                                body.Append(Paragraph178941ob);


                                                List<NoteModel> NotesList = new List<NoteModel>();
                                                NotesList = _bidReportservice.NotesByWorkReport(lstVendor[j].BidRequestKey, 106);
                                                for (int l = 0; l < NotesList.Count; l++)
                                                {
                                                    Paragraph Paragraph17894ob1 = new Paragraph();
                                                    Run run17894ob1 = new Run();
                                                    run17894ob1.Append(new Text(NotesList[l].Description));
                                                    //RunProperties run1Properties = new RunProperties();
                                                    //run1Properties.Append(new Bold());p
                                                    //run1.RunProperties = run1Properties;
                                                    ParagraphProperties paraProperties114ob1 = new ParagraphProperties();
                                                    Justification justification114ob1 = new Justification() { Val = JustificationValues.Left };
                                                    paraProperties114ob1.Append(justification114ob1);
                                                    Paragraph17894ob1.Append(paraProperties114ob1);
                                                    Paragraph17894ob1.Append(run17894ob1);
                                                    body.Append(Paragraph17894ob1);

                                                }
                                            }
                                       
                                            VendorCount++;







                                            //for (int h = 0; h < 23; h++)
                                            //{
                                            //    doc.InsertParagraph(Environment.NewLine);
                                            //}
                                            if (BidVendorKeys.Length - 1 != j)
                                            {
                                                if (BidRequestKeys.Length - 1 == i && (lstVendor.Count - 1 == j || BidVendorKeys.Length - 2 == k))
                                                {

                                                    
                                                }
                                                else
                                                {
                                                    if (VendorCount != 0)
                                                    {
                                                        //Paragraph p3 = doc.InsertParagraph();
                                                        ////p2.AppendLine("Hello First page.");
                                                        //p3.InsertPageBreakAfterSelf();
                                                        newPageCount = 1;
                                                    }
                                                    else
                                                    {
                                                        newPageCount = 0;
                                                    }
                                                }
                                            }
                                            //doc.Sections[0].SectionBreakType = SectionBreakType.defaultNextPage;
                                            ////doc.Sections[0].Paragraphs[3].AppendBreak(BreakType.PageBreak);

                                            //doc.Sections[0].GetType();
                                            ////doc.Sections[0].Paragraphs[3].AppendBreak(BreakType.PageBreak);
                                        }
                                    }
                                }

                               


                            }
                        }
                    }
                    //doc.Save();

                    SectionProperties objSectionProperties =
                         new SectionProperties();
                    FooterPart objFootPart = mainDocumentPart.AddNewPart<FooterPart>();
                    Footer objFooter = new Footer();
                    objFootPart.Footer = objFooter;
                    SdtBlock objSdtBlock_1 = new SdtBlock();
                    SdtContentBlock objSdtContentBlock_1 =
                        new SdtContentBlock();
                    SdtBlock objSdtBlock_2 = new SdtBlock();
                    SdtContentBlock objSdtContentBlock_2 =
                        new SdtContentBlock();
                    Paragraph objParagraph_1 = new Paragraph();
                    ParagraphProperties objParagraphProperties =
                        new ParagraphProperties();
                    ParagraphStyleId objParagraphStyleId =
                        new ParagraphStyleId() { Val = "Footer" };
                    objParagraphProperties.Append(objParagraphStyleId);
                    Justification objJustification =
                        new Justification() { Val = JustificationValues.Right };
                    objParagraphProperties.Append(objJustification);
                    objParagraph_1.Append(objParagraphProperties);
                    Run objRun_1 = new Run();
                    Text objText_1 = new Text();
                    objText_1.Text = " Page ";
                    objText_1.Space = SpaceProcessingModeValues.Preserve;
                    objRun_1.Append(objText_1);
                    objParagraph_1.Append(objRun_1);
                    Run objRun_2 = new Run();
                    FieldChar objFieldChar_1 =
                        new FieldChar()
                        { FieldCharType = FieldCharValues.Begin };
                    objRun_2.Append(objFieldChar_1);
                    objParagraph_1.Append(objRun_2);
                    Run objRun_3 = new Run();
                    FieldCode objFieldCode_1 =
                        new FieldCode()
                        { Space = SpaceProcessingModeValues.Preserve };
                    objFieldCode_1.Text = "PAGE ";
                    objFieldCode_1.Space = SpaceProcessingModeValues.Preserve;
                    objRun_3.Append(objFieldCode_1);
                    objParagraph_1.Append(objRun_3);
                    Run objRun_4 = new Run();
                    FieldChar objFieldChar_2 =
                        new FieldChar()
                        { FieldCharType = FieldCharValues.Separate };
                    objRun_4.Append(objFieldChar_2);
                    objParagraph_1.Append(objRun_4);
                    Run objRun_5 = new Run();
                    Text objText_2 = new Text();
                    objText_2.Text = "2 ";

                    objText_2.Space = SpaceProcessingModeValues.Preserve;
                    objRun_5.Append(objText_2);
                    objParagraph_1.Append(objRun_5);
                    Run objRun_6 = new Run();
                    FieldChar objFieldChar_3 =
                        new FieldChar() { FieldCharType = FieldCharValues.End };
                    objRun_6.Append(objFieldChar_3);
                    objParagraph_1.Append(objRun_6);
                    Run objRun_7 = new Run();
                    Text objText_3 = new Text();
                    objText_3.Text = " of ";
                    objText_3.Space = SpaceProcessingModeValues.Preserve;
                    objRun_7.Append(objText_3);
                    objParagraph_1.Append(objRun_7);
                    Run objRun_8 = new Run();
                    FieldChar objFieldChar_4 =
                        new FieldChar()
                        { FieldCharType = FieldCharValues.Begin };
                    objRun_8.Append(objFieldChar_4);
                    objParagraph_1.Append(objRun_8);
                    Run objRun_9 = new Run();
                    FieldCode objFieldCode_2 =
                        new FieldCode()
                        { Space = SpaceProcessingModeValues.Preserve };
                    objFieldCode_2.Text = "NUMPAGES  ";
                    objFieldCode_2.Space = SpaceProcessingModeValues.Preserve;
                    objRun_9.Append(objFieldCode_2);
                    objParagraph_1.Append(objRun_9);
                    Run objRun_10 = new Run();
                    FieldChar objFieldChar_5 =
                        new FieldChar()
                        { FieldCharType = FieldCharValues.Separate };
                    objRun_10.Append(objFieldChar_5);
                    objParagraph_1.Append(objRun_10);
                    Run objRun_11 = new Run();
                    Text objText_4 = new Text();
                    objText_4.Text = "2";
                    objText_4.Space = SpaceProcessingModeValues.Preserve;
                    objRun_11.Append(objText_4);
                    objParagraph_1.Append(objRun_11);
                    Run objRun_12 = new Run();
                    FieldChar objFieldChar_6 =
                        new FieldChar() { FieldCharType = FieldCharValues.End };
                    objRun_12.Append(objFieldChar_6);
                    objParagraph_1.Append(objRun_12);
                    objSdtContentBlock_2.Append(objParagraph_1);
                    objSdtBlock_2.Append(objSdtContentBlock_2);
                    objSdtContentBlock_1.Append(objSdtBlock_2);
                    objSdtBlock_1.Append(objSdtContentBlock_1);
                    objFooter.Append(objSdtBlock_1);
                    string strFootrID =
                        mainDocumentPart.GetIdOfPart(objFootPart);
                    FooterReference objFooterReference = new FooterReference()
                    {
                        Type = HeaderFooterValues.Default,
                        Id = strFootrID
                    };
                    objSectionProperties.Append(objFooterReference);
                    body.AppendChild(objSectionProperties);
                    DocumentSettingsPart objDocumentSettingPart = mainDocumentPart.DocumentSettingsPart;
                    objDocumentSettingPart.Settings = new Settings();
                    Compatibility objCompatibility = new Compatibility();
                    CompatibilitySetting objCompatibilitySetting =
                        new CompatibilitySetting()
                        {
                            Name = CompatSettingNameValues.CompatibilityMode,
                            Uri = "http://schemas.microsoft.com/office/word",
                            Val = "14"
                        };
                    objCompatibility.Append(objCompatibilitySetting);
                    objDocumentSettingPart.Settings.Append(objCompatibility);
                    mainDocumentPart.Document.Save();
                }



                //string fileName = Server.MapPath("~/Documents/Reports/") + ReportType + ".docx";
                string filePath = fileName;

                if (ReportType == "EmailReport")
                {
                    //_bidReportservice.EmailReport(fileName, ResourceKey, ReportTypeName);

                    using (var mainDoc = WordprocessingDocument.Open(fileNamen, false))
                    using (var resultDoc = WordprocessingDocument.Create(fileName, WordprocessingDocumentType.Document))
                    {
                        // copy parts from source document to new document
                        foreach (var part in mainDoc.Parts)
                            resultDoc.AddPart(part.OpenXmlPart, part.RelationshipId);
                        // perform replacements in resultDoc.MainDocumentPart
                        // ...
                    }
                    updatefile();
                    SaveReport(Convert.ToInt32(ResourceKey), IsDetailReport, ReportTypeNames, IncludeCOI, IsSent, VendorKeyList);


                    ReportEmailModel lstuser = new ReportEmailModel();
                    IList<VendorModel> Documentlist = null;
                    string lookUpTitle = "BidEmailSent";
                    //lstuser = __IAPIservice.GetReportList();
                    bool status = false;
                    //for (int i = 0; i < lstuser.Count; i++)
                    //{
                    //if (ReportTypeNames == ReportTypeNames)
                    //{
                    lstuser.ReportDocumentFilePath = Server.MapPath("~/Documents/Reports/") + ReportTypeNames + ".docx";
                    //lstuser[i].ReportDocumentFilePath = "../Documents/Reports/" + lstuser[i].DocumentName + ".docx";
                    lstuser.DocumentName = ReportTypeNames;
                    lstuser.IsDetailedReport = IsDetailReport;
                    lstuser.IsSent = IsSent;
                    lstuser.IncludeCOI = IncludeCOI;
                    lstuser.ResourceKey = Convert.ToInt32(ResourceKey);
                    lstuser.VendorList = VendorKeyList;
                    if (lstuser.IncludeCOI == true)
                    {
                        string[] VendorList = VendorKeyList.Split(',');
                        for (int j = 0; j < VendorList.Length - 1; j++)
                        {


                            if (VendorList[j].Contains("Cbl_"))
                            {
                                VendorList[j] = VendorList[j].Replace("Cbl_", "");
                            }

                            Documentlist = bindDocument12(Convert.ToInt32(VendorList[j]));
                            int count = 0;
                            for (int k = 0; k < Documentlist.Count; k++)
                            {
                                string pathd = Server.MapPath("~/Document/Insurance/" + Convert.ToInt32(VendorList[j]) + "/" + Documentlist[k].Insurance.InsuranceKey + "/" + Documentlist[k].Document.FileName);
                                string fileNamenew = pathd;
                                string path = fileNamenew;
                                FileInfo filem = new FileInfo(path);
                                if (filem.Exists)//check file exsit or not  
                                {
                                    lstuser.InsuranceDocumentFilePath += path + ",";
                                }
                                else
                                {
                                    string pathd1 = Server.MapPath("~/Document/Insurance/" + Documentlist[k].Insurance.InsuranceKey + Documentlist[k].Document.FileName);

                                    FileInfo filem1 = new FileInfo(pathd1);
                                    if (filem1.Exists)//check file exsit or not  
                                    {
                                        lstuser.InsuranceDocumentFilePath += pathd1 + ",";
                                    }
                                }

                            }
                        }
                        if (lstuser.InsuranceDocumentFilePath != "" && lstuser.InsuranceDocumentFilePath != null)
                        {
                            lstuser.InsuranceDocumentFilePath = lstuser.InsuranceDocumentFilePath.Remove(lstuser.InsuranceDocumentFilePath.Length - 1, 1);
                        }
                    }

                    status = __IAPIservice.BidAbdWorkOrderEmailSent(lstuser);
                    //}
                    //}





                    return Json(true, JsonRequestBehavior.AllowGet);
                }

                else
                {

                    using (var mainDoc = WordprocessingDocument.Open(fileNamen, false))
                    using (var resultDoc = WordprocessingDocument.Create(fileName,
                      WordprocessingDocumentType.Document))
                    {
                        // copy parts from source document to new document
                        foreach (var part in mainDoc.Parts)
                            resultDoc.AddPart(part.OpenXmlPart, part.RelationshipId);
                        // perform replacements in resultDoc.MainDocumentPart
                        // ...
                    }
                    
                    string[] file = new string[1];
                    /// Documents / Reports 
                    file[0] = "../Documents/Reports/" + ReportTypeNames + ".docx";
                    // DownloadFile(Server.MapPath("~/Documents/Reports/"), file.Name);
                    //Process.Start("WINWORD.EXE", fileName);

                    updatefile();
                    return Json(file, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(ex, JsonRequestBehavior.AllowGet);
            }
        }

        public IList<VendorModel> bindDocument12(int CompanyKey)
        {
            try
            {
                IList<VendorModel> Documentlist = null;

                Documentlist = __IAPIservice.GetbindDocument12(CompanyKey);
                return Documentlist;
            }
            catch
            {
                return null;
            }
        }
        public void SaveReport(int ResourceKey, bool IsDetailedReport, string DocumentName, bool IncludeCOI, bool IsSent, string VendorList)
        {
            ReportEmailModel ReportEmailModels = new ReportEmailModel();

            ReportEmailModels.ResourceKey = ResourceKey;
            ReportEmailModels.IsDetailedReport = IsDetailedReport;
            ReportEmailModels.DocumentName = DocumentName;
            ReportEmailModels.IncludeCOI = IncludeCOI;
            ReportEmailModels.IsSent = IsSent;
            ReportEmailModels.VendorList = VendorList;
            _bidReportservice.Insert(ReportEmailModels);
        }
        public ActionResult PMAdminBidReport()
        {
            Int32 resourcekey = Convert.ToInt32(Session["resourceid"]);
            Int32 companyKey = Convert.ToInt32(Session["CompanyKey"]);

            IList<BidRequestModel> lstProperty = null;
            lstProperty = _bidRequestservice.GetAllProperty(resourcekey, companyKey);

            List<System.Web.Mvc.SelectListItem> lstPropertylist = new List<System.Web.Mvc.SelectListItem>();
            System.Web.Mvc.SelectListItem sli2 = new System.Web.Mvc.SelectListItem();
            sli2.Text = "--Please Select--";
            sli2.Value = "0";
            lstPropertylist.Add(sli2);
            for (int i = 0; i < lstProperty.Count; i++)
            {
                System.Web.Mvc.SelectListItem sli = new System.Web.Mvc.SelectListItem();
                sli.Text = Convert.ToString(lstProperty[i].Property);
                sli.Value = Convert.ToString(lstProperty[i].PropertyKey);
                lstPropertylist.Add(sli);
            }

            ViewBag.lstProperty = lstPropertylist;

            //var lstBidStatus = new List<SelectListItem>()
            //{
            //  new SelectListItem{ Value="0",Text="Please Select",Selected=true},
            //  new SelectListItem{ Value="1",Text="Pending"},
            //  new SelectListItem{ Value="2",Text="Approve"},
            //  new SelectListItem{ Value="3",Text="Unapprove"},
            //};
            //ViewBag.lstBidStatus = lstBidStatus;

            //list of Bid Request Status
            IList<LookUpModel> lstBidReqStatus = _bidRequestservice.GetBidRequetStatusFilter();
            List<System.Web.Mvc.SelectListItem> lstBidReqStatuslist = new List<System.Web.Mvc.SelectListItem>();
            lstBidReqStatuslist.Add(new SelectListItem() { Text = "Show All", Value = "0,0" });
            for (int i = 0; i < lstBidReqStatus.Count; i++)
            {
                lstBidReqStatuslist.Add(new SelectListItem() { Text = Convert.ToString(lstBidReqStatus[i].Title), Value = Convert.ToString(lstBidReqStatus[i].LookUpKey1) });
            }
            ViewBag.lstBidStatus = lstBidReqStatuslist;
            return View();
        }
        public ActionResult PMViewAdminWorkOrderReport()
        {
            //var lststatus = new List<SelectListItem>()
            //{
            //  new SelectListItem{ Value="0",Text="Please Select",Selected=true},
            //  new SelectListItem{ Value="1",Text="Pending"},
            //  new SelectListItem{ Value="2",Text="Approve"},
            //  new SelectListItem{ Value="3",Text="Unapprove"},
            //};
            //ViewBag.lststatus = lststatus;
            IList<BidRequestModel> lstProperty = null;
            Int32 resourcekey = Convert.ToInt32(Session["resourceid"]);
            Int32 companyKey = Convert.ToInt32(Session["CompanyKey"]);

            //IList<BidRequestModel> lstProperty = null;
            lstProperty = _bidRequestservice.GetAllProperty(resourcekey, companyKey);
            List<System.Web.Mvc.SelectListItem> lstPropertylist = new List<System.Web.Mvc.SelectListItem>();
            System.Web.Mvc.SelectListItem sli2 = new System.Web.Mvc.SelectListItem();
            sli2.Text = "--Please Select--";
            sli2.Value = "0";
            lstPropertylist.Add(sli2);
            for (int i = 0; i < lstProperty.Count; i++)
            {
                System.Web.Mvc.SelectListItem sli = new System.Web.Mvc.SelectListItem();
                sli.Text = Convert.ToString(lstProperty[i].Property);
                sli.Value = Convert.ToString(lstProperty[i].PropertyKey);
                lstPropertylist.Add(sli);
            }

            ViewBag.lstProperty = lstPropertylist;

            IList<LookUpModel> lstBidReqStatus = _bidRequestservice.GetBidRequetStatusFilter();
            List<System.Web.Mvc.SelectListItem> lstBidReqStatuslist = new List<System.Web.Mvc.SelectListItem>();
            lstBidReqStatuslist.Add(new SelectListItem() { Text = "Show All", Value = "0,0" });
            for (int i = 0; i < lstBidReqStatus.Count; i++)
            {
                lstBidReqStatuslist.Add(new SelectListItem() { Text = Convert.ToString(lstBidReqStatus[i].Title), Value = Convert.ToString(lstBidReqStatus[i].LookUpKey1) });
            }
            ViewBag.lstBidStatus = lstBidReqStatuslist;
            return View();
        }
        public ActionResult PMViewAdminGeneratedReport(string BidRequestId)
        {
            string[] bidRequestId = BidRequestId.Split(',');
            string bidval = "";
            for (int i = 0; i < bidRequestId.Length; i++)
            {
                bidval += bidRequestId[i].Split('_')[1] + ",";
            }
            //string val = (bidval.Length - 1).ToString();
            string val = bidval.Remove(bidval.Length - 1, 1);
            ViewBag.BidRequestId = val;
            return View();
        }
    }
}



