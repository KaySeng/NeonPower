using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class powerBars_player2 : MonoBehaviour {

    private int currentPower;
    //public Slider PowerSlider;

    Animator anim;
    private CircleCollider2D ballCollider;
    private Slider PowerSlider2;
    // Use this for initialization

    void Awake()
    {
        //currentPower = starting;
    }
    void Start()
    {
        ballCollider = GameObject.Find("Ball").GetComponent<CircleCollider2D>();
        PowerSlider2 = GameObject.Find("PowerSlider2").GetComponent<Slider>();
    }

    void OnCollisionEnter2D(Collision2D coll)
    {

        if (coll.collider == ballCollider)
        {
            //var value = PowerSlider2.GetComponent<Slider>().value;
            if (PowerSlider2.value <= 100)
            {
                PowerSlider2.value += 5;
            }


        }

    }
}   