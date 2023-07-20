using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableButtonScript : MonoBehaviour
{
    [SerializeField] bool canReinteract = true;
    private bool canInteract;

    private void Awake()
    {
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

                //activate boss
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
}
