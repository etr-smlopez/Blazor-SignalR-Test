
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
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.SignalR.Client;
using BlazorServerApp.Service;
using DataAccess.Model;
using System.Data.Common;

namespace SampleForm
{
    public partial class Form1 : Form
    {
        private readonly SampleDataAccess data;
        private List<EmployeeModel> employees;
        private List<Employees> EmployeeList = new List<Employees>();
        private List<vwCostUnits> costunitsList = new List<vwCostUnits>();
        private HubConnection HubConnection;
        private EmployeeService employeeService;
        private CostunitsService costunitsService;

        public Form1(EmployeeService employeeService, CostunitsService costunitsService)
        {
            InitializeComponent();
            InitializeSignalR();
            var memoryCache = new MemoryCache(new MemoryCacheOptions());

            data = new SampleDataAccess(memoryCache);
            this.employeeService = employeeService;
            this.costunitsService = costunitsService;

            Load += Form1_Load;
           // LoadEmployees();
        }
        private async void InitializeSignalR()
        {
            HubConnection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7153/employeeshub") // Replace with your SignalR Hub URL
                .Build();

            try
            {
                await HubConnection.StartAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error connecting to SignalR Hub: {ex.Message}");
            }
        }
        private async void Form1_Load(object sender, EventArgs e)
        {
            employees = await data.GetEmployeesCache();

            UpdateUI();
        LoadEmployees();
        }
        private async void LoadEmployees()
        {
            EmployeeList = await employeeService.GetAllEmployees();
            costunitsList = await costunitsService.GetCostUnits();

            // Add the list of employees to the cache
            data.AddEmployeesToCache(EmployeeList.Select(e => new EmployeeModel
            {
                FirstName = e.FirstName,
                LastName = e.LastName
                // Add other properties as needed
            }).ToList());

            data.AddCostUnitsToCache(costunitsList.Select(e => new CostUnitsModel
            {

                Type = e.Type
                // Add other properties as needed
            }).ToList());

            //if (HubConnection == null)
            //{
            //    HubConnection = new HubConnectionBuilder()
            //        .WithUrl("https://localhost:7153/employeeshub") // Replace with your actual app URL
            //        .Build();

            //    HubConnection.On<List<Employees>>("RefreshEmployees", employees =>
            //    {
            //        UpdateUI();
            //    });

            //    await HubConnection.StartAsync();
            //}
        }
      

      
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (HubConnection != null)
            {
                HubConnection.DisposeAsync();
            }

            base.OnFormClosing(e);
        }

        private void UpdateUI()
        {
            if (employees is not null)
            {
                label1.Text = string.Empty;
                foreach (var e in employees)
                {
                    label1.Text += $"{e.FirstName} {e.LastName}\n";
                }
            }
            else
            {
                label1.Text = "Loading...";
            }
            //dataGridView1.Rows.Clear();

            //foreach (var employee in EmployeeList)
            //{
            //    dataGridView1.Rows.Add(employee.FirstName, employee.LastName /* other properties */);
            //}
            //dataGridView1.Refresh();
            if (EmployeeList is not null)
            {
                dataGridView1.DataSource = EmployeeList;
                dataGridView1.Refresh();
            }
            else
            {
                label1.Text = "Loading...";
            }

            if (costunitsList is not null)
            {
                dataGridView2.DataSource = costunitsList;
                dataGridView2.Refresh();
            }
            else
            {
                label1.Text = "Loading...";
            }


            LoadEmployees();
        }

        private async void button1_Click(object sender, EventArgs e)
        {

            UpdateUI();
            EmployeeList = await employeeService.GetAllEmployees();

            // Call the UpdateEmployees method on the SignalR Hub
            try
            {
                await HubConnection.InvokeAsync("RefreshEmployees", EmployeeList);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating employees: {ex.Message}");
            }
        }
    }

}


