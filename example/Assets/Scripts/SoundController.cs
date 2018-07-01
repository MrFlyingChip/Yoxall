using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {

    public GameObject[] dec;
    public GameObject[] slots;
    public Transform[] slotsTransform;

    public AudioSource audio;
    public float audioLevel;

    public int level;
    public bool graphics;

    public bool verticalAxesUsed;

    public GameObject[] graphicsOn;
    public GameObject[] graphicsOff;
    public GameObject mainmenuUI;

    public GameObject[] buttons;

    public FxPro fx;

	// Use this for initialization
	void Start () {
        if (PlayerPrefs.HasKey("sound"))
        {
            audioLevel = PlayerPrefs.GetFloat("sound");
            audio.volume = audioLevel;
        }
        else
        {
            audioLevel = 1f;
            audio.volume = audioLevel;
            PlayerPrefs.SetFloat("sound", audioLevel);            
        }
        if (PlayerPrefs.HasKey("graphics"))
        {
            graphics = (PlayerPrefs.GetInt("graphics") == 0) ? true : false;
        }
        else
        {
            graphics = false;
        }
        SetGraphics();
        gameObject.SetActive(false);
    }
	
    void SetGraphics()
    {
        if (!graphics)
        {
            graphicsOn[1].SetActive(true);
            graphicsOff[0].SetActive(true);
            graphicsOn[0].SetActive(false);
            graphicsOff[1].SetActive(false);
            
            PlayerPrefs.SetInt("graphics", 1);
            fx.enabled = true;
        }
        else
        {
            graphicsOn[0].SetActive(true);
            graphicsOff[1].SetActive(true);
            graphicsOn[1].SetActive(false);
            graphicsOff[0].SetActive(false);
            
            PlayerPrefs.SetInt("graphics", 0);
            fx.enabled = false;
        }
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            if (level == 0)
            {
                if (audioLevel <= 1)
                {
                    audioLevel += 0.01f;
                    audio.volume = audioLevel;
                    PlayerPrefs.SetFloat("sound", audioLevel);
                    ShowVolume();
                }
            }
            else if(level == 1)
            {
                graphics = false;
                SetGraphics();
            }
            
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            if (level == 0)
            {
                if (audioLevel >= 0)
                {
                    audioLevel -= 0.01f;
                    audio.volume = audioLevel;
                    PlayerPrefs.SetFloat("sound", audioLevel);
                    ShowVolume();
                }
            }
            else if (level == 1)
            {
                graphics = true;
                SetGraphics();
                
            } 
        }
        if (Input.GetAxisRaw("Vertical") < 0)
        {
            if(level < 2 && !verticalAxesUsed)
            {
                level++;
                verticalAxesUsed = true;
            }
            GetComponent<Animator>().SetInteger("Level", level);
        }
        else if (Input.GetAxisRaw("Vertical") > 0)
        {
            if (level > 0 && !verticalAxesUsed)
            {
                level--;
                verticalAxesUsed = true;
            }
            GetComponent<Animator>().SetInteger("Level", level);
        }
        else if(Input.GetAxisRaw("Vertical") == 0)
        {
            verticalAxesUsed = false;
        }
        if (Input.GetAxisRaw("Cancel") > 0)
        {
            level = 0;
            mainmenuUI.GetComponent<MainMenuController>().menu = true;
            
            mainmenuUI.SetActive(true);
            gameObject.SetActive(false);
            
        }
    }

    public void Reset()
    {
        foreach (var item in buttons)
        {
            item.transform.localPosition = new Vector3(0, item.transform.localPosition.y, item.transform.localPosition.z);
        }
    }

    public void ShowVolume()
    {
        int volume = (int)(audioLevel * 100);
        foreach (var item in slots) Destroy(item);
        List<int> list = new List<int>();
        do
        {
            list.Add(volume % 10);
            volume /= 10;
        } while (volume != 0);
        for (int i = 0; i < list.Count; i++)
        {
            GameObject go = Instantiate(dec[list[i]], slotsTransform[i].position, (list[i] == 5) ? Quaternion.Euler(-90f, 90f, -180f) : slotsTransform[i].rotation) as GameObject;
            slots[i] = go;
            go.transform.SetParent(slotsTransform[i]);
        }
    }
}
