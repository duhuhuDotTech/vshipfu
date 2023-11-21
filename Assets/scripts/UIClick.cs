using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIClick : MonoBehaviour
{
    public void Click(int id)
    {
       FleetSceneScript.updateDetails(id);
    }

    public void ClickFleet(int id)
    {
        FleetSceneScript.updateDetailsFleet(id);
    }

    public void AddtoFleet()
    {
        if(!GameState.gameData.activeFleet.Contains(FleetSceneScript.selectedShip))
        {
            GameState.gameData.activeFleet.Add(FleetSceneScript.selectedShip);
        }
        FleetSceneScript.UpdateFleet();
    }

    public void RemoveFromFleet()
    {
        if (GameState.gameData.activeFleet.Contains(FleetSceneScript.selectedShip))
        {
            GameState.gameData.activeFleet.Remove(FleetSceneScript.selectedShip);
        }
        FleetSceneScript.UpdateFleet();
    }
}
