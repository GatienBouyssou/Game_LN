using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class WaveManager : MonoBehaviour
{
    public TMP_Text waveNumMesh;
    public TMP_Text waveTimerMesh;
    public float waveDuration = 90;
    private bool isPause = false;
    private float curTime;
    private int waveNum = 1;
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
        waveNum += 1;
        waveNumMesh.text = $"Vague {waveNum}";
        foreach (GameObject spawn in spawns)
        {
            SpawnManager spawnManager = spawn.GetComponent<SpawnManager>();
            spawnManager.SetPause(true);
            spawnManager.NextLevel();
        }
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in enemies) {
            Destroy(enemy);
        }
        Invoke("DeactivatePause", 5f);
    }

    void DeactivatePause()
    {
        foreach (GameObject spawn in spawns)
        {
            SpawnManager spawnManager = spawn.GetComponent<SpawnManager>();
            spawnManager.SetPause(false);
        }
        isPause = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isPause) {
            return;
        }
        curTime -= Time.deltaTime;
        SetUpTimer();
        if (curTime <= 0)
        {
            if (waveNum==3) {
                SceneManager.LoadScene("LaWin");
                return;
            }
            ActivatePause();
            waveTimerMesh.text = "Pause Prépare toi !";
            isPause = true;
            curTime = waveDuration;
        }
    }
}
