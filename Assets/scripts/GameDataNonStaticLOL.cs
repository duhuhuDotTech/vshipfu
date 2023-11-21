using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameDataNonStaticLOL
{
    // Start is called before the first frame update

    public Dictionary<Guid, Shipfu> shipfus = new Dictionary<Guid, Shipfu>();
    public List<Guid> activeFleet = new List<Guid>();
    public float Shards;
    public int mapPositon;
    public Dictionary<Guid, EnemyFleet> enemyFleets;
    public Vector3Int playerMapPosition = new Vector3Int();

}
