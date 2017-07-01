using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour {

    public Sprite activeBombSprite;
    public Sprite deactiveBombSprite;
    public float waitSecondsBeforeExplode = 5f;

    public bool IsActive { get { return isActive; } }

    private bool isActive;
    private SpriteRenderer sr;
    private Animator animator;

    // Use this for initialization
    void Start () {
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        if (sr.sprite == activeBombSprite) isActive = true;
        else isActive = false;
        animator.SetBool("isActive", isActive);
    }

    public void ReadyToExplode()
    {
        StartCoroutine(AnimationDelay());
    }

    private IEnumerator AnimationDelay()
    {

        animator.SetTrigger("Explode");
        yield return new WaitForSeconds(2f);
        Explode();
    }
        
	
    private void Explode()
    {
        Collider2D[] surround = Physics2D.OverlapBoxAll(transform.position, new Vector2(2f, 2f), 0f);
        foreach (var objectCollider2D in surround)
        {

            if (objectCollider2D.gameObject.CompareTag("Bomb") && objectCollider2D.gameObject != gameObject) objectCollider2D.gameObject.GetComponent<BombController>().WaitAndExplode();
            if (objectCollider2D.gameObject.CompareTag("Bomb")) continue;

            DestroyByExplosion objectToDestroy = objectCollider2D.gameObject.GetComponent<DestroyByExplosion>();
            if (objectToDestroy != null) objectToDestroy.ToDestroyByExplosion();
        }
        Destroy(gameObject);
    }

    public void SetActive( bool isActiveFlag)
    {
        if (isActiveFlag) sr.sprite = activeBombSprite;
        else sr.sprite = deactiveBombSprite;
        isActive = isActiveFlag;
    }

    private IEnumerator WaitBeforeExplode()
    {
        yield return new WaitForSeconds(waitSecondsBeforeExplode);
        ReadyToExplode();

    }
    
    public void WaitAndExplode()
    {
        StartCoroutine(WaitBeforeExplode());
    }


  

}
