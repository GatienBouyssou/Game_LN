using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Code.RiveB.Challenge
{
    public class Fernando : MonoBehaviour
    {
        public Tilemap propsTilemap;
        public Sprite targetSprite;
        public GameObject textPrefab;

        private GameObject activeText;
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
            else if (activeText != null)
            {
                Destroy(activeText);
            }
        }

        private void TriggerDialogue(Vector3Int tilePosition)
        {
            if (activeText == null && textPrefab != null && !alreadyTalk)
            {
                alreadyTalk = true;
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                Vector3 worldPosition = propsTilemap.CellToWorld(tilePosition) + new Vector3(2.1f, 0.75f, 0);
                textPrefab.transform.position = worldPosition;
                player.transform.position -= new Vector3(0.5f, 0, 0);

                TMP_Text tmp = textPrefab.GetComponent<TMP_Text>();
                tmp.text = "Holaaa"; 

                StartCoroutine(DialogueCoroutine(tmp, player.GetComponent<PlayerMovement>(), tilePosition));
            }
        }

        private IEnumerator DialogueCoroutine(TMP_Text tmp, PlayerMovement player, Vector3Int tilePosition)
        {
            yield return new WaitForSeconds(2.5f);

            tmp.text = "Que tal estás ?";

            yield return new WaitForSeconds(4f);

            tmp.text = "¡ Oh non !";

            yield return new WaitForSeconds(2f);

            Vector3 worldPosition = propsTilemap.CellToWorld(tilePosition) + new Vector3(1f, 1.5f, 0);
            textPrefab.transform.position = worldPosition;
            tmp.text = "Ta mère... elle va nous faire réviser quand on va rentrer chez toi..";

            yield return new WaitForSeconds(8);

            Vector3 worldPosition2 = propsTilemap.CellToWorld(tilePosition) + new Vector3(0.95f, 1.78f, 0);
            textPrefab.transform.position = worldPosition2;
            tmp.text = "Bon va chercher dans mon coffre j'y ai mis des cadeaux pour toi J'espère que ça te permettras de pouvoir fuir";

            yield return new WaitForSeconds(10f);

            textPrefab.transform.position = worldPosition2 - new Vector3(0, 0.38f, 0);
            tmp.text = "Et si tu vois Olga demande lui de m'aider ce soir";

            yield return new WaitForSeconds(4f);

            tmp.text = string.Empty;
            player.canMove = true;
        }

        public bool HasAlreadyTalk()
        {
            return alreadyTalk;
        }
    }
}
