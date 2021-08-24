using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssociationBids.Portal.Common;
using AssociationBids.Portal.Model.Login;
using AssociationBids.Portal.Repository.Base;

namespace AssociationBids.Portal.Repository.Login.Code
{
    public class LoginRepository : BaseRepository,ILoginRepository
    {
        public LoginResponseModel GetUsersDetails(LoginModel loginModel)
        {
            LoginResponseModel loginResponseModel = null;
            string password = "";
            password = Security.Encrypt(loginModel.Password);
            //password = loginModel.Password;


            try
            {
                string storedProcedure = "site_User_Login";
                using (Database db = new Database(ConnectionString))
                {
                    using (DBCommandWrapper commandWrapper = db.GetStoredProcCommandWrapper(storedProcedure))
                    {

                        // add the stored procedure input parameters
                        commandWrapper.AddInputParameter("@email", SqlDbType.Text, loginModel.email);
                        commandWrapper.AddInputParameter("@password", SqlDbType.Text, password);

                        // add stored procedure output parameters
                        commandWrapper.AddOutputParameter("@errorCode", SqlDbType.Int);

                        using (DBDataReader dataReader = db.ExecuteReader(commandWrapper))
                        {
                            while (dataReader.Read())
                            {
                                loginResponseModel = new LoginResponseModel();

                                Load(dataReader, loginResponseModel);
                            }
                        }
                        // have to close reader before accessing output paramater values
                        if (commandWrapper.GetValueInt("@errorCode") != 0)
                        {
                            throw new Exception("Error occured in stored procedure: site_User_Login");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // error occured...
                throw;
            }

            return loginResponseModel;
        }
        protected void Load(DBDataReader dataReader, LoginResponseModel loginResponseModel)
        {

            if (dataReader.GetValueInt("Termaccpected") == 0)
            {
                loginResponseModel.Termaccpected = false;
            }
            else
            {
                loginResponseModel.Termaccpected = true;
            }
            loginResponseModel.UserId = dataReader.GetValueInt("UserKey");
            loginResponseModel.ResourceId = dataReader.GetValueInt("ResourceKey");
            loginResponseModel.GroupKey= dataReader.GetValueInt("GroupKey");
            loginResponseModel.UserName = dataReader.GetValueText("Username");
            loginResponseModel.Companyname = dataReader.GetValueText("companyname");
            loginResponseModel.PortalKey = dataReader.GetValueInt("PortalKey");
            loginResponseModel.password = dataReader.GetValueText("Password");
            loginResponseModel.password = Security.Decrypt(loginResponseModel.password);
            loginResponseModel.FirstTimeAccess = dataReader.GetValueBool("FirstTimeAccess");
            loginResponseModel.companyTypeKey = dataReader.GetValueInt("CompanyTypeKey");
            loginResponseModel.companyKey = dataReader.GetValueInt("CompanyKey");
            loginResponseModel.Title = dataReader.GetValueText("Title");
            loginResponseModel.ImageName = dataReader.GetValueText("ImageName");
            loginResponseModel.InsauranceKey = dataReader.GetValueInt("InsuranceKey");
        }
    }
}
