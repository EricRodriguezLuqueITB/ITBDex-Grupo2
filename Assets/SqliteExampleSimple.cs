using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;

public class SqliteExampleSimple : MonoBehaviour
{
    void Start()
    {
        IDbConnection connection;

        // First use a SqlClient connection
        connection = new SqlClient.SqlConnection(@"Server=(localdb)\V11.0");
        Debug.Log("SqlClient\r\n{0}", GetServerVersion(connection));
        connection = new SqlClient.SqlConnection(@"Server=(local);Integrated Security=true");
        Debug.Log("SqlClient\r\n{0}", GetServerVersion(connection));

        // Call the same method using ODBC
        // NOTE: LocalDB requires the SQL Server 2012 Native Client ODBC driver
        connection = new Odbc.OdbcConnection(@"Driver={SQL Server Native Client 11.0};Server=(localdb)\v11.0");
        Debug.Log("ODBC\r\n{0}", GetServerVersion(connection));
        connection = new Odbc.OdbcConnection(@"Driver={SQL Server Native Client 11.0};Server=(local);Trusted_Connection=yes");
        Debug.Log("ODBC\r\n{0}", GetServerVersion(connection));

        // Call the same method using OLE DB
        connection = new OleDb.OleDbConnection(@"Provider=SQLNCLI11;Server=(localdb)\v11.0;Trusted_Connection=yes;");
        Debug.Log("OLE DB\r\n{0}", GetServerVersion(connection));
        connection = new OleDb.OleDbConnection(@"Provider=SQLNCLI11;Server=(local);Trusted_Connection=yes;");
        Debug.Log("OLE DB\r\n{0}", GetServerVersion(connection));
    }
}
