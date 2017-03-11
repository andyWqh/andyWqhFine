using andyWqhCommon;
using andyWqhCommon.Operator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fine.Domain.Infrastructure
{
     public class IEntity<TEntity>
    {
        public void Create()
        {
            var entity = this as ICreationAudited;
            entity.F_Id = CommonHelper.GuId();
            var LoginInfo = OperatorProvider.Provider.GetCurrent();
            if(LoginInfo != null)
            {
                entity.F_CreatorUserId = LoginInfo.UserId;
            }
            entity.F_CreatorTime = DateTime.Now;
        }

        public void Modify(string keyValue)
        {
            var entity = this as IModificationAudited;
            entity.F_Id = keyValue;
            var loginInfo = OperatorProvider.Provider.GetCurrent();
            if(loginInfo != null)
            {
                entity.F_LastModifyUserId = loginInfo.UserId;
            }
            entity.F_LastModifyTime = DateTime.Now;
        }

        public void Remove()
        {
            var entity = this as IDeleteAudited;
            var loginIno = OperatorProvider.Provider.GetCurrent();
            if(loginIno != null)
            {
                entity.F_DeleteUserId = loginIno.UserId;
            }
            entity.F_DeleteTime = DateTime.Now;
            entity.F_DeleteMark = true;
        }
    }
}
