using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 2;
    private int health;
    [SerializeField] Collider2D hitbox;

    [SerializeField] HealingTank _healingTank;
    [SerializeField] float _hpDropRate = 0.1f;
    [SerializeField] bool canDropHealth;
    private float rnd;

    [SerializeField] bool receiveVerticalKnockback;

    [SerializeField] EnemyKnockback enemyKnockbackScript;

    [SerializeField] AudioClip hitSound, deathSound;

    [SerializeField] GameObject deathParticle;

    private void Start()
    {
        rnd = Random.value;
        health = maxHealth;
    }

    private void Update()
    {
        if (health <= 0)
        {
            if (rnd <= _hpDropRate && canDropHealth)
            {
                _healingTank = Instantiate(_healingTank, transform.position, Quaternion.identity);

            }

            if (deathParticle != null)
            {
                Instantiate(deathParticle, transform.position, Quaternion.identity);
            }

            if (deathSound != null)
            {
                AudioSource.PlayClipAtPoint(deathSound, transform.position);
            }

            LevelManager.Instance.FadeToBlackLoadScene("CutsceneStart");
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerProjectile projectile))
        {
            health -= projectile.Damage;

            AudioSource.PlayClipAtPoint(hitSound, transform.position);

            Vector3 _hitDirection = (transform.position - collision.gameObject.transform.position).normalized;
            if (!receiveVerticalKnockback)
            {
                _hitDirection.y = 0f;
            }
            StartCoroutine(enemyKnockbackScript.KnockbackForEnemy(_hitDirection));

            projectile.PlayDestroyAnimation();

            //Destroy(collision.gameObject);
        }
    }
}
