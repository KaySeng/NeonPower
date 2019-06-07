using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour {


    public float speed = 1f;
    public float secondsToLaunch = 2f;
    Rigidbody2D r2d;

    // Update is called once per frame
    void Start () {
       
        r2d = GetComponent<Rigidbody2D>();
        MoveDestroyer();
        
    }
    void Update(){
        if (gameObject.GetComponent<Transform>().position.x >15000)
        {
            gameObject.GetComponent<Transform>().position = new Vector2(-2300, 290);
            r2d.velocity = new Vector2(-transform.position.x * speed, 0);
            gameObject.GetComponent<AudioSource>().Play();
        }
    }

     void MoveDestroyer()
    {

        r2d.velocity = new Vector2(-transform.position.x *speed, 0);
    }

}
