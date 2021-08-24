using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public class InvoiceRepository : BaseRepository, IInvoiceRepository
    {
        public InvoiceRepository() { }

        public InvoiceRepository(string connectionString)
            : base(connectionString) { }

        public virtual bool Create(InvoiceModel item)
        {
        	bool status = false;

        	try
        	{
        	    string storedProcedure = "gensp_Invoice_InsertOne";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@vendorKey", SqlDbType.Int, (item.VendorKey == 0) ? SqlInt32.Null : item.VendorKey);
                        commandWrapper.AddInputParameter("@referenceNumber", SqlDbType.VarChar, String.IsNullOrEmpty(item.ReferenceNumber) ? 0 : item.ReferenceNumber.Length, String.IsNullOrEmpty(item.ReferenceNumber) ? SqlString.Null : item.ReferenceNumber);
                        commandWrapper.AddInputParameter("@transactionDate", SqlDbType.DateTime, (item.TransactionDate == DateTime.MinValue) ? SqlDateTime.Null : item.TransactionDate);
                        commandWrapper.AddInputParameter("@dueDate", SqlDbType.DateTime, (item.DueDate == DateTime.MinValue) ? SqlDateTime.Null : item.DueDate);
                        commandWrapper.AddInputParameter("@amount", SqlDbType.Money, (item.Amount == 0) ? SqlMoney.Null : item.Amount);
                        commandWrapper.AddInputParameter("@balance", SqlDbType.Money, (item.Balance == 0) ? SqlMoney.Null : item.Balance);
                        commandWrapper.AddInputParameter("@lastModificationTime", SqlDbType.DateTime, (item.LastModificationTime == DateTime.MinValue) ? SqlDateTime.Null : item.LastModificationTime);
                        commandWrapper.AddInputParameter("@status", SqlDbType.Int, (item.Status == 0) ? SqlInt32.Null : item.Status);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@invoiceKey", SqlDbType.Int);
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        db.ExecuteNonQuery(commandWrapper);

                        // save the newly inserted key value
                        item.InvoiceKey = commandWrapper.GetValueInt("@invoiceKey");

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

        public virtual bool Update(InvoiceModel item)
        {
        	bool status = false;

        	try
        	{
        	    string storedProcedure = "gensp_Invoice_UpdateOneByInvoiceKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@invoiceKey", SqlDbType.Int, (item.InvoiceKey == 0) ? SqlInt32.Null : item.InvoiceKey);
                        commandWrapper.AddInputParameter("@vendorKey", SqlDbType.Int, (item.VendorKey == 0) ? SqlInt32.Null : item.VendorKey);
                        commandWrapper.AddInputParameter("@referenceNumber", SqlDbType.VarChar, String.IsNullOrEmpty(item.ReferenceNumber) ? 0 : item.ReferenceNumber.Length, String.IsNullOrEmpty(item.ReferenceNumber) ? SqlString.Null : item.ReferenceNumber);
                        commandWrapper.AddInputParameter("@transactionDate", SqlDbType.DateTime, (item.TransactionDate == DateTime.MinValue) ? SqlDateTime.Null : item.TransactionDate);
                        commandWrapper.AddInputParameter("@dueDate", SqlDbType.DateTime, (item.DueDate == DateTime.MinValue) ? SqlDateTime.Null : item.DueDate);
                        commandWrapper.AddInputParameter("@amount", SqlDbType.Money, (item.Amount == 0) ? SqlMoney.Null : item.Amount);
                        commandWrapper.AddInputParameter("@balance", SqlDbType.Money, (item.Balance == 0) ? SqlMoney.Null : item.Balance);
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
        	    string storedProcedure = "gensp_Invoice_DeleteOneByInvoiceKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@invoiceKey", SqlDbType.Int, id);

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

        public virtual InvoiceModel Get(int id)
        {
            InvoiceModel item = null;

            try
            {
                string storedProcedure = "gensp_Invoice_SelectOneByInvoiceKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@invoiceKey", SqlDbType.Int, id);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            while (dataReader.Read())
                            {
                                item = new InvoiceModel();

                                Load(dataReader, item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_Invoice_SelectOneByInvoiceKey");
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

        public virtual IList<InvoiceModel> GetAll()
        {
            return GetAll(new InvoiceFilterModel());
        }

        public virtual IList<InvoiceModel> GetAll(InvoiceFilterModel filter)
        {
            List<InvoiceModel> itemList = new List<InvoiceModel>();

            try
            {
                string storedProcedure = "gensp_Invoice_SelectSomeBySearch";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@vendorKey", SqlDbType.Int, (filter.VendorKey == 0) ? SqlInt32.Null : filter.VendorKey);
                        commandWrapper.AddInputParameter("@status", SqlDbType.Int, (filter.Status == 0) ? SqlInt32.Null : filter.Status);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            InvoiceModel item = null;
                            while (dataReader.Read())
                            {
                                item = new InvoiceModel();

                                Load(dataReader, item);

                                itemList.Add(item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_Invoice_SelectSomeBySearch");
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

        public virtual IList<InvoiceModel> GetAll(InvoiceFilterModel filter, PagingModel paging)
        {
            List<InvoiceModel> itemList = new List<InvoiceModel>();

            try
            {
                string storedProcedure = "gensp_Invoice_SelectSomeBySearchAndPaging";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@vendorKey", SqlDbType.Int, (filter.VendorKey == 0) ? SqlInt32.Null : filter.VendorKey);
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
                            InvoiceModel item = null;
                            while (dataReader.Read())
                            {
                                item = new InvoiceModel();

                                Load(dataReader, item);

                                itemList.Add(item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_Invoice_SelectSomeBySearchAndPaging");
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

        protected void Load(DBDataReader dataReader, InvoiceModel item)
        {
            item.InvoiceKey = dataReader.GetValueInt("InvoiceKey");
            item.VendorKey = dataReader.GetValueInt("VendorKey");
            item.ReferenceNumber = dataReader.GetValueText("ReferenceNumber");
            item.TransactionDate = dataReader.GetValueDateTime("TransactionDate");
            item.DueDate = dataReader.GetValueDateTime("DueDate");
            item.Amount = dataReader.GetValueDecimal("Amount");
            item.Balance = dataReader.GetValueDecimal("Balance");
            item.LastModificationTime = dataReader.GetValueDateTime("LastModificationTime");
            item.Status = dataReader.GetValueInt("Status");
        }
    }
}
