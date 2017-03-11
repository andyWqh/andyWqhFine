using Fine.Data.Repository;
using Fine.Domain.Entity.SystemSecurity;
using Fine.Domain.IRepository.SystemSecurity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fine.Repository.SystemSecurity
{
    public class LogRepository : RepositoryBase<LogEntity>, ILogRepository
    {

    }
}
