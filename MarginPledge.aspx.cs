using Microsoft.Office.Interop.Excel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataTable = System.Data.DataTable;

public partial class MarginPledge : System.Web.UI.Page
{
    public string UCCID { get; set; }
    public string DPID { get; set; }
    public string REQID { get; set; }
    public string VERSION { get; set; }
    public string DETAILS { get; set; }


    private static readonly HttpClient client = new HttpClient();
    public class IsinDetails
    {
        public string prfnumber { get; set; }
        public string pledgorintref { get; set; }
        public string isinreqid { get; set; }
        public string isin { get; set; }
        public string quantity { get; set; }
        public string value { get; set; }
        public string segmentid { get; set; }
        public string cmid { get; set; }
        public string reasoncode { get; set; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Conn connn = new Conn();
        SqlConnection con = new SqlConnection(connn.Connn);
        SqlConnection conV = new SqlConnection(connn.Van);
        UCCID = Request.QueryString["Client"].ToString();
        SqlCommand cmd = new SqlCommand();
        cmd = new SqlCommand("Exec Dt_Load 301,'" + UCCID + "','','','',''", conV);
        DataSet ds = new DataSet();
        conV.Open();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        conV.Close();
        DataTable dt = ds.Tables[0];

        string dpid = "34600";
        string reqid = GetFormattedNonce();
        string version = "1.0";
        string details = OnlinePledge(dt);

        MarginInsert(dpid, reqid, version, details);

        Response.Redirect("PledgePopup.aspx");
    }

   

    private void MarginInsert(String dpid, String reqid, String version, String pledgedtls)
    {
        Conn connn = new Conn();
        SqlConnection con = new SqlConnection(connn.Connn);
        SqlConnection conV = new SqlConnection(connn.Van);
        SqlCommand cmd = new SqlCommand();
        try
        {
            if (conV.State == ConnectionState.Closed)
            {
                conV.Open();
            }

            cmd = new SqlCommand("EXEC MarginPledgeRequestData @DPID,@REQID,@VERSION,@DETAILS", conV);

            cmd.Parameters.AddWithValue("@DPID", dpid);
            cmd.Parameters.AddWithValue("@REQID", reqid);
            cmd.Parameters.AddWithValue("@VERSION", version);
            cmd.Parameters.AddWithValue("@DETAILS", pledgedtls);

            cmd.ExecuteNonQuery();
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
    }

    public string OnlinePledge(DataTable dataTable)
    {
        string apiKey = "1bjqt4zcitwawufuhip8ldcmdxwzsvm5";
        var reqDateTime = DateTime.Now.ToString("ddMMyyyyHHmmss");
        var reqDate = DateTime.Now.ToString("ddMMyyyy");

        // Build the IsinDetails list from the DataTable
        var isinDetailsList = new List<IsinDetails>();
        foreach (DataRow row in dataTable.Rows)
        {
            var isinDetails = new IsinDetails
            {
                prfnumber = row["PRFNonce"].ToString(),
                pledgorintref = row["PledgeNonce"].ToString(),
                isinreqid = row["FormattedNonce"].ToString(),
                isin = row["isin"].ToString(),
                quantity = row["quantity"].ToString(),
                value = row["value"].ToString(),
                segmentid = "FO",
                cmid = "M51255",
                reasoncode = "04"
            };
            isinDetailsList.Add(isinDetails);
        }

        var pledgeDetails = new
        {
            pledgeidentifier = "MP",
            reqtime = reqDateTime,
            pledgorboid = dataTable.Rows.Count > 0 ? dataTable.Rows[0]["DpId"].ToString() : string.Empty,
            pledgeeboid = "1203460000441213",
            executiondate = reqDate,
            expirydate = "31122050",
            uccid = UCCID.ToString(),
            exid = "12",
            entityidentifier = "TM",
            tmid = "11327",
            remarks = "RequestFromAPI",
            returnurl = "http://localhost:60028/PledgeStatus.aspx",
            isindtls = isinDetailsList
        };

        string jsonPledgeDetails = JsonConvert.SerializeObject(pledgeDetails);

        byte[] ApiKeyB = Encoding.UTF8.GetBytes(apiKey);
        byte[] JsonPledgeDetailsB = Encoding.UTF8.GetBytes(jsonPledgeDetails);

        byte[] encryptedPledgeDetailsB = EncryptAes256Cbc(JsonPledgeDetailsB, ApiKeyB);
        string encryptedPledgeDetailsS = Convert.ToBase64String(encryptedPledgeDetailsB);
        return encryptedPledgeDetailsS;
    }

    static string GetFormattedNonce()
    {
        string currentTime = DateTime.Now.ToString("ddMMyyyyHHmmssff");
        Random random = new Random();
        string randomNumber = random.Next(10000000, 99999999).ToString();
        string nonce = currentTime + randomNumber;
        return nonce;
    }

    static byte[] EncryptAes256Cbc(byte[] data, byte[] key)
    {
        using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
        {
            aesAlg.KeySize = 256;
            aesAlg.Mode = CipherMode.CBC;
            aesAlg.Padding = PaddingMode.PKCS7;
            byte[] iv = new byte[16];
            Array.Clear(iv, 0, iv.Length);
            // Use a null IV (Initialization Vector)
            aesAlg.IV = iv;

            using (ICryptoTransform encryptor = aesAlg.CreateEncryptor(key, aesAlg.IV))
            using (MemoryStream msEncrypt = new MemoryStream())
            using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
            {
                csEncrypt.Write(data, 0, data.Length);
                csEncrypt.FlushFinalBlock();
                return msEncrypt.ToArray();
                Console.WriteLine(msEncrypt.ToArray());
                Console.ReadLine();
            }
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