 
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
    public class CostunitsService
    {
        private readonly IHubContext<CostUnitsHub> _context;
        AppDbContext dbContext = new AppDbContext();
        private readonly SqlTableDependency<vwCostUnits> _dependency;
        private readonly string _connectionString;

        public CostunitsService(IHubContext<CostUnitsHub> context)
        {
            _context = context;
            _connectionString = "Server=DESKTOP-PK86BAT;Database=ETR-IS-PGA-TEST;User Id=awit;Password=awit;Trusted_Connection=True;MultipleActiveResultSets=true";
            _dependency = new SqlTableDependency<vwCostUnits>(_connectionString, "vwCostUnits");
            _dependency.Start();
        }

        public CostunitsService()
        {
        }

        //public async Task<List<Employees>> GetAllEmployees()
        //{
        //    return await dbContext.Employees.AsNoTracking().ToListAsync();
        //}
        public async Task<List<vwCostUnits>> GetCostUnits()
        {
            using (var dbContext = new AppDbContext())
            {
                return await dbContext.vwCostUnits.AsNoTracking().ToListAsync();
            }
        }

    }
}
