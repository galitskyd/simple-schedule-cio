using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using System.Web.Services;
using System.Web.Script.Services;
using System.Text.RegularExpressions;

public partial class PrintPage : System.Web.UI.Page
{
    DataTable dt = basicInfoSurgery.dt();
    DataView dv;
    protected void Page_Load(object sender, EventArgs e)
    {
        listView1Update();
        listView2Update();
        listView3Update();
    }

    protected void listView1Update()
    {
        dv = new DataView(dt);
        dv.RowFilter = "Date='" + Session["date"].ToString() + "' AND Room='" + "1" + "'AND Location='" + Session["location"].ToString() +"'";
        ListView1.DataSource = dv;
        ListView1.DataBind();
    }
    protected void listView2Update()
    {
        dv = new DataView(dt);
        dv.RowFilter = "Date='" + Session["date"].ToString() + "' AND Room='" + "2" + "'AND Location='" + Session["location"].ToString() + "'";
        ListView2.DataSource = dv;
        ListView2.DataBind();
    }
    protected void listView3Update()
    {
        dv = new DataView(dt);
        dv.RowFilter = "Date='" + Session["date"].ToString() + "' AND Room='" + "3" + "'AND Location='" + Session["location"].ToString() + "'";
        ListView3.DataSource = dv;
        ListView3.DataBind();
    }
}