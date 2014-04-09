
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SurgeryRoomManagerEquipment : System.Web.UI.Page
{
    DataTable dtEquipment = new DataTable();
    surgManager surgMan = new surgManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack) LoadEquipment();
    }
    private void LoadEquipment()
    {
        surgMan.getItems("surgEquipmentGet", dtEquipment);
        DataView dvEquipmentEnabled = dtEquipment.AsDataView();
        dvEquipmentEnabled.RowFilter = "enabled = 1";
        DataView dvEquipmentDisabled = dtEquipment.AsDataView();
        dvEquipmentDisabled.RowFilter = "enabled = 0";

        lbEnabled.DataSource = dvEquipmentEnabled;
        lbEnabled.DataTextField = "name";
        lbEnabled.DataValueField = "id";
        lbEnabled.DataBind();

        lbDisabled.DataSource = dvEquipmentDisabled;
        lbDisabled.DataTextField = "name";
        lbDisabled.DataValueField = "id";
        lbDisabled.DataBind();
    }
    private void toggleItems(ListBox lb)
    {
        surgMan.toggleItems("surgEquipmentToggle", lb);
    }
    private void deleteItems(ListBox lb)
    {
        surgMan.deleteItems("surgEquipmentRemove", lb);
    }
    private void addItem(String name)
    {
        surgMan.addItem("surgEquipmentAdd", name);
    }

    protected void btnToggle_Click(object sender, EventArgs e)
    {
        toggleItems(lbEnabled);
        toggleItems(lbDisabled);
        LoadEquipment();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        deleteItems(lbEnabled);
        deleteItems(lbDisabled);
        LoadEquipment();
    }
    protected void btnAddItem_Click(object sender, EventArgs e)
    {
        string name = txtEquipment.Text;
        addItem(name);
        LoadEquipment();
    }
}