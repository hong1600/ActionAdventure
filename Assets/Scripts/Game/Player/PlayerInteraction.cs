using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    Vector3 interactionOffset { get; }

    void Interact();
}

public class PlayerInteraction : MonoBehaviour
{
    IInteractable curTarget;

    InteractionUI interactionUI;
    GameState gameState;
    DialogueUI dialogueUI;

    private void Start()
    {
        interactionUI = GameUI.instance.InteractionUI;
        gameState = GameManager.instance.GameState;
        dialogueUI = GameUI.instance.DialogueUI;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (curTarget != null)
            {
                if (gameState.curState == EGameState.DIALOGUE)
                {
                    if(GameUI.instance.DialogueUI.isTyping) 
                    {
                        dialogueUI.SkipTyping();
                    }
                    else
                    {
                        DialogueManager.instance.NextLine();
                    }

                    return;
                }

                if (gameState.curState != EGameState.PLAY) return;

                curTarget.Interact();
                interactionUI.Hide();
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

            interactionUI.Show(EInteractionText.INVESTIGATE, coll.transform, interactable.interactionOffset);
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
