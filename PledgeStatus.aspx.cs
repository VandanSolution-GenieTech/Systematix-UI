using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web.Configuration;
using System.Web.UI;

public partial class PledgeStatus : System.Web.UI.Page
{
    public String Dateformat { get; set; }

    public class IsinResDtls
    {
        public string isinreqid { get; set; }
        public string isinresid { get; set; }
        public string isin { get; set; }
        public string quantity { get; set; }
        public string status { get; set; }
        public string errorcode { get; set; }
        public string errormsg { get; set; }
    }

    public class PledgeResDtls
    {
        public string reqid { get; set; }
        public string pledgeidentifier { get; set; }
        public string resid { get; set; }
        public string restime { get; set; }
        public string resstatus { get; set; }
        public string reserror { get; set; }
        public string reserrmsg { get; set; }
        public string remarks { get; set; }
        public List<IsinResDtls> isinresdtls { get; set; }
    }

    public class RootObject
    {
        public PledgeResDtls pledgeresdtls { get; set; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Dateformat = WebConfigurationManager.AppSettings["Dateformat"];
        if (!Page.IsPostBack) // First time only 
        {
            Conn connn = new Conn();
            SqlConnection con = new SqlConnection(connn.Connn);
            SqlConnection conV = new SqlConnection(connn.Van);
            SqlCommand cmd;

            try
            {
                string reqid = "";
                string pledgeidentifier = "";
                string pledgeresdtls = "";

                NameValueCollection nvc = Request.Form;
                if (Request.Params["reqid"] != null)
                {
                    reqid = Request.Params["reqid"].ToString();
                    pledgeidentifier = Request.Params["pledgeidentifier"].ToString();
                    pledgeresdtls = Request.Params["pledgeresdtls"].ToString();

                    string apiKey = "1bjqt4zcitwawufuhip8ldcmdxwzsvm5";
                    byte[] apiKeyBytes = Encoding.UTF8.GetBytes(apiKey);
                    byte[] encryptedResponse = Convert.FromBase64String(pledgeresdtls);
                    byte[] decryptedData = DecryptAes256Cbc(encryptedResponse, apiKeyBytes);
                    string decryptedResponse = Encoding.UTF8.GetString(decryptedData);

                    var responseObject = JsonConvert.DeserializeObject<RootObject>(decryptedResponse);
                    var responseFinal = responseObject.pledgeresdtls;

                    foreach (var isinDetail in responseFinal.isinresdtls)
                    {
                        using (cmd = new SqlCommand("exec Proc_UpdateOnlinePledgeRequests @Response, @Isinreqid, @Reqid, @Resid, @ResponseStatus, @ResponseMsg, @IsinResponseid, @IsinStatus", conV))
                        {
                            // Set parameter values
                            cmd.Parameters.AddWithValue("@Response", decryptedResponse);
                            cmd.Parameters.AddWithValue("@Isinreqid", isinDetail.isinreqid);
                            cmd.Parameters.AddWithValue("@Reqid", responseFinal.reqid);
                            cmd.Parameters.AddWithValue("@Resid", responseFinal.resid);
                            cmd.Parameters.AddWithValue("@ResponseStatus", responseFinal.resstatus);
                            cmd.Parameters.AddWithValue("@ResponseMsg", responseFinal.reserrmsg);
                            cmd.Parameters.AddWithValue("@IsinResponseid", isinDetail.isinresid);
                            cmd.Parameters.AddWithValue("@IsinStatus", isinDetail.status);

                            //try
                            //{
                            //    conV.Open();
                            //    using (SqlDataReader reader = cmd.ExecuteReader())
                            //    {
                            //        if (reader.Read())
                            //        {
                            //            string userid = reader["Userid"].ToString();
                            //            string password = reader["Password"].ToString();
                            //            AutoLogin(userid, password);
                            //        }
                            //    }
                            //    conV.Close();
                            //}
                            //catch (Exception ex)
                            //{
                            //    if (Session["CD"] == "")
                            //    {
                            //        Response.Redirect("~/Login.aspx");
                            //    }
                            //    else
                            //    {
                            //        Response.Redirect("~/Login.aspx");
                            //    }
                            //}
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox(ex.Message);
            }
        }
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
    static byte[] DecryptAes256Cbc(byte[] data, byte[] key)
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

            using (ICryptoTransform decryptor = aesAlg.CreateDecryptor(key, aesAlg.IV))
            using (MemoryStream msDecrypt = new MemoryStream(data))
            using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
            using (MemoryStream msOutput = new MemoryStream())
            {
                csDecrypt.CopyTo(msOutput);
                return msOutput.ToArray();
            }
        }
    }

    private void AutoLogin(string Userid, string Password)
    {
        try
        {
            DateTime now = DateTime.Now;
            DateTime startDate = new DateTime(2030, 12, 30);

            if (now > startDate)
            {
                MsgBox("The License of Software has Expired!");
                Session["CD"] = "";
                return;
            }

            Conn connn = new Conn();
            SqlConnection con = new SqlConnection(connn.Connn);
            SqlConnection conV = new SqlConnection(connn.Van);
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("Exec Dt_Load 30,'" + Userid + "','','','" + Password + "',''", conV);
            DataSet ds = new DataSet();
            conV.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            DataTable dt = new DataTable();
            da.Fill(dt);
            conV.Close();


            if (dt.Rows.Count > 0)
            {


                if (dt.Rows[0][0].ToString() == "0")
                {
                    MsgBox("User Id and Password is Invalid");
                    Session["CD"] = "";
                    return;
                }

                // Trading Clients

                Session["Name"] = dt.Rows[0][8].ToString();
                Session["LaunchUid"] = dt.Rows[0][0].ToString();
                Session["PAN"] = dt.Rows[0][1].ToString();
                Session["ADD"] = dt.Rows[0][6].ToString();
                Session["MOB"] = dt.Rows[0][5].ToString();
                Session["Email"] = dt.Rows[0][4].ToString();
                Session["Branch"] = dt.Rows[0][11].ToString();
                Session["CD"] = dt.Rows[0][0].ToString(); // "100586";


            }

            // Common for Trading and DP-Only Client
            Session["Admin"] = "0";
            Session["LUP"] = "";
            string fname = "";

            string[] strSplit = Session["Name"].ToString().Split();
            foreach (string res in strSplit)
            {
                if (res != "")
                {
                    fname = fname + res.Substring(0, 1);
                }
            }

            if (fname.Length > 2)
            {
                fname = fname.Substring(0, 2);
            }

            Session["Fame"] = fname;


            var startDate1 = new DateTime(now.Year, 4, 1);

            String FDate;

            if (now.Month > 3)
            {
                FDate = Convert.ToDateTime(startDate1).ToString("MM/dd/yyyy");
            }
            else
            {
                FDate = Convert.ToDateTime(startDate1.AddYears(-1)).ToString("MM/dd/yyyy");
            }

            String TDate = Convert.ToDateTime(now).ToString();

            DateTime Dt = Convert.ToDateTime(FDate);
            DateTime Dt1 = Convert.ToDateTime(TDate);

            Session["Dt"] = Dt.ToString("dd/MMM/yyyy");
            Session["Dt1"] = Dt1.ToString("dd/MMM/yyyy");
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