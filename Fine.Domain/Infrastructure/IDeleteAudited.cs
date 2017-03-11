using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fine.Domain.Infrastructure
{
    public interface IDeleteAudited
    {
        /// <summary>
        /// 逻辑删除标识
        /// </summary>
        bool? F_DeleteMark { get; set; }

        /// <summary>
        /// 删除实体的操作用户
        /// </summary>
        string F_DeleteUserId { get; set; }

        /// <summary>
        /// 删除实体的时间
        /// </summary>
       DateTime? F_DeleteTime { get; set; }
    }
}
