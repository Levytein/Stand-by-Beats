using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject target;
    public float speed;
    public int maxHealth = 100;
    public float countDown = 1;
    int currentHealth;
    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if(countDown > 0 )
        {
            countDown = countDown - Time.deltaTime;
        }
        else
        {
            FollowPlayer();
        }
       
    }



    public void FollowPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
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
