using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;

public partial class ft : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["CD"] == "")
        //{
        //    Response.Redirect("~/Login.aspx?id=1");
        //}
        //else
        //{ 


        try
        {

            Conn connn = new Conn();
            SqlConnection con = new SqlConnection(connn.Connn);
            SqlConnection conV = new SqlConnection(connn.Van);
            SqlCommand cmd = new SqlCommand();
            //EXEC CENTTrx19.Dbo.btspCENTTrx 208.001, 11, 0, '2019-05-02', NULL, '100586', '', ''
            //Session["CD"].ToString()

            //byte[] data = ConvertFromStringToHex("5072617665656e");
            //string base64;
            //string base64 = HexStringToString(Session["CD"].ToString());  //Convert.ToBase64String(data);
            //if (Session["CD"] == null)
            //{
            //    base64 = "NA";
            //}
            //else
            //{
            //    base64 = Encoding.UTF8.GetString(Convert.FromBase64String(Session["CD"].ToString()));  //Convert.ToBase64String(data);
            //}
            //string base64 = Encoding.UTF8.GetString(Convert.FromBase64String(Session["CD"].ToString()));  //Convert.ToBase64String(data);
            //try
            //{
            String LaunchUid = (Request.QueryString["LaunchUid"]);
            String T = (Request.QueryString["UserID"]);
            String L = (Request.QueryString["LIC"]);
            if (L == "VandanB123896TLKM") 
            {
                cmd = new SqlCommand("Exec Dt_Load 30,'" + T + "','','','VandanB123896TLKM' ,''", conV);
                DataSet ds = new DataSet();
                conV.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                DataTable dt = new DataTable();
                da.Fill(dt);

                Session["Name"] = dt.Rows[0][8].ToString();
                Session["PAN"] = dt.Rows[0][1].ToString();
                Session["ADD"] = dt.Rows[0][6].ToString();
                Session["MOB"] = dt.Rows[0][5].ToString();
                Session["Branch"] = dt.Rows[0][11].ToString();
                Session["CD"] = dt.Rows[0][0].ToString(); // "100586";
                Session["CD"] = T;
                if(LaunchUid != "" )
                {
                    Session["LaunchUid"] = LaunchUid;
                }
                else
                {
                    Session["LaunchUid"] = Session["Name"];
                }
                
                Response.Redirect("~/UserProfile.aspx");
            }
            else
            {
                Response.Redirect("~/Login.aspx?id=1");
            }

            //cmd = new SqlCommand("Exec Dt_Load 30,'" + T + "','','','VandanB123896TLKM',''", conV);
            //DataSet ds = new DataSet();
            //conV.Open();
            //SqlDataAdapter da = new SqlDataAdapter(cmd);
            //da.Fill(ds);
            //DataTable dt = new DataTable();
            //da.Fill(dt);

            //Session["Admin"] = "0";

            //if (dt.Rows[0][0].ToString() == "0")
            //{
            //    Response.Redirect("~/Login.aspx?id=1");
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('User Id '" + T + "' and Password is Invalid')", true);
            //    Session["CD"] = "";
            //    return;
            //}


            //DateTime now = DateTime.Now;
            //DateTime startDate = new DateTime(2021, 06, 30);

            //if (now > startDate)
            //{
            //    MsgBox("User Id and Password is Invalid");
            //    Session["CD"] = "";
            //    Response.Redirect("~/Login.aspx?id=1");
            //}

            //Session["Name"] = dt.Rows[0][8].ToString();
            //Session["PAN"] = dt.Rows[0][1].ToString();
            //Session["ADD"] = dt.Rows[0][6].ToString();
            //Session["MOB"] = dt.Rows[0][5].ToString();
            //Session["Branch"] = dt.Rows[0][11].ToString();
            //Session["CD"] = dt.Rows[0][0].ToString(); // "100586";
            

            }
            catch
            {
                Response.Redirect("~/Login.aspx?id=5");
            }
        //}
        //catch
        //{
        //    Response.Redirect("~/Login.aspx?id=1");
        //}
        }
    //}

    private void MsgBox(string sMessage)
    {
        string msg = "<script language=\"javascript\">";
        msg += "alert('" + sMessage + "');";
        msg += "</script>";
        Response.Write(msg);
    }

    public static byte[] ConvertFromStringToHex(string inputHex)
    {

    inputHex = inputHex.Replace("-", "");

    byte[] resultantArray = new byte[inputHex.Length / 2];
    for (int i = 0; i < resultantArray.Length; i++)
    {
    resultantArray[i] = Convert.ToByte(inputHex.Substring(i * 2, 2), 16);
    }
    return resultantArray;
    }


    string HexStringToString(string hexString)
    {
        if (hexString == null || (hexString.Length & 1) == 1)
        {
            throw new ArgumentException();
        }
        var sb = new StringBuilder();
        for (var i = 0; i < hexString.Length; i += 2)
        {
            var hexChar = hexString.Substring(i, 2);
            sb.Append((char)Convert.ToByte(hexChar, 16));
        }
        return sb.ToString();
    }


}