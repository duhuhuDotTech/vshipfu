using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.Device;
using static Enemy;
using UnityEngine.UI;
using TMPro;
using Unity.Mathematics;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System.Xml.Linq;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

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

    public int health = 100;
    public int maxhealth = 100;

    public Image imageShellCoolDown;
    public TMP_Text textShellCoolDown;

    public Image imageSkill1CoolDown;
    public TMP_Text textSkill1CoolDown;

    public Image imageSkill2CoolDown;
    public TMP_Text textSkill2CoolDown;

    public GameObject friendShip;

    public Dictionary<int, FriendShip> friendFleet = new Dictionary<int, FriendShip>();

    void Start()
    {
        healthBar.SetMaxHealth(health);
        textShellCoolDown.gameObject.SetActive(false);
        imageShellCoolDown.fillAmount = 0;

        // get the 
        GameObject last = gameObject;
        if (GameState.gameData.activeFleet.Count > 0)
        {
            for (int i = 0; i < GameState.gameData.activeFleet.Count; i++)
            {
                var f = Instantiate(friendShip);
                f.transform.position = new Vector3(last.transform.position.x - 1, last.transform.position.y, last.transform.position.z);
                var fs = new FriendShip();last = f;
                fs.follow = last;
                friendFleet.Add(friendFleet.Keys.Count, fs);
                
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.SetHealth(health);
        if (health <= 0)
        {
            //  state = ShipState.Sinking;
            //  GetComponent<SpriteRenderer>().sprite = sinking;
            //  sinkAnimCount--;

            //  if (sinkAnimCount < 0)
            //  {
            //      state = ShipState.Dead;
            //      GetComponent<SpriteRenderer>().sprite = deadship;
            //  }
            //  if (sinkAnimCount < -1000)
            {
                Destroy(gameObject);
            }
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
            if (gameObject.transform.position.y < 4 + speed * Time.deltaTime)
            {
                move.y += speed * Time.deltaTime;
            }
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (gameObject.transform.position.x > -9 - speed * Time.deltaTime)
            {
                move.x -= speed * Time.deltaTime;
            }
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (gameObject.transform.position.x < 9 + speed * Time.deltaTime)
            {
                move.x += speed * Time.deltaTime;
            }
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            if (gameObject.transform.position.y > -4 - speed * Time.deltaTime)
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
            GameObject g;
            if (i == 0)
            {
                g = gameObject;
            }
            else
            {
                g = friendFleet[i - 1].follow;
            }

            var dist = Vector3.Distance(g.transform.position, friendFleet[i].follow.transform.position);
            if (dist > 1 || dist < -1)
            {
                var x = (g.transform.position - new Vector3(-0.5f, 0) - friendFleet[i].follow.transform.position).normalized;
                friendFleet[i].follow.transform.Translate(x * Time.deltaTime * 2);
            }
        }
    }

    void autoFire()
    {
        if (DateTime.Now.Ticks > autoshotLastUse + Util.TickSecond / 3)
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
        }
    }

    void Fire()
    {
        if (DateTime.Now.Ticks > lastFired + Util.TickSecond)
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
        }
    }

    void FireSkill1()
    {
        if (DateTime.Now.Ticks > skill1LastUse + Util.TickSecond * 10)
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

        }
    }

    void FireSkill2()
    {
        if (DateTime.Now.Ticks > skill2LastUse + Util.TickSecond * 10)
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
            health -= e.health;
            e.health = 0;
        }
    }
}
