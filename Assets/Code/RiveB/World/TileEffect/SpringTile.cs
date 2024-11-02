using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Code.RiveB.World.TileEffect
{
    public class SpringTile : TileEffect
    {
        public Sprite springRelaxedSprite;
        public Sprite springActiveSprite;
        public float launchForce = 25f;

        private bool isActivated = false;

        public override void ApplyEffect(Rigidbody2D playerRb)
        {
            if (isActivated) return;

            PlayerMovement player = FindAnyObjectByType<PlayerMovement>();
            TileEffectManager tileEffectManager = FindAnyObjectByType<TileEffectManager>();

            Vector3Int playerTilePosition = tileEffectManager.tilemap.WorldToCell(playerRb.transform.position + new Vector3(0, -0.5f, 0));
            TileBase currentTile = tileEffectManager.tilemap.GetTile(playerTilePosition);

            StartCoroutine(ApplySpringEffect(player, playerRb, tileEffectManager, playerTilePosition));
        }

        private IEnumerator ApplySpringEffect(PlayerMovement player, Rigidbody2D playerRb, TileEffectManager tileEffectManager, Vector3Int tilePosition)
        {
            isActivated = true;
            player.canMove = false;

            yield return new WaitForSeconds(2);

            playerRb.velocity = new Vector2(playerRb.velocity.x, launchForce);
            player.canMove = true;
            yield return new WaitForSeconds(0.5f);

            Tile relaxedTile = ScriptableObject.CreateInstance<Tile>();
            relaxedTile.sprite = springRelaxedSprite;
            tileEffectManager.tilemap.SetTile(tilePosition, relaxedTile);

            yield return new WaitForSeconds(1.25f);

            Tile activeTile = ScriptableObject.CreateInstance<Tile>();
            activeTile.sprite = springActiveSprite;
            tileEffectManager.tilemap.SetTile(tilePosition, activeTile);

            isActivated = false;
        }
    }
}
