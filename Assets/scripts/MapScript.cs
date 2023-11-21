using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class MapScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Tilemap gridFleet;
    public Tilemap gridMap;
    public Tile Player;

    public Tile fleet1;
    public Tile fleet2;
    public Tile fleet3;
    public Tile fleet4;
    public Tile fleet5;

    public Tile clearTile;

    void Start()
    {
        if (GameState.battleResult == GameState.BattleResult.Win)
        {
            foreach (var t in GameData.enemyFleets.Keys)
            {
                if (GameData.enemyFleets[t].location == GameData.enemyFleets[GameState.currentTarget].location)
                {
                    GameData.enemyFleets.Remove(t);
                }
            }
            GameState.battleResult = GameState.BattleResult.none;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            UnityEngine.Application.Quit();
        }

        for (int x = -9; x < 9; x++)
        {
            for (int y = -5; y < 5; y++)
            {
                gridFleet.SetTile(new Vector3Int(x, y), null);
            }
        }

        foreach (var fleet in GameData.enemyFleets.Keys)
        {
            Tile t = fleet1;

            switch (GameData.enemyFleets[fleet].size)
            {
                case 1:
                    t = fleet1;
                    break;
                case 2:
                    t = fleet2;
                    break;
                case 3:
                    t = fleet3;
                    break;
                case 4:
                    t = fleet4;
                    break;
                case 5:
                    t = fleet5;
                    break;
            }
            gridFleet.SetTile(GameData.enemyFleets[fleet].location, t);
        }

        gridFleet.SetTile(GameData.playerMapPosition, Player);


        if ((Input.GetKeyUp(KeyCode.W)|| (Input.GetKeyUp(KeyCode.UpArrow))) && isTileClear(new Vector3Int(GameData.playerMapPosition.x, GameData.playerMapPosition.y + 1)))
        {
            GameData.playerMapPosition.y++;
        }
        if ((Input.GetKeyUp(KeyCode.A) || (Input.GetKeyUp(KeyCode.LeftArrow))) && isTileClear(new Vector3Int(GameData.playerMapPosition.x - 1, GameData.playerMapPosition.y)))
        {
            GameData.playerMapPosition.x--;
        }
        if ((Input.GetKeyUp(KeyCode.D) || (Input.GetKeyUp(KeyCode.RightArrow))) && isTileClear(new Vector3Int(GameData.playerMapPosition.x + 1, GameData.playerMapPosition.y)))
        {
            GameData.playerMapPosition.x++;
        }
        if ((Input.GetKeyUp(KeyCode.S) || (Input.GetKeyUp(KeyCode.DownArrow))) && isTileClear(new Vector3Int(GameData.playerMapPosition.x, GameData.playerMapPosition.y - 1)))
        {
            GameData.playerMapPosition.y--;
        }
    }

    bool isTileClear(Vector3Int pos)
    {
        var x = gridFleet.GetTile(pos);

        if (gridFleet.GetTile(pos) != null)
        {
            foreach (var fleet in GameData.enemyFleets.Keys)
            {
                if (GameData.enemyFleets[fleet].location == pos)
                {
                    GameState.currentTarget = fleet;
                    break;
                }
            }

            GameState.fleetSize = GameData.enemyFleets[GameState.currentTarget].size;
            GameState.fleetLevel = GameData.enemyFleets[GameState.currentTarget].level;
            SceneManager.LoadScene("game");
            return false;
        }
        if (gridMap.GetTile(pos) == clearTile)
            return true;

        return false;
    }
}