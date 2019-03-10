using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystemScript
{
    public static void SaveTable(HighscoreTable table)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/Highscore.score";

        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, table);
        stream.Close();
    }

    public static HighscoreTable LoadScore()
    {
        string path = Application.persistentDataPath + "/Highscore.score";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            HighscoreTable data = formatter.Deserialize(stream) as HighscoreTable;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("noFile");
            return new HighscoreTable();
        }
    }
}
