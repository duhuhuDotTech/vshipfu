using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleEndScript : MonoBehaviour
{
    public Sprite textureRank1;
    public Sprite textureRank2;
    public Sprite textureRank3;
    public Sprite textureRank4;
    public Sprite textureRank5;
    public Sprite textureRank6;
    public Sprite textureRank7;
    public Sprite textureRank8;
    public Sprite textureRank9;
    public Sprite textureRank10;
    public Sprite textureRank11;
    public Sprite textureRank12;


    public Sprite textureShip0;
    public Sprite textureShip1;
    public Sprite textureShip2;
    public Sprite textureShip3;
    public Sprite textureShip4;
    public Sprite textureShip5;
    public Sprite textureShip6;


    List<Sprite> rankSprites = new List<Sprite>();
    List<Sprite> shipfuSprites = new List<Sprite>();

    List<GameObject> fleetList = new List<GameObject>();
    List<System.Guid> fleet = GameData.activeFleet;

    List<Slider> sliders = new List<Slider>();
    List<Transform> texts = new List<Transform>();

    int xpGranted;
    int anim = 0;

    public GameObject nushipfu;
    public GameObject nushiptext;

    Shipfu gacha = null;

    public GameObject shardCount;
    public GameObject shardTotal;

    // Start is called before the first frame update
    void Awake()
    {
        rankSprites.Add(textureRank1);
        rankSprites.Add(textureRank2);
        rankSprites.Add(textureRank3);
        rankSprites.Add(textureRank4);
        rankSprites.Add(textureRank5);
        rankSprites.Add(textureRank6);
        rankSprites.Add(textureRank7);
        rankSprites.Add(textureRank8);
        rankSprites.Add(textureRank9);
        rankSprites.Add(textureRank10);
        rankSprites.Add(textureRank11);
        rankSprites.Add(textureRank12);

        shipfuSprites.Add(textureShip0);
        shipfuSprites.Add(textureShip1);
        shipfuSprites.Add(textureShip2);
        shipfuSprites.Add(textureShip3);
        shipfuSprites.Add(textureShip4);
        shipfuSprites.Add(textureShip5);
        shipfuSprites.Add(textureShip6);

        List<System.Guid> shipfus = GameData.shipfus.Keys.ToList();

        for (int i = 1; i <= 5; i++)
        {
            fleetList.Add(GameObject.Find("fleet" + i.ToString()));
    
            var y = fleetList[i - 1].transform.Find("shipfuImage");
            y.gameObject.GetComponent<Image>().sprite = null;
            y = fleetList[i - 1].transform.Find("rankImage");
            y.gameObject.GetComponent<Image>().sprite = null;
            var leveltext = fleetList[i - 1].transform.Find("level");
            leveltext.GetComponent<TextMeshProUGUI>().SetText("");

            var xp = fleetList[i - 1].transform.Find("xp");
            xp.gameObject.SetActive(false);
        }

        int ii = 0;
        foreach (var item in fleet)
        {
            var y = fleetList[ii].transform.Find("rankImage");
            y.gameObject.GetComponent<Image>().sprite = rankSprites[(int)GameData.shipfus[item].shipWeight];

            y = fleetList[ii].transform.Find("shipfuImage");
            y.gameObject.GetComponent<Image>().sprite = shipfuSprites[(int)GameData.shipfus[item].shipID];

            var leveltext = fleetList[ii].transform.Find("level");
            leveltext.GetComponent<TextMeshProUGUI>().SetText("Level: " + GameData.shipfus[item].level.ToString());
            texts.Add(leveltext);

            var xp = fleetList[ii].transform.Find("xp");
            xp.gameObject.SetActive(true);

            var slider = xp.gameObject.GetComponent<Slider>();
            slider.value = GameData.shipfus[item].xp;
            slider.maxValue = (((GameData.shipfus[item].level - (GameData.shipfus[item].level % 10) / 10))*100) + 100;
            slider.minValue = 0;
            sliders.Add(slider);
            GameData.shipfus[item].xp += GameState.battleSize * 50;
            ii++;
        }


        int newShards = 0;
        int x = GameState.random.Next(GameState.battleSize * 10);
        if (x > GameState.battleSize * 6)
        {
            newShards++;
        }
        if (x > GameState.battleSize * 9)
        {
            newShards++;
        }
        if (x > GameState.battleSize * 95)
        {
            newShards += 5;
        }

        GameData.Shards += newShards;

        shardCount.GetComponent<TextMeshProUGUI>().SetText("+ " + newShards.ToString());
        shardTotal.GetComponent<TextMeshProUGUI>().SetText(GameData.Shards.ToString());

    }

    // Update is called once per frame
    void Update()
    {
        anim++;
        if (anim % 5 == 0)
        {
            if (xpGranted < (GameState.battleSize * 50) + 50)
            {
                int ii = 0;
                foreach (var item in GameData.activeFleet)
                {
                    if (sliders[ii].value == ((GameData.shipfus[item].level - (GameData.shipfus[item].level % 10) / 10)) + 100)
                    {
                        sliders[ii].value = 0;
                        GameData.shipfus[item].levelup();
                        texts[ii].GetComponent<TextMeshProUGUI>().SetText("Level: " + GameData.shipfus[item].level.ToString());
                        sliders[ii].maxValue = ((GameData.shipfus[item].level - (GameData.shipfus[item].level % 10) / 10)) + 100;
                    }
                    sliders[ii].value++;

                    ii++;
                }
            }
            else
            {
                if (gacha == null)
                {
                    gacha = Shipfu.gachaPull();
                    GameData.shipfus.Add(System.Guid.NewGuid(), gacha);
                    nushipfu.SetActive(true);
                    nushiptext.SetActive(true);
                    nushipfu.GetComponent<Image>().sprite = shipfuSprites[(int)gacha.shipID];
                }
            }
            xpGranted++;
        }
    }
}
