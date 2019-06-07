using UnityEngine;
using System.Collections;

public class Shadow : MonoBehaviour {

    public float speed = 10f;


    //Variables to check ground
    bool grounded = false;
    public Transform groundCheck;
    float groundRadius = 0.2f;
    public LayerMask shadowWhatIsGround;


    //RigidBody and Animator of Shadow
    Rigidbody2D r2d;
    Animator anim;

    private BoxCollider2D shadowCollider;
    public GameObject realPlayer;


    public GameObject player1Shadow; 
    public GameObject player2Shadow;

    //sprite that will be changed when player is on the ground or not
    public Sprite shadowJump;
    private Sprite shadowGround;

    bool attack;

    void Start()
    {
        r2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        shadowGround = this.GetComponent<SpriteRenderer>().sprite;

        var player1ShadowCollider = player1Shadow.GetComponent<BoxCollider2D>();
        var player2ShadowCollider = player2Shadow.GetComponent<BoxCollider2D>();

        Physics2D.IgnoreCollision(player1ShadowCollider, player2ShadowCollider);

        
    }

    //update that is called each frame
    void FixedUpdate()
    {
        //this determines if boxColliderCircle is hitting any colliders
        //groundCheck.position is where the current current collider is located
        //groundRadius is the current radius
        //whatisGround is what its currrently hitting
        //returns true or false value

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, shadowWhatIsGround);
        //print(grounded);

        anim.SetBool("Ground", grounded);
        anim.SetBool("Attack", false);
        anim.SetFloat("vSpeed", r2d.velocity.y);
        float move = Input.GetAxisRaw("Player1_Horizontal");
        anim.SetFloat("Speed", Mathf.Abs(move));
        Move(move);
        

    }

    bool goingRight = true;
    void Update()
    {

        if (grounded)
        {
            anim.enabled = true;
            this.gameObject.GetComponent<SpriteRenderer>().sprite = shadowGround;
            //playerShadow.GetComponent<SpriteRenderer>().enabled = true;
        }

        if (grounded && Input.GetKeyDown(KeyCode.Y))  //ATTACK
        {
            anim.Play("attack");           
        }

        if (!grounded)
        {
            anim.enabled = false;
            this.gameObject.GetComponent<SpriteRenderer>().sprite = shadowJump;
            //playerShadow.GetComponent<SpriteRenderer>().enabled = false;
        }

        // flip
        if (Input.GetKeyDown(KeyCode.A) && goingRight ||
            Input.GetKeyDown(KeyCode.D) && !goingRight)
        {
            goingRight = !goingRight;

            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }


    }

    void Move(float move)
    {
        //adding velocity (or force into what direction the player is pressing the the horizontal axis)
        r2d.velocity = new Vector2(move * speed, r2d.velocity.y);

    }
}

