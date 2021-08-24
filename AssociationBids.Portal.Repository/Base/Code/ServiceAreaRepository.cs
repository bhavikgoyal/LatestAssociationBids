using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public class ServiceAreaRepository : BaseRepository, IServiceAreaRepository
    {
        public ServiceAreaRepository() { }

        public ServiceAreaRepository(string connectionString)
            : base(connectionString) { }

        public virtual bool Create(ServiceAreaModel item)
        {
        	bool status = false;

        	try
        	{
        	    string storedProcedure = "gensp_ServiceArea_InsertOne";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@vendorKey", SqlDbType.Int, (item.VendorKey == 0) ? SqlInt32.Null : item.VendorKey);
                        commandWrapper.AddInputParameter("@address", SqlDbType.VarChar, String.IsNullOrEmpty(item.Address) ? 0 : item.Address.Length, String.IsNullOrEmpty(item.Address) ? SqlString.Null : item.Address);
                        commandWrapper.AddInputParameter("@address2", SqlDbType.VarChar, String.IsNullOrEmpty(item.Address2) ? 0 : item.Address2.Length, String.IsNullOrEmpty(item.Address2) ? SqlString.Null : item.Address2);
                        commandWrapper.AddInputParameter("@city", SqlDbType.VarChar, String.IsNullOrEmpty(item.City) ? 0 : item.City.Length, String.IsNullOrEmpty(item.City) ? SqlString.Null : item.City);
                        commandWrapper.AddInputParameter("@state", SqlDbType.VarChar, String.IsNullOrEmpty(item.State) ? 0 : item.State.Length, String.IsNullOrEmpty(item.State) ? SqlString.Null : item.State);
                        commandWrapper.AddInputParameter("@zip", SqlDbType.VarChar, String.IsNullOrEmpty(item.Zip) ? 0 : item.Zip.Length, String.IsNullOrEmpty(item.Zip) ? SqlString.Null : item.Zip);
                        commandWrapper.AddInputParameter("@latitude", SqlDbType.Float, (item.Latitude == 0.0) ? SqlDouble.Null : item.Latitude);
                        commandWrapper.AddInputParameter("@longitude", SqlDbType.Float, (item.Longitude == 0.0) ? SqlDouble.Null : item.Longitude);
                        commandWrapper.AddInputParameter("@radius", SqlDbType.Int, (item.Radius == 0) ? SqlInt32.Null : item.Radius);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@serviceAreaKey", SqlDbType.Int);
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        db.ExecuteNonQuery(commandWrapper);

                        // save the newly inserted key value
                        item.ServiceAreaKey = commandWrapper.GetValueInt("@serviceAreaKey");

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

        public virtual bool Update(ServiceAreaModel item)
        {
        	bool status = false;

        	try
        	{
        	    string storedProcedure = "gensp_ServiceArea_UpdateOneByServiceAreaKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@serviceAreaKey", SqlDbType.Int, (item.ServiceAreaKey == 0) ? SqlInt32.Null : item.ServiceAreaKey);
                        commandWrapper.AddInputParameter("@vendorKey", SqlDbType.Int, (item.VendorKey == 0) ? SqlInt32.Null : item.VendorKey);
                        commandWrapper.AddInputParameter("@address", SqlDbType.VarChar, String.IsNullOrEmpty(item.Address) ? 0 : item.Address.Length, String.IsNullOrEmpty(item.Address) ? SqlString.Null : item.Address);
                        commandWrapper.AddInputParameter("@address2", SqlDbType.VarChar, String.IsNullOrEmpty(item.Address2) ? 0 : item.Address2.Length, String.IsNullOrEmpty(item.Address2) ? SqlString.Null : item.Address2);
                        commandWrapper.AddInputParameter("@city", SqlDbType.VarChar, String.IsNullOrEmpty(item.City) ? 0 : item.City.Length, String.IsNullOrEmpty(item.City) ? SqlString.Null : item.City);
                        commandWrapper.AddInputParameter("@state", SqlDbType.VarChar, String.IsNullOrEmpty(item.State) ? 0 : item.State.Length, String.IsNullOrEmpty(item.State) ? SqlString.Null : item.State);
                        commandWrapper.AddInputParameter("@zip", SqlDbType.VarChar, String.IsNullOrEmpty(item.Zip) ? 0 : item.Zip.Length, String.IsNullOrEmpty(item.Zip) ? SqlString.Null : item.Zip);
                        commandWrapper.AddInputParameter("@latitude", SqlDbType.Float, (item.Latitude == 0.0) ? SqlDouble.Null : item.Latitude);
                        commandWrapper.AddInputParameter("@longitude", SqlDbType.Float, (item.Longitude == 0.0) ? SqlDouble.Null : item.Longitude);
                        commandWrapper.AddInputParameter("@radius", SqlDbType.Int, (item.Radius == 0) ? SqlInt32.Null : item.Radius);

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
        	    string storedProcedure = "gensp_ServiceArea_DeleteOneByServiceAreaKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@serviceAreaKey", SqlDbType.Int, id);

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

        public virtual ServiceAreaModel Get(int id)
        {
            ServiceAreaModel item = null;

            try
            {
                string storedProcedure = "gensp_ServiceArea_SelectOneByServiceAreaKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@serviceAreaKey", SqlDbType.Int, id);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            while (dataReader.Read())
                            {
                                item = new ServiceAreaModel();

                                Load(dataReader, item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_ServiceArea_SelectOneByServiceAreaKey");
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

        public virtual IList<ServiceAreaModel> GetAll()
        {
            return GetAll(new ServiceAreaFilterModel());
        }

        public virtual IList<ServiceAreaModel> GetAll(ServiceAreaFilterModel filter)
        {
            List<ServiceAreaModel> itemList = new List<ServiceAreaModel>();

            try
            {
                string storedProcedure = "gensp_ServiceArea_SelectSomeBySearch";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@vendorKey", SqlDbType.Int, (filter.VendorKey == 0) ? SqlInt32.Null : filter.VendorKey);
                        commandWrapper.AddInputParameter("@state", SqlDbType.VarChar, GetFilterValue(filter.State).Length, String.IsNullOrEmpty(filter.State) ? SqlString.Null : GetFilterValue(filter.State));

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            ServiceAreaModel item = null;
                            while (dataReader.Read())
                            {
                                item = new ServiceAreaModel();

                                Load(dataReader, item);

                                itemList.Add(item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_ServiceArea_SelectSomeBySearch");
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

        public virtual IList<ServiceAreaModel> GetAll(ServiceAreaFilterModel filter, PagingModel paging)
        {
            List<ServiceAreaModel> itemList = new List<ServiceAreaModel>();

            try
            {
                string storedProcedure = "gensp_ServiceArea_SelectSomeBySearchAndPaging";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@vendorKey", SqlDbType.Int, (filter.VendorKey == 0) ? SqlInt32.Null : filter.VendorKey);
                        commandWrapper.AddInputParameter("@state", SqlDbType.VarChar, GetFilterValue(filter.State).Length, String.IsNullOrEmpty(filter.State) ? SqlString.Null : GetFilterValue(filter.State));

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
                            ServiceAreaModel item = null;
                            while (dataReader.Read())
                            {
                                item = new ServiceAreaModel();

                                Load(dataReader, item);

                                itemList.Add(item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_ServiceArea_SelectSomeBySearchAndPaging");
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

        protected void Load(DBDataReader dataReader, ServiceAreaModel item)
        {
            item.ServiceAreaKey = dataReader.GetValueInt("ServiceAreaKey");
            item.VendorKey = dataReader.GetValueInt("VendorKey");
            item.Address = dataReader.GetValueText("Address");
            item.Address2 = dataReader.GetValueText("Address2");
            item.City = dataReader.GetValueText("City");
            item.State = dataReader.GetValueText("State");
            item.Zip = dataReader.GetValueText("Zip");
            item.Latitude = dataReader.GetValueDouble("Latitude");
            item.Longitude = dataReader.GetValueDouble("Longitude");
            item.Radius = dataReader.GetValueInt("Radius");
        }
    }
}
