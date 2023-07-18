using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerProjectileSpawner : MonoBehaviour
{
    [SerializeField] PlayerProjectile projectilePrefab;
    private MovementScript moveScript;
    [SerializeField] Transform spawnPosition;
    [SerializeField] float spawnDelay = 0.25f;

    private bool canSpawn;

    private void Awake()
    {
        TryGetComponent(out moveScript);
    }

    void Start()
    {
        canSpawn = true;
    }

    public void SpawnProjectile()
    {
        if (!canSpawn)
        {
            return;
        }

        canSpawn = false;

        PlayerProjectile projectile = Instantiate(projectilePrefab, spawnPosition.position, Quaternion.identity);
        Vector3 newVelocity = Vector3.right;

        switch (moveScript.FacingRight)
        {
            case true:
                projectile.RB.velocity = newVelocity * projectile.ProjectileVelocity;
                break;

            case false:
                projectile.RB.velocity = -newVelocity * projectile.ProjectileVelocity;
                break;
        }

        StartCoroutine(DelayTimer());
    }

    IEnumerator DelayTimer()
    {
        yield return new WaitForSeconds(spawnDelay);
        canSpawn = true;
    }

}
