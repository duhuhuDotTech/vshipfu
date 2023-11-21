using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update

    long startTime = DateTime.Now.Ticks - 200000000;
    public GameObject SpawnPoint;
    public GameObject enemy1;
    public int wave = 0;
    public System.Random random = new System.Random((int)DateTime.Now.Ticks % 100000);

    List<GameObject> enemies = new List<GameObject>();

    void Start()
    {
        GameState.battleSize = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (DateTime.Now.Ticks > startTime + 200000000 && wave < GameState.fleetSize * GameState.fleetSize)
        {
            startTime = DateTime.Now.Ticks;
            spawnFleet(GameState.fleetSize,GameState.fleetLevel);
            wave++;
        }

        if (wave == GameState.fleetSize * GameState.fleetSize)
        {
            foreach
                (var e in enemies)
            {
                if (e != null)
                {
                    return;
                }
            }

            GameState.battleResult = GameState.BattleResult.Win;
            SceneManager.LoadScene("battleend");
        }

        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] != null)
            {
                if (enemies[i].transform.position.x < -10)
                {
                    GameState.battleResult = GameState.BattleResult.loss;
                    SceneManager.LoadScene("map");
                }
            }
        }

    }

    void spawnFleet(int size, int level)
    {
        int x = random.Next(size / 2, size) + size / 4;

        for (int i = 0; i < size; i++)
        {
            int wavesize = random.Next(5);
            if (wavesize <= 2)
            {
                var e1 = Instantiate(enemy1, SpawnPoint.transform);
                Enemy e = e1.GetComponent<Enemy>();
                e.acceleration = new Vector3(-0.5f, 0, 0);
                e.transform.position += new Vector3((float)+i * 6, -2f);
                e.level = level;
                e.initEnemy();
                enemies.Add(e1);
                GameState.battleSize++;
            }

            if (wavesize >= 3 && wavesize < 4)
            {
                var e1 = Instantiate(enemy1, SpawnPoint.transform);
                Enemy e = e1.GetComponent<Enemy>();
                e.acceleration = new Vector3(-0.5f, 0, 0);
                e.transform.position += new Vector3((float)+i * 6, -0f);
                e.level = level;
                e.initEnemy();
                enemies.Add(e1);
                e1 = Instantiate(enemy1, SpawnPoint.transform);
                e1.transform.position += new Vector3((float)+i * 6, -2f);
                e = e1.GetComponent<Enemy>();
                e.acceleration = new Vector3(-0.5f, 0, 0);
                e.level = level;
                e.initEnemy();
                enemies.Add(e1);
                e1 = Instantiate(enemy1, SpawnPoint.transform);
                e1.transform.position += new Vector3((float)+i * 6, 2f);
                e = e1.GetComponent<Enemy>();
                e.acceleration = new Vector3(-0.5f, 0, 0);
                e.level = level;
                e.initEnemy();
                enemies.Add(e1);
                GameState.battleSize += 3;
            }

            if (wavesize >= 4)
            {
                var e1 = Instantiate(enemy1, SpawnPoint.transform);
                Enemy e = e1.GetComponent<Enemy>();
                e.acceleration = new Vector3(-0.5f, 0, 0);
                e.transform.position += new Vector3((float)+i * 6, -0f);
                e.level = level;
                e.initEnemy();
                enemies.Add(e1);
                e1 = Instantiate(enemy1, SpawnPoint.transform);
                e1.transform.position += new Vector3((float)+i * 6, -3f);
                e = e1.GetComponent<Enemy>();
                e.acceleration = new Vector3(-0.5f, 0, 0);
                e.level = level;
                e.initEnemy();
                enemies.Add(e1);
                e1 = Instantiate(enemy1, SpawnPoint.transform);
                e1.transform.position += new Vector3((float)+i * 6, -1f);
                e = e1.GetComponent<Enemy>();
                e.acceleration = new Vector3(-0.5f, 0, 0);
                e.level = level;
                e.initEnemy();
                enemies.Add(e1);
                e1 = Instantiate(enemy1, SpawnPoint.transform);
                e1.transform.position += new Vector3((float)+i * 6, 1f);
                e = e1.GetComponent<Enemy>();
                e.acceleration = new Vector3(-0.5f, 0, 0);
                e.level = level;
                e.initEnemy();
                enemies.Add(e1);
                e1 = Instantiate(enemy1, SpawnPoint.transform);
                e1.transform.position += new Vector3((float)+i * 6, 3f);
                e = e1.GetComponent<Enemy>();
                e.acceleration = new Vector3(-0.5f, 0, 0);
                e.level = level;
                e.initEnemy();
                enemies.Add(e1);
                GameState.battleSize += 5;
            }

        }

    }
}
