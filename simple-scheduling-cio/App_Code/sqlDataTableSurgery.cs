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
    public static DataTable infoData(String cmdString)
    {
        DataTable dtInfo = new DataTable();
        using (SqlConnection conn = dbConnect.connectionSurgery())
        {
            using (SqlCommand cmd = new SqlCommand(cmdString, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    dtInfo.Load(reader);
                    conn.Close();
                }
                catch { Console.WriteLine("Error"); }
            }
        }
        return dtInfo;
    }
    public static DataTable infoDataSchedule()
    {
        return infoData("surgGetSchedule");
    }
    public static DataTable infoDataProviders()
    {
        return infoData("surgGetScheduleProviders");
    }
    public static DataTable infoDataItems()
    {
        return infoData("surgGetScheduleItems");
    }

    public static DataTable AuthenticateUser()
    {
        DataTable dt = new DataTable();
        using (SqlConnection conn = dbConnect.connectionSurgery())
        {
            string sqlCmdString = "surgGetIdentification";
            using (SqlCommand cmd = new SqlCommand(sqlCmdString, conn))
            {
                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    dt.Load(reader);
                    conn.Close();
                }
                catch (Exception)
                { System.Diagnostics.Debug.WriteLine("Error Authenticate User"); }
            }
        }
        return dt;
    }
    public static void updateOrder(int endNumber, int uniqueNumber)
    {
        DataTable dtInfo = new DataTable();
        SqlConnection conn = dbConnect.connectionSurgery();
        string cmdString =
            "UPDATE dbo.surgery_room_schedule " +
            "SET    ordering_position=" + endNumber + " " +
            "WHERE  surgery_event_id=" + uniqueNumber;
        conn.Open();
        SqlCommand cmd = new SqlCommand(cmdString, conn);
        cmd.ExecuteNonQuery();
        conn.Close();
    }
}