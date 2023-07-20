using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractScript : MonoBehaviour
{
    [SerializeField] GameObject interactText;
    public void ActiveText(bool value)
    {
        interactText.SetActive(value);
    }
}
