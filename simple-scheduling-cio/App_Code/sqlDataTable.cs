using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for sqlDataTable
/// </summary>
public class sqlDataTable
{
    static DataTable dtInfo = new DataTable();


public static DataTable infoData()
     {
         dtInfo.Clear();
        SqlConnection conn = dbConnect.connection();

        try
        {
            string sqlCmdString = "SELECT " +
                                    "dbo.provider_mstr.description," +
                                    "dbo.appointments.description," +
                                    "dbo.location_mstr.location_name," +
                                    "dbo.appointments.appt_date," +
                                    "dbo.appointments.begintime," +
                                    "dbo.appointments.endtime," +
                                    "dbo.appointments.duration," +
                                    "dbo.appointments.details," +
                                    "dbo.appointments.workflow_status " +
                                  "FROM " +
                                    "dbo.provider_mstr," +
                                    "dbo.location_mstr," +
                                    "dbo.appointments " +
                                  "WHERE " +
                                    "dbo.provider_mstr.primary_loc_id = dbo.location_mstr.location_id " +
                                  "AND " +
                                    "dbo.provider_mstr.provider_id = dbo.appointments.rendering_provider_id";
            conn.Open();
            SqlCommand cmd = new SqlCommand(sqlCmdString, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            dtInfo.Load(reader);
        }
        catch { Console.WriteLine("Error"); }
        return dtInfo;
    }
	}
