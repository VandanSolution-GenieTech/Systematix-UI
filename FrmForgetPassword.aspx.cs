using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using RestSharp;


public partial class FrmForgetPassword : System.Web.UI.Page
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

            }
            catch (Exception ex)
            {

            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        String Email;
        int Otp;

        if (User.Text == "")
        {

            MsgBox("Please Enter User ID");
            return;
        }

        Conn connn = new Conn();
        
        SqlConnection con = new SqlConnection(connn.Connn);
        SqlConnection conV = new SqlConnection(connn.Van);
        SqlCommand cmd = new SqlCommand();
        ////EXEC CENTTrx19.Dbo.btspCENTTrx 208.001, 11, 0, '2019-05-02', NULL, '100586', '', ''
        cmd = new SqlCommand("Exec Dt_Load 88,'','" + User.Text + "','','',''", conV);
        DataSet ds = new DataSet();
        conV.Open();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        DataTable dt = new DataTable();
        da.Fill(dt);
        conV.Close();

        if (dt.Rows.Count == 0)
        {
            MsgBox("User Id and Password is Invalid");
            Session["OTPUser"] = "";
            return;
        }

        Email = dt.Rows[0][0].ToString();



        Session["OTPUser"] = User.Text;


        var rand = new Random();
        Otp = rand.Next(1000000, 2000000);

        Session["OTP"] = Otp.ToString();

        ErrorMail(Email, Otp.ToString());

        Mobno.Text = "OTP Sent To " + Email.Substring(0, 2) + "******" + Email.Substring(Email.Length - 5);
        Mobno.Visible = true;

        MsgBox("OTP has been sent on Registered Email, Please check & Enter");


        User.Enabled = false;
        TextBox2.Enabled = true; 
        Password.Enabled = true;
        TextBox1.Enabled = true;
    }
    protected void Log_Click(object sender, EventArgs e)
    {
        if (TextBox2.Text != Session["OTP"].ToString())
        {
            MsgBox("Please Enter Correct OTP to proceed");
            return;
        }


        if (Password.Text == TextBox1.Text)
        {

            Conn connn = new Conn();
            SqlConnection con = new SqlConnection(connn.Connn);
            SqlConnection conV = new SqlConnection(connn.Van);
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("Exec Dt_Load 89,'" + Session["OTPUser"].ToString() + "','" + TextBox1.Text + "','','',''", conV);
            conV.Open();
            cmd.ExecuteNonQuery();
            conV.Close();
            MsgBox("Password Changed Successfully.");
        }
        else
        {
            MsgBox("OTP entered correctly, Inputted New Passwords Does Not Match. Please insert correctly");
        }
    }

    private void MsgBox(string sMessage)
    {
        string msg = "<script language=\"javascript\">";
        msg += "alert('" + sMessage + "');";
        msg += "</script>";
        Response.Write(msg);
    }

    public void ErrorMail(string to, string Otp)
    {

        MailMessage mail = new MailMessage();
        mail.To.Add(to);
        mail.From = new MailAddress("Fuel Admin<noreply-sfo@sparkcapital.in>");
        mail.Subject = "OTP For Fuel Password Reset";
        //string Body = "File Upload Confirmation Test";
        mail.Body = "Dear User,<br><br> As requested Your One Time Password is <b>" + Otp + "</b>to change the password on Spark Fuel-Admin Portal. <br /><br/>" + "Regards" + " <br />" + "Fuel Admin";
        mail.IsBodyHtml = true;
        SmtpClient smtp = new SmtpClient();
        smtp.Host = "smtp.trackcampaigns.com";
        smtp.Port = Convert.ToInt32(587);
        smtp.Credentials = new System.Net.NetworkCredential("_userd33fb7812b9c32ef9962a", "d33fb7812b9c32ef9962adc1d38458ce");
        //smtp.EnableSsl = true;
        smtp.Send(mail);
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Login.aspx");
    }
}