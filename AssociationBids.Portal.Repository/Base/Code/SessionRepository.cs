using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public class SessionRepository : BaseRepository, ISessionRepository
    {
        public SessionRepository() { }

        public SessionRepository(string connectionString)
            : base(connectionString) { }

        public virtual bool Create(SessionModel item)
        {
        	bool status = false;

        	try
        	{
        	    string storedProcedure = "gensp_Session_InsertOne";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@sessionID", SqlDbType.UniqueIdentifier, (item.SessionID == Guid.Empty) ? SqlGuid.Null : item.SessionID);
                        commandWrapper.AddInputParameter("@salt", SqlDbType.VarChar, String.IsNullOrEmpty(item.Salt) ? 0 : item.Salt.Length, String.IsNullOrEmpty(item.Salt) ? SqlString.Null : item.Salt);
                        commandWrapper.AddInputParameter("@data", SqlDbType.VarChar, String.IsNullOrEmpty(item.Data) ? 0 : item.Data.Length, String.IsNullOrEmpty(item.Data) ? SqlString.Null : item.Data);
                        commandWrapper.AddInputParameter("@lastModificationTime", SqlDbType.DateTime, (item.LastModificationTime == DateTime.MinValue) ? SqlDateTime.Null : item.LastModificationTime);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@sessionKey", SqlDbType.Int);
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        db.ExecuteNonQuery(commandWrapper);

                        // save the newly inserted key value
                        item.SessionKey = commandWrapper.GetValueInt("@sessionKey");

                        status = (commandWrapper.GetValueInt("@errorCode") == 0);
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

        public virtual bool Update(SessionModel item)
        {
        	bool status = false;

        	try
        	{
        	    string storedProcedure = "gensp_Session_UpdateOneBySessionKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@sessionKey", SqlDbType.Int, (item.SessionKey == 0) ? SqlInt32.Null : item.SessionKey);
                        commandWrapper.AddInputParameter("@sessionID", SqlDbType.UniqueIdentifier, (item.SessionID == Guid.Empty) ? SqlGuid.Null : item.SessionID);
                        commandWrapper.AddInputParameter("@salt", SqlDbType.VarChar, String.IsNullOrEmpty(item.Salt) ? 0 : item.Salt.Length, String.IsNullOrEmpty(item.Salt) ? SqlString.Null : item.Salt);
                        commandWrapper.AddInputParameter("@data", SqlDbType.VarChar, String.IsNullOrEmpty(item.Data) ? 0 : item.Data.Length, String.IsNullOrEmpty(item.Data) ? SqlString.Null : item.Data);
                        commandWrapper.AddInputParameter("@lastModificationTime", SqlDbType.DateTime, (item.LastModificationTime == DateTime.MinValue) ? SqlDateTime.Null : item.LastModificationTime);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        db.ExecuteNonQuery(commandWrapper);

                        status = (commandWrapper.GetValueInt("@errorCode") == 0);
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

        public virtual bool Delete(int id)
        {
        	bool status = false;

        	try
        	{
        	    string storedProcedure = "gensp_Session_DeleteOneBySessionKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@sessionKey", SqlDbType.Int, id);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        db.ExecuteNonQuery(commandWrapper);

                        status = (commandWrapper.GetValueInt("@errorCode") == 0);
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

        public virtual SessionModel Get(int id)
        {
            SessionModel item = null;

            try
            {
                string storedProcedure = "gensp_Session_SelectOneBySessionKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@sessionKey", SqlDbType.Int, id);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            while (dataReader.Read())
                            {
                                item = new SessionModel();

                                Load(dataReader, item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_Session_SelectOneBySessionKey");
                        }
                    }
                }
            }
            catch (Exception)
            {
                // error occured...
                throw;
            }

            return item;
        }

        public virtual IList<SessionModel> GetAll()
        {
            return GetAll(new SessionFilterModel());
        }

        public virtual IList<SessionModel> GetAll(SessionFilterModel filter)
        {
            List<SessionModel> itemList = new List<SessionModel>();

            try
            {
                string storedProcedure = "gensp_Session_SelectSomeBySearch";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@sessionID", SqlDbType.UniqueIdentifier, (filter.SessionID == Guid.Empty) ? SqlGuid.Null : filter.SessionID);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            SessionModel item = null;
                            while (dataReader.Read())
                            {
                                item = new SessionModel();

                                Load(dataReader, item);

                                itemList.Add(item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_Session_SelectSomeBySearch");
                        }
                    }
                }
            }
            catch (Exception)
            {
                // error occured...
                throw;
            }

            return itemList;
        }

        public virtual IList<SessionModel> GetAll(SessionFilterModel filter, PagingModel paging)
        {
            List<SessionModel> itemList = new List<SessionModel>();

            try
            {
                string storedProcedure = "gensp_Session_SelectSomeBySearchAndPaging";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@sessionID", SqlDbType.UniqueIdentifier, (filter.SessionID == Guid.Empty) ? SqlGuid.Null : filter.SessionID);

                        // add paging parameters
                        commandWrapper.AddInputParameter("@sortOrder", SqlDbType.VarChar, String.IsNullOrEmpty(paging.SortOrder) ? 0 : paging.SortOrder.Length, String.IsNullOrEmpty(paging.SortOrder) ? SqlString.Null : paging.SortOrder);
                        commandWrapper.AddInputParameter("@pageSize", SqlDbType.Int, paging.PageSize);
                        commandWrapper.AddInputParameter("@pageNumber", SqlDbType.Int, paging.PageNumber);

                        // add paging stored procedure output parameters
                        commandWrapper.AddOutputParameter("@totalRecordCount", SqlDbType.Int);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            SessionModel item = null;
                            while (dataReader.Read())
                            {
                                item = new SessionModel();

                                Load(dataReader, item);

                                itemList.Add(item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_Session_SelectSomeBySearchAndPaging");
                        }

                        // set total record count for paging
                        paging.TotalRecordCount = commandWrapper.GetValueInt("@totalRecordCount");
                    }
                }
            }
            catch (Exception)
            {
                // error occured...
                throw;
            }

            return itemList;
        }

        protected void Load(DBDataReader dataReader, SessionModel item)
        {
            item.SessionKey = dataReader.GetValueInt("SessionKey");
            item.SessionID = dataReader.GetValueGUID("SessionID");
            item.Salt = dataReader.GetValueText("Salt");
            item.Data = dataReader.GetValueText("Data");
            item.LastModificationTime = dataReader.GetValueDateTime("LastModificationTime");
        }
    }
}
