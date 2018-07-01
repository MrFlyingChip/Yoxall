using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject player;
    public BonusSpawner bonusSpawner;
    public EnemySpawner enemySpawner;

    public GameObject ui;
    public GameObject mainMenu;
    public GameObject bonusGameObject;

    public UIController uiController;

    public Material resetCell;
    public Material resetTower;
    public Material resetLight;

    public AudioController audio;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void TimerEnds()
    {
        player.GetComponent<PlayerController>().StartAgain();
    }

    public void StartGame()
    {
        ResetAll();
        player.SetActive(true);
        player.GetComponent<PlayerController>().Reset();
        ui.SetActive(true);
        StartCoroutine(bonusSpawner.SpawnBonusOne());
        StartCoroutine(bonusSpawner.SpawnBonusTwo());
        enemySpawner.SpawnEnemy();
    }
     
    public void GameOver()
    {
        ResetAll();
        uiController.GameOver();
        mainMenu.SetActive(true);
        player.SetActive(false);
        ui.SetActive(false);
        mainMenu.GetComponent<MainMenuController>().ShowScore(PlayerPrefs.GetInt("HighScore"));
        StopCoroutine(bonusSpawner.SpawnBonusOne());
        StopCoroutine(bonusSpawner.SpawnBonusTwo());
    }

    public void WinGame()
    {
        ResetAll();
        uiController.WinGame();
        mainMenu.SetActive(true);
        player.SetActive(false);
        ui.SetActive(false);
        mainMenu.GetComponent<MainMenuController>().ShowScore(PlayerPrefs.GetInt("HighScore"));
        StopCoroutine(bonusSpawner.SpawnBonusOne());
        StopCoroutine(bonusSpawner.SpawnBonusTwo());
    }

    public void ResetAll()
    {       
        GameObject[] bonuses = GameObject.FindGameObjectsWithTag("Bonus1");
        foreach (GameObject go in bonuses)
        {
            Destroy(go);
        }
        bonuses = GameObject.FindGameObjectsWithTag("Bonus2");
        foreach (GameObject go in bonuses)
        {
            Destroy(go);
        }
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject go in enemies)
        {
            Destroy(go);
        }
        CellController[] cells = GameObject.FindObjectsOfType<CellController>();
        foreach (CellController c in cells)
        {
            c.ResetCell(resetCell, resetTower, resetLight);
        }
    }

    public void ClearCell(CellController cell)
    {
        cell.ResetCell(resetCell, resetTower, resetLight);
    }

    public void ClearEnemyCell(GameObject enemy)
    {
        CellController[] cells = (from cell in FindObjectsOfType<CellController>() where cell.conquedPlayer == enemy.name select cell).ToArray();
        foreach (CellController c in cells)
            ClearCell(c);
    }

    public void DestroyEnemy(GameObject enemy)
    {
        audio.EnemyKilled();
        enemy.GetComponent<EnemyController>().dead = true;
        enemy.GetComponent<Animator>().SetBool("Death", true);
    }

    IEnumerator SpawnNewEnemy(string name)
    {
        yield return new WaitForSeconds(Random.Range(2, 6));
       
    }
}
