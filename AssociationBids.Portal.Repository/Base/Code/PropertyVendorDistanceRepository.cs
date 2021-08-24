using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public class PropertyVendorDistanceRepository : BaseRepository, IPropertyVendorDistanceRepository
    {
        public PropertyVendorDistanceRepository() { }

        public PropertyVendorDistanceRepository(string connectionString)
            : base(connectionString) { }

        public virtual bool Create(PropertyVendorDistanceModel item)
        {
        	bool status = false;

        	try
        	{
        	    string storedProcedure = "gensp_PropertyVendorDistance_InsertOne";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@propertyKey", SqlDbType.Int, (item.PropertyKey == 0) ? SqlInt32.Null : item.PropertyKey);
                        commandWrapper.AddInputParameter("@vendorKey", SqlDbType.Int, (item.VendorKey == 0) ? SqlInt32.Null : item.VendorKey);
                        commandWrapper.AddInputParameter("@distance", SqlDbType.Float, (item.Distance == 0.0) ? SqlDouble.Null : item.Distance);
                        commandWrapper.AddInputParameter("@lastModificationTime", SqlDbType.DateTime, (item.LastModificationTime == DateTime.MinValue) ? SqlDateTime.Null : item.LastModificationTime);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@propertyVendorDistanceKey", SqlDbType.Int);
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        db.ExecuteNonQuery(commandWrapper);

                        // save the newly inserted key value
                        item.PropertyVendorDistanceKey = commandWrapper.GetValueInt("@propertyVendorDistanceKey");

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

        public virtual bool Update(PropertyVendorDistanceModel item)
        {
        	bool status = false;

        	try
        	{
        	    string storedProcedure = "gensp_PropertyVendorDistance_UpdateOneByPropertyVendorDistanceKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@propertyVendorDistanceKey", SqlDbType.Int, (item.PropertyVendorDistanceKey == 0) ? SqlInt32.Null : item.PropertyVendorDistanceKey);
                        commandWrapper.AddInputParameter("@propertyKey", SqlDbType.Int, (item.PropertyKey == 0) ? SqlInt32.Null : item.PropertyKey);
                        commandWrapper.AddInputParameter("@vendorKey", SqlDbType.Int, (item.VendorKey == 0) ? SqlInt32.Null : item.VendorKey);
                        commandWrapper.AddInputParameter("@distance", SqlDbType.Float, (item.Distance == 0.0) ? SqlDouble.Null : item.Distance);
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
                string storedProcedure = "gensp_PropertyVendorDistance_DeleteOneByPropertyVendorDistanceKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@propertyVendorDistanceKey", SqlDbType.Int, id);

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

        public virtual PropertyVendorDistanceModel Get(int id)
        {
            PropertyVendorDistanceModel item = null;

            try
            {
                string storedProcedure = "gensp_PropertyVendorDistance_SelectOneByPropertyVendorDistanceKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@propertyVendorDistanceKey", SqlDbType.Int, id);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            while (dataReader.Read())
                            {
                                item = new PropertyVendorDistanceModel();

                                Load(dataReader, item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_PropertyVendorDistance_SelectOneByPropertyVendorDistanceKey");
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

        public virtual IList<PropertyVendorDistanceModel> GetAll()
        {
            return GetAll(new PropertyVendorDistanceFilterModel());
        }

        public virtual IList<PropertyVendorDistanceModel> GetAll(PropertyVendorDistanceFilterModel filter)
        {
            List<PropertyVendorDistanceModel> itemList = new List<PropertyVendorDistanceModel>();

            try
            {
                string storedProcedure = "gensp_PropertyVendorDistance_SelectSomeBySearch";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@propertyKey", SqlDbType.Int, (filter.PropertyKey == 0) ? SqlInt32.Null : filter.PropertyKey);
                        commandWrapper.AddInputParameter("@vendorKey", SqlDbType.Int, (filter.VendorKey == 0) ? SqlInt32.Null : filter.VendorKey);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            PropertyVendorDistanceModel item = null;
                            while (dataReader.Read())
                            {
                                item = new PropertyVendorDistanceModel();

                                Load(dataReader, item);

                                itemList.Add(item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_PropertyVendorDistance_SelectSomeBySearch");
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

        public virtual IList<PropertyVendorDistanceModel> GetAll(PropertyVendorDistanceFilterModel filter, PagingModel paging)
        {
            List<PropertyVendorDistanceModel> itemList = new List<PropertyVendorDistanceModel>();

            try
            {
                string storedProcedure = "gensp_PropertyVendorDistance_SelectSomeBySearchAndPaging";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@propertyKey", SqlDbType.Int, (filter.PropertyKey == 0) ? SqlInt32.Null : filter.PropertyKey);
                        commandWrapper.AddInputParameter("@vendorKey", SqlDbType.Int, (filter.VendorKey == 0) ? SqlInt32.Null : filter.VendorKey);

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
                            PropertyVendorDistanceModel item = null;
                            while (dataReader.Read())
                            {
                                item = new PropertyVendorDistanceModel();

                                Load(dataReader, item);

                                itemList.Add(item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_PropertyVendorDistance_SelectSomeBySearchAndPaging");
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

        protected void Load(DBDataReader dataReader, PropertyVendorDistanceModel item)
        {
            item.PropertyVendorDistanceKey = dataReader.GetValueInt("PropertyVendorDistanceKey");
            item.PropertyKey = dataReader.GetValueInt("PropertyKey");
            item.VendorKey = dataReader.GetValueInt("VendorKey");
            item.Distance = dataReader.GetValueDouble("Distance");
            item.LastModificationTime = dataReader.GetValueDateTime("LastModificationTime");
        }
    }
}
