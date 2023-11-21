using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleetScript: MonoBehaviour
{
    // Start is called before the first frame update    

    SaveManager saveManager;
    GameData gameData;
    void Start()
    {
       saveManager.LoadGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
