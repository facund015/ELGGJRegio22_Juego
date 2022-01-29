using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectionZone : MonoBehaviour
{
    public bool hasPlayer = false;
    public bool playerHasArmor = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hasPlayer = true;
            CharacterController playerCC = collision.gameObject.GetComponent<CharacterController>();
            if (playerCC.isArmored)
            {
                playerHasArmor = true;
            }
            else
            {
                playerHasArmor = false;
            }
        }
        else return;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CharacterController playerCC = collision.gameObject.GetComponent<CharacterController>();
            if (playerCC.isArmored)
            {
                playerHasArmor = true;
            }
            else
            {
                playerHasArmor = false;
            }
        }
        else return;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hasPlayer = false;
            playerHasArmor = false;
        }
        else return;
    }

}
