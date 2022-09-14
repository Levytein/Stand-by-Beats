using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject target;
    public float speed;
    public int maxHealth = 100;
    public float countDown = 1;
    int currentHealth;

    private GameObject GameManager;

    public GameObject damageText;

    [SerializeField] private int damageDone;

    [SerializeField] private HealthController healthController;

    public GameObject enemyManagement;
    EnemySpawner EM;
    void Start()
    {
        currentHealth = maxHealth;
        target = GameObject.FindGameObjectWithTag("Player");
        GameManager = GameObject.FindGameObjectWithTag("GameManager");
        enemyManagement = GameObject.FindGameObjectWithTag("EnemySpawner");

        healthController = (HealthController)GameManager.GetComponent(typeof(HealthController));

        EM = gameObject.GetComponent<EnemySpawner>();
    }

    void Update()
    {
        if(EM == null)
        {
            Debug.Log("Null");
        }
        if(countDown > 0 )
        {
            countDown = countDown - Time.deltaTime;
        }
        else
        {
            FollowPlayer();
        }
       
    }



    public void FollowPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        Debug.Log("Took damage");

        if(currentHealth <= 0)
        {
            
            Die();
            
        }
        if(damageText != null)
        {
            TrainingDummy indicator = Instantiate(damageText, transform.position, Quaternion.identity).GetComponent<TrainingDummy>();

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Damage();
        }
    }

    void Damage()
    {

        healthController.playerHealth = healthController.playerHealth - damageDone;

        healthController.UpdateHealth();
        
        
    }
    void Die()
    {
        EM.EnemyCount--;
        Debug.Log("enemy died");
        Destroy(this.gameObject);
        
        
        //GetComponent<Collider2D>.enabled = false;
        //this.enabled = false;
        
    }
}
