using Assets.Code.RiveB.World.TileEffect;
using UnityEngine;

public class UpperFanTile : TileEffect
{
    public override void ApplyEffect(Rigidbody2D playerRb)
    {
        Vector2 velocity = playerRb.velocity;
        velocity.y += 0.14f; // 0.37f quand build 
        playerRb.velocity = velocity;
    }
}
