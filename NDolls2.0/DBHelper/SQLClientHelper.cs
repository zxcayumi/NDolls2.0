using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace NDolls
{
    public class SQLClientHelper : IDBHelper
    {
        private static Dictionary<String, IDBHelper> dbCache = new Dictionary<String, IDBHelper>();
        private String connString;

        private SQLClientHelper(String connString)
        {
            this.connString = connString;
        }

        public static IDBHelper Instance(String connString)
        {
            if (!dbCache.ContainsKey(connString))
            {
                dbCache.Add(connString, new SQLClientHelper(connString));
            }

            return dbCache[connString];
        }

        public int ExecuteNonQuery(string cmdText, List<DbParameter> commandParameters,CommandType cmdType = CommandType.Text)
        {
            return -1;
        }

        public int ExecuteNonQuery(DbTransaction tran, string cmdText, List<DbParameter> commandParameters,CommandType cmdType = CommandType.Text)
        {
            return -1;
        }

        public DbDataReader ExecuteReader(string cmdText, List<DbParameter> commandParameters,CommandType cmdType = CommandType.Text)
        {
            return null;
        }

        public object ExecuteScalar(string cmdText, List<DbParameter> commandParameters,CommandType cmdType = CommandType.Text)
        {
            return null;
        }

        public DataTable Query(string sql, List<DbParameter> cmdParms)
        {
            DataTable dt = new DataTable();
            using (var conn = Connection)
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                prepareCommand(cmd, cmdParms);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }

            return dt;
        }

        #region  properties

        private SqlConnection Connection
        {
            get
            {
                return new SqlConnection(connString);
            }
        }

        #endregion

        #region utility methods
        private static void prepareCommand(DbCommand cmd, List<DbParameter> cmdParms)
        {
            if (cmdParms != null)
            {
                foreach (SqlParameter parm in cmdParms)
                {
                    if (parm.Value == null ||
                        ((parm.DbType == DbType.DateTime || parm.DbType == DbType.Date) && (parm.Value.ToString().Contains("0001"))))
                    {
                        parm.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(parm);
                }
            }
        }
        #endregion
    }
}