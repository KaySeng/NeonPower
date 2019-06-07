using UnityEngine;
using System.Collections;
public enum BallColor
{
    White,
    Purple,
    Red
}
public class Ball : MonoBehaviour
{
    // Movement Speed

	private float currentMaxSpeed =1200;
	public float maxSpeed= 1500;
    private float minSpeed = 500;
    public float speedModifier = 1.5f;
    public Sprite purple;
    public Sprite white;

    public BoxCollider2D player1DamageCollider;
    public BoxCollider2D player2DamageCollider;

	private BoxCollider2D player1SwordCollider;
	private BoxCollider2D player2SwordCollider;

    private BoxCollider2D player1AirSwordCollider;
    private BoxCollider2D player2AirSwordCollider;

    public GameObject player1;
    public GameObject player2;

    public AudioSource wallCollision;


    public BallColor color = BallColor.Red;
    //private Vector3 position;
    private Rigidbody2D rb;


    // Use this for initialization
    void Start()
    {

        //colliders for player1 and player 2

		var player1Collider = player1.GetComponent<CircleCollider2D>();
        var player2Collider = player2.GetComponent<CircleCollider2D>();

        //collider of ball
        var ballCollider = GetComponent<CircleCollider2D> ();

        //ball ignore collision of both players initially
		Physics2D.IgnoreCollision(player1Collider, ballCollider);
        Physics2D.IgnoreCollision(player2Collider, ballCollider);

        player1SwordCollider = GameObject.Find ("Sword1").GetComponent<BoxCollider2D> ();
		player2SwordCollider = GameObject.Find ("Sword2").GetComponent<BoxCollider2D> ();

        player1DamageCollider = GameObject.Find("DamageCollider1").GetComponent<BoxCollider2D>();
        player2DamageCollider = GameObject.Find("DamageCollider2").GetComponent<BoxCollider2D>();


        player1AirSwordCollider = GameObject.Find("AirSword").GetComponent<BoxCollider2D>();
        player2AirSwordCollider = GameObject.Find("AirSword2").GetComponent<BoxCollider2D>();

        var trailRenderer = gameObject.GetComponent<TrailRenderer>();
        trailRenderer.sortingOrder = 2;


        this.rb = this.GetComponent<Rigidbody2D>();
    }

	void FixedUpdate() {

		currentMaxSpeed = Mathf.Clamp (currentMaxSpeed + Time.deltaTime * speedModifier, 0, maxSpeed); //Max speed of moving creeping up

		//print (this.rb.velocity.magnitude);
		if (this.rb.velocity.magnitude > currentMaxSpeed) {
			this.rb.velocity = this.rb.velocity * 0.95f;

		}

	}

    public void ChangeToPurple()
    {
        //print("PURPLE");
        this.gameObject.GetComponent<SpriteRenderer>().sprite = purple;
        color = BallColor.Purple;

    }

    public void ChangeToWhite()
    {
        //print("WHITE");
        this.gameObject.GetComponent<SpriteRenderer>().sprite = white;
        color = BallColor.White;
        
    }

    void OnCollisionEnter2D(Collision2D coll)
    {

        if(coll.gameObject.tag == "Wall"|| coll.gameObject.tag == "Floor" || coll.gameObject.tag == "FloorShadow")
        {
            wallCollision.Play();
        }

		if (coll.collider == player1SwordCollider || coll.collider == player1AirSwordCollider)			
        {
            this.ChangeToPurple();

        } else if (coll.collider == player2SwordCollider || coll.collider == player2AirSwordCollider) {

            this.ChangeToWhite();
		}

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (this.rb.velocity.magnitude > minSpeed)
        {
            //print("Slowing down");
            this.rb.velocity = this.rb.velocity * 0.8f;
        }
    }
}