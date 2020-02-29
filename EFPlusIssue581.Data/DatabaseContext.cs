namespace EFPlusIssue581.Data
{
    using EFPlusIssue581.Data.Mapping.Base;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public static class DatabaseContextExtenions
    {
        public static void AddEntityConfigurationsFromAssembly(this ModelBuilder modelBuilder, DatabaseContext databaseContext)
        {
            IEnumerable<Type> mappingTypes = databaseContext.GetType().Assembly.GetMappingTypes(typeof(IEntityMappingConfiguration<>));

            foreach (IEntityMappingConfiguration config in mappingTypes.Select(Activator.CreateInstance).Cast<IEntityMappingConfiguration>())
            {
                config.Map(modelBuilder, databaseContext);
            }
        }

        private static IEnumerable<Type> GetMappingTypes(this Assembly assembly, Type mappingInterface)
        {
            return assembly
                .GetTypes()
                .Where(x => !x.GetTypeInfo().IsAbstract && x.GetInterfaces().Any(y => y.GetTypeInfo().IsGenericType && y.GetGenericTypeDefinition() == mappingInterface));
        }
    }

    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddEntityConfigurationsFromAssembly(this);
        }
    }
}
