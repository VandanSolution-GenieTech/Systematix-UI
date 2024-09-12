using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.OleDb;
using iTextSharp.text.xml.xmp;
using Microsoft.Office.Interop.Excel;
using System.Drawing;
using System.Data;
using System.IdentityModel.Protocols.WSTrust;

public partial class UploadPage : System.Web.UI.Page
{
    public String Titleprefix { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        Conn connn = new Conn();
        SqlConnection conV = new SqlConnection(connn.Van);
        conV.Open();

        String Uid = Session["Name"].ToString();
        if (!IsPostBack)
        {
            BindRepeter(conV);
        }

        if(Uid == "Admin")
        {
            txtAdd.Visible = true;
            Titleprefix = "Manage";
        }
        else
        {
            txtAdd.Visible = false;
            Rmsrm.Visible = false;
            Titleprefix = "View";
        }
        conV.Close();
    }


    public void BindRepeter(SqlConnection conV)
    {
        
        SqlCommand cmd = new SqlCommand();
        cmd = new SqlCommand("Exec ProcGetFileUpload", conV);
        System.Data.DataTable ds = new System.Data.DataTable();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        Repeater2.DataSource = ds;
        Repeater2.DataBind();
    }



    private void savedata(string Pid, string pname, string eid, string cid, string cname, string EmailId,SqlConnection conV,SqlTransaction transaction)
    {
        //Proc_FileUpload
        //insert into fileupload(ParentId,Pname,EmailId,childId,childname) values(" + Pid + ",'" + pname + "','" + eid + "','" + cid + "','" + cname + "')
        String query = "Proc_FileUpload '" + Pid + "','" + pname + "','" + eid + "','" + cid + "','" + cname + "','" + EmailId + "'";
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = query;
        cmd.Connection = conV;
        cmd.Transaction = transaction;
        cmd.ExecuteNonQuery();

    }

    protected void Close_Click(object sender, EventArgs e)
    {
        Rmsrm.Visible = false;
        txtAdd.Visible = true;
        Grid.Visible = true;
        Conn connn = new Conn();
        SqlConnection conV = new SqlConnection(connn.Van);
        conV.Open();
        BindRepeter(conV);
        conV.Close();
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {


        Conn connn = new Conn();
        SqlConnection conV = new SqlConnection(connn.Van);
        conV.Open();
        SqlTransaction transaction = conV.BeginTransaction();
        try
        {
            
            if (fupload.HasFile)
            {
               
                SqlCommand cmd1 = new SqlCommand("usp_truncatdata", conV, transaction);
                cmd1.CommandType = System.Data.CommandType.StoredProcedure;
                cmd1.ExecuteNonQuery();

                string pid;
                String pname;
                String eid;
                String cid;
                String cname;
                String EmailId;

                string randomnumber = DateTime.Now.ToString("yyyyMMddHHmmss");
                string filename;
                filename = fupload.FileName;
                fupload.SaveAs(Server.MapPath("~/Uploads/SRMMapping_") + randomnumber + filename);
                System.Data.DataTable dt;
                dt = ReadExcelFile("Sheet1", Server.MapPath("~/Uploads/SRMMapping_") + randomnumber + filename);

                dt.Rows.RemoveAt(0);

                foreach (DataRow row in dt.Rows)
                {
                    pid = row[0].ToString();
                    pname = row[1].ToString();
                    eid = row[2].ToString();
                    cid = row[3].ToString();
                    cname = row[4].ToString();
                    EmailId = row[5].ToString();
                    savedata(pid, pname, eid, cid, cname, EmailId, conV, transaction);
                }
                transaction.Commit();
                MsgBox("The RM-SRM mapping has been uploaded successfully");
            }
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            MsgBox("The RM-SRM mapping did not get uploaded.Please Try Again.\\n Current Error:\\n" + ex.Message);
        }
            conV.Close();
        
        //else
        //{
        //    MsgBox("Please select File First");
        //}


    }

    protected void Add(object sender, EventArgs e)
    {
        
        Rmsrm.Visible = true;
        Grid.Visible = false;
        txtAdd.Visible = false;
    }
        private System.Data.DataTable ReadExcelFile(string sheetName, string path)
    {

        using (OleDbConnection conn = new OleDbConnection())
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            string Import_FileName = path;
            string fileExtension = Path.GetExtension(Import_FileName);
            if (fileExtension == ".xls")
                conn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Import_FileName + ";" + "Extended Properties='Excel 8.0; HDR = No'";
            if (fileExtension == ".CSV")
                conn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Import_FileName + ";" + "Extended Properties='Text;HDR=No;FMT=CSVDelimited;'";
            if (fileExtension == ".csv")
                conn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Import_FileName + ";" + "Extended Properties='Text;HDR=No;FMT=CSVDelimited;'";
            if (fileExtension == ".xlsx")
                conn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Import_FileName + ";" + "Extended Properties='Excel 12.0 Xml;HDR=No;'";
            using (OleDbCommand comm = new OleDbCommand())
            {
                conn.Open();
                System.Data.DataTable dbSchema = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                conn.Close();

                sheetName = dbSchema.Rows[0]["TABLE_NAME"].ToString();

                comm.CommandText = "Select * from [" + sheetName + "]";
                comm.Connection = conn;
                using (OleDbDataAdapter da = new OleDbDataAdapter())
                {
                    da.SelectCommand = comm;
                    da.Fill(dt);
                    return dt;
                }
            }
        }
    }


    static System.Data.DataTable GetDataTableFromCsv(string path)
    {
        string header = "No";

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

    private void MsgBox(string sMessage)
    {
        string msg = "<script language=\"javascript\">";
        msg += "alert('" + sMessage + "');";
        msg += "</script>";
        Response.Write(msg);
    }




}
