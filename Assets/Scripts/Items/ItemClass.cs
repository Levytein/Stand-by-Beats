using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemClass : ScriptableObject
{


    public string itemName;
    public Sprite itemIcon;


    public abstract ItemClass GetItem();
    public abstract StatBoostItems GetStatBoost();
}
