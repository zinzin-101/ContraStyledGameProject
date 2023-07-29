using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTextScript : MonoBehaviour
{
    [SerializeField] GameObject text;

    private void Start()
    {
        text.SetActive(false);
    }

    private void Update()
    {
        if (GameManager.BossActivated)
        {
            text.SetActive(true);
        }
    }
}
