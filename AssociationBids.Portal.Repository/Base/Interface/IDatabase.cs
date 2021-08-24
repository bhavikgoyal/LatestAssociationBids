using System;
using System.Data;

namespace AssociationBids.Portal.Repository.Base
{
    public interface IDatabase : System.IDisposable
    {
        DataSet ExecuteDataSet(DBCommandWrapper commandWrapper);
        DataSet ExecuteDataSet(DBCommandWrapper commandWrapper, string tableName);
        int ExecuteNonQuery(DBCommandWrapper commandWrapper);
        DBDataReader ExecuteReader(DBCommandWrapper commandWrapper);
        DBCommandWrapper GetSqlStringCommandWrapper(string query);
        DBCommandWrapper GetStoredProcCommandWrapper(string storedProcedure);
    }
}
