using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public class UserAgreementRepository : BaseRepository, IUserAgreementRepository
    {
        public UserAgreementRepository() { }

        public UserAgreementRepository(string connectionString)
            : base(connectionString) { }

        public virtual bool Create(UserAgreementModel item)
        {
        	bool status = false;

        	try
        	{
        	    string storedProcedure = "gensp_UserAgreement_InsertOne";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@userKey", SqlDbType.Int, (item.UserKey == 0) ? SqlInt32.Null : item.UserKey);
                        commandWrapper.AddInputParameter("@agreementKey", SqlDbType.Int, (item.AgreementKey == 0) ? SqlInt32.Null : item.AgreementKey);
                        commandWrapper.AddInputParameter("@dateAdded", SqlDbType.DateTime, (item.DateAdded == DateTime.MinValue) ? SqlDateTime.Null : item.DateAdded);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@userAgreementKey", SqlDbType.Int);
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        db.ExecuteNonQuery(commandWrapper);

                        // save the newly inserted key value
                        item.UserAgreementKey = commandWrapper.GetValueInt("@userAgreementKey");

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

        public virtual bool Update(UserAgreementModel item)
        {
        	bool status = false;

        	try
        	{
        	    string storedProcedure = "gensp_UserAgreement_UpdateOneByUserAgreementKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@userAgreementKey", SqlDbType.Int, (item.UserAgreementKey == 0) ? SqlInt32.Null : item.UserAgreementKey);
                        commandWrapper.AddInputParameter("@userKey", SqlDbType.Int, (item.UserKey == 0) ? SqlInt32.Null : item.UserKey);
                        commandWrapper.AddInputParameter("@agreementKey", SqlDbType.Int, (item.AgreementKey == 0) ? SqlInt32.Null : item.AgreementKey);
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
        	    string storedProcedure = "gensp_UserAgreement_DeleteOneByUserAgreementKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@userAgreementKey", SqlDbType.Int, id);

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

        public virtual UserAgreementModel Get(int id)
        {
            UserAgreementModel item = null;

            try
            {
                string storedProcedure = "gensp_UserAgreement_SelectOneByUserAgreementKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@userAgreementKey", SqlDbType.Int, id);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            while (dataReader.Read())
                            {
                                item = new UserAgreementModel();

                                Load(dataReader, item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_UserAgreement_SelectOneByUserAgreementKey");
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

        public virtual IList<UserAgreementModel> GetAll()
        {
            return GetAll(new UserAgreementFilterModel());
        }

        public virtual IList<UserAgreementModel> GetAll(UserAgreementFilterModel filter)
        {
            List<UserAgreementModel> itemList = new List<UserAgreementModel>();

            try
            {
                string storedProcedure = "gensp_UserAgreement_SelectSomeBySearch";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@userKey", SqlDbType.Int, (filter.UserKey == 0) ? SqlInt32.Null : filter.UserKey);
                        commandWrapper.AddInputParameter("@agreementKey", SqlDbType.Int, (filter.AgreementKey == 0) ? SqlInt32.Null : filter.AgreementKey);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            UserAgreementModel item = null;
                            while (dataReader.Read())
                            {
                                item = new UserAgreementModel();

                                Load(dataReader, item);

                                itemList.Add(item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_UserAgreement_SelectSomeBySearch");
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

        public virtual IList<UserAgreementModel> GetAll(UserAgreementFilterModel filter, PagingModel paging)
        {
            List<UserAgreementModel> itemList = new List<UserAgreementModel>();

            try
            {
                string storedProcedure = "gensp_UserAgreement_SelectSomeBySearchAndPaging";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@userKey", SqlDbType.Int, (filter.UserKey == 0) ? SqlInt32.Null : filter.UserKey);
                        commandWrapper.AddInputParameter("@agreementKey", SqlDbType.Int, (filter.AgreementKey == 0) ? SqlInt32.Null : filter.AgreementKey);

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
                            UserAgreementModel item = null;
                            while (dataReader.Read())
                            {
                                item = new UserAgreementModel();

                                Load(dataReader, item);

                                itemList.Add(item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_UserAgreement_SelectSomeBySearchAndPaging");
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

        protected void Load(DBDataReader dataReader, UserAgreementModel item)
        {
            item.UserAgreementKey = dataReader.GetValueInt("UserAgreementKey");
            item.UserKey = dataReader.GetValueInt("UserKey");
            item.AgreementKey = dataReader.GetValueInt("AgreementKey");
            item.DateAdded = dataReader.GetValueDateTime("DateAdded");
        }
    }
}
