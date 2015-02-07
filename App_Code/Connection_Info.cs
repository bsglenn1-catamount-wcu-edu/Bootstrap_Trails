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

/// <summary>
/// Summary description for Connection_Info
/// </summary>
public class Connection_Info
{
    private string _poiConnectionString;

	public Connection_Info()
	{
        _poiConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;" +
            "Data Source=|DataDirectory|" + "Database_POI.accdb;";
	}

    public string poiConnectionString
    {
        get
        {
            return _poiConnectionString;
        }
    }

}
