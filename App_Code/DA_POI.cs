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

/// <summary>
/// This page contains the methods used when Getting, Adding and Updating POIs.
/// </summary>
public class DA_POI
{
    HttpContext currentContext = HttpContext.Current;

    //NEW
    public int GetNewlyAddedPOI_ID()
    {
        currentContext.Trace.Warn("Get Newly Added POI");
        //1. Build connection object
        Connection_Info connectionInfoObject = new Connection_Info();
        string connectionString = connectionInfoObject.poiConnectionString;

        OleDbConnection connectionObject =
            new OleDbConnection(connectionString);
        connectionObject.Open();

        //Build SQL String
        string sqlString = "Select * From POIs" +
                                " Order By POI_ID DESC";

        //Build Command object with Parameter
        OleDbCommand commandObject = new OleDbCommand();
        commandObject.Connection = connectionObject;
        commandObject.CommandType = CommandType.Text;
        commandObject.CommandText = sqlString;

        //Use the DataAdapter to fill a DataTable object
        OleDbDataAdapter dataAdapterObject =
            new OleDbDataAdapter();

        //Instantiate a DataTable object
        DataTable dataTableObject = new DataTable();

        //Set the SelectCommand property of the DataAdapter object to the filled Command object
        dataAdapterObject.SelectCommand = commandObject;

        //Fill the DataTable object
        dataAdapterObject.Fill(dataTableObject);

        //Grab the values out of the first (and only) row of
        //the DataTable object and put it in a DataRow object
        DataRow dataRowObject = dataTableObject.Rows[0];

        int newPOI_ID = Convert.ToInt32(dataRowObject["POI_ID"]);

        //Close the connection. 
        connectionObject.Close();

        return newPOI_ID;
        // return dataRowObject;
    }

    public void AddImageFilename(string parPOIID,
                                    string parImageFilename,
                                    int parImageNumber)
    {
        //Build connection object
        Connection_Info connectionInfoObject = new Connection_Info();
        string connectionString = connectionInfoObject.poiConnectionString;

        OleDbConnection connectionObject =
            new OleDbConnection(connectionString);
        connectionObject.Open();

        string sqlString = "UPDATE POIs " +
                            "SET ";


        //TODO:  What if bad parImageNumber??
        if (parImageNumber == 1)
            sqlString += " POI_Image1=?";
        else if (parImageNumber == 2)
            sqlString += " POI_Image2=?";
        else if (parImageNumber == 3)
            sqlString += " POI_Image3=?";
        else if (parImageNumber == 4)
            sqlString += " POI_Image4=?";
        else if (parImageNumber == 5)
            sqlString += " POI_Image5=?";
        else if (parImageNumber == 6)
            sqlString += " POI_Image6=?";

        sqlString += " WHERE POI_ID=?";

        //Set the Command object properties  properties
        OleDbCommand commandObject = new OleDbCommand();
        commandObject.Connection = connectionObject;
        commandObject.CommandType = CommandType.Text;
        commandObject.CommandText = sqlString;

        //Add parameters to collection
        commandObject.Parameters.AddWithValue("@POI_ImageFilename", parImageFilename);
        commandObject.Parameters.AddWithValue("@POI_ID", parPOIID);

        //Execute the INSERT command on the database
        commandObject.ExecuteNonQuery();

        //Close the connection
        connectionObject.Close();
    }

    public DataTable GetPOIs()
    {
        //1. Build connection object
        Connection_Info connectionInfoObject = new Connection_Info();
        string connectionString = connectionInfoObject.poiConnectionString;

        OleDbConnection connectionObject =
            new OleDbConnection(connectionString);
        connectionObject.Open();

        //Build sql string
        string sqlString = "Select * From POIs_Category, POIs Where POIs_Category.Category_Code=POIs.POI_Category" +
                            " Order by POI_Title";

        //Build Command object 
        OleDbCommand commandObject = new OleDbCommand();
        commandObject.Connection = connectionObject;
        commandObject.CommandType = CommandType.Text;
        commandObject.CommandText = sqlString;

        //Use the DataAdapter object to fill the DataTable object
        OleDbDataAdapter dataAdapterObject = new OleDbDataAdapter();
        DataTable dataTableObject = new DataTable();

        dataAdapterObject.SelectCommand = commandObject;
        dataAdapterObject.Fill(dataTableObject);

        //Close the connection object
        connectionObject.Close();

        return dataTableObject;
    }


    /// <summary>
    /// name:         AddPOI
    /// description:  Method for Adding a new POI to the database              
    /// </summary>
    public void AddPOI(decimal parLatitude, decimal parLongitude, string parTitle, string parURL, 
        string parDescription, string parCategoryCode, bool parIsActive)
    
    {
        //1. Build connection object
        Connection_Info connectionInfoObject = new Connection_Info();
        string connectionString = connectionInfoObject.poiConnectionString;

        currentContext.Trace.Warn(" Inside AddPOI DA_POI Class Method");

        OleDbConnection connectionObject =
            new OleDbConnection(connectionString);
        connectionObject.Open();

        //3a. Build the sql string
        string sqlString = "INSERT INTO POIs " +
                   "(POI_Latitude, POI_Longitude, POI_Title, POI_URL, POI_Description, POI_Category, IsActive)" +
                   "VALUES(?, ?, ?, ?, ?, ?, ?)";

        currentContext.Trace.Warn(" sqlstring = " + sqlString);

        //3. Set the Command object  properties
        OleDbCommand commandObject = new OleDbCommand();
        commandObject.Connection = connectionObject;
        commandObject.CommandType = CommandType.Text;
        commandObject.CommandText = sqlString;

        //3c. Add parameters to collection
        commandObject.Parameters.AddWithValue("@POI_Latitude", parLatitude);
        commandObject.Parameters.AddWithValue("@POI_Longitude", parLongitude);
        commandObject.Parameters.AddWithValue("@POI_Title", parTitle);
        commandObject.Parameters.AddWithValue("@POI_URL", parURL);
        commandObject.Parameters.AddWithValue("@POI_Description", parDescription);
        commandObject.Parameters.AddWithValue("@POI_Category", parCategoryCode);
        commandObject.Parameters.AddWithValue("@IsActive", parIsActive);

        //4. Execute the INSERT command on the database
        commandObject.ExecuteNonQuery();

        //6. Close the connection. 
        connectionObject.Close();
    }

    /// <summary>
    /// name:         GetPOI
    /// description:  Method for fetching all the POI's from the database     
    /// </summary>
    /// 
    public DataRow GetPOI(string parID)
    {
        //1. Build connection object
        Connection_Info connectionInfoObject = new Connection_Info();
        string connectionString = connectionInfoObject.poiConnectionString;

        OleDbConnection connectionObject =
            new OleDbConnection(connectionString);
        connectionObject.Open();

        //Build SQL String
        string sqlString = "Select * From POIs" +
                                         " Where POI_ID=?" + " Order By POI_ID";

        //Build Command object with Parameter
        OleDbCommand commandObject = new OleDbCommand();
        commandObject.Connection = connectionObject;
        commandObject.CommandType = CommandType.Text;
        commandObject.CommandText = sqlString;
        commandObject.Parameters.AddWithValue("@POI_ID", parID);

        //Use the DataAdapter to fill a DataTable object
        OleDbDataAdapter dataAdapterObject =
            new OleDbDataAdapter();

        //Instantiate a DataTable object
        DataTable dataTableObject = new DataTable();

        //Set the SelectCommand property of the DataAdapter object to the filled Command object
        dataAdapterObject.SelectCommand = commandObject;

        //Fill the DataTable object
        dataAdapterObject.Fill(dataTableObject);

        //Grab the values out of the first (and only) row of
        //the DataTable object and put it in a DataRow object
        DataRow dataRowObject = dataTableObject.Rows[0];

        //Close the connection. 
        connectionObject.Close();

        return dataRowObject;
    }

    /// <summary>
    /// name:         UpdatePOI
    /// description:  Method for updating a selected POI in the database
    /// </summary>
    public void UpdatePOI(decimal parLatitude, decimal parLongitude, string parTitle, string parURL,
                            string parDescription, string parCategory, bool parIsActive, string parID)
    {
        //Build connection object
        Connection_Info connectionInfoObject = new Connection_Info();
        string connectionString = connectionInfoObject.poiConnectionString;

        OleDbConnection connectionObject =
            new OleDbConnection(connectionString);
        connectionObject.Open();

        //Build SQL String
        string sqlString = "UPDATE POIs " +
        "SET " + " POI_Latitude=?," +
        " POI_Longitude=?," +
        " POI_Title=?," +
        " POI_URL=?," +
        " POI_Description=?," +
        " POI_Category=?," +
        " IsActive=?"+
        " WHERE POI_ID=?";

        //Set the Command object properties  properties
        OleDbCommand commandObject = new OleDbCommand();
        commandObject.Connection = connectionObject;
        commandObject.CommandType = CommandType.Text;
        commandObject.CommandText = sqlString;

        //Add parameters to collection
        commandObject.Parameters.AddWithValue("@POI_Latitude", parLatitude);
        commandObject.Parameters.AddWithValue("@POI_Longitude", parLongitude);
        commandObject.Parameters.AddWithValue("@POI_Title", parTitle);
        commandObject.Parameters.AddWithValue("@POI_URL", parURL);
        commandObject.Parameters.AddWithValue("@POI_Description", parDescription);
        commandObject.Parameters.AddWithValue("@POI_Category", parCategory);
        commandObject.Parameters.AddWithValue("@IsActive", parIsActive);
        commandObject.Parameters.AddWithValue("@POI_ID", parID);

        //Execute the INSERT command on the database
        commandObject.ExecuteNonQuery();

        //Close the connection
        connectionObject.Close();
    }

    /// <summary>
    /// name:         DeleteImage
    /// description:  Method for deleting a specific image, used the POI ID and ImageNumber(slot passed from POI_Images.aspx)
    /// </summary>
    public void DeleteImage(string parLocationID, int parImageNumber)
    {
        //1. Build connection object
        Connection_Info connectionInfoObject = new Connection_Info();
        currentContext.Trace.Warn("imageNum= " + parImageNumber);
        string connectionString = connectionInfoObject.poiConnectionString;

        OleDbConnection connectionObject = new OleDbConnection(connectionString);
        connectionObject.Open();

        //3a. Build the sql string
        string sqlString = "UPDATE POIs " +
                            "SET ";

        //TODO:  What if bad parImageNumber??
        if (parImageNumber == 1)
            sqlString += " POI_Image1=?";
        else if (parImageNumber == 2)
            sqlString += " POI_Image2=?";
        else if (parImageNumber == 3)
            sqlString += " POI_Image3=?";
        else if (parImageNumber == 4)
            sqlString += " POI_Image4=?";
        else if (parImageNumber == 5)
            sqlString += " POI_Image5=?";
        else if (parImageNumber == 6)
            sqlString += " POI_Image6=?";

        sqlString += " WHERE POI_ID=?";

        //3. Set the Command object  properties
        OleDbCommand commandObject = new OleDbCommand();
        commandObject.Connection = connectionObject;
        commandObject.CommandType = CommandType.Text;
        commandObject.CommandText = sqlString;

        //3c. Add parameters to collection
        commandObject.Parameters.AddWithValue("@POI_ImageFilename", "");
        commandObject.Parameters.AddWithValue("@POI_ID", parLocationID);

        //4. Execute the INSERT command on the database
        commandObject.ExecuteNonQuery();

        //this determines which picture was deleted then slides the other pictures into place.
        //ex. if picture #1 was deleted, the file name in slot 2 would move to slot 1 
        //    and if their was a picture in slot 3 it would move to slot 2
        if (parImageNumber == 1)
        {
            sqlString = "UPDATE POIs SET POI_Image1 = POI_Image2 WHERE POI_ID=?";
            commandObject.CommandText = sqlString;
            commandObject.Parameters.AddWithValue("@POI_ID", parLocationID);
            currentContext.Trace.Warn("sqlstring= " + sqlString);
            commandObject.ExecuteNonQuery();
        }

        //6. Close the connection. 
        //Always do this!
        connectionObject.Close();
    }

    //this determines which picture was deleted then slides the other pictures into place.
    //ex. if picture #1 was deleted, the file name in slot 2 would move to slot 1 
    //    and if their was a picture in slot 3 it would move to slot 2
    public void OrganizeImages(string parPOIID, int parImageNumber)
    {
        //Build connection object
        Connection_Info connectionInfoObject = new Connection_Info();
        string connectionString = connectionInfoObject.poiConnectionString;

        OleDbConnection connectionObject = new OleDbConnection(connectionString);
        connectionObject.Open();
        OleDbCommand commandObject = new OleDbCommand();
        commandObject.Connection = connectionObject;
        commandObject.CommandType = CommandType.Text;

        string sqlString1 = "";
        string sqlString2 = "";
        string sqlString3 = "";
        string sqlString4 = "";
        string sqlString5 = "";
        string sqlString6 = "";

        if (parImageNumber == 1)
        {
            sqlString1 = "UPDATE POIs SET POI_Image1 = POI_Image2 WHERE POI_ID=?";
            commandObject.CommandText = sqlString1;
            commandObject.Parameters.AddWithValue("@POI_ID", parPOIID);
            commandObject.ExecuteNonQuery();

            sqlString2 = "UPDATE POIs SET POI_Image2 = POI_Image3 WHERE POI_ID=?";
            commandObject.CommandText = sqlString2;
            commandObject.Parameters.AddWithValue("@POI_ID", parPOIID);
            commandObject.ExecuteNonQuery();

            sqlString3 = "UPDATE POIs SET POI_Image3 = POI_Image4 WHERE POI_ID=?";
            commandObject.CommandText = sqlString3;
            commandObject.Parameters.AddWithValue("@POI_ID", parPOIID);
            commandObject.ExecuteNonQuery();

            sqlString4 = "UPDATE POIs SET POI_Image4 = POI_Image5 WHERE POI_ID=?";
            commandObject.CommandText = sqlString4;
            commandObject.Parameters.AddWithValue("@POI_ID", parPOIID);
            commandObject.ExecuteNonQuery();

            sqlString5 = "UPDATE POIs SET POI_Image5 = POI_Image6 WHERE POI_ID=?";
            commandObject.CommandText = sqlString5;
            commandObject.Parameters.AddWithValue("@POI_ID", parPOIID);
            commandObject.ExecuteNonQuery();

            sqlString6 = "UPDATE POIs SET POI_Image6='' WHERE POI_ID=?";
            commandObject.CommandText = sqlString6;
            commandObject.Parameters.AddWithValue("@POI_ID", parPOIID);
            commandObject.ExecuteNonQuery();
        }

        if (parImageNumber == 2)
        {
            sqlString2 = "UPDATE POIs SET POI_Image2 = POI_Image3 WHERE POI_ID=?";
            commandObject.CommandText = sqlString2;
            commandObject.Parameters.AddWithValue("@POI_ID", parPOIID);
            commandObject.ExecuteNonQuery();

            sqlString3 = "UPDATE POIs SET POI_Image3 = POI_Image4 WHERE POI_ID=?";
            commandObject.CommandText = sqlString3;
            commandObject.Parameters.AddWithValue("@POI_ID", parPOIID);
            commandObject.ExecuteNonQuery();

            sqlString4 = "UPDATE POIs SET POI_Image4 = POI_Image5 WHERE POI_ID=?";
            commandObject.CommandText = sqlString4;
            commandObject.Parameters.AddWithValue("@POI_ID", parPOIID);
            commandObject.ExecuteNonQuery();

            sqlString5 = "UPDATE POIs SET POI_Image5 = POI_Image6 WHERE POI_ID=?";
            commandObject.CommandText = sqlString5;
            commandObject.Parameters.AddWithValue("@POI_ID", parPOIID);
            commandObject.ExecuteNonQuery();

            sqlString6 = "UPDATE POIs SET POI_Image6='' WHERE POI_ID=?";
            commandObject.CommandText = sqlString6;
            commandObject.Parameters.AddWithValue("@POI_ID", parPOIID);
            commandObject.ExecuteNonQuery();
        }

        if (parImageNumber == 3)
        {
            sqlString3 = "UPDATE POIs SET POI_Image3 = POI_Image4 WHERE POI_ID=?";
            commandObject.CommandText = sqlString3;
            commandObject.Parameters.AddWithValue("@POI_ID", parPOIID);
            commandObject.ExecuteNonQuery();

            sqlString4 = "UPDATE POIs SET POI_Image4 = POI_Image5 WHERE POI_ID=?";
            commandObject.CommandText = sqlString4;
            commandObject.Parameters.AddWithValue("@POI_ID", parPOIID);
            commandObject.ExecuteNonQuery();

            sqlString5 = "UPDATE POIs SET POI_Image5 = POI_Image6 WHERE POI_ID=?";
            commandObject.CommandText = sqlString5;
            commandObject.Parameters.AddWithValue("@POI_ID", parPOIID);
            commandObject.ExecuteNonQuery();

            sqlString6 = "UPDATE POIs SET POI_Image6='' WHERE POI_ID=?";
            commandObject.CommandText = sqlString6;
            commandObject.Parameters.AddWithValue("@POI_ID", parPOIID);
            commandObject.ExecuteNonQuery();
        }
        if (parImageNumber == 4)
        {
            sqlString4 = "UPDATE POIs SET POI_Image4 = POI_Image5 WHERE POI_ID=?";
            commandObject.CommandText = sqlString4;
            commandObject.Parameters.AddWithValue("@POI_ID", parPOIID);
            commandObject.ExecuteNonQuery();

            sqlString5 = "UPDATE POIs SET POI_Image5 = POI_Image6 WHERE POI_ID=?";
            commandObject.CommandText = sqlString5;
            commandObject.Parameters.AddWithValue("@POI_ID", parPOIID);
            commandObject.ExecuteNonQuery();

            sqlString6 = "UPDATE POIs SET POI_Image6='' WHERE POI_ID=?";
            commandObject.CommandText = sqlString6;
            commandObject.Parameters.AddWithValue("@POI_ID", parPOIID);
            commandObject.ExecuteNonQuery();
        }

        if (parImageNumber == 5)
        {
            sqlString4 = "UPDATE POIs SET POI_Image5 = POI_Image6 WHERE POI_ID=?";
            commandObject.CommandText = sqlString4;
            commandObject.Parameters.AddWithValue("@POI_ID", parPOIID);
            commandObject.ExecuteNonQuery();

            sqlString5 = "UPDATE POIs SET POI_Image6='' WHERE POI_ID=?";
            commandObject.CommandText = sqlString5;
            commandObject.Parameters.AddWithValue("@POI_ID", parPOIID);
            commandObject.ExecuteNonQuery();
        }

        if (parImageNumber == 6)
        {
            sqlString6 = "UPDATE POIs SET POI_Image6='' WHERE POI_ID=?";
            commandObject.CommandText = sqlString6;
            commandObject.Parameters.AddWithValue("@POI_ID", parPOIID);
            commandObject.ExecuteNonQuery();
        }

        //Close the connection
        connectionObject.Close();
    }
}