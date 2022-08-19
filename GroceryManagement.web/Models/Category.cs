
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GroceryManagement.web.Models
{
    [Table(name: "Categories")]

    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ItemCategoriesID")]  // Category ID
        public int IcId { get; set; }


        [Required(ErrorMessage = "Don't leave {0} Empty!")]
        [Column(TypeName = "varchar(100)")]
        [Display(Name = "Item Categories")]
        public string Categories { get; set; }


        #region Navigate Collection to Item
        public ICollection<Item> Items { get; set; }
        #endregion

    }
}