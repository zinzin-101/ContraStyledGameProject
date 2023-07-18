using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Walk Length")]
    [SerializeField] private Transform rightLimit;
    [SerializeField] private Transform leftLimit;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("Move speed")]
    [SerializeField] private float speed;
    private Vector3 _enemyScale;
    private bool _moveLeft;

    private void Awake()
    {

        _enemyScale = enemy.localScale;
    }

    private void FixedUpdate()
    {
        if (_moveLeft)
        {
            if (enemy.position.x >= leftLimit.position.x)
            {
                MoveInDirection(-1);

            }
            else
            {
                DirectionChange();
            }
        }
        else
        {
            if (enemy.position.x <= rightLimit.position.x) {
                MoveInDirection(1);
                    }
            else DirectionChange();
        } 
        
    }
    private void DirectionChange()
    {
        _moveLeft = !_moveLeft;
    }
    private void MoveInDirection(int _direction)
    { 
        enemy.localScale = new Vector3(Mathf.Abs( _enemyScale.x) * _direction, _enemyScale.y, _enemyScale.z);

        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed,
            enemy.position.y, enemy.position.z);


    }
}
