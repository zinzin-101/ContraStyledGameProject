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
        if (collision.gameObject.CompareTag("Wall"))
        {
            PlayDestroyAnimation();
        }
    }

    public void PlayDestroyAnimation()
    {
        animator.SetTrigger("Destroy");
        Destroy(gameObject, 0.06f);
    }
}
