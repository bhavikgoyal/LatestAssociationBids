using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public class PricingRepository : BaseRepository, IPricingRepository
    {
        public PricingRepository() { }

        public PricingRepository(string connectionString)
            : base(connectionString) { }

        public virtual bool Create(PricingModel item)
        {
        	bool status = false;

        	try
        	{
        	    string storedProcedure = "gensp_Pricing_InsertOne";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@companyKey", SqlDbType.Int, (item.CompanyKey == 0) ? SqlInt32.Null : item.CompanyKey);
                        commandWrapper.AddInputParameter("@pricingTypeKey", SqlDbType.Int, (item.PricingTypeKey == 0) ? SqlInt32.Null : item.PricingTypeKey);
                        commandWrapper.AddInputParameter("@startAmount", SqlDbType.Money, (item.StartAmount == 0) ? SqlMoney.Null : item.StartAmount);
                        commandWrapper.AddInputParameter("@endAmount", SqlDbType.Money, (item.EndAmount == 0) ? SqlMoney.Null : item.EndAmount);
                        commandWrapper.AddInputParameter("@fee", SqlDbType.Money, (item.Fee == 0) ? SqlMoney.Null : item.Fee);
                        commandWrapper.AddInputParameter("@lastModificationTime", SqlDbType.DateTime, (item.LastModificationTime == DateTime.MinValue) ? SqlDateTime.Null : item.LastModificationTime);
                        commandWrapper.AddInputParameter("@sortOrder", SqlDbType.Float, (item.SortOrder == 0.0) ? SqlDouble.Null : item.SortOrder);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@pricingKey", SqlDbType.Int);
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        db.ExecuteNonQuery(commandWrapper);

                        // save the newly inserted key value
                        item.PricingKey = commandWrapper.GetValueInt("@pricingKey");

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

        public virtual bool Update(PricingModel item)
        {
        	bool status = false;

        	try
        	{
        	    string storedProcedure = "gensp_Pricing_UpdateOneByPricingKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@pricingKey", SqlDbType.Int, (item.PricingKey == 0) ? SqlInt32.Null : item.PricingKey);
                        commandWrapper.AddInputParameter("@companyKey", SqlDbType.Int, (item.CompanyKey == 0) ? SqlInt32.Null : item.CompanyKey);
                        commandWrapper.AddInputParameter("@pricingTypeKey", SqlDbType.Int, (item.PricingTypeKey == 0) ? SqlInt32.Null : item.PricingTypeKey);
                        commandWrapper.AddInputParameter("@startAmount", SqlDbType.Money, (item.StartAmount == 0) ? SqlMoney.Null : item.StartAmount);
                        commandWrapper.AddInputParameter("@endAmount", SqlDbType.Money, (item.EndAmount == 0) ? SqlMoney.Null : item.EndAmount);
                        commandWrapper.AddInputParameter("@fee", SqlDbType.Money, (item.Fee == 0) ? SqlMoney.Null : item.Fee);
                        commandWrapper.AddInputParameter("@lastModificationTime", SqlDbType.DateTime, (item.LastModificationTime == DateTime.MinValue) ? SqlDateTime.Null : item.LastModificationTime);
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
        	    string storedProcedure = "gensp_Pricing_DeleteOneByPricingKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@pricingKey", SqlDbType.Int, id);

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

        public virtual PricingModel Get(int id)
        {
            PricingModel item = null;

            try
            {
                string storedProcedure = "gensp_Pricing_SelectOneByPricingKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@pricingKey", SqlDbType.Int, id);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            while (dataReader.Read())
                            {
                                item = new PricingModel();

                                Load(dataReader, item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_Pricing_SelectOneByPricingKey");
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

        public virtual IList<PricingModel> GetAll()
        {
            return GetAll(new PricingFilterModel());
        }

        public virtual IList<PricingModel> GetAll(PricingFilterModel filter)
        {
            List<PricingModel> itemList = new List<PricingModel>();

            try
            {
                string storedProcedure = "gensp_Pricing_SelectSomeBySearch";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@companyKey", SqlDbType.Int, (filter.CompanyKey == 0) ? SqlInt32.Null : filter.CompanyKey);
                        commandWrapper.AddInputParameter("@pricingTypeKey", SqlDbType.Int, (filter.PricingTypeKey == 0) ? SqlInt32.Null : filter.PricingTypeKey);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            PricingModel item = null;
                            while (dataReader.Read())
                            {
                                item = new PricingModel();

                                Load(dataReader, item);

                                itemList.Add(item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_Pricing_SelectSomeBySearch");
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

        public virtual IList<PricingModel> GetAll(PricingFilterModel filter, PagingModel paging)
        {
            List<PricingModel> itemList = new List<PricingModel>();

            try
            {
                string storedProcedure = "gensp_Pricing_SelectSomeBySearchAndPaging";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@companyKey", SqlDbType.Int, (filter.CompanyKey == 0) ? SqlInt32.Null : filter.CompanyKey);
                        commandWrapper.AddInputParameter("@pricingTypeKey", SqlDbType.Int, (filter.PricingTypeKey == 0) ? SqlInt32.Null : filter.PricingTypeKey);

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
                            PricingModel item = null;
                            while (dataReader.Read())
                            {
                                item = new PricingModel();

                                Load(dataReader, item);

                                itemList.Add(item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_Pricing_SelectSomeBySearchAndPaging");
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

        protected void Load(DBDataReader dataReader, PricingModel item)
        {
            item.PricingKey = dataReader.GetValueInt("PricingKey");
            item.CompanyKey = dataReader.GetValueInt("CompanyKey");
            item.PricingTypeKey = dataReader.GetValueInt("PricingTypeKey");
            item.StartAmount = dataReader.GetValueDecimal("StartAmount");
            item.EndAmount = dataReader.GetValueDecimal("EndAmount");
            item.Fee = dataReader.GetValueDecimal("Fee");
            item.LastModificationTime = dataReader.GetValueDateTime("LastModificationTime");
            item.SortOrder = dataReader.GetValueDouble("SortOrder");
        }
    }
}
