using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrenzySlash : MonoBehaviour
{

    public float moveSpeed = 10f;
    private Vector2 moveDirection;
    Rigidbody2D rb;

    // Start is called before the first frame update

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        Invoke("Destroy", 2f);
    }
    // Update is called once per frame
    void Update()
    {
        rb.velocity = (-transform.up * moveSpeed * Time.fixedDeltaTime);
    }

    public void SetMoveDirection(Vector2 dir)
    {
        moveDirection = dir;
    }

    private void Destroy()
    {
        gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        CancelInvoke();
    }
}
