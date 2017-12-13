﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSystem : MonoBehaviour
{


    public GameObject upgradeEffectPrefab;
    public int[] towerUpgradeCost;
    private GameObject[] friendlyTowers;
    private GameObject[] towersUpgrades;
    public GameObject tower1;
    public GameObject tower2;
    public GameObject tower3;
    public GameObject fort1;
    public GameObject fort2;
    public GameObject fort3;
    public GameObject ship1;
    GameObject ship2;
    GameObject ship3;
    private Health playerHealth;
    public GameObject healthBar;
    public GameObject healthBarUpgraded;
    public GameObject ship1Prefab;
    public GameObject ship2Prefab;

    public int towersLevel;
    public int towersMaxLevel;
    public int fortLevel;
    public int shipLevel;

    private GameObject player;
    private GameObject ship;

    private List<GameObject> TowerUI;
    private List<GameObject> FortUI;
    private List<GameObject> ShipUI;


    // Use this for initialization
    void Start()
    {

        friendlyTowers = GameObject.FindGameObjectsWithTag("PlayerTower");
        towersLevel = 1;

        TowerUI = new List<GameObject>();
        GameObject app = GameObject.Find("HUD").gameObject.transform.GetChild(0).transform.GetChild(0).Find("Upgrade").Find("Slot1").gameObject;
        TowerUI.Add(app.transform.Find("Tower-Level-1").gameObject);
        TowerUI.Add(app.transform.Find("Tower-Level-2").gameObject);
        TowerUI.Add(app.transform.Find("Tower-Level-3").gameObject);
        TowerUI[1].SetActive(false);
        TowerUI[2].SetActive(false);

        FortUI = new List<GameObject>();
        GameObject fort = GameObject.Find("HUD").gameObject.transform.GetChild(0).transform.GetChild(0).Find("Upgrade").Find("Slot2").gameObject;
        FortUI.Add(fort.transform.Find("Fort-Level-1").gameObject);
        FortUI.Add(fort.transform.Find("Fort-Level-2").gameObject);
        FortUI.Add(fort.transform.Find("Fort-Level-3").gameObject);
        FortUI[1].SetActive(false);
        FortUI[2].SetActive(false);

        ShipUI = new List<GameObject>();
        GameObject ship = GameObject.Find("HUD").gameObject.transform.GetChild(0).transform.GetChild(0).Find("Upgrade").Find("Slot5").gameObject;
        ShipUI.Add(ship.transform.Find("Ship-Level-1").gameObject);
        ShipUI.Add(ship.transform.Find("Ship-Level-2").gameObject);
        ShipUI.Add(ship.transform.Find("Ship-Level-3").gameObject);
        ShipUI[1].SetActive(false);
        ShipUI[2].SetActive(false);


        TowerUI[0].transform.parent.transform.parent.gameObject.SetActive(false);
        player = GameObject.Find("Player").gameObject;
        playerHealth = player.GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpgradeTower()
    {

        if (ResourcesOnIsland.MoneyOnIsland >= towerUpgradeCost[towersLevel] && towersLevel <= towersMaxLevel)
        {
            ResourcesOnIsland.MoneyOnIsland -= towerUpgradeCost[towersLevel];
            foreach (GameObject tower in friendlyTowers)
            {
                Debug.LogWarning(tower.transform.parent);
                GameObject towerCurrentLvl = tower.transform.parent.transform.Find("lvl" + (towersLevel)).gameObject;
                if(towerCurrentLvl) towerCurrentLvl.SetActive(false);
                else Debug.Log("Could not find current lvl");
                TowerUI[towersLevel - 1].SetActive(false);
                TowerUI[towersLevel].SetActive(true);
                Vector3 position = new Vector3(tower.transform.position.x, tower.transform.position.y + 1f, tower.transform.position.z);
                GameObject upgradeEffect = Instantiate(upgradeEffectPrefab, position, Quaternion.identity);
                Destroy(upgradeEffect, 3f);
                StartCoroutine(ExecuteAfterTime(tower, 3));
            }
            towersLevel += 1;
        }
    }

    IEnumerator ExecuteAfterTime(GameObject tower, float time)
    {
        yield return new WaitForSeconds(time);
    
        // Code to execute after the delay
        tower.transform.parent.transform.Find("lvl" + (towersLevel)).gameObject.SetActive(true);
    }

    public void UpgradeFort()
    {
        Debug.Log("UPGRADE FORT");
        int money = ResourcesOnIsland.MoneyOnIsland;

        if (fortLevel == 0)
        {
            fortLevel = 1;
            fort1.SetActive(true);
        }
        else if (fortLevel == 1 && money >= 50)
        {
            fortLevel = 2;
            ResourcesOnIsland.MoneyOnIsland -= 50;
            FortUI[0].SetActive(false);
            FortUI[1].SetActive(true);
            fort1.SetActive(false);
            fort2.SetActive(true);
        }
        else if (fortLevel == 2 && money >= 100)
        {
            fortLevel = 3;
            ResourcesOnIsland.MoneyOnIsland -= 100;
            FortUI[1].SetActive(false);
            FortUI[2].SetActive(true);
            fort2.SetActive(false);
            fort3.SetActive(true);
        }
    }


    public void UpgradeShip()
    {

        Debug.Log("UPGRADE SHIP");

        if (shipLevel == 1)
        {
            ship1Prefab.SetActive(false);
            ship2Prefab.SetActive(true);
            playerHealth.MaxHealth *= 2;
            playerHealth.health = playerHealth.MaxHealth;
            healthBar.SetActive(false);
            healthBarUpgraded.SetActive(true);
            playerHealth.upgradeHealthBar();
            //healthBar.GetComponent<RectTransform>().sizeDelta = new Vector2 (((RectTransform)healthBar.transform).rect.width *2, ((RectTransform)healthBar.transform).rect.height);
            // healthBar.transform.localScale += new Vector3(1F, 0, 0);
            //healthBar.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, ((RectTransform)healthBar.transform).rect.width *2);
        }
    }
}
