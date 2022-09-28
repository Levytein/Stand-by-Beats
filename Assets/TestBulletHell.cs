using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBulletHell : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D frenzySlash;
    public float cooldown ;
    private float timer;

    private float projectileSpeed;
    private float randomDirection;
    void Start()
    {
        timer = cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        

        if (timer <= 0)
        {
            var NewFrenzySlash = Instantiate(frenzySlash, transform.position, transform.rotation);
            randomDirection = Random.Range(-2f, 2f);
            NewFrenzySlash.AddRelativeForce(new Vector2(0f + randomDirection, 1f) * projectileSpeed);
            timer = cooldown;
        }
        timer--;
    }
}

