using PlayerManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerManagementSystem.DataAccessLayer.Mappers
{
    public static class ReportDataMapper
    {
        public static GraphModel MapDataTableToObject(this DataTable dt)
        {
            GraphModel graphModel= new GraphModel();

            graphModel.labels = new List<string>();

            if (dt != null && dt.Rows.Count > 0)
            {
                
                graphModel.datasets = new List<Models.DataSet>();

                List<string> heightData = new List<string>();
                List<string> weightData = new List<string>();

                foreach (DataRow dr in dt.Rows)
                {
                    graphModel.labels.Add( Convert.ToString( dr["TXT_NME_PLAYER"]));
                    heightData.Add(Convert.IsDBNull(dr["NUM_HEIGHT"]) ? "0.00" : string.Format("{0:0.00}",Convert.ToDecimal(dr["NUM_HEIGHT"])));
                    weightData.Add(Convert.IsDBNull(dr["NUM_WEIGHT"]) ? "0.00" : string.Format("{0:0.00}", Convert.ToDecimal(dr["NUM_WEIGHT"])));
                }
                graphModel.datasets = new List<Models.DataSet>
                {
                    new Models.DataSet
                    {
                         backgroundColor = "green",
                          label = "Weight",
                          data = weightData
                    },

                    new Models.DataSet
                    {
                         backgroundColor = "blue",
                          label = "Height",
                          data = heightData
                    }
                };
            }

            return graphModel;
        }


        public static GraphModel MapDataTableToAgeGroupGraphObject(this DataTable dt)
        {
            GraphModel graphModel = new GraphModel();

            graphModel.labels = new List<string>();

            foreach(DataColumn col in dt.Columns)
            {
                graphModel.labels.Add(col.ColumnName);
            }

            if (dt != null && dt.Rows.Count > 0)
            {

                graphModel.datasets = new List<Models.DataSet>();

                List<string> ageGroupCountList = new List<string>();

                

                foreach (DataRow dr in dt.Rows)
                {
                    foreach (DataColumn col in dt.Columns)
                    {                        
                        ageGroupCountList.Add(Convert.IsDBNull(dr[col.ColumnName]) ? "0" : $"{dr[col.ColumnName]}"); 
                    }
                                       
                }
                graphModel.datasets = new List<Models.DataSet>
                {
                    new Models.DataSet
                    {
                         backgroundColor = "blue",
                          label = "Count",
                          data = ageGroupCountList
                    }
                };
            }

            return graphModel;
        }
    }
}
