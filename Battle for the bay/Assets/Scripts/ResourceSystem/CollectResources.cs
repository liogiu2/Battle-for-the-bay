using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectResources : MonoBehaviour
{

    public int Money;
    public int MaxAmountOfMoneyInShip;
    public int MoneyForTreasure;
    public GameObject TreasureShipBar;
    private Image _bar;
    private Text _text;
    // Use this for initialization
    void Start()
    {
        if (TreasureShipBar)
        {
            _bar = TreasureShipBar.transform.Find("bar").Find("Image").GetComponent<Image>();
            _text = TreasureShipBar.transform.Find("bar").Find("Image").Find("Text").GetComponent<Text>();
        }
    }

    // Update is called once per frame
    void Update()
    {
		UpdateMoneyBar();
        if (Money > MaxAmountOfMoneyInShip)
        {
            Money = MaxAmountOfMoneyInShip;
        }
    }

    public void AddMoney(int money)
    {
        Money += money;
    }

    public void GetTreasure()
    {
        Money += MoneyForTreasure;
        Debug.Log("Got Treasure");
    }

    public void inIsland()
    {

    }

    public void OutIsland()
    {

    }

    private void UpdateMoneyBar()
    {
        _bar.fillAmount = (float)Money / (float)MaxAmountOfMoneyInShip;
		_text.text = Money+" / "+MaxAmountOfMoneyInShip;
    }
}
