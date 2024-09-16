using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EarlyPayinClients : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Submit_Click(object sender, EventArgs e)
    {
        Conn connn = new Conn();
        SqlConnection conV = new SqlConnection(connn.Van);
        SqlCommand cmd = new SqlCommand();
        if (string.IsNullOrWhiteSpace(clientcode.Text))
        {
            MsgBox("Client Cannot be Empty");
        }
        else
        {
            string checkQuery = "select count(*) FROM earlypayinautoclients WHERE clientcode = @clientcode";
            cmd = new SqlCommand(checkQuery, conV);
            cmd.Parameters.AddWithValue("@clientcode", clientcode.Text);
            conV.Open();
            int count = (int)cmd.ExecuteScalar();

            if (count == 0)
            {
                string insertQuery = "INSERT INTO earlypayinautoclients (clientcode) VALUES (@clientcode)";
                using (SqlCommand insertCmd = new SqlCommand(insertQuery, conV))
                {
                    insertCmd.Parameters.AddWithValue("@clientcode", clientcode.Text);
                    insertCmd.ExecuteNonQuery();
                }
                ClientScript.RegisterStartupScript(this.GetType(), "AlertAndRedirect", @"<script type='text/javascript'>alert('Client added Sucessfully !!!'); setTimeout(function() { window.location.href = 'EarlyPayinClientsList.aspx'; }, 50);</script>");
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "AlertAndRedirect", @"<script type='text/javascript'>alert('Client is already exists.'); setTimeout(function() { window.location.href = 'EarlyPayinClientsList.aspx'; }, 50);</script>");
            }
            conV.Close();
        }        
    }

    protected void Upload_Click(object sender, EventArgs e)
    {
        string randomnumber = DateTime.Now.ToString("yyyyMMddHHmmss");
        string filename;
        Conn connn = new Conn();
        SqlConnection con = new SqlConnection(connn.Connn);
        SqlConnection conV = new SqlConnection(connn.Van);
        SqlCommand cmd = new SqlCommand();
        try
        {
            if (FileUpload.HasFile)
            {
                filename = FileUpload.FileName;
                string filePath = Path.Combine(Server.MapPath("~/Uploads/"), randomnumber + filename);
                FileUpload.SaveAs(filePath);
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
                    using (cmd = new SqlCommand("InsertClientCodes"))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@ClientCodes", table);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        ClientScript.RegisterStartupScript(this.GetType(), "AlertAndRedirect", @"<script type='text/javascript'>alert('File Uploaded Sucessfully !!!'); setTimeout(function() { window.location.href = 'EarlyPayinClientsList.aspx'; }, 50);</script>");
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
            MsgBox("Issue In File: " + ex.Message);
        }
    }

    private void MsgBox(string sMessage)
    {
        string msg = "<script language=\"javascript\">";
        msg += "alert('" + sMessage + "');";
        msg += "</script>";
        Response.Write(msg);
    }
}