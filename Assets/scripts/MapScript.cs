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

    public Tile clearTile;

    void Start()
    {
        if (GameState.battleResult == GameState.BattleResult.Win)
        {
            foreach (var t in GameState.enemyFleets.Keys)
            {
                if (GameState.enemyFleets[t].location == GameState.enemyFleets[GameState.currentTarget].location)
                {
                    GameState.enemyFleets.Remove(t);
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

        foreach (var fleet in GameState.enemyFleets.Keys)
        {
            Tile t = fleet1;

            switch (GameState.enemyFleets[fleet].size)
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
            }
            gridFleet.SetTile(GameState.enemyFleets[fleet].location, t);
        }

        gridFleet.SetTile(GameState.playerMapPosition, Player);


        if ((Input.GetKeyUp(KeyCode.W)|| (Input.GetKeyUp(KeyCode.UpArrow))) && isTileClear(new Vector3Int(GameState.playerMapPosition.x, GameState.playerMapPosition.y + 1)))
        {
            GameState.playerMapPosition.y++;
        }
        if ((Input.GetKeyUp(KeyCode.A) || (Input.GetKeyUp(KeyCode.LeftArrow))) && isTileClear(new Vector3Int(GameState.playerMapPosition.x - 1, GameState.playerMapPosition.y)))
        {
            GameState.playerMapPosition.x--;
        }
        if ((Input.GetKeyUp(KeyCode.D) || (Input.GetKeyUp(KeyCode.RightArrow))) && isTileClear(new Vector3Int(GameState.playerMapPosition.x + 1, GameState.playerMapPosition.y)))
        {
            GameState.playerMapPosition.x++;
        }
        if ((Input.GetKeyUp(KeyCode.S) || (Input.GetKeyUp(KeyCode.DownArrow))) && isTileClear(new Vector3Int(GameState.playerMapPosition.x, GameState.playerMapPosition.y - 1)))
        {
            GameState.playerMapPosition.y--;
        }
    }

    bool isTileClear(Vector3Int pos)
    {
        var x = gridFleet.GetTile(pos);

        if (gridFleet.GetTile(pos) != null)
        {
            foreach (var fleet in GameState.enemyFleets.Keys)
            {
                if (GameState.enemyFleets[fleet].location == pos)
                {
                    GameState.currentTarget = fleet;
                    break;
                }
            }

            GameState.fleetSize = GameState.enemyFleets[GameState.currentTarget].size;
            SceneManager.LoadScene("game");
            return false;
        }
        if (gridMap.GetTile(pos) == clearTile)
            return true;

        return false;
    }
}