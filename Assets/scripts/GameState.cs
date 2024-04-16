using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameState
{

    public static int fleetSize;
    public static BattleResult battleResult = BattleResult.none;
    public static Guid currentTarget;
    public static int battleSize;
    public static System.Random random = new System.Random(1337);
    public static int fleetLevel;

    public enum BattleResult
    {
        none = 0, Win = 1, loss = 2, withdraw = 3
    }

    public static void setupWorld()
    {
        if (GameData.enemyFleets == null)
        {

            GameData.enemyFleets = new Dictionary<System.Guid, EnemyFleet>();

 

            var ef = new EnemyFleet();
            ef.alive = true;
            ef.location = new Vector3Int(-7, -2);
            ef.size = 1;
            ef.level = 10;
            GameData.enemyFleets.Add(Guid.NewGuid(), ef);

            ef = new EnemyFleet();
            ef.alive = true;
            ef.location = new Vector3Int(-8, -2);
            ef.size = 1;
            ef.level = 10;
            GameData.enemyFleets.Add(Guid.NewGuid(), ef);

            ef = new EnemyFleet();
            ef.alive = true;
            ef.location = new Vector3Int(-7, -1);
            ef.size = 2;
            ef.level = 20;
            GameData.enemyFleets.Add(Guid.NewGuid(), ef);

            ef = new EnemyFleet();
            ef.alive = true;
            ef.location = new Vector3Int(-8, -1);
            ef.size = 2;
            ef.level = 20;
            GameData.enemyFleets.Add(Guid.NewGuid(), ef);

            ef = new EnemyFleet();
            ef.alive = true;
            ef.location = new Vector3Int(-7, 0);
            ef.size = 1;
            ef.level = 20;
            GameData.enemyFleets.Add(Guid.NewGuid(), ef);

            ef = new EnemyFleet();
            ef.alive = true;
            ef.location = new Vector3Int(-8, 0);
            ef.size = 1;
            ef.level = 20;
            GameData.enemyFleets.Add(Guid.NewGuid(), ef);

            ef = new EnemyFleet();
            ef.alive = true;
            ef.location = new Vector3Int(-5, 0);
            ef.size = 3;
            ef.level = 20;
            GameData.enemyFleets.Add(Guid.NewGuid(), ef);

            ef = new EnemyFleet();
            ef.alive = true;
            ef.location = new Vector3Int(-4, 2);
            ef.size = 3;
            ef.level = 20;
            GameData.enemyFleets.Add(Guid.NewGuid(), ef);

            ef = new EnemyFleet();
            ef.alive = true;
            ef.location = new Vector3Int(-5, -2);
            ef.size = 1;
            ef.level = 20;
            GameData.enemyFleets.Add(Guid.NewGuid(), ef);

            ef = new EnemyFleet();
            ef.alive = true;
            ef.location = new Vector3Int(-5, -4);
            ef.size = 1;
            ef.level = 20;
            GameData.enemyFleets.Add(Guid.NewGuid(), ef);

            ef = new EnemyFleet();
            ef.alive = true;
            ef.location = new Vector3Int(-3, -3);
            ef.size = 2;
            ef.level = 30;
            GameData.enemyFleets.Add(Guid.NewGuid(), ef);

            ef = new EnemyFleet();
            ef.alive = true;
            ef.location = new Vector3Int(-3, -2);
            ef.size = 2;
            ef.level = 30;
            GameData.enemyFleets.Add(Guid.NewGuid(), ef);

            ef = new EnemyFleet();
            ef.alive = true;
            ef.location = new Vector3Int(-1, -1);
            ef.size = 2;
            ef.level = 30;
            GameData.enemyFleets.Add(Guid.NewGuid(), ef);


            ef = new EnemyFleet();
            ef.alive = true;
            ef.location = new Vector3Int(-1, -2);
            ef.size = 2;
            ef.level = 30;
            GameData.enemyFleets.Add(Guid.NewGuid(), ef);

            ef = new EnemyFleet();
            ef.alive = true;
            ef.location = new Vector3Int(2, 0);
            ef.size = 2;
            ef.level = 40;
            GameData.enemyFleets.Add(Guid.NewGuid(), ef);

            ef = new EnemyFleet();
            ef.alive = true;
            ef.location = new Vector3Int(2, -1);
            ef.size = 2;
            ef.level = 40;
            GameData.enemyFleets.Add(Guid.NewGuid(), ef);

            ef = new EnemyFleet();
            ef.alive = true;
            ef.location = new Vector3Int(-3, -4);
            ef.size = 2;
            ef.level = 30;
            GameData.enemyFleets.Add(Guid.NewGuid(), ef);



            ef = new EnemyFleet();
            ef.alive = true;
            ef.location = new Vector3Int(0, 2);
            ef.size = 3;
            ef.level = 30;
            GameData.enemyFleets.Add(Guid.NewGuid(), ef);


            ef = new EnemyFleet();
            ef.alive = true;
            ef.location = new Vector3Int(2, 1);
            ef.size = 2;
            ef.level = 40;
            GameData.enemyFleets.Add(Guid.NewGuid(), ef);

            ef = new EnemyFleet();
            ef.alive = true;
            ef.location = new Vector3Int(3, 1);
            ef.size = 2;
            ef.level = 40;
            GameData.enemyFleets.Add(Guid.NewGuid(), ef);

            ef = new EnemyFleet();
            ef.alive = true;
            ef.location = new Vector3Int(4, -1);
            ef.size = 3;
            ef.level = 50;
            GameData.enemyFleets.Add(Guid.NewGuid(), ef);

            ef = new EnemyFleet();
            ef.alive = true;
            ef.location = new Vector3Int(5, -1);
            ef.size = 3;
            ef.level = 50;
            GameData.enemyFleets.Add(Guid.NewGuid(), ef);

            ef = new EnemyFleet();
            ef.alive = true;
            ef.location = new Vector3Int(6, -1);
            ef.size = 3;
            ef.level = 50;
            GameData.enemyFleets.Add(Guid.NewGuid(), ef);


            ef = new EnemyFleet();
            ef.alive = true;
            ef.location = new Vector3Int(4, -2);
            ef.size = 3;
            ef.level = 60;
            GameData.enemyFleets.Add(Guid.NewGuid(), ef);

            ef = new EnemyFleet();
            ef.alive = true;
            ef.location = new Vector3Int(5, -2);
            ef.size = 4;
            ef.level = 60;
            GameData.enemyFleets.Add(Guid.NewGuid(), ef);

            ef = new EnemyFleet();
            ef.alive = true;
            ef.location = new Vector3Int(6, -2);
            ef.size = 3;
            ef.level = 60;
            GameData.enemyFleets.Add(Guid.NewGuid(), ef);


            ef = new EnemyFleet();
            ef.alive = true;
            ef.location = new Vector3Int(4, -3);
            ef.size = 4;
            ef.level = 70;
            GameData.enemyFleets.Add(Guid.NewGuid(), ef);

            ef = new EnemyFleet();
            ef.alive = true;
            ef.location = new Vector3Int(5, -3);
            ef.size = 4;
            ef.level = 70;
            GameData.enemyFleets.Add(Guid.NewGuid(), ef);

            ef = new EnemyFleet();
            ef.alive = true;
            ef.location = new Vector3Int(6, -3);
            ef.size = 5;
            ef.level = 70;
            GameData.enemyFleets.Add(Guid.NewGuid(), ef);






            GameData.playerMapPosition = new Vector3Int(-8, -4);
        }
    }

}
