using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public class BidVendorRepository : BaseRepository, IBidVendorRepository
    {
        public BidVendorRepository() { }

        public BidVendorRepository(string connectionString)
            : base(connectionString) { }

        public virtual bool Create(BidVendorModel item)
        {
        	bool status = false;

        	try
        	{
        	    string storedProcedure = "gensp_BidVendor_InsertOne";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@bidRequestKey", SqlDbType.Int, (item.BidRequestKey == 0) ? SqlInt32.Null : item.BidRequestKey);
                        commandWrapper.AddInputParameter("@vendorKey", SqlDbType.Int, (item.VendorKey == 0) ? SqlInt32.Null : item.VendorKey);
                        commandWrapper.AddInputParameter("@resourceKey", SqlDbType.Int, (item.ResourceKey == 0) ? SqlInt32.Null : item.ResourceKey);
                        commandWrapper.AddInputParameter("@bidVendorID", SqlDbType.VarChar, String.IsNullOrEmpty(item.BidVendorID) ? 0 : item.BidVendorID.Length, String.IsNullOrEmpty(item.BidVendorID) ? SqlString.Null : item.BidVendorID);
                        commandWrapper.AddInputParameter("@isAssigned", SqlDbType.Bit, (item.IsAssigned == false) ? SqlBoolean.Null : item.IsAssigned);
                        commandWrapper.AddInputParameter("@respondByDate", SqlDbType.DateTime, (item.RespondByDate == DateTime.MinValue) ? SqlDateTime.Null : item.RespondByDate);
                        commandWrapper.AddInputParameter("@dateAdded", SqlDbType.DateTime, (item.DateAdded == DateTime.MinValue) ? SqlDateTime.Null : item.DateAdded);
                        commandWrapper.AddInputParameter("@lastModificationTime", SqlDbType.DateTime, (item.LastModificationTime == DateTime.MinValue) ? SqlDateTime.Null : item.LastModificationTime);
                        commandWrapper.AddInputParameter("@bidVendorStatus", SqlDbType.Int, (item.BidVendorStatus == 0) ? SqlInt32.Null : item.BidVendorStatus);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@bidVendorKey", SqlDbType.Int);
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        db.ExecuteNonQuery(commandWrapper);

                        // save the newly inserted key value
                        item.BidVendorKey = commandWrapper.GetValueInt("@bidVendorKey");

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

        public virtual bool Update(BidVendorModel item)
        {
        	bool status = false;

        	try
        	{
        	    string storedProcedure = "gensp_BidVendor_UpdateOneByBidVendorKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@bidVendorKey", SqlDbType.Int, (item.BidVendorKey == 0) ? SqlInt32.Null : item.BidVendorKey);
                        commandWrapper.AddInputParameter("@bidRequestKey", SqlDbType.Int, (item.BidRequestKey == 0) ? SqlInt32.Null : item.BidRequestKey);
                        commandWrapper.AddInputParameter("@vendorKey", SqlDbType.Int, (item.VendorKey == 0) ? SqlInt32.Null : item.VendorKey);
                        commandWrapper.AddInputParameter("@resourceKey", SqlDbType.Int, (item.ResourceKey == 0) ? SqlInt32.Null : item.ResourceKey);
                        commandWrapper.AddInputParameter("@bidVendorID", SqlDbType.VarChar, String.IsNullOrEmpty(item.BidVendorID) ? 0 : item.BidVendorID.Length, String.IsNullOrEmpty(item.BidVendorID) ? SqlString.Null : item.BidVendorID);
                        commandWrapper.AddInputParameter("@isAssigned", SqlDbType.Bit, (item.IsAssigned == false) ? SqlBoolean.Null : item.IsAssigned);
                        commandWrapper.AddInputParameter("@respondByDate", SqlDbType.DateTime, (item.RespondByDate == DateTime.MinValue) ? SqlDateTime.Null : item.RespondByDate);
                        commandWrapper.AddInputParameter("@dateAdded", SqlDbType.DateTime, (item.DateAdded == DateTime.MinValue) ? SqlDateTime.Null : item.DateAdded);
                        commandWrapper.AddInputParameter("@lastModificationTime", SqlDbType.DateTime, (item.LastModificationTime == DateTime.MinValue) ? SqlDateTime.Null : item.LastModificationTime);
                        commandWrapper.AddInputParameter("@bidVendorStatus", SqlDbType.Int, (item.BidVendorStatus == 0) ? SqlInt32.Null : item.BidVendorStatus);

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
        	    string storedProcedure = "gensp_BidVendor_DeleteOneByBidVendorKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@bidVendorKey", SqlDbType.Int, id);

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

        public virtual BidVendorModel Get(int id)
        {
            BidVendorModel item = null;

            try
            {
                string storedProcedure = "gensp_BidVendor_SelectOneByBidVendorKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@bidVendorKey", SqlDbType.Int, id);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            while (dataReader.Read())
                            {
                                item = new BidVendorModel();

                                Load(dataReader, item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_BidVendor_SelectOneByBidVendorKey");
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

        public virtual IList<BidVendorModel> GetAll()
        {
            return GetAll(new BidVendorFilterModel());
        }

        public virtual IList<BidVendorModel> GetAll(BidVendorFilterModel filter)
        {
            List<BidVendorModel> itemList = new List<BidVendorModel>();

            try
            {
                string storedProcedure = "gensp_BidVendor_SelectSomeBySearch";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@bidRequestKey", SqlDbType.Int, (filter.BidRequestKey == 0) ? SqlInt32.Null : filter.BidRequestKey);
                        commandWrapper.AddInputParameter("@vendorKey", SqlDbType.Int, (filter.VendorKey == 0) ? SqlInt32.Null : filter.VendorKey);
                        commandWrapper.AddInputParameter("@resourceKey", SqlDbType.Int, (filter.ResourceKey == 0) ? SqlInt32.Null : filter.ResourceKey);
                        commandWrapper.AddInputParameter("@bidVendorStatus", SqlDbType.Int, (filter.BidVendorStatus == 0) ? SqlInt32.Null : filter.BidVendorStatus);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            BidVendorModel item = null;
                            while (dataReader.Read())
                            {
                                item = new BidVendorModel();

                                Load(dataReader, item);

                                itemList.Add(item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_BidVendor_SelectSomeBySearch");
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

        public virtual IList<BidVendorModel> GetAll(BidVendorFilterModel filter, PagingModel paging)
        {
            List<BidVendorModel> itemList = new List<BidVendorModel>();

            try
            {
                string storedProcedure = "gensp_BidVendor_SelectSomeBySearchAndPaging";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@bidRequestKey", SqlDbType.Int, (filter.BidRequestKey == 0) ? SqlInt32.Null : filter.BidRequestKey);
                        commandWrapper.AddInputParameter("@vendorKey", SqlDbType.Int, (filter.VendorKey == 0) ? SqlInt32.Null : filter.VendorKey);
                        commandWrapper.AddInputParameter("@resourceKey", SqlDbType.Int, (filter.ResourceKey == 0) ? SqlInt32.Null : filter.ResourceKey);
                        commandWrapper.AddInputParameter("@bidVendorStatus", SqlDbType.Int, (filter.BidVendorStatus == 0) ? SqlInt32.Null : filter.BidVendorStatus);

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
                            BidVendorModel item = null;
                            while (dataReader.Read())
                            {
                                item = new BidVendorModel();

                                Load(dataReader, item);

                                itemList.Add(item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_BidVendor_SelectSomeBySearchAndPaging");
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

        protected void Load(DBDataReader dataReader, BidVendorModel item)
        {
            item.BidVendorKey = dataReader.GetValueInt("BidVendorKey");
            item.BidRequestKey = dataReader.GetValueInt("BidRequestKey");
            item.VendorKey = dataReader.GetValueInt("VendorKey");
            item.ResourceKey = dataReader.GetValueInt("ResourceKey");
            item.BidVendorID = dataReader.GetValueText("BidVendorID");
            item.IsAssigned = dataReader.GetValueBool("IsAssigned");
            item.RespondByDate = dataReader.GetValueDateTime("RespondByDate");
            item.DateAdded = dataReader.GetValueDateTime("DateAdded");
            item.LastModificationTime = dataReader.GetValueDateTime("LastModificationTime");
            item.BidVendorStatus = dataReader.GetValueInt("BidVendorStatus");
        }
    }
}
