using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class IntroScroll : MonoBehaviour
{
    public float speed = 0.5f;
    public float speedUnit = 1f;
    public GameObject unit01;
    public GameObject unit02;
    public GameObject logo;
    public GameObject SFX;
    public GameObject startButton;
    public GameObject lightning;
    Rigidbody2D r2dUnit01;
    Rigidbody2D r2dUnit02;

    // Use this for initialization
    void Start()
    {
        gameObject.GetComponent<MeshRenderer>().sortingOrder = 1;
        r2dUnit01 = unit01.GetComponent<Rigidbody2D>();
        r2dUnit02 = unit02.GetComponent<Rigidbody2D>();
        turnOffScroll();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 offset = new Vector2(0, -Time.time * speed);
        GetComponent<Renderer>().material.mainTextureOffset = offset;

        if (unit01.GetComponent<Transform>().position.x > 0)
        {
            
            r2dUnit01.velocity = new Vector2(0, 0);
            r2dUnit02.velocity = new Vector2(0, 0);
            unit01.GetComponent<SpriteRenderer>().color = Color.red;//new Color(145f, 0f, 255f);
            unit02.GetComponent<SpriteRenderer>().color = Color.blue;
            logo.SetActive(true);
            SFX.SetActive(true);
            startButton.SetActive(true);
            lightning.SetActive(true);
        }
    }

    void turnOffScroll()
    {
        //gameObject.SetActive(false);
        r2dUnit01.velocity = new Vector2(100 * speedUnit, 0);
        r2dUnit02.velocity = new Vector2(-100 * speedUnit, 0);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("mainmenu");
    }
}
