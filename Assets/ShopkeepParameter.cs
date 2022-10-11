using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class ShopkeepParameter : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject shopKeeper;

    public bool inRange;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Keyboard keyboard = Keyboard.current;
        Debug.Log("Test");
        inRange = true;

    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        inRange = false;
        shopKeeper.SetActive(false) ;

    }

    public void OnShopkeeper()
    {
        if(inRange)
        {
            shopKeeper.SetActive(true);
        }
        

    }
}

