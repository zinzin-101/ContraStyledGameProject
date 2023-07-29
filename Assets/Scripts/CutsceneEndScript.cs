using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneEndScript : MonoBehaviour
{
    [SerializeField] float delay = 5f;

    private IEnumerator Start()
    {
        LevelManager.Instance.FadeFromBlack();
        yield return new WaitForSecondsRealtime(delay);
        LevelManager.Instance.SetFadeActive(false);
        LevelManager.Instance.FadeToBlackLoadScene("MainMenu");
    }
}
