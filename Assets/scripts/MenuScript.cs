using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    SaveManager saveManager = new SaveManager();

    public void SortieButton()
    {
        GameState.setupWorld();
        SceneManager.LoadScene("map", LoadSceneMode.Single);
    }

    public void GoToFleetButton()
    {
        SceneManager.LoadScene("fleet", LoadSceneMode.Single);
    }

    public void GoToMapButton()
    {
        SceneManager.LoadScene("map", LoadSceneMode.Single);
    }

    public void ReturnToMapButton()
    {
        SceneManager.LoadScene("map", LoadSceneMode.Single);
    }

    public void GoToMapGacha()
    {
        SceneManager.LoadScene("gacha", LoadSceneMode.Single);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("menu", LoadSceneMode.Single);
    }

    public void GoToEnhance()
    {
        SceneManager.LoadScene("enhance", LoadSceneMode.Single);
    }

    public void exit()
    {
        UnityEngine.Application.Quit();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (GameData.shipfus.Count == 0)
        {
            GameData.InitGameData();
        }
        //saveManager.LoadGame();
        long t = DateTime.Now.Ticks;
        int x = (int)(t % 18980);
        GameState.random = new System.Random(x);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
