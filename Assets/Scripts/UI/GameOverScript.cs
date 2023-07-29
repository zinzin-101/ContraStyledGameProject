using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    [SerializeField] GameObject gameOverPanel;

    private UIHeartScript heartScript;
    private PlayerHealthScript playerHealthScript;

    private bool panelActive;

    private void Awake()
    {
        heartScript = GetComponent<UIHeartScript>();
    }

    private void Start()
    {
        gameOverPanel.SetActive(false);
        GameManager.Instance.PlayerObject.TryGetComponent(out playerHealthScript);
        panelActive = false;
    }

    private void Update()
    {
        panelActive = gameOverPanel.activeSelf;

        if (!heartScript.IsPlayerAlive && !panelActive)
        {
            gameOverPanel.SetActive(true);

            GameManager.Instance.StopSceneBGM();
            SoundManager.DeathScene.Play();
        }

        if (heartScript.IsPlayerAlive && panelActive)
        {
            gameOverPanel.SetActive(false);
        }
    }

    public void ResetToCheckpoint()
    {
        GameManager.Instance.PlayerObject.SetActive(true);
        playerHealthScript.RespawnPlayer(GameManager.CurrentCheckpointPos);

        Vector3 _newCameraPosition = GameManager.Instance.CameraObject.transform.position;
        _newCameraPosition.x = GameManager.Instance.PlayerObject.transform.position.x;
        _newCameraPosition.y = GameManager.Instance.PlayerObject.transform.position.y;
        GameManager.Instance.CameraObject.transform.position = _newCameraPosition;

        SoundManager.DeathScene.Stop();
        GameManager.Instance.BGMManager();
    }

    public void RestartButton()
    {
        GameManager.RestartCurrentScene();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
