using PlayerManagementSystem.BusinessLayer.Abstract;
using PlayerManagementSystem.BusinessLayer.Concrete;
using PlayerManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Web.Helpers;
using PlayerManagementSystem.Filters;

namespace PlayerManagementSystem.Controllers
{
    [CustomAuthFilter]
    public class PlayerManagementController : Controller
    {
        private readonly IPlayerBusiness playerBusiness;
        private readonly ITeamBusiness teamBusiness;

        public PlayerManagementController()
        {
            playerBusiness = new PlayersBusiness();
            teamBusiness = new TeamBusiness();
        }

        // GET: PlayerManagement
        public ActionResult PlayerListView()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetPlayerList()
        {
            try
            {
                // Skip number of Rows count  
                var start = Request.Form["start"];

                // Paging Length 10,20  
                var length = Request.Form["length"];

                // Sort Column Name  
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"];

                // Sort Column Direction (asc, desc)  
                var sortColumnDirection = Request.Form["order[0][dir]"];

                // Search Value from (Search box)  
                var searchValue = Request.Form["search[value]"];

                //Paging Size (10, 20, 50,100)  
                int pageSize = length != null ? Convert.ToInt32(length) : 0;

                int skip = start != null ? Convert.ToInt32(start) : 0;
                var playerList = playerBusiness.GetAllPlayers();
                //Sorting  
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    var pi = typeof(PlayerModel).GetProperty(sortColumn);
                    if (pi != null)
                    {
                        if (sortColumnDirection == "asc")
                        {
                            playerList = playerList.OrderBy(x => pi.GetValue(x, null)).ToList();
                        }
                        else
                        {
                            playerList = playerList.OrderByDescending(x => pi.GetValue(x, null)).ToList();
                        }
                    }
                }

                //Search  
                if (!string.IsNullOrEmpty(searchValue))
                {
                    playerList = playerList.Where(m => m.PlaceOfBirth.ToLower().Contains(searchValue) || m.Age.ToString() == searchValue).ToList();
                }

                //Paging   
                var data = playerList.Skip(skip).Take(pageSize).ToList();

                //Returning Json Data  
                return Json(new { recordsFiltered = playerList.Count, recordsTotal = playerList.Count, data });
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        public bool DeletePlayer(int PlayerID)
        {
            bool isDeleted = false;
            try
            {
                var returnValue = playerBusiness.DeletePlayer(PlayerID);
                if (returnValue == 1)
                {
                    isDeleted = true;
                }

            }
            catch
            {
                throw;
            }
            return isDeleted;
        }

        public ActionResult EditPlayerView(int playerID)
        {
            PlayerModel playerDetails = new PlayerModel();
            if (playerID > 0)
            {
                playerDetails = playerBusiness.GetPlayerDetails(playerID);
            }
            return PartialView("_EditPlayerView", playerDetails);
        }

        [HttpPost]
        public ActionResult SavePlayerDetails(PlayerModel model)
        {
            playerBusiness.SavePlayerDetails(model);
            return Json(true, JsonRequestBehavior.AllowGet);
            //RedirectToAction("PlayerListView");
        }

        public JsonResult GetTeamList()
        {
            List<TeamModel> lstTeams = teamBusiness.GetAllTeams();
            return Json(lstTeams, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public PartialViewResult PlayerInformationView(int playerId)
        {
            string filePath = Server.MapPath($"~/Info/Players/{playerId}.txt");
            string content = string.Empty;
            if (System.IO.File.Exists(filePath))
            {
                content = System.IO.File.ReadAllText(filePath);
            }
            ViewBag.Content = content;
            ViewBag.PlayerId = playerId;
            return PartialView("_PlayerInformationView");
        }

        [HttpPost]
        public JsonResult SavePlayerInformation(int playerId, string content)
        {
            try
            {
                string filePath = Server.MapPath($"~/Info/Players/{playerId}.txt");
                System.IO.File.WriteAllText(filePath, content);
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public void Download(int playerId)
        {
            string filePath = Server.MapPath($"~/Info/Players/{playerId}.txt");
            if (System.IO.File.Exists(filePath))
            {
                string fileContent = System.IO.File.ReadAllText(filePath);
                Response.Clear();

                Response.AddHeader("content-disposition", "attachment;filename=" + $"{ playerId}.txt");
                Response.ContentType = "text/plain";
                Response.AddHeader("Content-Length", fileContent.Length.ToString());
                Response.Output.Write(fileContent);
                Response.Flush();
                Response.End();

            }


        }
    }
}