using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shipfu
{
    public float health = 100;
    public float maxhealth = 100;
    public float damage = 10;
    public float armour = 10;
    public float speed = 10;
    public float reload = 10;

    public ShipID shipID;
    public ShipWeight shipWeight;
    public ShipType shipType;

    public int xp = 40;
    public int level = 0;


    public enum ShipID
    {
        shippa = 0,
        protoShipfu = 1,
        tenma = 2,
        lumi = 3,
        lia = 4,
        kirsch = 5
    }

    public enum ShipWeight
    {
        tiny1 = 0,
        tiny2 = 1,
        small1 = 2,
        small2 = 3,
        middle1 = 4,
        middle2 = 5,
        large1 = 6,
        large2 = 7,
        capital1 = 8,
        capital2 = 9,
        super1 = 10,
        super2 = 11,
    }

    public enum ShipType
    {
        gunboat,
        torpedoBoat,
        destroyer,
        Cruiser,
        Battleship,
        //Carrier,
        //Support,
        //Repair
    }

    public enum ShipState
    {
        Active,
        Sinking,
        Dead
    }


    public Shipfu gachaPull()
    {


    }
}
