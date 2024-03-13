 
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Text;
using System.Collections.Generic;

namespace BlazorServerApp.Data
{
    //[Keyless]
    public class vwCostUnits
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public string Type { get; set; }
 
    }
}
