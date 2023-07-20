using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RangeEnemy : MonoBehaviour
{
    [Header("Attack Parameter")]
    [SerializeField] private float _cd;
    [SerializeField] private float _range;
    [SerializeField] private int _dmg;

    [Header("Projectile Parameter")]
    [SerializeField] private Transform firepoint;
    [SerializeField] private Transform aimDirection;
    Vector2 aimVector;

    [SerializeField] private GameObject bulletS;
    [SerializeField] private float _bulletSpeed;

    private float _cdTimer = Mathf.Infinity;

    [SerializeField] bool aimAtPlayer;
    private Transform playerTransform;

    private void Start()
    {
        aimVector = (aimDirection.position - firepoint.position).normalized;
        if (aimAtPlayer)
        {
            playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        }
    }

    private void Update()
    {
        _cdTimer += Time.deltaTime;

        if (_cdTimer > _cd)
        {
            _cdTimer = 0;
            var projectile = Instantiate(bulletS,firepoint.position,firepoint.rotation);

            if (aimAtPlayer)
            {
                /*
                try
                {
                    aimVector = (playerTransform.position - firepoint.position).normalized;
                }
                catch
                {
                    aimAtPlayer = false;
                }
                */
                aimVector = (playerTransform.position - firepoint.position).normalized;
            }
            projectile.GetComponent<Rigidbody2D>().velocity = aimVector * _bulletSpeed;
        }
    }
}
