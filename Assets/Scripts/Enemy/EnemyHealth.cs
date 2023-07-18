using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 2;
    private int health;
    [SerializeField] Collider2D hitbox;

    private void Start()
    {
        health = maxHealth;
    }

    private void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerProjectile>(out PlayerProjectile projectile))
        {
            health -= projectile.Damage;
            Destroy(collision.gameObject);
        }
    }
}
