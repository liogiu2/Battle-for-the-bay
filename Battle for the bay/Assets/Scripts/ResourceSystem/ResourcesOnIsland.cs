using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesOnIsland : MonoBehaviour
{

    public static int MoneyOnIsland = 0;
    private GameObject player;
    private CollectResources PlayerResources;
    public Text MoneyOnIslandText;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").gameObject;
        PlayerResources = player.GetComponent<CollectResources>();
    }

    // Update is called once per frame
    void Update()
    {
        MoneyOnIslandText.text = "On island: " + MoneyOnIsland;
    }

    public void GetMoneyFromPlayer()
    {
        MoneyOnIsland += PlayerResources.Money;
        PlayerResources.Money = 0;
    }
}
