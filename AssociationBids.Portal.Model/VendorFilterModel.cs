using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationBids.Portal.Model
{
    public class VendorFilterModel : BaseFilterModel
    {
        public int CompanyKey { get; set; }
        public string State { get; set; }
        public int Status { get; set; }
    }
}
