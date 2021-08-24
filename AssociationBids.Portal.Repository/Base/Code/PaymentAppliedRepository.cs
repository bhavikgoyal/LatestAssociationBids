using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public class PaymentAppliedRepository : BaseRepository, IPaymentAppliedRepository
    {
        public PaymentAppliedRepository() { }

        public PaymentAppliedRepository(string connectionString)
            : base(connectionString) { }

        public virtual bool Create(PaymentAppliedModel item)
        {
        	bool status = false;

        	try
        	{
        	    string storedProcedure = "gensp_PaymentApplied_InsertOne";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@paymentKey", SqlDbType.Int, (item.PaymentKey == 0) ? SqlInt32.Null : item.PaymentKey);
                        commandWrapper.AddInputParameter("@invociceKey", SqlDbType.Int, (item.InvociceKey == 0) ? SqlInt32.Null : item.InvociceKey);
                        commandWrapper.AddInputParameter("@amount", SqlDbType.Money, (item.Amount == 0) ? SqlMoney.Null : item.Amount);
                        commandWrapper.AddInputParameter("@lastModificationTime", SqlDbType.DateTime, (item.LastModificationTime == DateTime.MinValue) ? SqlDateTime.Null : item.LastModificationTime);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@paymentAppliedKey", SqlDbType.Int);
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        db.ExecuteNonQuery(commandWrapper);

                        // save the newly inserted key value
                        item.PaymentAppliedKey = commandWrapper.GetValueInt("@paymentAppliedKey");

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

        public virtual bool Update(PaymentAppliedModel item)
        {
        	bool status = false;

        	try
        	{
        	    string storedProcedure = "gensp_PaymentApplied_UpdateOneByPaymentAppliedKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@paymentAppliedKey", SqlDbType.Int, (item.PaymentAppliedKey == 0) ? SqlInt32.Null : item.PaymentAppliedKey);
                        commandWrapper.AddInputParameter("@paymentKey", SqlDbType.Int, (item.PaymentKey == 0) ? SqlInt32.Null : item.PaymentKey);
                        commandWrapper.AddInputParameter("@invociceKey", SqlDbType.Int, (item.InvociceKey == 0) ? SqlInt32.Null : item.InvociceKey);
                        commandWrapper.AddInputParameter("@amount", SqlDbType.Money, (item.Amount == 0) ? SqlMoney.Null : item.Amount);
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
        	    string storedProcedure = "gensp_PaymentApplied_DeleteOneByPaymentAppliedKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@paymentAppliedKey", SqlDbType.Int, id);

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

        public virtual PaymentAppliedModel Get(int id)
        {
            PaymentAppliedModel item = null;

            try
            {
                string storedProcedure = "gensp_PaymentApplied_SelectOneByPaymentAppliedKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@paymentAppliedKey", SqlDbType.Int, id);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            while (dataReader.Read())
                            {
                                item = new PaymentAppliedModel();

                                Load(dataReader, item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_PaymentApplied_SelectOneByPaymentAppliedKey");
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

        public virtual IList<PaymentAppliedModel> GetAll()
        {
            return GetAll(new PaymentAppliedFilterModel());
        }

        public virtual IList<PaymentAppliedModel> GetAll(PaymentAppliedFilterModel filter)
        {
            List<PaymentAppliedModel> itemList = new List<PaymentAppliedModel>();

            try
            {
                string storedProcedure = "gensp_PaymentApplied_SelectSomeBySearch";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@paymentKey", SqlDbType.Int, (filter.PaymentKey == 0) ? SqlInt32.Null : filter.PaymentKey);
                        commandWrapper.AddInputParameter("@invociceKey", SqlDbType.Int, (filter.InvociceKey == 0) ? SqlInt32.Null : filter.InvociceKey);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            PaymentAppliedModel item = null;
                            while (dataReader.Read())
                            {
                                item = new PaymentAppliedModel();

                                Load(dataReader, item);

                                itemList.Add(item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_PaymentApplied_SelectSomeBySearch");
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

        public virtual IList<PaymentAppliedModel> GetAll(PaymentAppliedFilterModel filter, PagingModel paging)
        {
            List<PaymentAppliedModel> itemList = new List<PaymentAppliedModel>();

            try
            {
                string storedProcedure = "gensp_PaymentApplied_SelectSomeBySearchAndPaging";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@paymentKey", SqlDbType.Int, (filter.PaymentKey == 0) ? SqlInt32.Null : filter.PaymentKey);
                        commandWrapper.AddInputParameter("@invociceKey", SqlDbType.Int, (filter.InvociceKey == 0) ? SqlInt32.Null : filter.InvociceKey);

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
                            PaymentAppliedModel item = null;
                            while (dataReader.Read())
                            {
                                item = new PaymentAppliedModel();

                                Load(dataReader, item);

                                itemList.Add(item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_PaymentApplied_SelectSomeBySearchAndPaging");
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

        protected void Load(DBDataReader dataReader, PaymentAppliedModel item)
        {
            item.PaymentAppliedKey = dataReader.GetValueInt("PaymentAppliedKey");
            item.PaymentKey = dataReader.GetValueInt("PaymentKey");
            item.InvociceKey = dataReader.GetValueInt("InvociceKey");
            item.Amount = dataReader.GetValueDecimal("Amount");
            item.LastModificationTime = dataReader.GetValueDateTime("LastModificationTime");
        }
    }
}
