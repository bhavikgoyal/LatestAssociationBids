using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public class LookUpRepository : BaseRepository, ILookUpRepository
    {
        public LookUpRepository() { }

        public LookUpRepository(string connectionString)
            : base(connectionString) { }

        public virtual bool Create(LookUpModel item)
        {
        	bool status = false;

        	try
        	{
        	    string storedProcedure = "gensp_LookUp_InsertOne";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@lookUpTypeKey", SqlDbType.Int, (item.LookUpTypeKey == 0) ? SqlInt32.Null : item.LookUpTypeKey);
                        commandWrapper.AddInputParameter("@title", SqlDbType.VarChar, String.IsNullOrEmpty(item.Title) ? 0 : item.Title.Length, String.IsNullOrEmpty(item.Title) ? SqlString.Null : item.Title);
                        commandWrapper.AddInputParameter("@value", SqlDbType.Int, (item.Value == 0) ? SqlInt32.Null : item.Value);
                        commandWrapper.AddInputParameter("@sortOrder", SqlDbType.Float, (item.SortOrder == 0.0) ? SqlDouble.Null : item.SortOrder);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@lookUpKey", SqlDbType.Int);
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        db.ExecuteNonQuery(commandWrapper);

                        // save the newly inserted key value
                        item.LookUpKey = commandWrapper.GetValueInt("@lookUpKey");

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

        public virtual bool Update(LookUpModel item)
        {
        	bool status = false;

        	try
        	{
        	    string storedProcedure = "gensp_LookUp_UpdateOneByLookUpKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@lookUpKey", SqlDbType.Int, (item.LookUpKey == 0) ? SqlInt32.Null : item.LookUpKey);
                        commandWrapper.AddInputParameter("@lookUpTypeKey", SqlDbType.Int, (item.LookUpTypeKey == 0) ? SqlInt32.Null : item.LookUpTypeKey);
                        commandWrapper.AddInputParameter("@title", SqlDbType.VarChar, String.IsNullOrEmpty(item.Title) ? 0 : item.Title.Length, String.IsNullOrEmpty(item.Title) ? SqlString.Null : item.Title);
                        commandWrapper.AddInputParameter("@value", SqlDbType.Int, (item.Value == 0) ? SqlInt32.Null : item.Value);
                        commandWrapper.AddInputParameter("@sortOrder", SqlDbType.Float, (item.SortOrder == 0.0) ? SqlDouble.Null : item.SortOrder);

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
        	    string storedProcedure = "gensp_LookUp_DeleteOneByLookUpKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@lookUpKey", SqlDbType.Int, id);

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

        public virtual LookUpModel Get(int id)
        {
            LookUpModel item = null;

            try
            {
                string storedProcedure = "gensp_LookUp_SelectOneByLookUpKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@lookUpKey", SqlDbType.Int, id);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            while (dataReader.Read())
                            {
                                item = new LookUpModel();

                                Load(dataReader, item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_LookUp_SelectOneByLookUpKey");
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

        public virtual IList<LookUpModel> GetAll()
        {
            return GetAll(new LookUpFilterModel());
        }

        public virtual IList<LookUpModel> GetAll(LookUpFilterModel filter)
        {
            List<LookUpModel> itemList = new List<LookUpModel>();

            try
            {
                string storedProcedure = "gensp_LookUp_SelectSomeBySearch";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@lookUpTypeKey", SqlDbType.Int, (filter.LookUpTypeKey == 0) ? SqlInt32.Null : filter.LookUpTypeKey);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            LookUpModel item = null;
                            while (dataReader.Read())
                            {
                                item = new LookUpModel();

                                Load(dataReader, item);

                                itemList.Add(item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_LookUp_SelectSomeBySearch");
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

        public virtual IList<LookUpModel> GetAll(LookUpFilterModel filter, PagingModel paging)
        {
            List<LookUpModel> itemList = new List<LookUpModel>();

            try
            {
                string storedProcedure = "gensp_LookUp_SelectSomeBySearchAndPaging";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@lookUpTypeKey", SqlDbType.Int, (filter.LookUpTypeKey == 0) ? SqlInt32.Null : filter.LookUpTypeKey);

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
                            LookUpModel item = null;
                            while (dataReader.Read())
                            {
                                item = new LookUpModel();

                                Load(dataReader, item);

                                itemList.Add(item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_LookUp_SelectSomeBySearchAndPaging");
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

        protected void Load(DBDataReader dataReader, LookUpModel item)
        {
            item.LookUpKey = dataReader.GetValueInt("LookUpKey");
            item.LookUpTypeKey = dataReader.GetValueInt("LookUpTypeKey");
            item.Title = dataReader.GetValueText("Title");
            item.Value = dataReader.GetValueInt("Value");
            item.SortOrder = dataReader.GetValueDouble("SortOrder");
        }
    }
}
