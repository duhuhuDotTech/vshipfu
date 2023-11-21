using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[System.Serializable]
public static class GameData
{
    public static Dictionary<Guid, Shipfu> shipfus = new Dictionary<Guid, Shipfu>();
    public static List<Guid> activeFleet = new List<Guid>();
    public static float Shards;
    public static int mapPositon;
    public static Dictionary<Guid, EnemyFleet> enemyFleets;
    public static Vector3Int playerMapPosition = new Vector3Int();

    public static void InitGameData()
    {

        FileHandler.Load("lol.txt");
        if (shipfus.Count == 0)
        {
            shipfus = new Dictionary<Guid, Shipfu>();

            Shipfu shipfu = new Shipfu();
            shipfu.shipID = Shipfu.ShipID.shippa;
            shipfu.shipWeight = Shipfu.ShipWeight.small1;
            shipfu.shipType = Shipfu.ShipType.destroyer;
            shipfus.Add(Guid.NewGuid(), shipfu);

            shipfu = new Shipfu();
            shipfu.shipID = Shipfu.ShipID.protoShipfu;
            shipfu.shipWeight = Shipfu.ShipWeight.tiny1;
            shipfu.shipType = Shipfu.ShipType.gunboat;
            shipfus.Add(Guid.NewGuid(), shipfu);

            shipfu = new Shipfu();
            shipfu.shipID = Shipfu.ShipID.protoShipfu;
            shipfu.shipWeight = Shipfu.ShipWeight.tiny1;
            shipfu.shipType = Shipfu.ShipType.gunboat;
            shipfus.Add(Guid.NewGuid(), shipfu);

            activeFleet = new List<Guid>();
            activeFleet = shipfus.Keys.ToList();

            Shards = 50;

            FileHandler.Save("lol.txt");
        }



    }
}
