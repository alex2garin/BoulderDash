using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController2 : MonoBehaviour
{

    public float explosionLengthTime = .5f;
    public float destroyDelayTime = .5f;
    public bool isActive;


    private Animator animator;
    private bool exploding = false;
    private Rigidbody2D rb2D;
    //   private BoxCollider2D BC2D;
    private MovingObjectController MOC;


    // Use this for initialization
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        //      if(animator==null) animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        //       BC2D = GetComponent<BoxCollider2D>();
        MOC = GetComponent<MovingObjectController>();

        SpriteRenderer sr = GetComponentInChildren<SpriteRenderer>();
        if (sr != null)
            sr.sortingOrder = ApplicationController.BombExplosionSortValue;

    }

    public void Planted()
    {
        MOC.enabled = false;
        rb2D.Sleep();
    }

    public void Activate()
    {
        rb2D.WakeUp();
        MOC.enabled = true;
    }

    public void ReadyToExplode()
    {
        Debug.Log("ready");
        if (exploding) return;
        exploding = true;

        MOC.enabled = false;
        StartCoroutine(AnimationDelay());
    }

    private IEnumerator AnimationDelay()
    {
        animator.SetTrigger("Explode");
        yield return new WaitForSeconds(explosionLengthTime);
        Explode();

        yield return new WaitForSeconds(destroyDelayTime);
        Destroy(gameObject);
    }


    private void Explode()
    {

        Collider2D[] surround = Physics2D.OverlapBoxAll(transform.position, new Vector2(2f, 2f), 0f);
        foreach (var objectCollider2D in surround)
        {
            //Debug.Log(objectCollider2D.gameObject);
            BombController surrondBC = objectCollider2D.gameObject.GetComponent<BombController>();
            //Debug.Log(surrondBC);
            //if (surrondBC != null
            //    //(objectCollider2D.gameObject.CompareTag("Bomb") || objectCollider2D.gameObject.CompareTag("BombPickUp")) 
            //    && objectCollider2D.gameObject != gameObject && (canExpl = objectCollider2D.GetComponent<DestroyByExplosion>().CanExplode(transform.position)))
            ////objectCollider2D.gameObject.GetComponent<BombController>().ReadyToExplode();
            //{
            //    surrondBC.ReadyToExplode();
            //    Debug.Log(canExpl);
            //}
            if (surrondBC != null)
            {
                Debug.Log(surrondBC);
                //Debug.Log(objectCollider2D.gameObject);
                if (objectCollider2D.gameObject != gameObject)
                {

                    Debug.Log(objectCollider2D.gameObject);
                    Debug.Log(objectCollider2D.GetComponent<DestroyByExplosion>().CanExplode(transform.position));
                    if (objectCollider2D.GetComponent<DestroyByExplosion>().CanExplode(transform.position))
                    {
                        {
                 //           surrondBC.ReadyToExplode();

                        }
                    }
                }
            }
            if (surrondBC != null)
                //(objectCollider2D.gameObject.CompareTag("Bomb") || objectCollider2D.gameObject.CompareTag("BombPickUp"))
                continue;

            DestroyByExplosion objectToDestroy = objectCollider2D.gameObject.GetComponent<DestroyByExplosion>();
            if (objectToDestroy != null && objectToDestroy.CanExplode(transform.position)) objectToDestroy.ToDestroyByExplosion();
        }
        var neighbours = Physics2D.OverlapBoxAll(transform.position, new Vector2(4f, 4f), 0f);
        foreach (var neighbour in neighbours)
        {
            CornersAndBordersController cabc = neighbour.gameObject.GetComponent<CornersAndBordersController>();
            if (cabc != null) cabc.needToUpdate = true;
        }

    }


    //public void SetActive( bool isActiveFlag)
    //{
    //    if (isActiveFlag) sr.sprite = activeBombSprite;
    //    else sr.sprite = deactiveBombSprite;
    //    isActive = isActiveFlag;
    //}




}
