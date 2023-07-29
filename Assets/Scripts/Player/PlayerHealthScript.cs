using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthScript : MonoBehaviour
{
    [SerializeField] KnockbackScript knockbackScript;
    [SerializeField] PlayerProjectileSpawner attackScript;

    [SerializeField] Collider2D hitbox;
    [SerializeField] int maxPlayerHealth = 10;
    private int playerHealth = 10;
    public int PlayerHealth => playerHealth;
    public int MaxPlayerHealth => maxPlayerHealth;
    private int previousHealth;

    private bool playerAlive;
    public bool PlayerAlive => playerAlive;

    [SerializeField] float takeDamageCoolDown = 1f;
    public float TakeDamageCoolDown => takeDamageCoolDown;
    private bool canTakeDamage;
    public bool CanTakeDamage => canTakeDamage;

    [SerializeField] UIHeartScript heartScript;

    private SpriteRenderer spriteRenderer;

    [SerializeField] float hitStopTime = 0f;
    [SerializeField] float hitStopDelay = 0.5f;

    private void Awake()
    {
        TryGetComponent(out knockbackScript);
        TryGetComponent(out attackScript);
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        playerHealth = maxPlayerHealth;
        playerAlive = true;
        canTakeDamage = true;

        previousHealth = playerHealth;
    }

    private void Start()
    {
        heartScript.RenderHeart();
    }

    private void Update()
    {
        if (previousHealth !=  playerHealth)
        {
            previousHealth = playerHealth;
            heartScript.RenderHeart();
        }

        if (playerHealth <= 0)
        {
            playerAlive = false;
            gameObject.SetActive(false);

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
                projectile.PlayDestroyAnimation();
            }
        }

        if (collision.gameObject.TryGetComponent(out BossCollideDamage collideDamage))
        {
            if (!canTakeDamage)
            {
                return;
            }
            canTakeDamage = false;
            TakeDamage(collideDamage.collideDmg);
            StartCoroutine(DamageCoolDown(takeDamageCoolDown));
        }


        if (collision.gameObject.TryGetComponent(out BurstDamage burstDamage))
        {
            if (!canTakeDamage)
            {
                return;
            }
            canTakeDamage = false;

            TakeDamage(burstDamage.burstDamage);

            StartCoroutine(DamageCoolDown(takeDamageCoolDown));
        }
        if (collision.gameObject.TryGetComponent(out BeamDamage beamDamage))
        {
            if (!canTakeDamage)
            {
                return;
            }
            canTakeDamage = false;

            TakeDamage(beamDamage.beamDamage);

            StartCoroutine(DamageCoolDown(takeDamageCoolDown));
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

        //heartScript.RenderHeart();
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

        if (playerHealth > 0)
        {
            StartCoroutine(HitStop(hitStopTime, 1f, hitStopDelay));
        }
        //heartScript.RenderHeart();
    }

    public void RespawnPlayer(Vector3 respawnPosition)
    {
        transform.position = respawnPosition;
        playerHealth = maxPlayerHealth;
        canTakeDamage = true;
        attackScript.SetCanFire(true);
        playerAlive = true;
    }

    IEnumerator HitStop(float timeScaleEffect, float timeScaleRestore, float delay)
    {
        Color _defaultColor = spriteRenderer.color;

        Time.timeScale = timeScaleEffect;
        spriteRenderer.color = Color.red;

        yield return new WaitForSecondsRealtime(delay);

        Time.timeScale = timeScaleRestore;
        spriteRenderer.color = _defaultColor;
    }
}
