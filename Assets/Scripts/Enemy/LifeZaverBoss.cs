using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeZaverBoss : MonoBehaviour
{
    public Vector2 teleportRange = new Vector2(28,18);
    public LayerMask obstructionMask;

    public float teleportCD = 3.0f;
    public int overheatCount;
    public int currentOverheatcount;
    public float pewpewDelay = 2.0f;
    public Coroutine teleportCoroutine;
    public float checkRange = .5f;
    public Transform centerRoom;

    public Animator lifezaverAnim;
    public string animationName;
    public string exitAnimation;


    public bool isReady = false;






    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(teleportCoroutine == null)
        {
            teleportCoroutine = StartCoroutine(Teleport());
        }
    }

    void TurnReady()
    {
        isReady = true;
    }
    void TurnUnready()
    {
        isReady = false;
    }

    IEnumerator Teleport()
    {
        isReady = false;


        Vector2 tempPos = new Vector2(Random.Range(-teleportRange.x / 2 , teleportRange.x /2 ), Random.Range(-teleportRange.y / 2, teleportRange.y / 2));
        
        bool isSucc = false ;
        for(int i = 0; i < 10; i++)
        {
            Debug.Log("Temp Pos : " + tempPos);
            if (Physics2D.OverlapCircle(tempPos, checkRange, obstructionMask) == null)
            {
                isSucc = true;
                break;

            }

            tempPos = new Vector2(Random.Range(-teleportRange.x / 2, teleportRange.x / 2), Random.Range(-teleportRange.y / 2, teleportRange.y / 2));

        }

        if(!isSucc)
        {

            tempPos = centerRoom.position;

        }
        lifezaverAnim.Play(animationName , -1 , 0f);



        while(!isReady)
        {
           yield return new WaitForEndOfFrame();
        }
        

        transform.position = tempPos;
        lifezaverAnim.Play(exitAnimation, -1, 0f);



     
        while (!isReady)
        {
            yield return new WaitForEndOfFrame();
        }
        teleportCoroutine = null;


    
        
    }

}
