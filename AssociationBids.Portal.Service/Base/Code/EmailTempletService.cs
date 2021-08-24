using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base.Code;
using AssociationBids.Portal.Repository.Base.Interface;
using AssociationBids.Portal.Service.Base.Interface;

namespace AssociationBids.Portal.Service.Base.Code
{

    public class EmailTemplateService : BaseService, IEmailTemplateService
    {
        protected IEmailTemplateRepository __eMailTemplateRepository;

        public EmailTemplateService()
         : this(new EmailTemplateRepository ()) { }

        public EmailTemplateService(string connectionString)
           : this(new EmailTemplateRepository (connectionString)) { }

        public EmailTemplateService(IEmailTemplateRepository eMailTempletRepository)
        {
            ConnectionString = eMailTempletRepository.ConnectionString;

            __eMailTemplateRepository = eMailTempletRepository;
        }

        public long EmailTempletEdit(EmailTemplateModel item, int lookuptype)
        {
            return __eMailTemplateRepository.EmailTempletEdit(item, lookuptype);
        }

        public EmailTemplateModel Get(int id)
        {
            throw new NotImplementedException();
        }

        public IList<EmailTemplateModel> GetAll()
        {
            return __eMailTemplateRepository.GetAll();
        }

        public IList<LookUpModel> GetAllTitle()
        {
            return __eMailTemplateRepository.GetAllLookUp();
        }

        public virtual EmailTemplateModel GetDataViewEdit(int EmailTemplateKey)
        {
            return __eMailTemplateRepository.GetDataViewEdit(EmailTemplateKey);
        }

        public long Insert(EmailTemplateModel item)
        {
            return __eMailTemplateRepository.Insert(item);
        }

        public bool Remove(int id)
        {
            return __eMailTemplateRepository.Remove(id);
        }

        public List<EmailTemplateModel > SearchEmailTittle(string Search, string Sort)
        {
            return __eMailTemplateRepository.SearchEmailTemplatet(Search, Sort);
        }

        public List<EmailTemplateModel> SearchUser(long PageSize, long PageIndex, string Search, string Sort,string EmailTite,string EmailType)
        {
            return __eMailTemplateRepository.SearchUser(PageSize, PageIndex, Search, Sort, EmailTite, EmailType);
        }

        public bool Validate(EmailTemplateModel item)
        {
            throw new NotImplementedException();
        }

        long IEmailTemplateService.EmailTempletupdates(EmailTemplateModel item)
        {
            return __eMailTemplateRepository.EmailTempletupdates(item);
        }
       }
    }

