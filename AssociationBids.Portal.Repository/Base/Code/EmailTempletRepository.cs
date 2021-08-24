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
   public  class EmailTemplateRepository: BaseRepository, IEmailTemplateRepository
    {


        public EmailTemplateRepository() { }

        public EmailTemplateRepository(string connectionString)
          : base(connectionString) { }
        protected void Load(DBDataReader dataReader, EmailTemplateModel item)
        {
            item.EmailTemplateKey = dataReader.GetValueInt("EmailTemplateKey");
            item.EmailTitle = dataReader.GetValueText("EmailTitle");
            item.Title = dataReader.GetValueText("Title");
            item.EmailSubject = dataReader.GetValueText("EmailSubject");
            item.Body = dataReader.GetValueText("Body");
          
        }
        protected void LoadIndexpaging(DBDataReader dataReader, EmailTemplateModel item)
        {
            item.EmailTemplateKey = dataReader.GetValueInt("EmailTemplateKey");
            item.EmailTitle = dataReader.GetValueText("EmailTitle");
            item.Title = dataReader.GetValueText("Title");
            item.EmailSubject = dataReader.GetValueText("EmailSubject");
            item.Body = dataReader.GetValueText("Body");
            item.TotalRecords = dataReader.GetValueInt("TotalRecord");
        }
        protected void LoadViewEdit(DBDataReader dataReader, EmailTemplateModel   item)
        {

            item.EmailTemplateKey = dataReader.GetValueInt("EmailTemplateKey");
            item.EmailTitle = dataReader.GetValueText("EmailTitle");
            item.lookUpType = dataReader.GetValueInt("lookUpType");
            item.Title = dataReader.GetValueText("Title");
            item.EmailSubject = dataReader.GetValueText("EmailSubject");
            item.Body = dataReader.GetValueText("Body");

        }

        public long Insert(EmailTemplateModel item, string strinbuilder, string strinbuilder1)
        {
            Int64 status = 0;

            try
            {

                string storedProcedure = "site_EmailTemplet_Insert";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters

                        commandWrapper.AddInputParameter("@EmailTitle", SqlDbType.VarChar, String.IsNullOrEmpty(item.EmailTitle) ? 0 : item.EmailTitle.Length, String.IsNullOrEmpty(item.EmailTitle) ? SqlString.Null : item.EmailTitle);

                        commandWrapper.AddInputParameter("@EmailSubject", SqlDbType.VarChar, String.IsNullOrEmpty(item.EmailSubject) ? 0 : item.EmailSubject.Length, String.IsNullOrEmpty(item.EmailSubject) ? SqlString.Null : item.EmailSubject);
                        commandWrapper.AddInputParameter("@Body", SqlDbType.VarChar, String.IsNullOrEmpty(item.Body) ? 0 : item.Body.Length, String.IsNullOrEmpty(item.Body) ? SqlString.Null : item.Body);

                        db.ExecuteNonQuery(commandWrapper);

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
                string storedProcedure = "site_EmailTemplate_Delete";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@EmailTemplateKey", SqlDbType.Int, id);

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

        public long Insert(EmailTemplateModel item)
        {
            Int64 status = 0;

            try
            {

                string storedProcedure = "site_EmailTemplate_Insert";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters
                        //commandWrapper.AddInputParameter("@EmailTemplateKey", SqlDbType.Int, (item.EmailTemplateKey == 0) ? SqlInt32.Null : item.EmailTemplateKey);
                        commandWrapper.AddInputParameter("@EmailTitle", SqlDbType.VarChar, string.IsNullOrEmpty(item.EmailTitle) ? "" : item.EmailTitle);
                        commandWrapper.AddInputParameter("@EmailSubject", SqlDbType.VarChar, string.IsNullOrEmpty(item.EmailSubject) ? "" : item.EmailSubject);
                        commandWrapper.AddInputParameter("@Body", SqlDbType.VarChar, string.IsNullOrEmpty(item.Body) ? "" : item.Body);
                        commandWrapper.AddInputParameter("@lookUpType", SqlDbType.Int, (item.lookUpType == 0) ? SqlInt32.Null : item.lookUpType);
                        commandWrapper.AddOutputParameter("@EmailTmpvalue", SqlDbType.Int);

                        db.ExecuteNonQuery(commandWrapper);
                        status = commandWrapper.GetValueInt("@EmailTmpvalue");
                    }
                }
            }
            catch (Exception ex)
            {
                // error occured...
                //throw;
            }

            return status;
        }

        public IList<EmailTemplateModel> GetAllLookUp()
        {
            throw new NotImplementedException();
        }

        protected void GetAllLookUp(DBDataReader dataReader, LookUpModel item)
        {
            item.LookUpKey = Convert.ToInt16( dataReader.GetValueText("LookUpKey"));
            item.Title  = dataReader.GetValueText("Title");
        }

        IList<LookUpModel> IEmailTemplateRepository.GetAllLookUp()
        {
            List<LookUpModel > itemList = new List<LookUpModel >();

            try
            {

                string storedProcedure = "site_lookup_SelectAll";
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

        public IList<EmailTemplateModel> GetAll()
        {
            return GetAll(new EmailTemplateModel ());
        }

        public virtual IList<EmailTemplateModel > GetAll(EmailTemplateModel  filter)
        {
            List<EmailTemplateModel > itemList = new List<EmailTemplateModel >();

            try
            {
                string storedProcedure = "site_EmailTemplate_SelectAll";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {



                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            EmailTemplateModel item = null;
                            while (dataReader.Read())
                            {
                                item = new EmailTemplateModel();

                                Load(dataReader, item);

                                itemList.Add(item);
                            }
                        }

                    }
                }
            }
            catch (Exception ex )
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
            }

            return itemList;
        }

        public virtual EmailTemplateModel GetDataViewEdit(int EmailTemplateKey)
        {
            EmailTemplateModel item = null;

            try
            {
                string storedProcedure = "site_EmailTemplate_SelectOneByEmailTemplateKey";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@EmailTemplateKey", SqlDbType.Int, EmailTemplateKey);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            while (dataReader.Read())
                            {
                                item = new EmailTemplateModel();

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

        long IEmailTemplateRepository.EmailTempletEdit(EmailTemplateModel item, int lookuptype)
        {
            Int64 status = 0;

            try
            {
                
                string storedProcedure = "site_EmailTemplate_Edit";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        commandWrapper.AddInputParameter("@EmailTemplateKey", SqlDbType.Int, (item.EmailTemplateKey == 0) ? SqlInt32.Null : item.EmailTemplateKey);
                        commandWrapper.AddInputParameter("@EmailTitle", SqlDbType.VarChar, string.IsNullOrEmpty(item.EmailTitle) ? "" : item.EmailTitle);
                        commandWrapper.AddInputParameter("@EmailSubject", SqlDbType.VarChar, string.IsNullOrEmpty(item.EmailSubject) ? "" : item.EmailSubject);
                        commandWrapper.AddInputParameter("@Body", SqlDbType.VarChar, string.IsNullOrEmpty(item.Body) ? "" : item.Body);
                        commandWrapper.AddInputParameter("@lookUpType", SqlDbType.Int, lookuptype);
                        commandWrapper.AddOutputParameter("@EmailTmpvalue", SqlDbType.Int);
                        db.ExecuteNonQuery(commandWrapper);
                        status = commandWrapper.GetValueInt("@EmailTmpvalue");
                    }
                  
                }
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
            }

            return status;
        }
        
        long IEmailTemplateRepository.EmailTempletupdates(EmailTemplateModel item)
        {
            Int64  status = 0;

            try
            {
                string storedProcedure = "site_EmailTemplate_update";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {


                        commandWrapper.AddInputParameter("@EmailTemplateKey", SqlDbType.Int, (item.EmailTemplateKey == 0) ? SqlInt32.Null : item.EmailTemplateKey);
                        commandWrapper.AddInputParameter("@EmailTitle", SqlDbType.VarChar, String.IsNullOrEmpty(item.EmailTitle) ? 0 : item.EmailTitle.Length, String.IsNullOrEmpty(item.EmailTitle) ? SqlString.Null : item.EmailTitle);
                        commandWrapper.AddInputParameter("@Title", SqlDbType.VarChar, String.IsNullOrEmpty(item.Title) ? 0 : item.Title.Length, String.IsNullOrEmpty(item.Title) ? SqlString.Null : item.Title);

                        commandWrapper.AddInputParameter("@EmailSubject", SqlDbType.VarChar, String.IsNullOrEmpty(item.EmailSubject) ? 0 : item.EmailSubject.Length, String.IsNullOrEmpty(item.EmailSubject) ? SqlString.Null : item.EmailSubject);
                        commandWrapper.AddInputParameter("@Body", SqlDbType.VarChar, String.IsNullOrEmpty(item.Body) ? 0 : item.Body.Length, String.IsNullOrEmpty(item.Body) ? SqlString.Null : item.Body);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        db.ExecuteNonQuery(commandWrapper);
                    }
                }
            }
            catch (Exception ex)
            {
                Common.Error.WriteErrorsToFile(ex.Message.ToString());
            }

            return status;
        }

        public List<EmailTemplateModel> SearchEmailTemplatet(string Search)
        {
            List<EmailTemplateModel> itemList = new List<EmailTemplateModel>();
            try
            {
                string storedProcedure = "USP_Resource_SelectIndexPaging";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters 
                        commandWrapper.AddInputParameter("@Search", SqlDbType.NText, (Search == "") ? "" : Search);
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            EmailTemplateModel item = null;
                            while (dataReader.Read())
                            {
                                item = new EmailTemplateModel();
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


        public List<EmailTemplateModel> SearchUser(long PageSize, long PageIndex, string Search, string Sort, string EmailTite,string EmailType)
        {
            List<EmailTemplateModel> itemList = new List<EmailTemplateModel>();
            try
            {

                string storedProcedure = "site_EmailTemplate_SelectIndexPaging";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters 
                        commandWrapper.AddInputParameter("@PageSize", SqlDbType.BigInt, (PageSize == 0) ? 0 : PageSize);
                        commandWrapper.AddInputParameter("@PageIndex", SqlDbType.BigInt, (PageIndex == 0) ? 0 : PageIndex);
                        commandWrapper.AddInputParameter("@Search", SqlDbType.NText, (Search == "") ? "" : Search);
                        commandWrapper.AddInputParameter("@Sort", SqlDbType.NText, (Sort == "") ? "" : Sort);
                        commandWrapper.AddInputParameter("@emailtitle", SqlDbType.NText, (EmailTite == "") ? "" : EmailTite);
                        commandWrapper.AddInputParameter("@emailtype", SqlDbType.NText, (EmailType == "") ? "" : EmailType);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            EmailTemplateModel item = null;
                            while (dataReader.Read())
                            {
                                item = new EmailTemplateModel();
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
        public List<EmailTemplateModel> SearchEmailTemplatet(string Search, string Sort)
        {
            List<EmailTemplateModel> itemList = new List<EmailTemplateModel>();
            try
            {
                string storedProcedure = "site_EmailTemplate_SelectIndexPaging";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters 
                        commandWrapper.AddInputParameter("@Search", SqlDbType.NText, (Search == "") ? "" : Search);
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            EmailTemplateModel item = null;
                            while (dataReader.Read())
                            {
                                item = new EmailTemplateModel();
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
        
    }
}
