using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SurgeryRoomManagerAnesthesia : System.Web.UI.Page
{
    DataTable dtAnesthesia = new DataTable();
    surgManager surgMan = new surgManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack) LoadAnesthesia();
    }
    private void LoadAnesthesia()
    {
        surgMan.getItems("surgAnesthesiaGet", dtAnesthesia);
        DataView dvAnesthesiaEnabled = dtAnesthesia.AsDataView();
        dvAnesthesiaEnabled.RowFilter = "enabled = 1";
        DataView dvAnesthesiaDisabled = dtAnesthesia.AsDataView();
        dvAnesthesiaDisabled.RowFilter = "enabled = 0";

        lbEnabled.DataSource = dvAnesthesiaEnabled;
        lbEnabled.DataTextField = "name";
        lbEnabled.DataValueField = "id";
        lbEnabled.DataBind();

        lbDisabled.DataSource = dvAnesthesiaDisabled;
        lbDisabled.DataTextField = "name";
        lbDisabled.DataValueField = "id";
        lbDisabled.DataBind();
    }
    private void toggleItems(ListBox lb)
    {
        surgMan.toggleItems("surgAnesthesiaToggle", lb);
    }
    private void deleteItems(ListBox lb)
    {
        surgMan.deleteItems("surgAnesthesiaRemove", lb);
    }
    private void addItem(String name)
    {
        surgMan.addItem("surgAnesthesiaAdd", name);
    }

    protected void btnToggle_Click(object sender, EventArgs e)
    {
        toggleItems(lbEnabled);
        toggleItems(lbDisabled);
        LoadAnesthesia();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        deleteItems(lbEnabled);
        deleteItems(lbDisabled);
        LoadAnesthesia();
    }
    protected void btnAddItem_Click(object sender, EventArgs e)
    {
        string name = txtAnesthesia.Text;
        addItem(name);
        LoadAnesthesia();
    }
}