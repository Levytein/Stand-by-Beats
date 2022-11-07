using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moth : Enemy
{

    public enum States 
    { 
        WalkingState,
        WindUpState,
        ChargingState,
        StunnedState
    }

    public States currState = States.WalkingState;

    public float delayCharge = .7f;
    public float chargeSpeed = 5f;
    public float stunDuration = 2f;
    public float chargeCooldown = 3f;
    public float currCD;

    Animator anim;
    Rigidbody2D rb;
    Coroutine chargeRoutine;
    // Update is called once per frame

    protected override void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        base.Start();
    }
    public override void FollowPlayer()
    {
        
        if(currState == States.WalkingState)
        {
            base.FollowPlayer();
        }
       

    }
    public override void Update()
    {
        if(currState == States.WalkingState)
        {
            anim.SetFloat("Blend", (Mathf.Sign(Player.ActivePlayer.transform.position.x - transform.position.x) + 1f) / 2f);

            if (currCD <= 0)
            {
                chargeRoutine = StartCoroutine(CHARGE());
            }
            currCD -= Time.deltaTime;
        }
        base.Update();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hit Wall");

        if (collision.CompareTag("Rooms") && currState == States.ChargingState)
        {
            chargeRoutine = StartCoroutine(Stunned());

        }
        if (collision.CompareTag("Player") && currState == States.ChargingState)
        {
            Player.ActivePlayer.UpdateHealth(-1);

        }

    }

    protected override void OnTriggerStay2D(Collider2D collision)
    {
        if(currState == States.WalkingState)
        {
            base.OnTriggerStay2D(collision);
        }
        else
        {
            if (collision.CompareTag("Rooms") && currState == States.ChargingState)
            {
                chargeRoutine = StartCoroutine(Stunned());

            }
            if (collision.CompareTag("Player") && currState == States.ChargingState)
            {
                Player.ActivePlayer.UpdateHealth(-1);

            }
        }
           
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Hit Wall");

        if (collision.collider.CompareTag("Rooms") && currState == States.ChargingState)
        {
            chargeRoutine = StartCoroutine(Stunned());
        }
        if (collision.collider.CompareTag("Player") && currState == States.ChargingState)
        {
            Player.ActivePlayer.UpdateHealth(-1);

        }
    }

    IEnumerator Stunned()
    {
        currState = States.StunnedState;

        rb.velocity = Vector2.zero;
        anim.Play("Crash");
        yield return new WaitForSeconds(stunDuration);
        anim.Play("MothWalking");
        currState = States.WalkingState;
        currCD = chargeCooldown;
        chargeRoutine = null;

        
    }
    IEnumerator CHARGE()
    {
        currState = States.WindUpState;
        anim.Play("WindUp");
        yield return new WaitForSeconds(delayCharge);
        anim.SetFloat("Blend", (Mathf.Sign(Player.ActivePlayer.transform.position.x - transform.position.x) + 1f) / 2f);

        currState = States.ChargingState;
        rb.velocity = (Vector2)(Player.ActivePlayer.transform.position - transform.position).normalized * chargeSpeed;
        anim.Play("Charging");

        chargeRoutine = null;




    }

  

}
