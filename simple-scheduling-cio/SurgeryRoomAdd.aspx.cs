﻿using System;
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
    DataTable dtAnesthesia = new DataTable();
    DataTable dtEquipment = new DataTable();
    DataTable dtPlatesImplants = new DataTable();
    surgManager surgMan = new surgManager();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            String location = Request.QueryString["loc"];
            String roomNum = Request.QueryString["rm"];
            String date = Request.QueryString["date"];
            //LoadPatients();
            LoadProviders();
            LoadItems();
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

        lbProvider.DataSource = providers;
        lbProvider.DataTextField = "description";
        lbProvider.DataValueField = "provider_id";
        lbProvider.DataBind();
    }
    private void LoadItems()
    {
        DataView dv;
        surgMan.getItems("surgAnesthesiaGet", dtAnesthesia);
        dv = dtAnesthesia.AsDataView();
        dv.RowFilter = "enabled = 1";
        dtAnesthesia = dv.ToTable();
        lbAnesthesia.DataSource = dtAnesthesia;
        lbAnesthesia.DataTextField = "name";
        lbAnesthesia.DataValueField = "id";
        lbAnesthesia.DataBind();
        surgMan.getItems("surgEquipmentGet", dtEquipment);
        dv = dtEquipment.AsDataView();
        dv.RowFilter = "enabled = 1";
        dtEquipment = dv.ToTable(); lbEquipment.DataSource = dtEquipment;
        lbEquipment.DataTextField = "name";
        lbEquipment.DataValueField = "id";
        lbEquipment.DataBind();
        surgMan.getItems("surgPlatesImplantsGet", dtPlatesImplants);
        dv = dtPlatesImplants.AsDataView();
        dv.RowFilter = "enabled = 1";
        dtPlatesImplants = dv.ToTable(); lbPlatesImplants.DataSource = dtPlatesImplants;
        lbPlatesImplants.DataTextField = "name";
        lbPlatesImplants.DataValueField = "id";
        lbPlatesImplants.DataBind();

    }

    private void addEventComponent(String cmdString, ListBox lb, int id, String type)
    {
        foreach (ListItem item in lb.Items)
        {
            if (item.Selected == true)
            {
                using (SqlConnection conn = dbConnect.connectionSurgery())
                {
                    using (SqlCommand cmd = new SqlCommand(cmdString, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@surgEventID", id);
                        if ((type == "A" || type == "E" || type == "P") && 
                            (cmdString=="surgAddEventItem"))
                        {
                            cmd.Parameters.AddWithValue("@itemID", item.Value);
                            cmd.Parameters.AddWithValue("@type", type);
                        }
                        else if ((type == "provider") && (cmdString=="surgAddEventProvider")) cmd.Parameters.AddWithValue("@providerID", item.Value);
                        try
                        {
                            System.Diagnostics.Debug.WriteLine("Insert Item " + type);
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();
                            cmd.Parameters.Clear();
                        }
                        catch { Console.WriteLine("Error"); }
                    }
                }
            }
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        String date = tbDate.Text;
        string pattern = "yyyy/MM/dd";
        DateTime parsedDate;
        DateTime.TryParseExact(date, pattern, null, System.Globalization.DateTimeStyles.None, out parsedDate);
        date = parsedDate.ToString("yyyyMMdd");
        String insertEvent = "surgAddEvent";
        Boolean blnRedirect = false;
        int id = -1;
        DataTable dtID = new DataTable();

        using (SqlConnection conn = dbConnect.connectionSurgery())
        {
            using (SqlCommand cmd = new SqlCommand(insertEvent, conn))
            {
                System.Diagnostics.Debug.WriteLine("Insert Event");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@location_name", ddlLocation.SelectedValue);
                cmd.Parameters.AddWithValue("@room_number", ddlRoom.SelectedValue);
                cmd.Parameters.AddWithValue("@surg_date", date);
                cmd.Parameters.AddWithValue("@duration", tbDuration.Text);
                cmd.Parameters.AddWithValue("@provider_id", lbProvider.SelectedValue);
                cmd.Parameters.AddWithValue("@med_rec_nbr", tbPatient.Text);
                cmd.Parameters.AddWithValue("@surgery_name", tbSurgery.Text);
                cmd.Parameters.AddWithValue("@details", "");

                try
                {
                    System.Diagnostics.Debug.WriteLine("Insert Event Try");
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    dtID.Load(reader);
                    conn.Close();
                    id = Int32.Parse(dtID.Rows[0][0].ToString());
                    System.Diagnostics.Debug.WriteLine(id);
                    blnRedirect = true;
                }
                catch { Console.WriteLine("Error"); }
            }
        }
        addEventComponent("surgAddEventProvider", lbProvider, id, "provider");
        addEventComponent("surgAddEventItem", lbAnesthesia, id, "A");
        addEventComponent("surgAddEventItem", lbEquipment, id, "E");
        addEventComponent("surgAddEventItem", lbPlatesImplants, id, "P");
        if (blnRedirect) Response.Redirect("SurgeryRoom.aspx");
    }
}