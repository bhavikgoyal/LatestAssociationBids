using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AssociationBids.Portal.Model
{
   
   public  class EmailTemplateModel: BaseModel
    {
        public int EmailTemplateKey { get; set; }
        public int Superadminkey { get; set; }
        public string  EmailTitle { get; set; }
        public string EmailSubject { get; set; }

        [AllowHtml]
        public string Body { get; set; }
        public Int32 TotalRecords { get; set; }
        public DateTime DateAdded { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        public int lookUpType { get; set; }
        public string Title { get; set; }

        
        public string PersonalDetails { get; set; }

   }
}
