using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(EnemyDamage))]

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] float duration = 5f;

    [SerializeField] float projectileVelocity = 10f;
    public float ProjectileVelocity => projectileVelocity;

    private EnemyDamage enemyDamage;
    private int damage;
    public int Damage => damage;
    private Rigidbody2D rb;
    public Rigidbody2D RB => rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        TryGetComponent(out enemyDamage);
        damage = enemyDamage.Damage;
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
