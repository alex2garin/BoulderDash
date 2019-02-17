using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    //private PlayerController player;
// private SpriteRenderer playerSR;
    private Transform target;
    // Use this for initialization
    void Start () {
        target = GameObject.FindWithTag("Player").GetComponentInChildren<SpriteRenderer>().transform;
        if (target != null)
            transform.position = target.position;
    }
	
	// Update is called once per frame
	void FixedUpdate()
    {
        //if (playerSR == null) playerSR = FindObjectOfType<PlayerController>().GetComponentInChildren<SpriteRenderer>();
        //else if(playerSR != null) transform.position = playerSR.transform.position + new Vector3(0f,0f,-10f);
        if (target != null) transform.position = target.position + new Vector3(0f, 0f, -10f);
    }
}
