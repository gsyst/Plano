using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Plano.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plano
{
    public class DataContext: DbContext
    {
        public DataContext() : base()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                            .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json")
                            .Build();
            string conn = config.GetConnectionString("SqlConn");
            optionsBuilder.UseSqlServer(conn);

        }

        public DbSet<CurrencyProps> CurrencyProps { get; set; }

        public DbSet<ExchangeRates> ExchangeRates { get; set; }
    }
}
