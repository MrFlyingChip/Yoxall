using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour {

    private bool m_isAxisInUse = false;
    private bool m_isSubmitInUse = false;

    public bool menu = true;

    public PlayButton play;
    public SettingsButton settings;

    public GameObject[] dec;
    public GameObject[] slotsScore;
    public Transform[] slotsTransformScore;
    // Use this for initialization
    void Start()
    {
        menu = true;
        if (PlayerPrefs.HasKey("HighScore"))
        {
            ShowScore(PlayerPrefs.GetInt("HighScore"));
        }
    }
        // Update is called once per frame
        void Update () {
        if (Input.GetAxisRaw("Vertical") > 0)
        {
            if (m_isAxisInUse == false)
            {
                m_isAxisInUse = true;
                menu = !menu;
                GetComponent<Animator>().SetBool("Menu", menu);
            }
        }
        else if (Input.GetAxisRaw("Vertical") < 0)
        {
            if (m_isAxisInUse == false)
            {
                m_isAxisInUse = true;
                menu = !menu;
                GetComponent<Animator>().SetBool("Menu", menu);
            }
        }
        if (Input.GetAxisRaw("Submit") > 0)
        {
            if (m_isSubmitInUse == false)
            {
                // Call your event function here.
                m_isSubmitInUse = true;
                if (menu) play.OnMouseDown();
                else settings.OnMouseDown();
            }
        }
        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            m_isAxisInUse = false;
        }
        if (Input.GetAxisRaw("Submit") == 0)
        {
            m_isSubmitInUse = false;
        }
    }

    public void ShowScore(int score)
    {
        int scoreInt = score;
        foreach (var item in slotsScore) Destroy(item);

        List<int> list = new List<int>();
        do
        {
            list.Add(scoreInt % 10);
            scoreInt /= 10;
        } while (scoreInt != 0);
        InsertZero(list);
        for (int i = 0; i < list.Count; i++)
        {
            GameObject go = Instantiate(dec[list[i]], slotsTransformScore[i]) as GameObject;
            slotsScore[i] = go;
            if (list[i] == 5) go.transform.localScale = new Vector3(22, 22, 22);
            else if (list[i] == 4 || list[i] == 8 || list[i] == 9) go.transform.localScale = new Vector3(2.8f, 2.8f, 2.8f);
            else go.transform.localScale = new Vector3(2, 2, 2);
        }
    }

    void InsertZero(List<int> array)
    {
        while(array.Count < 6)
        {
            array.Add(0);
        }
    }
}
