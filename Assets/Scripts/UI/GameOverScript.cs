using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    [SerializeField] GameObject gameOverPanel;

    private UIHeartScript heartScript;

    private void Awake()
    {
        heartScript = GetComponent<UIHeartScript>();
    }

    private void Update()
    {
        if (!heartScript.IsPlayerAlive)
        {
            gameOverPanel.SetActive(true);
        }
    }

    public void RestartButton()
    {
        GameManager.RestartCurrentScene();
    }
}
