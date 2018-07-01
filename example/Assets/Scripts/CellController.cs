using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellController : MonoBehaviour {

    public string tag;

    public string nextTag;
    public string nextTag1;

    public int row;
    public int column;

    public CellController up;
    public CellController down;
    public CellController left;
    public CellController right;

    public GameObject tower;

    public bool marked;
    public bool conqued;

    public string conquedPlayer;

    public Vector3 playerPosition;
    public Vector3 playerRotation;

    public GameObject bonus;
    public bool canBePainted;
    public string canBeConquedPlayer;

    // Use this for initialization
    void Start () {
        FindUp();
        FindDown();
        FindLeft();
        FindRight();
        SetPlayerPosition();
    }
	
    private void FindUp()
    {
        if (up == null)
        {
            GameObject[] ups = GameObject.FindGameObjectsWithTag(tag);
            foreach (GameObject go in ups)
            {
                if ((go.GetComponent<CellController>().column == column) && (go.GetComponent<CellController>().row == (row - 1)))
                {
                    up = go.GetComponent<CellController>();
                }
            }
        }
        if(nextTag != string.Empty && up == null)
        {
            GameObject[] ups = GameObject.FindGameObjectsWithTag(nextTag);
            foreach (GameObject go in ups)
            {
                if (tag == "Face4")
                {
                    if ((go.GetComponent<CellController>().column == 24) && (go.GetComponent<CellController>().row == 25 - column))
                    {
                        up = go.GetComponent<CellController>();
                    }
                }
                else if (tag == "Face5")
                {
                    if ((go.GetComponent<CellController>().column == 1) && (go.GetComponent<CellController>().row == column))
                    {
                        up = go.GetComponent<CellController>();
                    }
                }
                else
                {
                    if ((go.GetComponent<CellController>().column == column) && (go.GetComponent<CellController>().row == 24))
                    {
                        up = go.GetComponent<CellController>();
                    }
                }
            }
        }
    }

    private void FindDown()
    {
        if (down == null)
        {
            GameObject[] downs = GameObject.FindGameObjectsWithTag(tag);
            foreach (GameObject go in downs)
            {
                if ((go.GetComponent<CellController>().column == column) && (go.GetComponent<CellController>().row == (row + 1)))
                {
                    down = go.GetComponent<CellController>();
                }
            }
        }
        if (nextTag != string.Empty && down == null)
        {
            GameObject[] downs = GameObject.FindGameObjectsWithTag(nextTag);
            foreach (GameObject go in downs)
            {
                if (tag == "Face4")
                {
                    if ((go.GetComponent<CellController>().column == 24) && (go.GetComponent<CellController>().row == column))
                    {
                        down = go.GetComponent<CellController>();
                    }
                }
                else if (tag == "Face5")
                {
                    if ((go.GetComponent<CellController>().column == 1) && (go.GetComponent<CellController>().row == 25 - column))
                    {
                        down = go.GetComponent<CellController>();
                    }
                }
                else
                {
                    if ((go.GetComponent<CellController>().column == column) && (go.GetComponent<CellController>().row == 1))
                    {
                        down = go.GetComponent<CellController>();
                    }
                }
            }
        }
    }

    private void FindLeft()
    {
        if (left == null)
        {
            GameObject[] lefts = GameObject.FindGameObjectsWithTag(tag);
            foreach (GameObject go in lefts)
            {
                if ((go.GetComponent<CellController>().column == (column - 1)) && (go.GetComponent<CellController>().row == row))
                {
                    left = go.GetComponent<CellController>();
                }
            }
        }
        if (nextTag1 != string.Empty && left == null)
        {
            GameObject[] lefts = GameObject.FindGameObjectsWithTag(nextTag1);
            foreach (GameObject go in lefts)
            {
                if (tag == "Face1")
                {
                    if ((go.GetComponent<CellController>().column == 25 - row) && (go.GetComponent<CellController>().row == 24))
                    {
                        left = go.GetComponent<CellController>();
                    }
                }
                else if(tag == "Face5")
                {
                    if ((go.GetComponent<CellController>().column == 1) && (go.GetComponent<CellController>().row == 25 - row))
                    {
                        left = go.GetComponent<CellController>();
                    }
                }
                else if (tag == "Face2")
                {
                    if ((go.GetComponent<CellController>().column == 1) && (go.GetComponent<CellController>().row == 25 - row))
                    {
                        left = go.GetComponent<CellController>();
                    }
                }
                else if (tag == "Face6")
                {
                    if ((go.GetComponent<CellController>().column == row) && (go.GetComponent<CellController>().row == 1))
                    {
                        left = go.GetComponent<CellController>();
                    }
                }
                else
                {
                    if ((go.GetComponent<CellController>().column == 24) && (go.GetComponent<CellController>().row == row))
                    {
                        left = go.GetComponent<CellController>();
                    }
                }
            }
        }
    }

    private void FindRight()
    {
        if (right == null)
        {
            GameObject[] rights = GameObject.FindGameObjectsWithTag(tag);
            foreach (GameObject go in rights)
            {
                if ((go.GetComponent<CellController>().column == (column + 1)) && (go.GetComponent<CellController>().row == row))
                {
                    right = go.GetComponent<CellController>();
                }
            }
        }
        if (nextTag1 != string.Empty && right == null)
        {
            GameObject[] rights = GameObject.FindGameObjectsWithTag(nextTag1);
            foreach (GameObject go in rights)
            {
                if (tag == "Face1")
                {
                    if ((go.GetComponent<CellController>().column == row) && (go.GetComponent<CellController>().row == 24))
                    {
                        right = go.GetComponent<CellController>();
                    }
                }
                else if (tag == "Face4")
                {
                    if ((go.GetComponent<CellController>().column == 24) && (go.GetComponent<CellController>().row == 25 - row))
                    {
                        right = go.GetComponent<CellController>();
                    }
                }
                else if(tag == "Face6")
                {
                    if ((go.GetComponent<CellController>().column == 25 - row) && (go.GetComponent<CellController>().row == 1))
                    {
                        right = go.GetComponent<CellController>();
                    }
                }
                else if (tag == "Face2")
                {
                    if ((go.GetComponent<CellController>().column == 24) && (go.GetComponent<CellController>().row == 25 - row))
                    {
                        right = go.GetComponent<CellController>();
                    }
                }
                else
                {
                    if ((go.GetComponent<CellController>().column == 1) && (go.GetComponent<CellController>().row == row))
                    {
                        right = go.GetComponent<CellController>();
                    }
                }
            }
        }
    }

    private void SetPlayerPosition()
    {
        playerRotation = Vector3.zero;
        if (tag == "Face3")
        {
            playerPosition = new Vector3(-1.4375f + (column - 1) * 0.125f, 1.4375f - (row - 1) * 0.125f, 1.48f);
            CorrectPositionFace3();
        }
        else if (tag == "Face2")
        {
            playerPosition = new Vector3(-1.4375f + (column - 1) * 0.125f, -1.4375f + (row - 1) * 0.125f, -1.48f);
            CorrectPositionFace2();
        }
        else if (tag == "Face4")
        {
            playerPosition = new Vector3(1.48f, 1.4375f - (row - 1) * 0.125f, 1.4375f - (column - 1) * 0.125f);
            CorrectPositionFace4();
        }
        else if (tag == "Face5")
        {
            playerPosition = new Vector3(-1.48f, 1.4375f - (row - 1) * 0.125f, -1.4375f + (column - 1) * 0.125f);
            CorrectPositionFace5();
        }
        else if (tag == "Face1")
        {
            playerPosition = new Vector3(-1.4375f + (column - 1) * 0.125f, -1.48f, 1.4375f - (row - 1) * 0.125f);
            CorrectPositionFace1();
        }
        else if (tag == "Face6")
        {
            playerPosition = new Vector3(-1.4375f + (column - 1) * 0.125f, 1.48f, -1.4375f + (row - 1) * 0.125f);
            CorrectPositionFace6();
        }
    }

    private void CorrectPositionFace1()
    {
        if (column == 22) Add(new Vector3(0, 0, 4), new Vector3(0.0125f, 0.005f, 0));
        if (column == 23) Add(new Vector3(0, 0, 13), new Vector3(-0.0125f, 0.02f, 0));
        if (column == 24) Add(new Vector3(0, 0, 30), new Vector3(-0.0575f, 0.0525f, 0));
        if (column == 3) Add(new Vector3(0, 0, -4), new Vector3(-0.0125f, 0.005f, 0));
        if (column == 2) Add(new Vector3(0, 0, -13), new Vector3(0.0125f, 0.02f, 0));
        if (column == 1) Add(new Vector3(0, 0, -30), new Vector3(0.0575f, 0.0525f, 0));
        if (row == 22) Add(new Vector3(4, 0, 0), new Vector3(0, 0.005f, -0.0125f));
        if (row == 23) Add(new Vector3(13, 0, 0), new Vector3(0, 0.02f, 0.0125f));
        if (row == 24) Add(new Vector3(30, 0, 0), new Vector3(0, 0.0525f, 0.0575f));
        if (row == 3) Add(new Vector3(-4, 0, 0), new Vector3(0, 0.005f, 0.0125f));
        if (row == 2) Add(new Vector3(-13, 0, 0), new Vector3(0, 0.02f, -0.0125f));
        if (row == 1) Add(new Vector3(-30, 0, 0), new Vector3(0, 0.0525f, -0.0575f));
    }
    private void CorrectPositionFace2()
    { 
        if (column == 22) Add(new Vector3(0, -4, 0), new Vector3(0.0125f, 0, 0.005f));
        if (column == 23) Add(new Vector3(0, -13, 0), new Vector3(-0.0125f, 0, 0.02f));
        if (column == 24) Add(new Vector3(0, -30, 0), new Vector3(-0.0575f, 0, 0.0525f));
        if (column == 3) Add(new Vector3(0, 4, 0), new Vector3(-0.0125f, 0, 0.005f));
        if (column == 2) Add(new Vector3(0, 13, 0), new Vector3(0.0125f, 0, 0.02f));
        if (column == 1) Add(new Vector3(0, 30, 0), new Vector3(0.0575f, 0, 0.0525f));
        if (row == 22) Add(new Vector3(4, 0, 0), new Vector3(0, 0.0125f, 0.005f));
        if (row == 23) Add(new Vector3(13, 0, 0), new Vector3(0, -0.0125f, 0.02f));
        if (row == 24) Add(new Vector3(30, 0, 0), new Vector3(0, -0.0575f, 0.0525f));
        if (row == 3) Add(new Vector3(-4, 0, 0), new Vector3(0, -0.0125f, 0.005f));
        if (row == 2) Add(new Vector3(-13, 0, 0), new Vector3(0, 0.0125f, 0.02f));
        if (row == 1) Add(new Vector3(-30, 0, 0), new Vector3(0, 0.0575f, 0.0525f));
    }
    private void CorrectPositionFace3()
    {
        if (column == 22) Add(new Vector3(0, 4, 0), new Vector3(0.0125f, 0, -0.005f));
        if (column == 23) Add(new Vector3(0, 13, 0), new Vector3(-0.0125f, 0, -0.02f));
        if (column == 24) Add(new Vector3(0, 30, 0), new Vector3(-0.0575f, 0, -0.0525f));
        if (column == 3) Add(new Vector3(0, -4, 0), new Vector3(-0.0125f, 0, -0.005f));
        if (column == 2) Add(new Vector3(0, -13, 0), new Vector3(0.0125f, 0, -0.02f));
        if (column == 1) Add(new Vector3(0, -30, 0), new Vector3(0.0575f, 0, -0.0525f));
        if (row == 22) Add(new Vector3(4, 0, 0), new Vector3(0, -0.0125f, -0.005f));
        if (row == 23) Add(new Vector3(13, 0, 0), new Vector3(0, 0.0125f, -0.02f));
        if (row == 24) Add(new Vector3(30, 0, 0), new Vector3(0, 0.0575f, -0.0525f));
        if (row == 3) Add(new Vector3(-4, 0, 0), new Vector3(0, 0.0125f, -0.005f));
        if (row == 2) Add(new Vector3(-13, 0, 0), new Vector3(0, -0.0125f, -0.02f));
        if (row == 1) Add(new Vector3(-30, 0, 0), new Vector3(0, -0.0575f, -0.0525f));
    }
    private void CorrectPositionFace4()
    {
        if (column == 22) Add(new Vector3(0, 4, 0), new Vector3(-0.005f, 0, -0.0125f));
        if (column == 23) Add(new Vector3(0, 13, 0), new Vector3(-0.02f, 0, 0.0125f));
        if (column == 24) Add(new Vector3(0, 30, 0), new Vector3(-0.0525f, 0, 0.0575f));
        if (column == 3) Add(new Vector3(0, -4, 0), new Vector3(-0.005f, 0, 0.0125f));
        if (column == 2) Add(new Vector3(0, -13, 0), new Vector3(-0.02f, 0, -0.0125f));
        if (column == 1) Add(new Vector3(0, -30, 0), new Vector3(-0.0525f, 0, -0.0575f));
        if (row == 22) Add(new Vector3(0, 0, -4), new Vector3(-0.005f, -0.0125f, 0));
        if (row == 23) Add(new Vector3(0, 0, -13), new Vector3(-0.02f, 0.0125f, 0));
        if (row == 24) Add(new Vector3(0, 0, -30), new Vector3(-0.0525f, 0.0575f, 0));
        if (row == 3) Add(new Vector3(0, 0, 4), new Vector3(-0.005f, 0.0125f, 0));
        if (row == 2) Add(new Vector3(0, 0, 13), new Vector3(-0.02f, -0.0125f, 0));
        if (row == 1) Add(new Vector3(0, 0, 30), new Vector3(-0.0525f, -0.0575f, 0));
    }
    private void CorrectPositionFace5()
    {
        if (column == 22) Add(new Vector3(0, 4, 0), new Vector3(0.005f, 0, 0.0125f));
        if (column == 23) Add(new Vector3(0, 13, 0), new Vector3(0.02f, 0, -0.0125f));
        if (column == 24) Add(new Vector3(0, 30, 0), new Vector3(0.0525f, 0, -0.0575f));
        if (column == 3) Add(new Vector3(0, -4, 0), new Vector3(0.005f, 0, -0.0125f));
        if (column == 2) Add(new Vector3(0, -13, 0), new Vector3(0.02f, 0, 0.0125f));
        if (column == 1) Add(new Vector3(0, -30, 0), new Vector3(0.0525f, 0, 0.0575f));
        if (row == 22) Add(new Vector3(0, 0, 4), new Vector3(0.005f, -0.0125f, 0));
        if (row == 23) Add(new Vector3(0, 0, 13), new Vector3(0.02f, 0.0125f, 0));
        if (row == 24) Add(new Vector3(0, 0, 30), new Vector3(0.0525f, 0.0575f, 0));
        if (row == 3) Add(new Vector3(0, 0, -4), new Vector3(0.005f, 0.0125f, 0));
        if (row == 2) Add(new Vector3(0, 0, -13), new Vector3(0.02f, -0.0125f, 0));
        if (row == 1) Add(new Vector3(0, 0, -30), new Vector3(0.0525f, -0.0575f, 0));
    }
    private void CorrectPositionFace6()
    {
        if (column == 22) Add(new Vector3(0, 0, -4), new Vector3(0.0125f, -0.005f, 0));
        if (column == 23) Add(new Vector3(0, 0, -13), new Vector3(-0.0125f, -0.02f, 0));
        if (column == 24) Add(new Vector3(0, 0, -30), new Vector3(-0.0575f, -0.0525f, 0));
        if (column == 3) Add(new Vector3(0, 0, 4), new Vector3(-0.0125f, -0.005f, 0));
        if (column == 2) Add(new Vector3(0, 0, 13), new Vector3(0.0125f, -0.02f, 0));
        if (column == 1) Add(new Vector3(0, 0, 30), new Vector3(0.0575f, -0.0525f, 0));
        if (row == 22) Add(new Vector3(4, 0, 0), new Vector3(0, -0.005f, 0.0125f));
        if (row == 23) Add(new Vector3(13, 0, 0), new Vector3(0, -0.02f, -0.0125f));
        if (row == 24) Add(new Vector3(30, 0, 0), new Vector3(0, -0.0525f, -0.0575f));
        if (row == 3) Add(new Vector3(-4, 0, 0), new Vector3(0, -0.005f, -0.0125f));
        if (row == 2) Add(new Vector3(-13, 0, 0), new Vector3(0, -0.02f, 0.0125f));
        if (row == 1) Add(new Vector3(-30, 0, 0), new Vector3(0, -0.0525f, 0.0575f));
    }
    private void Add(Vector3 rotation, Vector3 position)
    {
        playerRotation += rotation;
        playerPosition += position;
    }

    public void ResetCell(Material materialCell, Material materialTower, Material light)
    {
        marked = false;
        conqued = false;
        conquedPlayer = null;
        ChangeMaterial(materialCell);
        ChangeTowerMaterial(materialTower, light);
    }

    public void ChangeMaterial(Material material)
    {
        GetComponent<Renderer>().material = material;
    }

    public void ChangeTowerMaterial(Material material, Material light)
    {
        if (tower != null)
        {
            tower.GetComponentInChildren<AnimateTiledTexture>().GetComponent<Renderer>().material = material;
            Material[] mat = tower.GetComponent<Renderer>().materials;
            mat[1] = light;
            tower.GetComponent<Renderer>().materials = mat;
        }
    }

    public void SetCellMarked(GameObject player)
    {
        marked = true;
        conquedPlayer = player.name;
    }

    public void SetCellMarked(GameObject player, Material markedMaterial)
    {
        marked = true;
        conquedPlayer = player.name;
        ChangeMaterial(markedMaterial);
    }

    public void SetCellConqued(GameObject player, Material conquedCellMaterial, Material conquedTowerMaterial, Material light)
    {
        marked = false;
        conqued = true;
        conquedPlayer = player.name;
        ChangeMaterial(conquedCellMaterial);
        ChangeTowerMaterial(conquedTowerMaterial, light);
    }

    public List<CellController> GetNeighbors()
    {
        List<CellController> list = new List<CellController>() { up, left, down, right};
        return list;
    }

}
