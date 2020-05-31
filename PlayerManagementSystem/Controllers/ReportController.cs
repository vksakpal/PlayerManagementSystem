using PlayerManagementSystem.BusinessLayer.Abstract;
using PlayerManagementSystem.BusinessLayer.Concrete;
using PlayerManagementSystem.Filters;
using PlayerManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlayerManagementSystem.Controllers
{
    [CustomAuthFilter]
    public class ReportController : Controller
    {
        private readonly IReportBusiness _reportBusiness;

        public ReportController()
        {
            _reportBusiness = new ReportBusiness();
        }

        public ActionResult Graph()
        {
            return View("GraphView");
        }

        public ActionResult Histogram()
        {
            return View("HistogramView");
        }

        [HttpGet]
        public JsonResult GetHeightWeightGraphData()
        {
            GraphModel graphModel = _reportBusiness.GetGraphData();
            return Json(graphModel, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAgeGroupGraphData()
        {
            GraphModel graphModel = _reportBusiness.GetAgeGroupGraphData();
            return Json(graphModel, JsonRequestBehavior.AllowGet);
        }
    }
}