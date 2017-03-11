﻿using Fine.Data.Repository;
using Fine.Domain.Entity.SystemManage;
using Fine.Domain.IRepository.SystemManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fine.Repository.SystemManage
{
    public class ModuleRepository : RepositoryBase<ModuleEntity>, IModuleRepository
    {
    }
}
