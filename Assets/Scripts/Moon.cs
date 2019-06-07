using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Moon : MonoBehaviour {

    //variable to check which player activated moon
    public bool player1activate;
    public bool player2activate;

    //ignoring environment except floor
    public GameObject ceiling;
    public GameObject eastWall;
    public GameObject westWall;
    public GameObject ball;

    //floor collider
    public GameObject floor;

    //variable to ignore player collider *** NOT DAMAGE COLLIDER ***
    public GameObject player1;
    public GameObject player2;

    //damage colliders of players
    public GameObject player1DamageCollider;
    public GameObject player2DamageCollider;

    //ui elements
    /*Text Player1_Life;
    Text Player2_Life;*/
    public GameObject WinText;
    public GameObject WinSFX;
    public float secondsToLaunch = 3f;

    //gameobjects to set active or hide
    //public GameObject player1Shadow;
    //public GameObject player2Shadow;

    //detect damage score allocator
    public bool detectDamagePlayer1 = false;
    public bool detectDamagePlayer2 = false;

    public GameObject GameManager;      //gameobject to determine winner of game NOT THE WINNER OF THE ROUND
    public GameObject mainCamera;


    // Use this for initialization
    void Start () {

        var eastWallCollider = eastWall.GetComponent<BoxCollider2D>();
        var westWallCollider = westWall.GetComponent<BoxCollider2D>();
        var ceilingCollider = ceiling.GetComponent<BoxCollider2D>();
        var ballCollider = ball.GetComponent<CircleCollider2D>();

        var player1Collider = player1.GetComponent<CircleCollider2D>();
        var player2Collider = player2.GetComponent<CircleCollider2D>();

       

        //IGNORE ALL COLLIDERS EXCEPT DAMAGER COLLIDER WHICH WILL BE HANDLE ON COLL ENTER
        Physics2D.IgnoreCollision(eastWallCollider, gameObject.GetComponent<CircleCollider2D>());
        Physics2D.IgnoreCollision(westWallCollider, gameObject.GetComponent<CircleCollider2D>());
        Physics2D.IgnoreCollision(ceilingCollider, gameObject.GetComponent<CircleCollider2D>());
        Physics2D.IgnoreCollision(ballCollider, gameObject.GetComponent<CircleCollider2D>());
        Physics2D.IgnoreCollision(player1Collider, gameObject.GetComponent<CircleCollider2D>());
        Physics2D.IgnoreCollision(player2Collider, gameObject.GetComponent<CircleCollider2D>());

        //Keep LIVES


    }
    

    void Awake()
    {
        // Set up the reference.
       /* Player1_Life = GameObject.Find("Player1_Life").GetComponent<Text>();
        Player2_Life = GameObject.Find("Player2_Life").GetComponent<Text>();*/
    }

    // Update is called once per frame
    void Update () {

    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        //var endGame = GameManager.GetComponent<Win>().endGame;       //to check if the bool of endgame is true or not. if true DO NOT reload scene

        //animator of both players (DEATH IS TO BE PLAYED)
        var player1Anim = player1.GetComponent<Animator>();
        var player2Anim = player2.GetComponent<Animator>();
        
        //damage colliders of both players
        var player1DamageColliderIGNORE = player1DamageCollider.GetComponent<BoxCollider2D>();
        var player2DamageColliderIGNORE = player2DamageCollider.GetComponent<BoxCollider2D>();

        if (player1activate == true && coll == player2DamageColliderIGNORE)
        {
            //print("Player 2 Dead");
            detectDamagePlayer2 = true;                              //bool to control score from detect damage script    
            player2.GetComponent<Unit02>().enabled = false;         //disable controller of player 2 when death happens
            player2Anim.Play("death");
            //player2Shadow.SetActive(false);
            player2.GetComponent<CircleCollider2D>().enabled = false;
            WinText.SetActive(true);
            WinSFX.SetActive(true);
        }
        if (player2activate == true && coll == player1DamageColliderIGNORE)
        {
            //print("Player 1 Dead");
            detectDamagePlayer1 = true;                             //bool to control score from detect damage script
            player1.GetComponent<Unit01>().enabled = false;         //disable controller of player 1 when death happens
            player1Anim.Play("death");
           //player1Shadow.SetActive(false);
            player1.GetComponent<CircleCollider2D>().enabled = false;   //disable collider that keeps player2 on ground
            WinText.SetActive(true);
            WinSFX.SetActive(true);        
        }
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        // if the moon hits the ground play animation of explosion
        var anim = gameObject.GetComponent<Animator>();
        var floorCollider = floor.GetComponent<BoxCollider2D>();

        if(coll.collider == floorCollider)
        {
            anim.Play("moonexplosion");
            //mainCamera.GetComponent<Shake>().amount = 15f;
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
        }

    }


}
