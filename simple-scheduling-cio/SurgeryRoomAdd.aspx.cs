using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default2 : System.Web.UI.Page
{
    DataTable patients = new DataTable();
    DataTable providers = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            String location = Request.QueryString["loc"];
            String roomNum = Request.QueryString["rm"];
            String date = Request.QueryString["date"];
            //LoadPatients();
            LoadProviders();
            ddlLocation.SelectedValue = location;
            ddlRoom.SelectedValue = roomNum;
            tbDate.Text = date;
        }
    }

    /**private void LoadPatients()
    {
        SqlConnection conn = dbConnect.connection();
        try
        {
            String sqlCmdString = "SELECT DISTINCT dbo.patient.med_rec_nbr,"+
                "dbo.person.last_name,"+
                "dbo.person.first_name"+
                " FROM dbo.patient,dbo.person"+
                " WHERE dbo.patient.person_id=dbo.person.person_id;";
            conn.Open();
            SqlCommand cmd = new SqlCommand(sqlCmdString, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            patients.Load(reader);
            conn.Close();
        }
        catch { Console.WriteLine("Error"); }
        
        patients.Columns.Add("full_name", typeof(string), "last_name + ', ' + first_name");
        ddlPatient.DataSource = patients;
        ddlPatient.DataTextField = "full_name";
        ddlPatient.DataValueField = "med_rec_nbr";
        ddlPatient.DataBind();
    }*/

    private void LoadProviders()
    {
        using (SqlConnection conn = dbConnect.connectionSurgery())
        {
            String sqlCmdString = "surgGetProviders";
            
            using (SqlCommand cmd = new SqlCommand(sqlCmdString, conn))
            {
                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    providers.Load(reader);
                    conn.Close();
                }
                catch { Console.WriteLine("Error"); }
            }
        }

        ddlProvider.DataSource = providers;
        ddlProvider.DataTextField = "description";
        ddlProvider.DataValueField = "provider_id";
        ddlProvider.DataBind();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        String date = tbDate.Text;
        string pattern = "yyyy/MM/dd";
        DateTime parsedDate;
        DateTime.TryParseExact(date, pattern, null, System.Globalization.DateTimeStyles.None, out parsedDate);
        date = parsedDate.ToString("yyyyMMdd");
        
        using (SqlConnection conn = dbConnect.connectionSurgery())
        {
            String insertEvent = "surgAddEvent";
            
            using (SqlCommand cmd = new SqlCommand(insertEvent, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@location_name", ddlLocation.SelectedValue);
                cmd.Parameters.AddWithValue("@room_number", ddlRoom.SelectedValue);
                cmd.Parameters.AddWithValue("@surg_date", date);
                cmd.Parameters.AddWithValue("@duration", tbDuration.Text);
                cmd.Parameters.AddWithValue("@provider_id", ddlProvider.SelectedValue);
                cmd.Parameters.AddWithValue("@med_rec_nbr", tbPatient.Text);
                cmd.Parameters.AddWithValue("@surgery_name", tbSurgery.Text);
                cmd.Parameters.AddWithValue("@details", "");

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    Response.Redirect("SurgeryRoom.aspx");
                }
                catch { Console.WriteLine("Error"); }
            }
        }
    }
}