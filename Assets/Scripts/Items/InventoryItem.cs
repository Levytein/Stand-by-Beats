using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class InventoryItem
{
    public ItemClass itemData;
    public int stackSize;



     
    public InventoryItem(ItemClass item)
    {
        itemData = item;
        AddToStack();
    }
    // Start is called before the first frame update
   public void AddToStack()
    {
        stackSize++;
    }
    public void RemoveFromStack()
    {
        stackSize--;
    }
}
