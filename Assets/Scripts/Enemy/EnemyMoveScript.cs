using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveScript : MonoBehaviour
{
    [SerializeField] float moveDistance = 3f;
    [SerializeField] float moveSpeed = 5f;
    private float moveDuration, timeLeftToMove;
    private bool moveLeft;

    private MovementScript moveScript;

    private void Awake()
    {
        TryGetComponent(out moveScript);
    }

    private void Start()
    {
        moveLeft = true;
        moveDuration = moveDistance / moveSpeed;
        timeLeftToMove = moveDuration;
    }

    private void Update()
    {
        timeLeftToMove -= Time.deltaTime;
        if (timeLeftToMove <= 0f )
        {
            moveLeft = !moveLeft;
            timeLeftToMove = moveDuration;
        }
    }

    private void FixedUpdate()
    {
        switch (moveLeft)
        {
            case true:
                moveScript.Move(-moveSpeed); 
                break;

            case false:
                moveScript.Move(moveSpeed);
                break;
        }
    }
}
