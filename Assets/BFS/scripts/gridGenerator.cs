using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
   * script by Chris O'Brien
   * this script is going to take in a prefab game object (grid node or similar) and spawn
   * in a regular square grid. the script will allow us to modify the width and height in the unity editor.
   */

public class gridGenerator : MonoBehaviour
{

    [SerializeField] GameObject gridCellPrefab;

    [SerializeField] int gridWidth;
    [SerializeField] int gridHeight;



    // Start is called before the first frame update
    void Start()
    {
        GenerateGrid(gridWidth, gridHeight);
    }

    private void GenerateGrid(int _width, int _height)
    {
        GameObject newGridCell;

        GridCell rootGridCell;


        bool rootGridCellSet = false;

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                newGridCell = Instantiate(gridCellPrefab, new Vector3(x, 1, y), gameObject.transform.rotation, gameObject.transform);
                newGridCell.name = "GridCell " + x.ToString() + "," + y.ToString();

                if (!rootGridCellSet)
                {
                    //set this newGridCell instance as the rootNode in the BFS algorithem
                    if(newGridCell.TryGetComponent<GridCell>(out rootGridCell))
                    {
                        BreadthFirstSearch.setRootCellEvent(rootGridCell);
                        rootGridCellSet = true;
                    }
                }
            }
        }
    }
}
