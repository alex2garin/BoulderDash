using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (CompareTag("Mineral")) collision.gameObject.GetComponent<PlayerController>().PickUpMinerals();
            else if (CompareTag("Crystal")) collision.gameObject.GetComponent<PlayerController>().PickUpCrystals();
            else if (CompareTag("BombPickUp")) collision.gameObject.GetComponent<PlayerController>().PickUpBomb();
            else if (CompareTag("Ballon")) collision.gameObject.GetComponent<PlayerController>().PickUpOxygen();

            Destroy(gameObject);

        }
    }
}