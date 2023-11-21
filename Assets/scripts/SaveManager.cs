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
        SaveGame();
    }

    public void LoadGame()
    {

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
        Shipfu s = new Shipfu();
    }
}
