using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy : MonoBehaviour
{
    public float size;
    private GameObject player;
    private GameObject castle;
    public float speed = 3f;
    public float attack = 10;
    public float health = 20;

    void Die() 
    {
        Destroy(gameObject);
    }

    void ApplyDamage(float damage)
    {
        health -= damage;
        if (health < 0) {
            Die();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Hero");
        castle = GameObject.Find("Castle");
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        float distanceToCastle = Vector2.Distance(transform.position, castle.transform.position);

        GameObject target = distanceToPlayer < distanceToCastle ? player : castle;

        Vector2 direction = target.transform.position - transform.position;
        direction.Normalize();
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Castle") || other.gameObject.CompareTag("Player")) {
            other.gameObject.SendMessage("ApplyDamage", attack);
            Destroy(gameObject);
        }
    }
}
