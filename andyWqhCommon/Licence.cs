/*********************************************************************
 * Copyright © 2016 Fine.Framework 版权所有
 * Author: andyWqh
 * Description: Fine快速开发平台
 * Email:andyWqh@163.com
 * weixin:andysun199054
 * QQ:240463491
**********************************************************************/
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using andyWqhCommon.Security;
namespace andyWqhCommon
{
   public sealed class Licence
    {
       public static bool IsLicence(string key)
        {
            string host = HttpContext.Current.Request.Url.Host.ToLower();
            if(host.Equals("localhost"))
            {
                return true;
            }
            string licence = ConfigurationManager.AppSettings["LicenceKey"];
            if (licence != null && licence == Md5.md5(key, 32))
            {
                return true;
            }

            return false;
        }

        public static string GetLicence()
        {
            var licence =andyWqhCommon.Configs.Configs.GetValue("LicenceKey");
            if (string.IsNullOrEmpty(licence))
            {
                licence = CommonHelper.GuId();
                andyWqhCommon.Configs.Configs.SetValue("LicenceKey", licence);
            }
            return Md5.md5(licence, 32);
        }
    }
}
