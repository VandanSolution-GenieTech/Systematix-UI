using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

public partial class Fangeble : System.Web.UI.Page
{
    public int CurrentBalance { get; set; }
    public int Utilization { get; set; }
    public int TotalFree { get; set; }
    public int Amttext { get; set; } 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CurrentBalance = 0;
            Utilization = 0;
            TotalFree = CurrentBalance - Utilization;
        }
        Grid_handler();

    }

    protected void Grid_handler()
    {
        try
        {
            Conn connn = new Conn();
            SqlConnection conV = new SqlConnection(connn.Van);
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("Exec Dt_Load 183,'Admin','','','',''", conV);
            DataSet ds = new DataSet();
            conV.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            conV.Close();
            Repeater1.DataSource = ds;
            Repeater1.DataBind();
            CurrentBalance = Convert.ToInt32(ds.Tables[0].Rows[0]["Amount"]);
            TotalFree = CurrentBalance - Utilization;

        }
        catch (Exception ex)
        {
            MsgBox(ex.StackTrace);
        }
    }
    protected void Submit_Click(object sender, EventArgs e)
    {
        Amttext = Convert.ToInt32(Amt.Text);
        if (Exchange.SelectedValue == "Up")
        {
            CurrentBalance = CurrentBalance + Amttext;

        }
        else
        {
            CurrentBalance = CurrentBalance - Amttext;
        }

        Conn connn = new Conn();
        using (SqlConnection conV = new SqlConnection(connn.Van))
        {
            conV.Open();
            using (SqlCommand cmd = new SqlCommand("Insert_fungiblelimit", conV))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Type", Exchange.SelectedValue);
                cmd.Parameters.Add("@Amt", Amttext);
                cmd.Parameters.Add("@Remark", Remark.Text);
                cmd.ExecuteNonQuery();
            }
        }
        TotalFree = CurrentBalance - Utilization;
        MsgBox("New Amount has been added successfully");
        Grid_handler();
        //Response.Redirect("Fangeble.aspx");

    }

    private void MsgBox(string sMessage)
    {
        string msg = "<script language=\"javascript\">";
        msg += "alert('" + sMessage + "');";
        msg += "</script>";
        Response.Write(msg);
    }
}