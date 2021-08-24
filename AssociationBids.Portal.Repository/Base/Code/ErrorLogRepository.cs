using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public class ErrorLogRepository : BaseRepository, IErrorLogRepository
    {
        public ErrorLogRepository() { }

        public ErrorLogRepository(string connectionString)
            : base(connectionString) { }

        public virtual bool Create(ErrorLogModel item)
        {
        	bool status = false;

        	try
        	{
        	    string storedProcedure = "gensp_ErrorLog_InsertOne";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@details", SqlDbType.VarChar, String.IsNullOrEmpty(item.Details) ? 0 : item.Details.Length, String.IsNullOrEmpty(item.Details) ? SqlString.Null : item.Details);
                        commandWrapper.AddInputParameter("@session", SqlDbType.VarChar, String.IsNullOrEmpty(item.Session) ? 0 : item.Session.Length, String.IsNullOrEmpty(item.Session) ? SqlString.Null : item.Session);
                        commandWrapper.AddInputParameter("@dateAdded", SqlDbType.DateTime, (item.DateAdded == DateTime.MinValue) ? SqlDateTime.Null : item.DateAdded);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorLogKey", SqlDbType.Int);
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        db.ExecuteNonQuery(commandWrapper);

                        // save the newly inserted key value
                        item.ErrorLogKey = commandWrapper.GetValueInt("@errorLogKey");

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

        public virtual bool Update(ErrorLogModel item)
        {
        	bool status = false;

        	try
        	{
        	    string storedProcedure = "gensp_ErrorLog_UpdateOneByErrorLogKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@errorLogKey", SqlDbType.Int, (item.ErrorLogKey == 0) ? SqlInt32.Null : item.ErrorLogKey);
                        commandWrapper.AddInputParameter("@details", SqlDbType.VarChar, String.IsNullOrEmpty(item.Details) ? 0 : item.Details.Length, String.IsNullOrEmpty(item.Details) ? SqlString.Null : item.Details);
                        commandWrapper.AddInputParameter("@session", SqlDbType.VarChar, String.IsNullOrEmpty(item.Session) ? 0 : item.Session.Length, String.IsNullOrEmpty(item.Session) ? SqlString.Null : item.Session);
                        commandWrapper.AddInputParameter("@dateAdded", SqlDbType.DateTime, (item.DateAdded == DateTime.MinValue) ? SqlDateTime.Null : item.DateAdded);

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
        	    string storedProcedure = "gensp_ErrorLog_DeleteOneByErrorLogKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@errorLogKey", SqlDbType.Int, id);

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

        public virtual ErrorLogModel Get(int id)
        {
            ErrorLogModel item = null;

            try
            {
                string storedProcedure = "gensp_ErrorLog_SelectOneByErrorLogKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@errorLogKey", SqlDbType.Int, id);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            while (dataReader.Read())
                            {
                                item = new ErrorLogModel();

                                Load(dataReader, item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_ErrorLog_SelectOneByErrorLogKey");
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

        public virtual IList<ErrorLogModel> GetAll()
        {
            return GetAll(new ErrorLogFilterModel());
        }

        public virtual IList<ErrorLogModel> GetAll(ErrorLogFilterModel filter)
        {
            List<ErrorLogModel> itemList = new List<ErrorLogModel>();

            try
            {
                string storedProcedure = "gensp_ErrorLog_SelectSomeBySearch";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            ErrorLogModel item = null;
                            while (dataReader.Read())
                            {
                                item = new ErrorLogModel();

                                Load(dataReader, item);

                                itemList.Add(item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_ErrorLog_SelectSomeBySearch");
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

        public virtual IList<ErrorLogModel> GetAll(ErrorLogFilterModel filter, PagingModel paging)
        {
            List<ErrorLogModel> itemList = new List<ErrorLogModel>();

            try
            {
                string storedProcedure = "gensp_ErrorLog_SelectSomeBySearchAndPaging";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters

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
                            ErrorLogModel item = null;
                            while (dataReader.Read())
                            {
                                item = new ErrorLogModel();

                                Load(dataReader, item);

                                itemList.Add(item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_ErrorLog_SelectSomeBySearchAndPaging");
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

        protected void Load(DBDataReader dataReader, ErrorLogModel item)
        {
            item.ErrorLogKey = dataReader.GetValueInt("ErrorLogKey");
            item.Details = dataReader.GetValueText("Details");
            item.Session = dataReader.GetValueText("Session");
            item.DateAdded = dataReader.GetValueDateTime("DateAdded");
        }
    }
}
