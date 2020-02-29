using EFPlusIssue581.Data.Mapping.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFPlusIssue581.Data.Mapping
{
    public class DeviceMapping : MappingBase<Device>
    {
        public override void Map(EntityTypeBuilder<Device> entity, DatabaseContext databaseContext)
        {
            // Properties
            entity
                .Property(x => x.Name)
                .HasMaxLength(255)
                .IsRequired();
        }
    }
}
