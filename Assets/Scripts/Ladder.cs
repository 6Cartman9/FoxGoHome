using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    private enum LadderPart
    {
        Complete,
        Down,
        Top
    };

    [SerializeField] LadderPart part = LadderPart.Complete;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerController>())
        {
            PlayerController player = collision.GetComponent<PlayerController>();

            switch (part)
            {
                case LadderPart.Complete:
                    player.canClimb = true;
                    break;
                case LadderPart.Down:
                    player.isDownLadder = true;
                    break;
                case LadderPart.Top:
                    player.isTopLadder = true;
                    break;
                default:
                    break;
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            PlayerController player = collision.GetComponent<PlayerController>();

            switch (part)
            {
                case LadderPart.Complete:
                    player.canClimb = false;
                    player.ladder = this;
                    break;
                case LadderPart.Down:
                    player.isDownLadder = false;
                    break;
                case LadderPart.Top:
                    player.isTopLadder = false;
                    break;
                default:
                    break;
            }

        }
    }
}
