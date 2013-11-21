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
    protected void Page_Load(object sender, EventArgs e)
    {
        //Connection String ---NOTE*** Im Looking into Changing DataSource Name--
        string connString = "Data Source=MATT-LT\\SQLEXPRESS;Initial Catalog=BSU_PROJECT;Persist Security Info=True;User ID=admin;Password=capstone";
        //SQL Statment String
        string sqlCmdString = "SELECT dbo.provider_mstr.last_name,dbo.provider_mstr.first_name, dbo.provider_mstr.middle_name, dbo.location_mstr.location_name, dbo.appointments.appt_date, dbo.appointments.begintime, dbo.appointments.endtime, dbo.appointments.duration,dbo.appointments.details,dbo.appointments.appt_kept_ind FROM dbo.provider_mstr, dbo.location_mstr, dbo.appointments WHERE dbo.provider_mstr.primary_loc_id = dbo.location_mstr.location_id AND dbo.provider_mstr.provider_id = dbo.appointments.rendering_provider_id";

        //SQL Connection
        SqlConnection conn = new SqlConnection(connString);
        //Attempt to Open Connection with DB on Local Server
        try { conn.Open();
            
            try
            {
                SqlCommand cmd = new SqlCommand(sqlCmdString, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            catch (Exception) { Response.Write("Error With Command"); }

        }
        catch (Exception) { Response.Write("Error Could not Connect<br>"); }
    }
}