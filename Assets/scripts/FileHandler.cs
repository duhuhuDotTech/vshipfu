using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public static class FileHandler
{
    public static GameData Load(string filename)
    {
        string path = Path.Combine(Application.persistentDataPath, filename);
        GameData loadedData = null;
        if (File.Exists(path))
        {
            try
            {

                string datatoload = "";
                using (FileStream stream = new FileStream(path, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        datatoload = reader.ReadToEnd();
                    }
                }

                loadedData = JsonUtility.FromJson<GameData>(datatoload);

            }
            catch (Exception e)
            {
                Debug.LogError("oh fuck loading is absolutely shitted");
            }
        }
        return loadedData;
    }

    public static void Save(string fileName)
    {
        string path = Path.Combine(Application.persistentDataPath, fileName);

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));

            string dataToStore = JsonUtility.ToJson(GameState.gameData, true);
            string data2 = JsonUtility.ToJson(GameState.gameData.shipfus, true);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
            throw;
        }
    }
}
