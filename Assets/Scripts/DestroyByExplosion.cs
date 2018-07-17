using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByExplosion : MonoBehaviour
{
    private SpriteRenderer sr;

    private void Start()
    {

        sr = GetComponentInChildren<SpriteRenderer>();
        if (sr == null) sr = GetComponent<SpriteRenderer>();
    }
    public void ToDestroyByExplosion()
    {
        Destroy(gameObject);
    }

    public bool CanExplode(Vector3 bombPosition)
    {

        if (sr == null) return true;
        if ((bombPosition.x - 1.5f <= sr.transform.position.x && sr.transform.position.x <= bombPosition.x + 1.5f)
             && (bombPosition.y-1.5f <= sr.transform.position.y && sr.transform.position.y <= bombPosition.y + 1.5f))
            return true;
        return false;
    }
}
