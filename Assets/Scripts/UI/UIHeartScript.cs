using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHeartScript : MonoBehaviour
{
    [SerializeField] PlayerHealthScript playerHealthScript;
    [SerializeField] GameObject[] heartArray;
    private int currentHealth;
    private int index;

    public void RenderHeart()
    {
        currentHealth = playerHealthScript.PlayerHealth;
        index = 1;
        Animator _heartAnimator;
        foreach (GameObject heart in heartArray)
        {
            heart.TryGetComponent(out _heartAnimator);

            if (index <= currentHealth)
            {
                _heartAnimator.SetBool("Damaged", false);
                //heart.SetActive(true);
            }
            else if (index > currentHealth) 
            {
                _heartAnimator.SetBool("Damaged", true);
                //heart.SetActive(false);
            }
            index++;
        }
    }
}
