using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    [SerializeField] Transform spawnTransform;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        animator.SetBool("Active", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerHealthScript playerHealthScript) && GameManager.CurrentCheckpointPos != spawnTransform.position)
        {
            GameManager.CurrentCheckpointPos = spawnTransform.position;
            animator.SetTrigger("Start");
            animator.SetBool("Active", true);

            SoundManager.PlaySound(SoundManager.PlayerGetHP, false);
        }
    }

    private void Update()
    {
        if (GameManager.CurrentCheckpointPos != spawnTransform.position)
        {
            animator.SetBool("Active", false);
        }
    }
}
