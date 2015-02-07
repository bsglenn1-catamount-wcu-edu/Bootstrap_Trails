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

using System.Data.OleDb;

using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;

/// <summary>
/// Summary description for JSON_Utils
/// </summary>
public class JSON_Utils
{
    HttpContext currentContext = HttpContext.Current;
    /// <summary>
    /// The ConvertTourDataTableTOJson method converts a filled data table into a
    /// JSON string to be saved as a text file by the caller.
    /// </summary>
    public string CovertCategoryDataTableToJSON(DataTable parFilledDataTable)
    {
        //Convert DataTable to List collection of Transfer Objects
        List<TO_POI_Cateogry> items = new List<TO_POI_Cateogry>();

        foreach (DataRow row in parFilledDataTable.Rows)
        {
            string ID = Convert.ToString(row["Category_Code"]);
            string title = Convert.ToString(row["Category_Name"]);
            string desc = Convert.ToString(row["Category_Description"]);
            string fileName = "Category_" + ID + ".js";

            TO_POI_Cateogry itemTransferObject = new TO_POI_Cateogry(ID, title, desc, fileName);
            items.Add(itemTransferObject);
        }

        //Create JSON-formatted string
        JavaScriptSerializer oSerializer = new JavaScriptSerializer();
        string JSONString = oSerializer.Serialize(items);

        //Format json string
        string formattedJSONString = JSONString;

        return formattedJSONString;
    }

    /// <summary>
    /// The CovertTourPOIDataTableToJSON method returns a JSON string with only the locations in
    /// a specific tour.
    /// </summary>
    public static string CovertCategoryPOIDataTableToJSON(string parCategoryID, DataTable parFilledDataTable, bool parCheckIsActive)
    {
        //1. Build connection object
        Connection_Info connectionInfoObject = new Connection_Info();
        string connectionString = connectionInfoObject.poiConnectionString;

        OleDbConnection connectionObject = new OleDbConnection(connectionString);
        connectionObject.Open();

        //Build sql string
        string sqlString = "SELECT * FROM POIs "
                            + " WHERE POIs.POI_Category=?"
                            + "And IsActive=?"
                            + "ORDER BY POI_ID;";

        //Build Command object with Parameter
        OleDbCommand commandObject = new OleDbCommand();
        commandObject.Connection = connectionObject;
        commandObject.CommandType = CommandType.Text;
        commandObject.CommandText = sqlString;
        commandObject.Parameters.AddWithValue("@POI_Category", parCategoryID);
        commandObject.Parameters.AddWithValue("@parCheckIsActive", parCheckIsActive);

        //4. Use the DataAdapter object to fill the DataTable object

        //Instantiate a DataAdapter object
        OleDbDataAdapter dataAdapterObject = new OleDbDataAdapter();

        //Instantiate a DataTable object
        DataTable dataTableObject = new DataTable();

        //Set the SelectCommand property of the DataAdapter object 
        //to the filled Command object
        dataAdapterObject.SelectCommand = commandObject;

        //Fill the DataTable object
        dataAdapterObject.Fill(dataTableObject);

        //6. Close the connection:  Always do this!!!!
        connectionObject.Close();

        //Convert DataTable to List collection of Transfer Objects
        List<TO_POI> items = new List<TO_POI>();

        foreach (DataRow row in dataTableObject.Rows)
        {

            string ID = Convert.ToString(row["POI_ID"]);
            decimal latitude = Convert.ToDecimal(row["POI_Latitude"]);
            decimal longitude = Convert.ToDecimal(row["POI_Longitude"]);
            string title = Convert.ToString(row["POI_Title"]);
            string url = Convert.ToString(row["POI_URL"]);
            string description = Convert.ToString(row["POI_Description"]);
            string category = Convert.ToString(row["POI_Category"]);
            string imgFileName1 = Convert.ToString(row["POI_Image1"]);
            string imgFileName2 = Convert.ToString(row["POI_Image2"]);
            string imgFileName3 = Convert.ToString(row["POI_Image3"]);
            string imgFileName4 = Convert.ToString(row["POI_Image4"]);
            string imgFileName5 = Convert.ToString(row["POI_Image5"]);
            string imgFileName6 = Convert.ToString(row["POI_Image6"]);

            TO_POI itemTransferObject =
                    new TO_POI(ID, latitude, longitude, title, url, description, imgFileName1,
                               imgFileName2, imgFileName3, imgFileName4, imgFileName5, imgFileName6, category);

            items.Add(itemTransferObject);
        }

        //Create JSON-formatted string
        JavaScriptSerializer oSerializer = new JavaScriptSerializer();
        string JSONString = oSerializer.Serialize(items);

       

        return JSONString;
    }

    /// <summary>
    /// name:         ConvertDataTableToJSON
    /// description:  Method that is passed a filled DataTable object
    ///               and returns a JSON-formatted string.
    /// </summary>
    public static string ConvertDataTableToJSON(DataTable parFilledDataTable)
    {      
        //Convert DataTable to List collection of Transfer Objects
        List<TO_POI> items = new List<TO_POI>();

        foreach (DataRow row in parFilledDataTable.Rows)
        {
            string ID = Convert.ToString(row["POI_ID"]);
            decimal latitude = Convert.ToDecimal(row["POI_Latitude"]);
            decimal longitude = Convert.ToDecimal(row["POI_Longitude"]);
            string title = Convert.ToString(row["POI_Title"]);
            string url = Convert.ToString(row["POI_URL"]);
            string description = Convert.ToString(row["POI_Description"]);
            string category = Convert.ToString(row["POI_Category"]);
            string imgFileName1 = Convert.ToString(row["POI_Image1"]);
            string imgFileName2 = Convert.ToString(row["POI_Image2"]);
            string imgFileName3 = Convert.ToString(row["POI_Image3"]);
            string imgFileName4 = Convert.ToString(row["POI_Image4"]);
            string imgFileName5 = Convert.ToString(row["POI_Image5"]);
            string imgFileName6 = Convert.ToString(row["POI_Image6"]);

            TO_POI itemTransferObject =
                    new TO_POI(ID, latitude, longitude, title, url, description, imgFileName1,
                               imgFileName2, imgFileName3, imgFileName4, imgFileName5, imgFileName6, category);

            items.Add(itemTransferObject);
        }

        //Create JSON-formatted string
        JavaScriptSerializer oSerializer = new JavaScriptSerializer();
        string JSONString = oSerializer.Serialize(items);

        
        return JSONString;
    }
    /// <summary>
    /// This method saves all tours as a JSON file. 
    /// It calls the appropriate methods and then writes the output string to a file.
    /// </summary>
    public void SaveJSONCategoriesAsFile()
    {
        //Instantiate and Fill a DataTable object with test data

        DA_POI_Category tourDataObject = new DA_POI_Category();
        DataTable tourDataTableObject = tourDataObject.GetAllCategories();

        /*Instantiate Method to retrieve the data from the filled DataTable object 
        and concatenates it into a JSON-formatted string*/

        JSON_Utils utils = new JSON_Utils();
        string tourJsonString = utils.CovertCategoryDataTableToJSON(tourDataTableObject);
        string pathToJSONFile;

        //Set path and filename for saved JSON file
        //pathToJSONFile = currentContext.Server.MapPath("../Data_Categories/ListOfCategories.js");
        pathToJSONFile = currentContext.Server.MapPath("~/ListOfCategories.js");

        //Create StreamWriter object            
        StreamWriter StreamWriter = new StreamWriter(pathToJSONFile);

        //Writing JSON string to the file.           
        StreamWriter.Write(tourJsonString);

        ////Close the file.           
        StreamWriter.Close();
    }

    public void SaveJSONCategoriesAndLocationsAsFile()
    {
        //Instantiate and Fill a DataTable object with test data

        DA_POI_Category tourDataObject = new DA_POI_Category();
        DataTable tourDataTableObject = tourDataObject.GetAllCategories();

        foreach (DataRow row in tourDataTableObject.Rows)
        {
            string tourID = Convert.ToString(row["Category_Code"]);
            bool checkIsActive = true;
            /*Instantiate Method to retrieve the data from the filled DataTable object 
            and concatenates it into a JSON-formatted string*/

            JSON_Utils utils = new JSON_Utils();
            string tourJsonString = JSON_Utils.CovertCategoryPOIDataTableToJSON(tourID, tourDataTableObject, checkIsActive);
            string pathToJSONFile;

            //Set path and filename for saved JSON file
            //pathToJSONFile = currentContext.Server.MapPath("../Data_Categories/Category_" + tourID + ".js");
            pathToJSONFile = currentContext.Server.MapPath("~/Category_" + tourID + ".js");
            //Create StreamWriter object            
            StreamWriter StreamWriter = new StreamWriter(pathToJSONFile);

            //Writing JSON string to the file.           
            StreamWriter.Write(tourJsonString);

            ////Close the file.           
            StreamWriter.Close();
        }
    }

    /// <summary>
    /// name:         SaveJSONStringAsFile
    /// description:  Method that is passed a JSON string and writes it to a file.
    /// </summary>
    /// <param name="parJSONString"></param>
    public void SaveJSONStringAsFile(string parJSONString)
    {
        string pathToJSONFile;

        //Set path and filename for saved JSON file
        pathToJSONFile = currentContext.Server.MapPath("~/Data_POI.js");

        //Create StreamWriter object            
        StreamWriter StreamWriter1 = new StreamWriter(pathToJSONFile);

        //Writing JSON string to the file.           
        StreamWriter1.Write(parJSONString);

        ////Close the file.           
        StreamWriter1.Close();
    }
  
}
