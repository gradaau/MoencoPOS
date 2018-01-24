using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoencoPOS.Models
{
    public class Address
    {
        [DisplayName("Address ID")]
        public int AddressId { get; set; }
        [DisplayName("Land Line Number")]
        [StringLength(13)]
        public string LandLineNo { get; set; }
        [DisplayName("Cellphone Number")]
        [StringLength(13)]
        public string CellPhoneNo { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        [DisplayName("Street Number")]
        public string StreetNo { get; set; }
        [DisplayName("House Number")]
        public string HouseNo { get; set; }
    }
}
