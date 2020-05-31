using PlayerManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerManagementSystem.BusinessLayer.Abstract
{
    public interface IReportBusiness
    {
        GraphModel GetGraphData();

        GraphModel GetAgeGroupGraphData();
    }
}
