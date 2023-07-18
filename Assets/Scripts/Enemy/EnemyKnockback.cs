using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class EnemyKnockback : MonoBehaviour
{
    private MovementScript movementScript;
    [SerializeField] float knockbackForce = 10f;
    [SerializeField] float knockbackDuration = 0.2f;

    private void Awake()
    {
        TryGetComponent(out movementScript);
    }

    public IEnumerator KnockbackForEnemy(Vector3 direction)
    {
        movementScript.SetToggleMove(false);
        Vector3 _forceDirection = direction * knockbackForce;
        movementScript.AddForce(_forceDirection);
        yield return new WaitForSeconds(knockbackDuration);
        movementScript.SetToggleMove(true);
    }
}
