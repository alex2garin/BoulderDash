using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationController : MonoBehaviour {

    public enum Level { random, level1 };

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
