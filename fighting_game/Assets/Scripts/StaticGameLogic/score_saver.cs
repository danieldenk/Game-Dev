using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

/**
 * This class saves the highest score to a local file and reads the data from it
 */
public class score_saver
{
    // Path containing the highscores
   static string path = Application.persistentDataPath + "/Highscore.csv";

    // This method saves the state of our score
    public static void SaveState(int score)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, score);
        stream.Close();
    }

    // This method loads the state of our score
    public static int LoadState()
    {
        // If the file exists then we are going to read the value from it
        if (File.Exists(path))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(path, FileMode.Open);
            int score = (int) binaryFormatter.Deserialize(fileStream);
            fileStream.Close();
            return score;
        }
        // else we are assuming that the score has not yet been set so we are setting it to 0
        else
        {
            SaveState(0);
            return 0;
        }
    }
}