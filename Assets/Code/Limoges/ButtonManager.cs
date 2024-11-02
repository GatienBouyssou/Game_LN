using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public string sceneName = "Limoges/Limoges";
    
    public void LoadScene() {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame() 
    {
        Application.Quit();
    }
}
