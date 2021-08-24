using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public class InvoiceLineRepository : BaseRepository, IInvoiceLineRepository
    {
        public InvoiceLineRepository() { }

        public InvoiceLineRepository(string connectionString)
            : base(connectionString) { }

        public virtual bool Create(InvoiceLineModel item)
        {
        	bool status = false;

        	try
        	{
        	    string storedProcedure = "gensp_InvoiceLine_InsertOne";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@invoiceKey", SqlDbType.Int, (item.InvoiceKey == 0) ? SqlInt32.Null : item.InvoiceKey);
                        commandWrapper.AddInputParameter("@quantity", SqlDbType.Int, (item.Quantity == 0) ? SqlInt32.Null : item.Quantity);
                        commandWrapper.AddInputParameter("@rate", SqlDbType.Money, (item.Rate == 0) ? SqlMoney.Null : item.Rate);
                        commandWrapper.AddInputParameter("@amount", SqlDbType.Money, (item.Amount == 0) ? SqlMoney.Null : item.Amount);
                        commandWrapper.AddInputParameter("@description", SqlDbType.VarChar, String.IsNullOrEmpty(item.Description) ? 0 : item.Description.Length, String.IsNullOrEmpty(item.Description) ? SqlString.Null : item.Description);
                        commandWrapper.AddInputParameter("@sortOrder", SqlDbType.Float, (item.SortOrder == 0.0) ? SqlDouble.Null : item.SortOrder);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@invoiceLineKey", SqlDbType.Int);
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        db.ExecuteNonQuery(commandWrapper);

                        // save the newly inserted key value
                        item.InvoiceLineKey = commandWrapper.GetValueInt("@invoiceLineKey");

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

        public virtual bool Update(InvoiceLineModel item)
        {
        	bool status = false;

            try
            {
                string storedProcedure = "gensp_InvoiceLine_UpdateOneByInvoiceLineKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@invoiceLineKey", SqlDbType.Int, (item.InvoiceLineKey == 0) ? SqlInt32.Null : item.InvoiceLineKey);
                        commandWrapper.AddInputParameter("@invoiceKey", SqlDbType.Int, (item.InvoiceKey == 0) ? SqlInt32.Null : item.InvoiceKey);
                        commandWrapper.AddInputParameter("@quantity", SqlDbType.Int, (item.Quantity == 0) ? SqlInt32.Null : item.Quantity);
                        commandWrapper.AddInputParameter("@rate", SqlDbType.Money, (item.Rate == 0) ? SqlMoney.Null : item.Rate);
                        commandWrapper.AddInputParameter("@amount", SqlDbType.Money, (item.Amount == 0) ? SqlMoney.Null : item.Amount);
                        commandWrapper.AddInputParameter("@description", SqlDbType.VarChar, String.IsNullOrEmpty(item.Description) ? 0 : item.Description.Length, String.IsNullOrEmpty(item.Description) ? SqlString.Null : item.Description);
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
        	    string storedProcedure = "gensp_InvoiceLine_DeleteOneByInvoiceLineKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@invoiceLineKey", SqlDbType.Int, id);

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

        public virtual InvoiceLineModel Get(int id)
        {
            InvoiceLineModel item = null;

            try
            {
                string storedProcedure = "gensp_InvoiceLine_SelectOneByInvoiceLineKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@invoiceLineKey", SqlDbType.Int, id);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            while (dataReader.Read())
                            {
                                item = new InvoiceLineModel();

                                Load(dataReader, item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_InvoiceLine_SelectOneByInvoiceLineKey");
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

        public virtual IList<InvoiceLineModel> GetAll()
        {
            return GetAll(new InvoiceLineFilterModel());
        }

        public virtual IList<InvoiceLineModel> GetAll(InvoiceLineFilterModel filter)
        {
            List<InvoiceLineModel> itemList = new List<InvoiceLineModel>();

            try
            {
                string storedProcedure = "gensp_InvoiceLine_SelectSomeBySearch";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@invoiceKey", SqlDbType.Int, (filter.InvoiceKey == 0) ? SqlInt32.Null : filter.InvoiceKey);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            InvoiceLineModel item = null;
                            while (dataReader.Read())
                            {
                                item = new InvoiceLineModel();

                                Load(dataReader, item);

                                itemList.Add(item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_InvoiceLine_SelectSomeBySearch");
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

        public virtual IList<InvoiceLineModel> GetAll(InvoiceLineFilterModel filter, PagingModel paging)
        {
            List<InvoiceLineModel> itemList = new List<InvoiceLineModel>();

            try
            {
                string storedProcedure = "gensp_InvoiceLine_SelectSomeBySearchAndPaging";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@invoiceKey", SqlDbType.Int, (filter.InvoiceKey == 0) ? SqlInt32.Null : filter.InvoiceKey);

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
                            InvoiceLineModel item = null;
                            while (dataReader.Read())
                            {
                                item = new InvoiceLineModel();

                                Load(dataReader, item);

                                itemList.Add(item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_InvoiceLine_SelectSomeBySearchAndPaging");
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

        protected void Load(DBDataReader dataReader, InvoiceLineModel item)
        {
            item.InvoiceLineKey = dataReader.GetValueInt("InvoiceLineKey");
            item.InvoiceKey = dataReader.GetValueInt("InvoiceKey");
            item.Quantity = dataReader.GetValueInt("Quantity");
            item.Rate = dataReader.GetValueDecimal("Rate");
            item.Amount = dataReader.GetValueDecimal("Amount");
            item.Description = dataReader.GetValueText("Description");
            item.SortOrder = dataReader.GetValueDouble("SortOrder");
        }
    }
}
