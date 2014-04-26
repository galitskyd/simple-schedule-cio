using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default2 : System.Web.UI.Page
{
    DataTable dtPatients = new DataTable();
    DataTable dtProviders = new DataTable();
    DataTable dtORRooms = new DataTable();
    DataTable dtAnesthesia = new DataTable();
    DataTable dtEquipment = new DataTable();
    DataTable dtPlatesImplants = new DataTable();
    DataTable dtModifyEvent = new DataTable();
    surgManager surgMan = new surgManager();
    protected int intSumDuration;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Session["date"] != null) tbDate.Text = Session["date"].ToString();
            if (Session["location"] != null) ddlLocation.SelectedValue = Session["location"].ToString();
            if (Session["room"] != null) ddlRoom.SelectedValue = Session["room"].ToString();
            LoadProviders();
            LoadORRooms();
            LoadItems();
            if (Session["surg_event_id"] != null)
            {
                loadModifyEvent();
                intSumDuration = sumDuration(Session["surg_event_id"].ToString());
                btnAdd.Text = "Modify Event";
            }
            else intSumDuration = sumDuration(null);
        }
        
        if (Page.IsPostBack)
        {
            WebControl wcICausedPostBack = (WebControl)GetPostBackControl(sender as Page);
            int indx = wcICausedPostBack.TabIndex;
            var ctrl = from control in wcICausedPostBack.Parent.Controls.OfType<WebControl>()
                       where control.TabIndex > indx
                       select control;
            ctrl.DefaultIfEmpty(wcICausedPostBack).First().Focus();

            if (Session["surg_event_id"] != null)
            {
                intSumDuration = sumDuration(Session["surg_event_id"].ToString());
            }
            else intSumDuration = sumDuration(null);
        }
    }

    protected Control GetPostBackControl(Page page)
    {
        Control control = null;
        string ctrlname = page.Request.Params.Get("__EVENTTARGET");
        if (ctrlname != null && ctrlname != string.Empty)
        {
            control = page.FindControl(ctrlname);
        }
        return control;
    }

    private void FindPatients(String medrecnbr)
    {
        dtPatients.Clear();
        if (medrecnbr.Length == 12)
            using (SqlConnection conn = dbConnect.connectionSurgery())
            {
                String sqlCmdString = "surgGetPatient";
                using (SqlCommand cmd = new SqlCommand(sqlCmdString, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@med_rec_nbr", medrecnbr);
                    try
                    {
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        dtPatients.Load(reader);
                        conn.Close();
                    }
                    catch { Console.WriteLine("Error"); }
                }
            }
        showPatient.Text = ""; 
        if (dtPatients.Rows.Count > 0)
            showPatient.Text = dtPatients.Rows[0]["last_name"] + ", " + dtPatients.Rows[0]["first_name"];
    }
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
                    dtProviders.Load(reader);
                    conn.Close();
                }
                catch { Console.WriteLine("Error"); }
            }
        }

        lbProvider.DataSource = dtProviders;
        lbProvider.DataBind();
    }
    private void LoadORRooms()
    {
        using (SqlConnection conn = dbConnect.connectionSurgery())
        {
            String sqlCmdString = "surgGetORRooms";
            using (SqlCommand cmd = new SqlCommand(sqlCmdString, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@location_name", ddlLocation.SelectedValue.ToString());
                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    dtORRooms.Load(reader);
                    conn.Close();
                }
                catch
                { Console.WriteLine("Error"); }
            }
        }
        ddlRoom.DataSource = dtORRooms;
        ddlRoom.DataBind();
    }
    private void LoadItems()
    {
        DataView dv;
        surgMan.getItems("surgAnesthesiaGet", dtAnesthesia);
        dv = dtAnesthesia.AsDataView();
        dv.RowFilter = "enabled = 1";
        dtAnesthesia = dv.ToTable();
        lbAnesthesia.DataSource = dtAnesthesia;
        lbAnesthesia.DataBind();
        surgMan.getItems("surgEquipmentGet", dtEquipment);
        dv = dtEquipment.AsDataView();
        dv.RowFilter = "enabled = 1";
        dtEquipment = dv.ToTable(); lbEquipment.DataSource = dtEquipment;
        lbEquipment.DataBind();
        surgMan.getItems("surgPlatesImplantsGet", dtPlatesImplants);
        dv = dtPlatesImplants.AsDataView();
        dv.RowFilter = "enabled = 1";
        dtPlatesImplants = dv.ToTable(); lbPlatesImplants.DataSource = dtPlatesImplants;
        lbPlatesImplants.DataBind();
    }
    protected void loadModifyEvent()
    {
        btnBlock.Enabled = false;
        btnBlock.Visible = false;
        btnDeleteItem.Enabled = true;
        btnDeleteItem.Visible = true;
        using (SqlConnection conn = dbConnect.connectionSurgery())
        {
            String sqlCmdString = "surgGetEvent";
            using (SqlCommand cmd = new SqlCommand(sqlCmdString, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@surgery_event_id", Session["surg_event_id"].ToString());
                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    dtModifyEvent.Load(reader);
                    conn.Close();
                }
                catch
                { Console.WriteLine("Error"); }
            }
        }
        foreach (DataColumn col in dtModifyEvent.Columns)
        {
            if (col.ColumnName == "Column1") ddlLocation.SelectedValue = dtModifyEvent.Rows[0][col].ToString();
            if (col.ColumnName == "room_number") ddlRoom.SelectedValue = dtModifyEvent.Rows[0][col].ToString();
            if (col.ColumnName == "surg_date")
            {
                String date = dtModifyEvent.Rows[0][col].ToString();
                String pattern = "yyyyMMdd";
                DateTime parsedDate;
                DateTime.TryParseExact(date, pattern, null, System.Globalization.DateTimeStyles.None, out parsedDate);
                date = parsedDate.ToString("MM/dd/yyyy");
                tbDate.Text = date;
            }
            if (col.ColumnName == "duration") tbDuration.Text = dtModifyEvent.Rows[0][col].ToString();
            if (col.ColumnName == "med_rec_nbr")
            {
                tbPatient.Text = dtModifyEvent.Rows[0][col].ToString();
                if (dtModifyEvent.Rows[0][col].ToString() != "")
                    FindPatients(dtModifyEvent.Rows[0][col].ToString());
            }
            if (col.ColumnName == "surgery_details")
                if (tbPatient.Text == "")
                    if (dtModifyEvent.Rows[0][col].ToString().Contains("BLOCKED TIME: "))
                        tbSurgery.Text = dtModifyEvent.Rows[0][col].ToString().Substring(14);
                    else tbSurgery.Text = "";
                else tbSurgery.Text = dtModifyEvent.Rows[0][col].ToString();
            if (col.ColumnName == "latex_allergy") chkLatex.Checked = Convert.ToBoolean(dtModifyEvent.Rows[0][col].ToString());
            if (col.ColumnName == "is_diabetic") chkDiabetic.Checked = Convert.ToBoolean(dtModifyEvent.Rows[0][col].ToString());
        }
        dtModifyEvent = new DataTable();
        using (SqlConnection conn = dbConnect.connectionSurgery())
        {
            String sqlCmdString = "surgGetEventProviders";
            using (SqlCommand cmd = new SqlCommand(sqlCmdString, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@surgery_event_id", Session["surg_event_id"].ToString());
                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    dtModifyEvent.Load(reader);
                    conn.Close();
                }
                catch
                { Console.WriteLine("Error"); }
            }
        }
        foreach (DataRow row in dtModifyEvent.Rows)
        {
            foreach (ListItem provider in lbProvider.Items)
            {
                if (provider.Value == row["provider_id"].ToString()) provider.Selected = true;
            }
        }
        dtModifyEvent = new DataTable();
        using (SqlConnection conn = dbConnect.connectionSurgery())
        {
            String sqlCmdString = "surgGetEventItems";
            using (SqlCommand cmd = new SqlCommand(sqlCmdString, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@surgery_event_id", Session["surg_event_id"].ToString());
                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    dtModifyEvent.Load(reader);
                    conn.Close();
                }
                catch
                { Console.WriteLine("Error"); }
            }
        }
        foreach (DataRow row in dtModifyEvent.Rows)
        {
            if (row["type"].ToString() == "A")
            {
                foreach (ListItem item in lbAnesthesia.Items)
                    if (item.Value == row["item_id"].ToString()) item.Selected = true;
            }
            if (row["type"].ToString() == "E")
                foreach (ListItem item in lbEquipment.Items)
                    if (item.Value == row["item_id"].ToString()) item.Selected = true;
            if (row["type"].ToString() == "P")
                foreach (ListItem item in lbPlatesImplants.Items)
                    if (item.Value == row["item_id"].ToString()) item.Selected = true;
        }
    }
    protected int sumDuration(String surg_event_id)
    {
        int sumDur = 0;
        String date = tbDate.Text;
        string pattern = "MM/dd/yyyy";
        DateTime parsedDate;
        DateTime.TryParseExact(date, pattern, null, System.Globalization.DateTimeStyles.None, out parsedDate);
        date = parsedDate.ToString("yyyyMMdd");
        using (SqlConnection conn = dbConnect.connectionSurgery())
        {
            String sqlCmdString = "surgGetSumDuration";
            using (SqlCommand cmd = new SqlCommand(sqlCmdString, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@location_name", ddlLocation.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@room_number", ddlRoom.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@surg_date", date);
                if (surg_event_id != null) cmd.Parameters.AddWithValue("@surgery_event_id", surg_event_id);
                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    DataTable dtSumDur = new DataTable();
                    dtSumDur.Load(reader);
                    sumDur = int.Parse(dtSumDur.Rows[0][0].ToString());
                    conn.Close();
                }
                catch
                { Console.WriteLine("Error"); }
            }
        }
        return sumDur;
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
                            (cmdString == "surgAddEventItem"))
                        {
                            cmd.Parameters.AddWithValue("@itemID", item.Value);
                            cmd.Parameters.AddWithValue("@type", type);
                        }
                        else if ((type == "provider") && (cmdString == "surgAddEventProvider")) cmd.Parameters.AddWithValue("@providerID", item.Value);
                        try
                        {
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
        if (Session["surg_event_id"] != null)
        {
            modifyEvent();
        }
        else addEvent("surgAddEvent", -1);
    }
    protected void addEvent(String insertEvent, int id)
    {
        String date = tbDate.Text;
        string pattern = "MM/dd/yyyy";
        DateTime parsedDate;
        DateTime.TryParseExact(date, pattern, null, System.Globalization.DateTimeStyles.None, out parsedDate);
        date = parsedDate.ToString("yyyyMMdd");
        Boolean blnRedirect = false;
        DataTable dtID = new DataTable();

        using (SqlConnection conn = dbConnect.connectionSurgery())
        {
            using (SqlCommand cmd = new SqlCommand(insertEvent, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (insertEvent == "surgModifyEvent")
                    cmd.Parameters.AddWithValue("@surgery_event_id", id);
                cmd.Parameters.AddWithValue("@location_name", ddlLocation.SelectedValue);
                cmd.Parameters.AddWithValue("@room_number", ddlRoom.SelectedValue);
                cmd.Parameters.AddWithValue("@surg_date", date);
                cmd.Parameters.AddWithValue("@duration", tbDuration.Text);
                cmd.Parameters.AddWithValue("@surgery_name", tbSurgery.Text);
                if (insertEvent != "surgAddBlock")
                {
                    if (tbPatient.Text == "")
                    {
                        cmd.Parameters.RemoveAt("@surgery_name");
                        if (tbSurgery.Text != "")
                            cmd.Parameters.AddWithValue("@surgery_name", "BLOCKED TIME: " + tbSurgery.Text);
                        else cmd.Parameters.AddWithValue("@surgery_name", "BLOCKED TIME");
                    }
                    cmd.Parameters.AddWithValue("@med_rec_nbr", tbPatient.Text);
                    cmd.Parameters.AddWithValue("@is_diabetic", chkDiabetic.Checked);
                    cmd.Parameters.AddWithValue("@latex_allergy", chkLatex.Checked);
                }

                if (insertEvent == "surgModifyEvent")
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    blnRedirect = true;
                }

                else try
                    {
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        dtID.Load(reader);
                        conn.Close();
                        id = Int32.Parse(dtID.Rows[0][0].ToString());
                        blnRedirect = true;
                    }
                    catch { Console.WriteLine("Error"); }
            }
        }
        addEventComponent("surgAddEventProvider", lbProvider, id, "provider");
        if (insertEvent != "surgAddBlock")
        {
            addEventComponent("surgAddEventItem", lbAnesthesia, id, "A");
            addEventComponent("surgAddEventItem", lbEquipment, id, "E");
            addEventComponent("surgAddEventItem", lbPlatesImplants, id, "P");
        }
        Session["date"] = tbDate.Text;
        Session["location"] = ddlLocation.SelectedValue;
        Session["room"] = ddlRoom.SelectedValue;
        if (blnRedirect) Response.Redirect("SurgeryRoom.aspx");
    }
    protected void modifyEvent()
    {
        int id = int.Parse(Session["surg_event_id"].ToString());
        Session["surg_event_id"] = null;
        addEvent("surgModifyEvent", id);
    }
    protected void btnBlock_Click(object sender, EventArgs e)
    {
        addEvent("surgAddBlock", -1);
    }

    protected void tbPatient_TextChanged(object sender, EventArgs e)
    {
        FindPatients(tbPatient.Text);
    }
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadORRooms();
        ddlRoom.SelectedValue = "1";
    }

    protected void deleteEvent(int id)
    {
        if (id != null)
        {
            using (SqlConnection conn = dbConnect.connectionSurgery())
            {
                String sqlCmdString = "surgDeleteEvent";
                using (SqlCommand cmd = new SqlCommand(sqlCmdString, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@surgery_event_id", id);
                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                    catch
                    { Console.WriteLine("Error"); }
                }
            }
        }
        Session["date"] = tbDate.Text;
        Session["location"] = ddlLocation.SelectedValue;
        Session["room"] = ddlRoom.SelectedValue;
        Response.Redirect("SurgeryRoom.aspx");
    }
    protected void btnDeleteItem_Click(object sender, EventArgs e)
    {
        int id = int.Parse(Session["surg_event_id"].ToString());
        deleteEvent(id);
    }
}