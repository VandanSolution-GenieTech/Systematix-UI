using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Microsoft.Office.Interop.Excel;
using System.Globalization;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.InteropServices;
using System.Web.Configuration;
using Org.BouncyCastle.Bcpg;
using System.Data.OleDb;

public partial class frmFtHoldingUpload : System.Web.UI.Page
{
    public String Dateformat { get; set; }
    public string FileName { get; private set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        Dateformat = WebConfigurationManager.AppSettings["Dateformat"];

        if (!Page.IsPostBack)
        {

            DropDownList1.Items.Add("Today");
            DropDownList1.Items.Add("Prev Day");
            DropDownList1.Items.Add("This Month");
            DropDownList1.Items.Add("Prev. Month");
            DropDownList1.Items.Add("This Quarter");
            DropDownList1.Items.Add("Prev Quarter");
            DropDownList1.Items.Add("This FY");
            DropDownList1.Items.Add("Prev FY");
            DropDownList1.Text = "Today";

            DateTime now = DateTime.Now;
            var startDate = new DateTime(now.Year, 4, 1);

            int currQuarter = (now.Month - 1) / 3 + 1;

            DateTime dtFirstDay = new DateTime(now.Year, 3 * currQuarter - 2, 1);
            if (WDate.Text == "")
            {
                WDate.Text = Convert.ToDateTime(now).ToString("dd/MM/yyyy");
            }
            if (ORMTXT.Text == "")
            {
                ORMTXT.Text = Convert.ToDateTime(now).ToString("dd/MM/yyyy");
            }

            if (FDate.Text == "")
            {
                FDate.Text = Convert.ToDateTime(now).ToString("dd/MM/yyyy");
            }

            if (TDate.Text == "")
            {
                TDate.Text = Convert.ToDateTime(now).ToString("dd/MM/yyyy");
            }

        }
        String Role = Session["Type"].ToString();
        String userid = Session["User"].ToString();
        if (userid == "Kushay@sparkcapital.in")
        {
            ORMTXTBTN1.Visible = true;
            ORMTXTBTN2.Visible = true;
            ORMTXT.Visible = true;


        }

        else
        {
            ExportBOD.Visible = false;
            Button2.Visible = false;
            DPtrade.Visible = true;

        }

        if (Role == "Admin")
        {
            ExportBOD.Visible = true;
            Button2.Visible = true;
            DPtrade.Visible = false;


        }
    }



    protected void Exchange_SelectedIndexChanged(object sender, EventArgs e)
    {

        DateTime now = DateTime.Now;
        var startDate = new DateTime(now.Year, 4, 1);

        if (now.Month > 3)
        {
            startDate = new DateTime(now.Year, 4, 1);
        }
        else
        {
            startDate = new DateTime(now.AddYears(-1).Year, 4, 1);
        }


        var startDateM = new DateTime(now.Year, now.Month, 1);
        int currQuarter = (now.Month - 1) / 3 + 1;
        DateTime dtFirstDay = new DateTime(now.Year, 3 * currQuarter - 2, 1);

        if (FDate.Text == "")
        {
            FDate.Text = dtFirstDay.ToString("dd/MM/yyyy"); //Convert.ToDateTime(startDate).ToString("dd/MM/yyyy");
        }

        if (TDate.Text == "")
        {
            TDate.Text = Convert.ToDateTime(now).ToString("dd/MM/yyyy");
        }


        if (DropDownList1.Text == "Today")
        {
            FDate.Text = Convert.ToDateTime(now).ToString("dd/MM/yyyy");
            TDate.Text = Convert.ToDateTime(now).ToString("dd/MM/yyyy");
        }

        if (DropDownList1.Text == "Prev Day")
        {
            FDate.Text = Convert.ToDateTime(now).AddDays(-1).ToString("dd/MM/yyyy");
            TDate.Text = Convert.ToDateTime(now).AddDays(-1).ToString("dd/MM/yyyy");
        }


        if (DropDownList1.Text == "This Month")
        {
            FDate.Text = Convert.ToDateTime(startDateM).ToString("dd/MM/yyyy");
            TDate.Text = Convert.ToDateTime(now).ToString("dd/MM/yyyy");
        }

        if (DropDownList1.Text == "Prev. Month")
        {
            DateTime curDate = DateTime.Now;
            DateTime startDate1 = curDate.AddMonths(-1).AddDays(1 - curDate.Day);
            FDate.Text = DateTime.Now.AddDays(1 - DateTime.Now.Day).AddMonths(-1).ToString("dd/MM/yyyy");
            TDate.Text = DateTime.Now.AddDays(1 - DateTime.Now.Day).AddMonths(-1).AddMonths(1).AddDays(-1).ToString("dd/MM/yyyy");
        }


        if (DropDownList1.Text == "This Quarter")
        {
            FDate.Text = dtFirstDay.ToString("dd/MM/yyyy");
            TDate.Text = Convert.ToDateTime(now).ToString("dd/MM/yyyy");
        }

        if (DropDownList1.Text == "Prev Quarter")
        {

            currQuarter = (dtFirstDay.AddDays(-1).Month - 1) / 3 + 1;
            DateTime dtFirstDay1 = new DateTime(dtFirstDay.AddDays(-1).Year, 3 * currQuarter - 2, 1);

            FDate.Text = dtFirstDay1.ToString("dd/MM/yyyy");
            TDate.Text = Convert.ToDateTime(dtFirstDay.AddDays(-1)).ToString("dd/MM/yyyy");

        }

        if (DropDownList1.Text == "This FY")
        {
            FDate.Text = startDate.ToString("dd/MM/yyyy");
            TDate.Text = Convert.ToDateTime(now).ToString("dd/MM/yyyy");
        }

        if (DropDownList1.Text == "Prev FY")
        {
            FDate.Text = startDate.AddYears(-1).ToString("dd/MM/yyyy");
            TDate.Text = Convert.ToDateTime(startDate.AddYears(-1).AddYears(1).AddDays(-1)).ToString("dd/MM/yyyy");
        }

        DateTime Dt = DateTime.ParseExact(FDate.Text, Dateformat, CultureInfo.InvariantCulture);
        DateTime Dt1 = DateTime.ParseExact(TDate.Text, Dateformat, CultureInfo.InvariantCulture);

        Session["Dt"] = Dt.ToString("dd/MMM/yyyy");
        Session["Dt1"] = Dt1.ToString("dd/MMM/yyyy");

    }
    protected void Submit_Click6(object sender, EventArgs e)
    {
        try
        {

            if (CA.HasFile == true)
            {
                Random r = new Random();
                int num = r.Next();
                String A1 = num.ToString() + CA.FileName;

                CA.PostedFile.SaveAs(Server.MapPath("~/Uploads/" + A1));
                uploadfilesave_btst(Server.MapPath("~/Uploads/" + A1));
            }
            else
            {
                MsgBox("Please select a valid BTST Holdings file");
            }

        }
        catch (Exception)
        {
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Uploaded file is NOT as per standard BTST Holdings file format')", true);

            MsgBox("Uploaded file is NOT as per standard BTST Holdings file format");
        }

    }
    //protected void Submit_Clickpo3(object sender, EventArgs e)
    //{

    //    Conn connn = new Conn();
    //    SqlConnection conV = new SqlConnection(connn.Van);
    //    conV.Open();
    //    try
    //    {
    //        SqlCommand cmd = new SqlCommand();
    //        System.Data.DataTable dt = new System.Data.DataTable();
    //        if (FileUpload1.HasFile == true)
    //        {
    //            Random r = new Random();
    //            int num = r.Next();
    //            String A1 = num.ToString() + FileUpload1.FileName;

    //            FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Uploads/" + A1));
    //            dt = GetDataTableFromCsv(Server.MapPath("~/Uploads/" + A1));
    //            cmd = new SqlCommand("Process_PS03_Format");
    //            cmd.CommandType = CommandType.StoredProcedure;
    //            cmd.Connection = conV;
    //            cmd.Parameters.AddWithValue("@PS03", dt);
    //            cmd.ExecuteNonQuery();
    //            SqlDataAdapter da = new SqlDataAdapter(cmd);
    //            //System.Data.DataTable dtpo3 = new System.Data.DataTable();
    //            DataSet ds = new DataSet();


    //            DateTime now = DateTime.Now;
    //            DateTime dtFirstDay = new DateTime(now.Year, now.Month, now.Day);
    //            string FileName = "BOD_PS03_Fuel_Output_" + dtFirstDay.ToString("yyyy-MM-dd");

    //            //SqlCommand cmd1 = new SqlCommand("Exec Dt_Load 121,'" + Session["CD"].ToString() + "','','','',''", conV);
    //            //SqlDataAdapter da = new SqlDataAdapter(cmd);
    //            //System.Data.DataTable dtpo3 = new System.Data.DataTable();

    //            da.Fill(ds);
    //            ExporttoCSV(ds.Tables["Table"],FileName);

    //            //string str = string.Empty;

    //            //foreach (DataRow row in dtpo3.Rows)
    //            //{
    //            //    foreach (DataColumn column in dtpo3.Columns)
    //            //    {
    //            //        str += row[column.ColumnName].ToString() + ",";
    //            //    }
    //            //    str += "\r\n";
    //            //}

    //            //Response.Clear();
    //            //Response.Buffer = true;
    //            //Response.AddHeader("content-disposition", "attachment;filename=" + FileName + ".csv");
    //            //Response.Charset = "";
    //            //Response.ContentType = "application/text";
    //            //Response.Output.Write(str);
    //            //Response.Flush();
    //            //Response.End();


    //        }
    //        else
    //        {
    //            throw new Exception("\\nInput csv file is not selected.");
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        MsgBox("Please check the following error.\\n" + ex.Message);
    //    }
    //    conV.Close();
    //}
    private void ExporttoCSV(System.Data.DataTable table, string Filename)
    {
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.ContentType = "text/csv";
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + Filename + ".csv");
        HttpContext.Current.Response.Charset = "";
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);

        // Write column headers
        foreach (DataColumn column in table.Columns)
        {
            HttpContext.Current.Response.Write(column.ColumnName + ",");
        }
        HttpContext.Current.Response.Write(Environment.NewLine);

        // Write data rows
        foreach (DataRow row in table.Rows)
        {
            for (int i = 0; i < table.Columns.Count; i++)
            {
                HttpContext.Current.Response.Write(row[i].ToString() + ",");
            }
            HttpContext.Current.Response.Write(Environment.NewLine);
        }

        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.SuppressContent = true;
        HttpContext.Current.ApplicationInstance.CompleteRequest();
        HttpContext.Current.Response.End();
        //Response.End();
    }
    static System.Data.DataTable GetDataTableFromCsv(string path)
    {
        string header = "Yes";

        string pathOnly = Path.GetDirectoryName(path);
        string fileName = Path.GetFileName(path);

        string sql = @"SELECT * FROM [" + fileName + "]";

        using (OleDbConnection connection = new OleDbConnection(
                    @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + pathOnly +
                    ";Extended Properties=\"Text;HDR=" + header + "\""))
        using (OleDbCommand command = new OleDbCommand(sql, connection))
        using (OleDbDataAdapter adapter = new OleDbDataAdapter(command))
        {
            System.Data.DataTable dataTable = new System.Data.DataTable();
            dataTable.Locale = System.Globalization.CultureInfo.CurrentCulture;
            adapter.Fill(dataTable);
            return dataTable;
        }

    }
    public void uploadfilesave_btst(string filename)
    {
        System.Data.DataTable dt;
        dt = ConvertTexttoDataTable_btst(filename);

        int totalColumnToReserve = 20;
        for (int i = dt.Columns.Count - 1; i >= totalColumnToReserve; i--)
        {
            dt.Columns.RemoveAt(i);
        }
        int totalrowstoreserve = 1000000;
        for (int i = dt.Rows.Count - 1; i >= totalrowstoreserve; i--)
        {
            dt.Columns.RemoveAt(i);
        }
        Conn connn = new Conn();
        using (SqlConnection conV = new SqlConnection(connn.Van))
        {
            DateTime now = DateTime.Now;
            DateTime dtFirstDay = new DateTime(now.Year, now.Month, now.Day);
            string FileName = "BOD_Holding_BTST_Fuel" + dtFirstDay.ToString("yyyy-MM-dd");

            using (SqlCommand cmd = new SqlCommand("Process_BTSTHolding"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conV;
                cmd.Parameters.AddWithValue("@Dt", dt);
                System.Data.DataTable ds = new System.Data.DataTable();
                conV.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da1 = new SqlDataAdapter(cmd);
                da1.Fill(ds);
                conV.Close();
                textexp(ds, "" + FileName + ".txt");
            }
        }
    }
    public static System.Data.DataTable ConvertTexttoDataTable_btst(string strFilePath)
    {
        System.Data.DataTable dt = new System.Data.DataTable();
        using (StreamReader sr = new StreamReader(strFilePath))
        {
            dt.Columns.Add("A1");
            dt.Columns.Add("A2");
            dt.Columns.Add("A3");
            dt.Columns.Add("A4");
            dt.Columns.Add("A5");
            dt.Columns.Add("A6");
            dt.Columns.Add("A7");
            dt.Columns.Add("A8");
            dt.Columns.Add("A9");
            dt.Columns.Add("A10");
            dt.Columns.Add("A11");
            dt.Columns.Add("A12");
            dt.Columns.Add("A13");
            dt.Columns.Add("A14");
            dt.Columns.Add("A15");
            dt.Columns.Add("A16");
            dt.Columns.Add("A17");
            dt.Columns.Add("A18");
            while (!sr.EndOfStream)
            {
                string[] rows = sr.ReadLine().Split(',');
                DataRow dr = dt.NewRow();
                for (int i = 0; i < 18; i++)
                {
                    dr[i] = rows[i];
                }
                dt.Rows.Add(dr);
            }
        }
        return dt;
    }

    protected void Submit_Click(object sender, EventArgs e)
    {

        if (CA.HasFile == true)
        {
            Random r = new Random();
            int num = r.Next();
            String A1 = num.ToString() + CA.FileName;

            CA.PostedFile.SaveAs(Server.MapPath("~/Uploads/" + A1));
            uploadfilesave(Server.MapPath("~/Uploads/" + A1));
        }
        else
        {
            MsgBox("Please select a valid Holdings file");
        }
    }
    protected void E_protector(object sender, EventArgs e)
    {

        if (CA.HasFile == true)
        {
            Random r = new Random();
            int num = r.Next();
            String A1 = num.ToString() + CA.FileName;

            CA.PostedFile.SaveAs(Server.MapPath("~/Uploads/" + A1));
            uploadfileeprotector(Server.MapPath("~/Uploads/" + A1));
        }
        else
        {
            MsgBox("Please select a valid Holdings file");
        }



    }

    public void uploadfileeprotector(string filename)
    {

        System.Data.DataTable dt;
        dt = ConvertTexttoDataTable(filename);

        int totalColumnToReserve = 15;
        for (int i = dt.Columns.Count - 1; i >= totalColumnToReserve; i--)
        {
            dt.Columns.RemoveAt(i);
        }
        int totalrowstoreserve = 1000000;
        for (int i = dt.Rows.Count - 1; i >= totalrowstoreserve; i--)
        {
            dt.Columns.RemoveAt(i);
        }


        Conn connn = new Conn();
        using (SqlConnection conV = new SqlConnection(connn.Van))
        {
            using (SqlCommand cmd = new SqlCommand("Ins_UploadHolding"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conV;
                cmd.Parameters.AddWithValue("@Dt", dt);
                conV.Open();
                cmd.ExecuteNonQuery();
                conV.Close();
            }
            DateTime now = DateTime.Now;
            DateTime dtFirstDay = new DateTime(now.Year, now.Month, now.Day);
            string FileName = "BOD_eProtector_Holdings_Fuel" + dtFirstDay.ToString("yyyy-MM-dd");

            SqlCommand cmd1 = new SqlCommand("Exec Dt_Load 46,'" + Session["CD"].ToString() + "','','','',''", conV);
            cmd1.CommandTimeout = 180;
            System.Data.DataTable ds = new System.Data.DataTable();
            conV.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd1);
            da.Fill(ds);
            conV.Close();
            textexp(ds, "" + FileName + ".txt");


        }


    }



    public void uploadfilesave(string filename)
    {

        System.Data.DataTable dt;
        dt = ConvertTexttoDataTable(filename);

        int totalColumnToReserve = 15;
        for (int i = dt.Columns.Count - 1; i >= totalColumnToReserve; i--)
        {
            dt.Columns.RemoveAt(i);
        }
        int totalrowstoreserve = 1000000;
        for (int i = dt.Rows.Count - 1; i >= totalrowstoreserve; i--)
        {
            dt.Columns.RemoveAt(i);
        }
        Conn connn = new Conn();
        using (SqlConnection conV = new SqlConnection(connn.Van))
        {
            using (SqlCommand cmd = new SqlCommand("Ins_UploadHolding"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conV;
                cmd.Parameters.AddWithValue("@Dt", dt);
                conV.Open();
                cmd.ExecuteNonQuery();
                conV.Close();
            }
            DateTime now = DateTime.Now;
            DateTime dtFirstDay = new DateTime(now.Year, now.Month, now.Day);
            string FileName = "BOD_Holdings_Fuel" + dtFirstDay.ToString("yyyy-MM-dd");

            SqlCommand cmd1 = new SqlCommand("Exec Dt_Load 99,'" + Session["CD"].ToString() + "','','','',''", conV);
            cmd1.CommandTimeout = 180;
            System.Data.DataTable ds = new System.Data.DataTable();
            conV.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd1);
            da.Fill(ds);
            conV.Close();
            textexp(ds, "" + FileName + ".txt");
        }

    }


    public static System.Data.DataTable ConvertTexttoDataTable(string strFilePath)
    {
        System.Data.DataTable dt = new System.Data.DataTable();
        using (StreamReader sr = new StreamReader(strFilePath))
        {
            dt.Columns.Add("A1");
            dt.Columns.Add("A2");
            dt.Columns.Add("A3");
            dt.Columns.Add("A4");
            dt.Columns.Add("A5");
            dt.Columns.Add("A6");
            dt.Columns.Add("A7");
            dt.Columns.Add("A8");
            dt.Columns.Add("A9");
            dt.Columns.Add("A10");
            dt.Columns.Add("A11");
            while (!sr.EndOfStream)
            {
                string[] rows = sr.ReadLine().Split(',');
                DataRow dr = dt.NewRow();
                for (int i = 0; i < 11; i++)
                {
                    dr[i] = rows[i];
                }
                dt.Rows.Add(dr);
            }

        }

        return dt;
    }
    protected void Submit_Click3(object sender, EventArgs e)
    {
        Utils utils = new Utils();
        String Uid = Session["Name"].ToString();
        string Role = Session["Type"].ToString();

        DateTime now = DateTime.Now;
        var startDate = new DateTime(now.Year, now.Month, 1);
        if (WDate.Text == "")
        {
            WDate.Text = Convert.ToDateTime(startDate).ToString();
        }

        DateTime Dt = DateTime.ParseExact(WDate.Text, Dateformat, CultureInfo.InvariantCulture);

        Conn connn = new Conn();
        SqlConnection conV = new SqlConnection(connn.Van);
        SqlCommand cmd = new SqlCommand("Exec Dt_Load 27,'" + Uid + "','" + Dt.ToString("dd/MMM/yyyy") + "','','','" + Role + "'", conV);
        cmd.CommandTimeout = 180;
        System.Data.DataTable dt = new System.Data.DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        conV.Open();
        da.Fill(dt);
        conV.Close();
        utils.ExporttoExcel(dt, "Client_Wise_DP_Holdings");
        //ExporttoExcel(dt, "Client_Wise_DP_Holdings");

    }

    public System.Data.DataTable createDataTable()
    {
        String Uid = Session["Name"].ToString();
        String Role = Session["Type"].ToString();

        DateTime now = DateTime.Now;
        var startDate = new DateTime(now.Year, now.Month, 1);

        if (FDate.Text == "")
        {
            FDate.Text = Convert.ToDateTime(startDate).ToString();
        }

        if (TDate.Text == "")
        {
            TDate.Text = Convert.ToDateTime(now).ToString();
        }

        DateTime Dt = DateTime.ParseExact(FDate.Text, Dateformat, CultureInfo.InvariantCulture);
        DateTime Dt1 = DateTime.ParseExact(TDate.Text, Dateformat, CultureInfo.InvariantCulture);


        Session["FD"] = Dt;
        Session["TD"] = Dt1;
        Conn connn = new Conn();
        SqlConnection con = new SqlConnection(connn.Connn);
        SqlConnection conV = new SqlConnection(connn.Van);
        SqlCommand cmd = new SqlCommand();
        cmd = new SqlCommand("Exec getDpTradesForExcel '" + Uid + "','" + Dt.ToString("dd-MMM-yyyy") + "','" + Dt1.ToString("dd-MMM-yyyy") + "','" + Role + "','" + DropDownList2.SelectedValue + "'", conV);
        cmd.CommandTimeout = 60;
        System.Data.DataTable ds = new System.Data.DataTable();
        conV.Open();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        conV.Close();
        return ds;
    }

    protected void Export_Click(object sender, EventArgs e)
    {

        Conn connn = new Conn();
        SqlConnection conV = new SqlConnection(connn.Van);

        string FileName = "Client_Wise_DP_Trades";
        string Name = "Export";
        System.Data.DataTable dt = createDataTable();

        string str = string.Empty;
        foreach (DataColumn column in dt.Columns)
        {
            // Add the header to the text file
            str += column.ColumnName + ",";
        }
        str += "\r\n";

        foreach (DataRow row in dt.Rows)
        {
            foreach (DataColumn column in dt.Columns)
            {
                // Insert the Data rows.
                str += row[column.ColumnName].ToString() + ",";
            }
            // Insert a  new line.
            str += "\r\n";
        }

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=" + FileName + ".csv");
        Response.Charset = "";
        Response.ContentType = "application/text";
        Response.Output.Write(str);
        Response.Flush();
        Response.End();




    }
    protected void Submit_Click4(object sender, EventArgs e)
    {
        Utils utils = new Utils();
        String Uid = Session["Name"].ToString();
        String Role = Session["Type"].ToString();

        if (Role == "Admin")
        {
            Uid = Role;
        }

        DateTime now = DateTime.Now;
        var startDate = new DateTime(now.Year, now.Month, 1);
        if (FDate.Text == "")
        {
            FDate.Text = Convert.ToDateTime(startDate).ToString();
        }

        if (TDate.Text == "")
        {
            TDate.Text = Convert.ToDateTime(now).ToString();
        }

        DateTime Dt = DateTime.ParseExact(FDate.Text, Dateformat, CultureInfo.InvariantCulture);
        DateTime Dt1 = DateTime.ParseExact(TDate.Text, Dateformat, CultureInfo.InvariantCulture);

        Conn connn = new Conn();
        SqlConnection conV = new SqlConnection(connn.Van);
        SqlCommand cmd = new SqlCommand("Dt_Load 115 ,'" + Uid + "','" + Dt.ToString("dd/MMM/yyyy") + "','" + Dt1.ToString("dd/MMM/yyyy") + "','" + DropDownList2.SelectedValue + "',''", conV);
        cmd.CommandTimeout = 180;
        System.Data.DataTable datat = new System.Data.DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        conV.Open();
        da.Fill(datat);
        conV.Close();
        utils.ExporttoExcel(datat, "DP_Off_Market_Trades_WS_MapId_51");
        //ExporttoExcel(datat, "DP_Off_Market_Trades_WS_MapId_51");
    }


    protected void Submit_Click5(object sender, EventArgs e)
    {
        Utils utils = new Utils();
        String Uid = Session["Name"].ToString();
        String Role = Session["Type"].ToString();

        if (Role == "Admin")
        {
            Uid = Role;
        }

        DateTime now = DateTime.Now;
        var startDate = new DateTime(now.Year, now.Month, 1);
        if (FDate.Text == "")
        {
            FDate.Text = Convert.ToDateTime(startDate).ToString();
        }

        if (TDate.Text == "")
        {
            TDate.Text = Convert.ToDateTime(now).ToString();
        }

        DateTime Dt = DateTime.ParseExact(FDate.Text, Dateformat, CultureInfo.InvariantCulture);
        DateTime Dt1 = DateTime.ParseExact(TDate.Text, Dateformat, CultureInfo.InvariantCulture);

        Conn connn = new Conn();
        SqlConnection conV = new SqlConnection(connn.Van);
        SqlCommand cmd = new SqlCommand("Exec Dt_Load 118,'" + Uid + "','" + Dt.ToString("dd/MMM/yyyy") + "','" + Dt1.ToString("dd/MMM/yyyy") + "','" + DropDownList2.SelectedValue + "','Admin'", conV);
        cmd.CommandTimeout = 180;
        System.Data.DataTable dt = new System.Data.DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        conV.Open();
        da.Fill(dt);
        conV.Close();
        utils.ExporttoExcel(dt, "DP_Off_Market_Trades_AuditTrail");
        //ExporttoExcel(dt, "DP_Off_Market_Trades_AuditTrail");

    }

    protected void Submit_Click2(object sender, EventArgs e)
    {
        Conn connn = new Conn();
        SqlConnection conV = new SqlConnection(connn.Van);
        DateTime now = DateTime.Now;
        DateTime dtFirstDay = new DateTime(now.Year, now.Month, now.Day);
        string FileName = "BOD_FNO_Position_Fuel" + dtFirstDay.ToString("yyyy-MM-dd");

        SqlCommand cmd = new SqlCommand("Exec Dt_Load 114,'" + Session["CD"].ToString() + "','','','',''", conV);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        System.Data.DataTable dt = new System.Data.DataTable();

        conV.Open();
        da.Fill(dt);
        conV.Close();

        string str = string.Empty;

        foreach (DataRow row in dt.Rows)
        {
            foreach (DataColumn column in dt.Columns)
            {
                str += row[column.ColumnName].ToString() + ",";
            }
            str += "\r\n";
        }

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=" + FileName + ".txt");
        Response.Charset = "";
        Response.ContentType = "application/text";
        Response.Output.Write(str);
        Response.Flush();
        Response.End();

    }

    //protected void Submit_Clickpo3(object sender, EventArgs e)
    //{
    //    Conn connn = new Conn();
    //    SqlConnection conV = new SqlConnection(connn.Van);
    //    DateTime now = DateTime.Now;
    //    DateTime dtFirstDay = new DateTime(now.Year, now.Month, now.Day);
    //    string FileName = "BOD_FNO_Position_3_Fuel_" + dtFirstDay.ToString("yyyy-MM-dd");

    //    SqlCommand cmd = new SqlCommand("Exec Dt_Load 121,'" + Session["CD"].ToString() + "','','','',''", conV);
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    System.Data.DataTable dt = new System.Data.DataTable();

    //    conV.Open();
    //    da.Fill(dt);
    //    conV.Close();

    //    string str = string.Empty;

    //    foreach (DataRow row in dt.Rows)
    //    {
    //        foreach (DataColumn column in dt.Columns)
    //        {
    //            str += row[column.ColumnName].ToString() + ",";
    //        }
    //        str += "\r\n";
    //    }

    //    Response.Clear();
    //    Response.Buffer = true;
    //    Response.AddHeader("content-disposition", "attachment;filename=" + FileName + ".csv");
    //    Response.Charset = "";
    //    Response.ContentType = "application/text";
    //    Response.Output.Write(str);
    //    Response.Flush();
    //    Response.End();

    //}

    protected void Submit_Click12(object sender, EventArgs e)
    {
        Conn connn = new Conn();
        SqlConnection conV = new SqlConnection(connn.Van);
        DateTime now = DateTime.Now;
        DateTime dtFirstDay = new DateTime(now.Year, now.Month, now.Day);
        string FileName = "Client_dump_for_ODIN" + dtFirstDay.ToString("yyyy-MM-dd");

        SqlCommand cmd = new SqlCommand("Exec Dt_Load 44,'" + Session["CD"].ToString() + "','','','',''", conV);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        System.Data.DataTable dt = new System.Data.DataTable();

        conV.Open();
        da.Fill(dt);
        conV.Close();

        string str = string.Empty;

        foreach (DataRow row in dt.Rows)
        {
            foreach (DataColumn column in dt.Columns)
            {
                str += row[column.ColumnName].ToString() + ",";
            }
            str += "\r\n";
        }

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=" + FileName + ".txt");
        Response.Charset = "";
        Response.ContentType = "application/text";
        Response.Output.Write(str);
        Response.Flush();
        Response.End();

    }
    public static void ToCSV(System.Data.DataTable dtDataTable, string strFilePath)
    {
        StreamWriter sw = new StreamWriter(strFilePath, false);
        //headers
        for (int i = 0; i < dtDataTable.Columns.Count; i++)
        {
            sw.Write(dtDataTable.Columns[i]);
            if (i < dtDataTable.Columns.Count - 1)
            {
                sw.Write(",");
            }
        }
        sw.Write(sw.NewLine);
        foreach (DataRow dr in dtDataTable.Rows)
        {
            for (int i = 0; i < dtDataTable.Columns.Count; i++)
            {
                if (!Convert.IsDBNull(dr[i]))
                {
                    string value = dr[i].ToString();
                    if (value.Contains(','))
                    {
                        value = String.Format("\"{0}\"", value);
                        sw.Write(value);
                    }
                    else
                    {
                        sw.Write(dr[i].ToString());
                    }
                }
                if (i < dtDataTable.Columns.Count - 1)
                {
                    sw.Write(",");
                }
            }
            sw.Write(sw.NewLine);
        }
        sw.Close();
    }

    private void Ins(String Nm, int ct)
    {
        Conn connn = new Conn();
        SqlConnection con = new SqlConnection(connn.Connn);
        SqlConnection conV = new SqlConnection(connn.Van);
        SqlCommand cmd = new SqlCommand();


        string csvData = File.ReadAllText(Nm);

        cmd = new SqlCommand("Exec Dt_Load 99,'" + Session["CD"].ToString() + "','','','',''", conV);
        DataSet ds = new DataSet();
        conV.Open();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        conV.Close();
        textexp(ds.Tables[0], "positionholding.txt");
    }
    private void textexp(System.Data.DataTable dt, string filename)
    {

        string txt = string.Empty;


        foreach (DataRow row in dt.Rows)
        {
            foreach (DataColumn column in dt.Columns)
            {
                //Add the Data rows.
                txt += row[column.ColumnName].ToString();
            }

            //Add new line.
            txt += "\r\n";
        }

        //Download the Text file.
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/text";
        Response.AddHeader("content-disposition", "attachment;filename=" + filename + "");
        Response.Charset = "";
        Response.Write(txt);
        Response.Flush();
        HttpContext.Current.Response.SuppressContent = true;
        HttpContext.Current.ApplicationInstance.CompleteRequest();
        Response.End();
    }



    //public void ExporttoExcel(System.Data.DataTable table, string Filename)
    //{
    //    HttpContext.Current.Response.Clear();
    //    HttpContext.Current.Response.ClearContent();
    //    HttpContext.Current.Response.ClearHeaders();
    //    HttpContext.Current.Response.Buffer = true;
    //    HttpContext.Current.Response.ContentType = "application/ms-excel";
    //    HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
    //    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename= " + Filename + ".xls");
    //    HttpContext.Current.Response.Charset = "utf-8";
    //    HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
    //    HttpContext.Current.Response.Write("<br>");
    //    HttpContext.Current.Response.Write("<br>");
    //    HttpContext.Current.Response.Write("<br>");

    //    HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
    //    HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
    //                                       "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
    //                                       "style='font-size:10.0pt; font-family:Calibri; background:white;'>" +
    //                                       " <TR style='vertical-align: middle;'>");

    //    int columnscount = table.Columns.Count;

    //    for (int j = 0; j < columnscount; j++)
    //    {
    //        HttpContext.Current.Response.Write("<Td>");
    //        HttpContext.Current.Response.Write("<B>");
    //        HttpContext.Current.Response.Write(table.Columns[j].Caption.ToString()); 
    //        HttpContext.Current.Response.Write("</B>");
    //        HttpContext.Current.Response.Write("</Td>");
    //    }

    //    HttpContext.Current.Response.Write("</TR>");

    //    foreach (DataRow row in table.Rows)
    //    {
    //        HttpContext.Current.Response.Write("<TR style='vertical-align: middle;'>");

    //        for (int i = 0; i < table.Columns.Count; i++)
    //        {
    //            String Cellclass = @"mso - number - format:""\@""";

    //            if (row[i].GetType() == typeof(decimal))
    //            {
    //                Cellclass = @"mso-number-format:""#\,##0.000""";
    //            }
    //            else
    //            {
    //                if (row[i].GetType() == typeof(double))
    //                {
    //                    Cellclass = @"mso-number-format:""#\,##0.00""";
    //                }
    //                else
    //                {
    //                    if (row[i].GetType() == typeof(int))
    //                    {
    //                        Cellclass = @"mso - number - format:""0""";
    //                    }
    //                }
    //            }
    //            HttpContext.Current.Response.Write("<Td style='" + Cellclass + "'>");
    //            HttpContext.Current.Response.Write(row[i].ToString());
    //            HttpContext.Current.Response.Write("</Td>");
    //        }


    //        HttpContext.Current.Response.Write("</TR>");
    //    }



    //    HttpContext.Current.Response.Write("</Table>");


    //    HttpContext.Current.Response.Write("</font>");


    //    HttpContext.Current.Response.Flush();
    //    HttpContext.Current.Response.End();
    //}

    protected void CA_DataBinding(object sender, EventArgs e)
    {

    }
    protected void CA_Disposed(object sender, EventArgs e)
    {

    }




    //  ORM

    protected void ORMTXT_Click(object sender, EventArgs e)
    {
        Conn connn = new Conn();
        SqlConnection conV = new SqlConnection(connn.Van);
        DateTime now = DateTime.Now;
        var startDate = new DateTime(now.Year, now.Month, 1);

        DateTime dtFirstDay = new DateTime(now.Year, now.Month, now.Day);
        string FileName = "OMS_Report" + dtFirstDay.ToString("yyyy-MM-dd");


        SqlCommand cmd = new SqlCommand("Exec Dt_Load 129,'" + Session["User"].ToString() + "','" + ORMTXT.Text + "','','',''", conV);

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        System.Data.DataTable dt = new System.Data.DataTable();

        conV.Open();
        da.Fill(dt);
        conV.Close();

        string str = string.Empty;

        foreach (DataRow row in dt.Rows)
        {
            foreach (DataColumn column in dt.Columns)
            {
                str += row[column.ColumnName].ToString() + ",";
            }
            str += "\r\n";
        }

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=" + FileName + ".txt");
        Response.Charset = "";
        Response.ContentType = "application/text";
        Response.Output.Write(str);
        Response.Flush();
        Response.End();
    }

    protected void ORMEXCEL_Click(object sender, EventArgs e)
    {
        Utils utils = new Utils();
        Conn connn = new Conn();
        DateTime now = DateTime.Now;
        DateTime dtFirstDay = new DateTime(now.Year, now.Month, now.Day);
        string FileName = "OMS_Report_" + dtFirstDay.ToString("yyyy-MM-dd");
        SqlConnection conV = new SqlConnection(connn.Van);
        SqlCommand cmd = new SqlCommand("Exec Dt_Load 129,'" + Session["User"].ToString() + "','" + ORMTXT.Text + "','','',''", conV);
        cmd.CommandTimeout = 180;
        System.Data.DataTable datat = new System.Data.DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        conV.Open();
        da.Fill(datat);
        conV.Close();
        utils.ExporttoExcel(datat, FileName);
    }
    protected void Adhoc_Limit(object sender, EventArgs e)
    {

        if (FileUploadMF.HasFile == true)
        {

            Random r = new Random();
            int num = r.Next();
            String A1 = num.ToString() + FileUploadMF.FileName;

            FileUploadMF.PostedFile.SaveAs(Server.MapPath("~/Uploads/" + A1));
            Ins1(Server.MapPath("~/Uploads/" + A1), 2);
        }
        Conn connn = new Conn();
        SqlConnection con = new SqlConnection(connn.Connn);
        SqlConnection conV = new SqlConnection(connn.Van);
        SqlCommand cmd = new SqlCommand();


        cmd = new SqlCommand("Exec Dt_Load 180,'" + Session["CD"].ToString() + "','','','',''", conV);
        DataSet ds = new DataSet();
        conV.Open();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        conV.Close();
        textexp(ds.Tables[0]);

    }
   

    private void Ins1(String Nm, int ct)
    {
        Conn connn = new Conn();
        SqlConnection con = new SqlConnection(connn.Connn);
        SqlConnection conV = new SqlConnection(connn.Van);
        SqlCommand cmd = new SqlCommand();


        cmd = new SqlCommand("Exec Admin_upload " + 7 + "," + 1 + ",'','',''", conV);
        conV.Open();
        cmd.ExecuteNonQuery();
        conV.Close();



        string csvData = File.ReadAllText(Nm);
        String A1 = "";
        String A2 = "";
        String A3 = "";
        int ai;
        int i = 0;
        
        foreach (string row in csvData.Split('\n'))
        {
            
                ai = 0;
                foreach (string cell in row.Split(','))
                {
                    if (ai == 0)
                    {
                        A1 = cell;
                    }

                    if (ai == 1)
                    {
                        A2 = cell;
                    }

                    if (ai == 2)
                    {
                        A3 = cell;
                    }

                    ai++;
                }
           
            
                    cmd = new SqlCommand("Exec Admin_upload " + 5 + "," + ai + ",'" + A1 + "','" + A2 + "','" + A3 + "'", conV);
                    conV.Open();
                    cmd.ExecuteNonQuery();
                    conV.Close();
          
        }



    }

    //protected void mf_export(object sender, EventArgs e)
    //{

    //    Conn connn = new Conn();
    //    SqlConnection con = new SqlConnection(connn.Connn);
    //    SqlConnection conV = new SqlConnection(connn.Van);
    //    SqlCommand cmd = new SqlCommand();
    //    cmd = new SqlCommand("Exec Dt_Load 180,'" + Session["CD"].ToString() + "','','','',''", conV);
    //    DataSet ds = new DataSet();
    //    conV.Open();
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    da.Fill(ds);
    //    conV.Close();
    //    textexp(ds.Tables[0]);
    //}
    private void textexp(System.Data.DataTable dt)
    {
        string txt = string.Empty;

        foreach (DataRow row in dt.Rows)
        {
            foreach (DataColumn column in dt.Columns)
            {
                //Add the Data rows.
                txt += row[column.ColumnName].ToString();
            }

            //Add new line.
            txt += "\r\n";
        }

        //Download the Text file.
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/text";
        Response.AddHeader("content-disposition", "attachment;filename=BOD_MF_Adhoc_Limit.txt");
        Response.Charset = "";
        Response.Write(txt);
        Response.Flush();
        Response.End();
    }
    protected void cash_allocation(object sender, EventArgs e)
    {
        
        string randomnumber = DateTime.Now.ToString("yyyyMMddHHmmss");
        string filename;
        Conn connn = new Conn();
        SqlConnection conV = new SqlConnection(connn.Connn);
        SqlCommand cmd = new SqlCommand();
        try
        {
            if (FileUploadcash.HasFile)
            {
                filename = FileUploadcash.FileName;
                FileUploadcash.SaveAs(Server.MapPath("~/Uploads/") + randomnumber + filename);
                System.Data.DataTable dt;
                dt = ConvertCSVtoDataTable(Server.MapPath("~/Uploads/") + randomnumber + filename);


                int totalColumnsToReserve = 70;
                for (int i = dt.Columns.Count - 1; i >= totalColumnsToReserve; i--)
                {
                    dt.Columns.RemoveAt(i);
                }
                //dt.Rows.RemoveAt(0);


                using (SqlConnection conn = new SqlConnection(connn.Van))

                {

                    using (SqlCommand cmd1 = new SqlCommand("Ins_UploadAllocation1st"))

                    {

                        cmd1.CommandType = CommandType.StoredProcedure;

                        cmd1.Connection = conn;

                        cmd1.Parameters.AddWithValue("@dt", dt);

                        conn.Open();

                        cmd1.ExecuteNonQuery();

                        conn.Close();

                    }

                }

            }
            else
            {


            }
            MsgBox("File Upload Sucessfully");

        }
        catch (Exception ex)
        {
            MsgBox(ex.Message);
        }
    }

    protected void Fo_allocation(object sender, EventArgs e)
    {

        string randomnumber = DateTime.Now.ToString("yyyyMMddHHmmss");
        string filename;
        Conn connn = new Conn();
        SqlConnection conV = new SqlConnection(connn.Connn);
        SqlCommand cmd = new SqlCommand();
        try
        {
            if (FileUploadFO.HasFile)
            {
                filename = FileUploadFO.FileName;
                FileUploadFO.SaveAs(Server.MapPath("~/Uploads/") + randomnumber + filename);
                System.Data.DataTable dt;
                dt = ConvertCSVtoDataTable(Server.MapPath("~/Uploads/") + randomnumber + filename);


                int totalColumnsToReserve = 70;
                for (int i = dt.Columns.Count - 1; i >= totalColumnsToReserve; i--)
                {
                    dt.Columns.RemoveAt(i);
                }
                //dt.Rows.RemoveAt(0);


                using (SqlConnection conn = new SqlConnection(connn.Van))

                {

                    using (SqlCommand cmd1 = new SqlCommand("Ins_UploadAllocation2nd"))

                    {

                        cmd1.CommandType = CommandType.StoredProcedure;

                        cmd1.Connection = conn;

                        cmd1.Parameters.AddWithValue("@dt", dt);

                        conn.Open();

                        cmd1.ExecuteNonQuery();

                        conn.Close();

                    }

                }

            }
            else
            {


            }
            MsgBox("File Upload Sucessfully");

        }
        catch (Exception ex)
        {
            MsgBox(ex.Message);
        }
    }
    public static System.Data.DataTable ConvertCSVtoDataTable(string strFilePath)
    {
        System.Data.DataTable dt = new System.Data.DataTable();
        using (StreamReader sr = new StreamReader(strFilePath))
        {
            string[] headers = sr.ReadLine().Split(',');
            foreach (string header in headers)
            {
                dt.Columns.Add(header);
            }
            while (!sr.EndOfStream)
            {
                string[] rows = sr.ReadLine().Split(',');
                DataRow dr = dt.NewRow();
                for (int i = 0; i < headers.Length; i++)
                {
                    dr[i] = rows[i];
                }
                dt.Rows.Add(dr);
            }

        }


        return dt;
    }


   
    
    
    
    private void MsgBox(string sMessage)
    {
        string msg = "<script language=\"javascript\">";
        msg += "alert('" + sMessage + "');";
        msg += "</script>";
        Response.Write(msg);
    }
}