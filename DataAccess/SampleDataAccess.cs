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

                await Task.Delay(3000);

                _memoryCache.Set("employees", output, TimeSpan.FromMinutes(1));
            }

            return output;
        }

    }
}
