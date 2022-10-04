using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    private static Player activePlayer;
    public static Player ActivePlayer
    {
        get
        {
            return activePlayer;
        }


    }
    private BoxCollider2D boxCollider;

    private Vector3 moveDelta;
    private Rigidbody2D rigidBody;



    //Canvas
    public string Great = "Great";
    public string Miss = "Miss";
    public Color goodHit;
    public Color badHit;
    public CanvasGroup judgeGroup;

    public TextMeshProUGUI attackDisplay;
    public TextMeshProUGUI ComboCount;

    private int comboCounter;


    public float judgeFadetime = .3f;
    public Text judgeText;


    public float BeatTimerMargin = .1f;
    public float rollSpeed = 5f;

    //Movement
    Vector2 movementInput = Vector2.zero;

    public float speed = 10f;
    Vector2 lastDirection = Vector2.down;
    public LayerMask enemyLayers;
    //Animator
    public Animator controller;
    SpriteRenderer p_spriteRenderer;

    //Cursor
    public Vector2 cursorPos;
    public Camera mainCam;


    //Hands
    public Transform leftHand;
    public Transform rightHand;
    public AnimationCurve punchCurve;
    public float punchDistance = 3f;
    public float punchTime = 1f;
    private Coroutine punchRoutine;
    private bool isRight;
    public float attackRange;


    //Attack Parameters
    public Transform attackPoint;
    public float attackRadius;
    public int attackDamage = 20;
    public float knockBack = 50;
    private float RollTimer;

    public float knockBacktime;

    private bool facingLeft = false;

    //Events
    public delegate void PlayerMovement(Vector2 mov);
    public event PlayerMovement OnPlayerMov;
       


    public GameObject InventoryMenu;
    private bool isInventoryOpen = false;

    public GameObject MainMenu;
    private bool isMenuOpen = false;




    public void Awake()
    {
        activePlayer = this;
    }
    public void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rigidBody = GetComponent<Rigidbody2D>();
        p_spriteRenderer = GetComponent<SpriteRenderer>();
        mainCam = Camera.main;
 
    }

    
    private void FixedUpdate()
    {
        

        
        if(RollTimer>0)
        {
            RollTimer--;
        }

        Vector2 movement = movementInput * speed;
       
        //Animation variables
        controller.SetFloat("Horizontal", movement.x);
        controller.SetFloat("Vertical", movement.y);
        controller.SetFloat("Speed", movement.sqrMagnitude);

        if(judgeGroup != null)
        {
            judgeGroup.alpha = Mathf.Lerp(judgeGroup.alpha, 0f, Time.fixedDeltaTime / judgeFadetime);

        }

        //Canvas Stuff
        if(attackDisplay!= null)
        {
            attackDisplay.text = "ATK: " + attackDamage.ToString();
        }

        if (ComboCount != null)
        {
            ComboCount.text = "x" + comboCounter.ToString();
        }

        //Make this thing move

        rigidBody.AddForce(movement,ForceMode2D.Force);

        //rigidBody.velocity = new Vector2(movement.x * speed, movement.y * speed);

       
       
    }

    public void OnMove(InputAction.CallbackContext value)
    {
        // Old way of flipping
        Keyboard keyboard = Keyboard.current;

      
       

      

       
      
        movementInput = value.ReadValue<Vector2>();

    
        if(movementInput != Vector2.zero)
        {
            lastDirection = movementInput;
        }

        //Debug.Log("Moving");
        PlayerMovement handler = OnPlayerMov;
        handler?.Invoke(movementInput);


    }
    
    public void OnRoll(InputAction.CallbackContext value)
    {
        if(value.performed && RollTimer <= 0)
        {
            rigidBody.AddForce(lastDirection * rollSpeed, ForceMode2D.Impulse);
            if (Mathf.Abs((float)(AudioSettings.dspTime - BPM.activeBPM.NextNoteTime)) <= BeatTimerMargin || Mathf.Abs((float)(AudioSettings.dspTime - BPM.activeBPM.LastTick)) <= BeatTimerMargin)
            {
                judgeText.text = Great;
                judgeText.color = goodHit;
                judgeGroup.alpha = 1;
                attackDamage = 20;
                comboCounter++;
            }
            else
            {
                judgeText.text = Miss;
                judgeText.color = badHit;
                judgeGroup.alpha = 1;
                attackDamage = 10;
                comboCounter = 0;
            }

            RollTimer += 30f;
        }
     
      
    }

    public void OnCursor(InputAction.CallbackContext value)
    {


        cursorPos = mainCam.ScreenToWorldPoint(value.ReadValue<Vector2>());



    }
    public void OnMelee(InputAction.CallbackContext value)
    {
        if(value.started)
        {
            Attack();

            if (Mathf.Abs((float)(AudioSettings.dspTime - BPM.activeBPM.NextNoteTime)) <= BeatTimerMargin || Mathf.Abs((float)(AudioSettings.dspTime - BPM.activeBPM.LastTick)) <= BeatTimerMargin)
            {
                judgeText.text = Great;
                judgeText.color = goodHit;
                judgeGroup.alpha = 1;
                attackDamage = 20;
                comboCounter++;
            }
            else
            {
                judgeText.text = Miss;
                judgeText.color = badHit;
                judgeGroup.alpha = 1;
                attackDamage = 10;
                comboCounter = 0;
            }

        }
        
    }

    public void OnOpenInventory(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            
                if (isInventoryOpen == false)
                {
                    InventoryMenu.gameObject.SetActive(true);
                    isInventoryOpen = true;
                    
                }
                else
                {
                    InventoryMenu.gameObject.SetActive(false);
                  
                    isInventoryOpen = false;
                }

          


           
        }

        
    }
    public void DialogueNext(InputAction.CallbackContext value)
    {
        if(value.started)
        {
            FindObjectOfType<DialogueManager>().DisplayNextSentence();
        }

    }
        public void OpenMenu(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            
            if(isMenuOpen == false)
            {
                MainMenu.gameObject.SetActive(true);
                isMenuOpen = true;
            }
            else
            {
                MainMenu.gameObject.SetActive(false);
                isMenuOpen = false;
            }
           

        }




    }
    void Attack()
    {
        //Play an attack animation
        //controller.SetTrigger("IsAttacking");
        //Detect enemies in range

        if (punchRoutine != null)
        {
            return;
        }
        isRight = !isRight;

        punchRoutine = StartCoroutine(Punch(isRight ? rightHand : leftHand));

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position + ((Vector3)cursorPos - attackPoint.position).normalized * attackRange,attackRadius,enemyLayers);

        
        //Start punch coroutine
       
        //Damage them
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.GetComponent<Enemy>() != null)
            {
                enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
            }
            else
            {
                enemy.GetComponent<Martini>().TakeDamage(attackDamage);
            }
           
            enemy.GetComponent<Rigidbody2D>().isKinematic = false;
            Vector2 difference = enemy.transform.position - transform.position;

            difference = difference.normalized * knockBack;

            enemy.GetComponent<Rigidbody2D>().AddForce(difference, ForceMode2D.Impulse);
            StartCoroutine(KnockbackCo(enemy.GetComponent<Rigidbody2D>()));


        }
       

        }

    private IEnumerator Punch(Transform handy)
    {
        float progress = 0f;

        Vector2 targetPos = (cursorPos - (Vector2)handy.parent.position ).normalized * punchDistance;
        while(progress < punchTime)
        {
            handy.localPosition = Vector2.LerpUnclamped(Vector2.zero, targetPos,punchCurve.Evaluate(Mathf.Clamp01(progress/punchTime)));
            progress += Time.deltaTime;
            yield return new WaitForEndOfFrame();


        }
        handy.localPosition = Vector2.zero;
        punchRoutine = null;

    }




    private IEnumerator  KnockbackCo(Rigidbody2D enemy)
    {
        if(enemy !=null)
        {
            yield return new WaitForSeconds(knockBacktime);
            enemy.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            enemy.GetComponent<Rigidbody2D>().isKinematic = true;
        }
    }

    void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
        if(Application.isPlaying)
        {
            Gizmos.DrawWireSphere(attackPoint.position + ((Vector3)cursorPos - attackPoint.position).normalized * attackRange, attackRadius);
        }
    }
}
