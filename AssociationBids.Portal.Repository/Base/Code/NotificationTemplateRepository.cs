using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base.Interface;

namespace AssociationBids.Portal.Repository.Base.Code
{
     public class NotificationTemplateRepository : BaseRepository,INotificationTemplateRepository 
    {
        public NotificationTemplateRepository() { }

        public NotificationTemplateRepository(string connectionString)
          : base(connectionString) { }
        protected void Load(DBDataReader dataReader, NotificationTmpModel item)
        {
            try
            {
                item.PushNotificaionTemplateKey = dataReader.GetValueInt("PushNotificaionTemplateKey");
                item.PushNotificationTitle = dataReader.GetValueText("PushNotificationTitle");
                item.NTSubject = dataReader.GetValueText("NTSubject");
                item.Title = dataReader.GetValueText("Title");
                item.Body = dataReader.GetValueText("Body");
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }
        protected void LoadSearch(DBDataReader dataReader, NotificationTmpModel item)
        {
            try
            {
                item.PushNotificaionTemplateKey = dataReader.GetValueInt("PushNotificaionTemplateKey");
                item.PushNotificationTitle = dataReader.GetValueText("PushNotificationTitle");
                item.NTSubject = dataReader.GetValueText("NTSubject");
                item.Title = dataReader.GetValueText("Title");
                item.Body = dataReader.GetValueText("Body");
                item.TotalRecords = dataReader.GetValueInt("TotalRecord");
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        protected void LoadViewEdit(DBDataReader dataReader, NotificationTmpModel item)
        {
            try
            {
                item.PushNotificaionTemplateKey = dataReader.GetValueInt("PushNotificaionTemplateKey");
                item.PushNotificationTitle = dataReader.GetValueText("PushNotificationTitle");
                item.NTSubject = dataReader.GetValueText("NTSubject");
                item.PushNotificationType = dataReader.GetValueInt("PushNotificationType");
                item.Title = dataReader.GetValueText("Title");
                item.Body = dataReader.GetValueText("Body");
            }
            catch (Exception ex)
            {

               
            }
           
        }

        public bool Remove(int id)
        {
            bool status = false;

            try
            {
                string storedProcedure = "site_PushNotificationTemplate_Delete";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@PushNotificaionTemplateKey", SqlDbType.Int, id);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        db.ExecuteNonQuery(commandWrapper);

                        status = (commandWrapper.GetValueInt("@errorCode") == 0);
                    }
                }
            }
            catch (Exception ex)
            {
                // error occured...

            }

            return status;
        }

        protected void GetAllLookUp(DBDataReader dataReader, LookUpModel item)
        {
            item.LookUpKey = Convert.ToInt16(dataReader.GetValueText("LookUpKey"));
            item.Title = dataReader.GetValueText("Title");
        }

        public virtual IList<NotificationTmpModel> GetAll(NotificationTmpModel filter)
        {
            List<NotificationTmpModel> itemList = new List<NotificationTmpModel>();

            try
            {
                string storedProcedure = "site_Notification_SelectAll";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            NotificationTmpModel item = null;
                            while (dataReader.Read())
                            {
                                item = new NotificationTmpModel();

                                Load(dataReader, item);

                                itemList.Add(item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        //if (commandWrapper.GetValueInt("@errorCode") != 0)
                        //{
                        //    throw new Exception("Error occured in stored procedure: gensp_CompanyVendor_SelectSomeBySearch");
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                // error occured...
                throw;
            }

            return itemList;
        }
     
        List<NotificationTmpModel> INotificationTemplateRepository.SearchUser(long PageSize, long PageIndex, string Search, string Sort)
        {
            List<NotificationTmpModel> itemList = new List<NotificationTmpModel>();
            try
            {

                string storedProcedure = "site__PushNotificationTemplate_SelectIndexPaging";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters 
                        commandWrapper.AddInputParameter("@PageSize", SqlDbType.BigInt, (PageSize == 0) ? 0 : PageSize);
                        commandWrapper.AddInputParameter("@PageIndex", SqlDbType.BigInt, (PageIndex == 0) ? 0 : PageIndex);
                        commandWrapper.AddInputParameter("@Search", SqlDbType.NText, (Search == "") ? "" : Search);
                        commandWrapper.AddInputParameter("@Sort", SqlDbType.NText, (Sort == "") ? "" : Sort);
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            NotificationTmpModel item = null;
                            while (dataReader.Read())
                            {
                                item = new NotificationTmpModel();
                                LoadSearch(dataReader, item);
                                itemList.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                

            }
            return itemList;
        }

        public List<NotificationTmpModel> AdvancedSearchNotificationTemplate(long PageSize, long PageIndex, string Search, string TitleType, string Sort)
        {
            List<NotificationTmpModel> itemList = new List<NotificationTmpModel>();
            try
            {

                string storedProcedure = "site__PushNotificationTemplate_SelectIndexPaging_New";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters 
                        commandWrapper.AddInputParameter("@PageSize", SqlDbType.BigInt, (PageSize == 0) ? 0 : PageSize);
                        commandWrapper.AddInputParameter("@PageIndex", SqlDbType.BigInt, (PageIndex == 0) ? 0 : PageIndex);
                        commandWrapper.AddInputParameter("@Search", SqlDbType.NText, (Search == "") ? "" : Search);
                        commandWrapper.AddInputParameter("@TitleType", SqlDbType.NText, (TitleType == "") ? "" : TitleType);
                        commandWrapper.AddInputParameter("@Sort", SqlDbType.NText, (Sort == "") ? "" : Sort);
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            NotificationTmpModel item = null;
                            while (dataReader.Read())
                            {
                                item = new NotificationTmpModel();
                                LoadSearch(dataReader, item);
                                itemList.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {


            }
            return itemList;
        }


        public virtual NotificationTmpModel GetDataViewEdit(int id)
        {

            NotificationTmpModel item = null;

            try
            {
                string storedProcedure = "site_Notification_SelectOneByNotificationKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@PushNotificaionTemplateKey", SqlDbType.Int, id);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            while (dataReader.Read())
                            {
                                item = new NotificationTmpModel();

                                LoadViewEdit(dataReader, item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return item;
        }

        public long Insert(NotificationTmpModel item)
        {
            Int64 status = 0;
            
            try
            {

                string storedProcedure = "site_PushNotificationTemplate_Insert";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters
                        //commandWrapper.AddInputParameter("@EmailTemplateKey", SqlDbType.Int, (item.EmailTemplateKey == 0) ? SqlInt32.Null : item.EmailTemplateKey);
                        commandWrapper.AddInputParameter("@PushNotificationTitle", SqlDbType.VarChar, string.IsNullOrEmpty(item.PushNotificationTitle) ? "" : item.PushNotificationTitle);
                        commandWrapper.AddInputParameter("@PushNotificationType", SqlDbType.Int, (item.PushNotificationType == 0) ? SqlInt32.Null : item.PushNotificationType);
                        commandWrapper.AddInputParameter("@NTSubject", SqlDbType.VarChar, string.IsNullOrEmpty(item.NTSubject) ? "" : item.NTSubject);
                        commandWrapper.AddInputParameter("@Body", SqlDbType.VarChar, string.IsNullOrEmpty(item.Body) ? "" : item.Body);
                        commandWrapper.AddOutputParameter("@Notificationvalue", SqlDbType.Int);

                        db.ExecuteNonQuery(commandWrapper);
                        status = commandWrapper.GetValueInt("@Notificationvalue");
                    }
                }
            }
            catch (Exception ex)
            {
                // error occured...
                //throw;
            }

            return status;
        }

        
        IList<LookUpModel> INotificationTemplateRepository.GetAllLookUp()
        {
            List<LookUpModel> itemList = new List<LookUpModel>();

            try
            {

                string storedProcedure = "site_lookup_SelectAll";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            LookUpModel item = null;
                            while (dataReader.Read())
                            {
                                item = new LookUpModel();
                                GetAllLookUp(dataReader, item);
                                itemList.Add(item);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                // error occured...
                throw;
            }

            return itemList;
        }

        IList<LookUpModel> INotificationTemplateRepository.GetAllLookUpForNotification()
        {
            List<LookUpModel> itemList = new List<LookUpModel>();

            try
            {

                string storedProcedure = "site_lookup_NotificationSelectAll";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            LookUpModel item = null;
                            while (dataReader.Read())
                            {
                                item = new LookUpModel();
                                GetAllLookUp(dataReader, item);
                                itemList.Add(item);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                // error occured...
                throw;
            }

            return itemList;
        }

        public long NotiFicationTmpEdit(NotificationTmpModel item)
        {
            Int64 status = 0;

            try
            {

                string storedProcedure = "site_PushNotificationTemplate_update";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        commandWrapper.AddInputParameter("@PushNotificaionTemplateKey", SqlDbType.Int, (item.PushNotificaionTemplateKey == 0) ? SqlInt32.Null : item.PushNotificaionTemplateKey);
                        commandWrapper.AddInputParameter("@PushNotificationTitle", SqlDbType.VarChar, string.IsNullOrEmpty(item.PushNotificationTitle) ? "" : item.PushNotificationTitle);
                        commandWrapper.AddInputParameter("@NTSubject", SqlDbType.VarChar, string.IsNullOrEmpty(item.NTSubject) ? "" : item.NTSubject);
                        commandWrapper.AddInputParameter("@Body", SqlDbType.VarChar, string.IsNullOrEmpty(item.Body) ? "" : item.Body);
                        commandWrapper.AddInputParameter("@PushNotificationType", SqlDbType.Int, (item.PushNotificationType == 0) ? SqlInt32.Null : item.PushNotificationType);
                        db.ExecuteNonQuery(commandWrapper);
                        //status = commandWrapper.GetValueInt("@ResourceValue");

                        return status;
                    }
                }
            }
            catch (Exception ex)
            {
                // error occured...
                throw;
            }
        }

        public long NotiFicationTmpupdates(NotificationTmpModel item)
        {
            Int64 status = 0;

            try
            {
                string storedProcedure = "site_PushNotificationTemplate_update";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {


                        commandWrapper.AddInputParameter("@PushNotificaionTemplateKey", SqlDbType.Int, (item.PushNotificaionTemplateKey == 0) ? SqlInt32.Null : item.PushNotificaionTemplateKey);
                        commandWrapper.AddInputParameter("@PushNotificationTitle", SqlDbType.VarChar, String.IsNullOrEmpty(item.PushNotificationTitle) ? 0 : item.PushNotificationTitle.Length, String.IsNullOrEmpty(item.PushNotificationTitle) ? SqlString.Null : item.PushNotificationTitle);
                        commandWrapper.AddInputParameter("@PushNotificationType", SqlDbType.Int, (item.PushNotificationType == 0) ? SqlInt32.Null : item.PushNotificationType);
                        commandWrapper.AddInputParameter("@NTSubject", SqlDbType.VarChar, String.IsNullOrEmpty(item.NTSubject) ? 0 : item.NTSubject.Length, String.IsNullOrEmpty(item.NTSubject) ? SqlString.Null : item.NTSubject);
                        commandWrapper.AddInputParameter("@Body", SqlDbType.VarChar, String.IsNullOrEmpty(item.Body) ? 0 : item.Body.Length, String.IsNullOrEmpty(item.Body) ? SqlString.Null : item.Body);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        db.ExecuteNonQuery(commandWrapper);


                    }
                }
            }
            catch (Exception)
            {
                // error occured...
                throw;
            }

            return status;
        }

        IList<NotificationTmpModel> INotificationTemplateRepository.GetAll()
        {
            return GetAll(new NotificationTmpModel());
        }
    }
}
