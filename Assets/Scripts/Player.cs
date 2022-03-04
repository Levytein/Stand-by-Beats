using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private BoxCollider2D boxCollider;

    private Vector3 moveDelta;
    Rigidbody2D rigidBody;

    public float speed = 10f;

    public float rollSpeed = 5f;
    Vector2 movementInput = Vector2.zero;

    Vector2 lastDirection = Vector2.down;
    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {

        Vector2 movement = movementInput * speed;

        //Reset MoveDelta
     

        if(moveDelta.x > 0)
        {
            transform.localScale = Vector3.one;

        }
        else if(moveDelta.x < 0 )
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        //Make this thing move

        rigidBody.AddForce(movement);
    }

    public void OnMove(InputAction.CallbackContext value)
    {

        movementInput = value.ReadValue<Vector2>();

        if(movementInput != Vector2.zero)
        {
            lastDirection = movementInput;
        }
    }
    
    public void OnRoll()
    {
        rigidBody.AddForce(lastDirection * rollSpeed, ForceMode2D.Impulse);

        //Debug.Log("idk");
    }
    
}
