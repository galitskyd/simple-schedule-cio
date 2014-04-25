using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using System.Net;
using System.Collections.Specialized;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
public partial class _Default : System.Web.UI.Page
{
    DataView dv;
    static Boolean asc = true;
    static string sort = "Asc";
    static string filters="?";
    static string filterSelections = null;
    static string url = HttpContext.Current.Request.Url.AbsolutePath;
    static int activeTabIndex;
    HttpCookie myCookie = new HttpCookie("activeTab");
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Uri uri = new Uri(HttpContext.Current.Request.Url.AbsoluteUri);
        var parameters = HttpUtility.ParseQueryString(uri.Query);
        string fullPathURL = HttpContext.Current.Request.Url.Query;
        
        if (fullPathURL == "")
        {
                string todayDate = DateTime.Today.ToString("MM/dd/yyyy");
                changeDate(todayDate, todayDate);
        }
        if(!IsPostBack)
        {
             startDateTxtBx.Text = parameters["startDate"];
             EndDate.Text = parameters["endDate"];
        }
        myCookie = Request.Cookies["activeTab"];
        if (myCookie != null) TabContainer1.ActiveTabIndex = Convert.ToInt32(myCookie.Value);
        GridView1.DataSource = basicInfo.dt(parameters["startDate"], parameters["endDate"]);
            GridView1.DataBind();
            //System.Console.Write(basicInfo.dt());
            filter();
    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (asc) { sort = "ASC"; asc = false; }
        else { sort = "DESC"; asc = true; }
        DataTable dataTable = GridView1.DataSource as DataTable;
        DataView dataView = new DataView(dataTable);
        dataView.Sort = e.SortExpression + " " + sort;
        GridView1.DataSource = dataView;
        GridView1.DataBind();
    }
    protected string filterSelection(string filter,string col, string[] val,int type)
    {
        if (val.Length > 0)
        {
            for (int i = 0; i < val.Length; i++)
            {
                if (type == 0 && i == 0) filter += col + " LIKE '%" + val[i] + "%'";
                if (val.Length > 1)
                {
                    if (i == 0) filter += " AND " + col + " LIKE '%" + val[i] + "%'";
                    else filter += " OR " + col + " LIKE '%" + val[i] + "%'";
                }
                else filter += " AND " + col + " LIKE '%" + val[i] + "%'";
            }
        }
        return filter;
    }
    protected void filter()
    {
        string filter = "";
        int type=0;
        string[] providers=null;
        string[] patient = null;
        string[] location = null;
        string[] startDate = null;
        string[] endDate = null;
        string[] details = null;
        string[] status = null;
        string[] birthDate = null;
        string[] medRec = null;
        Uri uri = new Uri(HttpContext.Current.Request.Url.AbsoluteUri);
        var parameters = HttpUtility.ParseQueryString(uri.Query);
        if (parameters["Provider"] != null) providers = Regex.Split(parameters["Provider"], ",");
        if (parameters["Patient"] != null) patient = Regex.Split(parameters["Patient"], ",");
        if (parameters["Location"] != null) location = Regex.Split(parameters["Location"], ",");
        if (parameters["startDate"] != null) startDate = Regex.Split(parameters["startDate"], ",");
        if (parameters["endDate"] != null) endDate = Regex.Split(parameters["endDate"], ",");
        if (parameters["Details"] != null) details = Regex.Split(parameters["Details"], ",");
        if (parameters["Status"] != null) status = Regex.Split(parameters["Status"], ",");
        if (parameters["birthDate"] != null) birthDate = Regex.Split(parameters["birthDate"], ",");
        if (parameters["medRecNum"] != null) medRec = Regex.Split(parameters["medRecNum"], ",");

        if (providers != null) filter = filterSelection(filter, "Doctor", providers, type);
        if (patient != null)
        {
            if (providers == null) type = 0; else type = 1;
            filter = filterSelection(filter, "Patient", patient, type);
        }
        if (birthDate != null)
        {
            if (providers == null && patient==null) type = 0; else type = 1;
            filter = filterSelection(filter, "[Birth Date]", birthDate, type);
        }
        if (medRec != null)
        {
            if (providers == null && patient == null && birthDate==null) type = 0; else type = 1;
            filter = filterSelection(filter, "[Med Rec Number]", medRec, type);
        }
        if (location != null)
        {
            if (providers == null && patient == null && birthDate == null && medRec==null) type = 0; else type = 1;
            filter = filterSelection(filter, "Location", location, type);
        }
        if (startDate != null)
        {
            DateTime tempDate = DateTime.Today;
            if (providers == null && patient == null && location == null && birthDate == null && medRec == null) type = 0; else type = 1;
            try { tempDate = Convert.ToDateTime(parameters["startDate"]); }
            catch { errorOut(EndDate, errorApptDate, "Not Valid Date"); }
            try
            {
                tempDate = Convert.ToDateTime(parameters["endDate"]);
                tempDate = tempDate.AddDays(1);
            }
            catch {errorOut(EndDate, errorApptDate, "Not Valid Date"); }
            
            
            if (type == 0) filter = "( [Appt Date] >= #" + parameters["startDate"] + "# And [Appt Date] <= #" + tempDate.ToString("MM/dd/yyyy") + "# )";
            else filter += " AND  ( [Appt Date] >= #" + parameters["startDate"] + "# And [Appt Date] <= #" + tempDate.ToString("MM/dd/yyyy") + "# )";
        }
        
        if (details != null)
        {
            if (providers == null && patient == null && location == null && startDate == null && birthDate == null && medRec == null) type = 0; else type = 1;
            filter = filterSelection(filter, "Details", details, type);
        }
        if (status != null)
        {
            if (providers == null && patient == null && location == null && startDate == null && details == null && birthDate == null && medRec == null) type = 0; else type = 1;
            filter = filterSelection(filter, "Status", status, type);
        }
        DateTime tempDate2=DateTime.Today;
        string bla= null;
        try
        {
             tempDate2 = Convert.ToDateTime(parameters["endDate"]);
            bla = tempDate2.AddDays(1).ToString();
        }
        catch { errorOut(EndDate, errorApptDate, "Not Valid Date"); }
        dv = basicInfo.dt(parameters["startDate"], parameters["endDate"]).DefaultView;
        dv.RowFilter = filter;
        DataTable dt = new DataTable();
        dt = dv.ToTable();
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
    protected void rememberActiveTab(string number)
    {
        HttpCookie myCookie = new HttpCookie("activeTab");
        myCookie.Value = number;
        TabContainer1.ActiveTabIndex = Convert.ToInt32(number);
        Response.Cookies.Add(myCookie);
    }
    protected void errorOut(TextBox field,Label error,string msg)
    {
        field.Style.Add("Border-color", "RED");
        field.Style.Add("Border-Weight", "3px");
        error.Style.Add("Color", "RED");
        error.Text = msg;
    }
    protected void providerNameBTN_Click(object sender, EventArgs e)
    {
        if (providerName.Text != "")
        {
            filters = filters + "Provider=" + providerName.Text + "&";
            rememberActiveTab("0");
            Response.Redirect(url + filters);
        }
        else
        {
            rememberActiveTab("0");
            errorOut(providerName,errorProvider,"*Please Insert A Doctor's Name");
        }
    }
    protected void patientNameBTN_Click(object sender, EventArgs e)
    {  
        if (patientName.Text != "")
        {
            filters = filters + "Patient=" + patientName.Text + "&";
            rememberActiveTab("1");
            Response.Redirect(url + filters);
        }
        else
        {
            rememberActiveTab("1");
            errorOut(patientName, errorPatient, "*Please Insert A Patient's Name");
        }
    }
    protected void locationNameBTN_Click(object sender, EventArgs e)
    {
        if (locationName.Text != "")
        {
        filters = filters + "Location=" + locationName.Text + "&";
        rememberActiveTab("4");
        Response.Redirect(url + filters);
        }
        else
        {
            rememberActiveTab("4");
            errorOut(locationName, errorLocation, "*Please Insert A Location");
        }
    }
    void emptyVal()
    {
        DateTime sT = DateTime.Today;
        DateTime eT = DateTime.Today;

        try { sT = Convert.ToDateTime(startDateTxtBx); }
        catch {errorOut(EndDate, errorApptDate, "Not Valid Date");}
        
        try{ eT = Convert.ToDateTime(EndDate.Text); }
        catch{ errorOut(EndDate, errorApptDate, "Not Valid Date"); }

        if (startDateTxtBx.Text == "") startDateTxtBx.Text = DateTime.Today.ToString("MM/dd/yyyy");
        if (EndDate.Text == "" || eT < sT) EndDate.Text = startDateTxtBx.Text;
            changeDate(startDateTxtBx.Text, EndDate.Text);
    }
    void changeDate(string start, string end)
    {
        Uri uri = new Uri(HttpContext.Current.Request.Url.AbsoluteUri);
        var parameters = HttpUtility.ParseQueryString(uri.Query);
        parameters.Remove("startDate");
        parameters.Remove("endDate");
        filters = "?" + parameters.ToString();
        filters = filters + "startDate=" + start + "&endDate=" + end + "&";
        Response.Redirect(url + filters);
    }
    public bool testDate(string date)
    {
        try
        {
            DateTime temp = Convert.ToDateTime(date);
            return true;
        }
        catch
        {
            return false;
        }
    }
    protected void apptDateBTN_Click(object sender, EventArgs e)
    {
        if (testDate(startDateTxtBx.Text) && testDate(EndDate.Text))
        {
            DateTime sT = DateTime.Today;
            DateTime eT = DateTime.Today;

            try { sT = Convert.ToDateTime(startDateTxtBx); }
            catch { errorOut(EndDate, errorApptDate, "Not Valid Date"); }

            try { eT = Convert.ToDateTime(EndDate.Text); }
            catch { errorOut(EndDate, errorApptDate, "Not Valid Date"); }
            rememberActiveTab("5");
            if (startDateTxtBx.Text != "" && EndDate.Text != "") changeDate(startDateTxtBx.Text, EndDate.Text);
            else emptyVal();
        }
        if (testDate(startDateTxtBx.Text) == false) errorOut(startDateTxtBx, errorApptDate, "Not Valid Date");
        if (testDate(EndDate.Text) == false) errorOut(EndDate, errorApptDate, "Not Valid Date");
    }
   
    protected void detailsBTN_Click(object sender, EventArgs e)
    {
        if (details.Text != "")
        {
            filters = filters + "Details=" + details.Text + "&";
            rememberActiveTab("6");
            Response.Redirect(url + filters);
        }
        else
        {
            rememberActiveTab("6");
            errorOut(details, errorDetails, "*Please Insert Details");
        }
    }
    protected void statusBTN_Click(object sender, EventArgs e)
    {
        if (status.Text != "")
        {
            filters = filters + "Status=" + status.Text + "&";
            rememberActiveTab("7");
            Response.Redirect(url + filters);
        }
        else
        {
            rememberActiveTab("7");
            errorOut(status, errorStatus, "*Please Insert A Status");
        }
    }
    protected void birthDateBTN_Click(object sender, EventArgs e)
    {
        if (birthDateName.Text != "")
        {
            filters = filters + "birthDate=" + birthDateName.Text + "&";
            rememberActiveTab("2");
            Response.Redirect(url + filters);
        }
        else
        {
            rememberActiveTab("2");
            errorOut(status, errorStatus, "*Please Insert A Birth Date");
        }
    }

    protected void medRecNumBTN_Click(object sender, EventArgs e)
    {
        if (medRecNum.Text != "" )
        {
            filters = filters + "medRecNum=" + medRecNum.Text + "&";
            rememberActiveTab("3");
            Response.Redirect(url + filters);
        }
        else
        {
            rememberActiveTab("2");
            errorOut(status, errorStatus, "*Please Insert A Med Rec Number");
        }
    }
    protected void clearFilter(string filterName,string tabNumber)
    {
        Uri uri = new Uri(HttpContext.Current.Request.Url.AbsoluteUri);
        var parameters = HttpUtility.ParseQueryString(uri.Query);
        parameters.Remove(filterName);
        filters = "?" + parameters.ToString();
        rememberActiveTab(tabNumber);
        Response.Redirect("~/Default.aspx?" + parameters.ToString());
    }
    protected void providerNameClearBTN_Click(object sender, EventArgs e)
    {
        clearFilter("Provider", "0");
    }
    protected void patientNameClearBTN_Click(object sender, EventArgs e)
    {
        clearFilter("Patient", "1");
    }
    protected void locationNameClearBTN_Click(object sender, EventArgs e)
    {
        clearFilter("Location", "4");
    }
    protected void apptDateClearBTN_Click(object sender, EventArgs e)
    {
        string now = DateTime.Today.ToString("MM/dd/yyyy");
        Uri uri = new Uri(HttpContext.Current.Request.Url.AbsoluteUri);
        var parameters = HttpUtility.ParseQueryString(uri.Query);
        parameters.Remove("startDate");
        parameters.Remove("endDate");
        parameters.Add("startDate",now);
        parameters.Add("endDate", now);
        filters = "?" + parameters.ToString();
        rememberActiveTab("5");
        Response.Redirect("~/Default.aspx?" + parameters.ToString());
    }
  
    protected void detailsClearBTN_Click(object sender, EventArgs e)
    {
        clearFilter("Details", "6");
    }
    protected void statusClearBTN_Click(object sender, EventArgs e)
    {
        clearFilter("Status", "7");
    }
    protected void clearAll_Click(object sender, EventArgs e)
    {
        filters = "?";
        Session["startDate"] = "";
        Response.Redirect("~/Default.aspx");
    }
    protected void medRecNumClearBTN_Click(object sender, EventArgs e)
    {
        clearFilter("medRecNum", "3");
    }
    protected void birthDateClearBTN_Click(object sender, EventArgs e)
    {
        clearFilter("birthDate", "2");
    }



    protected void startDateTxtBx_TextChanged(object sender, EventArgs e)
    {
        rememberActiveTab("5");
         if (testDate(startDateTxtBx.Text) )
        {
            DateTime sT = DateTime.Today;  
            try { sT = Convert.ToDateTime(startDateTxtBx); }
            catch { errorOut(EndDate, errorApptDate, "Not Valid Date"); } 
            if (startDateTxtBx.Text != "" && EndDate.Text != "") changeDate(startDateTxtBx.Text, startDateTxtBx.Text);
            else emptyVal();
        }
        if (testDate(startDateTxtBx.Text) == false) errorOut(startDateTxtBx, errorApptDate, "Not Valid Date"); 
    }
    protected void EndDate_TextChanged(object sender, EventArgs e)
    {
        rememberActiveTab("5");
        if (testDate(startDateTxtBx.Text) && testDate(EndDate.Text))
        { 
            DateTime eT = DateTime.Today;
            DateTime sT = DateTime.Today; 
            try { 
                eT = Convert.ToDateTime(EndDate.Text);
                sT = Convert.ToDateTime(startDateTxtBx.Text);
                if (EndDate.Text != "" && sT<=eT) changeDate(startDateTxtBx.Text, EndDate.Text);
                else emptyVal();
            }
            catch { errorOut(EndDate, errorApptDate, "Not Valid Date"); } 
        }
        if (testDate(startDateTxtBx.Text) == false) errorOut(startDateTxtBx, errorApptDate, "Not Valid Date");
        if (testDate(EndDate.Text) == false) errorOut(EndDate, errorApptDate, "Not Valid Date");
    }
}