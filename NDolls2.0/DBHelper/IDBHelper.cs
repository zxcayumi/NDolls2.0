using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;

namespace NDolls
{
    public interface IDBHelper
    {
        int ExecuteNonQuery(string cmdText, List<DbParameter> commandParameters,CommandType cmdType = CommandType.Text);
        int ExecuteNonQuery(DbTransaction tran, string cmdText, List<DbParameter> commandParameters,CommandType cmdType = CommandType.Text);
        DbDataReader ExecuteReader(string cmdText, List<DbParameter> commandParameters,CommandType cmdType = CommandType.Text);
        object ExecuteScalar(string cmdText, List<DbParameter> commandParameters,CommandType cmdType = CommandType.Text);
        DataTable Query(string sql, List<DbParameter> cmdParms);
    }
}
