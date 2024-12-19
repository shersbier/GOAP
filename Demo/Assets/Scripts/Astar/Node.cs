using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Astar
{
    public class Node
    {
        public Vector2 position;
        public float gCost;
        public float hCost;
        public float fCost => this.gCost + this.hCost;
        public Node parent;
        
        public Node(Vector2 pos,float gCost,float hCost,Node parent)
        {
            this.position = pos;
            this.gCost = gCost;
            this.hCost = hCost;
            this.parent = parent;
        }
    }
    
}

