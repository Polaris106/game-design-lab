
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlowerPowerup : BasePowerup
{
    // setup this object's type
    // instantiate variables
    protected override void Start()
    {
        base.Start(); // call base class Start()
        this.type = IPowerup.PowerupType.FireFlower;
        this.spawned = true;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player") && spawned)
        {
            // TODO: do something when colliding with Player
            ApplyPowerup(col.gameObject.GetComponent<MonoBehaviour>()); // apply powerup (optional
            // then destroy powerup (optional)
            DestroyPowerup();

        }
        else if (col.gameObject.layer == 10) // else if hitting Pipe, flip travel direction
        {
            if (spawned)
            {
                //goRight = !goRight;
                //rigidBody.AddForce(Vector2.right * 3 * (goRight ? 1 : -1), ForceMode2D.Impulse);

            }
        }
    }

    // interface implementation
    public override void SpawnPowerup()
    {
        spawned = true;
        //rigidBody.AddForce(Vector2.right * 3, ForceMode2D.Impulse); // move to the right
    }


    // interface implementation
    public override void ApplyPowerup(MonoBehaviour i)
    {
        // try
        MarioStateController mario;
        bool result = i.TryGetComponent<MarioStateController>(out mario);
        Debug.Log(result);
        if (result)
        {
            mario.SetPowerup((PowerupType)type);
        }
    }
}