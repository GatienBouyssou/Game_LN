using Assets.Code.RiveB.World.TileEffect;
using UnityEngine;

public class UpperFanTile : TileEffect
{
    public override void ApplyEffect(Rigidbody2D playerRb)
    {
        Vector2 velocity = playerRb.velocity;
        velocity.y += 0.12f; // 0.35f quand build 
        playerRb.velocity = velocity;
    }
}
