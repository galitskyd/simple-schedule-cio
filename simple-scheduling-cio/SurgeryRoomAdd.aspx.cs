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
            LoadPatients();
            LoadProviders();
            ddlLocation.SelectedValue = location;
            ddlRoom.SelectedValue = roomNum;
            tbDate.Text = date;
        }
    }

    private void LoadPatients()
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
    }

    private void LoadProviders()
    {
        SqlConnection conn = dbConnect.connection();
        try
        {
            String sqlCmdString = "SELECT DISTINCT dbo.provider_mstr.description,dbo.provider_mstr.provider_id FROM dbo.provider_mstr;";
            conn.Open();
            SqlCommand cmd = new SqlCommand(sqlCmdString, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            providers.Load(reader);
            conn.Close();
        }
        catch { Console.WriteLine("Error"); }

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

        //Need to determine exiting positions...
        String insertEvent = "INSERT INTO dbo.surgery_room_schedule " +
            "VALUES ("+
            "(SELECT Max(ordering_position) " +
            "FROM dbo.surgery_room_schedule " + 
            "WHERE location_id=(SELECT location_id " + 
                "FROM dbo.location_mstr " + 
                "WHERE location_name='" + ddlLocation.SelectedValue + "') " + 
            "AND room_number=" + ddlRoom.SelectedValue + ")+1"+
            ",'" +
            date + "'," + tbDuration.Text + 
            ",(SELECT location_id FROM dbo.location_mstr WHERE location_name='" +
            ddlLocation.SelectedValue+"')," +
            ddlRoom.SelectedValue+",'" +
            ddlProvider.SelectedValue+"','" +
            ddlPatient.SelectedValue+"','" +
            tbSurgery.Text + "','" + tbDetails.Text + "');";
        
        //SQL Injection Protection Stuff
        /*String insertEvent = "INSERT INTO dbo.surger_room_schedule " +
            "values(@ordering_position,@surg_date,@duration,"+
            "@location_id,@room_number,@provider_id,@med_rec_nbr"+
            "@surgery_name,@details)";*/

        SqlConnection conn = dbConnect.connection();
        try
        {
            String sqlCmdString = insertEvent;
            conn.Open();
            SqlCommand cmd = new SqlCommand(sqlCmdString, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            Response.Redirect("SurgeryRoom.aspx");
        }
        catch { Console.WriteLine("Error"); }
    }
}