using Assets.Code.RiveB.Challenge;
using Assets.Code.RiveB.Scenario;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Code.RiveB.World.TileEffect
{
    public class FernandoChestTile : MonoBehaviour
    {
        public Tilemap propsTilemap;
        public Sprite targetSprite;
        public Sprite healthSprite;
        public Sprite bookSprite;

        private bool alreadyTook = false;

        private void Update()
        {
            CheckForPlayerOnTargetTile();
        }

        private void CheckForPlayerOnTargetTile()
        {
            PlayerMovement player = FindAnyObjectByType<PlayerMovement>();
            if (player == null) return;

            Vector3Int playerTilePosition = propsTilemap.WorldToCell(player.transform.position);
            TileBase currentTile = propsTilemap.GetTile(playerTilePosition);

            if (currentTile is Tile tile && tile.sprite == targetSprite && player.GetComponent<PlayerMovement>().isPlayerGrounded() && !alreadyTook && FindAnyObjectByType<Fernando>().HasAlreadyTalk())
            {
                player.GetComponent<PlayerMovement>().canMove = false;
                alreadyTook = true;
                GameObject.FindGameObjectWithTag("Player").transform.position -= new Vector3(0.75f, 0, 0);
                // Récupérer le contenue..
                StartCoroutine(TriggerChest());
            }
        }

        private IEnumerator TriggerChest()
        {
            PlayerMovement player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
            player.AddLifeSavior();

            Vector3Int playerTilePosition = propsTilemap.WorldToCell(player.transform.position);
            Vector3Int aboveTilePosition = playerTilePosition + Vector3Int.up;
            Vector3Int aboveTilePositionFix = aboveTilePosition + Vector3Int.right;

            Tile tempTile = ScriptableObject.CreateInstance<Tile>();
            tempTile.sprite = healthSprite;
            propsTilemap.SetTile(aboveTilePositionFix, tempTile);

            yield return new WaitForSeconds(2.5f);

            tempTile.sprite = bookSprite;
            propsTilemap.SetTile(aboveTilePositionFix, null);

            yield return new WaitForSeconds(0.5f);

            propsTilemap.SetTile(aboveTilePositionFix, tempTile);
            FernandosScenario scenario = FindAnyObjectByType<FernandosScenario>();

            yield return new WaitForSeconds(2.5f);

            propsTilemap.SetTile(aboveTilePositionFix, null);

            yield return new WaitForSeconds(1.5f);

            StartCoroutine(scenario.EndScenario());
            player.canMove = true;

        }
    }
}