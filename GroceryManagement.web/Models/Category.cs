
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
        [Display(Name = "Item Categories")] //category name
        public string Categories { get; set; }


        #region Navigate Collection to Item
        [JsonIgnore]
        public ICollection<Item> Items { get; set; }
        #endregion

    }
}