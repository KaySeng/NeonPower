using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Unit01 : MonoBehaviour {
    //variable to check if player is grounded or not
    protected bool grounded = false;
    public Transform groundCheck;
    float groundRadius = 0.2f;
    public LayerMask whatIsGround;

    //movement speed and jumping height
    public float speed;
    public float jumpForce;

    //AUDIO
    public AudioSource jumpSound;
    public AudioSource attackNoContact;

    //global variable of local player
    Rigidbody2D r2d;
    Animator anim;

    //animator controllers
    bool attack;
    bool jump;
    bool jumpAtttack;
    bool goingRight = true;

    //player2 gameobject
    public GameObject player2;

    //variable to activate super
    public GameObject powerBarPlayer1;
    public GameObject superCard;
    public GameObject superAnimation;

    //super moves objects
    public GameObject moon;
    public GameObject mainCamera;
    private bool pauseToggle; //toggle pause/resume stage

    //shadow
    public GameObject shadow;

    void Start()
    {
        r2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        pauseToggle = false;

        var player1Collider = this.GetComponent<CircleCollider2D>();
        var player2Collider = player2.GetComponent<CircleCollider2D>();

        //var barrierColl = barrier.GetComponent<CircleCollider2D>();

        Physics2D.IgnoreCollision(player1Collider, player2Collider);
       

    }

    //update that is called each frame
    void FixedUpdate()
    {
        //this determines if boxColliderCircle is hitting any colliders
        //groundCheck.position is where the current current collider is located
        //groundRadius is the current radius
        //whatisGround is what its currrently hitting
        //returns true or false value

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("Ground", grounded);

        anim.SetFloat("vSpeed", r2d.velocity.y);
        float move = Input.GetAxisRaw("Player1_Horizontal");
        //This is to set variable of "Speed" that was set on Animator to initate walking animation
        anim.SetFloat("Speed", Mathf.Abs(move));
        Move(move);
    }
    void Update()
    {
        if (grounded)
        {
            shadow.SetActive(true);
        }

        if (!grounded)
        {
            shadow.SetActive(false);
        }
        if (Input.GetButtonDown("Player1Cheat"))
        {
            powerBarPlayer1.GetComponent<Slider>().value = 100;
        }


        // this statement plays if player is grounded and space bar is pressed
        if (grounded && Input.GetButtonDown("Player1Jump"))
        {
            grounded = false;
            jumpSound.Play();
            anim.Play("Jump");
            anim.SetBool("Ground", false);
            r2d.AddForce(new Vector2(0, jumpForce));
            
        }
        //attacking while in air
        if (!grounded && Input.GetButtonDown("Player1Attack"))
        {           
            grounded = false;
            anim.Play("jumpAttack");
            attackNoContact.Play();
        }


        //activate super
        if (powerBarPlayer1.GetComponent<Slider>().value >= 100 && Input.GetButtonDown("Player1Ultimate"))
        {
            Time.timeScale = .08f;
            superCard.SetActive(true);
            superAnimation.SetActive(true);
            powerBarPlayer1.GetComponent<Slider>().value = 0;
            StartCoroutine(SuperAnimation());
        }

        //attack on ground
        if (grounded && Input.GetButtonDown("Player1Attack"))
        {
			anim.Play("attack");
            attackNoContact.Play();
        }

        // flip
        if(pauseToggle == false)
        {
            if (Input.GetAxis("Player1_Horizontal") < 0 && goingRight ||
             Input.GetAxis("Player1_Horizontal") > 0 && !goingRight)
            {
                goingRight = !goingRight;

                Vector3 theScale = transform.localScale;
                theScale.x *= -1;
                transform.localScale = theScale;
            }
        }      

        // Pause/unPause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseToggle)
                Time.timeScale = 1;
            else
                Time.timeScale = 0;

            pauseToggle = !pauseToggle;
        }

    }

    void Move(float move)
    {
        //adding velocity (or force into what direction the player is pressing the the horizontal axis)
        r2d.velocity = new Vector2(move * speed, r2d.velocity.y);			
    }

    //activate moon super script
    IEnumerator SuperAnimation()
    {
        yield return new WaitForSeconds(0.2f);
        Time.timeScale = 1f;
        superCard.SetActive(false);
        moon.SetActive(true);
        mainCamera.GetComponent<Shake>().amount = 3f;
        //setting true to variable moon to know which player activated super
        moon.GetComponent<Moon>().player1activate = true;
    }
}
