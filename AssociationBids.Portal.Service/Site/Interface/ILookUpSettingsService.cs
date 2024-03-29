using System;
using System.Collections.Generic;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Service.Site
{
    public interface ILookUpSettingsService : IBaseService
    {
        IDictionary<string, int> GetAll();
    }
}
