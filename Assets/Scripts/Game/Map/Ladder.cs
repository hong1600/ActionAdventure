using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D _coll)
    {
        PlayerMovement player = _coll.GetComponent<PlayerMovement>();

        if(player != null) 
        {
            player.SetLadder(true);
        }
    }

    private void OnTriggerExit2D(Collider2D _coll)
    {
        PlayerMovement player = _coll.GetComponent<PlayerMovement>();

        if (player != null)
        {
            player.SetLadder(false);
        }
    }
}
