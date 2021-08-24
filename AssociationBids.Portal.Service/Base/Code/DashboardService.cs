using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationBids.Portal.Service.Base 
{
    public class DashboardService : BaseService, IDashboardService
    {
        protected IDashboardRepository __vDashboardService;


        public DashboardService()
           : this(new DashboardRepository()) { }

        public DashboardService(string connectionString)
           : this(new DashboardRepository(connectionString)) { }

        public DashboardService(DashboardRepository vDashboardService)
        {
            ConnectionString = vDashboardService.ConnectionString;

            __vDashboardService = vDashboardService;
        }
        public List<ADashboardModel> PieChartBidRequest(int ckey, int Portalkey, int ResourceKey)
        {
            return __vDashboardService.PieChartBidRequest(ckey, Portalkey, ResourceKey);
        }
        public List<ADashboardModel> PieChartForVendor(int ckey)
        {
            return __vDashboardService.PieChartForVendor(ckey);
        }

        public List<ADashboardModel> BindVendorsDashboard()
        {
            return __vDashboardService.BindVendorsDashboard();
        }
        public List<ADashboardModel> BindAdminProjectsValue(int ckey, int Portalkey, int ResourceKey)
        {
            return __vDashboardService.BindAdminProjectsValue(ckey, Portalkey, ResourceKey);
        }

    
        
        public List<ADashboardModelLineChart> BindAdminProjectsLineChartValue(int ckey, int Portalkey, int ResourceKey)
        {
            return __vDashboardService.BindAdminProjectsLineChartValue(ckey, Portalkey, ResourceKey);
        }
        
    }
}
