using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private BoxCollider2D boxCollider;

    private Vector3 moveDelta;
    Rigidbody2D rigidBody;

    

    public float BeatTimerMargin = .1f;
    public float rollSpeed = 5f;

    //Movement
    Vector2 movementInput = Vector2.zero;
   
    public float speed = 10f;
    Vector2 lastDirection = Vector2.down;
    public LayerMask enemyLayers;
    //Animator
    public Animator controller;
    SpriteRenderer p_spriteRenderer;

    //Attack Parameters
    public Transform attackPoint;
    public float attackRange ;
    public int attackDamage = 20;
    
    public void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rigidBody = GetComponent<Rigidbody2D>();
        p_spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {

        Vector2 movement = movementInput * speed;

        controller.SetFloat("Horizontal", movement.x);
        controller.SetFloat("Vertical", movement.y);
        controller.SetFloat("Speed", movement.sqrMagnitude);





        //Make this thing move

        rigidBody.AddForce(movement);
    }

    public void OnMove(InputAction.CallbackContext value)
    {
        // Old way of flipping
        Keyboard keyboard = Keyboard.current;

        if(keyboard.aKey.isPressed)
        {
            p_spriteRenderer.flipX = true;

        }
        if(keyboard.dKey.isPressed )
        {

            p_spriteRenderer.flipX = false;
        }
      
        movementInput = value.ReadValue<Vector2>();

        if(movementInput != Vector2.zero)
        {
            lastDirection = movementInput;
        }

       
    }
    
    public void OnRoll(InputAction.CallbackContext value)
    {
        if(value.performed)
        {
            rigidBody.AddForce(lastDirection * rollSpeed, ForceMode2D.Impulse);
            if (Mathf.Abs((float)(AudioSettings.dspTime - BPM.activeBPM.NextNoteTime)) <= BeatTimerMargin || Mathf.Abs((float)(AudioSettings.dspTime - BPM.activeBPM.LastTick)) <= BeatTimerMargin)
            {
                Debug.Log("less pain");
            }
            else
            {
                Debug.Log("pain");
            }

          
        }
     
        //Debug.Log("idk");
    }

    public void OnMelee(InputAction.CallbackContext value)
    {
        if(value.started)
        {
            Attack();


        }
        
    }
    

    void Attack()
    {
        //Play an attack animation
        controller.SetTrigger("IsAttacking");
        //Detect enemies in range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position,attackRange,enemyLayers);
        //Damage them
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }

    }

    void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
     
    }
}
