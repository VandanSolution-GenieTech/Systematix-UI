using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Utils
/// </summary>
public class Utils
{
  
        
        public void ExporttoExcel(System.Data.DataTable table, string Filename)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename= "+ Filename +".xls");
            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
            HttpContext.Current.Response.Write("<br>");
            HttpContext.Current.Response.Write("<br>");
            HttpContext.Current.Response.Write("<br>");

            HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
            HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
                                               "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
                                               "style='font-size:10.0pt; font-family:Calibri; background:white;'>" +
                                               " <TR style='vertical-align: middle;'>");
            
        int columnscount = table.Columns.Count;

            for (int j = 0; j < columnscount; j++)
            {      
                HttpContext.Current.Response.Write("<Td>");
                HttpContext.Current.Response.Write("<B>");
                HttpContext.Current.Response.Write(table.Columns[j].Caption.ToString());
                HttpContext.Current.Response.Write("</B>");
                HttpContext.Current.Response.Write("</Td>");
            }

            HttpContext.Current.Response.Write("</TR>");

            foreach (DataRow row in table.Rows)
            {
                HttpContext.Current.Response.Write("<TR style='vertical-align: middle;'>");

                for (int i = 0; i < table.Columns.Count; i++)
                {
                    String Cellclass = @"mso - number - format:""\@""";

                    if (row[i].GetType() == typeof(decimal))
                    {
                        Cellclass = @"mso-number-format:""#\,##0.000""";
                    }
                    else
                    {
                        if (row[i].GetType() == typeof(double))
                        {
                            Cellclass = @"mso-number-format:""#\,##0.00""";
                        }
                        else
                        {
                            if (row[i].GetType() == typeof(int))
                            {
                                Cellclass = @"mso - number - format:""0""";
                            }
                        }
                    }
                    HttpContext.Current.Response.Write("<Td style='" + Cellclass + "'>");
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
