using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Shell;

public class Torp : MonoBehaviour
{
    public Vector3 acceleration = new Vector3();
    public int ownerid;
    public int type;
    public long firedtime;
    public MunitionTypes.shellState state;
    public MunitionTypes.shellType shellType;
    public float ymin;
    public long animEnd;
    public float damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (state == MunitionTypes.shellState.active)
        {
            transform.position += acceleration * Time.deltaTime;
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
        if (state == MunitionTypes.shellState.active)
        {
            //Debug.Log("hittudesu");
            if (collision.gameObject.GetComponent<Enemy>())
            {
                var e = collision.gameObject.GetComponent<Enemy>();
                if (e.state == Shipfu.ShipState.Active)
                {
                    var damage = (int)(40 * (1 + (GameData.shipfus[GameData.activeFleet[0]].damage / 100)));
                    e.health -= damage;
                    state = MunitionTypes.shellState.explode;
                    gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    gameObject.transform.GetChild(2).gameObject.SetActive(true);
                    gameObject.transform.GetChild(3).gameObject.SetActive(true);
                    gameObject.transform.GetChild(4).gameObject.SetActive(true);
                    gameObject.transform.GetChild(5).gameObject.SetActive(true);
                    gameObject.transform.GetChild(5).GetChild(0).GetComponent<TextMeshProUGUI>().SetText(((int)damage).ToString());
                    animEnd = DateTime.UtcNow.Ticks + Util.TickSecond;
                    Debug.Log("hittudesu");
                }
            }
        }
    }
}
