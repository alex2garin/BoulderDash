using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour {

    public Sprite activeBombSprite;
    public Sprite deactiveBombSprite;

    public bool IsActive { get { return isActive; } }

    private bool isActive;
    private SpriteRenderer sr;

    // Use this for initialization
    void Start () {
        sr = GetComponent<SpriteRenderer>();
        if (sr.sprite == activeBombSprite) isActive = true;
        else isActive = false;
        //Debug.Log("Start");
        //Debug.Log(sr);
	}
	
    public void Explode()
    {

        if (!isActive) return;
        Collider2D[] surround = Physics2D.OverlapBoxAll(transform.position, new Vector2(2f, 2f), 0f);
        foreach (var objectCollider2D in surround)
        {
            if(objectCollider2D.gameObject.CompareTag("Bomb") && objectCollider2D.gameObject != gameObject)     objectCollider2D.gameObject.GetComponent<BombController>().Explode();
             
            DestroyByExplosion objectToDestroy = objectCollider2D.gameObject.GetComponent<DestroyByExplosion>();
            if (objectToDestroy != null) objectToDestroy.ToDestroyByExplosion();
        }
    }

    public void SetActive( bool isActiveFlag)
    {
        if (isActiveFlag) sr.sprite = activeBombSprite;
        else sr.sprite = deactiveBombSprite;
        isActive = isActiveFlag;
    }

  

}
