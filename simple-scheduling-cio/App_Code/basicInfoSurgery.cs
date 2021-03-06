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
        dc = new DataColumn("Patient");
        info.Columns.Add(dc);
        dc = new DataColumn("Birthdate");
        info.Columns.Add(dc);
        dc = new DataColumn("Age");
        info.Columns.Add(dc);
        dc = new DataColumn("Gender");
        info.Columns.Add(dc);
        dc = new DataColumn("Weight");
        info.Columns.Add(dc);
        dc = new DataColumn("Surgery Details");
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
        dc = new DataColumn("Provider");
        info.Columns.Add(dc);
        dc = new DataColumn("Anesthesia");
        info.Columns.Add(dc);
        dc = new DataColumn("Equipment");
        info.Columns.Add(dc);
        dc = new DataColumn("Plates");
        info.Columns.Add(dc);
        dc = new DataColumn("Latex");
        info.Columns.Add(dc);
        dc = new DataColumn("Diabetic");
        info.Columns.Add(dc);
        dc = new DataColumn("Vanco");
        info.Columns.Add(dc);
        dc = new DataColumn("Coaguchek");
        info.Columns.Add(dc);
        DataRow newRow;
        foreach (DataRow row in infoDataTable.Rows)
        {
            newRow = info.NewRow();
            if ((row["med_rec_nbr"].ToString() != "") && (row["med_rec_nbr"].ToString() != "000000000000"))
                foreach (DataColumn col in info.Columns)
                {
                    if (col.ColumnName == "Position") newRow[col] = row["ordering_position"].ToString();
                    if (col.ColumnName == "Duration") newRow[col] = row["duration"].ToString();
                    if (col.ColumnName == "Patient") newRow[col] = row["last_name"].ToString() + ", " + row["first_name"].ToString();
                    if (col.ColumnName == "Birthdate")
                    {
                        String birthdate = "";
                        if (row["date_of_birth"].ToString() != "") birthdate = DateTime.ParseExact(row["date_of_birth"].ToString(), "yyyyMMdd", null).ToString("MM/dd/yyyy");
                        newRow[col] = birthdate;
                    }
                    if (col.ColumnName == "Age")
                    {
                        if (row["date_of_birth"].ToString() != "") newRow[col] = getAge(row["date_of_birth"].ToString()).ToString();
                        else newRow[col] = "";
                    }
                    if (col.ColumnName == "Gender") newRow[col] = row["sex"].ToString();
                    if (col.ColumnName == "Weight") newRow[col] = row["weight_lb"].ToString();
                    if (col.ColumnName == "Surgery Details") newRow[col] = row["surgery_details"].ToString();
                    if (col.ColumnName == "Date") newRow[col] = DateTime.ParseExact(row["surg_date"].ToString(), "yyyyMMdd", null).ToString("MM/dd/yyyy");
                    if (col.ColumnName == "Location") newRow[col] = row["location_name"].ToString();
                    if (col.ColumnName == "Room") newRow[col] = row["room_name"].ToString();
                    if (col.ColumnName == "MedRec#") newRow[col] = row["med_rec_nbr"].ToString();
                    if (col.ColumnName == "ID") newRow[col] = row["surgery_event_id"].ToString();
                    if (col.ColumnName == "Latex") newRow[col] = row["latex_allergy"].ToString();
                    if (col.ColumnName == "Diabetic") newRow[col] = row["is_diabetic"].ToString();
                    if (col.ColumnName == "Vanco") newRow[col] = row["vanco_preop"].ToString();
                    if (col.ColumnName == "Coaguchek") newRow[col] = row["coaguchek"].ToString();
                }
            else
                foreach (DataColumn col in info.Columns)
                {
                    if (col.ColumnName == "Position") newRow[col] = row["ordering_position"].ToString();
                    if (col.ColumnName == "Duration") newRow[col] = row["duration"].ToString();
                    if (col.ColumnName == "Patient") newRow[col] = "";
                    if (col.ColumnName == "Birthdate") newRow[col] = "";
                    if (col.ColumnName == "Age") newRow[col] = "";
                    if (col.ColumnName == "Gender") newRow[col] = "";
                    if (col.ColumnName == "Weight") newRow[col] = "";
                    if (col.ColumnName == "Surgery Details") newRow[col] = row["surgery_details"].ToString();
                    if (col.ColumnName == "Date") newRow[col] = DateTime.ParseExact(row["surg_date"].ToString(), "yyyyMMdd", null).ToString("MM/dd/yyyy");
                    if (col.ColumnName == "Location") newRow[col] = row["location_name"].ToString();
                    if (col.ColumnName == "Room") newRow[col] = row["room_name"].ToString();
                    if (col.ColumnName == "MedRec#") newRow[col] = "";
                    if (col.ColumnName == "ID") newRow[col] = row["surgery_event_id"].ToString();
                    if (col.ColumnName == "Latex") newRow[col] = "";
                    if (col.ColumnName == "Diabetic") newRow[col] = "";
                    if (col.ColumnName == "Vanco") newRow[col] = "";
                    if (col.ColumnName == "Coaguchek") newRow[col] = "";
                }
            info.Rows.Add(newRow);
        }
        for (int i = 0; i < info.Rows.Count; i++)
        {
            DataTable dt;
            String items = "";
            int surgEventID = int.Parse(info.Rows[i]["ID"].ToString());
            dt = dtItemFilter(providerDataTable, surgEventID, "provider");
            items = parsedItems(dt, surgEventID, ", <br />");
            info.Rows[i]["Provider"] = items;
            dt = dtItemFilter(itemsDataTable, surgEventID, "A");
            items = parsedItems(dt, surgEventID, ", ");
            if (items.Count() == 0) info.Rows[i]["Anesthesia"] = "N/A";
            else info.Rows[i]["Anesthesia"] = items;
            dt = dtItemFilter(itemsDataTable, surgEventID, "E");
            items = parsedItems(dt, surgEventID, ", ");
            if (items.Count() == 0) info.Rows[i]["Equipment"] = "N/A";
            else info.Rows[i]["Equipment"] = items;
            dt = dtItemFilter(itemsDataTable, surgEventID, "P");
            items = parsedItems(dt, surgEventID, ", ");
            if (items.Count() == 0) info.Rows[i]["Plates"] = "N/A";
            else info.Rows[i]["Plates"] = items;
        }
        return info;
    }
    static private int getAge(String dateVal)
    {
        string pattern = "yyyyMMdd";
        DateTime parsedDate;
        DateTime today = DateTime.Today;
        DateTime.TryParseExact(dateVal, pattern, null, System.Globalization.DateTimeStyles.None, out parsedDate);
        int age = today.Year - parsedDate.Year;
        if (parsedDate > today.AddYears(-age)) age--;
        return age;
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