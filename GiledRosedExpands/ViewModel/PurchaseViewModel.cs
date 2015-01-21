using System.ComponentModel.DataAnnotations;

namespace GiledRosedExpands.ViewModel
{
    public class PurchaseViewModel
    {
        public int PurchaseId{ get; set; }

        [Required(ErrorMessage = "Item name is required")]
        public string ItemName { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

    }
}