using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public class ReminderRepository : BaseRepository, IReminderRepository
    {
        public ReminderRepository() { }

        public ReminderRepository(string connectionString)
            : base(connectionString) { }

        public virtual bool Create(ReminderModel item)
        {
        	bool status = false;

        	try
        	{
        	    string storedProcedure = "gensp_Reminder_InsertOne";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@moduleKey", SqlDbType.Int, (item.ModuleKey == 0) ? SqlInt32.Null : item.ModuleKey);
                        commandWrapper.AddInputParameter("@resourceKey", SqlDbType.Int, (item.ResourceKey == 0) ? SqlInt32.Null : item.ResourceKey);
                        commandWrapper.AddInputParameter("@objectKey", SqlDbType.Int, (item.ObjectKey == 0) ? SqlInt32.Null : item.ObjectKey);
                        commandWrapper.AddInputParameter("@description", SqlDbType.VarChar, String.IsNullOrEmpty(item.Description) ? 0 : item.Description.Length, String.IsNullOrEmpty(item.Description) ? SqlString.Null : item.Description);
                        commandWrapper.AddInputParameter("@reminderDate", SqlDbType.SmallDateTime, (item.ReminderDate == DateTime.MinValue) ? SqlDateTime.Null : item.ReminderDate);
                        commandWrapper.AddInputParameter("@lastModificationTime", SqlDbType.DateTime, (item.LastModificationTime == DateTime.MinValue) ? SqlDateTime.Null : item.LastModificationTime);
                        commandWrapper.AddInputParameter("@status", SqlDbType.Int, (item.Status == 0) ? SqlInt32.Null : item.Status);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@reminderKey", SqlDbType.Int);
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        db.ExecuteNonQuery(commandWrapper);

                        // save the newly inserted key value
                        item.ReminderKey = commandWrapper.GetValueInt("@reminderKey");

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

        public virtual bool Update(ReminderModel item)
        {
        	bool status = false;

        	try
        	{
        	    string storedProcedure = "gensp_Reminder_UpdateOneByReminderKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@reminderKey", SqlDbType.Int, (item.ReminderKey == 0) ? SqlInt32.Null : item.ReminderKey);
                        commandWrapper.AddInputParameter("@moduleKey", SqlDbType.Int, (item.ModuleKey == 0) ? SqlInt32.Null : item.ModuleKey);
                        commandWrapper.AddInputParameter("@resourceKey", SqlDbType.Int, (item.ResourceKey == 0) ? SqlInt32.Null : item.ResourceKey);
                        commandWrapper.AddInputParameter("@objectKey", SqlDbType.Int, (item.ObjectKey == 0) ? SqlInt32.Null : item.ObjectKey);
                        commandWrapper.AddInputParameter("@description", SqlDbType.VarChar, String.IsNullOrEmpty(item.Description) ? 0 : item.Description.Length, String.IsNullOrEmpty(item.Description) ? SqlString.Null : item.Description);
                        commandWrapper.AddInputParameter("@reminderDate", SqlDbType.SmallDateTime, (item.ReminderDate == DateTime.MinValue) ? SqlDateTime.Null : item.ReminderDate);
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
        	    string storedProcedure = "gensp_Reminder_DeleteOneByReminderKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@reminderKey", SqlDbType.Int, id);

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

        public virtual ReminderModel Get(int id)
        {
            ReminderModel item = null;

            try
            {
                string storedProcedure = "gensp_Reminder_SelectOneByReminderKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@reminderKey", SqlDbType.Int, id);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            while (dataReader.Read())
                            {
                                item = new ReminderModel();

                                Load(dataReader, item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_Reminder_SelectOneByReminderKey");
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

        public virtual IList<ReminderModel> GetAll()
        {
            return GetAll(new ReminderFilterModel());
        }

        public virtual IList<ReminderModel> GetAll(ReminderFilterModel filter)
        {
            List<ReminderModel> itemList = new List<ReminderModel>();

            try
            {
                string storedProcedure = "gensp_Reminder_SelectSomeBySearch";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@moduleKey", SqlDbType.Int, (filter.ModuleKey == 0) ? SqlInt32.Null : filter.ModuleKey);
                        commandWrapper.AddInputParameter("@resourceKey", SqlDbType.Int, (filter.ResourceKey == 0) ? SqlInt32.Null : filter.ResourceKey);
                        commandWrapper.AddInputParameter("@objectKey", SqlDbType.Int, (filter.ObjectKey == 0) ? SqlInt32.Null : filter.ObjectKey);
                        commandWrapper.AddInputParameter("@status", SqlDbType.Int, (filter.Status == 0) ? SqlInt32.Null : filter.Status);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            ReminderModel item = null;
                            while (dataReader.Read())
                            {
                                item = new ReminderModel();

                                Load(dataReader, item);

                                itemList.Add(item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_Reminder_SelectSomeBySearch");
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

        public virtual IList<ReminderModel> GetAll(ReminderFilterModel filter, PagingModel paging)
        {
            List<ReminderModel> itemList = new List<ReminderModel>();

            try
            {
                string storedProcedure = "gensp_Reminder_SelectSomeBySearchAndPaging";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@moduleKey", SqlDbType.Int, (filter.ModuleKey == 0) ? SqlInt32.Null : filter.ModuleKey);
                        commandWrapper.AddInputParameter("@resourceKey", SqlDbType.Int, (filter.ResourceKey == 0) ? SqlInt32.Null : filter.ResourceKey);
                        commandWrapper.AddInputParameter("@objectKey", SqlDbType.Int, (filter.ObjectKey == 0) ? SqlInt32.Null : filter.ObjectKey);
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
                            ReminderModel item = null;
                            while (dataReader.Read())
                            {
                                item = new ReminderModel();

                                Load(dataReader, item);

                                itemList.Add(item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_Reminder_SelectSomeBySearchAndPaging");
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

        protected void Load(DBDataReader dataReader, ReminderModel item)
        {
            item.ReminderKey = dataReader.GetValueInt("ReminderKey");
            item.ModuleKey = dataReader.GetValueInt("ModuleKey");
            item.ResourceKey = dataReader.GetValueInt("ResourceKey");
            item.ObjectKey = dataReader.GetValueInt("ObjectKey");
            item.Description = dataReader.GetValueText("Description");
            item.ReminderDate = dataReader.GetValueDateTime("ReminderDate");
            item.LastModificationTime = dataReader.GetValueDateTime("LastModificationTime");
            item.Status = dataReader.GetValueInt("Status");
        }
    }
}
