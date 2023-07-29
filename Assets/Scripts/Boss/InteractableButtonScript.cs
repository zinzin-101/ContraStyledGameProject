using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableButtonScript : MonoBehaviour
{
    [SerializeField] bool canReinteract = true;
    private bool canInteract;

    private bool interacted;
    public bool Interacted => interacted;

    private void Awake()
    {
        canInteract = true;
        interacted = false;
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
                    interacted = true;
                }
                else
                {
                    StartCoroutine(ReInteractCoolDown());
                }
                //Debug.Log("Interacted");
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

    IEnumerator ReInteractCoolDown()
    {
        interacted = true;
        yield return new WaitForSeconds(0.1f);
        interacted = false;
    }
}
