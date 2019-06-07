using UnityEngine;
using System.Collections; 

public class ChangeDirection : MonoBehaviour
{
    /*
        Strength is the strength of what the ball will traverse if hit 
        Multiplier is a value that the ball will increase in speed by
    */
    public float strength = 1000;
	public float multiplier;
    public float maxStrength = 50000;

    public string playerGameObjectTag;    

    public AudioSource collisionSound;

    public GameObject player1;
    public GameObject player2;
    public GameObject eastWall;
    public GameObject westWall;


    void Start()
    {
        var p1Collider = player1.GetComponent<CircleCollider2D>();
        var p2Collider = player2.GetComponent<CircleCollider2D>();
        var eastWallCollider = eastWall.GetComponent<BoxCollider2D>();
        var westWallCollider = westWall.GetComponent<BoxCollider2D>();



        var swordCollider = this.GetComponent<BoxCollider2D>();

        Physics2D.IgnoreCollision(p1Collider, swordCollider);
        Physics2D.IgnoreCollision(p2Collider, swordCollider);
        Physics2D.IgnoreCollision(eastWallCollider, swordCollider);
        Physics2D.IgnoreCollision(westWallCollider, swordCollider);
    }


    void Update()
    {

        if (strength > maxStrength)
        {
            strength = 500;
        }

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
		var player = GameObject.FindGameObjectWithTag (playerGameObjectTag);	
		var playerPosition = player.transform;
		Vector2 playerScale = playerPosition.localScale;  // GETTING PLAYER POSITION X AND Y AXIS

        if (coll.gameObject.tag == "Ball" && playerScale.x>0)	 // THIS MOVES THE BALL RIGHT
        {
            collisionSound.Play();
            Vector2 vector;
            multiplier = Random.Range(1, multiplier);
            strength = strength * multiplier;
            vector = transform.right * (strength);
           
            var rb = coll.gameObject.GetComponent<Rigidbody2D>();
            rb.velocity = vector;         
        }
		if (coll.gameObject.tag == "Ball" && playerScale.x<0)	 // THIS MOVES THE BALL LEFT
		{
            collisionSound.Play();
            Vector2 vector;
			multiplier = Random.Range(1, multiplier);
			strength = strength * multiplier;
			vector = -transform.right * (strength);			
			var rb = coll.gameObject.GetComponent<Rigidbody2D>();
			rb.velocity = vector;         
		}
    }
    

}

