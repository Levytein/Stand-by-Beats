using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2RegBullet : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed = 10f;

    Rigidbody2D rb;
    // Update is called once per frame
    private void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Player.ActivePlayer.UpdateHealth(-1);

        }
    }

    public void DestroyBullet()
    {
        Destroy(this);
    }
    IEnumerator DelayBullet()
    {
        yield return new WaitForSeconds(1.0f);
        DestroyBullet();
    }
}
