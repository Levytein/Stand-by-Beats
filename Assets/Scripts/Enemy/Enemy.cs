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
    protected int currentHealth;

    private GameObject GameManager;

    public GameObject damageText;

    [SerializeField] private int damageDone;


    public GameObject enemyManagement;
    EnemySpawner EM;
    private float coolDown = 0f;

    public int amountFlashes = 10;
    public float intervalFlashes = .2f;

    public GameObject looterTable;
    public LootTable loots;

  

    private Material ShrimpMaterial;
    private Coroutine flashRoutine;
    [SerializeField] private static float maxCooldown = 100f;
    public bool isBoss = false;
    protected virtual void Start()
    {
        currentHealth = maxHealth;
        //Visit for later 10/3/2022
        target = Player.ActivePlayer.gameObject;
        GameManager = GameObject.FindGameObjectWithTag("GameManager");
        enemyManagement = GameObject.FindGameObjectWithTag("EnemySpawner");
        looterTable = GameObject.FindGameObjectWithTag("LootManager");


        ShrimpMaterial = GetComponentInChildren<SpriteRenderer>().material;



        if (isBoss)
        {
            return;
        }

        EM = enemyManagement.GetComponent<EnemySpawner>();
        loots = looterTable.GetComponent<LootTable>();
      
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

       
    }



    public virtual void FollowPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }
    public virtual void TakeDamage(int damage)
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
            ChanceItem();
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

    void ChanceItem()
    {
        int dropItemChance = Random.Range(0, 100);


        if(dropItemChance >= 0)
        {
            Debug.Log("Dropping Item");
            int rngjesus = Random.Range(0, loots.lootCommon.Count);
            Instantiate(loots.lootCommon[rngjesus], transform.position, Quaternion.identity);
        }

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

    protected virtual void Die()
    {
        if(EM != null)
        {
            EM.EnemyCount--;
         
            Destroy(this.gameObject);
        }
       
        
        
        //GetComponent<Collider2D>.enabled = false;
        //this.enabled = false;
        
    }
}
