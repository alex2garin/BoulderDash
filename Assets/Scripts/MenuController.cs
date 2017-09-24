using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    public Button playRandomBtn;
    public Button play1stLevelBtn;
    public Button exitBtn;

    //public GameObject ApplicationController;


	// Use this for initialization
	void Start () {
        playRandomBtn.onClick.AddListener(()=> {
            ApplicationController.levelToLoad = ApplicationController.Level.random;
            SceneManager.LoadScene("Main");
        });
        play1stLevelBtn.onClick.AddListener(() => {
            ApplicationController.levelToLoad = ApplicationController.Level.level1;
            SceneManager.LoadScene("Main");
        });
        exitBtn.onClick.AddListener(() => { Application.Quit(); });
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
