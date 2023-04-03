using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/*Script by Chris O'Brien
 * script will be attached to GridCell prefab object
 * will contain methods for changing the color when mouse goes over it and
 * automatically setting up neighbours etc
 */

public class GridCell : MonoBehaviour
{

    private MeshRenderer meshRenderer;

    [SerializeField] Material greenMat;
    [SerializeField] Material redMat;
    [SerializeField] Material blueMat;

    public List<GridCell> neighbours = new List<GridCell>();

    [SerializeField] List<Vector3> neighbourDirection = new List<Vector3>();
    public GridCell pathCell { get; private set; }

    private bool activeGridCell;

    private void Awake()
    {
        activeGridCell = false;

        if(!TryGetComponent<MeshRenderer>(out meshRenderer))
        {
            Debug.Log("you need to attach a MeshRenderer to this object");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer.material = greenMat;

        FindNeighbours();


        BreadthFirstSearch.breadthFirstSearchEvent += ClearPathCell;
        BreadthFirstSearch.clearColourFromCellsEvent += ResetColour;
    }

    // Update is called once per frame
    void Update()
    {
        if(activeGridCell)
        {
            if(Input.GetMouseButtonDown(0))
            {
                Debug.Log("selected " + gameObject.name + "as destination");
                BreadthFirstSearch.setDestinationCellEvent(this);
            }
        }
    }
    
    private void FindNeighbours()
    {
        RaycastHit hit;
        GridCell gridcell;

        for(int i =0; i < neighbourDirection.Count; i++)
        {
            if(Physics.Raycast(transform.position, neighbourDirection[i], out hit, 2f))
            {
                if(hit.collider.TryGetComponent<GridCell>(out gridcell))
                {
                    neighbours.Add(gridcell);
                }
            }
        }
    }    

    private void ClearPathCell()
    {
        pathCell = null;
    }

    public void SetPathCell(GridCell _pathCell)
    {
        pathCell = _pathCell;
    }

    private void ResetColour()
    {
        meshRenderer.material = greenMat;
    }

    public void SetColour(int _choice)
    {
        if(_choice == 0)
        {
            meshRenderer.material = greenMat;
        }
        else if(_choice == 1)
        {
            meshRenderer.material = redMat;
        }
        else if(_choice == 2)
        {
            meshRenderer.material = blueMat;
        }
    }

    private void OnMouseEnter()
    {
        meshRenderer.material = blueMat;
        activeGridCell = true;
    }

    private void OnMouseExit()
    {
        meshRenderer.material = greenMat;
        activeGridCell = false;
    }


    private void OnDestroy()
    {
        BreadthFirstSearch.breadthFirstSearchEvent -= ClearPathCell;
        BreadthFirstSearch.clearColourFromCellsEvent -= ResetColour;
    }

}
