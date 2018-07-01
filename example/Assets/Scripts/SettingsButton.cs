using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsButton : MonoBehaviour {

    public GameObject settings;
    public SoundController sound;
    public AudioController audio;
    public GameObject mainMenuUI;
    public void OnMouseDown()
    {
        settings.SetActive(true);
        sound.gameObject.SetActive(true);
        audio.ButtonClick();
        sound.ShowVolume();
        sound.Reset();
        mainMenuUI.SetActive(false);
    }
}
