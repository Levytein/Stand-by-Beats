using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Shopkeep : MonoBehaviour
{
    public int[,] shopItems = new int[5, 5];

    public int playerCurrentMoney;

    public TextMeshProUGUI currentMoneyDisplay;
   

    private void Start()
    {

        playerCurrentMoney = Player.ActivePlayer.currency;

        currentMoneyDisplay.text = "Gold: " + playerCurrentMoney;

        //ID's
        shopItems[1, 1] = 1;
        shopItems[1, 2] = 2;
        shopItems[1, 3] = 3;
        shopItems[1, 4] = 4;

        //price
        shopItems[2, 1] = 10;
        shopItems[2, 2] = 10;
        shopItems[2, 3] = 10;
        shopItems[2, 4] = 10;


    }
    public void Buy()
    {
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;

        if(playerCurrentMoney >= shopItems[2,ButtonRef.GetComponent<ButtonInfo>().ItemID])
        {
            playerCurrentMoney -= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID];
            shopItems[3, ButtonRef.GetComponent<ButtonInfo>().ItemID]++;
            currentMoneyDisplay.text = "Gold: " + playerCurrentMoney;
            ButtonRef.GetComponent<ButtonInfo>().PirceTxt.text = "SOLD OUT";
            Player.attackDamage += (int)ButtonRef.GetComponent<ButtonInfo>().itemBoost.Damage;
            Player.maxHealth += (int)ButtonRef.GetComponent<ButtonInfo>().itemBoost.permHealth;
        }
    }




}
