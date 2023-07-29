using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using Color = UnityEngine.Color;

public class EnemyKnockback : MonoBehaviour
{
    private MovementScript movementScript;
    [SerializeField] float knockbackForce = 10f;
    [SerializeField] float knockbackDuration = 0.2f;
    
    [SerializeField] SpriteRenderer spriteRenderer;
    private Color defaultColor;

    private void Awake()
    {
        TryGetComponent(out movementScript);
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        defaultColor = spriteRenderer.color;
    }

    public IEnumerator KnockbackForEnemy(Vector3 direction)
    {
        spriteRenderer.color = Color.red;

        movementScript.SetToggleMove(false);
        Vector3 _forceDirection = direction * knockbackForce;
        movementScript.AddForce(_forceDirection);
        yield return new WaitForSeconds(knockbackDuration);
        movementScript.SetToggleMove(true);

        spriteRenderer.color = defaultColor;
    }
}
