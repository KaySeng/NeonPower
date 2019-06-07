using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour {

    //need player 1 and player 2 scores variables from detect damage gameobject EDIT---- score is static, need to get a boolean instead
    public GameObject player1DetectDamage;
    public GameObject player2DetectDamage;
    //need bool to turn off certain scripts eg. movement, reloading of scene of detectdamage script
    public bool endGame = false;
    public static bool player1Winner = false;
    public static bool player2Winner = false;
    public static bool drawGame = false;


    public GameObject audioManager;

    // Use this for initialization
    void Start () {


        endGame = true;
        var allLivesGonePlayer1 = player1DetectDamage.GetComponent<DetectDamage>().allLivesGonePlayer1;
        var allLivesGonePlayer2 = player2DetectDamage.GetComponent<DetectDamage_Player2>().allLivesGonePlayer2;

        if (allLivesGonePlayer1 == true && allLivesGonePlayer2 == true)
        {
            print("DRAW");
            drawGame = true;
            StartCoroutine(WinScreen());
        }

        if (allLivesGonePlayer1 == true && allLivesGonePlayer2 == false)
        {
            print("Player 2 Wins Game");
            player1Winner = false;
            player2Winner = true;
            StartCoroutine(WinScreen());
        }
        if (allLivesGonePlayer2 == true && allLivesGonePlayer1 == false)
        {
            print("Player 1 Wins Game");
            player1Winner = true;
            player2Winner = false;
            StartCoroutine(WinScreen());
        }
        
        

        
    }

    IEnumerator WinScreen()
    {
        //print("Doing");
        yield return new WaitForSeconds(3f);
		//Destroy (audioManager);

		var am = GameObject.Find ("AudioManager");
		am.GetComponent<AudioSource> ().enabled = false;

        //audioManager.GetComponent<AudioSource>().enabled = false;
        SceneManager.LoadScene("win-scene");
    }


}
