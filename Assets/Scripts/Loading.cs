using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour {

    AsyncOperation ao;

    //OLD CODE TO LOAD LEVEL, NOW JUST TO DISABLE GAMEOBJECTS DURING VERSUS SCREEN

    //variables to be enabled to show loading screen
    public GameObject loadingScreenBG;
    public Slider progBar;
    public AudioSource loadingBGM;


    //variables to be disabled when loading new scene
    //public Image unit1;
    //public Image unit2;
    public Text startGame;
    public Text stage;
    //public Text lives;
    public AudioSource characterBGM;
    public Button nextStage;

    public bool isFakeLoadingBar = false;
    public float fakeIncrement = 0f;
    public float fakeTiming = 0f;

    public string nextScene;
    


    public void loadLevelFromCharacterSelect() {
        //enabling loading screen bg and bar
        loadingScreenBG.SetActive(true);
        //progBar.gameObject.SetActive(true);
        //disabling other ui elements
        characterBGM.gameObject.SetActive(false);
        loadingBGM.gameObject.SetActive(true);

        nextStage.gameObject.SetActive(false);
        //unit1.gameObject.SetActive(false);
        //unit2.gameObject.SetActive(false);
        startGame.gameObject.SetActive(false);
        //lives.gameObject.SetActive(false);
        stage.gameObject.SetActive(false);
    }


  /*  IEnumerator LoadLevelWithFakeProgress()
    {
        yield return new WaitForSeconds(1);

        while (progBar.value != 1f)
        {
            {
                print("Progress: "+progBar.value);
                progBar.value += fakeIncrement;
                yield return new WaitForSeconds(fakeTiming);
            }

            if (progBar.value == 1f)
            {
                print("Finished: " + progBar.value);
                SceneManager.LoadScene(nextScene);
            }
            yield return null;
        }
    }*/


}
