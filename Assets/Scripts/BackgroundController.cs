using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour {
    //public float xMax;
    //public float yMax;
    //public float xMix;
    //public float yMix;
    public float depth;
    public float speed;

    //	public GameObject player;
    private Transform target;

    void Start()
    {
            
        	target = GameObject.FindWithTag("Player").transform;
        if (target!=null)
            transform.position = target.position;

    }

    void LateUpdate()
    {


        if (target != null)
        { 
            Vector3 pos = new Vector3(target.position.x, target.position.y, depth);
        transform.position = Vector3.Lerp(transform.position, pos, speed * Time.deltaTime);
        }
    }
}
