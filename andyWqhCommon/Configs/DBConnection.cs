/*********************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: andyWqh
 * Description: Fine快速开发平台
 * Email:andyWqh@163.com
 * weixin:andysun199054
 * QQ:240463491
**********************************************************************/
using andyWqhCommon.Security;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace andyWqhCommon.Configs
{
    /// <summary>
    /// 数据库连接字符串
    /// </summary>
   public class DBConnection
    {
        public  static  bool Encrypt { get; set; }

        public DBConnection(bool encrypt)
        {
            Encrypt = encrypt;
        }

        /// <summary>
        /// 根据参数获取数据库连接字符串
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                string connStr = ConfigurationManager.ConnectionStrings["andyWqhConn"].ConnectionString;
                if (Encrypt)
                {
                    return DESEncrypt.Decrypt(connStr);
                }
                else
                {
                    return connStr;
                }
            }
        }
    }
}
