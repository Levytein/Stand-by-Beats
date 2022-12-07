using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeZaverBoss : Enemy
{
    public Vector2 teleportRange = new Vector2(28, 18);
    public LayerMask obstructionMask;

    public float teleportCD = 3.0f;
    public int overheatCount;
    public int currentOverheatcount;
    public float pewpewDelay = 2.0f;
    public Coroutine currentCoroutine;
    public float checkRange = .5f;
    public Transform centerRoom;

    public float misslePhase = .75f;
    public float postMissleDelay = 1f;

    public GameObject normalBullets;
    public GameObject Missle;



    public float laserPhase = .35f;
    public int laserChance = 5;

    public Animator lifezaverAnim;
    public string animationName;
    public string exitAnimation;

    public float betweenShots = .2f;
    public int numberShots = 5;

    public int directionFace = 1;

    public GameObject laserBeam;
    public float laserTimer = 1.5f;


    public Dialogue dialogue;
    public Sprite characterSprite;

    public static bool boss2Dead = false;

    public enum States
    {
        ShootingState,
        MissleState,
        TeleportState,
        LaserState
    }

    public States currState = States.ShootingState;



    public bool isFlipped = false;

    Rigidbody2D rb;
    public bool isReady = false;

    public bool isGrounded = false;




    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        currentCoroutine = StartCoroutine(Teleport());
    }

    public override void Update()
    {
        if(currState == States.ShootingState)
        {
            base.Update();
        }
        LookAtPlayer();
        if(currentHealth <= 0)
        {
            TriggerDialogue();

            boss2Dead = true;

        }

    }
    // Update is called once per frame

    public void LookAtPlayer()
    {

        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;


        if (transform.position.x > Player.ActivePlayer.transform.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
            directionFace = 0;
        }
        else if (transform.position.x < Player.ActivePlayer.transform.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
            directionFace = 1;
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

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue, characterSprite);
        Destroy(this.gameObject);
    }
    IEnumerator Firing()
    {
        if (((float)currentHealth / maxHealth) <= misslePhase)
        {
            currState = States.MissleState;
            lifezaverAnim.Play("MissleShoot", -1, 0f);

            Instantiate(Missle, new Vector3(.9f, .1f, 0) + transform.position, (directionFace == 1 ? Quaternion.identity : Quaternion.Euler(0f, 0f, 180f)));

            yield return new WaitForSeconds(postMissleDelay);

        }


        currState = States.ShootingState;

        lifezaverAnim.Play("Shooting", -1, 0f);


        for (int i = 0; i < numberShots; i++)
        {


            Instantiate(normalBullets, new Vector3(.9f, .1f, 0) + transform.position, Quaternion.Euler(0,0, Vector2.SignedAngle(Vector2.right,(Player.ActivePlayer.transform.position - transform.position).normalized  )));


            yield return new WaitForSeconds(betweenShots);

        }

        if (((float)currentHealth / maxHealth) <= laserPhase && Random.Range(0 , laserChance ) == 0)
        {
            currState = States.LaserState;

            laserBeam.SetActive(true);

            float progress = 0f;

            while(progress < laserTimer)
            {
                

                laserBeam.transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Lerp(90, 270, Mathf.Clamp01(progress / laserTimer)));
                progress += Time.deltaTime;
                yield return new WaitForEndOfFrame();

            }

            laserBeam.SetActive(false);
        }
        
        currentCoroutine = StartCoroutine(Teleport());
       

    }
    IEnumerator Teleport()
    {
        currState = States.TeleportState;

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
        currentCoroutine = StartCoroutine(Firing());
    
    }

}
