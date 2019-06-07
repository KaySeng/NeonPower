using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class powerBars : MonoBehaviour {

    private int currentPower;
    //public Slider PowerSlider;

    Animator anim;
    private CircleCollider2D ballCollider;
    private Slider PowerSlider;
    //private int checkedValue;
    // Use this for initialization

    void Awake()
    {
        //currentPower = starting;
    }
	void Start () {
        ballCollider = GameObject.Find("Ball").GetComponent<CircleCollider2D>();
        PowerSlider = GameObject.Find("PowerSlider").GetComponent<Slider>();
    }
	
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider == ballCollider)
        {
            //var value = PowerSlider.GetComponent<Slider>().value;
            if (PowerSlider.value <= 100)
            {
                PowerSlider.value += 5;
            }
            
              
            //PowerSlider.value = currentPower;
        }

    }
}
	