using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    protected void AddNewClient(object sender, EventArgs e)
    {
        Conn connn = new Conn();
        SqlConnection conV = new SqlConnection(connn.Van);
        SqlCommand cmd = new SqlCommand();

        string checkQuery = "select count(*) FROM earlypayinautoclients WHERE clientcode = @clientcode";
        cmd = new SqlCommand(checkQuery, conV);
        cmd.Parameters.AddWithValue("@clientcode", earlypayinclient.Text);
        conV.Open();
        int count = (int)cmd.ExecuteScalar();

        if (count == 0)
        {
            string insertQuery = "INSERT INTO earlypayinautoclients (clientcode) VALUES (@clientcode)";
            using (SqlCommand insertCmd = new SqlCommand(insertQuery, conV))
            {
                insertCmd.Parameters.AddWithValue("@clientcode", earlypayinclient.Text);
                insertCmd.ExecuteNonQuery();
            }
            Response.Redirect(Request.RawUrl);
        }
        else
        {
            MsgBox("Client is already exists.");
        }
        conV.Close();
    }
    private void MsgBox(string sMessage)
    {
        string msg = "<script language=\"javascript\">";
        msg += "alert('" + sMessage + "');";
        msg += "</script>";
        Response.Write(msg);
    }
}