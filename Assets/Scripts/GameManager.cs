using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;

    [SerializeField] GameObject playerObject;
    public GameObject PlayerObject => playerObject;

    [SerializeField] GameObject cameraObject;
    public GameObject CameraObject => cameraObject;

    public static Vector3 CurrentCheckpointPos;
    [SerializeField] Vector3 defaultCheckpointPos;

    private string currentSceneName;

    public static bool BossActivated;
    public static bool IsBossDeath;
    private bool canStartBossSound;
    private bool canStartBossEndingSound;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        if (playerObject == null)
        {
            playerObject = GameObject.Find("Player");
        }

        BossActivated = false;
        canStartBossSound = true;

        IsBossDeath = false;
        canStartBossEndingSound = true;
    }

    private void Start()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
        BGMManager();

        CurrentCheckpointPos = defaultCheckpointPos;
    }

    private void Update()
    {
        if (BossActivated && canStartBossSound)
        {
            canStartBossSound = false;
            StopSceneBGM();
            SoundManager.BossBattle.Play();
        }

        if (IsBossDeath && canStartBossEndingSound)
        {
            canStartBossEndingSound = false;
            SoundManager.BossBattle.Stop();
            SoundManager.BossEnding.Play();
        }
    }

    public static void RestartCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    public void BGMManager()
    {
        if (BossActivated || IsBossDeath)
        {
            return;
        }

        switch (currentSceneName)
        {
            case "Tutorial":
                SoundManager.Lobby.Play();
                break;
            
            case "FinalizedMap":
                SoundManager.InGame.Play(); 
                break;
        }
    }

    public void StopSceneBGM()
    {
        switch (currentSceneName)
        {
            case "Tutorial":
                SoundManager.Lobby.Stop();
                break;

            case "FinalizedMap":
                SoundManager.InGame.Stop();
                break;
        }
    }
}
