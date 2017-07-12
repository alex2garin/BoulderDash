using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private PlayerController player;
    // Use this for initialization
    void Start () {
//        Debug.Log(player);

	}
	
	// Update is called once per frame
	void FixedUpdate()
    {
        if (player==null) player = FindObjectOfType<PlayerController>();
        else if(player!=null) transform.position = player.gameObject.transform.position + new Vector3(0f,0f,-10f);
    }
}
