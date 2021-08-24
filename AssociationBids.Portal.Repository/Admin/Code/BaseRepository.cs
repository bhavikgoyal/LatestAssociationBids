using System;

namespace AssociationBids.Portal.Repository.Admin
{
    public class BaseRepository : AssociationBids.Portal.Repository.Site.BaseRepository, IBaseRepository
    {
        public BaseRepository()
            : base() { }

        public BaseRepository(string connectionString)
            : base(connectionString) { }
    }
}
