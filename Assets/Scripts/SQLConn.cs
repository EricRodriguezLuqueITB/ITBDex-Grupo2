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
    static private IDbConnection InsertThings()
    {
        IDbConnection dbConnection = CreateAndOpenDatabase();
        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = "INSERT OR REPLACE INTO Seasons (SeasonID, season) VALUES (1, 'Summer'), (2, 'Autumm'), (3, 'Winter'), (4, 'Spring');";
        dbCommand.CommandText += "INSERT OR REPLACE INTO Types (TypeID, type) VALUES (1, 'Fire'), (2, 'Water'), (3, 'Ground'),(4, 'Wind'), (5, 'Electric');";
        dbCommand.CommandText += "INSERT OR REPLACE INTO Fakemons (id, name, SeasonID, TypeID, info) VALUES " +
        "(1, 'Flameon', 1, 1, 'Un fakemon de tipo fuego que aparece en verano.')," +
        "(2, 'Aquareon', 2, 2, 'Un fakemon de tipo agua más común en otoño.')," +
        "(3, 'Terraeon', 3, 3, 'Un fakemon de tipo tierra que domina en invierno.')," +
        "(4, 'Aireon', 4, 4, 'Un fakemon de tipo aire que vuela alto en primavera.')," +
        "(5, 'Shockeon', 1, 5, 'Un fakemon eléctrico que a menudo se encuentra durante tormentas de verano.')," +
        "(6, 'Jack o''Bat', 2, 1, 'Calabasa radioactiva autosuficiente come mursiegalos.')," +
        "(7, 'AttumBat', 2, 4, 'Calabaso-Mursiegalo mu xulo, es lo que pasa cuando una calabaza radioactiva muerde a un pobre e inocente murciegalo vegetariano.')," +
        "(8, 'Olaiuontforcris', 3, 3, 'I dont want a lot for Christmas\r\nThere is just one thing I need\r\nI dont care about the presents underneath the Christmas tree\r\nI just want you for my own\r\nMore than you could ever know\r\nMake my wish come true\r\nAll I want for Christmas is you\r\nYeah\r\nI dont want a lot for Christmas\r\nThere is just one thing I need (and I)\r\nDont care about the presents underneath the Christmas tree\r\nI dont need to hang my stocking there upon the fireplace\r\nSanta Claus wont make me happy with a toy on Christmas Day\r\nI just want you for my own\r\nMore than you could ever know\r\nMake my wish come true\r\nAll I want for Christmas is you\r\nYou, baby\r\nOh, I wont ask for much this Christmas\r\nI wont even wish for snow (and I)\r\nIm just gonna keep on waiting underneath the mistletoe\r\nI wont make a list and send it to the North Pole for Saint Nick\r\nI wont even stay awake to hear those magic reindeer click\r\nCause I just want you here tonight\r\nHolding on to me so tight\r\nWhat more can I do?\r\nOh, baby, all I want for Christmas is you\r\nYou, baby\r\nOh-oh, all the lights are shining so brightly everywhere (so brightly, baby)\r\nAnd the sound of childrens laughter fills the air (oh, oh, yeah)\r\nAnd everyone is singing (oh, yeah)\r\nI hear those sleigh bells ringing\r\nSanta, wont you bring me the one I really need? (Yeah, oh)\r\nWont you please bring my baby to me?\r\nOh, I dont want a lot for Christmas\r\nThis is all Im asking for\r\nI just wanna see my baby standing right outside my door\r\nOh, I just want you for my own\r\nMore than you could ever know\r\nMake my wish come true\r\nOh, baby, all I want for Christmas is you\r\nYou, baby\r\nAll I want for Christmas is you, baby\r\nAll I want for Christmas is you, baby\r\nAll I want for Christmas is you, baby\r\nAll I want for Christmas (all I really want) is you, baby\r\nAll I want (I want) for Christmas (all I really want) is you, baby')" +
        //Estos son los de verdad
        "(9, 'Praisand', 1, 2, 'Este espíritu es el guardián del mayor oasis de la isla veraniega. Se dice que custodia un tesoro el cual lo trajo a la vida, y cualquier persona con intención de echarle las manos encima es barrido por crueles olas de agua o arena. Es bastante agresivo y percibe a cualquier persona que se acerque a su oasis como un ladrón')" +
        "(10, 'Nortree', 3, 3, 'En el cruel frío de las islas nevadas, los aldeanos que las habitan seguían alegremente festejando las celebraciones de invierno, mientras cuidaban de un enorme árbol que pese al frío extremo lograba mantenerse en pie en el centro del gran lago helado. Los esfuerzos de los aldeanos misteriosamente dieron vida al árbol, quien se convirtió en el espíritu del Invierno. Ahora Nortree celebra festividades junto a los aldeanos, siendo literalmente el alma de la fiesta.')" +
        "(11, 'AttumBat', 2, 3, 'Este pequeño espíritu cariñoso a simple vista, es en realidad un travieso bromista, que le encanta gastarle bromas a los aldeanos y sus cultivos. No obstante, cuando sus bromas llegan demasiado lejos, ayuda a los aldeanos con sus cosechas como disculpa. Todo un bromista pero en el fondo solo quiere amigos con quien jugar y ayudar.')" +
        "(12, 'Jack O''bat', 2, 1, 'Un AttumBat que no logra ser aceptado acaba regresando como Jack O’bat, un espíritu que ahora hará más que simples bromas a aquellos que disturban su momento de solitud en su castillo o su campo de calabazas. Según testigos, las calabazas de su campo son sus amigos imaginarios.')" +
        "(13, 'Bunnillow', 4, 3, 'Tímido pero cariñoso, este espíritu vive tranquilamente en el centro de su laberinto donde se dice que hay un hermoso jardín con todas las flores y frutas de nuestro mundo. No obstante, algunos viajeros aseguran haberle visto paseando por el bosque o por el campo de flores.')" +
        "(14, 'Syzygy', 4, 5, 'Nadie sabe cuándo o de donde apareció este espíritu, pero dicen las leyendas que con su llegada las estaciones nacieron, y el mundo que solía ser vacío y desolado fue remodelado con un sinfín de vida y colores.');";

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
