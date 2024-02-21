using System.Data;
using UnityEngine;
using Mono.Data.Sqlite;
using TMPro;
using System.Data.Common;
using Unity.VisualScripting;
using System.Collections.Generic;
using System.Linq;

// Grupo2 (ITBDex)
static public class SQLConn
{
    // Resources:
    // https://www.mono-project.com/docs/database-access/providers/sqlite/


    static private IDbConnection InsertThings()
    {
        IDbConnection dbConnection = CreateAndOpenDatabase();
        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = "INSERT OR REPLACE INTO Seasons (SeasonID, season) VALUES (1, 'Verano'), (2, 'Otoño'), (3, 'Invierno'), (4, 'Primavera');";
        dbCommand.CommandText += "INSERT OR REPLACE INTO Types (TypeID, type) VALUES (1, 'Fuego'), (2, 'Agua'), (3, 'Tierra'),(4, 'Aire'), (5, 'Eléctrico');";
        dbCommand.CommandText += "INSERT OR REPLACE INTO Fakemons (id, name, SeasonID, TypeID, info) VALUES (1, 'Flameon', 1, 1, 'Un fakemon de tipo fuego que aparece en verano.')," +
            "(2, 'Aquareon', 2, 2, 'Un fakemon de tipo agua más común en otoño.')," +
            "(3, 'Terraeon', 3, 3, 'Un fakemon de tipo tierra que domina en invierno.')," +
            "(4, 'Aireon', 4, 4, 'Un fakemon de tipo aire que vuela alto en primavera.')," +
            "(5, 'Shockeon', 1, 5, 'Un fakemon eléctrico que a menudo se encuentra durante tormentas de verano.')," +
            "(6, 'Fakermon', 1, 3, 'Un fakemon falsísimo que no se encuentra.')," +
            "(7, 'Porromon', 1, 2, 'Un porro artístico con forma de fakemon.');";
        dbCommand.ExecuteNonQuery();
        return dbConnection;
    }

    static private IDbConnection CreateAndOpenDatabase()
    {
        // Open a connection to the database.
        string dbUri = $"URI=file:{Application.streamingAssetsPath}/itbdex.sqlite";
        IDbConnection dbConnection = new SqliteConnection(dbUri);
        dbConnection.Open();

        // Create a table for the fakemons in the database if it does not exist yet.
        IDbCommand dbCommandCreateTable = dbConnection.CreateCommand();
        dbCommandCreateTable.CommandText = "CREATE TABLE IF NOT EXISTS Seasons (SeasonID INT PRIMARY KEY, season VARCHAR(255));";
        dbCommandCreateTable.CommandText += "CREATE TABLE IF NOT EXISTS Types (TypeID INT PRIMARY KEY, type VARCHAR(255));";
        dbCommandCreateTable.CommandText += "CREATE TABLE IF NOT EXISTS Fakemons (id INT PRIMARY KEY, name VARCHAR(255), SeasonID INT, TypeID INT, info TEXT, FOREIGN KEY (SeasonID) REFERENCES Seasons(SeasonID), FOREIGN KEY (TypeID) REFERENCES Types(TypeID));";
        //dbCommandCreateTable.ExecuteReader();

        dbCommandCreateTable.CommandText += "CREATE VIEW IF NOT EXISTS FakemonsView AS " +
            "SELECT F.id, F.name, S.season AS SeasonName, T.type AS TypeName, F.info " +
            "FROM Fakemons F " +
            "JOIN Seasons S ON F.SeasonID = S.SeasonID " +
            "JOIN Types T ON F.TypeID = T.TypeID;";
        dbCommandCreateTable.ExecuteNonQuery();

        return dbConnection;
    }
    static public List<Fakemon> GetFakemon()
    {
        List<Fakemon> fakemons = new List<Fakemon>();
        IDbConnection dbConnection = InsertThings();
        IDbCommand dbCommanSelect = dbConnection.CreateCommand();
        dbCommanSelect.CommandText = "SELECT * FROM FakemonsView";
        IDataReader reader = dbCommanSelect.ExecuteReader();

        while (reader.Read())
        {
            Fakemon fakemon = new(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4));
            fakemons.Add(fakemon);
            //Debug.Log(reader.GetInt32(0) + reader.GetString(1) + reader.GetString(2) + reader.GetString(3) + reader.GetString(4));
        }
        fakemons.Where(fk => fk.fakename[0] == 'a');

        dbConnection.Close();
        return fakemons;
    }
}
