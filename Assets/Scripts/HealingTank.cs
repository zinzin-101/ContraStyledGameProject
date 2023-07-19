using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HealingTank : MonoBehaviour
{
    [SerializeField] Collider2D _healingTankCollider;
    [SerializeField] int _healAmount = 1;
    public int HealAmount => _healAmount;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        
        if (collider.gameObject.TryGetComponent(out PlayerHealthScript _playerHealthScript)) 
        {
            if (_playerHealthScript.PlayerAlive && _playerHealthScript.PlayerHealth < 10)
            {
                _playerHealthScript.Heal(_healAmount);
                Destroy(gameObject);
            }
        }
        
    }      
}

