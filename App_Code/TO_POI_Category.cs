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
public class TO_POI_Cateogry
{
    private string _Category_Code;
    private string _Category_Name;
    private string _Category_Description;
    private string _Tour_File_Name;
    

	public TO_POI_Cateogry()
	{

	}
    public TO_POI_Cateogry(string parCategoryCode, string parCategoryName, string parCategoryDesc, string parTourFileName)
    {
        _Category_Code = parCategoryCode;
        _Category_Name = parCategoryName;
        _Category_Description = parCategoryDesc;
        _Tour_File_Name = parTourFileName;
        
    }

    public string CategoryCode
    {
        get
        {
            return _Category_Code;
        }
        set
        {
            _Category_Code = value;
        }
    }
    public string CategoryName
    {
        get
        {
            return _Category_Name;
        }
        set
        {
            _Category_Name = value;
        }
    }
 public string CategoryDescription
    {
        get
        {
            return _Category_Description;
        }
        set
        {
            _Category_Description = value;
        }
    }
    public string Tour_FileName
    {
        get
        {
            return _Tour_File_Name;
        }
        set
        {
            _Tour_File_Name = value;
        }
    }
    
}


