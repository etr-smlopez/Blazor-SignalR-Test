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
using DataAccess.Model;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Caching.Memory;

namespace BlazorServerApp.Service
{
    public class EmployeeService
    {
        private readonly IHubContext<EmployeesHub> _context;
        AppDbContext dbContext = new AppDbContext();
        private readonly SqlTableDependency<Employees> _dependency;
        private readonly string _connectionString;

        private List<EmployeeModel> employees;
        private List<Employees> EmployeeList = new List<Employees>();
        private HubConnection HubConnection;
        private EmployeeService employeeService;
        private readonly SampleDataAccess data;
        public EmployeeService(IHubContext<EmployeesHub> context)
        {
            _context = context;
            _connectionString = "Server=DESKTOP-PK86BAT;Database=ETR-IS-PGA-TEST;User Id=awit;Password=awit;Trusted_Connection=True;MultipleActiveResultSets=true";
            _dependency = new SqlTableDependency<Employees>(_connectionString, "Employees");
            _dependency.OnChanged += Changed;
            _dependency.Start();

            var memoryCache = new MemoryCache(new MemoryCacheOptions());

            data = new SampleDataAccess(memoryCache);

        }

        public EmployeeService()
        {
        }
        private async void Changed(object sender, RecordChangedEventArgs<Employees> e)
        {
            var employees = await GetAllEmployees();
            await _context.Clients.All.SendAsync("RefreshEmployees", employees);
    //      EmployeeList = await employeeService.GetAllEmployees();
    //        // Add the list of employees to the cache
    //        data.AddEmployeesToCache(EmployeeList.Select(e => new EmployeeModel
    //        {
    //            FirstName = e.FirstName,
    //            LastName = e.LastName
    //    // Add other properties as needed
    //}).ToList());
        }
        //public async Task<List<Employees>> GetAllEmployees()
        //{
        //    return await dbContext.Employees.AsNoTracking().ToListAsync();
        //}
        public async Task<List<Employees>> GetAllEmployees()
        {
            using ( dbContext = new AppDbContext())
            {
                var employees = await dbContext.Employees.AsNoTracking().ToListAsync();
               // return await dbContext.Employees.AsNoTracking().ToListAsync();
                // Add logic to detect changes here, e.g., compare with previous state

                return employees;
            }
        }

    }
}
