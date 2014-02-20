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
        DataTable InfoDataTable = sqlDataTableSurgery.infoData();
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
        DataRow row;
        for (int i = 0; i < InfoDataTable.Rows.Count; i++)
        {
            row = info.NewRow();

            for (int x = 0; x < InfoDataTable.Columns.Count; x++)
            {
                if (x == 1) row[x-1] = InfoDataTable.Rows[i][x].ToString();
                if ((x == 2) || (x == 3)) row[x + 1] = InfoDataTable.Rows[i][x].ToString();
                if (x == 4)
                {
                    String lastName = InfoDataTable.Rows[i][x].ToString();
                    String firstName = InfoDataTable.Rows[i][5].ToString();
                    row[x+1] = lastName + ", " + firstName;
                }
                if (x == 6)
                {
                    string dateVal = InfoDataTable.Rows[i][x].ToString();
                    string pattern = "yyyyMMdd";
                    DateTime parsedDate;
                    DateTime today = DateTime.Today;
                    DateTime.TryParseExact(dateVal, pattern, null, System.Globalization.DateTimeStyles.None, out parsedDate);
                    int age = today.Year - parsedDate.Year;
                    if (parsedDate > today.AddYears(-age)) age--;
                    row[x] = age.ToString();
                }
                if ((x > 6) && (x < 10)) row[x] = InfoDataTable.Rows[i][x].ToString();
                if (x == 10)
                {
                    string dateVal = InfoDataTable.Rows[i][x].ToString();
                    string pattern = "yyyyMMdd";
                    DateTime parsedDate;
                    DateTime.TryParseExact(dateVal, pattern, null, System.Globalization.DateTimeStyles.None, out parsedDate);
                    row[x] = parsedDate.ToString("yyyy/MM/dd");
                }
                if (x > 10 && x<13) row[x] = InfoDataTable.Rows[i][x].ToString();
                if (x == 13) row[x] = InfoDataTable.Rows[i][x].ToString().Substring(3);

            }
            info.Rows.Add(row);
        }
        return info;

    }

}