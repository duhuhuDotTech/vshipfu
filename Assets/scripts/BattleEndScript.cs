using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;
//using UnityEngine.UIElements;

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


    public Sprite textureShip1;
    public Sprite textureShip2;
    public Sprite textureShip3;

    static List<Sprite> rankSprites = new List<Sprite>();
    static List<Sprite> shipfuSprites = new List<Sprite>();

    static List<GameObject> fleetList = new List<GameObject>();
    List<System.Guid> fleet = GameState.gameData.activeFleet;

    List<Slider> sliders = new List<Slider>();
    List<Transform> texts = new List<Transform>();

    int xpGranted;

    int anim = 0;

    GameObject nushipfu;

    // Start is called before the first frame update
    void Start()
    {
        nushipfu = GameObject.Find("newshipfu");

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

        shipfuSprites.Add(textureShip1);
        shipfuSprites.Add(textureShip2);
        shipfuSprites.Add(textureShip3);


        List<System.Guid> shipfus = GameState.gameData.shipfus.Keys.ToList();


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
            y.gameObject.GetComponent<Image>().sprite = rankSprites[(int)GameState.gameData.shipfus[item].shipWeight];

            y = fleetList[ii].transform.Find("shipfuImage");
            y.gameObject.GetComponent<Image>().sprite = shipfuSprites[(int)GameState.gameData.shipfus[item].shipID];

            var leveltext = fleetList[ii].transform.Find("level");
            leveltext.GetComponent<TextMeshProUGUI>().SetText("Level: " + GameState.gameData.shipfus[item].level.ToString());
            texts.Add(leveltext);

            var xp = fleetList[ii].transform.Find("xp");
            xp.gameObject.SetActive(true);

            var slider = xp.gameObject.GetComponent<Slider>();
            slider.value = GameState.gameData.shipfus[item].xp % 100;
            slider.maxValue = 100;//(GameState.gameData.shipfus[item].level+1) * 100;
            slider.minValue = 0;
            sliders.Add(slider);

            ii++;

            GameState.gameData.shipfus[item].xp += GameState.battleSize * 50;
        }

    }

    // Update is called once per frame
    void Update()
    {
        anim++;
        if (anim % 5 == 0)
        {
            if (xpGranted < GameState.battleSize * 50)
            {
                int ii = 0;
                foreach (var item in fleet)
                {
                    if (sliders[ii].value == 100)
                    { 
                        sliders[ii].value = 0;
                        GameState.gameData.shipfus[item].level++;
                        texts[ii].GetComponent<TextMeshProUGUI>().SetText("Level: " + GameState.gameData.shipfus[item].level.ToString());
                    }
                    sliders[ii].value++;
                    ii++;
                }
            }
            else
            {
                // rnd select ship


                    nushipfu.GetComponent<RawImage>().texture = mapTexture;
            }
            xpGranted++;
        }

        
    }
}
