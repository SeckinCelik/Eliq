using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eliq.BL.Models
{
    public class Team : BaseModel
    {
        public string team_name { get; set; }
        public bool own_team { get; set; }
    }
}
