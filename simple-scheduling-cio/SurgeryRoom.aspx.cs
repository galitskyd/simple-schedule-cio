﻿using System;
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
using System.Web.UI.HtmlControls;

public partial class _Default : System.Web.UI.Page
{
    DataTable dt = basicInfoSurgery.dt();
    DataTable dtBind = new DataTable();
    DataTable dtORRooms = new DataTable();
    DataView dv;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack) LoadORRooms();
        if (tbTime.Text == "") tbTime.Text = "06:30:00";
        if (Session["date"] != null)
        {
            tbDate.Text = Session["date"].ToString();
            Session["date"] = null;
        }
        if (Session["location"] != null)
        {
            ddlLocation.SelectedValue = Session["location"].ToString();
            Session["location"] = null;
        }
        if (Session["room"] != null)
        {
            ddlRoom.SelectedValue = Session["room"].ToString();
            Session["room"] = null;
        }
        else if (tbDate.Text == "") tbDate.Text = DateTime.Today.ToString("MM/dd/yyyy");
        Session["surg_event_id"] = null;
        listViewUpdate();
        checkUser();
    }
    protected void listViewUpdate()
    {
        dv = new DataView(dt);
        dv.RowFilter = "Location='" + ddlLocation.SelectedValue + "' AND Room='" + ddlRoom.SelectedItem + "' AND Date='" + tbDate.Text + "'";
        dtBind = dv.ToTable();
        dtBind = calculateTimes(dtBind);
        ListView1.DataSource = dtBind;
        ListView1.DataBind();
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
    protected DataTable calculateTimes(DataTable dt)
    {
        DateTime timeStub;
        DateTime timeStart = DateTime.Parse(tbTime.Text);
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
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Session["date"] = tbDate.Text;
        Session["location"] = ddlLocation.SelectedValue;
        Session["room"] = ddlRoom.SelectedValue;
        Response.Redirect("SurgeryRoomAdd.aspx");
    }
    protected void checkUser()
    {
        HtmlGenericControl javascript = new HtmlGenericControl("script");
        javascript.Attributes["type"] = "text/javascript";
        javascript.Attributes["src"] = "Scripts/dragDrop.js";

        if (Session["loggedIN"] != "true")
        {
            btnAdd.Visible = false;
            tbTime.Enabled = false;
            signIN.Visible = true;
            signOUT.Visible = false;
            aManager.Visible = false;
            aManager.Enabled = false;
            eManager.Visible = false;
            eManager.Enabled = false;
            pManager.Visible = false;
            pManager.Enabled = false;
            Page.Header.Controls.Remove(javascript);
            foreach (ListViewItem item in ListView1.Items)
            {
                Button btnDelete = item.FindControl("btnDeleteItem") as Button;
                if (btnDelete != null)
                {
                    btnDelete.Visible = false;
                    btnDelete.Enabled = false;
                }
                Button btnModify = item.FindControl("btnModifyItem") as Button;
                if (btnModify != null)
                {
                    btnModify.Visible = false;
                    btnModify.Enabled = false;
                }
            }
        }
        else
        {
            btnAdd.Visible = true;
            tbTime.Enabled = true;
            signIN.Visible = false;
            signOUT.Visible = true;
            aManager.Visible = true;
            aManager.Enabled = true;
            eManager.Visible = true;
            eManager.Enabled = true;
            pManager.Visible = true;
            pManager.Enabled = true;
            Page.Header.Controls.Add(javascript);
            foreach (ListViewItem item in ListView1.Items)
            {
                Button btnDelete = item.FindControl("btnDeleteItem") as Button;
                if (btnDelete != null)
                {
                    btnDelete.Visible = true;
                    btnDelete.Enabled = true;
                }
                Button btnModify = item.FindControl("btnModifyItem") as Button;
                if (btnModify != null)
                {
                    btnModify.Visible = true;
                    btnModify.Enabled = true;
                }
            }
        }
    }
    protected void loginUser_Click(object sender, EventArgs e)
    {
        string username = user.Text;
        string password = pass.Text;

        DataTable dt = sqlDataTableSurgery.AuthenticateUser();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (username == dt.Rows[i][0].ToString() && password == dt.Rows[i][1].ToString())
            {
                Session["loggedIN"] = "true";
                checkUser();
            }
        }
        user.Text = "";
        pass.Text = "";
    }
    protected void signOUT_Click(object sender, EventArgs e)
    {
        Session["loggedIN"] = "false";
        HtmlGenericControl javascript = new HtmlGenericControl("script");
        javascript.Attributes["type"] = "text/javascript";
        javascript.Attributes["src"] = "Scripts/dragDrop.js";
        Page.Header.Controls.Remove(javascript);
        checkUser();
        Session["date"] = tbDate.Text;
        Response.Redirect("SurgeryRoom.aspx");
    }
    protected void finalVal_TextChanged(object sender, EventArgs e)
    {
        int begin = int.Parse(beginVal.Value);
        int end = int.Parse(finalVal.Value);
        string date = Regex.Replace(tbDate.Text, @"[^\d]", "");
        string ID = dtBind.Rows[int.Parse(beginVal.Value) - 1]["ID"].ToString();
        string countCol = dtBind.Columns.Count.ToString();
        List<List<string>> numbers = new List<List<string>>();
        if (begin < end)
        {
            for (int i = begin; i < end; i++)
            {
                if (i != begin - 1)
                {
                    List<string> row = new List<string>();
                    row.Add(dtBind.Rows[i]["Position"].ToString());
                    row.Add(dtBind.Rows[i]["ID"].ToString());
                    numbers.Add(row);
                }
            }
            for (int i = 0; i < numbers.Count; i++)
            {
                sqlDataTableSurgery.updateOrder(int.Parse(numbers[i][0]) - 1, int.Parse(numbers[i][1]));
            }
        }
        if (begin > end)
        {
            for (int i = end - 1; i < begin; i++)
            {
                if (i != begin - 1)
                {
                    List<string> row = new List<string>();
                    row.Add(dtBind.Rows[i]["Position"].ToString());
                    row.Add(dtBind.Rows[i]["ID"].ToString());
                    numbers.Add(row);
                }
            }
            for (int i = 0; i < numbers.Count; i++)
            {
                sqlDataTableSurgery.updateOrder(int.Parse(numbers[i][0]) + 1, int.Parse(numbers[i][1]));
            }
        }
        sqlDataTableSurgery.updateOrder(end, int.Parse(ID));
        Session["date"] = tbDate.Text;
        Session["location"] = ddlLocation.SelectedValue;
        Session["room"] = ddlRoom.SelectedValue;
        Response.Redirect("SurgeryRoom.aspx");
    }

    protected void Print_Click(object sender, EventArgs e)
    {
        Session["date"] = tbDate.Text;
        Session["location"] = ddlLocation.SelectedValue;
        Response.Redirect("PrintPage.aspx");
    }

    protected void btnAnesthesia_Click(object sender, EventArgs e)
    {
        Response.Redirect("SurgeryRoomManagerAnesthesia.aspx");
    }

    protected void btnEquipment_Click(object sender, EventArgs e)
    {
        Response.Redirect("SurgeryRoomManagerEquipment.aspx");
    }

    protected void btnPlatesAndImplants_Click(object sender, EventArgs e)
    {
        Response.Redirect("SurgeryRoomManagerPlatesImplants.aspx");
    }

    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadORRooms();
        listViewUpdate();
        checkUser();
    }
    protected void modifyEvent(int id)
    {
        Session["surg_event_id"] = id;
        Session["date"] = tbDate.Text;
        Session["location"] = ddlLocation.SelectedValue;
        Session["room"] = ddlRoom.SelectedValue;
        Response.Redirect("SurgeryRoomAdd.aspx");
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
    protected void ListView1_ItemCommand(object sender, CommandEventArgs e)
    {
        if (e.CommandName == "ModifyEvent")
        {
            int id = int.Parse(e.CommandArgument.ToString());
            modifyEvent(id);
        }
    }

    public String checkDiabetes(string item)
    {
        if (item == "1")
        {
            return "<img src='Content/images/diabetes-icon-small.png' alt='Diabetic' width='25' />";
        }
        else
        {
            return "";
        }
    }

    public String checkLatex(string item)
    {
        if (item == "1")
        {
            return "<img src='Content/images/latex-icon-small.png'  alt='Latex Allergy' width='25' />";
        }
        else
        {
            return "";
        }
    }

    public String checkCoaguCheck(string item)
    {
        if (item == "1")
        {
            return "<img src='Content/images/coagucheck-small.png' alt='CoaguCheck' width='25' />";
        }
        else
        {
            return "";
        }
    }

    public String checkVancomycin(string item)
    {
        if (item == "1")
        {
            return "<img src='Content/images/vancomycin-icon-small.png' alt='Vanco preop' width='25' />";
        }
        else
        {
            return "";
        }
    }
}