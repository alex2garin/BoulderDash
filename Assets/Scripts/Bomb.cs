using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bomb : MonoBehaviour {

    public float explosionLengthTime = .5f;
    public float destroyDelayTime = .5f;
    public bool isActive;

    private bool shouldExplodeFlag = false;
    private Animator animator;
    private bool isExploding = false;
    private SpriteRenderer sr;

    protected Rigidbody2D rb2D;
    protected MovingObject MOC;
    //private bool exploding = false;

    //public bool ShouldExplodeFlag { get; set; }

    //protected abstract void BeforeFixedUpdate();

    public void ShouldExplode()
    {
        //Debug.Log(2);
        shouldExplodeFlag = true;
    }

    // Use this for initialization
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        MOC = GetComponent<MovingObject>();

        sr = GetComponentInChildren<SpriteRenderer>();
        if (sr != null) sr.sortingOrder = ApplicationController.BombExplosionSortValue;
    }

    private void FixedUpdate()
    {
        //Debug.Log("isExploding=");
        //Debug.Log(isExploding);
        //Debug.Log("shouldExplodeFlag=");
        //Debug.Log(shouldExplodeFlag);
        //Debug.Log("MOC.IsStopped=");
        //Debug.Log(MOC.IsStopped);
        if (isExploding) return;
        //BeforeFixedUpdate();
        if (shouldExplodeFlag == true)  MOC.Stop(); //Debug.Log("3"); }
        if (shouldExplodeFlag == false || MOC.IsStopped == false) return;
        //Debug.Log("4");
        StartCoroutine(AnimationDelay());

    }

    private IEnumerator AnimationDelay()
    {
        sr.sortingOrder += 500;
        animator.SetTrigger("Explode");
        yield return new WaitForSeconds(explosionLengthTime);
        KillHeigbours();

        yield return new WaitForSeconds(destroyDelayTime);
        Destroy(gameObject);
    }


    private void KillHeigbours()
    {

        Collider2D[] surround = Physics2D.OverlapBoxAll(transform.position, new Vector2(2f, 2f), 0f);
        foreach (var objectCollider2D in surround)
        {
        //    if(1==2)
        //    { 
        //    //Debug.Log(objectCollider2D.gameObject);
        //    BombController surrondBC = objectCollider2D.gameObject.GetComponent<BombController>();
        //    //Debug.Log(surrondBC);
        //    //if (surrondBC != null
        //    //    //(objectCollider2D.gameObject.CompareTag("Bomb") || objectCollider2D.gameObject.CompareTag("BombPickUp")) 
        //    //    && objectCollider2D.gameObject != gameObject && (canExpl = objectCollider2D.GetComponent<DestroyByExplosion>().CanExplode(transform.position)))
        //    ////objectCollider2D.gameObject.GetComponent<BombController>().ReadyToExplode();
        //    //{
        //    //    surrondBC.ReadyToExplode();
        //    //    Debug.Log(canExpl);
        //    //}
        //    if (surrondBC != null)
        //    {
        //        //Debug.Log(surrondBC);
        //        //Debug.Log(objectCollider2D.gameObject);
        //        if (objectCollider2D.gameObject != gameObject)
        //        {

        //            //Debug.Log(objectCollider2D.gameObject);
        //            //Debug.Log(objectCollider2D.GetComponent<DestroyByExplosion>().CanExplode(transform.position));
        //            if (objectCollider2D.GetComponent<DestroyByExplosion>().CanExplode(transform.position))
        //            {
        //                surrondBC.ReadyToExplode();

        //            }
        //        }
        //    }
        //    if (surrondBC != null)
        //        //(objectCollider2D.gameObject.CompareTag("Bomb") || objectCollider2D.gameObject.CompareTag("BombPickUp"))
        //        continue;

        //    DestroyByExplosion objectToDestroy = objectCollider2D.gameObject.GetComponent<DestroyByExplosion>();
        //    if (objectToDestroy != null && objectToDestroy.CanExplode(transform.position)) objectToDestroy.ToDestroyByExplosion();


        //}
            DestroyByExplosion dbeObj = objectCollider2D.gameObject.GetComponent<DestroyByExplosion>();
            if (dbeObj != null && dbeObj.CanExplode(transform.position))
            {
                Bomb bomb = objectCollider2D.gameObject.GetComponent<Bomb>();
                if (bomb != null)
                {
                    if (bomb.gameObject != gameObject) bomb.ShouldExplode();
                }
                else dbeObj.ToDestroyByExplosion();
                
            }


        }

        var neighbours = Physics2D.OverlapBoxAll(transform.position, new Vector2(4f, 4f), 0f);
        foreach (var neighbour in neighbours)
        {
            CornersAndBordersController cabc = neighbour.gameObject.GetComponent<CornersAndBordersController>();
            if (cabc != null) cabc.needToUpdate = true;
        }

    }
}
