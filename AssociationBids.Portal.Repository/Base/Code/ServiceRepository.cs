using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public class ServiceRepository : BaseRepository, IServiceRepository
    {
        public ServiceRepository() { }

        public ServiceRepository(string connectionString)
            : base(connectionString) { }

        public IList<ServiceModel> GetAll()
        {
            return GetAll(new ServiceModel());
        }
        protected void Load(DBDataReader dataReader, ServiceModel item)
        {
            item.ServiceKey = dataReader.GetValueInt("ServiceKey");
            item.Title = dataReader.GetValueText("Title");
        }
        protected void LoadSearch(DBDataReader dataReader, ServiceModel item)
        {
            item.ServiceKey = dataReader.GetValueInt("ServiceKey");
            item.Title = dataReader.GetValueText("Title");
            item.TotalRecords = dataReader.GetValueInt("TotalRecord");
        }

        protected void LoadViewEdit(DBDataReader dataReader, ServiceModel item)
        {
            item.ServiceKey = dataReader.GetValueInt("ServiceKey");
            item.Title = dataReader.GetValueText("Title");
        }

        public virtual IList<ServiceModel> GetAll(ServiceModel filter)
        {
            List<ServiceModel> itemList = new List<ServiceModel>();

            try
            {
                string storedProcedure = "site_Service_SelectAll";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            ServiceModel item = null;
                            while (dataReader.Read())
                            {
                                item = new ServiceModel();

                                Load(dataReader, item);

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
    

        public ServiceModel GetDataViewEdit(int ServiceKey)
        {
            ServiceModel item = null;

            try
            {
                string storedProcedure = "site_Service_SelectOneByServiceKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@ServiceKey", SqlDbType.Int, ServiceKey);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            while (dataReader.Read())
                            {
                                item = new ServiceModel();

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

        public long Insert(ServiceModel item)
        {
            Int64 status = 0;

            try
            {

                string storedProcedure = "site_Service_Insert";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        commandWrapper.AddInputParameter("@Title", SqlDbType.VarChar, string.IsNullOrEmpty(item.Title) ? "" : item.Title);

                        commandWrapper.AddOutputParameter("@Srvicevalue", SqlDbType.Int);
                        db.ExecuteNonQuery(commandWrapper);
                        status = commandWrapper.GetValueInt("@Srvicevalue");
                    }
                }
            }
            catch (Exception ex)
            {

               
            }

            return status;
        }

        public bool Remove(int id)
        {
            bool status = false;

            try
            {
                string storedProcedure = "site_Sevice_Delete";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@ServiceKey", SqlDbType.Int, id);

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

        public List<ServiceModel> SearchUser(long PageSize, long PageIndex, string Search, string Sort)
        {
            List<ServiceModel> itemList = new List<ServiceModel>();
            try
            {
                string storedProcedure = "site_Service_SelectIndexPaging";
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
                            ServiceModel item = null;
                            while (dataReader.Read())
                            {
                                item = new ServiceModel();
                                LoadSearch(dataReader, item);
                                itemList.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return itemList;
        }

        public long ServiceEdit(ServiceModel item)
        {
            Int64 status = 0;

            try
            {
                string storedProcedure = "site_Sevice_Edit";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        commandWrapper.AddInputParameter("@ServiceKey", SqlDbType.Int, (item.ServiceKey == 0) ? SqlInt32.Null : item.ServiceKey);
                        commandWrapper.AddInputParameter("@Title", SqlDbType.VarChar, string.IsNullOrEmpty(item.Title) ? "" : item.Title);
                        commandWrapper.AddOutputParameter("@Srvicevalue", SqlDbType.Int);
                        db.ExecuteNonQuery(commandWrapper);
                        status = commandWrapper.GetValueInt("@ServiceKey");
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return status;
        }

        public long Serviceupdates(ServiceModel item)
        {
            throw new NotImplementedException();
        }
    }
}
