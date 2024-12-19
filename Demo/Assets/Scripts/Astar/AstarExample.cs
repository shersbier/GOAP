using System.Collections;
using System.Collections.Generic;
using Astar;
using UnityEngine;

public class AstarExample : MonoBehaviour
{
    public int gridWidth = 10;
    public int gridHeight = 10;
    public float cellSize = 1;
    public Vector2 originPosition = new Vector2(0,0);
    public AStarAlgorithm algorithm;
    public AStar_Grid grid;
    public Node startNode;
    public Node endNode;
    public List<Node> path;
    void Start()
    {
        this.grid = new AStar_Grid(gridWidth, gridHeight, cellSize, originPosition);
        this.algorithm = new AStarAlgorithm(this.grid);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Hit");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit)
            {
                Debug.Log("Hit"+hit.point);
                Vector2 position = hit.point;
                Node node = this.grid.GetNodeFromWorldPosition(position);
                if (node != null)
                {
                    if (this.startNode == null)
                    {
                        this.startNode = node;
                    }
                    else if (this.endNode == null)
                    {
                        endNode = node;
                        path = this.algorithm.FindPath(this.startNode.position, this.endNode.position);
                    }
                }
            }
        }
    }
    
    void OnDrawGizmos()
    {
        if (this.grid != null)
        {
            foreach (var node in this.grid.nodes)
            {
                if (node != null)
                {
                    Gizmos.color = node == this.startNode || node == this.endNode ? Color.red : Color.white;
                    Gizmos.DrawCube(node.position, new Vector3(this.grid.cellSize,this.grid.cellSize,0));
                }

                if (this.path != null)
                {
                    foreach (var n in this.path)
                    {
                        Gizmos.color = Color.cyan;
                        Gizmos.DrawCube(n.position, Vector3.one * this.grid.cellSize);
                    }
                }
            }
        }
    }
}
