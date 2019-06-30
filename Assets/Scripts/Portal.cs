using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {

    LayerMask oldLayer;
    
    private void Start()
    {
        oldLayer = gameObject.layer;
        gameObject.layer = 8;//GameObject.//ApplicationController.playerController.blockingLayer;
    }

    private void FixedUpdate()
    {
        if(ApplicationController.playerController.GetCrystalsNum() >= ApplicationController.playerController.numOfCrysrtalsToExit ) gameObject.layer = oldLayer;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if ((collision.transform.position - transform.position).sqrMagnitude <= float.Epsilon)
            Destroy(collision.gameObject);
    }
}
