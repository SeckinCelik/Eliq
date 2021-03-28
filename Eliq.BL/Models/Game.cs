using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eliq.BL.Models
{
    public class Game : BaseModel
    {
        public Guid away_team_id { get; set; }
        public Guid home_team_id { get; set; }
        public int? away_team_score { get; set; }
        public int? home_team_score { get; set; }
        public DateTime game_date { get; set; }
    }
}
