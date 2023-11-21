using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shell : MonoBehaviour
{
    public static List<List<Sprite>> ShellSprites = new List<List<Sprite>>();
    public Vector3 acceleration = new Vector3();
    public int ownerid;
    public int type;
    public long firedtime;
    public MunitionTypes.shellState state;
    public MunitionTypes.shellType shellType;
    public float ymin;
    public long animEnd;
    public GameObject dmgtext;
    public float damage;

    // Start is called before
    // the first frame update
    void Start()
    {
        firedtime = DateTime.Now.Ticks;
        state = MunitionTypes.shellState.active;

        MunitionTypes.shellDamage = new Dictionary<MunitionTypes.shellType, float>();
        MunitionTypes.shellDamage.Add(MunitionTypes.shellType.primary, 20f);
        MunitionTypes.shellDamage.Add(MunitionTypes.shellType.secondary, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (firedtime + Util.TickSecond * 5 < DateTime.Now.Ticks)
        {
            GameObject.Destroy(gameObject);
        }
        if (state == MunitionTypes.shellState.active)
        {
            transform.position += acceleration * Time.deltaTime;
            acceleration.y += -(24f * Time.deltaTime);
            if (acceleration.x > 0)
            {
                acceleration.x += -(12f * Time.deltaTime);
            }

            if (transform.position.y < ymin)
            {
                state = MunitionTypes.shellState.slpash;
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                gameObject.transform.GetChild(5).gameObject.SetActive(true);
                animEnd = DateTime.UtcNow.Ticks + Util.TickSecond;
            }
        }

        if (state == MunitionTypes.shellState.slpash)
        {
            if (DateTime.UtcNow.Ticks > animEnd)
            {
                Destroy(this.gameObject);
            }
        }

        if (state == MunitionTypes.shellState.explode)
        {
            if (DateTime.UtcNow.Ticks > animEnd)
            {
                Destroy(this.gameObject);
            }

            gameObject.transform.GetChild(1).transform.position += new Vector3(1 * Time.deltaTime, 1 * Time.deltaTime);
            gameObject.transform.GetChild(2).transform.position += new Vector3(0.5f * Time.deltaTime, 1 * Time.deltaTime);
            gameObject.transform.GetChild(3).transform.position += new Vector3(-1 * Time.deltaTime, 1 * Time.deltaTime);
            gameObject.transform.GetChild(4).transform.position += new Vector3(-0.5f * Time.deltaTime, 1 * Time.deltaTime);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        var s = gameObject.GetComponent<Shell>();

        //Debug.Log("hittudesu");
        if (state == MunitionTypes.shellState.active)
        {
            if (collision.gameObject.GetComponent<Enemy>() && s.ownerid == 0)
            {
                var e = collision.gameObject.GetComponent<Enemy>();
                if (e.state == Shipfu.ShipState.Active)
                {

                    int x = GameState.random.Next(100);
                    if (x > e.evasion)
                    {

                        e.health -= s.damage - (1 - (e.armour / 100));
                        state = MunitionTypes.shellState.explode;
                        gameObject.GetComponent<SpriteRenderer>().enabled = false;
                        gameObject.transform.GetChild(0).gameObject.SetActive(true);
                        gameObject.transform.GetChild(1).gameObject.SetActive(true);
                        gameObject.transform.GetChild(2).gameObject.SetActive(true);
                        gameObject.transform.GetChild(3).gameObject.SetActive(true);
                        gameObject.transform.GetChild(4).gameObject.SetActive(true);
                        gameObject.transform.GetChild(6).gameObject.SetActive(true);
                        gameObject.transform.GetChild(6).GetChild(0).GetComponent<TextMeshProUGUI>().SetText(((int)s.damage).ToString());
                    }
                    else
                    {
                        gameObject.GetComponent<SpriteRenderer>().enabled = false;
                        state = MunitionTypes.shellState.slpash;
                        gameObject.transform.GetChild(5).gameObject.SetActive(true);
                        animEnd = DateTime.UtcNow.Ticks + Util.TickSecond;
                        gameObject.transform.GetChild(6).gameObject.SetActive(true);
                        gameObject.transform.GetChild(6).GetChild(0).GetComponent<TextMeshProUGUI>().SetText("Miss");
                    }

                    animEnd = DateTime.UtcNow.Ticks + Util.TickSecond;
                }
            }

            if (s.ownerid == 1)
            {
                if (collision.gameObject.GetComponent<PlayerController>())
                {
                    var p = collision.gameObject.GetComponent<PlayerController>();

                    int x = GameState.random.Next(100);
                    if (x > GameData.shipfus[GameData.activeFleet[0]].evasion)
                    {

                        GameData.shipfus[GameData.activeFleet[0]].health -= s.damage - GameData.shipfus[GameData.activeFleet[0]].armour;
                        state = MunitionTypes.shellState.explode;
                        gameObject.GetComponent<SpriteRenderer>().enabled = false;
                        gameObject.transform.GetChild(0).gameObject.SetActive(true);
                        gameObject.transform.GetChild(1).gameObject.SetActive(true);
                        gameObject.transform.GetChild(2).gameObject.SetActive(true);
                        gameObject.transform.GetChild(3).gameObject.SetActive(true);
                        gameObject.transform.GetChild(4).gameObject.SetActive(true);
                        gameObject.transform.GetChild(6).gameObject.SetActive(true);
                        gameObject.transform.GetChild(6).GetChild(0).GetComponent<TextMeshProUGUI>().SetText(((int)s.damage).ToString());
                        animEnd = DateTime.UtcNow.Ticks + Util.TickSecond;
                    }

                    else
                    {
                        gameObject.GetComponent<SpriteRenderer>().enabled = false;
                        state = MunitionTypes.shellState.slpash;
                        gameObject.transform.GetChild(5).gameObject.SetActive(true);
                        animEnd = DateTime.UtcNow.Ticks + Util.TickSecond;
                        gameObject.transform.GetChild(6).gameObject.SetActive(true);
                        gameObject.transform.GetChild(6).GetChild(0).GetComponent<TextMeshProUGUI>().SetText("Miss");
                    }
                }

                if (collision.gameObject.GetComponent<FriendShip>())
                {
                    var f = collision.gameObject.GetComponent<FriendShip>();

                    int x = GameState.random.Next(100);
                    if (x > GameData.shipfus[f.shipfuid].evasion)
                    {

                        f.health -= (int)s.damage - GameData.shipfus[f.shipfuid].armour;
                        state = MunitionTypes.shellState.explode;
                        gameObject.GetComponent<SpriteRenderer>().enabled = false;
                        gameObject.transform.GetChild(0).gameObject.SetActive(true);
                        gameObject.transform.GetChild(1).gameObject.SetActive(true);
                        gameObject.transform.GetChild(2).gameObject.SetActive(true);
                        gameObject.transform.GetChild(3).gameObject.SetActive(true);
                        gameObject.transform.GetChild(4).gameObject.SetActive(true);
                        gameObject.transform.GetChild(6).gameObject.SetActive(true);
                        gameObject.transform.GetChild(6).GetChild(0).GetComponent<TextMeshProUGUI>().SetText(((int)s.damage).ToString());
                        animEnd = DateTime.UtcNow.Ticks + Util.TickSecond;
                    }
                    else
                    {
                        gameObject.GetComponent<SpriteRenderer>().enabled = false;
                        state = MunitionTypes.shellState.slpash;
                        gameObject.transform.GetChild(5).gameObject.SetActive(true);
                        animEnd = DateTime.UtcNow.Ticks + Util.TickSecond;
                        gameObject.transform.GetChild(6).gameObject.SetActive(true);
                        gameObject.transform.GetChild(6).GetChild(0).GetComponent<TextMeshProUGUI>().SetText("Miss");
                    }
                }
            }
        }
    }
}