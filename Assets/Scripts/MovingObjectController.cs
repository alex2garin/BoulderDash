////////using System.Collections;
////////using System.Collections.Generic;
////////using UnityEngine;

////////public class MovingObjectController : MonoBehaviour
////////{
////////    public bool superFlag = false;

////////    public float sideMoveTime = 0.2f;
////////    public bool canRoll = false;
////////    public float rotationSpeed = 20f;
////////    public bool canKill = true;

////////    public bool IsMoving { get { return isMoving; } }
////////    public Vector3 DestinationVector { get { return destVector; } }

////////    public enum RotationSide { left, right, norotation };

////////    private bool isMoving;
////////    private float inverseMoveTime;

////////    private SpriteRenderer childSprite;

////////    private float angularSpeed;
////////    private BombController bomb;

////////    private Vector3 destination;
////////    private Vector3 destVector;
////////    private bool isFalling;


////////    private Vector3 lastposition;


////////    // Use this for initialization
////////    void Start()
////////    {
////////        isMoving = false;
////////        inverseMoveTime = 1f / sideMoveTime;

////////        childSprite = GetComponentInChildren<SpriteRenderer>();
////////        angularSpeed = rotationSpeed / sideMoveTime;
////////        bomb = gameObject.GetComponent<BombController>();
////////    }


////////    private Vector3 GetDestination(out bool isFalling)
////////    {
////////        isFalling = false;
////////        Collider2D down = Physics2D.OverlapBox(transform.position + ApplicationController.gravity, new Vector2(.9f, .9f), 0f);

////////        if (down == null)
////////        {
////////            isFalling = true;
////////            return ApplicationController.gravity;
////////        }

////////        MovingObjectController movingObject = down.gameObject.GetComponent<MovingObjectController>();
////////        if (movingObject != null && movingObject.IsMoving == true)
////////        {
////////            return Vector3.zero;
////////        }

////////        if (movingObject != null)
////////        {
////////            Collider2D left = Physics2D.OverlapBox(transform.position + ApplicationController.Left, new Vector2(.9f, .9f), 0f);
////////            if (left == null)
////////            {
////////                Collider2D leftDown = Physics2D.OverlapBox(transform.position + ApplicationController.DownLeft, new Vector2(.9f, .9f), 0f);
////////                if (leftDown == null)
////////                {

////////                    Collider2D upleft = Physics2D.OverlapBox(transform.position + ApplicationController.UpLeft, new Vector2(.9f, .9f), 0f);
////////                    if (upleft == null || upleft.gameObject.GetComponent<MovingObjectController>() == null)
////////                    {
////////                        if (!movingObject.WillYouMove())
////////                        {
////////                            return ApplicationController.Left;
////////                        }
////////                    }
////////                }
////////            }


////////            Collider2D right = Physics2D.OverlapBox(transform.position + ApplicationController.Right, new Vector2(.9f, .9f), 0f);
////////            if (right == null)
////////            {
////////                Collider2D rightDown = Physics2D.OverlapBox(transform.position + ApplicationController.DownRight, new Vector2(.9f, .9f), 0f);
////////                if (rightDown == null)
////////                {
////////                    Collider2D upright = Physics2D.OverlapBox(transform.position + ApplicationController.UpRight, new Vector2(.9f, .9f), 0f);
////////                    if (upright == null || upright.gameObject.GetComponent<MovingObjectController>() == null)
////////                    {

////////                        if (!movingObject.WillYouMove())
////////                        {
////////                            return ApplicationController.Right;
////////                        }
////////                    }
////////                }
////////            }
////////        }
////////        return Vector3.zero;
////////    }



////////    private void FixedUpdate()
////////    {

////////        if (isMoving)
////////        {
////////            Move2(isFalling);

////////        }
////////        else
////////        {

////////            var startPosition = transform.position;
////////            destVector = GetDestination(out isFalling);
////////            destination = transform.position + destVector;

////////            if (destination == transform.position) return;

////////            isMoving = true;

////////            transform.position = destVector / 2 + transform.position;
////////            childSprite.transform.localPosition = -destVector / 2;

////////            StartCoroutine(Rotate(GetRotationSide(destination - transform.position)));
////////            Move2(isFalling);
////////        }
////////    }

////////    private static RotationSide GetRotationSide(Vector3 direction)
////////    {
////////        if (direction.x < 0) return RotationSide.left;
////////        else if (direction.x > 0) return RotationSide.right;
////////        else return RotationSide.norotation;

////////    }

////////    //private float ConvertAngle(float angle)
////////    //{
////////    //    if (angle < -180) return angle + 180f;
////////    //    else if (angle > 180) return angle - 180f;
////////    //    else return angle;
////////    //}

////////    private IEnumerator Rotate(RotationSide side)
////////    {

////////        float angle = 0;
////////        int sign = 0;
////////        if (side == RotationSide.left) sign = 1;
////////        else if (side == RotationSide.right) sign = -1;
////////        if (!canRoll || sign == 0) yield break;

////////        float endAngle = childSprite.transform.rotation.eulerAngles.z + sign * rotationSpeed;

////////        while (angle < rotationSpeed)
////////        {
////////            angle += angularSpeed * Time.deltaTime;
////////            if (angle >= rotationSpeed) childSprite.transform.rotation = Quaternion.Euler(0f, 0f, endAngle);
////////            else childSprite.transform.Rotate(new Vector3(0f, 0f, sign * angularSpeed) * Time.deltaTime);
////////            yield return null;
////////        }

////////    }

////////    private void Move2(bool falling = false)
////////    {

////////        float sqrRemainingDistance = (destVector / 2 - childSprite.transform.localPosition).sqrMagnitude;

////////        if (sqrRemainingDistance > float.Epsilon && destVector / 2 != childSprite.transform.localPosition)
////////        {
////////            Vector3 newPostion = Vector3.MoveTowards(childSprite.transform.localPosition, destVector / 2, inverseMoveTime * Time.deltaTime);

////////            if (falling && ((newPostion - destVector / 2).sqrMagnitude <= float.Epsilon || destVector / 2 == newPostion))
////////            {

////////                Collider2D down = Physics2D.OverlapBox(transform.position + 3 * ApplicationController.gravity / 2, new Vector2(.9f, .9f), 0f);

////////                if (down == null)
////////                {

////////                    destination = destination + ApplicationController.gravity;
////////                    transform.position += ApplicationController.gravity;
////////                    newPostion = Vector3.MoveTowards(childSprite.transform.localPosition - ApplicationController.gravity, destVector / 2, inverseMoveTime * Time.deltaTime);
////////                }

////////            }

////////            childSprite.transform.localPosition = newPostion;
////////            return;
////////        }
////////        childSprite.transform.localPosition = Vector3.zero;
////////        //rb2D.MovePosition(destination);
////////        transform.position = destination;
////////        //if(superFlag)
////////        //{
////////        //    Debug.Log("here");
////////        //}
////////        if (falling)
////////        {
////////            Collider2D down = Physics2D.OverlapBox(transform.position + ApplicationController.gravity, new Vector2(.9f, .9f), 0f);
////////            if (down != null)
////////            {
////////                BombController downBomb = down.GetComponent<BombController>();
////////                if (downBomb != null && downBomb.isActive && down.gameObject.GetComponent<DestroyByExplosion>().CanExplode(transform.position)) downBomb.ReadyToExplode();
////////                if (down.gameObject.CompareTag("Player") && canKill && down.gameObject.GetComponent<DestroyByExplosion>().CanExplode(transform.position)) Destroy(down.gameObject);
////////            }

////////            if (down != null && bomb != null && bomb.isActive)
////////            {
////////                bomb.ReadyToExplode();
////////                return;
////////            }


////////        }
////////        isMoving = false;

////////    }

////////    public bool Push(Vector3 direction)
////////    {
////////        Vector3 fixedDirection;
////////        if (ApplicationController.gravity.x == 0) fixedDirection = new Vector3(direction.x, 0f, 0f);
////////        else if (ApplicationController.gravity.y == 0) fixedDirection = new Vector3(0f, direction.y, 0f);
////////        else fixedDirection = Vector3.zero;

////////        if (!isMoving)
////////        {
////////            if (Physics2D.OverlapBox(transform.position + ApplicationController.gravity, new Vector2(.9f, .9f), 0f) != null)
////////            {
////////                Collider2D side = Physics2D.OverlapBox(transform.position + fixedDirection, new Vector2(.9f, .9f), 0f);
////////                if (side == null)
////////                {
////////                    isMoving = true;
////////                    destVector = fixedDirection;
////////                    destination = transform.position + fixedDirection;

////////                    transform.position = destVector / 2 + transform.position;
////////                    childSprite.transform.localPosition = -destVector / 2;

////////                    StartCoroutine(Rotate(GetRotationSide(destination - transform.position)));
////////                    Move2();
////////                    return true;

////////                }
////////                return false;
////////            }
////////        }
////////        return false;
////////    }

////////    public bool WillYouMove()
////////    {
////////        if (isMoving) return true;
////////        Collider2D down = Physics2D.OverlapBox(transform.position + ApplicationController.gravity, new Vector2(.9f, .9f), 0f);
////////        if (down == null) return true;
////////        MovingObjectController MOCDown = down.gameObject.GetComponent<MovingObjectController>();
////////        if (MOCDown == null) return false;
////////        if (MOCDown.WillYouMove()) return true;

////////        Collider2D left = Physics2D.OverlapBox(transform.position + ApplicationController.Left, new Vector2(.9f, .9f), 0f);
////////        if (left == null)
////////        {
////////            Collider2D leftDown = Physics2D.OverlapBox(transform.position + ApplicationController.DownLeft, new Vector2(.9f, .9f), 0f);
////////            if (leftDown == null)
////////            {

////////                Collider2D leftUp = Physics2D.OverlapBox(transform.position + ApplicationController.UpLeft, new Vector2(.9f, .9f), 0f);
////////                if (leftUp == null || leftUp.gameObject.GetComponent<MovingObjectController>() == null) return true;
////////            }
////////        }

////////        Collider2D right = Physics2D.OverlapBox(transform.position + ApplicationController.Right, new Vector2(.9f, .9f), 0f);
////////        if (right == null)
////////        {
////////            Collider2D rightDown = Physics2D.OverlapBox(transform.position + ApplicationController.DownRight, new Vector2(.9f, .9f), 0f);
////////            if (rightDown == null)
////////            {

////////                Collider2D rightUp = Physics2D.OverlapBox(transform.position + ApplicationController.UpRight, new Vector2(.9f, .9f), 0f);
////////                if (rightUp == null || rightUp.gameObject.GetComponent<MovingObjectController>() == null) return true;
////////            }
////////        }

////////        return false;
////////    }


////////    public bool CanBeUsed(Vector3 userPosition)
////////    {
////////        if (childSprite == null) return true;

////////        if (((transform.position.x <= childSprite.transform.position.x && childSprite.transform.position.x <= userPosition.x) ||
////////               (userPosition.x <= childSprite.transform.position.x && childSprite.transform.position.x <= transform.position.x)) &&
////////            ((transform.position.y <= childSprite.transform.position.y && childSprite.transform.position.y <= userPosition.y) ||
////////               (userPosition.y <= childSprite.transform.position.y && childSprite.transform.position.y <= transform.position.y)))
////////            return true;
////////        return false;
////////    }
////////}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObjectController : MovingObject
{
    //  public float sideMoveTime = 0.2f;
    public bool canRoll = false;
    public float rotationSpeed = 20f;
    public bool canKill = true;

    private bool isFalling;
    private float angularSpeed;
    private Bomb bomb;

    private new void Start()
    {
        base.Start();
        angularSpeed = rotationSpeed / moveTime;
        bomb = gameObject.GetComponent<Bomb>();
        //if (bomb != null) Debug.Log(bomb);
    }
    private static int GetRotationSide(Vector3 direction)
    {
        if (direction.x * ApplicationController.gravity.x + direction.y * ApplicationController.gravity.y != 0) return 0;
        return (int)(-ApplicationController.gravity.x * direction.y + ApplicationController.gravity.y * direction.x);
        //if (direction.x < 0) return RotationSide.left;
        //else if (direction.x > 0) return RotationSide.right;
        //else return RotationSide.norotation;

    }
    private IEnumerator Rotate(int sign)//RotationSide side)
    {

        if (!canRoll || sign == 0) yield break;

        float angle = 0;
        float endAngle = childSprite.transform.rotation.eulerAngles.z + sign * rotationSpeed;
        while (angle < rotationSpeed)
        {
            angle += angularSpeed * Time.deltaTime;
            if (angle >= rotationSpeed) childSprite.transform.rotation = Quaternion.Euler(0f, 0f, endAngle);
            else childSprite.transform.Rotate(new Vector3(0f, 0f, sign * angularSpeed) * Time.deltaTime);
            yield return null;
        }

        //float angle = 0;
        //int sign = 0;
        //if (side == RotationSide.left) sign = 1;
        //else if (side == RotationSide.right) sign = -1;
        //if (!canRoll || sign == 0) yield break;

        //float endAngle = childSprite.transform.rotation.eulerAngles.z + sign * rotationSpeed;

        //while (angle < rotationSpeed)
        //{
        //    angle += angularSpeed * Time.deltaTime;
        //    if (angle >= rotationSpeed) childSprite.transform.rotation = Quaternion.Euler(0f, 0f, endAngle);
        //    else childSprite.transform.Rotate(new Vector3(0f, 0f, sign * angularSpeed) * Time.deltaTime);
        //    yield return null;
        //}

    }

    protected override Vector3 GetDestination()
    {
        isFalling = false;
        Collider2D down = Physics2D.OverlapBox(transform.position + ApplicationController.gravity, new Vector2(.9f, .9f), 0f);

        if (down == null)
        {
            isFalling = true;
            return ApplicationController.gravity;
        }

        MovingObjectController movingObject = down.gameObject.GetComponent<MovingObjectController>();
        if (movingObject != null && movingObject.IsMoving == true)
        {
            return Vector3.zero;
        }

        if (movingObject != null)
        {
            Collider2D left = Physics2D.OverlapBox(transform.position + ApplicationController.Left, new Vector2(.9f, .9f), 0f);
            if (left == null)
            {
                Collider2D leftDown = Physics2D.OverlapBox(transform.position + ApplicationController.DownLeft, new Vector2(.9f, .9f), 0f);
                if (leftDown == null)
                {

                    Collider2D upleft = Physics2D.OverlapBox(transform.position + ApplicationController.UpLeft, new Vector2(.9f, .9f), 0f);
                    if (upleft == null || upleft.gameObject.GetComponent<MovingObjectController>() == null)
                    {
                        if (!movingObject.WillYouMove())
                        {
                            return ApplicationController.Left;
                        }
                    }
                }
            }


            Collider2D right = Physics2D.OverlapBox(transform.position + ApplicationController.Right, new Vector2(.9f, .9f), 0f);
            if (right == null)
            {
                Collider2D rightDown = Physics2D.OverlapBox(transform.position + ApplicationController.DownRight, new Vector2(.9f, .9f), 0f);
                if (rightDown == null)
                {
                    Collider2D upright = Physics2D.OverlapBox(transform.position + ApplicationController.UpRight, new Vector2(.9f, .9f), 0f);
                    if (upright == null || upright.gameObject.GetComponent<MovingObjectController>() == null)
                    {

                        if (!movingObject.WillYouMove())
                        {
                            return ApplicationController.Right;
                        }
                    }
                }
            }
        }
        return Vector3.zero;
    }
    protected override Vector3 GetDestinationInMovement(Vector3 calculatedNewPosition)
    {
        if (isFalling && ((calculatedNewPosition - destVect / 2).sqrMagnitude <= float.Epsilon || destVect / 2 == calculatedNewPosition))
        {

            Collider2D down = Physics2D.OverlapBox(transform.position + 3 * ApplicationController.gravity / 2, new Vector2(.9f, .9f), 0f);

            if (down == null)
            {

                end = end + ApplicationController.gravity;
                transform.position += ApplicationController.gravity;
                return Vector3.MoveTowards(childSprite.transform.localPosition - ApplicationController.gravity, destVect / 2, inverseMoveTime * Time.deltaTime);
            }

        }
        return calculatedNewPosition;
    }
    protected override void ActionAfterMovement()
    {
        if (isFalling)
        {
            Collider2D down = Physics2D.OverlapBox(transform.position + ApplicationController.gravity, new Vector2(.9f, .9f), 0f);
            if (down != null)
            {
                Bomb downBomb = down.GetComponent<Bomb>();
                if (downBomb != null && downBomb.isActive && down.gameObject.GetComponent<DestroyByExplosion>().CanExplode(transform.position)) downBomb.ShouldExplode();
                if (down.gameObject.CompareTag("Player") && canKill && down.gameObject.GetComponent<DestroyByExplosion>().CanExplode(transform.position)) Destroy(down.gameObject);
            }

            if (down != null && bomb != null && bomb.isActive)
            {
                //Debug.Log("bom");
                bomb.ShouldExplode();
                return;
            }


        }
    }
    protected override void RotationStart()
    {
        StartCoroutine(Rotate(GetRotationSide(destVect)));
        //StartCoroutine(Rotate(GetRotationSide(end - transform.position)));
    }
    protected override void BeforeFixedUpdate()
    {
    }

    public bool Push(Vector3 direction)
    {
        Vector3 fixedDirection;
        if (ApplicationController.gravity.x == 0) fixedDirection = new Vector3(direction.x, 0f, 0f);
        else if (ApplicationController.gravity.y == 0) fixedDirection = new Vector3(0f, direction.y, 0f);
        else fixedDirection = Vector3.zero;

        if (!IsMoving)
        {
            if (Physics2D.OverlapBox(transform.position + ApplicationController.gravity, new Vector2(.9f, .9f), 0f) != null)
            {
                Collider2D side = Physics2D.OverlapBox(transform.position + fixedDirection, new Vector2(.9f, .9f), 0f);
                if (side == null)
                {
                    IsMoving = true;
                    destVect = fixedDirection;
                    end = transform.position + fixedDirection;

                    transform.position = destVect / 2 + transform.position;
                    childSprite.transform.localPosition = -destVect / 2;

                    RotationStart();
                    FixedUpdate();
                    return true;

                }
                return false;
            }
        }
        return false;

    }

    public bool WillYouMove()
    {
        if (IsMoving) return true;
        Collider2D down = Physics2D.OverlapBox(transform.position + ApplicationController.gravity, new Vector2(.9f, .9f), 0f);
        if (down == null) return true;
        MovingObjectController MOCDown = down.gameObject.GetComponent<MovingObjectController>();
        if (MOCDown == null) return false;
        if (MOCDown.WillYouMove()) return true;

        Collider2D left = Physics2D.OverlapBox(transform.position + ApplicationController.Left, new Vector2(.9f, .9f), 0f);
        if (left == null)
        {
            Collider2D leftDown = Physics2D.OverlapBox(transform.position + ApplicationController.DownLeft, new Vector2(.9f, .9f), 0f);
            if (leftDown == null)
            {

                Collider2D leftUp = Physics2D.OverlapBox(transform.position + ApplicationController.UpLeft, new Vector2(.9f, .9f), 0f);
                if (leftUp == null || leftUp.gameObject.GetComponent<MovingObjectController>() == null) return true;
            }
        }

        Collider2D right = Physics2D.OverlapBox(transform.position + ApplicationController.Right, new Vector2(.9f, .9f), 0f);
        if (right == null)
        {
            Collider2D rightDown = Physics2D.OverlapBox(transform.position + ApplicationController.DownRight, new Vector2(.9f, .9f), 0f);
            if (rightDown == null)
            {

                Collider2D rightUp = Physics2D.OverlapBox(transform.position + ApplicationController.UpRight, new Vector2(.9f, .9f), 0f);
                if (rightUp == null || rightUp.gameObject.GetComponent<MovingObjectController>() == null) return true;
            }
        }

        return false;
    }


    public bool CanBeUsed(Vector3 userPosition)
    {
        if (childSprite == null) return true;

        if (((transform.position.x <= childSprite.transform.position.x && childSprite.transform.position.x <= userPosition.x) ||
               (userPosition.x <= childSprite.transform.position.x && childSprite.transform.position.x <= transform.position.x)) &&
            ((transform.position.y <= childSprite.transform.position.y && childSprite.transform.position.y <= userPosition.y) ||
               (userPosition.y <= childSprite.transform.position.y && childSprite.transform.position.y <= transform.position.y)))
            return true;
        return false;
    }
}
