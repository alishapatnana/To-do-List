using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CreateList.Models
{
    public class ItemListModel
    {
        [Key]
        // [Display(Name="Item ID")]
        public int Id { get; set; }
        //[Display(Name ="Item Name")]
        public string Name { get; set; }
        // [Display(Name ="Category")]
        public string Category { get; set; }
    }
}
