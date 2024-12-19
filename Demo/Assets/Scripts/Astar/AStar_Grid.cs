using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Astar
{
    public class AStar_Grid
    {
        public int width;
        public int height;
        public float cellSize;
        public Vector2 originPosition;
        public Node[,] nodes;
        
        public AStar_Grid(int width, int height, float cellSize, Vector2 originPosition)
        {
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;
            this.originPosition = originPosition;
            nodes = new Node[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    nodes[x, y] = new Node(new Vector2(x * cellSize + originPosition.x, y * cellSize + originPosition.y), float.MaxValue,float.MaxValue,null);
                }
            }
        }

        public Node GetNodeFromWorldPosition(Vector2 worldPosition)
        {
            //相下取整
            int x = Mathf.FloorToInt((worldPosition.x - originPosition.x) / cellSize);
            int y = Mathf.FloorToInt((worldPosition.y - originPosition.y) / cellSize);
            if (x < 0 || x >= width || y < 0 || y >= height)
            {
                return null;
            }
            return nodes[x, y];
        }

        public Vector2Int? GetWorldPosition(Vector2 pos)
        {
            int x = Mathf.FloorToInt((pos.x - this.originPosition.x) / this.cellSize);
            int y = Mathf.FloorToInt((pos.y - this.originPosition.y) / this.cellSize);
            if (x < 0 || x >= this.width || y<0 || y >= this.height)
            {
                return null;
            }
            else
            {
                return new Vector2Int(x, y);
            }
        }
    }
    
}

