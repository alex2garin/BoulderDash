using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour {
    public float speed;
    
    private Transform target;

    void Start()
    {
            
        target = GameObject.FindWithTag("Player").transform;
        if (target!=null)
            transform.position = target.position;

    }

    void FixedUpdate()
    {


        if (target != null)
        { 
            Vector3 pos = new Vector3(target.position.x, target.position.y, 0f);
            transform.position = Vector3.Lerp(transform.position, pos, speed * Time.fixedDeltaTime);
        }
    }
}
