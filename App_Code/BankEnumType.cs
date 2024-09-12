using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BankEnumType
/// </summary>
public static class BankEnumType
{
    public static string GetBankName(int bankid)
    {
        switch (bankid)
        {
            case 1:
                return "ICICI Bank";
            default:
                return "";
        }
    }
}