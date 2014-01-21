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
        gridViewUpdate();
    }
    protected void gridViewUpdate()
    {
        dv = new DataView(dt);
        String dateValue = tbDate.Text;
        dv.RowFilter = "Location='" + ddlLocation.SelectedValue + "' AND Room='" + ddlRoom.SelectedValue + "' AND Date='" + tbDate.Text + "'";
        dt = dv.ToTable();
        dt = calculateTimes(dt);
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
    protected DataTable calculateTimes(DataTable dt)
    {
        TimeSpan timeStub;
        TimeSpan timeStart = TimeSpan.Parse(tbTime.Text);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            int j = 0;
            if (i == 0) timeStub = timeStart;
            else
            {
                timeStub = TimeSpan.Parse(dt.Rows[i - 1][0].ToString());
                j += Convert.ToInt32(dt.Rows[i - 1][1]);
            }
            j += timeStub.Minutes + timeStub.Hours * 60;
            timeStub = TimeSpan.FromMinutes(j);
            dt.Rows[i][0] = timeStub;
        }
        return dt;
    }
    protected void login1_Authenticate(object sender, AuthenticateEventArgs e)
    {
        /*bool blnAuth = false;
        Login login1 = (Login)LoginView1.FindControl("login1");
        blnAuth = authenticate(login1.UserName, "312647835");
        if (blnAuth) FormsAuthentication.SetAuthCookie(login1.UserName, false);
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
        
    }
}