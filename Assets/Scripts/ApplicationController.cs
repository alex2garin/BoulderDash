using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationController : MonoBehaviour {

    public enum Level { random, level1 };

    public static Level levelToLoad = Level.random;

	// Use this for initialization
	void Awake () {
        DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
