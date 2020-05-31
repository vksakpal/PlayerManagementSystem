using PlayerManagementSystem.BusinessLayer.Abstract;
using PlayerManagementSystem.BusinessLayer.Concrete;
using PlayerManagementSystem.Filters;
using PlayerManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace PlayerManagementSystem.Controllers
{
    [CustomAuthFilter]
    public class TeamManagementController : Controller
    {
        private readonly ITeamBusiness teamBusiness;
        private readonly IPlayerBusiness playerBusiness;
        public TeamManagementController()
        {
            teamBusiness = new TeamBusiness();
            playerBusiness = new PlayersBusiness();
        }


        // GET: TeamManagement
        public ActionResult TeamListView()
        {
            return View();
        }

        public ActionResult GetTeamList()
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

                int recordsTotal = 0;

                var teamList = teamBusiness.GetAllTeams();

                //Sorting  
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    var pi = typeof(TeamModel).GetProperty(sortColumn);
                    if (pi != null)
                    {
                        if (sortColumnDirection == "asc")
                        {
                            teamList = teamList.OrderBy(x => pi.GetValue(x, null)).ToList();
                        }
                        else
                        {
                            teamList = teamList.OrderByDescending(x => pi.GetValue(x, null)).ToList();
                        }
                    }
                    //playerList = playerList.OrderBy(sortColumn + " " + sortColumnDirection);
                }
                //Search  
                if (!string.IsNullOrEmpty(searchValue))
                {
                    teamList = teamList.Where(m => m.TeamName.ToLower().Contains(searchValue)).ToList();
                }

                //total number of rows counts
                recordsTotal = teamList.Count();

                //Paging   
                var data = teamList.Skip(skip).Take(pageSize).ToList();

                //Returning Json Data  
                return Json(new { recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data });
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        public bool DeleteTeam(int TeamID)
        {
            bool isDeleted = false;
            try
            {
                var returnValue = teamBusiness.DeleteTeam(TeamID);
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

        public ActionResult EditTeamView(int teamID)
        {
            TeamModel teamDetails = null;
            if (teamID > 0)
            {                
                teamDetails = teamBusiness.GetTeamDetails(teamID);
            }

            return PartialView("_EditTeamView", teamDetails);
        }

        public ActionResult TeamMembersView(int TeamID)
        {
            //List<PlayerModel> players = teamBusiness.GetTeamMembers(TeamID);
            ViewBag.TeamID = TeamID;
            return PartialView("_TeamMembersView");
        }

        public ActionResult NotAssignedMembersView(int TeamID)
        {
            ViewBag.TeamID = TeamID;
            return PartialView("_NotAssignedMembersView");
        }

        public ActionResult GetTeamPlayerList(int TeamID, int Option)
        {
            List<PlayerModel> players = null;

            if (Option == 0)
            {
                players = teamBusiness.GetTeamMembers(TeamID);
            }
            else
            {
                players = playerBusiness.GetAllPlayers().Where(p => p.AssignedTeam == "").ToList();
            }

            return Json(new { recordsFiltered = players.Count, recordsTotal = players.Count, data = players });
        }

        [HttpPost]
        public ActionResult SaveTeamDetails(TeamModel model)
        {
            int returnValue = teamBusiness.SaveTeamDetails(model);
            return Json(returnValue, JsonRequestBehavior.AllowGet);
            //RedirectToAction("PlayerListView");

        }

        [HttpPost]
        public ActionResult AssignSelectedMembers(int[] playerIDs, int teamId)
        {
            bool isUpdated = false;
            string[] stringValues = Array.ConvertAll<int, string>(playerIDs, Convert.ToString);
            string result = "(" + String.Join(",", stringValues) + ")";

            int returnValue = teamBusiness.AssignTeamMembers(teamId, result);
            if (returnValue > 0)
            {
                isUpdated = true;
            }

            return Json(isUpdated, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public PartialViewResult TeamInformationView(string teamName)
        {
            string filePath = Server.MapPath($"~/Info/Teams/{teamName}.txt");
            string content = string.Empty;
            if (System.IO.File.Exists(filePath))
            {
                content = System.IO.File.ReadAllText(filePath);
            }
            ViewBag.Content = content;
            ViewBag.TeamName = teamName;
            return PartialView("_TeamInformationView");
        }

        [HttpPost]
        public JsonResult SaveTeamInformation(string teamName, string content)
        {
            try
            {
                string filePath = Server.MapPath($"~/Info/Teams/{teamName}.txt");
                System.IO.File.WriteAllText(filePath, content);
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public void Download(string teamName)
        {
            string filePath = Server.MapPath($"~/Info/Teams/{teamName}.txt");
            if (System.IO.File.Exists(filePath))
            {
                string fileContent = System.IO.File.ReadAllText(filePath);
                Response.Clear();

                Response.AddHeader("content-disposition", "attachment;filename=" + $"{ teamName}.txt");
                Response.ContentType = "text/plain";
                Response.AddHeader("Content-Length", fileContent.Length.ToString());
                Response.Output.Write(fileContent);
                Response.Flush();
                Response.End();

            }
        }
    }
}