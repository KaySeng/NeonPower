using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class DetectDamage : MonoBehaviour {

    public static int player1score = 5;        // The player's score.
    Text Player1_Life;                      // Reference to the Text component.
    public BallColor ownColor;
    public float secondsToLaunch = 3f;

    public static int currentLevel = 0;  //CURRENT SELECTED LEVEL 3 MOON , 4 TRAIN, 5 SKYLINE, 6 SNOWY, 7 RAINY

    Animator anim;
    public GameObject player1;
    //public GameObject player1Shadow;
    public GameObject WinText;
    public GameObject WinSFX;
    public GameObject DeathSFX;
    public GameObject player1hp;

    public AudioSource hurtSound;

    public GameObject moon;         //getting bool value of moon if player takes damage from moon

    public GameObject GameManager;      //gameobject to determine winner of game NOT THE WINNER OF THE ROUND
    public bool allLivesGonePlayer1;               //bool to check if the player lives after under 0

    private GameObject Ball;

    void Start()
    {
        anim = player1.GetComponent<Animator>();
        DontDestroyOnLoad(Player1_Life);
        player1hp.GetComponent<Slider>().value = 100;
        
       
    }

    void Update()
    {
        // Set the displayed text to be the word "Score" followed by the score value.
       Player1_Life.text = "X: " + player1score;      
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var endGame = GameManager.GetComponent<Win>().endGame;
        //variables for detecting damage from moon
        var player1MoonDmg = moon.GetComponent<Moon>().detectDamagePlayer1;
        //var playerhp = player1hp.GetComponent<Slider>().value;
        var Ball = GameObject.FindGameObjectWithTag("Ball");

        if (other.tag == "Ball" && other.GetComponent<Ball>().color != ownColor)
        {
            if (player1hp.GetComponent<Slider>().value <= 0)
            {
                player1score--;
                gameObject.GetComponent<AudioSource>().Play();
                gameObject.GetComponent<BoxCollider2D>().enabled = false;  //disable box collider of damage so it doesn't collide twice with ball and subtract too much lives
                player1.GetComponent<Unit01>().enabled = false;         //disable controller of player 1 when death happens
                anim.Play("death");
                //player1Shadow.SetActive(false);
                player1.GetComponent<CircleCollider2D>().enabled = false;
                WinText.SetActive(true);
                WinSFX.SetActive(true);
                DeathSFX.SetActive(true);
                StartCoroutine(ReloadRound());
            }
           
            if(player1hp.GetComponent<Slider>().value > 0)
            {
                Ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 2f));
                player1hp.GetComponent<Slider>().value -= 10;
                player1.GetComponent<Unit01>().enabled = false;
                anim.SetBool("Hurt", true);
                player1.GetComponent<Rigidbody2D>().AddForce(new Vector2(0,15000f));
                StartCoroutine(PlayerHurt());
                hurtSound.Play();
            }

            if (player1score < 0)
            {
                GameManager.SetActive(true);        //set gamemanager active is player score goes under zero, as object starts endgame bool is set to true
                allLivesGonePlayer1 = true;        //all players lives are depleted, bool is taken from Win Script to get the winner and reload winner scene
            }


            other.GetComponent<Ball>().ChangeToPurple();
        }

        if (player1MoonDmg == true)
        {
            player1score--;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;

            if (player1score < 0)
            {
                GameManager.SetActive(true);        //set gamemanager active is player score goes under zero, as object starts endgame bool is set to true
                allLivesGonePlayer1 = true;        //all players lives are depleted, bool is taken from Win Script to get the winner and reload winner scene
            }

            else if (endGame == false)
            {
                StartCoroutine(ReloadRound());
            }
        }


    }

    void Awake()
    {
        // Set up the reference.
        Player1_Life = GameObject.Find("Player1_Life").GetComponent<Text>();
    }

    IEnumerator ReloadRound()
    {
        yield return new WaitForSeconds(secondsToLaunch);
		SceneManager.LoadScene(currentLevel);
    }

    IEnumerator PlayerHurt()
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("Hurt", false);
        player1.GetComponent<Unit01>().enabled = true;
    }
}
                
