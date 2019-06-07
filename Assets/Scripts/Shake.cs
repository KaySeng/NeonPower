using UnityEngine;
using System.Collections;

public class Shake : MonoBehaviour {

    public float amount = 1.0f; //how much it shakes
    public float shakeTimer;


    void Update()
    {
        if (amount > 0)
        {
            Vector2 ShakePos = Random.insideUnitCircle * amount;

            transform.position = new Vector3(transform.position.x, transform.position.y + ShakePos.y, transform.position.z);

            shakeTimer -= Time.deltaTime;
        }
    }
}
