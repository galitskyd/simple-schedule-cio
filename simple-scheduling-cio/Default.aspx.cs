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
        myCookie = Request.Cookies["activeTab"];

        if (myCookie != null) TabContainer1.ActiveTabIndex = Convert.ToInt32(myCookie.Value);
            GridView1.DataSource = basicInfo.dt();
            GridView1.DataBind();
            System.Console.Write(basicInfo.dt());
           // if (FilterSearchTermsAppointment.Text == "") FilterSearchTermsAppointment.Text = DateTime.Today.ToString("MM/dd/yyyy");
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
    protected void txtSearch_KeyUp(object sender, EventArgs e)
    {
        /*
        dv = basicInfo.dt().DefaultView;
        string outputInfo = "";

        if (outputInfo.Length == 0)
        {
            outputInfo = "(Doctor LIKE '%" + FilterSearchTermsDoctor.Text + "%' AND Patient LIKE '%" + FilterSearchTermsPatient.Text + "%' AND Location LIKE '%" + FilterSearchTermsLocation.Text + "%' AND Duration LIKE '%" + FilterSearchTermsDuration.Text + "%' AND [Appt Date] LIKE '%" + FilterSearchTermsAppointment.Text + "%' AND Details LIKE '%" + FilterSearchTermsDetail.Text + "%' AND Status LIKE '%" + FilterSearchTermsStatus.Text + "%')";
        }
        else
        {
            outputInfo += " AND (Doctor LIKE '%" + FilterSearchTermsDoctor.Text + "%' AND Patient LIKE '%" + FilterSearchTermsPatient.Text + "%' AND Location LIKE '%" + FilterSearchTermsLocation.Text + "%' AND Duration LIKE '%" + FilterSearchTermsDuration.Text + "%' AND [Appt Date] LIKE '%" + FilterSearchTermsAppointment.Text + "%' AND Details LIKE '%" + FilterSearchTermsDetail.Text + "%' AND Status LIKE '%" + FilterSearchTermsStatus.Text + "%')";
        }
        dv.RowFilter = outputInfo;
        GridView1.DataSource = dv;
        GridView1.DataBind();
         */
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
        string[] apptDate = null;
        string[] duration = null;
        string[] details = null;
        string[] status = null;
        Uri uri = new Uri(HttpContext.Current.Request.Url.AbsoluteUri);
        var parameters = HttpUtility.ParseQueryString(uri.Query);
        if (parameters["Provider"] != null) providers = Regex.Split(parameters["Provider"], ",");
        if (parameters["Patient"] != null) patient = Regex.Split(parameters["Patient"], ",");
        if (parameters["Location"] != null) location = Regex.Split(parameters["Location"], ",");
        if (parameters["AppointmentDate"] != null) apptDate = Regex.Split(parameters["AppointmentDate"], ",");
        if (parameters["Duration"] != null) duration = Regex.Split(parameters["Duration"], ",");
        if (parameters["Details"] != null) details = Regex.Split(parameters["Details"], ",");
        if (parameters["Status"] != null) status = Regex.Split(parameters["Status"], ",");

        if (providers != null) filter = filterSelection(filter, "Doctor", providers, type);
        if (patient != null)
        {
            if (providers == null) type = 0; else type = 1;
            filter = filterSelection(filter, "Patient", patient, type);
        }
        if (location != null)
        {
            if (providers == null && patient == null) type = 0; else type = 1;
            filter = filterSelection(filter, "Location", location, type);
        }
        if (apptDate != null)
        {
            if (providers == null && patient == null && location == null) type = 0; else type = 1;
            filter = filterSelection(filter, "[Appt Date]", apptDate, type);
        }
        if (duration != null)
        {
            if (providers == null && patient == null && location == null && apptDate == null) type = 0; else type = 1;
            filter = filterSelection(filter, "Duration", duration, type);
        }
        if (details != null)
        {
            if (providers == null && patient == null && location == null && apptDate == null && duration == null) type = 0; else type = 1;
            filter = filterSelection(filter, "Details", details, type);
        }
        if (status != null)
        {
            if (providers == null && patient == null && location == null && apptDate == null && duration == null && details == null) type = 0; else type = 1;
            filter = filterSelection(filter, "Status", status, type);
        }
        dv = basicInfo.dt().DefaultView;
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
        rememberActiveTab("2");
        Response.Redirect(url + filters);
        }
        else
        {
            rememberActiveTab("2");
            errorOut(locationName, errorLocation, "*Please Insert A Location");
        }
    }
    protected void apptDateBTN_Click(object sender, EventArgs e)
    {
        if (apptDate.Text != "")
        {
        filters = filters + "AppointmentDate=" + apptDate.Text + "&";
        rememberActiveTab("3");
        Response.Redirect(url + filters);
        }
        else
        {
            rememberActiveTab("3");
            errorOut(apptDate, errorApptDate, "*Please Insert An Appt Date");
        }
    }

    protected void durationBTN_Click(object sender, EventArgs e)
    {
        if (duration.Text != "")
        {
            filters = filters + "Duration=" + duration.Text + "&";
            rememberActiveTab("4");
            Response.Redirect(url + filters);
        }
        else
        {
            rememberActiveTab("4");
            errorOut(duration, errorDuration, "*Please Insert A Duration");
        }
    }
    protected void detailsBTN_Click(object sender, EventArgs e)
    {
        if (details.Text != "")
        {
            filters = filters + "Details=" + details.Text + "&";
            rememberActiveTab("5");
            Response.Redirect(url + filters);
        }
        else
        {
            rememberActiveTab("5");
            errorOut(details, errorDetails, "*Please Insert Details");
        }
    }
    protected void statusBTN_Click(object sender, EventArgs e)
    {
        if (status.Text != "")
        {
            filters = filters + "Status=" + status.Text + "&";
            rememberActiveTab("6");
            Response.Redirect(url + filters);
        }
        else
        {
            rememberActiveTab("6");
            errorOut(status, errorStatus, "*Please Insert A Status");
        }
    }
    protected void clearFilter(string filterName,string tabNumber)
    {
        Uri uri = new Uri(HttpContext.Current.Request.Url.AbsoluteUri);
        var parameters = HttpUtility.ParseQueryString(uri.Query);
        parameters.Remove(filterName);
        filters = "?" + parameters.ToString();
        rememberActiveTab(tabNumber);
        Response.Redirect("~/Default?" + parameters.ToString());
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
        clearFilter("Location", "2");
    }
    protected void apptDateClearBTN_Click(object sender, EventArgs e)
    {
        clearFilter("AppointmentDate", "3");
    }
    protected void durationClearBTN_Click(object sender, EventArgs e)
    {
        clearFilter("Duration", "4");
    }
    protected void detailsClearBTN_Click(object sender, EventArgs e)
    {
        clearFilter("Details", "5");
    }
    protected void statusClearBTN_Click(object sender, EventArgs e)
    {
        clearFilter("Status", "6");
    }
    protected void clearAll_Click(object sender, EventArgs e)
    {
        filters = "?";
        Response.Redirect("~/Default");
    }
}