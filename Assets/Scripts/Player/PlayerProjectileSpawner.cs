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
    public float OverheatStackLimit => _overheatStackLimit;

    [SerializeField] private float _overheatCooldownTimer;
    public float OverheatCooldownTimer => _overheatCooldownTimer;
    private float _overheatCooldownTimerCount;
    public float OverheatCooldownTimerCount => _overheatCooldownTimerCount;
    private float _overheatStack;
    public float OverheatStack => _overheatStack;
    private bool _isOverHeat;
    public bool IsOverheat => _isOverHeat;

    private bool canSpawn;

    private void Awake()
    {
        TryGetComponent(out moveScript);
    }

    void Start()
    {
        canSpawn = true;
        _overheatStack = 0f;
        
    }
    
    void Update()
    {
        if(_overheatStack > 0)
        {
            _overheatStack -= Time.deltaTime;
        }

        if (_isOverHeat)
        {
            _overheatCooldownTimerCount -= Time.deltaTime;
            if (_overheatCooldownTimerCount <= 0f)
            {
                _isOverHeat = false;
                _overheatStack = 0;
            }
        }
        else
        {
            _overheatCooldownTimerCount = _overheatCooldownTimer;
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
                _overheatStack = _overheatStackLimit;
                Debug.Log("Overheating"); //remove later
                _isOverHeat = true;
                //StartCoroutine(OverHeatCooldown());
            }
        }

        
    }

    IEnumerator DelayTimer()
    {
        yield return new WaitForSeconds(spawnDelay);
        canSpawn = true;
        
    }
}
