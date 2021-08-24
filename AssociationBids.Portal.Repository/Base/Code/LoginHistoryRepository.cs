using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public class LoginHistoryRepository : BaseRepository, ILoginHistoryRepository
    {
        public LoginHistoryRepository() { }

        public LoginHistoryRepository(string connectionString)
            : base(connectionString) { }

        public virtual bool Create(LoginHistoryModel item)
        {
        	bool status = false;

        	try
        	{
        	    string storedProcedure = "gensp_LoginHistory_InsertOne";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@userKey", SqlDbType.Int, (item.UserKey == 0) ? SqlInt32.Null : item.UserKey);
                        commandWrapper.AddInputParameter("@dateAdded", SqlDbType.DateTime, (item.DateAdded == DateTime.MinValue) ? SqlDateTime.Null : item.DateAdded);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@loginHistoryKey", SqlDbType.Int);
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        db.ExecuteNonQuery(commandWrapper);

                        // save the newly inserted key value
                        item.LoginHistoryKey = commandWrapper.GetValueInt("@loginHistoryKey");

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

        public virtual bool Update(LoginHistoryModel item)
        {
        	bool status = false;

        	try
        	{
        	    string storedProcedure = "gensp_LoginHistory_UpdateOneByLoginHistoryKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@loginHistoryKey", SqlDbType.Int, (item.LoginHistoryKey == 0) ? SqlInt32.Null : item.LoginHistoryKey);
                        commandWrapper.AddInputParameter("@userKey", SqlDbType.Int, (item.UserKey == 0) ? SqlInt32.Null : item.UserKey);
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
        	    string storedProcedure = "gensp_LoginHistory_DeleteOneByLoginHistoryKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@loginHistoryKey", SqlDbType.Int, id);

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

        public virtual LoginHistoryModel Get(int id)
        {
            LoginHistoryModel item = null;

            try
            {
                string storedProcedure = "gensp_LoginHistory_SelectOneByLoginHistoryKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@loginHistoryKey", SqlDbType.Int, id);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            while (dataReader.Read())
                            {
                                item = new LoginHistoryModel();

                                Load(dataReader, item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_LoginHistory_SelectOneByLoginHistoryKey");
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

        public virtual IList<LoginHistoryModel> GetAll()
        {
            return GetAll(new LoginHistoryFilterModel());
        }

        public virtual IList<LoginHistoryModel> GetAll(LoginHistoryFilterModel filter)
        {
            List<LoginHistoryModel> itemList = new List<LoginHistoryModel>();

            try
            {
                string storedProcedure = "gensp_LoginHistory_SelectSomeBySearch";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@userKey", SqlDbType.Int, (filter.UserKey == 0) ? SqlInt32.Null : filter.UserKey);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            LoginHistoryModel item = null;
                            while (dataReader.Read())
                            {
                                item = new LoginHistoryModel();

                                Load(dataReader, item);

                                itemList.Add(item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_LoginHistory_SelectSomeBySearch");
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

        public virtual IList<LoginHistoryModel> GetAll(LoginHistoryFilterModel filter, PagingModel paging)
        {
            List<LoginHistoryModel> itemList = new List<LoginHistoryModel>();

            try
            {
                string storedProcedure = "gensp_LoginHistory_SelectSomeBySearchAndPaging";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@userKey", SqlDbType.Int, (filter.UserKey == 0) ? SqlInt32.Null : filter.UserKey);

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
                            LoginHistoryModel item = null;
                            while (dataReader.Read())
                            {
                                item = new LoginHistoryModel();

                                Load(dataReader, item);

                                itemList.Add(item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_LoginHistory_SelectSomeBySearchAndPaging");
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

        protected void Load(DBDataReader dataReader, LoginHistoryModel item)
        {
            item.LoginHistoryKey = dataReader.GetValueInt("LoginHistoryKey");
            item.UserKey = dataReader.GetValueInt("UserKey");
            item.DateAdded = dataReader.GetValueDateTime("DateAdded");
        }
    }
}
