using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

    private AudioSource audio;

    public AudioClip buttonClick;
    public AudioClip startGame;
    public AudioClip playerStart;
    public AudioClip enemyKilled;
    public AudioClip bonusUp;
    public AudioClip[] bonuses;
    public AudioClip gameOver;
    public AudioClip winGame;
    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void ButtonClick()
    {
        PlayClip(buttonClick);
    }
    public void StartGame()
    {
        PlayClip(startGame);
    }
    public void PlayerStart()
    {
        PlayClip(playerStart);
    }
    public void EnemyKilled()
    {
        PlayClip(enemyKilled);
    }
    public void BonusUp()
    {
        PlayClip(bonusUp);
    }
    public void Bonuses(int bonus)
    {
        PlayClip(bonuses[bonus]);
    }
    public void GameOver()
    {
        PlayClip(gameOver);
    }
    public void WinGame()
    {
        PlayClip(winGame);
    }
    public void PlayClip(AudioClip clip)
    {
        audio.clip = clip;
        audio.Play();
    }
}
