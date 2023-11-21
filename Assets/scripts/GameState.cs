using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameState
{

    public static int fleetSize;
    public static BattleResult battleResult = BattleResult.none;
    public static int mapPositon;
    public static Dictionary<Guid, EnemyFleet> enemyFleets;
    public static Vector3Int playerMapPosition = new Vector3Int();
    public static Guid currentTarget;
    public static GameData gameData;
    public static int battleSize;

    public enum BattleResult
    {
        none = 0,Win = 1, loss =2, withdraw = 3
    } 
}
