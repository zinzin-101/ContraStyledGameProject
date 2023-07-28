using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    [SerializeField] GameObject gameOverPanel;

    private UIHeartScript heartScript;
    private PlayerHealthScript playerHealthScript;

    private bool canTrigger;

    private void Awake()
    {
        heartScript = GetComponent<UIHeartScript>();
    }

    private void Start()
    {
        gameOverPanel.SetActive(false);
        GameManager.Instance.PlayerObject.TryGetComponent(out playerHealthScript);
        canTrigger = true;
    }

    private void Update()
    {
        if (!heartScript.IsPlayerAlive && canTrigger)
        {
            canTrigger = false;
            gameOverPanel.SetActive(true);
        }
    }

    public void ResetToCheckpoint()
    {
        GameManager.Instance.PlayerObject.SetActive(true);
        playerHealthScript.RespawnPlayer(GameManager.CurrentCheckpointPos);
        gameOverPanel.SetActive(false);

        canTrigger = true;

        Vector3 _newCameraPosition = GameManager.Instance.CameraObject.transform.position;
        _newCameraPosition.x = GameManager.Instance.PlayerObject.transform.position.x;
        _newCameraPosition.y = GameManager.Instance.PlayerObject.transform.position.y;
        GameManager.Instance.CameraObject.transform.position = _newCameraPosition;
    }

    public void RestartButton()
    {
        GameManager.RestartCurrentScene();
    }
}
