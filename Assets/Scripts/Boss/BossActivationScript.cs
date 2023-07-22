using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActivationScript : MonoBehaviour
{
    [SerializeField] bool canReinteract = true;
    private bool canInteract;

    [SerializeField] float setDeactiveDelay = 5f;
    private Animator animator;

    //wait for boss object to finished

    private void Awake()
    {
        TryGetComponent(out animator);
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

                Debug.Log("Interacted");
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
        StartCoroutine(SetDeactivate(setDeactiveDelay));
    }

    IEnumerator SetDeactivate(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
