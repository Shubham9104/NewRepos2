using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace GroceryManagement.web.Models
{
    public enum MyIdentityRoleNames
    {

        [Display(Name = "Admin Role")]
        AppAdmin,


        [Display(Name = "User Role")]
        AppUser

    }
}
