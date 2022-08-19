
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GroceryManagement.web.Models
{
    [Table(name: "Items")]

    public class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ItemID")]
        public int ItemId { get; set; }


        #region Directing to Item Category


        [Display(Name = "Item Category")]
        public int CategoryID { get; set; }
        [ForeignKey(nameof(Item.CategoryID))]
        public Category Category { get; set; }

        #endregion


        [Required(ErrorMessage = "Don't leave {0} Empty!")]
        [Column(TypeName = "varchar(100)")]
        [Display(Name = "Item")]
        public string ItemName { get; set; }


        [Required]
        [DefaultValue(true)]
        [Display(Name = "In Stock")]
        public bool Available { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Price")]
        public float Price { get; set; }


        [Display(Name = "Item Image")]
        public string ImgUrl { get; set; } = null;

        #region Navigate Collection to Order
        public ICollection<Order> Orders { get; set; }
        #endregion

    }
}
