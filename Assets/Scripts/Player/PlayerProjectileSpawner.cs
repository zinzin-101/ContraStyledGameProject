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

    
    [SerializeField] private float _overheatStackLimit = 10f;
    public float OverheatStackLimit => _overheatStackLimit;

    [SerializeField] private float _overheatCooldownTimer = 2f;
    public float OverheatCooldownTimer => _overheatCooldownTimer;
    private float _overheatCooldownTimerCount;
    public float OverheatCooldownTimerCount => _overheatCooldownTimerCount;
    private float _overheatStack;
    [SerializeField] float unstackSpeed = 1f;
    public float OverheatStack => _overheatStack;
    private bool _isOverHeat;
    public bool IsOverheat => _isOverHeat;

    private bool canSpawn;

    //sound
    private bool canPlayOverheat;

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
            _overheatStack -= Time.deltaTime * unstackSpeed;
        }

        if (_isOverHeat)
        {
            _overheatCooldownTimerCount -= Time.deltaTime;
            if (_overheatCooldownTimerCount <= 0f)
            {
                _isOverHeat = false;
                _overheatStack = 0;

                SoundManager.PlayerOverheating.Stop();
                SoundManager.PlayerOverheatFinished.Play();
            }
        }
        else
        {
            _overheatCooldownTimerCount = _overheatCooldownTimer;
        }

        if (canPlayOverheat)
        {
            canPlayOverheat = false;
            SoundManager.PlayerOverheating.Play();
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
            Vector3 newScale = projectile.transform.localScale;

            switch (moveScript.FacingRight)
            {
                case true:
                    projectile.RB.velocity = newVelocity * projectile.ProjectileVelocity;
                    break;

                case false:
                    projectile.RB.velocity = -newVelocity * projectile.ProjectileVelocity;
                    newScale *= -1f;
                    break;
            }
            projectile.transform.localScale = newScale;

            _overheatStack++;
            StartCoroutine(DelayTimer());

            if (_overheatStack >= _overheatStackLimit)
            {
                _overheatStack = _overheatStackLimit;
                _isOverHeat = true;

                //overheat sfx
                SoundManager.PlayerOverheatStart.Play();
                canPlayOverheat = true;
            }

            SoundManager.PlayerShoot.Play();
        }

        
    }

    IEnumerator DelayTimer()
    {
        yield return new WaitForSeconds(spawnDelay);
        canSpawn = true;
        
    }

    public void SetCanFire(bool value)
    {
        canSpawn = value;
    }
}
