using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerProjectile : MonoBehaviour
{
    [SerializeField] float duration = 5f;

    [SerializeField] float projectileVelocity = 10f;
    public float ProjectileVelocity => projectileVelocity;

    [SerializeField] int damage = 1;
    private Rigidbody2D rb;
    public Rigidbody2D RB => rb;
    public int Damage => damage;

    private Animator animator;

    [SerializeField] GameObject particleEffect;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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
    }

    public void PlayDestroyAnimation()
    {
        if (particleEffect != null)
        {
            Instantiate(particleEffect, transform.position, Quaternion.identity);
        }

        animator.SetTrigger("Destroy");
        rb.velocity = Vector3.zero;
        Destroy(gameObject, 0.06f);
    }
}
