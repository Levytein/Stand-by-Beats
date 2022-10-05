using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Martini : MonoBehaviour
{
    public Transform player;
    public bool isFlipped = false;

    public float MaxHealth = 120f;
    private float currentHealth;

    public Transform enragedSpot;

    private bool hasTransitioned = false;
    public float speed = 100f;

    Rigidbody2D rb;
   
    void Start()
    {
        currentHealth = MaxHealth;
        rb = GetComponent<Rigidbody2D>();
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        Debug.Log("Took damage");

        if(currentHealth <= 100)
        {
            GetComponent<Animator>().SetBool("isEnraged", true);
            if(hasTransitioned == false)
            {
                Transition();

            }
        }
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
    public void Transition()
    {

        gameObject.transform.position = enragedSpot.position;
        rb.mass = 100000;
        hasTransitioned = true;
    }
    void Die()
    {
        
        Debug.Log("enemy died");
        Destroy(this.gameObject);


        //GetComponent<Collider2D>.enabled = false;
        //this.enabled = false;

    }

}
