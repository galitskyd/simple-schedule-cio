using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SurgeryRoomManagerPlatesImplants : System.Web.UI.Page
{
    DataTable dtPlatesImplants = new DataTable();
    surgManager surgMan = new surgManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack) LoadPlatesImplants();
    }
    private void LoadPlatesImplants()
    {
        using (SqlConnection conn = dbConnectSurgery.connection())
        {
            try
            {
                String sqlCmdString = "surgPlatesImplantsGet";
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlCmdString, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                dtPlatesImplants.Load(reader);
                conn.Close();
            }
            catch { Console.WriteLine("Error"); }
        }
        DataView dvPlatesImplantsEnabled = dtPlatesImplants.AsDataView();
        dvPlatesImplantsEnabled.RowFilter = "enabled = 1";
        DataView dvPlatesImplantsDisabled = dtPlatesImplants.AsDataView();
        dvPlatesImplantsDisabled.RowFilter = "enabled = 0";

        lbEnabled.DataSource = dvPlatesImplantsEnabled;
        lbEnabled.DataTextField = "name";
        lbEnabled.DataValueField = "id";
        lbEnabled.DataBind();

        lbDisabled.DataSource = dvPlatesImplantsDisabled;
        lbDisabled.DataTextField = "name";
        lbDisabled.DataValueField = "id";
        lbDisabled.DataBind();
    }
    private void toggleItems(ListBox lb)
    {
        surgMan.toggleItems("surgPlatesImplantsToggle", lb);
    }
    private void deleteItems(ListBox lb)
    {
        surgMan.deleteItems("surgPlatesImplantsRemove", lb);
    }
    private void addItem(String name)
    {
        surgMan.addItem("surgPlatesImplantsAdd", name);
    }

    protected void btnToggle_Click(object sender, EventArgs e)
    {
        toggleItems(lbEnabled);
        toggleItems(lbDisabled);
        LoadPlatesImplants();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        deleteItems(lbEnabled);
        deleteItems(lbDisabled);
        LoadPlatesImplants();
    }
    protected void btnAddItem_Click(object sender, EventArgs e)
    {
        string name = txtPlatesImplants.Text;
        addItem(name);
        LoadPlatesImplants();
    }
}