using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionBoxController : MonoBehaviour
{

    public Animator coinAnimator;
    public AudioSource questionBoxAudio;
    public AudioClip questionBoxSpawnCoin;
    public bool beenHit = false;
    public Sprite disabledSprite;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player") && !beenHit)
        {
            // update animator state
            coinAnimator.Play("Coin-Flash");
            questionBoxAudio.PlayOneShot(questionBoxSpawnCoin);
            beenHit = true;


        }



    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && beenHit)
        {
            GetComponent<SpriteRenderer>().sprite = disabledSprite;
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
    }
}
