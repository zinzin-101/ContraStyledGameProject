using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthScript : MonoBehaviour
{
    [SerializeField] KnockbackScript knockbackScript;

    [SerializeField] Collider2D hitbox;
    [SerializeField] int playerHealth = 10;
    public int PlayerHealth => playerHealth;

    private bool playerAlive;
    public bool PlayerAlive => playerAlive;

    [SerializeField] float takeDamageCoolDown = 1f;
    public float TakeDamageCoolDown => takeDamageCoolDown;
    private bool canTakeDamage;
    public bool CanTakeDamage => canTakeDamage;

    private void Awake()
    {
        TryGetComponent(out KnockbackScript knockbackScript);
        playerAlive = true;
        canTakeDamage = true;
    }

    private void Update()
    {
        if (playerHealth <= 0)
        {
            playerAlive = false;
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.gameObject.TryGetComponent(out EnemyDamage enemyDamage))
        {
            if (!canTakeDamage)
            {
                return;
            }
            canTakeDamage = false;
            
            Vector3 _hitDirection = (transform.position - collision.gameObject.transform.position).normalized;
            knockbackScript.TriggerKnockback(_hitDirection);

            playerHealth -= enemyDamage.Damage;
            Debug.Log("damage taken");
            StartCoroutine(DamageCoolDown(takeDamageCoolDown));

            if (collision.gameObject.TryGetComponent(out EnemyProjectile projectile))
            {
                Destroy(collision.gameObject);
            }
        }
        /*
        if (collision.gameObject.TryGetComponent(HealScript healScript)
        {
            playerHealth += healScript.heal
        }
        */
    }

    IEnumerator DamageCoolDown(float duration)
    {
        yield return new WaitForSeconds(duration);
        canTakeDamage = true;
    }
}
