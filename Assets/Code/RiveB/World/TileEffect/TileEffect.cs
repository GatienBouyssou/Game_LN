using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Code.RiveB.World.TileEffect
{
    public abstract class TileEffect : MonoBehaviour, ITileEffect
    {
        public Sprite[] Sprites;

        private GameObject player;
        private Vector3 feetOffset = new Vector3(0, -0.5f, 0);

        void Start()
        {
            player = GameObject.FindWithTag("Player");
        }

        public bool IsApplicable(TileBase tile, Tilemap tilemap)
        {
            if (tile != null)
            {
                Vector3Int tilePosition = tilemap.WorldToCell(player.transform.position + feetOffset);
                Sprite tileSprite = tilemap.GetSprite(tilePosition);

                foreach (Sprite sprite in Sprites)
                {
                    if (tileSprite == sprite)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public abstract void ApplyEffect(Rigidbody2D playerRb);
    }
}