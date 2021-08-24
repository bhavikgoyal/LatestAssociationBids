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
    public class VendorPolicyRepository : BaseRepository, IVendorPolicyRepository
    {
        public VendorPolicyRepository() { }

        public VendorPolicyRepository(string connectionString)
        : base(connectionString) { }

        public List<InsuranceModel> GetInsurancePaging(int CompanyKey, int PageSize, int PageIndex, string Search, string Sort)
        {
            List<InsuranceModel> item = new List<InsuranceModel>();

            try
            {
                string storedProcedure = "site_Vendor_InsuranceByCompanyKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@CompanyKey", SqlDbType.Int, CompanyKey);
                        commandWrapper.AddInputParameter("@PageSize", SqlDbType.Int, PageSize);
                        commandWrapper.AddInputParameter("@PageIndex", SqlDbType.Int, PageIndex);
                        commandWrapper.AddInputParameter("@Search", SqlDbType.NVarChar, Search);
                        commandWrapper.AddInputParameter("@Sort", SqlDbType.NVarChar, Sort);


                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            while (dataReader.Read())
                            {
                                InsuranceModel insurance = new InsuranceModel();
                                Load(dataReader, insurance);
                                item.Add(insurance);
                            }
                        }

                        // have to close reader before accessing output paramater values
                        //if (commandWrapper.GetValueInt("@errorCode") != 0)
                        //{
                        //    throw new Exception("Error occured in stored procedure: site_Vendor_InsuranceByCompanyKey");
                        //}
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
        protected void Load(DBDataReader dataReader, InsuranceModel item)
        {
            item.InsuranceKey = dataReader.GetValueInt("InsuranceKey");
            item.VendorKey = dataReader.GetValueInt("VendorKey");
            item.InsuranceAmount = dataReader.GetValueDecimal("InsuranceAmount");
            item.PolicyNumber = dataReader.GetValueText("PolicyNumber");
            item.EndDate = dataReader.GetValueDateTime("EndDate");
           
            try
            {
                item.EndDates = item.EndDate.ToString("MM/dd/yyyy");
            }
            catch (Exception)
            { }


            item.TotalRecords = dataReader.GetValueInt("TotalRecord");
            try
            {
                item.priority = dataReader.GetValueInt("priority");
                item.NotificationId = dataReader.GetValueText("NotificationId");
                item.NotificationType = dataReader.GetValueText("NotificationType");
            }
            catch { }
        }

        public IList<VendorManagerModel> GetbindDocumentByInsuranceKey(int CompanyKey, int InsuranceKey)
        {
            List<VendorManagerModel> itemList = new List<VendorManagerModel>();

            try
            {

                string storedProcedure = "site_VendorManagerDocument_SelectByCompanyAndInsuranceKeyV2_New";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        commandWrapper.AddInputParameter("@CompanyKey", SqlDbType.Int, CompanyKey);
                        commandWrapper.AddInputParameter("@InsuranceKey", SqlDbType.Int, InsuranceKey);
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            VendorManagerModel item = null;
                            while (dataReader.Read())
                            {
                                item = new VendorManagerModel();
                                item.Document = new DocumentModel();
                                item.Insurance = new InsuranceModel();
                                item.Resource = new ResourceModel();
                                item.ServiceModel = new VendorServiceModel();
                                item.Vendor = new VendorManagerVendorModel();
                                GetbindDocument(dataReader, item);
                                itemList.Add(item);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                // error occured...
                throw;
            }

            return itemList;
        }

        protected void GetbindDocument(DBDataReader dataReader, VendorManagerModel item)
        {
            try
            {
                item.Insurance.InsuranceKey = dataReader.GetValueInt("InsuranceKey");
                item.Vendor.CompanyKey = dataReader.GetValueInt("VendorKey");
                item.Insurance.VendorKey = dataReader.GetValueInt("VendorKey");
                item.Insurance.PolicyNumber = dataReader.GetValueText("PolicyNumber");
                item.Insurance.InsuranceAmount = dataReader.GetValueDecimal("InsuranceAmount");
                item.Insurance.AgentName = dataReader.GetValueText("AgentName");
                item.Insurance.Email = dataReader.GetValueText("Email");
                item.Vendor.Email = dataReader.GetValueText("Email");
                item.Vendor.CompanyName = dataReader.GetValueText("CompanyName");
                item.Document.DocumentKey = dataReader.GetValueInt("DocumentKey");
                item.Document.FileName = dataReader.GetValueText("FileName");
                item.Document.ModuleKey = dataReader.GetValueInt("ModuleKey");
                item.Document.FileSize = dataReader.GetValueDouble("FileSize");
                item.Insurance.StartDate = dataReader.GetValueDateTime("StartDate");
                try
                {
                    item.Insurance.StartDates = item.Insurance.StartDate.ToString("MM/dd/yyyy");
                }catch (Exception)
                {}
                item.Insurance.EndDate = dataReader.GetValueDateTime("EndDate");
                try
                {
                    item.Insurance.EndDates = item.Insurance.EndDate.ToString("MM/dd/yyyy");
                }
                catch (Exception)
                { }
                item.Insurance.RenewalDate = dataReader.GetValueDateTime("RenewalDate");
                try
                {
                    item.Insurance.RenewalDates = item.Insurance.RenewalDate.ToString("MM/dd/yyyy");
                }
                catch (Exception)
                { }
                try
                {
                    item.Insurance.priority = dataReader.GetValueInt("priority");
                    item.Insurance.NotificationId = dataReader.GetValueText("NotificationId");
                    item.Insurance.NotificationType = dataReader.GetValueText("NotificationType");
                }
                catch { }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public VendorManagerVendorModel GetVendorByCompanyKey(int CompanyKey)
        {
            VendorManagerVendorModel item = null;

            try
            {
                string storedProcedure = "site_vendorManager_SelectOneByvendorKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@CompanyKey", SqlDbType.Int, CompanyKey);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            while (dataReader.Read())
                            {
                                item = new VendorManagerVendorModel();

                                LoadViewEdit(dataReader, item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return item;
        }

        protected void LoadViewEdit(DBDataReader dataReader, VendorManagerVendorModel item)
        {
            item.CompanyKey = dataReader.GetValueInt("CompanyKey");
            item.CompanyName = dataReader.GetValueText("CompanyName");
            item.Name = dataReader.GetValueText("Name");
            item.Fax = dataReader.GetValueText("Fax");
            item.Address = dataReader.GetValueText("Address");
            item.Address2 = dataReader.GetValueText("Address2");
            item.City = dataReader.GetValueText("City");
            item.State = dataReader.GetValueText("State");
            item.Zip = dataReader.GetValueText("Zip");
            item.Website = dataReader.GetValueText("Website");
            item.CellPhone = dataReader.GetValueText("CellPhone");
            item.Work = dataReader.GetValueText("Work");
            item.Work2 = dataReader.GetValueText("Work2");
            item.Email = dataReader.GetValueText("Email");
            item.ServiceKey = dataReader.GetValueInt("ServiceKey");
            item.ServiceKey1 = dataReader.GetValueInt("ServiceKey");
            item.ServiceKey2 = dataReader.GetValueInt("ServiceKey");

        }

        public long VendorManagerAddInsurance(InsuranceModel item)
        {
            long id = 0;
            try
            {
                using (Database db = new Database(ConnectionString))
                {
                    //DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper("gensp_Insurance_InsertOne");
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper("gensp_Insurance_InsertOne"))
                    {
                        commandWrapper.AddInputParameter("@vendorKey", SqlDbType.Int, (item.VendorKey == 0) ? SqlInt32.Null : item.VendorKey);
                        commandWrapper.AddInputParameter("@companyName ", SqlDbType.VarChar, String.IsNullOrEmpty(item.CompanyName) ? 0 : item.CompanyName.Length, String.IsNullOrEmpty(item.CompanyName) ? SqlString.Null : item.CompanyName);
                        commandWrapper.AddInputParameter("@policyNumber ", SqlDbType.VarChar, String.IsNullOrEmpty(item.PolicyNumber) ? 0 : item.PolicyNumber.Length, String.IsNullOrEmpty(item.PolicyNumber) ? SqlString.Null : item.PolicyNumber);
                        commandWrapper.AddInputParameter("@insuranceAmount", SqlDbType.Money, (item.InsuranceAmount == 0) ? 0 : item.InsuranceAmount);
                        commandWrapper.AddInputParameter("@agentName", SqlDbType.VarChar, String.IsNullOrEmpty(item.AgentName) ? 0 : item.AgentName.Length, String.IsNullOrEmpty(item.AgentName) ? SqlString.Null : item.AgentName);
                        commandWrapper.AddInputParameter("@email", SqlDbType.VarChar, String.IsNullOrEmpty(item.Email) ? 0 : item.Email.Length, String.IsNullOrEmpty(item.Email) ? SqlString.Null : item.Email);
                        commandWrapper.AddInputParameter("@Work", SqlDbType.VarChar, String.IsNullOrEmpty(item.Work) ? 0 : item.Work.Length, String.IsNullOrEmpty(item.Work) ? SqlString.Null : item.Work);
                        commandWrapper.AddInputParameter("@cellphone", SqlDbType.VarChar, String.IsNullOrEmpty(item.CellPhone) ? 0 : item.CellPhone.Length, String.IsNullOrEmpty(item.CellPhone) ? SqlString.Null : item.CellPhone);
                        commandWrapper.AddInputParameter("@Fax", SqlDbType.VarChar, String.IsNullOrEmpty(item.Fax) ? 0 : item.Fax.Length, String.IsNullOrEmpty(item.Fax) ? SqlString.Null : item.Fax);
                        commandWrapper.AddInputParameter("@Address", SqlDbType.VarChar, String.IsNullOrEmpty(item.Address) ? 0 : item.Address.Length, String.IsNullOrEmpty(item.Address) ? SqlString.Null : item.Address);
                        commandWrapper.AddInputParameter("@Address2", SqlDbType.VarChar, String.IsNullOrEmpty(item.Address2) ? 0 : item.Address2.Length, String.IsNullOrEmpty(item.Address2) ? SqlString.Null : item.Address2);
                        commandWrapper.AddInputParameter("@City", SqlDbType.VarChar, String.IsNullOrEmpty(item.City) ? 0 : item.City.Length, String.IsNullOrEmpty(item.City) ? SqlString.Null : item.City);
                        commandWrapper.AddInputParameter("@State", SqlDbType.VarChar, String.IsNullOrEmpty(item.State) ? 0 : item.State.Length, String.IsNullOrEmpty(item.State) ? SqlString.Null : item.State);
                        commandWrapper.AddInputParameter("@Zip", SqlDbType.VarChar, String.IsNullOrEmpty(item.Zip) ? 0 : item.Zip.Length, String.IsNullOrEmpty(item.Zip) ? SqlString.Null : item.Zip);

                        commandWrapper.AddInputParameter("@startDate", SqlDbType.DateTime, item.StartDate);

                        commandWrapper.AddInputParameter("@endDate", SqlDbType.DateTime, item.EndDate);
                        commandWrapper.AddInputParameter("@renewalDate", SqlDbType.DateTime, item.RenewalDate.Year < 2000 ? SqlDateTime.Null : item.RenewalDate);

                        commandWrapper.AddInputParameter("@Status", SqlDbType.Int, 1);
                        commandWrapper.AddOutputParameter("@insuranceKey", SqlDbType.Int);
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        id = db.ExecuteNonQuery(commandWrapper);
                        id = commandWrapper.GetValueInt("@insuranceKey");
                        if (id <= 0 || commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            id = 0;
                        }

                    }
                }
            }
            catch (Exception ex)
            {

            }
            return id;
        }

        public virtual bool DocumentDelete(int BidRequestKey, string Docname)
        {
            bool status = false;

            try
            {
                string storedProcedure = "site_Document_BidRequestDelete";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@BidRequestKey", SqlDbType.Int, BidRequestKey);
                        commandWrapper.AddInputParameter("@Docname", SqlDbType.VarChar, Docname.Trim());

                        // add stored procedure output parameters
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

        public long VendorManagerEditInsurance(InsuranceModel item)
        {
            long id = 0;
            try
            {
                using (Database db = new Database(ConnectionString))
                {
                    //DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper("gensp_Insurance_InsertOne");
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper("site_Insurance_UpdateOne"))
                    {
                        commandWrapper.AddInputParameter("@InsuranceKey", SqlDbType.Int, (item.InsuranceKey == 0) ? SqlInt32.Null : item.InsuranceKey);
                        commandWrapper.AddInputParameter("@vendorKey", SqlDbType.Int, (item.VendorKey == 0) ? SqlInt32.Null : item.VendorKey);
                        commandWrapper.AddInputParameter("@policyNumber ", SqlDbType.VarChar, String.IsNullOrEmpty(item.PolicyNumber) ? 0 : item.PolicyNumber.Length, String.IsNullOrEmpty(item.PolicyNumber) ? SqlString.Null : item.PolicyNumber);
                        commandWrapper.AddInputParameter("@insuranceAmount", SqlDbType.Money, (item.InsuranceAmount == 0) ? 0 : item.InsuranceAmount);

                        commandWrapper.AddInputParameter("@startDate", SqlDbType.DateTime, item.StartDate);

                        commandWrapper.AddInputParameter("@endDate", SqlDbType.DateTime, item.EndDate);
                        commandWrapper.AddInputParameter("@renewalDate", SqlDbType.DateTime, item.RenewalDate.Year < 2000 ? SqlDateTime.Null : item.RenewalDate);

                        commandWrapper.AddInputParameter("@Status", SqlDbType.Int, 1);
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        id = db.ExecuteNonQuery(commandWrapper);
                        int status = commandWrapper.GetValueInt("@errorCode");
                        if (commandWrapper.GetValueInt("@errorCode") == 0)
                        {
                            id = 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return id;
        }

        public virtual bool UpdateDocInsert(int BidRequestKey, string FileName, string FileSize, int ModuleKey)
        {
            bool status = false;

            try
            {
                string storedProcedure = "site_Document_InsuranceUpdate";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@InsuranceKey", SqlDbType.Int, BidRequestKey);
                        commandWrapper.AddInputParameter("@FileName", SqlDbType.VarChar, String.IsNullOrEmpty(FileName) ? 0 : FileName.Length, String.IsNullOrEmpty(FileName) ? SqlString.Null : FileName);
                        commandWrapper.AddInputParameter("@FileSize", SqlDbType.VarChar, String.IsNullOrEmpty(FileSize) ? 0 : FileSize.Length, String.IsNullOrEmpty(FileSize) ? SqlString.Null : FileSize);
                        commandWrapper.AddInputParameter("@modulekeyval", SqlDbType.Int, ModuleKey);
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
