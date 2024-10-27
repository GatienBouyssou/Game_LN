using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy : MonoBehaviour
{
    public float size;
    private GameObject player;
    private GameObject castle;
    public float speed = 3f;
    public int attack = 10;
    public int health = 20;


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
}
