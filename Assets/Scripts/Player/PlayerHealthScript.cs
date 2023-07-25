using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthScript : MonoBehaviour
{
    [SerializeField] KnockbackScript knockbackScript;

    [SerializeField] Collider2D hitbox;
    [SerializeField] int maxPlayerHealth = 10;
    private int playerHealth = 10;
    public int PlayerHealth => playerHealth;
    public int MaxPlayerHealth => maxPlayerHealth;

    private bool playerAlive;
    public bool PlayerAlive => playerAlive;

    [SerializeField] float takeDamageCoolDown = 1f;
    public float TakeDamageCoolDown => takeDamageCoolDown;
    private bool canTakeDamage;
    public bool CanTakeDamage => canTakeDamage;

    [SerializeField] UIHeartScript heartScript;

    private void Awake()
    {
        TryGetComponent(out KnockbackScript knockbackScript);
        playerHealth = maxPlayerHealth;
        playerAlive = true;
        canTakeDamage = true;
    }

    private void Start()
    {
        heartScript.RenderHeart();
    }

    private void Update()
    {
        if (playerHealth <= 0)
        {
            playerAlive = false;
            gameObject.SetActive(false);
            Debug.Log("dead");

            SoundManager.PlayerOneHeart.Stop();

            //Destroy(gameObject);
        }

        if (playerHealth > maxPlayerHealth)
        {
            playerHealth = maxPlayerHealth;
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

            TakeDamage(enemyDamage.Damage);

            StartCoroutine(DamageCoolDown(takeDamageCoolDown));

            if (collision.gameObject.TryGetComponent(out EnemyProjectile projectile))
            {
                Destroy(collision.gameObject);
            }
        }
        
    }

    IEnumerator DamageCoolDown(float duration)
    {
        yield return new WaitForSeconds(duration);
        canTakeDamage = true;
    }

    public void Heal(int _healAmount)
    {
        playerHealth += _healAmount;

        SoundManager.PlaySound(SoundManager.PlayerGetHP, false);

        if (playerHealth > 2)
        {
            SoundManager.PlayerOneHeart.Stop();
        }

        heartScript.RenderHeart();
    }

    public void TakeDamage(int _damage)
    {
        playerHealth -= _damage;

        if (playerHealth % 2 == 0)
        {
            SoundManager.PlaySound(SoundManager.PlayerLoseHP2, false);
        }
        else
        {
            SoundManager.PlaySound(SoundManager.PlayerLoseHP1, false);
        }

        if (playerHealth <= 2)
        {
            SoundManager.PlayerOneHeart.Play();
        }

        heartScript.RenderHeart();
    }
}
