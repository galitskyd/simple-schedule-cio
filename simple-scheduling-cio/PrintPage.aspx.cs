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
    Dictionary<String,DataTable> dtList = new Dictionary<String,DataTable>();
    DataTable dt = basicInfoSurgery.dt();
    DataView dv;
    String location;
    protected String date;
    protected String room = "Testing";
    DataTable dtORRooms = new DataTable();
    protected DateTime timestamp = new DateTime();
    int i = 0;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        location = Session["location"].ToString();
        date = Session["date"].ToString();
        System.Diagnostics.Debug.Write(date);
        populateTimestamp();
        calculateTimes(dt);
        LoadORRooms();
        System.Diagnostics.Debug.WriteLine(dtORRooms.Rows.Count);
        listViewUpdate();
        ListViewPrime.DataSource = dtList;
        ListViewPrime.DataBind();
    }

    private void LoadORRooms()
    {
        using (SqlConnection conn = dbConnect.connectionSurgery())
        {
            String sqlCmdString = "surgGetORRooms";
            using (SqlCommand cmd = new SqlCommand(sqlCmdString, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@location_name", location);
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
    }
    protected DataTable calculateTimes(DataTable dt)
    {
        DateTime timeStub;
        DateTime timeStart = DateTime.Parse("06:30");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            Double j = 0;
            if (i == 0) timeStub = timeStart;
            else timeStub = DateTime.Parse(dt.Rows[i - 1]["End Time"].ToString());
            dt.Rows[i]["Start Time"] = timeStub.ToShortTimeString();
            j = Double.Parse(dt.Rows[i]["Duration"].ToString());
            dt.Rows[i]["End Time"] = timeStub.AddMinutes(j).ToShortTimeString();
        }
        return dt;
    }
    protected void populateTimestamp()
    {
        timestamp = DateTime.Now;
    }
    protected void listViewUpdate()
    {
        foreach (DataRow row in dtORRooms.Rows)
        {
            dv = dt.AsDataView();
            dv.RowFilter = "Location = '" + location + "' AND Room = '" + row["room_name"] + "' AND Date = '" + date + "'";
            if (dv.ToTable().Rows.Count > 0)
                dtList.Add(row["room_name"].ToString(),dv.ToTable());
        }
    }
    protected void ListViewPrime_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        var ListView1 = (ListView)e.Item.FindControl("ListView1");
        ListView1.DataSource = dtList.ElementAt(i).Value;
        i++;
        ListView1.DataBind();
    }
}