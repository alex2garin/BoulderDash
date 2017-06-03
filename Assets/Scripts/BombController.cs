using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        bool explode = false;
        Collider2D[] objects = Physics2D.OverlapBoxAll(transform.position, new Vector2(2f, 2f), 0f);
        foreach(var objectCollider2D in objects)
        {
            if (objectCollider2D.gameObject.CompareTag("Respawn"))
            {
                explode = true;
                break;
            }
        }
        if(explode)
        {
            Destroy(gameObject);
            foreach (var objectCollider2D in objects) Destroy(objectCollider2D.gameObject);
        }
	}
}
