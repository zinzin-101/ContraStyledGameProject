using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance;
    public static LevelManager Instance => instance;

    [SerializeField] GameObject loader;
    [SerializeField] UnityEngine.UI.Image progressBar;
    private float target;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    
    public async void LoadScene(string sceneName)
    {
        target = 0f;
        progressBar.fillAmount = 0f;

        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

        loader.SetActive(true);

        do
        {
            await Task.Delay(100);
            target = scene.progress;
        }
        while (scene.progress < 0.9f);

        await Task.Delay(1000);

        scene.allowSceneActivation = true;
        loader.SetActive(false);
    }

    private void Update()
    {
        progressBar.fillAmount = Mathf.MoveTowards(progressBar.fillAmount, target, 3 * Time.deltaTime);
    }
}
