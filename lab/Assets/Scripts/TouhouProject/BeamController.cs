using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamController : MonoBehaviour
{
    public AudioClip beamSetupAudioClip;
    public AudioClip beamDamageAudioClip;
    public AudioSource BeamAudio;


    [SerializeField]
    private BoxCollider2D beamHitBox;

    private Vector2 moveDirection;
    private int damage = 2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(moveDirection * 1 * Time.deltaTime);
    }

    public void SetMoveDirection(Vector2 dir)
    {
        moveDirection = dir;
    }

    private void EnableBeamHitBox()
    {
        beamHitBox.enabled = true;
    }

    private void DisableBeamHitBox()
    {
        beamHitBox.enabled = false;
    }

    private void EndBeam()
    {
        Destroy(gameObject);
    }

    private void PlayBeamAudio()
    {
        BeamAudio.PlayOneShot(beamSetupAudioClip);
    }

    private void PlayContinuousBeamAudio()
    {
        BeamAudio.PlayOneShot(beamDamageAudioClip);
    }

    // TO BE CHANGED, need find a better way of calculating damage taken by player
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ReimuMovement rm = other.GetComponent<ReimuMovement>();
            if (rm != null)
            {
                rm.TakeDamage(damage);
            }

        }
    }
}
