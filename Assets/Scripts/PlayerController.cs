using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public bool handleFlag = false;

    public float moveTime = .1f;
    public LayerMask blockingLayer;
    public LayerMask pickUpLayer;
    public int secondsForBallon = 60;
    public int startingSecondsOfOxygen = 60;
    public GameObject bombGO;


    private Rigidbody2D rb2D;
    private Collider2D cc2D;
    private float inverseMoveTime;
    private bool isMoving;

    private int bombs = 0;
    private int crystals = 0;
    private int minerals = 0;
    private int oxygen = 0;



    private Text crystalText;
    private Text bombText;
    private Text mineralText;
    private Text oxygenText;
    private Text crystalsToExitText;

    private GameObject plantedBomb;

    private Vector3 end;
    private Vector3 currDirection;
    private Vector3 newDirection;
    private Vector3 destVector;
    private int numOfCrysrtalsToExit;
    private SpriteRenderer childSprite;

    public void SetNumCrystalsToExit(int num)
    {
        numOfCrysrtalsToExit = num;
    }


    // Use this for initialization
    void Start()
    {

        oxygen = startingSecondsOfOxygen;

        inverseMoveTime = 1f / moveTime;
        rb2D = GetComponent<Rigidbody2D>();
        cc2D = GetComponent<Collider2D>();
        isMoving = false;
        crystalText = GameObject.Find("Crystals").GetComponent<Text>();
        mineralText = GameObject.Find("Minerals").GetComponent<Text>();
        bombText = GameObject.Find("Bombs").GetComponent<Text>();
        oxygenText = GameObject.Find("Oxygen").GetComponent<Text>();
        crystalsToExitText = GameObject.Find("CrystalsToExit").GetComponent<Text>();
        crystalsToExitText.text = "Crystals To Exit: " + numOfCrysrtalsToExit;

        childSprite = GetComponentInChildren<SpriteRenderer>();
        StartCoroutine(OxygenFlow());
    }

    private bool switcher = false;

    private void SetBomb()
    {
        if (bombs > 0 && plantedBomb == null)
        {

            plantedBomb = Instantiate(bombGO, transform.position, Quaternion.identity);
            plantedBomb.GetComponent<BombController>().Planted();

            PickUpBomb(true);
        }
    }

    private void debugger()
    {
        Collider2D upLeft = Physics2D.OverlapBox(transform.position + new Vector3(-1f, 1f), new Vector2(.9f, .9f), 0f);
        Collider2D up = Physics2D.OverlapBox(transform.position + new Vector3(0f, 1f), new Vector2(.9f, .9f), 0f);
        Collider2D upRight = Physics2D.OverlapBox(transform.position + new Vector3(1f, 1f), new Vector2(.9f, .9f), 0f);

        if (
            (upLeft == null || !upLeft.CompareTag("Stone")) &&
            (up == null || !up.CompareTag("Stone")) &&
            (upRight == null || !upRight.CompareTag("Stone"))
            ) return;


        if (upLeft != null && upLeft.CompareTag("Stone") && upLeft.GetComponent<MovingObjectController>().IsMoving)
        {
            Debug.Log("Player");
            Debug.Log(transform.position);
            Debug.Log(GetComponent<Collider2D>().offset);
            Debug.Log("left");
            Debug.Log(upLeft.transform.position);
            Debug.Log(upLeft.offset);
        }
        if (up != null && up.CompareTag("Stone") && up.gameObject.GetComponent<MovingObjectController>().IsMoving)
        {
            Debug.Log("Player");
            Debug.Log(transform.position);
            Debug.Log(GetComponent<Collider2D>().offset);
            Debug.Log("up");
            Debug.Log(up.transform.position);
            Debug.Log(up.offset);
        }
        if (upRight != null && upRight.CompareTag("Stone") && upRight.gameObject.GetComponent<MovingObjectController>().IsMoving)
        {
            Debug.Log("Player" + transform.position.ToString() + GetComponent<Collider2D>().offset.ToString());
            //   Debug.Log(transform.position);
            //   Debug.Log(GetComponent<Collider2D>().offset);
            Debug.Log("right");
            Debug.Log(upRight.transform.position);
            Debug.Log(upRight.offset);
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (plantedBomb != null)
        {
            if ((plantedBomb.transform.position - transform.position).sqrMagnitude >= 1)
            {
                plantedBomb.GetComponent<BombController>().Activate();
                plantedBomb = null;
            }
        }

        float horizontalInput = ApplicationController.inputCTRL.HorizontalMovement();
        //Input.GetAxisRaw("Horizontal");
        float verticalInput = ApplicationController.inputCTRL.VerticalMovement();
        //Input.GetAxisRaw("Vertical");

        if (horizontalInput != 0) newDirection = new Vector2(horizontalInput, 0f);
        else if (verticalInput != 0) newDirection = new Vector2(0f, verticalInput);
        else newDirection = Vector2.zero;

        if(handleFlag) newDirection = new Vector2(-1f, 0f);

        bool control = ApplicationController.inputCTRL.ActionWithoutMoving();
        //Input.GetKey(KeyCode.LeftControl);
        if (switcher != control) switcher = control;

        bool space = ApplicationController.inputCTRL.PlantBomb();
        //Input.GetKey(KeyCode.Space);
        //			if(Input.GetKey(KeyCode.LeftCommand)) 
        //				Debug.Log (1);
        //		if(Input.GetKey(KeyCode.LeftControl))
        //				Debug.Log (2);


        if (isMoving)
        {

            //float sqrRemainingDistance = (destVector / 2 - childSprite.transform.localPosition).sqrMagnitude;

            //if (sqrRemainingDistance > float.Epsilon && destVector / 2 != childSprite.transform.localPosition)
            //{
            //    Vector3 newPostion = Vector3.MoveTowards(childSprite.transform.localPosition, destVector / 2, inverseMoveTime * Time.deltaTime);

            //    if (falling && ((newPostion - destVector / 2).sqrMagnitude <= float.Epsilon || destVector / 2 == newPostion))
            //    {

            //        Collider2D down = Physics2D.OverlapBox(transform.position + 3 * ApplicationController.gravity / 2, new Vector2(.9f, .9f), 0f);

            //        if (down == null)
            //        {

            //            destination = destination + ApplicationController.gravity;
            //            rb2D.MovePosition(transform.position + ApplicationController.gravity);
            //            newPostion = Vector3.MoveTowards(childSprite.transform.localPosition - ApplicationController.gravity, destVector / 2, inverseMoveTime * Time.deltaTime);
            //        }

            //    }

            //    childSprite.transform.localPosition = newPostion;
            //    return;
            //}
            //childSprite.transform.localPosition = Vector3.zero;
            //rb2D.MovePosition(destination);

            //if (falling)
            //{
            //    Collider2D down = Physics2D.OverlapBox(transform.position + ApplicationController.gravity, new Vector2(.9f, .9f), 0f);
            //    if (down != null)
            //    {
            //        BombController downBomb = down.GetComponent<BombController>();
            //        if (downBomb != null && downBomb.isActive) downBomb.ReadyToExplode();
            //        if (down.gameObject.CompareTag("Player") && canKill) Destroy(down.gameObject);
            //    }

            //    if (down != null && bomb != null && bomb.isActive)
            //    {
            //        bomb.ReadyToExplode();
            //        return;
            //    }


            //}
            //isMoving = false;



            if (control || (space && bombs > 0)) newDirection = Vector2.zero;

            float sqrRemainingDistance = (destVector / 2 - childSprite.transform.localPosition).sqrMagnitude;
            if (sqrRemainingDistance > float.Epsilon && destVector / 2 != childSprite.transform.localPosition)
            {
                Vector3 newPostion = Vector3.MoveTowards(childSprite.transform.localPosition, destVector / 2, inverseMoveTime * Time.deltaTime);

                if (newPostion == destVector / 2 && currDirection == newDirection)
                {
                    Collider2D directionObject = Physics2D.OverlapBox(end + newDirection, new Vector2(.9f, .9f), 0);
                    if (directionObject == null || directionObject.gameObject.CompareTag("Ground") || directionObject.gameObject.layer == LayerMask.NameToLayer("PickUp"))
                    {
                        //Collider2D directionUp = Physics2D.OverlapBox(end + newDirection + new Vector3(0f, 1f, 0f), new Vector2(.9f, .9f), 0);
                        //if (directionUp != null)
                        //{
                        //    MovingObjectController moverUp = directionUp.gameObject.GetComponent<MovingObjectController>();
                        //    if (moverUp == null || !moverUp.IsMoving)
                        //    {
                        //        end += newDirection;
                        //        newPostion = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime);
                        //    }
                        //}
                        //else
                        //{
                        //end += newDirection;
                        //newPostion = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime);
                        //}
                        end += newDirection;
                        rb2D.MovePosition(transform.position + newDirection);
                        newPostion = Vector3.MoveTowards(childSprite.transform.localPosition - newDirection, destVector / 2, inverseMoveTime * Time.deltaTime);
                    }
                }
                //cc2D.offset = (end - transform.position) / 2;
                //rb2D.MovePosition(newPostion);
                childSprite.transform.localPosition = newPostion;
                return;
            }
            //cc2D.offset = new Vector2(0f, 0f);
            childSprite.transform.localPosition = Vector3.zero;
            rb2D.MovePosition(end);
            isMoving = false;
        }
        else
        {
            if (space) SetBomb();

            if (newDirection == Vector3.zero) return;
            destVector = newDirection;
            end = newDirection + transform.position;
            currDirection = newDirection;

            Collider2D directionObject = Physics2D.OverlapBox(end, new Vector2(.9f, .9f), 0);

            //if (directionObject == null)
            //{
            //    //Collider2D directionUp = Physics2D.OverlapBox(end + new Vector3(0f, 1f), new Vector2(.9f, .9f), 0);
            //    //Debug.Log(directionUp);
            //    //if (directionUp != null)
            //    //{
            //    //    MovingObjectController moverUp = directionUp.gameObject.GetComponent<MovingObjectController>();
            //    //    if (moverUp != null && moverUp.IsMoving) return;
            //    //}
            //}

            if (directionObject != null && 1 << directionObject.gameObject.layer == blockingLayer.value)
            {

                MovingObjectController MOCStone = directionObject.gameObject.GetComponent<MovingObjectController>();
                if (MOCStone == null) return;

                if (!MOCStone.Push(newDirection)) return;
                else if (control) return;
            }

            if (directionObject != null && 1 << directionObject.gameObject.layer == pickUpLayer.value)
            {
                if (directionObject.gameObject.CompareTag("Ballon")) PickUpOxygen();
                else if (directionObject.gameObject.CompareTag("BombPickUp")) PickUpBomb();
                else if (directionObject.gameObject.CompareTag("Crystal")) PickUpCrystals();
                else if (directionObject.gameObject.CompareTag("Mineral")) PickUpMinerals();

               
                if (control)
                {
                    Destroy(directionObject.gameObject);
                    return;
                }
            }

            if (directionObject != null && directionObject.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                Destroy(directionObject.gameObject);
                if (control) return;
            }
            if (!control)
            {

                isMoving = true;
            //    Debug.Log(transform.position);
            //    Debug.Log(newDirection / 2 + transform.position);
                rb2D.MovePosition(newDirection / 2 + transform.position);

                childSprite.transform.localPosition = -destVector / 2;
                FixedUpdate();
            }
        }


    }

    public void PickUpBomb(bool remove = false)
    {
        if (remove) bombs--;
        else bombs++;
        bombText.text = "Bombs: " + bombs;
    }

    public void PickUpMinerals()
    {
        minerals++;
        mineralText.text = "Minerals: " + minerals;
    }

    public void PickUpOxygen()
    {
        oxygen += secondsForBallon;
        oxygenText.text = "Oxygen: " + oxygen;
    }

    public void PickUpCrystals()
    {
        crystals++;
        crystalText.text = "Crystals: " + crystals;
    }

    private IEnumerator OxygenFlow()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            oxygen--;
            oxygenText.text = "Oxygen: " + oxygen;
            if (oxygen <= 0) Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        SceneManager.LoadScene("Menu");
    }
}

