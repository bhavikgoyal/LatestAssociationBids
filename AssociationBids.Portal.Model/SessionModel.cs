using System;

namespace AssociationBids.Portal.Model
{
    public class SessionModel : BaseModel
    {
        public int SessionKey { get; set; }
        public Guid SessionID { get; set; }
        public string Salt { get; set; }
        public string Data { get; set; }
        public DateTime LastModificationTime { get; set; }

        public bool IsDirty { get; set; }
        public int CompanyKey { get; set; }
        public int ResourceKey { get; set; }
        public int UserKey { get; set; }
        public int SelectedCompanyKey { get; set; }
        public int SelectedResourceKey { get; set; }
        public string ViewExtension { get; set; }

        public SessionDataModel SessionData { get; set; }

        public SessionModel()
        {
            SessionData = new SessionDataModel();
        }
    }

    public class SessionDataModel : IEquatable<SessionDataModel>
    {
        public int CompanyKey { get; set; }
        public int ResourceKey { get; set; }
        public int UserKey { get; set; }
        public int SelectedCompanyKey { get; set; }
        public int SelectedResourceKey { get; set; }
        public string ViewExtension { get; set; }

        public bool Equals(SessionDataModel item)
        {
            return (item.CompanyKey.Equals(CompanyKey) &&
                item.ResourceKey.Equals(ResourceKey) &&
                item.UserKey.Equals(UserKey) &&
                item.SelectedCompanyKey.Equals(SelectedCompanyKey) &&
                item.SelectedResourceKey.Equals(SelectedResourceKey) &&
                item.ViewExtension.Equals(ViewExtension));
        }

        public SessionDataModel ShallowCopy()
        {
            return (SessionDataModel)this.MemberwiseClone();
        }
    }
}
