using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GroceryManagement.web.Models
{
    [Table(name: "Order Details ")]

    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Order ID")]
        public int Id { get; set; }

        #region
        [Display(Name = "Customer Id")]
        public int CustomerId { get; set; }
        [ForeignKey(nameof(Order.CustomerId))]
        public Customer Customers { get; set; }
        #endregion


        [Display(Name = "Date")]
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime Date { get; set; } = DateTime.Now;


        #region Item Link 

        [Display(Name = "Item")]
        public int ItemId { get; set; }
        [ForeignKey(nameof(Order.ItemId))]
        public Item Item { get; set; }

        #endregion


        [Required(ErrorMessage = "Don't leave {0} Empty!")]
        [Display(Name = "Quantity(Number of Items)")]
        [DefaultValue(1)]
        public short Quantity { get; set; }



        #region Payment Link

        [Display(Name = "Payment Method")]
        public int PaymentId { get; set; }
        [ForeignKey(nameof(Order.PaymentId))]
        public PaymentMethod PaymentMethods { get; set; }

        #endregion

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Price")]
        public float Price { get; set; }


        [Required]
        [DefaultValue(true)]
        [Display(Name = "Order Placed")]
        public bool OrderPlaced { get; set; }


    }
}
