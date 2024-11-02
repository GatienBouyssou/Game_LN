using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SpawnManager : MonoBehaviour
{
    private string enemyDirectory = "Assets/Prefabs/Limoges/Resources/Enemies";
    private string baseEnemyResources = "Enemies/";
    private List<GameObject> allEnemiesType;
    public float bossSpawnRate = 0.2f;
    public float midBossSpawnRate = 0.2f;
    public float swarmRate = 0.1f;
    public float spawnRate = 0.5f;
    public float timeInterSpawn = 5.0f;
    private float timeBeforeSpawn;
    private bool isPause = false;

    float GetNextRandomSpawn()
    {
        return Random.Range(timeInterSpawn - 2.5f, timeInterSpawn + 2.5f);
    }
    
    void GenerateEnemy()
    {
        float random = Random.Range(0f, 1f);
        if (0 <= random && random < bossSpawnRate)
        {
            Instantiate(allEnemiesType[0], transform.position, Quaternion.identity);
        } else if (bossSpawnRate <= random && random < bossSpawnRate+midBossSpawnRate) {
            Instantiate(allEnemiesType[2], transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(allEnemiesType[1], transform.position, Quaternion.identity);
        }
    }

    public void SetPause(bool pause) {
        isPause = pause;
    }

    // Start is called before the first frame update
    void Start()
    {
        allEnemiesType = new List<GameObject>();
        string[] prefabs = AssetDatabase.FindAssets("t:Prefab", new string[] { enemyDirectory });
        Debug.Log(prefabs);
        foreach (string prefabPath in prefabs)
        {
            string path = AssetDatabase.GUIDToAssetPath(prefabPath);
            string fileName = path.Split('/')[^1].Split('.')[0];
            var enemyPrefab = Resources.Load<GameObject>(baseEnemyResources+fileName);
            allEnemiesType.Add(enemyPrefab);
        }
        timeBeforeSpawn = GetNextRandomSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPause) {
            return;
        }
        timeBeforeSpawn -= Time.deltaTime;
        if (timeBeforeSpawn < 0)
        {
            timeBeforeSpawn = GetNextRandomSpawn();
            GenerateEnemy();
        }
    }
}
