using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Shipfu;
using static UnityEditor.Progress;

public class FleetSceneScript : MonoBehaviour
{
    public static Guid selectedShip;

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

    static List<GameObject> shipfuListItems = new List<GameObject>();
    static List<GameObject> fleetList = new List<GameObject>();
    static List<Sprite> rankSprites = new List<Sprite>();
    static List<Sprite> shipfuSprites = new List<Sprite>();
    int topofList = 0;

    public static GameObject detailImage;
    public static GameObject detailRankImage;
    public static Slider detailSliderDamage;
    public static GameObject detailTextDamage;
    public static Slider detailSliderSpeed;
    public static GameObject detailTextSpeed;
    public static Slider detailSliderArmour;
    public static GameObject detailTextArmour;
    public static Slider detailSliderReload;
    public static GameObject detailTextReload;

    public GameObject _detailImage;
    public GameObject _detailRankImage;
    public Slider _detailSliderDamage;
    public GameObject _detailTextDamage;
    public Slider _detailSliderSpeed;
    public GameObject _detailTextSpeed;
    public Slider _detailSliderArmour;
    public GameObject _detailTextArmour;
    public Slider _detailSliderReload;
    public GameObject _detailTextReload;

    void Start()
    {
        detailImage = _detailImage;
        detailRankImage = _detailRankImage;
        detailSliderDamage = _detailSliderDamage;
        detailTextDamage = _detailTextDamage;
        detailSliderSpeed = _detailSliderSpeed;
        detailTextSpeed = _detailTextSpeed;
        detailSliderArmour = _detailSliderArmour;
        detailTextArmour = _detailTextArmour;
        detailSliderReload = _detailSliderReload;
        detailTextReload = _detailTextReload;

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

        UpdateFleet();
    }

    public static void UpdateFleet()
    {



        List<Guid> shipfus = GameState.gameData.shipfus.Keys.ToList();
        List<Guid> fleet = GameState.gameData.activeFleet;

        for (int i = 1; i <= 15; i++)
        {
            shipfuListItems.Add(GameObject.Find(i.ToString()));
        }

        int ii = 0;
        foreach (var item in shipfus)
        {
            var y = shipfuListItems[ii].transform.Find("TxtClass");
            var t = y.GetComponentInChildren<TextMeshProUGUI>();
            t.SetText(GameState.gameData.shipfus[item].shipType.ToString());
            y = shipfuListItems[ii].transform.Find("TxtName");
            t = y.GetComponentInChildren<TextMeshProUGUI>();
            t.SetText(GameState.gameData.shipfus[item].shipID.ToString());
            y = shipfuListItems[ii].transform.Find("Image");
            y.gameObject.GetComponent<Image>().sprite = rankSprites[(int)GameState.gameData.shipfus[item].shipWeight];
            ii++;
        }

        for (int i = 1; i <= 5; i++)
        {
            fleetList.Add(GameObject.Find("fleet" + i.ToString()));
            var y = fleetList[i - 1].transform.Find("shipfuImage");
            y.gameObject.GetComponent<Image>().sprite = null;
            y = fleetList[i - 1].transform.Find("rankImage");
            y.gameObject.GetComponent<Image>().sprite = null;
        }

        ii = 0;
        foreach (var item in fleet)
        {
            var y = fleetList[ii].transform.Find("rankImage");
            y.gameObject.GetComponent<Image>().sprite = rankSprites[(int)GameState.gameData.shipfus[item].shipWeight];

            y = fleetList[ii].transform.Find("shipfuImage");
            y.gameObject.GetComponent<Image>().sprite = shipfuSprites[(int)GameState.gameData.shipfus[item].shipID];
            ii++;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void updateDetails(int Id)
    {
        if (GameState.gameData.shipfus.Count > Id)
        {
            int ii = 0;
            foreach (var item in GameState.gameData.shipfus.Keys.ToList())
            {
                if (ii == Id)
                {
                    selectedShip = item;
                    detailImage.gameObject.GetComponent<Image>().sprite = shipfuSprites[(int)GameState.gameData.shipfus[item].shipID];
                    detailRankImage.gameObject.GetComponent<Image>().sprite = rankSprites[(int)GameState.gameData.shipfus[item].shipWeight];
                }
                ii++;
            }
        }
    }

    public static void updateDetailsFleet(int Id)
    {
        if (GameState.gameData.shipfus.Count > Id)
        {
            int ii = 0;
            foreach (var item in GameState.gameData.activeFleet)
            {
                if (ii == Id)
                {
                    selectedShip = item;
                    detailImage.gameObject.GetComponent<Image>().sprite = shipfuSprites[(int)GameState.gameData.shipfus[item].shipID];
                    detailRankImage.gameObject.GetComponent<Image>().sprite = rankSprites[(int)GameState.gameData.shipfus[item].shipWeight];
                }
                ii++;
            }
        }
    }

    public void ReturnToMenuScene()
    {
        SceneManager.LoadScene("menu");
    }
}
