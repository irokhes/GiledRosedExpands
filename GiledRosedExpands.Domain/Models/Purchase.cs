using System;
using System.ComponentModel.DataAnnotations;

namespace GiledRosedExpands.Domain.Models
{
    public class Purchase
    {
        public int Id { get; set; }
        
        public Item Item { get; set; }
        
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

    }
}