using UnityEngine;
using UnityEngine.Tilemaps;

public interface ITileEffect
{
    void ApplyEffect(Rigidbody2D playerRb);
    bool IsApplicable(TileBase tile, Tilemap tilemap);
}
