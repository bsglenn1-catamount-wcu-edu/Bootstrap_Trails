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
/// Summary description for TO_POI
/// </summary>
public class TO_POI
{
    private string _POI_ID;
    private decimal _POI_Latitude;
    private decimal _POI_Logitude;
    private string _POI_Title;
    private string _POI_URL;
    private string _POI_Description;
    private string _POI_Image1;
    private string _POI_Image2;
    private string _POI_Image3;
    private string _POI_Image4;
    private string _POI_Image5;
    private string _POI_Image6;
    private string _POI_Category;

	public TO_POI()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public TO_POI(string parPOI_ID, decimal parPOI_Latitude, decimal parPOI_Longitude, 
                  string parPOI_title, string parPOI_URL, string parPOI_Description, 
                  string parPOI_Image1, string parPOI_Image2, string parPOI_Image3,
                  string parPOI_Image4, string parPOI_Image5,
                  string parPOI_Image6, string parPOI_Category)
    {
        _POI_ID = parPOI_ID;
        _POI_Latitude = parPOI_Latitude;
        _POI_Logitude = parPOI_Longitude;
        _POI_Title = parPOI_title;
        _POI_URL = parPOI_URL;
        _POI_Description = parPOI_Description;
        _POI_Image1 = parPOI_Image1;
        _POI_Image2 = parPOI_Image2;
        _POI_Image3 = parPOI_Image3;
        _POI_Image4 = parPOI_Image4;
        _POI_Image5 = parPOI_Image5;
        _POI_Image6 = parPOI_Image6;
        _POI_Category = parPOI_Category;
    }

    public string POI_ID
    {
        get
        {
            return _POI_ID;
        }
        set
        {
            _POI_ID = value;
        }
    }
    public decimal POI_Latitude
    {
        get
        {
            return _POI_Latitude;
        }
        set
        {
            _POI_Latitude = value;
        }
    }
    public decimal POI_Longitude
    {
        get
        {
            return _POI_Logitude;
        }
        set
        {
            _POI_Logitude = value;
        }
    }
    public string POI_Title
    {
        get
        {
            return _POI_Title;
        }
        set
        {
            _POI_Title = value;
        }
    }
    public string POI_URL
    {
        get
        {
            return _POI_URL;
        }
        set
        {
            _POI_URL = value;
        }
    }
    public string POI_Description
    {
        get
        {
            return _POI_Description;
        }
        set
        {
            _POI_Description = value;
        }
    }
    public string POI_Image1
    {
        get
        {
            return _POI_Image1;
        }
        set
        {
            _POI_Image1 = value;
        }
    }
    public string POI_Image2
    {
        get
        {
            return _POI_Image2;
        }
        set
        {
            _POI_Image2 = value;
        }
    }
    public string POI_Image3
    {
        get
        {
            return _POI_Image3;
        }
        set
        {
            _POI_Image3= value;
        }
    }
    public string POI_Image4
    {
        get
        {
            return _POI_Image4;
        }
        set
        {
            _POI_Image4 = value;
        }
    }
    public string POI_Image5
    {
        get
        {
            return _POI_Image5;
        }
        set
        {
            _POI_Image5 = value;
        }
    }
    public string POI_Image6
    {
        get
        {
            return _POI_Image6;
        }
        set
        {
            _POI_Image6 = value;
        }
    }
    
    public string POI_Category
    {
        get
        {
            return _POI_Category;
        }
        set
        {
            _POI_Category = value;
        }
    }
}