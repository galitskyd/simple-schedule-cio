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
        using (SqlConnection conn = dbConnectSurgery.connection())
        {
            try
            {
                string sqlCmdString = "surgGetSchedule";
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlCmdString, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                dtInfo.Load(reader);
                conn.Close();
            }
            catch { Console.WriteLine("Error"); }
        }
        return dtInfo;
    }
public static DataTable AuthenticateUser()
{
    DataTable dt = new DataTable();
    using (SqlConnection conn = dbConnectSurgery.connection())
    {
        try
        {
            string sqlCmdString = "surgGetIdentification";
            conn.Open();
            SqlCommand cmd = new SqlCommand(sqlCmdString, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            dt.Load(reader);
            conn.Close();
        }
        catch (Exception)
        { Console.WriteLine("Error Authenticate User"); }
    }
    return dt;
}
}
