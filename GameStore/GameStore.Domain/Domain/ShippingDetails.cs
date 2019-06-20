using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Domain.Domain
{
    public class ShippingDetails
    {
        [Required(ErrorMessage = "Pleace enter a name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Pleace enter your adress")]
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }
        [Required(ErrorMessage = "Pleace enter a city name")]
        public string City { get; set; }

        [Required (ErrorMessage = "Pleace enter a state name")]
        public string State { get; set; }
        public string Zip { get; set; }
        public bool GiftWrap { get; set; }
    }
}
