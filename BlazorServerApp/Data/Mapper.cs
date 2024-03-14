using System.Collections.Generic;
using System.Linq;
namespace BlazorServerApp.Data
{
 

public class Mapper
{
    public static List<Employees> MapToBlazorEmployees(List<DataAccess.Model.EmployeeModel> employeeModels)
    {
        return employeeModels.Select(emp => new  Employees
        {
            LastName = emp.LastName,
            FirstName = emp.FirstName
            // Map other properties as needed
        }).ToList();
    }
}
}