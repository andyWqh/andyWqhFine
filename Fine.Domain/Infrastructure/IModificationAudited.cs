using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fine.Domain.Infrastructure
{
    public interface IModificationAudited
    {
        string F_Id { get; set; }

        string F_LastModifyUserId { get; set; }

        DateTime? F_LastModifyTime { get; set; }
    }
}
