using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Text;
using System.Collections.Generic;

namespace BlazorServerApp.Data
{
     //[Keyless]
    public class Employees
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int EmployeeID { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }
    }
}
