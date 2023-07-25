using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHeartScript : MonoBehaviour
{
    [SerializeField] PlayerHealthScript playerHealthScript;
    [SerializeField] GameObject[] heartArray = new GameObject[10];
    private int currentHealth;
    private int index;

    private void Start()
    {
        RenderHeart();
    }

    public void RenderHeart()
    {
        currentHealth = playerHealthScript.PlayerHealth;
        index = 1;
        foreach (GameObject heart in heartArray)
        {
            heart.TryGetComponent(out Animator _heartAnimator);

            if (index <= currentHealth)
            {
                _heartAnimator.SetTrigger("Reset");
                //heart.SetActive(true);
            }
            else
            {
                _heartAnimator.SetTrigger("Start");
                //heart.SetActive(false);
            }
            index++;
        }
    }
}
