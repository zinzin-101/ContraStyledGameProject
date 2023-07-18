using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerProjectileSpawner : MonoBehaviour
{
    [SerializeField] PlayerProjectile projectilePrefab;
    private MovementScript moveScript;
    [SerializeField] Transform spawnPosition;
    [SerializeField] float spawnDelay = 0.25f;

    
    [SerializeField] private float _overheatStackLimit;
    [SerializeField] private float _overheatCooldownTimer;
    float _overheatStack =0;
    private bool _isOverHeat;

    private bool canSpawn;

    private void Awake()
    {
        TryGetComponent(out moveScript);
    }

    void Start()
    {
        canSpawn = true;
        
    }
    
    void Update()
    {
        if(_overheatStack > 0)
        {
            _overheatStack -= Time.deltaTime;
        }
    }
    

    public void SpawnProjectile()
    {
        if (!canSpawn)
        {
            return;
        }

        

        if (_overheatStack <= _overheatStackLimit && _isOverHeat == false)
        {
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
            _overheatStack++;
            Debug.Log(_overheatStack); //remove later
            StartCoroutine(DelayTimer());
            if (_overheatStack >= _overheatStackLimit)
            {
                Debug.Log("Overheating"); //remove later
                _isOverHeat = true;
                StartCoroutine(OverHeatCooldown());
            }
        }

        
    }

    IEnumerator DelayTimer()
    {
        yield return new WaitForSeconds(spawnDelay);
        canSpawn = true;
        
    }

    IEnumerator OverHeatCooldown()
    {
        yield return new WaitForSeconds(_overheatCooldownTimer);
        _isOverHeat = false;
        _overheatStack = 0;
    }
}
