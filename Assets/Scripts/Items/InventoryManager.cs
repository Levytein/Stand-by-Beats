using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    public ItemClass itemToAdd;
    public ItemClass itemToRemove;
    public List <ItemClass> items = new List<ItemClass>();
    // Start is called before the first frame update


    public void Start()
    {
        Add(itemToAdd);
        Remove(itemToRemove);
    }

    public void Add(ItemClass item)
    {
        items.Add(item);
    }

    public void Remove(ItemClass item)
    {
        items.Remove(item);
    }
}
