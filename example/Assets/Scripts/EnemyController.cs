using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{ 
    public Material matPlayerEnter;
    public Material matPlayerConqued;
    public Material matTowerPlayer;
    public Material matlightPlayer;
    public CellController currentCell;
    public CellController previousCell;
    public CellController startPosition;

    public int dir = 1;

    public GameController gameController;

    public float speedTime;

    public bool dead;
    private CellBrush brush;
    public List<string> faces = new List<string>();

    public Stack<CellController> path = new Stack<CellController>();
    public CellController endPoint;
    public bool toHome = false;
    public void Reset()
    {
        currentCell = startPosition;
        StartPosition();
        dir = 1;
        speedTime = 0.24f;
        brush = new CellBrush();
        StartCoroutine(Move());
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
    }

    void SetConqueredCells()
    {
        foreach (var item in faces)
        {
            List<CellController> faceCells = CellControllerHolder.GetControllers(item);
            foreach (var cell in faceCells)
            {
                if (cell.marked && cell.conquedPlayer == name)
                {
                    SetConqueredCell(cell);
                }
            }
        }
    }

    void Conquer()
    {
        PlayerController.SetConqued del = SetConqueredCells;
        brush.PaintCells(gameObject, del, faces);
        faces = new List<string>();
    }

    public IEnumerator Move()
    {
        while (true)
        {
            yield return new WaitForSeconds(speedTime);
            if (currentCell.marked && currentCell.conquedPlayer == "PlayerUnit")
            {
                if (GameObject.Find(currentCell.conquedPlayer).GetComponent<PlayerController>().shield)
                {
                    dead = true;
                    gameController.DestroyEnemy(gameObject);
                }
                else
                {
                    GameObject.Find(currentCell.conquedPlayer).GetComponent<PlayerController>().lives--;
                    currentCell.SetCellMarked(gameObject, matPlayerEnter);

                }
            }
            else if (currentCell.conqued && currentCell.conquedPlayer == "PlayerUnit")
            {
                dead = true;
                gameController.DestroyEnemy(gameObject);
            }
            else if (!currentCell.conqued && !currentCell.marked)
            {
                currentCell.SetCellMarked(gameObject, matPlayerEnter);
            }
            else if (currentCell.conqued && currentCell.conquedPlayer == gameObject.name)
            {
                Conquer();
            }
            if (!dead)
            {
                transform.position = currentCell.playerPosition;
                transform.rotation = Quaternion.Euler(currentCell.playerRotation.x, currentCell.playerRotation.y, currentCell.playerRotation.z);
                if (currentCell == endPoint)
                {
                    endPoint = null;
                    toHome = !toHome;
                    if (toHome)
                    {
                        CellController[] allCells = GameObject.FindObjectsOfType<CellController>();
                        List<CellController> myCells = new List<CellController>();
                        foreach (var item in allCells)
                        {
                            if (item.conqued && item.conquedPlayer == name)
                            {
                                myCells.Add(item);
                            }
                        }
                        endPoint = myCells[Random.Range(0, myCells.Count)];

                    }
                }
                CreatePath();     
                previousCell = currentCell;
                currentCell = path.Peek();
            }
            if (!faces.Contains(currentCell.tag))
            {
                faces.Add(currentCell.tag);
            }
        }
    }

    Dictionary<CellController, int> Wave()
    {
        Dictionary<CellController, int> cells = new Dictionary<CellController, int>();
        Queue<CellController> frontier = new Queue<CellController>();
        frontier.Enqueue(currentCell);
        cells.Add(currentCell, 1);
        int i = 0;
        int maxLenght = Random.Range(8, 15);
        while (frontier.Count > 0)
        {
            CellController current = frontier.Dequeue();
            cells.TryGetValue(current, out i);
            i++;
            if (endPoint != null && current == endPoint ) break;

            if(endPoint == null && i >= maxLenght || (current.marked && current.conquedPlayer == "PlayerUnit"))
            {
                int r = Random.Range(0, 100);
                if(r < 20)
                {
                    endPoint = current;
                    break;
                }       
            }

            foreach (var item in current.GetNeighbors())
            {
                if (!cells.ContainsKey(item) && !(item.marked && item.conquedPlayer != "PlayerUnit") && !(item.conqued && item.conquedPlayer != name) && item.tower == null)
                {
                    frontier.Enqueue(item);
                    cells.Add(item, i);
                }
            }
        }
        return cells;
    }

    void CreatePath()
    {
        Dictionary<CellController, int> cells = Wave();
        path = new Stack<CellController>();
        int i = 0;
        int counter;
        CellController current = endPoint;
        path.Push(endPoint);
        cells.TryGetValue(current, out i);
        i--;
        while (i > 1)
        {
        foreach (var item in current.GetNeighbors())
            {
                if (cells.TryGetValue(item, out counter) && counter == i)
                {
                    current = item;
                    i--;
                    path.Push(item);
                    break;
                }
            }
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }


    public void Destroy()
    {
        gameController.ClearEnemyCell(gameObject);
        Destroy(gameObject);
    }

    public void DrawCellsInRed(Material mat)
    {
        CellController[] cells = GameObject.FindObjectsOfType<CellController>();
        foreach (CellController c in cells)
        {
            if (c.conquedPlayer == name)
            {
                c.GetComponent<Renderer>().material = mat;
            }
        }
    }
}
