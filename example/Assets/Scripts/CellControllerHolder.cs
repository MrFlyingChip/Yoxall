using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellControllerHolder : MonoBehaviour {

    public static CellController[] cells = GameObject.FindObjectsOfType<CellController>();


    public static List<CellController> GetControllers(string tag)
    {
        List<CellController> cellsTag = new List<CellController>();
        for (int i = 0; i < cells.Length; i++)
        {
            if (cells[i].tag == tag) cellsTag.Add(cells[i]);
        }
        return cellsTag;
    }

}
