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
    [SerializeField] private GameObject[] bullet;
    [SerializeField] private GameObject bulletS;
    [SerializeField] private float _bulletSpeed;

    private float _cdTimer = Mathf.Infinity;

    private void Update()
    {
        _cdTimer += Time.deltaTime;

        if (_cdTimer > _cd)
        {
            _cdTimer = 0;
            var projectile = Instantiate(bulletS,firepoint.position,firepoint.rotation);
            projectile.GetComponent<Rigidbody2D>().velocity = firepoint.up * _bulletSpeed;
        }
    }
    private int FindBullet()
    {
        for (int i = 0; i < bullet.Length; i++)
        {
            if (!bullet[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }
}
