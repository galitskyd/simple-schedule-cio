using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for sqlDataTable
/// </summary>
public class sqlDataTableSurgery
{
    static DataTable dtInfo = new DataTable();


public static DataTable infoData()
     {
         dtInfo.Clear();
        SqlConnection conn = dbConnect.connection();

        try
        {
            string sqlCmdString = "SELECT dbo.surgery_room_schedule.ordering_position,dbo.surgery_room_schedule.duration,dbo.provider_mstr.description,dbo.patient.user_defined1,dbo.surgery_room_schedule.surgery_name,dbo.surgery_room_schedule.details,dbo.surgery_room_schedule.surg_date,dbo.location_mstr.location_name,dbo.surgery_room_schedule.room_number FROM dbo.location_mstr,dbo.provider_mstr,dbo.patient,dbo.surgery_room_schedule WHERE dbo.patient.person_id = dbo.surgery_room_schedule.patient_id AND dbo.provider_mstr.provider_id = dbo.surgery_room_schedule.provider_id AND dbo.location_mstr.location_id = dbo.surgery_room_schedule.location_id ORDER BY dbo.surgery_room_schedule.ordering_position";
            conn.Open();
            SqlCommand cmd = new SqlCommand(sqlCmdString, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            dtInfo.Load(reader);
        }
        catch { Console.WriteLine("Error"); }
        return dtInfo;
    }
}
