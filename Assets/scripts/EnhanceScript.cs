using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnhanceScript : MonoBehaviour
{
    // Start is called before the first frame update

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
    public static GameObject detailTextDamage;
    public static GameObject detailTextSpeed;
    public static GameObject detailTextArmour;
    public static GameObject detailTextReload;

    public GameObject _detailImage;
    public GameObject _detailRankImage;
    public GameObject _detailTextDamage;
    public GameObject _detailTextSpeed;
    public GameObject _detailTextArmour;
    public GameObject _detailTextReload;

    void Start()
    {
        detailImage = _detailImage;
        detailRankImage = _detailRankImage;
        detailTextDamage = _detailTextDamage;
        detailTextSpeed = _detailTextSpeed;
        detailTextArmour = _detailTextArmour;
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
        List<Guid> shipfus = GameData.shipfus.Keys.ToList();
        List<Guid> fleet = GameData.activeFleet;

        //if (shipfus.Count == 0)
        //{

        shipfuListItems = new List<GameObject>();
        for (int i = 1; i <= 15; i++)
        {
            shipfuListItems.Add(GameObject.Find(i.ToString()));

            var y = shipfuListItems[i - 1].transform.Find("TxtClass");
            var t = y.GetComponentInChildren<TextMeshProUGUI>();
            t.SetText("");
            y = shipfuListItems[i - 1].transform.Find("TxtName");
            t = y.GetComponentInChildren<TextMeshProUGUI>();
            t.SetText("");
            y = shipfuListItems[i - 1].transform.Find("Image");
            y.gameObject.GetComponent<Image>().sprite = null;
        }
        //}
        int ii = 0;
        foreach (var item in shipfus)
        {
            if (ii < 15)
            {
                var y = shipfuListItems[ii].transform.Find("TxtClass");
                var t = y.GetComponentInChildren<TextMeshProUGUI>();
                t.SetText(GameData.shipfus[item].shipType.ToString());
                y = shipfuListItems[ii].transform.Find("TxtName");
                t = y.GetComponentInChildren<TextMeshProUGUI>();
                t.SetText(GameData.shipfus[item].shipID.ToString());
                y = shipfuListItems[ii].transform.Find("Image");
                y.gameObject.GetComponent<Image>().sprite = rankSprites[(int)GameData.shipfus[item].shipWeight];
                ii++;
            }
        }

        //if (fleetList.Count == 0)
        //{
        //for (int i = 1; i <= 5; i++)
        //{
        //    fleetList.Add(GameObject.Find("fleet" + i.ToString()));
        //    var y = fleetList[i - 1].transform.Find("shipfuImage");
        //    y.gameObject.GetComponent<Image>().sprite = null;
        //    y = fleetList[i - 1].transform.Find("rankImage");
        //    y.gameObject.GetComponent<Image>().sprite = null;
        //}
        //}
        //ii = 0;
        //foreach (var item in fleet)
        //{
        //    var y = fleetList[ii].transform.Find("rankImage");
        //    y.gameObject.GetComponent<Image>().sprite = rankSprites[(int)GameData.shipfus[item].shipWeight];

        //    y = fleetList[ii].transform.Find("shipfuImage");
        //    y.gameObject.GetComponent<Image>().sprite = shipfuSprites[(int)GameData.shipfus[item].shipID];
        //    ii++;
        //}

    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void updateDetails(int Id)
    {
        if (GameData.shipfus.Count > Id)
        {
            int ii = 0;
            foreach (var item in GameData.shipfus.Keys.ToList())
            {
                if (ii == Id)
                {
                    selectedShip = item;
                    detailImage.gameObject.GetComponent<Image>().sprite = shipfuSprites[(int)GameData.shipfus[item].shipID];
                    detailRankImage.gameObject.GetComponent<Image>().sprite = rankSprites[(int)GameData.shipfus[item].shipWeight];


                    detailTextDamage.GetComponent<TextMeshProUGUI>().SetText("Damage: " + GameData.shipfus[item].damage);
                    detailTextArmour.GetComponent<TextMeshProUGUI>().SetText("Armour: " + GameData.shipfus[item].armour);
                    detailTextReload.GetComponent<TextMeshProUGUI>().SetText("Reload: " + GameData.shipfus[item].reload);
                    detailTextSpeed.GetComponent<TextMeshProUGUI>().SetText("Speed: " + GameData.shipfus[item].speed);

                }
                ii++;
            }
        }
    }

    public static void updateDetailsGuid(Guid id)
    {

        detailImage.gameObject.GetComponent<Image>().sprite = shipfuSprites[(int)GameData.shipfus[id].shipID];
        detailRankImage.gameObject.GetComponent<Image>().sprite = rankSprites[(int)GameData.shipfus[id].shipWeight];

        detailTextDamage.GetComponent<TextMeshProUGUI>().SetText("Damage: " + GameData.shipfus[id].damage);
        detailTextArmour.GetComponent<TextMeshProUGUI>().SetText("Armour: " + GameData.shipfus[id].armour);
        detailTextReload.GetComponent<TextMeshProUGUI>().SetText("Reload: " + GameData.shipfus[id].reload);
        detailTextSpeed.GetComponent<TextMeshProUGUI>().SetText("Speed: " + GameData.shipfus[id].speed);

    }

    public static void updateDetailsFleet(int Id)
    {
        if (GameData.shipfus.Count > Id)
        {
            int ii = 0;
            foreach (var item in GameData.activeFleet)
            {
                if (ii == Id)
                {
                    selectedShip = item;
                    detailImage.gameObject.GetComponent<Image>().sprite = shipfuSprites[(int)GameData.shipfus[item].shipID];
                    detailRankImage.gameObject.GetComponent<Image>().sprite = rankSprites[(int)GameData.shipfus[item].shipWeight];
                }
                ii++;
            }
        }
    }

    public static void SacrificeShips()
    {

        if (selectedShip == Guid.Empty)
        {
            return;
        }

        List<Guid> sacrifices = new List<Guid>();
        List<Guid> shipfus = GameData.shipfus.Keys.ToList();
        int ii = 0;
        foreach (var item in shipfus)
        {
            if (ii < 15)
            {
                var y = shipfuListItems[ii].transform.Find("Toggle");
                if (y != null)
                {
                    var tog = y.GetComponent<Toggle>();
                    if (tog.isOn)
                    {
                        sacrifices.Add(shipfus[ii]);
                    }

                }
                ii++;
            }
        }

        foreach (var ship in sacrifices)
        {
            GameData.shipfus[selectedShip].levelup();
            GameData.shipfus.Remove(ship);
            if (GameData.activeFleet.Contains(ship))
            {
                GameData.activeFleet.Remove(ship);
            }
        }

        UpdateFleet();
        updateDetailsGuid(selectedShip);

    }
}