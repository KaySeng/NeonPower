using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class DetectDamage_Player2 : MonoBehaviour
{
       
    public static int player2score = 5;     // The player's score.                 
    Text Player2_Life;                      // Reference to the Text component.
    public BallColor ownColor;
    public float secondsToLaunch = 3f;

    public static int currentLevel = 0;  //CURRENT SELECTED LEVEL 3 MOON , 4 TRAIN, 5 SKYLINE, 6 SNOWY, 7 RAINY

    Animator anim;
    public GameObject player2;
    //public GameObject player2Shadow;
    public GameObject WinText;
    public GameObject WinSFX;
    public GameObject DeathSFX;


    public AudioSource hurtSound;

    public GameObject moon;         //getting bool value of moon if player takes damage from moon

    public GameObject GameManager;      //gameobject to determine winner of game NOT THE WINNER OF THE ROUND
    public bool allLivesGonePlayer2;               //bool to check if the player lives after under 0

    public GameObject player2hp;
    private GameObject Ball;

    void Start()
    {
        anim = player2.GetComponent<Animator>();
        DontDestroyOnLoad(Player2_Life);
        player2hp.GetComponent<Slider>().value = 100;
    }


    void Update()
    {
        // Set the displayed text to be the word "Score" followed by the score value.
        Player2_Life.text = "X: " + player2score;
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var endGame = GameManager.GetComponent<Win>().endGame;

        //variables for detecting damage from moon
        var player2MoonDmg = moon.GetComponent<Moon>().detectDamagePlayer2;
        var Ball = GameObject.FindGameObjectWithTag("Ball");

        if (other.tag == "Ball" && other.GetComponent<Ball>().color != ownColor)
        {

            if (player2hp.GetComponent<Slider>().value <= 0)
            {
                player2score--;
                anim.Play("death");
                gameObject.GetComponent<AudioSource>().Play();
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                player2.GetComponent<Unit02>().enabled = false;
                //player2Shadow.SetActive(false);
                player2.GetComponent<CircleCollider2D>().enabled = false;
                WinText.SetActive(true);
                WinSFX.SetActive(true);
                DeathSFX.SetActive(true);
                StartCoroutine(ReloadRound());
            }
            //losing hp
            if (player2hp.GetComponent<Slider>().value > 0)
            {
                Ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 2f));
                player2hp.GetComponent<Slider>().value -= 10;
                player2.GetComponent<Unit02>().enabled = false;
                anim.SetBool("Hurt", true);
                player2.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 15000f));
                StartCoroutine(PlayerHurt());
                hurtSound.Play();
            }

            if (player2score<0)
            {
                GameManager.SetActive(true);        //set gamemanager active is player score goes under zero, as object starts endgame bool is set to true
                allLivesGonePlayer2 = true;        //all players lives are depleted, bool is taken from Win Script to get the winner and reload winner scene
            }

            other.GetComponent<Ball>().ChangeToWhite();

        }

        if (player2MoonDmg == true)
        {
            player2score--;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            if (player2score < 0)
            {
                GameManager.SetActive(true);        //set gamemanager active is player score goes under zero, as object starts endgame bool is set to true
                allLivesGonePlayer2 = true;        //all players lives are depleted, bool is taken from Win Script to get the winner and reload winner scene
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
        Player2_Life = GameObject.Find("Player2_Life").GetComponent<Text>();
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
        player2.GetComponent<Unit02>().enabled = true;
    }

}

