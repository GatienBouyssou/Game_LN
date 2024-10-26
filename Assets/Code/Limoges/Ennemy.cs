using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy : MonoBehaviour
{
    public float size;
    public GameObject player;
    public GameObject castle;
    public float speed = 3f;


    // Start is called before the first frame update
    void Start()
    {

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
