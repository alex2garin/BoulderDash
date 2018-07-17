//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public abstract class Enemy : MonoBehaviour {

//    public float moveTime = 2f;

//    protected bool isMoving = false;
//    protected float inverseMoveTime;
//    protected Vector3 end;
//    protected SpriteRenderer childSprite;

//    private Vector3 destVect;


//    private void Start()
//    {
//        inverseMoveTime = 1f / moveTime;

//        childSprite = GetComponentInChildren<SpriteRenderer>();
//    }

//    protected abstract Vector3 GetDestination();


//    private void FixedUpdate()
//    {

//        if (isMoving)
//        {

//            float sqrRemainingDistance = (destVect / 2 - childSprite.transform.localPosition).sqrMagnitude;
//            if (sqrRemainingDistance > float.Epsilon && destVect / 2 != childSprite.transform.localPosition)
//            {
//                Vector3 newPostion = Vector3.MoveTowards(childSprite.transform.localPosition, destVect / 2, inverseMoveTime * Time.deltaTime);
//                childSprite.transform.localPosition = newPostion;
//                return;
//            }
//            childSprite.transform.localPosition = Vector3.zero;
//            transform.position = end;
//            isMoving = false;
//        }
//        else
//        {
//            destVect = GetDestination();
//            if (destVect == Vector3.zero) return;

//            end = transform.position + destVect;

//            isMoving = true;
//            transform.position = destVect / 2 + transform.position;
//            childSprite.transform.localPosition = -destVect / 2;

//            FixedUpdate();

//        }
//    }
//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        if (collision.CompareTag("Player")) Destroy(collision.gameObject);
//    }
//}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MovingObject
{

    //public float moveTime = 2f;

    //protected bool isMoving = false;
    //protected float inverseMoveTime;
    //protected Vector3 end;
    //protected SpriteRenderer childSprite;

    //private Vector3 destVect;


    //private void Start()
    //{
    //    inverseMoveTime = 1f / moveTime;

    //    childSprite = GetComponentInChildren<SpriteRenderer>();
    //}

//    protected abstract Vector3 GetDestination();


    //private void FixedUpdate()
    //{

    //    if (isMoving)
    //    {

    //        float sqrRemainingDistance = (destVect / 2 - childSprite.transform.localPosition).sqrMagnitude;
    //        if (sqrRemainingDistance > float.Epsilon && destVect / 2 != childSprite.transform.localPosition)
    //        {
    //            Vector3 newPostion = Vector3.MoveTowards(childSprite.transform.localPosition, destVect / 2, inverseMoveTime * Time.deltaTime);
    //            childSprite.transform.localPosition = newPostion;
    //            return;
    //        }
    //        childSprite.transform.localPosition = Vector3.zero;
    //        transform.position = end;
    //        isMoving = false;
    //    }
    //    else
    //    {
    //        destVect = GetDestination();
    //        if (destVect == Vector3.zero) return;

    //        end = transform.position + destVect;

    //        isMoving = true;
    //        transform.position = destVect / 2 + transform.position;
    //        childSprite.transform.localPosition = -destVect / 2;

    //        FixedUpdate();

    //    }
    //}
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player")) Destroy(collision.gameObject);
    //}
}
