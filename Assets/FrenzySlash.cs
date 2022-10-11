using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrenzySlash : MonoBehaviour
{

    public float moveSpeed = 10f;
    private Vector2 moveDirection;
    Rigidbody2D rb;
    [SerializeField] private int damageDone;
    // Start is called before the first frame update
    private GameObject GameManager;
    HealthController healthController;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameManager = GameObject.FindGameObjectWithTag("GameManager");
        healthController = (HealthController)GameManager.GetComponent(typeof(HealthController));
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {

            Player.ActivePlayer.currentHealth = Player.ActivePlayer.currentHealth - damageDone;


            healthController.UpdateHealth();
        }
       
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
