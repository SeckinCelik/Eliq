using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eliq.BL.Models
{
    public class Player : BaseModel
    {
        [Required]
        public string first_name { get; set; }

        [Required]
        public string last_name { get; set; }

        [Required]
        public DateTime birth_date { get; set; }

        [Required]
        public DateTime contract_start { get; set; }
        public DateTime? contract_end { get; set; }
        
        [Required]
        public int jersey_number { get; set; }
    }
}
