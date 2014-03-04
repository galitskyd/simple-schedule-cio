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

public partial class _Default : System.Web.UI.Page
{
    DataTable dt = basicInfoSurgery.dt();
    DataView dv;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (tbDate.Text == "") tbDate.Text = DateTime.Today.ToString("yyyy/MM/dd");
        if (tbTime.Text == "") tbTime.Text = "07:00:00";
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
    protected void login1_Authenticate(object sender, AuthenticateEventArgs e)
    {
        /*bool blnAuth = false;
        Login login1 = (Login)LoginView1.FindControl("login1");
        blnAuth = authenticate(login1.UserName, "312647835");
        if (blnAuth) FormsAuthentication.SetAuthCookie(login1.UserName, true);
        e.Authenticated = blnAuth;*/
    }
    bool authenticate(string UserName, string Password)
    {
        bool boolReturnValue = false;
        /*SqlConnection conn = dbConnect.connection();
        String strSQL = "Select * From surgery_room_identification";
        SqlCommand command = new SqlCommand(strSQL, conn);
        SqlDataReader dataReader;
        conn.Open();
        dataReader = command.ExecuteReader();
        while (dataReader.Read())
        {
            if ((UserName == dataReader["surgery_room_user"].ToString()) & (Password == dataReader["surgery_room_password"].ToString()))
            {
                boolReturnValue = true;
            }
            dataReader.Close();
            return boolReturnValue;
        }*/
        return boolReturnValue;
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
}