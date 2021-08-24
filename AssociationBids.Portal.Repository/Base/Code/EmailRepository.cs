using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public class EmailRepository : BaseRepository, IEmailRepository
    {
        public EmailRepository() { }

        public EmailRepository(string connectionString)
            : base(connectionString) { }

        public virtual bool Create(EmailModel item)
        {
        	bool status = false;

        	try
        	{
        	    string storedProcedure = "gensp_Email_InsertOne";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {


                       
                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@moduleKey", SqlDbType.Int, (item.ModuleKey == 0) ? SqlInt32.Null : item.ModuleKey);
                        commandWrapper.AddInputParameter("@resourceKey", SqlDbType.Int, (item.ResourceKey == 0) ? SqlInt32.Null : item.ResourceKey);
                        commandWrapper.AddInputParameter("@objectKey", SqlDbType.Int, (item.ObjectKey == 0) ? SqlInt32.Null : item.ObjectKey);
                        commandWrapper.AddInputParameter("@from", SqlDbType.VarChar, String.IsNullOrEmpty(item.From) ? 0 : item.From.Length, String.IsNullOrEmpty(item.From) ? SqlString.Null : item.From);
                        commandWrapper.AddInputParameter("@to", SqlDbType.VarChar, String.IsNullOrEmpty(item.To) ? 0 : item.To.Length, String.IsNullOrEmpty(item.To) ? SqlString.Null : item.To);
                        commandWrapper.AddInputParameter("@cc", SqlDbType.VarChar, String.IsNullOrEmpty(item.Cc) ? 0 : item.Cc.Length, String.IsNullOrEmpty(item.Cc) ? SqlString.Null : item.Cc);
                        commandWrapper.AddInputParameter("@bcc", SqlDbType.VarChar, String.IsNullOrEmpty(item.Bcc) ? 0 : item.Bcc.Length, String.IsNullOrEmpty(item.Bcc) ? SqlString.Null : item.Bcc);
                        commandWrapper.AddInputParameter("@subject", SqlDbType.VarChar, String.IsNullOrEmpty(item.Subject) ? 0 : item.Subject.Length, String.IsNullOrEmpty(item.Subject) ? SqlString.Null : item.Subject);
                        commandWrapper.AddInputParameter("@body", SqlDbType.VarChar, String.IsNullOrEmpty(item.Body) ? 0 : item.Body.Length, String.IsNullOrEmpty(item.Body) ? SqlString.Null : item.Body);
                        commandWrapper.AddInputParameter("@dateAdded", SqlDbType.DateTime, (item.DateAdded == DateTime.MinValue) ? SqlDateTime.Null : item.DateAdded);
                        commandWrapper.AddInputParameter("@dateSent", SqlDbType.SmallDateTime, (item.DateSent == DateTime.MinValue) ? SqlDateTime.Null : item.DateSent);
                        commandWrapper.AddInputParameter("@emailStatus", SqlDbType.Int, (item.EmailStatus == 0) ? SqlInt32.Null : item.EmailStatus);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@emailKey", SqlDbType.Int);
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        db.ExecuteNonQuery(commandWrapper);

                        // save the newly inserted key value
                        item.EmailKey = commandWrapper.GetValueInt("@emailKey");

                        status = (commandWrapper.GetValueInt("@errorCode") == 0);
                    }
                }
        	}
        	catch (Exception ex)
        	{
        	    // error occured...
        	    throw;
        	}

        	return status;
        }

        public virtual bool Update(EmailModel item)
        {
        	bool status = false;

        	try
        	{
        	    string storedProcedure = "gensp_Email_UpdateOneByEmailKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@emailKey", SqlDbType.Int, (item.EmailKey == 0) ? SqlInt32.Null : item.EmailKey);
                        commandWrapper.AddInputParameter("@moduleKey", SqlDbType.Int, (item.ModuleKey == 0) ? SqlInt32.Null : item.ModuleKey);
                        commandWrapper.AddInputParameter("@resourceKey", SqlDbType.Int, (item.ResourceKey == 0) ? SqlInt32.Null : item.ResourceKey);
                        commandWrapper.AddInputParameter("@objectKey", SqlDbType.Int, (item.ObjectKey == 0) ? SqlInt32.Null : item.ObjectKey);
                        commandWrapper.AddInputParameter("@from", SqlDbType.VarChar, String.IsNullOrEmpty(item.From) ? 0 : item.From.Length, String.IsNullOrEmpty(item.From) ? SqlString.Null : item.From);
                        commandWrapper.AddInputParameter("@to", SqlDbType.VarChar, String.IsNullOrEmpty(item.To) ? 0 : item.To.Length, String.IsNullOrEmpty(item.To) ? SqlString.Null : item.To);
                        commandWrapper.AddInputParameter("@cc", SqlDbType.VarChar, String.IsNullOrEmpty(item.Cc) ? 0 : item.Cc.Length, String.IsNullOrEmpty(item.Cc) ? SqlString.Null : item.Cc);
                        commandWrapper.AddInputParameter("@bcc", SqlDbType.VarChar, String.IsNullOrEmpty(item.Bcc) ? 0 : item.Bcc.Length, String.IsNullOrEmpty(item.Bcc) ? SqlString.Null : item.Bcc);
                        commandWrapper.AddInputParameter("@subject", SqlDbType.VarChar, String.IsNullOrEmpty(item.Subject) ? 0 : item.Subject.Length, String.IsNullOrEmpty(item.Subject) ? SqlString.Null : item.Subject);
                        commandWrapper.AddInputParameter("@body", SqlDbType.VarChar, String.IsNullOrEmpty(item.Body) ? 0 : item.Body.Length, String.IsNullOrEmpty(item.Body) ? SqlString.Null : item.Body);
                        commandWrapper.AddInputParameter("@dateAdded", SqlDbType.DateTime, (item.DateAdded == DateTime.MinValue) ? SqlDateTime.Null : item.DateAdded);
                        commandWrapper.AddInputParameter("@dateSent", SqlDbType.SmallDateTime, (item.DateSent == DateTime.MinValue) ? SqlDateTime.Null : item.DateSent);
                        commandWrapper.AddInputParameter("@emailStatus", SqlDbType.Int, (item.EmailStatus == 0) ? SqlInt32.Null : item.EmailStatus);

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
        	    string storedProcedure = "gensp_Email_DeleteOneByEmailKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@emailKey", SqlDbType.Int, id);

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

        public virtual EmailModel Get(int id)
        {
            EmailModel item = null;

            try
            {
                string storedProcedure = "gensp_Email_SelectOneByEmailKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@emailKey", SqlDbType.Int, id);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            while (dataReader.Read())
                            {
                                item = new EmailModel();

                                Load(dataReader, item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_Email_SelectOneByEmailKey");
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

        public virtual IList<EmailModel> GetAll()
        {
            return GetAll(new EmailFilterModel());
        }

        public virtual IList<EmailModel> GetAll(EmailFilterModel filter)
        {
            List<EmailModel> itemList = new List<EmailModel>();

            try
            {
                string storedProcedure = "gensp_Email_SelectSomeBySearch";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@moduleKey", SqlDbType.Int, (filter.ModuleKey == 0) ? SqlInt32.Null : filter.ModuleKey);
                        commandWrapper.AddInputParameter("@resourceKey", SqlDbType.Int, (filter.ResourceKey == 0) ? SqlInt32.Null : filter.ResourceKey);
                        commandWrapper.AddInputParameter("@objectKey", SqlDbType.Int, (filter.ObjectKey == 0) ? SqlInt32.Null : filter.ObjectKey);
                        commandWrapper.AddInputParameter("@emailStatus", SqlDbType.Int, (filter.EmailStatus == 0) ? SqlInt32.Null : filter.EmailStatus);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            EmailModel item = null;
                            while (dataReader.Read())
                            {
                                item = new EmailModel();

                                Load(dataReader, item);

                                itemList.Add(item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_Email_SelectSomeBySearch");
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

        public virtual IList<EmailModel> GetAll(EmailFilterModel filter, PagingModel paging)
        {
            List<EmailModel> itemList = new List<EmailModel>();

            try
            {
                string storedProcedure = "gensp_Email_SelectSomeBySearchAndPaging";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@moduleKey", SqlDbType.Int, (filter.ModuleKey == 0) ? SqlInt32.Null : filter.ModuleKey);
                        commandWrapper.AddInputParameter("@resourceKey", SqlDbType.Int, (filter.ResourceKey == 0) ? SqlInt32.Null : filter.ResourceKey);
                        commandWrapper.AddInputParameter("@objectKey", SqlDbType.Int, (filter.ObjectKey == 0) ? SqlInt32.Null : filter.ObjectKey);
                        commandWrapper.AddInputParameter("@emailStatus", SqlDbType.Int, (filter.EmailStatus == 0) ? SqlInt32.Null : filter.EmailStatus);

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
                            EmailModel item = null;
                            while (dataReader.Read())
                            {
                                item = new EmailModel();

                                Load(dataReader, item);

                                itemList.Add(item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_Email_SelectSomeBySearchAndPaging");
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

        protected void Load(DBDataReader dataReader, EmailModel item)
        {
            item.EmailKey = dataReader.GetValueInt("EmailKey");
            item.ModuleKey = dataReader.GetValueInt("ModuleKey");
            item.ResourceKey = dataReader.GetValueInt("ResourceKey");
            item.ObjectKey = dataReader.GetValueInt("ObjectKey");
            item.From = dataReader.GetValueText("From");
            item.To = dataReader.GetValueText("To");
            item.Cc = dataReader.GetValueText("Cc");
            item.Bcc = dataReader.GetValueText("Bcc");
            item.Subject = dataReader.GetValueText("Subject");
            item.Body = dataReader.GetValueText("Body");
            item.DateAdded = dataReader.GetValueDateTime("DateAdded");
            item.DateSent = dataReader.GetValueDateTime("DateSent");
            item.EmailStatus = dataReader.GetValueInt("EmailStatus");
        }
    }
}
