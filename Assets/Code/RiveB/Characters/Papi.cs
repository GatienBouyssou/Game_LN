using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Code.RiveB.Characters
{
    public class Papi : MonoBehaviour
    {
        public Tilemap propsTilemap;
        public Sprite targetSprite;
        public GameObject textPrefab;

        private bool alreadyTalk = false;

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

            if (currentTile is Tile tile && tile.sprite == targetSprite && player.GetComponent<PlayerMovement>().isPlayerGrounded() && !alreadyTalk)
            {
                player.GetComponent<PlayerMovement>().canMove = false;
                TriggerDialogue(playerTilePosition);
            }
        }

        private void TriggerDialogue(Vector3Int tilePosition)
        {
            if (textPrefab != null && !alreadyTalk)
            {
                alreadyTalk = true;
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                Vector3 worldPosition = propsTilemap.CellToWorld(tilePosition) + new Vector3(2.1f, 1.6f, 0);
                textPrefab.transform.position = worldPosition;
                player.transform.position -= new Vector3(0.5f, 0, 0);

                TMP_Text tmp = textPrefab.GetComponent<TMP_Text>();

                StartCoroutine(DialogueCoroutine(tmp, player.GetComponent<PlayerMovement>(), tilePosition));
            }
        }

        private IEnumerator DialogueCoroutine(TMP_Text tmp, PlayerMovement player, Vector3Int tilePosition)
        {
            tmp.text = "Bah alors tu n'as pas rangé ta chambre ?";

            yield return new WaitForSeconds(3.25f);

            Vector3 worldPosition = propsTilemap.CellToWorld(tilePosition) + new Vector3(1f, 1.5f, 0);
            textPrefab.transform.position = worldPosition;
            tmp.text = "Bon aller ce n'est pas grave... mange ta soupe ma chérie, ça ira mieux après..";

            yield return new WaitForSeconds(7.5f);

            Vector3 worldPosition2 = propsTilemap.CellToWorld(tilePosition) + new Vector3(0.95f, 1.78f, 0);
            textPrefab.transform.position = worldPosition2;
            tmp.text = "Et le dis pas à ta mère mais j'ai caché dans mon coffre un dessert pour toi, profites-en !";

            yield return new WaitForSeconds(10);

            textPrefab.transform.position = worldPosition2 - new Vector3(0, 0.38f, 0);
            tmp.text = "A tout de suite !";

            yield return new WaitForSeconds(3f);

            tmp.text = string.Empty;
            player.canMove = true;
        }

        public bool HasAlreadyTalk()
        {
            return alreadyTalk;
        }
    }
}