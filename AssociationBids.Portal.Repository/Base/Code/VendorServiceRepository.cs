using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public class VendorServiceRepository : BaseRepository, IVendorServiceRepository
    {
        public VendorServiceRepository() { }

        public VendorServiceRepository(string connectionString)
            : base(connectionString) { }

        public virtual bool Create(VendorServiceModel item)
        {
        	bool status = false;

        	try
        	{
        	    string storedProcedure = "gensp_VendorService_InsertOne";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@vendorKey", SqlDbType.Int, (item.VendorKey == 0) ? SqlInt32.Null : item.VendorKey);
                        commandWrapper.AddInputParameter("@serviceKey", SqlDbType.Int, (item.ServiceKey == 0) ? SqlInt32.Null : item.ServiceKey);
                        commandWrapper.AddInputParameter("@sortOrder", SqlDbType.Float, (item.SortOrder == 0.0) ? SqlDouble.Null : item.SortOrder);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@vendorServiceKey", SqlDbType.Int);
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        db.ExecuteNonQuery(commandWrapper);

                        // save the newly inserted key value
                        item.VendorServiceKey = commandWrapper.GetValueInt("@vendorServiceKey");

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

        public virtual bool Update(VendorServiceModel item)
        {
        	bool status = false;

        	try
        	{
        	    string storedProcedure = "gensp_VendorService_UpdateOneByVendorServiceKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@vendorServiceKey", SqlDbType.Int, (item.VendorServiceKey == 0) ? SqlInt32.Null : item.VendorServiceKey);
                        commandWrapper.AddInputParameter("@vendorKey", SqlDbType.Int, (item.VendorKey == 0) ? SqlInt32.Null : item.VendorKey);
                        commandWrapper.AddInputParameter("@serviceKey", SqlDbType.Int, (item.ServiceKey == 0) ? SqlInt32.Null : item.ServiceKey);
                        commandWrapper.AddInputParameter("@sortOrder", SqlDbType.Float, (item.SortOrder == 0.0) ? SqlDouble.Null : item.SortOrder);

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
        	    string storedProcedure = "gensp_VendorService_DeleteOneByVendorServiceKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@vendorServiceKey", SqlDbType.Int, id);

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

        public virtual VendorServiceModel Get(int id)
        {
            VendorServiceModel item = null;

            try
            {
                string storedProcedure = "gensp_VendorService_SelectOneByVendorServiceKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@vendorServiceKey", SqlDbType.Int, id);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            while (dataReader.Read())
                            {
                                item = new VendorServiceModel();

                                Load(dataReader, item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_VendorService_SelectOneByVendorServiceKey");
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

        public virtual IList<VendorServiceModel> GetAll()
        {
            return GetAll(new VendorServiceFilterModel());
        }

        public virtual IList<VendorServiceModel> GetAll(VendorServiceFilterModel filter)
        {
            List<VendorServiceModel> itemList = new List<VendorServiceModel>();

            try
            {
                string storedProcedure = "gensp_VendorService_SelectSomeBySearch";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@vendorKey", SqlDbType.Int, (filter.VendorKey == 0) ? SqlInt32.Null : filter.VendorKey);
                        commandWrapper.AddInputParameter("@serviceKey", SqlDbType.Int, (filter.ServiceKey == 0) ? SqlInt32.Null : filter.ServiceKey);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            VendorServiceModel item = null;
                            while (dataReader.Read())
                            {
                                item = new VendorServiceModel();

                                Load(dataReader, item);

                                itemList.Add(item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_VendorService_SelectSomeBySearch");
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

        public virtual IList<VendorServiceModel> GetAll(VendorServiceFilterModel filter, PagingModel paging)
        {
            List<VendorServiceModel> itemList = new List<VendorServiceModel>();

            try
            {
                string storedProcedure = "gensp_VendorService_SelectSomeBySearchAndPaging";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@vendorKey", SqlDbType.Int, (filter.VendorKey == 0) ? SqlInt32.Null : filter.VendorKey);
                        commandWrapper.AddInputParameter("@serviceKey", SqlDbType.Int, (filter.ServiceKey == 0) ? SqlInt32.Null : filter.ServiceKey);

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
                            VendorServiceModel item = null;
                            while (dataReader.Read())
                            {
                                item = new VendorServiceModel();

                                Load(dataReader, item);

                                itemList.Add(item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_VendorService_SelectSomeBySearchAndPaging");
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

        protected void Load(DBDataReader dataReader, VendorServiceModel item)
        {
            item.VendorServiceKey = dataReader.GetValueInt("VendorServiceKey");
            item.VendorKey = dataReader.GetValueInt("VendorKey");
            item.ServiceKey = dataReader.GetValueInt("ServiceKey");
            item.SortOrder = dataReader.GetValueDouble("SortOrder");
        }
    }
}
