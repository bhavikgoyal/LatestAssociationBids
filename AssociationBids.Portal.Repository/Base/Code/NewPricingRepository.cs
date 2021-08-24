using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;

namespace AssociationBids.Portal.Repository.Base.Code
{
    public class NewPricingRepository : BaseRepository, INewPricingRepository
    {
        public NewPricingRepository() { }

        public NewPricingRepository(string connectionString)
            : base(connectionString) { }

        //drop down
        public IList<PricingModel> GetAllLookUp1()
        {
            throw new NotImplementedException();
        }

        protected void GetAllLookUp1(DBDataReader dataReader, LookUpModel item)
        {
            item.LookUpKey = Convert.ToInt16(dataReader.GetValueText("CompanyKey"));
            item.Title = dataReader.GetValueText("Name");
        }

        IList<LookUpModel> INewPricingRepository.GetAllLookUp()
        {
            List<LookUpModel> itemList = new List<LookUpModel>();

            try
            {

                string storedProcedure = "site_Company_SelectAll";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            LookUpModel item = null;
                            while (dataReader.Read())
                            {
                                item = new LookUpModel();
                                GetAllLookUp1(dataReader, item);
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


        public IList<PricingModel> GetAllLookUp()
        {
            throw new NotImplementedException();
        }

        protected void GetAllLookUp(DBDataReader dataReader, LookUpModel item)
        {
            item.LookUpKey = Convert.ToInt16(dataReader.GetValueText("LookUpKey"));
            item.Title = dataReader.GetValueText("Title");
        }

        IList<LookUpModel> INewPricingRepository.GetAllLookUpTitle(String Type)
        {
            List<LookUpModel> itemList = new List<LookUpModel>();

            try
            {

                string storedProcedure = "site_LookUp_SelectSomeByLookUpTypeTitle";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        commandWrapper.AddInputParameter("@lookUpTypeTitle", SqlDbType.NVarChar, Type);


                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            LookUpModel item = null;
                            while (dataReader.Read())
                            {
                                item = new LookUpModel();
                                GetAllLookUp(dataReader, item);
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

        //Add Pricing
        public Int64 Insert(PricingModel item)
        {
            Int64 status = 0;

            try
            {                
                string storedProcedure = "site_Pricing_InsertOne";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@CompanyKey", SqlDbType.Int, (item.CompanyKey == 0) ? SqlInt32.Null : item.CompanyKey);
                        commandWrapper.AddInputParameter("@PricingTypeKey", SqlDbType.Int, (item.PricingTypeKey == 0) ? SqlInt32.Null : item.PricingTypeKey);
                        commandWrapper.AddInputParameter("@StartAmount", SqlDbType.Money, (item.StartAmount == 0) ? SqlMoney.Null : item.StartAmount);
                        commandWrapper.AddInputParameter("@EndAmount", SqlDbType.Money, (item.EndAmount == 0) ? SqlMoney.Null : item.EndAmount);
                        commandWrapper.AddInputParameter("@feetype", SqlDbType.VarChar, (item.FeeType == null || item.FeeType == "") ? "Fixed" : item.FeeType);
                        commandWrapper.AddInputParameter("@Fee", SqlDbType.Money, (item.Fee == 0) ? SqlMoney.Null : item.Fee);
                        commandWrapper.AddInputParameter("@LastModificationTime", SqlDbType.DateTime, ("2-2-2020"));
                        commandWrapper.AddInputParameter("@SortOrder", SqlDbType.Float, (item.SortOrder == 0.0) ? SqlDouble.Null : item.SortOrder);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@Notificationvalue", SqlDbType.Int);
                        //commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        db.ExecuteNonQuery(commandWrapper);
                        status = (commandWrapper.GetValueInt("@Notificationvalue"));

                    }
                }
            }
            catch (Exception ex)
            {
                // error occured...
                throw;
            }

            return status;
        }


        //List Pricing
        public List<PricingModel> SearchPricing(long PageSize, long PageIndex, string Search, string Sort)
        {
            List<PricingModel> itemList = new List<PricingModel>();
            try
            {

                string storedProcedure = "site_Price_SelectIndexPaging";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters 
                        commandWrapper.AddInputParameter("@PageSize", SqlDbType.BigInt, (PageSize == 0) ? 0 : PageSize);
                        commandWrapper.AddInputParameter("@PageIndex", SqlDbType.BigInt, (PageIndex == 0) ? 0 : PageIndex);
                        commandWrapper.AddInputParameter("@Search", SqlDbType.NVarChar, Search);
                        commandWrapper.AddInputParameter("@Sort", SqlDbType.Text, Sort);
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
                    }
                }
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
            }
            return itemList;
        }

        protected void Load(DBDataReader dataReader, PricingModel item)
        {
           //panding
            item.PricingKey = dataReader.GetValueInt("PricingKey");
            //item.CompanyKey = dataReader.GetValueInt("CompanyKey");
            //item.Company = dataReader.GetValueText("Name");
            item.PricingTypeKey = dataReader.GetValueInt("PricingTypeKey");
            item.PricingType = dataReader.GetValueText("Title");
            item.StartAmount = dataReader.GetValueDecimal("StartAmount");
            item.EndAmount = dataReader.GetValueDecimal("EndAmount");
            item.Fee = dataReader.GetValueDecimal("Fee");
            item.FeeType = dataReader.GetValueText("FeeType");
            item.SortOrder = dataReader.GetValueInt("SortOrder");

            item.TotalRecords = dataReader.GetValueInt("TotalRecord");

        }

        public virtual PricingModel GetDataViewEdit(int PricingKey)
        {
            PricingModel item = null;
                        
            try
            {
                string storedProcedure = "site_Pricing_SelectOneByPricingKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@pricingKey", SqlDbType.Int, PricingKey);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            while (dataReader.Read())
                            {
                                item = new PricingModel();

                                LoadViewEdit(dataReader, item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
            }

            return item;
        }

        protected void LoadViewEdit(DBDataReader dataReader, PricingModel item)
        {

            item.PricingKey = dataReader.GetValueInt("PricingKey");
            item.StartAmount = dataReader.GetValueDecimal("StartAmount");
            item.EndAmount = dataReader.GetValueDecimal("EndAmount");
            item.Fee = dataReader.GetValueDecimal("Fee");
            item.PricingTypeKey = dataReader.GetValueInt("PricingTypeKey");
            //item.CompanyKey = dataReader.GetValueInt("CompanyKey");
            item.SortOrder = dataReader.GetValueInt("SortOrder");
            item.PricingType = dataReader.GetValueText("PricingType");
            //item.Company = dataReader.GetValueText("Company");
            item.FeeType = dataReader.GetValueText("FeeType");
        }

        public Int64 PricingEdit(PricingModel item)
        {
            Int64 status = 0;

            try
            {
              
                string storedProcedure = "site_Pricing_UpdateOneByPricingKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        commandWrapper.AddInputParameter("@pricingKey", SqlDbType.Int, (item.PricingKey == 0) ? SqlInt32.Null : item.PricingKey);

                        commandWrapper.AddInputParameter("@PricingTypeKey", SqlDbType.Int, (item.PricingTypeKey == 0) ? SqlInt32.Null : item.PricingTypeKey);
                        commandWrapper.AddInputParameter("@StartAmount", SqlDbType.Money, (item.StartAmount == 0) ? SqlMoney.Null : item.StartAmount);
                        commandWrapper.AddInputParameter("@EndAmount", SqlDbType.Money, (item.EndAmount == 0) ? SqlMoney.Null : item.EndAmount);
                        commandWrapper.AddInputParameter("@feetype", SqlDbType.VarChar, (item.FeeType == null || item.FeeType == "" ) ? "Fixed" : item.FeeType);
                        commandWrapper.AddInputParameter("@Fee", SqlDbType.Money, (item.Fee == 0) ? SqlMoney.Null : item.Fee);
                        //commandWrapper.AddInputParameter("@LastModificationTime", SqlDbType.DateTime, ("2-2-2020"));
                        commandWrapper.AddInputParameter("@SortOrder", SqlDbType.Float, (item.SortOrder == 0.0) ? SqlDouble.Null : item.SortOrder);
                        commandWrapper.AddOutputParameter("@PricingTmpvalue", SqlDbType.Int);
                        db.ExecuteNonQuery(commandWrapper);
                        //status = commandWrapper.GetValueInt("@pricingKey");

                        return status;
                    }
                }
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
            }

            return status;
        }

        public bool Remove(int id)
        {
            bool status = false;

            try
            {
                string storedProcedure = "site_Pricing_DeleteOneByPricingKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@PricingKey", SqlDbType.Int, id);

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


    }

}
