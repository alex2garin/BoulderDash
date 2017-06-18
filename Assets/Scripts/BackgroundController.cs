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
        //		target = GameObject.FindWithTag("Player").transform;

    }

    void LateUpdate()
    {
        if (target == null)
        {
            target = GameObject.FindWithTag("Player").transform;
        }
        else
        {

            //Vector3 pos = new Vector3 (Mathf.Clamp (target.position.x, xMix, xMax), Mathf.Clamp (target.position.y, yMix, yMax), depth);
            Vector3 pos = new Vector3(target.position.x, target.position.y, depth);
            transform.position = Vector3.Lerp(transform.position, pos, speed * Time.deltaTime);
        }
    }
}
