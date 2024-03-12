using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BlazorServerApp;
using BlazorServerApp.Data;
using DataAccess;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.SignalR.Client;
using BlazorServerApp.Service; 

namespace SampleForm
{
    public partial class Form1 : Form
    {
        private readonly SampleDataAccess data;
        private List<EmployeeModel> employees;
        private List<Employees> EmployeeList = new List<Employees>();
        private HubConnection? HubConnection;
        public Form1()
        {
            InitializeComponent();
              var memoryCache = new MemoryCache(new MemoryCacheOptions());

            // Pass IMemoryCache to SampleDataAccess constructor
            data = new SampleDataAccess(memoryCache);

            // Load employees asynchronously
            Load += Form1_Load;// LoadEmployees();
            LoadEmployees();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            employees = await data.GetEmployeesCache(); 

            // Update UI
            UpdateUI();
            LoadEmployees();
        }
        private async void LoadEmployees()
        {
            // Create an instance of EmployeeService
            EmployeeService employeeService = new EmployeeService();

            // Call the GetAllEmployees method on the instance
            EmployeeList = await employeeService.GetAllEmployees();
            HubConnection = new HubConnectionBuilder()
               // .WithUrl("https://localhost:5001/employeeshub") // Replace with your actual app URL
                .WithUrl("https://localhost:7153/employeeshub") // Replace with your actual app URL
                .Build();

            HubConnection.On <List<Employees>>("RefreshEmployees", employees =>
            {
                //EmployeeList = employees;
                //InvokeAsync(StateHasChanged);
                UpdateUI();
            });

       await HubConnection.StartAsync();
        } 
        
        //private async void LoadEmployees()
        //{
        //    // Use async/await to load employees asynchronously
        //    employees = await data.GetEmployeesCache();

        //    // Update UI
        //    UpdateUI();
        //}

        private void UpdateUI()
        {
            if (employees is not null)
            {
                foreach (var e in employees)
                {
                    // Display employee names in a Label or any other UI element
                    label1.Text += $"{e.FirstName} {e.LastName}\n";
                }
            }
            else
            {
                label1.Text = "Loading...";
            }
            dataGridView1.Rows.Clear();

            // Update DataGridView with the latest employee data
            foreach (var employee in EmployeeList)
            {
                // Add a new row to the DataGridView
                dataGridView1.Rows.Add(employee.FirstName, employee.LastName  /* other properties */);
            }

            // Optionally, you can customize DataGridView appearance or behavior here

            // Refresh the DataGridView
            dataGridView1.Refresh();
        }
    }

}
