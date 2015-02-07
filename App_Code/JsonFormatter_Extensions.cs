using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using System.Text;

/// <summary>
/// Summary description for JsonFormatter_Extensions
/// </summary>
public static class JsonFormatter_Extensions
{
    public static string Repeat(this string str, int count)
    {
        return new StringBuilder().Insert(0, str, count).ToString();
    }

    public static bool IsEscaped(this string str, int index)
    {
        bool escaped = false;
        while (index > 0 && str[--index] == '\\') escaped = !escaped;
        return escaped;
    }

    public static bool IsEscaped(this StringBuilder str, int index)
    {
        return str.ToString().IsEscaped(index);
    }
}
