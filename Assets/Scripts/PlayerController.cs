using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float moveTime = .1f;
    public LayerMask blockingLayer;
    public LayerMask pickUpLayer;
    public float secondsForOxygenBallon = 0.1f;


    private Rigidbody2D rb2D;
    private BoxCollider2D bc2D;
    private float inverseMoveTime;
    private bool isMoving;

    private int bombs = 0;
    private int crystals = 0;
    private int minerals = 0;
    private int oxygen = 0;

    public int Bombs
    {
        get { return bombs; }
        set
        {
            bombs = Bombs;
            bombText.text = "Bombs: " + bombs;
        }
    }

    public int Crystals
    {
        get { return crystals; }
        set
        {
            crystals = Crystals;
            crystalText.text = "Oxygen: " + crystals;
        }
    }

    public int Minerals
    {
        get { return minerals; }
        set
        {
            minerals = Minerals;
            mineralText.text = "Oxygen: " + minerals;
        }
    }

    public int Oxygen
    {
        get { return oxygen; }
        set
        {
            oxygen = Oxygen;
            oxygenText.text = "Oxygen: " + oxygen;
        }
    }


    private Vector3 newDirection;

    private Text crystalText;
    private Text bombText;
    private Text mineralText;
    private Text oxygenText;

    // Use this for initialization
    void Start () {

        inverseMoveTime = 1f / moveTime;
        rb2D = GetComponent<Rigidbody2D>();
        bc2D = GetComponent<BoxCollider2D>();
        isMoving = false;
        crystalText = GameObject.Find("Crystals").GetComponent<Text>();
        mineralText = GameObject.Find("Minerals").GetComponent<Text>();
        bombText = GameObject.Find("Bombs").GetComponent<Text>();
        oxygenText = GameObject.Find("Oxygen").GetComponent<Text>();

        StartCoroutine(OxygenFlow());
    }

    private bool switcher = false;
    // Update is called once per frame
    void FixedUpdate() {

       
        

        if (!isMoving)
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");

            bool control = Input.GetKey(KeyCode.LeftControl);
            if (switcher != control)
            {
                Debug.Log(control);
                switcher = control;
            }
            Vector2 direction;

            if (horizontalInput != 0) direction = new Vector2(horizontalInput, 0f);
            else if (verticalInput != 0) direction = new Vector2(0f, verticalInput);
            else return;



            Vector2 end = transform.position;
            end += direction;

            Collider2D directionObject = Physics2D.OverlapBox(end, new Vector2(.9f, .9f), 0);

            if(directionObject==null)
            {
                Collider2D directionUp = Physics2D.OverlapBox(end + new Vector2(0f,1f), new Vector2(.9f, .9f), 0);
                if(directionUp!=null)
                {
                    MovingObjectController moverUp = directionUp.gameObject.GetComponent<MovingObjectController>();
                    if (moverUp != null && moverUp.IsMoving) return;
                }
            }
            
            if (directionObject != null && 1 << directionObject.gameObject.layer == blockingLayer.value)
            {
             
                MovingObjectController MOCStone = directionObject.gameObject.GetComponent<MovingObjectController>();
               if (MOCStone == null) return;

                    if (!MOCStone.Push(direction)) return;
                    else if (control) return;
            }

            if (directionObject != null && 1 << directionObject.gameObject.layer == pickUpLayer.value)
            {
                if (directionObject.gameObject.CompareTag("Ballon")) Oxygen++;
                else if (directionObject.gameObject.CompareTag("Bomb")) Bombs++;
                else if (directionObject.gameObject.CompareTag("Crystal")) Crystals++;
                else if (directionObject.gameObject.CompareTag("Mineral")) Minerals++;

                Destroy(directionObject.gameObject);
                if (control) return;
            }

            if (directionObject != null && directionObject.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                Destroy(directionObject.gameObject);
                if (control) return;
            }
            if(!control) StartCoroutine(Move(end));
        }
        else
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");

            Vector2 direction;

            if (horizontalInput != 0) direction = new Vector2(horizontalInput, 0f);
            else if (verticalInput != 0) direction = new Vector2(0f, verticalInput);
            else direction = Vector2.zero;

            newDirection = direction;
        }
    }

    private IEnumerator Move(Vector3 end)
    {
        isMoving = true;
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;
        Vector3 currentDirection = (end - transform.position);
        //Debug.Log(currentDirection);
        //While that distance is greater than a very small amount (Epsilon, almost zero):
        while (sqrRemainingDistance > float.Epsilon && end != transform.position)
        {
            //Find a new position proportionally closer to the end, based on the moveTime
            Vector3 newPostion = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime);
           
            if (newPostion == end && currentDirection == newDirection)
            {
                Collider2D directionObject = Physics2D.OverlapBox(end + newDirection, new Vector2(.9f, .9f), 0);

                if (directionObject == null)
                {
                    Collider2D directionUp = Physics2D.OverlapBox(end + newDirection + new Vector3(0f, 1f, 0f), new Vector2(.9f, .9f), 0);
                    if (directionUp != null)
                    {
                        MovingObjectController moverUp = directionUp.gameObject.GetComponent<MovingObjectController>();
                        if (moverUp == null || !moverUp.IsMoving)
                        {
                            end += newDirection;
                            newPostion = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime);
                        }
                    }
                    else
                    {
                        end += newDirection;
                        newPostion = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime);
                    }
                        
                }

                
                
            }

            bc2D.offset = (end - transform.position) / 2;

            //Call MovePosition on attached Rigidbody2D and move it to the calculated position.
            rb2D.MovePosition(newPostion);

            //Recalculate the remaining distance after moving.
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;

            //Return and loop until sqrRemainingDistance is close enough to zero to end the function
            yield return null;
        }



        bc2D.offset = new Vector2(0f, 0f);
        isMoving = false;
    }
    
/*
    public void PickUpBomb()
    {
        bombs++;
        bombText.text = "Bombs: " + bombs;
    }

    public void PickUpMinerals()
    {
        minerals++;
        mineralText.text = "Minerals: " + minerals;
    }

    public void PickUpOxygen()
    {
        oxygen++;
        oxygenText.text = "Oxygen: " + oxygen;
    }

    public void PickUpCrystals()
    {
        crystals++;
        crystalText.text = "Crystals: " + crystals;
    }
    */
    private IEnumerator OxygenFlow()
    {
        while(true)
        {
            yield return new WaitForSeconds(secondsForOxygenBallon);
            Oxygen--;
        }
    }


}

