/*********************************************************************
 * Copyright © 2016 Fine.Framework 版权所有
 * Author: andyWqh
 * Description: Fine快速开发平台
 * Email:andyWqh@163.com
 * weixin:andysun199054
 * QQ:240463491
**********************************************************************/
using andyWqhCommon.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace andyWqhCommon.Web.Tree
{
    public static class TreeSelect
    {
        public static string TreeSelectJson(this List<TreeSelectModel> dataList)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            sb.Append(TreeSelectJson(dataList,"0",""));
            sb.Append("]");
            return sb.ToString();
        }
        public static string TreeSelectJson(List<TreeSelectModel> dataList,string parentId,string blank)
        {
            StringBuilder sb = new StringBuilder();
            var childNodeList = dataList.FindAll(m => m.ParentId == parentId);
            var tabLine = "";
            if(parentId !="0")
            {
                tabLine = " ";
            }
            if(childNodeList.Count > 0)
            {
                tabLine = tabLine + blank;
            }
            foreach (TreeSelectModel  entity in childNodeList)
            {
                entity.Text = tabLine + entity.Text;
                string strJson = entity.ToJson();
                sb.Append(strJson);
                sb.Append(TreeSelectJson(dataList, entity.Id, tabLine));
            }
            return sb.ToString().Replace("}{","},{");
        }
    }
}
