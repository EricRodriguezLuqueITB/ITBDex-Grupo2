using System.Data;
using UnityEngine;
using Mono.Data.Sqlite;
using TMPro;
using System.Data.Common;
using Unity.VisualScripting;
using System.Collections.Generic;
using System.Linq;
using System.IO;

// Grupo2 (ITBDex)
static public class SQLConn
{
    static private IDbConnection InsertThings()
    {
        IDbConnection dbConnection = CreateAndOpenDatabase();
        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = "INSERT OR REPLACE INTO Seasons (SeasonID, season) VALUES (1, 'Summer'), (2, 'Autumm'), (3, 'Winter'), (4, 'Spring');";
        dbCommand.CommandText += "INSERT OR REPLACE INTO Types (TypeID, type) VALUES (1, 'Fire'), (2, 'Water'), (3, 'Ground'),(4, 'Wind'), (5, 'Electric');";
        dbCommand.CommandText += "INSERT OR REPLACE INTO Fakemons (id, name, SeasonID, TypeID, info) VALUES " +
        "(1, 'Jack o''Bat', 2, 1, 'La voluntad del Otoño|Un AttumBat que no logra ser aceptado acaba regresando como Jack O’bat, un espíritu que ahora hará más que simples bromas a aquellos que disturban su momento de solitud en su castillo o su campo de calabazas. Según testigos, las calabazas de su campo son sus amigos imaginarios.')," +
        "(2, 'AttumBat', 2, 4, 'El espíritu del Otoño|Este pequeño espíritu cariñoso a simple vista, es en realidad un travieso bromista, que le encanta gastarle bromas a los aldeanos y sus cultivos. No obstante, cuando sus bromas llegan demasiado lejos, ayuda a los aldeanos con sus cosechas como disculpa. Todo un bromista pero en el fondo solo quiere amigos con quien jugar y ayudar.')," +
        "(3, 'Nortree', 3, 3, 'El espíritu del Invierno|En el cruel frío de las islas nevadas, los aldeanos que las habitan seguían alegremente festejando las celebraciones de invierno, mientras cuidaban de un enorme árbol que pese al frío extremo lograba mantenerse en pie en el centro del gran lago helado. Los esfuerzos de los aldeanos misteriosamente dieron vida al árbol, quien se convirtió en el espíritu del Invierno. Ahora Nortree celebra festividades junto a los aldeanos, siendo literalmente el alma de la fiesta.')," +
        //Estos son los de verdad
        "(4, 'Praisand', 1, 2, 'El espíritu del Verano|Este espíritu es el guardián del mayor oasis de la isla veraniega. Se dice que custodia un tesoro el cual lo trajo a la vida, y cualquier persona con intención de echarle las manos encima es barrido por crueles olas de agua o arena. Es bastante agresivo y percibe a cualquier persona que se acerque a su oasis como un ladrón')," +
        "(5, 'Bunnillow', 4, 3, 'El espíritu del Primavera|Tímido pero cariñoso, este espíritu vive tranquilamente en el centro de su laberinto donde se dice que hay un hermoso jardín con todas las flores y frutas de nuestro mundo. No obstante, algunos viajeros aseguran haberle visto paseando por el bosque o por el campo de flores.')," +
        "(6, 'Syzygy', 4, 5, 'El creador de las estaciones|Nadie sabe cuándo o de donde apareció este espíritu, pero dicen las leyendas que con su llegada las estaciones nacieron, y el mundo que solía ser vacío y desolado fue remodelado con un sinfín de vida y colores.');";

        dbCommand.ExecuteNonQuery();
        return dbConnection;
    }

    static private IDbConnection CreateAndOpenDatabase()
    {
        //Application database Path android
        string filepath = Application.persistentDataPath + "/" + "itbdex.s3db";
        if (!File.Exists(filepath))
        {
            // UNITY_ANDROID
            WWW loadDB = new WWW("jar:file://" + Application.dataPath + "!/assets/itbdex.s3db");
            while (!loadDB.isDone) { }
            // then save to Application.persistentDataPath
            File.WriteAllBytes(filepath, loadDB.bytes);

        }
            // Open a connection to the database.
            string dbUri = $"URI=file:{Application.persistentDataPath}/itbdex.s3db";
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
        dbCommandCreateTable.ExecuteReader();
       // dbCommandCreateTable.ExecuteNonQuery();

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
