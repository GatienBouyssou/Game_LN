using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameLN.Limoges;

public class Ennemy : RegenableEntity
{
    public float size;
    private GameObject player;
    private GameObject castle;

    public override void Start()
    {
        base.Start();
        player = GameObject.Find("Hero");
        castle = GameObject.Find("Castle");
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
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
