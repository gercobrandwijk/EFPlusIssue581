using EFPlusIssue581.Data.Model.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFPlusIssue581.Data.Mapping.Base
{
    public interface IEntityMappingConfiguration
    {
        void Map(ModelBuilder entity, DatabaseContext databaseContext);
    }

    public interface IEntityMappingConfiguration<T> : IEntityMappingConfiguration
        where T : class
    {
        void Map(EntityTypeBuilder<T> entity, DatabaseContext databaseContext);
    }

    public abstract class MappingBase<T> : IEntityMappingConfiguration<T>
        where T : ModelBase
    {
        public abstract void Map(EntityTypeBuilder<T> entity, DatabaseContext databaseContext);

        public void Map(ModelBuilder entity, DatabaseContext databaseContext)
        {
            EntityTypeBuilder<T> entityBuilder = entity.Entity<T>();

            // Table
            entityBuilder.ToTable(typeof(T).Name);

            // Key
            entityBuilder
                .HasKey(x => x.Id);

            this.Map(entityBuilder, databaseContext);
        }
    }
}
