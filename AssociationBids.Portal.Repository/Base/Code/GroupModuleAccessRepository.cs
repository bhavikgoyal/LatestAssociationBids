using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public class GroupModuleAccessRepository : BaseRepository, IGroupModuleAccessRepository
    {
        public GroupModuleAccessRepository() { }

        public GroupModuleAccessRepository(string connectionString)
            : base(connectionString) { }

        public virtual bool Create(GroupModuleAccessModel item)
        {
        	bool status = false;

        	try
        	{
        	    string storedProcedure = "gensp_GroupModuleAccess_InsertOne";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@portalKey", SqlDbType.Int, (item.PortalKey == 0) ? SqlInt32.Null : item.PortalKey);
                        commandWrapper.AddInputParameter("@groupKey", SqlDbType.Int, (item.GroupKey == 0) ? SqlInt32.Null : item.GroupKey);
                        commandWrapper.AddInputParameter("@moduleKey", SqlDbType.Int, (item.ModuleKey == 0) ? SqlInt32.Null : item.ModuleKey);
                        commandWrapper.AddInputParameter("@access", SqlDbType.Int, (item.Access == 0) ? SqlInt32.Null : item.Access);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@groupModuleAccessKey", SqlDbType.Int);
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        db.ExecuteNonQuery(commandWrapper);

                        // save the newly inserted key value
                        item.GroupModuleAccessKey = commandWrapper.GetValueInt("@groupModuleAccessKey");

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

        public virtual bool Update(GroupModuleAccessModel item)
        {
        	bool status = false;

        	try
        	{
        	    string storedProcedure = "gensp_GroupModuleAccess_UpdateOneByGroupModuleAccessKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@groupModuleAccessKey", SqlDbType.Int, (item.GroupModuleAccessKey == 0) ? SqlInt32.Null : item.GroupModuleAccessKey);
                        commandWrapper.AddInputParameter("@portalKey", SqlDbType.Int, (item.PortalKey == 0) ? SqlInt32.Null : item.PortalKey);
                        commandWrapper.AddInputParameter("@groupKey", SqlDbType.Int, (item.GroupKey == 0) ? SqlInt32.Null : item.GroupKey);
                        commandWrapper.AddInputParameter("@moduleKey", SqlDbType.Int, (item.ModuleKey == 0) ? SqlInt32.Null : item.ModuleKey);
                        commandWrapper.AddInputParameter("@access", SqlDbType.Int, (item.Access == 0) ? SqlInt32.Null : item.Access);

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
        	    string storedProcedure = "gensp_GroupModuleAccess_DeleteOneByGroupModuleAccessKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@groupModuleAccessKey", SqlDbType.Int, id);

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

        public virtual GroupModuleAccessModel Get(int id)
        {
            GroupModuleAccessModel item = null;

            try
            {
                string storedProcedure = "gensp_GroupModuleAccess_SelectOneByGroupModuleAccessKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@groupModuleAccessKey", SqlDbType.Int, id);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            while (dataReader.Read())
                            {
                                item = new GroupModuleAccessModel();

                                Load(dataReader, item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_GroupModuleAccess_SelectOneByGroupModuleAccessKey");
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

        public virtual IList<GroupModuleAccessModel> GetAll()
        {
            return GetAll(new GroupModuleAccessFilterModel());
        }

        public virtual IList<GroupModuleAccessModel> GetAll(GroupModuleAccessFilterModel filter)
        {
            List<GroupModuleAccessModel> itemList = new List<GroupModuleAccessModel>();

            try
            {
                string storedProcedure = "gensp_GroupModuleAccess_SelectSomeBySearch";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@portalKey", SqlDbType.Int, (filter.PortalKey == 0) ? SqlInt32.Null : filter.PortalKey);
                        commandWrapper.AddInputParameter("@propertyKeyList", SqlDbType.VarChar, filter.PropertyKeyList.Length, String.IsNullOrEmpty(filter.PropertyKeyList) ? SqlString.Null : filter.PropertyKeyList);
                        commandWrapper.AddInputParameter("@groupKey", SqlDbType.Int, (filter.GroupKey == 0) ? SqlInt32.Null : filter.GroupKey);
                        commandWrapper.AddInputParameter("@moduleKey", SqlDbType.Int, (filter.ModuleKey == 0) ? SqlInt32.Null : filter.ModuleKey);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            GroupModuleAccessModel item = null;
                            while (dataReader.Read())
                            {
                                item = new GroupModuleAccessModel();

                                Load(dataReader, item);

                                itemList.Add(item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_GroupModuleAccess_SelectSomeBySearch");
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

        public virtual IList<GroupModuleAccessModel> GetAll(GroupModuleAccessFilterModel filter, PagingModel paging)
        {
            List<GroupModuleAccessModel> itemList = new List<GroupModuleAccessModel>();

            try
            {
                string storedProcedure = "gensp_GroupModuleAccess_SelectSomeBySearchAndPaging";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@portalKey", SqlDbType.Int, (filter.PortalKey == 0) ? SqlInt32.Null : filter.PortalKey);
                        commandWrapper.AddInputParameter("@propertyKeyList", SqlDbType.VarChar, filter.PropertyKeyList.Length, String.IsNullOrEmpty(filter.PropertyKeyList) ? SqlString.Null : filter.PropertyKeyList);
                        commandWrapper.AddInputParameter("@groupKey", SqlDbType.Int, (filter.GroupKey == 0) ? SqlInt32.Null : filter.GroupKey);
                        commandWrapper.AddInputParameter("@moduleKey", SqlDbType.Int, (filter.ModuleKey == 0) ? SqlInt32.Null : filter.ModuleKey);

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
                            GroupModuleAccessModel item = null;
                            while (dataReader.Read())
                            {
                                item = new GroupModuleAccessModel();

                                Load(dataReader, item);

                                itemList.Add(item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_GroupModuleAccess_SelectSomeBySearchAndPaging");
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

        protected void Load(DBDataReader dataReader, GroupModuleAccessModel item)
        {
            item.GroupModuleAccessKey = dataReader.GetValueInt("GroupModuleAccessKey");
            item.PortalKey = dataReader.GetValueInt("PortalKey");
            item.GroupKey = dataReader.GetValueInt("GroupKey");
            item.ModuleKey = dataReader.GetValueInt("ModuleKey");
            item.Access = dataReader.GetValueInt("Access");
        }
    }
}
