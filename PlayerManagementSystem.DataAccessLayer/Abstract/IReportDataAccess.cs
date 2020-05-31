using PlayerManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerManagementSystem.DataAccessLayer.Abstract
{
    public interface IReportDataAccess
    {
        GraphModel GetGraphData();
        GraphModel GetAgeGroupGraphData();

    }
}
