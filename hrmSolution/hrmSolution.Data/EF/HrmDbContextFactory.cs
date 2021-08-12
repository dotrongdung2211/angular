using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace hrmSolution.Data.EF
{
    public class HrmDbContextFactory : IDesignTimeDbContextFactory<HrmDbContext>
    {
        public HrmDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("HrmDatabase");

            var optionsBuilder = new DbContextOptionsBuilder<HrmDbContext>();
            optionsBuilder.UseSqlServer(connectionString);
            return new HrmDbContext(optionsBuilder.Options);
        }
    }
}
