using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{
    public float baseHealth = 1000;

    public float regenRate = 5;
    private float health;
    private float cooldownHealing = 1;
    private float remainingTimeHealing;

    void ApplyDamage(float damage) {
        health -= damage;
    }

    // Start is called before the first frame update
    void Start()
    {
        health = baseHealth;
        remainingTimeHealing = cooldownHealing;
    }

    // Update is called once per frame
    void Update()
    {
        remainingTimeHealing -= Time.deltaTime;
        if (remainingTimeHealing < 0) {
            remainingTimeHealing = cooldownHealing;
            health +=  (baseHealth - health)*(regenRate/100);
            if (health > baseHealth) {
                health = baseHealth;
            }
        }
    }
}
