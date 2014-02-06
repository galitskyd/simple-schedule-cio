using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for sqlDataTableSurgery
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
            string sqlCmdString = "SELECT dbo.surgery_room_schedule.surgery_event_id,"+
                "dbo.surgery_room_schedule.ordering_position,"+
                "dbo.surgery_room_schedule.duration,"+
                "dbo.provider_mstr.description,"+
                "dbo.person.last_name,"+
                "dbo.person.first_name,"+
                "dbo.person.date_of_birth,"+
                "dbo.person.sex,"+
                "dbo.surgery_room_schedule.surgery_name,"+
                "dbo.surgery_room_schedule.details,"+
                "dbo.surgery_room_schedule.surg_date,"+
                "dbo.location_mstr.location_name,"+
                "dbo.surgery_room_schedule.room_number"+
                " FROM dbo.location_mstr,dbo.provider_mstr,dbo.person,dbo.patient,dbo.surgery_room_schedule"+
                " WHERE dbo.patient.med_rec_nbr = dbo.surgery_room_schedule.med_rec_nbr"+
                " AND dbo.person.person_id = dbo.patient.person_id"+ 
                " AND dbo.provider_mstr.provider_id = dbo.surgery_room_schedule.provider_id"+
                " AND dbo.location_mstr.location_id = dbo.surgery_room_schedule.location_id"+
                " ORDER BY dbo.surgery_room_schedule.ordering_position";
            conn.Open();
            SqlCommand cmd = new SqlCommand(sqlCmdString, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            dtInfo.Load(reader);
        }
        catch { Console.WriteLine("Error"); }
        return dtInfo;
    }
}
