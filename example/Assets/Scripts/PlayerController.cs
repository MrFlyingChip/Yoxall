using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public class PlayerController : MonoBehaviour {

    public Material matPlayerEnter;
    public Material matPlayerConqued;
    public Material superBonus;
    public Material matTowerPlayer;
    public Material matlightPlayer;

    public Material playerMaterial;

    public CellController currentCell;
    public CellController previousCell;
    public CellController startPosition;

    public int dir = 1;

    public int bonus = -1;

    private bool m_isAxisInUse = false;
    private bool m_isSubmitInUse = false;

    public float speedTime;

    public bool shield;

    public int towers;
    public int points;
    public int lives;

    public GameController gameController;
    public UIController ui;
    public bool wall;

    public GameObject unitStart;
    public AudioClip moveVertical;
    public AudioClip moveHorizontal;

    public AudioController audioController;

    private CellBrush brush;
    SetConqued del;
    public List<string> faces = new List<string>();
    public void Reset()
    {
        currentCell = startPosition;
        transform.position = currentCell.playerPosition;
        transform.rotation = Quaternion.Euler(currentCell.playerRotation.x, currentCell.playerRotation.y, currentCell.playerRotation.z);
        dir = 1;
        speedTime = 0.24f;
        bonus = 0;
        towers = 0;
        points = 0;
        lives = 0;
        shield = false;
        brush = new CellBrush();
        Material mat = GetComponent<MeshRenderer>().material;
        mat = playerMaterial;
        GetComponent<MeshRenderer>().material = mat;
        del = SetConqueredCells;
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        unitStart.SetActive(true);
        yield return new WaitForSeconds(0.24f);
        StartPosition();
        yield return new WaitForSeconds(0.8f);
        unitStart.SetActive(false);
        StartCoroutine(Move());
    }

    public void DrawCellsInRed(Material mat)
    {
        CellController[] cells = GameObject.FindObjectsOfType<CellController>();
        foreach (CellController c in cells)
        {
            if (c.conquedPlayer == "PlayerUnit")
            {
                c.GetComponent<Renderer>().material = mat;
            }
        }
    }

    private void StartPosition()
    {
        SetConqueredCell(startPosition);
        SetConqueredCell(startPosition.up);
        SetConqueredCell(startPosition.down);
        SetConqueredCell(startPosition.left);
        SetConqueredCell(startPosition.right);
        SetConqueredCell(startPosition.left.up);
        SetConqueredCell(startPosition.right.up);
        SetConqueredCell(startPosition.left.down);
        SetConqueredCell(startPosition.right.down);
    }

    private void SetConqueredCell(CellController cell)
    {
        cell.SetCellConqued(gameObject, matPlayerConqued, matTowerPlayer, matlightPlayer);
        if (cell.tower != null)
        {
            towers++;
            points += 1000;
        }
        points++;
        ui.ShowTowers(towers);
        ui.ShowScore(points);
        if (towers >= 54)
        {
            StopAllCoroutines();
            GetComponent<Animator>().SetBool("Con", true);
        }
    }

    private void Update()
    {
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            
            if (m_isAxisInUse == false)
            {
                // Call your event function here.
                m_isAxisInUse = true;
                if ((currentCell.tag == "Face2" && dir != 2) /*|| (currentCell.tag == "Face1" && dir != 2 && currentCell.row > 12)*/)
                {
                    dir = 4;
                }
                if (dir != 4)
                {
                    dir = 2;
                }
                
            }
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            if (m_isAxisInUse == false)
            {
                // Call your event function here.
                m_isAxisInUse = true;
                if ((currentCell.tag == "Face2" && dir != 4) /*|| (currentCell.tag == "Face1" && dir != 4 && currentCell.row > 12)*/)
                {
                    dir = 2;
                }
                if (dir != 2)
                {
                    dir = 4;
                }
            }
        }
        else if (Input.GetAxisRaw("Vertical") > 0)
        {
            if (m_isAxisInUse == false)
            {
                m_isAxisInUse = true;
                // Call your event function here.
                if ((currentCell.tag == "Face2" && dir != 1) /*|| (currentCell.tag == "Face1" && dir != 1 && currentCell.row > 12)*/)
                {
                    dir = 3;
                }
                if (dir != 3)
                {
                    dir = 1;
                }
            }
        }
        else if (Input.GetAxisRaw("Vertical") < 0)
        {
            if (m_isAxisInUse == false)
            {
                // Call your event function here.
                m_isAxisInUse = true;
                if ((currentCell.tag == "Face2" && dir != 3) /*|| (currentCell.tag == "Face1" && dir != 3 && currentCell.row > 12)*/)
                {
                    dir = 1;
                }
                if (dir != 1)
                {
                    dir = 3;
                }
            }
        }
        if (Input.GetAxisRaw("Submit") > 0)
        {
            if (m_isSubmitInUse == false)
            {
                // Call your event function here.
                m_isSubmitInUse = true;
                ProcessBonus();
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

    private void ProcessBonus()
    {
        switch (bonus)
        {
            case 1:
                StartCoroutine(DoubleSpeed());
                break;
            case 2:
                StartCoroutine(SuperBonus());
                break;
            
        }
        bonus = -1;
        ui.ClearBonus(bonus);
       
    }

    private void GenerateBonus(CellController cell)
    {
        if (currentCell.bonus.tag == "Bonus1") bonus = 1;
        else bonus = 2;
        ui.ShowBonus(bonus);
        Destroy(cell.bonus);
    }

    IEnumerator DoubleSpeed()
    {
        float temp = 0.24f;
        speedTime = 0.15f;
        yield return new WaitForSeconds(20);
        speedTime = temp; 
    }

    IEnumerator SuperBonus()
    {
        float temp = 0.24f;
        speedTime = 0.21f;
        GetComponent<Animator>().SetBool("Super", true);
        shield = true;
        yield return new WaitForSeconds(10);
        shield = false;
        speedTime = temp;
        GetComponent<Animator>().SetBool("Super", false);
    }

    IEnumerator Wall()
    {
        wall = true;
        yield return new WaitForSeconds(5);
        wall = false;
    }

    void SetConqueredCells()
    {
        foreach (var item in faces)
        {
            List<CellController> faceCells = CellControllerHolder.GetControllers(item);
            foreach (var cell in faceCells)
            {
                if (cell.marked && cell.conquedPlayer == "PlayerUnit")
                {
                    SetConqueredCell(cell);
                }
            }
        }
    }

    public delegate void SetConqued();

    void Conquer()
    {
        int currNumber = points;    
        brush.PaintCells(gameObject, del, faces);
        faces = new List<string>();
        if ((points - currNumber) > 0) GameObject.FindObjectOfType<UIController>().ShowNumbers(points - currNumber);
    }

    public IEnumerator Move()
    {
        while (true)
        {
            yield return new WaitForSeconds(speedTime);
           
            transform.position = currentCell.playerPosition;
            transform.rotation = Quaternion.Euler(currentCell.playerRotation.x, currentCell.playerRotation.y, currentCell.playerRotation.z);
            if (currentCell.bonus != null)
            {
                GenerateBonus(currentCell);
            }
            StepOnCell();
            
            previousCell = currentCell;
            Turn();
            CorrectDir();
            if (currentCell.tower != null)
            {
                GetComponent<Animator>().SetBool("Super", false);
                StartAgain();
            }
            GameOver();
        }
    }

    private void StepOnCell()
    {
        if (currentCell.marked && currentCell.conquedPlayer != "PlayerUnit")
        {
            GameObject enemy = GameObject.Find(currentCell.conquedPlayer);
            if (enemy.GetComponent<EnemyController>().dead)
            {
                currentCell.SetCellMarked(gameObject, matPlayerEnter);
            }
            else
            {
                gameController.DestroyEnemy(enemy);
                currentCell.SetCellMarked(gameObject, matPlayerEnter);
                points += 1000;
                GameObject.FindObjectOfType<UIController>().ShowNumbers(1000);
                ui.ShowScore(points);
            }   
        }
        else if (currentCell.conqued && currentCell.conquedPlayer != "PlayerUnit")
        {
            GameObject enemy = GameObject.Find(currentCell.conquedPlayer);
            if (enemy.GetComponent<EnemyController>().dead)
            {
                currentCell.SetCellMarked(gameObject, matPlayerEnter);
            }
            else if (shield)
            {
                currentCell.SetCellMarked(gameObject, matPlayerEnter);
            }
            else
            {
                StartAgain();
            }
            
        }
        else if (currentCell.marked && currentCell.conquedPlayer == "PlayerUnit")
        {
            StartAgain();
        }
        else if (!currentCell.conqued && !currentCell.marked)
        {
            currentCell.SetCellMarked(gameObject, matPlayerEnter);
        }
        else if (currentCell.conqued && currentCell.conquedPlayer == "PlayerUnit")
        {
            CellController[] cells = GameObject.FindObjectsOfType<CellController>();
            int i = 0;
            foreach (CellController c in cells)
            {
                if (c.marked && c.conquedPlayer == "PlayerUnit") i++;
            }
            if(i > 0) Conquer();
        }
        if (!faces.Contains(currentCell.tag))
        {
            faces.Add(currentCell.tag);
        }
    }

    private void Turn()
    {
        if (dir == 1) TurnUp();
        else if (dir == 2) TurnRight();
        else if (dir == 3) TurnDown();
        else if (dir == 4) TurnLeft();
    }

    private void CorrectDir()
    {
        if ((currentCell.tag == "Face4" && previousCell.tag == "Face2") || (currentCell.tag == "Face2" && previousCell.tag == "Face4")) dir = 4;
        else if ((currentCell.tag == "Face5" && previousCell.tag == "Face2") || (currentCell.tag == "Face2" && previousCell.tag == "Face5")) dir = 2;
        else if ((currentCell.tag == "Face4" && previousCell.tag == "Face6") || (currentCell.tag == "Face5" && previousCell.tag == "Face6")) dir = 3;
        else if ((currentCell.tag == "Face4" && previousCell.tag == "Face1") || (currentCell.tag == "Face5" && previousCell.tag == "Face1")) dir = 1;
        else if ((currentCell.tag == "Face1" && previousCell.tag == "Face5") || (currentCell.tag == "Face6" && previousCell.tag == "Face5")) dir = 2;
        else if ((currentCell.tag == "Face6" && previousCell.tag == "Face4") || (currentCell.tag == "Face1" && previousCell.tag == "Face4")) dir = 4;
    }

    private void GameOver()
    {
        if (lives < 0)
        {
            audioController.GameOver();
            StopAllCoroutines();
            GetComponent<Animator>().SetBool("Over", true);
        }
    }

    public void StartAgain()
    {
        lives--;
        if (lives >= 0)
        {
            transform.position = startPosition.playerPosition;
            transform.rotation = Quaternion.Euler(startPosition.playerRotation);
            currentCell = startPosition;
        }
        
    }

    private void TurnUp()
    {
        GetComponent<AudioSource>().clip = moveVertical;
        GetComponent<AudioSource>().Play();
        currentCell = currentCell.up;
    }

    private void TurnDown()
    {
        GetComponent<AudioSource>().clip = moveVertical;
        GetComponent<AudioSource>().Play();
        currentCell = currentCell.down;      
    }

    private void TurnRight()
    {
        GetComponent<AudioSource>().clip = moveHorizontal;
        GetComponent<AudioSource>().Play();
        currentCell = currentCell.right;
    }

    private void TurnLeft()
    {
        GetComponent<AudioSource>().clip = moveHorizontal;
        GetComponent<AudioSource>().Play();
        currentCell = currentCell.left;   
    }

    public void ShowWinGame()
    {
        ui.ShowWinGame();
    }

    public void ShowGameOver()
    {
        ui.ShowGameOver();
    }

    public void WinGame()
    {
        SaveResult();
        Material mat = GetComponent<MeshRenderer>().material;
        mat = playerMaterial;
        GetComponent<MeshRenderer>().material = mat;
        gameController.WinGame();
    }

    public void MainMenu()
    {
        SaveResult();
        Material mat = GetComponent<MeshRenderer>().material;
        mat = playerMaterial;
        GetComponent<MeshRenderer>().material = mat;
        gameController.GameOver();
    }

    void SaveResult()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            if (PlayerPrefs.GetInt("HighScore") < points) PlayerPrefs.SetInt("HighScore", points);
        }
        else
        {
            PlayerPrefs.SetInt("HighScore", points);
        }
    }


}
