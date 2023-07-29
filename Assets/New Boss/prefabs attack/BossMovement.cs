using System.Collections;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    public int maxBossHealth = 50;
    private int bossHealth;
    public Collider2D bossHitbox;

    public float speed = 5f;
    public GameObject warningPrefab;
    public GameObject burstAttackPrefab;
    public GameObject beamAttackPrefab;
    public Transform attackSpawner;
    public float burstCooldownTime = 5f;
    public float burstWarningDuration = 2f;
    public float burstSpawnOffset = 5f;
    public float minBeamCooldownTime = 5f;
    public float maxBeamCooldownTime = 10f;

    private Transform player;
    private Rigidbody2D rb;
    private bool isBurstOnCooldown = false;
    private bool isAttacking = false;

    private Animator bossAnimator;
    private bool isDead = false;
    private bool deathAnimationFinished = false;

    private PlayerHealthScript playerHealthScript;

    [SerializeField] AudioClip burstSound, beamChargeSound, bossDeathSound, hitSound;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        bossAnimator = GetComponent<Animator>();
        bossHealth = maxBossHealth;

        float initialDelay = 2f;
        StartCoroutine(StartBurstAttackWithDelay(initialDelay));
        StartCoroutine(ActivateRandomBeamAttack());
        GameManager.Instance.PlayerObject.TryGetComponent(out playerHealthScript);
    }

    IEnumerator StartBurstAttackWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        StartCoroutine(BurstAttackRoutine());
    }

    void Update()
    {
        if (Vector2.Distance(player.transform.position, GameManager.CurrentCheckpointPos) <= 5f)
        {
            transform.position = new Vector2(player.position.x + 9f, player.position.y + 1f);
        }

        if (!playerHealthScript.PlayerAlive)
        {
            StopBossMovement();
            return;
        }

        if (!isDead && !isAttacking)
        {
            Vector2 targetX = new Vector2(player.position.x + 9f, player.position.y + 1f);
            Vector2 newPos = Vector2.MoveTowards(rb.position, targetX, speed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
        }
        else if (isDead && !isAttacking)
        {
            StopBossMovement();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerProjectile projectile))
        {
            int damageAmount = projectile.Damage;
            TakeDamage(damageAmount);

            AudioSource.PlayClipAtPoint(hitSound, transform.position);

            projectile.PlayDestroyAnimation();
        }
    }

    IEnumerator BurstAttackRoutine()
    {
        while (!isDead)
        {
            if (!isBurstOnCooldown)
            {
                GameObject warningBox = Instantiate(warningPrefab, attackSpawner.position, attackSpawner.rotation);

                BurstWarning warningbox = warningBox.GetComponent<BurstWarning>();

                StopBossMovement();

                yield return new WaitForSeconds(burstWarningDuration);

                ResumeBossMovement();

                Vector3 burstSpawnPosition = attackSpawner.position;
                burstSpawnPosition.x -= burstSpawnOffset;
                Instantiate(burstAttackPrefab, burstSpawnPosition, attackSpawner.rotation);

                AudioSource.PlayClipAtPoint(burstSound, transform.position);

                isBurstOnCooldown = true;
                yield return new WaitForSeconds(burstCooldownTime);
                isBurstOnCooldown = false;
            }
            else
            {
                yield return null;
            }
        }

        yield return new WaitUntil(() => deathAnimationFinished);

        Destroy(gameObject);
    }

    void StopBossMovement()
    {
        isAttacking = true;
        rb.velocity = Vector2.zero;
    }

    void ResumeBossMovement()
    {
        isAttacking = false;
        rb.velocity = new Vector2(-speed, rb.velocity.y);
    }

    IEnumerator ActivateRandomBeamAttack()
    {
        while (!isDead)
        {
            float beamCooldownTime = Random.Range(minBeamCooldownTime, maxBeamCooldownTime);
            yield return new WaitForSeconds(beamCooldownTime);

            Vector3 beamSpawnPosition = attackSpawner.position;
            beamSpawnPosition.x -= 20f;
            Instantiate(beamAttackPrefab, beamSpawnPosition, attackSpawner.rotation);

            AudioSource.PlayClipAtPoint(beamChargeSound, transform.position);
        }
    }

    public void BossDeath()
    {
        if (!isDead)
        {
            isDead = true;

            // Trigger the BossDeath animation
            AudioSource.PlayClipAtPoint(bossDeathSound, transform.position);
            GameManager.IsBossDeath = true;
            bossAnimator.SetBool("IsDead", true);
        }
    }

    public void BossDeathAnimationFinished()
    {
        Destroy(gameObject);
    }


    public void TakeDamage(int damage)
    {
        if (!isDead)
        {
            bossHealth -= damage;

            if (bossHealth <= 0)
            {
                bossHealth = 0;
                BossDeath();
            }
        }
    }
}