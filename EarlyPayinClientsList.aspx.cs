using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EarlyPayinClientsList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Conn connn = new Conn();
        SqlConnection sq = new SqlConnection(connn.Van);
        sq.Open();
        string query = "select * from earlypayinautoclients";
        SqlCommand cmd = new SqlCommand(query, sq);
        DataSet ds = new DataSet();
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        sda.Fill(ds);
        sq.Close();
        Repeater1.DataSource = ds;
        Repeater1.DataBind();
    }
    //protected void AddNewClient(object sender, EventArgs e)
    //{
    //    Conn connn = new Conn();
    //    SqlConnection conV = new SqlConnection(connn.Van);
    //    SqlCommand cmd = new SqlCommand();

    //    string checkQuery = "select count(*) FROM earlypayinautoclients WHERE clientcode = @clientcode";
    //    cmd = new SqlCommand(checkQuery, conV);
    //    cmd.Parameters.AddWithValue("@clientcode", earlypayinclient.Text);
    //    conV.Open();
    //    int count = (int)cmd.ExecuteScalar();

    //    if (count == 0)
    //    {
    //        string insertQuery = "INSERT INTO earlypayinautoclients (clientcode) VALUES (@clientcode)";
    //        using (SqlCommand insertCmd = new SqlCommand(insertQuery, conV))
    //        {
    //            insertCmd.Parameters.AddWithValue("@clientcode", earlypayinclient.Text);
    //            insertCmd.ExecuteNonQuery();
    //        }
    //        Response.Redirect(Request.RawUrl);
    //    }
    //    else
    //    {
    //        MsgBox("Client is already exists.");
    //    }
    //    conV.Close();
    //}

    //protected void SubmitClient(object sender, EventArgs e)
    //{
    //    // Your form processing logic here

    //    // After processing, hide TextBox and Submit button
    //    earlypayinclient.Visible = false;
    //    Button1.Visible = false;

    //    // Show Upload and Add New buttons again
    //    client.Visible = true;
    //    // Make sure the Import button is also displayed by setting its style
    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "showButtons", "document.querySelector(\"input[type='button'][value='Import']\").style.display = 'block';", true);
    //}


    //protected void Import_Click(object sender, EventArgs e)
    //{
    //    string randomnumber = DateTime.Now.ToString("yyyyMMddHHmmss");
    //    string filename;
    //    Conn connn = new Conn();
    //    SqlConnection con = new SqlConnection(connn.Connn);
    //    SqlConnection conV = new SqlConnection(connn.Van);
    //    SqlCommand cmd = new SqlCommand();
    //    try
    //    {
    //        if (FileUpload1.HasFile)
    //        {
    //            filename = FileUpload1.FileName;
    //            string filePath = Path.Combine(Server.MapPath("~/Uploads/"), randomnumber + filename);
    //            FileUpload1.SaveAs(filePath);
    //            var table = new System.Data.DataTable();
    //            var fileContents = File.ReadAllLines(filePath);
    //            var splitFileContents = (from f in fileContents select f.Split(',')).ToArray();

    //            int maxLength = (from s in splitFileContents select s.Count()).Max();

    //            for (int i = 0; i < maxLength; i++)
    //            {
    //                table.Columns.Add();
    //            }

    //            foreach (var line in splitFileContents)
    //            {
    //                DataRow row = table.NewRow();
    //                row.ItemArray = (object[])line;
    //                table.Rows.Add(row);
    //            }

    //            table.Rows.RemoveAt(0);

    //            using (con = new SqlConnection(connn.Van))
    //            {
    //                using (cmd = new SqlCommand("InsertClientCodes"))
    //                {
    //                    cmd.CommandType = CommandType.StoredProcedure;
    //                    cmd.Connection = con;
    //                    cmd.Parameters.AddWithValue("@ClientCodes", table);
    //                    con.Open();
    //                    cmd.ExecuteNonQuery();
    //                    con.Close();
    //                    MsgBox("File Upload Sucessfully");
    //                }
    //            }
    //        }
    //        else
    //        {
    //            MsgBox("Please Select File");
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        MsgBox("Issue In File: " + ex.Message);
    //    }
    //}


    private void MsgBox(string sMessage)
    {
        string msg = "<script language=\"javascript\">";
        msg += "alert('" + sMessage + "');";
        msg += "</script>";
        Response.Write(msg);
    }
}