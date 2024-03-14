using DataAccess.Model;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class SampleDataAccess
    {
        private readonly IMemoryCache _memoryCache;
     
        public SampleDataAccess(IMemoryCache memoryCache)
        { 
            _memoryCache = memoryCache;
        }

        public List<EmployeeModel> GetEmployees()
        {
            List<EmployeeModel> output = new();

            output.Add(new() { FirstName = "A", LastName = "AA"});
            output.Add(new() { FirstName = "B", LastName = "BB"});
            output.Add(new() { FirstName = "C", LastName = "CC"});

            Thread.Sleep(3000);

            return output;
        }   
        
        public async Task<List<EmployeeModel>> GetEmployeesAsync()
        {
            List<EmployeeModel> output = new();

            output.Add(new() { FirstName = "A", LastName = "AA"});
            output.Add(new() { FirstName = "B", LastName = "BB"});
            output.Add(new() { FirstName = "C", LastName = "CC"});

            await Task.Delay(3000);

            return output;
        }

        public async Task<List<EmployeeModel>> GetEmployeesCache()
        {
            List<EmployeeModel> output;

            output = _memoryCache.Get<List<EmployeeModel>>("employees");


            if (output is null)
            {
                output = new();

                output.Add(new() { FirstName = "A", LastName = "AA" });
                output.Add(new() { FirstName = "B", LastName = "BB" });
                output.Add(new() { FirstName = "C", LastName = "CC" });

                await Task.Delay(1000);

                _memoryCache.Set("employees", output, TimeSpan.FromMinutes(1));
            }

            return output;
        }

        public void AddEmployeesToCache(List<EmployeeModel> employees)
        {
            var cachedEmployees = _memoryCache.Get<List<EmployeeModel>>("Employees");

            if (cachedEmployees == null)
            {
                _memoryCache.Set("Employees", employees, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
                });
            }     //var cachedEmployees = _memoryCache.Get("Employees");

            //if (cachedEmployees != null)
            //{
            //    _memoryCache.GetOrCreate("Employees", employees =>
            //    {
            //        employees.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
            //        return new List<EmployeeModel>();
            //    });
            //}
            //else
            //{
            // //   _memoryCache.Set("employees", output, TimeSpan.FromMinutes(1));
            //    _memoryCache.Set("Employees", employees, new MemoryCacheEntryOptions
            //    {
            //        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
            //    });
            //}

        }

        public List<EmployeeModel> GetEmployeesFromCache()
        {
            return _memoryCache.GetOrCreate("Employees", entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
                return new List<EmployeeModel>();
            });
        }
         
        public void AddCostUnitsToCache(List<CostUnitsModel> employees)
        {

            _memoryCache.Set("CostUnits", employees, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
            });
        }

        public async Task<List<CostUnitsModel>> GetCostUnitsFromCache()
        {
            return _memoryCache.GetOrCreate("CostUnits", entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
                return new List<CostUnitsModel>();
            });
        }
    }
}
