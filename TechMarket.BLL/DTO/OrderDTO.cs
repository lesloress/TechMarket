using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TechMarket.BLL.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter your name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter your address")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Please enter your city name")]
        public string City { get; set; }
        [Required(ErrorMessage = "Please enter your country name")]
        public string Country { get; set; }
        public bool Shipped { get; set; }
    }
}
