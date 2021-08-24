using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Net;
using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public class AgreementRepository : BaseRepository, IAgreementRepository
    {
        public AgreementRepository() { }

        public AgreementRepository(string connectionString)
            : base(connectionString) { }
        protected void Load(DBDataReader dataReader, AgreementModel item)
        {
            item.AgreementKey = dataReader.GetValueInt("AgreementKey");
            item.Title = dataReader.GetValueText("Title");
            item.AgreementDate = dataReader.GetValueDateTime("AgreementDate");
            try
            {
                item.AgreementDates = item.AgreementDate.ToString("MM/dd/yyyy");
            }
            catch (Exception)
            { }
            item.Status = dataReader.GetValueInt("Status");
            item.Description = dataReader.GetValueText("Description");
         

        }

        protected void LoadIndexpaging(DBDataReader dataReader, AgreementModel item)
        {
            item.AgreementKey = dataReader.GetValueInt("AgreementKey");
            item.Title = dataReader.GetValueText("Title");
            item.AgreementDate = dataReader.GetValueDateTime("AgreementDate");
            try
            {
                item.AgreementDates = item.AgreementDate.ToString("MM/dd/yyyy");
            }
            catch (Exception)
            { }
            item.Status = dataReader.GetValueInt("Status");
            item.Description = dataReader.GetValueText("Description");
            item.TotalRecords = dataReader.GetValueInt("TotalRecord");
        }
        
        protected void LoadViewEdit(DBDataReader dataReader, AgreementModel item)
        {

            item.AgreementKey = dataReader.GetValueInt("AgreementKey");
            item.Title = dataReader.GetValueText("Title");
            item.AgreementDate = dataReader.GetValueDateTime("AgreementDate");
            try
            {
                item.AgreementDates = item.AgreementDate.ToString("MM/dd/yyyy");
            }
            catch (Exception)
            { }
            item.Status = dataReader.GetValueInt("Status");
            item.Description = dataReader.GetValueText("Description");
            item.TotalRecords = dataReader.GetValueInt("TotalRecord");

        }

        public virtual bool Update(AgreementModel item)
        {
        	bool status = false;

        	try
        	{
        	    string storedProcedure = "site_Agreement_Edit";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        item.Description = WebUtility.HtmlEncode(item.Description);
                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@agreementKey", SqlDbType.Int, (item.AgreementKey == 0) ? SqlInt32.Null : item.AgreementKey);
                        commandWrapper.AddInputParameter("@portalKey", SqlDbType.Int, (item.PortalKey == 0) ? SqlInt32.Null : item.PortalKey);
                        commandWrapper.AddInputParameter("@title", SqlDbType.VarChar, String.IsNullOrEmpty(item.Title) ? 0 : item.Title.Length, String.IsNullOrEmpty(item.Title) ? SqlString.Null : item.Title);
                        commandWrapper.AddInputParameter("@agreementDate", SqlDbType.DateTime, (item.AgreementDate == DateTime.MinValue) ? SqlDateTime.Null : item.AgreementDate);
                        commandWrapper.AddInputParameter("@description", SqlDbType.VarChar, String.IsNullOrEmpty(item.Description) ? 0 : item.Description.Length, String.IsNullOrEmpty(item.Description) ? SqlString.Null : item.Description);
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
        	    string storedProcedure = "[site_Agreement_DeleteOneByAgreementKey]";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@agreementKey", SqlDbType.Int, id);

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

        public virtual AgreementModel Get(int id)
        {
            AgreementModel item = null;

            try
            {
                string storedProcedure = "gensp_Agreement_SelectOneByAgreementKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@agreementKey", SqlDbType.Int, id);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            while (dataReader.Read())
                            {
                                item = new AgreementModel();

                                Load(dataReader, item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_Agreement_SelectOneByAgreementKey");
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

        public virtual IList<AgreementModel> GetAll()
        {
            return GetAll(new AgreementModel());
        }

        public virtual IList<AgreementModel> GetAll(AgreementModel filter)
        {
            List<AgreementModel> itemList = new List<AgreementModel>();

            try
            {
                string storedProcedure = "site_Agreement_SelectAll";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            AgreementModel item = null;
                            while (dataReader.Read())
                            {
                                item = new AgreementModel();

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

        public virtual IList<AgreementModel> GetAll(AgreementFilterModel filter, PagingModel paging)
        {
            List<AgreementModel> itemList = new List<AgreementModel>();

            try
            {
                string storedProcedure = "gensp_Agreement_SelectSomeBySearchAndPaging";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@portalKey", SqlDbType.Int, (filter.PortalKey == 0) ? SqlInt32.Null : filter.PortalKey);
                        commandWrapper.AddInputParameter("@propertyKeyList", SqlDbType.VarChar, filter.PropertyKeyList.Length, String.IsNullOrEmpty(filter.PropertyKeyList) ? SqlString.Null : filter.PropertyKeyList);
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
                            AgreementModel item = null;
                            while (dataReader.Read())
                            {
                                item = new AgreementModel();

                                Load(dataReader, item);

                                itemList.Add(item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_Agreement_SelectSomeBySearchAndPaging");
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

       
        public List<AgreementModel> SearchUser(long PageSize, long PageIndex, string Search, string Sort)
        {
            List<AgreementModel> itemList = new List<AgreementModel>();
            try
            {

                string storedProcedure = "site_Agreement_SelectIndexPaging";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters 
                        commandWrapper.AddInputParameter("@PageSize", SqlDbType.BigInt, (PageSize == 0) ? 0 : PageSize);
                        commandWrapper.AddInputParameter("@PageIndex", SqlDbType.BigInt, (PageIndex == 0) ? 0 : PageIndex);
                        commandWrapper.AddInputParameter("@Search", SqlDbType.NText, (Search == "") ? "" : Search);
                        commandWrapper.AddInputParameter("@Sort", SqlDbType.NText, (Sort == "") ? "" : Sort);
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            AgreementModel item = null;
                            while (dataReader.Read())
                            {
                                item = new AgreementModel();
                                LoadIndexpaging(dataReader, item);
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

        public List<AgreementModel> AdvancedSearchAgreement(long PageSize, long PageIndex, string Search, string Status, string Sort)
        {
            List<AgreementModel> itemList = new List<AgreementModel>();
            try
            {

                string storedProcedure = "site_Agreement_SelectIndexPaging_New";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters 
                        commandWrapper.AddInputParameter("@PageSize", SqlDbType.BigInt, (PageSize == 0) ? 0 : PageSize);
                        commandWrapper.AddInputParameter("@PageIndex", SqlDbType.BigInt, (PageIndex == 0) ? 0 : PageIndex);
                        commandWrapper.AddInputParameter("@Search", SqlDbType.NText, (Search == "") ? "" : Search);
                        commandWrapper.AddInputParameter("@Status", SqlDbType.NText, (Status == "") ? "" : Status);
                        commandWrapper.AddInputParameter("@Sort", SqlDbType.NText, (Sort == "") ? "" : Sort);
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            AgreementModel item = null;
                            while (dataReader.Read())
                            {
                                item = new AgreementModel();
                                LoadIndexpaging(dataReader, item);
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


        public AgreementModel GetDataViewEdit(int AgreementKey )
        {
            AgreementModel item = null;

            try
            {
                string storedProcedure = "site_Agreement_SelectOneByAgreementKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@AgreementKey", SqlDbType.Int, AgreementKey);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            while (dataReader.Read())
                            {
                                item = new AgreementModel();

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
        public long AgreenemtEdit(AgreementModel item)
        {
            Int64 status = 0;

            try
            {
                string storedProcedure = "site_Agreement_Edit";

                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        commandWrapper.AddInputParameter("@AgreementKey", SqlDbType.Int, (item.AgreementKey == 0) ? SqlInt32.Null : item.AgreementKey);
                        commandWrapper.AddInputParameter("@title", SqlDbType.VarChar, String.IsNullOrEmpty(item.Title) ? 0 : item.Title.Length, String.IsNullOrEmpty(item.Title) ? SqlString.Null : item.Title);
                        commandWrapper.AddInputParameter("@AgreementDate", SqlDbType.DateTime, (item.AgreementDate == DateTime.MinValue) ? SqlDateTime.Null : item.AgreementDate);
                        commandWrapper.AddInputParameter("@description", SqlDbType.VarChar, String.IsNullOrEmpty(item.Description) ? 0 : item.Description.Length, String.IsNullOrEmpty(item.Description) ? SqlString.Null : item.Description);

                        commandWrapper.AddInputParameter("@status", SqlDbType.Int, (item.Status == 0) ? SqlInt32.Null : item.Status);
                        commandWrapper.AddOutputParameter("@Agreementvalue", SqlDbType.Int);
                        db.ExecuteNonQuery(commandWrapper);
                        status = commandWrapper.GetValueInt("@Agreementvalue");
                    }
                }
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
            }

            return status;
        }

        public long Agreementupdates(AgreementModel item)
        {
            throw new NotImplementedException();
        }

       

        public int Insert(AgreementModel item)
        {
            
            int status = 0;

            try
            {
                string storedProcedure = "site_Agreement_Insert";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        commandWrapper.AddInputParameter("@title", SqlDbType.VarChar, String.IsNullOrEmpty(item.Title) ? 0 : item.Title.Length, String.IsNullOrEmpty(item.Title) ? SqlString.Null : item.Title);
                        commandWrapper.AddInputParameter("@agreementDate", SqlDbType.DateTime, (item.AgreementDate == DateTime.MinValue) ? SqlDateTime.Null : item.AgreementDate);
                        commandWrapper.AddInputParameter("@description", SqlDbType.VarChar, String.IsNullOrEmpty(item.Description) ? 0 : item.Description.Length, String.IsNullOrEmpty(item.Description) ? SqlString.Null : item.Description);

                        commandWrapper.AddInputParameter("@status", SqlDbType.Int, (item.Status == 0) ? SqlInt32.Null : item.Status);

                        commandWrapper.AddOutputParameter("@Agreementvalue", SqlDbType.Int);
                        db.ExecuteNonQuery(commandWrapper);
                        status = commandWrapper.GetValueInt("@Agreementvalue");
                    }
                }
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
            }

            return status;
        }

        public IList<AgreementModel> GetAll(AgreementFilterModel filter)
        {
            throw new NotImplementedException();
        }
    }
}
