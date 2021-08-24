using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using AssociationBids.Portal.Model;


namespace AssociationBids.Portal.Repository.Base.Code
{
    public class vBillingRepository : BaseRepository,IvBillingRepository
    {
        public vBillingRepository() { }

        public vBillingRepository(string connectionString)
            : base(connectionString) { }

        public List<vBillingModel> LoadBillingList(Int64 CompanyKey, Int64 resourceid)
        {
            List<vBillingModel> itemList = new List<vBillingModel>();
            try
            {

                string storedProcedure = "site_PaymentMethod_ByCompanyKey_Copy";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters

                        commandWrapper.AddInputParameter("@CompanyKey", SqlDbType.BigInt, (CompanyKey == 0) ? 0 : CompanyKey);
                        commandWrapper.AddInputParameter("@AddedByResourceKey", SqlDbType.BigInt, (resourceid == 0) ? 0 : resourceid);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            vBillingModel item = null;
                            while (dataReader.Read())
                            {
                                item = new vBillingModel();
                                Load(dataReader, item);
                                itemList.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
            }
            return itemList;
        }

        public List<vBillingModel> LoadBillingListByResurceKey(Int64 resourceid)
        {
            List<vBillingModel> itemList = new List<vBillingModel>();
            try
            {

                string storedProcedure = "site_PaymentMethod_ByResourceKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters

                        commandWrapper.AddInputParameter("@ResourceKey", SqlDbType.BigInt,resourceid);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            vBillingModel item = null;
                            while (dataReader.Read())
                            {
                                item = new vBillingModel();
                                Load(dataReader, item);
                                itemList.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
            }
            return itemList;
        }

        protected void Load(DBDataReader dataReader, vBillingModel item)
        {
            //panding
            item.PaymentMethodKey = dataReader.GetValueInt("PaymentMethodKey");
            item.MaskedCCNumber = dataReader.GetValueText("MaskedCCNumber");
            item.PrimaryMethod = dataReader.GetValueBool("PrimaryMethod");
            item.CardHolderFirstName = dataReader.GetValueText("CardHolderFirstName");
            item.CardHolderLastName = dataReader.GetValueText("CardHolderLastName");
            item.StripeTokenID = dataReader.GetValueText("StripeTokenID");
            item.PaymentMethodId = dataReader.GetValueText("PaymentMethodId");
            item.ValidTillMM = dataReader.GetValueText("CardExpiryMonth");
            item.ValidTillYY = dataReader.GetValueText("CardExpiryYear");
            try
            {
                item.priority = dataReader.GetValueInt("priority");
                item.NotificationId = dataReader.GetValueText("NotificationId");
                item.NotificationType = dataReader.GetValueText("NotificationType");
            }
            catch { }
        }

        public bool Remove(int id)
        {
            bool status = false;

            try
            {
                string storedProcedure = "site_PaymentMethod_DeleteOneByagreementKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@PaymentMethodKey", SqlDbType.Int, id);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        db.ExecuteNonQuery(commandWrapper);

                        status = (commandWrapper.GetValueInt("@errorCode") == 0);
                    }
                }
            }
            catch (Exception ex)
            {
                // error occured...

            }

            return status;
        }

        public bool ChangePrimaryMethod(int PaymentMethodKey,int CompanyKey,int ResourceKey)
        {
            bool status = false;

            try
            {
                string storedProcedure = "site_PaymentMethod_ChangePrimaryMethod";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@PaymentMethodKey", SqlDbType.Int, PaymentMethodKey);
                        commandWrapper.AddInputParameter("@CompanyKey", SqlDbType.Int, CompanyKey);
                        commandWrapper.AddInputParameter("@AddedByResourceKey", SqlDbType.Int, ResourceKey);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        db.ExecuteNonQuery(commandWrapper);

                        status = (commandWrapper.GetValueInt("@errorCode") == 0);
                    }
                }
            }
            catch (Exception ex)
            {
                // error occured...

            }

            return status;
        }


        public bool vBillingInsert(vBillingModel vBilling)
        {
            bool status = false;

            try
            {

                string storedProcedure = "site_PaymentMethod_Insert_New";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters

                        commandWrapper.AddInputParameter("@CompanyKey", SqlDbType.Int, (vBilling.CompanyKey == 0) ? SqlInt32.Null : vBilling.CompanyKey);
                        commandWrapper.AddInputParameter("@CardHolderFirstName", SqlDbType.VarChar, String.IsNullOrEmpty(vBilling.CardHolderFirstName) ? 0 : vBilling.CardHolderFirstName.Length, String.IsNullOrEmpty(vBilling.CardHolderFirstName) ? SqlString.Null : vBilling.CardHolderFirstName);
                        commandWrapper.AddInputParameter("@CardHolderLastName", SqlDbType.VarChar, String.IsNullOrEmpty(vBilling.CardHolderLastName) ? 0 : vBilling.CardHolderLastName.Length, String.IsNullOrEmpty(vBilling.CardHolderLastName) ? SqlString.Null : vBilling.CardHolderLastName);
                        commandWrapper.AddInputParameter("@MaskedCCNumber", SqlDbType.VarChar, String.IsNullOrEmpty(vBilling.MaskedCCNumber) ? 0 : vBilling.MaskedCCNumber.Length, String.IsNullOrEmpty(vBilling.MaskedCCNumber) ? SqlString.Null : vBilling.MaskedCCNumber);
                        commandWrapper.AddInputParameter("@StripeTokenID", SqlDbType.VarChar, String.IsNullOrEmpty(vBilling.StripeTokenID) ? 0 : vBilling.StripeTokenID.Length, String.IsNullOrEmpty(vBilling.StripeTokenID) ? SqlString.Null : vBilling.StripeTokenID);
                        commandWrapper.AddInputParameter("@PaymentMethodId", SqlDbType.VarChar, String.IsNullOrEmpty(vBilling.PaymentMethodId) ? 0 : vBilling.PaymentMethodId.Length, String.IsNullOrEmpty(vBilling.PaymentMethodId) ? SqlString.Null : vBilling.PaymentMethodId);
                        commandWrapper.AddInputParameter("@AddedByResourceKey", SqlDbType.Int, (vBilling.AddedByResourceKey == 0) ? SqlInt32.Null : vBilling.AddedByResourceKey);
                        commandWrapper.AddInputParameter("@Status", SqlDbType.Int, (vBilling.Status == 0) ? SqlInt32.Null : vBilling.Status);
                        commandWrapper.AddInputParameter("@PrimaryMethod", SqlDbType.Bit, (vBilling.PrimaryMethod));

                        commandWrapper.AddInputParameter("@CardExpiryMonth", SqlDbType.VarChar, String.IsNullOrEmpty(vBilling.ValidTillMM) ? 0 : vBilling.ValidTillMM.Length, String.IsNullOrEmpty(vBilling.ValidTillMM) ? SqlString.Null : vBilling.ValidTillMM);
                        commandWrapper.AddInputParameter("@CardExpiryYear", SqlDbType.VarChar, String.IsNullOrEmpty(vBilling.ValidTillYY) ? 0 : vBilling.ValidTillYY.Length, String.IsNullOrEmpty(vBilling.PaymentMethodId) ? SqlString.Null : vBilling.ValidTillYY);
                        commandWrapper.AddInputParameter("@CvvNumber", SqlDbType.VarChar, String.IsNullOrEmpty(vBilling.CVV) ? 0 : vBilling.CVV.Length, String.IsNullOrEmpty(vBilling.CVV) ? SqlString.Null : vBilling.CVV);

                        
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);
                        db.ExecuteNonQuery(commandWrapper);
                        status = (commandWrapper.GetValueInt("@errorCode") == 0);
                    }
                }
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
            }

            return status;

        }
    }

}
