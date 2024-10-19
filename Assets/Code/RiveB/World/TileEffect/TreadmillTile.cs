using Assets.Code.RiveB.World.TileEffect;
using UnityEngine;

public class TreadmillTileEffect : TileEffect
{
    public override void ApplyEffect(Rigidbody2D playerRb)
    {
        Vector2 velocity = playerRb.velocity;

        velocity.x += 1.0f;
        if (velocity.x < 0)
        {
            velocity.x /= 2.0f;
        }
        else if (velocity.x > 0)
        {
            velocity.x *= 1.2f;
        }

        playerRb.velocity = velocity;
    }

}
