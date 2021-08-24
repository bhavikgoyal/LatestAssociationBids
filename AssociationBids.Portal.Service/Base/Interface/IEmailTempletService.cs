using AssociationBids.Portal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationBids.Portal.Service.Base.Interface
{
   public interface IEmailTemplateService : IBaseService
    {
       

        //List<EmailTemplateModel> SearchUser(string Search);
        List<EmailTemplateModel> SearchUser(long PageSize, long PageIndex, string Search, string Sort,string EmailTite,string EmailType);
        bool Validate(EmailTemplateModel   item);
        EmailTemplateModel Get(int id);
        IList<EmailTemplateModel> GetAll();
        Int64 Insert(EmailTemplateModel item);
        
        EmailTemplateModel GetDataViewEdit(int EmailTemplateKey);
        IList<LookUpModel> GetAllTitle();
        Int64 EmailTempletEdit(EmailTemplateModel item, int lookuptype);
        Int64 EmailTempletupdates(EmailTemplateModel item);
        bool Remove(int id);
    }
}
