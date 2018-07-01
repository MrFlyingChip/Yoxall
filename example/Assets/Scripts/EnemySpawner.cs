using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    public GameObject enemy;
    public GameController gameController;

    public void SpawnEnemy()
    {
        int n = Random.Range(3, 5);
        for(int i = 0; i < n; i++)
        {
            Spawn("Enemy" + i);
        }        
        }

    public void Spawn(string name)
    {
        CellController[] cells = GameObject.FindObjectsOfType<CellController>();
        CellController cell = cells[Random.Range(0, cells.Length)];
        int i = 0;
        bool canSpawn = true;
        do
        {
            i++;
            cell = cells[Random.Range(0, cells.Length)];
            if (i > 8)
            {
                canSpawn = false;
                break;
            }
        } while (cell.bonus != null || cell.tower != null || cell.tag == "Face3" || cell.conquedPlayer == string.Empty);
        if (canSpawn)
        {
            GameObject go = Instantiate(enemy, cell.playerPosition, Quaternion.Euler(cell.playerRotation)) as GameObject;
            go.name = name;
            go.GetComponent<EnemyController>().gameController = gameController;
            go.GetComponent<EnemyController>().startPosition = cell;
            go.GetComponent<EnemyController>().Reset();
        }
    }

}



