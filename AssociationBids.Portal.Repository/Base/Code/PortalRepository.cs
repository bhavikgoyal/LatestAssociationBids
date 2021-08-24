using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public class PortalRepository : BaseRepository, IPortalRepository
    {
        public PortalRepository() { }

        public PortalRepository(string connectionString)
            : base(connectionString) { }

        public virtual bool Create(PortalModel item)
        {
        	bool status = false;

        	try
        	{
        	    string storedProcedure = "gensp_Portal_InsertOne";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@portalID", SqlDbType.VarChar, String.IsNullOrEmpty(item.PortalID) ? 0 : item.PortalID.Length, String.IsNullOrEmpty(item.PortalID) ? SqlString.Null : item.PortalID);
                        commandWrapper.AddInputParameter("@title", SqlDbType.VarChar, String.IsNullOrEmpty(item.Title) ? 0 : item.Title.Length, String.IsNullOrEmpty(item.Title) ? SqlString.Null : item.Title);
                        commandWrapper.AddInputParameter("@url", SqlDbType.VarChar, String.IsNullOrEmpty(item.Url) ? 0 : item.Url.Length, String.IsNullOrEmpty(item.Url) ? SqlString.Null : item.Url);
                        commandWrapper.AddInputParameter("@siteImage", SqlDbType.VarChar, String.IsNullOrEmpty(item.SiteImage) ? 0 : item.SiteImage.Length, String.IsNullOrEmpty(item.SiteImage) ? SqlString.Null : item.SiteImage);
                        commandWrapper.AddInputParameter("@homePageImage", SqlDbType.VarChar, String.IsNullOrEmpty(item.HomePageImage) ? 0 : item.HomePageImage.Length, String.IsNullOrEmpty(item.HomePageImage) ? SqlString.Null : item.HomePageImage);
                        commandWrapper.AddInputParameter("@stylesheet", SqlDbType.VarChar, String.IsNullOrEmpty(item.Stylesheet) ? 0 : item.Stylesheet.Length, String.IsNullOrEmpty(item.Stylesheet) ? SqlString.Null : item.Stylesheet);
                        commandWrapper.AddInputParameter("@description", SqlDbType.VarChar, String.IsNullOrEmpty(item.Description) ? 0 : item.Description.Length, String.IsNullOrEmpty(item.Description) ? SqlString.Null : item.Description);
                        commandWrapper.AddInputParameter("@notificationSetting", SqlDbType.Int, (item.NotificationSetting == 0) ? SqlInt32.Null : item.NotificationSetting);
                        commandWrapper.AddInputParameter("@dateAdded", SqlDbType.DateTime, (item.DateAdded == DateTime.MinValue) ? SqlDateTime.Null : item.DateAdded);
                        commandWrapper.AddInputParameter("@lastModificationTime", SqlDbType.DateTime, (item.LastModificationTime == DateTime.MinValue) ? SqlDateTime.Null : item.LastModificationTime);
                        commandWrapper.AddInputParameter("@status", SqlDbType.Int, (item.Status == 0) ? SqlInt32.Null : item.Status);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@portalKey", SqlDbType.Int);
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        db.ExecuteNonQuery(commandWrapper);

                        // save the newly inserted key value
                        item.PortalKey = commandWrapper.GetValueInt("@portalKey");

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

        public virtual bool Update(PortalModel item)
        {
        	bool status = false;

        	try
        	{
        	    string storedProcedure = "gensp_Portal_UpdateOneByPortalKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@portalKey", SqlDbType.Int, (item.PortalKey == 0) ? SqlInt32.Null : item.PortalKey);
                        commandWrapper.AddInputParameter("@portalID", SqlDbType.VarChar, String.IsNullOrEmpty(item.PortalID) ? 0 : item.PortalID.Length, String.IsNullOrEmpty(item.PortalID) ? SqlString.Null : item.PortalID);
                        commandWrapper.AddInputParameter("@title", SqlDbType.VarChar, String.IsNullOrEmpty(item.Title) ? 0 : item.Title.Length, String.IsNullOrEmpty(item.Title) ? SqlString.Null : item.Title);
                        commandWrapper.AddInputParameter("@url", SqlDbType.VarChar, String.IsNullOrEmpty(item.Url) ? 0 : item.Url.Length, String.IsNullOrEmpty(item.Url) ? SqlString.Null : item.Url);
                        commandWrapper.AddInputParameter("@siteImage", SqlDbType.VarChar, String.IsNullOrEmpty(item.SiteImage) ? 0 : item.SiteImage.Length, String.IsNullOrEmpty(item.SiteImage) ? SqlString.Null : item.SiteImage);
                        commandWrapper.AddInputParameter("@homePageImage", SqlDbType.VarChar, String.IsNullOrEmpty(item.HomePageImage) ? 0 : item.HomePageImage.Length, String.IsNullOrEmpty(item.HomePageImage) ? SqlString.Null : item.HomePageImage);
                        commandWrapper.AddInputParameter("@stylesheet", SqlDbType.VarChar, String.IsNullOrEmpty(item.Stylesheet) ? 0 : item.Stylesheet.Length, String.IsNullOrEmpty(item.Stylesheet) ? SqlString.Null : item.Stylesheet);
                        commandWrapper.AddInputParameter("@description", SqlDbType.VarChar, String.IsNullOrEmpty(item.Description) ? 0 : item.Description.Length, String.IsNullOrEmpty(item.Description) ? SqlString.Null : item.Description);
                        commandWrapper.AddInputParameter("@notificationSetting", SqlDbType.Int, (item.NotificationSetting == 0) ? SqlInt32.Null : item.NotificationSetting);
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
        	    string storedProcedure = "gensp_Portal_DeleteOneByPortalKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@portalKey", SqlDbType.Int, id);

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

        public virtual PortalModel Get(int id)
        {
            PortalModel item = null;

            try
            {
                string storedProcedure = "gensp_Portal_SelectOneByPortalKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@portalKey", SqlDbType.Int, id);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            while (dataReader.Read())
                            {
                                item = new PortalModel();

                                Load(dataReader, item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_Portal_SelectOneByPortalKey");
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

        public virtual IList<PortalModel> GetAll()
        {
            return GetAll(new PortalFilterModel());
        }

        public virtual IList<PortalModel> GetAll(PortalFilterModel filter)
        {
            List<PortalModel> itemList = new List<PortalModel>();

            try
            {
                string storedProcedure = "gensp_Portal_SelectSomeBySearch";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@status", SqlDbType.Int, (filter.Status == 0) ? SqlInt32.Null : filter.Status);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            PortalModel item = null;
                            while (dataReader.Read())
                            {
                                item = new PortalModel();

                                Load(dataReader, item);

                                itemList.Add(item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_Portal_SelectSomeBySearch");
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

        public virtual IList<PortalModel> GetAll(PortalFilterModel filter, PagingModel paging)
        {
            List<PortalModel> itemList = new List<PortalModel>();

            try
            {
                string storedProcedure = "gensp_Portal_SelectSomeBySearchAndPaging";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
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
                            PortalModel item = null;
                            while (dataReader.Read())
                            {
                                item = new PortalModel();

                                Load(dataReader, item);

                                itemList.Add(item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_Portal_SelectSomeBySearchAndPaging");
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

        protected void Load(DBDataReader dataReader, PortalModel item)
        {
            item.PortalKey = dataReader.GetValueInt("PortalKey");
            item.PortalID = dataReader.GetValueText("PortalID");
            item.Title = dataReader.GetValueText("Title");
            item.Url = dataReader.GetValueText("Url");
            item.SiteImage = dataReader.GetValueText("SiteImage");
            item.HomePageImage = dataReader.GetValueText("HomePageImage");
            item.Stylesheet = dataReader.GetValueText("Stylesheet");
            item.Description = dataReader.GetValueText("Description");
            item.NotificationSetting = dataReader.GetValueInt("NotificationSetting");
            item.DateAdded = dataReader.GetValueDateTime("DateAdded");
            item.LastModificationTime = dataReader.GetValueDateTime("LastModificationTime");
            item.Status = dataReader.GetValueInt("Status");
        }
    }
}
