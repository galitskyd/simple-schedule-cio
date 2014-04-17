using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for connectionString
/// </summary>
/// 

public class basicInfoSurgery
{

    static public DataTable dt()
    {
        DataTable infoDataTable = sqlDataTableSurgery.infoDataSchedule();
        DataTable providerDataTable = sqlDataTableSurgery.infoDataProviders();
        DataTable itemsDataTable = sqlDataTableSurgery.infoDataItems();
        DataTable info = new DataTable();
        DataColumn dc;
        dc = new DataColumn("Position");
        info.Columns.Add(dc);
        dc = new DataColumn("Start Time");
        info.Columns.Add(dc);
        dc = new DataColumn("End Time");
        info.Columns.Add(dc);
        dc = new DataColumn("Duration");
        info.Columns.Add(dc);
        dc = new DataColumn("Provider");
        info.Columns.Add(dc);
        dc = new DataColumn("Patient");
        info.Columns.Add(dc);
        dc = new DataColumn("Age");
        info.Columns.Add(dc);
        dc = new DataColumn("Gender");
        info.Columns.Add(dc);
        dc = new DataColumn("Surgery");
        info.Columns.Add(dc);
        dc = new DataColumn("Details");
        info.Columns.Add(dc);
        dc = new DataColumn("Date");
        info.Columns.Add(dc);
        dc = new DataColumn("Location");
        info.Columns.Add(dc);
        dc = new DataColumn("Room");
        info.Columns.Add(dc);
        dc = new DataColumn("MedRec#");
        info.Columns.Add(dc);
        dc = new DataColumn("ID");
        info.Columns.Add(dc);
        dc = new DataColumn("Anesthesia");
        info.Columns.Add(dc);
        dc = new DataColumn("Equipment");
        info.Columns.Add(dc);
        dc = new DataColumn("Plates");
        info.Columns.Add(dc);
        DataRow row;
        for (int i = 0; i < infoDataTable.Rows.Count; i++)
        {
            row = info.NewRow();

            for (int x = 0; x < info.Columns.Count; x++)
            {
                if (x == 1) row[x - 1] = infoDataTable.Rows[i][x].ToString();
                if ((x == 2) || (x == 3)) row[x + 1] = infoDataTable.Rows[i][x].ToString();
                if (x == 4)
                {
                    String lastName = infoDataTable.Rows[i][x].ToString();
                    String firstName = infoDataTable.Rows[i][5].ToString();
                    row[x + 1] = lastName + ", " + firstName;
                }
                if (x == 6)
                {
                    string dateVal = infoDataTable.Rows[i][x].ToString();
                    string pattern = "yyyyMMdd";
                    DateTime parsedDate;
                    DateTime today = DateTime.Today;
                    DateTime.TryParseExact(dateVal, pattern, null, System.Globalization.DateTimeStyles.None, out parsedDate);
                    int age = today.Year - parsedDate.Year;
                    if (parsedDate > today.AddYears(-age)) age--;
                    row[x] = age.ToString();
                }
                if ((x > 6) && (x < 10)) row[x] = infoDataTable.Rows[i][x].ToString();
                if (x == 10)
                {
                    string dateVal = infoDataTable.Rows[i][x].ToString();
                    string pattern = "yyyyMMdd";
                    DateTime parsedDate;
                    DateTime.TryParseExact(dateVal, pattern, null, System.Globalization.DateTimeStyles.None, out parsedDate);
                    row[x] = parsedDate.ToString("yyyy/MM/dd");
                }
                if (x > 10 && x < 13)
                {
                    row[x] = infoDataTable.Rows[i][x].ToString();
                }
                if (x == 14) row[x] = infoDataTable.Rows[i][0].ToString();
                if (x == 13) row[x] = infoDataTable.Rows[i][x].ToString().Substring(3);

            }
            info.Rows.Add(row);
        }
        for (int i = 0; i < info.Rows.Count; i++)
        {
            DataTable dt;
            String items = "";
            int surgEventID = int.Parse(info.Rows[i][14].ToString());
            dt = dtItemFilter(providerDataTable, surgEventID, "provider");
            items = parsedItems(dt, surgEventID, ", <br />&nbsp&nbsp&nbsp");
            info.Rows[i][4] = items;
            dt = dtItemFilter(itemsDataTable, surgEventID, "A");
            items = parsedItems(dt, surgEventID, ", ");
            info.Rows[i][15] = items;
            dt = dtItemFilter(itemsDataTable, surgEventID, "E");
            items = parsedItems(dt, surgEventID, ", ");
            info.Rows[i][16] = items;
            dt = dtItemFilter(itemsDataTable, surgEventID, "P");
            items = parsedItems(dt, surgEventID, ", ");
            info.Rows[i][17] = items;
        }
        return info;

    }
    static private DataTable dtItemFilter(DataTable dt, int surgEventID, String type)
    {
        DataTable dtItems;
        DataView dv = dt.AsDataView();
        if (type == "A" || type == "E" || type == "P")
            dv.RowFilter = "surgery_event_id = " + surgEventID + " AND type = '" + type + "'";
        else if (type == "provider") dv.RowFilter = "surgery_event_id = " + surgEventID;
        dtItems = dv.ToTable();
        return dtItems;
    }
    static private String parsedItems(DataTable dt, int surgEventID, String separated)
    {
        String items = "";
        foreach (DataRow dr in dt.Rows)
        {
            items += dr[1].ToString() + separated;
        }
        if (items != "") items = items.Substring(0, items.Length - separated.Length);
        return items;
    }
}