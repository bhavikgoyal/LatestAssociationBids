using System;
using System.Data;
using System.Data.SqlClient;

namespace AssociationBids.Portal.Repository.Base
{
    /// <summary>
    /// This object has all the data related functions needed execute sql commands and retrieve recordsets and datasets.
    /// All connections and command objects are disposed upon exit
    /// </summary>
    public class Database : IDatabase
    {
        private SqlConnection __connection;
        private DBCommandWrapper __command;
        private DBDataReader __dataReader;

        /// <summary>
        /// This object has all the data related functions needed execute sql commands and retrieve recordsets and datasets.
        /// All connections and command objects are disposed upon exit
        /// </summary>
        public Database()
        {
            CreateConnection(GetConnectionString("DefaultConnection"));
        }

        /// <summary>
        /// This object has all the data related functions needed execute sql commands and retrieve recordsets and datasets.
        /// All connections and command objects are disposed upon exit
        /// </summary>
        /// <param name="connectionStringName">This is the name of the connection string configuration key in the web.config file</param>
        public Database(string connectionStringName)
        {
            if (connectionStringName != null && connectionStringName.Length > 0)
            {
                CreateConnection(GetConnectionString(connectionStringName));
            }
            else
            {
                CreateConnection(GetConnectionString("DefaultConnection"));
            }
        }

        /// <summary>
        /// Returns the application configuration key value for the given key
        /// </summary>
        /// <param name="name">Name of application configuration key</param>
        /// <returns>The connection string</returns>
        private string GetConnectionString(string name)
        {
            return (System.Configuration.ConfigurationManager.ConnectionStrings[name].ConnectionString);
        }

        /// <summary>
        /// Creates a new connection object based on the connection string passed in
        /// </summary>
        /// <param name="connectionString">Connection string to be used for the connection object</param>
        private void CreateConnection(string connectionString)
        {
            try
            {
                __connection = new SqlConnection(connectionString);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Checks to see if the connection object exists and is not open before opening the connection
        /// </summary>
        public void OpenConnection()
        {
            try
            {
                if (__connection != null && __connection.State != ConnectionState.Open)
                {
                    __connection.Open();
                }

            }
            catch (InvalidOperationException ex1) {
                SqlConnection.ClearPool(__connection);
                try
                {
                    if (__connection != null && __connection.State != ConnectionState.Open)
                    {
                        __connection.Open();
                    }
                }
                catch (InvalidOperationException)
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void CloseConnection()
        {
            if (__connection != null && __connection.State == ConnectionState.Open)
            {
                __connection.Close();
            }
        }
        /// <summary>
        /// Creates a new DBCommandWrapper object for the sql query passed in
        /// </summary>
        /// <param name="query">Query to be used with the command object</param>
        /// <returns></returns>
        public DBCommandWrapper GetSqlStringCommandWrapper(string query)
        {
            __command = new DBCommandWrapper();

            __command.CreateSqlStringCommandWrapper(query);
            return __command;
        }

        /// <summary>
        /// Creates a new DBCommandWrapper object for the stored procedure passed in
        /// </summary>
        /// <param name="storedProcedure">Name of stored procedure to be used with the command object</param>
        /// <returns></returns>
        public DBCommandWrapper GetStoredProcCommandWrapper(string storedProcedure)
        {
            __command = new DBCommandWrapper();

            __command.CreateStoredProcCommandWrapper(storedProcedure);
            return __command;
        }

        /// <summary>
        /// Returns a Data Reader using the command object passed in
        /// </summary>
        /// <param name="commandWrapper">Command object to be executed by the connection</param>
        /// <returns></returns>
        public DBDataReader ExecuteReader(DBCommandWrapper commandWrapper)
        {
            try
            {
                // open the connection
                if (__connection != null && __connection.State != ConnectionState.Open)
                {
                    OpenConnection();

                }
                    commandWrapper.DBCommand.Connection = __connection;
                __dataReader = new DBDataReader(commandWrapper.DBCommand.ExecuteReader());

                return __dataReader;
            }
            catch (Exception ex)
            {
                CloseConnection();
                throw ex;
            }
           
        }

        /// <summary>
        /// Returns a Data Set using the command object passed in
        /// </summary>
        /// <param name="commandWrapper">Command object to be executed by the connection</param>
        /// <returns></returns>
        public DataSet ExecuteDataSet(DBCommandWrapper commandWrapper)
        {
            return ExecuteDataSet(commandWrapper, null);
        }

        /// <summary>
        /// Returns a Data Set using the command object passed in
        /// </summary>
        /// <param name="commandWrapper">Command object to be executed by the connection</param>
        /// <returns></returns>
        public DataSet ExecuteDataSet(DBCommandWrapper commandWrapper, string tableName)
        {
            try
            {
                // open the connection
                OpenConnection();
                commandWrapper.DBCommand.Connection = __connection;

                DataSet dataSet = new DataSet();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(commandWrapper.DBCommand);

                if (tableName != null && tableName.Length > 0)
                {
                    dataAdapter.Fill(dataSet, tableName);
                }
                else
                {
                    dataAdapter.Fill(dataSet);
                }

                return dataSet;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CloseConnection();
                //__connection.Close();
            }
        }

        /// <summary>
        /// Executes the command object and returns status
        /// </summary>
        /// <param name="commandWrapper"></param>
        /// <returns>Status of command execution</returns>
        public int ExecuteNonQuery(DBCommandWrapper commandWrapper)
        {
            try
            {
                // open the connection
                OpenConnection();
                commandWrapper.DBCommand.Connection = __connection;

                return commandWrapper.DBCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                // close connection since we don't need it anymore
                CloseConnection();
                //__connection.Close();
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // dispose managed resources
                if (__dataReader != null)
                {
                    __dataReader.Dispose();
                }
                if (__command != null)
                {
                    __command.Dispose();
                }
                if (__connection != null)
                {
                    __connection.Close();
                }
            }
            // free native resources
        }

        public void Dispose()
        {
            CloseConnection();
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

    /// <summary>
    /// This object wraps all command specific functionality for ease of use. 
    /// All connection and command objects are disposed upon exit.
    /// </summary>
    public class DBCommandWrapper : System.IDisposable
    {
        private SqlCommand __command;

        /// <summary>
        /// This object wraps all command specific functionality for ease of use. 
        /// All connection and command objects are disposed upon exit.
        /// </summary>
        public DBCommandWrapper() { }

        /// <summary>
        /// Returns the SQLCommand object used by this DBCommandWrapper object
        /// </summary>
        public SqlCommand DBCommand
        {
            get { return __command; }
        }

        /// <summary>
        /// Creates the SQLCommand object using the query passed in
        /// </summary>
        /// <param name="query">Query to be used by the command object</param>
        public void CreateSqlStringCommandWrapper(string query)
        {
            try
            {
                // create command instance
                __command = new SqlCommand(query);
                __command.CommandType = CommandType.Text;
            }
            catch (Exception)
            {
                closeConnection();
                throw;
            }
        }

        /// <summary>
        /// Creates the SQLCommand object using the stored procedure passed in
        /// </summary>
        /// <param name="storedProcedure">Stored Procedure to be used by the command object</param>
        public void CreateStoredProcCommandWrapper(string storedProcedure)
        {
            try
            {
                // create command instance
                __command = new SqlCommand(storedProcedure);
                __command.CommandType = CommandType.StoredProcedure;
            }
            catch (Exception)
            {
                closeConnection();
                throw;
            }
        }

        #region Parameter functions
        /// <summary>
        /// Adds a parameter to the existing command object
        /// </summary>
        /// <param name="parameter">SQLParameter object to be added to the command object</param>
        public void AddParameter(SqlParameter parameter)
        {
            __command.Parameters.Add(parameter);
        }

        public void closeConnection() {
            if(__command.Connection.State == ConnectionState.Open)
                __command.Connection.Close();
        }

        /// <summary>
        /// Adds an Input parameter to the existing command object
        /// </summary>
        /// <param name="parameterName">Parameter Name</param>
        /// <param name="dbType">Parameter Type</param>
        /// <param name="parameterValue">Parameter Value</param>
        public void AddInputParameter(string parameterName, SqlDbType dbType, object parameterValue)
        {
            SqlParameter parameter = new SqlParameter(parameterName, dbType);
            parameter.Value = parameterValue;
            parameter.Direction = ParameterDirection.Input;

            __command.Parameters.Add(parameter);
        }

        /// <summary>
        /// Adds an Input parameter to the existing command object
        /// </summary>
        /// <param name="parameterName">Parameter Name</param>
        /// <param name="dbType">Parameter Type</param>
        /// <param name="size">Parameter Size</param>
        /// <param name="parameterValue">Parameter Value</param>
        public void AddInputParameter(string parameterName, SqlDbType dbType, int size, object parameterValue)
        {
            SqlParameter parameter = new SqlParameter(parameterName, dbType, size);
            parameter.Value = parameterValue;
            parameter.Direction = ParameterDirection.Input;

            __command.Parameters.Add(parameter);
        }

        /// <summary>
        /// Adds an Output parameter to the existing command object
        /// </summary>
        /// <param name="parameterName">Parameter Name</param>
        /// <param name="dbType">Parameter Type</param>
        public void AddOutputParameter(string parameterName, SqlDbType dbType)
        {
            SqlParameter parameter = new SqlParameter(parameterName, dbType);
            parameter.Direction = ParameterDirection.Output;

            __command.Parameters.Add(parameter);
        }

        /// <summary>
        /// Adds an Output parameter to the existing command object
        /// </summary>
        /// <param name="parameterName">Parameter Name</param>
        /// <param name="dbType">Parameter Type</param>
        /// <param name="size">Parameter Size</param>
        public void AddOutputParameter(string parameterName, SqlDbType dbType, int size)
        {
            SqlParameter parameter = new SqlParameter(parameterName, dbType, size);
            parameter.Direction = ParameterDirection.Output;

            __command.Parameters.Add(parameter);
        }

        #endregion

        #region Get Parameter functions
        /// <summary>
        /// Checks to see whether a command parameter is null or its value is null or its value is empty
        /// </summary>
        /// <param name="name">Parameter name to check value for</param>
        /// <returns>True if value is null, False if it isn't</returns>
        private bool IsParameterNull(string name)
        {
            return (__command.Parameters[name] == null || __command.Parameters[name].Value == null || __command.Parameters[name].Value.ToString() == "");
        }

        /// <summary>
        /// Gets the parameter value converted to string
        /// </summary>
        /// <param name="name">Parameter name to get value for</param>
        /// <returns>Value of parameter converted to string</returns>
        public string GetValueText(string name)
        {
            return (!IsParameterNull(name) ? __command.Parameters[name].Value.ToString() : "");
        }

        /// <summary>
        /// Gets the parameter value converted to an integer
        /// </summary>
        /// <param name="name">Parameter name to get value for</param>
        /// <returns>Value of parameter converted to an integer</returns>
        public int GetValueInt(string name)
        {
            return (!IsParameterNull(name) ? int.Parse(__command.Parameters[name].Value.ToString()) : 0);
        }

        /// <summary>
        /// Gets the parameter value converted to a double
        /// </summary>
        /// <param name="name">Parameter name to get value for</param>
        /// <returns>Value of parameter converted to a double</returns>
        public double GetValueDouble(string name)
        {
            return (!IsParameterNull(name) ? double.Parse(__command.Parameters[name].Value.ToString()) : 0.0);
        }

        /// <summary>
        /// Gets the parameter value converted to a Decimal
        /// </summary>
        /// <param name="name">Parameter name to get value for</param>
        /// <returns>Value of parameter converted to a Decimal</returns>
        public Decimal GetValueDecimal(string name)
        {
            return (!IsParameterNull(name) ? Decimal.Parse(__command.Parameters[name].Value.ToString()) : 0);
        }

        /// <summary>
        /// Gets the parameter value converted to a boolean
        /// </summary>
        /// <param name="name">Parameter name to get value for</param>
        /// <returns>Value of parameter converted to a boolean</returns>
        public bool GetValueBool(string name)
        {
            return (!IsParameterNull(name) ? bool.Parse(__command.Parameters[name].Value.ToString()) : false);
        }

        /// <summary>
        /// Gets the parameter value converted to a date
        /// </summary>
        /// <param name="name">Parameter name to get value for</param>
        /// <returns>Value of parameter converted to a date</returns>
        public DateTime GetValueDateTime(string name)
        {
            return (!IsParameterNull(name) ? DateTime.Parse(__command.Parameters[name].Value.ToString()) : DateTime.MinValue);
        }

        /// <summary>
        /// Gets the parameter value converted to a Guid (Uniqueidentifier)
        /// </summary>
        /// <param name="name">Parameter name to get value for</param>
        /// <returns>Value of parameter converted to a Guid (Uniqueidentifier)</returns>
        public Guid GetValueGUID(string name)
        {
            return (!IsParameterNull(name) ? new Guid(__command.Parameters[name].Value.ToString()) : Guid.Empty);
        }

        #endregion

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // dispose managed resources
                if (__command != null)
                {
                    closeConnection();
                    __command.Dispose();
                }
            }
            // free native resources
        }

        public void Dispose()
        {
            closeConnection();
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        internal void AddInputParameter(string v1 , int v2, int v3)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// This object wraps all data reader specific functionality for ease of use. 
    /// </summary>
    public class DBDataReader : System.IDisposable
    {
        private SqlDataReader __dataReader;

        /// <summary>
        /// This object wraps all data reader specific functionality for ease of use. 
        /// </summary>
        /// <param name="dataReader">Data reader to wrap the functionality for</param>
        public DBDataReader(SqlDataReader dataReader)
        {
            __dataReader = dataReader;
        }

        /// <summary>
        /// Advances the data reader to the next record
        /// </summary>
        /// <returns></returns>
        public bool Read()
        {
            return __dataReader.Read();
        }

        #region Get Field Value functions
        /// <summary>
        /// Checks to see if a particular field from the record set is null or not
        /// </summary>
        /// <param name="name">Name of field to check</param>
        /// <returns></returns>
        private bool IsFieldNull(string name)
        {
            return (__dataReader[name] == System.DBNull.Value);
        }

        /// <summary>
        /// Returns the field value converted to string
        /// </summary>
        /// <param name="name">Field Name</param>
        /// <returns></returns>
        public string GetValueText(string name)
        {
            return (!IsFieldNull(name) ? __dataReader[name].ToString() : "");
        }

        /// <summary>
        /// Returns the field value converted to an integer
        /// </summary>
        /// <param name="name">Field Name</param>
        /// <returns></returns>
        public int GetValueInt(string name)
        {
            return (!IsFieldNull(name) ? int.Parse(__dataReader[name].ToString()) : 0);
        }

        /// <summary>
        /// Returns the field value converted to double
        /// </summary>
        /// <param name="name">Field Name</param>
        /// <returns></returns>
        public double GetValueDouble(string name)
        {
            return (!IsFieldNull(name) ? double.Parse(__dataReader[name].ToString()) : 0.0);
        }

        /// <summary>
        /// Gets the parameter value converted to a Decimal
        /// </summary>
        /// <param name="name">Parameter name to get value for</param>
        /// <returns>Value of parameter converted to a Decimal</returns>
        public Decimal GetValueDecimal(string name)
        {
            return (!IsFieldNull(name) ? Decimal.Parse(__dataReader[name].ToString()) : 0);
        }

        /// <summary>
        /// Returns the field value converted to a boolean
        /// </summary>
        /// <param name="name">Field Name</param>
        /// <returns></returns>
        public bool GetValueBool(string name)
        {
            return (!IsFieldNull(name) ? bool.Parse(__dataReader[name].ToString()) : false);
        }

        /// <summary>
        /// Returns the field value converted to a Date
        /// </summary>
        /// <param name="name">Field Name</param>
        /// <returns></returns>
        public DateTime GetValueDateTime(string name)
        {
            return (!IsFieldNull(name) ? DateTime.Parse(__dataReader[name].ToString()) : System.DateTime.MinValue);
        }

        /// <summary>
        /// Returns the field value converted to a Guid
        /// </summary>
        /// <param name="name">Field Name</param>
        /// <returns></returns>
        public Guid GetValueGUID(string name)
        {
            return (!IsFieldNull(name) ? new Guid(__dataReader[name].ToString()) : Guid.Empty);
        }

        #endregion

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // dispose managed resources
                if (__dataReader != null)
                {
                    __dataReader.Dispose();
                }
            }
            // free native resources
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
