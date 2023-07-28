using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    private bool isClicked;

    private void Awake()
    {
        mainMenu.SetActive(true);
        isClicked = false;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && !isClicked)
        {
            isClicked = true;
            mainMenu.SetActive(false);
            LevelManager.Instance.LoadScene("Tutorial");
        }
    }
}
