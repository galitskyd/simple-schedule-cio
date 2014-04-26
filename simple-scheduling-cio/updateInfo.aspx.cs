using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Collections.Specialized;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;
public partial class updateInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        System.Diagnostics.Debug.Write("test--updatePage");

        /*
        int begin = int.Parse(Request.QueryString["initial"]);
        int end = int.Parse(Request.QueryString["final"]) ;
        string date = Request.QueryString["date"];
        string location = Request.QueryString["location"];
        string room = Request.QueryString["room"];

        
        
        DataTable dtBind = new DataTable();
        DataTable dt = basicInfoSurgery.dt();
        DataView dv = new DataView(dt);

        dv.RowFilter = "Location='" + location + "' AND Room='" + room + "' AND Date='" + date + "'";
        dt = dv.ToTable();

        string ID = dt.Rows[begin - 1]["ID"].ToString();
        string countCol = dt.Columns.Count.ToString();
        List<List<string>> numbers = new List<List<string>>();
         */
    }
}