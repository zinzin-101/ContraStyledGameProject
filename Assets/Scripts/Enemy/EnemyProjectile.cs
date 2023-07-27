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

    private Collider2D col;

    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();

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
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Ground"))
        {
            PlayDestroyAnimation();
        }

        if (!collision.gameObject.TryGetComponent(out PlayerHealthScript playerScript))
        {
            Physics2D.IgnoreCollision(collision.collider, col);
        }
    }

    public void PlayDestroyAnimation()
    {
        animator.SetTrigger("Destroy");
        rb.velocity = Vector3.zero;
        Destroy(gameObject, 0.06f);
    }
}
