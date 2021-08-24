using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public class MessageRepository : BaseRepository, IMessageRepository
    {
        public MessageRepository() { }

        public MessageRepository(string connectionString)
            : base(connectionString) { }

        public virtual bool Create(MessageModel item)
        {
        	bool status = false;

        	try
        	{
        	    string storedProcedure = "gensp_Message_InsertOne";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@moduleKey", SqlDbType.Int, (item.ModuleKey == 0) ? SqlInt32.Null : item.ModuleKey);
                        commandWrapper.AddInputParameter("@resourceKey", SqlDbType.Int, (item.ResourceKey == 0) ? SqlInt32.Null : item.ResourceKey);
                        commandWrapper.AddInputParameter("@objectKey", SqlDbType.Int, (item.ObjectKey == 0) ? SqlInt32.Null : item.ObjectKey);
                        commandWrapper.AddInputParameter("@body", SqlDbType.VarChar, String.IsNullOrEmpty(item.Body) ? 0 : item.Body.Length, String.IsNullOrEmpty(item.Body) ? SqlString.Null : item.Body);
                        commandWrapper.AddInputParameter("@lastModificationTime", SqlDbType.DateTime, (item.LastModificationTime == DateTime.MinValue) ? SqlDateTime.Null : item.LastModificationTime);
                        commandWrapper.AddInputParameter("@messageStatus", SqlDbType.Int, (item.MessageStatus == 0) ? SqlInt32.Null : item.MessageStatus);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@messageKey", SqlDbType.Int);
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        db.ExecuteNonQuery(commandWrapper);

                        // save the newly inserted key value
                        item.MessageKey = commandWrapper.GetValueInt("@messageKey");

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

        public virtual bool Update(MessageModel item)
        {
        	bool status = false;

        	try
        	{
        	    string storedProcedure = "gensp_Message_UpdateOneByMessageKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@messageKey", SqlDbType.Int, (item.MessageKey == 0) ? SqlInt32.Null : item.MessageKey);
                        commandWrapper.AddInputParameter("@moduleKey", SqlDbType.Int, (item.ModuleKey == 0) ? SqlInt32.Null : item.ModuleKey);
                        commandWrapper.AddInputParameter("@resourceKey", SqlDbType.Int, (item.ResourceKey == 0) ? SqlInt32.Null : item.ResourceKey);
                        commandWrapper.AddInputParameter("@objectKey", SqlDbType.Int, (item.ObjectKey == 0) ? SqlInt32.Null : item.ObjectKey);
                        commandWrapper.AddInputParameter("@body", SqlDbType.VarChar, String.IsNullOrEmpty(item.Body) ? 0 : item.Body.Length, String.IsNullOrEmpty(item.Body) ? SqlString.Null : item.Body);
                        commandWrapper.AddInputParameter("@lastModificationTime", SqlDbType.DateTime, (item.LastModificationTime == DateTime.MinValue) ? SqlDateTime.Null : item.LastModificationTime);
                        commandWrapper.AddInputParameter("@messageStatus", SqlDbType.Int, (item.MessageStatus == 0) ? SqlInt32.Null : item.MessageStatus);

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
        	    string storedProcedure = "gensp_Message_DeleteOneByMessageKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@messageKey", SqlDbType.Int, id);

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

        public virtual MessageModel Get(int id)
        {
            MessageModel item = null;

            try
            {
                string storedProcedure = "gensp_Message_SelectOneByMessageKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@messageKey", SqlDbType.Int, id);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            while (dataReader.Read())
                            {
                                item = new MessageModel();

                                Load(dataReader, item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_Message_SelectOneByMessageKey");
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

        public virtual IList<MessageModel> GetAll()
        {
            return GetAll(new MessageFilterModel());
        }

        public virtual IList<MessageModel> GetAll(MessageFilterModel filter)
        {
            List<MessageModel> itemList = new List<MessageModel>();

            try
            {
                string storedProcedure = "gensp_Message_SelectSomeBySearch";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@moduleKey", SqlDbType.Int, (filter.ModuleKey == 0) ? SqlInt32.Null : filter.ModuleKey);
                        commandWrapper.AddInputParameter("@resourceKey", SqlDbType.Int, (filter.ResourceKey == 0) ? SqlInt32.Null : filter.ResourceKey);
                        commandWrapper.AddInputParameter("@objectKey", SqlDbType.Int, (filter.ObjectKey == 0) ? SqlInt32.Null : filter.ObjectKey);
                        commandWrapper.AddInputParameter("@messageStatus", SqlDbType.Int, (filter.MessageStatus == 0) ? SqlInt32.Null : filter.MessageStatus);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            MessageModel item = null;
                            while (dataReader.Read())
                            {
                                item = new MessageModel();

                                Load(dataReader, item);

                                itemList.Add(item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_Message_SelectSomeBySearch");
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

        public virtual IList<MessageModel> GetAll(MessageFilterModel filter, PagingModel paging)
        {
            List<MessageModel> itemList = new List<MessageModel>();

            try
            {
                string storedProcedure = "gensp_Message_SelectSomeBySearchAndPaging";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@moduleKey", SqlDbType.Int, (filter.ModuleKey == 0) ? SqlInt32.Null : filter.ModuleKey);
                        commandWrapper.AddInputParameter("@resourceKey", SqlDbType.Int, (filter.ResourceKey == 0) ? SqlInt32.Null : filter.ResourceKey);
                        commandWrapper.AddInputParameter("@objectKey", SqlDbType.Int, (filter.ObjectKey == 0) ? SqlInt32.Null : filter.ObjectKey);
                        commandWrapper.AddInputParameter("@messageStatus", SqlDbType.Int, (filter.MessageStatus == 0) ? SqlInt32.Null : filter.MessageStatus);

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
                            MessageModel item = null;
                            while (dataReader.Read())
                            {
                                item = new MessageModel();

                                Load(dataReader, item);

                                itemList.Add(item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_Message_SelectSomeBySearchAndPaging");
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

        protected void Load(DBDataReader dataReader, MessageModel item)
        {
            item.MessageKey = dataReader.GetValueInt("MessageKey");
            item.ModuleKey = dataReader.GetValueInt("ModuleKey");
            item.ResourceKey = dataReader.GetValueInt("ResourceKey");
            item.ObjectKey = dataReader.GetValueInt("ObjectKey");
            item.Body = dataReader.GetValueText("Body");
            item.LastModificationTime = dataReader.GetValueDateTime("LastModificationTime");
            item.MessageStatus = dataReader.GetValueInt("MessageStatus");
        }
    }
}
