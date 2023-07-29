using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField] GameObject pausePanel;

    private UIHeartScript heartScript;

    private void Awake()
    {
        TryGetComponent(out heartScript);
    }

    private void Start()
    {
        pausePanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePausePanel();
        }
    }

    void TogglePausePanel()
    {
        if (!heartScript.IsPlayerAlive)
        {
            return;
        }

        if (pausePanel.activeSelf)
        {
            Time.timeScale = 1f;
            pausePanel.SetActive(false);
        }
        else if (!pausePanel.activeSelf)
        {
            Time.timeScale = 0f;
            pausePanel.SetActive(true);
        }
    }
}
