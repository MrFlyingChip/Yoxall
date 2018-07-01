using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class CellBrush
{

    private CellController[] GetControllers(string face)
    {
        GameObject[] faceCells = GameObject.FindGameObjectsWithTag(face);
        CellController[] faceContr = new CellController[faceCells.Length];
        for (int i = 0; i < faceCells.Length; i++)
        {
            faceContr[i] = faceCells[i].GetComponent<CellController>();
        }
        return faceContr;
    }

    private CellController FindWithCoord(CellController[] array, int i, int j)
    {
        foreach (var item in array)
        {
            if (item.row == i && item.column == j)
            {
                return item;
            }
        }
        return null;
    }

    private void PaintList(List<CellController> cells, GameObject obj)
    {
        foreach (var item in cells)
        {
            if (!(item.conqued && item.conquedPlayer == obj.name))
            {
                item.SetCellMarked(obj);
            }
        }
    }

    private bool CheckCell(CellController cell, GameObject obj)
    {
        bool check = true;
        if (cell.column != 1)
        {
            if ((cell.right.conqued && cell.right.conquedPlayer == obj.name) && (cell.conqued && cell.conquedPlayer == obj.name))
            {
                check = false;
            }
            else if ((cell.left.conqued && cell.left.conquedPlayer == obj.name) && (cell.conqued && cell.conquedPlayer == obj.name))
            {
                check = false;
            }

            if ((cell.right.conquedPlayer != obj.name) && (cell.left.conquedPlayer == obj.name) && (cell.conquedPlayer == obj.name) && (cell.right.up.conquedPlayer == obj.name))
            { 
                check = true;
            }
            if ((cell.right.conquedPlayer != obj.name) && (cell.left.conquedPlayer == obj.name) && (cell.conquedPlayer == obj.name) && (cell.right.down.conquedPlayer == obj.name))
            {
                check = true;
            }
        }

        return check;
    }

    private List<string> PaintFace(string face, GameObject obj, PlayerController.SetConqued del)
    {
        List<CellController> faceCells = CellControllerHolder.GetControllers(face);
        List<CellController> cellsToPaint = new List<CellController>();
        CellController first = faceCells.Find(item => item.row == 1 && item.column == 1);
        List<string> faceToRepaint = new List<string>();   
        for (int i = 1; i < 25; i++)
        {
            bool fl = false;
            first = faceCells.Find(item => item.row == i && item.column == 1);
            cellsToPaint = new List<CellController>();
            while (first.tag == face)
            {
                if (first.left.canBePainted && first.left.canBeConquedPlayer == obj.name && first.column == 1)
                {
                    if(!faceToRepaint.Contains(first.left.tag)) faceToRepaint.Add(first.left.tag);
                    cellsToPaint.Add(first.left);
                    fl = true;
                }
                if(first.row == 1 && first.up.canBePainted && first.up.canBeConquedPlayer == obj.name)
                {
                    if (!faceToRepaint.Contains(first.up.tag)) faceToRepaint.Add(first.up.tag);
                }
                if(first.left.conquedPlayer == obj.name && first.column == 1)
                {
                    fl = true;
                }
                if (fl)
                {
                    if(first.conquedPlayer == obj.name && first.conqued)
                    {
                        fl = false;
                    }
                }
                else
                {
                    if (first.conquedPlayer == obj.name && CheckCell(first, obj))
                    {
                        fl = true;
                    }
                }
                if (fl)
                {
                    first.canBePainted = true;
                    first.canBeConquedPlayer = obj.name;
                    cellsToPaint.Add(first);
                }
                first = first.right;
                if(fl && first.conquedPlayer == obj.name)
                {
                    PaintList(cellsToPaint, obj);
                }
            }
        }
        return faceToRepaint;
    }

    public void PaintCells(GameObject obj, PlayerController.SetConqued del, List<string> faces)
    {
        
        foreach (var item in faces)
        {
            List<string> face = PaintFace(item, obj, del);
            if (face.Count > 0)
            {
                foreach (var faceP in face)
                {
                    PaintFace(faceP, obj, del);
                }            
            }
        }
        foreach (var item in faces)
        {
            List<CellController> faceCells = CellControllerHolder.GetControllers(item).FindAll(c => c.canBePainted && c.canBeConquedPlayer == obj.name);

            foreach (var cell in faceCells)
            {            
                cell.canBePainted = false;
                cell.canBeConquedPlayer = null;                         
            }
        }
        del.Invoke();
    }
}

