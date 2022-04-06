using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update

    public int maxHealth = 100;

    int currentHealth;
    void Start()
    {
        currentHealth = maxHealth;
    }

   public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        Debug.Log("Took damage");

        if(currentHealth <= 0)
        {
            Die();
        }
    }


    void Die()
    {

        Destroy(this.gameObject);

        Debug.Log("enemy died");
        //GetComponent<Collider2D>.enabled = false;
        //this.enabled = false;
        
    }
}
