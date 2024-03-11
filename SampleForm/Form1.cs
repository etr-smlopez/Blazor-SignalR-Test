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
using DataAccess;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;

namespace SampleForm
{
    public partial class Form1 : Form
    {
        private readonly SampleDataAccess data;
        private List<EmployeeModel> employees;
        public Form1()
        {
            InitializeComponent();
              var memoryCache = new MemoryCache(new MemoryCacheOptions());

            // Pass IMemoryCache to SampleDataAccess constructor
            data = new SampleDataAccess(memoryCache);

            // Load employees asynchronously
            LoadEmployees();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
        private async void LoadEmployees()
        {
            // Use async/await to load employees asynchronously
            employees = await data.GetEmployeesCache();

            // Update UI
            UpdateUI();
        }

        private void UpdateUI()
        {
            if (employees != null)
            {
                foreach (var e in employees)
                {
                    label1.Text += $"{e.FirstName} {e.LastName}\n";
                }
            }
            else
            {
                label1.Text = "Loading...";
            }
        }
    }

}
