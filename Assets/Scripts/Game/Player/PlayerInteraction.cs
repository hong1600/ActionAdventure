using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void Interact();
}

public class PlayerInteraction : MonoBehaviour
{
    IInteractable curTarget;

    InteractionUI interactionUI;

    private void Start()
    {
        interactionUI = GameUI.instance.InteractionUI;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (curTarget != null)
            {
                curTarget.Interact();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (interactionUI == null) return;

        IInteractable interactable = coll.GetComponent<IInteractable>();

        if (interactable != null)
        {
            curTarget = interactable;

            interactionUI.Show(EInteractionText.INVESTIGATE, coll.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        IInteractable interactable = coll.GetComponent<IInteractable>();

        if (interactable != null && interactable == curTarget)
        {
            curTarget = null;
            interactionUI.Hide();
        }
    }
}
