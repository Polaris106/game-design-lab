using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickCoinController : MonoBehaviour
{

    public Animator coinAnimator;
    public AudioSource questionBoxAudio;
    public AudioClip questionBoxSpawnCoin;
    public bool beenHit = false;

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
}
