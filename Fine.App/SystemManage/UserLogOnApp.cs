﻿using andyWqhCommon;
using andyWqhCommon.Security;
using Fine.Domain.Entity.SystemManage;
using Fine.Domain.IRepository.SystemManage;
using Fine.Repository.SystemManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fine.App.SystemManage
{
    public class UserLogOnApp
    {
        private IUserLogOnRepository service = new UserLogOnRepository();

        public UserLogOnEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void UpdateForm(UserLogOnEntity userLogOnEntity)
        {
            service.Update(userLogOnEntity);
        }
        public void RevisePassword(string userPassword, string keyValue)
        {
            UserLogOnEntity userLogOnEntity = new UserLogOnEntity();
            userLogOnEntity.F_Id = keyValue;
            userLogOnEntity.F_UserSecretkey = Md5.md5(CommonHelper.CreateNo(), 16).ToLower();
            userLogOnEntity.F_UserPassword = Md5.md5(DESEncrypt.Encrypt(Md5.md5(userPassword, 32).ToLower(), userLogOnEntity.F_UserSecretkey).ToLower(), 32).ToLower();
            service.Update(userLogOnEntity);
        }
    }
}
