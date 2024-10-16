using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileEffectManager : MonoBehaviour
{
    public Tilemap tilemap;
    private List<ITileEffect> tileEffects = new List<ITileEffect>();

    private Rigidbody2D playerRb;

    void Start()
    {
        playerRb = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
    }

    public void RegisterTileEffect(ITileEffect tileEffect)
    {
        tileEffects.Add(tileEffect);
    }

    void Update()
    {
        Vector3Int playerTilePosition = tilemap.WorldToCell(playerRb.transform.position + new Vector3(0, -0.5f, 0));
        TileBase currentTile = tilemap.GetTile(playerTilePosition);

        foreach (ITileEffect effect in tileEffects)
        {
            if (effect.IsApplicable(currentTile, tilemap))
            {
                effect.ApplyEffect(playerRb);
                return;
            }
        }
    }
}
