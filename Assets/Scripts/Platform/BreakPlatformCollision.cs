using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakPlatformCollision : MonoBehaviour
{
    private BreakablePlatform mainScript;

    private void Awake()
    {
        mainScript = GetComponentInParent<BreakablePlatform>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Rigidbody2D rb))
        {
            if (!mainScript.StartBreak && rb.velocity.y <= 0f)
            {
                mainScript.SetStartBreak(true);
            }
        }      
    }
}
