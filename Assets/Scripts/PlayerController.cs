﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveTime = .1f;
    public LayerMask blockingLayer;

    private Rigidbody2D rb2D;
    private float inverseMoveTime;
    private bool isMoving;


	// Use this for initialization
	void Start () {

        inverseMoveTime = 1f / moveTime;
        rb2D = GetComponent<Rigidbody2D>();
        isMoving = false;
	}
	
	// Update is called once per frame
	void Update () {
		
        if(!isMoving)
        {

            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");
            Vector2 direction;

            if (horizontalInput != 0) direction = new Vector2(horizontalInput, 0f);
            else if (verticalInput != 0) direction = new Vector2(0f, verticalInput);
            else return;




            Vector2 end = transform.position;
            end += direction;

            Collider2D directionObject = Physics2D.OverlapBox(end, new Vector2(.9f, .9f), 0);

            //if (directionObject != null && directionObject.gameObject.layer == 8)
            if (directionObject != null && 1 << directionObject.gameObject.layer == blockingLayer.value)
            {
                    return;
            }

            StartCoroutine(Move(end));


        }


	}

    private IEnumerator Move(Vector3 end)
    {
        isMoving = true;
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

        //While that distance is greater than a very small amount (Epsilon, almost zero):
        while (sqrRemainingDistance > float.Epsilon)
        {
            //Find a new position proportionally closer to the end, based on the moveTime
            Vector3 newPostion = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime);

            //Call MovePosition on attached Rigidbody2D and move it to the calculated position.
            rb2D.MovePosition(newPostion);

            //Recalculate the remaining distance after moving.
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;

            //Return and loop until sqrRemainingDistance is close enough to zero to end the function
            yield return null;
        }
        isMoving = false;
    }
}
