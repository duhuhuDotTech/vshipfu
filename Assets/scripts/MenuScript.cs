using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    SaveManager saveManager = new SaveManager();

    public void SortieButton()
    {
        GameState.enemyFleets = new Dictionary<System.Guid, EnemyFleet>();

        var ef = new EnemyFleet();
        ef.alive = true;
        ef.location = new Vector3Int(-7, 0);
        ef.size = 1;
        GameState.enemyFleets.Add(Guid.NewGuid(), ef);

        ef = new EnemyFleet();
        ef.alive = true;
        ef.location = new Vector3Int(-7, -1);
        ef.size = 2;
        GameState.enemyFleets.Add(Guid.NewGuid(), ef);

        ef = new EnemyFleet();
        ef.alive = true;
        ef.location = new Vector3Int(-7, -2);
        ef.size = 3;
        GameState.enemyFleets.Add(Guid.NewGuid(), ef);

        GameState.playerMapPosition = new Vector3Int(-8, -4);

        SceneManager.LoadScene("map");
    }

    public void GoToFleetButton()
    {
        SceneManager.LoadScene("fleet");
    }

    public void GoToMapButton()
    {
        SceneManager.LoadScene("map");
    }
        

    // Start is called before the first frame update
    void Start()
    {
        saveManager.LoadGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
