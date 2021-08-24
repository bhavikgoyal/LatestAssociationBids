using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationBids.Portal.Repository.Base.Code
{
    public class MembershipRepository : BaseRepository, IMembershipRepository
    {
        public MembershipRepository() { }

        public MembershipRepository(string connectionString)
            : base(connectionString) { }

        protected void LoadViewEdit(DBDataReader dataReader, MembershipModel item)
        {

            item.MembershipKey  = dataReader.GetValueInt("MembershipKey");
            item.RenewalDate = dataReader.GetValueDateTime("RenewalDate");
            item.CurrentDate= dataReader.GetValueDateTime("CurrentDate");
            item.AutomaticRenewal = dataReader.GetValueBool("AutomaticRenewal");
            item.MaskedCCNumber = dataReader.GetValueText("MaskedCCNumber");
            item.EndDate = dataReader.GetValueDateTime("EndDate");
            item.StartDate = dataReader.GetValueDateTime("StartDate");
            try
            {
                item.priority = dataReader.GetValueInt("priority");
                item.NotificationId = dataReader.GetValueText("NotificationId");
                item.NotificationType = dataReader.GetValueText("NotificationType");
            }
            catch { }

        }

        public virtual bool Create(MembershipModel item)
        {
        	bool status = false;

        	try
        	{
        	    string storedProcedure = "gensp_Membership_InsertOne";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@vendorKey", SqlDbType.Int, (item.VendorKey == 0) ? SqlInt32.Null : item.VendorKey);
                        commandWrapper.AddInputParameter("@startDate", SqlDbType.DateTime, (item.StartDate == DateTime.MinValue) ? SqlDateTime.Null : item.StartDate);
                        commandWrapper.AddInputParameter("@endDate", SqlDbType.DateTime, (item.EndDate == DateTime.MinValue) ? SqlDateTime.Null : item.EndDate);
                        commandWrapper.AddInputParameter("@renewalDate", SqlDbType.DateTime, (item.RenewalDate == DateTime.MinValue) ? SqlDateTime.Null : item.RenewalDate);
                        commandWrapper.AddInputParameter("@automaticRenewal", SqlDbType.Bit, (item.AutomaticRenewal == false) ? SqlBoolean.Null : item.AutomaticRenewal);
                        commandWrapper.AddInputParameter("@lastModificationTime", SqlDbType.DateTime, (item.LastModificationTime == DateTime.MinValue) ? SqlDateTime.Null : item.LastModificationTime);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@membershipKey", SqlDbType.Int);
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        db.ExecuteNonQuery(commandWrapper);

                        // save the newly inserted key value
                        item.MembershipKey = commandWrapper.GetValueInt("@membershipKey");

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

        public virtual bool Update(MembershipModel item)
        {
        	bool status = false;

        	try
        	{
        	    string storedProcedure = "gensp_Membership_UpdateOneByMembershipKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@membershipKey", SqlDbType.Int, (item.MembershipKey == 0) ? SqlInt32.Null : item.MembershipKey);
                        commandWrapper.AddInputParameter("@vendorKey", SqlDbType.Int, (item.VendorKey == 0) ? SqlInt32.Null : item.VendorKey);
                        commandWrapper.AddInputParameter("@startDate", SqlDbType.DateTime, (item.StartDate == DateTime.MinValue) ? SqlDateTime.Null : item.StartDate);
                        commandWrapper.AddInputParameter("@endDate", SqlDbType.DateTime, (item.EndDate == DateTime.MinValue) ? SqlDateTime.Null : item.EndDate);
                        commandWrapper.AddInputParameter("@renewalDate", SqlDbType.DateTime, (item.RenewalDate == DateTime.MinValue) ? SqlDateTime.Null : item.RenewalDate);
                        commandWrapper.AddInputParameter("@automaticRenewal", SqlDbType.Bit, (item.AutomaticRenewal == false) ? SqlBoolean.Null : item.AutomaticRenewal);
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
        	    string storedProcedure = "gensp_Membership_DeleteOneByMembershipKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@membershipKey", SqlDbType.Int, id);

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

        public virtual MembershipModel Get(int id)
        {
            MembershipModel item = null;

            try
            {
                string storedProcedure = "gensp_Membership_SelectOneByMembershipKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@membershipKey", SqlDbType.Int, id);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            while (dataReader.Read())
                            {
                                item = new MembershipModel();

                                Load(dataReader, item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_Membership_SelectOneByMembershipKey");
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

        public virtual IList<MembershipModel> GetAll()
        {
            return GetAll(new MembershipFilterModel());
        }

        public virtual IList<MembershipModel> GetAll(MembershipFilterModel filter)
        {
            List<MembershipModel> itemList = new List<MembershipModel>();

            try
            {
                string storedProcedure = "gensp_Membership_SelectSomeBySearch";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@vendorKey", SqlDbType.Int, (filter.VendorKey == 0) ? SqlInt32.Null : filter.VendorKey);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            MembershipModel item = null;
                            while (dataReader.Read())
                            {
                                item = new MembershipModel();

                                Load(dataReader, item);

                                itemList.Add(item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_Membership_SelectSomeBySearch");
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

        //public virtual IList<MembershipModel> GetAll(MembershipFilterModel filter)
        //{
        //    List<MembershipModel> itemList = new List<MembershipModel>();

        //    try
        //    {
        //        string storedProcedure = "gensp_Membership_SelectSomeBySearch";
        //        using (Database db = new Database(ConnectionString))
        //        {
        //            DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure);

        //            // add the stored procedure input parameters
        //	        commandWrapper.AddInputParameter("@vendorKey", SqlDbType.Int, (filter.VendorKey == 0) ? SqlInt32.Null : filter.VendorKey);

        //            // add stored procedure output parameters
        //            commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

        //            using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
        //            {
        //                MembershipModel item = null;
        //                while (dataReader.Read())
        //                {
        //                    item = new MembershipModel();

        //                    Load(dataReader, item);

        //                    itemList.Add(item);
        //                }
        //            }

        //            // have to close reader before accessing output paramater values
        //            if (commandWrapper.GetValueInt("@errorCode") != 0)
        //            {
        //                throw new Exception("Error occured in stored procedure: gensp_Membership_SelectSomeBySearch");
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        // error occured...
        //        throw;
        //    }

        //    return itemList;
        //}


        public virtual MembershipModel GetDataViewEdit(int vendorKey)
        {
            MembershipModel item = null;

            try
            {
                string storedProcedure = "site_MemberShip_SelectAll_Copy";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@vendorKey", SqlDbType.Int, vendorKey);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            while (dataReader.Read())
                            {
                                item = new MembershipModel();

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

        public virtual IList<MembershipModel> GetAll(MembershipFilterModel filter, PagingModel paging)
        {
            List<MembershipModel> itemList = new List<MembershipModel>();

            try
            {
                string storedProcedure = "gensp_Membership_SelectSomeBySearchAndPaging";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@vendorKey", SqlDbType.Int, (filter.VendorKey == 0) ? SqlInt32.Null : filter.VendorKey);

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
                            MembershipModel item = null;
                            while (dataReader.Read())
                            {
                                item = new MembershipModel();

                                Load(dataReader, item);

                                itemList.Add(item);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: gensp_Membership_SelectSomeBySearchAndPaging");
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

        protected void Load(DBDataReader dataReader, MembershipModel item)
        {
            item.MembershipKey = dataReader.GetValueInt("MembershipKey");
            item.VendorKey = dataReader.GetValueInt("VendorKey");
            item.StartDate = dataReader.GetValueDateTime("StartDate");
            item.EndDate = dataReader.GetValueDateTime("EndDate");
            item.RenewalDate = dataReader.GetValueDateTime("RenewalDate");
            item.AutomaticRenewal = dataReader.GetValueBool("AutomaticRenewal");
            item.LastModificationTime = dataReader.GetValueDateTime("LastModificationTime");
        }
    }
}
