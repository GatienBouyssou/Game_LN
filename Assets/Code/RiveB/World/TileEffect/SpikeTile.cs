using Assets.Code.RiveB.World.TileEffect;
using System.Collections;
using UnityEngine;

public class SpikeTileEffect : TileEffect
{
    public float slowFactor = 0.5f;
    private bool alreadyApplyed = false;

    public override void ApplyEffect(Rigidbody2D playerRb)
    {
        Vector2 velocity = playerRb.velocity;
        velocity.x *= slowFactor;
        playerRb.velocity = velocity;

        Mami mami = FindObjectOfType<Mami>();
        if (!alreadyApplyed && mami.moveSpeed < 10)
        {
            mami.moveSpeed += 1;
            alreadyApplyed = true;

            StartCoroutine(ResetAlreadyApplyed());
        }
    }
    
    private IEnumerator ResetAlreadyApplyed()
    {
        yield return new WaitForSeconds(3f);
        alreadyApplyed = false;
    }
}
