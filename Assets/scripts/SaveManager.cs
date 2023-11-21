using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [SerializeField] private string fileName;

    public string CurrentSaveFile = "autosave.game";

    private void Start()
    {
        Debug.Log("start");
    }

    public void NewGame()
    {
        GameState.gameData = new GameData();
        SaveGame();
    }

    public void LoadGame()
    {
        Debug.Log("loading");
        GameState.gameData = FileHandler.Load(CurrentSaveFile);

        if (GameState.gameData == null)
        {
            Debug.LogError("no save found");
            NewGame();
        }
    }

    public void SaveGame()
    {
        FileHandler.Save(CurrentSaveFile);
    }

    public void OnApplicationQuit()
    {
        SaveGame();
    }

    private void CreateNewSave()
    {
        GameState.gameData.steel = 100;
        GameState.gameData.fuel = 500;
        GameState.gameData.ammo = 400;

        Shipfu s = new Shipfu();
    }
}
