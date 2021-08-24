using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public class PaymentRepository : BaseRepository, IPaymentRepository
    {
        public PaymentRepository() { }

        public PaymentRepository(string connectionString)
            : base(connectionString) { }

        public virtual bool Create(PaymentModel item)
        {
        	bool status = false;

        	try
        	{
        	    string storedProcedure = "gensp_Payment_InsertOne";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@vendorKey", SqlDbType.Int, (item.VendorKey == 0) ? SqlInt32.Null : item.VendorKey);
                        commandWrapper.AddInputParameter("@paymentTypeKey", SqlDbType.Int, (item.PaymentTypeKey == 0) ? SqlInt32.Null : item.PaymentTypeKey);
                        commandWrapper.AddInputParameter("@referenceNumber", SqlDbType.VarChar, String.IsNullOrEmpty(item.ReferenceNumber) ? 0 : item.ReferenceNumber.Length, String.IsNullOrEmpty(item.ReferenceNumber) ? SqlString.Null : item.ReferenceNumber);
                        commandWrapper.AddInputParameter("@transactionDate", SqlDbType.DateTime, (item.TransactionDate == DateTime.MinValue) ? SqlDateTime.Null : item.TransactionDate);
                        commandWrapper.AddInputParameter("@amount", SqlDbType.Money, (item.Amount == 0) ? SqlMoney.Null : item.Amount);
                        commandWrapper.AddInputParameter("@balance", SqlDbType.Money, (item.Balance == 0) ? SqlMoney.Null : item.Balance);
                        commandWrapper.AddInputParameter("@description", SqlDbType.VarChar, String.IsNullOrEmpty(item.Description) ? 0 : item.Description.Length, String.IsNullOrEmpty(item.Description) ? SqlString.Null : item.Description);
                        commandWrapper.AddInputParameter("@lastModificationTime", SqlDbType.DateTime, (item.LastModificationTime == DateTime.MinValue) ? SqlDateTime.Null : item.LastModificationTime);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@paymentKey", SqlDbType.Int);
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        db.ExecuteNonQuery(commandWrapper);

                        // save the newly inserted key value
                        item.PaymentKey = commandWrapper.GetValueInt("@paymentKey");

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

        public virtual bool Update(PaymentModel item)
        {
        	bool status = false;

        	try
        	{
        	    string storedProcedure = "gensp_Payment_UpdateOneByPaymentKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@paymentKey", SqlDbType.Int, (item.PaymentKey == 0) ? SqlInt32.Null : item.PaymentKey);
                        commandWrapper.AddInputParameter("@vendorKey", SqlDbType.Int, (item.VendorKey == 0) ? SqlInt32.Null : item.VendorKey);
                        commandWrapper.AddInputParameter("@paymentTypeKey", SqlDbType.Int, (item.PaymentTypeKey == 0) ? SqlInt32.Null : item.PaymentTypeKey);
                        commandWrapper.AddInputParameter("@referenceNumber", SqlDbType.VarChar, String.IsNullOrEmpty(item.ReferenceNumber) ? 0 : item.ReferenceNumber.Length, String.IsNullOrEmpty(item.ReferenceNumber) ? SqlString.Null : item.ReferenceNumber);
                        commandWrapper.AddInputParameter("@transactionDate", SqlDbType.DateTime, (item.TransactionDate == DateTime.MinValue) ? SqlDateTime.Null : item.TransactionDate);
                        commandWrapper.AddInputParameter("@amount", SqlDbType.Money, (item.Amount == 0) ? SqlMoney.Null : item.Amount);
                        commandWrapper.AddInputParameter("@balance", SqlDbType.Money, (item.Balance == 0) ? SqlMoney.Null : item.Balance);
                        commandWrapper.AddInputParameter("@description", SqlDbType.VarChar, String.IsNullOrEmpty(item.Description) ? 0 : item.Description.Length, String.IsNullOrEmpty(item.Description) ? SqlString.Null : item.Description);
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
        	    string storedProcedure = "gensp_Payment_DeleteOneByPaymentKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@paymentKey", SqlDbType.Int, id);

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

        public virtual PaymentModel Get(int id)
        {
            PaymentModel item = null;

            try
            {
                string storedProcedure = "gensp_Payment_SelectOneByPaymentKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@paymentKey", SqlDbType.Int, id);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            while (dataReader.Read())
                            {
                                item = new PaymentModel();

                                Load(dataReader, item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_Payment_SelectOneByPaymentKey");
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

        public virtual IList<PaymentModel> GetAll()
        {
            return GetAll(new PaymentFilterModel());
        }

        public virtual IList<PaymentModel> GetAll(PaymentFilterModel filter)
        {
            List<PaymentModel> itemList = new List<PaymentModel>();

            try
            {
                string storedProcedure = "gensp_Payment_SelectSomeBySearch";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@vendorKey", SqlDbType.Int, (filter.VendorKey == 0) ? SqlInt32.Null : filter.VendorKey);
                        commandWrapper.AddInputParameter("@paymentTypeKey", SqlDbType.Int, (filter.PaymentTypeKey == 0) ? SqlInt32.Null : filter.PaymentTypeKey);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            PaymentModel item = null;
                            while (dataReader.Read())
                            {
                                item = new PaymentModel();

                                Load(dataReader, item);

                                itemList.Add(item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_Payment_SelectSomeBySearch");
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

        public virtual IList<PaymentModel> GetAll(PaymentFilterModel filter, PagingModel paging)
        {
            List<PaymentModel> itemList = new List<PaymentModel>();

            try
            {
                string storedProcedure = "gensp_Payment_SelectSomeBySearchAndPaging";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@vendorKey", SqlDbType.Int, (filter.VendorKey == 0) ? SqlInt32.Null : filter.VendorKey);
                        commandWrapper.AddInputParameter("@paymentTypeKey", SqlDbType.Int, (filter.PaymentTypeKey == 0) ? SqlInt32.Null : filter.PaymentTypeKey);

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
                            PaymentModel item = null;
                            while (dataReader.Read())
                            {
                                item = new PaymentModel();

                                Load(dataReader, item);

                                itemList.Add(item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_Payment_SelectSomeBySearchAndPaging");
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

        protected void Load(DBDataReader dataReader, PaymentModel item)
        {
            item.PaymentKey = dataReader.GetValueInt("PaymentKey");
            item.VendorKey = dataReader.GetValueInt("VendorKey");
            item.PaymentTypeKey = dataReader.GetValueInt("PaymentTypeKey");
            item.ReferenceNumber = dataReader.GetValueText("ReferenceNumber");
            item.TransactionDate = dataReader.GetValueDateTime("TransactionDate");
            item.Amount = dataReader.GetValueDecimal("Amount");
            item.Balance = dataReader.GetValueDecimal("Balance");
            item.Description = dataReader.GetValueText("Description");
            item.LastModificationTime = dataReader.GetValueDateTime("LastModificationTime");
        }
    }
}
