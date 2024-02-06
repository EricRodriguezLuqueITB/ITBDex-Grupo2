using System.Data;
using UnityEngine;
using Mono.Data.Sqlite;
using TMPro;

public class SqliteExampleSimple : MonoBehaviour
{
    // Resources:
    // https://www.mono-project.com/docs/database-access/providers/sqlite/

    void Start()
    {
        transform.GetComponentInChildren<TextMeshPro>().text = $"URI=file:{Application.streamingAssetsPath}/pepest.sqlite";
        // Read all values from the table.
        IDbConnection dbConnection = CreateAndOpenDatabase();
        IDbCommand dbCommand = dbConnection.CreateCommand();

        dbCommand.CommandText = "INSERT OR REPLACE INTO Pepest VALUES (0, 1)";
        dbCommand.ExecuteNonQuery();

        dbCommand.CommandText = "SELECT * FROM Pepest";
        IDataReader dataReader = dbCommand.ExecuteReader();

        while (dataReader.Read())
        {
            // The `id` has index 0, our `hits` have the index 1.
            Debug.Log(dataReader.GetInt32(0) + ", " + dataReader.GetInt32(1));
            transform.GetComponentInChildren<TextMeshPro>().text = dataReader.GetInt32(0) + ", " + dataReader.GetInt32(1);
        }

        // Remember to always close the connection at the end.
        dbConnection.Close();
    }

    private IDbConnection CreateAndOpenDatabase()
    {
        // Open a connection to the database.
        string dbUri = $"URI=file:{Application.streamingAssetsPath}/pepest.sqlite";
        IDbConnection dbConnection = new SqliteConnection(dbUri);
        dbConnection.Open();
        
        // Create a table for the fakemons in the database if it does not exist yet.
        IDbCommand dbCommandCreateTable = dbConnection.CreateCommand();
        dbCommandCreateTable.CommandText = "CREATE TABLE IF NOT EXISTS Pepest (id INTEGER PRIMARY KEY, name INTEGER )";
        dbCommandCreateTable.ExecuteReader();
        
        return dbConnection;
    }
    /*
    public InputField inputDBInstance;
    public InputField username;
    public InputField password;
    public Text connectionStatus;

    public static bool RemoteDBConnection = false;

    void Start()
    {

        inputDBInstance.text = "";
        connectionStatus.text = "No";
    }

    static SqlDataAdapter TableDataFetcher;
    static DataTable SQLDataTable;
    public static SqlConnection AppConnection = new SqlConnection();
    static SqlCommand SQLCommand = new SqlCommand();
    static UnityEngine.Random RandomNumber = new UnityEngine.Random();
    private static readonly UnityEngine.Random RandomBase = new UnityEngine.Random();

    public static void FConnectToDatabase(string DBServer, string SQLServerInstance, string Database, string UserName, string Password)
    {
        AppConnection = new SqlConnection(string.Format(@"Data Source={0}\{1}; Initial Catalog={2}; User id={3}; Password={4};", DBServer + (RemoteDBConnection == true ? ".ksdl.local" : ""), "Initial Catalogue=" + SQLServerInstance, Database, UserName, Password));

        if (AppConnection == null)
        {
            AppConnection = new SqlConnection() { ConnectionString = "INVALID" };
        }
    }

    public static void FOpenConnection(SqlConnection ConnectionString)
    {
        try
        {
            if (ConnectionString.ConnectionString == "INVALID")
            {
                throw new Exception();
            }

            if (ConnectionString.State == ConnectionState.Closed)
            {
                ConnectionString.Open();
            }
        }
        catch (Exception ex)
        {
            FException(ex);
        }
    }

    public static void FCloseConnection(SqlConnection ConnectionString)
    {
        try
        {
            if (ConnectionString.State == ConnectionState.Open)
            {
                ConnectionString.Close();
            }
        }
        catch (Exception ex)
        {
            FException(ex);
        }
    }

    public static void FGetStockTypes(ref DataTable Table, SqlConnection Connection)
    {
        try
        {
            TableDataFetcher = new SqlDataAdapter("SELECT SHORT_DESC FROM stock_type", Connection);
            TableDataFetcher.Fill(Table);
        }
        catch (Exception ex)
        {
            FException(ex);
        }
    }
    public static void FException(string ExceptionMessage, string ExceptionTitle = "ERROR!")

    {
        //MessageBox.Show(ExceptionMessage, ExceptionTitle, Buttons);
        Debug.Log("Error");

    }

    public static void FException(Exception ex, string ExceptionTitle = "ERROR!")
    {
        if (ex.InnerException != null && ex.InnerException.InnerException != null)
        {
            Debug.Log(ex);
        }
        else if (ex.InnerException != null && ex.InnerException.Message != null)
        {
            Debug.Log(ex);
        }
        else
        {
            Debug.Log(ex);
        }
    }

    public void OnDatabaseConnectButton_Click()
    {
        string[] ConnectionStringElements = inputDBInstance.text.Split(new string[] { "\\", ":" }, StringSplitOptions.RemoveEmptyEntries);

        if (ConnectionStringElements.Length != 3)
        {
            Debug.LogError("Invalid Connection string entered!");
        }
        else
        {
            FConnectToDatabase(ConnectionStringElements[0], ConnectionStringElements[1], ConnectionStringElements[2], username.text, password.text);

            FOpenConnection(SQLFunctions.AppConnection);

            if (AppConnection.State == ConnectionState.Open)
            {
                connectionStatus.text = "Connection Successful";
                FCloseConnection(AppConnection);
            }
        }
    }*/
}
