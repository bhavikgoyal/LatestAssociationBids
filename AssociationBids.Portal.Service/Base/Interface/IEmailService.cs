using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Service.Base
{
    public interface IEmailService : IBaseService
    {
        bool Validate(EmailModel item);
        bool IsFilterEnabled(EmailFilterModel filter);
        EmailFilterModel CreateFilter();
        EmailFilterModel CreateFilter(EmailModel item);
        EmailFilterModel UpdateFilter(EmailFilterModel filter);
        EmailModel Create();
        bool Create(EmailModel item);
        bool Update(EmailModel item);
        bool Delete(int id);
        EmailModel Get(int id);
        IList<EmailModel> GetAll();
        IList<EmailModel> GetAll(EmailFilterModel filter);
        IList<EmailModel> GetAll(EmailFilterModel filter, PagingModel paging);
    }
}
