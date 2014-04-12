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
public partial class _Default : System.Web.UI.Page
{
    DataTable dt = basicInfoSurgery.dt();
    DataView dv;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (tbTime.Text == "") tbTime.Text = "07:00:00";
        if (Session["date"] != null)
        {
            tbDate.Text = Session["date"].ToString();
            Session["date"] = null;
        } if (tbDate.Text == "") tbDate.Text = DateTime.Today.ToString("yyyy/MM/dd");
        listViewUpdate();
        checkUser();
    }
    protected void listViewUpdate()
    {
        dv = new DataView(dt);
        dv.RowFilter = "Location='" + ddlLocation.SelectedValue + "' AND Room='" + ddlRoom.SelectedValue + "' AND Date='" + tbDate.Text + "'";
        dt = dv.ToTable();
        dt = calculateTimes(dt);
        ListView1.DataSource = dt;
        ListView1.DataBind();
    }
    protected DataTable calculateTimes(DataTable dt)
    {
        DateTime timeStub;
        DateTime timeStart = DateTime.Parse(tbTime.Text);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            Double j = 0;
            if (i == 0) timeStub = timeStart;
            else timeStub = DateTime.Parse(dt.Rows[i - 1][2].ToString());
            dt.Rows[i][1] = timeStub.ToShortTimeString();
            j = Double.Parse(dt.Rows[i][3].ToString());
            dt.Rows[i][2] = timeStub.AddMinutes(j).ToShortTimeString();
        }
        return dt;
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        String redirect = "";
        redirect += "SurgeryRoomAdd.aspx?";
        redirect += "loc=" + ddlLocation.SelectedValue;
        redirect += "&rm=" + ddlRoom.SelectedValue;
        redirect += "&date=" + tbDate.Text;
        Response.Redirect(redirect);
    }
    protected void checkUser(){
        
        if(Session["loggedIN"]!="true"){
            btnAdd.Visible = false;
            tbTime.Enabled = false;
            signIN.Visible = true;
            signOUT.Visible = false;
        }
        else
        {
            btnAdd.Visible = true;
            tbTime.Enabled = true;
            signIN.Visible = false;
            signOUT.Visible = true;
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
        checkUser();
    }
    protected void finalVal_TextChanged(object sender, EventArgs e)
    {
        int begin = int.Parse(beginVal.Value);
        int end = int.Parse(finalVal.Value);
        string date = Regex.Replace(tbDate.Text, @"[^\d]", "");
        string ID = dt.Rows[int.Parse(beginVal.Value) - 1][14].ToString();
        string countCol = dt.Columns.Count.ToString();
        List<List<string>> numbers = new List<List<string>>();
        if (begin < end)
        {
            for (int i = begin; i < end; i++)
            {
                if (i != begin - 1)
                {
                    List<string> row = new List<string>();
                    row.Add(dt.Rows[i][0].ToString());
                    row.Add(dt.Rows[i][14].ToString());
                    numbers.Add(row);
                }
            }
            for (int i = 0; i < numbers.Count; i++)
            {
                sqlDataTableSurgery.updateOrder(int.Parse(numbers[i][0]), int.Parse(numbers[i][0]) - 1, date, int.Parse(numbers[i][1]));
            }
        }
        if (begin > end)
        {
            for (int i = end - 1; i < begin; i++)
            {
                if (i != begin - 1)
                {
                    List<string> row = new List<string>();
                    row.Add(dt.Rows[i][0].ToString());
                    row.Add(dt.Rows[i][14].ToString());
                    numbers.Add(row);
                }
            }
            for (int i = 0; i < numbers.Count; i++)
            {
                sqlDataTableSurgery.updateOrder(int.Parse(numbers[i][0]), int.Parse(numbers[i][0]) + 1, date, int.Parse(numbers[i][1]));
            }
        }
        sqlDataTableSurgery.updateOrder(begin, end, date, int.Parse(ID));
        Session["date"] = tbDate.Text;
        Response.Redirect("SurgeryRoom.aspx");
    }
    protected void Print_Click(object sender, EventArgs e)
    {
        Session["date"] = tbDate.Text;
        Session["location"] = ddlLocation.SelectedValue;
        Response.Redirect("PrintPage.aspx");
    }
}