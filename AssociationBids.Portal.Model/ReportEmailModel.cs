using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationBids.Portal.Model
{
    public class ReportEmailModel
    {
        public int ReportEmailKey { get; set; }
        public int ResourceKey { get; set; }
        public bool IsDetailedReport { get; set; }
        public string DocumentName { get; set; }
        public bool IncludeCOI { get; set; }
        public bool IsSent { get; set; }
        public string VendorList { get; set; }
        public string ReportDocumentFilePath { get; set; }
        public string InsuranceDocumentFilePath { get; set; }
    }
}
