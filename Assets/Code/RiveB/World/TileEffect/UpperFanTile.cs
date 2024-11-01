using Assets.Code.RiveB.World.TileEffect;
using UnityEngine;

public class UpperFanTile : TileEffect
{
    public override void ApplyEffect(Rigidbody2D playerRb)
    {
        Vector2 velocity = playerRb.velocity;
        velocity.y += 0.37f; // 0.37f quand build  et 0.14 quand editeur Unity
        playerRb.velocity = velocity;
    }
}
