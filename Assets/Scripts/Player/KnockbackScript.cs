using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackScript : MonoBehaviour
{
    [SerializeField] PlayerHealthScript healthScript;
    [SerializeField] MovementScript movementScript;
    [SerializeField] float knockbackVelocity = 3f;
    [SerializeField] float verticalKnockbackVelocity = 3f;

    [SerializeField] float knockbackDuration = 0.5f;
    private float knockbackTimer;
    private bool canKnockback;
    private float previousVelocityY;

    private Vector3 knockbackDirection;

    private void Start()
    {
        canKnockback = true;
        knockbackTimer = knockbackDuration;
    }

    private void Update()
    {
        if (!healthScript.CanTakeDamage && canKnockback)
        {
            canKnockback = false;
            InitiateKnockback();
        }

        if (!canKnockback)
        {
            knockbackTimer -= Time.deltaTime;
            
            if (knockbackTimer <= 0f)
            {
                canKnockback = true;
                movementScript.SetRigidbodyVelocity(new Vector3(0f, previousVelocityY, 0f));
                movementScript.SetToggleMove(true);
            }
        }
        else
        {
            knockbackTimer = knockbackDuration;
        }


    }

    void InitiateKnockback()
    {
        movementScript.SetToggleMove(false);
        previousVelocityY = movementScript.GetCurrentVelocity().y;
        Vector2 _direction = new Vector2(knockbackDirection.x, verticalKnockbackVelocity + knockbackDirection.y);
        _direction.x *= knockbackVelocity;
        movementScript.SetRigidbodyVelocity(_direction * movementScript.KnockbackMultiplier);
    }

    public void TriggerKnockback(Vector3 direction)
    {
        canKnockback = true;
        knockbackDirection = direction;
    }
}
