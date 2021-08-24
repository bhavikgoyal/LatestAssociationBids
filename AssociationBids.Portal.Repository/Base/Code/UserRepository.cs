using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository() { }

        public UserRepository(string connectionString)
            : base(connectionString) { }

        public virtual bool Create(UserModel item)
        {
        	bool status = false;

        	try
        	{
        	    string storedProcedure = "gensp_User_InsertOne";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@resourceKey", SqlDbType.Int, (item.ResourceKey == 0) ? SqlInt32.Null : item.ResourceKey);
                        commandWrapper.AddInputParameter("@username", SqlDbType.VarChar, String.IsNullOrEmpty(item.Username) ? 0 : item.Username.Length, String.IsNullOrEmpty(item.Username) ? SqlString.Null : item.Username);
                        commandWrapper.AddInputParameter("@password", SqlDbType.VarChar, String.IsNullOrEmpty(item.Password) ? 0 : item.Password.Length, String.IsNullOrEmpty(item.Password) ? SqlString.Null : item.Password);
                        commandWrapper.AddInputParameter("@tokenReset", SqlDbType.VarChar, String.IsNullOrEmpty(item.TokenReset) ? 0 : item.TokenReset.Length, String.IsNullOrEmpty(item.TokenReset) ? SqlString.Null : item.TokenReset);
                        commandWrapper.AddInputParameter("@resetExpirationDate", SqlDbType.DateTime, (item.ResetExpirationDate == DateTime.MinValue) ? SqlDateTime.Null : item.ResetExpirationDate);
                        commandWrapper.AddInputParameter("@accountLocked", SqlDbType.Bit, (item.AccountLocked == false) ? SqlBoolean.Null : item.AccountLocked);
                        commandWrapper.AddInputParameter("@firstTimeAccess", SqlDbType.Bit, (item.FirstTimeAccess == false) ? SqlBoolean.Null : item.FirstTimeAccess);
                        commandWrapper.AddInputParameter("@dateAdded", SqlDbType.DateTime, (item.DateAdded == DateTime.MinValue) ? SqlDateTime.Null : item.DateAdded);
                        commandWrapper.AddInputParameter("@lastModificationTime", SqlDbType.DateTime, (item.LastModificationTime == DateTime.MinValue) ? SqlDateTime.Null : item.LastModificationTime);
                        commandWrapper.AddInputParameter("@status", SqlDbType.Int, (item.Status == 0) ? SqlInt32.Null : item.Status);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@userKey", SqlDbType.Int);
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        db.ExecuteNonQuery(commandWrapper);

                        // save the newly inserted key value
                        item.UserKey = commandWrapper.GetValueInt("@userKey");

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

        public virtual bool Update(UserModel item)
        {
        	bool status = false;

        	try
        	{
        	    string storedProcedure = "gensp_User_UpdateOneByUserKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@userKey", SqlDbType.Int, (item.UserKey == 0) ? SqlInt32.Null : item.UserKey);
                        commandWrapper.AddInputParameter("@resourceKey", SqlDbType.Int, (item.ResourceKey == 0) ? SqlInt32.Null : item.ResourceKey);
                        commandWrapper.AddInputParameter("@username", SqlDbType.VarChar, String.IsNullOrEmpty(item.Username) ? 0 : item.Username.Length, String.IsNullOrEmpty(item.Username) ? SqlString.Null : item.Username);
                        commandWrapper.AddInputParameter("@password", SqlDbType.VarChar, String.IsNullOrEmpty(item.Password) ? 0 : item.Password.Length, String.IsNullOrEmpty(item.Password) ? SqlString.Null : item.Password);
                        commandWrapper.AddInputParameter("@tokenReset", SqlDbType.VarChar, String.IsNullOrEmpty(item.TokenReset) ? 0 : item.TokenReset.Length, String.IsNullOrEmpty(item.TokenReset) ? SqlString.Null : item.TokenReset);
                        commandWrapper.AddInputParameter("@resetExpirationDate", SqlDbType.DateTime, (item.ResetExpirationDate == DateTime.MinValue) ? SqlDateTime.Null : item.ResetExpirationDate);
                        commandWrapper.AddInputParameter("@accountLocked", SqlDbType.Bit, (item.AccountLocked == false) ? SqlBoolean.Null : item.AccountLocked);
                        commandWrapper.AddInputParameter("@firstTimeAccess", SqlDbType.Bit, (item.FirstTimeAccess == false) ? SqlBoolean.Null : item.FirstTimeAccess);
                        commandWrapper.AddInputParameter("@dateAdded", SqlDbType.DateTime, (item.DateAdded == DateTime.MinValue) ? SqlDateTime.Null : item.DateAdded);
                        commandWrapper.AddInputParameter("@lastModificationTime", SqlDbType.DateTime, (item.LastModificationTime == DateTime.MinValue) ? SqlDateTime.Null : item.LastModificationTime);
                        commandWrapper.AddInputParameter("@status", SqlDbType.Int, (item.Status == 0) ? SqlInt32.Null : item.Status);

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
        	    string storedProcedure = "gensp_User_DeleteOneByUserKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@userKey", SqlDbType.Int, id);

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

        public virtual UserModel Get(int id)
        {
            UserModel item = null;

            try
            {
                string storedProcedure = "gensp_User_SelectOneByUserKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@userKey", SqlDbType.Int, id);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            while (dataReader.Read())
                            {
                                item = new UserModel();

                                Load(dataReader, item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_User_SelectOneByUserKey");
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

        public virtual IList<UserModel> GetAll()
        {
            return GetAll(new UserFilterModel());
        }

        public virtual IList<UserModel> GetAll(UserFilterModel filter)
        {
            List<UserModel> itemList = new List<UserModel>();

            try
            {
                string storedProcedure = "gensp_User_SelectSomeBySearch";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@resourceKey", SqlDbType.Int, (filter.ResourceKey == 0) ? SqlInt32.Null : filter.ResourceKey);
                        commandWrapper.AddInputParameter("@username", SqlDbType.VarChar, GetFilterValue(filter.Username).Length, String.IsNullOrEmpty(filter.Username) ? SqlString.Null : GetFilterValue(filter.Username));
                        commandWrapper.AddInputParameter("@tokenReset", SqlDbType.VarChar, GetFilterValue(filter.TokenReset).Length, String.IsNullOrEmpty(filter.TokenReset) ? SqlString.Null : GetFilterValue(filter.TokenReset));
                        commandWrapper.AddInputParameter("@status", SqlDbType.Int, (filter.Status == 0) ? SqlInt32.Null : filter.Status);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            UserModel item = null;
                            while (dataReader.Read())
                            {
                                item = new UserModel();

                                Load(dataReader, item);

                                itemList.Add(item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_User_SelectSomeBySearch");
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

        public virtual IList<UserModel> GetAll(UserFilterModel filter, PagingModel paging)
        {
            List<UserModel> itemList = new List<UserModel>();

            try
            {
                string storedProcedure = "gensp_User_SelectSomeBySearchAndPaging";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@resourceKey", SqlDbType.Int, (filter.ResourceKey == 0) ? SqlInt32.Null : filter.ResourceKey);
                        commandWrapper.AddInputParameter("@username", SqlDbType.VarChar, GetFilterValue(filter.Username).Length, String.IsNullOrEmpty(filter.Username) ? SqlString.Null : GetFilterValue(filter.Username));
                        commandWrapper.AddInputParameter("@tokenReset", SqlDbType.VarChar, GetFilterValue(filter.TokenReset).Length, String.IsNullOrEmpty(filter.TokenReset) ? SqlString.Null : GetFilterValue(filter.TokenReset));
                        commandWrapper.AddInputParameter("@status", SqlDbType.Int, (filter.Status == 0) ? SqlInt32.Null : filter.Status);

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
                            UserModel item = null;
                            while (dataReader.Read())
                            {
                                item = new UserModel();

                                Load(dataReader, item);

                                itemList.Add(item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_User_SelectSomeBySearchAndPaging");
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

        protected void Load(DBDataReader dataReader, UserModel item)
        {
            item.UserKey = dataReader.GetValueInt("UserKey");
            item.ResourceKey = dataReader.GetValueInt("ResourceKey");
            item.Username = dataReader.GetValueText("Username");
            item.Password = dataReader.GetValueText("Password");
            item.TokenReset = dataReader.GetValueText("TokenReset");
            item.ResetExpirationDate = dataReader.GetValueDateTime("ResetExpirationDate");
            item.AccountLocked = dataReader.GetValueBool("AccountLocked");
            item.FirstTimeAccess = dataReader.GetValueBool("FirstTimeAccess");
            item.DateAdded = dataReader.GetValueDateTime("DateAdded");
            item.LastModificationTime = dataReader.GetValueDateTime("LastModificationTime");
            item.Status = dataReader.GetValueInt("Status");
        }
    }
}
