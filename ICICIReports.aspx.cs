using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class ICICIReports : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DateTime now = DateTime.Now;

            if (FDate.Text == "")
            {
                FDate.Text = Convert.ToDateTime(now).ToString("dd/MM/yyyy");
            }

            if (TDate.Text == "")
            {
                TDate.Text = Convert.ToDateTime(now).ToString("dd/MM/yyyy");
            }

            BindTransactionDropdown();
            BindRepeater(FDate.Text, TDate.Text, Select.SelectedValue);
        }
    }
    private void BindTransactionDropdown()
    {
        string connString = ConfigurationManager.AppSettings["brics"];

        using (MySqlConnection conn = new MySqlConnection(connString))
        {
            using (MySqlCommand cmd = new MySqlCommand("GetTypeofTransaction", conn))  
            {
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();

                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Add default "All" option at the top
                    DataRow newRow = dt.NewRow();
                    newRow["NameofTransaction"] = 0; 
                    newRow["NameofTransaction"] = "All"; 
                    dt.Rows.InsertAt(newRow, 0);

                    Select.DataSource = dt;
                    Select.DataTextField = "NameofTransaction"; 
                    Select.DataValueField = "NameofTransaction";  
                    Select.DataBind();
                }
            }
        }
    }

    protected void Submit_Click(object sender, EventArgs e)
    {
        BindRepeater(FDate.Text, TDate.Text, Select.SelectedValue);
    }
    private void BindRepeater(string fromDate, string ToDate,string Dropdown)

    {

        string connString = ConfigurationManager.AppSettings["brics"];

        using (MySqlConnection conn = new MySqlConnection(connString))

        {

            using (MySqlCommand cmd = new MySqlCommand("GetDataByTypeAndDate", conn))

            {

                DateTime fromDateTime = DateTime.ParseExact(fromDate, "dd/MM/yyyy", null);
                DateTime toDateTime = DateTime.ParseExact(ToDate, "dd/MM/yyyy", null);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("type", 1);
                cmd.Parameters.AddWithValue("fromdate", fromDateTime);
                cmd.Parameters.AddWithValue("todate", toDateTime);
                cmd.Parameters.AddWithValue("mode", Dropdown);
                

                conn.Open();

                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))

                {

                    DataTable dt = new DataTable();

                    da.Fill(dt);

                    dt.Columns.Add("BankName", typeof(string));

                    foreach (DataRow row in dt.Rows)

                    {

                        int bankid = Convert.ToInt32(row["bankid"]);

                        row["BankName"] = BankEnumType.GetBankName(bankid);

                    }

                    Repeater1.DataSource = dt;

                    Repeater1.DataBind();

                }

            }

        }

    }


    //private void BindRepeater(string fromDate, string ToDate, string selectValue)
    //{
    //    string connString = ConfigurationManager.AppSettings["brics"];
    //    using (MySqlConnection conn = new MySqlConnection(connString))
    //    {
    //        using (MySqlCommand cmd = new MySqlCommand("GetDataByTypeAndDate,'" + Select.Text + "'", conn))
    //        {
    //            cmd.CommandType = CommandType.StoredProcedure;

    //            cmd.Parameters.AddWithValue("type", 1);
    //            DateTime fromDateValue;
    //            DateTime toDateValue;

    //            if (DateTime.TryParse(fromDate, out fromDateValue))
    //            {
    //                cmd.Parameters.AddWithValue("fromdate", fromDateValue.ToString("yyyy-MM-dd"));
    //            }
    //            else
    //            {
    //                cmd.Parameters.AddWithValue("fromdate", DBNull.Value);
    //            }

    //            if (DateTime.TryParse(ToDate, out toDateValue))
    //            {
    //                cmd.Parameters.AddWithValue("todate", toDateValue.ToString("yyyy-MM-dd"));
    //            }
    //            else
    //            {
    //                cmd.Parameters.AddWithValue("todate", DBNull.Value);
    //            }

    //            conn.Open();
    //            using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
    //            {
    //                DataTable dt = new DataTable();
    //                da.Fill(dt);

    //                dt.Columns.Add("BankName", typeof(string));

    //                foreach (DataRow row in dt.Rows)
    //                {
    //                    int bankid = Convert.ToInt32(row["bankid"]);
    //                    row["BankName"] = BankEnumType.GetBankName(bankid);
    //                }

    //                Repeater1.DataSource = dt;
    //                Repeater1.DataBind();
    //            }
    //        }
    //    }
    //}

    protected void export_Click(object sender, EventArgs e)
    {
        BindRepeater(FDate.Text, TDate.Text, Select.SelectedValue);

        DataTable dt = (Repeater1.DataSource as DataTable);
        string filename = "BankTransaction_ICICI_" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";

        ExporttoExcel(dt, filename);
       
    }

    private void ExporttoExcel(System.Data.DataTable table, String Fnamer)
    {
        //DateTime dt = Convert.ToDateTime(FDate.Text);
        //DateTime Dt = Convert.ToDateTime(Session["Dt1"].ToString());

        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + Fnamer);

        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        //string img = "localhost/images/CentrumLogo.jpg";

        //HttpContext.Current.Response.Write("<img src='" + img + @"' align='center' style='position:static; height: 100%; width: 100%;'/>");
        //HttpContext.Current.Response.Write("<Table><tr><td colspan='4' style='text-align: center; vertical-align: middle;'></td> <td colspan='4' style='text-align: center; vertical-align: middle;'> <center><img src='http://www.centrumbroking.com/wp-content/uploads/2017/03/1new-logoCentrum-1-1.jpg' align='center' style='max-height:20px;max-width:90px;height:auto;width:auto;'/></center></td> <td colspan='10' style='text-align: center; vertical-align: middle;'></td></tr></Table>");
        //HttpContext.Current.Response.Write("<Table><tr><td colspan='18' align='center'><div align='center' style='text-align:center' width='70' height='80'><img src='http://www.centrumbroking.com/wp-content/uploads/2017/03/1new-logoCentrum-1-1.jpg'/></div></td></tr></Table>");
        HttpContext.Current.Response.Write("<br>");
        HttpContext.Current.Response.Write("<br>");
        HttpContext.Current.Response.Write("<br>");

        //HttpContext.Current.Response.Write("<Table><tr><td colspan='18' style='border:1px solid black;'><center><Lable style='font-size:15.0pt; font-family:Arial; background:white;'>Centrum Broking Ltd.</Lable></center> <center> <Lable style='font-size:9.0pt; font-family:Arial; background:white;'>Registered Office :Bombay Mutual Building, 2nd Floor, Dr D.N.Road, Fort, Mumbai 400 001</Lable></center> <center> <Lable style='font-size:9.0pt; font-family:Arial; background:white;'>Tel : 022-22662434 Fax : 022-22611105</Lable></center> <center> <Lable style='font-size:9.0pt; font-family:Arial; background:white;'>Website: www.centrumbroking.com</Lable></center>  <center> <Lable style='font-size:9.0pt; font-family:Arial; background:white;'>Compliance Officer: Ashok Devarajan Kadambi. Tel.No: +91 22 42159000 Extn: 9936 Email: compliance@centrum.co.in</Lable></center></td></tr><tr><td>&nbsp;</td></tr></Table>");

        //HttpContext.Current.Response.Write("<Table><tr><td colspan='9' style='text-align:left'><Lable style='font-size:15.0pt; font-family:Arial; background:white; text-align:left;'> Client Code : " + Session["CD"].ToString() + " </Lable></td>");
        //HttpContext.Current.Response.Write("<td colspan='9' style='text-align:right'><Lable style='font-size:15.0pt; font-family:Arial; background:white; text-align:right;'> Tel. No. : " + Session["MOB"].ToString() + " </Lable> </td></tr>");
        //HttpContext.Current.Response.Write("<tr><td colspan='9' style='text-align:left'><Lable style='font-size:15.0pt; font-family:Arial; background:white; text-align:left;'> Name : " + Session["Name"].ToString() + " </Lable></td>");
        //HttpContext.Current.Response.Write("<td colspan='9' style='text-align:right'><Lable style='font-size:15.0pt; font-family:Arial; background:white; text-align:right;'> Pan No. : " + Session["PAN"].ToString() + " </Lable> </td></tr>");
        //HttpContext.Current.Response.Write("<tr><td colspan='9' style='text-align:left'><Lable style='font-size:15.0pt; font-family:Arial; background:white; text-align:left;'> Address : " + Session["ADD"].ToString() + " </Lable></td>");
        //HttpContext.Current.Response.Write("<td colspan='9' style='text-align:right'><Lable style='font-size:15.0pt; font-family:Arial; background:white; text-align:right;'> GST No. :  </Lable> </td></tr></Table>");

        //HttpContext.Current.Response.Write("<Table><tr><td colspan='18' style='border:1px solid black;' ><center><Lable style='font-size:15.0pt; font-family:Arial; background:white;'>Statement of Holding As On " + Dt.ToString("dd/MMM/yyyy") + ".</Lable></center></td></tr></Table>");
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        //sets the table border, cell spacing, border color, font of the text, background, foreground, font height
        HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
          "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
          "style='font-size:10.0pt; font-family:Calibri; background:white;'> <TR>");
        //am getting my grid's column headers
        int columnscount = table.Columns.Count;

        //HttpContext.Current.Response.Write("<B>");
        //HttpContext.Current.Response.Write("<th>RequestID</th>");
        //HttpContext.Current.Response.Write("<th>Request Date</th>");
        //HttpContext.Current.Response.Write("<th>Client Code</th>");
        //HttpContext.Current.Response.Write("<th>Amount</th>");
        //HttpContext.Current.Response.Write("<th>Partial Amount</th>");
        //HttpContext.Current.Response.Write("<th>Status</th>");
        //HttpContext.Current.Response.Write("<th>Reason</th>");

        //HttpContext.Current.Response.Write("</B>");

        for (int j = 0; j < columnscount; j++)
        {      //write in new column
            HttpContext.Current.Response.Write("<Td>");
            //Get column headers  and make it as bold in excel columns
            HttpContext.Current.Response.Write("<B>");
            HttpContext.Current.Response.Write(table.Columns[j].Caption.ToString());
            HttpContext.Current.Response.Write("</B>");
            HttpContext.Current.Response.Write("</Td>");
        }
        HttpContext.Current.Response.Write("</TR>");

        foreach (DataRow row in table.Rows)
        {//write in new row
            HttpContext.Current.Response.Write("<TR>");
            for (int i = 0; i < table.Columns.Count; i++)
            {
                HttpContext.Current.Response.Write("<Td>");
                HttpContext.Current.Response.Write(row[i].ToString());
                HttpContext.Current.Response.Write("</Td>");
            }

            HttpContext.Current.Response.Write("</TR>");
        }
        HttpContext.Current.Response.Write("</Table>");

        HttpContext.Current.Response.Write("</font>");

        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.End();
    }
}