using UnityEngine;
using UnityEngine.Tilemaps;

public class TileEffectManager : MonoBehaviour
{
    public Tilemap tilemap; 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Sol"))
        {
            Vector3 hitPosition = Vector3.zero;
            foreach (ContactPoint2D hit in collision.contacts)
            {
                hitPosition.x = hit.point.x;
                hitPosition.y = hit.point.y;

                Vector3Int tilePosition = tilemap.WorldToCell(hitPosition);
                TileBase tile = tilemap.GetTile(tilePosition);

                if (tile is EffetTile effetTile)
                {
                    Debug.Log("Tuile avec effet détectée : " + effetTile.effet);
                    ApplyEffect(effetTile.effet);
                }
            }
        }
    }

    private void ApplyEffect(string effet)
    {
        switch (effet)
        {
            case "glisser":
                // TODO: glissade 
                Debug.Log("BB");
                break;
            default:
                Debug.Log("AA");
                break;
        }
    }
}
