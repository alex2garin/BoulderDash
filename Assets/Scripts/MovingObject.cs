using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingObject : MonoBehaviour {

    //public enum RotationSide { left, right, norotation };

    public float moveTime = 2f;

    public bool IsMoving = false;
    public Vector3 DestVect
    {
        get { return destVect; }
    }

    protected float inverseMoveTime;
    protected Vector3 end;
    protected SpriteRenderer childSprite;

    protected Vector3 destVect;
    protected bool isStopped = false;
    protected bool stop = false;

 //   private bool sflag = false;

    //public bool Stop
    //{
    //    set { stop = Stop;
    //        sflag = true;
    //  //      Debug.Log("bom");
    //    }
    //    get { return stop; }
    //}
    

    public bool IsStopped
    {
        get { return isStopped; }
    }

    public void Stop()
    {
        stop = true;
    }

    protected void Start()
    {
        inverseMoveTime = 1f / moveTime;

        childSprite = GetComponentInChildren<SpriteRenderer>();
    }

    protected abstract Vector3 GetDestination();
    protected abstract Vector3 GetDestinationInMovement(Vector3 calculatedNewPosition);
    protected abstract void ActionAfterMovement();
    protected abstract void RotationStart();
    protected abstract void BeforeFixedUpdate();

    protected void FixedUpdate()
    {
     //   if(sflag) Debug.Log(stop);

        BeforeFixedUpdate();
        //Debug.Log(stop);
        if (stop)
        {
            //    Debug.Log(IsMoving);
            if (!IsMoving)
            {
                isStopped = true;
                return;
            }
            else isStopped = false;
      //      Debug.Log("bom");
      //      Debug.Log(isStopped);
      //      return;
            //Debug.Log("it's stopped");
        }
        else isStopped = false;

        
        if (IsMoving)
        {

            float sqrRemainingDistance = (destVect / 2 - childSprite.transform.localPosition).sqrMagnitude;
            if (sqrRemainingDistance > float.Epsilon && destVect / 2 != childSprite.transform.localPosition)
            {
                Vector3 newPostion = Vector3.MoveTowards(childSprite.transform.localPosition, destVect / 2, inverseMoveTime * Time.deltaTime);

              //  Debug.Log(newPostion);
                newPostion = GetDestinationInMovement(newPostion);

                childSprite.transform.localPosition = newPostion;
                return;
            }
            childSprite.transform.localPosition = Vector3.zero;
            transform.position = end;

            ActionAfterMovement();

            IsMoving = false;
        }
        else
        {
            destVect = GetDestination();
            if (destVect == Vector3.zero) return;

            end = transform.position + destVect;

            IsMoving = true;
            transform.position = destVect / 2 + transform.position;
            childSprite.transform.localPosition = -destVect / 2;

            RotationStart();

            FixedUpdate();

        }
    }
}
