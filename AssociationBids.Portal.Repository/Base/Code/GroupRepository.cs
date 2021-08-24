using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public class GroupRepository : BaseRepository, IGroupRepository
    {
        public GroupRepository() { }

        public GroupRepository(string connectionString)
            : base(connectionString) { }

        public virtual bool Create(GroupModel item)
        {
        	bool status = false;

        	try
        	{
        	    string storedProcedure = "gensp_Group_InsertOne";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@title", SqlDbType.VarChar, String.IsNullOrEmpty(item.Title) ? 0 : item.Title.Length, String.IsNullOrEmpty(item.Title) ? SqlString.Null : item.Title);
                        commandWrapper.AddInputParameter("@description", SqlDbType.VarChar, String.IsNullOrEmpty(item.Description) ? 0 : item.Description.Length, String.IsNullOrEmpty(item.Description) ? SqlString.Null : item.Description);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@groupKey", SqlDbType.Int);
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        db.ExecuteNonQuery(commandWrapper);

                        // save the newly inserted key value
                        item.GroupKey = commandWrapper.GetValueInt("@groupKey");

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

        public virtual bool Update(GroupModel item)
        {
        	bool status = false;

        	try
        	{
        	    string storedProcedure = "gensp_Group_UpdateOneByGroupKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@groupKey", SqlDbType.Int, (item.GroupKey == 0) ? SqlInt32.Null : item.GroupKey);
                        commandWrapper.AddInputParameter("@title", SqlDbType.VarChar, String.IsNullOrEmpty(item.Title) ? 0 : item.Title.Length, String.IsNullOrEmpty(item.Title) ? SqlString.Null : item.Title);
                        commandWrapper.AddInputParameter("@description", SqlDbType.VarChar, String.IsNullOrEmpty(item.Description) ? 0 : item.Description.Length, String.IsNullOrEmpty(item.Description) ? SqlString.Null : item.Description);

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
        	    string storedProcedure = "gensp_Group_DeleteOneByGroupKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@groupKey", SqlDbType.Int, id);

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

        public virtual GroupModel Get(int id)
        {
            GroupModel item = null;

            try
            {
                string storedProcedure = "gensp_Group_SelectOneByGroupKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@groupKey", SqlDbType.Int, id);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            while (dataReader.Read())
                            {
                                item = new GroupModel();

                                Load(dataReader, item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_Group_SelectOneByGroupKey");
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

        public virtual IList<GroupModel> GetAll()
        {
            return GetAll(new GroupFilterModel());
        }

        public virtual IList<GroupModel> GetAll(GroupFilterModel filter)
        {
            List<GroupModel> itemList = new List<GroupModel>();

            try
            {
                string storedProcedure = "gensp_Group_SelectSomeBySearch";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            GroupModel item = null;
                            while (dataReader.Read())
                            {
                                item = new GroupModel();

                                Load(dataReader, item);

                                itemList.Add(item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_Group_SelectSomeBySearch");
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

        public virtual IList<GroupModel> GetAll(GroupFilterModel filter, PagingModel paging)
        {
            List<GroupModel> itemList = new List<GroupModel>();

            try
            {
                string storedProcedure = "gensp_Group_SelectSomeBySearchAndPaging";
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
                            GroupModel item = null;
                            while (dataReader.Read())
                            {
                                item = new GroupModel();

                                Load(dataReader, item);

                                itemList.Add(item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_Group_SelectSomeBySearchAndPaging");
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

        protected void Load(DBDataReader dataReader, GroupModel item)
        {
            item.GroupKey = dataReader.GetValueInt("GroupKey");
            item.Title = dataReader.GetValueText("Title");
            item.Description = dataReader.GetValueText("Description");
        }
    }
}
