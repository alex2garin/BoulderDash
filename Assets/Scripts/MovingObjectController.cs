//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class MovingObjectController : MonoBehaviour {

//    public float sideMoveTime = 0.2f;
//    public bool canRoll = false;
//    public float rotationSpeed = 20f;
//    public bool canKill = true;

//    public bool IsMoving { get { return isMoving; } }
//    private bool isMoving;
//    private float inverseMoveTime;
//    private Rigidbody2D rb2D;
//    //private CircleCollider2D cc2D;
//    private Collider2D cc2D;
//    private SpriteRenderer childSprite;
//    //private Rotator rotor;
//    private float angularSpeed;
//    private BombController bomb;


//    public enum RotationSide { left, right, norotation };

//    // Use this for initialization
//    void Start () {
//        rb2D = GetComponent<Rigidbody2D>();
//        cc2D = GetComponent<Collider2D>();
//        isMoving = false;
//        inverseMoveTime = 1f / sideMoveTime;
//        //rotor = GetComponentInChildren<Rotator>();
//        if (canRoll) childSprite = GetComponentInChildren<SpriteRenderer>();
//        angularSpeed = rotationSpeed / sideMoveTime;
//        bomb = gameObject.GetComponent<BombController>();
//    }

//	void FixedUpdate()
//    {
//  //      if (canRoll) childSprite.transform.Rotate(new Vector3(0f, 0f, rotationSpeed) * Time.deltaTime);
//        if (!isMoving)
//        {

//            Collider2D down = Physics2D.OverlapBox(transform.position + new Vector3(0f, -1f, 0f),new Vector2(.9f,.9f),0f);
//            if (down == null)
//            {

//                isMoving = true;
//                StartCoroutine(Move(transform.position + new Vector3(0f, -1f, 0f),true));
//                return;
//            }

//            MovingObjectController movingObject = down.gameObject.GetComponent<MovingObjectController>();
//            if (movingObject != null  && movingObject.IsMoving == true)
//            {
//                return;
//            }

//            if (movingObject != null)
//            {
//                Collider2D left = Physics2D.OverlapBox(transform.position + new Vector3(-1f, 0f, 0f), new Vector2(.9f, .9f), 0f);
//                if (left == null)
//                {
//                    Collider2D leftDown = Physics2D.OverlapBox(transform.position + new Vector3(-1f, -1f, 0f), new Vector2(.9f, .9f), 0f);
//                    if (leftDown == null)
//                    {

//                        Collider2D upleft = Physics2D.OverlapBox(transform.position + new Vector3(-1f, 1f, 0f), new Vector2(.9f, .9f), 0f);
//                        if (upleft == null || upleft.gameObject.GetComponent<MovingObjectController>() == null)
//                        {
//                            if (!movingObject.WillYouMove())
//                            {
//                                isMoving = true;
//                                StartCoroutine(Move(transform.position + new Vector3(-1f, 0f, 0f)));
//                                return;
//                            }
//                        }
//                    }
//                }


//                Collider2D right = Physics2D.OverlapBox(transform.position + new Vector3(1f, 0f, 0f), new Vector2(.9f, .9f), 0f);
//                if (right == null)
//                {
//                    Collider2D rightDown = Physics2D.OverlapBox(transform.position + new Vector3(1f, -1f, 0f), new Vector2(.9f, .9f), 0f);
//                    if (rightDown == null)
//                    {
//                        Collider2D upright = Physics2D.OverlapBox(transform.position + new Vector3(1f, 1f, 0f), new Vector2(.9f, .9f), 0f);
//                        if (upright == null || upright.gameObject.GetComponent<MovingObjectController>() == null)
//                        {

//                            if (!movingObject.WillYouMove())
//                            {
//                                isMoving = true;
//                                StartCoroutine(Move(transform.position + new Vector3(1f, 0f, 0f)));
//                                return;
//                            }
//                        }
//                    }
//                }
//            }
//        }
//    }


//    private static RotationSide GetRotationSide(Vector3 direction)
//    {
//        if (direction.x < 0) return RotationSide.left;
//        else if (direction.x > 0) return RotationSide.right;
//        else return RotationSide.norotation;

//    }

//    private float ConvertAngle(float angle)
//    {
//        if (angle < -180) return angle + 180f;
//        else if (angle > 180) return angle - 180f;
//        else return angle;
//    }


//    private IEnumerator Rotate(RotationSide side)
//    {

//        float angle = 0;
//        int sign = 0;
//            //RotationSide side = GetRotationSide(end - transform.position);
//            if (side == RotationSide.left) sign = 1;
//            else if (side == RotationSide.right) sign = -1;
//        if (!canRoll || sign == 0) yield break;

//        float endAngle = childSprite.transform.rotation.eulerAngles.z + sign * rotationSpeed;

//        while (angle < rotationSpeed)
//        {
//            //Debug.Log(angle);
//            angle += angularSpeed * Time.deltaTime;
//            if( angle >= rotationSpeed) childSprite.transform.rotation = Quaternion.Euler(0f, 0f, endAngle);
//            else childSprite.transform.Rotate(new Vector3(0f, 0f, sign * angularSpeed) * Time.deltaTime);
//            yield return null;
//        }

//        //Debug.Log(angle);
//    }

//    private IEnumerator Move(Vector3 end, bool falling = false)
//    {
//        /*
//        int sign = 0;
//        if (canRoll)
//        {*/
//         //   RotationSide side = GetRotationSide(end - transform.position);
//        StartCoroutine(Rotate(GetRotationSide(end - transform.position)));
//         /*   if (side == RotationSide.left) sign = 1;
//            else if (side == RotationSide.right) sign = -1;
//        }*/

//        float sqrRemainingDistance = (end - transform.position).sqrMagnitude;

//        //While that distance is greater than a very small amount (Epsilon, almost zero):
//        while (sqrRemainingDistance > float.Epsilon && end!=transform.position)
//        {

//            cc2D.offset = (end - transform.position) / 2;

//            if (falling)
//            {
//                Collider2D down = Physics2D.OverlapBox(transform.position + new Vector3(0f, -1f, 0f), new Vector2(.9f, .9f), 0f);
//                if (down == null) end = end + new Vector3(0f, -1f, 0f);
//            }

//            //Find a new position proportionally closer to the end, based on the moveTime
//            Vector3 newPostion = Vector3.MoveTowards(transform.position, end, inverseMoveTime * Time.deltaTime);

//            //Call MovePosition on attached Rigidbody2D and move it to the calculated position.
//            rb2D.MovePosition(newPostion);

//           // if(canRoll && sign!=0) childSprite.transform.Rotate(new Vector3(0f, 0f, sign * angularSpeed) * Time.deltaTime);

//            //Recalculate the remaining distance after moving.
//            sqrRemainingDistance = (transform.position - end).sqrMagnitude;

//            //Return and loop until sqrRemainingDistance is close enough to zero to end the function
//            yield return null;
//        }
//        //if (rotor != null) rotor.StopRotation();
//        cc2D.offset = new Vector2(0f, 0f);

//        if (falling)
//        {
//            Collider2D down = Physics2D.OverlapBox(transform.position + new Vector3(0f, -1f, 0f), new Vector2(.9f, .9f), 0f);
//            if(down!=null)
//            {
////                Debug.Log("down not null");
//                BombController downBomb = down.GetComponent<BombController>();
////                Debug.Log(downBomb);
//                if (downBomb != null && downBomb.isActive) downBomb.ReadyToExplode();
//                //Debug.Log(canKill);
//                if (down.gameObject.CompareTag("Player") && canKill) Destroy(down.gameObject);
//            }

//            if (bomb != null && bomb.isActive)
//            {
//                bomb.ReadyToExplode();
//                yield break;
//            }


//        }
//        isMoving = false;
//    }

//    public bool Push(Vector3 direction)
//    {
//        Vector3 fixedDirection = new Vector3(direction.x, 0f, 0f);
//        if (!isMoving)
//        {
//            if (Physics2D.OverlapBox(transform.position + new Vector3(0f, -1f, 0f), new Vector2(.9f, .9f), 0f) != null)
//            {
//                Collider2D side = Physics2D.OverlapBox(transform.position + fixedDirection, new Vector2(.9f, .9f), 0f);
//                if (side == null)
//                {
//                    isMoving = true;
//                    StartCoroutine(Move(transform.position + fixedDirection));
//                    return true;
//                }
//                return false;
//            }
//        }
//        return false;
//    }

//    public bool WillYouMove()
//    {
//        if (isMoving) return true;
//        Collider2D down = Physics2D.OverlapBox(transform.position + new Vector3(0f, -1f, 0f), new Vector2(.9f, .9f), 0f);
//        if (down == null) return true;
//        MovingObjectController MOCDown = down.gameObject.GetComponent<MovingObjectController>();
//        if (MOCDown == null) return false;
//        if (MOCDown.WillYouMove()) return true;

//        Collider2D left = Physics2D.OverlapBox(transform.position + new Vector3(-1f, 0f, 0f), new Vector2(.9f, .9f), 0f);
//        if (left == null)
//        {
//            Collider2D leftDown = Physics2D.OverlapBox(transform.position + new Vector3(-1f, -1f, 0f), new Vector2(.9f, .9f), 0f);
//            if (leftDown == null)
//            {

//                Collider2D leftUp = Physics2D.OverlapBox(transform.position + new Vector3(-1f, 1f, 0f), new Vector2(.9f, .9f), 0f);
//                if (leftUp == null || leftUp.gameObject.GetComponent<MovingObjectController>() == null) return true;
//            }
//        }

//        Collider2D right = Physics2D.OverlapBox(transform.position + new Vector3(1f, 0f, 0f), new Vector2(.9f, .9f), 0f);
//        if (right == null)
//        {
//            Collider2D rightDown = Physics2D.OverlapBox(transform.position + new Vector3(1f, -1f, 0f), new Vector2(.9f, .9f), 0f);
//            if (rightDown == null)
//            {

//                Collider2D rightUp = Physics2D.OverlapBox(transform.position + new Vector3(1f, 1f, 0f), new Vector2(.9f, .9f), 0f);
//                if (rightUp == null || rightUp.gameObject.GetComponent<MovingObjectController>() == null) return true;
//            }
//        }

//        return false;
//    }
//}


/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObjectController : MonoBehaviour
{

    public float sideMoveTime = 0.2f;
    public bool canRoll = false;
    public float rotationSpeed = 20f;
    public bool canKill = true;

    public bool IsMoving { get { return isMoving; } }
    private bool isMoving;
    private float inverseMoveTime;
    private Rigidbody2D rb2D;

    private Collider2D cc2D;
    private SpriteRenderer childSprite;

    private float angularSpeed;
    private BombController bomb;

    private Vector3 destination;
    private bool isFalling;


    private Vector3 lastposition;

    public enum RotationSide { left, right, norotation };

    // Use this for initialization
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        cc2D = GetComponent<Collider2D>();
        isMoving = false;
        inverseMoveTime = 1f / sideMoveTime;

        if (canRoll) childSprite = GetComponentInChildren<SpriteRenderer>();
        angularSpeed = rotationSpeed / sideMoveTime;
        bomb = gameObject.GetComponent<BombController>();
        //StartCoroutine(Checker());
    }

    void FixedUpdate2()
    {

        //if (!isMoving)
        //{

        //    Collider2D down = Physics2D.OverlapBox(transform.position + new Vector3(0f, -1f, 0f), new Vector2(.9f, .9f), 0f);
        //    if (down == null)
        //    {

        //        isMoving = true;
        //        StartCoroutine(Move(transform.position + new Vector3(0f, -1f, 0f), true));
        //        return;
        //    }

        //    MovingObjectController movingObject = down.gameObject.GetComponent<MovingObjectController>();
        //    if (movingObject != null && movingObject.IsMoving == true)
        //    {
        //        return;
        //    }

        //    if (movingObject != null)
        //    {
        //        Collider2D left = Physics2D.OverlapBox(transform.position + new Vector3(-1f, 0f, 0f), new Vector2(.9f, .9f), 0f);
        //        if (left == null)
        //        {
        //            Collider2D leftDown = Physics2D.OverlapBox(transform.position + new Vector3(-1f, -1f, 0f), new Vector2(.9f, .9f), 0f);
        //            if (leftDown == null)
        //            {

        //                Collider2D upleft = Physics2D.OverlapBox(transform.position + new Vector3(-1f, 1f, 0f), new Vector2(.9f, .9f), 0f);
        //                if (upleft == null || upleft.gameObject.GetComponent<MovingObjectController>() == null)
        //                {
        //                    if (!movingObject.WillYouMove())
        //                    {
        //                        isMoving = true;
        //                        StartCoroutine(Move(transform.position + new Vector3(-1f, 0f, 0f)));
        //                        return;
        //                    }
        //                }
        //            }
        //        }


        //        Collider2D right = Physics2D.OverlapBox(transform.position + new Vector3(1f, 0f, 0f), new Vector2(.9f, .9f), 0f);
        //        if (right == null)
        //        {
        //            Collider2D rightDown = Physics2D.OverlapBox(transform.position + new Vector3(1f, -1f, 0f), new Vector2(.9f, .9f), 0f);
        //            if (rightDown == null)
        //            {
        //                Collider2D upright = Physics2D.OverlapBox(transform.position + new Vector3(1f, 1f, 0f), new Vector2(.9f, .9f), 0f);
        //                if (upright == null || upright.gameObject.GetComponent<MovingObjectController>() == null)
        //                {

        //                    if (!movingObject.WillYouMove())
        //                    {
        //                        isMoving = true;
        //                        StartCoroutine(Move(transform.position + new Vector3(1f, 0f, 0f)));
        //                        return;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}
    }

    private Vector3 GetDestination(out bool isFalling)
    {
        isFalling = false;
        Collider2D down = Physics2D.OverlapBox(transform.position + new Vector3(0f, -1f, 0f), new Vector2(.9f, .9f), 0f);
       
        if (down == null)
        {

            //   isMoving = true;
            //   StartCoroutine(Move(transform.position + new Vector3(0f, -1f, 0f), true));
            isFalling = true;
            return new Vector3(0f, -1f, 0f);
        }

        MovingObjectController movingObject = down.gameObject.GetComponent<MovingObjectController>();
        if (movingObject != null && movingObject.IsMoving == true)
        {
            return Vector3.zero;
        }

        if (movingObject != null)
        {
            Collider2D left = Physics2D.OverlapBox(transform.position + new Vector3(-1f, 0f, 0f), new Vector2(.9f, .9f), 0f);
            if (left == null)
            {
                Collider2D leftDown = Physics2D.OverlapBox(transform.position + new Vector3(-1f, -1f, 0f), new Vector2(.9f, .9f), 0f);
                if (leftDown == null)
                {

                    Collider2D upleft = Physics2D.OverlapBox(transform.position + new Vector3(-1f, 1f, 0f), new Vector2(.9f, .9f), 0f);
                    if (upleft == null || upleft.gameObject.GetComponent<MovingObjectController>() == null)
                    {
                        if (!movingObject.WillYouMove())
                        {
                            //     isMoving = true;
                            //     StartCoroutine(Move(transform.position + new Vector3(-1f, 0f, 0f)));
                            return new Vector3(-1f, 0f, 0f);
                        }
                    }
                }
            }


            Collider2D right = Physics2D.OverlapBox(transform.position + new Vector3(1f, 0f, 0f), new Vector2(.9f, .9f), 0f);
            if (right == null)
            {
                Collider2D rightDown = Physics2D.OverlapBox(transform.position + new Vector3(1f, -1f, 0f), new Vector2(.9f, .9f), 0f);
                if (rightDown == null)
                {
                    Collider2D upright = Physics2D.OverlapBox(transform.position + new Vector3(1f, 1f, 0f), new Vector2(.9f, .9f), 0f);
                    if (upright == null || upright.gameObject.GetComponent<MovingObjectController>() == null)
                    {

                        if (!movingObject.WillYouMove())
                        {
                            //    isMoving = true;
                            //   StartCoroutine(Move(transform.position + new Vector3(1f, 0f, 0f)));
                            return new Vector3(1f, 0f, 0f);
                        }
                    }
                }
            }
        }
        return Vector3.zero;
    }

    private IEnumerator Checker()
    {
        while(true)
        {
            if(IsMoving && (lastposition-transform.position).sqrMagnitude <=float.Epsilon)
            {
                Debug.Log(Time.time);
                Debug.Log("position" + transform.position.ToString());
                Debug.Log("destination " + destination);
                Debug.Log("is position == destination in logic? " + ((destination - transform.position).sqrMagnitude <= float.Epsilon).ToString());
                Debug.Log("isMoving flag" + isMoving.ToString());
                Debug.Log("object" + gameObject.ToString());
                Debug.Log("Collider offset" + cc2D.offset.ToString());

            }

            lastposition = transform.position;

            yield return null;
        }

    }

    private void FixedUpdate()
    {
        if (IsMoving && ((lastposition - transform.position).sqrMagnitude <= float.Epsilon || lastposition == transform.position))
        {
         //   Debug.Log(Time.time);
            Debug.Log("position " + transform.position.ToString());
            Debug.Log("destination " + destination);
            Debug.Log("is position == destination in math logic? " + ((destination - transform.position).sqrMagnitude <= float.Epsilon).ToString());
            Debug.Log("is position == destination in eq logic? " + (destination == transform.position).ToString());
            Debug.Log("isMoving flag " + isMoving.ToString());
            Debug.Log("object " + gameObject.ToString());
            Debug.Log("Collider offset " + cc2D.offset.ToString());
            Debug.Log("Collider under " + Physics2D.OverlapBox(transform.position + new Vector3(0f, -1f), new Vector2(0.9f, .9f), 0f));
            //int i = 0;
            //var j = i / 0;
        }
        lastposition = transform.position;

        if (isMoving)
        {
            Move2(isFalling);

        }
        else
        {
            // bool isFalling;
            destination = transform.position + GetDestination(out isFalling);

            if (destination == transform.position) return;

            isMoving = true;
            StartCoroutine(Rotate(GetRotationSide(destination - transform.position)));
            Move2(isFalling);
            //FixedUpdate();
        }
    }

    private static RotationSide GetRotationSide(Vector3 direction)
    {
        if (direction.x < 0) return RotationSide.left;
        else if (direction.x > 0) return RotationSide.right;
        else return RotationSide.norotation;

    }

    private float ConvertAngle(float angle)
    {
        if (angle < -180) return angle + 180f;
        else if (angle > 180) return angle - 180f;
        else return angle;
    }


    private IEnumerator Rotate(RotationSide side)
    {

        float angle = 0;
        int sign = 0;
        //RotationSide side = GetRotationSide(end - transform.position);
        if (side == RotationSide.left) sign = 1;
        else if (side == RotationSide.right) sign = -1;
        if (!canRoll || sign == 0) yield break;

        float endAngle = childSprite.transform.rotation.eulerAngles.z + sign * rotationSpeed;

        while (angle < rotationSpeed)
        {
            //Debug.Log(angle);
            angle += angularSpeed * Time.deltaTime;
            if (angle >= rotationSpeed) childSprite.transform.rotation = Quaternion.Euler(0f, 0f, endAngle);
            else childSprite.transform.Rotate(new Vector3(0f, 0f, sign * angularSpeed) * Time.deltaTime);
            yield return null;
        }

        //Debug.Log(angle);
    }

    private void Move2(bool falling = false)
    {
        //var end = destination;
        float sqrRemainingDistance = (destination - transform.position).sqrMagnitude;
       
        if (sqrRemainingDistance > float.Epsilon && destination != transform.position)
        {
            Vector3 newPostion = Vector3.MoveTowards(transform.position, destination, inverseMoveTime * Time.deltaTime);
            if (falling && ((newPostion - destination).sqrMagnitude <= float.Epsilon || destination == transform.position))
            {
                cc2D.offset = (destination - transform.position) / 2;
                Collider2D down = Physics2D.OverlapBox(transform.position + new Vector3(0f, -1f, 0f), new Vector2(.9f, .9f), 0f);

             //   Debug.Log(down);
                if (down == null)
                {
                    destination = destination + new Vector3(0f, -1f, 0f);
                    newPostion = Vector3.MoveTowards(transform.position, destination, inverseMoveTime * Time.deltaTime);
                }
            }
            cc2D.offset = (destination - transform.position) / 2;
            rb2D.MovePosition(newPostion);
            return;
        }
        cc2D.offset = new Vector2(0f, 0f);
        rb2D.MovePosition(destination);

        if (falling && ((destination - transform.position).sqrMagnitude <= float.Epsilon || destination == transform.position))
        {
            Collider2D down = Physics2D.OverlapBox(transform.position + new Vector3(0f, -1f, 0f), new Vector2(.9f, .9f), 0f);
            if (down != null)
            {
                BombController downBomb = down.GetComponent<BombController>();
                if (downBomb != null && downBomb.isActive) downBomb.ReadyToExplode();
                if (down.gameObject.CompareTag("Player") && canKill) Destroy(down.gameObject);
            }

            if (down != null && bomb != null && bomb.isActive)
            {
                bomb.ReadyToExplode();
                return;
            }


        }
        isMoving = false;
    }

    private IEnumerator Move6(Vector3 end, bool falling = false)
    {
        /*
        int sign = 0;
        if (canRoll)
        {*/
        //   RotationSide side = GetRotationSide(end - transform.position);
        StartCoroutine(Rotate(GetRotationSide(end - transform.position)));
        /*   if (side == RotationSide.left) sign = 1;
           else if (side == RotationSide.right) sign = -1;
       }*/

        float sqrRemainingDistance = (end - transform.position).sqrMagnitude;

        //While that distance is greater than a very small amount (Epsilon, almost zero):
        while (sqrRemainingDistance > float.Epsilon && end != transform.position)
        {

            cc2D.offset = (end - transform.position) / 2;

            if (falling)
            {
                Collider2D down = Physics2D.OverlapBox(transform.position + new Vector3(0f, -1f, 0f), new Vector2(.9f, .9f), 0f);
                if (down == null) end = end + new Vector3(0f, -1f, 0f);
            }

            //Find a new position proportionally closer to the end, based on the moveTime
            Vector3 newPostion = Vector3.MoveTowards(transform.position, end, inverseMoveTime * Time.deltaTime);

            //Call MovePosition on attached Rigidbody2D and move it to the calculated position.
            rb2D.MovePosition(newPostion);

            // if(canRoll && sign!=0) childSprite.transform.Rotate(new Vector3(0f, 0f, sign * angularSpeed) * Time.deltaTime);

            //Recalculate the remaining distance after moving.
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;

            //Return and loop until sqrRemainingDistance is close enough to zero to end the function
            yield return null;
        }
        //if (rotor != null) rotor.StopRotation();
        cc2D.offset = new Vector2(0f, 0f);

        if (falling)
        {
            Collider2D down = Physics2D.OverlapBox(transform.position + new Vector3(0f, -1f, 0f), new Vector2(.9f, .9f), 0f);
            if (down != null)
            {
                BombController downBomb = down.GetComponent<BombController>();
                if (downBomb != null && downBomb.isActive) downBomb.ReadyToExplode();
                if (down.gameObject.CompareTag("Player") && canKill) Destroy(down.gameObject);
            }

            if (bomb != null && bomb.isActive)
            {
                bomb.ReadyToExplode();
                yield break;
            }


        }
        isMoving = false;
    }

    public bool Push(Vector3 direction)
    {
        Vector3 fixedDirection = new Vector3(direction.x, 0f, 0f);
        if (!isMoving)
        {
            if (Physics2D.OverlapBox(transform.position + new Vector3(0f, -1f, 0f), new Vector2(.9f, .9f), 0f) != null)
            {
                Collider2D side = Physics2D.OverlapBox(transform.position + fixedDirection, new Vector2(.9f, .9f), 0f);
                if (side == null)
                {
                    isMoving = true;
                    destination = transform.position + fixedDirection;
                    StartCoroutine(Rotate(GetRotationSide(destination - transform.position)));
                    Move2();
                    //   StartCoroutine(Move(transform.position + fixedDirection));
                    return true;
                }
                return false;
            }
        }
        return false;
    }

    public bool WillYouMove()
    {
        if (isMoving) return true;
        Collider2D down = Physics2D.OverlapBox(transform.position + new Vector3(0f, -1f, 0f), new Vector2(.9f, .9f), 0f);
        if (down == null) return true;
        MovingObjectController MOCDown = down.gameObject.GetComponent<MovingObjectController>();
        if (MOCDown == null) return false;
        if (MOCDown.WillYouMove()) return true;

        Collider2D left = Physics2D.OverlapBox(transform.position + new Vector3(-1f, 0f, 0f), new Vector2(.9f, .9f), 0f);
        if (left == null)
        {
            Collider2D leftDown = Physics2D.OverlapBox(transform.position + new Vector3(-1f, -1f, 0f), new Vector2(.9f, .9f), 0f);
            if (leftDown == null)
            {

                Collider2D leftUp = Physics2D.OverlapBox(transform.position + new Vector3(-1f, 1f, 0f), new Vector2(.9f, .9f), 0f);
                if (leftUp == null || leftUp.gameObject.GetComponent<MovingObjectController>() == null) return true;
            }
        }

        Collider2D right = Physics2D.OverlapBox(transform.position + new Vector3(1f, 0f, 0f), new Vector2(.9f, .9f), 0f);
        if (right == null)
        {
            Collider2D rightDown = Physics2D.OverlapBox(transform.position + new Vector3(1f, -1f, 0f), new Vector2(.9f, .9f), 0f);
            if (rightDown == null)
            {

                Collider2D rightUp = Physics2D.OverlapBox(transform.position + new Vector3(1f, 1f, 0f), new Vector2(.9f, .9f), 0f);
                if (rightUp == null || rightUp.gameObject.GetComponent<MovingObjectController>() == null) return true;
            }
        }

        return false;
    }
}
