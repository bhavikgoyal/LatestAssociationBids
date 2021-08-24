using System;
using System.Collections.Generic;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Site;

namespace AssociationBids.Portal.Service.Site
{
    public class LookUpSettingsService : BaseService, ILookUpSettingsService
    {
        protected ILookUpRepository __lookUpRepository;
        protected ILookUpTypeRepository __lookUpTypeRepository;

        public LookUpSettingsService()
        {
            __lookUpRepository = new LookUpRepository();
            __lookUpTypeRepository = new LookUpTypeRepository();
        }

        public LookUpSettingsService(string connectionString)
            : this()
        {
            __lookUpRepository.ConnectionString = connectionString;
            __lookUpTypeRepository.ConnectionString = connectionString;
        }

        public IDictionary<string, int> GetAll()
        {
            IDictionary<string, int> settings = new Dictionary<string, int>();
            LookUpFilterModel lookUpFilter = new LookUpFilterModel();
            string key = "";

            foreach (LookUpTypeModel lookUpType in __lookUpTypeRepository.GetAll())
            {
                // Add LookUpType
                key = "LookUpType." + GetCleanTitle(lookUpType.Title);
                if (!settings.ContainsKey(key))
                {
                    settings.Add(key, lookUpType.LookUpTypeKey);
                }

                lookUpFilter.LookUpTypeKey = lookUpType.LookUpTypeKey;

                foreach (LookUpModel lookUp in __lookUpRepository.GetAll(lookUpFilter))
                {
                    // Add LookUp
                    key = "LookUp." + GetCleanTitle(lookUpType.Title) + "." + GetCleanTitle(lookUp.Title);
                    if (!settings.ContainsKey(key))
                    {
                        settings.Add(key, (lookUp.Value > 0 ? lookUp.Value : lookUp.LookUpKey));
                    }
                }
            }

            return settings;
        }

        private string GetCleanTitle(string key)
        {
            return key.Replace(" ", "").Replace("&", "");
        }
    }
}
