using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {

    public float moveTime = 2f;

    protected bool isMoving = false;
    protected Collider2D cc2D;
    protected float inverseMoveTime;
    protected Rigidbody2D rb2D;
    protected Vector3 end;



    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        cc2D = GetComponent<Collider2D>();
        inverseMoveTime = 1f / moveTime;


    }

    protected abstract Vector3 GetDestination();
    

    private void FixedUpdate()
    {


        //if (isMoving)
        //{
        //    Move2(isFalling);

        //}
        //else
        //{

        //    var startPosition = transform.position;
        //    destVector = GetDestination(out isFalling);
        //    destination = transform.position + destVector;

        //    if (destination == transform.position) return;

        //    isMoving = true;

        //    transform.position = destVector / 2 + transform.position;
        //    childSprite.transform.localPosition = -destVector / 2;

        //    StartCoroutine(Rotate(GetRotationSide(destination - transform.position)));
        //    Move2(isFalling);
        //}


        if (isMoving)
        {
            float sqrRemainingDistance = (end - transform.position).sqrMagnitude;
            if (sqrRemainingDistance > float.Epsilon && end != transform.position)
            {
                cc2D.offset = (end - transform.position) / 2;
                Vector3 newPostion = Vector3.MoveTowards(transform.position, end, inverseMoveTime * Time.deltaTime);
                rb2D.MovePosition(newPostion);
                return;
            }
            cc2D.offset = new Vector2(0f, 0f);
            rb2D.MovePosition(end);
            isMoving = false;
        }
        else
        {
            end = transform.position + GetDestination();

            isMoving = true;
            FixedUpdate();
        }
    }
}
