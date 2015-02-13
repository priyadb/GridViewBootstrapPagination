using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;

namespace GridViewBootstrapPagination
{
    public class Revenue
    {

        public Revenue(string country, string revenue, string salesmanager, string year)
        {
            this.country = country;
            this.revenue = revenue;
            this.salesmanager = salesmanager;
            this.year = year;
        }

        public Revenue() { }
         
        public string country { get; set; }
        public string revenue { get; set; }
        public string salesmanager { get; set; }
        public string year { get; set; }

        public List<Revenue> GetRevenueDetails(int pagenumber,int maxrecords)
        {
            List<Revenue> lstRevenue = new List<Revenue>();
            string filename = HttpContext.Current.Server.MapPath("~/App_Data/country_revenue.csv");
            int startrecord = (pagenumber * maxrecords) - maxrecords;
            if (File.Exists(filename))
            {
                IEnumerable<int> range = Enumerable.Range(startrecord, maxrecords);
                IEnumerable<String> lines = getFileLines(filename, range);
                foreach (String line in lines)
                {
                    string[] row = line.Split(',');
                    lstRevenue.Add(new Revenue(row[0], row[1], row[2], row[3]));
                }
                   
            }
            return lstRevenue;
        }

        public static IEnumerable<String> getFileLines(String path, IEnumerable<int> lineIndices)
        {
            return File.ReadLines(path).Where((l, i) => lineIndices.Contains(i));
        }

        public int GetTotalRecordCount()
        {          
            string filename = HttpContext.Current.Server.MapPath("~/App_Data/country_revenue.csv");
            int count = 0;
            if (File.Exists(filename))
            {
                string[] data = File.ReadAllLines(filename);
                count= data.Length;
            }
            return count;
        }        
    }
}
