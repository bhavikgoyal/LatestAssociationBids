using AssociationBids.Portal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssociationBids.Portal.Controllers
{
    public class DashboardController : Controller
    {
        private readonly AssociationBids.Portal.Service.Base.IDashboardService _DashboardService;

        public DashboardController(AssociationBids.Portal.Service.Base.IDashboardService DashboardService)
        {
            this._DashboardService = DashboardService;
        }
        // GET: Dashboard
        public ActionResult Dashboard()
        {
            return View();
        }

        public JsonResult PieChart(string bType)
        {
            int ckey = 0;
            int Portalkey = Convert.ToInt32(Session["Portalkey"]);
            int ResourceKey = Convert.ToInt32(Session["resourceid"]);
            if (Portalkey == 1)
            {

            }
            else if (Portalkey == 2)
            {
                ckey = Convert.ToInt32(Session["CompanyKey"]);
            }
            List<ADashboardModel> lstEmailTemplate = null;
            lstEmailTemplate = _DashboardService.PieChartBidRequest(ckey, Portalkey, ResourceKey);
            List<ADashboardModelC> dashboard;
          


            if (bType == "B")
            {
                dashboard = new List<ADashboardModelC>
             {
                     new ADashboardModelC { BidValue = lstEmailTemplate[0].OpenBidRequest , Type = "Open (" +  lstEmailTemplate[0].OpenBidRequest  + ")", },
                     new ADashboardModelC { BidValue = lstEmailTemplate[0].AwardedBidRequest  , Type = "Awarded (" +  lstEmailTemplate[0].AwardedBidRequest  + ")",  },
                     new ADashboardModelC { BidValue = lstEmailTemplate[0].ClosedBidRequest  , Type = "Closed (" +  lstEmailTemplate[0].ClosedBidRequest  + ")",  }
            };
            }
            else
            {
                dashboard = new List<ADashboardModelC>
             {
                     new ADashboardModelC { BidValue = lstEmailTemplate[1].OpenBidRequest  , Type = "Open (" +  lstEmailTemplate[1].OpenBidRequest  + ")",  },
                     new ADashboardModelC { BidValue = lstEmailTemplate[1].AwardedBidRequest  , Type = "Awarded (" +  lstEmailTemplate[1].AwardedBidRequest  + ")",  },
                     new ADashboardModelC { BidValue = lstEmailTemplate[1].ClosedBidRequest , Type = "Closed (" +  lstEmailTemplate[1].ClosedBidRequest  + ")",  }
            };
            }

            return Json(dashboard, JsonRequestBehavior.AllowGet);
        }

   
        public JsonResult PieChartForVendor()
        {

            int ckey = ckey = Convert.ToInt32(Session["CompanyKey"]);

            List<ADashboardModel> lstEmailTemplate = null;
            lstEmailTemplate = _DashboardService.PieChartForVendor(ckey);
            List<ADashboardModelC> dashboard;

            dashboard = new List<ADashboardModelC>
             {




                     new ADashboardModelC { BidValue = lstEmailTemplate[0].NotIntersted, Type = "Not Interested  (" +  lstEmailTemplate[0].NotIntersted  + ")", },
                     new ADashboardModelC { BidValue = lstEmailTemplate[0].BidNotSubmitted, Type = "Open  (" +  lstEmailTemplate[0].BidNotSubmitted  + ")",  },
                     new ADashboardModelC { BidValue = lstEmailTemplate[0].Awarded, Type = "Awarded  (" +  lstEmailTemplate[0].Awarded  + ")",  },
                     new ADashboardModelC { BidValue = lstEmailTemplate[0].Rejected, Type = " Rejected (" +  lstEmailTemplate[0].Rejected  + ")",  },
                     new ADashboardModelC { BidValue = lstEmailTemplate[0].BidSubmitted, Type = "Submitted  (" +  lstEmailTemplate[0].BidSubmitted  + ")",  }
            };





            return Json(dashboard, JsonRequestBehavior.AllowGet);
        }


        public JsonResult BindTotalAmount()
        {
            int ckey = 0;
            int Portalkey = Convert.ToInt32(Session["Portalkey"]);
            int ResourceKey = Convert.ToInt32(Session["resourceid"]);
            if (Portalkey == 1)
            {

            }
            else if (Portalkey == 2)
            {
                ckey = Convert.ToInt32(Session["CompanyKey"]);
            }

            else if (Portalkey == 3)
            {
                ckey = Convert.ToInt32(Session["CompanyKey"]);
            }

            List<ADashboardModel> lstEmailTemplate = null;
            lstEmailTemplate = _DashboardService.PieChartBidRequest(ckey, Portalkey, ResourceKey);
            return Json(lstEmailTemplate, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ProjectsValue()
        {

            int ckey = 0;
            int Portalkey = Convert.ToInt32(Session["Portalkey"]);
            int ResourceKey = Convert.ToInt32(Session["resourceid"]);
            if (Portalkey == 1)
            {
                ckey = 0;
            }

            else if (Portalkey == 2)
            {
                ckey = Convert.ToInt32(Session["CompanyKey"]);
            }
            else if (Portalkey == 3)
            {
                ckey = Convert.ToInt32(Session["CompanyKey"]);
            }


            List<ADashboardModel> lstEmailTemplate = null;
            lstEmailTemplate = _DashboardService.BindAdminProjectsValue(ckey, Portalkey, ResourceKey);
            return Json(lstEmailTemplate, JsonRequestBehavior.AllowGet);
        }
        public JsonResult BindTotalVendors()
        {

            List<ADashboardModel> lstEmailTemplate = null;
            lstEmailTemplate = _DashboardService.BindVendorsDashboard();
            return Json(lstEmailTemplate, JsonRequestBehavior.AllowGet);
        }
      
        public JsonResult ProjectsLineChartValue()
        {
            int ckey = 0;
            int Portalkey = Convert.ToInt32(Session["Portalkey"]);
            int ResourceKey = Convert.ToInt32(Session["resourceid"]);

            if (Portalkey == 1)
            {
                ckey = 0;
            }

            else if (Portalkey == 2)
            {
                ckey = Convert.ToInt32(Session["CompanyKey"]);
            }
            else if (Portalkey == 3)
            {
                ckey = Convert.ToInt32(Session["CompanyKey"]);
            }


            List<ADashboardModelLineChart> lstEmailTemplate = null;
            lstEmailTemplate = _DashboardService.BindAdminProjectsLineChartValue(ckey, Portalkey, ResourceKey);
            return Json(lstEmailTemplate, JsonRequestBehavior.AllowGet);
        }



    }
}