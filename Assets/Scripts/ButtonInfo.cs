using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonInfo : MonoBehaviour
{



    public int ItemID;
    public TextMeshProUGUI PirceTxt;

    public GameObject ShopManager;
    public StatBoostItems itemBoost;

    void Start()
    {
        PirceTxt.text = "Price: $" + ShopManager.GetComponent<Shopkeep>().shopItems[2, ItemID].ToString();

    }
}
