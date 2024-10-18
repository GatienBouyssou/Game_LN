using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    public TileEffectManager tileEffectManager;

    void Start()
    {
        TreadmillTileEffect TreadmillEffect = FindObjectOfType<TreadmillTileEffect>();
        UpperFanTile upperFanEffect = FindObjectOfType<UpperFanTile>();

        tileEffectManager.RegisterTileEffect(TreadmillEffect);
        tileEffectManager.RegisterTileEffect(upperFanEffect);
    }
}
