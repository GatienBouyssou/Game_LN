using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameLN.Limoges
{
    public class RegenableEntity : MonoBehaviour
    {
        public float baseHealth;
        public float regenRate;
        public float speed;
        public float attack;
        protected float health;
        protected float cooldownHealing = 1;
        protected float remainingTimeHealing;

        protected Slider healthBar;

        public virtual void Die()
        {
            Destroy(gameObject);
        }

        public virtual void ApplyDamage(float damage)
        {
            health -= damage;
            if (healthBar != null) {
                healthBar.value = health/baseHealth;
            }
            if (health <= 0)
            {
                Die();
            }
        }

        protected virtual void SetUpHealthBar()
        {
            var healthCanvas = Resources.Load<Canvas>("HealthBar");
            var healthBarObject = Instantiate(healthCanvas, transform, false);
            healthBarObject.transform.position = transform.position + new Vector3(0, 1, 0);
            healthBar = healthBarObject.GetComponent<Slider>();
            healthBar.value = 100;
        }

        // Start is called before the first frame update
        public virtual void Start()
        {
            health = baseHealth;
            remainingTimeHealing = cooldownHealing;
            // SetUpHealthBar();
        }

        // Update is called once per frame
        public virtual void Update()
        {
            remainingTimeHealing -= Time.deltaTime;
            if (remainingTimeHealing < 0)
            {
                remainingTimeHealing = cooldownHealing;
                health += (baseHealth - health) * (regenRate / 100);
                if (health > baseHealth)
                {
                    health = baseHealth;
                }
            }
        }
    }
}

