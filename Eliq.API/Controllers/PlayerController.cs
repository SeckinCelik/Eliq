using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Eliq.BL;
using Eliq.BL.Models;

namespace Eliq.API.Controllers
{
    public class PlayerController : ApiController
    {
        private FileManager fileManager;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public PlayerController()
        {
            fileManager = new FileManager();
        }

        [HttpGet]
        public HttpResponseMessage GetAllPlayers()
        {
            try
            {
                var listOfPlayers = fileManager.ReadFile<Player>();
                return Request.CreateResponse(HttpStatusCode.OK, listOfPlayers);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public HttpResponseMessage AddPlayer(Player player)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Request.CreateResponse(HttpStatusCode.BadRequest);

                var playerList = fileManager.ReadFile<Player>();
                if (playerList.Any(x => x.jersey_number == player.jersey_number))
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "invalid jersey number");

                var listOfPlayers = fileManager.AppendToFile<Player>(player);

                return Request.CreateResponse(HttpStatusCode.OK, listOfPlayers);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public HttpResponseMessage DeletePlayer([FromUri] Guid playerid)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, fileManager.DeleteFromFile<Player>(playerid));
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}
