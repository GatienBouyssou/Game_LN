using UnityEngine;
using UnityEngine.Tilemaps;

public class TreadmillTileEffect : MonoBehaviour, ITileEffect
{
    public Sprite[] TreadmillSprites; 

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

            foreach (Sprite TreadmillSprite in TreadmillSprites)
            {
                if (tileSprite == TreadmillSprite)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void ApplyEffect(Rigidbody2D playerRb)
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
