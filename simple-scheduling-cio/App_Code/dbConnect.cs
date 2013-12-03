using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

/// <summary>
/// Summary description for basicInfo
/// </summary>
public class dbConnect
{
    static public SqlConnection connection()
    {
        string connString = "Data Source=MATT-LT\\SQLEXPRESS;Initial Catalog=BSU_PROJECT;Persist Security Info=True;User ID=admin;Password=capstone";
        SqlConnection conn = new SqlConnection(connString);
        return conn;
    }
}