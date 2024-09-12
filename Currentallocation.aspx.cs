using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Currentallocation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Conn connn = new Conn();
            SqlConnection conV = new SqlConnection(connn.Van);
            SqlCommand cmd = new SqlCommand();
            String Uid = Session["Name"].ToString();
            String Role = Session["Type"].ToString();
            if (!Page.IsPostBack) // First time only 
            {

                cmd.CommandTimeout = 900;
                cmd = new SqlCommand("Exec Dt_Load 107,'" + Uid + "','" + Role + "','','',''", conV);
                DataSet ds1 = new DataSet();
                conV.Open();
                SqlDataAdapter da1 = new SqlDataAdapter(cmd);
                da1.Fill(ds1);
                conV.Close();

                Client.DataTextField = ds1.Tables[0].Columns[1].ToString(); // text field name of table dispalyed in dropdown
                Client.DataValueField = ds1.Tables[0].Columns[0].ToString();
                Client.DataSource = ds1.Tables[0];
                Client.DataBind();

                ListItem selectList3 = new ListItem("All", "0");
                selectList3.Selected = true;
                Client.Items.Insert(0, selectList3);


            }
            string Clientcod = Client.SelectedValue;

            cmd = new SqlCommand("Exec Dt_Load 184 ,'" + Session["Name"].ToString() +"','','','"+Session["Type"].ToString()+"',''", conV);
                DataSet ds = new DataSet();
                conV.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                conV.Close();
                Repeater1.DataSource = ds;
                Repeater1.DataBind();
            
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Timeout')", true);

            if (Session["CD"] == "")
            {

                Response.Redirect("~/Login.aspx?id=1");
            }
        }
    }
    protected void Submit_Click(object sender, EventArgs e)
    {

    }
    protected void export_Click(object sender, EventArgs e)
    {
        Utils utils = new Utils();
        Conn connn = new Conn();
        using (var conV = new SqlConnection(connn.Van))
        {
            SqlCommand cmd = new SqlCommand();
            //EXEC CENTTrx19.Dbo.btspCENTTrx 208.001, 11, 0, '2019-05-02', NULL, '100586', '', ''
            cmd = new SqlCommand("Exec Dt_Load 184,'" + Session["Name"].ToString() + "','','','"+ Session["Role"].ToString() +"',''", conV);
            DataSet ds = new DataSet();
            conV.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            conV.Close();
            utils.ExporttoExcel(ds.Tables["Table"], "Current_Allocation");

        }
    }
}