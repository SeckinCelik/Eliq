using Eliq.BL;
using Eliq.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Eliq.API.Controllers
{
    public class GameController : ApiController
    {
        FileManager fileManager;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public GameController()
        {
            fileManager = new FileManager();
        }

        [HttpGet]
        public HttpResponseMessage GetAllGames(bool cominggames = false)
        {
            try
            {
                var gameList = fileManager.ReadFile<Game>();
                var teamList = fileManager.ReadFile<Team>();

                var list = from x in gameList
                           from t in teamList
                           where (x.home_team_id == t.id || x.away_team_id == t.id)
                           && (cominggames ? x.game_date > DateTime.Today.Date : x.game_date <= DateTime.Today.Date)
                           && t.own_team == true
                           select new
                           {
                               HomeTeam = teamList.Where(a => a.id == x.home_team_id).Select(a => a.team_name).FirstOrDefault(),
                               AwayTeam = teamList.Where(a => a.id == x.away_team_id).Select(a => a.team_name).FirstOrDefault(),
                               GameDate = x.game_date,
                               HomeScore = x.home_team_score,
                               AwayScore = x.away_team_score
                           };

                return Request.CreateResponse(HttpStatusCode.OK, list);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}
