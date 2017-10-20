using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour {
    
    public float explosionLengthTime = .5f;
    public float destroyDelayTime = .5f;
    public bool isActive;
    

//    private SpriteRenderer sr;
    private Animator animator;
    private bool exploding = false;
    private Rigidbody2D rb2D;
    private BoxCollider2D BC2D;
    private MovingObjectController MOC;
    

    // Use this for initialization
    void Awake () {
        //        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        //animator.SetBool("isActive", isActive);
        rb2D = GetComponent<Rigidbody2D>();
        BC2D = GetComponent<BoxCollider2D>();
        MOC = GetComponent<MovingObjectController>();

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
            sr.sortingOrder = ApplicationController.BombExplosionSortValue;

        //Debug.Log(rb2D);
    }

    public void Planted()
    {
        MOC.enabled = false;
        rb2D.Sleep();
    //    BC2D.enabled = false;
        
    }

    public void Activate()
    {
        rb2D.WakeUp();
    //    BC2D.enabled = true;
        MOC.enabled = true;
    }

    public void ReadyToExplode()
    {
        if (exploding) return;
        exploding = true;

        MOC.enabled = false;
        //Debug.Log("ready to explode is called");
        //Debug.Log(gameObject);
        //Debug.Log(transform.position);
        StartCoroutine(AnimationDelay());
    }

    private IEnumerator AnimationDelay()
    {
        animator.SetTrigger("Explode");
        //Debug.Log(explosionLengthTime);
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

            if ((objectCollider2D.gameObject.CompareTag("Bomb") || objectCollider2D.gameObject.CompareTag("BombPickUp")) && objectCollider2D.gameObject != gameObject) objectCollider2D.gameObject.GetComponent<BombController>().ReadyToExplode();
            if (objectCollider2D.gameObject.CompareTag("Bomb") || objectCollider2D.gameObject.CompareTag("BombPickUp")) continue;

            DestroyByExplosion objectToDestroy = objectCollider2D.gameObject.GetComponent<DestroyByExplosion>();
            if (objectToDestroy != null) objectToDestroy.ToDestroyByExplosion();
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
