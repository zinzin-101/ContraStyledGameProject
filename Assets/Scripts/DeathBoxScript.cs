using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBoxScript : MonoBehaviour
{
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerHealthScript playerHealthScript))
        {
            playerHealthScript.TakeDamage(playerHealthScript.MaxPlayerHealth);
        }
    }
}
