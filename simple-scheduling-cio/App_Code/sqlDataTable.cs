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


public static DataTable infoData(string begin,string end)
     {
         dtInfo.Clear();
        SqlConnection conn = dbConnect.connection();

        try
        {

            conn.Open();
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            cmd.CommandText = "getAppointments_BSU";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@beginDateVal", begin);
            cmd.Parameters.AddWithValue("@endDateVal", end);
            cmd.Connection = conn;
            

            
            reader = cmd.ExecuteReader();
            dtInfo.Load(reader);


            conn.Close();

        }
        catch { Console.WriteLine("Error"); }
        return dtInfo;
    }
	}
