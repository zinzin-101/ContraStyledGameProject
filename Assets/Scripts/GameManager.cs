using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
    }

    private void Start()
    {
        //placeholder bgm
        SoundManager.InGame.Play();
    }

    public static void RestartCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
