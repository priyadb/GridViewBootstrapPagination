using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;


namespace GridViewBootstrapPagination
{
    public partial class Default : System.Web.UI.Page
    {
        private const int MAX_RECORDS = 5;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            string filename = Server.MapPath("~/App_Data/country_revenue.csv");
            if (!IsPostBack)
            {
                List<Revenue> revenue = GetRevenueDetail(1);
                gvBSPagination.DataSource = revenue;
                gvBSPagination.DataBind();

            }
           
        }

        [WebMethod]
	    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]	       
	       public static List<Revenue> GetRevenueDetail(int pagenumber)
           {
               Revenue rv = new Revenue();
               List<Revenue> lstrevenue = rv.GetRevenueDetails(pagenumber,MAX_RECORDS);            
               return lstrevenue;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public static int GetTotalPageCount()
        {
            int count=0;
            Revenue rv=new Revenue();
            count = rv.GetTotalRecordCount();
            count = count / MAX_RECORDS;
            return count;
        }
        protected void gvBSPagination_PreRender(object sender, EventArgs e)
        {
            GridView gv = (GridView)sender;
            GridViewRow pagerRow = (GridViewRow)gv.BottomPagerRow;

            if (pagerRow != null && pagerRow.Visible == false)
                pagerRow.Visible = true;
        }
    }
}