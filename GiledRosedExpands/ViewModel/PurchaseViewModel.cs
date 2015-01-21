using System.ComponentModel.DataAnnotations;

namespace GiledRosedExpands.ViewModel
{
    public class PurchaseViewModel
    {
        public int PurchaseId{ get; set; }
        [Required(ErrorMessage = "Item id is required")]
        public int ItemId { get; set; }
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

    }
}