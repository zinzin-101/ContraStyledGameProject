using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakablePlatform : MonoBehaviour
{
    [SerializeField] float destroyDelay = 2f;
    private float timer;
    private bool startBreak;

    [SerializeField] bool canRespawn;

    public bool StartBreak => startBreak;

    [SerializeField] GameObject platform;
    SpriteRenderer spriteRenderer;
    float _transparentAmount;
    float lerpTimer;

    private Animator animator;

    [SerializeField] float respawnDelay = 5f;

    [SerializeField] AudioClip breakSound;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        startBreak = false;
        platform.SetActive(true);
        timer = destroyDelay;
    }

    private void Update()
    {
        if (startBreak)
        {
            lerpTimer += Time.deltaTime / destroyDelay;
            _transparentAmount = Mathf.Lerp(1f, 0f, lerpTimer);
            Color _color = spriteRenderer.color;
            _color.a = _transparentAmount;
            spriteRenderer.color = _color;

            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                animator.SetTrigger("Reset");
                startBreak = false;
                _color.a = 1f;
                spriteRenderer.color = _color;
                if (canRespawn)
                {
                    StartCoroutine(RespawnPlatform());
                }
                platform.SetActive(false);
            }
        }
        else
        {
            lerpTimer = 0f;
            timer = destroyDelay;
        }
    }

    public void SetPlatformOn(bool value)
    {
        platform.SetActive (value);
    }

    public void SetStartBreak(bool value)
    {
        startBreak = value;
        if (breakSound != null)
        {
            AudioSource.PlayClipAtPoint(breakSound, transform.position);
        }
        animator.SetTrigger("StartBreak");
    }

    IEnumerator RespawnPlatform()
    {
        yield return new WaitForSeconds(respawnDelay);
        SetPlatformOn(true);
    }
}
