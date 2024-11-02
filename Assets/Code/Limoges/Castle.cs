using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameLN.Limoges;

public class Castle : RegenableEntity
{
    public override void Die()
    {
        SceneManager.LoadScene("GameOver");
    }
}
