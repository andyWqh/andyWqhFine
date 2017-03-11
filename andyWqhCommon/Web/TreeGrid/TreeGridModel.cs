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
   public class TreeGridModel
    {
        public string Id { get; set; }

        public string ParentId { get; set; }

        public string Text { get; set; }

        public bool IsLeaf { get; set; }

        public bool Expanded { get; set; }

        public bool Loaded { get; set; }
        
        public string EntityJson { get; set; } 
    }
}
