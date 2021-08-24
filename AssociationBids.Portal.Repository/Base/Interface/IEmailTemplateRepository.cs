using AssociationBids.Portal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationBids.Portal.Repository.Base.Interface
{
    public interface IEmailTemplateRepository : IBaseRepository
    {
       
        List<EmailTemplateModel> SearchEmailTemplatet( string Search, String Sort);
        List<EmailTemplateModel> SearchUser(Int64 PageSize, Int64 PageIndex, string Search, String Sort,string EmailTite,string EmailType);
        EmailTemplateModel   GetDataViewEdit(int EmailTemplateKey);
        //Int64 Insert(EmailTempletModel  item, string strinbuilder, string strinbuilder1);
        Int64 Insert(EmailTemplateModel  item);
        IList<EmailTemplateModel> GetAll();
        IList<LookUpModel> GetAllLookUp();
        Int64  EmailTempletEdit(EmailTemplateModel item,  int lookuptype);
        Int64 EmailTempletupdates(EmailTemplateModel   item );
        //bool Delete(int id);
        bool Remove(int id);



       
    }
}
