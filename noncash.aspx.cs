using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using DocumentFormat.OpenXml.Office.Word;
using System.Globalization;

public partial class noncash : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }
    protected void Submit_Click(object sender, EventArgs e)
    {
        Conn connn = new Conn();
        SqlConnection con = new SqlConnection(connn.Connn);
        SqlConnection conV = new SqlConnection(connn.Van);
        conV.Open();
        //SqlCommand cmd = new SqlCommand("exec insert_Noncash '" + clientcode.Text + "','" + Exchange.Text + "','"+ Amount.Text + "'", conV);
        SqlCommand cmd = new SqlCommand("insert_Noncash", conV);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@userid", Session["Name"].ToString());
        cmd.Parameters.AddWithValue("@ClientCode", clientcode.Text);
       // cmd.Parameters.AddWithValue("@exchange", Exchange.Text);
        cmd.Parameters.AddWithValue("@Amount", Amount.Text);
        cmd.Parameters.AddWithValue("@status", "0");
        cmd.Parameters.AddWithValue("@dt", DateTime.Now);
        cmd.Parameters.AddWithValue("@mode", 1);

        cmd.ExecuteNonQuery();

        conV.Close();
        MsgBox("New Entry has been added successfully");
    }
    private void MsgBox(string sMessage)
    {
        string msg = "<script language=javascript>";
        msg += "alert('" + sMessage + "');";
        msg += "</script>";
        Response.Write(msg);
    }
    protected void Non_cashbtn(object sender, EventArgs e)
    {

        string randomnumber = DateTime.Now.ToString("yyyyMMddHHmmss");
        string filename;
        Conn connn = new Conn();
        SqlConnection conV = new SqlConnection(connn.Connn);
        SqlCommand cmd = new SqlCommand();
        try
        {
            if (Non_cash.HasFile)
            {
                filename = Non_cash.FileName;
                Non_cash.SaveAs(Server.MapPath("~/Uploads/") + randomnumber + filename);
                System.Data.DataTable dt;
                dt = ConvertCSVtoDataTable(Server.MapPath("~/Uploads/") + randomnumber + filename);


                int totalColumnsToReserve = 70;
                for (int i = dt.Columns.Count - 1; i >= totalColumnsToReserve; i--)
                {
                    dt.Columns.RemoveAt(i);
                }
                //dt.Rows.RemoveAt(0);

                

                using (SqlConnection conn = new SqlConnection(connn.Van))
                {
                    using (SqlCommand cmd1 = new SqlCommand("noncashbulk"))
                    {
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Connection = conn;
                        cmd1.Parameters.AddWithValue("@dt", dt);
                        conn.Open();
                        cmd1.ExecuteNonQuery();
                        conn.Close();
                        MsgBox("File Upload Successfully");
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

        }
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
    public System.Data.DataTable createDataTable()
    {
        
        DateTime now = DateTime.Now;
        var startDate = new DateTime(now.Year, now.Month, 1);

        Conn connn = new Conn();
        SqlConnection con = new SqlConnection(connn.Connn);
        SqlConnection conV = new SqlConnection(connn.Van);
        SqlCommand cmd = new SqlCommand("Exec insert_Noncash '','','','','','0'", conV);
        cmd.CommandTimeout = 60;
        System.Data.DataTable ds = new System.Data.DataTable();
        conV.Open();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        conV.Close();
        return ds;
    }

    protected void Export_Click(object sender, EventArgs e)
    {

        Conn connn = new Conn();
        SqlConnection conV = new SqlConnection(connn.Van);

        string FileName = "Non_Cash";
        string Name = "Export";
        System.Data.DataTable dt = createDataTable();

        string str = string.Empty;
        foreach (DataColumn column in dt.Columns)
        {
            // Add the header to the text file
            str += column.ColumnName + ",";
        }
        str += "\r\n";

        foreach (DataRow row in dt.Rows)
        {
            foreach (DataColumn column in dt.Columns)
            {
                // Insert the Data rows.
                str += row[column.ColumnName].ToString() + ",";
            }
            // Insert a  new line.
            str += "\r\n";
        }

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=" + FileName + ".csv");
        Response.Charset = "";
        Response.ContentType = "application/text";
        Response.Output.Write(str);
        Response.Flush();
        Response.End();




    }
}