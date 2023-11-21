using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIClickEnhance : MonoBehaviour
{
    // Start is called before the first frame update
    public void Click(int id)
    {
        EnhanceScript.updateDetails(id);
    }

    public void ClickFleet(int id)
    {
        EnhanceScript.updateDetailsFleet(id);
    }

    public void AddtoFleet()
    {
        if (!GameData.activeFleet.Contains(EnhanceScript.selectedShip))
        {
            GameData.activeFleet.Add(EnhanceScript.selectedShip);
        }
        EnhanceScript.UpdateFleet();
    }

    public void RemoveFromFleet()
    {
        if (GameData.activeFleet.Contains(EnhanceScript.selectedShip))
        {
            GameData.activeFleet.Remove(EnhanceScript.selectedShip);
        }
        EnhanceScript.UpdateFleet();
    }

    public void togglesSelectedShip(int id)
    {

    }

    public void SacrificeShips()
    {

    }
}
