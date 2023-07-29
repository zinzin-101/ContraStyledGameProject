using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int maxBossHealth = 100;
    private int bossHealth;

    private bool isDead = false;
    // Add other variables or methods related to boss health if needed

    private void Start()
    {
        bossHealth = maxBossHealth;
    }

    public void TakeDamage(int damage)
    {
        if (!isDead)
        {
            bossHealth -= damage;

            if (bossHealth <= 0)
            {
                bossHealth = 0;
                isDead = true;
                // Call a method in the BossMovement script to handle the boss's death
                BossMovement bossMovement = GetComponentInParent<BossMovement>();
                bossMovement.BossDeath();
            }
        }
    }
}