using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationController : MonoBehaviour {

    public enum Level { random, level1 };
    public static Vector3 gravity = Vector2.up;
    public static Vector3 Left
    {
        get
        {
            return new Vector3(gravity.y, -gravity.x);
        }
    }
    public static Vector3 UpLeft
    {
        get
        {
            return new Vector3(-gravity.x + gravity.y, -gravity.x - gravity.y);
        }
    }
    public static Vector3 DownLeft
    {
        get
        {
            return new Vector3(gravity.x + gravity.y, -gravity.x + gravity.y);
        }
    }
    public static Vector3 Right
    {
        get
        {
            return new Vector3(-gravity.y, gravity.x);
        }
    }
    public static Vector3 UpRight
    {
        get
        {
            return new Vector3(-gravity.x - gravity.y, gravity.x - gravity.y);
        }
    }
    public static Vector3 DownRight
    {
        get
        {
            return new Vector3(gravity.x - gravity.y, gravity.x + gravity.y);
        }
    }

    public static Level levelToLoad = Level.random;

    private static int bombExplosionSortValue = 0;
    public static int BombExplosionSortValue
    {
        get { return bombExplosionSortValue++; }
    }

    // Use this for initialization
    void Awake () {
        DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
