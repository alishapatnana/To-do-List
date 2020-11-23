using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCClient.Models
{
    public class ItemList
    {
        // [Display(Name="Item ID")]
        public int Id { get; set; }
        //[Display(Name ="Item Name")]
        public string Name { get; set; }
        // [Display(Name ="Category")]
        public string Category { get; set; }
    }
}
