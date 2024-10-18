using UnityEngine;
using UnityEngine.Tilemaps;

public class IceTileEffect : MonoBehaviour, ITileEffect
{
    public Sprite[] iceSprites; 
    public float slideFactor = 5f;

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

            foreach (Sprite iceSprite in iceSprites)
            {
                if (tileSprite == iceSprite)
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
        velocity.x *= slideFactor;
        playerRb.velocity = velocity;
    }
}
