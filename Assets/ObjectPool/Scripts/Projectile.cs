using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 5f;
    private float lifeTimer;

    void OnEnable()
    {
        lifeTimer = lifetime;
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        lifeTimer -= Time.deltaTime;

        if (lifeTimer <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }
}