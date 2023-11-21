using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    // Start is called before the first frame update

    public Vector3 acceleration;
    public bool active = true;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += acceleration * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (active)
        {
            var p = gameObject.GetComponent<Pickup>();

            if (collision.gameObject.GetComponent<PlayerController>())
            {
                active = false;
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                collision.gameObject.GetComponent<PlayerController>().health += 20;
                if (collision.gameObject.GetComponent<PlayerController>().health > collision.gameObject.GetComponent<PlayerController>().maxhealth)
                {
                    collision.gameObject.GetComponent<PlayerController>().health = collision.gameObject.GetComponent<PlayerController>().maxhealth;
                }

            }
        }
    }

}
