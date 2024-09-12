using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class allocationfileupload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void cash_allocation(object sender, EventArgs e)
    {

        string randomnumber = DateTime.Now.ToString("yyyyMMddHHmmss");
        string filename;
        Conn connn = new Conn();
        SqlConnection con = new SqlConnection(connn.Connn);
        SqlConnection conV = new SqlConnection(connn.Van);
        SqlCommand cmd = new SqlCommand();
        try
        {
            if (FileUploadcash.HasFile)
            {
                filename = FileUploadcash.FileName;
                string filePath = Path.Combine(Server.MapPath("~/Uploads/"), randomnumber + filename);
                FileUploadcash.SaveAs(filePath);
                var table = new System.Data.DataTable();
                var fileContents = File.ReadAllLines(filePath);
                var splitFileContents = (from f in fileContents select f.Split(',')).ToArray();

                int maxLength = (from s in splitFileContents select s.Count()).Max();

                for (int i = 0; i < maxLength; i++)
                {
                    table.Columns.Add();
                }

                foreach (var line in splitFileContents)
                {
                    DataRow row = table.NewRow();
                    row.ItemArray = (object[])line;
                    table.Rows.Add(row);
                }
                table.Rows.RemoveAt(0);

                using (con = new SqlConnection(connn.Van))
                {
                    using (cmd = new SqlCommand("Ins_UploadAllocation1st"))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@dt", table);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MsgBox("File Upload Sucessfully");
                    }
                } 
            }
            else {
                MsgBox("Please Select File");
            }
        }
        catch (Exception ex)
        {
            EvntNeo("Issue In File: " + ex.Message);
        }

    }

    private void ShowAlert(string message)
    {
        // Escape special characters
        message = message.Replace("'", "\\'").Replace("\"", "\\\"");
        string script = "alert('" + message + "');";
        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", script, true);
    }



    protected void Fo_allocation(object sender, EventArgs e)
    {

        string randomnumber = DateTime.Now.ToString("yyyyMMddHHmmss");
        string filename;
        Conn connn = new Conn();
        SqlConnection con = new SqlConnection(connn.Connn);
        SqlConnection conV = new SqlConnection(connn.Van);
        SqlCommand cmd = new SqlCommand();
        try
        {
            if (FileUploadFO.HasFile)
            {
                filename = FileUploadFO.FileName;
                string filePath = Path.Combine(Server.MapPath("~/Uploads/"), randomnumber + filename);
                FileUploadFO.SaveAs(filePath);
                var table = new System.Data.DataTable();
                var fileContents = File.ReadAllLines(filePath);
                var splitFileContents = (from f in fileContents select f.Split(',')).ToArray();

                int maxLength = (from s in splitFileContents select s.Count()).Max();

                for (int i = 0; i < maxLength; i++)
                {
                    table.Columns.Add();
                }

                foreach (var line in splitFileContents)
                {
                    DataRow row = table.NewRow();
                    row.ItemArray = (object[])line;
                    table.Rows.Add(row);
                }
                table.Rows.RemoveAt(0);

                using (con = new SqlConnection(connn.Van))
                {
                    using (cmd = new SqlCommand("Ins_UploadAllocation2nd"))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@dt", table);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MsgBox("File Upload Sucessfully");
                    }
                }

                

            }
            else
            {
                MsgBox("Please Select File");
            }
           

        }
        catch (Exception ex)
        {
            EvntNeo(ex.Message);
        }
    }
    private void EvntNeo(string str)
    {
        Conn connn = new Conn();
        SqlConnection con = new SqlConnection(connn.Connn);
        SqlConnection conV = new SqlConnection(connn.Van);
        SqlCommand cmd = new SqlCommand();
        if (conV.State == ConnectionState.Open)
        {
            conV.Close();
        }
        cmd = new SqlCommand("Insert into TradeLogs values(Getdate(),'" + str.Replace("'", "") + "')", conV);
        conV.Open();
        cmd.ExecuteNonQuery();
        conV.Close();
    }

    public static System.Data.DataTable ConvertCSVtoDataTable(string strFilePath)
    {
        System.Data.DataTable dt = new System.Data.DataTable();
        using (StreamReader sr = new StreamReader(strFilePath))
        {
            string[] headers = sr.ReadLine().Split(',');
            foreach (string header in headers)
            {
                dt.Columns.Add(header);
            }
            while (!sr.EndOfStream)
            {
                string[] rows = sr.ReadLine().Split(',');
                DataRow dr = dt.NewRow();
                for (int i = 0; i < headers.Length; i++)
                {
                    dr[i] = rows[i];
                }
                dt.Rows.Add(dr);
            }

        }


        return dt;
    }

    private void MsgBox(string sMessage)
    {
        string msg = "<script language=\"javascript\">";
        msg += "alert('" + sMessage + "');";
        msg += "</script>";
        Response.Write(msg);
    }
}