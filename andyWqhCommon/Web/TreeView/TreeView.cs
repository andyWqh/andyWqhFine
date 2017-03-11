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

namespace andyWqhCommon.Web.TreeView
{
    public static class TreeView
    {
        public static string TreeViewJson(this List<TreeViewModel> data, string parentId = "0")
        {
            StringBuilder strJson = new StringBuilder();
            List<TreeViewModel> item = data.FindAll(t => t.ParentId == parentId);
            strJson.Append("[");
            if (item.Count > 0)
            {
                foreach (TreeViewModel entity in item)
                {
                    strJson.Append("{");
                    strJson.Append("\"id\":\"" + entity.Id + "\",");
                    strJson.Append("\"text\":\"" + entity.Text.Replace("&nbsp;", "") + "\",");
                    strJson.Append("\"value\":\"" + entity.Value + "\",");
                    if (entity.Title != null && !string.IsNullOrEmpty(entity.Title.Replace("&nbsp;", "")))
                    {
                        strJson.Append("\"title\":\"" + entity.Title.Replace("&nbsp;", "") + "\",");
                    }
                    if (entity.Image != null && !string.IsNullOrEmpty(entity.Image.Replace("&nbsp;", "")))
                    {
                        strJson.Append("\"img\":\"" + entity.Image.Replace("&nbsp;", "") + "\",");
                    }
                    if (entity.CheckState != null)
                    {
                        strJson.Append("\"checkstate\":" + entity.CheckState + ",");
                    }
                    if (entity.ParentId != null)
                    {
                        strJson.Append("\"parentnodes\":\"" + entity.ParentId + "\",");
                    }
                    strJson.Append("\"showcheck\":" + entity.ShowCheck.ToString().ToLower() + ",");
                    strJson.Append("\"isexpand\":" + entity.IsExpand.ToString().ToLower() + ",");
                    if (entity.Complete == true)
                    {
                        strJson.Append("\"complete\":" + entity.Complete.ToString().ToLower() + ",");
                    }
                    strJson.Append("\"hasChildren\":" + entity.HasChildren.ToString().ToLower() + ",");
                    strJson.Append("\"ChildNodes\":" + TreeViewJson(data, entity.Id) + "");
                    strJson.Append("},");
                }
                strJson = strJson.Remove(strJson.Length - 1, 1);
            }
            strJson.Append("]");
            return strJson.ToString();
        }
    }
}
