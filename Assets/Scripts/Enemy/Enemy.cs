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


    public GameObject enemyManagement;
    EnemySpawner EM;
    private float coolDown = 0f;

    public int amountFlashes = 10;
    public float intervalFlashes = .2f;

    private Material ShrimpMaterial;
    private Coroutine flashRoutine;
    [SerializeField] private static float maxCooldown = 100f;
    protected virtual void Start()
    {
        currentHealth = maxHealth;
        //Visit for later 10/3/2022
        target = Player.ActivePlayer.gameObject;
        GameManager = GameObject.FindGameObjectWithTag("GameManager");
        enemyManagement = GameObject.FindGameObjectWithTag("EnemySpawner");

        
       

        
      
       
        EM = enemyManagement.GetComponent<EnemySpawner>();

      
    }

    public virtual void Update()
    {
       
        if(countDown > 0 )
        {
            countDown = countDown - Time.deltaTime;
        }
        else
        {
            if(target != null)
            {
                FollowPlayer();
            }
         
        }
        coolDown -= Time.deltaTime;

        if (currentHealth <= 0)
        {

            Die();

        }
    }



    public virtual void FollowPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        Debug.Log("Took damage");
        if (flashRoutine != null)
        {
            return;
        }

        flashRoutine = StartCoroutine("HitFlash");
        if (currentHealth <= 0)
        {
            
            Die();
            
        }
        if(damageText != null)
        {
            TrainingDummy indicator = Instantiate(damageText, transform.position, Quaternion.identity).GetComponent<TrainingDummy>();

        }
    }

   
    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        if(coolDown <= 0 )
        {
            if (collision.CompareTag("Player"))
            {
                Damage();
                coolDown = maxCooldown;
            }
        }
    }

    void Damage()
    {
    
            Player.ActivePlayer.UpdateHealth(-damageDone);

    }
    private IEnumerator HitFlash()
    {
        for (int i = 0; i < amountFlashes; i++)
        {
            yield return new WaitForSeconds(intervalFlashes / 2);
            ShrimpMaterial.SetFloat("_HitBlend", 1f);
            yield return new WaitForSeconds(intervalFlashes / 2);
            ShrimpMaterial.SetFloat("_HitBlend", 0f);

        }
        flashRoutine = null;
    }

    void Die()
    {
        if(EM != null)
        {
            EM.EnemyCount--;
            Debug.Log("enemy died");
            Destroy(this.gameObject);
        }
       
        
        
        //GetComponent<Collider2D>.enabled = false;
        //this.enabled = false;
        
    }
}
