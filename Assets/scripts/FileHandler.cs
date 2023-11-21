using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using Newtonsoft.Json;
using System.Net;

public static class FileHandler
{
    public static void Load(string filename)
    {
        string path = Path.Combine(Application.persistentDataPath, filename);
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

                GameDataNonStaticLOL loadedData = JsonConvert.DeserializeObject<GameDataNonStaticLOL>(datatoload);

                GameData.enemyFleets = loadedData.enemyFleets;
                GameData.activeFleet = loadedData.activeFleet;
                GameData.mapPositon = loadedData.mapPositon;
                GameData.shipfus = loadedData.shipfus;
                GameData.Shards = loadedData.Shards;
                GameData.playerMapPosition = loadedData.playerMapPosition;

                //copy to static here
            }
            catch (Exception e)
            {
                Debug.LogError("oh fuck loading is absolutely shitted");
            }
        }

    }

    public static void Save(string fileName)
    {
        string path = Path.Combine(Application.persistentDataPath, fileName);

        //copy to non static here
        GameDataNonStaticLOL gdnsl = new GameDataNonStaticLOL();
        gdnsl.enemyFleets = GameData.enemyFleets;
        gdnsl.activeFleet = GameData.activeFleet;
        gdnsl.mapPositon = GameData.mapPositon;
        gdnsl.shipfus = GameData.shipfus;
        gdnsl.Shards = GameData.Shards;
        gdnsl.playerMapPosition = GameData.playerMapPosition;

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));

            string dataToStore = JsonConvert.SerializeObject(gdnsl);
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
