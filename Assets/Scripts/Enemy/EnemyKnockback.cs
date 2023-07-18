using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnockback : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (TryGetComponent(out PlayerHealthScript playerScript))
        {
            TryGetComponent(out MovementScript movementScript);
            if (playerScript.CanTakeDamage)
            {
                Vector2 _direction = (transform.position - collision.gameObject.transform.position).normalized;
                float relativeVelocity = collision.relativeVelocity.magnitude;
                Vector2 _newVelocity = relativeVelocity * movementScript.KnockbackMultiplier * -_direction;
                _newVelocity.x *= 10f;
                collision.gameObject.TryGetComponent(out Rigidbody2D rb);
                rb.velocity = _newVelocity;
            }
        }
    }
}
