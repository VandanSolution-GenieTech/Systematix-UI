using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO.Compression;
using Microsoft.Win32;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.Globalization;
using iTextSharp.text.html;
using System.IO;
using System.Text;
using System.Reflection;
using DamienG.Security.Cryptography;

public partial class Login : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            
            try
            {
                String BK = (Request.QueryString["id"]);
                if (BK == "1")
                {
                    logoutlab.Visible = true;
                }
                if (BK == "2")
                {
                    Timeout1.Visible = true;
                }

            }
            catch (Exception ex)
            {

            }

            

        }
        
    }

    protected void Log_Click(object sender, EventArgs e)
    {

            Conn connn = new Conn();
            SqlConnection con = new SqlConnection(connn.Connn);
            SqlConnection conV = new SqlConnection(connn.Van);
            SqlCommand cmd = new SqlCommand();
            //EXEC CENTTrx19.Dbo.btspCENTTrx 208.001, 11, 0, '2019-05-02', NULL, '100586', '', ''
            cmd = new SqlCommand("Exec Dt_Load 83,'" + User.Text + "','" + Password.Text + "','','',''", conV);
            DataSet ds = new DataSet();
            conV.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            DataTable dt = new DataTable();
            da.Fill(dt);
            conV.Close();

        //if (User.Text == "Admin" & Password.Text == "admincent1234")
        //    {
        //        Response.Redirect("~/WithdrawalProcess.aspx");
        //    }

        
            if (dt.Rows.Count == 0)
            {
                MsgBox("User Id and Password is Invalid");
                Session["CD"] = "";
                return;
            }
            Session["Username"] = ds.Tables[0].Rows[0][5];
            Session["Name"] = User.Text;
            Session["Fame"] = User.Text;
            Session["Type"] = dt.Rows[0][2].ToString();
            Session["Email"] = dt.Rows[0][4].ToString();
        Session["Role"] = User.Text;
        Session["CD"] = "MUB1R20";
        DateTime now = DateTime.Now;
        DateTime startDate = new DateTime(2030, 12, 30);
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

        if (dt.Rows[0][2].ToString() == "Admin")
            {
                Session["User"] = "Admin";
            }
            else
            {
                Session["User"] = dt.Rows[0][0].ToString();
            Session["upass"] = dt.Rows[0][1].ToString();
            }
            



            //Session["Admin"] = "0"; // "100586";

            //DateTime now = DateTime.Now;
            //DateTime startDate = new DateTime(2022, 06, 30);

            //if (now > startDate)
            //{
            //    MsgBox("User Id and Password is Invalid");
            //    Session["CD"] = "";
            //    return;
            //}


            //if (dt.Rows[0][0].ToString() == "0")
            //{
            //    cmd = new SqlCommand("Exec Dt_Load 51,'" + User.Text + "','" + Password.Text + "','','',''", conV);
            //    DataSet ds1 = new DataSet();
            //    conV.Open();
            //    SqlDataAdapter da1 = new SqlDataAdapter(cmd);
            //    da1.Fill(ds1);
            //    DataTable dt1 = new DataTable();
            //    da1.Fill(dt1);
            //    conV.Close();
            //    if (dt1.Rows[0][0].ToString() == "1")
            //    {
            //        Session["Admin"] = "1"; // "100586";
            //        //Response.Redirect("~/AdminIndex.aspx");

            //        Session["Name"] = "Call Center";
            //        Session["PAN"] = "";
            //        Session["ADD"] = "";
            //        Session["MOB"] = "";
            //        Session["Branch"] = "";
            //        Session["CD"] = "A18084";
            //        Response.Redirect("~/Index.aspx?Typ=MTD");
       

            //    }

            //    else
            //    {

            //        MsgBox("User Id and Password is Invalid");
            //        Session["CD"] = "";
            //        return;
            //    }
            //}


            

            //Session["Name"] = dt.Rows[0][8].ToString();
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

        //Session["PAN"] = dt.Rows[0][1].ToString();
        //Session["ADD"] = dt.Rows[0][6].ToString();
        //Session["MOB"] = dt.Rows[0][5].ToString();
        //Session["Branch"] = dt.Rows[0][11].ToString();
        //Session["CD"] = dt.Rows[0][0].ToString(); // "100586";
        //Session["LUP"] = "";
        if (dt.Rows[0][3].ToString() == "Active")
        {

       
            if (dt.Rows[0][2].ToString() == "Admin")
            {
                Response.Redirect("~/allocationfileupload.aspx");
            }
            else
            {
                Response.Redirect("~/RMDashboard.aspx?Typ=Primary RM");
            }
        }
        else
        {
            MsgBox("Your login has been deactivated. Please contact Fuel Admin");
           
        }




    }


    private void MsgBox(string sMessage)
    {
        string msg = "<script language=\"javascript\">";
        msg += "alert('" + sMessage + "');";
        msg += "</script>";
        Response.Write(msg);
    }






    protected void Button1_Click(object sender, EventArgs e)
    {
        Conn connn = new Conn();
        SqlConnection con = new SqlConnection(connn.Connn);
        SqlConnection conV = new SqlConnection(connn.Van);
        SqlCommand cmd = new SqlCommand();

        cmd = new SqlCommand("Select Top 1 DocContent From Contract_Test24Dec2020", conV);
        DataSet ds5 = new DataSet();
        conV.Open();
        SqlDataAdapter da5 = new SqlDataAdapter(cmd);
        da5.Fill(ds5);
        conV.Close();


        byte[] bytes;
        bytes = (byte[])ds5.Tables[0].Rows[0][0];
        WriteUZipFile("Test.pdf", bytes);

    }

    private byte[] GetUZipBytes(byte[] fbytes)
    {
        MemoryStream s = new MemoryStream(fbytes);
        Ionic.Zlib.GZipStream gz = new Ionic.Zlib.GZipStream(s, Ionic.Zlib.CompressionMode.Decompress);
        byte[] UzipBytes;
        using (MemoryStream s2 = new MemoryStream())
        {
            gz.CopyTo(s2);
            UzipBytes = s2.ToArray();
        }
        return UzipBytes;
    }

    private void WriteUZipFile(string MyFile, byte[] fbytes)
    {

        try
        {
            // -- Unzip varbinary
            byte[] UZipBytes = GetUZipBytes(fbytes);
            // -- Write the file 
            //Server.MapPath("~/Files/Lincoln.txt")





            Response.BufferOutput = true;
            Response.Clear();

            //Response.ContentType = "text/pdf";
            //Response.AddHeader("Expires", "0");
            //Response.AddHeader("Cache-Control", "must-revalidate, post-check=0, pre-check=0");
            //Response.AddHeader("Pragma", "public");
            //Response.AddHeader("Content-Disposition", "attachment; filename=" + MyFile);
            ////Response.BinaryWrite(UZipBytes);


 


            //Response.ContentType = "application/pdf";
            //Response.AddHeader("content-disposition", "attachment;filename=Holding.pdf");

            StringReader sb = new StringReader(Encoding.Default.GetString(UZipBytes));


            string ss = sb.GetType().GetField("_s", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).GetValue(sb).ToString();

            ss = (ss.Substring(ss.IndexOf("<body>"), ss.Length - ss.IndexOf("<body>"))).Replace("</html>", "").Replace("<body>", "").Replace("</body>", "").Replace("<tr></tr>", "").Substring(0, 821) + "</main>";

            StringReader sr = new StringReader(ss);

            //StringReader sr1 = s




            iTextSharp.text.Rectangle rec = new Rectangle(400,300);
            Document pdfDoc = new Document(rec);

            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            byte[] bytes;

            using (MemoryStream memoryStream = new MemoryStream())
            {
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
                pdfDoc.Open();
                htmlparser.Parse(sr);
                pdfDoc.Close();

                bytes = memoryStream.ToArray();
                memoryStream.Close();
            }

            //Response.Clear();
            //Response.ContentType = "application/pdf";
            //Response.AddHeader("Content-Disposition", "attachment;filename=Test.pdf");
            //Response.Buffer = true;
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //Response.BinaryWrite(bytes);
            //Response.End();
            //Response.Close();


            //Document pdfDoc = new Document(PageSize.A4, 5f, 5f, 10f, 0f);
            //HTMLWorker htmlparser = new HTMLWorker(pdfDoc);

            //PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            //pdfDoc.Open();
            //htmlparser.Parse(sr);
            //pdfDoc.Close();
            //Response.Write(pdfDoc);
            //Response.End();

            //Response.Flush();
            //Response.End();

            //string pathUser = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            //string pathDownload = Path.Combine(pathUser, "Downloads\\");
            //File.WriteAllBytes("attachment;filename=Reports.pdf", UZipBytes);

            //string downloadsPath = pathDownload + MyFile;
            //FileStream fs = new FileStream(downloadsPath, FileMode.Create, FileAccess.Write);
            //fs.Write(UZipBytes, 0, UZipBytes.Length);
            //fs.Flush();
            //fs.Close();
            //fs = null;
            //Lab.Text = "File Saved As " //MyFile;
            //Lab.Visible = true;
            ///MsgBox("File Saved To " + downloadsPath);
            ///

        }

        catch (Exception ex)
        {
            //MsgBox(ex.Message);
        }
    }
}