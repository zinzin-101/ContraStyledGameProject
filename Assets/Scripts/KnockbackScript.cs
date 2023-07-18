using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackScript : MonoBehaviour
{
    [SerializeField] PlayerHealthScript healthScript;
    [SerializeField] MovementScript movementScript;
    [SerializeField] float knockbackVelocity = 3f;
    [SerializeField] float knockbackDuration = 0.5f;
    [SerializeField] float knockBackGravity = 2.5f;

    private bool canKnockback;

    private void Awake()
    {
        TryGetComponent(out PlayerHealthScript healthScript);
        TryGetComponent(out MovementScript movementScript);
        TryGetComponent(out Rigidbody2D rb);
    }

    private void Start()
    {
        canKnockback = true;
    }

    private void Update()
    {
        if (!healthScript.CanTakeDamage && canKnockback)
        {
            canKnockback = false;
            StartCoroutine(InitiateKnockback());
        }
    }

    IEnumerator InitiateKnockback()
    {
        movementScript.SetToggleMove(false);
        Vector2 _direction = Vector2.zero;

        switch (movementScript.FacingRight)
        {
            case true:
                _direction.x = -1;
                break;
            case false:
                _direction.x = 1;
                break;
        }
        _direction.x *= movementScript.KnockbackMultiplier;
        Vector2 _velocity = _direction * knockbackVelocity;
        movementScript.SetRigidbodyVelocity(_velocity);
        //movementScript.SetRigidbodyGravity(knockBackGravity);

        yield return new WaitForSeconds(knockbackDuration);
        movementScript.SetRigidbodyVelocity(Vector3.zero);
        //movementScript.SetRigidbodyGravity(1f);
        movementScript.SetToggleMove(true);
        canKnockback = true;
    }
}
