using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Code.RiveB.World.TileEffect
{
    public class KeyTier2Tile : TileEffect
    {
        public override void ApplyEffect(Rigidbody2D playerRb)
        {
            SceneManager.LoadScene("RiveB_checkpoint2");
        }
    }
}