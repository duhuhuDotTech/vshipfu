using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Plane : MonoBehaviour
{

    public Vector3 acceleration = new Vector3();
    public int ownerid;
    public int type;
    public long firedtime;
    public MunitionTypes.shellState state;
    public float xmax = 30;
    public int animTimer;
    public long lastFired = DateTime.Now.Ticks;
    public GameObject bomb1;
    public static System.Random r = new System.Random();
    // Start is called before the first frame update
    void Start()
    {
        state = MunitionTypes.shellState.active;
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
            animTimer--;
            if (animTimer < 0)
            {
                Destroy(this.gameObject);
            }
            gameObject.transform.GetChild(1).transform.position += new Vector3(1 * Time.deltaTime, 1 * Time.deltaTime);
            gameObject.transform.GetChild(2).transform.position += new Vector3(0.5f * Time.deltaTime, 1 * Time.deltaTime);
            gameObject.transform.GetChild(3).transform.position += new Vector3(-1 * Time.deltaTime, 1 * Time.deltaTime);
            gameObject.transform.GetChild(4).transform.position += new Vector3(-0.5f * Time.deltaTime, 1 * Time.deltaTime);
        }

        if (gameObject.transform.position.x > xmax)
        {
            Destroy(gameObject);
        }

        if (DateTime.Now.Ticks > lastFired + 2000000)
        {
            lastFired = DateTime.Now.Ticks;

            GameObject bomb = Instantiate(bomb1, gameObject.transform.parent);
            bomb.transform.position = gameObject.transform.position;
            bomb.SetActive(true);

            var b = bomb.GetComponent<Bomb>();

            b.ownerid = 0;
            b.type = 0;
            b.acceleration = new Vector3(0, 1, 0);
            b.ymin = r.Next(-5, 0);


        }
    }
}
