using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSystem : MonoBehaviour
{

    public GameObject tower1;
    public GameObject tower2;
    public GameObject tower3;
    public GameObject fort1;
    public GameObject fort2;
    public GameObject fort3;
    public GameObject ship1;
    GameObject ship2;
    GameObject ship3;
    public GameObject ship2Prefab;
    public GameObject ship3Prefab;
    public int towerLevel;
    public int fortLevel;
    public int shipLevel;

    private List<GameObject> TowerUI;
    private List<GameObject> FortUI;


    // Use this for initialization
    void Start()
    {
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


        TowerUI[0].transform.parent.transform.parent.gameObject.SetActive(false);        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpgradeTower()
    {
        Debug.Log("UPGRADE TOWER");
        int money = ResourcesOnIsland.MoneyOnIsland;
        if (towerLevel == 0)
        {
            towerLevel = 1;
            tower1.SetActive(true);
        }
        else if (towerLevel == 1 && money >= 50)
        {
            ResourcesOnIsland.MoneyOnIsland -= 50;
            towerLevel = 2;
            TowerUI[0].SetActive(false);
            TowerUI[1].SetActive(true);
            tower1.SetActive(false);
            tower2.SetActive(true);
        }
        else if (towerLevel == 2 && money >= 100)
        {
            ResourcesOnIsland.MoneyOnIsland -= 100;
            TowerUI[1].SetActive(false);
            TowerUI[2].SetActive(true);
            towerLevel = 3;
            tower3.SetActive(true);
        }
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
            //TAKE CURRENT POSITION AND ROTATION OF THE SHIP
            Vector3 position = ship1.transform.position;
            Quaternion rotation = ship1.transform.rotation;

            Destroy(ship1);

            //INTANTIATE THE NEW SHIP WITH THE COLLECTED POSITION AND ROTATION
            ship2 = (GameObject)Instantiate(
            ship2Prefab,
            position,
            rotation);
            shipLevel = 2;
        }
        else if (shipLevel == 2)
        {
            //TAKE CURRENT POSITION AND ROTATION OF THE SHIP
            Vector3 position = ship2.transform.position;
            Quaternion rotation = ship2.transform.rotation;

            Destroy(ship2);

            //INTANTIATE THE NEW SHIP WITH THE COLLECTED POSITION AND ROTATION
            var ship3 = (GameObject)Instantiate(
            ship3Prefab,
            position,
            rotation);
            shipLevel = 3;
        }
    }
}
