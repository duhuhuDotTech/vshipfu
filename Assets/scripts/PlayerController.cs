using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.Device;
using static Enemy;
using UnityEngine.UI;
using Unity.Mathematics;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System.Xml.Linq;

public class PlayerController : MonoBehaviour
{
    float speed = 3f;
    long lastFired = DateTime.Now.Ticks;
    long skill1LastUse = DateTime.Now.Ticks;
    long skill2LastUse = DateTime.Now.Ticks;
    long autoshotLastUse = DateTime.Now.Ticks;

    public GameObject shell1;
    public GameObject autoShell;
    public GameObject Skill1;
    public GameObject Skill2;
    public HealthBar healthBar;

    public GameObject AirSpawnLocation;

    public Image imageShellCoolDown;
    public TMP_Text textShellCoolDown;

    public Image imageSkill1CoolDown;
    public TMP_Text textSkill1CoolDown;

    public Image imageSkill2CoolDown;
    public TMP_Text textSkill2CoolDown;

    public GameObject friendShip;

    public List<GameObject> friendFleet = new List<GameObject>();

    void Start()
    {
        healthBar.SetMaxHealth((int)GameData.shipfus[GameData.activeFleet[0]].maxhealth);

        textShellCoolDown.gameObject.SetActive(false);
        imageShellCoolDown.fillAmount = 0;

        GameData.shipfus[GameData.activeFleet[0]].health = GameData.shipfus[GameData.activeFleet[0]].maxhealth;

        // get the 
        GameObject last = gameObject;

        if (GameData.activeFleet.Count > 0)
        {
            for (int i = 1; i < GameData.activeFleet.Count; i++)
            {
                var f = Instantiate(friendShip);
                f.transform.position = new Vector3(last.transform.position.x - 1, last.transform.position.y, last.transform.position.z);
                var x = f.GetComponent<FriendShip>();
                x.initShipfu(GameData.activeFleet[i]);
                friendFleet.Add(f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.SetHealth((int)GameData.shipfus[GameData.activeFleet[0]].health);
        if (GameData.shipfus[GameData.activeFleet[0]].health <= 0)
        {
            Destroy(gameObject);

            GameState.battleResult = GameState.BattleResult.loss;
            SceneManager.LoadScene("map");
        }

        if (DateTime.Now.Ticks < lastFired + Util.TickSecond)
        {
            float x = 1 - ((float)(DateTime.Now.Ticks - lastFired) / Util.TickSecond);
            string xs = UnityEngine.Mathf.Round(x * 100).ToString();
            textShellCoolDown.gameObject.SetActive(true);
            textShellCoolDown.text = xs;

            imageShellCoolDown.fillAmount = x;
        }
        else
        {
            textShellCoolDown.gameObject.SetActive(false);
            imageShellCoolDown.fillAmount = 0;
        }

        if (DateTime.Now.Ticks < skill1LastUse + Util.TickSecond * 10)
        {
            float x = 1 - ((float)(DateTime.Now.Ticks - skill1LastUse) / (Util.TickSecond * 10));
            string xs = UnityEngine.Mathf.Round(x * 100).ToString();
            textSkill1CoolDown.gameObject.SetActive(true);
            textSkill1CoolDown.text = xs;

            imageSkill1CoolDown.fillAmount = x;
        }
        else
        {
            textSkill1CoolDown.gameObject.SetActive(false);
            imageSkill1CoolDown.fillAmount = 0;
        }

        if (DateTime.Now.Ticks < skill2LastUse + Util.TickSecond * 10)
        {
            float x = 1 - ((float)(DateTime.Now.Ticks - skill2LastUse) / (Util.TickSecond * 10));
            string xs = UnityEngine.Mathf.Round(x * 100).ToString();
            textSkill2CoolDown.gameObject.SetActive(true);
            textSkill2CoolDown.text = xs;

            imageSkill2CoolDown.fillAmount = x;
        }
        else
        {
            textSkill2CoolDown.gameObject.SetActive(false);
            imageSkill2CoolDown.fillAmount = 0;
        }

        autoFire();

        UpdatePlayerInput();
    }

    void UpdatePlayerInput()
    {
        Vector3 move = gameObject.transform.position;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            if (gameObject.transform.position.y < 4 + (speed * 1 + (GameData.shipfus[GameData.activeFleet[0]].speed / 100)) * Time.deltaTime)
            {
                move.y += speed * Time.deltaTime;
            }
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (gameObject.transform.position.x > -9 - (speed * 1 + (GameData.shipfus[GameData.activeFleet[0]].speed / 100)) * Time.deltaTime)
            {
                move.x -= speed * Time.deltaTime;
            }
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (gameObject.transform.position.x < 9 + (speed * 1 + (GameData.shipfus[GameData.activeFleet[0]].speed / 100)) * Time.deltaTime)
            {
                move.x += speed * Time.deltaTime;
            }
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            if (gameObject.transform.position.y > -4 - (speed * 1 + (GameData.shipfus[GameData.activeFleet[0]].speed / 100)) * Time.deltaTime)
            {
                move.y -= speed * Time.deltaTime;
            }
        }

        if (Input.GetKey(KeyCode.Space))
        {
            Fire();
        }

        if (Input.GetKey(KeyCode.Alpha1))
        {
            FireSkill1();
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            FireSkill2();
        }

        gameObject.transform.position = move;


        for (int i = 0; i < friendFleet.Count; i++)
        {
            if (friendFleet[i] != null)
            {
                GameObject g;
                if (i == 0)
                {
                    g = gameObject;
                }
                else
                {
                    g = friendFleet[i - 1];

                    if (g == null)
                    {
                        g = findFollow(i);
                    }
                }

                var dist = Vector3.Distance(g.transform.position, friendFleet[i].transform.position);
                if (dist > 1 || dist < -1)
                {
                    var x = (g.transform.position - new Vector3(-0.5f, 0) - friendFleet[i].transform.position).normalized;
                    var f = friendFleet[i].GetComponent<FriendShip>();
                    friendFleet[i].transform.Translate((x * Time.deltaTime * 2) * (1 + (GameData.shipfus[f.shipfuid].speed / 100)));
                }
            }
        }
    }

    GameObject findFollow(int index)
    {
        for (int i = index - 1; i >= 0; i--)
        {
            if (friendFleet[i] != null)
            {
                return friendFleet[i].gameObject;
            }
        }

        return gameObject;
    }

    void autoFire()
    {
        double x = (1 - (GameData.shipfus[GameData.activeFleet[0]].reload / 100));
        var y = ((Util.TickSecond / 2) * (1 - (GameData.shipfus[GameData.activeFleet[0]].reload / 100)));
        var p = DateTime.Now.Ticks > autoshotLastUse + ((Util.TickSecond / 2) * (1 - (GameData.shipfus[GameData.activeFleet[0]].reload / 100)));
        if (DateTime.Now.Ticks > autoshotLastUse + ((Util.TickSecond / 2) * x))
        {
            autoshotLastUse = DateTime.Now.Ticks;
            GameObject shell1 = Instantiate(autoShell, gameObject.transform.parent);

            shell1.transform.localScale = new Vector3(0.5f, 0.5f);
            shell1.transform.position = gameObject.transform.position;
            shell1.SetActive(true);

            Shell s = shell1.GetComponent<Shell>();
            s.acceleration = new Vector3(40f, 10f, 0);
            s.ownerid = 0;
            s.type = 0;
            s.ymin = -1000;
            s.shellType = MunitionTypes.shellType.secondary;
            s.damage = MunitionTypes.shellDamage[s.shellType] * (1 + (GameData.shipfus[GameData.activeFleet[0]].damage / 100));


            GameObject shell2 = Instantiate(autoShell, gameObject.transform.parent);
            shell2.transform.localScale = new Vector3(0.5f, 0.5f);
            shell2.transform.position = gameObject.transform.position;
            shell2.SetActive(true);

            s = shell2.GetComponent<Shell>();
            s.acceleration = new Vector3(40f, 5f, 0);
            s.ownerid = 0;
            s.type = 0;
            s.ymin = -1000;
            s.shellType = MunitionTypes.shellType.secondary;
            s.damage = MunitionTypes.shellDamage[s.shellType] * (1 + (GameData.shipfus[GameData.activeFleet[0]].damage / 100));

            GameObject shell3 = Instantiate(autoShell, gameObject.transform.parent);
            shell3.transform.localScale = new Vector3(0.5f, 0.5f);
            shell3.transform.position = gameObject.transform.position;
            shell3.SetActive(true);

            s = shell3.GetComponent<Shell>();
            s.acceleration = new Vector3(40f, 0f, 0);
            s.ownerid = 0;
            s.type = 0;
            s.ymin = -1000;
            s.shellType = MunitionTypes.shellType.secondary;
            s.damage = MunitionTypes.shellDamage[s.shellType] * (1 + (GameData.shipfus[GameData.activeFleet[0]].damage / 100));
        }
    }

    void Fire()
    {
        double x = (1 - (GameData.shipfus[GameData.activeFleet[0]].reload / 100));
        if (DateTime.Now.Ticks > lastFired + (Util.TickSecond * x))
        {
            lastFired = DateTime.Now.Ticks;

            GameObject shell = Instantiate(shell1, gameObject.transform.parent);

            shell.transform.localScale = new Vector3(0.5f, 0.5f);
            shell.transform.position = gameObject.transform.position;
            shell.SetActive(true);

            Shell s = shell.GetComponent<Shell>();
            s.acceleration = new Vector3(20f, 10f, 0);
            s.ownerid = 0;
            s.type = 0;
            s.ymin = shell.transform.position.y;
            s.shellType = MunitionTypes.shellType.primary;
            s.damage = MunitionTypes.shellDamage[s.shellType] * (1 + (GameData.shipfus[GameData.activeFleet[0]].damage / 100));
        }
    }

    void FireSkill1()
    {
        double x = (1 - (GameData.shipfus[GameData.activeFleet[0]].reload / 100));
        if (DateTime.Now.Ticks > skill1LastUse + (Util.TickSecond * 10 * x))
        {
            skill1LastUse = DateTime.Now.Ticks;

            GameObject skill1 = Instantiate(Skill1, gameObject.transform.parent);

            skill1.transform.localScale = new Vector3(0.5f, 0.5f);
            skill1.transform.position = gameObject.transform.position;
            skill1.transform.position += new Vector3(0f, 1f);
            skill1.SetActive(true);

            Torp t = skill1.GetComponent<Torp>();

            t.acceleration = new Vector3(10f, 0, 0);

            t.ownerid = 0;
            t.type = 0;
            t.damage = MunitionTypes.shellDamage[t.shellType] * (1 + (GameData.shipfus[GameData.activeFleet[0]].damage / 100));


        }
    }

    void FireSkill2()
    {
        double x = (1 - (GameData.shipfus[GameData.activeFleet[0]].reload / 100));
        if (DateTime.Now.Ticks > skill2LastUse + (Util.TickSecond * 10 * x))
        {
            skill2LastUse = DateTime.Now.Ticks;

            GameObject skill2 = Instantiate(Skill2, gameObject.transform.parent);

            skill2.transform.localScale = new Vector3(0.5f, 0.5f);
            skill2.transform.position = AirSpawnLocation.transform.position;

            skill2.transform.position += new Vector3(0f, 1f);
            skill2.SetActive(true);

            Plane p = skill2.GetComponent<Plane>();

            p.acceleration = new Vector3(10f, 0, 0);

            p.ownerid = 0;
            p.type = 0;

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Enemy>())
        {
            var e = collision.gameObject.GetComponent<Enemy>();
            GameData.shipfus[GameData.activeFleet[0]].health -= e.health;
            e.health = 0;
        }
    }
}
