using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByExplosion : MonoBehaviour
{
    public void ToDestroyByExplosion()
    {
        Destroy(gameObject);
    }

    public bool CanExplode(Vector3 bombPosition)
    {
        SpriteRenderer sr = GetComponentInChildren<SpriteRenderer>();
        if (sr == null) sr = GetComponent<SpriteRenderer>();
        if (sr == null) return true;

        if (((transform.position.x <= sr.transform.position.x && sr.transform.position.x <= bombPosition.x) ||
               (bombPosition.x <= sr.transform.position.x && sr.transform.position.x <= transform.position.x)) &&
            ((transform.position.y <= sr.transform.position.y && sr.transform.position.y <= bombPosition.y) ||
               (bombPosition.y <= sr.transform.position.y && sr.transform.position.y <= transform.position.y)))
            return true;
        return false;
    }
}
