using System;
using System.Collections.Generic;

namespace AssociationBids.Portal.Model
{
    public class SiteSettingsModel
    {
        public int PortalKey { get; set; }
        public int PortalTypeKey { get; set; }
        public string PortalID { get; set; }
        public string PortalTitle { get; set; }
        public bool AdminPortal { get; set; }
        public string PropertyKeyList { get; set; }

        public int UserKey { get; set; }
        public int ResourceKey { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        
        public string DocumentDir { get; set; }
        public string SiteDir { get; set; }
        public string DocumentUrl { get; set; }
        public string WebsiteUrl { get; set; }
        public string PortalUrl { get; set; }
        public string ReferrerUrl { get; set; }
        
        public int SelectedPortalKey { get; set; }
        public string SelectedPortalID { get; set; }
        public string SelectedPortalTitle { get; set; }
        public int SelectedResourceKey { get; set; }
        
        public int CurrentPortalKey
        {
            get { return (SelectedPortalKey > 0 ? SelectedPortalKey : PortalKey); }
        }
        public int CurrentResourceKey
        {
            get { return (SelectedResourceKey > 0 ? SelectedResourceKey : ResourceKey); }
        }
        public string CurrentPortalID
        {
            get { return (!String.IsNullOrEmpty(SelectedPortalID) ? SelectedPortalID : PortalID); }
        }
        public string CurrentPortalTitle
        {
            get { return (!String.IsNullOrEmpty(SelectedPortalTitle) ? SelectedPortalTitle : PortalTitle); }
        }

        public IDictionary<string, int> LookUpSettings { get; set; }
    }
}

