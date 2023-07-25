using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnStartScript : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    void Start()
    {
        //SoundManager.Instance.PlaySound(audioSource, false);
    }
}
