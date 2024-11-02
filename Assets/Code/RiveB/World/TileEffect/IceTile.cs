using UnityEngine;

namespace Assets.Code.RiveB.World.TileEffect
{
    public class IceTileEffect : TileEffect
    {
        public float slideFactor = 1.5f;

        public override void ApplyEffect(Rigidbody2D playerRb)
        {
            Vector2 velocity = playerRb.velocity;
            velocity.x *= slideFactor;
            playerRb.velocity = velocity;
        }
    }
}
