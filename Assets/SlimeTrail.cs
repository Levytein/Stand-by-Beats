using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeTrail : MonoBehaviour
{

    private static float coolDown = 0f;

    private static SlimeTrail daddyTrail;
    [SerializeField] private static float maxCooldown = 1f;

    void Start()
    {
        Invoke("Destroy", 10f);

        if(daddyTrail == null)
        {
            daddyTrail = this;
        }
    }
    private void Update()
    {

        if (daddyTrail == null)
        {
            daddyTrail = this;
        }
        if (daddyTrail == this)
        {
            coolDown -= Time.deltaTime;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && coolDown <= 0)
        {
            Damage();
            coolDown = maxCooldown;
        }
        
    }
    void Damage()
    {
            Debug.Log("Slime Trail Damage");
            Player.ActivePlayer.UpdateHealth(-1);

    }
    void Destroy()
    {
        Destroy(gameObject);
    }

}
