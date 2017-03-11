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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace andyWqhCommon.Web
{
    /// <summary>
    /// 分页信息类
    /// </summary>
   public class Pagination
    {
        /// <summary>
        /// 每页记录条数
        /// </summary>
        public int Rows { get; set; }

        /// <summary>
        /// 当前页数
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// 排序列
        /// </summary>
        public string Sidx { get; set; }

        /// <summary>
        /// 排序类型
        /// </summary>
        public string Sord { get; set; }

        /// <summary>
        /// 总记录条数
        /// </summary>
        public int Records { get; set; }

        /// <summary>
        /// 总页码数
        /// </summary>
        public int Total
        {
            get
            {
                if(Records > 0)
                {
                    return Records % this.Rows == 0 ? Records / this.Rows : Records / this.Rows + 1;
                }
                else
                {
                    return default(int);
                }
            }
        }
    }
}
