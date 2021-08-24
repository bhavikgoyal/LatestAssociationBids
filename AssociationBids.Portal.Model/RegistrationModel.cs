using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AssociationBids.Portal.Model
{
    public class RegistrationModel : BaseModel
    {
        
        public string CompanyName { get; set; }        
        public string lblresult { get; set; }        
        public int CompanyKey { get; set; }        
        public int Resourcekey { get; set; }        
        public string LegalName { get; set; }
        public string TaxID { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }        
        public string StateKey { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Work { get; set; }
        public string Work2 { get; set; }
        public string Fax { get; set; }
        public string Website { get; set; }


        //user information
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }

        //Service
        public int  ServiceKey { get; set; }
        public string ServiceTitle1 { get; set; }
        public string ServiceTitle2 { get; set; }
        public string ServiceTitle3 { get; set; }
        public string ServiceTitle1v { get; set; }
        public string ServiceTitle2v { get; set; }
        public string ServiceTitle3v { get; set; }
        //ServiceArea
        public string Radius { get; set; }
        public string RadiusKey { get; set; }
        public string ServiceAddress { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }


        //Insurance
        public string InsuranceKey { get; set; }
        public HttpPostedFile Insurancefiles { get; set; }
        public HttpPostedFile licensefiles { get; set; }
        public string FileSize { get; set; }
        public string FileName { get; set; }


        //Insurance
        public string PolicyNumber { get; set; }
        public decimal InsuranceAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime RenewalDate { get; set; }


        //card details


        [Key]
        public Guid PaymentModelID { get; set; }        
        public string NameOfCard {get; set; }
        public string CardNumber {get; set; }
        public string StripeTokenID { get; set; }
        public string PMId { get; set; }
        public string ValidTillMM {get; set; }
        public string FullValidTill {get; set; }
        public string ValidTillYY { get; set; }
        public string CVV {get; set; }


        //Insurance
        [AllowHtml]
        public string BindAgreementDetails { get; set; }
        public int AgreementKey { get; set; }



    }
}
