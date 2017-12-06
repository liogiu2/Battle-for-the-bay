using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSystem : MonoBehaviour {

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



    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpgradeTower()
    {
        Debug.Log("UPGRADE TOWER");

        if(towerLevel == 0)
        {
            towerLevel = 1;
            tower1.SetActive(true);
        }
        else if (towerLevel == 1)
        {
            towerLevel = 2;
            tower1.SetActive(false);
            tower2.SetActive(true);
        }
        else if (towerLevel == 2)
        {
            towerLevel = 3;
            tower3.SetActive(true);
        }
    }

    public void UpgradeFort()
    {
        Debug.Log("UPGRADE FORT");

        if(fortLevel == 0)
        {
            fortLevel = 1;
            fort1.SetActive(true);
        }
        else if (fortLevel == 1)
        {
            fortLevel = 2;
            fort1.SetActive(false);
            fort2.SetActive(true);
        }
        else if (fortLevel == 2)
        {
            fortLevel = 3;
            fort2.SetActive(false);
            fort3.SetActive(true);
        }
    }
    

    public void UpgradeShip()
    {

        Debug.Log("UPGRADE SHIP");

        if(shipLevel == 1)
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
        else if(shipLevel == 2)
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
