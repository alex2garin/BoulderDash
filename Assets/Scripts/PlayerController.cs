//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.SceneManagement;

//public class PlayerController : MonoBehaviour
//{
//    public bool handleFlag = false;

//    public float moveTime = .1f;
//    public LayerMask blockingLayer;
//    public LayerMask pickUpLayer;
//    public int secondsForBallon = 60;
//    public int startingSecondsOfOxygen = 60;
//    public GameObject bombGO;


//    private float inverseMoveTime;
//    private bool isMoving;

//    private int bombs = 0;
//    private int crystals = 0;
//    private int minerals = 0;
//    private int oxygen = 0;



//    private Text crystalText;
//    private Text bombText;
//    private Text mineralText;
//    private Text oxygenText;
//    private Text crystalsToExitText;

//    private GameObject plantedBomb;

//    private Vector3 end;
//    private Vector3 currDirection;
//    private Vector3 newDirection;
//    private Vector3 destVector;
//    private int numOfCrysrtalsToExit;
//    private SpriteRenderer childSprite;

//    public void SetNumCrystalsToExit(int num)
//    {
//        numOfCrysrtalsToExit = num;
//    }


//    // Use this for initialization
//    void Start()
//    {

//        oxygen = startingSecondsOfOxygen;

//        inverseMoveTime = 1f / moveTime;
//        isMoving = false;
//        crystalText = GameObject.Find("Crystals").GetComponent<Text>();
//        mineralText = GameObject.Find("Minerals").GetComponent<Text>();
//        bombText = GameObject.Find("Bombs").GetComponent<Text>();
//        oxygenText = GameObject.Find("Oxygen").GetComponent<Text>();
//        crystalsToExitText = GameObject.Find("CrystalsToExit").GetComponent<Text>();
//        crystalsToExitText.text = "Crystals To Exit: " + numOfCrysrtalsToExit;

//        childSprite = GetComponentInChildren<SpriteRenderer>();
//        StartCoroutine(OxygenFlow());
//    }

//    private bool switcher = false;

//    private void SetBomb()
//    {
//        if (bombs > 0 && plantedBomb == null)
//        {

//            plantedBomb = Instantiate(bombGO, transform.position, Quaternion.identity);
//            plantedBomb.GetComponent<BombController>().Planted();

//            PickUpBomb(true);
//        }
//    }


//    // Update is called once per frame
//    void FixedUpdate()
//    {


//        float horizontalInput = ApplicationController.inputCTRL.HorizontalMovement();
//        float verticalInput = ApplicationController.inputCTRL.VerticalMovement();

//        if (horizontalInput != 0) newDirection = new Vector2(horizontalInput, 0f);
//        else if (verticalInput != 0) newDirection = new Vector2(0f, verticalInput);
//        else newDirection = Vector2.zero;

//        if (handleFlag) newDirection = new Vector2(1f, 0f);

//        bool control = ApplicationController.inputCTRL.ActionWithoutMoving();
//        if (switcher != control) switcher = control;

//        bool space = ApplicationController.inputCTRL.PlantBomb();


//        if (isMoving)
//        {

//            if (control || (space && bombs > 0)) newDirection = Vector2.zero;

//            float sqrRemainingDistance = (destVector / 2 - childSprite.transform.localPosition).sqrMagnitude;
//            if (sqrRemainingDistance > float.Epsilon && destVector / 2 != childSprite.transform.localPosition)
//            {
//                Vector3 newPostion = Vector3.MoveTowards(childSprite.transform.localPosition, destVector / 2, inverseMoveTime * Time.deltaTime);

//                if (newPostion == destVector / 2 && currDirection == newDirection)
//                {
//                    Collider2D directionObject = Physics2D.OverlapBox(end + newDirection, new Vector2(.9f, .9f), 0);
//                    if (directionObject == null || directionObject.gameObject.CompareTag("Ground"))
//                    {

//                        end += newDirection;
//                        transform.position += newDirection;
//                        newPostion = Vector3.MoveTowards(childSprite.transform.localPosition - newDirection, destVector / 2, inverseMoveTime * Time.deltaTime);
//                    }
//                    else if (directionObject != null  && directionObject.gameObject.layer == LayerMask.NameToLayer("PickUp"))
//                    {
//                        MovingObjectController dirMOC = directionObject.gameObject.GetComponent<MovingObjectController>();
//                        if ( dirMOC != null && dirMOC.IsMoving) { }
//                        else
//                        {
//                            end += newDirection;
//                            transform.position += newDirection;
//                            newPostion = Vector3.MoveTowards(childSprite.transform.localPosition - newDirection, destVector / 2, inverseMoveTime * Time.deltaTime);
//                        }
//                    }
//                }
//                childSprite.transform.localPosition = newPostion;
//                return;
//            }
//            childSprite.transform.localPosition = Vector3.zero;
//            transform.position = end;
//            isMoving = false;
//        }
//        else
//        {
//            if (space) SetBomb();

//            if (newDirection == Vector3.zero) return;
//            destVector = newDirection;
//            end = newDirection + transform.position;
//            currDirection = newDirection;

//            Collider2D directionObject = Physics2D.OverlapBox(end, new Vector2(.9f, .9f), 0);


//            if (directionObject != null && 1 << directionObject.gameObject.layer == blockingLayer.value)
//            {

//                MovingObjectController MOCStone = directionObject.gameObject.GetComponent<MovingObjectController>();
//                if (MOCStone == null) return;

//                if (!MOCStone.Push(newDirection)) return;
//                else if (control) return;
//            }

//            if (directionObject != null && 1 << directionObject.gameObject.layer == pickUpLayer.value)
//            {

//                if (newDirection == ApplicationController.gravity && directionObject.gameObject.GetComponent<MovingObjectController>().IsMoving) return;

//                if (newDirection == -ApplicationController.gravity && directionObject.gameObject.GetComponent<MovingObjectController>().IsMoving) return;

//                if((newDirection == directionObject.gameObject.GetComponent<MovingObjectController>().DestVect ||
//                    -newDirection == directionObject.gameObject.GetComponent<MovingObjectController>().DestVect ) &&
//                    directionObject.gameObject.GetComponent<MovingObjectController>().IsMoving) return;


//                if (directionObject.gameObject.GetComponent<PickUp>().TryPickUp(transform.position))
//                {

//                    if (control)
//                    {
//                        if (directionObject.gameObject.CompareTag("Ballon")) PickUpOxygen();
//                        else if (directionObject.gameObject.CompareTag("BombPickUp")) PickUpBomb();
//                        else if (directionObject.gameObject.CompareTag("Crystal")) PickUpCrystals();
//                        else if (directionObject.gameObject.CompareTag("Mineral")) PickUpMinerals();

//                        Destroy(directionObject.gameObject);
//                        return;
//                    }
//                }
//                else return;
//            }

//            if (directionObject != null && directionObject.gameObject.layer == LayerMask.NameToLayer("Ground"))
//            {
//                if (control)
//                {
//                    Destroy(directionObject.gameObject);
//                    return;
//                }
//            }
//            if (!control)
//            {

//                isMoving = true;

//                transform.position = newDirection / 2 + transform.position;

//                childSprite.transform.localPosition = -destVector / 2;
//                FixedUpdate();
//            }
//        }


//    }

//    public void PickUpBomb(bool remove = false)
//    {
//        if (remove) bombs--;
//        else bombs++;
//        bombText.text = "Bombs: " + bombs;
//    }

//    public void PickUpMinerals()
//    {
//        minerals++;
//        mineralText.text = "Minerals: " + minerals;
//    }

//    public void PickUpOxygen()
//    {
//        oxygen += secondsForBallon;
//        oxygenText.text = "Oxygen: " + oxygen;
//    }

//    public void PickUpCrystals()
//    {
//        crystals++;
//        crystalText.text = "Crystals: " + crystals;
//    }

//    private IEnumerator OxygenFlow()
//    {
//        while (true)
//        {
//            yield return new WaitForSeconds(1f);
//            oxygen--;
//            oxygenText.text = "Oxygen: " + oxygen;
//            if (oxygen <= 0) Destroy(gameObject);
//        }
//    }

//    void OnDestroy()
//    {
//        SceneManager.LoadScene("Menu");
//    }
//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MovingObject
{

    public LayerMask blockingLayer;
    public LayerMask pickUpLayer;
    public int secondsForBallon = 60;
    public int startingSecondsOfOxygen = 60;
    public GameObject bombGO;
    
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
    private int numOfCrysrtalsToExit;
    private bool switcher = false;

    public void SetNumCrystalsToExit(int num)
    {
        numOfCrysrtalsToExit = num;
    }

    private new void Start()
    {
        base.Start();
        oxygen = startingSecondsOfOxygen;
        crystalText = GameObject.Find("Crystals").GetComponent<Text>();
        mineralText = GameObject.Find("Minerals").GetComponent<Text>();
        bombText = GameObject.Find("Bombs").GetComponent<Text>();
        oxygenText = GameObject.Find("Oxygen").GetComponent<Text>();
        crystalsToExitText = GameObject.Find("CrystalsToExit").GetComponent<Text>();
        crystalsToExitText.text = "Crystals To Exit: " + numOfCrysrtalsToExit;

        StartCoroutine(OxygenFlow());
    }

    private void SetBomb()
    {
        if (bombs > 0 && plantedBomb == null)
        {

            plantedBomb = Instantiate(bombGO, transform.position, Quaternion.identity);
            plantedBomb.GetComponent<BombController>().Planted();

            PickUpBomb(true);
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

    protected override Vector3 GetDestination()
    {
        float horizontalInput = ApplicationController.inputCTRL.HorizontalMovement();
        float verticalInput = ApplicationController.inputCTRL.VerticalMovement();
        Vector3 direction;

        bool control = ApplicationController.inputCTRL.ActionWithoutMoving();

        bool space = ApplicationController.inputCTRL.PlantBomb();
        if (space) SetBomb();

        if (switcher != control) switcher = control;

        if (horizontalInput != 0) direction = new Vector2(horizontalInput, 0f);
        else if (verticalInput != 0) direction = new Vector2(0f, verticalInput);
        else direction = Vector2.zero;

        Vector3 endDir = transform.position + direction;


        Collider2D directionObject = Physics2D.OverlapBox(endDir, new Vector2(.9f, .9f), 0);


        if (directionObject != null && 1 << directionObject.gameObject.layer == blockingLayer.value)
        {

            MovingObjectController MOCStone = directionObject.gameObject.GetComponent<MovingObjectController>();
            if (MOCStone == null) return Vector3.zero;

            if (!MOCStone.Push(direction)) return Vector3.zero;
            else if (control) return Vector3.zero;
        }

        if (directionObject != null && 1 << directionObject.gameObject.layer == pickUpLayer.value)
        {

            if (direction == ApplicationController.gravity && directionObject.gameObject.GetComponent<MovingObjectController>().IsMoving) return Vector3.zero;

            if (direction == -ApplicationController.gravity && directionObject.gameObject.GetComponent<MovingObjectController>().IsMoving) return Vector3.zero;

            if ((direction == directionObject.gameObject.GetComponent<MovingObjectController>().DestVect ||
                -direction == directionObject.gameObject.GetComponent<MovingObjectController>().DestVect) &&
                directionObject.gameObject.GetComponent<MovingObjectController>().IsMoving) return Vector3.zero;


            if (directionObject.gameObject.GetComponent<PickUp>().TryPickUp(transform.position))
            {

                if (control)
                {
                    if (directionObject.gameObject.CompareTag("Ballon")) PickUpOxygen();
                    else if (directionObject.gameObject.CompareTag("BombPickUp")) PickUpBomb();
                    else if (directionObject.gameObject.CompareTag("Crystal")) PickUpCrystals();
                    else if (directionObject.gameObject.CompareTag("Mineral")) PickUpMinerals();

                    Destroy(directionObject.gameObject);
                    return Vector3.zero;
                }
            }
            else return Vector3.zero;
        }

        if (directionObject != null && directionObject.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            if (control)
            {
                Destroy(directionObject.gameObject);
                return Vector3.zero;
            }
        }
        if (!control) return direction;
        else return Vector3.zero;



    }
    protected override Vector3 GetDestinationInMovement(Vector3 calculatedNewPosition)
    {
        float horizontalInput = ApplicationController.inputCTRL.HorizontalMovement();
        float verticalInput = ApplicationController.inputCTRL.VerticalMovement();
        Vector3 newDirection;
        if (horizontalInput != 0) newDirection = new Vector2(horizontalInput, 0f);
        else if (verticalInput != 0) newDirection = new Vector2(0f, verticalInput);
        else newDirection = Vector2.zero;

        //bool space = ApplicationController.inputCTRL.PlantBomb();

        //bool control = ApplicationController.inputCTRL.ActionWithoutMoving();



        //if (switcher != control) switcher = control;



        if (switcher)
            //|| (space && bombs > 0)) 
            return Vector2.zero;


        if ((calculatedNewPosition == destVect / 2 || (calculatedNewPosition - destVect / 2).sqrMagnitude <= float.Epsilon) && newDirection == destVect)
        {
            Collider2D directionObject = Physics2D.OverlapBox(end + newDirection, new Vector2(.9f, .9f), 0);
            if (directionObject == null || directionObject.gameObject.CompareTag("Ground"))
            {
                end += newDirection;
                transform.position += newDirection;
                return Vector3.MoveTowards(childSprite.transform.localPosition - newDirection, destVect / 2, inverseMoveTime * Time.deltaTime);
            }
            else if (directionObject != null && directionObject.gameObject.layer == LayerMask.NameToLayer("PickUp"))
            {
                MovingObjectController dirMOC = directionObject.gameObject.GetComponent<MovingObjectController>();
                if (dirMOC != null && dirMOC.IsMoving) { }
                else
                {
                    end += newDirection;
                    transform.position += newDirection;
                    return Vector3.MoveTowards(childSprite.transform.localPosition - newDirection, destVect / 2, inverseMoveTime * Time.deltaTime);
                }
            }
        }

        return calculatedNewPosition;
    }
    protected override void ActionAfterMovement()
    {

    }
    protected override void RotationStart()
    {

    }
    protected override void BeforeFixedUpdate()
    {
        if (plantedBomb != null)
        {
            if ((plantedBomb.transform.position - transform.position).sqrMagnitude >= 1)
            {
                plantedBomb.GetComponent<BombController>().Activate();
                plantedBomb = null;
            }
        }
    }
}
