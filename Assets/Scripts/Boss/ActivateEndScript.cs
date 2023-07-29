using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateEndScript : MonoBehaviour
{
    [SerializeField] GameObject endInteract;

    private void Start()
    {
        endInteract.SetActive(false);
    }

    private void Update()
    {
        if (GameManager.BossActivated)
        {
            endInteract.SetActive(true);
        }
    }
}
