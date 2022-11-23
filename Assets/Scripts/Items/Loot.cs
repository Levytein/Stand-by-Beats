using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    // Start is called before the first frame update

    public StatBoostItems item;
    private SpriteRenderer spritePic;
    public string itemName;
    [TextArea(3, 10)]
    public string itemDescription;
    void Start()
    {
        spritePic = GetComponent<SpriteRenderer>();
        spritePic.sprite = item.itemIcon;
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Player.ActivePlayer.attackModifier += (int)item.Damage;
            Player.ActivePlayer.currentHealth += (int)item.permHealth;
            Destroy(gameObject);
        }
        
    }
}
