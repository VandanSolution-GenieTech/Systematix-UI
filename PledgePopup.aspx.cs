using System;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Text;

public partial class PledgePopup : System.Web.UI.Page
{
    public string DPID { get; set; }
    public string REQID { get; set; }
    public string VERSION { get; set; }
    public string DETAILS { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        Conn connn = new Conn();
        SqlConnection con = new SqlConnection(connn.Connn);
        SqlConnection conV = new SqlConnection(connn.Van);
        SqlCommand cmd;
        try
        {
            if (conV.State == ConnectionState.Closed)
            {
                conV.Open();
            }

            cmd = new SqlCommand("EXEC MarginPledgeRequestDataLoad", conV);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    DPID = reader["DPID"].ToString();
                    REQID = reader["REQID"].ToString();
                    VERSION = reader["VERSION"].ToString();
                    DETAILS = reader["DETAILS"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            MsgBox(ex.Message);
            if (conV.State == ConnectionState.Open)
            {
                conV.Close();
            }
        }
        finally
        {
            if (conV.State == ConnectionState.Open)
            {
                conV.Close();
            }
        }
        PostData(DPID, REQID, VERSION, DETAILS);
    }
    private void PostData(String dpid, String reqid, String version, String pledgedtls)
    {
        try
        {
            NameValueCollection collections = new NameValueCollection();
            collections.Add("dpid", dpid);
            collections.Add("reqid", reqid);
            collections.Add("version", version);
            collections.Add("pledgedtls", pledgedtls);

            //string remoteUrl = "https://api.cdslindia.com/APIServices/pledgeapi/pledgesetup";
            string remoteUrl = "https://mockapiweb.cdslindia.com/APIServices/pledgeapi/pledgesetup";

            string html = "<html><head>";
            html += "</head><body onload='document.forms[0].submit()'>";
            html += string.Format("<form name='frmpledge' method='POST' action='{0}'>", remoteUrl);
            foreach (string key in collections.Keys)
            {
                html += string.Format("<input type='hidden' name='{0}' value='{1}'>", key, collections[key]);
            }
            html += "</form></body></html>";

            Response.Clear();
            Response.ContentEncoding = Encoding.GetEncoding("ISO-8859-1");
            Response.HeaderEncoding = Encoding.GetEncoding("ISO-8859-1");
            Response.Charset = "ISO-8859-1";
            Response.ContentType = "text/html"; // Ensure the content type is set to HTML
            Response.Write(html);
        }
        catch (Exception ex)
        {
            MsgBox(ex.Message);
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