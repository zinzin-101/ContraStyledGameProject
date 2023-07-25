using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundScript : MonoBehaviour
{
    private PlayerProjectileSpawner playerProjectileSpawner;
    private PlayerHealthScript playerHealthScript;

    private bool playerOnOneHealth;
    private bool canPlay;

    private void Awake()
    {
        TryGetComponent(out playerProjectileSpawner);
        TryGetComponent(out playerHealthScript);

        playerOnOneHealth = false;
    }

    private void Update()
    {
        if (playerHealthScript.PlayerHealth <= 2)
        {
            playerOnOneHealth = true;
            canPlay = true;
        }
        else
        {
            playerOnOneHealth = false;
            canPlay = false;
        }

        if (canPlay && playerOnOneHealth)
        {
            canPlay = false;
            SoundManager.PlayerOneHeart.Play();
        }

        if (!playerOnOneHealth)
        {
            SoundManager.PlayerOneHeart.Stop();
        }
    }
}
