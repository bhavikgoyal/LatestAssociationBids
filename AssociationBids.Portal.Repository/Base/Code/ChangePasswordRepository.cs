using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model;
using AssociationBids.Portal.Repository.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationBids.Portal.Repository.ChangePassword
{
    public class ChangePasswordRepository : BaseRepository, IChangePasswordRepository
    {
        public ChangePasswordRepository() { }

        public ChangePasswordRepository(string connectionString)
            : base(connectionString) { }
        public int ChangePassword(ChangePasswordModel changePasswordModel)
        {
            string Oldpassword = "";
            Oldpassword = Security.Encrypt(changePasswordModel.OldPassword);
            string Newpassword = "";
            Newpassword = Security.Encrypt(changePasswordModel.NewPassword);
            //string loginResponseModel = null;
            int dataReader = 0;
            try
            {
                string storedProcedure = "[site_User_ChangePassword]";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        commandWrapper.AddInputParameter("@UserId", SqlDbType.Int, changePasswordModel.UserId);
                        commandWrapper.AddInputParameter("@NewPassword", SqlDbType.Text, Newpassword);
                        commandWrapper.AddInputParameter("@OldPassword", SqlDbType.Text, Oldpassword);

                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);
                        commandWrapper.AddOutputParameter("@Status", SqlDbType.Int);

                        dataReader = db.ExecuteNonQuery(commandWrapper);

                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: USP_User_ChangePassword");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // error occured...
                throw;
            }

            return dataReader;
        }





        protected void CheckToken(DBDataReader dataReader, ChangePasswordModel item)
        {
            item.TokenReset = dataReader.GetValueText("TokenReset");
            item.TokenExpirationDate = dataReader.GetValueDateTime("ResetExpirationDate");
        }

        public List<ChangePasswordModel> CheckToken(int ResourceKey)
        {
            bool status = false;
            List<ChangePasswordModel> itemList = new List<ChangePasswordModel>();
            try
            {
                string storedProcedure = "site_User_CheckToken";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        // add the stored procedure input parameters 
                        commandWrapper.AddInputParameter("@ResourceKey", SqlDbType.Int, ResourceKey);
                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            ChangePasswordModel item = null;
                            while (dataReader.Read())
                            {
                                item = new ChangePasswordModel();
                                CheckToken(dataReader, item);
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

        public int ResetPassword(ChangePasswordModel changePasswordModel)
        {
            string Newpassword = "";
            Newpassword = Security.Encrypt(changePasswordModel.NewPassword);
            //string loginResponseModel = null;
            int dataReader = 0;
            try
            {
                string storedProcedure = "[site_User_ResetPassword_New]";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        //commandWrapper.AddInputParameter("@UserId", SqlDbType.Int, changePasswordModel.UserKey);
                        commandWrapper.AddInputParameter("@ResourceKey", SqlDbType.Int, changePasswordModel.UserKey);
                        commandWrapper.AddInputParameter("@NewPassword", SqlDbType.Text, Newpassword);

                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);
                        commandWrapper.AddOutputParameter("@Status", SqlDbType.Int);

                        dataReader = db.ExecuteNonQuery(commandWrapper);

                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: USP_User_ChangePassword");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // error occured...
                throw;
            }

            return dataReader;
        }

        public int ResetPasswordByUser(ChangePasswordModel changePasswordModel)
        {
            string Newpassword = "";
            Newpassword = Security.Encrypt(changePasswordModel.NewPassword);
            //string loginResponseModel = null;
            int dataReader = 0;
            try
            {
                string storedProcedure = "[site_User_ResetPassword]";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {
                        //commandWrapper.AddInputParameter("@UserId", SqlDbType.Int, changePasswordModel.UserKey);
                        commandWrapper.AddInputParameter("@UserId", SqlDbType.Int, changePasswordModel.UserKey);
                        commandWrapper.AddInputParameter("@NewPassword", SqlDbType.Text, Newpassword);

                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);
                        commandWrapper.AddOutputParameter("@Status", SqlDbType.Int);

                        dataReader = db.ExecuteNonQuery(commandWrapper);

                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: USP_User_ChangePassword");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // error occured...
                throw;
            }

            return dataReader;
        }


        public virtual ChangePasswordModel GeAgreementDetails()
        {
            ChangePasswordModel item = null;

            try
            {

                string storedProcedure = "site_Agreement_SelectShowForVendorRegistration";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        //commandWrapper.AddInputParameter("@CompanyKey", SqlDbType.Int, companykey);

                        // add stored procedure output parameters
                        //commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            while (dataReader.Read())
                            {
                                item = new ChangePasswordModel();

                                LoadAgreementViewEdit(dataReader, item);
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



        protected void LoadAgreementViewEdit(DBDataReader dataReader, ChangePasswordModel item)
        {
            item.BindAgreementDetails = dataReader.GetValueText("Description");
            item.AggrementKey = dataReader.GetValueInt("AgreementKey");
        }


        public virtual bool UpdateTermsConditions(string Term, int UserKey, int  AggrementKey)
        {
            bool status = false;

            try
            {
                string storedProcedure = "site_UpdateUserTerms_Users";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@Term", SqlDbType.NVarChar, Term);
                        commandWrapper.AddInputParameter("@UserKey", SqlDbType.Int, UserKey);
                        commandWrapper.AddInputParameter("@AggrementKey", SqlDbType.Int, AggrementKey);
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);
                        db.ExecuteNonQuery(commandWrapper);
                        // save the newly inserted key value
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

    }
}


