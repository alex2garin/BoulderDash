using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObjectController : MonoBehaviour {

    public float sideMoveTime = 0.2f;
    
    public bool IsMoving;
    private float inverseMoveTime;
    private Rigidbody2D rb2D;
	// Use this for initialization
	void Start () {
        rb2D = GetComponent<Rigidbody2D>();
        IsMoving = false;
        inverseMoveTime = 1f / sideMoveTime;
    }
	
	void Update ()
    {
        if (!IsMoving)
        {
            Collider2D down = Physics2D.OverlapBox(transform.position + new Vector3(0f, -1f, 0f),new Vector2(.9f,.9f),0f);
            if (down == null)
            {
                rb2D.gravityScale = 1f;
                IsMoving = true;
                return;
            }

            MovingObjectController movingObject = down.gameObject.GetComponent<MovingObjectController>();
            if (movingObject != null && movingObject.IsMoving == true) return;

            
            Collider2D left = Physics2D.OverlapBox(transform.position + new Vector3(-1f, 0f, 0f), new Vector2(.9f, .9f), 0f);
            if (left == null)
            {
                Collider2D leftDown = Physics2D.OverlapBox(transform.position + new Vector3(-1f, -1f, 0f), new Vector2(.9f, .9f), 0f);
                if (leftDown == null)
                {
                    IsMoving = true;
                    StartCoroutine(Move(transform.position + new Vector3(-1f, 0f, 0f)));
                    return;
                }
            }

            
            Collider2D right = Physics2D.OverlapBox(transform.position + new Vector3(1f, 0f, 0f), new Vector2(.9f, .9f), 0f);
            if (right == null)
            {
                Collider2D rightDown = Physics2D.OverlapBox(transform.position + new Vector3(1f, -1f, 0f), new Vector2(.9f, .9f), 0f);
                if (rightDown == null)
                {
                    IsMoving = true;
                    StartCoroutine(Move(transform.position + new Vector3(1f, 0f, 0f)));
                    return;
                }
            }
            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       // Debug.Log("This is collision");
       // Debug.Log(collision.gameObject);
        IsMoving = false;
        rb2D.gravityScale = 0;
        rb2D.velocity = new Vector2(0f, 0f);
      //  Debug.Log(transform.position.x);
      //  Debug.Log(transform.position.y);
        transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), 0f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
      //  Debug.Log("This is trigger");
        IsMoving = false;
        rb2D.gravityScale = 0;
        rb2D.velocity = new Vector2(0f, 0f);
     //   Debug.Log(transform.position.x);
     //   Debug.Log(transform.position.y);
        transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), 0f);
    }


    private IEnumerator Move(Vector3 end)
    {
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
        IsMoving = false;
    }

    public bool Push(Vector3 direction)
    {
        if (!IsMoving)
        {
            Collider2D side = Physics2D.OverlapBox(transform.position + direction, new Vector2(.9f, .9f), 0f);
            if (side == null)
            {
                IsMoving = true;
                StartCoroutine(Move(transform.position + direction));
                return true;
            }
            return false;
        }
        return false;
    }
}
