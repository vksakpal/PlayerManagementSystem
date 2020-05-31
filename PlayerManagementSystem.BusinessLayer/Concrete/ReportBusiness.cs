using PlayerManagementSystem.BusinessLayer.Abstract;
using PlayerManagementSystem.DataAccessLayer.Abstract;
using PlayerManagementSystem.DataAccessLayer.Concrete;
using PlayerManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerManagementSystem.BusinessLayer.Concrete
{
    public class ReportBusiness : IReportBusiness
    {
        private readonly IReportDataAccess _reportDataAccess;
        public ReportBusiness()
        {
            _reportDataAccess = new ReportDataAccess();
        }

        public GraphModel GetAgeGroupGraphData()
        {
            return _reportDataAccess.GetAgeGroupGraphData();
        }

        public GraphModel GetGraphData()
        {
            return _reportDataAccess.GetGraphData();
        }
    }
}
