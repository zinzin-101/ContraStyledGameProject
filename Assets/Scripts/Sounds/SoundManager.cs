using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("BGM")]
    [SerializeField] AudioSource bossBattle;
    [SerializeField] AudioSource deathScene;
    [SerializeField] AudioSource bossEnding;
    [SerializeField] AudioSource inGame;
    [SerializeField] AudioSource lobby;

    public static AudioSource BossBattle;
    public static AudioSource DeathScene;
    public static AudioSource BossEnding;
    public static AudioSource InGame;
    public static AudioSource Lobby;

    [Header("Player")]
    [SerializeField] AudioSource playerGetHP;
    [SerializeField] AudioSource playerLoseHP1;
    [SerializeField] AudioSource playerLoseHP2;
    [SerializeField] AudioSource playerOverheatFinished;
    [SerializeField] AudioSource playerOverheating;
    [SerializeField] AudioSource playerOverheatStart;
    [SerializeField] AudioSource playerShoot;
    [SerializeField] AudioSource playerOneHeart;
    [SerializeField] AudioSource playerWalk;

    public static AudioSource PlayerGetHP;
    public static AudioSource PlayerLoseHP1;
    public static AudioSource PlayerLoseHP2;
    public static AudioSource PlayerOverheatFinished;
    public static AudioSource PlayerOverheating;
    public static AudioSource PlayerOverheatStart;
    public static AudioSource PlayerShoot;
    public static AudioSource PlayerOneHeart;
    public static AudioSource PlayerWalk;

    private void Awake()
    {
        //BGM
        BossBattle = bossBattle;
        DeathScene = deathScene;
        BossEnding = bossEnding;
        InGame = inGame;
        Lobby = lobby;

        //Player
        PlayerGetHP = playerGetHP;
        PlayerLoseHP1 = playerLoseHP1;
        PlayerLoseHP2 = playerLoseHP2;
        PlayerOverheatFinished = playerOverheatFinished;
        PlayerOverheating = playerOverheating;
        PlayerOverheatStart = playerOverheatStart;
        PlayerShoot = playerShoot;
        PlayerOneHeart = playerOneHeart;
        PlayerWalk = playerWalk;
    }

    public static void PlaySound(AudioSource audio, bool doLoop)
    {
        audio.loop = doLoop;
        audio.Play();
    }

    public static void StopSound(ref AudioSource audio)
    {
        audio.Stop();
    }
}
