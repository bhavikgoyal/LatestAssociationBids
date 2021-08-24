using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using AssociationBids.Portal.Model;

namespace AssociationBids.Portal.Repository.Base
{
    public class PropertyRepository : BaseRepository, IPropertyRepository
    {
        public PropertyRepository() { }

        public PropertyRepository(string connectionString)
            : base(connectionString) { }

        public virtual bool Create(PropertyModel item)
        {
        	bool status = false;

        	try
        	{
        	    string storedProcedure = "gensp_Property_InsertOne";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@companyKey", SqlDbType.Int, (item.CompanyKey == 0) ? SqlInt32.Null : item.CompanyKey);
                        commandWrapper.AddInputParameter("@title", SqlDbType.VarChar, String.IsNullOrEmpty(item.Title) ? 0 : item.Title.Length, String.IsNullOrEmpty(item.Title) ? SqlString.Null : item.Title);
                        commandWrapper.AddInputParameter("@numberOfUnits", SqlDbType.Int, (item.NumberOfUnits == 0) ? SqlInt32.Null : item.NumberOfUnits);
                        commandWrapper.AddInputParameter("@address", SqlDbType.VarChar, String.IsNullOrEmpty(item.Address) ? 0 : item.Address.Length, String.IsNullOrEmpty(item.Address) ? SqlString.Null : item.Address);
                        commandWrapper.AddInputParameter("@address2", SqlDbType.VarChar, String.IsNullOrEmpty(item.Address2) ? 0 : item.Address2.Length, String.IsNullOrEmpty(item.Address2) ? SqlString.Null : item.Address2);
                        commandWrapper.AddInputParameter("@city", SqlDbType.VarChar, String.IsNullOrEmpty(item.City) ? 0 : item.City.Length, String.IsNullOrEmpty(item.City) ? SqlString.Null : item.City);
                        commandWrapper.AddInputParameter("@state", SqlDbType.VarChar, String.IsNullOrEmpty(item.State) ? 0 : item.State.Length, String.IsNullOrEmpty(item.State) ? SqlString.Null : item.State);
                        commandWrapper.AddInputParameter("@zip", SqlDbType.VarChar, String.IsNullOrEmpty(item.Zip) ? 0 : item.Zip.Length, String.IsNullOrEmpty(item.Zip) ? SqlString.Null : item.Zip);
                        commandWrapper.AddInputParameter("@bidRequestAmount", SqlDbType.Money, (item.BidRequestAmount == 0) ? SqlMoney.Null : item.BidRequestAmount);
                        commandWrapper.AddInputParameter("@minimumInsuranceAmount", SqlDbType.Money, (item.MinimumInsuranceAmount == 0) ? SqlMoney.Null : item.MinimumInsuranceAmount);
                        commandWrapper.AddInputParameter("@description", SqlDbType.VarChar, String.IsNullOrEmpty(item.Description) ? 0 : item.Description.Length, String.IsNullOrEmpty(item.Description) ? SqlString.Null : item.Description);
                        commandWrapper.AddInputParameter("@status", SqlDbType.Int, (item.Status == 0) ? SqlInt32.Null : item.Status);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@propertyKey", SqlDbType.Int);
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        db.ExecuteNonQuery(commandWrapper);

                        // save the newly inserted key value
                        item.PropertyKey = commandWrapper.GetValueInt("@propertyKey");

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

        public virtual bool Update(PropertyModel item)
        {
        	bool status = false;

        	try
        	{
        	    string storedProcedure = "gensp_Property_UpdateOneByPropertyKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@propertyKey", SqlDbType.Int, (item.PropertyKey == 0) ? SqlInt32.Null : item.PropertyKey);
                        commandWrapper.AddInputParameter("@companyKey", SqlDbType.Int, (item.CompanyKey == 0) ? SqlInt32.Null : item.CompanyKey);
                        commandWrapper.AddInputParameter("@title", SqlDbType.VarChar, String.IsNullOrEmpty(item.Title) ? 0 : item.Title.Length, String.IsNullOrEmpty(item.Title) ? SqlString.Null : item.Title);
                        commandWrapper.AddInputParameter("@numberOfUnits", SqlDbType.Int, (item.NumberOfUnits == 0) ? SqlInt32.Null : item.NumberOfUnits);
                        commandWrapper.AddInputParameter("@address", SqlDbType.VarChar, String.IsNullOrEmpty(item.Address) ? 0 : item.Address.Length, String.IsNullOrEmpty(item.Address) ? SqlString.Null : item.Address);
                        commandWrapper.AddInputParameter("@address2", SqlDbType.VarChar, String.IsNullOrEmpty(item.Address2) ? 0 : item.Address2.Length, String.IsNullOrEmpty(item.Address2) ? SqlString.Null : item.Address2);
                        commandWrapper.AddInputParameter("@city", SqlDbType.VarChar, String.IsNullOrEmpty(item.City) ? 0 : item.City.Length, String.IsNullOrEmpty(item.City) ? SqlString.Null : item.City);
                        commandWrapper.AddInputParameter("@state", SqlDbType.VarChar, String.IsNullOrEmpty(item.State) ? 0 : item.State.Length, String.IsNullOrEmpty(item.State) ? SqlString.Null : item.State);
                        commandWrapper.AddInputParameter("@zip", SqlDbType.VarChar, String.IsNullOrEmpty(item.Zip) ? 0 : item.Zip.Length, String.IsNullOrEmpty(item.Zip) ? SqlString.Null : item.Zip);
                        commandWrapper.AddInputParameter("@bidRequestAmount", SqlDbType.Money, (item.BidRequestAmount == 0) ? SqlMoney.Null : item.BidRequestAmount);
                        commandWrapper.AddInputParameter("@minimumInsuranceAmount", SqlDbType.Money, (item.MinimumInsuranceAmount == 0) ? SqlMoney.Null : item.MinimumInsuranceAmount);
                        commandWrapper.AddInputParameter("@description", SqlDbType.VarChar, String.IsNullOrEmpty(item.Description) ? 0 : item.Description.Length, String.IsNullOrEmpty(item.Description) ? SqlString.Null : item.Description);
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
        	    string storedProcedure = "gensp_Property_DeleteOneByPropertyKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@propertyKey", SqlDbType.Int, id);

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

        public virtual PropertyModel Get(int id)
        {
            PropertyModel item = null;

            try
            {
                string storedProcedure = "gensp_Property_SelectOneByPropertyKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@propertyKey", SqlDbType.Int, id);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            while (dataReader.Read())
                            {
                                item = new PropertyModel();

                                Load(dataReader, item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_Property_SelectOneByPropertyKey");
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

        public virtual IList<PropertyModel> GetAll()
        {
            return GetAll(new PropertyFilterModel());
        }

        public virtual IList<PropertyModel> GetAll(PropertyFilterModel filter)
        {
            List<PropertyModel> itemList = new List<PropertyModel>();

            try
            {
                string storedProcedure = "gensp_Property_SelectSomeBySearch";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@companyKey", SqlDbType.Int, (filter.CompanyKey == 0) ? SqlInt32.Null : filter.CompanyKey);
                        commandWrapper.AddInputParameter("@state", SqlDbType.VarChar, GetFilterValue(filter.State).Length, String.IsNullOrEmpty(filter.State) ? SqlString.Null : GetFilterValue(filter.State));
                        commandWrapper.AddInputParameter("@status", SqlDbType.Int, (filter.Status == 0) ? SqlInt32.Null : filter.Status);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            PropertyModel item = null;
                            while (dataReader.Read())
                            {
                                item = new PropertyModel();

                                Load(dataReader, item);

                                itemList.Add(item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_Property_SelectSomeBySearch");
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

        public virtual IList<PropertyModel> GetAll(PropertyFilterModel filter, PagingModel paging)
        {
            List<PropertyModel> itemList = new List<PropertyModel>();

            try
            {
                string storedProcedure = "gensp_Property_SelectSomeBySearchAndPaging";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@companyKey", SqlDbType.Int, (filter.CompanyKey == 0) ? SqlInt32.Null : filter.CompanyKey);
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
                            PropertyModel item = null;
                            while (dataReader.Read())
                            {
                                item = new PropertyModel();

                                Load(dataReader, item);

                                itemList.Add(item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_Property_SelectSomeBySearchAndPaging");
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

        protected void Load(DBDataReader dataReader, PropertyModel item)
        {
            item.PropertyKey = dataReader.GetValueInt("PropertyKey");
            item.CompanyKey = dataReader.GetValueInt("CompanyKey");
            item.Title = dataReader.GetValueText("Title");
            item.NumberOfUnits = dataReader.GetValueInt("NumberOfUnits");
            item.Address = dataReader.GetValueText("Address");
            item.Address2 = dataReader.GetValueText("Address2");
            item.City = dataReader.GetValueText("City");
            item.State = dataReader.GetValueText("State");
            item.Zip = dataReader.GetValueText("Zip");
            item.BidRequestAmount = dataReader.GetValueDecimal("BidRequestAmount");
            item.MinimumInsuranceAmount = dataReader.GetValueDecimal("MinimumInsuranceAmount");
            item.Description = dataReader.GetValueText("Description");
            item.Status = dataReader.GetValueInt("Status");
        }
    }
}
