using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : MonoBehaviour {

    public GameObject gameUI;
    public GameObject mainMenuUI;

    public AudioController audio;

    public void OnMouseDown()
    {
        audio.ButtonClick();
        gameUI.SetActive(true);
        mainMenuUI.SetActive(false);
    }



    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
