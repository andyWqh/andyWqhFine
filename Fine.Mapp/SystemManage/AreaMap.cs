using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Fine.Domain.Entity.SystemManage;
using System.Data.Entity.ModelConfiguration;

namespace Fine.Mapp.SystemManage
{
    public class AreaMap: EntityTypeConfiguration<AreaEntity>
    {
        public AreaMap()
        {
            this.ToTable("Sys_Area");
            this.HasKey(t => t.F_Id);
        }
    }
}
