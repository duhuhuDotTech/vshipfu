using System;
using System.Collections.Generic;
using UnityEngine;
using static Enemy;
using static Shipfu;

public class FriendShip : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public Shipfu.ShipState state;
    public HealthBar healthBar;
    public long lastFired = DateTime.Now.Ticks;
    long autoshotLastUse = DateTime.Now.Ticks;
    public GameObject autoShell;
    public GameObject follow;
    public Sprite sinking;
    public Sprite deadship;
    long animEnd;

    public Guid shipfuid;

    public Sprite textureShip0;
    public Sprite textureShip1;
    public Sprite textureShip2;
    public Sprite textureShip3;
    public Sprite textureShip4;
    public Sprite textureShip5;
    public Sprite textureShip6;

    public List<Sprite> shipsprites = new List<Sprite>();

    // Start is called before the first frame update
    //void Start()
    //{
    //    health = GameData.shipfus[shipfuid].health;
    //    maxHealth = GameData.shipfus[shipfuid].health;
    //    healthBar.SetMaxHealth((int) maxHealth); ;
    //    healthBar.SetHealth((int) health) ;
    //    state = Shipfu.ShipState.Active;
    //}

    public void initShipfu(Guid shipid)
    {
        shipfuid = shipid;
        health = GameData.shipfus[shipid].health;
        maxHealth = GameData.shipfus[shipid].health;
        healthBar.SetMaxHealth((int)maxHealth); 
        healthBar.SetHealth((int)health);
        state = Shipfu.ShipState.Active;

        shipsprites.Add(textureShip0);
        shipsprites.Add(textureShip1);
        shipsprites.Add(textureShip2);
        shipsprites.Add(textureShip3);
        shipsprites.Add(textureShip4);
        shipsprites.Add(textureShip5);
        shipsprites.Add(textureShip6);


    }

    // Update is called once per frame
    void Update()
    {

        if (state == Shipfu.ShipState.Active)
        {
            autoFire();
        }

        healthBar.SetHealth((int)health);
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
        else
        {
            GetComponent<SpriteRenderer>().sprite = shipsprites[(int)GameData.shipfus[shipfuid].shipID];
        }
    }


    void autoFire()
    {

        double x = (1 - (GameData.shipfus[shipfuid].reload / 100));
        if (DateTime.Now.Ticks > autoshotLastUse + (Util.TickSecond))
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
            s.damage = MunitionTypes.shellDamage[s.shellType] * (1 + (GameData.shipfus[shipfuid].damage / 100));

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
            s.damage = MunitionTypes.shellDamage[s.shellType] * (1 + (GameData.shipfus[shipfuid].damage / 100));

        }
    }
}
