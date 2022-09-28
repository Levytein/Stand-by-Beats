using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Martini : MonoBehaviour
{
    public Transform player;
    public bool isFlipped = false;

    public float MaxHealth = 300f;
    private float currentHealth;


   
    void Start()
    {
        currentHealth = MaxHealth;
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        Debug.Log("Took damage");

        if (currentHealth <= 0)
        {

            Die();

        }
       
    }
    public void LookAtPlayer()
    {

        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;


        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;

        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;

        }

    }
    void Die()
    {
        
        Debug.Log("enemy died");
        Destroy(this.gameObject);


        //GetComponent<Collider2D>.enabled = false;
        //this.enabled = false;

    }

}
