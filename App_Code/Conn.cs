using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
/// <summary>
/// Summary description for Conn
/// </summary>
public class Conn
{
    public String Ibeats { get; set; }
    public String Fuel { get; set; }
    
     String ConnString;
    
     SqlConnection conn = new SqlConnection("Data Source=MUMBKCCMTAPPUAT;Initial Catalog=Vandan;Integrated Security=False;User ID=vandan;Password=Vandan@11022020");

    

     public string Connn
     {
         get
         {
            Ibeats = WebConfigurationManager.AppSettings["Ibeats"];
            ConnString = Ibeats;
            return ConnString;
         }
    }
    

     public string Van
     {
         get
         {
            Fuel = WebConfigurationManager.AppSettings["Fuel"];
            ConnString = Fuel;
            return ConnString;
         }
         set
         {
            Fuel = WebConfigurationManager.AppSettings["Fuel"];
            ConnString = Fuel;
         }
     }


}