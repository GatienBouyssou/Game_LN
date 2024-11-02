using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveManager : MonoBehaviour
{
    public TMP_Text waveNumMesh;
    public TMP_Text waveTimerMesh;
    public float waveDuration = 10;
    private bool isPause = false;
    private float curTime;
    private int waveNum;
    private List<GameObject> spawns = new List<GameObject>();

    public void setPause(bool pause)
    {
        isPause = pause;
    }

    // Start is called before the first frame update
    void Start()
    {
        curTime = waveDuration;
        GameObject spawnsHandler = GameObject.Find("Spawns");
        int childCount = spawnsHandler.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Transform child = spawnsHandler.transform.GetChild(i);

            // Access the child GameObject
            spawns.Add(child.gameObject);

            // Do something with the child GameObject
            Debug.Log("Child name: " + child.gameObject.name);
        }

    }

    void SetUpTimer()
    {
        int minutes = (int)curTime / 60;
        int seconds = (int)curTime % 60;
        waveTimerMesh.text = $"{minutes:D2}:{seconds:D2}";
    }

    void ActivatePause()
    {
        foreach (GameObject spawn in spawns)
        {
            SpawnManager spawnManager = spawn.GetComponent<SpawnManager>();
            spawnManager.SetPause(true);
        }
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in enemies) {
            Destroy(enemy);
        }
    }

    // Update is called once per frame
    void Update()
    {
        curTime -= Time.deltaTime;
        SetUpTimer();
        if (curTime <= 0)
        {
            ActivatePause();
            waveTimerMesh.text = "Pause";
            isPause = true;
            curTime = waveDuration;
        }
    }
}
