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
    public class TreeViewModel
    {
        public string ParentId { get; set; }

        public string Id { get; set; }

        public string Text { get; set; }

        public string Value { get; set; }

        public int? CheckState { get; set; }

        public bool ShowCheck { get; set; }

        public bool Complete { get; set; }

        public bool IsExpand { get; set; }
           
        public bool HasChildren { get; set; }

        public string Image { get; set; }

        public string Title { get; set; }
    }
}
