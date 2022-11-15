using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{
    public List<InventoryItem> inventory;
    private Dictionary<ItemClass, InventoryItem> itemDictionary;



    public void Add(ItemClass itemData)
    {
        if (itemDictionary.TryGetValue(itemData, out InventoryItem item))
        {
            item.AddToStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(itemData);
            inventory.Add(newItem);
            itemDictionary.Add(itemData, newItem);
        }
    }

    public void Remove(ItemClass itemData)
    {
        if (itemDictionary.TryGetValue(itemData, out InventoryItem item))
        {
            item.RemoveFromStack();
            if(item.stackSize == 0 )
            {
                inventory.Remove(item);
                itemDictionary.Remove(itemData);
            }
        }
    }
}
