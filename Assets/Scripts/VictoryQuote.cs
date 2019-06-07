using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class VictoryQuote : MonoBehaviour {

    public GameObject player1Text;  //quote of player 1 winning
    public GameObject player2Text;
    public GameObject drawGameText;

    public GameObject player1WinSplashImg;  //image of player 1 winning
    public GameObject player2WinSplashImg;
    public GameObject drawGameImg;

    public GameObject fireWorkImg;


    // Use this for initialization
    void Start () {

        var player1Win = Win.player1Winner;
        var player2Win = Win.player2Winner;
        var drawGame = Win.drawGame;
        
        if(player1Win)
        {
            player1WinSplashImg.SetActive(true);
            player1Text.SetActive(true);
            
        }
        else if(player2Win)
        {
            player2WinSplashImg.SetActive(true);
            player2Text.SetActive(true);
        }
        else if (drawGame)
        {
            drawGameText.SetActive(true);
            drawGameImg.SetActive(true);
            fireWorkImg.SetActive(true);
        }
        StartCoroutine(LoadMainMenu());
    }

    IEnumerator LoadMainMenu()
    {
        yield return new WaitForSeconds(9f);
        DetectDamage.player1score = 5;
        DetectDamage_Player2.player2score = 5;
        SceneManager.LoadScene("mainmenu");
    }


}
