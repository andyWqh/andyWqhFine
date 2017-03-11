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
using System.Xml;
using NPOI.OpenXml4Net.OPC;

namespace andyWqhCommon.Configs
{
    public class Configs
    {
        /// <summary>
        /// 根据key获取配置Value信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetValue(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return default(string);
            }
            return ConfigurationManager.AppSettings[key].ToString().Trim();
        }

        public static void SetValue(string key, string value)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(HttpContext.Current.Server.MapPath("~/Configs/system.config"));
            XmlNode xNode = xDoc.SelectSingleNode("//appSettings");
            if (xNode != null)
            {
                XmlElement xElem1 =(XmlElement) xNode.SelectSingleNode("//add['@key='" + key + "']");
                if (xElem1 != null)
                {
                    xElem1.SetAttribute("value", value);
                }
                else
                {
                    XmlElement xElem2 = xDoc.CreateElement("add");
                    xElem2.SetAttribute("key", key);
                    xElem2.SetAttribute("value", value);
                    xNode.AppendChild(xElem2);
                }
            }
            xDoc.Save(HttpContext.Current.Server.MapPath("~/Configs/system.config"));
        }
    }
}
