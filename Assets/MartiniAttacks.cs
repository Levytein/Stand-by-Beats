using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MartiniAttacks : MonoBehaviour
{

    public int attackDamage;
    public float secondattackDamage;

    public Vector3 attackOffset;
    public float attackRange = 2f;
    public GameObject Slash;

    public HealthController HealthManager;

    public LayerMask attackMask;
    private GameObject playerPos;
    public Martini MartiniControl;
    Animator MartiniAnimator;
    // Start is called before the first frame update

    void Start ()
    {
        MartiniAnimator = GetComponent<Animator>();
    }
   
    public void Attack()
    { 
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;


        playerPos = GameObject.FindGameObjectWithTag("Player");
        Vector3 playerPosition = playerPos.transform.position;
        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
      
        if(MartiniControl.isFlipped == true)
        {
            Instantiate(Slash, pos, new Quaternion(0, 0, 0, 0));
        }
        else if(MartiniControl.isFlipped != true)
        {
            
             Instantiate(Slash, pos, new Quaternion(0, 180, 0, 0));
         
        }
        
        
        
        if (colInfo != null)
        {
            Debug.Log("I hit the player");
            HealthManager.playerHealth = HealthManager.playerHealth - attackDamage;
            HealthManager.UpdateHealth();
        }
    }
    public void enragedSlashes()
    {
        if(MartiniAnimator.GetBool("EnragedSlash") == true)
        {

        }    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
