using UnityEngine;
using UnityEngine.Tilemaps;

public class Mami : MonoBehaviour
{
    [Header("Attributs de base")]
    public float moveSpeed = 2f;
    [Header("Positions")]
    public Transform[] checkpoints;

    private Tilemap foundationTilemap;
    private int currentCheckpointIndex = 0;

    private void Start()
    {
        foundationTilemap = GameObject.Find("Fondation").GetComponent<Tilemap>();
    }

    void Update()
    {
        MoveTowardsNextCheckpoint();
        DestroyPlatformAtCurrentPosition();
    }

    void MoveTowardsNextCheckpoint()
    {
        if (currentCheckpointIndex < checkpoints.Length)
        {
            Transform targetCheckpoint = checkpoints[currentCheckpointIndex];
            transform.position = Vector2.MoveTowards(transform.position, targetCheckpoint.position, moveSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, targetCheckpoint.position) < 0.1f)
            {
                currentCheckpointIndex++;
            }
        }
    }

    void DestroyPlatformAtCurrentPosition()
    {
        Vector3 currentPosition = transform.position;
        Vector3Int cellPosition = foundationTilemap.WorldToCell(currentPosition);
        foundationTilemap.SetTile(cellPosition, null);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerDeath();
        }
    }

    void PlayerDeath()
    {
        Debug.Log("Touché");
    }
}
