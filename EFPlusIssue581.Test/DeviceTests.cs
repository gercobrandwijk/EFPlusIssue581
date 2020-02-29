using EFPlusIssue581.Data;
using EFPlusIssue581.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using NUnit.Framework;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace EFPlusIssue581.Test
{
    public class Tests
    {
        private DatabaseContext DatabaseContext;

        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder<DatabaseContext> databaseContextBuilder;

            databaseContextBuilder = new DbContextOptionsBuilder<DatabaseContext>()
                .EnableSensitiveDataLogging()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            databaseContextBuilder = databaseContextBuilder
                    .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                    .EnableSensitiveDataLogging();

            this.DatabaseContext = new DatabaseContext(databaseContextBuilder.Options);

            BatchUpdateManager.InMemoryDbContextFactory = () =>
            {
                return this.DatabaseContext;
            };
        }

        [Test]
        public async Task Test1()
        {
            DeviceRepository deviceRepository;
            List<Device> devices;

            deviceRepository = new DeviceRepository(this.DatabaseContext);

            devices = deviceRepository.GetAll(true);

            Assert.IsTrue(devices.Count == 0);

            devices = new List<Device>()
            {
                new Device() {
                    Name = "Active device",
                    Active = true
                },
                new Device() {
                    Name = "Inactive device",
                    Active = false
                }
            };

            deviceRepository.AddRange(devices);

            this.DatabaseContext.SaveChanges();

            devices = deviceRepository.GetAll(true);

            Assert.IsTrue(devices.Count == 2);

            devices = deviceRepository.GetAll(false);

            Assert.IsTrue(devices.Count == 1);

            await deviceRepository.GetQueryable()
                            .Where(x => !x.Active)
                            .UpdateAsync(x => new Device() { Name = x.Name + " - INACTIVE" });

            devices = deviceRepository.GetAll(true);

            Assert.IsTrue(devices.SingleOrDefault(x => x.Name == "Active device") != null);
            Assert.IsTrue(devices.SingleOrDefault(x => x.Name == "Inactive device - INACTIVE") != null);
        }
    }
}