using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CreateList.Models.ViewModel
{
    public class ItemListViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Item Name")]
        public string Name { get; set; }
        [Display(Name = "Category is required")]
        public string Category { get; set; }
    }
}
