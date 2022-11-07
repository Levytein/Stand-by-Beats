using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Missle : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed = 10f;
    public float turnSpeed = 40f;

    Rigidbody2D rb;
    Animator anim;

    private void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {

        float angleCalc = Vector2.SignedAngle(Vector3.right ,(Player.ActivePlayer.transform.position - transform.position).normalized);

        //transform.Rotate(0, 0, Mathf.Min(Mathf.Abs(angleCalc), turnSpeed) * Mathf.Sign(angleCalc) * Time.deltaTime);

        transform.right = Vector3.Scale (Vector3.RotateTowards(transform.right, (Player.ActivePlayer.transform.position - transform.position).normalized, turnSpeed * Mathf.Deg2Rad * Time.deltaTime, 0), Vector3.one - Vector3.forward);

        rb.velocity = transform.right * speed;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("hit player");

        if (collision.CompareTag("Player") || collision.CompareTag("Rooms"))
        {
            Player.ActivePlayer.UpdateHealth(-1);
            anim.SetTrigger("Explode");
            StartCoroutine(DelayBullet());
            

        }
    }



    public void DestroyBullet()
    {
        Destroy(this.gameObject);
        
    }
    IEnumerator DelayBullet()
    {
        yield return new WaitForSeconds(.3f);
        DestroyBullet();
    }
}
