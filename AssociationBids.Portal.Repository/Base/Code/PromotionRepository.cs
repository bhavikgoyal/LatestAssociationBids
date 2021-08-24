using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public class PromotionRepository : BaseRepository, IPromotionRepository
    {
        public PromotionRepository() { }

        public PromotionRepository(string connectionString)
            : base(connectionString) { }

        public virtual bool Create(PromotionModel item)
        {
        	bool status = false;

        	try
        	{
        	    string storedProcedure = "gensp_Promotion_InsertOne";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@companyKey", SqlDbType.Int, (item.CompanyKey == 0) ? SqlInt32.Null : item.CompanyKey);
                        commandWrapper.AddInputParameter("@title", SqlDbType.VarChar, String.IsNullOrEmpty(item.Title) ? 0 : item.Title.Length, String.IsNullOrEmpty(item.Title) ? SqlString.Null : item.Title);
                        commandWrapper.AddInputParameter("@promotionCode", SqlDbType.VarChar, String.IsNullOrEmpty(item.PromotionCode) ? 0 : item.PromotionCode.Length, String.IsNullOrEmpty(item.PromotionCode) ? SqlString.Null : item.PromotionCode);
                        commandWrapper.AddInputParameter("@amount", SqlDbType.Money, (item.Amount == 0) ? SqlMoney.Null : item.Amount);
                        commandWrapper.AddInputParameter("@percentage", SqlDbType.Float, (item.Percentage == 0.0) ? SqlDouble.Null : item.Percentage);
                        commandWrapper.AddInputParameter("@startDate", SqlDbType.DateTime, (item.StartDate == DateTime.MinValue) ? SqlDateTime.Null : item.StartDate);
                        commandWrapper.AddInputParameter("@endDate", SqlDbType.DateTime, (item.EndDate == DateTime.MinValue) ? SqlDateTime.Null : item.EndDate);
                        commandWrapper.AddInputParameter("@lastModificationTime", SqlDbType.DateTime, (item.LastModificationTime == DateTime.MinValue) ? SqlDateTime.Null : item.LastModificationTime);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@promotionKey", SqlDbType.Int);
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        db.ExecuteNonQuery(commandWrapper);

                        // save the newly inserted key value
                        item.PromotionKey = commandWrapper.GetValueInt("@promotionKey");

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

        public virtual bool Update(PromotionModel item)
        {
        	bool status = false;

        	try
        	{
        	    string storedProcedure = "gensp_Promotion_UpdateOneByPromotionKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@promotionKey", SqlDbType.Int, (item.PromotionKey == 0) ? SqlInt32.Null : item.PromotionKey);
                        commandWrapper.AddInputParameter("@companyKey", SqlDbType.Int, (item.CompanyKey == 0) ? SqlInt32.Null : item.CompanyKey);
                        commandWrapper.AddInputParameter("@title", SqlDbType.VarChar, String.IsNullOrEmpty(item.Title) ? 0 : item.Title.Length, String.IsNullOrEmpty(item.Title) ? SqlString.Null : item.Title);
                        commandWrapper.AddInputParameter("@promotionCode", SqlDbType.VarChar, String.IsNullOrEmpty(item.PromotionCode) ? 0 : item.PromotionCode.Length, String.IsNullOrEmpty(item.PromotionCode) ? SqlString.Null : item.PromotionCode);
                        commandWrapper.AddInputParameter("@amount", SqlDbType.Money, (item.Amount == 0) ? SqlMoney.Null : item.Amount);
                        commandWrapper.AddInputParameter("@percentage", SqlDbType.Float, (item.Percentage == 0.0) ? SqlDouble.Null : item.Percentage);
                        commandWrapper.AddInputParameter("@startDate", SqlDbType.DateTime, (item.StartDate == DateTime.MinValue) ? SqlDateTime.Null : item.StartDate);
                        commandWrapper.AddInputParameter("@endDate", SqlDbType.DateTime, (item.EndDate == DateTime.MinValue) ? SqlDateTime.Null : item.EndDate);
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
        	    string storedProcedure = "gensp_Promotion_DeleteOneByPromotionKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@promotionKey", SqlDbType.Int, id);

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

        public virtual PromotionModel Get(int id)
        {
            PromotionModel item = null;

            try
            {
                string storedProcedure = "gensp_Promotion_SelectOneByPromotionKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@promotionKey", SqlDbType.Int, id);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            while (dataReader.Read())
                            {
                                item = new PromotionModel();

                                Load(dataReader, item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_Promotion_SelectOneByPromotionKey");
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

        public virtual IList<PromotionModel> GetAll()
        {
            return GetAll(new PromotionFilterModel());
        }

        public virtual IList<PromotionModel> GetAll(PromotionFilterModel filter)
        {
            List<PromotionModel> itemList = new List<PromotionModel>();

            try
            {
                string storedProcedure = "gensp_Promotion_SelectSomeBySearch";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@companyKey", SqlDbType.Int, (filter.CompanyKey == 0) ? SqlInt32.Null : filter.CompanyKey);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            PromotionModel item = null;
                            while (dataReader.Read())
                            {
                                item = new PromotionModel();

                                Load(dataReader, item);

                                itemList.Add(item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_Promotion_SelectSomeBySearch");
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

        public virtual IList<PromotionModel> GetAll(PromotionFilterModel filter, PagingModel paging)
        {
            List<PromotionModel> itemList = new List<PromotionModel>();

            try
            {
                string storedProcedure = "gensp_Promotion_SelectSomeBySearchAndPaging";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@companyKey", SqlDbType.Int, (filter.CompanyKey == 0) ? SqlInt32.Null : filter.CompanyKey);

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
                            PromotionModel item = null;
                            while (dataReader.Read())
                            {
                                item = new PromotionModel();

                                Load(dataReader, item);

                                itemList.Add(item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_Promotion_SelectSomeBySearchAndPaging");
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

        protected void Load(DBDataReader dataReader, PromotionModel item)
        {
            item.PromotionKey = dataReader.GetValueInt("PromotionKey");
            item.CompanyKey = dataReader.GetValueInt("CompanyKey");
            item.Title = dataReader.GetValueText("Title");
            item.PromotionCode = dataReader.GetValueText("PromotionCode");
            item.Amount = dataReader.GetValueDecimal("Amount");
            item.Percentage = dataReader.GetValueDouble("Percentage");
            item.StartDate = dataReader.GetValueDateTime("StartDate");
            item.EndDate = dataReader.GetValueDateTime("EndDate");
            item.LastModificationTime = dataReader.GetValueDateTime("LastModificationTime");
        }
    }
}
