using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGOAP
{
    public class PlanNode
    {
        public WorldState State; // 当前状态
        public PlanNode Parent;  // 父节点
        public Action Action;    // 当前节点的动作
        public float Cost;       // 从起点到当前节点的代价
        public float Heuristic;  // 启发式估计

        public float TotalCost => Cost + Heuristic; // A* 的 f(n) = g(n) + h(n)

        public PlanNode(WorldState state, PlanNode parent, Action action, float cost, float heuristic)
        {
            State = state;
            Parent = parent;
            Action = action;
            Cost = cost;
            Heuristic = heuristic;
        }
    }
    
}


