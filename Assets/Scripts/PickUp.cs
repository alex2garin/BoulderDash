using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private int _value = 0;
    private SpriteRenderer sr;
    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && TryPickUp(collision.transform.position))
        {
            if (CompareTag("Mineral")) collision.gameObject.GetComponent<PlayerController>().PickUpMinerals(_value);
            else if (CompareTag("Crystal")) collision.gameObject.GetComponent<PlayerController>().PickUpCrystals();
            else if (CompareTag("BombPickUp")) collision.gameObject.GetComponent<PlayerController>().PickUpBomb();
            else if (CompareTag("Ballon")) collision.gameObject.GetComponent<PlayerController>().PickUpOxygen();

            Destroy(gameObject);

        }
    }
    //public void SetValue(int value)
    //{
    //    _value = value;
    //}
    //public int GetValue()
    //{
    //    return _value;
    //}
    public bool TryPickUp(Vector3 player)
    {
        
        if (ApplicationController.gravity.x == 0) //means up and down
        {
            
            if((player.y<= sr.transform.position.y && sr.transform.position.y <= transform.position.y)||
                (transform.position.y <= sr.transform.position.y && sr.transform.position.y <= player.y))
            return true;
        }
        else
        {
            if ((player.x <= sr.transform.position.x && sr.transform.position.x <= transform.position.x) ||
                (transform.position.x <= sr.transform.position.x && sr.transform.position.x <= player.x))
                return true;
        }
        return false;
    }
}