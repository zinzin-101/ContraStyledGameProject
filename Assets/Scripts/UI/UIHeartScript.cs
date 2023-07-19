using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHeartScript : MonoBehaviour
{
    [SerializeField] PlayerHealthScript playerHealthScript;
    [SerializeField] GameObject[] heartArray = new GameObject[10];
    private int maxHealth, currentHealth;
    private int index, indexCount;

    private void Awake()
    {
        maxHealth = playerHealthScript.MaxPlayerHealth;
    }

    private void Start()
    {
        RenderHeart();
    }

    public void RenderHeart()
    {
        currentHealth = playerHealthScript.PlayerHealth;
        index = currentHealth;
        indexCount = 0;
        foreach (GameObject heart in heartArray)
        {
            if (indexCount < index)
            {
                heart.SetActive(true);
            }
            else
            {
                heart.SetActive(false);
            }
            indexCount++;
        }
    }
}
