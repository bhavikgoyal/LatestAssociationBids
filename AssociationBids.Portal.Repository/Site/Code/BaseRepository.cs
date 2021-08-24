using System;

namespace AssociationBids.Portal.Repository.Site
{
    public class BaseRepository : AssociationBids.Portal.Repository.Base.BaseRepository, IBaseRepository
    {
        public BaseRepository()
            : base() { }

        public BaseRepository(string connectionString)
            : base(connectionString) { }
    }
}
