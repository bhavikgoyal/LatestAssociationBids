using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AssociationBids.Portal.Model
{
    class EmailTempletModel
    {
        public int EmailTemplateKey { get; set; }

        [Required(ErrorMessage = "Please enter Email Title")]
        public string EmailTitle { get; set; }
        [Required(ErrorMessage = "Please enter Email Title")]
        public string EmailSubject { get; set; }
        public string Body { get; set; }
        public Int32 TotalRecords { get; set; }
        public string DateAdded { get; set; }
        public int LookUpTypeKey { get; set; }
        public string Title { get; set; }
        [AllowHtml]
        public string PersonalDetails { get; set; }
    }
}
