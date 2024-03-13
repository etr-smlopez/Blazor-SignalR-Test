using BlazorServerApp.Service;

namespace SampleForm
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
        
            var employeeService = new EmployeeService();
            var costunitsService = new CostunitsService();
            Application.Run(new Form1(employeeService, costunitsService));
           // Application.Run(new Form1());
        }
    }
}