using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{
    // Start is called before the first frame update
    
    // references
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    //-----------------------------------------------------------

    //общее 
    private Vector2 targetVelocity= new Vector2();  
    //-----------------------------------------------------------
  
    // RunVars
    
   
    [Header("RunVars")]
    [SerializeField] private float speed;
    private float horizontalMove;
    //-----------------------------------------------------------

    // jumpVars
    [Header("jumpVars")]
    [SerializeField] private float jumpForce;
    private bool onGround=false;
    private float checkRadius=0.2f;
   
    [Tooltip("список слоев, тут нужно выбрать Ground ")] 
    [SerializeField] LayerMask Ground;
    [SerializeField] Transform groundChecker;
    //-----------------------------------------------------------
    
   

    
    
    // ladderVars
    [Header("ladderVars")]
    [SerializeField] private float ladderSpeed=1;
    private bool isOnLadder=false;
    //-----------------------------------------------------------

    //IceButtonVars
    [Header("IceVars")]
    [SerializeField] private Sprite spriteHeroWithIce;

    [SerializeField] private bool isReadyToTakeIce;

    private bool iceTaken=false;
    

    [Tooltip("на сколько уменьшится скорость после поднятия льда")] 
    [SerializeField]private float speedReduce;

   [SerializeField] private float IceTimer;

    [Tooltip("сколько времени нужно для того что бы взять лед")] 
    [SerializeField] private float TimeToTakeIce; 

   //----------------------------------------------------------
    
    void Start()
    {
        this.rb= GetComponent<Rigidbody2D>();
       this.sr= GetComponent<SpriteRenderer>();
       this.anim= GetComponent<Animator>();
         
    }

    // Update is called once per frame
    void Update()
    {
      
        Move();

         if(Input.GetKeyDown(KeyCode.Space))
            Jump();
    }

  

    void FixedUpdate()
    {
       
            targetVelocity.x= horizontalMove*Time.fixedDeltaTime;
            targetVelocity.y= rb.velocity.y;
            rb.velocity=targetVelocity;
          
       
        
    }
      private void Move()
    {
        horizontalMove=0;
        if(!isOnLadder)
        {
            horizontalMove= Input.GetAxisRaw("Horizontal")*speed*10;
             anim.SetFloat("horizontalMove",Mathf.Abs(horizontalMove));
        }
        if(horizontalMove<0)
            sr.flipX=true;
        if(horizontalMove>0)
            sr.flipX= false;

    }
    void Jump(int multiply = 1)
    {
        CheckIsGround();
        if(onGround|| isOnLadder)
        { 
            //анимация 
           
            rb.AddForce( Vector2.up*jumpForce* multiply);

        }
       
    }

    void JumpFromLadder(float multiply=0.5f)
    {
        isReadyToExitLadder = false;
        OnLadderExit();
        if (targetVelocity.y < 0)
            multiply = multiply *2.5f;
        rb.AddForce(Vector2.up * jumpForce * multiply);

    }

    void CheckIsGround()
    {
        onGround = Physics2D.OverlapCircle(groundChecker.position,checkRadius,Ground);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
       
        switch (other.tag)
        {
            case TagManager.SPIKE:
            Death();
            break;
            
            case TagManager.ICE:
            GameManager.instance.ShowButton();
            break;


            default:
            break;
        }
    }



    public void Death()
    {
        //анимация 
        GameManager.instance.Death();

    }


    private bool isReadyToExitLadder=true;//флаг
    private void OnTriggerStay2D(Collider2D other)
    {
     
        switch (other.tag)
        {
            case TagManager.LADDER:
                
                if (Input.GetKey(KeyCode.Space) && isReadyToExitLadder)
                {

                    JumpFromLadder();
                    break;
                }
               
                // rb.velocity=Vector2.zero;
            targetVelocity.y= Input.GetAxisRaw("Vertical");
            if(targetVelocity.y!=0)
            {

                isReadyToExitLadder = true;
                isOnLadder = true;
                rb.bodyType=RigidbodyType2D.Kinematic;
                CenterOnLadder(other.bounds.center.x);
                rb.velocity=targetVelocity*ladderSpeed;
                onGround=true;
            //анимация
            }
            break;

            case TagManager.ICE:
            if(isReadyToTakeIce)
            {
                IceTimer += Time.deltaTime;
                if (Input.anyKey)
                {
                    isReadyToTakeIce= false;
                    IceTimer=0;
                    GameManager.instance.ShowButton();
                } 
               
                if(IceTimer>=TimeToTakeIce)   //поднятие льда 
                {
                    sr.sprite=spriteHeroWithIce;
                    isReadyToTakeIce=false;
                    Destroy(other.gameObject);
                    this.speed= speed-speedReduce;
                    iceTaken=true;
                }
            }
            break;




            default:
            break;
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        switch (other.tag)
        {
            case TagManager.LADDER: 
                OnLadderExit();
                break;
            
            case TagManager.ICE:
            GameManager.instance.HideButton();
            break;




            default:
            break;
        }
          
        
    }

    bool isCorrect=true;//флаг
    private void CenterOnLadder(float x)
    {
        if(isOnLadder && isCorrect)
        {
            isCorrect=false;
            this.transform.position = new Vector2(x,transform.position.y);
        }
    }

    public void ReadyToTakeIce()
    {
        //rb.velocity=Vector2.zero;
        isReadyToTakeIce= true;
    }

    private void OnLadderExit()
    {
       // targetVelocity.y = 0; 
        isOnLadder = false;
        rb.bodyType = RigidbodyType2D.Dynamic;
        isCorrect = true;
    }


}
