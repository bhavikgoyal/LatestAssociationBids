using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public class VendorRatingRepository : BaseRepository, IVendorRatingRepository
    {
        public VendorRatingRepository() { }

        public VendorRatingRepository(string connectionString)
            : base(connectionString) { }

        public virtual bool Create(VendorRatingModel item)
        {
        	bool status = false;

        	try
        	{
        	    string storedProcedure = "gensp_VendorRating_InsertOne";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@vendorKey", SqlDbType.Int, (item.VendorKey == 0) ? SqlInt32.Null : item.VendorKey);
                        commandWrapper.AddInputParameter("@resourceKey", SqlDbType.Int, (item.ResourceKey == 0) ? SqlInt32.Null : item.ResourceKey);
                        commandWrapper.AddInputParameter("@ratingOne", SqlDbType.Int, (item.RatingOne == 0) ? SqlInt32.Null : item.RatingOne);
                        commandWrapper.AddInputParameter("@ratingTwo", SqlDbType.Int, (item.RatingTwo == 0) ? SqlInt32.Null : item.RatingTwo);
                        commandWrapper.AddInputParameter("@ratingThree", SqlDbType.Int, (item.RatingThree == 0) ? SqlInt32.Null : item.RatingThree);
                        commandWrapper.AddInputParameter("@ratingFour", SqlDbType.Int, (item.RatingFour == 0) ? SqlInt32.Null : item.RatingFour);
                        commandWrapper.AddInputParameter("@ratingFive", SqlDbType.Int, (item.RatingFive == 0) ? SqlInt32.Null : item.RatingFive);
                        commandWrapper.AddInputParameter("@lastModificationTime", SqlDbType.DateTime, (item.LastModificationTime == DateTime.MinValue) ? SqlDateTime.Null : item.LastModificationTime);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@vendorRatingKey", SqlDbType.Int);
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        db.ExecuteNonQuery(commandWrapper);

                        // save the newly inserted key value
                        item.VendorRatingKey = commandWrapper.GetValueInt("@vendorRatingKey");

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

        public virtual bool Update(VendorRatingModel item)
        {
        	bool status = false;

        	try
        	{
        	    string storedProcedure = "gensp_VendorRating_UpdateOneByVendorRatingKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@vendorRatingKey", SqlDbType.Int, (item.VendorRatingKey == 0) ? SqlInt32.Null : item.VendorRatingKey);
                        commandWrapper.AddInputParameter("@vendorKey", SqlDbType.Int, (item.VendorKey == 0) ? SqlInt32.Null : item.VendorKey);
                        commandWrapper.AddInputParameter("@resourceKey", SqlDbType.Int, (item.ResourceKey == 0) ? SqlInt32.Null : item.ResourceKey);
                        commandWrapper.AddInputParameter("@ratingOne", SqlDbType.Int, (item.RatingOne == 0) ? SqlInt32.Null : item.RatingOne);
                        commandWrapper.AddInputParameter("@ratingTwo", SqlDbType.Int, (item.RatingTwo == 0) ? SqlInt32.Null : item.RatingTwo);
                        commandWrapper.AddInputParameter("@ratingThree", SqlDbType.Int, (item.RatingThree == 0) ? SqlInt32.Null : item.RatingThree);
                        commandWrapper.AddInputParameter("@ratingFour", SqlDbType.Int, (item.RatingFour == 0) ? SqlInt32.Null : item.RatingFour);
                        commandWrapper.AddInputParameter("@ratingFive", SqlDbType.Int, (item.RatingFive == 0) ? SqlInt32.Null : item.RatingFive);
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
                string storedProcedure = "gensp_VendorRating_DeleteOneByVendorRatingKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@vendorRatingKey", SqlDbType.Int, id);

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

        public virtual VendorRatingModel Get(int id)
        {
            VendorRatingModel item = null;

            try
            {
                string storedProcedure = "gensp_VendorRating_SelectOneByVendorRatingKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@vendorRatingKey", SqlDbType.Int, id);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            while (dataReader.Read())
                            {
                                item = new VendorRatingModel();

                                Load(dataReader, item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_VendorRating_SelectOneByVendorRatingKey");
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

        public virtual IList<VendorRatingModel> GetAll()
        {
            return GetAll(new VendorRatingFilterModel());
        }

        public virtual IList<VendorRatingModel> GetAll(VendorRatingFilterModel filter)
        {
            List<VendorRatingModel> itemList = new List<VendorRatingModel>();

            try
            {
                string storedProcedure = "gensp_VendorRating_SelectSomeBySearch";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@vendorKey", SqlDbType.Int, (filter.VendorKey == 0) ? SqlInt32.Null : filter.VendorKey);
                        commandWrapper.AddInputParameter("@resourceKey", SqlDbType.Int, (filter.ResourceKey == 0) ? SqlInt32.Null : filter.ResourceKey);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            VendorRatingModel item = null;
                            while (dataReader.Read())
                            {
                                item = new VendorRatingModel();

                                Load(dataReader, item);

                                itemList.Add(item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_VendorRating_SelectSomeBySearch");
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

        public virtual IList<VendorRatingModel> GetAll(VendorRatingFilterModel filter, PagingModel paging)
        {
            List<VendorRatingModel> itemList = new List<VendorRatingModel>();

            try
            {
                string storedProcedure = "gensp_VendorRating_SelectSomeBySearchAndPaging";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@vendorKey", SqlDbType.Int, (filter.VendorKey == 0) ? SqlInt32.Null : filter.VendorKey);
                        commandWrapper.AddInputParameter("@resourceKey", SqlDbType.Int, (filter.ResourceKey == 0) ? SqlInt32.Null : filter.ResourceKey);

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
                            VendorRatingModel item = null;
                            while (dataReader.Read())
                            {
                                item = new VendorRatingModel();

                                Load(dataReader, item);

                                itemList.Add(item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_VendorRating_SelectSomeBySearchAndPaging");
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

        protected void Load(DBDataReader dataReader, VendorRatingModel item)
        {
            item.VendorRatingKey = dataReader.GetValueInt("VendorRatingKey");
            item.VendorKey = dataReader.GetValueInt("VendorKey");
            item.ResourceKey = dataReader.GetValueInt("ResourceKey");
            item.RatingOne = dataReader.GetValueInt("RatingOne");
            item.RatingTwo = dataReader.GetValueInt("RatingTwo");
            item.RatingThree = dataReader.GetValueInt("RatingThree");
            item.RatingFour = dataReader.GetValueInt("RatingFour");
            item.RatingFive = dataReader.GetValueInt("RatingFive");
            item.LastModificationTime = dataReader.GetValueDateTime("LastModificationTime");
        }
    }
}
