using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for basicInfo
/// </summary>
public class surgManager
{
    private void executeSQL(String sqlCmdString, String sqlVar, object item)
    {
        using (SqlConnection conn = dbConnectSurgery.connection())
        {
            using (SqlCommand cmd = new SqlCommand(sqlCmdString, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue(sqlVar, item);
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                catch { Console.WriteLine("Error"); }
            }
        }
    }
    
    public void toggleItems(String cmd, ListBox lb)
    {
        foreach (ListItem item in lb.Items)
        {
            if (item.Selected == true)
            {
                executeSQL(cmd, "@id", item.Value);
            }
        }
    }
    public void deleteItems(String cmd, ListBox lb)
    {
        foreach (ListItem item in lb.Items)
        {
            if (item.Selected == true)
            {
                executeSQL(cmd, "@id", item.Value);
            }
        }
    }
    public void addItem(String cmd, String name)
    {
        executeSQL(cmd, "@name", name);
    }
}