using UnityEngine;
using UnityEngine.Tilemaps;

public interface ITileEffect
{
    abstract void ApplyEffect(Rigidbody2D playerRb);
    bool IsApplicable(TileBase tile, Tilemap tilemap);
}
