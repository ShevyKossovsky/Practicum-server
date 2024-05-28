using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Server.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Server.Data
{
    public class DataContext: DbContext
    {
        private readonly IConfiguration _configuration;

        public DataContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<Employee> EmployeesList { get; set; }
        public DbSet <Position> PositionsList { get; set; }
        public DbSet<EmployeePosition> EmployeePositionsList { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=EmployeesDB");
            var connectionString = _configuration.GetConnectionString("cloud_DB");
            optionsBuilder.UseSqlServer(connectionString);

        }


    }
}
