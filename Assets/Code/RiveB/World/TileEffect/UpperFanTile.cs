using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class UpperFanTile : MonoBehaviour, ITileEffect
{
    public Sprite[] UpperFanSprites;
    private GameObject player;
    private Vector3 feetOffset = new Vector3(0, -0.5f, 0);

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    public void ApplyEffect(Rigidbody2D playerRb)
    {
        Vector2 velocity = playerRb.velocity;
        velocity.y += 0.12f;
        playerRb.velocity = velocity;
    }

    public bool IsApplicable(TileBase tile, Tilemap tilemap)
    {
        if (tile != null)
        {
            Vector3Int tilePosition = tilemap.WorldToCell(player.transform.position + feetOffset);
            Sprite tileSprite = tilemap.GetSprite(tilePosition);

            foreach (Sprite UpperFanSprite in UpperFanSprites)
            {
                if (tileSprite == UpperFanSprite)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
