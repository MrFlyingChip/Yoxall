using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {


    public AudioController audio;

    public GameObject[] bonuses;

    public GameController gameController;

    public GameObject[] dec;
    public GameObject[] slotsScore;
    public Transform[] slotsTransformScore;

    public GameObject[] slotsTowers;
    public Transform[] slotsTransformTowers;

    public GameObject[] slotsNumbers;
    public Transform[] slotsTransformNumbers;

    public GameObject[] slotsTimer;
    public Transform[] slotsTransformTimer;

    public int seconds = 200;

    public AudioClip smallPoints;
    public AudioClip middlePoints;
    public AudioClip bigPoints;

    public GameObject pause;

    public GameObject[] objectsToTurnOff;

    private int currentSeconds;

    public void StartGame()
    {
        currentSeconds = seconds;
        StartCoroutine(Timer());
        ShowScore(0);
        ShowTowers(0);
        foreach (GameObject b in bonuses) b.SetActive(false);
        bonuses[0].SetActive(true);
        audio.PlayerStart();
        gameController.StartGame();
    }

    public void PlaySound(AudioClip clip)
    {
        audio.PlayClip(clip);
    }

    public void GameOver()
    {
        foreach (var item in objectsToTurnOff)
        {
            item.SetActive(false);
        }
        gameObject.SetActive(false);
    }

    public void WinGame()
    {
        audio.WinGame();
        foreach (var item in objectsToTurnOff)
        {
            item.SetActive(false);
        }
        gameObject.SetActive(false);
    }

    public void PlayAudioGameStarted()
    {
        audio.StartGame();
    }

    public void ShowBonus(int bonus)
    {
        GetComponent<Animator>().SetInteger("Bonus", bonus);
        audio.BonusUp();
        foreach (GameObject b in bonuses) b.SetActive(false);
        bonuses[bonus].SetActive(true);
    }

    public void ClearBonus(int bonus)
    {
        if(bonus > 0) audio.Bonuses(bonus);
        GetComponent<Animator>().SetInteger("Bonus", 0);
        foreach (GameObject b in bonuses) b.SetActive(false);
        bonuses[0].SetActive(true);
    }

    public void TurnOffBonus()
    {
        GetComponent<Animator>().SetInteger("Bonus", 0);
    }

    public void TurnOffNumbers()
    {
        GetComponent<Animator>().SetBool("Numbers", false);
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
        for (int i = 0; i < list.Count; i++)
        {
            GameObject go = Instantiate(dec[list[i]], slotsTransformScore[i]) as GameObject;
            slotsScore[i] = go;
            if(list[i] == 5) go.transform.localScale = new Vector3(11f, 11f, 11f);
            else if(list[i] == 4 || list[i] == 8 || list[i] == 9) go.transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
            else go.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void ShowTowers(int towers)
    {
        int scoreInt = towers;
        foreach (var item in slotsTowers) Destroy(item);
        List<int> list = new List<int>();
        do
        {
            list.Add(scoreInt % 10);
            scoreInt /= 10;
        } while (scoreInt != 0);
        if (list.Count == 1)
        {
            list.Add(0);
        }
        for (int i = 0; i < list.Count; i++)
        {
            GameObject go = Instantiate(dec[list[i]], slotsTransformTowers[i]) as GameObject;
            slotsTowers[i] = go;
            go.transform.SetParent(slotsTransformTowers[i]);
            if (list[i] == 5) go.transform.localScale = new Vector3(11f, 11f, 11f);
            else if (list[i] == 4 || list[i] == 8 || list[i] == 9) go.transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
            else go.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void ShowNumbers(int numbers)
    {
        if (numbers < 100) PlaySound(smallPoints);
        else if (numbers < 1200) PlaySound(middlePoints);
        else PlaySound(bigPoints);
        int scoreInt = numbers;
        foreach (var item in slotsNumbers) Destroy(item);
        List<int> list = new List<int>();
        do
        {
            list.Add(scoreInt % 10);
            scoreInt /= 10;
        } while (scoreInt != 0);
        for (int i = 0; i < list.Count; i++)
        {
            GameObject go = Instantiate(dec[list[i]], slotsTransformNumbers[i]) as GameObject;
            slotsNumbers[i] = go;
            go.transform.SetParent(slotsTransformNumbers[i]);
            if (list[i] == 5) go.transform.localScale = new Vector3(11f, 11f, 11f);
            else if (list[i] == 4 || list[i] == 8 || list[i] == 9) go.transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
            else go.transform.localScale = new Vector3(1, 1, 1);
        }
        GetComponent<Animator>().SetBool("Numbers", true);
    }

    public void ShowTimer()
    {
        foreach (var item in slotsTimer) Destroy(item);
        int timerInt = currentSeconds;
        List<int> list = new List<int>();
        int sec = timerInt % 60;
        timerInt /= 60;
        if (timerInt >= 10)
        {
            list.Add(timerInt / 10);
            list.Add(timerInt % 10);
        }
        else
        {
            list.Add(0);
            list.Add(timerInt);
        }
        list.Add(sec / 10);
        list.Add(sec % 10);
        for (int i = 0; i < list.Count; i++)
        {
            GameObject go = Instantiate(dec[list[i]], slotsTransformTimer[i]) as GameObject;
            slotsTimer[i] = go;
            go.transform.SetParent(slotsTransformTimer[i]);
            if (list[i] == 5) go.transform.localScale = new Vector3(11f, 11f, 11f);
            else if (list[i] == 4 || list[i] == 8 || list[i] == 9) go.transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
            else go.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void ShowWinGame()
    {
        GetComponent<Animator>().SetBool("Con", true);
    }

    public void ShowGameOver()
    {
        GetComponent<Animator>().SetBool("Over", true);
    }

    public void TimesUp()
    {
        GetComponent<Animator>().SetBool("Time", true);
    }

    private bool m_isAxisInUse = false;

    private IEnumerator Timer()
    {
        
        while(currentSeconds >= 0)
        {
            if (currentSeconds == 0)
            {
                gameController.TimerEnds();
            }
            ShowTimer();
            yield return new WaitForSeconds(1);
            currentSeconds--;
        }
        
    }

    public void Update()
    {
        if (Input.GetKeyDown("joystick button 10"))
        {
                // Call your event function here.
                if (Time.timeScale == 1)
                {
                    pause.SetActive(true);
                    Time.timeScale = 0;
                }
                else
                {
                    Time.timeScale = 1;
                    pause.SetActive(false);
                }
                
            }
    }

}
