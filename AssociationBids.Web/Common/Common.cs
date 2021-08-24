using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AssociationBids.Portal.Web
{
    public class Common
    {
        public static List<SelectListItem> GetRadiusList()
        {
            return new List<SelectListItem>()
            {
              new SelectListItem{ Value="0",Text="Please Select",Selected=true},
              new SelectListItem{ Value="10",Text="10 miles"},
              new SelectListItem{ Value="25",Text="25 miles"},
              new SelectListItem{ Value="50",Text="50 miles"},
              new SelectListItem{ Value="100",Text="100 miles"},
            };
        }
    }
}
