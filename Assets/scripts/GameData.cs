using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[System.Serializable]
public class GameData
{

    public Dictionary<Guid, Shipfu> shipfus = new Dictionary<Guid, Shipfu>();
    public List<Guid> activeFleet = new List<Guid>();
    public float fuel;
    public float ammo;
    public float steel;

    public GameData() {
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

        fuel = 0;
        ammo = 0;
        steel = 0;
    }
}
