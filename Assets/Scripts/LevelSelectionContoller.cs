using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectionContoller : MonoBehaviour {

    //public Button playRandomBtn;
    //public Button play1stLevelBtn;
    //public Button exitBtn;

    //public GameObject ApplicationController;
    public GameObject button_prefab;
    public GameObject canvas;

    // Use this for initialization
    void Start()
    {
        int startX = -220;
        int startY = 200;
        int xStep = 110;
        int yStep = 55;
        int x = startX;
        int y = startY;

        foreach (var file in ApplicationController.fileLevels)
        {
            
            GameObject button = Instantiate(button_prefab, Vector3.zero, Quaternion.identity, canvas.transform);
   
            button.GetComponentInChildren<Text>().text = file.name;
            button.GetComponent<Button>().onClick.AddListener(() => {
                ApplicationController.SelectedFile = file;
                SceneManager.LoadScene("Main"); });
            button.transform.localPosition = new Vector3(x, y, 0f);

            x += xStep;
            if (x == 220)
            {
                x = startX;
                y -= yStep;
            }
        }

        //if(ApplicationController.fileLevels.Length > 0)
        //{
        //    GameObject button = Instantiate(button_prefab, Vector3.zero, Quaternion.identity, canvas.transform);
        //    //Debug.Log(
        //    button.GetComponentInChildren<Text>().text = ApplicationController.fileLevels[1].name;
        //    button.GetComponent<Button>().onClick.AddListener(() => { Application.Quit(); });
        //}



        //playRandomBtn.onClick.AddListener(() => {
        //    ApplicationController.levelToLoad = ApplicationController.Level.random;
        //    SceneManager.LoadScene("Main");
        //});
        //play1stLevelBtn.onClick.AddListener(() => {
        //    ApplicationController.levelToLoad = ApplicationController.Level.level1;
        //    SceneManager.LoadScene("Main");
        //});
        //exitBtn.onClick.AddListener(() => { Application.Quit(); });
    }
}
