using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public class InsuranceRepository : BaseRepository, IInsuranceRepository
    {
        public InsuranceRepository() { }

        public InsuranceRepository(string connectionString)
            : base(connectionString) { }

        public virtual bool Create(InsuranceModel item)
        {
        	bool status = false;

        	try
        	{
        	    string storedProcedure = "gensp_Insurance_InsertOne";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@vendorKey", SqlDbType.Int, (item.VendorKey == 0) ? SqlInt32.Null : item.VendorKey);
                        commandWrapper.AddInputParameter("@companyName", SqlDbType.VarChar, String.IsNullOrEmpty(item.CompanyName) ? 0 : item.CompanyName.Length, String.IsNullOrEmpty(item.CompanyName) ? SqlString.Null : item.CompanyName);
                        commandWrapper.AddInputParameter("@policyNumber", SqlDbType.VarChar, String.IsNullOrEmpty(item.PolicyNumber) ? 0 : item.PolicyNumber.Length, String.IsNullOrEmpty(item.PolicyNumber) ? SqlString.Null : item.PolicyNumber);
                        commandWrapper.AddInputParameter("@insuranceAmount", SqlDbType.Money, (item.InsuranceAmount == 0) ? SqlMoney.Null : item.InsuranceAmount);
                        commandWrapper.AddInputParameter("@agentName", SqlDbType.VarChar, String.IsNullOrEmpty(item.AgentName) ? 0 : item.AgentName.Length, String.IsNullOrEmpty(item.AgentName) ? SqlString.Null : item.AgentName);
                        commandWrapper.AddInputParameter("@email", SqlDbType.VarChar, String.IsNullOrEmpty(item.Email) ? 0 : item.Email.Length, String.IsNullOrEmpty(item.Email) ? SqlString.Null : item.Email);
                        commandWrapper.AddInputParameter("@work", SqlDbType.VarChar, String.IsNullOrEmpty(item.Work) ? 0 : item.Work.Length, String.IsNullOrEmpty(item.Work) ? SqlString.Null : item.Work);
                        commandWrapper.AddInputParameter("@cellPhone", SqlDbType.VarChar, String.IsNullOrEmpty(item.CellPhone) ? 0 : item.CellPhone.Length, String.IsNullOrEmpty(item.CellPhone) ? SqlString.Null : item.CellPhone);
                        commandWrapper.AddInputParameter("@fax", SqlDbType.VarChar, String.IsNullOrEmpty(item.Fax) ? 0 : item.Fax.Length, String.IsNullOrEmpty(item.Fax) ? SqlString.Null : item.Fax);
                        commandWrapper.AddInputParameter("@address", SqlDbType.VarChar, String.IsNullOrEmpty(item.Address) ? 0 : item.Address.Length, String.IsNullOrEmpty(item.Address) ? SqlString.Null : item.Address);
                        commandWrapper.AddInputParameter("@address2", SqlDbType.VarChar, String.IsNullOrEmpty(item.Address2) ? 0 : item.Address2.Length, String.IsNullOrEmpty(item.Address2) ? SqlString.Null : item.Address2);
                        commandWrapper.AddInputParameter("@city", SqlDbType.VarChar, String.IsNullOrEmpty(item.City) ? 0 : item.City.Length, String.IsNullOrEmpty(item.City) ? SqlString.Null : item.City);
                        commandWrapper.AddInputParameter("@state", SqlDbType.VarChar, String.IsNullOrEmpty(item.State) ? 0 : item.State.Length, String.IsNullOrEmpty(item.State) ? SqlString.Null : item.State);
                        commandWrapper.AddInputParameter("@zip", SqlDbType.VarChar, String.IsNullOrEmpty(item.Zip) ? 0 : item.Zip.Length, String.IsNullOrEmpty(item.Zip) ? SqlString.Null : item.Zip);
                        commandWrapper.AddInputParameter("@startDate", SqlDbType.DateTime, (item.StartDate == DateTime.MinValue) ? SqlDateTime.Null : item.StartDate);
                        commandWrapper.AddInputParameter("@endDate", SqlDbType.DateTime, (item.EndDate == DateTime.MinValue) ? SqlDateTime.Null : item.EndDate);
                        commandWrapper.AddInputParameter("@renewalDate", SqlDbType.DateTime, (item.RenewalDate == DateTime.MinValue) ? SqlDateTime.Null : item.RenewalDate);
                        commandWrapper.AddInputParameter("@status", SqlDbType.Int, (item.Status == 0) ? SqlInt32.Null : item.Status);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@insuranceKey", SqlDbType.Int);
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        db.ExecuteNonQuery(commandWrapper);

                        // save the newly inserted key value
                        item.InsuranceKey = commandWrapper.GetValueInt("@insuranceKey");

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

        public virtual bool Update(InsuranceModel item)
        {
        	bool status = false;

        	try
        	{
        	    string storedProcedure = "gensp_Insurance_UpdateOneByInsuranceKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@insuranceKey", SqlDbType.Int, (item.InsuranceKey == 0) ? SqlInt32.Null : item.InsuranceKey);
                        commandWrapper.AddInputParameter("@vendorKey", SqlDbType.Int, (item.VendorKey == 0) ? SqlInt32.Null : item.VendorKey);
                        commandWrapper.AddInputParameter("@companyName", SqlDbType.VarChar, String.IsNullOrEmpty(item.CompanyName) ? 0 : item.CompanyName.Length, String.IsNullOrEmpty(item.CompanyName) ? SqlString.Null : item.CompanyName);
                        commandWrapper.AddInputParameter("@policyNumber", SqlDbType.VarChar, String.IsNullOrEmpty(item.PolicyNumber) ? 0 : item.PolicyNumber.Length, String.IsNullOrEmpty(item.PolicyNumber) ? SqlString.Null : item.PolicyNumber);
                        commandWrapper.AddInputParameter("@insuranceAmount", SqlDbType.Money, (item.InsuranceAmount == 0) ? SqlMoney.Null : item.InsuranceAmount);
                        commandWrapper.AddInputParameter("@agentName", SqlDbType.VarChar, String.IsNullOrEmpty(item.AgentName) ? 0 : item.AgentName.Length, String.IsNullOrEmpty(item.AgentName) ? SqlString.Null : item.AgentName);
                        commandWrapper.AddInputParameter("@email", SqlDbType.VarChar, String.IsNullOrEmpty(item.Email) ? 0 : item.Email.Length, String.IsNullOrEmpty(item.Email) ? SqlString.Null : item.Email);
                        commandWrapper.AddInputParameter("@work", SqlDbType.VarChar, String.IsNullOrEmpty(item.Work) ? 0 : item.Work.Length, String.IsNullOrEmpty(item.Work) ? SqlString.Null : item.Work);
                        commandWrapper.AddInputParameter("@cellPhone", SqlDbType.VarChar, String.IsNullOrEmpty(item.CellPhone) ? 0 : item.CellPhone.Length, String.IsNullOrEmpty(item.CellPhone) ? SqlString.Null : item.CellPhone);
                        commandWrapper.AddInputParameter("@fax", SqlDbType.VarChar, String.IsNullOrEmpty(item.Fax) ? 0 : item.Fax.Length, String.IsNullOrEmpty(item.Fax) ? SqlString.Null : item.Fax);
                        commandWrapper.AddInputParameter("@address", SqlDbType.VarChar, String.IsNullOrEmpty(item.Address) ? 0 : item.Address.Length, String.IsNullOrEmpty(item.Address) ? SqlString.Null : item.Address);
                        commandWrapper.AddInputParameter("@address2", SqlDbType.VarChar, String.IsNullOrEmpty(item.Address2) ? 0 : item.Address2.Length, String.IsNullOrEmpty(item.Address2) ? SqlString.Null : item.Address2);
                        commandWrapper.AddInputParameter("@city", SqlDbType.VarChar, String.IsNullOrEmpty(item.City) ? 0 : item.City.Length, String.IsNullOrEmpty(item.City) ? SqlString.Null : item.City);
                        commandWrapper.AddInputParameter("@state", SqlDbType.VarChar, String.IsNullOrEmpty(item.State) ? 0 : item.State.Length, String.IsNullOrEmpty(item.State) ? SqlString.Null : item.State);
                        commandWrapper.AddInputParameter("@zip", SqlDbType.VarChar, String.IsNullOrEmpty(item.Zip) ? 0 : item.Zip.Length, String.IsNullOrEmpty(item.Zip) ? SqlString.Null : item.Zip);
                        commandWrapper.AddInputParameter("@startDate", SqlDbType.DateTime, (item.StartDate == DateTime.MinValue) ? SqlDateTime.Null : item.StartDate);
                        commandWrapper.AddInputParameter("@endDate", SqlDbType.DateTime, (item.EndDate == DateTime.MinValue) ? SqlDateTime.Null : item.EndDate);
                        commandWrapper.AddInputParameter("@renewalDate", SqlDbType.DateTime, (item.RenewalDate == DateTime.MinValue) ? SqlDateTime.Null : item.RenewalDate);
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
        	    string storedProcedure = "gensp_Insurance_DeleteOneByInsuranceKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@insuranceKey", SqlDbType.Int, id);

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

        public virtual InsuranceModel Get(int id)
        {
            InsuranceModel item = null;

            try
            {
                string storedProcedure = "gensp_Insurance_SelectOneByInsuranceKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@insuranceKey", SqlDbType.Int, id);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            while (dataReader.Read())
                            {
                                item = new InsuranceModel();

                                Load(dataReader, item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_Insurance_SelectOneByInsuranceKey");
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

        public virtual IList<InsuranceModel> GetAll()
        {
            return GetAll(new InsuranceFilterModel());
        }

        public virtual IList<InsuranceModel> GetAll(InsuranceFilterModel filter)
        {
            List<InsuranceModel> itemList = new List<InsuranceModel>();

            try
            {
                string storedProcedure = "gensp_Insurance_SelectSomeBySearch";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@vendorKey", SqlDbType.Int, (filter.VendorKey == 0) ? SqlInt32.Null : filter.VendorKey);
                        commandWrapper.AddInputParameter("@state", SqlDbType.VarChar, GetFilterValue(filter.State).Length, String.IsNullOrEmpty(filter.State) ? SqlString.Null : GetFilterValue(filter.State));
                        commandWrapper.AddInputParameter("@status", SqlDbType.Int, (filter.Status == 0) ? SqlInt32.Null : filter.Status);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            InsuranceModel item = null;
                            while (dataReader.Read())
                            {
                                item = new InsuranceModel();

                                Load(dataReader, item);

                                itemList.Add(item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_Insurance_SelectSomeBySearch");
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

        public virtual IList<InsuranceModel> GetAll(InsuranceFilterModel filter, PagingModel paging)
        {
            List<InsuranceModel> itemList = new List<InsuranceModel>();

            try
            {
                string storedProcedure = "gensp_Insurance_SelectSomeBySearchAndPaging";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@vendorKey", SqlDbType.Int, (filter.VendorKey == 0) ? SqlInt32.Null : filter.VendorKey);
                        commandWrapper.AddInputParameter("@state", SqlDbType.VarChar, GetFilterValue(filter.State).Length, String.IsNullOrEmpty(filter.State) ? SqlString.Null : GetFilterValue(filter.State));
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
                            InsuranceModel item = null;
                            while (dataReader.Read())
                            {
                                item = new InsuranceModel();

                                Load(dataReader, item);

                                itemList.Add(item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_Insurance_SelectSomeBySearchAndPaging");
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

        protected void Load(DBDataReader dataReader, InsuranceModel item)
        {
            item.InsuranceKey = dataReader.GetValueInt("InsuranceKey");
            item.VendorKey = dataReader.GetValueInt("VendorKey");
            item.CompanyName = dataReader.GetValueText("CompanyName");
            item.PolicyNumber = dataReader.GetValueText("PolicyNumber");
            item.InsuranceAmount = dataReader.GetValueDecimal("InsuranceAmount");
            item.AgentName = dataReader.GetValueText("AgentName");
            item.Email = dataReader.GetValueText("Email");
            item.Work = dataReader.GetValueText("Work");
            item.CellPhone = dataReader.GetValueText("CellPhone");
            item.Fax = dataReader.GetValueText("Fax");
            item.Address = dataReader.GetValueText("Address");
            item.Address2 = dataReader.GetValueText("Address2");
            item.City = dataReader.GetValueText("City");
            item.State = dataReader.GetValueText("State");
            item.Zip = dataReader.GetValueText("Zip");
            item.StartDate = dataReader.GetValueDateTime("StartDate");
            item.EndDate = dataReader.GetValueDateTime("EndDate");
            item.RenewalDate = dataReader.GetValueDateTime("RenewalDate");
            item.Status = dataReader.GetValueInt("Status");
        }
    }
}
