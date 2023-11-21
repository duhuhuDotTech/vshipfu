using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enemy;

public class FriendShip : MonoBehaviour
{
    public int health = 100;
    public Shipfu.ShipState state;
    public HealthBar healthBar;
    public long lastFired = DateTime.Now.Ticks;
    long autoshotLastUse = DateTime.Now.Ticks;
    public GameObject autoShell;
    public GameObject follow;
    public Sprite sinking;
    public Sprite deadship;
    long animEnd;

    // Start is called before the first frame update
    void Start()
    {
        healthBar.SetMaxHealth(100);
        healthBar.SetHealth(100);
        state = Shipfu.ShipState.Active;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == Shipfu.ShipState.Active)
        {
            autoFire();
        }

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
                Destroy(gameObject);
            }
        }
    }


    void autoFire()
    {
        if (DateTime.Now.Ticks > autoshotLastUse + Util.TickSecond)
        {
            autoshotLastUse = DateTime.Now.Ticks;

            GameObject shell1 = Instantiate(autoShell);


            shell1.transform.localScale = new Vector3(0.5f, 0.5f);
            shell1.transform.position = gameObject.transform.position;
            shell1.SetActive(true);

            Shell s = shell1.GetComponent<Shell>();
            s.acceleration = new Vector3(40f, 10f, 0);
            s.ownerid = 0;
            s.type = 0;
            s.ymin = -1000;
            s.shellType = MunitionTypes.shellType.secondary;

            GameObject shell2 = Instantiate(autoShell, gameObject.transform.parent);
            shell2.transform.localScale = new Vector3(0.5f, 0.5f);
            shell2.transform.position = gameObject.transform.position;
            shell2.SetActive(true);

            s = shell2.GetComponent<Shell>();
            s.acceleration = new Vector3(40f, 0f, 0);
            s.ownerid = 0;
            s.type = 0;
            s.ymin = -1000;
            s.shellType = MunitionTypes.shellType.secondary;

        }
    }
}
