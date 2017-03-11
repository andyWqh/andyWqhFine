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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace andyWqhCommon.Extend
{
    public class ExtList<T>:IEqualityComparer<T> where T: class ,new ()
    {
        private string[] comparintFiledName = {};
        public  ExtList()
        { }

        public ExtList(params string[] comparintFiledName)
        {
            this.comparintFiledName = comparintFiledName;
        }

        bool IEqualityComparer<T>.Equals(T x, T y)
        {
            if (x == null && y == null)
            {
                return false;
            }
            if (comparintFiledName.Length <= 0)
            {
                return x.Equals(y);
            }
            bool result = true;
            var typeX = x.GetType();
            var typeY = y.GetType();
            foreach (var fieldName in comparintFiledName)
            {
                var xProperyInfo = (from p in typeX.GetProperties()
                    where p.Name.Equals(fieldName)
                    select p).FirstOrDefault();
                var yProperInfo = (from p in typeY.GetProperties()
                    where p.Name.Equals(fieldName)
                    select p).FirstOrDefault();
                result = result && xProperyInfo != null && yProperInfo != null &&
                         xProperyInfo.GetValue(x, null).ToString().Equals(yProperInfo.GetValue(y, null));
            }
            return result;
        }
        int IEqualityComparer<T>.GetHashCode(T obj)
        {
            return obj.ToString().GetHashCode();
        }
    }
}
