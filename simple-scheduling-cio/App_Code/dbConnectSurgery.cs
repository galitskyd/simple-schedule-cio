﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

/// <summary>
/// Summary description for basicInfo
/// </summary>
public class dbConnectSurgery
{
    static public SqlConnection connection()
    {

        string connString = "Data Source=DULDEN-PC\\SQLEXPRESS;Initial Catalog=BSU_PROJECT_SURGERY;Integrated Security=True";
        SqlConnection conn = new SqlConnection(connString);
        return conn;
    }
}