using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpawner : MonoBehaviour {

    public float minSpawnTime;
    public float maxSpawnTime;

    public GameObject[] bonuses;

    public IEnumerator SpawnBonusOne()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
            CellController[] cells = GameObject.FindObjectsOfType<CellController>();
            CellController cell = cells[Random.Range(0, cells.Length)];
            do
            {
                cell = cells[Random.Range(0, cells.Length)];
            } while (cell.bonus != null || cell.tower != null || cell.row > 20 || cell.column > 20 || cell.row < 4 || cell.column < 4);
            GameObject go = Instantiate(bonuses[0], cell.playerPosition, Quaternion.Euler(cell.playerRotation)) as GameObject;
            cell.bonus = go;
        }   
    }
    public IEnumerator SpawnBonusTwo()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnTime*10, maxSpawnTime*10));
            CellController[] cells = GameObject.FindObjectsOfType<CellController>();
            CellController cell = cells[Random.Range(0, cells.Length)];
            do
            {
                cell = cells[Random.Range(0, cells.Length)];
            } while (cell.bonus != null || cell.tower != null || cell.row > 20 || cell.column > 20 || cell.row < 4 || cell.column < 4);
            GameObject go = Instantiate(bonuses[1], cell.playerPosition, Quaternion.Euler(cell.playerRotation)) as GameObject;
            cell.bonus = go;
        }
    }
}
