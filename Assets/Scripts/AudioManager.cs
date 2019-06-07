using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

    public AudioSource BGM;
    private GameObject[] music;

    // Use this for initialization
    void Start () {

        music = GameObject.FindGameObjectsWithTag("BGM");
        if (music.Length != 1)
        {
            Destroy(music[1]);
        }
       
	}
	
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

}
