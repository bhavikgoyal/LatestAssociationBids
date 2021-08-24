using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public interface IEmailRepository : IBaseRepository
    {
        bool Create(EmailModel item);
        bool Update(EmailModel item);
        bool Delete(int id);
        EmailModel Get(int id);
        IList<EmailModel> GetAll();
        IList<EmailModel> GetAll(EmailFilterModel filter);
        IList<EmailModel> GetAll(EmailFilterModel filter, PagingModel paging);
    }
}
