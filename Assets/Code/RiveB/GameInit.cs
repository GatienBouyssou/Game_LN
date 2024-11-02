using Assets.Code.RiveB.World.TileEffect;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    public TileEffectManager tileEffectManager;

    void Start()
    {
        tileEffectManager.RegisterTileEffects();
    }
}
