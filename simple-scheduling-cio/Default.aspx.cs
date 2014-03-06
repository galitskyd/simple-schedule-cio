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
    DataView dv;
    static Boolean asc = true;
    static string sort = "Asc";
    protected void Page_Load(object sender, EventArgs e)
    {
            GridView1.DataSource = basicInfo.dt();
            GridView1.DataBind();
            System.Console.Write(basicInfo.dt());
            if (FilterSearchTermsAppointment.Text == "") FilterSearchTermsAppointment.Text = DateTime.Today.ToString("MM/dd/yyyy");
            
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
   /* protected void gridViewUpdate()
    {
        DataView dv;
        dv = basicInfo.dt().DefaultView;
        dv.RowFilter = "Doctor" + " LIKE '*" + FilterSearchTerms.Text + "*'";
        GridView1.DataSource = dv;
        GridView1.DataBind();
    }
    */
    protected void txtSearch_KeyUp(object sender, EventArgs e)
    {
        dv = basicInfo.dt().DefaultView;
        string outputInfo = "";

        if (outputInfo.Length == 0)
        {
            outputInfo = "(Doctor LIKE '%" + FilterSearchTermsDoctor.Text + "%' AND Patient LIKE '%" + FilterSearchTermsPatient.Text + "%' AND Location LIKE '%" + FilterSearchTermsLocation.Text + "%' AND Duration LIKE '%" + FilterSearchTermsDuration.Text + "%' AND [Appt Date] LIKE '%" + FilterSearchTermsAppointment.Text + "%' AND Details LIKE '%" + FilterSearchTermsDetail.Text + "%' AND Status LIKE '%" + FilterSearchTermsStatus.Text + "%')";
        }
        else
        {
            outputInfo += " AND (Doctor LIKE '%" + FilterSearchTermsDoctor.Text + "%' AND Patient LIKE '%" + FilterSearchTermsPatient.Text + "%' AND Location LIKE '%" + FilterSearchTermsLocation.Text + "%' AND Duration LIKE '%" + FilterSearchTermsDuration.Text + "%' AND [Appt Date] LIKE '%" + FilterSearchTermsAppointment.Text + "%' AND Details LIKE '%" + FilterSearchTermsDetail.Text + "%' AND Status LIKE '%" + FilterSearchTermsStatus.Text + "%')";
        }



        dv.RowFilter = outputInfo;
        GridView1.DataSource = dv;
        GridView1.DataBind();
    }

}