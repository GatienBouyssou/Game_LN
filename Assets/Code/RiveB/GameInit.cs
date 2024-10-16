using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    public TileEffectManager tileEffectManager;

    void Start()
    {
        IceTileEffect iceEffect = FindObjectOfType<IceTileEffect>();
        tileEffectManager.RegisterTileEffect(iceEffect);
    }
}
