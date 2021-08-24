using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public class ModuleRepository : BaseRepository, IModuleRepository
    {
        public ModuleRepository() { }

        public ModuleRepository(string connectionString)
            : base(connectionString) { }

        public virtual bool Create(ModuleModel item)
        {
        	bool status = false;

        	try
        	{
        	    string storedProcedure = "gensp_Module_InsertOne";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@title", SqlDbType.VarChar, String.IsNullOrEmpty(item.Title) ? 0 : item.Title.Length, String.IsNullOrEmpty(item.Title) ? SqlString.Null : item.Title);
                        commandWrapper.AddInputParameter("@controller", SqlDbType.VarChar, String.IsNullOrEmpty(item.Controller) ? 0 : item.Controller.Length, String.IsNullOrEmpty(item.Controller) ? SqlString.Null : item.Controller);
                        commandWrapper.AddInputParameter("@action", SqlDbType.VarChar, String.IsNullOrEmpty(item.Action) ? 0 : item.Action.Length, String.IsNullOrEmpty(item.Action) ? SqlString.Null : item.Action);
                        commandWrapper.AddInputParameter("@image", SqlDbType.VarChar, String.IsNullOrEmpty(item.Image) ? 0 : item.Image.Length, String.IsNullOrEmpty(item.Image) ? SqlString.Null : item.Image);
                        commandWrapper.AddInputParameter("@description", SqlDbType.VarChar, String.IsNullOrEmpty(item.Description) ? 0 : item.Description.Length, String.IsNullOrEmpty(item.Description) ? SqlString.Null : item.Description);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@moduleKey", SqlDbType.Int);
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        db.ExecuteNonQuery(commandWrapper);

                        // save the newly inserted key value
                        item.ModuleKey = commandWrapper.GetValueInt("@moduleKey");

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

        public virtual bool Update(ModuleModel item)
        {
        	bool status = false;

        	try
        	{
        	    string storedProcedure = "gensp_Module_UpdateOneByModuleKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@moduleKey", SqlDbType.Int, (item.ModuleKey == 0) ? SqlInt32.Null : item.ModuleKey);
                        commandWrapper.AddInputParameter("@title", SqlDbType.VarChar, String.IsNullOrEmpty(item.Title) ? 0 : item.Title.Length, String.IsNullOrEmpty(item.Title) ? SqlString.Null : item.Title);
                        commandWrapper.AddInputParameter("@controller", SqlDbType.VarChar, String.IsNullOrEmpty(item.Controller) ? 0 : item.Controller.Length, String.IsNullOrEmpty(item.Controller) ? SqlString.Null : item.Controller);
                        commandWrapper.AddInputParameter("@action", SqlDbType.VarChar, String.IsNullOrEmpty(item.Action) ? 0 : item.Action.Length, String.IsNullOrEmpty(item.Action) ? SqlString.Null : item.Action);
                        commandWrapper.AddInputParameter("@image", SqlDbType.VarChar, String.IsNullOrEmpty(item.Image) ? 0 : item.Image.Length, String.IsNullOrEmpty(item.Image) ? SqlString.Null : item.Image);
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
        	    string storedProcedure = "gensp_Module_DeleteOneByModuleKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@moduleKey", SqlDbType.Int, id);

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

        public virtual ModuleModel Get(int id)
        {
            ModuleModel item = null;

            try
            {
                string storedProcedure = "gensp_Module_SelectOneByModuleKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@moduleKey", SqlDbType.Int, id);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            while (dataReader.Read())
                            {
                                item = new ModuleModel();

                                Load(dataReader, item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_Module_SelectOneByModuleKey");
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

        public virtual IList<ModuleModel> GetAll()
        {
            return GetAll(new ModuleFilterModel());
        }

        public virtual IList<ModuleModel> GetAll(ModuleFilterModel filter)
        {
            List<ModuleModel> itemList = new List<ModuleModel>();

            try
            {
                string storedProcedure = "gensp_Module_SelectSomeBySearch";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            ModuleModel item = null;
                            while (dataReader.Read())
                            {
                                item = new ModuleModel();

                                Load(dataReader, item);

                                itemList.Add(item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_Module_SelectSomeBySearch");
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

        public virtual IList<ModuleModel> GetAll(ModuleFilterModel filter, PagingModel paging)
        {
            List<ModuleModel> itemList = new List<ModuleModel>();

            try
            {
                string storedProcedure = "gensp_Module_SelectSomeBySearchAndPaging";
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
                            ModuleModel item = null;
                            while (dataReader.Read())
                            {
                                item = new ModuleModel();

                                Load(dataReader, item);

                                itemList.Add(item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_Module_SelectSomeBySearchAndPaging");
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

        protected void Load(DBDataReader dataReader, ModuleModel item)
        {
            item.ModuleKey = dataReader.GetValueInt("ModuleKey");
            item.Title = dataReader.GetValueText("Title");
            item.Controller = dataReader.GetValueText("Controller");
            item.Action = dataReader.GetValueText("Action");
            item.Image = dataReader.GetValueText("Image");
            item.Description = dataReader.GetValueText("Description");
        }
    }
}
