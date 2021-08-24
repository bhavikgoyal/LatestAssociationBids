using System;

namespace AssociationBids.Portal.Repository.Base
{
    public class BaseRepository : IBaseRepository
    {
        public BaseRepository()
        {
            ConnectionString = "DefaultConnection";
        }

        public BaseRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public string ConnectionString { get; set; }

        public string GetFilterValue(string data)
        {
            if (String.IsNullOrEmpty(data)) 
            {
                return "";
            }
            else
            {
                return String.Format("%{0}%", data);
            }
        }

        public string GetFullName(string firstName, string lastName)
        {
            return String.Format("{0} {1}", firstName, lastName).Trim();
        }
    }
}
