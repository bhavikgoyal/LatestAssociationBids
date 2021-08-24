using AssociationBids.Portal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationBids.Portal.Repository.Base
{
    public interface IDashboardRepository
    {
        List<ADashboardModel> PieChartBidRequest(int ckey, int Portalkey, int ResourceKey);
        List<ADashboardModel> PieChartForVendor(int ckey);
        List<ADashboardModel> BindVendorsDashboard();
        List<ADashboardModel> BindAdminProjectsValue(int ckey, int Portalkey, int ResourceKey);
        List<ADashboardModelLineChart> BindAdminProjectsLineChartValue(int ckey, int PortalKey, int ResourceKey);
    }
}
