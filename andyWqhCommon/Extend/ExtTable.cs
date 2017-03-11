/*********************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: andyWqh
 * Description: Fine快速开发平台
 * Email:andyWqh@163.com
 * weixin:andysun199054
 * QQ:240463491
**********************************************************************/
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace andyWqhCommon.Extend
{
    public  static class ExtTable
    {
        /// <summary>
        /// 获取表里某页的数据
        /// </summary>
        /// <param name="data">表数据</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="totalCount">返回总页数</param>
        /// <returns>返回当页表数据</returns>
        public static DataTable GetPage(this DataTable data, int pageIndex, int pageSize, out int totalCount)
        {
            totalCount = data.Rows.Count/pageSize;
            totalCount += data.Rows.Count % pageSize == 0 ? 0:1;
            DataTable nTable = data.Clone();
            int startIndex = pageIndex*pageSize;
            int endIndex = startIndex + pageSize > data.Rows.Count ? data.Rows.Count : startIndex + pageSize;
            if (startIndex < endIndex)
            {
                for (int i = startIndex; i < endIndex; i++)
                {
                    nTable.ImportRow(data.Rows[i]);
                }
            }
            return nTable;
        }

        /// <summary>
        /// 根据字段过滤表的内容
        /// </summary>
        /// <param name="data">表数据</param>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public static DataTable GetDataFilter(DataTable data, string condition)
        {
            if (data != null && data.Rows.Count > 0)
            {
                if (string.IsNullOrEmpty(condition))
                {
                    return data;
                }
                else
                {
                    DataTable newDt = data.Clone();
                    DataRow[] dr = data.Select(condition);
                    for (int i = 0; i < dr.Length; i++)
                    {
                        newDt.ImportRow((DataRow)dr[i]);
                    }
                    return newDt;
                }
            }
            return default(DataTable);
        }
    }
}
