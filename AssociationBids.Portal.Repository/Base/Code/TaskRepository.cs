using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public class TaskRepository : BaseRepository, ITaskRepository
    {
        public TaskRepository() { }

        public TaskRepository(string connectionString)
            : base(connectionString) { }

        public virtual bool Create(TaskModel item)
        {
        	bool status = false;

        	try
        	{
        	    string storedProcedure = "gensp_Task_InsertOne";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@moduleKey", SqlDbType.Int, (item.ModuleKey == 0) ? SqlInt32.Null : item.ModuleKey);
                        commandWrapper.AddInputParameter("@resourceKey", SqlDbType.Int, (item.ResourceKey == 0) ? SqlInt32.Null : item.ResourceKey);
                        commandWrapper.AddInputParameter("@assignedToKey", SqlDbType.Int, (item.AssignedToKey == 0) ? SqlInt32.Null : item.AssignedToKey);
                        commandWrapper.AddInputParameter("@objectKey", SqlDbType.Int, (item.ObjectKey == 0) ? SqlInt32.Null : item.ObjectKey);
                        commandWrapper.AddInputParameter("@subject", SqlDbType.VarChar, String.IsNullOrEmpty(item.Subject) ? 0 : item.Subject.Length, String.IsNullOrEmpty(item.Subject) ? SqlString.Null : item.Subject);
                        commandWrapper.AddInputParameter("@taskStatus", SqlDbType.Int, (item.TaskStatus == 0) ? SqlInt32.Null : item.TaskStatus);
                        commandWrapper.AddInputParameter("@taskPriority", SqlDbType.Int, (item.TaskPriority == 0) ? SqlInt32.Null : item.TaskPriority);
                        commandWrapper.AddInputParameter("@dueDate", SqlDbType.SmallDateTime, (item.DueDate == DateTime.MinValue) ? SqlDateTime.Null : item.DueDate);
                        commandWrapper.AddInputParameter("@startDate", SqlDbType.SmallDateTime, (item.StartDate == DateTime.MinValue) ? SqlDateTime.Null : item.StartDate);
                        commandWrapper.AddInputParameter("@description", SqlDbType.VarChar, String.IsNullOrEmpty(item.Description) ? 0 : item.Description.Length, String.IsNullOrEmpty(item.Description) ? SqlString.Null : item.Description);
                        commandWrapper.AddInputParameter("@dateAdded", SqlDbType.DateTime, (item.DateAdded == DateTime.MinValue) ? SqlDateTime.Null : item.DateAdded);
                        commandWrapper.AddInputParameter("@dateCompleted", SqlDbType.DateTime, (item.DateCompleted == DateTime.MinValue) ? SqlDateTime.Null : item.DateCompleted);
                        commandWrapper.AddInputParameter("@lastModificationTime", SqlDbType.DateTime, (item.LastModificationTime == DateTime.MinValue) ? SqlDateTime.Null : item.LastModificationTime);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@taskKey", SqlDbType.Int);
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        db.ExecuteNonQuery(commandWrapper);

                        // save the newly inserted key value
                        item.TaskKey = commandWrapper.GetValueInt("@taskKey");

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

        public virtual bool Update(TaskModel item)
        {
        	bool status = false;

        	try
        	{
        	    string storedProcedure = "gensp_Task_UpdateOneByTaskKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@taskKey", SqlDbType.Int, (item.TaskKey == 0) ? SqlInt32.Null : item.TaskKey);
                        commandWrapper.AddInputParameter("@moduleKey", SqlDbType.Int, (item.ModuleKey == 0) ? SqlInt32.Null : item.ModuleKey);
                        commandWrapper.AddInputParameter("@resourceKey", SqlDbType.Int, (item.ResourceKey == 0) ? SqlInt32.Null : item.ResourceKey);
                        commandWrapper.AddInputParameter("@assignedToKey", SqlDbType.Int, (item.AssignedToKey == 0) ? SqlInt32.Null : item.AssignedToKey);
                        commandWrapper.AddInputParameter("@objectKey", SqlDbType.Int, (item.ObjectKey == 0) ? SqlInt32.Null : item.ObjectKey);
                        commandWrapper.AddInputParameter("@subject", SqlDbType.VarChar, String.IsNullOrEmpty(item.Subject) ? 0 : item.Subject.Length, String.IsNullOrEmpty(item.Subject) ? SqlString.Null : item.Subject);
                        commandWrapper.AddInputParameter("@taskStatus", SqlDbType.Int, (item.TaskStatus == 0) ? SqlInt32.Null : item.TaskStatus);
                        commandWrapper.AddInputParameter("@taskPriority", SqlDbType.Int, (item.TaskPriority == 0) ? SqlInt32.Null : item.TaskPriority);
                        commandWrapper.AddInputParameter("@dueDate", SqlDbType.SmallDateTime, (item.DueDate == DateTime.MinValue) ? SqlDateTime.Null : item.DueDate);
                        commandWrapper.AddInputParameter("@startDate", SqlDbType.SmallDateTime, (item.StartDate == DateTime.MinValue) ? SqlDateTime.Null : item.StartDate);
                        commandWrapper.AddInputParameter("@description", SqlDbType.VarChar, String.IsNullOrEmpty(item.Description) ? 0 : item.Description.Length, String.IsNullOrEmpty(item.Description) ? SqlString.Null : item.Description);
                        commandWrapper.AddInputParameter("@dateAdded", SqlDbType.DateTime, (item.DateAdded == DateTime.MinValue) ? SqlDateTime.Null : item.DateAdded);
                        commandWrapper.AddInputParameter("@dateCompleted", SqlDbType.DateTime, (item.DateCompleted == DateTime.MinValue) ? SqlDateTime.Null : item.DateCompleted);
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
        	    string storedProcedure = "gensp_Task_DeleteOneByTaskKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@taskKey", SqlDbType.Int, id);

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

        public virtual TaskModel Get(int id)
        {
            TaskModel item = null;

            try
            {
                string storedProcedure = "gensp_Task_SelectOneByTaskKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@taskKey", SqlDbType.Int, id);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            while (dataReader.Read())
                            {
                                item = new TaskModel();

                                Load(dataReader, item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_Task_SelectOneByTaskKey");
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

        public virtual IList<TaskModel> GetAll()
        {
            return GetAll(new TaskFilterModel());
        }

        public virtual IList<TaskModel> GetAll(TaskFilterModel filter)
        {
            List<TaskModel> itemList = new List<TaskModel>();

            try
            {
                string storedProcedure = "gensp_Task_SelectSomeBySearch";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@moduleKey", SqlDbType.Int, (filter.ModuleKey == 0) ? SqlInt32.Null : filter.ModuleKey);
                        commandWrapper.AddInputParameter("@resourceKey", SqlDbType.Int, (filter.ResourceKey == 0) ? SqlInt32.Null : filter.ResourceKey);
                        commandWrapper.AddInputParameter("@assignedToKey", SqlDbType.Int, (filter.AssignedToKey == 0) ? SqlInt32.Null : filter.AssignedToKey);
                        commandWrapper.AddInputParameter("@objectKey", SqlDbType.Int, (filter.ObjectKey == 0) ? SqlInt32.Null : filter.ObjectKey);
                        commandWrapper.AddInputParameter("@taskStatus", SqlDbType.Int, (filter.TaskStatus == 0) ? SqlInt32.Null : filter.TaskStatus);
                        commandWrapper.AddInputParameter("@taskPriority", SqlDbType.Int, (filter.TaskPriority == 0) ? SqlInt32.Null : filter.TaskPriority);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            TaskModel item = null;
                            while (dataReader.Read())
                            {
                                item = new TaskModel();

                                Load(dataReader, item);

                                itemList.Add(item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_Task_SelectSomeBySearch");
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

        public virtual IList<TaskModel> GetAll(TaskFilterModel filter, PagingModel paging)
        {
            List<TaskModel> itemList = new List<TaskModel>();

            try
            {
                string storedProcedure = "gensp_Task_SelectSomeBySearchAndPaging";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@moduleKey", SqlDbType.Int, (filter.ModuleKey == 0) ? SqlInt32.Null : filter.ModuleKey);
                        commandWrapper.AddInputParameter("@resourceKey", SqlDbType.Int, (filter.ResourceKey == 0) ? SqlInt32.Null : filter.ResourceKey);
                        commandWrapper.AddInputParameter("@assignedToKey", SqlDbType.Int, (filter.AssignedToKey == 0) ? SqlInt32.Null : filter.AssignedToKey);
                        commandWrapper.AddInputParameter("@objectKey", SqlDbType.Int, (filter.ObjectKey == 0) ? SqlInt32.Null : filter.ObjectKey);
                        commandWrapper.AddInputParameter("@taskStatus", SqlDbType.Int, (filter.TaskStatus == 0) ? SqlInt32.Null : filter.TaskStatus);
                        commandWrapper.AddInputParameter("@taskPriority", SqlDbType.Int, (filter.TaskPriority == 0) ? SqlInt32.Null : filter.TaskPriority);

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
                            TaskModel item = null;
                            while (dataReader.Read())
                            {
                                item = new TaskModel();

                                Load(dataReader, item);

                                itemList.Add(item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_Task_SelectSomeBySearchAndPaging");
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

        protected void Load(DBDataReader dataReader, TaskModel item)
        {
            item.TaskKey = dataReader.GetValueInt("TaskKey");
            item.ModuleKey = dataReader.GetValueInt("ModuleKey");
            item.ResourceKey = dataReader.GetValueInt("ResourceKey");
            item.AssignedToKey = dataReader.GetValueInt("AssignedToKey");
            item.ObjectKey = dataReader.GetValueInt("ObjectKey");
            item.Subject = dataReader.GetValueText("Subject");
            item.TaskStatus = dataReader.GetValueInt("TaskStatus");
            item.TaskPriority = dataReader.GetValueInt("TaskPriority");
            item.DueDate = dataReader.GetValueDateTime("DueDate");
            item.StartDate = dataReader.GetValueDateTime("StartDate");
            item.Description = dataReader.GetValueText("Description");
            item.DateAdded = dataReader.GetValueDateTime("DateAdded");
            item.DateCompleted = dataReader.GetValueDateTime("DateCompleted");
            item.LastModificationTime = dataReader.GetValueDateTime("LastModificationTime");
        }
    }
}
