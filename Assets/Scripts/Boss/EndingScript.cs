using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingScript : MonoBehaviour
{
    [SerializeField] InteractableButtonScript interactableButtonScript;
    private bool pressed;

    private void Awake()
    {
        TryGetComponent(out  interactableButtonScript);
        pressed = false;
    }

    private void Update()
    {
        if (interactableButtonScript.Interacted && !pressed)
        {
            pressed = true;
            LevelManager.Instance.FadeToBlackLoadScene("CutsceneEnd");
        }
    }
}
