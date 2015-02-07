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
/// Summary description for Class1
/// </summary>
public class DA_POI_Category
{
    HttpContext currentContext = HttpContext.Current;

    /// <summary>
    /// name:         GetAllCategories
    /// description:  Method that retrieves all Categories from database
    /// </summary>
    public DataTable GetAllCategories()
    {
        //1. Build connection object

        Connection_Info connectionInfoObject = new Connection_Info();
        string connectionString = connectionInfoObject.poiConnectionString;

        OleDbConnection connectionObject = new OleDbConnection(connectionString);
        connectionObject.Open();

        //2. Construct sql string
        string sqlString = "Select * From POIs_Category" + " Order By Category_Code";

        //3. Build Command object 
        OleDbCommand commandObject = new OleDbCommand();
        commandObject.Connection = connectionObject;
        commandObject.CommandType = CommandType.Text;
        commandObject.CommandText = sqlString;

        //4. Use the DataAdapter object to fill the DataTable object

        //Instantiate a DataAdapter object
        OleDbDataAdapter dataAdapterObject = new OleDbDataAdapter();

        //Instantiate a DataTable object
        DataTable dataTableObject = new DataTable();

        //Set the SelectCommand property of the DataAdapter object to the filled Command object
        dataAdapterObject.SelectCommand = commandObject;

        //Fill the DataTable object
        dataAdapterObject.Fill(dataTableObject);

        //6. Close the connection:  Always do this!!!!
        connectionObject.Close();

        return dataTableObject;
    }

    /// <summary>
    /// name:         GetCategoryByCategoryCode
    /// description:  Retrieves Category name by CategoryCode
    /// </summary>
    public DataRow GetCategoryByCategoryCode(string parCategoryCode)
    {
        //1. Build connection object
        Connection_Info connectionInfoObject = new Connection_Info();
        string connectionString = connectionInfoObject.poiConnectionString;

        OleDbConnection connectionObject = new OleDbConnection(connectionString);
        connectionObject.Open();

        //2. Construct sql string
        //Build sql string
        string sqlString = "Select * From POIs_Category" + " Where Category_Code=?" + " Order By Category_Code";

        //Build Command object with Parameter
        OleDbCommand commandObject = new OleDbCommand();
        commandObject.Connection = connectionObject;
        commandObject.CommandType = CommandType.Text;
        commandObject.CommandText = sqlString;
        commandObject.Parameters.AddWithValue("@Category_Code", parCategoryCode);

        //4. Use the DataAdapter to fill a DataTable object
        //Instantiate a DataAdapter object
        OleDbDataAdapter dataAdapterObject = new OleDbDataAdapter();

        //Instantiate a DataTable object
        DataTable dataTableObject = new DataTable();

        //Set the SelectCommand property of the DataAdapter object to the filled Command object
        dataAdapterObject.SelectCommand = commandObject;

        //Fill the DataTable object
        dataAdapterObject.Fill(dataTableObject);

        //Grab the values out of the first (and only) row of
        //the DataTable object and put it in a DataRow object
        DataRow dataRowObject = dataTableObject.Rows[0];

        //6. Close the connection. 
        //Always do this!
        connectionObject.Close();

        return dataRowObject;
    }

    /// <summary>
    /// The add category method adds a category to the database given user input
    /// </summary>
    public void AddCategory(string parCategoryName, string parCategoryDesc)
    {
        //1. Build connection object
        Connection_Info connectionInfoObject = new Connection_Info();
        string connectionString = connectionInfoObject.poiConnectionString;

        OleDbConnection connectionObject = new OleDbConnection(connectionString);
        connectionObject.Open();

        //3a. Build the sql string
        string sqlString = "INSERT INTO POIs_Category " +
                   "(Category_Name, Category_Description)" +
                   "VALUES(?, ?)";

        //3. Set the Command object  properties
        OleDbCommand commandObject = new OleDbCommand();
        commandObject.Connection = connectionObject;
        commandObject.CommandType = CommandType.Text;
        commandObject.CommandText = sqlString;

        //3c. Add parameters to collection
        commandObject.Parameters.AddWithValue("@Category_Name", parCategoryName);
        commandObject.Parameters.AddWithValue("@Category_Description", parCategoryDesc);

        //4. Execute the INSERT command on the database
        commandObject.ExecuteNonQuery();

        //6. Close the connection. 
        //Always do this!
        connectionObject.Close();
    }

    /// <summary>
    /// The update category method updates a specific category based on a given category ID and
    /// supplied user input.
    /// </summary>
    public void UpdateCategory(string parCategoryName, string parCategoryDesc, string parCategoryCode)
    {
        //1. Build connection object
        Connection_Info connectionInfoObject = new Connection_Info();
        string connectionString = connectionInfoObject.poiConnectionString;

        OleDbConnection connectionObject = new OleDbConnection(connectionString);
        connectionObject.Open();

        //3a. Build the sql string
        string sqlString = "UPDATE POIs_Category " +
        "SET Category_Name=?, " + 
        "Category_Description=? " +
        "WHERE Category_Code=? ";
        currentContext.Trace.Warn("sqlString = " + sqlString);

        //3b. Set the Command object properties  properties
        OleDbCommand commandObject = new OleDbCommand();
        commandObject.Connection = connectionObject;
        commandObject.CommandType = CommandType.Text;
        commandObject.CommandText = sqlString;

        //3c. Add parameters to collection
        commandObject.Parameters.AddWithValue("@Category_Name", parCategoryName);
        commandObject.Parameters.AddWithValue("@Category_Description", parCategoryDesc);
        commandObject.Parameters.AddWithValue("@Category_Code", parCategoryCode);

        //4. Execute the INSERT command on the database
        commandObject.ExecuteNonQuery();

        //6. Close the connection. 
        //Always do this!
        connectionObject.Close();
    }

    /// <summary>
    /// The DeleteCategory method deletes a specified category
    /// </summary>
    public void DeleteCategory(int parCategoryID)
    {
        //1. Build connection object
        Connection_Info connectionInfoObject = new Connection_Info();
        string connectionString = connectionInfoObject.poiConnectionString;

        OleDbConnection connectionObject = new OleDbConnection(connectionString);
        connectionObject.Open();

        //3a. Build the sql string
        //SQL (Updating POIs to have Uncategorized Category Code
        string sqlString = "UPDATE POIs SET POI_Category=1" +
            " WHERE POI_Category=?";
        
        //SQL (Delete Category)
        string sqlString2 = "DELETE * FROM POIs_Category WHERE Category_Code=?";

        //3. Set the Command object  properties
        OleDbCommand commandObject = new OleDbCommand();
        commandObject.Connection = connectionObject;
        commandObject.CommandType = CommandType.Text;
        commandObject.CommandText = sqlString;

        //3c. Add parameters to collection
        commandObject.Parameters.AddWithValue("@POI_Category", parCategoryID);

        //Execute SQL (Updating POIs to have Uncategorized Category Code
        commandObject.ExecuteNonQuery();

        //Execute SQL (Delete Category)
        commandObject.CommandText = sqlString2;
        commandObject.Parameters.AddWithValue("@Category_Code", parCategoryID);
        commandObject.ExecuteNonQuery();

        //6. Close the connection. 
        //Always do this!
        connectionObject.Close();
    }
}