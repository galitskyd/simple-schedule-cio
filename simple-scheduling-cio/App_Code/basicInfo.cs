﻿using System;
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

public class basicInfo{

    static public DataTable dt()
    {
        DataTable InfoDataTable = sqlDataTable.infoData();
        DataTable info = new DataTable();
        DataColumn dc;
        dc = new DataColumn("Doctor");
        info.Columns.Add(dc);
        dc = new DataColumn("Patient");
        info.Columns.Add(dc);
        dc = new DataColumn("Location");
        info.Columns.Add(dc);
        dc = new DataColumn("Appt Date");
        info.Columns.Add(dc);
        dc = new DataColumn("Duration");
        info.Columns.Add(dc);
        dc = new DataColumn("Details");
        info.Columns.Add(dc);
        dc = new DataColumn("Status");
        info.Columns.Add(dc);
        DataRow row;
        for (int i = 0; i < InfoDataTable.Rows.Count; i++)
        {
            row = info.NewRow();
            
            for (int x = 0; x < InfoDataTable.Columns.Count; x++)
            {
                if(x<3) row[x] =(InfoDataTable.Rows[i][x].ToString());
                if (x==3){
                    string dateVal = InfoDataTable.Rows[i][x].ToString();
                    string time = InfoDataTable.Rows[i][4].ToString();
                    string pattern = "yyyyMMddHHmm";
                    DateTime parsedDate;
                    DateTime.TryParseExact(dateVal+time, pattern, null,System.Globalization.DateTimeStyles.None, out parsedDate);
                    row[x] = parsedDate.ToString("MM/dd/yyyy-hh:mm tt");
                    
                }
                if (x >5) row[x-2] = (InfoDataTable.Rows[i][x].ToString());
            }
            info.Rows.Add(row);
        }
        return info;

    }
    
}