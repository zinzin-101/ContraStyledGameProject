using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingEnemy : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;

    Vector2 moveVector;
    [SerializeField] float distanceToStartMoving = 6f;
    private float distanceFromPlayer;
    private Transform playerTransform;
    private bool canMove;

    private MovementScript movementScript;

    private void Awake()
    {
        TryGetComponent(out movementScript);
    }

    private void Start()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        canMove = false;
    }

    private void Update()
    {
        distanceFromPlayer = Vector2.Distance(transform.position, playerTransform.position);
        if (distanceFromPlayer <= distanceToStartMoving)
        {
            canMove = true;
        }

        if (canMove)
        {
            moveVector = (playerTransform.position - transform.position).normalized;

        }
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            movementScript.SetRigidbodyVelocity(moveVector * moveSpeed);
        }
        else
        {
            movementScript.SetRigidbodyVelocity(Vector3.zero);
        }
    }
}
