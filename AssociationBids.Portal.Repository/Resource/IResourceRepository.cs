using AssociationBids.Portal.Model.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationBids.Portal.Repository.Resource
{
    public interface IResourceRepository
    {
        ResourceModel GetUsersDetails(ResourceModel loginModel);
    }
}
