using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Net.Mail;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Net;
using DocumentFormat.OpenXml.Office.Word;

public partial class Index : System.Web.UI.Page
{

    public string newtest { get; set; }
    public string Active_RM { get; set; }
    public Decimal Holding1 { get; set; }
    public string Position { get; set; }
    public string Name { get; set; }

    public Decimal AUM { get; set; }
    public Decimal AUMA { get; set; }
    public string AftHC { get; set; }
    public Decimal T1Val { get; set; }

    public string Cash { get; set; }
    public string marginAVAILABLE { get; set; }
    public string UnClear { get; set; }
    public string Margin { get; set; }
    public Decimal Limit { get; set; }
    public Decimal EPNL { get; set; }
    public Decimal FOPNL { get; set; }
    public Decimal behHC { get; set; }




    public string Volume { get; set; }
    public string Volumeeq { get; set; }


    public string FTDClass { get; set; }
    public string WTDClass { get; set; }
    public string MTDClass { get; set; }
    public string YTDClass { get; set; }
    public string YRClass { get; set; }



    public string Revenue { get; set; }
    public string Revenueeq { get; set; }

    public string NewAcc { get; set; }
    public string NewAcceq { get; set; }

    public string GrossBrokrage { get; set; }

    public string TotalClient { get; set; }
    public string Active_Trading_Client { get; set; }
    public string ActiveClient { get; set; }
    public string DormantClient { get; set; }
    public string DPClient { get; set; }
    public string BaseUrl { get; set; }
    public string LaunchUid { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {

        LaunchUid = Session["Name"].ToString();
        BaseUrl = WebConfigurationManager.AppSettings["BaseUrl"];

        Session["CD"] = "";
        try
        {

            if (!Page.IsPostBack) // First time only 
            {
                EODMail();
                try
                {

                    if (Session["Type"].ToString() == "Admin")
                    {
                        newtest = "";
                    }
                    else
                    {
                        newtest = "Primary";
                    }

                    String T = "0";

                    try
                    {
                        T = (Request.QueryString["Typ"]);
                    }
                    catch
                    {

                    }

                    if (T == "0")
                    {
                        T = "FTD";
                    }

                    FClass(T);

                    DateTime now = DateTime.Now;
                    var startDate = new DateTime(now.Year, 4, 1);

                    int currQuarter = (now.Month - 1) / 3 + 1;

                    DateTime dtFirstDay = new DateTime(now.Year, 3 * currQuarter - 2, 1);

                    Session["FDt"] = dtFirstDay.ToString("dd/MM/yyyy");
                    Session["TDt"] = Convert.ToDateTime(now).ToString("dd/MM/yyyy");


                    Conn connn = new Conn();
                    SqlConnection con = new SqlConnection(connn.Connn);
                    SqlConnection conV = new SqlConnection(connn.Van);
                    SqlCommand cmd = new SqlCommand();

                    cmd = new SqlCommand("Select Replace(Convert(varchar(50),Dt,106),' ','-') from RunDate", conV);
                    conV.Open();
                    Session["EndDate"] = cmd.ExecuteScalar().ToString();
                    Session["Dt"] = cmd.ExecuteScalar().ToString();
                    Session["Dt1"] = cmd.ExecuteScalar().ToString();
                    conV.Close();

                    cmd = new SqlCommand("Select Replace(Convert(varchar(50),(Select DATEADD(month, DATEDIFF(month, 0, dt), 0)),106),' ','-') from RunDate", conV);
                    conV.Open();
                    Session["StartDate"] = cmd.ExecuteScalar().ToString();
                    conV.Close();



                    cmd = new SqlCommand("Exec Dt_Load 80,'" + Session["CD"].ToString() + "','" + T + "','','',''", conV);
                    DataSet ds = new DataSet();
                    conV.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    conV.Close();

                    Active_Trading_Client = ds.Tables[0].Rows[0][3].ToString();
                    Active_RM = ds.Tables[0].Rows[0][4].ToString();

                    Volume = ds.Tables[0].Rows[0][0].ToString();
                    Volumeeq = ds.Tables[0].Rows[0][0].ToString();

                    Revenue = (Math.Round(decimal.Parse(ds.Tables[0].Rows[0][1].ToString()) - decimal.Parse(ds.Tables[1].Rows[0][0].ToString()), 2)).ToString();
                    Revenueeq = (Math.Round(decimal.Parse(ds.Tables[0].Rows[0][1].ToString()) - decimal.Parse(ds.Tables[1].Rows[0][0].ToString()), 2)).ToString();


                    NewAcc = ds.Tables[2].Rows[0][1].ToString();
                    NewAcceq = ds.Tables[2].Rows[0][1].ToString();

                    GrossBrokrage = ds.Tables[0].Rows[0][2].ToString();

                    ActiveClient = ds.Tables[2].Rows[0][0].ToString();
                    TotalClient = ds.Tables[2].Rows[0][2].ToString();
                    DormantClient = ds.Tables[2].Rows[0][3].ToString();
                    DPClient = ds.Tables[2].Rows[0][4].ToString();
                    Repeater1.DataSource = ds.Tables[3];
                    Repeater1.DataBind();

                    Repeater2.DataSource = ds.Tables[4];
                    Repeater2.DataBind();

                    Repeater3.DataSource = ds.Tables[5];
                    Repeater3.DataBind();


                    //    Repeater3.DataSource = ds.Tables[5];
                    //    Repeater3.DataBind();

                    //    Repeater4.DataSource = ds.Tables[6];
                    //    Repeater4.DataBind();
                }
                catch (Exception ex)
                {
                    MsgBox(ex.Message);
                }

            }

        }
        catch
        {

        }
    }
    private void EODMail()
    {
        try
        {
            Conn connn = new Conn();
            SqlConnection conV = new SqlConnection(connn.Van);
            conV.Open();
            SqlCommand cmdDt = new SqlCommand("Exec EODMailer", conV);
            cmdDt.CommandTimeout = 180;
            //errorlog("Query Excuted");
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmdDt);
            da.Fill(dt);
            //errorlog("Data Filled");
            if (dt.Rows.Count > 0)
            {
                string Tablename = dt.Rows[0]["Tablename"].ToString();
                //errorlog("Count is Equal to Zero");
                MailMessage mail = new MailMessage();
                //mail.To.Add("himanshu.s@sparkcapital.in");
                mail.To.Add("Himanshu Suthar<himanshu.s@sparkcapital.in>");
                //mail.CC.Add("Maheshkumar<maheshkumar@sparkcapital.in>");
                mail.From = new MailAddress("Fuel BOD<noreply-sfo@sparkcapital.in>");
                mail.Subject = "Fuel BOD Failed Tablename :- " + Tablename + " ";
                // Initialize email body
                string body = "Hii Team,<br>";
                body += "</br>Please Connect With Fuel Teams,Today BOD is Failed Due to Rows not insert Properly into " + Tablename + "<br><br>";
                body += "<table style='border: 1px solid black; border-collapse: collapse; width: 100%; text-align: center;'>" +
                    "<tr>" +
                    "<th style='border: 1px solid black; text-align: center;'>ID</th>" +
                    "<th style='border: 1px solid black; text-align: center;'>Counts</th>" +
                    "<th style='border: 1px solid black; text-align: center;'>DateTime</th>" +
                    "<th style='border: 1px solid black; text-align: center;'>Tablename</th>" +
                    "</tr>";

                using (SqlDataReader readerDt = cmdDt.ExecuteReader())
                {
                    while (readerDt.Read())
                    {
                        body += "<tr>" +
                                "<td style='border: 1px solid black; text-align: left;'>&nbsp;" + readerDt["Id"] + "</td>" +
                                    "<td style='border: 1px solid black; text-align: left;'>&nbsp;" + readerDt["Counts"] + "</td>" +
                                    "<td style='border: 1px solid black; text-align: left;'>&nbsp;" + readerDt["dt"] + "</td>" +
                                    "<td style='border: 1px solid black; text-align: left;'>&nbsp;" + readerDt["Tablename"] + "</td>" +
                                    "</tr>";
                    }
                }



                body += "</table>";
                body += "<br><br>This is an automated email. In case there are any issues please connect with IT Team.";
                body += "<br><br>Regards,<br>Fuel Admin";
                mail.Body = body;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.trackcampaigns.com";
                smtp.Port = Convert.ToInt32(587);
                smtp.Credentials = new System.Net.NetworkCredential("_userd33fb7812b9c32ef9962a", "d33fb7812b9c32ef9962adc1d38458ce");
                //smtp.EnableSsl = true;
                smtp.Send(mail);
                //errorlog("Mail Send");
                conV.Close();
            }
        }
        catch (Exception ex)
        {
            //logfile(ex.StackTrace);
            //errorlog(ex.Message);
        }
    }
    private void PMSmailer()
    {
        Conn connn = new Conn();
        SqlConnection conV = new SqlConnection(connn.Van);
        try
        {
            SqlCommand cmdDt = new SqlCommand("Exec PMSMailer", conV);
            cmdDt.CommandTimeout = 180;
            System.Data.DataTable datat = new System.Data.DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmdDt);
            conV.Open();
            da.Fill(datat);
            if (datat.Rows.Count > 0)
            {

                MailMessage mail = new MailMessage();
                mail.To.Add("sumeet.aqm@sparkcapital.in");
                mail.CC.Add("Himanshu.s@sparkcapital.in,maheshkumar@sparkcapital.in");
                mail.From = new MailAddress("Fuel Admin<noreply-sfo@sparkcapital.in>");
                mail.Subject = "Missing CP code in PMS Trades";
                // Initialize email body
                string body = "Hii Team,<br>";
                body += "</br>Please find below PMS trades with missing CP Codes. Request you to kindly verify the same and take necessary corrective actions.<br><br>";
                body += "<table style='border: 1px solid black; border-collapse: collapse; width: 100%; text-align: center;'>" +
                    "<tr>" +
                    "<th style='border: 1px solid black; text-align: center;'>Dealer Terminal</th>" +
                    "<th style='border: 1px solid black; text-align: center;'>Order Time</th>" +
                    "<th style='border: 1px solid black; text-align: center;'>Trade ID</th>" +
                    "<th style='border: 1px solid black; text-align: center;'>Client ID</th>" +
                    "<th style='border: 1px solid black; text-align: center;'>Client Name</th>" +
                    "<th style='border: 1px solid black; text-align: center;'>Trade Type</th>" +
                    "<th style='border: 1px solid black; text-align: center;'>Trade Segment</th>" +
                    "<th style='border: 1px solid black; text-align: center;'>Symbol</th>" +
                    "<th style='border: 1px solid black; text-align: center;'>Qty</th>" +
                    "<th style='border: 1px solid black; text-align: center;'>Price</th>" +
                    "</tr>";

                // Retrieve data from database




                using (SqlDataReader readerDt = cmdDt.ExecuteReader())
                {
                    while (readerDt.Read())
                    {
                        body += "<tr>" +
                                "<td style='border: 1px solid black; text-align: left;'>&nbsp;" + readerDt["Dealer_Terminal"] + "</td>" +
                                    "<td style='border: 1px solid black; text-align: left;'>&nbsp;" + readerDt["OrderTime"] + "</td>" +
                                    "<td style='border: 1px solid black; text-align: left;'>&nbsp;" + readerDt["TradeID"] + "</td>" +
                                    "<td style='border: 1px solid black; text-align: left;'>&nbsp;" + readerDt["FTlogin"] + "</td>" +
                                    "<td style='border: 1px solid black; text-align: left;'>&nbsp;" + readerDt["Accountname"] + "</td>" +
                                    "<td style='border: 1px solid black; text-align: left;'>&nbsp;" + readerDt["BuySell"] + "</td>" +
                                    "<td style='border: 1px solid black; text-align: left;'>&nbsp;" + readerDt["Tradesegment"] + "</td>" +
                                    "<td style='border: 1px solid black; text-align: left;'>&nbsp;" + readerDt["Symbol"] + "</td>" +
                                     "<td style='border: 1px solid black; text-align: right;'>" + readerDt["Qty"] + "&nbsp;</td>" +
                                      "<td style='border: 1px solid black; text-align: right;'>" + readerDt["Rate"] + "&nbsp;</td>" +
                                    "</tr>";
                    }
                }



                body += "</table>";
                body += "<br><br>This is an automated email. In case there are any issues please connect with IT Team.";
                body += "<br><br>Regards,<br>Fuel Admin";
                mail.Body = body;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.trackcampaigns.com";
                smtp.Port = Convert.ToInt32(587);
                smtp.Credentials = new System.Net.NetworkCredential("_userd33fb7812b9c32ef9962a", "d33fb7812b9c32ef9962adc1d38458ce");
                //smtp.EnableSsl = true;
                smtp.Send(mail);
                conV.Close();
            }
        }
        catch (Exception ex)
        {

        }

    }
    private void FClass(string sMessage)
    {
        if (sMessage == "FTD")
        {
            FTDClass = "btn btn-white active";
            WTDClass = "btn btn-white";
            MTDClass = "btn btn-white";
            YTDClass = "btn btn-white";
            YRClass = "btn btn-white";
        }

        if (sMessage == "WTD")
        {
            FTDClass = "btn btn-white";
            WTDClass = "btn btn-white active";
            MTDClass = "btn btn-white";
            YTDClass = "btn btn-white";
            YRClass = "btn btn-white";
        }


        if (sMessage == "MTD")
        {
            FTDClass = "btn btn-white";
            WTDClass = "btn btn-white";
            MTDClass = "btn btn-white active";
            YTDClass = "btn btn-white";
            YRClass = "btn btn-white";
        }


        if (sMessage == "YTD")
        {
            FTDClass = "btn btn-white";
            WTDClass = "btn btn-white";
            MTDClass = "btn btn-white";
            YTDClass = "btn btn-white active";
            YRClass = "btn btn-white";
        }


        if (sMessage == "1YER")
        {
            FTDClass = "btn btn-white";
            WTDClass = "btn btn-white";
            MTDClass = "btn btn-white";
            YTDClass = "btn btn-white";
            YRClass = "btn btn-white active";
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