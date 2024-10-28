using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class Mami : MonoBehaviour
{
    [Header("Attributs de base")]
    public float moveSpeed = 2f;
    [Header("Positions")]
    public GameObject[] checkpoints;

    private Tilemap foundationTilemap;
    private int currentCheckpointIndex = 0;

    [Header("Taille de la zone de destruction")]
    public Vector2 influenceArea = new Vector2(1f, 1f); 

    private void Start()
    {
        foundationTilemap = GameObject.Find("Fondation").GetComponent<Tilemap>();
    }

    void Update()
    {
        MoveTowardsNextCheckpoint();
        DestroyTilesInArea();
    }

    void MoveTowardsNextCheckpoint()
    {
        if (currentCheckpointIndex < checkpoints.Length)
        {
            Transform targetCheckpoint = checkpoints[currentCheckpointIndex].transform;
            transform.position = Vector2.MoveTowards(transform.position, targetCheckpoint.position, moveSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, targetCheckpoint.position) < 0.1f)
            {
                currentCheckpointIndex++;
            }
        }
    }

    void DestroyTilesInArea()
    {
        Vector3 currentPosition = transform.position;

        Vector3Int minCellPosition = foundationTilemap.WorldToCell(currentPosition - (Vector3)influenceArea / 2);
        Vector3Int maxCellPosition = foundationTilemap.WorldToCell(currentPosition + (Vector3)influenceArea / 2);

        for (int x = minCellPosition.x; x <= maxCellPosition.x; x++)
        {
            for (int y = minCellPosition.y; y <= maxCellPosition.y; y++)
            {
                Vector3Int cellPosition = new Vector3Int(x, y, 0);
                foundationTilemap.SetTile(cellPosition, null);
            }
        }
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
