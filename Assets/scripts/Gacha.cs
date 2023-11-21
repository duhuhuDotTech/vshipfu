using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Gacha : MonoBehaviour
{
    Shipfu gacha = null;
    public GameObject nushipfu;
    public GameObject nushiptext;
    static List<Sprite> shipfuSprites = new List<Sprite>();

    public Sprite textureShip1;
    public Sprite textureShip2;
    public Sprite textureShip3;

    public GameObject summonNumber;

    // Start is called before the first frame update
    void Start()
    {
        shipfuSprites.Add(textureShip1);
        shipfuSprites.Add(textureShip2);
        shipfuSprites.Add(textureShip2);
        shipfuSprites.Add(textureShip2);
        shipfuSprites.Add(textureShip2);
        shipfuSprites.Add(textureShip2);
        shipfuSprites.Add(textureShip2);
        shipfuSprites.Add(textureShip2);
    }

    // Update is called once per frame
    void Update()
    {
        summonNumber.GetComponent<TextMeshProUGUI>().SetText("Summons remaining: " + GameData.Shards);
    }

    public void summon()
    {
        if(GameData.Shards>= 10)
        {
            GameData.Shards-=10;
            gacha = Shipfu.gachaPull();
            GameData.shipfus.Add(System.Guid.NewGuid(), gacha);
            nushipfu.SetActive(true);
            nushiptext.SetActive(true);
            nushipfu.GetComponent<Image>().sprite = shipfuSprites[(int)gacha.shipID];
        }
    }
}
