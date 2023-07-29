using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPanelScript : MonoBehaviour
{
    private InteractableButtonScript interactScript;
    [SerializeField] GameObject panel;
    [SerializeField] CameraFollow _camera;

    private void Awake()
    {
        TryGetComponent(out interactScript);
        panel.SetActive(false);
        _camera.SetCameraLock(true);
    }

    private void Update()
    {
        if (interactScript.Interacted)
        {
            panel.SetActive(true);
            _camera.SetCameraLock(false);
        }
    }
}
