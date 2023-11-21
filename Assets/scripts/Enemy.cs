using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;
//using Random = System.Random;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public int health = 100;
    public Shipfu.ShipState state;
    public HealthBar healthBar;
    public Sprite sinking;
    public Sprite deadship;
    long animEnd;
    public Vector3 acceleration;
    private long lastFired = DateTime.Now.Ticks;
    public GameObject shell1;
    public GameObject pickup;

    void Start()
    {
        healthBar.SetMaxHealth(health);
        healthBar.SetHealth(health);
        state = Shipfu.ShipState.Active;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.SetHealth(health);
        if (health <= 0)
        {
            if (state == Shipfu.ShipState.Active)
            {
                animEnd = DateTime.UtcNow.Ticks + (Util.TickSecond);
                state = Shipfu.ShipState.Sinking;
                GetComponent<SpriteRenderer>().sprite = sinking;
            }

            if (DateTime.UtcNow.Ticks > animEnd)
            {
                state = Shipfu.ShipState.Dead;
                GetComponent<SpriteRenderer>().sprite = deadship;
            }

            if (DateTime.UtcNow.Ticks > animEnd + Util.TickSecond)
            {
                GameObject pick = Instantiate(pickup, gameObject.transform.parent);
                //pick.transform.localScale = new Vector3(0.5f, 0.5f);
                pick.transform.position = gameObject.transform.position;
                pick.SetActive(true);
                gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }

        transform.position += acceleration * Time.deltaTime;

        if (state == Shipfu.ShipState.Active && DateTime.Now.Ticks > lastFired)
        {
            long a = (long)Random.Range(60000000, 75000000);
            Debug.Log(a);
            lastFired = DateTime.Now.Ticks +a;

            GameObject shell = Instantiate(shell1, gameObject.transform.parent);

            shell.transform.localScale = new Vector3(0.5f, 0.5f);
            shell.transform.position = gameObject.transform.position;
            shell.SetActive(true);

            Shell s = shell.GetComponent<Shell>();

            s.acceleration = new Vector3(-20f, 10f, 0);

            s.ownerid = 1;
            s.type = 0;
            s.ymin = shell.transform.position.y;
        }
    }

}
