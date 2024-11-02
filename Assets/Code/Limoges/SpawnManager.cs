using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SpawnManager : MonoBehaviour
{
    private string[] enemyPath = new string[]{"Enemies/Enemy", "Enemies/MidBoss", "Enemies/Boss"};
    private List<GameObject> allEnemiesType;
    public float bossSpawnRate = 0.2f;
    public float midBossSpawnRate = 0.2f;
    public float swarmRate = 0.1f;
    public float spawnRate = 0.5f;
    public float timeInterSpawn = 5.0f;
    private float timeBeforeSpawn;
    private bool isPause = false;

    private int level =1;

    float GetNextRandomSpawn()
    {
        return Random.Range((timeInterSpawn - 2.5f)/level, (timeInterSpawn + 2.5f));
    }

    void GenerateEnemy()
    {
        float random = Random.Range(0f, 1f);
        if (0 <= random && random < bossSpawnRate)
        {
            Instantiate(allEnemiesType[0], transform.position, Quaternion.identity);
        }
        else if (bossSpawnRate <= random && random < bossSpawnRate + midBossSpawnRate)
        {
            Instantiate(allEnemiesType[2], transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(allEnemiesType[1], transform.position, Quaternion.identity);
        }
    }

    public void SetPause(bool pause)
    {
        isPause = pause;
        timeBeforeSpawn = timeInterSpawn / level;
    }

    public void NextLevel()
    {
        level += 1;
    }


    // Start is called before the first frame update
    void Start()
    {
        allEnemiesType = new List<GameObject>();
        foreach (string prefabPath in enemyPath)
        {
            var enemyPrefab = Resources.Load<GameObject>(prefabPath);
            allEnemiesType.Add(enemyPrefab);
        }
        timeBeforeSpawn = GetNextRandomSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPause)
        {
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
