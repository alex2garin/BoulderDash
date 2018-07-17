using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {

    private void OnTriggerStay2D(Collider2D collision)
    {

        if ((collision.transform.position - transform.position).sqrMagnitude <= float.Epsilon)
            Destroy(collision.gameObject);
    }
}
