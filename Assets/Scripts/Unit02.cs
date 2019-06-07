using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Unit02 : MonoBehaviour {
	
	public float speed = 10f;
	
	
	//JUMP MOVEMENT - Animation Variables
	bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;
	public float jumpForce = 700f;
    public string playerGameObject;
    bool goingRight = true;

    public AudioSource jumpSound;
    public AudioSource attackNoContact;

    Rigidbody2D r2d;
	Animator anim;

    //variable to activate super
    public GameObject powerBarPlayer2;
    public GameObject superCard;
    public GameObject superAnimation;

    //super moves objects
    public GameObject moon;
    public GameObject mainCamera;

    //shadow
    public GameObject shadow;


    void Start()
	{
		r2d = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();

		var player = GameObject.FindGameObjectWithTag(playerGameObject).transform;
		var pCollider = player.GetComponent<CircleCollider2D> ();
		var bCollider = GetComponent<CircleCollider2D> ();
		Physics2D.IgnoreCollision(pCollider, bCollider);

		
	}
	
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
		
		float move = Input.GetAxisRaw("Player2_Horizontal");
		
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

        if (Input.GetButtonDown("Player2Cheat"))
        {
            powerBarPlayer2.GetComponent<Slider>().value = 100;
        }


        // this statement plays if player is grounded and space bar is pressed
        if (grounded && Input.GetButtonDown("Player2Jump"))
		{
            grounded = false;
            anim.SetBool("Ground", false);
            jumpSound.Play();
            r2d.AddForce(new Vector2(0, jumpForce));
		}
        
		if (grounded && Input.GetButtonDown("Player2Attack"))
		{
            attackNoContact.Play();
            anim.Play("attack");
        }

        if (powerBarPlayer2.GetComponent<Slider>().value >= 100 && Input.GetButtonDown("Player2Ultimate"))
        {
            Time.timeScale = .08f;
            superCard.SetActive(true);
            superAnimation.SetActive(true);
            powerBarPlayer2.GetComponent<Slider>().value = 0;
            StartCoroutine(SuperAnimation());
        }


        if (!grounded && Input.GetButtonDown("Player2Attack"))
        {
            grounded = false;
            attackNoContact.Play();
            anim.Play("jumpattack");
        }

        // flip

		if (Input.GetAxis("Player2_Horizontal") > 0 && goingRight ||
		    Input.GetAxis("Player2_Horizontal") < 0 && !goingRight)
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
    IEnumerator SuperAnimation()
    {
        yield return new WaitForSeconds(0.2f);
        Time.timeScale = 1f;
        superCard.SetActive(false);
        moon.SetActive(true);
        mainCamera.GetComponent<Shake>().amount = 3f;
        //setting true to variable moon to know which player activated super
        moon.GetComponent<Moon>().player2activate = true;
    }
}
