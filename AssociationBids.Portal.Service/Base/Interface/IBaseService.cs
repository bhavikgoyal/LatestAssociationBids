using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Service.Base
{
    public interface IBaseService
    {
        string ConnectionString { get; set; }
        IList<ErrorModel> Errors { get; }
        SiteSettingsModel SiteSettings { get; set; }
        T CreateService<T>() where T : new();
        void AddError(string key, string message);
        void AddErrors(IList<ErrorModel> errors);
        void AddErrors(IList<ErrorModel> errors, string prefix);
    }
}
