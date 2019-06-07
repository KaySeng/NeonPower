using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class levelSelector : MonoBehaviour
{
    private Button nextLevelButton;
    private Button startGame;
    private Text stage;
    public Sprite moon;
    public Sprite train;
    public Sprite skyline;
    public Sprite snow;
    public Sprite rain;
    private int i = 0;
    private SpriteRenderer stageSelection;
    string[] names = new string[] {"STAGE: MOON", "STAGE: RAINY", "STAGE: SKYLINE", "STAGE: SNOWY","STAGE: TRAIN" };

    public Slider progBar;

    public float fakeIncrement = 0f;
    public float fakeTiming = 0f;

    // Use this for initialization
    void Start()
    {
        nextLevelButton = GameObject.Find("NextStage").GetComponent<Button>(); // next scene button
        startGame = GameObject.Find("Start Game").GetComponent<Button>(); // start game button
        stage = GameObject.Find("Stage").GetComponent<Text>();
        //Button b = gameObject.GetComponent<Button>();
        nextLevelButton.onClick.AddListener(delegate () { StartGame("Level1"); });
        startGame.onClick.AddListener(delegate () { StartGame1("Level1"); });
        stageSelection = GameObject.Find("StageSelection").GetComponent<SpriteRenderer>();

    }

    public void StartGame1(string Level1)
    {
        if (stage.text == "STAGE: MOON")
        {
            StartCoroutine(LoadLevelWithFakeProgress("moon-scene"));
            DetectDamage.currentLevel = 3;
            DetectDamage_Player2.currentLevel = 3;
            //SceneManager.LoadScene("moon-scene");

        }
        else if (stage.text == "STAGE: RAINY")
        {
            StartCoroutine(LoadLevelWithFakeProgress("rainy-scene"));
            DetectDamage.currentLevel = 7;
            DetectDamage_Player2.currentLevel = 7;
            //SceneManager.LoadScene("rainy-scene");
        }
        else if (stage.text == "STAGE: SKYLINE")
        {
            StartCoroutine(LoadLevelWithFakeProgress("skyline-scene"));
            DetectDamage.currentLevel = 5;
            DetectDamage_Player2.currentLevel = 5;
            //SceneManager.LoadScene("skyline-scene");
        }
        else if (stage.text == "STAGE: SNOWY")
        {
            StartCoroutine(LoadLevelWithFakeProgress("snowy-scene"));
            DetectDamage.currentLevel = 6;
            DetectDamage_Player2.currentLevel = 6;
            //SceneManager.LoadScene("snowy-scene");
        }
        else if (stage.text == "STAGE: TRAIN")
        {
            StartCoroutine(LoadLevelWithFakeProgress("train-scene"));
            DetectDamage.currentLevel = 4;
            DetectDamage_Player2.currentLevel = 4;
            //SceneManager.LoadScene("train-scene");
        }

    }

    public void StartGame(string level)
    {
        
        if (i < names.Length)
        {
            stage.text = names[i];
            i++;

        }
        else
        {
            stage.text = names[0];
            i = 1;
        }

        if (stage.text == "STAGE: MOON")
        {
            stageSelection.sprite = moon;
        }
        else if(stage.text == "STAGE: RAINY")
        {
            stageSelection.sprite = rain;
        }
        else if (stage.text == "STAGE: SKYLINE")
        {
            stageSelection.sprite = skyline;
        }
        else if (stage.text == "STAGE: SNOWY")
        {
            stageSelection.sprite = snow;
        }
        else if (stage.text == "STAGE: TRAIN")
        {
            stageSelection.sprite = train;
        }   
    }
    IEnumerator LoadLevelWithFakeProgress(string level)
    {
        yield return new WaitForSeconds(6);

        SceneManager.LoadScene(level);
        
    }

}
