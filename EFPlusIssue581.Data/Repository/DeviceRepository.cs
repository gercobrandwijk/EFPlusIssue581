using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFPlusIssue581.Data.Repository
{
    public class DeviceRepository
    {
        protected DbSet<Device> DatabaseSet;

        public DeviceRepository(DatabaseContext databaseContext)
        {
            this.DatabaseSet = databaseContext.Set<Device>();
        }

        public List<Device> GetAll(bool includeDeleted)
        {
            return this.DatabaseSet
                .Where(x => includeDeleted || x.Active)
                .OrderBy(x => x.Name)
                .ToList();
        }

        public void AddRange(List<Device> devices)
        {
            this.DatabaseSet.AddRange(devices);
        }

        public IQueryable<Device> GetQueryable()
        {
            return this.DatabaseSet.AsQueryable();
        }
    }
}
