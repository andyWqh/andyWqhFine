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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace andyWqhCommon.Web.TreeGrid
{
    public static class TreeGrid
    {
        public static string TreeGridJson(this List<TreeGridModel> dataList)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{ \"rows\": [");
            sb.Append(TreeGridJson(dataList, -1, "0"));
            sb.Append("]}");
            return sb.ToString();
        }

        public static string TreeGridJson(List<TreeGridModel> dataList,int index,string parentId)
        {
            StringBuilder sb = new StringBuilder();
            var chaildNodeList = dataList.FindAll(t => t.ParentId== parentId);
            if(chaildNodeList.Count > 0)
            {
                index++;
            }
            foreach (TreeGridModel entity in dataList)
            {
                string strJson = entity.EntityJson;
                strJson = strJson.Insert(1, "\"loaded\":" + (entity.Loaded == true ? false : true).ToString().ToLower() + ",");
                strJson = strJson.Insert(1, "\"expanded\":" + (entity.Expanded).ToString().ToLower() + ",");
                strJson = strJson.Insert(1, "\"isLeaf\":" + (entity.IsLeaf == true ? false : true).ToString().ToLower() + ",");
                strJson = strJson.Insert(1, "\"parent\":\"" + parentId + "\",");
                strJson = strJson.Insert(1, "\"level\":" + index + ",");
                sb.Append(strJson);
                sb.Append(TreeGridJson(dataList, index, entity.Id));
            }
            return sb.ToString().Replace("}{", "},{");
        }
    }
}
