using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public class CalendarRepository : BaseRepository, ICalendarRepository
    {
        public CalendarRepository() { }

        public CalendarRepository(string connectionString)
            : base(connectionString) { }

        public virtual bool Create(CalendarModel item)
        {
        	bool status = false;

        	try
        	{
        	    string storedProcedure = "gensp_Calendar_InsertOne";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@moduleKey", SqlDbType.Int, (item.ModuleKey == 0) ? SqlInt32.Null : item.ModuleKey);
                        commandWrapper.AddInputParameter("@resourceKey", SqlDbType.Int, (item.ResourceKey == 0) ? SqlInt32.Null : item.ResourceKey);
                        commandWrapper.AddInputParameter("@objectKey", SqlDbType.Int, (item.ObjectKey == 0) ? SqlInt32.Null : item.ObjectKey);
                        commandWrapper.AddInputParameter("@subject", SqlDbType.VarChar, String.IsNullOrEmpty(item.Subject) ? 0 : item.Subject.Length, String.IsNullOrEmpty(item.Subject) ? SqlString.Null : item.Subject);
                        commandWrapper.AddInputParameter("@startDate", SqlDbType.SmallDateTime, (item.StartDate == DateTime.MinValue) ? SqlDateTime.Null : item.StartDate);
                        commandWrapper.AddInputParameter("@endDate", SqlDbType.SmallDateTime, (item.EndDate == DateTime.MinValue) ? SqlDateTime.Null : item.EndDate);
                        commandWrapper.AddInputParameter("@allDayEvent", SqlDbType.Bit, (item.AllDayEvent == false) ? SqlBoolean.Null : item.AllDayEvent);
                        commandWrapper.AddInputParameter("@location", SqlDbType.VarChar, String.IsNullOrEmpty(item.Location) ? 0 : item.Location.Length, String.IsNullOrEmpty(item.Location) ? SqlString.Null : item.Location);
                        commandWrapper.AddInputParameter("@description", SqlDbType.VarChar, String.IsNullOrEmpty(item.Description) ? 0 : item.Description.Length, String.IsNullOrEmpty(item.Description) ? SqlString.Null : item.Description);
                        commandWrapper.AddInputParameter("@lastModificationTime", SqlDbType.DateTime, (item.LastModificationTime == DateTime.MinValue) ? SqlDateTime.Null : item.LastModificationTime);
                        commandWrapper.AddInputParameter("@status", SqlDbType.Int, (item.Status == 0) ? SqlInt32.Null : item.Status);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@calendarKey", SqlDbType.Int);
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        db.ExecuteNonQuery(commandWrapper);

                        // save the newly inserted key value
                        item.CalendarKey = commandWrapper.GetValueInt("@calendarKey");

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

        public virtual bool Update(CalendarModel item)
        {
        	bool status = false;

        	try
        	{
        	    string storedProcedure = "gensp_Calendar_UpdateOneByCalendarKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@calendarKey", SqlDbType.Int, (item.CalendarKey == 0) ? SqlInt32.Null : item.CalendarKey);
                        commandWrapper.AddInputParameter("@moduleKey", SqlDbType.Int, (item.ModuleKey == 0) ? SqlInt32.Null : item.ModuleKey);
                        commandWrapper.AddInputParameter("@resourceKey", SqlDbType.Int, (item.ResourceKey == 0) ? SqlInt32.Null : item.ResourceKey);
                        commandWrapper.AddInputParameter("@objectKey", SqlDbType.Int, (item.ObjectKey == 0) ? SqlInt32.Null : item.ObjectKey);
                        commandWrapper.AddInputParameter("@subject", SqlDbType.VarChar, String.IsNullOrEmpty(item.Subject) ? 0 : item.Subject.Length, String.IsNullOrEmpty(item.Subject) ? SqlString.Null : item.Subject);
                        commandWrapper.AddInputParameter("@startDate", SqlDbType.SmallDateTime, (item.StartDate == DateTime.MinValue) ? SqlDateTime.Null : item.StartDate);
                        commandWrapper.AddInputParameter("@endDate", SqlDbType.SmallDateTime, (item.EndDate == DateTime.MinValue) ? SqlDateTime.Null : item.EndDate);
                        commandWrapper.AddInputParameter("@allDayEvent", SqlDbType.Bit, (item.AllDayEvent == false) ? SqlBoolean.Null : item.AllDayEvent);
                        commandWrapper.AddInputParameter("@location", SqlDbType.VarChar, String.IsNullOrEmpty(item.Location) ? 0 : item.Location.Length, String.IsNullOrEmpty(item.Location) ? SqlString.Null : item.Location);
                        commandWrapper.AddInputParameter("@description", SqlDbType.VarChar, String.IsNullOrEmpty(item.Description) ? 0 : item.Description.Length, String.IsNullOrEmpty(item.Description) ? SqlString.Null : item.Description);
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
        	    string storedProcedure = "gensp_Calendar_DeleteOneByCalendarKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@calendarKey", SqlDbType.Int, id);

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

        public virtual CalendarModel Get(int id)
        {
            CalendarModel item = null;

            try
            {
                string storedProcedure = "gensp_Calendar_SelectOneByCalendarKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@calendarKey", SqlDbType.Int, id);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            while (dataReader.Read())
                            {
                                item = new CalendarModel();

                                Load(dataReader, item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_Calendar_SelectOneByCalendarKey");
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

        public virtual IList<CalendarModel> GetAll()
        {
            return GetAll(new CalendarFilterModel());
        }

        public virtual IList<CalendarModel> GetAll(CalendarFilterModel filter)
        {
            List<CalendarModel> itemList = new List<CalendarModel>();

            try
            {
                string storedProcedure = "gensp_Calendar_SelectSomeBySearch";
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
                            CalendarModel item = null;
                            while (dataReader.Read())
                            {
                                item = new CalendarModel();

                                Load(dataReader, item);

                                itemList.Add(item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_Calendar_SelectSomeBySearch");
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

        public virtual IList<CalendarModel> GetAll(CalendarFilterModel filter, PagingModel paging)
        {
            List<CalendarModel> itemList = new List<CalendarModel>();

            try
            {
                string storedProcedure = "gensp_Calendar_SelectSomeBySearchAndPaging";
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
                            CalendarModel item = null;
                            while (dataReader.Read())
                            {
                                item = new CalendarModel();

                                Load(dataReader, item);

                                itemList.Add(item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_Calendar_SelectSomeBySearchAndPaging");
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

        protected void Load(DBDataReader dataReader, CalendarModel item)
        {
            item.CalendarKey = dataReader.GetValueInt("CalendarKey");
            item.ModuleKey = dataReader.GetValueInt("ModuleKey");
            item.ResourceKey = dataReader.GetValueInt("ResourceKey");
            item.ObjectKey = dataReader.GetValueInt("ObjectKey");
            item.Subject = dataReader.GetValueText("Subject");
            item.StartDate = dataReader.GetValueDateTime("StartDate");
            item.EndDate = dataReader.GetValueDateTime("EndDate");
            item.AllDayEvent = dataReader.GetValueBool("AllDayEvent");
            item.Location = dataReader.GetValueText("Location");
            item.Description = dataReader.GetValueText("Description");
            item.LastModificationTime = dataReader.GetValueDateTime("LastModificationTime");
            item.Status = dataReader.GetValueInt("Status");
        }
    }
}
