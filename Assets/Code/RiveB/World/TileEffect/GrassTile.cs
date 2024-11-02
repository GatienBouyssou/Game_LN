using UnityEngine;

namespace Assets.Code.RiveB.World.TileEffect
{
    public class GrassTileEffect : TileEffect
    {
        public float slowFactor = 1.5f;

        public override void ApplyEffect(Rigidbody2D playerRb)
        {
            Vector2 velocity = playerRb.velocity;
            velocity.x /= slowFactor;
            playerRb.velocity = velocity;
        }
    }
}
