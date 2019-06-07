using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class MainMenu : MonoBehaviour {

    public string nextScene; //playable scene

    public GameObject loadingScreenBG;
    public Slider progBar;
    public Image startUI;
    public Image quitUI;
    public Image copyrightUI;
    public AudioSource selectionSound;

    public bool isFakeLoadingBar = false;
    public float fakeIncrement = 0f;
    public float fakeTiming = 0f;

    public void StartGame()
    {
        selectionSound.gameObject.SetActive(true);
        loadingScreenBG.SetActive(true);
        progBar.gameObject.SetActive(true);

        startUI.gameObject.SetActive(false);
        quitUI.gameObject.SetActive(false);
        copyrightUI.gameObject.SetActive(false);

        if (isFakeLoadingBar == true)
        {
            StartCoroutine(LoadLevelWithFakeProgress());
        }
    }


    public void QuitGame()
    {
        Application.Quit();
    }
    IEnumerator LoadLevelWithFakeProgress()
    {
        yield return new WaitForSeconds(1);

        while (progBar.value != 1f)
        {
            {
                //print("Progress: " + progBar.value);
                progBar.value += fakeIncrement;
                yield return new WaitForSeconds(fakeTiming);
            }

            if (progBar.value == 1f)
            {
                //print("Finished: " + progBar.value);
                SceneManager.LoadScene(nextScene);
            }
            yield return null;
        }
    }
}
