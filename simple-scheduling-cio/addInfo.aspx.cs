using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

public partial class addInfo : System.Web.UI.Page
{
    private static Random rand = new Random(DateTime.Now.Second);
    private static Random randNum = new Random();
    //private string startTime;

    protected void Button1_Click(object sender, EventArgs e)
    {
        int count = 1;

        if (!TextBox1.Text.Equals(""))
        {
        bool test=    int.TryParse(TextBox1.Text, out count);
        if (!test) Label1.Text = "Make Sure you Entered a Number";
        }

        for (int i = 0; i < count; i++)
        {
            insertNewAppt();
        }
        Response.Redirect("Default.aspx");

    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }


    private void insertNewAppt()
    {

        //creates a random GUID
        Guid g = Guid.NewGuid();
        //creates random duration amount
        int duration = RandDur();
        //creates random hour 
        int tempHr = RandHour();
        //creates random min
        int tempMin = RandMin();

        //changes the time to correct format according to the database
        string hr, min;
        if (tempHr < 10) hr = "0" + tempHr.ToString();
        else hr = tempHr.ToString();
        if (tempMin < 10) min = "0" + tempMin.ToString();
        else min = tempMin.ToString();
        string startTime = hr + "" + min;




        int tempEndHr;
        int tempEndMin;
        int temp;

        temp = 60 - tempMin;
        if (temp <= duration)
        {
            tempEndHr = tempHr + 1;
            temp = duration - temp;

            tempEndMin = temp;
        }
        else
        {
            tempEndHr = tempHr;
            tempEndMin = tempMin + duration;
        }

        if (tempEndHr < 10) hr = "0" + tempEndHr.ToString();
        else hr = tempEndHr.ToString();
        if (tempEndMin < 10) min = "0" + tempEndMin.ToString();
        else min = tempEndMin.ToString();
        string endTime = hr + "" + min;

        // sTime_lb.Text = (startTime);
        //  dur_lb.Text = duration.ToString();
        //  eTime_lb.Text = endTime;
        // Label1.Text = RandDoctor();

        SqlConnection conn = dbConnect.connection();
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText =


        "insert into dbo.appointments" +
            "(description,appt_date,begintime,endtime,duration,details,workflow_status,rendering_provider_id," +
            "pm_ind,practice_id,appt_id,proc_as_precept_ind,confirm_ind,appt_kept_ind,cancel_ind,retain_ind," +
            "prompt_retain_ind,resched_ind,delete_ind,location_id,appt_type,appt_link_ind,first_app_ind," +
            "last_app_ind,create_timestamp,created_by,modify_timestamp,modified_by,appt_nbr,site_id,intrf_no_show_ind)" +
        "values" +
            "('" + RandName() + "','" + RandYear() + "','" + startTime + "','" + endTime + "','" + duration + "','Back Pain','KEPT','" + RandDoctor() + "','N'," +
            "'0001','" + g + "','N','N','N','N','N','N','N','N','9D971E61-2B5A-4504-9016-7FD863790EE2'," +
            "'U','N','N','N',GETDATE(),4,GETDATE(),4,294409,000,'N');";



        cmd.Connection = conn;
        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();
       
         
    }

    private string RandName()
    {
        string[] maleNames = new string[17] { "Aaron", "Jacob", "Ethan", "Emma", "Emily", "Chloe", "Mary", "Bob", "Mathew", "David", "Susan", "Josh", "Dan", "James", "John", "Trever", "Tim" };
        string[] lastNames = new string[17] { "George", "Fry", "Jones", "Smith", "Brown" , "Wilson", "Moore", "White", "Harris", "Clark","Lewis","Carter","Phillips","Evans","Morris","Cook","Rogers"};
        string FirstName = maleNames[rand.Next(0, maleNames.Length - 1)];
        string LastName = lastNames[rand.Next(0, lastNames.Length - 1)];
        string name = LastName + ", " + FirstName;
        return name;
    }
    private string RandYear()
    {
        string date;
        string tempMonth;
        string tempDay;
        int year = randNum.Next(2000, 2015);
        int month = randNum.Next(1, 13);
        int day = randNum.Next(1, 31);
        if (month < 10) tempMonth = "0" + month.ToString();
        else tempMonth = month.ToString();
        if (day < 10) tempDay = "0" + day.ToString();
        else tempDay = day.ToString();
         date = year.ToString()+""+tempMonth+""+tempDay;
        return date;
    }
    private string RandTime()
    {
        string tempHour;
        string tempMin;
        int hour = randNum.Next(24);
        int min = randNum.Next(60);
        if(hour<10)tempHour = "0"+hour.ToString();
        else tempHour = hour.ToString();
        if(min<10)tempMin = "0"+min.ToString();
        else tempMin = min.ToString();
        string time = tempHour +""+ tempMin;

        return time;
    }
    private int RandHour()
    {
        return randNum.Next(23);
    }
    private int RandMin()
    {
        return randNum.Next(60);
    }
    private int RandDur()
    {
        int duration = randNum.Next(30,60);
        return duration;
    }
    private string RandDoctor()
    {
        DataTable dt = new DataTable();
        string Doctor="";
        ArrayList Doctors = new ArrayList();
        SqlConnection conn = dbConnect.connection();

        try
        {
            string cmd = "select provider_id from dbo.provider_mstr;";
            conn.Open();
            SqlCommand sqlCMD = new SqlCommand(cmd, conn);
            SqlDataReader reader = sqlCMD.ExecuteReader();
            dt.Load(reader);
        }
        catch { Console.Write("Error"); }
        int doctorCount = dt.Rows.Count;
        for (int i=0;i<doctorCount; i++){
            Doctors.Add(dt.Rows[i][0]);
        }
         Doctor = Doctors[rand.Next(0,( Doctors.Count - 1))].ToString();

        return Doctor;
    }

}