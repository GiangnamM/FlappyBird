using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    public static BirdController instance;

    public float bounceForce;

    private Rigidbody2D myBody;
    private Animator anim;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip flyClip, pingClip, diedClip;

    private bool isAlive;
    private bool didFlap;

    private GameObject spawner;

    public float flag = 0;
    public int score = 0;

    // Start is called before the first frame update
    void Awake()
    {
        isAlive = true;
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        MakeInstance();
        spawner = GameObject.Find("Spawner Pipe");
    }

    void MakeInstance()
    {
        if (instance == null)
            instance = this;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        _BirdMoveMent();
    }
    void _BirdMoveMent()
    {
        if (isAlive)
        {
            if (didFlap)
            {
                didFlap = false;
                myBody.velocity = new Vector2(myBody.velocity.x, bounceForce);
                audioSource.PlayOneShot(flyClip);
            }
        }

        if (myBody.velocity.y > 0)
        {
            float angel = 0;
            angel = Mathf.Lerp(0, 90, myBody.velocity.y / 7);
            transform.rotation = Quaternion.Euler(0, 0, angel);
        }
        else if (myBody.velocity.y == 0)
        {       
            transform.rotation = Quaternion.Euler(0, 0, 0); 
        }
        else if (myBody.velocity.y < 0)
        {
            float angel = 0;
            angel = Mathf.Lerp(0, -90, -myBody.velocity.y / 7);
            transform.rotation = Quaternion.Euler(0, 0, angel);
        }
    } 
    public void FlapButton()
    {
        didFlap = true;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag =="PipeHolder")
        {
            score++;
            if (GamePlayController.instance != null)
            {
                GamePlayController.instance.SetScore(score);
            }
            audioSource.PlayOneShot(pingClip);
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Pipe" || col.gameObject.tag == "Ground")
        {
                flag = 1;
            if (isAlive)
            {
                isAlive = false;
                Destroy(spawner);
                audioSource.PlayOneShot(diedClip);
                anim.SetTrigger("Died");
            }
            if (GamePlayController.instance != null)
            {
                GamePlayController.instance.BirdDiedShowPanel(score);
            }
        }
    }
}
