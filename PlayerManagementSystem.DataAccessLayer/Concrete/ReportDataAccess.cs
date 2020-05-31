using PlayerManagementSystem.DataAccessLayer.Abstract;
using PlayerManagementSystem.DataAccessLayer.Mappers;
using PlayerManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerManagementSystem.DataAccessLayer.Concrete
{
    public class ReportDataAccess : IReportDataAccess
    {
        public GraphModel GetAgeGroupGraphData()
        {
            GraphModel graphModel = null;
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection sql_conn = new SqlConnection(ConfigurationManager.ConnectionStrings["PMSConnectionString"].ConnectionString))
                {
                    SqlCommand sqlComm = new SqlCommand("GetAgeGroupGraphData", sql_conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    da.Fill(dt);

                    graphModel = dt.MapDataTableToAgeGroupGraphObject();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return graphModel;
        }

        public GraphModel GetGraphData()
        {
           GraphModel graphModel = null;
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection sql_conn = new SqlConnection(ConfigurationManager.ConnectionStrings["PMSConnectionString"].ConnectionString))
                {
                    SqlCommand sqlComm = new SqlCommand("GetGraphData", sql_conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    da.Fill(dt);

                    graphModel = dt.MapDataTableToObject();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return graphModel;
        }
    }
}
