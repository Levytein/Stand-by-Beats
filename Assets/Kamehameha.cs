using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamehameha : MonoBehaviour
{
    // Start is called before the first frame update

    public float tickRate = 5f;
    private float timer = 0f;
    public int damage = 1;


    // Update is called once per frame
    void Update()
    {
       if(timer > 0)
        {
            timer -=  Time.deltaTime;
        }




    }

    private void OnTriggerStay2D(Collider2D collision)
    {
       if(collision.transform == Player.ActivePlayer.transform && timer <= 0)
        {
            Player.ActivePlayer.UpdateHealth(-damage);

            timer = 1f / tickRate;
        }
    }
}
