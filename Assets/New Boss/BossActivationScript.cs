using System.Collections;
using UnityEngine;

public class BossActivationScript : MonoBehaviour
{
    [SerializeField] bool canReinteract = true;
    private bool canInteract;

    [SerializeField] float setDeactiveDelay = 5f;
    private Animator animator;
    public Transform bossSpawnPoint;
    public GameObject bossPrefab;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        canInteract = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerInteractScript playerInteractScript) && canInteract)
        {
            playerInteractScript.ActiveText(true);

            if (Input.GetKey(KeyCode.E))
            {
                if (!canReinteract)
                {
                    canInteract = false;
                    playerInteractScript.ActiveText(false);
                }

                Activation();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerInteractScript playerInteractScript))
        {
            playerInteractScript.ActiveText(false);
        }
    }

    public void Activation()
    {
        animator.SetTrigger("Activate");
        Instantiate(bossPrefab, bossSpawnPoint.position, bossSpawnPoint.rotation);
        GameManager.BossActivated = true;
        StartCoroutine(SetDeactivate(setDeactiveDelay));
    }

    IEnumerator SetDeactivate(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}