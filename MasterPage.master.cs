using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;
using Microsoft.Office.Interop.Excel;
using System.ServiceModel.Security;

public partial class MasterPage : System.Web.UI.MasterPage
{
    public string UserSession_ID { get; set; }
    public string Name1 { get; set; }

    public string Clientcode1 { get; set; }
    public string FName1 { get; set; }
    public string CD1 { get; set; }
    public string Brach1 { get; set; }
    public string LUP { get; set; }

    public string Role { get; set; }

    public string User1 { get; set; }
    public string Username { get; set; }
    public string X2 { get; set; }
    public string bannername { get; set; }



    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Session["User"] as string))
        {
            HttpContext.Current.Session.Clear();
            Response.Redirect("~/Login.aspx?id=2");
        }

        

        UserSession_ID = Session.SessionID;

        string x1 = Session["Name"].ToString();

        Conn connn = new Conn();
        SqlConnection con = new SqlConnection(connn.Connn);
        SqlConnection conV = new SqlConnection(connn.Van);
        SqlCommand cmd = new SqlCommand();

        cmd = new SqlCommand("Exec Newsp_getuserdetailforusername '" + x1 + "'", conV);
        System.Data.DataTable ds1 = new System.Data.DataTable();
        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        conV.Open();
        adapter.Fill(ds1);
        conV.Close();


        Username = ds1.Rows[0]["Username"].ToString();
        Name1 = ds1.Rows[0]["Userid"].ToString();
        Clientcode1 = ds1.Rows[0]["Userid"].ToString();
        FName1 = FirstLetterToUpper(Session["Fame"].ToString());
        String newtype = FirstLetterToUpper(Session["Type"].ToString());

        if (newtype == "Admin")
        {
            //PRM.Visible = false;
        }

        else
        {
            //PRM.Visible = true;
            //MTV.Visible = false;
        }

        //for banner Header 
        User1 = Session["Username"].ToString();
        User1 = "Hi" + " " + User1;
        bannername = User1;
        if (bannername.Length >= 60)
        {
            User1 = bannername.Substring(0, 60);
            X2 = bannername.Substring(60);
        }
        else
        {
            User1 = bannername.Substring(0, bannername.Length);
            X2 = "";
        }

        if (X2 != "")
        {
            X2 = X2 + " | ";
        }

        X2 = X2 + "User Id: " + Clientcode1;

        Role = Session["Type"].ToString();

        //Conn connn = new Conn();
        //SqlConnection con = new SqlConnection(connn.Connn);
        //SqlConnection conV = new SqlConnection(connn.Van);
        //SqlCommand cmd = new SqlCommand();

        cmd = new SqlCommand("Select * from RoleMaster where Rname = '" + Role + "'", conV);
        System.Data.DataTable dt = new System.Data.DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        conV.Open();
        da.Fill(dt);
        conV.Close();


        for (int i = 0; i < dt.Rows.Count; i++)
        {

            bool Stat = Convert.ToBoolean(dt.Rows[i]["View"].ToString());
            string FormName = dt.Rows[i]["Formname"].ToString();

            Session["Stat"] = Stat;

            //if (Role == "Admin")

            //{
            //BasketManagement
            if (FormName == "Dashboard")
            {
                if (Stat == true)
                {
                    //Dashboard.Visible = true;
                }
                else
                {
                    //Dashboard.Visible = false;
                }

            }


            //Manage
            if (FormName == "Manage")
            {
                if (Stat == true)
                {
                    Manage.Visible = true;
                    if (Name1 == "Admin")
                    {
                        //Superadmin.Visible = true;
                        //UD.Visible = true;
                        // DLD.Visible = true;
                    }
                }
                else
                {
                    Manage.Visible = false;
                }

            }
            if (Role == "Admin")
            {
                LogoURL.HRef = "Index.aspx?Typ=1YER";
                Manage.Visible = true;
                //MTV.Visible = true;
                //RMV.Visible = true;
                //DE.Visible = true;
                //RSRM.Visible = true;
                //HR.Visible = true;
                //MTFD.Visible=true;
                //OAC.Visible = true;
                ////KYC.Visible = true;
                //CA.Visible = true;
                //MTFD.Visible=true;
                //DPA.Visible = true;
                //Livetrade.Visible = true;
            }
            else
            {
                if (Role == "Research_Admin")
                {
                    Response.Redirect("https://research.sparkadvisors.in?session_id="+UserSession_ID+"&referrer=fuel_admin");
                }
                else
                {

                    //LogoURL.HRef = "RMDashboard.aspx?Typ=Primary RM";
                    //PRM.Visible = true;
                    //SRM.Visible = true;
                    //PD.Visible = true;
                    //SD.Visible = true;
                    //Manage.Visible = true;
                    //RSRM.Visible = true;
                    //DE.Visible = true;
                    //MTFD.Visible = true;
                    //DPA.Visible = false;
                }
            }


        }
    }
    public string FirstLetterToUpper(string str)
    {

        if (str == null)
            return null;

        if (str.Length > 1)
            return char.ToUpper(str[0]) + str.Substring(1);

        return str.ToUpper();

    }
    
    protected void Logout_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Session.Clear();
        Response.Redirect("~/Login.aspx?id=1");
    }
}
