using Fine.Data.Repository;
using Fine.Domain.Entity.SystemManage;
using Fine.Domain.IRepository.SystemManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fine.Repository.SystemManage
{
    public class ModuleButtonRepository : RepositoryBase<ModuleButtonEntity>, IModuleButtonRepository
    {
        public void SubmitCloneButton(List<ModuleButtonEntity> entityList)
        {
            using (var db = new RepositoryBase().BeginTrans())
            {
                foreach (var item in entityList)
                {
                    db.Insert(item);
                }
                db.Commit();
            }
        }
    }
}
