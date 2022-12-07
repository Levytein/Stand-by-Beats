using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Martini : Enemy
{
    public Transform player;
    public bool isFlipped = false;

    
    

    public Transform enragedSpot;

    private bool hasTransitioned = false;
   

    Rigidbody2D rb;

    public Dialogue dialogue;
    public Sprite characterSprite;

    public static bool martiniDead = false;
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
    }
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        if (currentHealth <= 100)
        {
            GetComponent<Animator>().SetBool("isEnraged", true);
            
            speed = 0;
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
       
        hasTransitioned = true;
        
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue, characterSprite);
        Destroy(this.gameObject);
    }
    protected override void Die()
    {

        martiniDead = true;


        TriggerDialogue();
        //Destroy(this.gameObject);


        //GetComponent<Collider2D>.enabled = false;
        //this.enabled = false;

    }

}
