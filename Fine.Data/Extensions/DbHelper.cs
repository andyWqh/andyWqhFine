/*********************************************************************
 * Copyright © 2017 Fine.Framework 版权所有
 * Author: andyWqh
 * Description: Fine快速开发平台
 * Email:andyWqh@163.com
 * weixin:andysun199054
 * QQ:240463491
**********************************************************************/
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fine.Data.Extensions
{
    public class DbHelper
    {
        private static string connStr = ConfigurationManager.ConnectionStrings["andyWqhConn"].ConnectionString;
        public static int ExecuteSqlCommand(string cmdText)
        {
            using (DbConnection conn = new SqlConnection(connStr))
            {
                DbCommand cmd = new SqlCommand();
                PrepareCommand(cmd, conn, null, CommandType.Text, cmdText, null);
                return cmd.ExecuteNonQuery();
            }
        }

        private static void PrepareCommand(DbCommand cmd,DbConnection conn,DbTransaction isOpenTrans,CommandType cmdType,string cmdText,DbParameter[] cmdParms)
        {
            if(conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            cmd.CommandText = cmdText;
            cmd.Connection = conn;
            if(isOpenTrans != null)
            {
                cmd.Transaction = isOpenTrans;
            }
            cmd.CommandType = cmdType;
            if(cmdParms != null)
            {
                cmd.Parameters.AddRange(cmdParms);
            }
        }
    }
}
