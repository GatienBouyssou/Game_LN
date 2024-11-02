using Assets.Code.RiveB.Challenge;
using Assets.Code.RiveB.Characters;
using Assets.Code.RiveB.Scenario;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Code.RiveB.World.TileEffect
{
    public class PapiChestTile : MonoBehaviour
    {

        public Tilemap propsTilemap;
        public Sprite targetSprite;
        public Sprite dessertSprite;
        public Sprite loveSprite;

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

            if (currentTile is Tile tile && tile.sprite == targetSprite && player.GetComponent<PlayerMovement>().isPlayerGrounded() && !alreadyTook && FindAnyObjectByType<Papi>().HasAlreadyTalk())
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
            yield return new WaitForEndOfFrame();

            PlayerMovement player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

            Vector3Int playerTilePosition = propsTilemap.WorldToCell(player.transform.position);
            Vector3Int aboveTilePosition = playerTilePosition + Vector3Int.up;
            Vector3Int aboveTilePositionFix = aboveTilePosition + Vector3Int.right;

            Tile tempTile = ScriptableObject.CreateInstance<Tile>();
            tempTile.sprite = dessertSprite;
            propsTilemap.SetTile(aboveTilePositionFix, tempTile);

            yield return new WaitForSeconds(2.5f);

            propsTilemap.SetTile(aboveTilePositionFix, null);

            yield return new WaitForSeconds(0.75f);

            Vector3Int hearth1 = aboveTilePositionFix + Vector3Int.up;
            Vector3Int hearth2 = hearth1 + Vector3Int.up;
            Vector3Int hearth3 = hearth1 + Vector3Int.right;
            Vector3Int hearth4 = hearth1 + Vector3Int.left;
            Vector3Int hearth5 = hearth2 + Vector3Int.right;
            Vector3Int hearth6 = hearth2 + Vector3Int.left;
            Vector3Int hearth7 = hearth5 + Vector3Int.right;
            Vector3Int hearth8 = hearth6 + Vector3Int.left;
            Vector3Int hearth9 = hearth5 + Vector3Int.up;
            Vector3Int hearth10 = hearth6 + Vector3Int.up;

            tempTile.sprite = loveSprite;
            Tile tempTile1 = ScriptableObject.CreateInstance<Tile>();
            tempTile1.sprite = loveSprite;
            // Tile tempTile2 = ScriptableObject.CreateInstance<Tile>();
            // tempTile2.sprite = whiteSprite;

            propsTilemap.SetTile(aboveTilePositionFix, tempTile);

            yield return new WaitForSeconds(0.5f);

            propsTilemap.SetTile(hearth1, tempTile1);

            yield return new WaitForSeconds(0.5f);

            propsTilemap.SetTile(hearth2, tempTile1);

            yield return new WaitForSeconds(0.5f);

            propsTilemap.SetTile(hearth3, tempTile1);

            yield return new WaitForSeconds(0.5f);

            propsTilemap.SetTile(hearth4, tempTile1);

            yield return new WaitForSeconds(0.5f);

            propsTilemap.SetTile(hearth5, tempTile1);

            yield return new WaitForSeconds(0.5f);

            propsTilemap.SetTile(hearth6, tempTile1);

            yield return new WaitForSeconds(0.5f);

            propsTilemap.SetTile(hearth7, tempTile1);

            yield return new WaitForSeconds(0.5f);

            propsTilemap.SetTile(hearth8, tempTile1);

            yield return new WaitForSeconds(0.5f);

            propsTilemap.SetTile(hearth9, tempTile1);

            yield return new WaitForSeconds(0.5f);

            propsTilemap.SetTile(hearth10, tempTile1);

            yield return new WaitForSeconds(5f);

            propsTilemap.SetTile(hearth1, null);

            yield return new WaitForSeconds(0.5f);

            propsTilemap.SetTile(hearth5, null);
            propsTilemap.SetTile(hearth6, null);

            yield return new WaitForSeconds(0.5f);

            propsTilemap.SetTile(hearth2, null);
            propsTilemap.SetTile(hearth3, null);
            propsTilemap.SetTile(hearth4, null);
            propsTilemap.SetTile(hearth7, null);
            propsTilemap.SetTile(hearth8, null);
            propsTilemap.SetTile(hearth9, null);
            propsTilemap.SetTile(hearth10, null);

            yield return new WaitForSeconds(2);

            propsTilemap.SetTile(aboveTilePositionFix, null);

            yield return new WaitForSeconds(1.5f);

            PapisScenario scenario = FindAnyObjectByType<PapisScenario>();
            StartCoroutine(scenario.EndScenario());

            player.canMove = true;
        }
    }
}