using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSystem : MonoBehaviour
{
    
    public GameObject upgradeEffectPrefab;
    public int[] towerUpgradeCost;
    public int[] fortUpgradeCost;
    public int shipUpgradeCost;
    private GameObject[] friendlyTowers;
    private GameObject playerBase;
    
    private Health playerHealth;
    public GameObject healthBar;
    public GameObject healthBarUpgraded;
    public GameObject ship1Prefab;
    public GameObject ship2Prefab;

    public int towersLevel;
    public int towersMaxLevel;
    public int fortLevel;
    public int fortMaxLevel;
    public int shipLevel;
    public int shipMaxLevel;

    private GameObject player;
    private GameObject ship;

    private List<GameObject> TowerUI;
    private List<GameObject> FortUI;
    private List<GameObject> ShipUI;


    // Use this for initialization
    void Start()
    {
        playerBase = GameObject.Find("PlayerBase").transform.gameObject;

        friendlyTowers = GameObject.FindGameObjectsWithTag("PlayerTower");
        
        towersLevel = 1;
        fortLevel = 1;

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
        GameObject ship = GameObject.Find("HUD").gameObject.transform.GetChild(0).transform.GetChild(0).Find("Upgrade").Find("Slot3").gameObject;
        ShipUI.Add(ship.transform.Find("Ship-Level-1").gameObject);
        ShipUI.Add(ship.transform.Find("Ship-Level-2").gameObject);
        ShipUI[1].SetActive(false);


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
        friendlyTowers = GameObject.FindGameObjectsWithTag("PlayerTower");
        if (ResourcesOnIsland.MoneyOnIsland >= towerUpgradeCost[towersLevel - 1] && towersLevel <= towersMaxLevel)
        {
            ResourcesOnIsland.MoneyOnIsland -= towerUpgradeCost[towersLevel - 1];
            foreach (GameObject tower in friendlyTowers)
            {
                // Debug.LogWarning(tower);//.transform.parent);
                GameObject towerCurrentLvl = tower.transform.parent.transform.Find("lvl" + (towersLevel)).gameObject;
                if (towerCurrentLvl) towerCurrentLvl.SetActive(false);
                else Debug.Log("Could not find current lvl");
                
                tower.GetComponent<WatchTower>().damage *= 2;
                
                tower.transform.parent.GetComponent<structureHealth>().initHealth *= 2;
                tower.transform.parent.GetComponent<structureHealth>().health =  tower.transform.parent.GetComponent<structureHealth>().initHealth;

                TowerUI[towersLevel - 1].SetActive(false);
                TowerUI[towersLevel].SetActive(true);
                Vector3 position = new Vector3(tower.transform.position.x, tower.transform.position.y + 1f, tower.transform.position.z);
                GameObject upgradeEffect = Instantiate(upgradeEffectPrefab, position, Quaternion.identity);
                Destroy(upgradeEffect, 2f);
                StartCoroutine(TowerAfterTime(tower, 2f));
            }
            towersLevel += 1;
        }
    }

    public void UpgradeFort()
    {

        if (ResourcesOnIsland.MoneyOnIsland >= fortUpgradeCost[fortLevel - 1] && fortLevel <= fortMaxLevel)
        {
            ResourcesOnIsland.MoneyOnIsland -= fortUpgradeCost[fortLevel - 1];
            
            player.GetComponent<CollectResources>().MaxAmountOfMoneyInShip *= 2;
            playerBase.transform.Find("Spawner").GetComponent<spawner>().spawnCount += 1;

            GameObject chest = playerBase.transform.Find("Chest").gameObject;
            Vector3 position = new Vector3(chest.transform.position.x, chest.transform.position.y + 1f, chest.transform.position.z);
            GameObject fortCurrentLvl = chest.transform.Find("lvl" + (fortLevel)).gameObject;
            if (fortCurrentLvl) fortCurrentLvl.SetActive(false);
            else Debug.Log("Could not find current lvl");
            FortUI[fortLevel - 1].SetActive(false);
            FortUI[fortLevel].SetActive(true);
            GameObject upgradeEffect = Instantiate(upgradeEffectPrefab, position, Quaternion.identity);
            Destroy(upgradeEffect, 3f);
            StartCoroutine(FortAfterTime(fortCurrentLvl, 3));
            
            fortLevel += 1;
        }
    }

    IEnumerator TowerAfterTime(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);

        // Code to execute after the delay
        obj.transform.parent.transform.Find("lvl" + (towersLevel)).gameObject.SetActive(true);
    }

    IEnumerator FortAfterTime(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);

        // Code to execute after the delay
        obj.transform.parent.transform.Find("lvl" + (fortLevel)).gameObject.SetActive(true);
    }
    
    public void UpgradeShip()
    {
        Debug.Log("UPGRADE SHIP");

        if (ResourcesOnIsland.MoneyOnIsland >= shipUpgradeCost)
        {
            ResourcesOnIsland.MoneyOnIsland -= shipUpgradeCost;

            ship1Prefab.SetActive(false);
            ship2Prefab.SetActive(true);
            playerHealth.MaxHealth *= 2;
            playerHealth.health = playerHealth.MaxHealth;
            healthBar.SetActive(false);
            healthBarUpgraded.SetActive(true);
            playerHealth.upgradeHealthBar();
            ShipUI[0].SetActive(false);
            ShipUI[1].SetActive(true);
            //GameObject upgradeEffect = Instantiate(upgradeEffectPrefab, position, Quaternion.identity);
            //Destroy(upgradeEffect, 3f);
            //StartCoroutine(FortAfterTime(fortCurrentLvl, 3));
            
        }
    }
}
