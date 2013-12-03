using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;

public partial class _Default : System.Web.UI.Page
{
    static Boolean asc = true;
    static string sort = "Asc";
    protected void Page_Load(object sender, EventArgs e)
    {
            GridView1.DataSource = basicInfo.dt();
            GridView1.DataBind();
    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (asc) { sort = "ASC"; asc = false; }
        else { sort = "DESC"; asc = true; }
        DataTable dataTable = GridView1.DataSource as DataTable;

            DataView dataView = new DataView(dataTable);
            dataView.Sort = e.SortExpression + " " + sort;

            GridView1.DataSource = dataView;
            GridView1.DataBind();
    }
}