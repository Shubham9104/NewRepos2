using GroceryManagement.web.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace GroceryManagement.web.Areas.User2.ViewModels
{
    public class ShowViewModel
    {
        [Required]
        [Display(Name = "Customer")]
        public int CustomerId { get; set; }

        public int CategoryId { get; set; }
        public ICollection<Category> Categories { get; set; }

        public ICollection<Order> Orders { get; set; }

        public ICollection<Customer> Customers { get; set; }

        public ICollection<Item> Items { get; set; }

        public ICollection<PaymentMethod> PaymentMethods { get; set; }

        
    }
}
