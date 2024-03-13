using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.SignalR;
using BlazorServerApp.Data;
using BlazorServerApp.Hubs;
using System.Runtime.CompilerServices;
using System.Threading.Channels;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.EventArgs;
using DataAccess;
using Microsoft.Data.SqlClient;

namespace BlazorServerApp.Service
{
    public class EmployeeService
    {
        private readonly IHubContext<EmployeesHub> _context;
        AppDbContext dbContext = new AppDbContext();
        private readonly SqlTableDependency<Employees> _dependency;
        private readonly string _connectionString;

        public EmployeeService(IHubContext<EmployeesHub> context)
        {
            _context = context;
            _connectionString = "Server=DESKTOP-PK86BAT;Database=ETR-IS-PGA-TEST;User Id=awit;Password=awit;Trusted_Connection=True;MultipleActiveResultSets=true";
            _dependency = new SqlTableDependency<Employees>(_connectionString, "Employees");
            _dependency.Start();
        }

        public EmployeeService()
        {
        }

        //public async Task<List<Employees>> GetAllEmployees()
        //{
        //    return await dbContext.Employees.AsNoTracking().ToListAsync();
        //}
        public async Task<List<Employees>> GetAllEmployees()
        {
            using (var dbContext = new AppDbContext())
            {
                return await dbContext.Employees.AsNoTracking().ToListAsync();
            }
        }

    }
}
