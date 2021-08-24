using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Site;

namespace AssociationBids.Portal.Service.Site
{
    public class SessionService : AssociationBids.Portal.Service.Base.SessionService, ISessionService
    {
        new protected ISessionRepository __repository;

        public SessionService()
            : this(new SessionRepository()) { }

        public SessionService(string connectionString)
            : this(new SessionRepository(connectionString)) { }

        public SessionService(ISessionRepository repository)
            : base(repository)
        {
            __repository = repository;
        }

/// <summary>
/// TODO: Get session by SessionID
/// </summary>
/// <param name="id"></param>
/// <returns></returns>
        public virtual SessionModel Get(Guid id)
        {
            //SessionModel item = __repository.Get(id);

            //Load(item);
            //return item;

            return new SessionModel();
        }

        public override bool Create(SessionModel item)
        {
            item.LastModificationTime = DateTime.Now;

            if (Validate(item))
            {
                return __repository.Create(item);
            }
            else
            {
                return false;
            }
        }

        public override bool Update(SessionModel item)
        {
            item.LastModificationTime = DateTime.Now;
            
            if (Validate(item))
            {
                item.Data = SerializeObject(item.SessionData);
                return __repository.Update(item);
            }
            else
            {
                return false;
            }
        }

        #region Serialize Object
        public string SerializeObject(SessionDataModel item)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(item.GetType());

            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, item);
                return textWriter.ToString();
            }
        }

        public SessionDataModel DeSerializeObject(string data)
        {
            SessionDataModel item = null;
            XmlSerializer xmlSerializer = new XmlSerializer(item.GetType());

            using (TextReader reader = new StringReader(data))
            {
                item = (SessionDataModel)xmlSerializer.Deserialize(reader);
            }

            return item;
        }
        #endregion
    }
}
