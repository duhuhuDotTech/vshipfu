using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shipfu
{
    public float health = 100;
    public float maxhealth = 100;
    public float damage = 10;
    public float armour = 10;
    public float evasion = 10;
    public float speed = 10;
    public float reload = 10;

    public ShipID shipID;
    public ShipWeight shipWeight;
    public ShipType shipType;

    public int xp = 0;
    public int level = 0;

    public enum ShipID
    {        protoShipfu = 0,
        shippa = 1,
        tenma = 2,
        kirsch = 3
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

    public static Shipfu gachaPull()
    {
        var nuship = new Shipfu();


        var y = GameState.random.Next(50);
        y = y - y % 10;
        y = y / 10;

        nuship.shipID = (Shipfu.ShipID)y;
        nuship.shipWeight = 0;
        nuship.shipType = ShipType.gunboat;

        return nuship;
    }

    public void levelup()
    {
        level++;
        xp = 0;
        maxhealth += 10;
        health = maxhealth;
        damage += 3;
        armour += 3;
        evasion += 1;
        speed  += 1;
        reload += 1; 

        if(level % 10 == 0 && (int)shipWeight < 11)
        {
            shipWeight++;
            health += 100;
            damage += 30;
            armour += 6;

            if(shipID == 0)
            {
                shipID = (Shipfu.ShipID)GameState.random.Next(1, 3);
            }
            
        }

    }
}
